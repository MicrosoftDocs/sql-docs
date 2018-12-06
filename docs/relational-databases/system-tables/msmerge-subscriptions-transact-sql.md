---
title: "MSmerge_subscriptions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_subscriptions"
  - "MSmerge_subscriptions_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_subscriptions system table"
ms.assetid: cafd954a-92f8-44cb-a5d0-dce9aafa5ee1
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_subscriptions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_subscriptions** table contains one row for each subscription serviced by the Merge Agent at the Subscriber. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_id**|**smallint**|The ID of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication_id**|**int**|The ID of the publication.|  
|**subscriber_id**|**smallint**|The ID of the Subscriber.|  
|**subscriber_db**|**sysname**|The name of the subscription database.|  
|**subscription_type**|**int**|The type of subscription:<br /><br /> 0 = Push.<br /><br /> 1 = Pull.<br /><br /> 2 = Anonymous.|  
|**sync_type**|**tinyint**|The type of synchronization:<br /><br /> 1 = Automatic.<br /><br /> 2 = No sync.|  
|**status**|**tinyint**|The status of the subscription.|  
|**subscription_time**|**datetime**|The time the subscription was added.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
