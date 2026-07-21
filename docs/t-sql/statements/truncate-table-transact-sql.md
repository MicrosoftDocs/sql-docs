---
title: "TRUNCATE TABLE (Transact-SQL)"
description: TRUNCATE TABLE removes all rows from a table or specified partitions of a table.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 02/14/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "TRUNCATE"
  - "TRUNCATE TABLE"
  - "TRUNCATE_TSQL"
  - "TRUNCATE_TABLE_TSQL"
helpviewer_keywords:
  - "row removal [SQL Server], TRUNCATE TABLE statement"
  - "table truncating [SQL Server]"
  - "removing rows"
  - "TRUNCATE TABLE statement"
  - "deleting rows"
  - "dropping rows"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# TRUNCATE TABLE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

Removes all rows from a table or specified partitions of a table, without logging the individual row deletions. `TRUNCATE TABLE` is similar to the `DELETE` statement with no `WHERE` clause; however, `TRUNCATE TABLE` is faster and uses fewer system and transaction log resources.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, Fabric SQL database

```syntaxsql
TRUNCATE TABLE
    { database_name.schema_name.table_name | schema_name.table_name | table_name }
    [ WITH ( PARTITIONS ( { <partition_number_expression> | <range> }
    [ , ...n ] ) ) ]
[ ; ]

<range> ::=
<partition_number_expression> TO <partition_number_expression>
```

Syntax for Microsoft Fabric, Azure Synapse Analytics, and Parallel Data Warehouse.

```syntaxsql
TRUNCATE TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
[ ; ]
```

## Arguments

#### *database_name*

The name of the database.

#### *schema_name*

The name of the schema to which the table belongs.

#### *table_name*

The name of the table to truncate or from which all rows are removed. *table_name* must be a literal. *table_name* can't be the `OBJECT_ID()` function or a variable.

#### WITH ( PARTITIONS ( { <*partition_number_expression*> | <*range*> } [ , ...n ] ) )

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.

Specifies the partitions to truncate or from which all rows are removed. If the table isn't partitioned, the `WITH PARTITIONS` argument generates an error. If the `WITH PARTITIONS` clause isn't provided, the entire table is truncated.

`<partition_number_expression>` can be specified in the following ways:

- Provide the number of a partition, for example: `WITH (PARTITIONS (2))`

- Provide the partition numbers for several individual partitions separated by commas, for example: `WITH (PARTITIONS (1, 5))`

- Provide both ranges and individual partitions, for example: `WITH (PARTITIONS (2, 4, 6 TO 8))`

- `<range>` can be specified as partition numbers separated by the word `TO`, for example: `WITH (PARTITIONS (6 TO 8))`

To truncate a partitioned table, the table and indexes must be aligned (partitioned on the same partition function).

## Remarks

Compared to the `DELETE` statement, `TRUNCATE TABLE` has the following advantages:

- Less transaction log space is used.

  The `DELETE` statement removes rows one at a time and records an entry in the transaction log for each deleted row. `TRUNCATE TABLE` removes the data by deallocating the data pages used to store the table and index data and records only the page deallocations in the transaction log.

- Fewer locks are typically used.

  When the `DELETE` statement is executed using a row lock, each row in the table is locked for deletion. `TRUNCATE TABLE` always locks the table (including a schema (`SCH-M`) lock) and page, but not each row.

- Without exception, zero pages are left in the table or its indexes.

  After a `DELETE` statement is executed, the table can still contain empty pages. For example, empty pages in a heap can't be deallocated without at least an exclusive (`LCK_M_X`) table lock. If the delete operation doesn't use a table lock, the table (heap) might contain many empty pages. For indexes, the `DELETE` statement can leave empty pages behind. A background cleanup process then deallocates these pages.

`TRUNCATE TABLE` removes all rows from a table, but the table structure and its columns, constraints, indexes, and so on, remain. To remove the table definition in addition to its data, use the `DROP TABLE` statement.

If the table contains an identity column, the counter for that column is reset to the seed value defined for the column. If no seed was defined, the default value `1` is used. To retain the identity counter, use `DELETE` instead.

A `TRUNCATE TABLE` operation can be rolled back within a transaction.

In Fabric SQL database, truncating a table deletes all mirrored data from Fabric OneLake for that table.

### Deferred deallocation

When a table that uses 128 extents or more is truncated, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. Truncation occurs in two separate phases: logical and physical. In the logical phase, the existing allocation units used by the table and its indexes are marked for deallocation and locked until the transaction commits. In the physical phase, a background process removes the pages marked for deallocation. This means that the space released by `TRUNCATE TABLE` might not be available for new allocations immediately.

If [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md) is enabled, truncation uses separate logical and physical phases regardless of the number of extents.

## Limitations

You can't use `TRUNCATE TABLE` on tables that:

- Are referenced by a `FOREIGN KEY` constraint. You can truncate a table that has a foreign key that references itself.

- Participate in an indexed view.

- Are published by using transactional replication or merge replication.

- Are system-versioned temporal.

- Are referenced by an `EDGE` constraint.

For tables with one or more of these characteristics, use the `DELETE` statement instead.

`TRUNCATE TABLE` can't activate a trigger because the operation doesn't log individual row deletions. For more information, see [CREATE TRIGGER (Transact-SQL)](create-trigger-transact-sql.md).

In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [sspdw](../../includes/sspdw-md.md)]:

- `TRUNCATE TABLE` isn't allowed within the `EXPLAIN` statement.

- `TRUNCATE TABLE` can't be executed inside of a transaction.

## Permissions

The minimum permission required is `ALTER` on *table_name*. `TRUNCATE TABLE` permissions default to the table owner, members of the `sysadmin` fixed server role, and the `db_owner` and `db_ddladmin` fixed database roles, and aren't transferable. However, you can incorporate the `TRUNCATE TABLE` statement within a module, such as a stored procedure, and grant appropriate permissions to the module using the `EXECUTE AS` clause.

## Examples

### A. Truncate a table

The following example removes all data from the `JobCandidate` table. `SELECT` statements are included before and after the `TRUNCATE TABLE` statement to compare results.

```sql
USE AdventureWorks2022;

SELECT COUNT(*) AS BeforeTruncateCount
FROM HumanResources.JobCandidate;

TRUNCATE TABLE HumanResources.JobCandidate;

SELECT COUNT(*) AS AfterTruncateCount
FROM HumanResources.JobCandidate;
```

### B. Truncate table partitions

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.

The following example truncates specified partitions of a partitioned table. The `WITH (PARTITIONS (2, 4, 6 TO 8))` syntax causes partition numbers 2, 4, 6, 7, and 8 to be truncated.

```sql
TRUNCATE TABLE PartitionTable1
WITH (PARTITIONS (2, 4, 6 TO 8));
GO
```

### C. Roll back a truncate operation

The following example demonstrates that a `TRUNCATE TABLE` operation inside a transaction can be rolled back.

1. Create a test table with three rows.

   ```sql
   USE [tempdb];
   
   CREATE TABLE TruncateTest (ID INT IDENTITY (1, 1) NOT NULL);
   GO
   INSERT INTO TruncateTest DEFAULT VALUES;
   GO 3
   ```

1. Check the data before truncate.

   ```sql
   SELECT ID FROM TruncateTest;
   ```

1. Truncate the table within a transaction, and check the number of rows.

   ```sql
   BEGIN TRANSACTION;

   TRUNCATE TABLE TruncateTest;

   SELECT ID FROM TruncateTest;
   ```

   You see that the table is empty.

1. Roll back the transaction and check the data.

   ```sql
   ROLLBACK TRANSACTION;

   SELECT ID FROM TruncateTest;
   ```

   You see all three rows.

1. Clean up the table.

   ```sql
   DROP TABLE TruncateTest;
   ```

## Related content

- [DELETE (Transact-SQL)](delete-transact-sql.md)
- [DROP TABLE (Transact-SQL)](drop-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL) IDENTITY (Property)](create-table-transact-sql-identity-property.md)
