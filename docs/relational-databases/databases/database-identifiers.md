---
title: Database Identifiers
description: Get acquainted with database identifiers. Learn about their collation, various classes, delimiting requirements, and naming rules.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 04/08/2026
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "regular identifiers [SQL Server]"
  - "identifiers [SQL Server]"
  - "names [SQL Server], identifiers"
  - "identifiers [SQL Server], about identifiers"
  - "SQL Server identifiers"
  - "Transact-SQL identifiers"
  - "database objects [SQL Server], names"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Database identifiers

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSE FabricDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

The database object name is its identifier.

Servers, databases, and database objects, such as tables, views, columns, indexes, triggers, procedures, constraints, and rules, can have identifiers. Most objects require identifiers, but some objects, such as constraints, make them optional.

You create an object identifier when you define the object. Use the identifier to reference the object. For example, the following statement creates a table with the identifier `TableX`, and two columns with the identifiers `KeyCol` and `Description`:

```sql
CREATE TABLE TableX
(
    KeyCol INT PRIMARY KEY,
    Description NVARCHAR (80)
);
```

This table has an unnamed constraint. The primary key constraint has no user-specified identifier, so the system assigns it a generated name like `PK__TableX__D7CB9CCCEEF0806C`. You can see this name in system metadata views like [sys.key_constraints](../system-catalog-views/sys-key-constraints-transact-sql.md).

Constraint names and other schema-scoped objects must be unique within a database schema. For example, two primary key constraints can't share a name. However, column names only need to be unique within each table, not within the schema.

The collation of an identifier depends on the level at which you define it.

- The default collation of the instance is assigned to identifiers of instance-level objects, such as logins and database names.

- The default collation of the database is assigned to identifiers of objects in a database, such as tables, views, and column names. For example, you can create two tables with names that differ only in case in a database that has case-sensitive collation, but you can't create them in a database that has case-insensitive collation.

> [!NOTE]  
> The names of variables, or the parameters of functions and stored procedures, must comply with the rules for [!INCLUDE [tsql](../../includes/tsql-md.md)] identifiers.

## Classes of identifiers

There are two classes of identifiers:

- **Regular identifiers** comply with the format rules for identifiers. They aren't delimited when used in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. Regular identifiers must follow the [rules for regular identifiers](#rules-for-regular-identifiers): they can only contain letters, digits, and certain symbols (`_`, `@`, `#`, `$`), must start with a letter or one of `_`, `@`, `#`, and can't be a reserved word.

  ```sql
  USE AdventureWorks2025;
  GO

  SELECT *
  FROM HumanResources.Employee
  WHERE NationalIDNumber = 153479919;
  ```

- **Delimited identifiers** are enclosed in double quotation marks (`"`) or brackets (`[` and `]`). Delimiters allow you to use names that would otherwise be invalid as regular identifiers, such as reserved keywords, names with spaces, or names with special characters. Identifiers that already comply with the regular identifier rules can also be delimited, but the delimiters are optional in that case. For more information, see [Rules for delimited identifiers](#rules-for-delimited-identifiers).

  ```sql
  USE AdventureWorks2025;
  GO

  SELECT *
  FROM [HumanResources].[Employee] --Delimiter is optional.
  WHERE [NationalIDNumber] = 153479919; --Delimiter is optional.
  ```

Identifiers that don't comply with the rules for regular identifiers must be delimited in a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. For example:

```sql
USE AdventureWorks2025;
GO

--Identifier contains a space and uses a reserved keyword.
CREATE TABLE [SalesOrderDetail Table]
(
    [Order] INT NOT NULL,
    [SalesOrderDetailID] INT IDENTITY (1, 1) NOT NULL,
    [OrderQty] SMALLINT NOT NULL,
    [ProductID] INT NOT NULL,
    [UnitPrice] MONEY NOT NULL,
    [UnitPriceDiscount] MONEY NOT NULL,
    [ModifiedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_SalesOrderDetail_Order_SalesOrderDetailID] PRIMARY KEY CLUSTERED
    (
        [Order] ASC,
        [SalesOrderDetailID] ASC
    )
);
GO

SELECT *
FROM [SalesOrderDetail Table] --Identifier contains a space and uses a reserved keyword.
WHERE [Order] = 10; --Identifier is a reserved keyword.
```

Both regular and delimited identifiers must contain from 1 through 128 characters. For local temporary tables, the identifier can have a maximum of 116 characters.

## Rules for delimited identifiers

Delimited identifiers are either enclosed in brackets (`[` and `]`) or double quotation marks (`"`). They can contain any combination of characters, including spaces, reserved keywords, and special characters that aren't allowed in regular identifiers.

### Bracket-delimited identifiers

Bracket-delimited identifiers are enclosed in square brackets (`[` and `]`). If the identifier itself contains a right bracket (`]`), escape it by doubling it (`]]`). A left bracket (`[`) doesn't require escaping.

For example, to create and query a table whose name contains brackets:

```sql
-- Create a table with a ] character in its name.
CREATE TABLE [My]]Table]
(
    ID INT PRIMARY KEY
);
GO

-- Reference the table in a query.
SELECT *
FROM [My]]Table];
GO
```

The `QUOTENAME` function returns a valid bracket-delimited identifier for a given string and handles the escaping automatically:

```sql
SELECT QUOTENAME('abc[]def');
```

The previous example returns `[abc[]]def]`.

### Double-quote-delimited identifiers

Double-quote-delimited identifiers are enclosed in double quotation marks (`"`). If the identifier itself contains a double quotation mark, escape it by doubling it (`""`).

Double-quote delimiters require `SET QUOTED_IDENTIFIER ON` (the default for most connections). When `QUOTED_IDENTIFIER` is `OFF`, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] treats double-quoted strings as string literals instead of identifiers. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

For example, to create and query a table that uses reserved keywords as identifiers:

```sql
SET QUOTED_IDENTIFIER ON;
GO

-- Create a table using double-quote delimiters.
CREATE TABLE "My Table"
(
    "Order" INT NOT NULL,
    "Description" NVARCHAR (100)
);
GO

SELECT "Order",
       "Description"
FROM "My Table";
GO
```

> [!NOTE]  
> `SET QUOTED_IDENTIFIER` doesn't affect bracket-delimited identifiers. Bracket delimiters always work regardless of the `QUOTED_IDENTIFIER` setting.

## Rules for regular identifiers

The names of variables, functions, and stored procedures must follow these rules for [!INCLUDE [tsql](../../includes/tsql-md.md)] identifiers.

1. The first character must be one of the following characters:

   - A letter as defined by the Unicode Standard 3.2. The Unicode definition of letters includes Latin characters from `a` through `z`, from `A` through `Z`, and also letter characters from other languages.

   - The underscore (`_`), at sign (`@`), or number sign (`#`).

     Certain symbols at the beginning of an identifier have special meaning in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)]. A regular identifier that starts with the at sign always denotes a local variable or parameter and can't be used as the name of any other type of object. An identifier that starts with a number sign denotes a temporary table or procedure. An identifier that starts with double number signs (`##`) denotes a global temporary object. Although the number sign or double number sign characters can be used to begin the names of other types of objects, you should avoid this practice.

     Some [!INCLUDE [tsql](../../includes/tsql-md.md)] functions have names that start with double at signs (`@@`). To avoid confusion with these functions, don't use names that start with `@@`.

1. Subsequent characters can include the following list:

   - Letters as defined in the Unicode Standard 3.2.

   - Decimal numbers from either Basic Latin or other national scripts.

   - The at sign (`@`), dollar sign (`$`), number sign (`#`), or underscore (`_`).

1. The identifier must not be a [!INCLUDE [tsql](../../includes/tsql-md.md)] reserved word. The [!INCLUDE [ssde-md](../../includes/ssde-md.md)] reserves both the uppercase and lowercase versions of reserved words. When you use identifiers in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, delimit identifiers that don't comply with these rules by using double quotation marks or brackets. The words that are reserved depend on the database compatibility level. Set the database compatibility level by using the [ALTER DATABASE compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) statement.

1. Don't use embedded spaces or special characters.

1. Don't use [Supplementary characters](../collations/collation-and-unicode-support.md#Supplementary_Characters).

When you use identifiers in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, delimit identifiers that don't comply with these rules by using double quotation marks or brackets. Some of these rules vary depending on the database [compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

## Catalog collation in Azure SQL Database

You can't change or set the logical server collation on Azure SQL Database. However, you can configure each database's collations separately for data in the database and for catalog. The catalog collation determines the collation for system metadata, such as object identifiers. You can specify both collations independently when you [create the database in the Azure portal](/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&preserve-view=true&tabs=azure-portal#create-a-single-database), in Transact-SQL (T-SQL) with [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true#collation_name), or in PowerShell with [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase).

For details and examples, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true#collation_name). Specify a collation for the database (`COLLATE`) and a catalog collation for system metadata and object identifiers (`CATALOG_COLLATION`).

## Catalog collation in SQL database in Microsoft Fabric

The default collation of a SQL database in Fabric is `SQL_Latin1_General_CP1_CI_AS`. You can configure a different collation at deployment time, but you can't change it after the database is created. Individual columns can use their own collations. For more information on deployment options, see [Options to create a SQL database in Fabric](/fabric/database/sql/create-options).

## Related content

- [Reserved Keywords (Transact-SQL)](../../t-sql/language-elements/reserved-keywords-transact-sql.md)
- [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md)
- [QUOTENAME (Transact-SQL)](../../t-sql/functions/quotename-transact-sql.md)
