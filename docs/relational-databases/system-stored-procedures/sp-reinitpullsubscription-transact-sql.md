---
title: "sp_reinitpullsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_reinitpullsubscription_TSQL"
  - "sp_reinitpullsubscription"
helpviewer_keywords: 
  - "sp_reinitpullsubscription"
ms.assetid: 7d9abe49-ce92-47f3-82c9-aea749518c91
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_reinitpullsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Marks a transactional pull or anonymous subscription for reinitialization the next time the Distribution Agent runs. This stored procedure is executed at the Subscriber on the pull subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_reinitpullsubscription [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of all, which marks all subscriptions for reinitialization.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_reinitpullsubscription** is used in transactional replication.  
  
 **sp_reinitpullsubscription** is not supported for peer-to-peer transactional replication.  
  
 **sp_reinitpullsubscription** can be called from the Subscriber to reinitialize the subscription, during the next run of the Distribution Agent.  
  
 Subscriptions to publications created with a value of **false** for **@immediate_sync** cannot be reinitialized from the Subscriber.  
  
 You can reinitialize a pull subscription by either executing **sp_reinitpullsubscription** at the Subscriber or **sp_reinitsubscription** at the Publisher.  
  
## Example  
 [!code-sql[HowTo#sp_reinitpullsub](../../relational-databases/replication/codesnippet/tsql/sp-reinitpullsubscriptio_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_reinitpullsubscription**.  
  
## See Also  
 [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md)   
 [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
