---
title: "sp_validatelogins (Transact-SQL)"
description: Reports information about Windows users and groups that are mapped to SQL Server principals but no longer exist in the Windows environment.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_validatelogins"
  - "sp_validatelogins_TSQL"
helpviewer_keywords:
  - "sp_validatelogins"
dev_langs:
  - "TSQL"
---
# sp_validatelogins (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about Windows users and groups that are mapped to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] principals but no longer exist in the Windows environment.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_validatelogins
[ ; ]
```

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `SID` | **varbinary(85)** | Windows security identifier (SID) of the Windows user or group. |
| `NT Login` | **sysname** | Name of the Windows user or group. |

## Remarks

If the orphaned server-level principal owns a database user, the database user must be removed before the orphaned server principal can be removed. To remove a database user, use [DROP USER](../../t-sql/statements/drop-user-transact-sql.md). If the server-level principal owns securables in the database, ownership of the securables must be transferred or they must be dropped. To transfer ownership of database securables, use [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md).

To remove mappings to Windows users and groups that no longer exist, use [DROP LOGIN](../../t-sql/statements/drop-login-transact-sql.md).

## Permissions

Requires membership in the **sysadmin** or **securityadmin** fixed server role.

## Examples

The following example displays the Windows users and groups that no longer exist but are still granted access to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
EXEC sp_validatelogins;
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [DROP USER (Transact-SQL)](../../t-sql/statements/drop-user-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](../../t-sql/statements/drop-login-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md)
