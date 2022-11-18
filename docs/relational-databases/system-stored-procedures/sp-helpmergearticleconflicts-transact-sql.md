---
description: "sp_helpmergearticleconflicts (Transact-SQL)"
title: "sp_helpmergearticleconflicts (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_helpmergearticleconflicts"
  - "sp_helpmergearticleconflicts_TSQL"
helpviewer_keywords: 
  - "sp_helpmergearticleconflicts"
ms.assetid: 4678a2b9-9a5f-4193-a20d-2e11fc896c3a
author: markingmyname
ms.author: maghan
---
# sp_helpmergearticleconflicts (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the articles in the publication that have conflicts. This stored procedure is executed at the Publisher on the publication database, or at the Subscriber on the merge subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpmergearticleconflicts [ [ @publication = ] 'publication' ]  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @publisher_db = ] 'publsher_db' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the merge publication.*publication* is **sysname**, with a default of **%**, which returns all articles in the database that have conflicts.  
  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher.*publisher* is **sysname**, with a default of NULL.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publisher database.*publisher_db* is **sysname**, with a default of NULL.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article**|**sysname**|Name of the article.|  
|**source_owner**|**sysname**|Owner of the source object.|  
|**source_object**|**nvarchar(386)**|Name of the source object.|  
|**conflict_table**|**nvarchar(258)**|Name of the table storing the insert or update conflicts.|  
|**guidcolname**|**sysname**|Name of the RowGuidCol for the source object.|  
|**centralized_conflicts**|**int**|Whether conflict records are stored on the given Publisher.|  
  
 If the article has only delete conflicts and no **conflict_table** rows, the name of the **conflict_table** in the result set is NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpmergearticleconflicts** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute **sp_helpmergearticleconflicts**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
