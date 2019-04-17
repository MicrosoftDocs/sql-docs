---
title: "sp_helparticlecolumns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helparticlecolumns"
  - "sp_helparticlecolumns_TSQL"
helpviewer_keywords: 
  - "sp_helparticlecolumns"
ms.assetid: 9ea55df3-2e99-4683-88ad-bde718288bc7
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helparticlecolumns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns all columns in the underlying table. This stored procedure is executed at the Publisher on the publication database. For Oracle Publishers, this stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helparticlecolumns [ @publication = ] 'publication'   
        , [ @article = ] 'article'  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication that contains the article. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article that has its columns returned. *article* is **sysname**, with no default.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be specified when the requested article is published by a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (columns that are not published) or **1** (columns that are published)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**column id**|**int**|Identifier for the column.|  
|**column**|**sysname**|Name of the column.|  
|**published**|**bit**|Whether column is published:<br /><br /> **0** = No<br /><br /> **1** = Yes|  
|**publisher type**|**sysname**|Data type of the column at the Publisher.|  
|**subscriber type**|**sysname**|Data type of the column at the Subscriber.|  
  
## Remarks  
 **sp_helparticlecolumns** is used in snapshot and transactional replication.  
  
 **sp_helparticlecolumns** is useful in checking a vertical partition.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the publication access list for the current publication can execute **sp_helparticlecolumns**.  
  
## See Also  
 [Define and Modify a Column Filter](../../relational-databases/replication/publish/define-and-modify-a-column-filter.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_articlecolumn &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)   
 [sp_droparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md)   
 [sp_droppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppublication-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
