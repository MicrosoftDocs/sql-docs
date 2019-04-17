---
title: "sys.dm_exec_distributed_request_steps (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "SYS.DM_EXEC_DISTRIBUTED_REQUEST_STEPS_TSQL"
  - "DM_EXEC_DISTRIBUTED_REQUEST_STEPS_TSQL"
  - "DM_EXEC_DISTRIBUTED_REQUEST_STEPS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PolyBase,views"
  - "PolyBase"
  - "dm_exec_distributed_request_steps"
  - "sys.dm_exec_distributed_request_steps management view"
ms.assetid: 1954541d-b716-4e03-8fcc-7022f428e01d
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_distributed_request_steps (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Holds information about all steps that compose a given PolyBase request or query. It lists one row per query step.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|execution_id|**int**|execution_id and step_index make up the key for this view. Unique numeric id associated with the request.|See ID in [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).|  
|step_index|**int**|The position of this step in the sequence of steps that make up the request.|0 to (n-1) for a request with n steps.|  
|operation_type|**nvarchar(128)**|Type of the operation represented by this step.|'MoveOperation','OnOperation','RandomIDOperation','RemoteOperation','ReturnOperation','ShuffleMoveOperation','TempTablePropertiesOperation','DropDiagnosticsNotifyOperation', 'HadoopShuffleOperation', 'HadoopBroadCastOperation', 'HadoopRoundRobinOperation'|  
|distribution_type|**nvarchar(32)**|Where the step is executing.|'AllComputeNodes','AllDistributions','ComputeNode','Distribution','AllNodes','SubsetNodes','SubsetDistributions','Unspecified'.|  
|location_type|**nvarchar(32)**|Where the step is executing.|'Compute','Head' or 'DMS'. All data movement steps show 'DMS'.|  
|status|**nvarchar(32)**|Status of this step|'Pending', 'Running', 'Complete', 'Failed', 'UndoFailed', 'PendingCancel', 'Cancelled', 'Undone', 'Aborted'|  
|error_id|**nvarchar(36)**|Unique id of the error associated with this step, if any|See id of [sys.dm_exec_compute_node_errors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-node-errors-transact-sql.md), NULL if no error occurred.|  
|start_time|**datetime**|Time at which the step started execution|Smaller or equal to current time and larger or equal to end_compile_time of the query to which this step belongs.|  
|end_time|**datetime**|Time at which this step completed execution, was cancelled, or failed.|Smaller or equal to current time and larger or equal to start_time, set to NULL for steps currently in execution or queued.|  
|total_elapsed_time|**int**|Total amount of time the query step has been executing, in milliseconds|Between 0 and the difference between end_time and start_time. 0 for queued steps.|  
|row_count|**bigint**|Total number of rows changed or returned by this request|0 for steps that did not change or return data, number of rows affected otherwise. Set to -1 for DMS steps.|  
|command|nvarchar(4000)|Holds the full text of the command of this step.|Any valid request string for a step. Truncated if longer than 4000 characters.|  
  
## See Also  
 [PolyBase troubleshooting with dynamic management views](https://msdn.microsoft.com/library/ce9078b7-a750-4f47-b23e-90b83b783d80)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
  
