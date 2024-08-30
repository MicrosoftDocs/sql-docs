---
title: "sp_droprolemember (Transact-SQL)"
description: sp_droprolemember removes a security account from a SQL Server role in the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_droprolemember_TSQL"
  - "sp_droprolemember"
helpviewer_keywords:
  - "sp_droprolemember"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_droprolemember (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Removes a security account from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] role in the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER ROLE](../../t-sql/statements/alter-role-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazurede-md](../../includes/ssazurede-md.md)].

```syntaxsql
sp_droprolemember
    [ @rolename = ] N'rolename'
    , [ @membername = ] N'membername'
[ ; ]
```

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
sp_droprolemember N'rolename' , 'membername'
[ ; ]
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of the role from which the member is being removed. *@rolename* is **sysname**, with no default. *@rolename* must exist in the current database.

#### [ @membername = ] N'*membername*'

The name of the security account being removed from the role. *@membername* is **sysname**, with no default. *@membername* can be a database user, another database role, a Windows account, or a Windows group. *@membername* must exist in the current database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_droprolemember` removes a member from a database role by deleting a row from the `sysmembers` table. When a member is removed from a role, the member loses any permissions it has by membership in that role.

To remove a user from a fixed server role, use `sp_dropsrvrolemember`. Users can't be removed from the **public** role, and `dbo` can't be removed from any role.

Use `sp_helpuser` to see the members of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] role, and use `ALTER ROLE` to add a member to a role.

## Permissions

Requires `ALTER` permission on the role.

## Examples

The following example removes the user `JonB` from the role `Sales`.

```sql
EXEC sp_droprolemember 'Sales', 'Jonb';
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example removes the user `JonB` from the role `Sales`.

```sql
EXEC sp_droprolemember 'Sales', 'JonB'
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addrolemember (Transact-SQL)](sp-addrolemember-transact-sql.md)
- [sp_droprole (Transact-SQL)](sp-droprole-transact-sql.md)
- [sp_dropsrvrolemember (Transact-SQL)](sp-dropsrvrolemember-transact-sql.md)
- [sp_helpuser (Transact-SQL)](sp-helpuser-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
