---
title: "sp_helpreplicationdboption (Transact-SQL)"
description: sp_helpreplicationdboption shows whether databases at the Publisher are enabled for replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpreplicationdboption_TSQL"
  - "sp_helpreplicationdboption"
helpviewer_keywords:
  - "sp_helpreplicationdboption"
dev_langs:
  - "TSQL"
---
# sp_helpreplicationdboption (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Shows whether databases at the Publisher are enabled for replication. This stored procedure is executed at the Publisher on any database. *Not supported for Oracle Publishers.*

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpreplicationdboption
    [ [ @dbname = ] N'dbname' ]
    [ , [ @type = ] N'type' ]
    [ , [ @reserved = ] reserved ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database. *@dbname* is **sysname**, with a default of `%`, which returns all databases at the Publisher, otherwise only information on the specified database is returned. Information isn't returned for any databases on which the user doesn't have the appropriate permissions.

#### [ @type = ] N'*type*'

Restricts the result set to contain only databases on which the specified replication option *@type* value is enabled. *@type* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `publish` | Transactional replication allowed. |
| `merge publish` | Merge replication allowed. |
| `replication allowed (default)` | Either transactional or merge replication allowed. |

#### [ @reserved = ] *reserved*

Specifies whether information on existing publications and subscriptions is returned. *@reserved* is **bit**, with a default of `0`. If `1`, the result set includes information on whether the database specified has any existing publications or subscriptions.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Name of the database. |
| `id` | **int** | Database identifier. |
| `transpublish` | **bit** | If the database is enabled for snapshot or transactional publishing; where a value of `1` means that snapshot or transactional publishing is enabled. |
| `mergepublish` | **bit** | If the database is enabled for merge publishing; where a value of `1` means that merge publishing is enabled. |
| `dbowner` | **bit** | If the user is a member of the **db_owner** fixed database role; where a value of `1` indicates that the user is a member of this role. |
| `dbreadonly` | **bit** | Is if the database is marked as read-only; where a value of `1` means that the database is read-only. |
| `haspublications` | **bit** | Is if the database has any existing publications; where a value of `1` means that there are existing publications. |
| `haspullsubscriptions` | **bit** | Is if the database has any existing pull subscriptions; where a value of `1` means that there are existing pull subscriptions. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpreplicationdboption` is used in snapshot, transactional, and merge replication.

## Permissions

Members of the **sysadmin** fixed server role can execute `sp_helpreplicationdboption` for any database. Members of the **db_owner** fixed database role can execute `sp_helpreplicationdboption` for that database.

## Related content

- [sp_replicationdboption (Transact-SQL)](sp-replicationdboption-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
