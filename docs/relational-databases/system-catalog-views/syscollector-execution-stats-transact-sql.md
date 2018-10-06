---
title: "syscollector_execution_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "syscollector_execution_stats"
  - "syscollector_execution_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syscollector_execution_stats view"
  - "data collector view"
ms.assetid: 23e35ac5-fbbf-4922-970c-f4fac44c1263
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# syscollector_execution_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Provides information about task execution for a collection set or package.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**log_id**|**bigint**|Identifies each collection set execution. Used to join this view with other detailed logs. Is not nullable.|  
|**task_name**|**nvarchar(128)**|The name of the collection set or package task that this information is for. Is not nullable.|  
|**execution_row_count_in**|**int**|Number of rows processed at the beginning of data flow. Is nullable.|  
|**execution_row_count_out**|**int**|Number of rows processed at the end of data flow. Is nullable.|  
|**execution_row_count_errors**|**int**|Number of rows that failed during the data flow. Is nullable.|  
|**execution_time_ms**|**int**|The time, in milliseconds, required for the task to complete. Is nullable.|  
|**log_time**|**datetime**|The time that this information was logged. Is not nullable.|  
  
## Permissions  
 Requires SELECT permission for **dc_operator**.  
  
## See Also  
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Data Collector Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-collector-views-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
