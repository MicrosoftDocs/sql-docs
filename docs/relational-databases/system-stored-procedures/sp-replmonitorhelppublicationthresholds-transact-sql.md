---
title: "sp_replmonitorhelppublicationthresholds (T-SQL)"
description: sp_replmonitorhelppublicationthresholds returns the threshold metrics set for a monitored publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replmonitorhelppublicationthresholds"
  - "sp_replmonitorhelppublicationthresholds_TSQL"
helpviewer_keywords:
  - "sp_replmonitorhelppublicationthresholds"
dev_langs:
  - "TSQL"
---
# sp_replmonitorhelppublicationthresholds (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the threshold metrics set for a monitored publication. This stored procedure, which is used to monitor replication, is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replmonitorhelppublicationthresholds
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    [ , [ @publication_type = ] publication_type ]
    [ , [ @thresholdmetricname = ] N'thresholdmetricname' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the published database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @publication_type = ] *publication_type*

If the type of publication. *@publication_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` | Transactional publication. |
| `1` | Snapshot publication. |
| `2` | Merge publication. |
| `NULL` (default) | Replication tries to determine the publication type. |

#### [ @thresholdmetricname = ] N'*thresholdmetricname*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `metric_id` | **int** | ID of the replication performance metric, which can be one of the following.<br /><br />`1expiration` - monitors for imminent expiration of subscriptions to transactional publications.<br /><br />`2latency` - monitors for the performance of subscriptions to transactional publications.<br /><br />`4mergeexpiration` - monitors for imminent expiration of subscriptions to merge publications.<br /><br />`5mergeslowrunduration` - monitors the duration of merge synchronizations over low-bandwidth (dial-up) connections.<br /><br />`6mergefastrunduration` - monitors the duration of merge synchronizations over high-bandwidth (LAN) connections.<br /><br />`7mergefastrunspeed` - monitors the synchronization rate of merge synchronizations over high-bandwidth (LAN) connections.<br /><br />`8mergeslowrunspeed` - monitors the synchronization rate of merge synchronizations over low-bandwidth (dial-up) connections. |
| `title` | **sysname** | Name of the replication performance metric. |
| `value` | **int** | The threshold value of the performance metric. |
| `shouldalert` | **bit** | Is if an alert should be generated when the metric exceeds the defined threshold for this publication; a value of `1` indicates that an alert should be raised. |
| `isenabled` | **bit** | Is if monitoring is enabled for this replication performance metric for this publication; a value of `1` indicates that monitoring is enabled. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_replmonitorhelppublicationthresholds` is used with all types of replication.

## Permissions

Only members of the **db_owner** or **replmonitor** fixed database role on the distribution database can execute `sp_replmonitorhelppublicationthresholds`.

## Related content

- [Programmatically Monitor Replication](../replication/monitor/programmatically-monitor-replication.md)
