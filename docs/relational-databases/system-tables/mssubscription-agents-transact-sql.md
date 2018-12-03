---
title: "MSsubscription_agents (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSsubscription_agents"
  - "MSsubscription_agents_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSsubscription_agents system table"
ms.assetid: 86ad5891-0bef-4963-9381-7d5b45245a0c
author: stevestein
ms.author: sstein
manager: craigg
---
# MSsubscription_agents (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSsubscription_agents** table is used by Distribution Agent and triggers of updateable subscriptions to track subscription properties. This table is stored in the subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|The ID of the row.|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**publisher_db**|**sysname**|The name of the publication database.|  
|**publication**|**sysname**|The name of the publication.|  
|**subscription_type**|**int**|The subscription type:<br /><br /> 0 = Push.<br /><br /> 1 = Pull<br /><br /> 2 = Pull anonymous.|  
|**queue_id**|**sysname**|The ID of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queue at the Publisher. *queue_id* is set to **SQL** for SQL-based queued updating.|  
|**update_mode**|**tinyint**|The type of updating:<br /><br /> **0** = Read-only.<br /><br /> **1** = Immediate update.<br /><br /> **2** = Queued update using Message Queuing.<br /><br /> **3** = Immediate update with queued update as failover using Message Queuing.<br /><br /> **4** = Queued update using SQL Server queue.<br /><br /> **5** = immediate update with queued update failover, using SQL Server queue.|  
|**failover_mode**|**bit**|If a failover type of updating was select, this is the type of failover chosen:<br /><br /> **0** = Immediate update is being used. Failover is not enabled.<br /><br /> **1** = Queued update is being used. Failover is enabled. The queue being used for failover is specified in the *update_mode* value.|  
|**spid**|**int**|The system process ID for the connection used by the Distribution Agent that is currently running or has just run.|  
|**login_time**|**datetime**|The date and time of the Distribution Agent connection that is currently running or has just run.|  
|**allow_subscription_copy**|**bit**|Specifies whether or not the ability to copy the subscription database is allowed.|  
|**attach_state**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**attach_version**|**binary(16)**|The unique identifier representing the version of an attached subscription.|  
|**last_sync_status**|**int**|The last run status of the Distribution Agent that is currently running or has just run. The status can be:<br /><br /> **1** = Started.<br /><br /> **2** = Succeeded.<br /><br /> **3** = In progress.<br /><br /> **4** = Idle.<br /><br /> **5** = Retry.<br /><br /> **6** = Fail.|  
|**last_sync_summary**|**sysname**|The last message of the Distribution Agent that is currently running or has just run. The status can be:<br /><br /> **Started.**<br /><br /> **Succeeded.**<br /><br /> **In progress.**<br /><br /> **Idle.**<br /><br /> **Retry.**<br /><br /> **Fail.**|  
|**last_sync_time**|**datetime**|The date and time when the *last_sync_summary* and *last_sync_status* columns were updated. Pull or anonymous distribution agents running as SqlServer Agent Service jobs do not update these columns. The history information instead logs to the job history table in that case.|  
|**queue_server**|**sysname**|Internal use only.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_helppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppullsubscription-transact-sql.md)  
  
  
