---
title: "sp_helpmergearticleconflicts (Transact-SQL)"
description: "Returns the articles in the publication that have conflicts."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergearticleconflicts"
  - "sp_helpmergearticleconflicts_TSQL"
helpviewer_keywords:
  - "sp_helpmergearticleconflicts"
dev_langs:
  - "TSQL"
---
# sp_helpmergearticleconflicts (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the articles in the publication that have conflicts. This stored procedure is executed at the Publisher on the publication database, or at the Subscriber on the merge subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergearticleconflicts
    [ [ @publication = ] N'publication' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the merge publication. *@publication* is **sysname**, with a default of `%`, which returns all articles in the database that have conflicts.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publisher database. *@publisher_db* is **sysname**, with a default of `NULL`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `article` | **sysname** | Name of the article. |
| `source_owner` | **sysname** | Owner of the source object. |
| `source_object` | **nvarchar(386)** | Name of the source object. |
| `conflict_table` | **nvarchar(258)** | Name of the table storing the insert or update conflicts. |
| `guidcolname` | **sysname** | Name of the `rowguidcol` for the source object. |
| `centralized_conflicts` | **int** | Specifies whether conflict records are stored on the given Publisher. |

If the article has only delete conflicts and no `conflict_table` rows, the name of the `conflict_table` in the result set is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergearticleconflicts` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_helpmergearticleconflicts`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
