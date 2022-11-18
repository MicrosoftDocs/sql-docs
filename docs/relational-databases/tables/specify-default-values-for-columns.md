---
title: "Specify Default Values for Columns"
description: "Specify Default Values for Columns"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], defaults"
  - "default values"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: 10/21/2021
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify Default Values for Columns

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

You can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to specify a default value that will be entered into the table column. You can set a default by using the SQL Server Management Studio (SSMS) Object Explorer or by executing [!INCLUDE[tsql](../../includes/tsql-md.md)].

If you do not assign a default value to the column, and the user leaves the column blank, then:

- If you set the option to allow null values, `NULL` will be inserted into the column.

- If you do not set the option to allow null values, the column will remain blank, but the user will not be able to save the row until they supply a value for the column.

## <a name="Restrictions"></a> Limitations and Restrictions

Before you begin, be aware of the following limitations and restrictions:

- If your entry in the **Default Value** field replaces a bound default (which is shown without parentheses), you will be prompted to unbind the default and replace it with your new default.

- To enter a text string, enclose the value in single quotation marks ('); do not use double quotation marks (") because they are reserved for quoted identifiers.

- To enter a numeric default, enter the number without quotation marks around it.

- To enter an object/function, enter the name of the object/function without quotation marks around it.

> [!NOTE]
> In Azure Synapse Analytics, only constants can be used for a default constraint. An expression cannot be used with a default constraint.

## <a name="Security"></a> Permissions

The actions described in this article require **ALTER** permission on the table.

## <a name="SSMSProcedure"></a> Use SSMS to specify a default

You can use the Object Explorer to specify a default value for a table column.

### Object Explorer

1. In **Object Explorer**, right-click the table with columns for which you want to change the scale and select **Design**.

2. Select the column for which you want to specify a default value.

3. In the **Column Properties** tab, enter the new default value in the **Default Value or Binding** property.

   > [!NOTE]
   > To enter a numeric default value, enter the number. For an object or function enter its name. For an alphanumeric default enter the value inside single quotes.

4. On the **File** menu, select **Save** _table name_.

## <a name="TsqlProcedure"></a> Use Transact-SQL to specify a default

There are various ways that you can specify a default value for a column, by using SSMS to submit T-SQL.

### ALTER TABLE (T-SQL)

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

2. On the Standard bar, select **New Query**.

3. Copy and paste the following example into the query window and select **Execute**.

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

## Next steps

For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).
