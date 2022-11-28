---
description: "sp_setreplfailovermode (Transact-SQL)"
title: "sp_setreplfailovermode (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_setreplfailovermode"
  - "sp_setreplfailovermode_TSQL"
helpviewer_keywords: 
  - "sp_setreplfailovermode"
ms.assetid: ca98a4c3-bea4-4130-88d7-79e0fd1e85f6
author: markingmyname
ms.author: maghan
---
# sp_setreplfailovermode (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Allows you to set the failover operation mode for subscriptions enabled for immediate updating with queued updating as failover. This stored procedure is executed at the Subscriber on the subscription database. For more information about failover modes, see [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_setreplfailovermode [ @publisher= ] 'publisher'  
    [ , [ @publisher_db = ] 'publisher_db' ]  
    [ , [ @publication= ] 'publication' ]  
    [ , [ @failover_mode= ] 'failover_mode' ]  
    [ , [ @override = ] override ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the publication. *publication* is **sysname**, with no default. The publication must already exist.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publication database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @failover_mode = ] 'failover_mode'`
 Is the failover mode for the subscription. *failover_mode* is **nvarchar(10)** and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**immediate** or **sync**|Data modifications made at the Subscriber are bulk-copied to the Publisher as they occur.|  
|**queued**|Data modifications are stored in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queue.|  
  
> [!NOTE]  
>  [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing has been deprecated and is no longer supported.  
  
`[ @override = ] override`
 Internal use only.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_setreplfailovermode** is used in snapshot replication or transactional replication for which subscriptions are enabled, either for queued updating with failover to immediate updating, or for immediate updating with failover to queued updating.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_setreplfailovermode**.  
  
## See Also  
 [Switch Between Update Modes for an Updatable Transactional Subscription](../../relational-databases/replication/administration/switch-between-update-modes-for-an-updatable-transactional-subscription.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
