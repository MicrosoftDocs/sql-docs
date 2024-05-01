---
title: "sp_bindefault (Transact-SQL)"
description: sp_bindefault binds a default to a column or to an alias data type.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_bindefault"
  - "sp_bindefault_TSQL"
helpviewer_keywords:
  - "sp_bindefault"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_bindefault (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Binds a default to a column or to an alias data type.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you create default definitions by using the DEFAULT keyword of the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) statements instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_bindefault
    [ @defname = ] N'defname'
    , [ @objname = ] N'objname'
    [ , [ @futureonly = ] 'futureonly' ]
[ ; ]
```

## Arguments

#### [ @defname = ] N'*defname*'

The name of the default created by `CREATE DEFAULT`. *@defname* is **nvarchar(776)**, with no default.

#### [ @objname = ] N'*objname*'

The name of table and column, or the alias data type, to which the default is to be bound. *@objname* is **nvarchar(776)**, with no default. *@objname* can't be defined with **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, or CLR user-defined types.

If *@objname* is a one-part name, it resolves as an alias data type. If it's a two- or three-part name, it first resolves as a table and column; and if this resolution fails, it resolves as an alias data type. By default, existing columns of the alias data type inherit *@defname*, unless a default is bound directly to the column. A default can't be bound to a **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **timestamp**, or CLR user-defined type column, a column with the `IDENTITY` property, a computed column, or a column that already has a `DEFAULT` constraint.

*@objname* can contain brackets (`[` and `]`) as delimited identifiers. For more information, see [Database identifiers](../databases/database-identifiers.md).

#### [ @futureonly = ] '*futureonly*'

Used only when binding a default to an alias data type. *@futureonly* is **varchar(15)**, with a default of `NULL`. When this parameter is set to `futureonly`, existing columns of that data type can't inherit the new default. This parameter is never used when binding a default to a column. If *@futureonly* is `NULL`, the new default is bound to any columns of the alias data type that currently have no default or that are using the existing default of the alias data type.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You can use `sp_bindefault` to bind a new default to a column, although using the `DEFAULT` constraint is preferred, or to an alias data type without unbinding an existing default. The old default is overridden. You can't bind a default to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system data type or a CLR user-defined type. If the default isn't compatible with the column to which you have bound it, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] returns an error message when it tries to insert the default value, not when you bind it.

Existing columns of the alias data type inherit the new default, unless either a default is bound directly to them or *futureonly* is specified as `futureonly`. New columns of the alias data type always inherit the default.

When you bind a default to a column, related information is added to the `sys.columns` catalog view. When you bind a default to an alias data type, related information is added to the `sys.types` catalog view.

## Permissions

User must own the table, or be a member of the **sysadmin** fixed server role, or the **db_owner** and **db_ddladmin** fixed database roles.

## Examples

### A. Bind a default to a column

A default named `today` is defined in the current database by using `CREATE DEFAULT`. The following example binds the default to the `HireDate` column of the `Employee` table. Whenever a row is added to the `Employee` table and data for the `HireDate` column isn't supplied, the column gets the value of the default `today`.

```sql
USE master;
GO

EXEC sp_bindefault 'today', 'HumanResources.Employee.HireDate';
```

### B. Bind a default to an alias data type

A default named `def_ssn` and an alias data type named `ssn` already exists. The following example binds the default `def_ssn` to `ssn`. When a table is created, the default is inherited by all columns that are assigned the alias data type `ssn`. Existing columns of type `ssn` also inherit the default `def_ssn`, unless `futureonly` is specified for the *@futureonly* value, or the column has a default bound directly to it. Defaults bound to columns always take precedence over defaults bound to data types.

```sql
USE master;
GO

EXEC sp_bindefault 'def_ssn', 'ssn';
```

### C. Use the `futureonly` option

The following example binds the default `def_ssn` to the alias data type `ssn`. Because `futureonly` is specified, no existing columns of type `ssn` are affected.

```sql
USE master;
GO

EXEC sp_bindefault 'def_ssn', 'ssn', 'futureonly';
```

### D. Use delimited identifiers

The following example shows using delimited identifiers, `[t.1]`, in *@objname*.

```sql
USE master;
GO

CREATE TABLE [t.1] (c1 int);
-- Notice the period as part of the table name.
EXEC sp_bindefault 'default1', '[t.1].c1';
-- The object contains two periods;
-- the first is part of the table name,
-- and the second distinguishes the table name from the column name.
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [CREATE DEFAULT (Transact-SQL)](../../t-sql/statements/create-default-transact-sql.md)
- [DROP DEFAULT (Transact-SQL)](../../t-sql/statements/drop-default-transact-sql.md)
- [sp_unbindefault (Transact-SQL)](sp-unbindefault-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
