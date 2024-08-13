---
title: "Add Columns to a Table (Database Engine)"
description: "Learn how to add columns to an existing table in SQL Server and Azure SQL platforms by using SQL Server Management Studio or Transact-SQL."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/05/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "inserting columns"
  - "columns [SQL Server], adding"
  - "adding columns"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# Add Columns to a Table (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw-fabricdw.md)]

This article describes how to add new columns to a table in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Remarks

 Using the `ALTER TABLE` statement to add columns to a table automatically adds those columns to the end of the table.

 If you want the columns in a specific order in the table, you must use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Though it isn't recommended, for more information on reordering tables, see [Change Column Order in a Table](change-column-order-in-a-table.md).

 To query existing columns, use the [sys.columns](../system-catalog-views/sys-columns-transact-sql.md) object catalog view.

## <a id="Permissions"></a> Permissions

Requires ALTER permission on the table.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

> [!IMPORTANT]
> Always use the latest version of [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

SQL Server Management Studio (SSMS) doesn't support all data definition language (DDL) options in Azure Synapse. Use [T-SQL scripts](#TsqlProcedure) instead.

### Insert columns into a table with Table Designer

1. In **Object Explorer**, right-click the table to which you want to add columns and choose **Design**.
1. Select the first blank cell in the **Column Name** column.
1. Type the column name in the cell. The column name is a required value.
1. Press the TAB key to go to the **Data Type** cell and select a data type from the dropdown list. Data type is a required value, and is assigned the default value if you don't choose one.

   > [!NOTE]
   > You can change the default value in the **Options** dialog box under **Database Tools**.

1. Continue to define any other column properties in the **Column Properties** tab.

    > [!NOTE]
    > The default values for your column properties are added when you create a new column, but you can change them in the **Column Properties** tab.

1. When you're finished adding columns, from the **File** menu, choose **Save _table name_**.
  
## <a id="TsqlProcedure"></a> Use Transact-SQL
  
### Add columns to a table
  
The following example adds two columns to the table `dbo.doc_exa`.

```sql
ALTER TABLE dbo.doc_exa 
ADD column_b VARCHAR(20) NULL, column_c INT NULL ;
```

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [Column Properties (General Page)](column-properties-general-page.md)
- [Create Check Constraints](create-check-constraints.md)
- [Specify Default Values for Columns](specify-default-values-for-columns.md)
- [Specify Computed Columns in a Table](specify-computed-columns-in-a-table.md)
- [Create unique constraints](create-unique-constraints.md)
- [Indexes](../indexes/indexes.md)
