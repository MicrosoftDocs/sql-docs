---
title: "sp_certify_removable (Transact-SQL)"
description: Verifies that a database is correctly configured for distribution on removable media and reports any problems to the user.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_certify_removable_TSQL"
  - "sp_certify_removable"
helpviewer_keywords:
  - "sp_certify_removable"
dev_langs:
  - "TSQL"
---
# sp_certify_removable (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Verifies that a database is correctly configured for distribution on removable media and reports any problems to the user.

> [!IMPORTANT]  
> [!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_certify_removable
    [ @dbname = ] N'dbname'
    [ , [ @autofix = ] N'autofix' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

Specifies the database to be verified. *@dbname* is **sysname**.

#### [ @autofix = ] N'*autofix*'

Gives ownership of the database and all database objects to the system administrator, and drops any user-created database users and nondefault permissions. *@autofix* is **nvarchar(4)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

If the database is correctly configured, `sp_certify_removable` performs the following steps:

- Sets the database offline so the files can be copied.
- Updates statistics on all tables and reports any ownership or user problems
- Marks the data filegroups as read-only so these files can be copied to read-only media.

The system administrator must be the owner of the database and all database objects. The system administrator is a known user that exists on all servers running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and can be expected to exist when the database is later distributed and installed.

If you run `sp_certify_removable` without the `AUTO` value and it returns information about any of the following conditions:

- The system administrator isn't the database owner.
- Any user-created users exist.
- The system administrator doesn't own all objects in the database.
- Nondefault permissions have been granted.

You can correct those conditions in the following ways:

- Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tools and procedures, and then run `sp_certify_removable` again.
- Run `sp_certify_removable` with the `AUTO` value.

This stored procedure only checks for users and user permissions. You can add groups to the database and to grant permissions to those groups. For more information, see [GRANT](../../t-sql/statements/grant-transact-sql.md).

## Permissions

Execute permissions are restricted to members of the **sysadmin** fixed server role.

## Examples

The following example certifies that the `inventory` database is ready to be removed.

```sql
EXEC sp_certify_removable inventory, AUTO;
```

## Related content

- [Database detach and attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)
- [sp_create_removable (Transact-SQL)](sp-create-removable-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sp_dbremove (Transact-SQL)](sp-dbremove-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
