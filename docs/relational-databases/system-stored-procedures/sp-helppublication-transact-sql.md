---
title: "sp_helppublication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helppublication_TSQL"
  - "sp_helppublication"
helpviewer_keywords: 
  - "sp_helppublication"
ms.assetid: e801c3f0-dcbd-4b4a-b254-949a05f63518
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helppublication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about a publication. For a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publication, this stored procedure is executed at the Publisher on the publication database. For an Oracle publication, this stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helppublication [ [ @publication = ] 'publication' ]  
    [ , [ @found=] found OUTPUT]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to be viewed. *publication* is sysname, with a default of **%**, which returns information about all publications.  
  
`[ @found = ] 'found' OUTPUT`
 Is a flag to indicate returning rows. *found*is **int** and an OUTPUT parameter, with a default of **23456**. **1** indicates the publication is found. **0** indicates the publication is not found.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is sysname, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be specified when requesting publication information from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|pubid|**int**|ID for the publication.|  
|name|**sysname**|Name of the publication.|  
|restricted|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|status|**tinyint**|The current status of the publication.<br /><br /> **0** = Inactive.<br /><br /> **1** = Active.|  
|task||Used for backward compatibility.|  
|replication frequency|**tinyint**|Type of replication frequency:<br /><br /> **0** = Transactional<br /><br /> **1** = Snapshot|  
|synchronization method|**tinyint**|Synchronization mode:<br /><br /> **0** = Native bulk copy program (**bcp** utility)<br /><br /> **1** = Character bulk copy<br /><br /> **3** = Concurrent, which means that native bulk copy (**bcp**utility) is used but tables are not locked during the snapshot<br /><br /> **4** = Concurrent_c, which means that character bulk copy is used but tables are not locked during the snapshot|  
|description|**nvarchar(255)**|Optional description for the publication.|  
|immediate_sync|**bit**|Whether the synchronization files are created or re-created each time the Snapshot Agent runs.|  
|enabled_for_internet|**bit**|Whether the synchronization files for the publication are exposed to the Internet, through file transfer protocol (FTP) and other services.|  
|allow_push|**bit**|Whether push subscriptions are allowed on the publication.|  
|allow_pull|**bit**|Whether pull subscriptions are allowed on the publication.|  
|allow_anonymous|**bit**|Whether anonymous subscriptions are allowed on the publication.|  
|independent_agent|**bit**|Whether there is a stand-alone Distribution Agent for this publication.|  
|immediate_sync_ready|**bit**|Whether or not the Snapshot Agent generated a snapshot that is ready to be used by new subscriptions. This parameter is defined only if the publication is set to always have a snapshot available for new or reinitialized subscriptions.|  
|allow_sync_tran|**bit**|Whether immediate-updating subscriptions are allowed on the publication.|  
|autogen_sync_procs|**bit**|Whether to automatically generate stored procedures to support immediate-updating subscriptions.|  
|snapshot_jobid|**binary(16)**|Scheduled task ID.|  
|retention|**int**|Amount of change, in hours, to save for the given publication.|  
|has subscription|**bit**|If the publication has an active subscriptions. **1** means that the publication has active subscriptions, and **0** means that the publication has no subscriptions.|  
|allow_queued_tran|**bit**|Specifies whether disables queuing of changes at the Subscriber until they can be applied at the Publisher has been enabled. If **0**, changes at the Subscriber are not queued.|  
|snapshot_in_defaultfolder|**bit**|Specifies whether snapshot files are stored in the default folder. If **0**, snapshot files have been stored in the alternate location specified by *alternate_snapshot_folder*. If **1**, snapshot files can be found in the default folder.|  
|alt_snapshot_folder|**nvarchar(255)**|Specifies the location of the alternate folder for the snapshot.|  
|pre_snapshot_script|**nvarchar(255)**|Specifies a pointer to an **.sql** file location. The Distribution Agent will run the pre-snapshot script before running any of the replicated object scripts when applying a snapshot at a Subscriber.|  
|post_snapshot_script|**nvarchar(255)**|Specifies a pointer to an **.sql** file location. The Distribution Agent will run the post-snapshot script after all the other replicated object scripts and data have been applied during an initial synchronization.|  
|compress_snapshot|**bit**|Specifies that the snapshot that is written to the *alt_snapshot_folder* location is to be compressed into the [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB format. **0** specifies that the snapshot will not be compressed.|  
|ftp_address|**sysname**|The network address of the FTP service for the Distributor. Specifies where publication snapshot files are located for the Distribution Agent or Merge Agent of a subscriber to pick up.|  
|ftp_port|**int**|The port number of the FTP service for the Distributor.|  
|ftp_subdirectory|**nvarchar(255)**|Specifies where the snapshot files will be available for the Distribution Agent or Merge Agent of subscriber to pick up if the publication supports propagating snapshots using FTP.|  
|ftp_login|**sysname**|The username used to connect to the FTP service.|  
|allow_dts|**bit**|Specifies that the publication allows data transformations. **0** specifies that DTS transformations are not allowed.|  
|allow_subscription_copy|**bit**|Specifies whether the ability to copy the subscription databases that subscribe to this publication has been enabled. **0** means that copying is not allowed.|  
|centralized_conflicts|**bit**|Specifies whether conflict records are stored on the Publisher:<br /><br /> **0** = Conflict records are stored at both the publisher and at the subscriber that caused the conflict.<br /><br /> **1** = Conflict records are stored at the Publisher.|  
|conflict_retention|**int**|Specifies the conflict retention period, in days.|  
|conflict_policy|**int**|Specifies the conflict resolution policy followed when the queued updating subscriber option is used. Can be one of these values:<br /><br /> **1** = Publisher wins the conflict.<br /><br /> **2** = Subscriber wins the conflict.<br /><br /> **3** = Subscription is reinitialized.|  
|queue_type||Specifies which type of queue is used. Can be one of these values:<br /><br /> **msmq** = Use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing to store transactions.<br /><br /> **sql** = Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store transactions.<br /><br /> Note: Support for Message Queuing has been discontinued.|  
|backward_comp_level||Database compatibility level, and can be one of the following:<br /><br /> **90** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]<br /><br /> **100** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|  
|publish_to_AD|**bit**|Specifies whether the publication is published in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory???. A value of **1** indicates that it is published, and a value of **0** indicates that it is not published.|  
|allow_initialize_from_backup|**bit**|Indicates if Subscribers can initialize a subscription to this publication from a backup rather than an initial snapshot. **1** means that subscriptions can be initialized from a backup, and **0** means that they cannot. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md) a transactional Subscriber without a snapshot.|  
|replicate_ddl|**int**|Indicates if schema replication is supported for the publication. **1** indicates that data definition language (DDL) statements executed at the publisher are replicated, and **0** indicates that DDL statements are not replicated. For more information, see [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).|  
|enabled_for_p2p|**int**|If the publication can be used in a peer-to-peer replication topology. **1** indicates that the publication supports peer-to-peer replication. For more information, see [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).|  
|publish_local_changes_only|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|enabled_for_het_sub|**int**|Specifies whether the publication supports non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. A value of **1** means that non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers are supported. A value of **0** means that only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers are supported. For more information, see [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md).|  
|enabled_for_p2p_conflictdetection|**int**|Specifies whether the Distribution Agent detects conflicts for a publication that is enabled for peer-to-peer replication. A value of **1** means that conflicts are detected. For more information, see [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).|  
|originator_id|**int**|Specifies an ID for a node in a peer-to-peer topology. This ID is used for conflict detection if **enabled_for_p2p_conflictdetection** is set to **1**. For a list of IDs that have already been used, query the [Mspeer_originatorid_history](../../relational-databases/system-tables/mspeer-originatorid-history-transact-sql.md) system table.|  
|p2p_continue_onconflict|**int**|Specifies whether The Distribution Agent continues to process changes when a conflict is detected. A value of **1** means that the agent continues to process changes.<br /><br /> **\*\* Caution \*\*** We recommend that you use the default value of **0**. When this option is set to **1**, the Distribution Agent tries to converge data in the topology by applying the conflicting row from the node that has the highest originator ID. This method does not guarantee convergence. You should make sure that the topology is consistent after a conflict is detected. For more information, see "Handling Conflicts" in [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).|  
|allow_partition_switch|**int**|Specifies whether ALTER TABLE...SWITCH statements can be executed against the published database. For more information, see [Replicate Partitioned Tables and Indexes](../../relational-databases/replication/publish/replicate-partitioned-tables-and-indexes.md).|  
|replicate_partition_switch|**int**|Specifies whether ALTER TABLE...SWITCH statements that are executed against the published database should be replicated to Subscribers. This option is valid only if *allow_partition_switch* is set to **1**.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_helppublication is used in snapshot and transactional replication.  
  
 sp_helppublication will return information on all publications that are owned by the user executing this procedure.  
  
## Example  
 [!code-sql[HowTo#sp_helppublication](../../relational-databases/replication/codesnippet/tsql/sp-helppublication-trans_1.sql)]  
  
## Permissions  
 Only members of the sysadmin fixed server role at the Publisher or members of the db_owner fixed database role on the publication database or users in the publication access list (PAL) can execute sp_helppublication.  
  
 For a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher, only members of the sysadmin fixed server role at the Distributor or members of the db_owner fixed database role on the distribution database or users in the PAL can execute sp_helppublication.  
  
## See Also  
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)   
 [sp_changepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md)   
 [sp_droppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppublication-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
