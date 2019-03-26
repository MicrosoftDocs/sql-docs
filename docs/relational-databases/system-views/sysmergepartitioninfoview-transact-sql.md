---
title: "sysmergepartitioninfoview (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysmergepartitioninfoview"
  - "sysmergepartitioninfoview_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmergepartitioninfoview view"
ms.assetid: 714e2935-1bc7-4901-aea2-64b1bbda03d6
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmergepartitioninfoview (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **sysmergepartitioninfoview** view exposes partitioning information for table articles. This view is stored in the publication database at the Publisher and subscription database at the Subscriber.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|The name of the article.|  
|**type**|**tinyint**|Indicates the article type, which can be one of the following:<br /><br /> **0x0a** = Table.<br /><br /> **0x20** = Procedure schema only.<br /><br /> **0x40** = View schema only or indexed view schema only.<br /><br /> **0x80** = Function schema only.|  
|**objid**|**int**|The identifier for the published object.|  
|**sync_objid**|**int**|The object ID of the view representing the synchronized data set.|  
|**view_type**|**tinyint**|The type of view:<br /><br /> **0** = Not a view; use all of base object.<br /><br /> **1** = Permanent view.<br /><br /> **2** = Temporary view.|  
|**artid**|**uniqueidentifier**|The unique identification number for the given article.|  
|**description**|**nvarchar(255)**|The brief description of the article.|  
|**pre_creation_command**|**tinyint**|The default action to take when the article is created in the subscription database:<br /><br /> **0** = None - if the table already exists at the Subscriber, no action is taken.<br /><br /> **1** = Drop - drops the table before recreating it.<br /><br /> **2** = Delete - issues a delete based on the WHERE clause in the subset filter.<br /><br /> **3** = Truncate - same as 2, but deletes pages instead of rows. However, does not take a WHERE clause.|  
|**pubid**|**uniqueidentifier**|The ID of the publication to which the current article belongs.|  
|**nickname**|**int**|The nickname mapping for article identification.|  
|**column_tracking**|**int**|The indicates whether column tracking is implemented for the article.|  
|**status**|**tinyint**|Indicates the status of the article, which can be one of the following:<br /><br /> **1** = Unsynced - the initial processing script to publish the table will run the next time the Snapshot Agent runs.<br /><br /> **2** = Active - the initial processing script to publish the table has been run.|  
|**conflict_table**|**sysname**|The name of the local table that contains the conflicting records for the current article. This table is supplied for information only, and its contents may be modified or deleted by custom conflict resolution routines or directly by the administrator.|  
|**creation_script**|**nvarchar(255)**|The creation script for this article.|  
|**conflict_script**|**nvarchar(255)**|The conflict script for this article.|  
|**article_resolver**|**nvarchar(255)**|The conflict resolver for this article.|  
|**ins_conflict_proc**|**sysname**|The procedure used to write conflict information to the conflict table.|  
|**insert_proc**|**sysname**|The procedure used to insert rows during synchronization.|  
|**update_proc**|**sysname**|The procedure used to update rows during synchronization.|  
|**select_proc**|**sysname**|The name of an automatically-generated stored procedure that the Merge Agent uses to accomplish locking, and finding columns and rows for an article.|  
|**metadata_select_proc**|**sysname**|The name of the automatically-generated stored procedure used to access metadata in the merge replication system tables.|  
|**delete_proc**|**sysname**|The procedure used to delete rows during synchronization.|  
|**schema_option**|**binary(8)**|The bitmap of the schema generation option for the given article. For information on supported *schema_option* values, see [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md).|  
|**destination_object**|**sysname**|The name of the table created at the Subscriber.|  
|**destination_owner**|**sysname**|The name of the owner of the destination object.|  
|**resolver_clsid**|**nvarchar(50)**|The ID of the custom conflict resolver. For a business logic handler, this value is NULL.|  
|**subset_filterclause**|**nvarchar(1000)**|The filter clause for this article.|  
|**missing_col_count**|**int**|The number of published columns missing from the article.|  
|**missing_cols**|**varbinary(128)**|The bitmap that describes the columns missing from the article.|  
|**excluded_cols**|**varbinary(128)**|The bitmap of the columns excluded from the article.|  
|**excluded_col_count**|**int**|The number of columns excluded from the article.|  
|**columns**|**varbinary(128)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**deleted_cols**|**varbinary(128)**|The bitmap that describes the columns deleted from the article.|  
|**resolver_info**|**nvarchar(255)**|The storage for additional information required by custom conflict resolvers.|  
|**view_sel_proc**|**nvarchar(290)**|The name of a stored procedure that the Merge Agent uses for doing the initial population of an article in a dynamically-filtered publication, and for enumerating changed rows in any filtered publication.|  
|**gen_cur**|**bigint**|Generates number for local changes to the base table of an article.|  
|**vertical_partition**|**int**|Specifies whether column filtering is enabled on a table article. **0** indicates there is no vertical filtering and publishes all columns.|  
|**identity_support**|**int**|Specifies whether automatic identity range handling is enabled. **1** means that identity range handling is enabled, and **0** means that there is no identity range support.|  
|**before_image_objid**|**int**|The tracking table object ID. The tracking table contains certain key column values when partition change optimizations have been enabled for the publication.|  
|**before_view_objid**|**int**|The object ID of a view table. The view is on a table that tracks whether a row belonged at a particular Subscriber before it was deleted or updated. Applies only when partition change optimizations have been enabled for the publication.|  
|**verify_resolver_signature**|**int**|Specifies whether a digital signature is verified before using a resolver in merge replication:<br /><br /> **0** = Signature is not verified.<br /><br /> **1** = Signature is verified to see whether it is from a trusted source.|  
|**allow_interactive_resolver**|**bit**|Specifies whether the use of the Interactive Resolver on an article is enabled. **1** means that the Interactive Resolver can be used on the article.|  
|**fast_multicol_updateproc**|**bit**|Specifies whether the Merge Agent has been enabled to apply changes to multiple columns in the same row in one UPDATE statement.<br /><br /> **0** = Issues a separate UPDATE for each column changed.<br /><br /> **1** = Issued on UPDATE statement which causes updates to occur to multiple columns in one statement.|  
|**check_permissions**|**int**|The bitmap of the table-level permissions that will be verified when the Merge Agent applies changes to the Publisher. *check_permissions* can have one of these values:<br /><br /> **0x00** = Permissions are not checked.<br /><br /> **0x10** = Checks permissions at the Publisher before INSERTs are made at a Subscriber can be uploaded.<br /><br /> **0x20** = Checks permissions at the Publisher before UPDATEs made at a Subscriber can be uploaded.<br /><br /> **0x40** = Checks permissions at the Publisher before DELETEs made at a Subscriber can be uploaded.|  
|**maxversion_at_cleanup**|**int**|The maximum generation that is cleaned up the next time the Merge Agent runs.|  
|**processing_order**|**int**|Indicates the processing order of articles in a merge publication; where a value of **0** indicates that the article is unordered, and articles are processed in order from lowest to highest value. If two articles have the same value, they are processed concurrently. For more information, see [Specify Merge Replication properties](../../relational-databases/replication/merge/specify-merge-replication-properties.md).|  
|**upload_options**|**tinyint**|Defines whether changes can be made at or uploaded from the Subscriber, which can be one of the following values.<br /><br /> **0** = There are no restrictions on updates made at the Subscriber; all changes are uploaded to the Publisher.<br /><br /> **1** = Changes are allowed at the Subscriber, but they are not uploaded to the Publisher.<br /><br /> **2** = Changes are not allowed at the Subscriber.|  
|**published_in_tran_pub**|**bit**|Indicates that an article in a merge publication is also published in a transactional publication.<br /><br /> **0** = The article is not published in a transactional article.<br /><br /> **1** = The article is also published in a transactional article.|  
|**lightweight**|**bit**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**procname_postfix**|**nchar(32)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**well_partitioned_lightweight**|**bit**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**before_upd_view_objid**|**int**|The ID of the view of the table before updates.|  
|**delete_tracking**|**bit**|Indicates if deletes are replicated.<br /><br /> **0** = Deletes are not replicated.<br /><br /> **1** = Deletes are replicated, which is the default behavior for merge replication.<br /><br /> When the value of *delete_tracking* is **0**, rows deleted at the Subscriber must be manually removed at the Publisher, and rows deleted at the Publisher must be manually removed at the Subscriber.<br /><br /> Note: A value of **0** results in non-convergence.|  
|**compensate_for_errors**|**bit**|Indicates if compensating actions are taken when errors are encountered during synchronization.<br /><br /> **0** = Compensating actions are disabled.<br /><br /> **1** = Changes that cannot be applied at a Subscriber or Publisher always lead to compensating actions to undo these changes, which is the default behavior for merge replication.<br /><br /> Note: A value of **0** results in non-convergence.|  
|**pub_range**|**bigint**|The publisher identity range size.|  
|**range**|**bigint**|The size of the consecutive identity values that would be assigned to subscribers in an adjustment.|  
|**threshold**|**int**|The identity range threshold percentage.|  
|**stream_blob_columns**|**bit**|Indicates whether the streaming optimization for binary large object columns is used. **1** means that the optimization is attempted.|  
|**preserve_rowguidcol**|**bit**|Indicates whether replication uses an existing rowguid column. A value of **1** means that an existing ROWGUIDCOL column is used. **0** means that replication added the ROWGUIDCOL column.|  
|**partition_view_id**|**int**|Identifies the view that defines a subscriber partition.|  
|**repl_view_id**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**partition_deleted_view_rule**|**sysname**|The statement used inside a merge replication trigger to retrieve the partition ID for each deleted or updated row based on its old column values.|  
|**partition_inserted_view_rule**|**Sysname**|The statement used inside a merge replication trigger to retrieve the partition ID for each inserted or updated based on its new column values.|  
|**membership_eval_proc_name**|**sysname**|The name of the procedure that evaluates the current partition IDs of rows in [MSmerge_contents &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-contents-transact-sql.md).|  
|**column_list**|**sysname**|A comma-separated list of columns published in an article.|  
|**column_list_blob**|**sysname**|A comma-separated list of columns published in an article, including binary large object columns.|  
|**expand_proc**|**sysname**|The name of the procedure that reevaluates partition IDs for all child rows of a newly inserted parent row and for parent rows that have undergone a partition change or have been deleted.|  
|**logical_record_parent_nickname**|**int**|The nickname of the top-level parent of a given article in a logical record.|  
|**logical_record_view**|**int**|A view that outputs the top-level parent article rowguid corresponding to each child rowguid.|  
|**logical_record_deleted_view_rule**|**sysname**|Similar to **logical_record_view**, except that it shows child rows in the "deleted" table in update and delete triggers.|  
|**logical_record_level_conflict_detection**|**bit**|Indicates whether conflicts should be detected at the logical record level or at the row or column level.<br /><br /> **0** = Row- or column-level conflict detection is used.<br /><br /> **1** = Logical record conflict detection is used, where a change in a row at the Publisher and change in a separate row the same logical record at the Subscriber is handled as a conflict.<br /><br /> When this value is 1, only logical record-level conflict resolution can be used.|  
|**logical_record_level_conflict_resolution**|**bit**|Indicates whether conflicts should be resolved at the logical record-level or at the row- or column-level.<br /><br /> **0** = Row- or column-level resolution is used.<br /><br /> **1** = In case of a conflict, the entire logical record from the winner overwrites the entire logical record on the losing side.<br /><br /> A value of 1 can be used with both logical record-level detection and with row- or column-level detection.|  
|**partition_options**|**tinyint**|Defines the way in which data in the article is partitioned, which enables performance optimizations when all rows belong in only one partition or in only one subscription. The *partition_options* can be one of the following values.<br /><br /> **0** = The filtering for the article either is static or does not yield a unique subset of data for each partition, that is, an "overlapping" partition.<br /><br /> **1** = The partitions are overlapping, and DML updates made at the Subscriber cannot change the partition to which a row belongs.<br /><br /> **2** = The filtering for the article yields non-overlapping partitions, but multiple Subscribers can receive the same partition.<br /><br /> **3** = The filtering for the article yields non-overlapping partitions that are unique for each subscription.|  
|**name**|**sysname**|The name of a partition.|  
  
## See Also  
 [Manage Partitions for a Merge Publication with Parameterized Filters](../../relational-databases/replication/publish/manage-partitions-for-a-merge-publication-with-parameterized-filters.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_addmergepartition &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepartition-transact-sql.md)   
 [sp_helpmergepartition &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepartition-transact-sql.md)  
  
  
