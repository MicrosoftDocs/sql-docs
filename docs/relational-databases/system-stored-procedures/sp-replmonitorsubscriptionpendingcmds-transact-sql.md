---
title: "sp_replmonitorsubscriptionpendingcmds (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replmonitorsubscriptionpendingcmds_TSQL"
  - "sp_replmonitorsubscriptionpendingcmds"
helpviewer_keywords: 
  - "sp_replmonitorsubscriptionpendingcmds"
ms.assetid: df5b955a-feb0-4863-9b3b-7f71e9653b3d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replmonitorsubscriptionpendingcmds (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information on the number of pending commands for a subscription to a transactional publication and a rough estimate of how much time it takes to process them. This stored procedure returns one row for each returned subscription. This stored procedure, which is used to monitor replication, is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replmonitorsubscriptionpendingcmds [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'  
        , [ @subscriber = ] 'subscriber'  
        , [ @subscriber_db = ] 'subscriber_db'   
        , [ @subscription_type = ] subscription_type  
```  
  
## Arguments  
 [ **@publisher** = ] **'***publisher***'**  
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
 [ **@publisher_db** = ] **'***publisher_db***'**  
 Is the name of the published database. *publisher_db* is **sysname**, with no default.  
  
 [ **@publication** = ] **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@subscriber** = ] **'***subscriber***'**  
 Is the name of the Subscriber. *subscriber* is **sysname**, with no default.  
  
 [ **@subscriber_db** = ] **'***subscriber_db***'**  
 Is the name of the subscription database. *subscriber_db* is **sysname**, with no default.  
  
 [ **@subscription_type** = ] *subscription_type*  
 If the type of subscription. *publication_type* is **int**, with no default and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Push subscription|  
|**1**|Pull subscription|  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pendingcmdcount**|**int**|The number of commands that are pending for the subscription.|  
|**estimatedprocesstime**|**int**|Estimate of the number of seconds required to deliver all of the pending commands to the Subscriber.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replmonitorsubscriptionpendingcmds** is used with transactional replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role in the distribution database can execute **sp_replmonitorsubscriptionpendingcmds**. Members of the publication access list for a publication that uses the distribution database can execute **sp_replmonitorsubscriptionpendingcmds** to return pending commands for that publication.  
  
## See Also  
 [Programmatically Monitor Replication](../../relational-databases/replication/monitor/programmatically-monitor-replication.md)  
  
  
