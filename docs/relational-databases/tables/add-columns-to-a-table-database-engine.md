---
description: "Add Columns to a Table (Database Engine)"
title: "Add Columns to a Table (Database Engine)"
ms.custom: ""
ms.date: "01/28/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "inserting columns"
  - "columns [SQL Server], adding"
  - "adding columns"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Add Columns to a Table (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

This article describes how to add new columns to a table in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

## Remarks

 Using the ALTER TABLE statement to add columns to a table automatically adds those columns to the end of the table. 

 If you want the columns in a specific order in the table, you must use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Though it isn't recommended, for more information on reordering tables, see [Change Column Order in a Table](change-column-order-in-a-table.md).

 To query existing columns, use the [sys.columns](../system-catalog-views/sys-columns-transact-sql.md) object catalog view.

## <a name="Permissions"></a> Permissions

Requires ALTER permission on the table.

## <a name="SSMSProcedure"></a> Use SQL Server Management Studio

### Insert columns into a table with Table Designer

1. In **Object Explorer**, right-click the table to which you want to add columns and choose **Design**.
2. Select the first blank cell in the **Column Name** column.
3. Type the column name in the cell. The column name is a required value.
4. Press the TAB key to go to the **Data Type** cell and select a data type from the dropdown.

   This is a required value, and will be assigned the default value if you don't choose one.

   > [!NOTE]
   > You can change the default value in the **Options** dialog box under **Database Tools**.

5. Continue to define any other column properties in the **Column Properties** tab.

    > [!NOTE]
    > The default values for your column properties are added when you create a new column, but you can change them in the **Column Properties** tab.

6. When you're finished adding columns, from the **File** menu, choose **Save** _table name_.
  
## <a name="TsqlProcedure"></a> Use Transact-SQL
  
### Add columns to a table  
  
The following example adds two columns to the table `dbo.doc_exa`.

```sql
ALTER TABLE dbo.doc_exa 
ADD column_b VARCHAR(20) NULL, column_c INT NULL ;
```

## See also

- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
- [Column properties](column-properties-general-page.md)

## Next steps

- [Create check constraint](create-check-constraints.md)
- [Specify default values for columns](specify-default-values-for-columns.md)
- [Create unique constraints](create-unique-constraints.md)