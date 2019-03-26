---
title: "sp_addpullsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addpullsubscription"
  - "sp_addpullsubscription_TSQL"
helpviewer_keywords: 
  - "sp_addpullsubscription"
ms.assetid: 0f4bbedc-0c1c-414a-b82a-6fd47f0a6a7f
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addpullsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a pull subscription to a snapshot or transactional publication. This stored procedure is executed at the Subscriber on the database where the pull subscription is to be created.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addpullsubscription [ @publisher= ] 'publisher'  
    [ , [ @publisher_db= ] 'publisher_db' ]  
        , [ @publication= ] 'publication'  
    [ , [ @independent_agent= ] 'independent_agent' ]  
    [ , [ @subscription_type= ] 'subscription_type' ]  
    [ , [ @description= ] 'description' ]  
    [ , [ @update_mode= ] 'update_mode' ]  
    [ , [ @immediate_sync = ] immediate_sync ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with a default of NULL. *publisher_db* is ignored by Oracle Publishers.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @independent_agent = ] 'independent_agent'`
 Specifies if there is a stand-alone Distribution Agent for this publication. *independent_agent* is **nvarchar(5)**, with a default of TRUE. If **true**, there is a stand-alone Distribution Agent for this publication. If **false**, there is one Distribution Agent for each Publisher database/Subscriber database pair. *independent_agent* is a property of the publication and must have the same value here as it has at the Publisher.  
  
`[ @subscription_type = ] 'subscription_type'`
 Is the type of subscription. *subscription_type* is **nvarchar(9)**, with a default of **anonymous**. You must specify a value of **pull** for *subscription_type*, unless you want to create a subscription without registering the subscription at the Publisher. In this case, you must specify a value of **anonymous**. This is necessary for cases in which you cannot establish a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection to the Publisher during subscription configuration.  
  
`[ @description = ] 'description'`
 Is the description of the publication. *description* is **nvarchar(100)**, with a default of NULL.  
  
`[ @update_mode = ] 'update_mode'`
 Is the type of update. *update_mode* is **nvarchar(30)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**read only** (default)|The subscription is read-only. Any changes at the Subscriber will not be sent back to the Publisher. Should be used when updates will not be made at the Subscriber.|  
|**synctran**|Enables support for immediate updating subscriptions.|  
|**queued tran**|Enables the subscription for queued updating. Data modifications can be made at the Subscriber, stored in a queue, and then propagated to the Publisher.|  
|**failover**|Enables the subscription for immediate updating with queued updating as a failover. Data modifications can be made at the Subscriber and propagated to the Publisher immediately. If the Publisher and Subscriber are not connected, data modifications made at the Subscriber can be stored in a queue until the Subscriber and Publisher are reconnected.|  
|**queued failover**|Enables the subscription as a queued updating subscription with the ability to change to immediate updating mode. Data modifications can be made at the Subscriber and stored in a queue until a connection is established between the Subscriber and Publisher. When a continuous connection is established the updating mode can be changed to immediate updating. *Not supported for Oracle Publishers*.|  
  
`[ @immediate_sync = ] immediate_sync`
 Is whether the synchronization files are created or re-created each time the Snapshot Agent runs. *immediate_sync* is **bit** with a default of 1, and must be set to the same value as *immediate_sync* in **sp_addpublication**.*immediate_sync* is a property of the publication and must have the same value here as it has at the Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addpullsubscription** is used in snapshot replication and transactional replication.  
  
> [!IMPORTANT]  
>  For queued updating subscriptions, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication for connections to Subscribers, and specify a different account for the connection to each Subscriber. When creating a pull subscription that supports queued updating, replication always sets the connection to use Windows Authentication (for pull subscriptions, replication cannot access metadata at the Subscriber required to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication). In this case, you should execute [sp_changesubscription](../../relational-databases/system-stored-procedures/sp-changesubscription-transact-sql.md) to change the connection to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication after the subscription is configured.  
  
 If the [MSreplication_subscriptions &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msreplication-subscriptions-transact-sql.md) table does not exist at the Subscriber, **sp_addpullsubscription** creates it. It also adds a row to the [MSreplication_subscriptions &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msreplication-subscriptions-transact-sql.md) table. For pull subscriptions, [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) should be called at the Publisher first.  
  
## Example  
 [!code-sql[HowTo#sp_addtranpullsubscriptionagent](../../relational-databases/replication/codesnippet/tsql/sp-addpullsubscription-t_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addpullsubscription**.  
  
## See Also  
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [Create an Updatable Subscription to a Transactional Publication](../../relational-databases/replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md)
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md)   
 [sp_change_subscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-subscription-properties-transact-sql.md)   
 [sp_droppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppullsubscription-transact-sql.md)   
 [sp_helppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppullsubscription-transact-sql.md)   
 [sp_helpsubscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-properties-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
