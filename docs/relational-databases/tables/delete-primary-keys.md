---
title: "Delete Primary Keys"
description: "Learn more about how to delete the primary key from a table in the SQL Server Database Engine."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 01/23/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "removing primary keys"
  - "deleting primary keys"
  - "primary keys [SQL Server], deleting"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Delete primary keys

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

You can delete (drop) a primary key in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

When the primary key is deleted, the corresponding index is deleted. This index might be the clustered index of the table, causing the table to become a heap. For more information, see [Heaps (tables without clustered indexes)](../indexes/heaps-tables-without-clustered-indexes.md). Most tables should have a clustered index. To re-create the primary key, see [Create primary keys](create-primary-keys.md).

Primary keys can be referenced by foreign keys in another table. If referenced by a foreign key, you need to drop referencing foreign keys first, then drop the primary key. For more information, see [Primary and foreign key constraints](primary-and-foreign-key-constraints.md).

## Permissions

Requires `ALTER` permission on the table.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

### Delete a primary key constraint using Object Explorer

1. In Object Explorer, expand the table that contains the primary key and then expand **Keys**.

1. Right-click the key and select **Delete**.

1. In the **Delete Object** dialog box, verify the correct key is specified and select **OK**.

### Delete a primary key constraint using Table Designer

1. In Object Explorer, right-click the table with the primary key, and select **Design**.

1. In the table grid, right-click the row with the primary key and choose **Remove Primary Key** to toggle the setting from on to off.

   > [!NOTE]  
   > To undo this action, close the table without saving the changes. Deleting a primary key can't be undone without losing all other changes made to the table.

1. On the **File** menu, select **Save** _table name_.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

### Delete a primary key constraint

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   - You must first identify the name of the primary key constraint.

     ```sql
     USE AdventureWorks2022;
     GO

     -- Return the name of primary key.
     SELECT name
     FROM sys.key_constraints
     WHERE type = 'PK'
         AND OBJECT_NAME(parent_object_id) = N'TransactionHistoryArchive';
     GO
     ```

   - To create the `DROP` script, use the result from the previous query. Replace `<primary-key-constraint>` with the correct value. It might look similar to `PK_TransactionHistoryArchive_TransactionID`.

     ```sql
     ALTER TABLE Production.TransactionHistoryArchive
         DROP CONSTRAINT <primary-key-constraint>;
     GO
     ```

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [sys.key_constraints (Transact-SQL)](../system-catalog-views/sys-key-constraints-transact-sql.md)
- [Clustered and nonclustered indexes](../indexes/clustered-and-nonclustered-indexes-described.md)
