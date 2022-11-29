---
description: "sp_helparticledts (Transact-SQL)"
title: "sp_helparticledts (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_helparticledts"
  - "sp_helparticledts_TSQL"
helpviewer_keywords: 
  - "sp_helparticledts"
ms.assetid: cd1aed60-e056-4ff3-86ee-62b19433d890
author: markingmyname
ms.author: maghan
---
# sp_helparticledts (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Used to get information on the correct custom task names to use when creating a transformation subscription using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helparticledts [ @publication = ] 'publication', [ @article = ] 'article'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of an article in the publication. *article* is **sysname**, with no default.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pre_script_ignore_error_task_name**|**sysname**|Task name for the programming task that occurs before the snapshot data is copied. Program execution should continue if a script error is encountered.|  
|**pre_script_task_name**|**sysname**|Task name for the programming task that occurs before the snapshot data is copied. Program execution halts on error.|  
|**transformation_task_name**|**sysname**|Task name for the programming task when using a Data Driven Query task.|  
|**post_script_ignore_error_task_name**|**sysname**|Task name for the programming task that occurs after the snapshot data is copied. Program execution should continue if a script error is encountered.|  
|**post_script_task_name**|**sysname**|Task name for the programming task that occurs after the snapshot data is copied. Program execution halts on error.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helparticledts** is used in snapshot replication and transactional replication.  
  
 There are naming conventions, required by the replication agents, which must be followed when naming tasks in a replication Data Transformation Services (DTS) program. For custom tasks, such as an Execute SQL task, the name is a concatenated string consisting of the article name, a prefix, and an optional part. When writing the code, if you are unsure what the task names should be, the result set gives the task names that should be used.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute **sp_helparticledts**.  
  
  
