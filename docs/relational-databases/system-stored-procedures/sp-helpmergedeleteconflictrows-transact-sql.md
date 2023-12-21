---
title: "sp_helpmergedeleteconflictrows (Transact-SQL)"
description: "Returns information on data rows that lost delete conflicts."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergedeleteconflictrows"
  - "sp_helpmergedeleteconflictrows_TSQL"
helpviewer_keywords:
  - "sp_helpmergedeleteconflictrows"
dev_langs:
  - "TSQL"
---
# sp_helpmergedeleteconflictrows (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information on data rows that lost delete conflicts. This stored procedure is executed at the Publisher on the publication database or at the Subscriber on the subscription database when decentralized conflict logging is used.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergedeleteconflictrows
    [ [ @publication = ] N'publication' ]
    [ , [ @source_object = ] N'source_object' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @logical_record_conflicts = ] logical_record_conflicts ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`. If the publication is specified, all conflicts qualified by the publication are returned.

#### [ @source_object = ] N'*source_object*'

The name of the source object. *@source_object* is **nvarchar(386)**, with a default of `NULL`.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publisher database. *@publisher_db* is **sysname**, with a default of `NULL`.

#### [ @logical_record_conflicts = ] *logical_record_conflicts*

*@logical_record_conflicts* is **int**, with a default of `0`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `source_object` | **nvarchar(386)** | Source object for the delete conflict. |
| `rowguid` | **uniqueidentifier** | Row identifier for the delete conflict. |
| `conflict_type` | **int** | Code indicating type of conflict:<br /><br />`1` = UpdateConflict: Conflict is detected at the row level.<br /><br />`2` = ColumnUpdateConflict: Conflict detected at the column level.<br /><br />`3` = UpdateDeleteWinsConflict: Delete wins the conflict.<br /><br />`4` = UpdateWinsDeleteConflict: The deleted `rowguid` that loses the conflict is recorded in this table.<br /><br />`5` = UploadInsertFailed: Insert from Subscriber couldn't be applied at the Publisher.<br /><br />`6` = DownloadInsertFailed: Insert from Publisher couldn't be applied at the Subscriber.<br /><br />`7` = UploadDeleteFailed: Delete at Subscriber couldn't be uploaded to the Publisher.<br /><br />`8` = DownloadDeleteFailed: Delete at Publisher couldn't be downloaded to the Subscriber.<br /><br />`9` = UploadUpdateFailed: Update at Subscriber couldn't be applied at the Publisher.<br /><br />`10` = DownloadUpdateFailed: Update at Publisher couldn't be applied to the Subscriber. |
| `reason_code` | **Int** | Error code that can be context-sensitive. |
| `reason_text` | **varchar(720)** | Error description that can be context-sensitive. |
| `origin_datasource` | **varchar(255)** | Origin of the conflict. |
| `pubid` | **uniqueidentifier** | Publication identifier. |
| `MSrepl_create_time` | **datetime** | Time the conflict information was added. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergedeleteconflictrows` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_helpmergedeleteconflictrows`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
