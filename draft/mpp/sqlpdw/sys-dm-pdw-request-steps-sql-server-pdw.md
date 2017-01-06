---
title: "sys.dm_pdw_request_steps (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 01d40e34-5649-429d-84a2-b6c94bd72b74
caps.latest.revision: 20
author: BarbKess
---
# sys.dm_pdw_request_steps (SQL Server PDW)
Holds information about all steps that compose a given request or query in SQL Server PDW. It lists one row per query step.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|request_id|**nvarchar(32)**|request_id and step_index make up the key for this view.<br /><br />Unique numeric id associated with the request.|See request_id in [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md).|  
|step_index|**int**|request_id and step_index make up the key for this view.<br /><br />The position of this step in the sequence of steps that make up the request.|0 to (n-1) for a request with n steps.|  
|operation_type|**nvarchar(32)**|Type of operation represented by this step.|**DMS query plan operations:** 'ReturnOperation', 'PartitionMoveOperation', 'MoveOperation', 'BroadcastMoveOperation', 'ShuffleMoveOperation', 'TrimMoveOperation', 'CopyOperation', 'DistributeReplicatedTableMoveOperation'<br /><br />**SQL query plan operations:** 'OnOperation', 'RemoteOperation'<br /><br />**Other query plan operations:** 'MetaDataCreateOperation', 'RandomIDOperation'<br /><br />**External operations for reads:** 'HadoopShuffleOperation', 'HadoopRoundRobinOperation', 'HadoopBroadcastOperation'<br /><br />**External operations for MapReduce:** 'HadoopJobOperation', 'HdfsDeleteOperation'<br /><br />**External operations for writes:** 'ExternalExportDistributedOperation', 'ExternalExportReplicatedOperation', 'ExternalExportControlOperation'<br /><br />For more information, see [Understanding Query Plans &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-plans-sql-server-pdw.md).|  
|distribution_type|**nvarchar(32)**|Type of distribution this step will undergo.|'AllNodes', 'AllDistributions', 'AllComputeNodes', 'ComputeNode', 'Distribution', 'SubsetNodes', 'SubsetDistributions', 'Unspecified'|  
|location_type|**nvarchar(32)**|Where the step is running.|'Compute', 'Control'|  
|status|**nvarchar(32)**|Status of this step.|'Pending', 'Running', 'Complete', 'Failed', 'UndoFailed', 'PendingCancel', 'Cancelled', 'Undone', 'Aborted'.|  
|error_id|**nvarchar(36)**|Unique id of the error associated with this step, if any.|See error_id of sys.dm_pdw_request_errors (SQL Server PDW). NULL if no error occurred.|  
|start_time|**datetime**|Time at which the step started execution.|Smaller or equal to current time and larger or equal to end_compile_time of the query to which this step belongs. For more information on queries, see [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md).|  
|end_time|**datetime**|Time at which this step completed execution, was cancelled, or failed.|Smaller or equal to current time and larger or equal to start_time. Set to NULL for steps currently in execution or queued.|  
|total_elapsed_time|**int**|Total amount of time the query step has been running, in milliseconds.|Between 0 and the difference between end_time and start_time. 0 for queued steps.<br /><br />If total_elapsed_time exceeds the maximum value for an integer, total_elapsed_time will continue to be the maximum value. This condition will generate the warning “The maximum value has been exceeded.”<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
|row_count|**bigint**|Total number of rows changed or returned by this request.|0 for steps that did not change or return data. Otherwise, number of rows affected.|  
|command|**nvarchar(4000)**|Holds the full text of the command of this step.|Any valid request string for a step. Truncated if longer than 4000 characters.|  
  
For information about the maximum rows retained by this view, see the Maximum System View Values section in the [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md) topic.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
