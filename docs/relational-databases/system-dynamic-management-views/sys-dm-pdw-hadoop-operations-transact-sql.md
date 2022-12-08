---
title: "sys.dm_pdw_hadoop_operations (Transact-SQL)"
description: sys.dm_pdw_hadoop_operations (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 5d2337d4-e2c7-48de-9c26-cdc7e6eb5d55
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_hadoop_operations (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Contains a row for each map-reduce job that is pushed down to Hadoop as part of running a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] query on an external Hadoop table. Each map-reduce job represents one of the predicates in the query. This is only used when predicate pushdown is enabled for queries on Hadoop external tables.

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|request_id|**nvarchar(32)**|ID for this external Hadoop operation.|Same as ID in [sys.dm_pdw_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md).|  
|step_index|**int**|Index of the query step that refers to this Hadoop operation.|Same as step_index in [sys.dm_pdw_request_steps &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql.md).|  
|operation_type|**nvarchar(255)**|Describes the type of external operation.|'External Hadoop Operation'|  
|operation_name|**nvarchar(4000)**|The job ID for a map-reduce job. This is returned by Hadoop after [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] submits the job.||  
|map_progress|**float**|The percentage of input data that has been consumed so far by the map job.|A floating point number between, and including, 0 and 100.|  
|reduce_progress|**int**|The percentage of the reduce job that has completed..|A floating point number between, and including, 0 and 100.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)  
  
