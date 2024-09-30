---
title: "Delete columns from a table"
description: Learn how to delete table columns in the SQL Server Database Engine with SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/25/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - UpdateFrequency5
helpviewer_keywords:
  - "columns [SQL Server], deleting"
  - "removing columns"
  - "deleting columns"
  - "dropping columns"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete columns from a table

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

This article describes how to delete table columns in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] using [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md) (SSMS) or [!INCLUDE [tsql](../../includes/tsql-md.md)].

> [!CAUTION]  
> When you delete a column from a table, the column and all the data it contains are deleted.

## Limitations

You can't delete a column that has a `CHECK` constraint. You must first delete the constraint.

You can't delete a column that has `PRIMARY KEY` or `FOREIGN KEY` constraints or other dependencies except when using the [Table Designer](../../ssms/visual-db-tools/visual-database-tool-designers.md#table-designer) in SSMS. When using [Object Explorer](../../ssms/object/object-explorer.md) in SSMS or [!INCLUDE [tsql](../../includes/tsql-md.md)], you must first remove all dependencies on the column.

## Permissions

Requires `ALTER` permission on the table.

## Delete columns using SQL Server Management Studio

You can delete columns in SSMS using Object Explorer or Table Designer.

### Delete columns using Object Explorer

The following steps explain how to delete columns with Object Explorer in SSMS:

1. Connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. In **Object Explorer**, locate the table from which you want to delete columns, and expand the table to expose the column names.

1. Right-click the column that you want to delete, and choose **Delete**.

1. In the **Delete Object** dialog box, select **OK**.

If the column contains constraints or other dependencies, an error message displays in the **Delete Object** dialog box. Resolve the error by deleting the referenced constraints.

### Delete columns using Table Designer

The following steps explain how to delete columns with Table Designer in SSMS:

1. In **Object Explorer**, right-click the table from which you want to delete columns and choose **Design**.

1. Right-click the column you want to delete and choose **Delete Column** from the shortcut menu.

1. If the column participates in a relationship (`FOREIGN KEY` or `PRIMARY KEY`), a message prompts you to confirm the deletion of the selected columns and their relationships. Choose **Yes**.

## <a id="TsqlProcedure"></a> Delete columns using Transact-SQL

You can delete columns using Transact-SQL in SSMS, [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio), or command-line tools such as the [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md).

The following example shows you how to delete a column `column_b` from table `dbo.doc_exb`. The table and column must already exist.

```sql
ALTER TABLE dbo.doc_exb DROP COLUMN column_b;
GO
```

If the column contains constraints or other dependencies, an error message is returned. Resolve the error by deleting the referenced constraints.

For more examples, see [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [What is SQL Server Management Studio (SSMS)?](../../ssms/sql-server-management-studio-ssms.md)
- [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio)
- [Object Explorer](../../ssms/object/object-explorer.md)
- [Table Designer](../../ssms/visual-db-tools/visual-database-tool-designers.md#table-designer)
