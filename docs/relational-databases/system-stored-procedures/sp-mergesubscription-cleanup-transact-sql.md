---
description: "sp_mergesubscription_cleanup (Transact-SQL)"
title: "sp_mergesubscription_cleanup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_mergesubscription_cleanup"
  - "sp_mergesubscription_cleanup_TSQL"
helpviewer_keywords: 
  - "sp_mergesubscription_cleanup"
ms.assetid: bfad414f-2bda-4bf5-9507-56a1e743dfc4
author: markingmyname
ms.author: maghan
---
# sp_mergesubscription_cleanup (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes metadata, such as triggers and entries, in **sysmergesubscriptions** and **sysmergearticles** after the specified merge push subscription is removed at the Publisher. This stored procedure is run at the Subscriber on the subscription database.  
  
> [!NOTE]  
>  For a pull subscription, metadata is removed when [sp_dropmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergepullsubscription-transact-sql.md) is executed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_mergesubscription_cleanup [ @publisher =] 'publisher'  
        , [ @publisher_db =] 'publisher_db'  
        , [ @publication =] 'publication'  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_mergesubscription_cleanup** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_mergesubscription_cleanup**.  
  
## See Also  
 [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md)   
 [sp_expired_subscription_cleanup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-expired-subscription-cleanup-transact-sql.md)   
 [sp_subscription_cleanup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-subscription-cleanup-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
