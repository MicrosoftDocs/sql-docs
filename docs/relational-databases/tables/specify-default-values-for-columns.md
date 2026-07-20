---
title: Specify default values for columns
description: Specify a default value that is entered into the table column, with SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 12/09/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - UpdateFrequency5
  - ignite-2025
helpviewer_keywords:
  - "columns [SQL Server], defaults"
  - "default values"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Specify default values for columns

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

You can use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) to specify a default value that is entered into the table column. You can set a default by using the Object Explorer, or by executing [!INCLUDE [tsql](../../includes/tsql-md.md)].

If you don't assign a default value to the column, and the user leaves the column blank, then:

- If you set the option to allow null values, `NULL` is inserted into the column.

- If you don't set the option to allow null values, the column remains blank, but the user or application can't insert the row until they supply a value for the column.

You can use a default constraint for various tasks to ensure database-level data consistency:

- Set the row value for an "active" or "enable" column to `1` upon insertion.
- Set the row value for a date field to the current date. 
- Set the row value for a field to a deterministic system function, for example, `DB_NAME()`.

## Limitations

Before you begin, be aware of the following limitations and restrictions:

- If your entry in the **Default Value** field replaces a bound default (which is shown without parentheses), you're prompted to unbind the default and replace it with your new default.

- To enter a text string, enclose the value in single quotation marks (`'`). Don't use double quotation marks (`"`), because they're reserved for quoted identifiers.

- To enter a numeric default, enter the number without quotation marks around it.

- To enter an object/function, enter the name of the object/function without quotation marks around it.

- In Azure Synapse Analytics, only constants can be used for a default constraint. An expression can't be used with a default constraint.

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

### Use a named constraint

When working with database projects, it's recommended to create constraints with names. Otherwise, the default constraint is given a system-generated name, which will differ on each SQL server environment where your database objects are created.

```sql
CREATE TABLE dbo.doc_exz (
    column_a INT,
    column_b INT CONSTRAINT DF_Doc_Exz_Column_B DEFAULT 50
);
```

### Use ALTER TABLE

You can add a named constraint to an existing table with `ALTER TABLE`.

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

You can create a new table with default constraints with `CREATE TABLE`.

```sql
CREATE TABLE dbo.doc_exz (
    column_a INT,
    column_b INT CONSTRAINT DF_Doc_Exz_Column_B DEFAULT 50
);
```

### Set a created date

The following example uses the `sysdatetimeoffset()` system function to populate the row value of the `dateinserted` column with the date when the row was created. 

```sql
CREATE TABLE dbo.test (
    id INT identity(1, 1) NOT NULL CONSTRAINT PK_test PRIMARY KEY
    ,date_inserted DATETIMEOFFSET(2) NOT NULL CONSTRAINT DF_test_date_inserted DEFAULT(sysdatetimeoffset())
);
```

A default constraint doesn't change when the row is updated. To update a value whenever the row is modified, consider using a [trigger](../../t-sql/functions/trigger-functions-transact-sql.md), a [temporal table](temporal-tables.md), a [computed column](specify-computed-columns-in-a-table.md), or a [rowversion](../../t-sql/data-types/rowversion-transact-sql.md) binary string. Consider also inserting rows by executing stored procedures instead of directly inserting rows, where stored procedures can enforce business logic, default values, and other data consistency rules.

To detect rows that change, consider [Change data capture (CDC)](../track-changes/about-change-data-capture-sql-server.md), [Change Tracking](../track-changes/about-change-tracking-sql-server.md), a [temporal table](temporal-tables.md), or a [Ledger table](../security/ledger/ledger-overview.md).

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [TABLE_CONSTRAINTS (Transact-SQL)](../system-information-schema-views/table-constraints-transact-sql.md)
