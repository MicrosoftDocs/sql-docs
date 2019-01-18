---
title: "sp_helparticle (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helparticle_TSQL"
  - "sp_helparticle"
helpviewer_keywords: 
  - "sp_helparticle"
ms.assetid: 9c4a1a88-56f1-45a0-890c-941b8e0f0799
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helparticle (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays information about an article. This stored procedure is executed at the Publisher on the publication database. For Oracle Publishers, this stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helparticle [ @publication = ] 'publication'   
    [ , [ @article = ] 'article' ]  
    [ , [ @returnfilter = ] returnfilter ]  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @found = ] found OUTPUT ]  
```  
  
## Arguments  
 [ **@publication =**] **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@article=**] **'***article***'**  
 Is the name of an article in the publication. *article* is **sysname**, with a default of **%**. If *article* is not supplied, information on all articles for the specified publication is returned.  
  
 [ **@returnfilter=**] *returnfilter*  
 Specifies whether the filter clause should be returned. *returnfilter* is **bit**, with a default of **1**, which returns the filter clause.  
  
 [ **@publisher**= ] **'***publisher***'**  
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be specified when requesting information on an article published by a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
 [ **@found=** ] *found* OUTPUT  
 Internal use only.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article id**|**int**|ID of the article.|  
|**article name**|**sysname**|Name of the article.|  
|**base object**|**nvarchar(257)**|Name of the underlying table represented by the article or stored procedure.|  
|**destination object**|**sysname**|Name of the destination (subscription) table.|  
|**synchronization object**|**nvarchar(257)**|Name of the view that defines the published article.|  
|**type**|**smallint**|The type of article:<br /><br /> **1** = Log-based.<br /><br /> **3** = Log-based with manual filter.<br /><br /> **5** = Log-based with manual view.<br /><br /> **7** = Log-based with manual filter and manual view.<br /><br /> **8** = Stored procedure execution.<br /><br /> **24** = Serializable stored procedure execution.<br /><br /> **32** = Stored procedure (schema only).<br /><br /> **64** = View (schema only).<br /><br /> **96** = Aggregate function (schema only).<br /><br /> **128** = Function (schema only).<br /><br /> **257** = Log-based indexed view.<br /><br /> **259** = Log-based indexed view with manual filter.<br /><br /> **261** = Log-based indexed view with manual view.<br /><br /> **263** = Log-based indexed view with manual filter and manual view.<br /><br /> **320** = Indexed view (schema only).<br /><br />|  
|**status**|**tinyint**|Can be the [& (Bitwise AND)](../../t-sql/language-elements/bitwise-and-transact-sql.md) result of one or more or these article properties:<br /><br /> **0x00** = [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br /><br /> **0x01** = Article is active.<br /><br /> **0x08** = Include the column name in insert statements.<br /><br /> **0x16** = Use parameterized statements.<br /><br /> **0x32** = Use parameterized statements and include the column name in insert statements.|  
|**filter**|**nvarchar(257)**|Stored procedure used to horizontally filter the table. This stored procedure must have been created using FOR REPLICATION clause.|  
|**description**|**nvarchar(255)**|Descriptive entry for the article.|  
|**insert_command**|**nvarchar(255)**|The replication command type used when replicating inserts with table articles. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).|  
|**update_command**|**nvarchar(255)**|The replication command type used when replicating updates with table articles. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).|  
|**delete_command**|**nvarchar(255)**|The replication command type used when replicating deletes with table articles. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).|  
|**creation script path**|**nvarchar(255)**|Path and name of an article schema script used to create target tables.|  
|**vertical partition**|**bit**|Is whether vertical partitioning is enabled for the article; where a value of **1** means that vertical partitioning is enabled.|  
|**pre_creation_cmd**|**tinyint**|Precreation command for DROP TABLE, DELETE TABLE, or TRUNCATE TABLE.|  
|**filter_clause**|**ntext**|WHERE clause specifying the horizontal filtering.|  
|**schema_option**|**binary(8)**|Bitmap of the schema generation option for the given article. For a complete list of **schema_option** values, see [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).|  
|**dest_owner**|**sysname**|Name of the owner of the destination object.|  
|**source_owner**|**sysname**|Owner of the source object.|  
|**unqua_source_object**|**sysname**|Name of the source object, without the owner name.|  
|**sync_object_owner**|**sysname**|Owner of the view that defines the published article. .|  
|**unqualified_sync_object**|**sysname**|Name of the view that defines the published article, without the owner name.|  
|**filter_owner**|**sysname**|Owner of the filter.|  
|**unqua_filter**|**sysname**|Name of the filter, without the owner name.|  
|**auto_identity_range**|**int**|Flag indicating if automatic identity range handling was turned on at the publication at the time it was created. **1** means that automatic identity range is enabled; **0** means it is disabled.|  
|**publisher_identity_range**|**int**|Range size of the identity range at the Publisher if the article has *identityrangemanagementoption* set to **auto** or **auto_identity_range** set to **true**.|  
|**identity_range**|**bigint**|Range size of the identity range at the Subscriber if the article has *identityrangemanagementoption* set to **auto** or **auto_identity_range** set to **true**.|  
|**threshold**|**bigint**|Percentage value indicating when the Distribution Agent assigns a new identity range.|  
|**identityrangemanagementoption**|**int**|Indicates the identity range management handled for the article.|  
|**fire_triggers_on_snapshot**|**bit**|Is if replicated user triggers are executed when the initial snapshot is applied.<br /><br /> **1** = user triggers are executed.<br /><br /> **0** = user triggers are not executed.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helparticle** is used in snapshot replication and transactional replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the publication access list for the current publication can execute **sp_helparticle**.  
  
## Example  
 [!code-sql[HowTo#sp_helptranarticle](../../relational-databases/replication/codesnippet/tsql/sp-helparticle-transact-_1.sql)]  
  
## See Also  
 [View and Modify Article Properties](../../relational-databases/replication/publish/view-and-modify-article-properties.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_articlecolumn &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)   
 [sp_droparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
