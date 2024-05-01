---
title: "xp_enumgroups (Transact-SQL)"
description: "Provides a list of local Microsoft Windows groups or a list of global groups that are defined in a specified Windows domain."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_enumgroups_TSQL"
  - "xp_enumgroups"
helpviewer_keywords:
  - "xp_enumgroups"
dev_langs:
  - "TSQL"
---
# xp_enumgroups (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Provides a list of local Microsoft Windows groups or a list of global groups that are defined in a specified Windows domain.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_enumgroups [ 'domain_name' ]
```

## Arguments

#### '*domain_name*'

The name of the Windows domain for which to enumerate a list of global groups. *domain_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| **group** | **sysname** | Name of the Windows group |
| **comment** | **sysname** | Description of the Windows group provided by Windows |

## Remarks

If *domain_name* is the name of the Windows-based computer that an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running on, or no domain name is specified, `xp_enumgroups` enumerates the local groups from the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Permissions

Requires membership in the **db_owner** fixed database role in the `master` database, or membership in the **sysadmin** fixed server role.

## Examples

The following example lists the groups in the `sales` domain.

```sql
EXEC xp_enumgroups 'sales';
```

## Related content

- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [sp_revokelogin (Transact-SQL)](sp-revokelogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
- [xp_loginconfig (Transact-SQL)](xp-loginconfig-transact-sql.md)
- [xp_logininfo (Transact-SQL)](xp-logininfo-transact-sql.md)
