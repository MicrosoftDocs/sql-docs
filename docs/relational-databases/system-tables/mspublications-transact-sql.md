---
title: "MSpublications (Transact-SQL)"
description: MSpublications (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 12/16/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSpublications"
  - "MSpublications_TSQL"
helpviewer_keywords:
  - "MSpublications system table"
dev_langs:
  - "TSQL"
---
# MSpublications (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `MSpublications` table contains one row for each publication that is replicated by a Publisher. This table is stored in the distribution database.

| Column name | Data type | Description |
| --- | --- | --- |
| `publisher_id` | **smallint** | The ID of the Publisher. |
| `publisher_db` | **sysname** | The name of the Publisher database. |
| `publication` | **sysname** | The name of the publication. |
| `publication_id` | **int** | The ID of the publication. |
| `publication_type` | **int** | The type of publication:<br /><br />`0` = Transactional<br />`1` = Snapshot<br />`2` = Merge |
| `thirdparty_flag` | **bit** | Indicates whether a publication is a [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]<br />`1` = Data source other than [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] |
| `independent_agent` | **bit** | Indicates whether there's a stand-alone Distribution Agent for this publication. |
| `immediate_sync` | **bit** | Indicates whether synchronization files are created or re-created each time the Snapshot Agent runs. |
| `allow_push` | **bit** | Indicates whether push subscriptions can be created for the given publication. |
| `allow_pull` | **bit** | Indicates whether pull subscriptions can be created for the given publication. |
| `allow_anonymous` | **bit** | Indicates whether anonymous subscriptions can be created for the given publication. |
| `description` | **nvarchar(255)** | The description of the publication. |
| `vendor_name` | **nvarchar(100)** | The name of the vendor if Publisher isn't a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `retention` | **int** | The retention period of the publication, in hours. |
| `sync_method` | **int** | The synchronization method:<br /><br />`0` = Native (produces native-mode bulk copy output of all tables)<br /><br />`1` = Character (produces a character-mode bulk copy output of all tables)<br /><br />`3` = Concurrent (produces native-mode bulk copy output of all tables but doesn't lock the table during the snapshot)<br /><br />`4` = Concurrent_c (produces a character-mode bulk copy output of all tables but doesn't lock the table during the snapshot)<br /><br />The values `3` and `4` are available for transactional replication and merge replication, but not for snapshot replication. |
| `allow_subscription_copy` | **bit** | Enables or disables the ability to copy the subscription databases that subscribe to this publication.<br /><br />`0` - copying is disabled<br />`1` - copying is enabled |
| `thirdparty_options` | **int** | Specifies whether the display of a publication in the Replication folder in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is suppressed:<br /><br />`0` = Display a heterogeneous publication in the Replication folder in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].<br /><br />`1` = Suppress the display a heterogeneous publication in the Replication folder in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. |
| `allow_queued_tran` | **bit** | Specifies whether publication allows queued updating:<br /><br />`0` = Publication is non-queued<br />`1` = Publication is queued |
| `options` | **int** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `retention_period_unit` | **tinyint** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `allow_initialize_from_backup` | **bit** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `min_autonosync_lsn` | **varbinary(16)** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |

## Related content

- [Replication Tables (Transact-SQL)](replication-tables-transact-sql.md)
- [Replication Views (Transact-SQL)](../system-views/replication-views-transact-sql.md)
