---
title: "sp_helpreplicationoption (Transact-SQL)"
description: sp_helpreplicationoption shows the types of replication options enabled for a server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpreplicationoption"
  - "sp_helpreplicationoption_TSQL"
helpviewer_keywords:
  - "sp_helpreplicationoption"
dev_langs:
  - "TSQL"
---
# sp_helpreplicationoption (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Shows the types of replication options enabled for a server. This stored procedure is executed at any server on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpreplicationoption [ [ @optname = ] N'optname' ]
[ ; ]
```

## Arguments

#### [ @optname = ] N'*optname*'

The name of the replication option to query for. *@optname* is **sysname**, with a default of `NULL`.

| Value | Description |
| --- | --- |
| `transactional` | A result set is returned when transactional replication is enabled. |
| `merge` | A result set is returned when merge replication is enabled. |
| `NULL` (default) | A result set isn't returned. |

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `optname` | **sysname** | Name of the replication option and can be one of the following values:<br /><br />`transactional`<br />`merge` |
| `value` | **bit** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `major_version` | **int** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `minor_version` | **int** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `revision` | **int** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `install_failures` | **int** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpreplicationoption` is used to get information about replication options enabled on a particular server. To get information on a particular database, use `sp_helpreplicationdboption`.

## Permissions

Execute permissions default to the **public** role.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
