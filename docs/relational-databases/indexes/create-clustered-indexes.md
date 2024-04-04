---
title: "Create a clustered index"
description: Create a clustered index in SQL Server and Azure SQL.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mikeray
ms.date: 01/12/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "index creation [SQL Server], clustered indexes"
  - "clustered indexes, creating"
  - "clustered indexes, PRIMARY KEY constraint"
  - "clustered indexes, UNIQUE constraint"
  - "indexes [SQL Server], clustered"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create a clustered index

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can create clustered indexes on tables by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. With few exceptions, every table should have a clustered index. Besides improving query performance, a clustered index can be rebuilt or reorganized on demand to control table fragmentation. A clustered index can also be created on a view. (Clustered indexes are defined in the article [Clustered and nonclustered indexes](clustered-and-nonclustered-indexes-described.md).)

## <a name="Implementations"></a> Typical implementations

Clustered indexes are implemented in the following ways:

- **PRIMARY KEY and UNIQUE constraints**

  When you create a `PRIMARY KEY` constraint, a unique clustered index on the column or columns is automatically created if a clustered index on the table doesn't already exist and you don't specify a unique nonclustered index. The primary key column can't allow `NULL` values.

  When you create a `UNIQUE` constraint, a unique nonclustered index is created to enforce a `UNIQUE` constraint by default. You can specify a unique clustered index if a clustered index on the table doesn't already exist.

  An index created as part of the constraint is automatically given the same name as the constraint name. For more information, see [Primary and Foreign Key Constraints](../tables/primary-and-foreign-key-constraints.md) and [Unique constraints and check constraints](../tables/unique-constraints-and-check-constraints.md).

- **Index independent of a constraint**

  You can create a clustered index on a column other than primary key column if a nonclustered primary key constraint was specified.

## Limitations

- When a clustered index structure is created, disk space for both the old (source) and new (target) structures is required in their respective files and filegroups. The old structure isn't deallocated until the complete transaction commits. Additional temporary disk space for sorting might also be required. For more information, see [Disk Space Requirements for Index DDL Operations](disk-space-requirements-for-index-ddl-operations.md).

- If a clustered index is created on a heap with several existing nonclustered indexes, all the nonclustered indexes must be rebuilt so that they contain the clustering key value instead of the row identifier (RID). Similarly, if a clustered index is dropped on a table that has several nonclustered indexes, the nonclustered indexes are all rebuilt as part of the `DROP` operation. This process might take significant time on large tables.

  The preferred way to build indexes on large tables is to start with the clustered index and then build any nonclustered indexes. Consider setting the `ONLINE` option to ON when you create indexes on existing tables. When set to ON, long-term table locks aren't held. This enables queries or updates to the underlying table to continue. For more information, see [Perform Index Operations Online](perform-index-operations-online.md).

- The index key of a clustered index can't contain **varchar** columns that have existing data in the `ROW_OVERFLOW_DATA` allocation unit. If a clustered index is created on a **varchar** column and the existing data is in the `IN_ROW_DATA` allocation unit, subsequent insert or update actions on the column that would push the data off-row fail. To obtain information about tables that might contain row-overflow data, use the [sys.dm_db_index_physical_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) dynamic management function.

## Permissions

Requires `ALTER` permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.

## Use SQL Server Management Studio

### Create a clustered index from Object Explorer

1. In Object Explorer, expand the table on which you want to create a clustered index.

1. Right-click the **Indexes** folder, point to **New Index**, and select **Clustered Index...**.

1. In the **New Index** dialog box, on the **General** page, enter the name of the new index in the **Index name** box.

1. Under **Index key columns**, select **Add...**.

1. In the **Select Columns from** *table_name* dialog box, select the check box of the table column to be added to the clustered index.

1. Select **OK**.

1. In the **New Index** dialog box, select **OK**.

### Create a clustered index by using the Table Designer

1. In Object Explorer, expand the database on which you want to create a table with a clustered index.

1. Right-click the **Tables** folder and select **New Table...**.

1. Create a new table as you normally would. For more information, see [Create tables (Database Engine)](../tables/create-tables-database-engine.md).

1. Right-click the new table created previously, and select **Design**.

1. On the **Table Designer** menu, select **Indexes/Keys**.

1. In the **Indexes/Keys** dialog box, select **Add**.

1. Select the new index in the **Selected Primary/Unique Key or Index** text box.

1. In the grid, select **Create as Clustered**, and choose **Yes** from the drop-down list to the right of the property.

1. Select **Close**.

1. On the **File** menu, select **Save** *table_name*.

## Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2022;
   GO
   
   -- Create a new table with three columns.
   CREATE TABLE dbo.TestTable (
       TestCol1 INT NOT NULL,
       TestCol2 NCHAR(10) NULL,
       TestCol3 NVARCHAR(50) NULL
   );
   GO
   
   -- Create a clustered index called IX_TestTable_TestCol1
   -- on the dbo.TestTable table using the TestCol1 column.
   CREATE CLUSTERED INDEX IX_TestTable_TestCol1 ON dbo.TestTable (TestCol1);
   GO
   ```

For more information, see [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md).

## Related content

- [Create Primary Keys](../tables/create-primary-keys.md)
- [Create unique constraints](../tables/create-unique-constraints.md)
