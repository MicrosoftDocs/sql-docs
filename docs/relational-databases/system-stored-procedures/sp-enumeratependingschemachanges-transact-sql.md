---
description: "sp_enumeratependingschemachanges (Transact-SQL)"
title: "sp_enumeratependingschemachanges (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_enumeratependingschemachanges"
  - "sp_enumeratependingschemachanges_TSQL"
helpviewer_keywords: 
  - "sp_enumeratependingschemachanges"
ms.assetid: df169b21-d10a-41df-b3a1-654cfb58bc21
author: markingmyname
ms.author: maghan
---
# sp_enumeratependingschemachanges (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a list of all pending schema changes. This stored procedure can be used with [sp_markpendingschemachange](../../relational-databases/system-stored-procedures/sp-markpendingschemachange-transact-sql.md), which enables an administrator to skip selected pending schema changes so that they are not replicated. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_enumeratependingschemachanges [ @publication = ] 'publication'   
    [ , [ @starting_schemaversion = ] starting_schemaversion ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @starting_schemaversion = ] starting_schemaversion`
 Is the lowest number schema change to include in the result set.  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article_name**|**sysname**|Name of the article to which the schema change applies, or **Publication-wide** for schema changes that apply to the entire publication.|  
|**schemaversion**|**int**|Number of the pending schema change.|  
|**schematype**|**sysname**|A text value that represents the type of schema change.|  
|**schematext**|**nvarchar(max)**|[!INCLUDE[tsql](../../includes/tsql-md.md)] that describes the schema change.|  
|**schemastatus**|**nvarchar(10)**|Indicates if a schema change is pending for the article, which can be one of the following values:<br /><br /> **active** = schema change is pending<br /><br /> **inactive** = schema change is inactive<br /><br /> **skip** = schema change is not replicated|  
|**schemaguid**|**uniqueidentifier**|Identifies the schema change.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_enumeratependingschemachanges** is used in merge replication.  
  
 **sp_enumeratependingschemachanges**, used with [sp_markpendingschemachange](../../relational-databases/system-stored-procedures/sp-markpendingschemachange-transact-sql.md), is intended for the supportability of merge replication and should be used only when other corrective actions, such as reinitialization, have failed to correct the situation.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_enumeratependingschemachanges**.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)   
 [sysmergeschemachange &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysmergeschemachange-transact-sql.md)  
  
  
