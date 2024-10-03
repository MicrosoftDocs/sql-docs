---
title: "sp_renamedb (Transact-SQL)"
description: sp_renamedb changes the name of a database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_renamedb"
  - "sp_renamedb_TSQL"
helpviewer_keywords:
  - "sp_renamedb"
dev_langs:
  - "TSQL"
---
# sp_renamedb (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the name of a database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use `ALTER DATABASE MODIFY NAME` instead. For more information, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_renamedb
    [ @dbname = ] N'dbname'
    , [ @newname = ] N'newname'
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The current name of the database. *@dbname* is **sysname**, with no default.

#### [ @newname = ] N'*newname*'

The new name of the database. *@newname* is **sysname**, with no default. *@newname* must follow the rules for identifiers.

## Return code values

`0` (success) or a nonzero number (failure).

## Remarks

It isn't possible to rename an Azure SQL database configured in an [active geo-replication](/azure/azure-sql/database/active-geo-replication-overview) relationship.

## Permissions

Requires membership in the **sysadmin** or **dbcreator** fixed server roles.

## Examples

The following example creates the `Accounting` database and then changes the name of the database to `Financial`. The `sys.databases` catalog view is then queried to verify the new name of the database.

```sql
USE master;
GO

CREATE DATABASE Accounting;
GO

EXEC sp_renamedb N'Accounting', N'Financial';
GO

SELECT name,
    database_id,
    create_date
FROM sys.databases
WHERE name = N'Financial';
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sp_changedbowner (Transact-SQL)](sp-changedbowner-transact-sql.md)
- [sp_helpdb (Transact-SQL)](sp-helpdb-transact-sql.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
