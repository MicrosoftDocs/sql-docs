---
title: "sp_replmonitorchangepublicationthreshold (T-SQL)"
description: sp_replmonitorchangepublicationthreshold changes the monitoring threshold metric for a publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replmonitorchangepublicationthreshold_TSQL"
  - "sp_replmonitorchangepublicationthreshold"
helpviewer_keywords:
  - "sp_replmonitorchangepublicationthreshold"
dev_langs:
  - "TSQL"
---
# sp_replmonitorchangepublicationthreshold (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the monitoring threshold metric for a publication. This stored procedure, which is used to monitor replication, is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replmonitorchangepublicationthreshold
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    [ , [ @publication_type = ] publication_type ]
    [ , [ @metric_id = ] metric_id ]
    [ , [ @thresholdmetricname = ] N'thresholdmetricname' ]
    [ , [ @value = ] value ]
    [ , [ @shouldalert = ] shouldalert ]
    [ , [ @mode = ] mode ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the published database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication for which the monitoring threshold attributes are being changed. *@publication* is **sysname**, with no default.

#### [ @publication_type = ] *publication_type*

If the type of publication. *@publication_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` | Transactional publication. |
| `1` | Snapshot publication. |
| `2` | Merge publication. |
| `NULL` (default) | Replication attempts to determine the publication type. |

#### [ @metric_id = ] *metric_id*

The ID of the publication threshold metric being changed. *@metric_id* is **int**, and can be one of these values.

| Value | Metric Name |
| --- | --- |
| `1` | `expiration` - monitors for imminent expiration of subscriptions to transactional publications. |
| `2` | `latency` - monitors for the performance of subscriptions to transactional publications. |
| `4` | `mergeexpiration` - monitors for imminent expiration of subscriptions to merge publications. |
| `5` | `mergeslowrunduration` - monitors the duration of merge synchronizations over low-bandwidth (dial-up) connections. |
| `6` | `mergefastrunduration` - monitors the duration of merge synchronizations over high-bandwidth local area network (LAN) connections. |
| `7` | `mergefastrunspeed` - monitors the synchronization rate of merge synchronizations over high-bandwidth (LAN) connections. |
| `8` | `mergeslowrunspeed` - monitors the synchronization rate of merge synchronizations over low-bandwidth (dial-up) connections. |

You must specify either *@metric_id* or *@thresholdmetricname*. If *@thresholdmetricname* is specified, then *@metric_id* should be `NULL`.

#### [ @thresholdmetricname = ] N'*thresholdmetricname*'

The name of the publication threshold metric being changed. *@thresholdmetricname* is **sysname**, with a default of `NULL`. You must specify either *@thresholdmetricname* or *@metric_id*. If *@metric_id* is specified, then *@thresholdmetricname* should be `NULL`.

#### [ @value = ] *value*

The new value of the publication threshold metric. *@value* is **int**, with a default of `NULL`. If `NULL`, then the metric value isn't updated.

#### [ @shouldalert = ] *shouldalert*

Specifies whether an alert is generated when a publication threshold metric is reached. *@shouldalert* is **bit**, with a default of `NULL`.

- A value of `1` means that an alert is generated.
- A value of `0` means that an alert isn't generated.

#### [ @mode = ] *mode*

Specifies whether the publication threshold metric is enabled. *@mode* is **tinyint**, with a default of `1`.

- A value of `1` means that monitoring of this metric is enabled.
- A value of `2` means that monitoring of this metric is disabled.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_replmonitorchangepublicationthreshold` is used with all types of replication.

## Permissions

Only members of the **db_owner** or **replmonitor** fixed database role in the distribution database can execute `sp_replmonitorchangepublicationthreshold`.

## Related content

- [Programmatically Monitor Replication](../replication/monitor/programmatically-monitor-replication.md)
