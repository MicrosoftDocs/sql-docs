---
title: "MSmerge_subscriptions (Transact-SQL)"
description: MSmerge_subscriptions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/16/2022"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_subscriptions"
  - "MSmerge_subscriptions_TSQL"
helpviewer_keywords:
  - "MSmerge_subscriptions system table"
dev_langs:
  - "TSQL"
ms.assetid: cafd954a-92f8-44cb-a5d0-dce9aafa5ee1
---
# MSmerge_subscriptions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
|**status**|**tinyint**|This column is no longer in use. To check the status of a merge subscription, review the status column in the [sysmergesubscriptions](sysmergesubscriptions-transact-sql.md) table in the publication or subscription database. |  
|**subscription_time**|**datetime**|The time the subscription was added.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
