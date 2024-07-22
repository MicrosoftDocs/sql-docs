---
title: "sp_dbcmptlevel (Transact-SQL)"
description: sp_dbcmptlevel sets certain database behaviors to be compatible with the specified version of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbcmptlevel"
  - "sp_dbcmptlevel_TSQL"
helpviewer_keywords:
  - "sp_dbcmptlevel"
dev_langs:
  - "TSQL"
---
# sp_dbcmptlevel (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets certain database behaviors to be compatible with the specified version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER DATABASE (Transact-SQL) compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbcmptlevel
    [ [ @dbname = ] N'dbname' ]
    [ , [ @new_cmptlevel = ] new_cmptlevel OUTPUT ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database for which the compatibility level is to be changed. Database names must conform to the rules for identifiers. *@dbname* is **sysname**, with a default of `NULL`.

#### [ @new_cmptlevel = ] *new_cmptlevel* OUTPUT

The version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with which the database is to be made compatible. *@new_cmptlevel* is an OUTPUT parameter of type **tinyint**, and must be one of the following values:

- `90` = [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]
- `100` = [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)]
- `110` = [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)]
- `120` = [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)]
- `130` = [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]
- `140` = [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]
- `150` = [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]
- `160` = [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

If no parameters are specified or if the *@dbname* parameter isn't specified, `sp_dbcmptlevel` returns an error.

If *@dbname* is specified without *@new_cmptlevel*, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] returns a message displaying the current compatibility level of the specified database.

## Remarks

For a description of compatibilities levels, see [ALTER DATABASE (Transact-SQL) compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

## Permissions

Only the database owner, members of the **sysadmin** fixed server role, and the **db_owner** fixed database role (if you're changing the current database) can execute this procedure.

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Reserved Keywords (Transact-SQL)](../../t-sql/language-elements/reserved-keywords-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
