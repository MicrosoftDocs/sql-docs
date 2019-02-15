---
title: "sysextendedarticlesview (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysextendedarticlesview_TSQL"
  - "sysextendedarticlesview"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysextendedarticlesview view"
ms.assetid: 8bdd22f7-c268-49b6-820c-3fe603feb128
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysextendedarticlesview (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **sysextendedarticlesview** view provides information about published articles. This view is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**int**|The identity column that provides a unique ID number for the article.|  
|**creation_script**|**nvarchar(255)**|The schema creation script for the article.|  
|**del_cmd**|**nvarchar(255)**|The command to execute upon DELETE; otherwise, construct from the log.|  
|**description**|**nvarchar(255)**|The descriptive entry for the article.|  
|**dest_table**|**nvarchar(128)**|The name of the destination table.|  
|**filter**|**int**|The object identifier of the stored procedure used for horizontal partitioning.|  
|**filter_clause**|**ntext**|The WHERE clause of the article, used for horizontal filtering.|  
|**ins_cmd**|**nvarchar(255)**|The command to execute upon INSERT.|  
|**name**|**nvarchar(128)**|The name associated with the article, unique within the publication.|  
|**objid**|**int**|The published table object ID.|  
|**pubid**|**int**|The ID of the publication to which the article belongs.|  
|**pre_creation_cmd**|**tinyint**|The pre-creation command for DROP TABLE, DELETE TABLE, or TRUNCATE:<br /><br /> **0** = None.<br /><br /> **1** = DROP.<br /><br /> **2** = DELETE.<br /><br /> **3** = TRUNCATE.|  
|**status**|**int**|The bitmask of the article options and status, which can be the bitwise logical OR result of one or more of these values:<br /><br /> **1** = Article is active.<br /><br /> **8** = Include the column name in INSERT statements.<br /><br /> **16** = Use parameterized statements.<br /><br /> **24** = Both include the column name in INSERT statements and use parameterized statements.<br /><br /> For example, an active article using parameterized statements would have a value of 17 in this column. A value of 0 means that the article is inactive and no additional properties are defined.|  
|**sync_objid**|**int**|The ID of the table or view that represents the article definition.|  
|**type**|**tinyint**|The type of article:<br /><br /> **1** = Log-based article.<br /><br /> **3** = Log-based article with manual filter.<br /><br /> **5** = Log-based article with manual view.<br /><br /> **7** = Log-based article with manual filter and manual view.|  
|**upd_cmd**|**nvarchar(255)**|The command to execute upon UPDATE; otherwise, construct from the log.|  
|**schema_option**|**binary**|Indicates what properties of the published object are scripted out in the snapshot. For a list of supported schema options, see [sp_addarticle](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).|  
|**dest_owner**|**nvarchar(128)**|The owner of the table at the destination database.|  
|**ins_scripting_proc**|**int**|The object identifier of the custom stored procedure or script that is executed when an INSERT statement is replicated.|  
|**del_scripting_proc**|**int**|The object identifier of the custom stored procedure or script that is executed when a DELETE statement is replicated.|  
|**upd_scripting_proc**|**int**|The object identifier of the custom stored procedure or script that is executed when an UPDATE statement is replicated.|  
|**custom_script**|**int**|The object identifier of the custom script or procedure that is executed on the completion of a DDL trigger.|  
|**fire_triggers_on_snapshot**|**int**|Indicates whether replicated triggers are executed when the snapshot is applied, which can be one of these values:<br /><br /> **0** = Triggers are not executed.<br /><br /> **1** = Triggers are executed.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)   
 [sp_helparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md)   
 [sysarticles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysarticles-transact-sql.md)  
  
  
