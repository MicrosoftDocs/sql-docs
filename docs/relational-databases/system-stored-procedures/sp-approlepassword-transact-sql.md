---
title: "sp_approlepassword (Transact-SQL)"
description: sp_approlepassword changes the password of an application role in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_approlepassword"
  - "sp_approlepassword_TSQL"
helpviewer_keywords:
  - "sp_approlepassword"
dev_langs:
  - "TSQL"
---
# sp_approlepassword (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the password of an application role in the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER APPLICATION ROLE](../../t-sql/statements/alter-application-role-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_approlepassword
    [ @rolename = ] N'rolename'
    , [ @newpwd = ] N'newpwd'
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of the application role. *@rolename* is **sysname**, with no default. *@rolename* must exist in the current database.

#### [ @newpwd = ] N'*newpwd*'

The new password for the application role. *@newpwd* is **sysname**, with no default. *@newpwd* can't be `NULL`.

> [!IMPORTANT]  
> Don't use a `NULL` password. Use a strong password. For more information, see [Strong Passwords](../security/strong-passwords.md).

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_approlepassword` can't be executed within a user-defined transaction.

## Permissions

Requires `ALTER ANY APPLICATION ROLE` permission on the database.

## Examples

The following example sets the password for the `PayrollAppRole` application role to `B3r12-36`.

```sql
EXEC sp_approlepassword 'PayrollAppRole', 'B3r12-36';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [Application Roles](../security/authentication-access/application-roles.md)
- [sp_addapprole (Transact-SQL)](sp-addapprole-transact-sql.md)
- [sp_setapprole (Transact-SQL)](sp-setapprole-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
