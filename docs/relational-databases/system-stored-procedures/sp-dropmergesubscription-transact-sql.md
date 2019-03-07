---
title: "sp_dropmergesubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dropmergesubscription_TSQL"
  - "sp_dropmergesubscription"
helpviewer_keywords: 
  - "sp_dropmergesubscription"
ms.assetid: 34244ae6-bd98-4a6a-bbd3-85f50edfcdc0
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dropmergesubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops a subscription to a merge publication and its associated Merge Agent. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropmergesubscription [ [ @publication= ] 'publication' ]   
    [ , [ @subscriber= ] 'subscriber'    
    [ , [ @subscriber_db= ] 'subscriber_db' ]   
    [ , [ @subscription_type= ] 'subscription_type' ]   
    [ , [ @ignore_distributor = ] ignore_distributor ]   
    [ , [ @reserved = ] reserved ]  
```  
  
## Arguments  
 [ **@publication=** ] **'**_publication_**'**  
 Is the publication name. *publication* is **sysname**, with a default of NULL. The publication must already exist and conform to the rules for identifiers.  
  
 [ **@subscriber=**] **'**_subscriber_**'**  
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL.  
  
 [ **@subscriber_db=** ] **'**_subscriber_db_**'**  
 Is the name of the subscription database. *subscription_database*is **sysname**, with a default of NULL.  
  
 [ **@subscription_type=** ] **'**_subscription_type_**'**  
 Is the type of subscription. *subscription_type*is **nvarchar(15)**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**all**|Push, pull, and anonymous subscriptions|  
|**anonymous**|Anonymous subscription.|  
|**push**|Push subscription.|  
|**pull**|Pull subscription.|  
|**both** (default)|Both push and pull subscriptions.|  
  
 [ **@ignore_distributor =** ] *ignore_distributor*  
 Indicates whether this stored procedure is executed without connecting to the Distributor. *ignore_distributor* is **bit**, with a default of **0**. This parameter can be used to drop a subscription without doing cleanup tasks at the Distributor. It is also useful if you had to reinstall the Distributor.  
  
 [ **@reserved=** ] *reserved*  
 Is reserved for future use. *reserved* is **bit**, with a default of **0**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropmergesubscription** is used in merge replication.  
  
## Example  
 [!code-sql[HowTo#sp_dropmergesubscription](../../relational-databases/replication/codesnippet/tsql/sp-dropmergesubscription_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_dropmergesubscription**.  
  
## See Also  
 [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md)   
 [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md)   
 [sp_addmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md)   
 [sp_changemergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergesubscription-transact-sql.md)   
 [sp_helpmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergesubscription-transact-sql.md)  
  
  
