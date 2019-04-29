---
title: "sp_dropmergepullsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dropmergepullsubscription"
  - "sp_dropmergepullsubscription_TSQL"
helpviewer_keywords: 
  - "sp_dropmergepullsubscription"
ms.assetid: 9301dd80-72f7-4adb-9b13-87e7f9114248
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dropmergepullsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops a merge pull subscription. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropmergepullsubscription [ @publication= ] 'publication'   
        , [ @publisher= ] 'publisher'   
        , [ @publisher_db= ] 'publisher_db'   
    [ , [ @reserved= ] 'reserved' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of NULL. This parameter is required. Specify a value of **all** to remove subscriptions to all publications  
  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher*is **sysname**, with a default of NULL. This parameter is required.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db*is **sysname**, with a default of NULL. This parameter is required.  
  
`[ @reserved = ] 'reserved'`
 Is reserved for future use. *reserved* is **bit**, with a default of **0**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropmergepullsubscription** is used in merge replication.  
  
 **sp_dropmergepullsubscription** drops the Merge Agent for this merge pull subscription, although the Merge Agent is not created in **sp_addmergepullsubscription**.  
  
## Example  
 [!code-sql[HowTo#sp_dropmergepullsubscription](../../relational-databases/replication/codesnippet/tsql/sp-dropmergepullsubscrip_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the user that created the merge pull subscription can execute **sp_dropmergepullsubscription**. The **db_owner** fixed database role is only able to execute **sp_dropmergepullsubscription** if the user that created the merge pull subscription belongs to this role.  
  
## See Also  
 [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md)   
 [sp_addmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md)   
 [sp_changemergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepullsubscription-transact-sql.md)   
 [sp_dropmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergesubscription-transact-sql.md)   
 [sp_helpmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepullsubscription-transact-sql.md)  
  
  
