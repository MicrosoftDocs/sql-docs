---
title: "sp_changemergepublication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changemergepublication_TSQL"
  - "sp_changemergepublication"
helpviewer_keywords: 
  - "sp_changemergepublication"
ms.assetid: 81fe1994-7678-4852-980b-e02fedf1e796
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changemergepublication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of a merge publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changemergepublication [ @publication= ] 'publication'  
    [ , [ @property= ] 'property' ]  
    [ , [ @value= ] 'value' ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
```  
  
## Arguments  
 [ **@publication=**] **'***publication***'**  
 The name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@property=**] **'***property***'**  
 The property to change for the given publication. *property* is **sysname**, and can be one of the values listed in the table that follows.  
  
 [ **@value=**]  **'***value***'**  
 The new value for the specified property. *value* is **nvarchar(255)**, and can be one of the values listed in the table that follows.  
  
 This table describes the properties of the publication that can be changed, and describes restrictions on the values for those properties.  
  
|Property|Value|Description|  
|--------------|-----------|-----------------|  
|**allow_anonymous**|**true**|Anonymous subscriptions are allowed.|  
||**false**|Anonymous subscriptions are not allowed.|  
|**allow_partition_realignment**|**true**|Deletes are sent to the Subscriber to reflect the results of a partition change by removing data that is no longer part of the Subscriber's partition. This is the default behavior.|  
||**false**|Data from an old partition is left on the Subscriber, where changes made to this data on the Publisher do not replicate to this Subscriber. Instead, changes that are made on the Subscriber replicate to the Publisher. This is used to retain data in a subscription from an old partition when the data has to be accessible for historical purposes.|  
|**allow_pull**|**true**|Pull subscriptions are allowed for the given publication.|  
||**false**|Pull subscriptions are not allowed for the given publication.|  
|**allow_push**|**true**|Push subscriptions are allowed for the given publication.|  
||**false**|Push subscriptions are not allowed for the given publication.|  
|**allow_subscriber_initiated_snapshot**|**true**|Subscriber can initiate the snapshot process.|  
||**false**|Subscriber cannot initiate the snapshot process.|  
|**allow_subscription_copy**|**true**|You can copy the subscription databases that subscribe to this publication.|  
||**false**|You cannot copy the subscription databases that subscribe to this publication.|  
|**allow_synctoalternate**|**true**|Allows an alternative synchronization partner to synchronize with this Publisher.|  
||**false**|Does not allow an alternative synchronization partner to synchronize with this Publisher.|  
|**allow_web_synchronization**|**true**|Subscriptions can be synchronized over HTTPS.|  
||**false**|Subscriptions cannot be synchronized over HTTPS.|  
|**alt_snapshot_folder**||Specifies the location of the alternative folder for the snapshot.|  
|**automatic_reinitialization_policy**|**1**|Changes are uploaded from the Subscriber before the subscription is reinitialized.|  
||**0**|The subscription is reinitialized without first uploading changes.|  
|**centralized_conflicts**|**true**|All conflict records are stored at the Publisher. If you change this property, existing Subscribers must be reinitialized.|  
||**false**|Conflict records are stored at the server that lost in the conflict resolution. If you change this property, existing Subscribers must be reinitialized.|  
|**compress_snapshot**|**true**|Snapshot in an alternative snapshot folder is compressed into the CAB format. The snapshot in the default snapshot folder cannot be compressed. Changing this property requires a new snapshot.|  
||**false**|By default, the snapshot is not compressed. Changing this property requires a new snapshot.|  
|**conflict_logging**|**publisher**|Conflict records are stored at the Publisher.|  
||**subscriber**|Conflict records are stored at the Subscriber that caused the conflict. Not supported for [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers*.*|  
||**both**|Conflict records are stored at both the Publisher and Subscriber.|  
|**conflict_retention**||An **int** that specifies the retention period, in days, for which conflicts are retained. Setting *conflict_retention* to **0** means no conflict cleanup is needed.|  
|**description**||Description of the publication.|  
|**dynamic_filters**|**true**|Publication is filtered based on a dynamic clause.|  
||**false**|Publication is not filtered dynamically.|  
|**enabled_for_internet**|**true**|Publication is enabled for the Internet. File Transfer Protocol (FTP) can be used to transfer the snapshot files to a Subscriber. The synchronization files for the publication are put into the C:\Program Files\Microsoft SQL Server\MSSQL\Repldata\ftp directory.|  
||**false**|Publication is not enabled for the Internet.|  
|**ftp_address**||The network address of the FTP service for the Distributor. Specifies where publication snapshot files are stored.|  
|**ftp_login**||The user name that is used to connect to the FTP service.|  
|**ftp_password**||The user password that is used to connect to the FTP service.|  
|**ftp_port**||The port number of the FTP service for the Distributor. Specifies the TCP port number of the FTP site where the publication snapshot files are stored.|  
|**ftp_subdirectory**||Specifies where the snapshot files are created if the publication supports propagating snapshots by using FTP.|  
|**generation_leveling_threshold**|**int**|Specifies the number of changes that are contained in a generation. A generation is a collection of changes that are delivered to a Publisher or Subscriber.|  
|**keep_partition_changes**|**true**|Synchronization is optimized, and only Subscribers that have rows in the changed partitions are affected. Changing this property requires a new snapshot.|  
||**false**|Synchronization is not optimized, and the partitions that are sent to Subscribers are verified when data changes in a partition. Changing this property requires a new snapshot.|  
|**max_concurrent_merge**||This is an **int** that represents the maximum number of concurrent merge processes that can be run against a publication. If 0, there is no limit.If more than this number of merge processes are scheduled to run at the same time, the excess jobs are put into a queue until a currentlmerge process finishes.|  
|**max_concurrent_dynamic_snapshots**||This is an **int** that represents the maximum number of snapshot sessions to generate a filtered data snapshot that can concurrently run against a merge publication that uses parameterized row filters. If **0**, there is no limit. If more than this number of snapshot processes are scheduled to run at the same time, the excess jobs are put into a queue until a current merge process finishes.|  
|**post_snapshot_script**||Specifies a pointer to an **.sql** file location. The Distribution Agent or Merge Agent runs the post-snapshot script after all the other replicated object scripts and data have been applied during an initial synchronization. Changing this property requires a new snapshot.|  
|**pre_snapshot_script**||Specifies a pointer to an **.sql** file location. The Merge Agent runs the pre-snapshot script before any of the replicated object scripts when applying a snapshot at a Subscriber. Changing this property requires a new snapshot.|  
|**publication_compatibility_level**|**100RTM**|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|  
||**90RTM**|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|  
|**publish_to_activedirectory**|**true**|This parameter has been deprecated and is only supported for the backward compatibility of scripts. You can no longer add publication information to Active Directory.|  
||**false**|Removes the publication information from Active Directory.|  
|**replicate_ddl**|**1**|Data Definition Language (DDL) statements that are executed at the Publisher are replicated.|  
||**0**|DDL statements are not replicated.|  
|**retention**||This is an **int** that represents the number of *retention_period_unit* units for which to save changes for the given publication. If the subscription is not synchronized within the retention period, and the pending changes it would have received have been removed by a clean-up operation at the Distributor, the subscription expires and must be reinitialized. The maximum allowable retention period is the number of days between December 31, 9999, and the current date.<br /><br /> Note: The retention period for merge publications has a 24 hour grace period to accommodate Subscribers in different time zones.|  
|**retention_period_unit**|**day**|Retention period is specified in days.|  
||**week**|Retention period is specified in weeks.|  
||**month**|Retention period is specified in months.|  
||**year**|Retention period is specified in years.|  
|**snapshot_in_defaultfolder**|**true**|Snapshot files are stored in the default snapshot folder.|  
||**false**|Snapshot files are stored in the alternative location that is specified by *alt_snapshot_folder*. This combination specifies that the snapshot files are stored in both the default and alternative locations.|  
|**snapshot_ready**|**true**|Snapshot for the publication is available.|  
||**false**|Snapshot for the publication is not available.|  
|**status**|**active**|Publication is in an active state.|  
||**inactive**|Publication is in an inactive state.|  
|**sync_mode**|**native** or<br /><br /> **bcp native**|Native-mode bulk-copy program output of all tables is used for the initial snapshot.|  
||**character**<br /><br /> or **bcp character**|Character-mode bulk-copy program output of all tables is used for the initial snapshot, which is required for all non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.|  
|**use_partition_groups**<br /><br /> Note: After using partition_groups,  if you to revert to using **setupbelongs**, and set **use_partition_groups=false** in **changemergearticle**, this might not be correctly reflected after a snapshot is taken. The triggers that are generated by snapshot are compliant with partition groups.<br /><br /> The workaround to this scenario is to set the status to Inactive, modify the **use_partition_groups**, and then set status to Active.|**true**|Publication uses precomputed partitions.|  
||**false**|Publication does not use precomputed partitions.|  
|**validate_subscriber_info**||Lists the functions that are being used to retrieve Subscriber information. Then, validates the dynamic filtering criteria that is being used for the Subscriber to verify that the information is partitioned consistently.|  
|**web_synchronization_url**||Default value of the Internet URL used for Web synchronization.|  
|NULL (default)||Returns the list of supported values for *property*.|  
  
 [ **@force_invalidate_snapshot =** ] *force_invalidate_snapshot*  
 Acknowledges that the action taken by this stored procedure might invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default of **0**.  
  
 **0** specifies that changing the publication does not invalidate the snapshot. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.  
  
 **1** specifies that changing the publication might invvalidate the snapshot. If there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and for a new snapshot to be generated.  
  
 See the Remarks section for the properties that, when changed, require a new snapshot to be generated.  
  
 [ **@force_reinit_subscription =** ] *force_reinit_subscription*  
 Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit** with a default of **0**.  
  
 **0** specifies that changing the publication does not require that subscriptions be reinitialized. If the stored procedure detects that the change would require existing subscriptions to be reinitialized, an error occurs and no changes are made.  
  
 **1** specifies that changing the publication causes existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
 See the Remarks section for the properties that, when changed, require that all existing subscriptions be reinitialized.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changemergepublication** is used in merge replication.  
  
 Changing the following properties requires that a new snapshot be generated. You must specify a value of **1** for the *force_invalidate_snapshot* parameter.  
  
-   **alt_snapshot_folder**  
  
-   **compress_snapshot**  
  
-   **dynamic_filters**  
  
-   **ftp_address**  
  
-   **ftp_login**  
  
-   **ftp_password**  
  
-   **ftp_port**  
  
-   **ftp_subdirectory**  
  
-   **post_snapshot_script**  
  
-   **publication_compatibility_level** (to **80SP3** only)  
  
-   **pre_snapshot_script**  
  
-   **snapshot_in_defaultfolder**  
  
-   **sync_mode**  
  
-   **use_partition_groups**  
  
 Changing the following properties requires that existing subscriptions be reinitialized. You must specify a value of **1** for the *force_reinit_subscription* parameter.  
  
-   **dynamic_filters**  
  
-   **validate_subscriber_info**  
  
 To list publication objects to Active Directory by using the *publish_to_active_directory*, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object must already be created in Active Directory.  
  
## Example  
 [!code-sql[HowTo#sp_changemergepublication](../../relational-databases/replication/codesnippet/tsql/sp-changemergepublicatio_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changemergepublication**.  
  
## See Also  
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md)   
 [sp_dropmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergepublication-transact-sql.md)   
 [sp_helpmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
