---
title: "MStracer_history (Transact-SQL)"
description: MStracer_history (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MStracer_history"
  - "MStracer_history_TSQL"
helpviewer_keywords:
  - "MStracer_history system table"
dev_langs:
  - "TSQL"
ms.assetid: 97237a0c-d574-4b17-8a94-1a8730b31d98
---
# MStracer_history (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MStracer_history** table maintains a record of all tracer tokens that have been received at the Subscriber. This table is stored in the distribution database and is used by replication for performance monitoring.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**parent_tracer_id**|**int**|Uniquely identifies a tracer token.|  
|**agent_id**|**int**|Identifies the agent which handled the tracer token record.|  
|**subscriber_commit**|**datetime**|The date and time when the tracer token record was committed at the Subscriber.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
