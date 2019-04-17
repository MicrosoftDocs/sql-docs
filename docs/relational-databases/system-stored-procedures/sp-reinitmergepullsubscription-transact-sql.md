---
title: "sp_reinitmergepullsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_reinitmergepullsubscription"
  - "sp_reinitmergepullsubscription_TSQL"
helpviewer_keywords: 
  - "sp_reinitmergepullsubscription"
ms.assetid: 48464bc9-60aa-4886-b526-163f010102b8
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_reinitmergepullsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Marks a merge pull subscription for reinitialization the next time the Merge Agent runs. This stored procedure is executed at the Subscriber in the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_reinitmergepullsubscription [ [ @publisher = ] 'publisher' ]  
    [ , [ @publisher_db = ] 'publisher_db' ]  
    [ , [ @publication = ] 'publication' ]  
    [ , [ @upload_first = ] 'upload_first'  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with a default of ALL.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with a default of ALL.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of ALL.  
  
`[ @upload_first = ] 'upload_first'`
 Is whether changes at the Subscriber are uploaded before the subscription is reinitialized. *upload_first* is **nvarchar(5)**, with a default of FALSE. If **true**, changes are uploaded before the subscription is reinitialized. If **false**, changes are not uploaded.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_reinitmergepullsubscription** is used in merge replication.  
  
 If you add, drop, or change a parameterized filter, pending changes at the Subscriber cannot be uploaded to the Publisher during reinitialization. If you want to upload pending changes, synchronize all subscriptions before changing the filter.  
  
## Example  
 [!code-sql[HowTo#sp_reinitmergepullsub](../../relational-databases/replication/codesnippet/tsql/sp-reinitmergepullsubscr_1.sql)]  
  
## Example  
 [!code-sql[HowTo#sp_reinitmergepullsubwithupload](../../relational-databases/replication/codesnippet/tsql/sp-reinitmergepullsubscr_2.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_reinitmergepullsubscription**.  
  
## See Also  
 [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md)   
 [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
