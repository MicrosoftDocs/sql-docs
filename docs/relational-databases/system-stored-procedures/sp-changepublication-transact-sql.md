---
title: "sp_changepublication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/29/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changepublication"
  - "sp_changepublication_TSQL"
helpviewer_keywords: 
  - "sp_changepublication"
ms.assetid: c36e5865-25d5-42b7-b045-dc5036225081
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changepublication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of a publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_changepublication [ [ @publication = ] 'publication' ]  
    [ , [ @property = ] 'property' ]  
    [ , [ @value = ] 'value' ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@publication =** ] **'**_publication_**'**  
 Is the name of the publication. *publication* is **sysname**, with a default of NULL.  
  
 [ **@property =** ] **'**_property_**'**  
 Is the publication property to change. *property* is **nvarchar(255)**.  
  
 [ **@value =** ] **'**_value_**'**  
 Is the new property value. *value* is **nvarchar(255)**, with a default of NULL.  
  
 This table describes the properties of the publication that can be changed and restrictions on the values for those properties.  
  
|Property|Value|Description|  
|--------------|-----------|-----------------|  
|**allow_anonymous**|**true**|Anonymous subscriptions can be created for the given publication, and *immediate_sync* must also be **true**. Cannot be changed for peer-to-peer publications.|  
||**false**|Anonymous subscriptions cannot be created for the given publication. Cannot be changed for peer-to-peer publications.|  
|**allow_initialize_from_backup**|**true**|Subscribers can initialize a subscription to this publication from a backup rather than an initial snapshot. This property cannot be changed for non-[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
||**false**|Subscribers must use the initial snapshot. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**allow_partition_switch**|**true**|ALTER TABLE...SWITCH statements can be executed against the published database. For more information, see [Replicate Partitioned Tables and Indexes](../../relational-databases/replication/publish/replicate-partitioned-tables-and-indexes.md).|  
||**false**|ALTER TABLE...SWITCH statements cannot be executed against the published database.|  
|**allow_pull**|**true**|Pull subscriptions are allowed for the given publication. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
||**false**|Pull subscriptions are not allowed for the given publication. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**allow_push**|**true**|Push subscriptions are allowed for the given publication.|  
||**false**|Push subscriptions are not allowed for the given publication.|  
|**allow_subscription_copy**|**true**|Enables the ability to copy databases that subscribe to this publication. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
||**false**|Disables the ability to copy databases that subscribe to this publication. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**alt_snapshot_folder**||Location of the alternate folder for the snapshot.|  
|**centralized_conflicts**|**true**|Conflict records are stored at the Publisher. Can be changed only if there are no active subscriptions. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
||**false**|Conflict records are stored at both the Publisher and at the Subscriber that caused the conflict. Can be changed only if there are no active subscriptions. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**compress_snapshot**|**true**|Snapshot in an alternate snapshot folder is compressed into the .cab file format. The snapshot in the default snapshot folder cannot be compressed.|  
||**false**|Snapshot is not compressed, which is the default behavior for replication.|  
|**conflict_policy**|**pub wins**|Conflict resolution policy for updating Subscribers where the Publisher wins the conflict. This property can be changed only if there are no active subscriptions. Not supported for Oracle Publishers.|  
||**sub reinit**|For updating Subscribers, if a conflict occurs the subscription must be reinitialized. This property can be changed only if there are no active subscriptions. Not supported for Oracle Publishers.|  
||**sub wins**|Conflict resolution policy for updating Subscribers where the Subscriber wins the conflict. This property can be changed only if there are no active subscriptions. Not supported for Oracle Publishers.|  
|**conflict_retention**||**int** that specifies the conflict retention period, in days. The default retention is 14 days. **0** means that no conflict cleanup is needed. Not supported for Oracle Publishers.|  
|**description**||Optional entry describing the publication.|  
|**enabled_for_het_sub**|**true**|Enables the publication to support non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. **enabled_for_het_sub** cannot be changed when there are subscriptions to the publication. You may need to execute [Replication Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md) to comply with the following requirements before setting **enabled_for_het_sub** to true:<br /> - **allow_queued_tran** must be **false**.<br /> - **allow_sync_tran** must be **false**.<br /> Changing **enabled_for_het_sub** to **true** may change existing publication settings. For more information, see [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md). This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
||**false**|Publication does not support non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**enabled_for_internet**|**true**|Publication is enabled for the Internet, and File Transfer Protocol (FTP) can be used to transfer the snapshot files to a subscriber. The synchronization files for the publication are put into the following directory: C:\Program Files\Microsoft SQL Server\MSSQL\Repldata\ftp. *ftp_address* cannot be NULL. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
||**false**|Publication is not enabled for the Internet. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**enabled_for_p2p**|**true**|The publication supports peer-to-peer replication. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.<br /> To set **enabled_for_p2p** to **true**, the following restrictions apply:<br /> - **allow_anonymous** must be **false**<br /> - **allow_dts** must be **false**.<br /> - **allow_initialize_from_backup** must be **true**<br /> - **allow_queued_tran** must be **false**.<br /> - **allow_sync_tran** must be **false**.<br /> - **enabled_for_het_sub** must be **false**.<br /> - **independent_agent** must be **true**.<br /> - **repl_freq** must be **continuous**.<br /> - **replicate_ddl** must be **1**.|  
||**false**|The publication does not support peer-to-peer replication. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**ftp_address**||FTP accessible location of the publication snapshot files. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**ftp_login**||User name used to connect to the FTP service, and the value ANONYMOUS is allowed. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**ftp_password**||Password for the user name used to connect to the FTP service. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**ftp_port**||Port number of the FTP service for the Distributor. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**ftp_subdirectory**||Specifies where the snapshot files are created if the publication supports propagating snapshots using FTP. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
|**immediate_sync**|**true**|Synchronization files for the publication are created or re-created each time the Snapshot Agent runs. Subscribers are able to receive the synchronization files immediately after the subscription if the Snapshot Agent has been completed once before the subscription. New subscriptions get the newest synchronization files generated by the most recent execution of the Snapshot Agent. *independent_agent* must also be **true**. See remarks below for additional information about **immediate_sync**.|  
||**false**|Synchronization files are created only if there are new subscriptions. Subscribers cannot receive the synchronization files after the subscription until the Snapshot Agent is started and completes.|  
|**independent_agent**|**true**|Publication has its own dedicated Distribution Agent.|  
||**false**|Publication uses a shared Distribution Agent, and each publication/subscription database pair has a shared agent.|  
|**p2p_continue_onconflict**|**true**|The Distribution Agent continues to process changes when a conflict is detected.<br /> **Caution:** We recommend that you use the default value of `FALSE`. When this option is set to `TRUE`, the Distribution Agent tries to converge data in the topology by applying the conflicting row from the node that has the highest originator ID. This method does not guarantee convergence. You should make sure that the topology is consistent after a conflict is detected. For more information, see "Handling Conflicts" in [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).|  
||**false**|The Distribution Agent stops processing changes when a conflict is detected.|  
|**post_snapshot_script**||Specifies the location of a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file that the Distribution Agent runs after all the other replicated object scripts and data have been applied during an initial synchronization.|  
|**pre_snapshot_script**||Specifies the location of a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file that the Distribution Agent runs before all the other replicated object scripts and data have been applied during an initial synchronization.|  
|**publish_to_ActiveDirectory**|**true**|This parameter has been deprecated and is only supported for the backward compatibility of scripts. You can no longer add publication information to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory.|  
||**false**|Removes the publication information from Active Directory.|  
|**queue_type**|**sql**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store transactions. This property can be changed only if there are no active subscriptions.<br /><br /> Note: Support for using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing has been discontinued. Specifying a value of **msmq** for *value* results in an error.|  
|**repl_freq**|**continuous**|Publishes output of all log-based transactions.|  
||**snapshot**|Publishes only scheduled synchronization events.|  
|**replicate_ddl**|**1**|Data definition language (DDL) statements executed at the publisher are replicated. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.|  
||**0**|DDL statements are not replicated. This property cannot be changed for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications. Replication of schema changes cannot be disabled when using peer-to-peer replication.|  
|**replicate_partition_switch**|**true**|ALTER TABLE...SWITCH statements that are executed against the published database should be replicated to Subscribers. This option is valid only if *allow_partition_switch* is set to TRUE. For more information, see [Replicate Partitioned Tables and Indexes](../../relational-databases/replication/publish/replicate-partitioned-tables-and-indexes.md).|  
||**false**|ALTER TABLE...SWITCH statements should not be replicated to Subscribers.|  
|**retention**||**int** representing the retention period, in hours, for subscription activity. If a subscription is not active within the retention period, it is removed.|  
|**snapshot_in_defaultfolder**|**true**|Snapshot files are stored in the default snapshot folder. If *alt_snapshot_folder*is also specified, snapshot files are stored in both the default and alternate locations.|  
||**false**|Snapshot files are stored in the alternate location specified by *alt_snapshot_folder*.|  
|**status**|**active**|Publication data is available for Subscribers immediately when the publication is created. Not supported for Oracle Publishers.|  
||**inactive**|Publication data are not available for Subscribers when the publication is created. Not supported for Oracle Publishers.|  
|**sync_method**|**native**|Uses native-mode bulk copy output of all tables when synchronizing subscriptions.|  
||**character**|Uses character-mode bulk copy output of all tables when synchronizing subscriptions.|  
||**concurrent**|Uses native-mode bulk-copy program output of all tables, but does not lock tables during the snapshot generation process. Not valid for snapshot replication.|  
||**concurrent_c**|Uses character-mode bulk copy program output of all tables, but does not lock tables during the snapshot generation process. Not valid for snapshot replication.|  
|**taskid**||This property has been deprecated and is no longer supported.|  
|**allow_drop**|**true**|Enables `DROP TABLE` DLL support for articles which are part of transactional replication. Minimum supported version: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 2 or above and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Service Pack 1 or above. Additional reference: [KB 3170123](https://support.microsoft.com/help/3170123/supports-drop-table-ddl-for-articles-that-are-included-in-transactional-replication-in-sql-server-2014-or-in-sql-server-2016-sp1)|
||**false**|Disables `DROP TABLE` DLL support for articles that are part of transactional replication. This is the **default** value for this property.|
|**NULL** (default)||Returns the list of supported values for *property*.|  
  
[ **@force_invalidate_snapshot =** ] *force_invalidate_snapshot*  
 Acknowledges that the action taken by this stored procedure may invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default of **0**.  
  - **0** specifies that changes to the article do not cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.  
  - **1** specifies that changes to the article may cause the snapshot to be invalid. If there are existing subscriptions that would require a new snapshot, this value gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.   
See the Remarks section for the properties that, when changed, require the generation of a new snapshot.  
  
[**@force_reinit_subscription =** ] *force_reinit_subscription*  
 Acknowledges that the action taken by this stored procedure may require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit** with a default of **0**.  
  - **0** specifies that changes to the article do not cause the subscription to be reinitialized. If the stored procedure detects that the change would require existing subscriptions to be reinitialized, an error occurs and no changes are made.  
  - **1** specifies that changes to the article cause the existing subscription to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
[ **@publisher** = ] **'**_publisher_**'**  
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
  > [!NOTE]  
  >  *publisher* should not be used when changing article properties on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changepublication** is used in snapshot replication and transactional replication.  
  
 After changing any of the following properties, you must generate a new snapshot, and you must specify a value of **1** for the *force_invalidate_snapshot* parameter.  
-   **alt_snapshot_folder**  
-   **compress_snapshot**  
-   **enabled_for_het_sub**  
-   **ftp_address**  
-   **ftp_login**  
-   **ftp_password**  
-   **ftp_port**  
-   **ftp_subdirectory**  
-   **post_snapshot_script**  
-   **pre_snapshot_script**  
-   **snapshot_in_defaultfolder**  
-   **sync_mode**  
  
To list publication objects in the Active Directory using the **publish_to_active_directory** parameter, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object must already be created in the Active Directory.  
  
## Impact of immediate sync  
 When immediate sync is on, all changes in the log are tracked immediately after the initial snapshot is generated even if there are no subscriptions. Logged changes are used when a customer is using backup to add a new peer node. After the backup is restored, the peer is synched with any other changes occurring after the backup was taken. Since the commands are tracked in the distribution database, the synchronization logic can look at the last backed up LSN and use this as a starting point, knowing that the command is available if the backup was taken within the max retention period. (The default value for the min retention period is 0 hrs and max retention period is 24 hrs.)  
  
 When immediate sync is off, changes are kept at least the min retention period and cleaned up immediately for all the transactions that are already replicated. If immediate sync is off and configured with the default retention period, it is likely that the required changes after the backup was taken were cleaned up and the new peer node will not be initialized properly. The only option left is to quiesce the topology. Setting immediate sync to on provides greater flexibility and is the recommended setting for P2P replication.  
  
## Example  
 [!code-sql[HowTo#sp_changepublication](../../relational-databases/replication/codesnippet/tsql/sp-changepublication-tra_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changepublication**.  
  
## See Also  
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)   
 [sp_droppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppublication-transact-sql.md)   
 [sp_helppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
