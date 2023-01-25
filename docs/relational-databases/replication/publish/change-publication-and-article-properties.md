---
description: "Change Publication and Article Properties"
title: "Change Publication and Article Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying article properties"
  - "modifying publication properties"
  - "administering replication, properties"
  - "publications [SQL Server replication], changing properties"
  - "articles [SQL Server replication], properties"
ms.assetid: f7df51ef-c088-4efc-b247-f91fb2c6ff32
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Change Publication and Article Properties
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  After a publication has been created, most publication and article properties can be changed, but some require that the snapshot be regenerated and/or subscriptions be reinitialized. This topic provides information about all properties that require one or both of these actions if they are changed.  
  
## Publication Properties for Snapshot and Transactional Replication  
  
|Description|Stored procedure|Properties|Requirements|  
|-----------------|----------------------|----------------|------------------|  
|Change snapshot format.|**sp_changepublication**|**sync_method**|New snapshot.|  
|Change snapshot location.|**sp_changepublication**|**alt_snapshot_folder**<br /><br /> **snapshot_in_defaultfolder**|New snapshot.|  
|Change snapshot location.|**sp_changedistpublisher**|**working_directory**|New snapshot.|  
|Change snapshot compression.|**sp_changepublication**|**compress_snapshot**|New snapshot.|  
|Change any File Transfer Protocol (FTP) snapshot options.|**sp_changepublication**|**enabled_for_internet**<br /><br /> **ftp_address**<br /><br /> **ftp_login**<br /><br /> **ftp_password**<br /><br /> **ftp_port**<br /><br /> **ftp_subdirectory**|New snapshot.|  
|Change pre- or post-snapshot script location.|**sp_changepublication**|**pre_snapshot_script**<br /><br /> **post_snapshot_script**|New snapshot (also required if you change the script contents).<br /><br /> Reinitialization is required to apply the new script to the Subscriber.|  
|Enable or disable support for non-[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers.|**sp_changepublication**|**is_enabled_for_het_sub**|New snapshot.|  
|Change conflict reporting for queued updating subscriptions|**sp_changepublication**|**centralized_conflicts**|Can only be changed if there are no active subscriptions.|  
|Change conflict resolution policy for queued updating subscriptions.|**sp_changepublication**|**conflict_policy**|Can only be changed if there are no active subscriptions.|  
  
## Article Properties for Snapshot and Transactional Replication  
  
|Description|Stored procedure|Properties|Requirements|  
|-----------------|----------------------|----------------|------------------|  
|Drop an article|**sp_droparticle**|All parameters.|Articles can be dropped prior to subscriptions being created. Using stored procedures, it is possible to drop a subscription to an article; using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], the entire subscription must be dropped, recreated, and synchronized. For more information, see [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).|  
|Change a column filter.|**sp_articlecolumn**|`@column`<br /><br /> `@operation`|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Add a row filter.|**sp_articlefilter**|All parameters.|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Drop a row filter.|**sp_articlefilter**|`@article`|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change a row filter.|**sp_articlefilter**|`@filter_clause`|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change a row filter.|**sp_changearticle**|**filter**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change schema options.|**sp_changearticle**|**schema_option**|New snapshot.|  
|Change how tables at the Subscriber are handled prior to applying the snapshot.|**sp_changearticle**|**pre_creation_cmd**|New snapshot.|  
|Change article status|**sp_changearticle**|**status**|New snapshot.|  
|Change INSERT, UPDATE or DELETE commands.|**sp_changearticle**|**ins_cmd**<br /><br /> **upd_cmd**<br /><br /> **del_cmd**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change destination table name|**sp_changearticle**|**dest_table**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change destination table owner (schema).|**sp_changearticle**|**destination_owner**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change data type mappings (applies to Oracle publishing only).|**sp_changearticlecolumndatatype**|`@type` <br /><br /> `@length` <br /><br /> `@precision`<br /><br /> `@scale`|New snapshot.<br /><br /> Reinitialize subscriptions.|  
  
## Publication Properties for Merge Replication  
  
|Description|Stored procedure|Properties|Requirements|  
|-----------------|----------------------|----------------|------------------|  
|Change snapshot format|**sp_changemergepublication**|**sync_mode**|New snapshot.|  
|Change snapshot location.|**sp_changemergepublication**|**alt_snapshot_folder**<br /><br /> **snapshot_in_defaultfolder**|New snapshot.|  
|Change snapshot location.|**sp_changedistpublisher**|**working_directory**|New snapshot.|  
|Change snapshot compression|**sp_changemergepublication**|**compress_snapshot**|New snapshot.|  
|Change any FTP snapshot options|**sp_changemergepublication**|**enabled_for_internet**<br /><br /> **ftp_address**<br /><br /> **ftp_login**<br /><br /> **ftp_password**<br /><br /> **ftp_port**<br /><br /> **ftp_subdirectory**|New snapshot.|  
|Change pre- or post-snapshot scripts.|**sp_changemergepublication**|**pre_snapshot_script**<br /><br /> **post_snapshot_script**|New snapshot (also required if you change the script contents).<br /><br /> Reinitialization is required to apply the new script to the Subscriber.|  
|Add a join filter or logical record.|**sp_addmergefilter**|All parameters.|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Drop a join filter or logical record.|**sp_dropmergefilter**|All parameters.|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change a join filter or logical record.|**sp_changemergefilter**|`@property`<br /><br /> `@value`|New snapshot<br /><br /> Reinitialize subscriptions.|  
|Disable the use of parameterized filters (enabling parameterized filters does not require any special actions).|**sp_changemergepublication**|A value of **false** for **dynamic_filters**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Enable or disable the use of precomputed partitions.|**sp_changemergepublication**|**use_partition_groups**|New snapshot.|  
|Enable or disable [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] partition optimization.|**sp_changemergepublication**|**keep_partition_changes**|Reinitialize subscriptions.|  
|Enable or disable Subscriber partition validation.|**sp_changemergepublication**|**validate_subscriber_info**|Reinitialize subscriptions.|  
|Change the publication compatibility level to 80sp3 or lower.|**sp_changemergepublication**|**publication_compatibility_level**|New snapshot.|  
  
## Article Properties for Merge Replication  
  
|Description|Stored Procedure|Properties|Requirements|  
|-----------------|----------------------|----------------|------------------|  
|Drop an article, where the article has the last parameterized filter in the publication.|**sp_dropmergearticle**|All parameters|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Drop an article, where the article is a parent in a join filter or logical record (this has the side effect of dropping the join).|**sp_dropmergearticle**|All parameters|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Drop an article, all other circumstances.|**sp_dropmergearticle**|All parameters|New snapshot.|  
|Include a column filter that was previously unpublished.|**sp_mergearticlecolumn**|`@column`<br /><br /> `@operation`|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Add, drop, or change a row filter.|**sp_changemergearticle**|**subset_filterclause**|New snapshot.<br /><br /> Reinitialize subscriptions.<br /><br /> If you add, drop, or change a parameterized filter, pending changes at the Subscriber cannot be uploaded to the Publisher during reinitialization. If you want to upload pending changes, synchronize all subscriptions before changing the filter.<br /><br /> If an article is not involved in any join filters, you can drop the article and add it again with a different row filter, which does not require the entire subscription to be reinitialized. For more information about adding and dropping articles, see [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).|  
|Change schema options.|**sp_changemergearticle**|**schema_option**|New snapshot.|  
|Change tracking from column-level to row-level (changing from row-level tracking to column-level tracking does not require any special actions).|**sp_changemergearticle**|A value of **false** for **column_tracking**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Change whether permissions are checked before statements made at the Subscriber are applied at the Publisher.|**sp_changemergearticle**|**check_permissions**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
|Enable or disable download-only subscriptions (changing to or from other upload options does not require any special actions).|**sp_changemergearticle**|Change to or from a value of **2** for **subscriber_upload_options**|Reinitialize subscriptions.|  
|Change destination table owner.|**sp_changemergearticle**|**destination_owner**|New snapshot.<br /><br /> Reinitialize subscriptions.|  
  
## See Also  
 [Replication Administration FAQ](../../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml)   
 [Create and Apply the Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)   
 [Reinitialize Subscriptions](../../../relational-databases/replication/reinitialize-subscriptions.md)   
 [sp_addmergefilter &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql.md)   
 [sp_articlecolumn &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md)   
 [sp_articlefilter &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-articlefilter-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)   
 [sp_changearticlecolumndatatype &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changearticlecolumndatatype-transact-sql.md)   
 [sp_changedistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changedistpublisher-transact-sql.md)   
 [sp_changemergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md)   
 [sp_changemergefilter &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changemergefilter-transact-sql.md)   
 [sp_changemergepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md)   
 [sp_changepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md)   
 [sp_droparticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md)   
 [sp_dropmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-dropmergearticle-transact-sql.md)   
 [sp_dropmergefilter &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-dropmergefilter-transact-sql.md)   
 [sp_mergearticlecolumn &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-mergearticlecolumn-transact-sql.md)  
  
  
