---
title: "sp_showrowreplicainfo (Transact-SQL)"
description: sp_showrowreplicainfo displays information about a row in a table that is being used as an article in merge replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_showrowreplicainfo_TSQL"
  - "sp_showrowreplicainfo"
helpviewer_keywords:
  - "sp_showrowreplicainfo"
dev_langs:
  - "TSQL"
---
# sp_showrowreplicainfo (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays information about a row in a table that is being used as an article in merge replication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_showrowreplicainfo
    [ [ @ownername = ] N'ownername' ]
    [ , [ @tablename = ] N'tablename' ]
    , [ @rowguid = ] 'rowguid'
    [ , [ @show = ] N'show' ]
[ ; ]
```

## Arguments

#### [ @ownername = ] N'*ownername*'

The name of the table owner. *@ownername* is **sysname**, with a default of `NULL`. This parameter is useful to differentiate tables if a database contains multiple tables with the same name, but each table has a different owner.

#### [ @tablename = ] N'*tablename*'

The name of the table that contains the row for which the information is returned. *@tablename* is **sysname**, with a default of `NULL`.

#### [ @rowguid = ] '*rowguid*'

The unique identifier of the row. *@rowguid* is **uniqueidentifier**, with no default.

#### [ @show = ] N'*show*'

Determines the amount of information to return in the result set. *@show* is **nvarchar(20)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `row` | Only row version information is returned |
| `columns` | Only column version information is returned |
| `both` (default) | Information for both row and column is returned |

## Result set

The results depend on the value provided for *@show*.

### Result set for row information

| Column name | Data type | Description |
| --- | --- | --- |
| `server_name` | **sysname** | Name of the server hosting the database that made the row version entry. |
| `db_name` | **sysname** | Name of the database that made this entry. |
| `db_nickname` | **binary(6)** | Nickname of the database that made this entry. |
| `version` | **int** | Version of the entry. |
| `current_state` | **nvarchar(9)** | Returns information on the current state of the row.<br /><br />`y` - Row data represents the current state of the row.<br />`n` - Row data doesn't represent the current state of the row.<br />`<n/a>` - Not applicable.<br />`<unknown>` - Current state can't be determined. |
| `rowversion_table` | **nchar(17)** | Indicates whether the row versions are stored in the [MSmerge_contents](../system-tables/msmerge-contents-transact-sql.md) table or the [MSmerge_tombstone](../system-tables/msmerge-tombstone-transact-sql.md) table. |
| `comment` | **nvarchar(255)** | Additional information about this row version entry. Usually, this field is empty. |

### Result set for column information

| Column name | Data type | Description |
| --- | --- | --- |
| `server_name` | **sysname** | Name of the server hosting the database that made the column version entry. |
| `db_name` | **sysname** | Name of the database that made this entry. |
| `db_nickname` | **binary(6)** | Nickname of the database that made this entry. |
| `version` | **int** | Version of the entry. |
| `colname` | **sysname** | Name of the article column that the column version entry represents. |
| `comment` | **nvarchar(255)** | Additional information about this column version entry. Usually, this field is empty. |

### Result set for both

If the value `both` is chosen for *@show*, then both the row and column result sets is returned.

## Remarks

`sp_showrowreplicainfo` is used in merge replication.

## Permissions

`sp_showrowreplicainfo` can only be executed by members of the **db_owner** fixed database role on the publication database or by members of the publication access list (PAL) on the publication database.

## Related content

- [Advanced Merge Replication - Conflict Detection and Resolution](../replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
