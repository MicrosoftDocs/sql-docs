---
title: "sp_dropapprole (Transact-SQL)"
description: sp_dropapprole removes an application role from the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropapprole_TSQL"
  - "sp_dropapprole"
helpviewer_keywords:
  - "sp_dropapprole"
dev_langs:
  - "TSQL"
---
# sp_dropapprole (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an application role from the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP APPLICATION ROLE](../../t-sql/statements/drop-application-role-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropapprole [ @rolename = ] N'rolename'
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The application role to remove. *@rolename* is **sysname**, with no default. *@rolename* must exist in the current database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropapprole` can only be used to remove application roles. If a role owns any securables, the role can't be dropped. Before dropping an application role that owns securables, you must first transfer ownership of the securables, or drop them.

`sp_dropapprole` can't be executed within a user-defined transaction.

## Permissions

Requires `ALTER ANY APPLICATION ROLE` permission on the database.

## Examples

The following example removes the `SalesApp` application role from the current database.

```sql
EXEC sp_dropapprole 'SalesApp';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addapprole (Transact-SQL)](sp-addapprole-transact-sql.md)
- [DROP APPLICATION ROLE (Transact-SQL)](../../t-sql/statements/drop-application-role-transact-sql.md)
- [sp_changeobjectowner (Transact-SQL)](sp-changeobjectowner-transact-sql.md)
- [sp_setapprole (Transact-SQL)](sp-setapprole-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
