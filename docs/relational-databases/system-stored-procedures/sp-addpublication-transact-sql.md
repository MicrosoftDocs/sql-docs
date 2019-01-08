---
title: "sp_addpublication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addpublication_TSQL"
  - "sp_addpublication"
helpviewer_keywords: 
  - "sp_addpublication"
ms.assetid: c7167ed1-2b7e-4824-b82b-65f4667c4407
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addpublication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a snapshot or transactional publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addpublication [ @publication = ] 'publication'  
    [ , [ @taskid = ] tasked ]  
    [ , [ @restricted = ] 'restricted' ]  
    [ , [ @sync_method = ] 'sync_method' ]  
    [ , [ @repl_freq = ] 'repl_freq' ]  
    [ , [ @description = ] 'description' ]  
    [ , [ @status = ] 'status' ]  
    [ , [ @independent_agent = ] 'independent_agent' ]  
    [ , [ @immediate_sync = ] 'immediate_sync' ]  
    [ , [ @enabled_for_internet = ] 'enabled_for_internet' ]  
    [ , [ @allow_push = ] 'allow_push'  
    [ , [ @allow_pull = ] 'allow_pull' ]  
    [ , [ @allow_anonymous = ] 'allow_anonymous' ]  
    [ , [ @allow_sync_tran = ] 'allow_sync_tran' ]  
    [ , [ @autogen_sync_procs = ] 'autogen_sync_procs' ]  
    [ , [ @retention = ] retention ]  
    [ , [ @allow_queued_tran= ] 'allow_queued_updating' ]  
    [ , [ @snapshot_in_defaultfolder= ] 'snapshot_in_default_folder' ]  
    [ , [ @alt_snapshot_folder= ] 'alternate_snapshot_folder' ]  
    [ , [ @pre_snapshot_script= ] 'pre_snapshot_script' ]  
    [ , [ @post_snapshot_script= ] 'post_snapshot_script' ]  
    [ , [ @compress_snapshot= ] 'compress_snapshot' ]  
    [ , [ @ftp_address = ] 'ftp_address' ]  
    [ , [ @ftp_port= ] ftp_port ]  
    [ , [ @ftp_subdirectory = ] 'ftp_subdirectory' ]  
    [ , [ @ftp_login = ] 'ftp_login' ]  
    [ , [ @ftp_password = ] 'ftp_password' ]  
    [ , [ @allow_dts = ] 'allow_dts' ]  
    [ , [ @allow_subscription_copy = ] 'allow_subscription_copy' ]  
    [ , [ @conflict_policy = ] 'conflict_policy' ]  
    [ , [ @centralized_conflicts = ] 'centralized_conflicts' ]   
    [ , [ @conflict_retention = ] conflict_retention ]  
    [ , [ @queue_type = ] 'queue_type' ]  
    [ , [ @add_to_active_directory = ] 'add_to_active_directory' ]  
    [ , [ @logreader_job_name = ] 'logreader_agent_name' ]  
    [ , [ @qreader_job_name = ] 'queue_reader_agent_name' ]  
    [ , [ @publisher = ] 'publisher' ]   
    [ , [ @allow_initialize_from_backup = ] 'allow_initialize_from_backup' ]  
    [ , [ @replicate_ddl = ] replicate_ddl ]  
    [ , [ @enabled_for_p2p = ] 'enabled_for_p2p' ]  
    [ , [ @publish_local_changes_only = ] 'publish_local_changes_only' ]  
    [ , [ @enabled_for_het_sub = ] 'enabled_for_het_sub' ]  
    [ , [ @p2p_conflictdetection = ] 'p2p_conflictdetection' ]  
    [ , [ @p2p_originator_id = ] p2p_originator_id  
    [ , [ @p2p_continue_onconflict = ] 'p2p_continue_onconflict'  
    [ , [ @allow_partition_switch = ] 'allow_partition_switch'  
    [ , [ @replicate_partition_switch = ]'replicate_partition_switch'  
```  
  
## Arguments  
 [ **\@publication=**] **'**_publication_**'**  
 Is the name of the publication to create. *publication* is **sysname**, with no default. The name must be unique within the database.  
  
 [ **\@taskid=**] *taskid*  
 Supported for backward compatibility only; use [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md).  
  
 [ **\@restricted=**] **'**_restricted_**'**  
 Supported for backward compatibility only; use *default_access*.  
  
 [ **\@sync_method=**] _'sync_method_**'**  
 Is the synchronization mode. *sync_method* is **nvarchar(13)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**native**|Produces native-mode bulk copy program output of all tables. *Not supported for Oracle Publishers*.|  
|**character**|Produces character-mode bulk copy program output of all tables. _For an Oracle Publisher,_ **character** _is valid only for snapshot replication_.|  
|**concurrent**|Produces native-mode bulk copy program output of all tables but does not lock tables during the snapshot. Only supported for transactional publications. *Not supported for Oracle Publishers*.|  
|**concurrent_c**|Produces character-mode bulk copy program output of all tables but does not lock tables during the snapshot. Only supported for transactional publications.|  
|**database snapshot**|Produces native-mode bulk copy program output of all tables from a database snapshot. Database snapshots are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).|  
|**database snapshot character**|Produces character-mode bulk copy program output of all tables from a database snapshot. Database snapshots are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).|  
|NULL (default)|Defaults to **native** for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. For non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, defaults to **character** when the value of *repl_freq* is **Snapshot** and to **concurrent_c** for all other cases.|  
  
 [ **\@repl_freq=**] **'**_repl_freq_**'**  
 Is the type of replication frequency, *repl_freq* is **nvarchar(10)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**continuous** (default)|Publisher provides output of all log-based transactions. For non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, this requires that *sync_method* be set to **concurrent_c**.|  
|**snapshot**|Publisher produces only scheduled synchronization events. For non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, this requires that *sync_method* be set to **character**.|  
  
 [ **\@description=**] **'**_description_**'**  
 Is an optional description for the publication. *description* is **nvarchar(255)**, with a default of NULL.  
  
 [ **\@status=**] **'**_status_**'**  
 Specifies if publication data is available. *status* is **nvarchar(8)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**active**|Publication data is available for Subscribers immediately.|  
|**inactive** (default)|Publication data is not available for Subscribers when the publication is first created (they can subscribe, but the subscriptions are not processed).|  
  
 *Not supported for Oracle Publishers*.  
  
 [ **\@independent_agent=**] **'**_independent_agent_**'**  
 Specifies if there is a stand-alone Distribution Agent for this publication. *independent_agent* is **nvarchar(5)**, with a default of FALSE. If **true**, there is a stand-alone Distribution Agent for this publication. If **false**, the publication uses a shared Distribution Agent, and each Publisher database/Subscriber database pair has a single, shared Agent.  
  
 [ **\@immediate_sync=**] **'**_immediate_synchronization_**'**  
 Specifies if the synchronization files for the publication are created each time the Snapshot Agent runs. *immediate_synchronization* is **nvarchar(5)**, with a default of FALSE. If **true**, the synchronization files are created or re-created each time the Snapshot Agent runs. Subscribers are able to get the synchronization files immediately if the Snapshot Agent has completed before the subscription is created. New subscriptions get the newest synchronization files generated by the most recent execution of the Snapshot Agent. *independent_agent* must be **true** for *immediate_synchronization* to be **true**. If **false**, the synchronization files are created only if there are new subscriptions. You must call [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) for each subscription when you incrementally add a new article to an existing publication. Subscribers cannot receive the synchronization files after the subscription until the Snapshot Agents are started and completed.  
  
 [ **\@enabled_for_internet=**] **'**_enabled_for_internet_**'**  
 Specifies if the publication is enabled for the Internet, and determines if file transfer protocol (FTP) can be used to transfer the snapshot files to a subscriber. *enabled_for_internet* is **nvarchar(5)**, with a default of FALSE. If **true**, the synchronization files for the publication are put into the C:\Program Files\Microsoft SQL Server\MSSQL\MSSQL.x\Repldata\Ftp directory. The user must create the Ftp directory.  
  
 [ **\@allow_push=**] **'**_allow_push_**'**  
 Specifies if push subscriptions can be created for the given publication. *allow_push* is **nvarchar(5)**, with a default of TRUE, which allows push subscriptions on the publication.  
  
 [ **\@allow_pull=**] **'**_allow_pull_**'**  
 Specifies if pull subscriptions can be created for the given publication. *allow_pull* is **nvarchar(5)**, with a default of FALSE. If **false**, pull subscriptions are not allowed on the publication.  
  
 [ **\@allow_anonymous=**] **'**_allow_anonymous_**'**  
 Specifies if anonymous subscriptions can be created for the given publication. *allow_anonymous* is **nvarchar(5)**, with a default of FALSE. If **true**, *immediate_synchronization* must also be set to **true**. If **false**, anonymous subscriptions are not allowed on the publication.  
  
 [ **\@allow_sync_tran=**] **'**_allow_sync_tran_**'**  
 Specifies if immediate-updating subscriptions are allowed on the publication. *allow_sync_tran* is **nvarchar(5)**, with a default of FALSE. **true** is *Not supported for Oracle Publishers*.  
  
 [ **\@autogen_sync_procs=**] **'**_autogen_sync_procs_**'**  
 Specifies if the synchronizing stored procedure for updating subscriptions is generated at the Publisher. *autogen_sync_procs* is **nvarchar(5)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**true**|Set automatically when updating subscriptions is enabled.|  
|**false**|Set automatically when updating subscriptions is not enabled or for Oracle Publishers.|  
|NULL (default)|Defaults to **true** when updating subscriptions is enabled and to **false** when updating subscriptions is not enabled.|  
  
> [!NOTE]  
>  The user supplied value for *autogen_sync_procs*will be overridden depending on the values specified for *allow_queued_tran* and *allow_sync_tran*.  
  
 [ **\@retention=**] *retention*  
 Is the retention period in hours for subscription activity. *retention* is **int**, with a default of 336 hours. If a subscription is not active within the retention period, it expires and is removed. The value can be greater than the maximum retention period of the distribution database used by the Publisher. If **0**, well-known subscriptions to the publication will never expire and be removed by the Expired Subscription Cleanup Agent.  
  
 [ **\@allow_queued_tran=** ] **'**_allow_queued_updating_**'**  
 Enables or disables queuing of changes at the Subscriber until they can be applied at the Publisher. *allow_queued_updating* is **nvarchar(5)** with a default of FALSE. If **false**, changes at the Subscriber are not queued. **true** is *Not supported for Oracle Publishers*.  
  
 [ **\@snapshot_in_defaultfolder=** ] **'**_snapshot_in_default_folder_**'**  
 Specifies if snapshot files are stored in the default folder. *snapshot_in_default_folder* is **nvarchar(5)** with a default of TRUE. If **true**, snapshot files can be found in the default folder. If **false**, snapshot files have been stored in the alternate location specified by *alternate_snapshot_folder*. Alternate locations can be on another server, on a network drive, or on removable media (such as CD-ROM or removable disks). You can also save the snapshot files to an FTP site, for retrieval by the Subscriber at a later time. Note that this parameter can be true and still have a location in the **\@alt_snapshot_folder** parameter. This combination specifies that the snapshot files will be stored in both the default and alternate locations.  
  
 [ **\@alt_snapshot_folder=** ] **'**_alternate_snapshot_folder_**'**  
 Specifies the location of the alternate folder for the snapshot. *alternate_snapshot_folder* is **nvarchar(255)** with a default of NULL.  
  
 [ **\@pre_snapshot_script=** ] **'**_pre_snapshot_script_**'**  
 Specifies a pointer to an **.sql** file location. *pre_snapshot_script* is **nvarchar(255),**with a default of NULL. The Distribution Agent will run the pre-snapshot script before running any of the replicated object scripts when applying a snapshot at a Subscriber. The script is executed in the security context used by the Distribution Agent when connecting to the subscription database.  
  
 [ **\@post_snapshot_script=** ] **'**_post_snapshot_script_**'**  
 Specifies a pointer to an **.sql** file location. *post_snapshot_script* is **nvarchar(255)**, with a default of NULL. The Distribution Agent will run the post-snapshot script after all the other replicated object scripts and data have been applied during an initial synchronization. The script is executed in the security context used by the Distribution Agent when connecting to the subscription database.  
  
 [ **\@compress_snapshot=** ] **'**_compress_snapshot_**'**  
 Specifies that the snapshot that is written to the **\@alt_snapshot_folder** location is to be compressed into the [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB format. *compress_snapshot* is **nvarchar(5)**, with a default of FALSE. **false** specifies that the snapshot will not be compressed; **true** specifies that the snapshot will be compressed. Snapshot files that are larger than 2 gigabytes (GB) cannot be compressed. Compressed snapshot files are uncompressed at the location where the Distribution Agent runs; pull subscriptions are typically used with compressed snapshots so that files are uncompressed at the Subscriber. The snapshot in the default folder cannot be compressed.  
  
 [ **\@ftp_address =** ] **'**_ftp_address_**'**  
 Is the network address of the FTP service for the Distributor. *ftp_address* is **sysname**, with a default of NULL. Specifies where publication snapshot files are located for the Distribution Agent or Merge Agent of a subscriber to pick up. Since this property is stored for each publication, each publication can have a different *ftp_address*. The publication must support propagating snapshots using FTP.  
  
 [ **\@ftp_port=** ] *ftp_port*  
 Is the port number of the FTP service for the Distributor. *ftp_port* is **int**, with a default of 21. Specifies where the publication snapshot files are located for the Distribution Agent or Merge Agent of a subscriber to pick up. Since this property is stored for each publication, each publication can have its own *ftp_port*.  
  
 [ **\@ftp_subdirectory =** ] **'**_ftp_subdirectory_**'**  
 Specifies where the snapshot files will be available for the Distribution Agent or Merge Agent of subscriber to pick up if the publication supports propagating snapshots using FTP. *ftp_subdirectory* is **nvarchar(255)**, with a default of NULL. Since this property is stored for each publication, each publication can have its own *ftp_subdirctory* or choose to have no subdirectory, indicated with a NULL value.  
  
 [ **\@ftp_login =** ] **'**_ftp_login_**'**  
 Is the username used to connect to the FTP service. *ftp_login* is **sysname**, with a default of ANONYMOUS.  
  
 [ **\@ftp_password =** ] **'**_ftp_password_**'**  
 Is the user password used to connect to the FTP service. *ftp_password* is **sysname**, with a default of NULL.  
  
 [ **\@allow_dts =** ] **'**_allow_dts_**'**  
 Specifies that the publication allows data transformations. You can specify a DTS package when creating a subscription. *allow_transformable_subscriptions* is **nvarchar(5)** with a default of FALSE, which does not allow DTS transformations. When *allow_dts* is true, *sync_method* must be set to either **character** or **concurrent_c**.  
  
 **true** is *not supported for Oracle Publishers*.  
  
 [ **\@allow_subscription_copy =** ] **'**_allow_subscription_copy_**'**  
 Enables or disables the ability to copy the subscription databases that subscribe to this publication. *allow_subscription_copy* is**nvarchar(5)**, with a default of FALSE.  
  
 [ **\@conflict_policy =** ] **'**_conflict_policy_**'**  
 Specifies the conflict resolution policy followed when the queued updating subscriber option is used. *conflict_policy* is **nvarchar(100)** with a default of NULL, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**pub wins**|Publisher wins the conflict.|  
|**sub reinit**|Reinitialize the subscription.|  
|**sub wins**|Subscriber wins the conflict.|  
|NULL (default)|If NULL, and the publication is a snapshot publication, the default policy becomes **sub reinit**. If NULL and the publication is not a snapshot publication, the default becomes **pub wins**.|  
  
 *Not supported for Oracle Publishers*.  
  
 [ **\@centralized_conflicts =** ] **'**_centralized_conflicts_**'**  
 Specifies if conflict records are stored on the Publisher. *centralized_conflicts* is **nvarchar(5)**, with a default of TRUE. If **true**, conflict records are stored at the Publisher. If **false**, conflict records are stored at both the publisher and at the subscriber that caused the conflict. *Not supported for Oracle Publishers*.  
  
 [ **\@conflict_retention =** ] *conflict_retention*  
 Specifies the conflict retention period, in days. This is the period of time that conflict metadata is stored for peer-to-peer transactional replication and queued updating subscriptions. *conflict_retention* is **int**, with a default of 14. *Not supported for Oracle Publishers*.  
  
 [ **\@queue_type =** ] **'**_queue_type_**'**  
 Specifies which type of queue is used. *queue_type* is **nvarchar(10)**, with a default of NULL, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**sql**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store transactions.|  
|NULL (default)|Defaults to **sql**, which specifies to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store transactions.|  
  
> [!NOTE]  
>  Support for using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing has been discontinued. Specifying a value of **msmq** will result in a warning, and replication will automatically set the value to **sql**.  
  
 *Not supported for Oracle Publishers*.  
  
 [ **\@add_to_active_directory =** ] **'**_add_*\_*_to_active_directory_**'**  
 This parameter has been deprecated and is only supported for the backward compatibility of scripts. You can no longer add publication information to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory.  
  
 [ **\@logreader_job_name =** ] **'**_logreader_agent_name_**'**  
 Is the name of an existing agent job. *logreader_agent_name* is **sysname**, with a default value of NULL. This parameter is only specified when the Log Reader Agent will use an existing job instead of a new one being created.  
  
 [ **\@qreader_job_name =** ] **'**_queue_reader_agent_name_**'**  
 Is the name of an existing agent job. *queue_reader_agent_name* is **sysname**, with a default value of NULL. This parameter is only specified when the Queue Reader Agent will use an existing job instead of a new one being created.  
  
 [ **\@publisher =** ] **'**_publisher_**'**  
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when adding a publication to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
 [ **\@allow_initialize_from_backup =** ] **'**_allow_initialize_from_backup_**'**  
 Indicates if Subscribers can initialize a subscription to this publication from a backup rather than an initial snapshot. *allow_initialize_from_backup* is **nvarchar(5)**, and can be one of these values:  
  
|Value|Description|  
|-----------|-----------------|  
|**true**|Enables initialization from a backup.|  
|**false**|Disables initialization from a backup.|  
|NULL (default)|Defaults to **true** for a publication in a peer-to-peer replication topology and **false** for all other publications.|  
  
 For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
> [!WARNING]  
>  To avoid missing subscriber data, when using **sp_addpublication** with `@allow_initialize_from_backup = N'true'`, always use `@immediate_sync = N'true'`.  
  
 [ **\@replicate_ddl =** ] *replicate_ddl*  
 Indicates if schema replication is supported for the publication. *replicate_ddl* is **int**, with a default of **1** for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers and **0** for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. **1** indicates that data definition language (DDL) statements executed at the publisher are replicated, and **0** indicates that DDL statements are not replicated. *Schema replication is not supported for Oracle Publishers.* For more information, see [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).  
  
 The *\@replicate_ddl* parameter is honored when a DDL statement adds a column. The *\@replicate_ddl* parameter is ignored when a DDL statement alters or drops a column for the following reasons.  
  
-   When a column is dropped, sysarticlecolumns must be updated to prevent new DML statements from including the dropped column which would cause the distribution agent to fail. The *\@replicate_ddl* parameter is ignored because replication must always replicate the schema change.  
  
-   When a column is altered, the source data type or nullability might have changed, causing DML statements to contain a value that may not be compatible with the table at the subscriber. Such DML statements might cause distribution agent to fail. The *\@replicate_ddl* parameter is ignored because replication must always replicate the schema change.  
  
-   When a DDL statement adds a new column, sysarticlecolumns does not include the new column. DML statements will not try to replicate data for the new column. The parameter is honored because either replicating or not replicating the DDL is acceptable.  
  
 [ **\@enabled_for_p2p =** ] **'**_enabled_for_p2p_**'**  
 Enables the publication to be used in a peer-to-peer replication topology. *enabled_for_p2p* is **nvarchar(5)**, with a default of FALSE. **true** indicates that the publication supports peer-to-peer replication. When setting *enabled_for_p2p* to **true**, the following restrictions apply:  
  
-   *allow_anonymous* must be **false**.  
  
-   *allow_dts* must be **false**.  
  
-   *allow_initialize_from_backup* must be **true**.  
  
-   *allow_queued_tran* must be **false**.  
  
-   *allow_sync_tran* must be **false**.  
  
-   *conflict_policy* must be **false**.  
  
-   *independent_agent* must be **true**.  
  
-   *repl_freq* must be **continuous**.  
  
-   *replicate_ddl* must be **1**.  
  
 For more information, see [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).  
  
 [ **\@publish_local_changes_only =** ] **'**_publish_local_changes_only_**'**  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ **\@enabled_for_het_sub=** ] **'**_enabled_for_het_sub_**'**  
 Enables the publication to support non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. *enabled_for_het_sub* is **nvarchar(5)** with a default value of FALSE. A value of **true** means that the publication supports non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. When *enabled_for_het_sub* is **true**, the following restrictions apply:  
  
-   *allow_initialize_from_backup* must be **false**.  
  
-   *allow_push* must be **true**.  
  
-   *allow_queued_tran* must be **false**.  
  
-   *allow_subscription_copy* must be **false**.  
  
-   *allow_sync_tran* must be **false**.  
  
-   *autogen_sync_procs* must be **false**.  
  
-   *conflict_policy* must be NULL.  
  
-   *enabled_for_internet* must be **false**.  
  
-   *enabled_for_p2p* must be **false**.  
  
-   *ftp_address* must be NULL.  
  
-   *ftp_subdirectory* must be NULL.  
  
-   *ftp_password* must be NULL.  
  
-   *pre_snapshot_script* must be NULL.  
  
-   *post_snapshot_script* must be NULL.  
  
-   *replicate_ddl* must be 0.  
  
-   *qreader_job_name* must be NULL.  
  
-   *queue_type* must be NULL.  
  
-   *sync_method* cannot be **native** or **concurrent**.  
  
 For more information, see [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md).  
  
 [ **\@p2p_conflictdetection=** ] **'**_p2p_conflictdetection_**'**  
 Enables the Distribution Agent to detect conflicts if the publication is enabled for peer-to-peer replication. *p2p_conflictdetection* is **nvarchar(5)** with a default value of TRUE. For more information, see [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).  
  
 [ **\@p2p_originator_id=** ] *p2p_originator_id*  
 Specifies an ID for a node in a peer-to-peer topology. *p2p_originator_id* is **int**, with a default of NULL. This ID is used for conflict detection if *p2p_conflictdetection* is set to TRUE. Specify a positive, non-zero ID that has never been used in the topology. For a list of IDs that have already been used, execute [sp_help_peerconflictdetection](../../relational-databases/system-stored-procedures/sp-help-peerconflictdetection-transact-sql.md).  
  
 [ **\@p2p_continue_onconflict=** ] **'**_p2p_continue_onconflict_**'**  
 Determines whether the Distribution Agent continues to process changes after a conflict is detected. *p2p_continue_onconflict* is **nvarchar(5)** with a default value of FALSE.  
  
> [!CAUTION]  
>  We recommend that you use the default value of FALSE. When this option is set to TRUE, the Distribution Agent tries to converge data in the topology by applying the conflicting row from the node that has the highest originator ID. This method does not guarantee convergence. You should make sure that the topology is consistent after a conflict is detected. For more information, see "Handling Conflicts" in [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).  
  
  
 [ **\@allow_partition_switch=** ] **'**_allow_partition_switch_**'**  
 Specifies whether ALTER TABLE...SWITCH statements can be executed against the published database. *allow_partition_switch* is **nvarchar(5)** with a default value of FALSE. For more information, see [Replicate Partitioned Tables and Indexes](../../relational-databases/replication/publish/replicate-partitioned-tables-and-indexes.md).  
  
 [ **\@replicate_partition_switch=** ] **'**_replicate_partition_switch_**'**  
 Specifies whether ALTER TABLE...SWITCH statements that are executed against the published database should be replicated to Subscribers. *replicate_partition_switch* is **nvarchar(5)** with a default value of FALSE. This option is valid only if *allow_partition_switch* is set to TRUE.  

## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addpublication** is used in snapshot replication and transactional replication.  
  
 If multiple publications exist that publish the same database object, only publications with a *replicate_ddl* value of **1** will replicate ALTER TABLE, ALTER VIEW, ALTER PROCEDURE, ALTER FUNCTION, and ALTER TRIGGER DDL statements. However, an ALTER TABLE DROP COLUMN DDL statement will be replicated by all publications that are publishing the dropped column.  
  
 With DDL replication enabled (*replicate_ddl* = **1**) for a publication, in order to make non-replicating DDL changes to the publication, [sp_changepublication](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md) must first be executed to set *replicate_ddl* to **0**. After the non-replicating DDL statements have been issued, [sp_changepublication](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md) can be run again to turn DDL replication back on.  
  
## Example  
 [!code-sql[HowTo#sp_AddTranPub](../../relational-databases/replication/codesnippet/tsql/sp-addpublication-transa_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addpublication**. Windows authentication logins must have a user account in the database representing their Windows user account. A user account representing a Windows group is not sufficient.  
  
## See Also  
 [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md)   
 [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md)   
 [sp_changepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md)   
 [sp_droppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppublication-transact-sql.md)   
 [sp_helppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md)   
 [sp_replicationdboption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
