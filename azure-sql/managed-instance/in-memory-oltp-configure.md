---
title: In-memory OLTP improves SQL transaction performance
description: Adopt In-memory OLTP to improve transactional performance in an existing database in Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 12/12/2023
ms.service: azure-sql-managed-instance
ms.subservice: performance
ms.topic: how-to
monikerRange: "=azuresql||=azuresql-mi"
---
# Use in-memory OLTP in Azure SQL Managed Instance to improve your application performance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/in-memory-oltp-configure.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](in-memory-oltp-configure.md?view=azuresql-mi&preserve-view=true)

[In-memory OLTP](in-memory-oltp-overview.md) can be used to improve the performance of transaction processing, data ingestion, and transient data scenarios. The Business Critical service tier includes a certain amount of **Max In-Memory OLTP memory**, a [limit determined by the number of vCores](resource-limits.md?view=azuresql-mi&preserve-view=true).

Follow these steps to adopt in-memory OLTP in an existing database in Azure SQL Managed Instance.

## Step 1: Identify objects to migrate to in-memory OLTP

[SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) includes a **Transaction Performance Analysis Overview** report that you can run against a database with an active workload. The report identifies tables and stored procedures that are candidates for migration to in-memory OLTP.

In SSMS, to generate the report:

- In the **Object Explorer**, right-click your database node.
- Select **Reports** > **Standard Reports** > **Transaction Performance Analysis Overview**.

For more information on assessing the benefits of in-memory OLTP, see [Determining if a table or stored procedure should be ported to in-memory OLTP](/sql/relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp?view=azuresqldb-mi-current&preserve-view=true).

## Step 2: Create a comparable test database

Suppose the report indicates your database has a table that would benefit from being converted to a memory-optimized table. We recommend that you first test to confirm the indication by testing.

You need a test copy of your production database. The test database should be at the same service tier (Business Critical) and vCore count as your production database.

To ease testing, tweak your test database as follows:

1. Connect to the test database by using [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).
1. To avoid needing the `WITH (SNAPSHOT)` option in queries, set the current database's  `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT` option, as shown in the following T-SQL statement:

   ```sql
   ALTER DATABASE CURRENT
    SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;
   ```

## Step 3: Migrate tables

You must create and populate a memory-optimized copy of the table you want to test. You can create it by using either:

- The handy [Memory Optimization Wizard in SSMS](#memory-optimization-wizard-in-ssms).
- Use [T-SQL commands](#manual-t-sql).

### Memory Optimization Wizard in SSMS

To use this migration option:

1. Connect to the test database with SSMS.
1. In the **Object Explorer**, right-click on the table, and then select **Memory Optimization Advisor**.

   The **Table Memory Optimizer Advisor** wizard is displayed.
1. In the wizard, select **Migration validation** (or the **Next** button) to see if the table has any unsupported features that are unsupported in memory-optimized tables. For more information, see:

   * The *memory optimization checklist* in [Memory Optimization Advisor](/sql/relational-databases/in-memory-oltp/memory-optimization-advisor).
   * [Transact-SQL Constructs Not Supported by in-memory OLTP](/sql/relational-databases/in-memory-oltp/transact-sql-constructs-not-supported-by-in-memory-oltp).
   * [Migrating to in-memory OLTP](/sql/relational-databases/in-memory-oltp/plan-your-adoption-of-in-memory-oltp-features-in-sql-server).

1. If the table has no unsupported features, the advisor can perform the actual schema and data migration for you.

### Manual T-SQL

To use this migration option:

1. Connect to your test database by using SSMS (or a similar utility).
1. Obtain the complete T-SQL script for your table and its indexes.
   * In SSMS, right-click your table node.
   * Select **Script Table As** > **CREATE To** > **New Query Window**.
1. In the script window, add `WITH (MEMORY_OPTIMIZED = ON)` to the `CREATE TABLE` statement.
1. If there is a CLUSTERED index, change it to NONCLUSTERED.
1. Rename the existing table by using [sp_rename](/sql/relational-databases/system-stored-procedures/sp-rename-transact-sql?view=azuresqldb-mi-current&preserve-view=true).
1. Create the new memory-optimized copy of the table by running your edited `CREATE TABLE` script.
1. Copy the data to your memory-optimized table by using `INSERT...SELECT * INTO`:
    ```sql
    INSERT INTO [<new_memory_optimized_table>]
            SELECT * FROM [<old_disk_based_table>];
    ```

## Step 4 (optional): Migrate stored procedures

The in-memory feature can also modify a stored procedure for improved performance.

### Considerations with natively compiled stored procedures

A natively compiled stored procedure must have the following options on its T-SQL `WITH` clause:

- [NATIVE_COMPILATION](/sql/relational-databases/in-memory-oltp/native-compilation-of-tables-and-stored-procedures?view=azuresqldb-mi-current&preserve-view=true#native-compilation-of-stored-procedures): meaning the Transact-SQL statements in the procedure are all compiled to native code for efficient execution.
- [SCHEMABINDING](/sql/t-sql/statements/create-view-transact-sql?view=azuresqldb-mi-current&preserve-view=true#schemabinding): meaning tables that the stored procedure cannot have their column definitions changed in any way that would affect the stored procedure, unless you drop the stored procedure.

A native module must use one big [ATOMIC block](/sql/relational-databases/in-memory-oltp/atomic-blocks-in-native-procedures) for transaction management. There is no role for an explicit `BEGIN TRANSACTION` or `ROLLBACK TRANSACTION.` If your code detects a violation of a business rule, it can terminate the atomic block with a [THROW](/sql/t-sql/language-elements/throw-transact-sql) statement.

### Typical CREATE PROCEDURE for natively compiled

Typically the T-SQL to create a natively compiled stored procedure is similar to the following template:

```sql
CREATE PROCEDURE schemaname.procedurename
    @param1 type1, ...
    WITH NATIVE_COMPILATION, SCHEMABINDING
    AS
        BEGIN ATOMIC WITH
            (TRANSACTION ISOLATION LEVEL = SNAPSHOT,
            LANGUAGE = N'<desired sys.syslanuages.sysname value>'
            )
        ...
        END;
```

- For the `TRANSACTION_ISOLATION_LEVEL`, SNAPSHOT is the most common value for the natively compiled stored procedure. However, a subset of the other values is also supported:
  * REPEATABLE READ
  * SERIALIZABLE
- The `LANGUAGE` value must be present in the `sys.syslanguages` view, in the `name` column. For example, `N'us_english'`.

### How to migrate a stored procedure

The migration steps are:

1. Obtain the `CREATE PROCEDURE` script to the regular interpreted stored procedure.
1. Rewrite its header to match the previous template.
1. Determine whether the stored procedure T-SQL code uses any features that are not supported for natively compiled stored procedures. Implement workarounds if necessary. For more information, see [Migration issues for natively compiled stored procedures](/sql/relational-databases/in-memory-oltp/a-guide-to-query-processing-for-memory-optimized-tables?view=azuresqldb-mi-current&preserve-view=true).
1. Rename the old stored procedure by using [sp_rename](/sql/relational-databases/system-stored-procedures/sp-rename-transact-sql?view=azuresqldb-mi-current&preserve-view=true). Or simply DROP it.
1. Run your edited `CREATE PROCEDURE` T-SQL script.

## Step 5: Run your workload in test

Run a workload in your test database that is similar to the workload that runs in your production database. This should reveal the performance gain achieved by your use of the in-memory feature for tables and stored procedures.

Major attributes of the workload are:

- Number of concurrent connections.
- Read/write ratio.

To tailor and run the test workload, consider using the handy ostress.exe tool.

To minimize network latency, run your test in the same Azure geographic region where the database exists.

## Step 6: Post-implementation monitoring

Consider monitoring the performance effects of your in-memory implementations in production:

- [Monitor in-memory storage](in-memory-oltp-monitor-space.md?view=azuresql-mi&preserve-view=true).
- [Monitoring using dynamic management views](monitoring-with-dmvs.md?view=azuresql-mi&preserve-view=true).

## Related content

- [In-memory sample in Azure SQL Managed Instance](in-memory-oltp-sample.md?view=azuresql-mi&preserve-view=true)
- [In-memory OLTP (In-memory Optimization)](/sql/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization?view=azuresqldb-mi-current&preserve-view=true)
- [A Guide to Query Processing for Memory-Optimized Tables](/sql/relational-databases/in-memory-oltp/a-guide-to-query-processing-for-memory-optimized-tables?view=azuresqldb-mi-current&preserve-view=true)
- [Memory Optimization Advisor](/sql/relational-databases/in-memory-oltp/memory-optimization-advisor?view=azuresqldb-mi-current&preserve-view=true)
