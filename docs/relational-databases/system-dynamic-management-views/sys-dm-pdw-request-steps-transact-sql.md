---
title: "sys.dm_pdw_request_steps (Transact-SQL)"
description: sys.dm_pdw_request_steps (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "08/28/2020"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: cc563e88-0d34-436e-b914-b60d6ee0d50b
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_request_steps (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds information about all steps that compose a given request or query in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. It lists one row per query step.

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|request_id|**nvarchar(32)**|request_id and step_index make up the key for this view.<br /><br /> Unique numeric ID associated with the request.|See request_id in [sys.dm_pdw_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md).|  
|step_index|**int**|request_id and step_index make up the key for this view.<br /><br /> The position of this step in the sequence of steps that make up the request.|0 to (n-1) for a request with n steps.|  
|plan_node_id|**int**|The node ID corresponding to the operator ID of that step in the execution plan.|None|  
|operation_type|**nvarchar(35)**|Type of operation represented by this step.|**DMS query plan operations:** 'ReturnOperation', 'PartitionMoveOperation', 'MoveOperation', 'BroadcastMoveOperation', 'ShuffleMoveOperation', 'TrimMoveOperation', 'CopyOperation', 'DistributeReplicatedTableMoveOperation'<br /><br /> **SQL query plan operations:** 'OnOperation', 'RemoteOperation'<br /><br /> **Other query plan operations:** 'MetaDataCreateOperation', 'RandomIDOperation'<br /><br /> **External operations for reads:** 'HadoopShuffleOperation', 'HadoopRoundRobinOperation', 'HadoopBroadcastOperation'<br /><br /> **External operations for MapReduce:** 'HadoopJobOperation', 'HdfsDeleteOperation'<br /><br /> **External operations for writes:** 'ExternalExportDistributedOperation', 'ExternalExportReplicatedOperation', 'ExternalExportControlOperation'<br /><br /> For more information, see "Understanding Query Plans" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)]. <br /><br />  A query plan can also be affected by the database settings.  Check [ALTER DATABASE SET options](../../t-sql/statements/alter-database-transact-sql-set-options.md?bc=%252fazure%252fsql-data-warehouse%252fbreadcrumb%252ftoc.json&toc=%252fazure%252fsql-data-warehouse%252ftoc.json&view=azure-sqldw-latest&preserve-view=true) for details.|  
|distribution_type|**nvarchar(32)**|Type of distribution this step will undergo.|'AllNodes', 'AllDistributions', 'AllComputeNodes', 'ComputeNode', 'Distribution', 'SubsetNodes', 'SubsetDistributions', 'Unspecified'|  
|location_type|**nvarchar(32)**|Where the step is running.|'Compute', 'Control', 'DMS'|  
|status|**nvarchar(32)**|Status of this step.|Pending, Running, Complete, Failed, UndoFailed, PendingCancel, Canceled, Undone, Aborted|  
|error_id|**nvarchar(36)**|Unique ID of the error associated with this step, if any.|See error_id of [sys.dm_pdw_errors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-errors-transact-sql.md). NULL if no error occurred.|  
|start_time|**datetime**|Time at which the step started execution.|Smaller or equal to current time and larger or equal to end_compile_time of the query to which this step belongs. For more information on queries, see [sys.dm_pdw_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md).|  
|end_time|**datetime**|Time at which this step completed execution, was cancelled, or failed.|Smaller or equal to current time and larger or equal to start_time. Set to NULL for steps currently in execution or queued.|  
|total_elapsed_time|**int**|Total amount of time the query step has been running, in milliseconds.|Between 0 and the difference between end_time and start_time. 0 for queued steps.<br /><br /> If total_elapsed_time exceeds the maximum value for an integer, total_elapsed_time will continue to be the maximum value. This condition will generate the warning "The maximum value has been exceeded."<br /><br /> The maximum value in milliseconds is equivalent to 24.8 days.|  
|row_count|**bigint**|Total number of rows changed or returned by this request.|The number of rows affected by the step.  Greater than or equal to zero for data operation steps.  -1 for steps that do not operate on data.|  
|estimated_rows|**bigint**|Total number of rows of work calculated during query compilation.|The number of rows estimated by the step.  Greater than or equal to zero for data operation steps.  -1 for steps that do not operate on data.|  
|command|**nvarchar(4000)**|Holds the full text of the command of this step.|Any valid request string for a step. NULL when the operation is of the type MetaDataCreateOperation. Truncated if longer than 4000 characters.|  
  
 For information about the maximum rows retained by this view, see the Maximum System View Values section in the "Minimum and Maximum Values" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
