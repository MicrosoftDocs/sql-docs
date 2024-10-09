---
title: In-Memory OLTP improves SQL transaction performance
description: Adopt In-Memory OLTP to improve transactional performance in an existing database in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 09/20/2024
ms.service: azure-sql-database
ms.subservice: performance
ms.topic: how-to
ms.custom:
  - sqldbrb=2
monikerRange: "=azuresql||=azuresql-db"
---
# Use In-Memory OLTP in Azure SQL Database to improve your application performance

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](in-memory-oltp-configure.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/in-memory-oltp-configure.md?view=azuresql-mi&preserve-view=true)

[In-Memory OLTP](in-memory-oltp-overview.md) can be used to improve the performance of transaction processing, data ingestion, and transient data scenarios, without increasing the service objective of the database or elastic pool.

- Databases and elastic pools in the [Premium (DTU)](service-tiers-dtu.md) and [Business Critical (vCore)](service-tiers-sql-database-vcore.md) service tiers support In-Memory OLTP.
- The Hyperscale service tier supports a subset of In-Memory OLTP objects, but does not include memory-optimized tables. For more information, see [Hyperscale limitations](service-tier-hyperscale.md?view=azuresql&preserve-view=true#known-limitations).

Follow these steps to start using In-Memory OLTP in your existing databases.

## Step 1: Ensure you are using a Premium or Business Critical tier database

In-Memory OLTP is supported if the result from the following query is `1` (not `0`):

```sql
SELECT DATABASEPROPERTYEX(DB_NAME(), 'IsXTPSupported');
```

*XTP* stands for *Extreme Transaction Processing*, which is an informal name of the In-Memory OLTP feature.

## Step 2: Identify objects to migrate to In-Memory OLTP

[SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) includes a **Transaction Performance Analysis Overview** report that you can run against a database with an active workload. The report identifies tables and stored procedures that are candidates for migration to In-Memory OLTP.

To generate the report in SSMS:

- In the **Object Explorer**, right-click your database node.
- Select **Reports** > **Standard Reports** > **Transaction Performance Analysis Overview**.

For more information on assessing the benefits of In-Memory OLTP, see [Determining if a table or stored procedure should be ported to In-Memory OLTP](/sql/relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp?view=azuresqldb-current&preserve-view=true).

## Step 3: Create a comparable test database

Suppose the report indicates your database has a table that would benefit from being converted to a memory-optimized table. We recommend that you first test to confirm the indication by testing.

You need a test copy of your production database. The test database should be at the same service tier level as your production database.

To ease testing, tweak your test database as follows:

1. Connect to the test database by using [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).
1. To avoid needing the `WITH (SNAPSHOT)` option in queries, set the current database's  `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT` option, as shown in the following T-SQL statement:

   ```sql
   ALTER DATABASE CURRENT SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;
   ```

## Step 4: Migrate tables

You must create and populate a memory-optimized copy of the table you want to test. You can create it by using either:

- The [Memory Optimization Wizard in SSMS](#memory-optimization-wizard-in-ssms).
- Use [T-SQL commands](#manual-t-sql).

### Memory Optimization Wizard in SSMS

To use this migration option:

1. Connect to the test database with SSMS.
1. In the **Object Explorer**, right-click on the table, and then select **Memory Optimization Advisor**.

   The **Table Memory Optimizer Advisor** wizard is displayed.

1. In the wizard, select **Migration validation** (or the **Next** button) to see if the table has any features that are unsupported in memory-optimized tables. For more information, see:

   * The *memory optimization checklist* in [Memory Optimization Advisor](/sql/relational-databases/in-memory-oltp/memory-optimization-advisor).
   * [Transact-SQL Constructs Not Supported by In-Memory OLTP](/sql/relational-databases/in-memory-oltp/transact-sql-constructs-not-supported-by-in-memory-oltp).
   * [Migrating to In-Memory OLTP](/sql/relational-databases/in-memory-oltp/plan-your-adoption-of-in-memory-oltp-features-in-sql-server).

1. If the table has no unsupported features, the advisor can perform the actual schema and data migration for you.

### Manual T-SQL

To use this migration option:

1. Connect to your test database by using SSMS.
1. Obtain the complete T-SQL script for your table and its constraints and indexes.
   * In SSMS, right-click your table node.
   * Select **Script Table As** > **CREATE To** > **New Query Window**.
1. In the script window, add `WITH (MEMORY_OPTIMIZED = ON)` to the `CREATE TABLE` statement. For more information, see [Syntax for memory optimized tables](/sql/t-sql/statements/create-table-transact-sql#syntax-for-memory-optimized-tables).
1. If there is a CLUSTERED index, change it to NONCLUSTERED.
1. Rename the existing table by using [sp_rename](/sql/relational-databases/system-stored-procedures/sp-rename-transact-sql?view=azuresqldb-current&preserve-view=true).
1. Create the new memory-optimized copy of the table by running your edited `CREATE TABLE` script.
1. Copy the data to your memory-optimized table by using `INSERT...SELECT * INTO`:
    ```sql
    INSERT INTO [<new_memory_optimized_table>]
    SELECT * FROM [<old_disk_based_table>];
    ```

## Step 5 (optional): Migrate stored procedures

In-Memory OLTP also supports natively compiled stored procedures, which can improve T-SQL performance.

### Considerations with natively compiled stored procedures

A natively compiled stored procedure must have the following options in its T-SQL `WITH` clause:

- [NATIVE_COMPILATION](/sql/relational-databases/in-memory-oltp/native-compilation-of-tables-and-stored-procedures?view=azuresqldb-current&preserve-view=true#native-compilation-of-stored-procedures): meaning the Transact-SQL statements in the procedure are all compiled to native code for efficient execution.
- [SCHEMABINDING](/sql/t-sql/statements/create-view-transact-sql?view=azuresqldb-current&preserve-view=true#schemabinding): meaning that the tables referenced in the stored procedure cannot have their definitions changed in any way that would affect the stored procedure, unless you drop the stored procedure.

A natively compiled module must use one [ATOMIC block](/sql/relational-databases/in-memory-oltp/atomic-blocks-in-native-procedures) for transaction management. There is no use of explicit `BEGIN TRANSACTION` or `ROLLBACK TRANSACTION` statements. Your code can terminate the atomic block with a [THROW](/sql/t-sql/language-elements/throw-transact-sql) statement, for example if it detects a business rule violation.

### An example of a natively compiled stored procedure

The T-SQL to create a natively compiled stored procedure is similar to the following template:

```sql
CREATE PROCEDURE schemaname.procedurename
    @param1 type1, ...
    WITH NATIVE_COMPILATION, SCHEMABINDING
    AS
        BEGIN ATOMIC WITH
            (
            TRANSACTION ISOLATION LEVEL = SNAPSHOT,
            LANGUAGE = N'<desired sys.syslanuages.sysname value>'
            )
        ...
        END;
```

- For the `TRANSACTION_ISOLATION_LEVEL`, `SNAPSHOT` is the most common value for the natively compiled stored procedures. However, a subset of the other values is also supported:
  * `REPEATABLE READ`
  * `SERIALIZABLE`
- The `LANGUAGE` value must be present in the `sys.syslanguages` view, in the `name` column. For example, `N'us_english'`.

### How to migrate a stored procedure to use native compilation

The migration steps are:

1. Obtain the `CREATE PROCEDURE` script to the regular (interpreted) stored procedure.
1. Rewrite its header to match the previous template.
1. Determine whether the stored procedure T-SQL code uses any features that are not supported for natively compiled stored procedures. Implement workarounds if necessary. For more information, see [Migration issues for natively compiled stored procedures](/sql/relational-databases/in-memory-oltp/a-guide-to-query-processing-for-memory-optimized-tables?view=azuresqldb-current&preserve-view=true).
1. Rename the old stored procedure by using [sp_rename](/sql/relational-databases/system-stored-procedures/sp-rename-transact-sql?view=azuresqldb-current&preserve-view=true), or drop it.
1. Execute your edited `CREATE PROCEDURE` T-SQL script.

## Step 6: Run your workload in test

Run a workload in your test database that is similar to the workload that runs in your production database. This should reveal the performance gain achieved by the use of In-Memory OLTP for tables and stored procedures.

Major attributes of the workload are:

- Number of concurrent connections.
- Read/write ratio.

To tailor and run the test workload, consider using the `ostress.exe` tool from the [RML Utilities](/troubleshoot/sql/tools/replay-markup-language-utility) group of tools. For more information, see [In-memory sample in Azure SQL Database](in-memory-oltp-sample.md?view=azuresql-db&preserve-view=true).

To minimize network latency, run `ostress.exe` in the same Azure region as the database.

## Step 7: Post-implementation monitoring

Consider monitoring the performance effects of your In-Memory OLTP implementation in production:

- [Monitor In-Memory OLTP storage](in-memory-oltp-monitor-space.md?view=azuresql-db&preserve-view=true).
- [Monitoring using dynamic management views](monitoring-with-dmvs.md?view=azuresql-db&preserve-view=true).

## Related content

- [In-Memory OLTP (In-memory Optimization)](/sql/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization?view=azuresqldb-current&preserve-view=true)
- [A Guide to Query Processing for Memory-Optimized Tables](/sql/relational-databases/in-memory-oltp/a-guide-to-query-processing-for-memory-optimized-tables?view=azuresqldb-current&preserve-view=true)
- [Memory Optimization Advisor](/sql/relational-databases/in-memory-oltp/memory-optimization-advisor?view=azuresqldb-current&preserve-view=true)
