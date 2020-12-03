---
description: "Specify Default Values for Columns"
title: "Specify Default Values for Columns | Microsoft Docs"
ms.custom: ""
ms.date: 03/17/2020
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], defaults"
  - "default values"
ms.assetid: 64514aed-b846-407b-992e-cf813f9a1a91
author: stevestein
ms.author: sstein
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify Default Values for Columns

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

You can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to specify a default value that will be entered into the table column. You can set a default by using the Object Explorer of the user interface or by submitting [!INCLUDE[tsql](../../includes/tsql-md.md)].

If you do not assign a default value to the column, and the user leaves the column blank, then:

- If you set the option to allow null values, NULL will be inserted into the column.

- If you do not set the option to allow null values, the column will remain blank, but the user will not be able to save the row until they supply a value for the column.

## <a name="Restrictions"></a> Limitations and Restrictions

Before you begin, be aware of the following limitations and restrictions:

- If your entry in the **Default Value** field replaces a bound default (which is shown without parentheses), you will be prompted to unbind the default and replace it with your new default.

- To enter a text string, enclose the value in single quotation marks ('); do not use double quotation marks (") because they are reserved for quoted identifiers.

- To enter a numeric default, enter the number without quotation marks around it.

- To enter an object/function, enter the name of the object/function without quotation marks around it.

### <a name="Security"></a> Security Permissions

The actions described in this article require ALTER permission on the table.

## <a name="SSMSProcedure"></a> Use SSMS to specify a default

You can use the Object Explorer to specify a default value for a table column.

### Object Explorer

1. In **Object Explorer**, right-click the table with columns for which you want to change the scale and click **Design**.

2. Select the column for which you want to specify a default value.

3. In the **Column Properties** tab, enter the new default value in the **Default Value or Binding** property.

   > [!NOTE]
   > To enter a numeric default value, enter the number. For an object or function enter its name. For an alphanumeric default enter the value inside single quotes.

4. On the **File** menu, click **Save** _table name_.

## <a name="TsqlProcedure"></a> Use Transact-SQL to specify a default

There are various ways that you can specify a default value for a column, by using SSMS to submit T-SQL.

### ALTER TABLE (T-SQL)

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

2. On the Standard bar, click **New Query**.

3. Copy and paste the following example into the query window and click **Execute**.

   ```sql
   CREATE TABLE dbo.doc_exz (column_a INT, column_b INT); -- Allows nulls.
   GO
   INSERT INTO dbo.doc_exz (column_a) VALUES (7);
   GO
   ALTER TABLE dbo.doc_exz
     ADD CONSTRAINT DF_Doc_Exz_Column_B
     DEFAULT 50 FOR column_b;
   GO
   ```

<!--
The following two T-SQL code examples were offered by 'nycdotnet' (Steve) via public PR 1660, Feb 2019.
-->

### CREATE TABLE (T-SQL)

```sql
    CREATE TABLE dbo.doc_exz (
      column_a INT,
      column_b INT DEFAULT 50);
```

### Named CONSTRAINT (T-SQL)

```sql
    CREATE TABLE dbo.doc_exz (
      column_a INT,
      column_b INT CONSTRAINT DF_Doc_Exz_Column_B DEFAULT 50);
```

For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).
