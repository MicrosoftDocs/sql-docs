---
title: "Delete columns from a table"
description: "Delete columns from a table"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], deleting"
  - "removing columns"
  - "deleting columns"
  - "dropping columns"
ms.assetid: 0d8f6e4f-bc71-4fa3-8615-74249c8e072d
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: ""
ms.date: "12/15/2021"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete columns from a table

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

This article describes how to delete table columns in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] using [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md) (SSMS) or [!INCLUDE[tsql](../../includes/tsql-md.md)].

> [!CAUTION]
> When you delete a column from a table, the column and all the data it contains are deleted.

## <a name="Restrictions"></a> Limitations and restrictions

You can't delete a column that has a CHECK constraint. You must first delete the constraint.

You can't delete a column that has PRIMARY KEY or FOREIGN KEY constraints or other dependencies except when using the [Table Designer](../../ssms/visual-db-tools/visual-database-tool-designers.md#table-designer) in SSMS. When using [Object Explorer](../../ssms/object/object-explorer.md) or [!INCLUDE[tsql](../../includes/tsql-md.md)], you must first remove all dependencies on the column.
## <a name="Permissions"></a> Permissions

Requires ALTER permission on the table.

## Delete columns using Object Explorer

The following steps explain how to delete columns with Object Explorer in SSMS:

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].
2. In **Object Explorer**, locate the table from which you want to delete columns, and expand to expose the column names.
3. Right-click the column that you want to delete, and choose **Delete**.
4. In **Delete Object** dialog box, click **OK**.

If the column contains constraints or other dependencies, an error message will display in the **Delete Object** dialog box. Resolve the error by deleting the referenced constraints.

## Delete columns using Table Designer

The following steps explain how to delete columns with Table Designer in SSMS:

1. In **Object Explorer**, right-click the table from which you want to delete columns and choose **Design**.
2. Right-click the column you want to delete and choose **Delete Column** from the shortcut menu.
3. If the column participates in a relationship (FOREIGN KEY or PRIMARY KEY), a message prompts you to confirm the deletion of the selected columns and their relationships. Choose **Yes**.

## <a name="TsqlProcedure"></a> Delete columns using Transact-SQL

You can delete columns using Transact-SQL in SSMS, [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md), or command-line tools such as the [sqlcmd utility](../../tools/sqlcmd-utility.md).

The following example shows you how to delete a column.

```sql
ALTER TABLE dbo.doc_exb DROP COLUMN column_b;
GO
```

If the column contains constraints or other dependencies, an error message will be returned. Resolve the error by deleting the referenced constraints.

For more examples, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).

## Next steps

For more information about altering tables and related tools, see the following articles:

- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
- [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md)
- [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md)
- [Object Explorer](../../ssms/object/object-explorer.md)
- [Table Designer](../../ssms/visual-db-tools/visual-database-tool-designers.md#table-designer)
