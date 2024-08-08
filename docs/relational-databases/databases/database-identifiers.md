---
title: Database identifiers
description: "Get acquainted with database identifiers. Learn about their collation, various classes, delimiting requirements, and naming rules."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/06/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "regular identifiers [SQL Server]"
  - "identifiers [SQL Server]"
  - "names [SQL Server], identifiers"
  - "identifiers [SQL Server], about identifiers"
  - "SQL Server identifiers"
  - "Transact-SQL identifiers"
  - "database objects [SQL Server], names"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Database identifiers

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW Fabric SE Fabric DW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

The database object name is referred to as its identifier. Everything in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can have an identifier. Servers, databases, and database objects, such as tables, views, columns, indexes, triggers, procedures, constraints, and rules, can have identifiers. Identifiers are required for most objects, but are optional for some objects such as constraints.

An object identifier is created when the object is defined. The identifier is then used to reference the object. For example, the following statement creates a table with the identifier `TableX`, and two columns with the identifiers `KeyCol` and `Description`:

```sql
CREATE TABLE TableX (
    KeyCol INT PRIMARY KEY,
    Description NVARCHAR(80)
);
```

This table also has an unnamed constraint. The `PRIMARY KEY` constraint has no identifier.

The collation of an identifier depends on the level at which it's defined. Identifiers of instance-level objects, such as logins and database names, are assigned the default collation of the instance. Identifiers of objects in a database, such as tables, views, and column names, are assigned the default collation of the database. For example, two tables with names that differ only in case can be created in a database that has case-sensitive collation, but can't be created in a database that has case-insensitive collation.

> [!NOTE]  
> The names of variables, or the parameters of functions and stored procedures must comply with the rules for [!INCLUDE [tsql](../../includes/tsql-md.md)] identifiers.

## Classes of identifiers

There are two classes of identifiers:

- **Regular identifiers** comply with the rules for the format of identifiers. Regular identifiers aren't delimited when they're used in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements.

  ```sql
  USE AdventureWorks2022;
  GO

  SELECT *
  FROM HumanResources.Employee
  WHERE NationalIDNumber = 153479919;
  ```

- **Delimited identifiers** are enclosed in double quotation marks (`"`) or brackets (`[` and `]`). Identifiers that comply with the rules for the format of identifiers might not be delimited. For example:

  ```sql
  USE AdventureWorks2022;
  GO

  SELECT *
  FROM [HumanResources].[Employee] --Delimiter is optional.
  WHERE [NationalIDNumber] = 153479919 --Delimiter is optional.
  ```

Identifiers that don't comply with all the rules for identifiers must be delimited in a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. For example:

```sql
USE AdventureWorks2022;
GO

--Identifier contains a space and uses a reserved keyword.
CREATE TABLE [SalesOrderDetail Table] (
    [Order] INT NOT NULL,
    [SalesOrderDetailID] INT IDENTITY(1, 1) NOT NULL,
    [OrderQty] SMALLINT NOT NULL,
    [ProductID] INT NOT NULL,
    [UnitPrice] MONEY NOT NULL,
    [UnitPriceDiscount] MONEY NOT NULL,
    [ModifiedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_SalesOrderDetail_Order_SalesOrderDetailID] PRIMARY KEY CLUSTERED (
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

## Rules for regular identifiers

The names of variables, functions, and stored procedures must comply with the following rules for [!INCLUDE [tsql](../../includes/tsql-md.md)] identifiers.

1. The first character must be one of the following items:

   - A letter as defined by the Unicode Standard 3.2. The Unicode definition of letters includes Latin characters from `a` through `z`, from `A` through `Z`, and also letter characters from other languages.

   - The underscore (`_`), at sign (`@`), or number sign (`#`).

     Certain symbols at the beginning of an identifier have special meaning in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. A regular identifier that starts with the at sign always denotes a local variable or parameter and can't be used as the name of any other type of object. An identifier that starts with a number sign denotes a temporary table or procedure. An identifier that starts with double number signs (`##`) denotes a global temporary object. Although the number sign or double number sign characters can be used to begin the names of other types of objects, we don't recommend this practice.

     Some [!INCLUDE [tsql](../../includes/tsql-md.md)] functions have names that start with double at signs (`@@`). To avoid confusion with these functions, you shouldn't use names that start with `@@`.

1. Subsequent characters can include the following list:

   - Letters as defined in the Unicode Standard 3.2.

   - Decimal numbers from either Basic Latin or other national scripts.

   - The at sign (`@`), dollar sign (`$`), number sign (`#`), or underscore (`_`).

1. The identifier must not be a [!INCLUDE [tsql](../../includes/tsql-md.md)] reserved word. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reserves both the uppercase and lowercase versions of reserved words. When identifiers are used in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, the identifiers that don't comply with these rules must be delimited by double quotation marks or brackets. The words that are reserved depend on the database compatibility level. This level can be set by using the [ALTER DATABASE compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) statement.

1. Embedded spaces or special characters aren't allowed.

1. [Supplementary characters](../../relational-databases/collations/collation-and-unicode-support.md#Supplementary_Characters) aren't allowed.

When identifiers are used in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, the identifiers that don't comply with these rules must be delimited by double quotation marks or brackets.

> [!NOTE]  
> Some rules for the format of regular identifiers depend on the database compatibility level. This level can be set by using [ALTER DATABASE compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

## Catalog collation in Azure SQL Database

You can't change or set the logical server collation on Azure SQL Database. However, you can configure each database's collations separately for data in the database and for catalog. The catalog collation determines the collation for system metadata, such as object identifiers. Both collations can be specified independently when you [create the database in the Azure portal](/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&preserve-view=true&tabs=azure-portal#create-a-single-database), in T-SQL with [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true#collation_name), in PowerShell with [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase).

For details and examples, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true#collation_name). Specify a collation for the database (`COLLATE`) and a catalog collation for system metadata and object identifiers (`CATALOG_COLLATION`).

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [CREATE DEFAULT (Transact-SQL)](../../t-sql/statements/create-default-transact-sql.md)
- [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)
- [CREATE RULE (Transact-SQL)](../../t-sql/statements/create-rule-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [CREATE TRIGGER (Transact-SQL)](../../t-sql/statements/create-trigger-transact-sql.md)
- [CREATE VIEW (Transact-SQL)](../../t-sql/statements/create-view-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
- [DELETE (Transact-SQL)](../../t-sql/statements/delete-transact-sql.md)
- [INSERT (Transact-SQL)](../../t-sql/statements/insert-transact-sql.md)
- [Reserved Keywords (Transact-SQL)](../../t-sql/language-elements/reserved-keywords-transact-sql.md)
- [SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md)
- [UPDATE (Transact-SQL)](../../t-sql/queries/update-transact-sql.md)
