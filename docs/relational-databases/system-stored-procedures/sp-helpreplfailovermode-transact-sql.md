---
description: "sp_helpreplfailovermode (Transact-SQL)"
title: "sp_helpreplfailovermode (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_helpreplfailovermode"
  - "sp_helpreplfailovermode_TSQL"
helpviewer_keywords: 
  - "sp_helpreplfailovermode"
ms.assetid: d1090e42-6840-4bf6-9aa9-327fd8987ec2
author: markingmyname
ms.author: maghan
---
# sp_helpreplfailovermode (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Displays the current failover mode of a subscription. This stored procedure is executed at the Subscriber on any database. For more information about failover modes, see [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpreplfailovermode [ @publisher= ] 'publisher'   
    [ , [ @publisher_db = ] 'publisher_db' ]   
    [ , [ @publication = ] 'publication' ]   
    [ , [ @failover_mode_id= ] 'failover_mode_id'OUTPUT]   
    [ , [ @failover_mode = ] 'failover_mode'OUTPUT]   
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher that is participating in the update of this Subscriber. *publisher* is **sysname**, with no default. The Publisher must already be configured for publishing.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publication database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication that is participating in the update of this Subscriber. *publication*is **sysname**, with no default.  
  
`[ @failover_mode_id = ] 'failover_mode_id' OUTPUT`
 Returns the integer value of the failover mode and is an **OUTPUT** parameter. *failover_mode_id* is a **tinyint** with a default of **0**. It returns **0** for immediate updating and **1** for queued updating.  
  
`[ @failover_mode = ] 'failover_mode' OUTPUT`
 Returns the mode in which data modifications are made at the Subscriber. *failover_mode* is a **nvarchar(10)** with a default of NULL. Is an **OUTPUT** parameter.  
  
|Value|Description|  
|-----------|-----------------|  
|**immediate**|Immediate updating: updates made at the Subscriber are immediately propagated to the Publisher using two-phase commit protocol (2PC).|  
|**queued**|Queued updating: updates made at the Subscriber are stored in a queue.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpreplfailovermode** is used in snapshot replication or transactional replication for which subscriptions are enabled for immediate updating with queued updating as failover, in case of failure.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_helpreplfailovermode**.  
  
## See Also  
 [sp_setreplfailovermode &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-setreplfailovermode-transact-sql.md)  
  
  
