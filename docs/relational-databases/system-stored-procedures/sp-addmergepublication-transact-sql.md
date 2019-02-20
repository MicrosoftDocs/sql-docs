---
title: "sp_addmergepublication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addmergepublication"
  - "sp_addmergepublication_TSQL"
helpviewer_keywords: 
  - "sp_addmergepublication"
ms.assetid: 28a629a1-7374-4614-9b04-279d290a942a
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addmergepublication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a new merge publication. This stored procedure is executed at the Publisher on the database that is being published.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmergepublication [ @publication = ] 'publication'   
    [ , [ @description = ] 'description'   
    [ , [ @retention = ] retention ]   
    [ , [ @sync_mode = ] 'sync_mode' ]   
    [ , [ @allow_push = ] 'allow_push' ]   
    [ , [ @allow_pull = ] 'allow_pull' ]   
    [ , [ @allow_anonymous = ] 'allow_anonymous' ]   
    [ , [ @enabled_for_internet = ] 'enabled_for_internet' ]   
    [ , [ @centralized_conflicts = ] 'centralized_conflicts' ]   
    [ , [ @dynamic_filters = ] 'dynamic_filters' ]   
    [ , [ @snapshot_in_defaultfolder = ] 'snapshot_in_default_folder' ]   
    [ , [ @alt_snapshot_folder = ] 'alternate_snapshot_folder' ]   
    [ , [ @pre_snapshot_script = ] 'pre_snapshot_script' ]   
    [ , [ @post_snapshot_script = ] 'post_snapshot_script' ]   
    [ , [ @compress_snapshot = ] 'compress_snapshot' ]   
    [ , [ @ftp_address = ] 'ftp_address' ]   
    [ , [ @ftp_port = ] ftp_port ]   
    [ , [ @ftp_subdirectory = ] 'ftp_subdirectory' ]   
    [ , [ @ftp_login = ] 'ftp_login' ]   
    [ , [ @ftp_password = ] 'ftp_password' ]   
    [ , [ @conflict_retention = ] conflict_retention ]   
    [ , [ @keep_partition_changes = ] 'keep_partition_changes' ]   
    [ , [ @allow_subscription_copy = ] 'allow_subscription_copy' ]   
    [ , [ @allow_synctoalternate = ] 'allow_synctoalternate' ]   
    [ , [ @validate_subscriber_info = ] 'validate_subscriber_info' ]   
    [ , [ @add_to_active_directory = ] 'add_to_active_directory' ]   
    [ , [ @max_concurrent_merge = ] maximum_concurrent_merge ]   
    [ , [ @max_concurrent_dynamic_snapshots = ] max_concurrent_dynamic_snapshots ]  
    [ , [ @use_partition_groups = ] 'use_partition_groups' ]  
    [ , [ @publication_compatibility_level = ] 'backward_comp_level' ]  
    [ , [ @replicate_ddl = ] replicate_ddl ]  
    [ , [ @allow_subscriber_initiated_snapshot = ] 'allow_subscriber_initiated_snapshot' ]   
    [ , [ @allow_web_synchronization = ] 'allow_web_synchronization' ]   
    [ , [ @web_synchronization_url = ] 'web_synchronization_url' ]  
    [ , [ @allow_partition_realignment = ] 'allow_partition_realignment' ]  
    [ , [ @retention_period_unit = ] 'retention_period_unit' ]  
    [ , [ @generation_leveling_threshold = ] generation_leveling_threshold ]  
    [ , [ @automatic_reinitialization_policy = ] automatic_reinitialization_policy ]  
    [ , [ @conflict_logging = ] 'conflict_logging' ]  
```  
  
## Arguments  
 [ **@publication =** ] **'**_publication_**'**  
 Is the name of the merge publication to create. *publication* is **sysname**, with no default, and must not be the keyword ALL. The name of the publication must be unique within the database.  
  
 [ **@description =** ] **'**_description_**'**  
 Is the publication description. *description* is **nvarchar(255)**, with a default of NULL.  
  
 [ **@retention =** ] *retention*  
 Is the retention period, in retention period units, for which to save changes for the given *publication*. *retention* is **int**, with a default of 14 units. Retention period units are defined by *retention_period_unit*. If the subscription is not synchronized within the retention period, and the pending changes it would have received have been removed by a clean-up operation at the Distributor, the subscription expires and must be reinitialized. The maximum allowable retention period is the number of days between Dec. 31, 9999 and the current date.  
  
> [!NOTE]  
>  The retention period for merge publications has a 24 hour grace period to accommodate Subscribers in different time zones. If, for example, you set a retention period of one day, the actual retention period is 48 hours.  
  
 [ **@sync_mode =** ] **'**_sync_mode_**'**  
 Is the mode of the initial synchronization of subscribers to the publication. *sync_mode* is **nvarchar(10)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**native** (default)|Produces native-mode bulk copy program output of all tables.|  
|**character**|Produces character-mode bulk copy program output of all tables. Required to support [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssEW](../../includes/ssew-md.md)] and non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.|  
  
 [ **@allow_push =** ] **'**_allow_push_**'**  
 Specifies if push subscriptions can be created for the given publication. *allow_push* is **nvarchar(5)**, with a default of TRUE, which allows push subscriptions on the publication.  
  
 [ **@allow_pull =** ] **'**_allow_pull_**'**  
 Specifies if pull subscriptions can be created for the given publication. *allow_pull* is **nvarchar(5)**, with a default of TRUE, which allows pull subscriptions on the publication. You must specify true to support [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.  
  
 [ **@allow_anonymous =** ] **'**_allow_anonymous_**'**  
 Specifies if anonymous subscriptions can be created for the given publication. *allow_anonymous* is **nvarchar(5)**, with a default of TRUE, which allows anonymous subscriptions on the publication. To support [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers, you must specify **true**.  
  
 [ **@enabled_for_internet =** ] **'**_enabled_for_internet_**'**  
 Specifies if the publication is enabled for the Internet, and determines if file transfer protocol (FTP) can be used to transfer the snapshot files to a subscriber. *enabled_for_internet* is **nvarchar(5)**, with a default of FALSE. If **true**, the synchronization files for the publication are put into the C:\Program Files\Microsoft SQL Server\MSSQL\MSSQL.x\Repldata\Ftp directory. The user must create the Ftp directory. If **false**, the publication is not enabled for Internet access.  
  
 [ **@centralized_conflicts =**] **'**_centralized_conflicts_**'**  
 This parameter has been deprecated and is only supported for the backward compatibility of scripts. Use *conflict_logging* to specify the location where conflict records are stored.  
  
 [ **@dynamic_filters =**] **'**_dynamic_filters_**'**  
 Enables the merge publication to use parameterized row filters. *dynamic_filters* is **nvarchar(5)**, with a default of FALSE.  
  
> [!NOTE]  
>  You should not specify this parameter but instead allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to automatically determine if parameterized row filters are being used. If you specify a value of **true** for *dynamic_filters*, you must define a parameterized row filter for the article. For more information, see [Define and Modify a Parameterized Row Filter for a Merge Article](../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md).  
  
 [ **@snapshot_in_defaultfolder =** ] **'**_snapshot_in_default_folder_**'**  
 Specifies if the snapshot files are stored in the default folder. *snapshot_in_default_folder* is **nvarchar(5)**, with a default of TRUE. If **true**, snapshot files can be found in the default folder. If **false**, snapshot files will be stored in the alternate location specified by *alternate_snapshot_folder*. Alternate locations can be on another server, on a network drive, or on a removable media (such as CD-ROM or removable disks). You can also save the snapshot files to a File Transfer Protocol (FTP) site, for retrieval by the Subscriber at a later time. Note that this parameter can be true and still have a location specified by *alt_snapshot_folder*. This combination specifies that the snapshot files will be stored in both the default and alternate locations.  
  
 [ **@alt_snapshot_folder =** ] **'**_alternate_snapshot_folder_**'**  
 Specifies the location of the alternate folder for the snapshot. *alternate_snapshot_folder* is **nvarchar(255)**, with a default of NULL.  
  
 [ **@pre_snapshot_script =** ] **'**_pre_snapshot_script_**'**  
 Specifies a pointer to an **.sql** file location. *pre_snapshot_script* is **nvarchar(255)**, with a default of NULL. The Merge Agent will run the pre-snapshot script before any of the replicated object scripts when applying the snapshot at a Subscriber. The script is executed in the security context used by the Merge Agent when connecting to the subscription database. Pre-snapshot scripts are not run on [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.  
  
 [ **@post_snapshot_script =** ] **'**_post_snapshot_script_**'**  
 Specifies a pointer to an **.sql** file location. *post_snapshot_script* is **nvarchar(255)**, with a default of NULL. The Merge Agent will run the post-snapshot script after all the other replicated object scripts and data have been applied during an initial synchronization. The script is executed in the security context used by the Merge Agent when connecting to the subscription database. Post-snapshot scripts are not run on [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.  
  
 [ **@compress_snapshot =** ] **'**_compress_snapshot_**'**  
 Specifies that the snapshot written to the **@alt_snapshot_folder** location is to be compressed into the [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB format. *compress_snapshot* is **nvarchar(5)**, with a default of FALSE. **false** specifies that the snapshot will not be compressed; **true** specifies that the snapshot is to be compressed. Snapshot files that are larger than 2GB cannot be compressed. Compressed snapshot files are uncompressed at the location where the Merge Agent runs; pull subscriptions are typically used with compressed snapshots so that files are uncompressed at the Subscriber. The snapshot in the default folder cannot be compressed. To support [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers, you must specify **false**.  
  
 [ **@ftp_address =** ] **'**_ftp_address_**'**  
 Is the network address of the FTP service for the Distributor. *ftp_address* is **sysname**, with a default of NULL. Specifies where publication snapshot files are located for the Merge Agent of a subscriber to pick up. Since this property is stored for each publication, each publication can have a different *ftp_address*. The publication must support propagating snapshots using FTP.  
  
 [ **@ftp_port=** ] *ftp_port*  
 Is the port number of the FTP service for the Distributor. *ftp_port* is **int**, with a default of 21. Specifies where the publication snapshot files are located for the Merge Agent of a subscriber to pick up. Since this property is stored for each publication, each publication can have its own *ftp_port*.  
  
 [ **@ftp_subdirectory =** ] **'**_ftp_subdirectory_**'**  
 Specifies where the snapshot files will be available for the Merge Agent of the subscriber to pick up if the publication supports propagating snapshots using FTP. *ftp_subdirectory* is **nvarchar(255)**, with a default of NULL. Since this property is stored for each publication, each publication can have its own *ftp_subdirctory* or choose to have no subdirectory, indicated with a NULL value.  
  
 When pre-generating snapshots for publications with parameterized filters, the data snapshot for each Subscriber partition needs to be in its own folder. The directory structure for pre-generated snapshots using FTP must obey the following structure:  
  
 *alternate_snapshot_folder*\ftp\\*publisher_publicationDB_publication*\\*partitionID*.  
  
> [!NOTE]  
>  The values above in italics will depend on the specifics of the publication and Subscriber partition.  
  
 [ **@ftp_login =** ] **'**_ftp_login_**'**  
 Is the username used to connect to the FTP service. *ftp_login* is **sysname**, with a default of 'anonymous'.  
  
 [ **@ftp_password =** ] **'**_ftp_password_**'**  
 Is the user password used to connect to the FTP service. *ftp_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password.  
  
 [ **@conflict_retention =** ] *conflict_retention*  
 Specifies the retention period, in days, for which conflicts are retained. *conflict_retention* is **int**, with a default of 14 days before the conflict row is purged from the conflict table.  
  
 [ **@keep_partition_changes =** ] **'**_keep_partition_changes_**'**  
 Specifies whether to enable partition change optimizations when precomputed partitions cannot be used. *keep_partition_changes* is **nvarchar(5)**, with a default of TRUE. **false** means that partition changes are not optimized, and when precomputed partitions are not used, the partitions sent to all Subscribers will be verified when data changes in a partition. **true** means that partition changes are optimized, and only Subscribers having rows in the changed partitions are affected. When using precomputed partitions, set *use_partition_groups* to **true** and set *keep_partition_changes* to **false**. For more information, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).  
  
> [!NOTE]  
>  If you specify a value of **true** for *keep_partition_changes*, specify a value of **1** for the Snapshot Agent parameter **-MaxNetworkOptimization**. For more information about this parameter, see [Replication Snapshot Agent](../../relational-databases/replication/agents/replication-snapshot-agent.md). For information about how to specify agent parameters, see [Replication Agent Administration](../../relational-databases/replication/agents/replication-agent-administration.md).  
  
 With [!INCLUDE[ssEW](../../includes/ssew-md.md)] subscribers, *keep_partition_changes* must be set to true to ensure that deletes are correctly propagated. When set to false, the subscriber might have more rows than expected.  
  
 [ **@allow_subscription_copy=** ] **'**_allow_subscription_copy_**'**  
 Enables or disables the ability to copy the subscription databases that subscribe to this publication. *allow_subscription_copy* is **nvarchar(5)**, with a default of FALSE. The size of the subscription database being copied must be less than 2 gigabytes (GB).  
  
 [ **@allow_synctoalternate =** ] **'**_allow_synctoalternate_**'**  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ **@validate_subscriber_info =** ] **'**_validate_subscriber_info_**'**  
 Lists the functions that are used to define a Subscriber partition of the published data when parameterized row filters are used. *validate_subscriber_info* is **nvarchar(500)**, with a default of NULL. This information is used by the Merge Agent to validate the Subscriber's partition. For example, if [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) is used in the parameterized row filter, the parameter should be `@validate_subscriber_info=N'SUSER_SNAME()'`.  
  
> [!NOTE]  
>  You should not specify this parameter but instead allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to automatically determine the filtering criterion.  
  
 [ **@add_to_active_directory =** ] **'**_add_to_active_directory_**'**  
 This parameter has been deprecated and is only supported for the backward compatibility of scripts. You can no longer add publication information to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory.  
  
 [ **@max_concurrent_merge =** ] *maximum_concurrent_merge*  
 The maximum number of concurrent merge processes. *maximum_concurrent_merge* is **int** with a default of 0. A value of **0** for this property means that there is no limit to the number of concurrent merge processes running at any given time. This property sets a limit on the number of concurrent merge processes that can be run against a merge publication at one time. If there are more merge processes scheduled at the same time than the value allows to run, then the excess jobs will be put into a queue and wait until a currently-running merge process finishes.  
  
 [ **@max_concurrent_dynamic_snapshots =**] *max_concurrent_dynamic_snapshots*  
 The maximum number of Snapshot Agent sessions that can be run concurrently to generate filtered data snapshots for Subscriber partitions. *maximum_concurrent_dynamic_snapshots* is **int** with a default of 0. If **0**, there is no limit to the number snapshot sessions. If there are more snapshot processes scheduled at the same time than the value allows to run, then the excess jobs will be put into a queue and wait until a currently-running snapshot process finishes.  
  
 [ **@use_partition_groups =** ] **'**_use_partition_groups_**'**  
 Specifies that precomputed partitions should be used to optimize the synchronization process. *use_partition_groups* is **nvarchar(5)**, and can be one of these values:  
  
|Value|Description|  
|-----------|-----------------|  
|**true**|Publication uses precomputed partitions.|  
|**false**|Publication does not use precomputed partitions.|  
|NULL(default)|System decides on the partitioning strategy.|  
  
 Precomputed partitions are used by default. To avoid using precomputed partitions, *use_partition_groups* must be set to **false**. When NULL, the system will decide if precomputed partitions can be used. If precomputed partitions cannot be used, then this value will effectively become **false** without generating any errors. In such cases, *keep_partition_changes* can be set to **true** to provide some optimization. For more information, see [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md) and [Optimize Parameterized Filter Performance with Precomputed Partitions](../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).  
  
 [ **@publication_compatibility_level =** ] *backward_comp_level*  
 Indicates the backward compatibility of the publication. *backward_comp_level* is **nvarchar(6)**, and can be one of these values:  
  
|Value|Version|  
|-----------|-------------|  
|**90RTM**|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|  
|**100RTM**|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|  
  
 [ **@replicate_ddl =** ] *replicate_ddl*  
 Indicates if schema replication is supported for the publication. *replicate_ddl* is **int**, with a default of 1. **1** indicates that data definition language (DDL) statements executed at the publisher are replicated, and **0** indicates that DDL statements are not replicated. For more information, see [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).  
  
 The *@replicate_ddl* parameter is honored when a DDL statement adds a column. The *@replicate_ddl* parameter is ignored when a DDL statement alters or drops a column for the following reasons.  
  
-   When a column is dropped, sysarticlecolumns must be updated to prevent new DML statements from including the dropped column which would cause the distribution agent to fail. The *@replicate_ddl* parameter is ignored because replication must always replicate the schema change.  
  
-   When a column is altered, the source data type or nullability might have changed, causing DML statements to contain a value that may not be compatible with the table at the subscriber. Such DML statements might cause distribution agent to fail. The *@replicate_ddl* parameter is ignored because replication must always replicate the schema change.  
  
-   When a DDL statement adds a new column, sysarticlecolumns does not include the new column. DML statements will not try to replicate data for the new column. The parameter is honored because either replicating or not replicating the DDL is acceptable.  
  
 [ **@allow_subscriber_initiated_snapshot =** ] **'**_allow_subscriber_initiated_snapshot_**'**  
 Indicates if Subscribers to this publication can initiate the snapshot process to generate the filtered snapshot for their data partition. *allow_subscriber_initiated_snapshot* is **nvarchar(5)**, with a default of FALSE. **true** indicates that Subscribers can initiate the snapshot process.  
  
 [ **@allow_web_synchronization =** ] **'**_allow_web_synchronization_**'**  
 Specifies if the publication is enabled for Web synchronization. *allow_web_synchronization* is **nvarchar(5)**, with a default of FALSE. **true** specifies that subscriptions to this publication can be synchronized over HTTPS. For more information, see [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md). To support [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers, you must specify **true**.  
  
 [ **@web_synchronization_url=** ] **'**_web_synchronization_url_**'**  
 Specifies the default value of the Internet URL used for Web synchronization. *web_synchronization_url i*s **nvarchar(500)**, with a default of NULL. Defines the default Internet URL if one is not explicitly set when [sp_addmergepullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md) is executed.  
  
 [ **@allow_partition_realignment =** ] **'**_allow_partition_realignment_**'**  
 Determines whether deletes are sent to the subscriber when modification of the row on the publisher causes it to change its partition. *allow_partition_realignment* is **nvarchar(5)**, with a default of TRUE. **true** sends deletes to the Subscriber to reflect the results of a partition change by removing data that is no longer part of the Subscriber's partition. **false** leaves the data from an old partition on the Subscriber, where changes made to this data on the Publisher will not replicate to this Subscriber, but changes made on the Subscriber will replicate to the Publisher. Setting *allow_partition_realignment* to **false** is used to retain data in a subscription from an old partition when the data needs to be accessible for historical purposes.  
  
> [!NOTE]  
>  Data that remains at the Subscriber as a result of setting *allow_partition_realignment* to **false** should be treated as if it were read-only; however, this is not enforced by the replication system.  
  
 [ **@retention_period_unit =** ] **'**_retention_period_unit_**'**  
 Specifies the units for the retention period set by *retention*. *retention_period_unit* is **nvarchar(10)**, and can be one of the following values.  
  
|Value|Version|  
|-----------|-------------|  
|**day** (default)|Retention period is specified in days.|  
|**week**|Retention period is specified in weeks.|  
|**month**|Retention period is specified in months.|  
|**year**|Retention period is specified in years.|  
  
 [ **@generation_leveling_threshold=** ] *generation_leveling_threshold*  
 Specifies the number of changes that are contained in a generation. A generation is a collection of changes that are delivered to a Publisher or Subscriber. *generation_leveling_threshold* is **int**, with a default value of 1000.  
  
 [ **@automatic_reinitialization_policy =** ] *automatic_reinitialization_policy*  
 Specifies whether changes are uploaded from the Subscriber before an automatic reinitialization required by a change to the publication, where a value of **1** was specified for **@force_reinit_subscription**. *automatic_reinitialization_policy* is bit, with a default value of 0. **1** means that changes are uploaded from the Subscriber before an automatic reinitialization occurs.  
  
> [!IMPORTANT]  
>  If you add, drop, or change a parameterized filter, pending changes at the Subscriber cannot be uploaded to the Publisher during reinitialization. If you want to upload pending changes, synchronize all subscriptions before changing the filter.  
  
 [ **@conflict_logging =** ] **'**_conflict_logging_**'**  
 Specifies where conflict records are stored. *conflict_logging* is **nvarchar(15)**, and can be one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**publisher**|Conflict records are stored at the Publisher.|  
|**subscriber**|Conflict records are stored at the Subscriber that caused the conflict. Not supported for [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.|  
|**both**|Conflict records are stored at both the Publisher and Subscriber.|  
|NULL (default)|Replication automatically sets *conflict_logging* to **both** when the value *backward_comp_level* is **90RTM** and to **publisher** in all other cases.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_addmergepublication** is used in merge replication.  
  
 To list publication objects to the Active Directory using the **@add_to_active_directory** parameter, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object must already be created in the Active Directory.  
  
 If multiple publications exist that publish the same database object, only publications with a *replicate_ddl* value of **1** will replicate ALTER TABLE, ALTER VIEW, ALTER PROCEDURE, ALTER FUNCTION, and ALTER TRIGGER DDL statements. However, an ALTER TABLE DROP COLUMN DDL statement will be replicated by all publications that are publishing the dropped column.  
  
 For [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers, the value of *alternate_snapshot_folder* is only used when the value of *snapshot_in_default_folder* is **false**.  
  
 With DDL replication enabled (_replicate_ddl_**=1**) for a publication, in order to make non-replicating DDL changes to the publication, [sp_changemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md) must first be executed to set *replicate_ddl* to **0**. After the non-replicating DDL statements have been issued, **sp_changemergepublication** can be run again to turn DDL replication back on.  
  
## Example  
 [!code-sql[HowTo#sp_AddMergePub](../../relational-databases/replication/codesnippet/tsql/sp-addmergepublication-t_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addmergepublication**.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [sp_changemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md)   
 [sp_dropmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergepublication-transact-sql.md)   
 [sp_helpmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
