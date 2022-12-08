---
description: "sp_addqueued_artinfo (Transact-SQL)"
title: "sp_addqueued_artinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_addqueued_artinfo"
  - "sp_addqueued_artinfo_TSQL"
helpviewer_keywords: 
  - "sp_addqueued_artinfo"
ms.assetid: decdb6eb-3dcd-4053-a21d-fd367c3fbafb
author: markingmyname
ms.author: maghan
---
# sp_addqueued_artinfo (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  
  
> [!IMPORTANT]  
>  The [sp_script_synctran_commands](../../relational-databases/system-stored-procedures/sp-script-synctran-commands-transact-sql.md) procedure should be used instead of **sp_addqueued_artinfo**. [sp_script_synctran_commands](../../relational-databases/system-stored-procedures/sp-script-synctran-commands-transact-sql.md) generates a script that contains the **sp_addqueued_artinfo** and **sp_addsynctrigger** calls.  
  
 Creates the [MSsubscription_articles](../../relational-databases/system-tables/mssubscription-articles-transact-sql.md) table at the Subscriber that is used to track article subscription information (Queued Updating and Immediate Updating with Queued Updating as Failover). This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addqueued_artinfo [ @artid= ] 'artid'  
        , [ @article= ] 'article'  
        , [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'  
        , [ @dest_table= ] 'dest_table'  
        , [ @owner = ] 'owner'  
        , [ @cft_table= ] 'cft_table'  
```  
  
## Arguments  
`[ @artid = ] 'artid'`
 Is the name of the article ID. *artid* is **int**, with no default  
  
`[ @article = ] 'article'`
 Is the name of the article to be scripted. *article* is **sysname**, with no default  
  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher server. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication to be scripted. *publication* is **sysname**, with no default.  
  
`[ @dest_table = ] _'dest_table'`
 Is the name of the destination table. *dest_table* is **sysname**, with no default.  
  
 [**@owner =** ] **'**_owner_**'**  
 Is the owner of the subscription. *owner* is **sysname**, with no default.  
  
`[ @cft_table = ] 'cft_table'`
 Name of the queued updating conflict table for this article. *cft_table*is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addqueued_artinfo** is used by the Distribution Agent as part of subscription initialization. This stored procedure is not commonly run by users, but may be useful if the user needs to manually set up a subscription.  
  
 [sp_script_synctran_commands](../../relational-databases/system-stored-procedures/sp-script-synctran-commands-transact-sql.md) instead of **sp_addqueued_artinfo**.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addqueued_artinfo**.  
  
## See Also  
 [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)   
 [sp_script_synctran_commands &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-script-synctran-commands-transact-sql.md)   
 [MSsubscription_articles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mssubscription-articles-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
