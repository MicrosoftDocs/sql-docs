---
title: Configure Last Writer Conflict Detection & Resolution
description: Describes how to configure last writer conflict detection and resolution for peer-to-peer replication.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/07/2025
ms.service: sql
ms.topic: how-to
ms.custom:
  - template-how-to
  - updatefrequency5
---

# Configure last writer conflict detection & resolution

Beginning with [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU13, you can configure peer-to-peer replication to automatically resolve conflicts by allowing the most recent insert or update to win the conflict. If either write deletes the row, SQL Server allows the delete to win the conflict. This method is known as last write wins.

Use stored procedures to configure last write wins. Don't use Peer-to-Peer Topology Wizard to add or remove nodes when you use last writer.

## Important configuration considerations

> [!NOTE]  
> After you update to [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU13 or a later version, when you configure a publication with conflict resolution set to last write wins, extra metadata is included in the publication. If you later uninstall/downgrade to a release earlier than [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU13, this extra metadata causes problems. You should drop any such publications before downgrading, and then recreate them on the lower version.

When you configure peer-to-peer replication with automatic conflict discovery and resolution to resolve as last write wins, include the following configurations and settings:

- Create the publication with the following parameters,

  - Set `@p2p_conflictdetection_policy = 'lastwriter'` to specify last write wins. See [sp_addpublication (Transact-SQL)](../../../system-stored-procedures/sp-addpublication-transact-sql.md). This parameter is introduced in [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU13. The default value `originatorid` resolves conflict based on the originator ID and is the same as conflict resolution before [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU13.

  - Set `@p2p_continue_onconflict = 'true'` to allow the distribution agent to resolve the conflict.

- When you add the article (`sp_addarticle`), confirm the command type behavior for update command (`@upd_cmd`). Options include:

  - `CALL` (Default)
  - `SCALL`

  See details in [sp_addarticle](../../../system-stored-procedures/sp-addarticle-transact-sql.md).

- When you add an article (`sp_addarticle`) in a publication with a last writer conflict detection policy, use `CALL` or `SCALL` as command type for `@upd_cmd` parameter, `CALL` is default.

  > [!NOTE]  
  > SQL Server supports `SCALL` for `@upd_cmd`. With `SCALL`, when a transaction updates a value to the same value, it's not considered as a change and `SCALL` format doesn't supply the value for columns that aren't updated or modified. For more information about SCALL call format, see [Call syntax for stored procedures](../transactional-articles-specify-how-changes-are-propagated.md#call-syntax-for-stored-procedures).

- You can use peer-to-peer publication with last writer conflict detection and resolution in an availability group. See:

  - [Configure one peer as part of availability group](single-availability-group.md)
  - [Configure both peers in availability groups](multi-availability-group.md)

- You can see the conflict and its resolution.

  - In SQL Server Management Studio, right-click the publication and select **View Conflicts**.

  Or

  - Query `conflict_schemaname_tablename` on publication database. For example, `conflict_dbo_tab1`. See [conflict_&lt;schema&gt;_&lt;table&gt; (Transact-SQL)](../../../system-tables/conflict-schema-table-transact-sql.md).

- Insert and update conflicts are resolved based on last writer, but delete always prevail. For example, if you have delete-update conflict and update was done at a later time, the delete still wins.

- Last writer conflict detection and resolution is determined based on a hidden column `$sys_mw_cd_id`. The data type of this column is **datetime2**.

## Conflict detection comparison

The following table compares how conflicts are detected and resolved with traditional peer-to-peer replication, and when last writer conflict resolution is enabled:

| Conflict type | Conflict details | Peer-to-peer | Last writer |
| --- | --- | --- | --- |
| Insert-Insert | All rows in each table participating in peer-to-peer replication are uniquely identified by using primary key values. An insert-insert conflict occurs when a row with the same key value was inserted at more than one node. | If the incoming row is the winner, then we update the destination row. In either case, we record the information. | If the incoming row is the winner, then we update the destination row. In either case, we record the information. |
| Update-Update | Occurs when the same row was updated at more than one node. | If the incoming row is the winner, then we modify ONLY the changed columns. | If the incoming row is the winner, then we modify all the columns at the destination (if `@upd_cmd` is set to `default` – `CALL`). |
| Update-Insert | Occurs if a row was updated at one node, but the same row was deleted and then reinserted on another node. | If the incoming row is the winner, then we modify ONLY the changed columns. | This occurs when a row is updated on `peer1` and the same row is deleted and reinserted on `peer2`. When the sync occurs, the row on `peer1` is deleted as delete always wins and then same row is inserted, whereas the row is updated on `peer2` as updated happened at a later time. This leads to nonconvergence. |
| Insert- Update | Occurs if a row was deleted and then reinserted at one node and the same row was updated on another node. | If the incoming row is the winner, then we update all the columns. | This occurs when a row is deleted and reinserted on `peer1` and the same row is updated on `peer2`. When the sync occurs, the row is deleted on `peer2` as delete always wins and then it's inserted again. On `peer1`, the update is skipped. |
| Delete-Insert<br /><br />Insert-Delete | Occurs if a row was deleted at one node, but the same row was deleted and then reinserted at another node. | We currently think this as D-U conflict and if the incoming row is then winner then we delete the row from destination. | This occurs when a row is deleted on `peer1` and the same row is deleted + reinserted on `peer2`. When the sync occurs, the row on `peer2` is deleted, whereas the row is inserted on `peer1`. This occurs because we don't store information about the deleted row, so we don't know whether the row was deleted or wasn't present on the peer. This leads to nonconvergence. |
| Delete-Update | Occurs if a row was deleted at one node, but the same row was updated at another node. | We currently think this as D-U conflict and if the incoming row is the winner then we delete the row from the destination. | This is a D-U conflict. As delete always wins, incoming delete is the winner and we delete the row from destination. |
| Update-Delete | Occurs if a row was updated at one node, but the same row was deleted at another node. | In the peer-to-peer Update stored procedure, if there's an U-D conflict then we print the following message and don't resolve it.<br /><br />`An update-delete conflict was detected and unresolved. The row could not be updated since the row does not exist.` | This is a U-D conflict. As delete always wins, incoming update is skipped. |
| Delete-Delete | Occurs when a row was deleted at more than one node. | In the peer-to-peer Delete stored procedure, if there's D-D conflict then we don't process any change, just record it. | If there's D-D conflict, then we don't process any change, just record it. |

> [!NOTE]  
> In the current implementation of the last writer conflict detection policy, the delete always wins when there's an insert-delete, delete-insert, or update-delete conflict.

## Examples

### Create publication on first peer (Node1)

In this example, the script:

- Publishes a database called `MWPubDB`.
- Names the publication `PublMW`.
- Configures conflict detection and resolution policy as last write wins:
  `, @p2p_continue_onconflict= 'true'`
  `, @p2p_conflictdetection_policy = 'lastwriter'`

```sql
USE [MWPubDB];

EXECUTE sp_replicationdboption
    @dbname = N'MWPubDB',
    @optname = N'publish',
    @value = N'true';
GO

-- Adding the transactional publication
USE [MWPubDB];

EXECUTE sp_addpublication
    @publication = N'PublMW',
    @description = N'Peer-to-Peer publication of database ''MWPubDB'' from Publisher ''Node1''.',
    @sync_method = N'native',
    @retention = 0,
    @allow_push = N'true',
    @allow_pull = N'true',
    @allow_anonymous = N'false',
    @enabled_for_internet = N'false',
    @snapshot_in_defaultfolder = N'true',
    @compress_snapshot = N'false',
    @ftp_port = 21,
    @allow_subscription_copy = N'false',
    @add_to_active_directory = N'false',
    @repl_freq = N'continuous',
    @status = N'active',
    @independent_agent = N'true',
    @immediate_sync = N'true',
    @allow_sync_tran = N'false',
    @allow_queued_tran = N'false',
    @allow_dts = N'false',
    @replicate_ddl = 1,
    @allow_initialize_from_backup = N'true',
    @enabled_for_p2p = N'true',
    @enabled_for_het_sub = N'false',
    @p2p_conflictdetection = N'true',
    @p2p_originator_id = 100,
    @p2p_continue_onconflict = 'true',
    @p2p_conflictdetection_policy = 'lastwriter';
GO

USE [MWPubDB];

EXECUTE sp_addarticle
    @publication = N'PublMW',
    @article = N'tab1',
    @source_owner = N'dbo',
    @source_object = N'tab1',
    @type = N'logbased',
    @description = NULL,
    @creation_script = NULL,
    @pre_creation_cmd = N'drop',
    @schema_option = 0x0000000008035DDB,
    @identityrangemanagementoption = N'manual',
    @destination_table = N'tab1',
    @destination_owner = N'dbo',
    @status = 16,
    @vertical_partition = N'false',
    @ins_cmd = N'CALL sp_MSins_dbotab1',
    @del_cmd = N'CALL sp_MSdel_dbotab1',
    @upd_cmd = N'CALL sp_MSupd_dbotab1';
GO
```

### Create publication on second peer (Node2)

The following script creates the publication on the second peer (Node 2).

```sql
USE [MWPubDB];

EXECUTE sp_replicationdboption
    @dbname = N'MWPubDB',
    @optname = N'publish',
    @value = N'true';
GO

-- Adding the transactional publication
USE [MWPubDB];

EXECUTE sp_addpublication
    @publication = N'PublMW',
    @description = N'Peer-to-Peer publication of database ''MWPubDB'' from Publisher ''Node2''.',
    @sync_method = N'native',
    @retention = 0,
    @allow_push = N'true',
    @allow_pull = N'true',
    @allow_anonymous = N'false',
    @enabled_for_internet = N'false',
    @snapshot_in_defaultfolder = N'true',
    @compress_snapshot = N'false',
    @ftp_port = 21,
    @allow_subscription_copy = N'false',
    @add_to_active_directory = N'false',
    @repl_freq = N'continuous',
    @status = N'active',
    @independent_agent = N'true',
    @immediate_sync = N'true',
    @allow_sync_tran = N'false',
    @allow_queued_tran = N'false',
    @allow_dts = N'false',
    @replicate_ddl = 1,
    @allow_initialize_from_backup = N'true',
    @enabled_for_p2p = N'true',
    @enabled_for_het_sub = N'false',
    @p2p_conflictdetection = N'true',
    @p2p_originator_id = 1,
    @p2p_continue_onconflict = 'true',
    @p2p_conflictdetection_policy = 'lastwriter';
GO

USE [MWPubDB];

EXECUTE sp_addarticle
    @publication = N'PublMW',
    @article = N'tab1',
    @source_owner = N'dbo',
    @source_object = N'tab1',
    @type = N'logbased',
    @description = NULL,
    @creation_script = NULL,
    @pre_creation_cmd = N'drop',
    @schema_option = 0x0000000008035DDB,
    @identityrangemanagementoption = N'manual',
    @destination_table = N'tab1',
    @destination_owner = N'dbo',
    @status = 16,
    @vertical_partition = N'false',
    @ins_cmd = N'CALL sp_MSins_dbotab1',
    @del_cmd = N'CALL sp_MSdel_dbotab1',
    @upd_cmd = N'CALL sp_MSupd_dbotab1';
GO
```

### Create subscription from Node1 to Node2

```sql
USE [MWPubDB];

EXECUTE sp_addsubscription
    @publication = N'PublMW',
    @subscriber = N'Node2',
    @destination_db = N'MWPubDB',
    @subscription_type = N'Push',
    @sync_type = N'replication support only',
    @article = N'all',
    @update_mode = N'read only',
    @subscriber_type = 0;
GO

EXECUTE sp_addpushsubscription_agent
    @publication = N'PublMW',
    @subscriber = N'Node2',
    @subscriber_db = N'MWPubDB',
    @job_login = NULL,
    @job_password = NULL,
    @subscriber_security_mode = 1,
    @frequency_type = 64,
    @frequency_interval = 1,
    @frequency_relative_interval = 1,
    @frequency_recurrence_factor = 0,
    @frequency_subday = 4,
    @frequency_subday_interval = 5,
    @active_start_time_of_day = 0,
    @active_end_time_of_day = 235959,
    @active_start_date = 0,
    @active_end_date = 0,
    @dts_package_location = N'Distributor';
GO
```

### Create subscription from Node2 to Node1

```sql
USE [MWPubDB];

EXECUTE sp_addsubscription
    @publication = N'PublMW',
    @subscriber = N'Node1',
    @destination_db = N'MWPubDB',
    @subscription_type = N'Push',
    @sync_type = N'replication support only',
    @article = N'all',
    @update_mode = N'read only',
    @subscriber_type = 0;
GO

EXECUTE sp_addpushsubscription_agent
    @publication = N'PublMW',
    @subscriber = N'Node1',
    @subscriber_db = N'MWPubDB',
    @job_login = NULL,
    @job_password = NULL,
    @subscriber_security_mode = 1,
    @frequency_type = 64,
    @frequency_interval = 1,
    @frequency_relative_interval = 1,
    @frequency_recurrence_factor = 0,
    @frequency_subday = 4,
    @frequency_subday_interval = 5,
    @active_start_time_of_day = 0,
    @active_end_time_of_day = 235959,
    @active_start_date = 0,
    @active_end_date = 0,
    @dts_package_location = N'Distributor';
GO
```

## Related content

- [Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication](../peer-to-peer-conflict-detection-in-peer-to-peer-replication.md)
- [Transactional Articles - Specify How Changes Are Propagated](../transactional-articles-specify-how-changes-are-propagated.md)
- [sys.sp_addpublication (Transact-SQL)](../../../system-stored-procedures/sp-addpublication-transact-sql.md)
- [Configure one peer as part of availability group](single-availability-group.md)
- [Configure both peers in availability groups](multi-availability-group.md)
