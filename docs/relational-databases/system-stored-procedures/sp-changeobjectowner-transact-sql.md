---
title: "sp_changeobjectowner (Transact-SQL)"
description: sp_changeobjectowner changes the owner of an object in the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_changeobjectowner_TSQL"
  - "sp_changeobjectowner"
helpviewer_keywords:
  - "sp_changeobjectowner"
dev_langs:
  - "TSQL"
---
# sp_changeobjectowner (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the owner of an object in the current database.

> [!IMPORTANT]  
> This stored procedure only works with the objects available in [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SCHEMA](../../t-sql/statements/alter-schema-transact-sql.md) or [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md) instead. `sp_changeobjectowner` changes both the schema and the owner. To preserve compatibility with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this stored procedure will only change object owners when both the current owner and the new owner own schemas that have the same name as their database user names.  

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changeobjectowner
    [ @objname = ] N'objname'
    , [ @newowner = ] N'newowner'
[ ; ]
```

## Arguments

#### [ @objname = ] N'*objname*'

*@objname* is **nvarchar(776)**, with no default.

The name of an existing table, view, user-defined function, or stored procedure in the current database. *@objname* is an **nvarchar(776)**, with no default. *@objname* can be qualified with the owner of the existing object, in the form `<existing_owner>.<object_name>` if the schema and its owner have the same name.

#### [ @newowner = ] N'*newowner*'

The name of the security account that will be the new owner of the object. *@newowner* is **sysname**, with no default. *@newowner* must be a valid database user, server role, Windows user, or Windows group with access to the current database. If the new owner is a Windows user or Windows group for which there's no corresponding database-level principal, a database user is created.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changeobjectowner` removes all existing permissions from the object. You'll have to reapply any permissions that you want to keep after running `sp_changeobjectowner`. Therefore, we recommend that you script out existing permissions before running `sp_changeobjectowner`. After ownership of the object changes, you can use the script to reapply permissions. You must modify the object owner in the permissions script before running.

To change the owner of a securable, use `ALTER AUTHORIZATION`. To change a schema, use `ALTER SCHEMA`.

## Permissions

Requires membership in the **db_owner** fixed database role, or membership in both the **db_ddladmin** fixed database role and the **db_securityadmin** fixed database role, and also `CONTROL` permission on the object.

## Examples

The following example changes the owner of the `authors` table to `Corporate\GeorgeW`.

```sql
EXEC sp_changeobjectowner 'authors', 'Corporate\GeorgeW';
GO
```

## Related content

- [ALTER SCHEMA (Transact-SQL)](../../t-sql/statements/alter-schema-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md)
- [sp_changedbowner (Transact-SQL)](sp-changedbowner-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
