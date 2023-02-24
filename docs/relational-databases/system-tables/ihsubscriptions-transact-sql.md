---
title: "IHsubscriptions (Transact-SQL)"
description: IHsubscriptions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "IHsubscriptions_TSQL"
  - "IHsubscriptions"
helpviewer_keywords:
  - "IHsubscriptions system table"
dev_langs:
  - "TSQL"
ms.assetid: 9ec21119-35f1-4e39-abaa-b2c790c485b1
---
# IHsubscriptions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHsubscriptions** system table contains one row for each subscription to a publication from a non-SQL Server Publisher using the current Distributor. This table is stored in the distribution database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article_id**|**int**|Uniquely identifies a published article.|  
|**srvid**|**smallint**|The server ID of the Subscriber.|  
|**dest_db**|**sysname**|The name of the destination database|  
|**login_name**|**sysname**|The login name used when adding the subscription.|  
|**distribution_jobid**|**binary(16)**|The job ID of the Distribution Agent|  
|**timestamp**|**timestamp**|The date and time that the subscription was created.|  
|**queued_reinit**|**bit**|Specifies whether the article is marked for initialization or reinitialization. A value of **1** specifies that the subscribed article is marked for initialization or reinitialization.|  
|**status**|**tinyint**|The status of the subscription:<br /><br /> **0** = Inactive.<br /><br /> **1** = Subscribed.<br /><br /> **2** = Active.|  
|**sync_type**|**tinyint**|The type of initial synchronization:<br /><br /> **1** = Automatic.<br /><br /> **2** = None.|  
|**subscription_type**|**int**|The type of subscription:<br /><br /> **0** = Push - the distribution agent runs at the Subscriber.<br /><br /> **1** = Pull - the distribution agent runs at the Distributor.|  
|**update_mode**|**tinyint**|The update mode:<br /><br /> **0** = Read-only.<br /><br /> **1** = Immediate-updating.|  
|**loopback_detection**|**bit**|Applies to subscriptions that are part of a bidirectional transactional replication topology. Loopback detection determines whether the Distribution Agent sends transactions originated at the Subscriber back to the Subscriber:<br /><br /> **0** = Sends back.<br /><br /> **1** = Does not send back.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md)   
 [sp_helpsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md)  
  
  
