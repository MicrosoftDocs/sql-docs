---
title: "sp_dropuser (Transact-SQL)"
description: sp_dropuser removes a database user from the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropuser"
  - "sp_dropuser_TSQL"
helpviewer_keywords:
  - "sp_dropuser"
dev_langs:
  - "TSQL"
---
# sp_dropuser (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a database user from the current database. `sp_dropuser` provides compatibility with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP USER](../../t-sql/statements/drop-user-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropuser [ @name_in_db = ] N'name_in_db'
[ ; ]
```

## Arguments

#### [ @name_in_db = ] N'*name_in_db*'

The name of the user to remove. *@name_in_db* is **sysname**, with no default. *@name_in_db* must exist in the current database. When specifying a Windows account, use the name by which the database knows that account.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropuser` executes `sp_revokedbaccess` to remove the user from the current database.

Use `sp_helpuser` to display a list of the user names that can be removed from the current database.

When a database user is removed, any aliases to that user are also removed. If the user owns an empty schema with the same name as the user, the schema is dropped. If the user owns any other securables in the database, the user isn't dropped. Ownership of the objects must first be transferred to another principal. For more information, see [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md). Removing a database user automatically removes the permissions associated with that user and removes the user from any database roles of which it's a member.

`sp_dropuser` can't be used to remove the database owner (**dbo**) `INFORMATION_SCHEMA` users, or the **guest** user from the `master` or `tempdb` databases. In nonsystem databases, `EXEC sp_dropuser 'guest'` revokes `CONNECT` permission from user **guest**, but the user itself isn't dropped.

`sp_dropuser` can't be executed within a user-defined transaction.

## Permissions

Requires `ALTER ANY USER` permission on the database.

## Examples

The following example removes the user `Albert` from the current database.

```sql
EXEC sp_dropuser 'Albert';
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_grantdbaccess (Transact-SQL)](sp-grantdbaccess-transact-sql.md)
- [DROP USER (Transact-SQL)](../../t-sql/statements/drop-user-transact-sql.md)
- [sp_revokedbaccess (Transact-SQL)](sp-revokedbaccess-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
