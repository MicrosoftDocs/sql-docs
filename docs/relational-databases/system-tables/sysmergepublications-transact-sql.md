---
title: "sysmergepublications (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysmergepublications"
  - "sysmergepublications_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmergepublications system table"
ms.assetid: 7f82c6c3-22d1-47c0-a92b-4d64b98cc455
author: stevestein
ms.author: sstein
manager: craigg
---
# sysmergepublications (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each merge publication defined in the database. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the default server.|  
|**publisher_db**|**sysname**|The name of the default Publisher database.|  
|**name**|**sysname**|The name of the publication.|  
|**description**|**nvarchar(255)**|A brief description of the publication.|  
|**retention**|**int**|The retention period for the entire publication set, where the unit is indicated by the value of the **retention_period_unit** column.|  
|**publication_type**|**tinyint**|Indicates the publication is filtered:<br /><br /> **0** = Not filtered.<br /><br /> **1** = Filtered.|  
|**pubid**|**uniqueidentifier**|The unique identification number for this publication. This is generated when the publication is added.|  
|**designmasterid**|**uniqueidentifier**|Reserved for future use.|  
|**parentid**|**uniqueidentifier**|Indicates the parent publication from which the current peer or subset publication was created (used for hierarchical publishing topologies).|  
|**sync_mode**|**tinyint**|The synchronization mode of this publication:<br /><br /> **0** = Native.<br /><br /> **1** = Character.|  
|**allow_push**|**int**|Indicates whether the publication allows push subscriptions.<br /><br /> **0** = Push subscriptions not allowed.<br /><br /> **1** = Push subscriptions are allowed.|  
|**allow_pull**|**int**|Indicates whether the publication allows pull subscriptions.<br /><br /> **0** = Pull subscriptions not allowed.<br /><br /> **1** = Pull subscriptions are allowed.|  
|**allow_anonymous**|**int**|Indicates whether the publication allows anonymous subscriptions.<br /><br /> **0** = Anonymous subscriptions not allowed.<br /><br /> **1** = Anonymous subscriptions are allowed.|  
|**centralized_conflicts**|**int**|Indicates whether the conflict records are stored at the Publisher:<br /><br /> **0** = Conflict records are not stored at the Publisher.<br /><br /> **1** = Conflict records are stored at the Publisher.|  
|**status**|**tinyint**|Reserved for future use.|  
|**snapshot_ready**|**tinyint**|Indicates the status for the snapshot of the publication:<br /><br /> **0** = Snapshot is not ready for use.<br /><br /> **1** = Snapshot is ready for use.<br /><br /> **2** = A new snapshot for this publication must be created.|  
|**enabled_for_internet**|**bit**|Indicates whether the synchronization files for the publication are exposed to the Internet, through FTP and other services.<br /><br /> **0** = Synchronization files can be accessed from the Internet.<br /><br /> **1** = Synchronization files cannot be accessed from the Internet.|  
|**dynamic_filters**|**bit**|Indicates whether the publication is filtered using a parameterized row filter.<br /><br /> **0** = The publication is not row filtered.<br /><br /> **1** = The publication is row filtered.|  
|**snapshot_in_defaultfolder**|**bit**|Specifies whether snapshot files are stored in the default folder:<br /><br /> **0** = The snapshot files are in the default folder.<br /><br /> **1** = The snapshot files are stored in the location specified by **alt_snapshot_folder**.|  
|**alt_snapshot_folder**|**nvarchar(255)**|The location of the alternate folder for the snapshot.|  
|**pre_snapshot_script**|**nvarchar(255)**|Pointer to a .**sql** file that the Merge Agent runs before any of the replication object scripts when applying the snapshot at the Subscriber.|  
|**post_snapshot_script**|**nvarchar(255)**|The pointer to a .**sql** file that the Merge Agent runs after all the other replication object scripts and data have been applied during an initial synchronization.|  
|**compress_snapshot**|**bit**|Specifies whether the snapshot written to the **alt_snapshot_folder** location is compressed into the [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB format. **0** specifies that the file is not compressed.|  
|**ftp_address**|**sysname**|Network address of the File Transfer Protocol (FTP) service for the Distributor. Specifies where publication snapshot files are located for the Merge Agent to pick up, if FTP is enabled.|  
|**ftp_port**|**int**|The port number of the FTP service for the Distributor.|  
|**ftp_subdirectory**|**nvarchar(255)**|The subdirectory where the snapshot files are available for the Merge Agent to pick up.|  
|**ftp_login**|**sysname**|The user name used to connect to the FTP service.|  
|**ftp_password**|**nvarchar(524)**|The user password used to connect to the FTP service.|  
|**conflict_retention**|**int**|Specifies the retention period, in days, for which conflicts are retained. After this time, the conflict row is purged from the conflict table.|  
|**keep_before_values**|**int**|Specifies whether synchronization optimization is occurring for this publication:<br /><br /> **0** = Synchronization is not optimized, and the partitions sent to all Subscribers will be verified when data changes in a partition.<br /><br /> **1** = Synchronization is optimized, and only Subscribers having rows in the changed partition are affected.|  
|**allow_subscription_copy**|**bit**|Specifies whether the ability to copy the subscription database has been enabled. **0** means copying is not allowed.|  
|**allow_synctoalternate**|**bit**|Specifies whether an alternate synchronization partner is allowed to synchronize with this Publisher. **0** means that a synchronization partner is not allowed.|  
|**validate_subscriber_info**|**nvarchar(500)**|Lists the functions that are being used to retrieve Subscriber information and validate the parameterized row filtering criteria on the Subscriber.|  
|**ad_guidname**|**sysname**|Specifies whether the publication is published in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory. A valid GUID specifies that the publication is published in the Active Directory, and the GUID is the corresponding Active Directory publication object **objectGUID**. If NULL, the publication is not published in Active Directory.|  
|**backward_comp_level**|**int**|Database compatibility level. Can be one of the following values:<br /><br /> **90** = [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].<br /><br /> **100** = [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].|  
|**max_concurrent_merge**|**int**|The maximum number of allowed concurrent merge processes. A value of **0** for this property means that there is no limit to the number of concurrent merge processes running at any given time. This property sets a limit as to the number of concurrent merge processes that can be run against a merge publication at one time. If there are more snapshot processes scheduled at the same time than the value allows to run, then the excess jobs will be put into a queue and wait until a currently-running merge process finishes.|  
|**max_concurrent_dynamic_snapshots**|**int**|The maximum number of allowed concurrent filtered data snapshot sessions that can be running against the merge publication. If **0**, there is no limit to the maximum number of concurrent filtered data snapshot sessions that can run simultaneously against the publication at any given time. This property sets a limit as to the number of concurrent snapshot processes that can be run against a merge publication at one time. If there are more snapshot processes scheduled at the same time than the value allows to run, then the excess jobs will be put into a queue and wait until a currently-running merge process finishes.|  
|**use_partition_groups**|**smallint**|Specifies whether the publication uses precomputed partitions.|  
|**dynamic_filters_function_list**|**nvarchar(500)**|A semi-colon delimited list of functions used in the publication's parameterized row filters.|  
|**partition_id_eval_proc**|**sysname**|Specifies the name of the procedure run by the Merge Agent of a Subscriber to determine its assigned partition ID.|  
|**publication_number**|**smallint**|Specifies the identity column that provides a 2-byte mapping to **pubid**. **pubid** is a globally unique identifier for a publication, whereas publication number is unique only in a specififed database.|  
|**replicate_ddl**|**int**|Indicates whether schema replication is supported for the publication.<br /><br /> **0** = DDL statements are not replicated.<br /><br /> **1** = DDL statements executed at the publisher are replicated.<br /><br /> For more information, see [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).|  
|**allow_subscriber_initiated_snapshot**|**bit**|Indicates that Subscribers can initiate the process that generates the snapshot for a publication using parameterized filters. **1** indicates that Subscribers can initiate the snapshot process.|  
|**dynamic_snapshot_queue_timeout**|**int**|Specifies how many minutes a Subscriber must wait in the queue for the snapshot generation process to begin when using parameterized filters.|  
|**dynamic_snapshot_ready_timeout**|**int**|Specifies how many minutes a Subscriber waits for the snapshot generation process to be completed when using parameterized filters.|  
|**distributor**|**sysname**|The name of the Distributor for the publication.|  
|**snapshot_jobid**|**binary(16)**|Identifies the agent job that generates the snapshot when the Subscriber can initiate the snapshot generation process.|  
|**allow_web_synchronization**|**bit**|Specifies whether the publication is enabled for Web synchronization, where **1** means that Web synchronization is enabled for the publication.|  
|**web_synchronization_url**|**nvarchar(500)**|Specifies the default value of the Internet URL used for Web synchronization.|  
|**allow_partition_realignment**|**bit**|Indicates whether deletes are sent to the subscriber when modification of the row on the publisher causes it to change its partition.<br /><br /> **0** = Data from an old partition will be left on the subscriber, where changes made to this data on the publisher will not replicate to this Subscriber, but changes made on the Subscriber will replicate to the Publisher.<br /><br /> **1** = Deletes to the Subscriber to reflect the results of a partition change by removing data that is not longer part of the Subscriber's partition.<br /><br /> For more information, see [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md).<br /><br /> Note: Data that remains at the Subscriber when this value is **0** should be treated as if it were read-only; however, this is not strictly enforced by the replication system.|  
|**retention_period_unit**|**tinyint**|Defines the unit used when defining *retention*, which can be one of these values:<br /><br /> **0** = Day.<br /><br /> **1** = Week.<br /><br /> **2** = Month.<br /><br /> **3** = Year.|  
|**decentralized_conflicts**|**int**|Indicates whether the conflict records are stored at the Subscriber that caused the conflict:<br /><br /> **0** = Conflict records are not stored at the Subscriber.<br /><br /> **1** = Conflict records are stored at the Subscriber.|  
|**generation_leveling_threshold**|**int**|Specifies the number of changes contained in a generation. A generation is a collection of changes that are delivered to a Publisher or Subscriber.|  
|**automatic_reinitialization_policy**|**bit**|Indicates whether changes are uploaded from the Subscriber before an automatic reinitialization occurs.<br /><br /> **1** = Changes are uploaded from the Subscriber before an automatic reinitialization occurs.<br /><br /> **0** = Changes are not uploaded before an automatic reinitialization.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md)   
 [sp_changemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md)   
 [sp_helpmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md)  
  
  
