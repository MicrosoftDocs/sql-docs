---
title: "sp_helpmergearticlecolumn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpmergearticlecolumn"
  - "sp_helpmergearticlecolumn_TSQL"
helpviewer_keywords: 
  - "sp_helpmergearticlecolumn"
ms.assetid: 651c017b-9e9a-48f2-a0bd-6fc896eab334
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpmergearticlecolumn (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the list of columns in the specified table or view article for a merge publication. Because stored procedures do not have columns, this stored procedure returns an error if a stored procedure is specified as the article. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpmergearticlecolumn [ @publication = ] 'publication' ]  
        , [ @article= ] 'article' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication.*publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of a table or view that is the article to retrieve information on.*article* is **sysname**, with no default.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**column_id**|**sysname**|Identifies the column.|  
|**column_name**|**sysname**|Is the name of the column for a table or view.|  
|**published**|**bit**|Specifies if the column name is published.<br /><br /> **1** specifies that the column is being published.<br /><br /> **0** specifies that it is not published.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpmergearticlecolumn** is used in merge replication.  
  
## Permissions  
 Only members of the **replmonitor** fixed database role in the distribution database or the publication access list for the publication can execute **sp_helpmergearticlecolumn**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
