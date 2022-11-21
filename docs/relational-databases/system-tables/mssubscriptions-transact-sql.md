---
title: "MSsubscriptions (Transact-SQL)"
description: MSsubscriptions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSsubscriptions_TSQL"
  - "MSsubscriptions"
helpviewer_keywords:
  - "MSsubscriptions system table"
dev_langs:
  - "TSQL"
ms.assetid: b7e8301d-d115-41f6-8d4f-e0d25f453b25
---
# MSsubscriptions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSsubscriptions** table contains one row for each published article in a subscription serviced by the local Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_database_id**|**int**|The ID of the Publisher database.|  
|**publisher_id**|**smallint**|The ID of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication_id**|**int**|The ID of the publication.|  
|**article_id**|**int**|The ID of the article.|  
|**subscriber_id**|**smallint**|The ID of the Subscriber.|  
|**subscriber_db**|**sysname**|The name of the subscription database.|  
|**subscription_type**|**int**|The type of subscription:<br /><br /> **0** = Push.<br /><br /> **1** = Pull.<br /><br /> **2** = Anonymous.|  
|**sync_type**|**tinyint**|The type of synchronization:<br /><br /> **1** = Automatic.<br /><br /> **2** = No synchronization.|  
|**status**|**tinyint**|The status of the subscription:<br /><br /> **0** = Inactive.<br /><br /> **1** = Subscribed.<br /><br /> **2** = Active.|  
|**subscription_seqno**|**varbinary(16)**|The snapshot transaction sequence number.|  
|**snapshot_seqno_flag**|**bit**|Indicates the source of the snapshot transaction sequence number, where a value of **1** means that **subscription_seqno** is the snapshot sequence number.|  
|**independent_agent**|**bit**|Indicates whether there is a stand-alone Distribution Agent for this publication.|  
|**subscription_time**|**datetime**|Internal use only.|  
|**loopback_detection**|**bit**|Applies to subscriptions that are part of a bidirectional transactional replication topology. Loopback detection determines whether the Distribution Agent sends transactions originated at the Subscriber back to the Subscriber:<br /><br /> **1** = Does not send back.<br /><br /> **0** = Sends back.<br /><br />|  
|**agent_id**|**int**|The ID of the agent.|  
|**update_mode**|**tinyint**|The type of update.|  
|**publisher_seqno**|**varbinary(16)**|The sequence number of the transaction at the Publisher for this subscription.|  
|**ss_cplt_seqno**|**varbinary(16)**|The sequence number used to signify the completion of the concurrent snapshot processing.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_helpsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md)  
  
  
