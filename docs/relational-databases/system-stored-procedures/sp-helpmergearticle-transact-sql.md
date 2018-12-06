---
title: "sp_helpmergearticle (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpmergearticle"
  - "sp_helpmergearticle_TSQL"
helpviewer_keywords: 
  - "sp_helpmergearticle"
ms.assetid: 0fb9986a-3c33-46ef-87bb-297396ea5a6a
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpmergearticle (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about an article. This stored procedure is executed at the Publisher on the publication database or at a republishing Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpmergearticle [ [ @publication = ] 'publication' ]  
    [ , [ @article= ] 'article' ]  
```  
  
## Arguments  
 [ **@publication=**] **'***publication***'**  
 Is the name of the publication about which to retrieve information. *publication*is **sysname**, with a default of **%**, which returns information about all merge articles contained in all publications in the current database.  
  
 [ **@article=**] **'***article***'**  
 Is the name of the article for which to return information. *article*is **sysname**, with a default of **%**, which returns information about all merge articles in the given publication.  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Article identifier.|  
|**name**|**sysname**|Name of the article.|  
|**source_owner**|**sysname**|Name of the owner of the source object.|  
|**source_object**|**sysname**|Name of the source object from which to add the article.|  
|**sync_object_owner**|**sysname**|Name of the owner of the view that defines the published article.|  
|**sync_object**|**sysname**|Name of the custom object used to establish the initial data for the partition.|  
|**description**|**nvarchar(255)**|Description of the article.|  
|**status**|**tinyint**|Status of the article, which can be one of the following:<br /><br /> **1** = inactive<br /><br /> **2** = active<br /><br /> **5** = data definition language (DDL) operation pending<br /><br /> **6** = DDL operation with a newly generated snapshot<br /><br /> Note: When an article is reinitialized, values of **5** and **6** are changed to **2**.|  
|**creation_script**|**nvarchar(255)**|Path and name of an optional article schema script used to create the article in the subscription database.|  
|**conflict_table**|**nvarchar(270)**|Name of the table storing the insert or update conflicts.|  
|**article_resolver**|**nvarchar(255)**|Custom resolver for the article.|  
|**subset_filterclause**|**nvarchar(1000)**|WHERE clause specifying the horizontal filtering.|  
|**pre_creation_command**|**tinyint**|Pre-creation method, which can be one of the following:<br /><br /> **0** = none<br /><br /> **1** = drop<br /><br /> **2** = delete<br /><br /> **3** = truncate|  
|**schema_option**|**binary(8)**|Bitmap of the schema generation option for the article. For information about this bitmap option, see [sp_addmergearticle](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) or [sp_changemergearticle](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md).|  
|**type**|**smallint**|Type of article, which can be one of the following:<br /><br /> **10** = table<br /><br /> **32** = stored procedure<br /><br /> **64** = view or indexed view<br /><br /> **128** = user defined function<br /><br /> **160** = synonym schema only|  
|**column_tracking**|**int**|Setting for column-level tracking; where **1** means that column-level tracking is on, and **0** means that column-level tracking is off.|  
|**resolver_info**|**nvarchar(255)**|Name of the article resolver.|  
|**vertical_partition**|**bit**|If the article is vertically partitioned; where **1** means that the article is vertically partitioned, and **0** means that it is not.|  
|**destination_owner**|**sysname**|Owner of the destination object. Applicable to merge stored procedures, views, and user-defined function (UDF) schema articles only.|  
|**identity_support**|**int**|If automatic identity range handling is enabled; where **1** is enabled and **0** is disabled.|  
|**pub_identity_range**|**bigint**|The range size to use when assigning new identity values. For more information, see the "Merge Replication" section of [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).|  
|**identity_range**|**bigint**|The range size to use when assigning new identity values. For more information, see the "Merge Replication" section of [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).|  
|**threshold**|**int**|Percentage value used for Subscribers running [!INCLUDE[ssEW](../../includes/ssew-md.md)] or previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. **threshold** controls when the Merge Agent assigns a new identity range. When the percentage of values specified in threshold is used, the Merge Agent creates a new identity range. For more information, see the "Merge Replication" section of [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).|  
|**verify_resolver_signature**|**int**|If a digital signature is verified before using a resolver in merge replication; where **0** means that the signature is not verified, and **1** means that the signature is verified to see if it is from a trusted source.|  
|**destination_object**|**sysname**|Name of the destination object. Applicable to merge stored procedures, views, and UDF schema articles only.|  
|**allow_interactive_resolver**|**int**|If the Interactive Resolver is used on an article; where **1** means that this resolver is used, and **0** means that it is not used.|  
|**fast_multicol_updateproc**|**int**|Enables or disables the Merge Agent to apply changes to multiple columns in the same row in one UPDATE statement; where **1** means that multiple columns are updated in one statement, and **0** means that separate UPDATE statements are issues for each updated column.|  
|**check_permissions**|**int**|Integer value that represents the bitmap of the table-level permissions that are verified. For a list of possible values, see [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md).|  
|**processing_order**|**int**|The order in which data changes are applied to articles in a publication.|  
|**upload_options**|**tinyint**|Defines restrictions on updates made at a Subscriber with a client subscription, which can be one of the following values.<br /><br /> **0** = There are no restrictions on updates made at a Subscriber with a client subscription; all changes are uploaded to the Publisher.<br /><br /> **1** = Changes are allowed at a Subscriber with a client subscription, but they are not uploaded to the Publisher.<br /><br /> **2** = Changes are not allowed at a Subscriber with a client subscription.<br /><br /> For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).|  
|**identityrangemanagementoption**|**int**|If automatic identity range handling is enabled; where **1** is enabled and **0** is disabled.|  
|**delete_tracking**|**bit**|If deletes are replicated; where **1** means that deletes are replicated, and **0** means that they are not.|  
|**compensate_for_errors**|**bit**|Indicates if compensating actions are taken when errors are encountered during synchronization; where **1** indicates that compensating actions are taken, and **0** means that compensating actions are not taken.|  
|**partition_options**|**tinyint**|Defines the way in which data in the article is partitioned, which enables performance optimizations when all rows belong in only one partition or in only one subscription. *partition_options* can be one of the following values.<br /><br /> **0** = The filtering for the article either is static or does not yield a unique subset of data for each partition; that is, it is an "overlapping" partition.<br /><br /> **1** = The partitions are overlapping, and data manipulation language (DML) updates made at the Subscriber cannot change the partition to which a row belongs.<br /><br /> **2** = The filtering for the article yields non-overlapping partitions, but multiple Subscribers can receive the same partition.<br /><br /> **3** = The filtering for the article yields non-overlapping partitions that are unique for each subscription.|  
|**artid**|**uniqueidentifier**|An identifier that uniquely identifies the article.|  
|**pubid**|**uniqueidentifier**|An identifier that uniquely identifies the publication in which the article is published.|  
|**stream_blob_columns**|**bit**|Is if the data stream optimization is being used when replicating binary large object columns. **1** means that the optimization is being used, and **0** means that the optimization is not being used.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpmergearticle** is used in merge replication.  
  
## Permissions  
 Only members of the **db_owner** fixed database role in the publication database, the **replmonitor** role in the distribution database, or the publication access list for a publication can execute **sp_helpmergearticle**.  
  
## Example  
 [!code-sql[HowTo#sp_helpmergearticle](../../relational-databases/replication/codesnippet/tsql/sp-helpmergearticle-tran_1.sql)]  
  
## See Also  
 [View and Modify Article Properties](../../relational-databases/replication/publish/view-and-modify-article-properties.md)   
 [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md)   
 [sp_changemergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md)   
 [sp_dropmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergearticle-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
