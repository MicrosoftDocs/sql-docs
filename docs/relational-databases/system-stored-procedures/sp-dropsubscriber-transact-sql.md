---
description: "sp_dropsubscriber (Transact-SQL)"
title: "sp_dropsubscriber (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_dropsubscriber_TSQL"
  - "sp_dropsubscriber"
helpviewer_keywords: 
  - "sp_dropsubscriber"
ms.assetid: 8c6eb282-81b5-4ec4-b691-aa061d9267dc
author: markingmyname
ms.author: maghan
---
# sp_dropsubscriber (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Removes the Subscriber designation from a registered server. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  This stored procedure has been deprecated. You are no longer required to explicitly register a Subscriber at the Publisher.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropsubscriber [ @subscriber= ] 'subscriber'   
    [ , [ @reserved= ] 'reserved' ]   
    [ , [ @ignore_distributor = ] ignore_distributor ]  
```  
  
## Arguments  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber to be dropped. *subscriber* is **sysname**, with no default.  
  
`[ @reserved = ] 'reserved'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
`[ @ignore_distributor = ] ignore_distributor`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropsubscriber** is used in all types of replication.  
  
 This stored procedure removes the server **sub** option and removes the remote login mapping of system administrator to **repl_subscriber**.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_dropsubscriber**.  
  
## See Also  
 [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md)   
 [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md)   
 [sp_addsubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscriber-transact-sql.md)   
 [sp_changesubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubscriber-transact-sql.md)   
 [sp_helpdistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributor-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [sp_helpsubscriberinfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
