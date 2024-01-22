---
title: "sp_helpntgroup (Transact-SQL)"
description: sp_helpntgroup reports information about Windows groups with accounts in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpntgroup"
  - "sp_helpntgroup_TSQL"
helpviewer_keywords:
  - "sp_helpntgroup"
dev_langs:
  - "TSQL"
---
# sp_helpntgroup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about Windows groups with accounts in the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpntgroup [ [ @ntname = ] N'ntname' ]
[ ; ]
```

## Arguments

#### [ @ntname = ] N'*ntname*'

The name of the Windows group. *@ntname* is **sysname**, with a default of `NULL`. *@ntname* must be a valid Windows group with access to the current database. If *@ntname* isn't specified, all Windows groups with access to the current database are included in the output.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `NTGroupName` | **sysname** | Name of the Windows group. |
| `NTGroupId` | **smallint** | Group identifier (ID). |
| `SID` | **varbinary(85)** | Security identifier (SID) of `NTGroupName`. |
| `HasDbAccess` | **int** | `1` = Windows group has permission to access the database. |

## Remarks

To see a list of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] roles in the current database, use `sp_helprole`.

## Permissions

Requires membership in the **public** role.

## Examples

The following example prints a list of the Windows groups with access to the current database.

```sql
EXEC sp_helpntgroup;
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_grantdbaccess (Transact-SQL)](sp-grantdbaccess-transact-sql.md)
- [sp_helprole (Transact-SQL)](sp-helprole-transact-sql.md)
- [sp_revokedbaccess (Transact-SQL)](sp-revokedbaccess-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
