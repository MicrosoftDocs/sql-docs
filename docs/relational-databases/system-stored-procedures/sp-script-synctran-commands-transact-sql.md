---
description: "sp_script_synctran_commands (Transact-SQL)"
title: "sp_script_synctran_commands (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_script_synctran_commands"
  - "sp_script_synctran_commands_TSQL"
helpviewer_keywords: 
  - "sp_script_synctran_commands"
ms.assetid: f132694a-dd05-405b-9d84-21acce9e564a
author: markingmyname
ms.author: maghan
---
# sp_script_synctran_commands (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Generates a script that contains the **sp_addsynctrigger** calls to be applied at Subscribers for updatable subscriptions. There is one **sp_addsynctrigger** call for each article in the publication. The generated script also contains the **sp_addqueued_artinfo** calls that create the **MSsubsciption_articles** table that is needed to process queued publications. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_script_synctran_commands [@publication = ] 'publication'  
    [ , [@article = ] 'article']  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to be scripted. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article to be scripted. *article* is **sysname**, with a default of **all**, which specifies all articles are scripted.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Results Set  
 **sp_script_synctran_commands** returns a result set that consists of a single **nvarchar(4000)** column. The result set forms the complete scripts necessary to create both the **sp_addsynctrigger** and **sp_addqueued_artinfo** calls to be applied at Subscribers.  
  
## Remarks  
 **sp_script_synctran_commands** is used in snapshot and transactional replication.  
  
 **sp_addqueued_artinfo** is used for queued updatable subscriptions.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_script_synctran_commands**.  
  
## See Also  
 [sp_addsynctriggers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsynctriggers-transact-sql.md)   
 [sp_addqueued_artinfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addqueued-artinfo-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
