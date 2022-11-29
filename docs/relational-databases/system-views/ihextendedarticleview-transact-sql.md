---
description: "IHextendedArticleView (Transact-SQL)"
title: "IHextendedArticleView (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
f1_keywords: 
  - "IHextendedArticleView_TSQL"
  - "IHextendedArticleView"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IHextendedArticleView view"
ms.assetid: 19ef0a12-3214-4bb0-9c25-a665897e65a2
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# IHextendedArticleView (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHextendedArticleView** view exposes information on articles in a non-SQL Server publication. This view is stored in the **distribution** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_id**|**smallint**|The unique identifier for the Publisher.|  
|**publication_id**|**int**|The unique identifier for the publication.|  
|**article**|**sysname**|The name of the article|  
|**destination_object**|**sysname**|The name of the published object at the Subscriber.|  
|**source_owner**|**sysname**|The owner of the published object at the Publisher.|  
|**source_object**|**sysname**|The name of the published object at the Publisher.|  
|**description**|**nvarchar(255)**|The description of the article.|  
|**creation_script**|**nvarchar(255)**|The schema creation script for the article.|  
|**del_cmd**|**nvarchar(255)**|The command that is executed for a DELETE.|  
|**filter**|**int**|The identifier for the stored procedure used to define the horizontal partition.|  
|**filter_clause**|**ntext**|The WHERE clause used to horizontally filter the article.|  
|**ins_cmd**|**nvarchar(255)**|The command that is executed for an INSERT.|  
|**pre_creation_cmd**|**tinyint**|The Pre-creation command for DROP TABLE, DELETE TABLE, or TRUNCATE:<br /><br /> **0** = None.<br /><br /> **1** = DROP.<br /><br /> **2** = DELETE.<br /><br /> **3** = TRUNCATE.|  
|**status**|**tinyint**|The bitmask of the article options and status, which can be the bitwise logical OR result of one or more of these values:<br /><br /> **1** = Article is active.<br /><br /> **8** = Include the column name in INSERT statements.<br /><br /> **16** = Use parameterized statements.<br /><br /> **24** = Both include the column name in INSERT statements and use parameterized statements.<br /><br /> For example, an active article using parameterized statements would have a value of **17** in this column. A value of **0** means that the article is inactive and no additional properties are defined.|  
|**type**|**tinyint**|Type of article:<br /><br /> **1** = Log-based article.<br /><br /> **3** = Log-based article with manual filter.<br /><br /> **5** = Log-based article with manual view.<br /><br /> **7** = Log-based article with manual filter and manual view.|  
|**upd_cmd**|**nvarchar(255)**|The command that is executed for a UPDATE.|  
|**schema_option**|**binary**|Indicates what is to be scripted out. See [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) for a list of supported schema options.|  
|**dest_owner**|**sysname**|The owner of the published object at the destination database.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
