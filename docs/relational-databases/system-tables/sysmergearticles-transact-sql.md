---
title: "sysmergearticles (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysmergearticles"
  - "sysmergearticles_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmergearticles system table"
ms.assetid: e9b1648e-4660-4688-9f56-18b2baf7228c
author: stevestein
ms.author: sstein
manager: craigg
---
# sysmergearticles (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each merge article defined in the local database. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the article.|  
|**type**|**tinyint**|Indicates the article type, which can be one of the following:<br /><br /> **10** = Table.<br /><br /> **32** = Stored procedure (schema only).<br /><br /> **64** = View or indexed view (schema only).<br /><br /> **128** = User-defined function (schema only).<br /><br /> **160** = Synonym (schema only).|  
|**objid**|**int**|The object identifier.|  
|**sync_objid**|**int**|The object ID of the view representing the synchronized data set.|  
|**view_type**|**tinyint**|The type of view:<br /><br /> **0** = Not a view; use all of base object.<br /><br /> **1** = Permanent view.<br /><br /> **2** = Temporary view.|  
|**artid**|**uniqueidentifier**|The unique identification number for the given article.|  
|**description**|**nvarchar(255)**|The brief description of the article.|  
|**pre_creation_command**|**tinyint**|The default action to take when the article is created in the subscription database:<br /><br /> **0 =** None - if the table already exists at the Subscriber, no action is taken.<br /><br /> **1** = Drop - drops the table before re-creating it.<br /><br /> **2** = Delete -issues a delete based on the WHERE clause in the subset filter.<br /><br /> **3** = Truncate -same as **2**, but deletes pages instead of rows. However, does not take a WHERE clause.|  
|**pubid**|**uniqueidentifier**|The ID of the publication to which the current article belongs.|  
|**nickname**|**int**|The nickname mapping for article identification.|  
|**column_tracking**|**int**|Iindicates whether column tracking is implemented for the article.|  
|**status**|**tinyint**|Indicates the status of the article, which can be one of the following:<br /><br /> **1** = Unsynced - the initial processing script to publish the table will run the next time the Snapshot Agent runs.<br /><br /> **2** = Active - the initial processing script to publish the table has been run.<br /><br /> **5** = New_inactive - to be added.<br /><br /> **6** = New_active - to be added.|  
|**conflict_table**|**sysname**|The name of the local table that contains the conflicting records for the current article. This table is supplied for information only, and its contents may be modified or deleted by custom conflict resolution routines or directly by the administrator.|  
|**creation_script**|**nvarchar(255)**|The creation script for this article.|  
|**conflict_script**|**nvarchar(255)**|The conflict script for this article.|  
|**article_resolver**|**nvarchar(255)**|The custom row-level conflict resolver for this article.|  
|**ins_conflict_proc**|**sysname**|The procedure used to write conflict to **conflict_table**.|  
|**insert_proc**|**sysname**|The procedure used by the default conflict resolver to insert rows during synchronization.|  
|**update_proc**|**sysname**|The procedure used by the default conflict resolver to update rows during synchronization.|  
|**select_proc**|**sysname**|The name of an automatically generated stored procedure that the Merge Agent uses to accomplish locking, and finding columns and rows for an article.|  
|**metadata_select_proc**|**sysname**|The name of the automatically generated stored procedure used to access metadata in the merge replication system tables.|  
|**delete_proc**|**sysname**|The procedure used by the default conflict resolver to delete rows during synchronization.|  
|**schema_option**|**binary(8)**|For the supported values of *schema_option*, see [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md).|  
|**destination_object**|**sysname**|The name of the table created at the Subscriber.|  
|**destination_owner**|**sysname**|The name of the owner of the destination object.|  
|**resolver_clsid**|**nvarchar(50)**|The ID of the custom conflict resolver.|  
|**subset_filterclause**|**nvarchar(1000)**|The filter clause for this article.|  
|**missing_col_count**|**int**|The number of missing columns.|  
|**missing_cols**|**varbinary(128)**|The bitmap of missing columns.|  
|**excluded_cols**|**varbinary(128)**|The bitmap of the columns excluded from the article when it is sent to the Subscriber.|  
|**excluded_col_count**|**int**|The number of columns excluded.|  
|**columns**|**varbinary(128)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**deleted_cols**|**varbinary(128)**|The bitmaps of columns that have been deleted from the source table.|  
|**resolver_info**|**nvarchar(255)**|The storage for additional information required by custom conflict resolvers.|  
|**view_sel_proc**|**nvarchar(290)**|The name of a stored procedure that the Merge Agent uses for doing the initial population of an article in a dynamically filtered publication, and for enumerating changed rows in any filtered publication.|  
|**gen_cur**|**int**|The generate number for local changes to the base table of an article.|  
|**vertical_partition**|**int**|Specifies whether column filtering is enabled on a table article. **0** indicates there is no vertical filtering and publishes all columns.|  
|**identity_support**|**int**|Specifies whether automatic identity range handling is enabled. **1** means that identity range handling is enabled, and **0** means that there is no identity range support.|  
|**before_image_objid**|**int**|The tracking table object ID. The tracking table contains certain key column values when a publication is created with *@keep_partition_changes* = **true**.|  
|**before_view_objid**|**int**|The object ID of a view table. The view is on a table that tracks whether a row belonged at a particular Subscriber before it was deleted or updated. Applies only when a publication is created with *@keep_partition_changes* = **true.**|  
|**verify_resolver_signature**|**int**|Specifies whether a digital signature is verified before using a resolver in merge replication:<br /><br /> **0** = Signature is not verified.<br /><br /> **1** = Signature is verified to see whether it is from a trusted source.|  
|**allow_interactive_resolver**|**bit**|Specifies whether the use of the Interactive Resolver on an article is enabled. **1** specifies that the Interactive Resolver is used on the article.|  
|**fast_multicol_updateproc**|**bit**|Specifies whether the Merge Agent has been enabled to apply changes to multiple columns in the same row in one UPDATE statement.<br /><br /> **0** = Issues a separate UPDATE for each column changed.<br /><br /> **1** = Issues an UPDATE statement which causes updates to occur to multiple columns in one statement.|  
|**check_permissions**|**int**|The bitmap of the table-level permissions that is verified when the Merge Agent applies changes to the Publisher. *check_permissions* can have one of these values:<br /><br /> **0x00 =** Permissions are not checked.<br /><br /> **0x10 =** Checks permissions at the Publisher before INSERTs made at a Subscriber can be uploaded.<br /><br /> **0x20 =** Checks permissions at the Publisher before UPDATEs made at a Subscriber can be uploaded.<br /><br /> **0x40 =** Checks permissions at the Publisher before DELETEs made at a Subscriber can be uploaded.|  
|**maxversion_at_cleanup**|**int**|The highest generation for which the metadata is cleaned up.|  
|**processing_order**|**int**|Indicates the processing order of articles in a merge publication; where a value of **0** indicated that the article is unordered, and articles are processed in order from lowest to highest value. If two articles have the same value, they are processed concurrently. For more information, see [Specify Merge Replication properties](../../relational-databases/replication/merge/specify-merge-replication-properties.md).|  
|**upload_options**|**tinyint**|Defines restrictions on updates made at a Subscriber with a client subscription, which can be one of the following values.<br /><br /> **0** = There are no restrictions on updates made at a Subscriber with a client subscription; all changes are uploaded to the Publisher.<br /><br /> **1** = Changes are allowed at a Subscriber with a client subscription, but they are not uploaded to the Publisher.<br /><br /> **2** = Changes are not allowed at a Subscriber with a client subscription.<br /><br /> For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).|  
|**published_in_tran_pub**|**bit**|Indicates that an article in a merge publication is also published in a transactional publication.<br /><br /> **0** = The article is not published in a transactional article.<br /><br /> **1** = The article is also published in a transactional article.|  
|**lightweight**|**bit**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**procname_postfix**|**nchar(32)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**well_partitioned_lightweight**|**bit**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**before_upd_view_objid**|**int**|To be added.|  
|**delete_tracking**|**bit**|Indicates whether deletes are replicated.<br /><br /> **0** = Deletes are not replicated<br /><br /> **1** = Deletes are replicated, which is the default behavior for merge replication.<br /><br /> When the value of *delete_tracking* is **0**, rows deleted at the Subscriber must be manually removed at the Publisher, and rows deleted at the Publisher must be manually removed at the Subscriber.<br /><br /> Note: A value of **0** results in non-convergence.|  
|**compensate_for_errors**|**bit**|Indicates whether compensating actions are taken when errors are encountered during synchronization.<br /><br /> **0** = Compensating actions are disabled.<br /><br /> **1** = Changes that cannot be applied at a Subscriber or Publisher always lead to compensating actions to undo these changes, which is the default behavior for merge replication.<br /><br /> Note: A value of **0** results in non-convergence.|  
|**pub_range**|**bigint**|The publisher identity range size.|  
|**range**|**bigint**|The size of the consecutive identity values that would be assigned to subscribers in an adjustment.|  
|**threshold**|**int**|The identity range threshold percentage.|  
|**stream_blob_columns**|**bit**|Specifies whether a data stream optimization is used when replicating binary large object columns. **1** means that the optimization is attempted.|  
|**preserve_rowguidcol**|**bit**|Indicates if replication uses an existing rowguid column. A value of **1** means that an existing ROWGUIDCOL column is used. **0** means that replication added the ROWGUIDCOL column.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md)   
 [sp_changemergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md)   
 [sp_helpmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md)  
  
  
