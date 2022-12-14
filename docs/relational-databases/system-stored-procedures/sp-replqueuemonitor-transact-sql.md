---
description: "sp_replqueuemonitor (Transact-SQL)"
title: "sp_replqueuemonitor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_replqueuemonitor"
  - "sp_replqueuemonitor_TSQL"
helpviewer_keywords: 
  - "sp_replqueuemonitor"
ms.assetid: 6909a3f1-43a2-4df5-a6a5-9e6f347ac841
author: markingmyname
ms.author: maghan
---
# sp_replqueuemonitor (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Lists the queue messages from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queue or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing for queued updating subscriptions to a specified publication. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queues are used, this stored procedure is executed at the Subscriber on the subscription database. If Message Queuing is used, this stored procedure is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replqueuemonitor [ @publisher = ] 'publisher'  
    [ , [ @publisherdb = ] 'publisher_db' ]  
    [ , [ @publication = ] 'publication' ]  
    [ , [ @tranid = ] 'tranid' ]  
    [ , [ @queuetype = ] 'queuetype' ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with a default of NULL. The server must be configured for publishing. NULL for all Publishers.  
  
`[ @publisherdb = ] 'publisher_db' ]`
 Is the name of the publication database. *publisher_db* is **sysname**, with a default of NULL. NULL for all publication databases.  
  
`[ @publication = ] 'publication' ]`
 Is the name of the publication. *publication*is **sysname**, with a default of NULL. NULL for all publications.  
  
`[ @tranid = ] 'tranid' ]`
 Is the transaction ID. *tranid*is **sysname**, with a default of NULL. NULL for all transactions.  
  
`[ @queuetype = ] 'queuetype' ]`
 Is the type of queue that stores transactions. *queuetype* is **tinyint** with a default of **0**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|All types of queues|  
|**1**|Message Queuing|  
|**2**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queue|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replqueuemonitor** is used in snapshot replication or transactional replication with queued updating subscriptions. The queue messages that do not contain SQL commands or are part of a spanning SQL command are not displayed.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_replqueuemonitor**.  
  
## See Also  
 [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
