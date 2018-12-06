---
title: "IHpublications (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "IHpublications_TSQL"
  - "IHpublications"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IHpublications system table"
ms.assetid: b519a101-fa53-44be-bd55-6ea79245b5d1
author: stevestein
ms.author: sstein
manager: craigg
---
# IHpublications (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **IHpublications** system table contains one row for each non-SQL Server publication using the current Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pubid**|**int**|The identity column providing a unique ID for the publication.|  
|**name**|**sysname**|The unique name associated with the publication.|  
|**repl_freq**|**tinyint**|The replication frequency:<br /><br /> **0** = Transaction based.<br /><br /> **1** = Scheduled table refresh.|  
|**status**|**tinyint**|The status of the publication, which can be one of the following.<br /><br /> **0** = Inactive.<br /><br /> **1** = Active.|  
|**sync_method**|**tinyint**|The synchronization method:<br /><br /> **1** = Character bulk copy.<br /><br /> **4** = Concurrent_c, which means that character bulk copy is used but tables are not locked during the snapshot.|  
|**snapshot_jobid**|**binary**|The scheduled task ID.|  
|**enabled_for_internet**|**bit**|Indicates whether the synchronization files for the publication are exposed to the Internet through FTP and other services, where **1** means that they can be accessed from the Internet.|  
|**immediate_sync_ready**|**bit**|Indicates whether the synchronization files are available, where **1** means that they are available. *Not supported for non-SQL Publishers.*|  
|**allow_queued_tran**|**bit**|Specifies whether queuing of changes at the Subscriber until they can be applied at the Publisher has been enabled. If **1**, changes at the Subscriber are queued. *Not supported for non-SQL Publishers.*|  
|**allow_sync_tran**|**bit**|Specifies whether immediate-updating subscriptions are allowed on the publication. **1** means that immediate-updating subscriptions are allowed. *Not supported for non-SQL Publishers.*|  
|**autogen_sync_procs**|**bit**|Specifies whether the synchronizing stored procedure for immediate-updating subscriptions is generated at the Publisher. **1** means that it is generated at the Publisher. *Not supported for non-SQL Publishers.*|  
|**snapshot_in_defaultfolder**|**bit**|Specifies whether snapshot files are stored in the default folder. If **0**, snapshot files have been stored in the alternate location specified by *alternate_snapshot_folder*. If **1**, snapshot files can be found in the default folder.|  
|**alt_snapshot_folder**|**nvarchar(510)**|Specifies the location of the alternate folder for the snapshot.|  
|**pre_snapshot_script**|**nvarchar(510)**|Specifies a pointer to a **.sql** file location. The Distribution Agent runs the pre-snapshot script before running any of the replicated object scripts when applying a snapshot at a Subscriber.|  
|**post_snapshot_script**|**nvarchar(510)**|Specifies a pointer to a **.sql** file location. The Distribution Agent runs the post-snapshot script after all the other replicated object scripts and data have been applied during an initial synchronization.|  
|**compress_snapshot**|**bit**|Specifies that the snapshot that is written to the *alt_snapshot_folder* location is to be compressed into the [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB format. **0** specifies that the snapshot will not be compressed.|  
|**ftp_address**|**sysname**|The network address of the FTP service for the Distributor. Specifies where publication snapshot files are located for the Distribution Agent to pick up.|  
|**ftp_port**|**int**|The port number of the FTP service for the Distributor. Specifies where the publication snapshot files are located for the Distribution Agent to pick up|  
|**ftp_subdirectory**|**nvarchar(510)**|Specifies where the snapshot files are available for the Distribution Agent to pick up if the publication supports propagating snapshots using FTP.|  
|**ftp_login**|**nvarchar(256)**|The username used to connect to the FTP service.|  
|**ftp_password**|**nvarchar(1048)**|The user password used to connect to the FTP service.|  
|**allow_dts**|**bit**|Specifies that the publication allows data transformations. **1** specifies that DTS transformations are allowed. *Not supported for non-SQL Publishers.*|  
|**allow_anonymous**|**bit**|Indicates whether anonymous subscriptions are allowed on the publication, where **1** means that they are allowed.|  
|**centralized_conflicts**|**bit**|Specifies whether conflict records are stored on the Publisher:<br /><br /> **0** = Conflict records are stored at both the publisher and at the subscriber that caused the conflict.<br /><br /> **1** = Conflict records are stored at the Publisher.<br /><br /> *Not supported for non-SQL Publishers.*|  
|**conflict_retention**|**int**|Specifies the conflict retention period, in days. *Not supported for non-SQL Publishers.*|  
|**conflict_policy**|**int**|Specifies the conflict resolution policy followed when the queued updating subscriber option is used. Can be one of these values:<br /><br /> **1** = Publisher wins the conflict.<br /><br /> **2** = Subscriber wins the conflict.<br /><br /> **3** = Subscription is reinitialized.<br /><br /> *Not supported for non-SQL Publishers.*|  
|**queue_type**|**int**|Specifies which type of queue is used. Can be one of these values:<br /><br /> **1** = msmq, which uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing to store transactions.<br /><br /> **2** = sql, which uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store transactions.<br /><br /> This column is not used by non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.<br /><br /> Note: Using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing has been deprecated and is no longer supported.<br /><br /> *This column is not supported for non-SQL Publishers.*|  
|**ad_guidname**|**sysname**|Specifies whether the publication is published in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory. A valid globally unique identifier (GUID) specifies that the publication is published in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory, and the GUID is the corresponding Active Directory publication object **objectGUID**. If NULL, the publication is not published in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory. *Not supported for non-SQL Publishers.*|  
|**backward_comp_level**|**int**|Database compatibility level, which can be one of the following values:<br /><br /> **90** = [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].<br /><br /> **100** = [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].<br /><br /> *Not supported for non-SQL Publishers.*|  
|**description**|**nvarchar(255)**|Descriptive entry for the publication.|  
|**independent_agent**|**bit**|Specifies whether there is a stand-alone Distribution Agent for this publication.<br /><br /> **0** = The publication uses a shared Distribution Agent, and each Publisher database/Subscriber database pair has a single, shared Agent.<br /><br /> **1** = There is a stand-alone Distribution Agent for this publication.|  
|**immediate_sync**|**bit**|Indicates whether the synchronization files are created or recreated each time the Snapshot Agent runs, where **1** means that they are created every time the agent runs.|  
|**allow_push**|**bit**|Indicates whether push subscriptions are allowed on the publication, where **1** means that they are allowed.|  
|**allow_pull**|**bit**|Indicates whether pull subscriptions are allowed on the publication, where **1** means that they are allowed.|  
|**retention**|**int**|The amount of change, in hours, to save for the given publication.|  
|**allow_subscription_copy**|**bit**|Specifies whether the ability to copy the subscription databases that subscribe to this publication has been enabled. **1** means that copying is allowed.|  
|**allow_initialize_from_backup**|**bit**|Indicates if Subscribers can initialize a subscription to this publication from a backup rather than an initial snapshot. **1** means that subscriptions can be initialized from a backup, and **0** means that they cannot. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md). *Not supported for non-SQL Publishers.*|  
|**min_autonosync_lsn**|**binary(1)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**replicate_ddl**|**int**|Indicates if schema replication is supported for the publication. **1** indicates that DDL statements executed at the publisher are replicated, and **0** indicates that DDL statements are not replicated. For more information, see [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md). *Not supported for non-SQL Publishers.*|  
|**options**|**int**|Bitmap that specifies additional publishing options, where the bitwise option values are:<br /><br /> **0x1** - enabled for peer-to-peer replication.<br /><br /> **0x2** - publish only local changes.<br /><br /> **0x4** - enabled for non-SQL Server subscribers.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)   
 [sp_changepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md)   
 [sp_helppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md)   
 [syspublications &#40;System View&#41; &#40;Transact-SQL&#41;](../../relational-databases/system-views/syspublications-system-view-transact-sql.md)   
 [syspublications &#40;Transact-SQL&#41;](../../relational-databases/system-tables/syspublications-transact-sql.md)  
  
  
