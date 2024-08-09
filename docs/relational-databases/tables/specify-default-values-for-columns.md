---
title: Specify default values for columns
description: Specify a default value that is entered into the table column, with SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - UpdateFrequency5
helpviewer_keywords:
  - "columns [SQL Server], defaults"
  - "default values"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Specify default values for columns

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

You can use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) to specify a default value that is entered into the table column. You can set a default by using the Object Explorer, or by executing [!INCLUDE [tsql](../../includes/tsql-md.md)].

If you don't assign a default value to the column, and the user leaves the column blank, then:

- If you set the option to allow null values, `NULL` is inserted into the column.

- If you don't set the option to allow null values, the column remains blank, but the user isn't able to save the row until they supply a value for the column.

## Limitations

Before you begin, be aware of the following limitations and restrictions:

- If your entry in the **Default Value** field replaces a bound default (which is shown without parentheses), you're prompted to unbind the default and replace it with your new default.

- To enter a text string, enclose the value in single quotation marks (`'`). Don't use double quotation marks (`"`), because they're reserved for quoted identifiers.

- To enter a numeric default, enter the number without quotation marks around it.

- To enter an object/function, enter the name of the object/function without quotation marks around it.

> [!NOTE]  
> In Azure Synapse Analytics, only constants can be used for a default constraint. An expression can't be used with a default constraint.

## Permissions

The actions described in this article require `ALTER` permission on the table.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio to specify a default

You can use Object Explorer in SSMS to specify a default value for a table column. To do so, follow these steps:

1. Connect to your SQL Server instance in SSMS.

1. In **Object Explorer**, right-click the table with columns for which you want to change the scale and select **Design**.

1. Select the column for which you want to specify a default value.

1. In the **Column Properties** tab, enter the new default value in the **Default Value or Binding** property.

   To enter a numeric default value, enter the number. For an object or function, enter its name. For an alphanumeric default, enter the value inside single quotes.

1. On the **File** menu, select **Save *\<table name>***.

## <a id="TsqlProcedure"></a> Use Transact-SQL to specify a default

There are various ways that you can specify a default value for a column by using T-SQL. In each of the following examples, you can open a new Transact-SQL query with these steps.

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the example into the query window and select **Execute**.

### Use ALTER TABLE

```sql
CREATE TABLE dbo.doc_exz (
    column_a INT,
    column_b INT
); -- Allows nulls.
GO

INSERT INTO dbo.doc_exz (column_a)
VALUES (7);
GO

ALTER TABLE dbo.doc_exz
ADD CONSTRAINT DF_Doc_Exz_Column_B DEFAULT 50 FOR column_b;
GO
```

### Use CREATE TABLE

```sql
CREATE TABLE dbo.doc_exz (
    column_a INT,
    column_b INT DEFAULT 50
);
```

### Use a named constraint

```sql
CREATE TABLE dbo.doc_exz (
    column_a INT,
    column_b INT CONSTRAINT DF_Doc_Exz_Column_B DEFAULT 50
);
```

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [TABLE_CONSTRAINTS (Transact-SQL)](../system-information-schema-views/table-constraints-transact-sql.md)
