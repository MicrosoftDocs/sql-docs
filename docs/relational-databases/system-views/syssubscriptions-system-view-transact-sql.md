---
description: "syssubscriptions (System View) (Transact-SQL)"
title: "syssubscriptions (System View) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
f1_keywords: 
  - "syssubscriptions_TSQL"
  - "syssubscriptions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syssubscriptions view"
ms.assetid: c9613858-9512-43a9-aa53-7ee8064f064c
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# syssubscriptions (System View) (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **syssubscriptions** view exposes subscription information. This view is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**int**|The unique ID of a subscribed article.|  
|**srvid**|**smallint**|The server ID of the Subscriber.|  
|**dest_db**|**sysname**|The name of the subscription database.|  
|**status**|**tinyint**|The status of the subscription:<br /><br /> **0** = Inactive.<br /><br /> **1** = Subscribed.<br /><br /> **2** = Active.|  
|**sync_type**|**tinyint**|The type of initial synchronization:<br /><br /> **1** = Automatic.<br /><br /> **2** = None.|  
|**login_name**|**sysname**|The login name used when connecting to the Publisher to add the subscription.|  
|**subscription_type**|**int**|The type of subscription:<br /><br /> **0** = Push - the distribution agent runs at the Distributor.<br /><br /> **1** = Pull - the distribution agent runs at the Subscriber.|  
|**distribution_jobid**|**binary(16)**|Identifies the Distribution Agent job used to synchronize the subscription.|  
|**timestamp**|**timestamp**|The date and time that the subscription was created.|  
|**update_mode**|**tinyint**|The update mode:<br /><br /> **0** = Read-only.<br /><br /> **1** = Immediate-updating.|  
|**loopback_detection**|**bit**|Applies to subscriptions that are part of a bidirectional transactional replication topology. Loopback detection determines whether the Distribution Agent sends transactions originated at the Subscriber back to the Subscriber:<br /><br /> **0** = Sends back.<br /><br /> **1** = Does not send back.|  
|**queued_reinit**|**bit**|Specifies whether the article is marked for initialization or reinitialization. A value of **1** specifies that the subscribed article is marked for initialization or reinitialization.|  
|**nosync_type**|**tinyint**|The type of subscription initialization:<br /><br /> **0** = automatic (snapshot)<br /><br /> **1** = replication support only<br /><br /> **2** = initialize with backup<br /><br /> **3** = initialize from log sequence number (LSN)<br /><br /> For more information, see the **\@sync_type** parameter of [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md).<br /><br /> **3** = [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**srvname**|**sysname**|The name of the Subscriber.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [syssubscriptions &#40;Transact-SQL&#41;](../../relational-databases/system-tables/syssubscriptions-transact-sql.md)  
  
  
