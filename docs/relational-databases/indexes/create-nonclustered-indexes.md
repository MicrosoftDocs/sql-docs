---
title: Create Nonclustered Indexes
description: This article shows you how to create nonclustered indexes by using SQL Server Management Studio or Transact-SQL.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/02/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "index creation [SQL Server], nonclustered indexes"
  - "nonclustered indexes [SQL Server], creating"
  - "nonclustered indexes [SQL Server], UNIQUE constraint"
  - "indexes [SQL Server], nonclustered"
  - "nonclustered indexes [SQL Server], PRIMARY KEY constraint"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Create nonclustered indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

You can create nonclustered indexes in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. A nonclustered index is an index structure separate from the data stored in a table that reorders one or more selected columns. Nonclustered indexes can often help you find data more quickly than searching the underlying table; queries can sometimes be answered entirely by the data in the nonclustered index, or the nonclustered index can point the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to the rows in the underlying table. Generally, nonclustered indexes are created to improve the performance of frequently used queries not covered by the clustered index or to locate rows in a table without a clustered index (called a heap). You can create multiple nonclustered indexes on a table or indexed view.

<a id="BeforeYouBegin"></a>
<a id="Implementations"></a>

## Typical implementations

Nonclustered indexes are implemented in the following ways:

- `UNIQUE` constraints**

  When you create a `UNIQUE` constraint, a unique nonclustered index is created to enforce a `UNIQUE` constraint by default. You can specify a unique clustered index if a clustered index on the table doesn't already exist. For more information, see [Unique constraints and check constraints](../tables/unique-constraints-and-check-constraints.md).

- **Index independent of a constraint**

  By default, a nonclustered index is created if clustered isn't specified. The maximum number of nonclustered indexes that can be created per table is 999. This includes any indexes created by `PRIMARY KEY` or `UNIQUE` constraints, but doesn't include XML indexes.

- **Nonclustered index on an indexed view**

  After a unique clustered index has been created on a view, nonclustered indexes can be created. For more information, see [Create indexed views](../views/create-indexed-views.md).

<a id="Security"></a>
<a id="Permissions"></a>

## Permissions

Requires `ALTER` permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.

<a id="SSMSProcedure"></a>

<a id="using-sql-server-management-studio"></a>

## Use SQL Server Management Studio

<a id="to-create-a-nonclustered-index-by-using-the-table-designer"></a>

### Create a nonclustered index by using the Table Designer

1. In Object Explorer, expand the database that contains the table on which you want to create a nonclustered index.

1. Expand the **Tables** folder.

1. Right-click the table on which you want to create a nonclustered index and select **Design**.

1. Right-click on the column you want to create the nonclustered index on and select **Indexes/Keys**.

1. In the **Indexes/Keys** dialog box, select **Add**.

1. Select the new index in the **Selected Primary/Unique Key or Index** text box.

1. In the grid, select **Create as Clustered**, and choose **No** from the dropdown list to the right of the property.

1. Select **Close**.

1. On the **File** menu, select **Save** _table_name_.

<a id="to-create-a-nonclustered-index-by-using-object-explorer"></a>

### Create a nonclustered index by using Object Explorer

1. In Object Explorer, expand the database that contains the table on which you want to create a nonclustered index.

1. Expand the **Tables** folder.

1. Expand the table on which you want to create a nonclustered index.

1. Right-click the **Indexes** folder, point to **New Index**, and select **Non-Clustered Index...**.

1. In the **New Index** dialog box, on the **General** page, enter the name of the new index in the **Index name** box.

1. Under **Index key columns**, select **Add...**.

1. In the **Select Columns from** _table_name_ dialog box, select the check box or check boxes of the table column or columns to be added to the nonclustered index.

1. Select **OK**.

1. In the **New Index** dialog box, select **OK**.

<a id="TsqlProcedure"></a>

<a id="using-transact-sql"></a>

## Use Transact-SQL

<a id="to-create-a-nonclustered-index-on-a-table-using-transact-sql"></a>

### Create a nonclustered index on a table using Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] with [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] installed. You can download [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] from [sample databases](../../samples/adventureworks-install-configure.md?view=sql-server-ver15&tabs=ssms&preserve-view=true).

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2022;
   GO
   -- Find an existing index named IX_ProductVendor_VendorID and delete it if found.
   IF EXISTS (SELECT name FROM sys.indexes
               WHERE name = N'IX_ProductVendor_VendorID')
       DROP INDEX IX_ProductVendor_VendorID ON Purchasing.ProductVendor;
   GO
   -- Create a nonclustered index called IX_ProductVendor_VendorID
   -- on the Purchasing.ProductVendor table using the BusinessEntityID column.
   CREATE NONCLUSTERED INDEX IX_ProductVendor_VendorID
       ON Purchasing.ProductVendor (BusinessEntityID);
   GO
   ```

## Related content

- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [Index architecture and design guide](../sql-server-index-design-guide.md)
