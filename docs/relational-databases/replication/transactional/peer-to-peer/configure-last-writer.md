---
title: Configure last writer conflict detection & resolution
description: Describes how to configure last writer conflict detection and resolution for peer-to-peer replication.
author: MikeRayMSFT
ms.author: mikeray
ms.service: sql
ms.topic: how-to
ms.date: 11/1/2021
ms.custom: template-how-to 
---

# Configure last writer conflict detection & resolution

Beginning with [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU 13, you can configure peer-to-peer replication to automatically resolve conflicts by allowing the most recent insert or update to win the conflict. If either write deletes the row, SQL Server allows the delete to win the conflict. This method is known as last write wins.

Use stored procedures to configure last write wins.

## Important configuration considerations

>[!NOTE]
>After updating to [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU13 or above, when you configure a publication with conflict resolution set to last write wins, additional metadata is included in the publication.  If you later uninstall/downgrade to a release below [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU13, this additional metadata will cause problems.  It is recommended to drop any such publications before downgrading, and then recreate them on the lower version.

When you configure peer-to-peer replication with automatic conflict discovery and resolution to resolve as last write wins, include the following configurations and settings:

- Create the publication with the following parameters, 
  - Set `@p2p_conflictdetection_policy =  'lastwriter'` to specify last write wins. See [sp_addpublication (Transact-SQL)](../../../system-stored-procedures/sp-addpublication-transact-sql.md). This parameter is introduced in [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU 13. The default value `originatorid` resolves conflict based on the originator ID and is the same as conflict resolution prior to [!INCLUDE [sssql19-md](../../../../includes/sssql19-md.md)] CU 13.
  - Set `@p2p_continue_onconflict = 'true'` to allow the distribution agent to resolve the conflict.

- When you add the article (`sp_addarticle`), confirm the command type behavior for update command (`@upd_cmd`). Options include:
  - `CALL` (Default)
  - `SCALL`

   See details in [sp_addarticle (Transact-SQL)](../../../system-stored-procedures/sp-addarticle-transact-sql.md).
  
- When you add an article (`sp_addarticle`) in a publication with a last writer conflict detection policy, USE `CALL` or `SCALL` as command type for `@upd_cmd` parameter, `CALL` is default.

   > [!NOTE]
   > SQL Server supports `SCALL` for `@upd_cmd`. With `SCALL`, when a transaction updates a value to the same value, it is not considered as a change and `SCALL` format doesn't supply the value for columns that are not updated or modified. Please review the following for details about SCALL call format - [Call syntax for stored procedures](../transactional-articles-specify-how-changes-are-propagated.md#call-syntax-for-stored-procedures).

- You can use peer-to-peer publication with last writer conflict detection and resolution in an availability group. See:
  - [Configure peer-to-peer with one peer as a publication database in an availability group](single-availability-group.md)
  - [Configure peer-to-peer with both peers as publication databases in an availability group](multi-availability-group.md)

- You can see the conflict and its resolution. 
  - In SQL Server Management Studio, right-click the publication and select **View Conflicts**. 
  
  Or

  - Query `conflict_schemaname_tablename` on publication database. For example - `conflict_dbo_tab1`. See [conflict_&lt;schema&gt;_&lt;table&gt; (Transact-SQL)](../../../system-tables/conflict-schema-table-transact-sql.md).

- Insert and update conflicts are resolved on the basis of last writer, but delete always prevail. For example, if you have delete-update conflict and update was done at a later time, the delete still wins.
- Last writer conflict detection and resolution is determined based on a hidden column `$sys_mw_cd_id`. The datatype of this column is `datetime2`.

## Conflict detection comparison

The following table compares how conflicts are detected and resolved with traditional peer-to-peer replication, and  when last writer conflict resolution is enabled: 


|Conflict type  |Conflict details  |Peer-to-peer  |Last writer  |
|---------|---------|---------|---------|
|Insert-Insert   |All rows in each table participating in peer-to-peer replication are uniquely identified by using primary key values. An insert-insert conflict occurs when a row with the same key value was inserted at more than one node. |If the incoming row is the winner, then we update the destination row. In either case, we record the information. |If the incoming row is the winner, then we update the destination row. In either case, we record the information. |
|Update-Update |Occurs when the same row was updated at more than one node. |If the incoming row is the winner, then we modify ONLY the changed columns.  |If the incoming row is the winner, then we modify all the columns at the destination (if `@upd_cmd` is set to `default` – CALL).|
|Update-Insert  |Occurs if a row was updated at one node, but the same row was deleted and then reinserted on another node. |If the incoming row is the winner, then we modify ONLY the changed columns. |This occurs when a row is updated on peer1 and the same row is delete and re-inserted on peer2. When the sync occurs, the row on peer1 is deleted as delete always wins and then same row is inserted, whereas the row is updated on peer2 as updated happened at a later time. This will lead to nonconvergence.|
|Insert- Update |Occurs if a row was deleted and then reinserted at one node and the same row was updated on another node.  |If the incoming row is the winner then we update all the columns. |This occurs when a row is delete and re-inserted on peer1 and the same row is updated on peer2. When the sync occurs, the row is deleted on peer2 as delete always wins and then it is inserted again. On peer1, the update is skipped. |
|Delete-Insert <br><br> Insert-Delete|Occurs if a row was deleted at one node, but the same row was deleted and then reinserted at another node. |We currently think this as D-U conflict and if the incoming row is then winner then we delete the row from destination. |This occurs when a row is deleted on peer1 and the same row is delete + re-inserted on peer2. When the sync occurs, the row on peer2 is deleted, whereas the row is inserted on peer1. This occurs because we don't store information about the deleted row, so we don't know whether the row was deleted or was not present on the peer. This will lead to nonconvergence. |
|Delete-Update |Occurs if a row was deleted at one node, but the same row was updated at another node. |We currently think this as D-U conflict and if the incoming row is the winner then we delete the row from the destination.  |This is a D-U conflict. As delete always wins, incoming delete will be the winner and we delete the row from destination.  |
| Update-Delete| Occurs if a row was updated at one node, but the same row was deleted at another node.  | In the peer-to-peer Update stored procedure, if there is an U-D conflict then we print the following message and don't resolve it. </br> </br> `An update-delete conflict was detected and unresolved. The row could not be updated since the row does not exist. `| This is a U-D conflict. As delete always wins, incoming update is skipped.   | 
| Delete-Delete |  Occurs when a row was deleted at more than one node.  | In the peer-to-peer Delete stored procedure, if there is D-D conflict then we don't process any change, just record it. | If there is D-D conflict then we don't process any change, just record it. |

> [!NOTE]
> In the current implementation of the last writer conflict detection policy, the delete always wins when there is an insert-delete, delete-insert or update-delete conflict. 

## Examples

### Create publication on first peer (Node1)

In this example, the script:

- Publishes a database called `MWPubDB`.
- Names the publication `PublMW`.
- Configures conflict detection and resolution policy as last write wins:   
    `, @p2p_continue_onconflict= 'true'`   
    `, @p2p_conflictdetection_policy = 'lastwriter'`


```sql
USE [MWPubDB]
EXEC sp_replicationdboption @dbname = N'MWPubDB'
  , @optname = N'publish'
  , @value = N'true'
GO
-- Adding the transactional publication
USE [MWPubDB]
EXEC sp_addpublication @publication = N'PublMW'
  , @description = N'Peer-to-Peer publication of database ''MWPubDB'' from Publisher ''Node1''.'
  , @sync_method = N'native'
  , @retention = 0
  , @allow_push = N'true'
  , @allow_pull = N'true'
  , @allow_anonymous = N'false'
  , @enabled_for_internet = N'false'
  , @snapshot_in_defaultfolder = N'true'
  , @compress_snapshot = N'false'
  , @ftp_port = 21
  , @allow_subscription_copy = N'false'
  , @add_to_active_directory = N'false'
  , @repl_freq = N'continuous'
  , @status = N'active'
  , @independent_agent = N'true'
  , @immediate_sync = N'true'
  , @allow_sync_tran = N'false'
  , @allow_queued_tran = N'false'
  , @allow_dts = N'false'
  , @replicate_ddl = 1, @allow_initialize_from_backup = N'true'
  , @enabled_for_p2p = N'true'
  , @enabled_for_het_sub = N'false'
  , @p2p_conflictdetection = N'true'
  , @p2p_originator_id = 100
  , @p2p_continue_onconflict= 'true'
  , @p2p_conflictdetection_policy = 'lastwriter'
GO

USE [MWPubDB]
EXEC sp_addarticle @publication = N'PublMW'
  , @article = N'tab1'
  , @source_owner = N'dbo'
  , @source_object = N'tab1'
  , @type = N'logbased'
  , @description = null
  , @creation_script = null
  , @pre_creation_cmd = N'drop'
  , @schema_option = 0x0000000008035DDB
  , @identityrangemanagementoption = N'manual'
  , @destination_table = N'tab1'
  , @destination_owner = N'dbo'
  , @status = 16
  , @vertical_partition = N'false'
  , @ins_cmd = N'CALL sp_MSins_dbotab1'
  , @del_cmd = N'CALL sp_MSdel_dbotab1'
  , @upd_cmd = N'CALL sp_MSupd_dbotab1'
GO
```

### Create publication on second peer (Node2)

The script below creates the publication on the second peer (Node 2).

```sql
USE [MWPubDB]
EXEC sp_replicationdboption @dbname = N'MWPubDB'
 , @optname = N'publish'
 , @value = N'true'
GO
-- Adding the transactional publication
USE [MWPubDB]
EXEC sp_addpublication @publication = N'PublMW'
 , @description = N'Peer-to-Peer publication of database ''MWPubDB'' from Publisher ''Node2''.'
 ,@sync_method = N'native'
 , @retention = 0, @allow_push = N'true'
 , @allow_pull = N'true'
 , @allow_anonymous = N'false'
 , @enabled_for_internet = N'false'
 , @snapshot_in_defaultfolder = N'true'
 , @compress_snapshot = N'false'
 , @ftp_port = 21, @allow_subscription_copy = N'false'
 , @add_to_active_directory = N'false'
 , @repl_freq = N'continuous'
 , @status = N'active'
 , @independent_agent = N'true'
 , @immediate_sync = N'true'
 , @allow_sync_tran = N'false'
 , @allow_queued_tran = N'false'
 , @allow_dts = N'false'
 , @replicate_ddl = 1, @allow_initialize_from_backup = N'true'
 , @enabled_for_p2p = N'true'
 , @enabled_for_het_sub = N'false'
 , @p2p_conflictdetection = N'true'
 , @p2p_originator_id = 1
 , @p2p_continue_onconflict= 'true'
,  @p2p_conflictdetection_policy = 'lastwriter'
GO

USE [MWPubDB]
EXEC sp_addarticle @publication = N'PublMW'
 , @article = N'tab1'
 , @source_owner = N'dbo'
 , @source_object = N'tab1'
 , @type = N'logbased'
 , @description = null
 , @creation_script = null
 , @pre_creation_cmd = N'drop'
 , @schema_option = 0x0000000008035DDB
 , @identityrangemanagementoption = N'manual'
 , @destination_table = N'tab1'
 , @destination_owner = N'dbo'
 , @status = 16, @vertical_partition = N'false'
 , @ins_cmd = N'CALL sp_MSins_dbotab1'
 , @del_cmd = N'CALL sp_MSdel_dbotab1'
 , @upd_cmd = N'CALL sp_MSupd_dbotab1'
GO
```

### Create subscription from Node1 to Node2

```sql
USE [MWPubDB]
EXEC sp_addsubscription @publication = N'PublMW'
 , @subscriber = N'Node2'
 , @destination_db = N'MWPubDB'
 , @subscription_type = N'Push'
 , @sync_type = N'replication support only'
 , @article = N'all'
 , @update_mode = N'read only'
 , @subscriber_type = 0
GO
EXEC sp_addpushsubscription_agent @publication = N'PublMW'
 , @subscriber = N'Node2'
 , @subscriber_db = N'MWPubDB'
 , @job_login = null
 , @job_password = null
 , @subscriber_security_mode = 1
 , @frequency_type = 64
 , @frequency_interval = 1
 , @frequency_relative_interval = 1
 , @frequency_recurrence_factor = 0
 , @frequency_subday = 4
 , @frequency_subday_interval = 5
 , @active_start_time_of_day = 0
 , @active_end_time_of_day = 235959
 , @active_start_date = 0 
 , @active_end_date = 0
 , @dts_package_location = N'Distributor'
GO
```

### Create subscription from Node2 to Node1

```sql
USE [MWPubDB]
EXEC sp_addsubscription @publication = N'PublMW'
 , @subscriber = N'Node1'
 , @destination_db = N'MWPubDB'
 , @subscription_type = N'Push',
@sync_type = N'replication support only'
 , @article = N'all'
 , @update_mode = N'read only'
 , @subscriber_type = 0
go
EXEC sp_addpushsubscription_agent @publication = N'PublMW'
 , @subscriber = N'Node1'
 , @subscriber_db = N'MWPubDB'
 , @job_login = null
 , @job_password = null
 , @subscriber_security_mode = 1
 , @frequency_type = 64
 , @frequency_interval = 1
 , @frequency_relative_interval = 1
 , @frequency_recurrence_factor = 0
 , @frequency_subday = 4
 , @frequency_subday_interval = 5
 , @active_start_time_of_day = 0
 , @active_end_time_of_day = 235959
 , @active_start_date = 0
 , @active_end_date = 0
 , @dts_package_location = N'Distributor'
GO
```

## See also

- [Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication](../peer-to-peer-conflict-detection-in-peer-to-peer-replication.md)
- [Transactional Articles - Specify How Changes Are Propagated](../transactional-articles-specify-how-changes-are-propagated.md)
- [sp_addpublication (Transact-SQL)](../../../system-stored-procedures/sp-addpublication-transact-sql.md)
- [Configure peer-to-peer publication database to be part of availability group](single-availability-group.md)
- [Configure peer-to-peer publication database to be part of availability groups](multi-availability-group.md)
