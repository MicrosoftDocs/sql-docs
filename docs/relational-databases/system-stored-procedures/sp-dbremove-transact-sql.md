---
title: "sp_dbremove (Transact-SQL)"
description: sp_dbremove removes a database and all files associated with that database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbremove"
  - "sp_dbremove_TSQL"
helpviewer_keywords:
  - "sp_dbremove"
dev_langs:
  - "TSQL"
---
# sp_dbremove (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a database and all files associated with that database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbremove
    [ [ @dbname = ] N'dbname' ]
    [ , [ @dropdev = ] 'dropdev' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database to be removed. *@dbname* is **sysname**, with a default of `NULL`.

#### [ @dropdev = ] 'dropdev'

A flag provided for backward compatibility only and is currently ignored. *@dropdev* is **varchar(10)**, with a default of `dropdev`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example removes a database named `sales` and all files associated with it.

```sql
EXEC sp_dbremove sales;
```

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [sp_detach_db (Transact-SQL)](sp-detach-db-transact-sql.md)
