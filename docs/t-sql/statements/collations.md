---
title: "COLLATE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/21/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COLLATE"
  - "COLLATE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "collations [SQL Server], COLLATE clause"
  - "COLLATE clause"
ms.assetid: 76763ac8-3e0d-4bbb-aa53-f5e7da021daa
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# COLLATE (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Defines a collation of a database or table column, or a collation cast operation when applied to character string expression. Collation name can be either a Windows collation name or a SQL collation name. If not specified during database creation, the database is assigned the default collation of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If not specified during table column creation, the column is assigned the default collation of the database.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```
COLLATE { <collation_name> | database_default }
<collation_name> :: =
    { Windows_collation_name } | { SQL_collation_name }
```

## Arguments

*collation_name*
Is the name of the collation to be applied to the expression, column definition, or database definition. *collation_name* can be only a specified *Windows_collation_name* or a *SQL_collation_name*. *collation_name* must be a literal value. *collation_name* cannot be represented by a variable or expression.

*Windows_collation_name* is the collation name for a [Windows Collation Name](../../t-sql/statements/windows-collation-name-transact-sql.md).

*SQL_collation_name* is the collation name for a [SQL Server Collation Name](../../t-sql/statements/sql-server-collation-name-transact-sql.md).

**database_default**
Causes the COLLATE clause to inherit the collation of the current database.

## Remarks

The COLLATE clause can be specified at several levels. These include the following:

1. Creating or altering a database.

    You can use the COLLATE clause of the `CREATE DATABASE` or `ALTER DATABASE` statement to specify the default collation of the database. You can also specify a collation when you create a database using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If you do not specify a collation, the database is assigned the default collation of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

    > [!NOTE]
    > Windows Unicode-only collations can only be used with the COLLATE clause to apply collations to the **nchar**, **nvarchar**, and **ntext** data types on column-level and expression-level data; these cannot be used with the COLLATE clause to define or change the collation of a database or server instance.

2. Creating or altering a table column.

    You can specify collations for each character string column using the COLLATE clause of the `CREATE TABLE` or `ALTER TABLE` statement. You can also specify a collation when you create a table using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If you do not specify a collation, the column is assigned the default collation of the database.

    You can also use the `database_default` option in the COLLATE clause to specify that a column in a temporary table use the collation default of the current user database for the connection instead of **tempdb**.

3. Casting the collation of an expression.

    You can use the COLLATE clause to apply a character expression to a certain collation. Character literals and variables are assigned the default collation of the current database. Column references are assigned the definition collation of the column.

The collation of an identifier depends on the level at which it is defined. Identifiers of instance-level objects, such as logins and database names, are assigned the default collation of the instance. Identifiers of objects within a database, such as tables, views, and column names, are assigned the default collation of the database. For example, two tables with names different only in case may be created in a database with case-sensitive collation, but may not be created in a database with case-insensitive collation. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

Variables, GOTO labels, temporary stored procedures, and temporary tables can be created when the connection context is associated with one database, and then referenced when the context has been switched to another database. The identifiers for variables, GOTO labels, temporary stored procedures, and temporary tables are in the default collation of the server instance.

The COLLATE clause can be applied only for the **char**, **varchar**, **text**, **nchar**, **nvarchar**, and **ntext** data types.

COLLATE uses *collate_name* to refer to the name of either the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation or the Windows collation to be applied to the expression, column definition, or database definition. *collation_name* can be only a specified *Windows_collation_name* or a *SQL_collation_name* and the parameter must contain a literal value. *collation_name* cannot be represented by a variable or expression.

Collations are generally identified by a collation name, except in Setup. In Setup, you instead specify the root collation designator (the collation locale) for Windows collations, and then specify sort options that are sensitive or insensitive to case or accents.

You can execute the system function [fn_helpcollations](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md) to retrieve a list of all the valid collation names for Windows collations and SQL Server collations:

```sql
SELECT name, description
FROM fn_helpcollations();
```

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can support only code pages that are supported by the underlying operating system. When you perform an action that depends on collations, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation used by the referenced object must use a code page supported by the operating system running on the computer. These actions can include the following:

- Specifying a default collation for a database when you create or alter the database.
- Specifying a collation for a column when you create or alter a table.
- When restoring or attaching a database, the default collation of the database and the collation of any **char**, **varchar**, and **text** columns or parameters in the database must be supported by the operating system.

> [!NOTE]
> [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] managed instance server collation is **SQL_Latin1_General_CP1_CI_AS** and cannot be changed.
>
> Code page translations are supported for **char** and **varchar** data types, but not for **text** data type. Data loss during code page translations is not reported.
>
> If the collation specified or the collation used by the referenced object uses a code page not supported by Windows, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] displays an error.

## Examples

### A. Specifying collation during a SELECT

The following example creates a simple table and inserts 4 rows. Then the example applies two collations when selecting data from the table, demonstrating how `Chiapas` is sorted differently.

```sql
CREATE TABLE Locations
(Place varchar(15) NOT NULL);
GO
INSERT Locations(Place) VALUES ('Chiapas'),('Colima')
                             , ('Cinco Rios'), ('California');
GO
--Apply an typical collation
SELECT Place FROM Locations
ORDER BY Place
COLLATE Latin1_General_CS_AS_KS_WS ASC;
GO
-- Apply a Spanish collation
SELECT Place FROM Locations
ORDER BY Place
COLLATE Traditional_Spanish_ci_ai ASC;
GO
```

Here are the results from the first query.

```
Place
-------------
California
Chiapas
Cinco Rios
Colima
```

Here are the results from the second query.

```
Place
-------------
California
Cinco Rios
Colima
Chiapas
```

### B. Additional examples

For additional examples that use **COLLATE**, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=sql-server-2017#examples) example **G. Creating a database and specifying a collation name and options**, and [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md#alter_column) example **V. Changing column collation**.

## See Also

- [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md)
- [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)
- [Collation Precedence](../../t-sql/statements/collation-precedence-transact-sql.md)
- [Constants](../../t-sql/data-types/constants-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=sql-server-2017)
- [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md)
- [DECLARE @local_variable](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
- [Table data type](../../t-sql/data-types/table-transact-sql.md)
