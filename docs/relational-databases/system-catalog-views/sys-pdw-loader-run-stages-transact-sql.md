---
title: "sys.pdw_loader_run_stages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 255681e9-323c-42c0-a63c-1f05536efdd5
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = sqlallproducts-allversions"
---
# sys.pdw_loader_run_stages (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Contains information about ongoing and completed load operations in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]. The information persists across system restarts.  
  
|||||  
|-|-|-|-|  
|Column Name|Data Type|Description|Range|  
|run_id|**int**|Unique identifier of a loader run.||  
|stage|**nvarchar(30)**|The current stage for the run.|'CREATE_STAGING', 'DMS_LOAD', 'LOAD_INSERT', 'LOAD_CLEANUP'|  
|request_id|**nvarchar(32)**|ID of the request running this stage.||  
|status|**nvarchar(16)**|Status of this phase.||  
|start_time|**datetime**|Time at which the stage was started.||  
|end_time|**datetime**|Time at which the stage ended, if any.|NULL if not started or in progress.|  
|total_elapsed_time|**int**|Total time this stage spent (or spent so far) running.|If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br /> The maximum value in milliseconds is equivalent to 24.8 days.|  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
