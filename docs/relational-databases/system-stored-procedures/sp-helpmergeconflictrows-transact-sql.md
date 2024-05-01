---
title: "sp_helpmergeconflictrows (Transact-SQL)"
description: "Returns the rows in the specified conflict table."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergeconflictrows_TSQL"
  - "sp_helpmergeconflictrows"
helpviewer_keywords:
  - "sp_helpmergeconflictrows"
dev_langs:
  - "TSQL"
---
# sp_helpmergeconflictrows (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the rows in the specified conflict table. This stored procedure is run on the computer where the conflict table is stored.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergeconflictrows
    [ [ @publication = ] N'publication' ]
    , [ @conflict_table = ] N'conflict_table'
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @logical_record_conflicts = ] logical_record_conflicts ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`. If the publication is specified, all conflicts qualified by the publication are returned. For example, if the `MSmerge_conflict_Customers` table has conflict rows for the `WA` and the `CA` publications, passing in a publication name `CA` retrieves conflicts that pertain to the `CA` publication.

#### [ @conflict_table = ] N'*conflict_table*'

The name of the conflict table. *@conflict_table* is **sysname**, with no default. Conflict tables are named using the format names with `MSmerge_conflict__publication_article_*`, with one table for each published article.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publisher database. *@publisher_db* is **sysname**, with a default of `NULL`.

#### [ @logical_record_conflicts = ] *logical_record_conflicts*

Indicates whether the result set contains information about logical record conflicts. *@logical_record_conflicts* is **int**, with a default of `0`. `1` means that logical record conflict information is returned.

## Result set

`sp_helpmergeconflictrows` returns a result set consisting of the base table structure and these additional columns.

| Column name | Data type | Description |
| --- | --- | --- |
| `origin_datasource` | **varchar(255)** | Origin of the conflict. |
| `conflict_type` | **int** | Code indicating the type of conflict:<br /><br />`1` = Update Conflict: The conflict is detected at the row level.<br /><br />`2` = Column Update Conflict: The conflict detected at the column level.<br /><br />`3` = Update Delete Wins Conflict: The delete wins the conflict.<br /><br />`4` = Update Wins Delete Conflict: The deleted `rowguid` that loses the conflict is recorded in this table.<br /><br />`5` = Upload Insert Failed: The insert from Subscriber couldn't be applied at the Publisher.<br /><br />`6` = Download Insert Failed: The insert from Publisher couldn't be applied at the Subscriber.<br /><br />`7` = Upload Delete Failed: The delete at Subscriber couldn't be uploaded to the Publisher.<br /><br />`8` = Download Delete Failed: The delete at Publisher couldn't be downloaded to the Subscriber.<br /><br />`9` = Upload Update Failed: The update at Subscriber couldn't be applied at the Publisher.<br /><br />`10` = Download Update Failed: The update at Publisher couldn't be applied to the Subscriber.<br /><br />`12` = Logical Record Update Wins Delete: The deleted logical record that loses the conflict is recorded in this table.<br /><br />`13` = Logical Record Conflict Insert Update: Insert to a logical record conflicts with an update.<br /><br />`14` = Logical Record Delete Wins Update Conflict: The updated logical record that loses the conflict is recorded in this table. |
| `reason_code` | **int** | Error code that can be context-sensitive. |
| `reason_text` | **varchar(720)** | Error description that can be context-sensitive. |
| `pubid` | **uniqueidentifier** | Publication identifier. |
| `MSrepl_create_time` | **datetime** | Time the conflict information was added. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergeconflictrows` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, and the **replmonitor** role in the distribution database can execute `sp_helpmergeconflictrows`.

## Related content

- [Conflict resolution for Merge Replication](../replication/view-and-resolve-data-conflicts-for-merge-publications.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
