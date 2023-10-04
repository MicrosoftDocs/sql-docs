---
title: "sys.dm_pdw_nodes_exec_query_profiles (Transact-SQL)"
description: Dynamic management view that can be used to monitor real time data warehouse query progress while the query is in execution.
author: jacinda-eng
ms.author: jacindaeng
ms.reviewer: wiassaf
ms.date: "10/14/2019"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---

# sys.dm_pdw_nodes_exec_query_profiles (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Monitors real time data warehouse query progress while the query is in execution. 

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
## Table returned
  
The counters returned are per operator per thread. The results are dynamic and do not match the results of existing options such as `SET STATISTICS XML ON` which only create output when the query is finished.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|pdw_node_id|**int**|Unique numeric ID associated with the node.|
|session_id|**smallint**|Identifies the session in which this query runs. References dm_exec_sessions.session_id.|  
|request_id|**int**|Identifies the target request. References dm_exec_sessions.request_id.|  
|sql_handle|**varbinary(64)**|Is a token that uniquely identifies the batch or stored procedure that the query is part of. References dm_exec_query_stats.sql_handle.|  
|plan_handle|**varbinary(64)**|Is a token that uniquely identifies a query execution plan for a batch that has executed and its plan resides in the plan cache, or is currently executing. References dm_exec_query_stats.plan_handle.|  
|physical_operator_name|**nvarchar(256)**|Physical operator name.|  
|node_id|**int**|Identifies an operator node in the query tree.|  
|thread_id|**int**|Distinguishes the threads (for a parallel query) belonging to the same query operator node.|  
|task_address|**varbinary(8)**|Identifies the SQLOS task that this thread is using. References dm_os_tasks.task_address.|  
|row_count|**bigint**|Number of rows returned by the operator so far.|  
|rewind_count|**bigint**|Number of rewinds so far.|  
|rebind_count|**bigint**|Number of rebinds so far.|  
|end_of_scan_count|**bigint**|Number of end of scans so far.|  
|estimate_row_count|**bigint**|Estimated number of rows. It can be useful to compare to estimated_row_count to the actual row_count.|  
|first_active_time|**bigint**|The time, in milliseconds, when the operator was first called.|  
|last_active_time|**bigint**|The time, in milliseconds, when the operator was last called.|  
|open_time|**bigint**|Timestamp when open (in milliseconds).|  
|first_row_time|**bigint**|Timestamp when first row was opened (in milliseconds).|  
|last_row_time|**bigint**|Timestamp when last row was opened(in milliseconds).|  
|close_time|**bigint**|Timestamp when close (in milliseconds).|  
|elapsed_time_ms|**bigint**|Total elapsed time (in milliseconds) used by the target node's operations so far.|  
|cpu_time_ms|**bigint**|Total CPU time (in milliseconds) use by target node's operations so far.|  
|database_id|**smallint**|ID of the database that contains the object on which the reads and writes are being performed.|  
|object_id|**int**|The identifier for the object on which the reads and writes are being performed. References sys.objects.object_id.|  
|index_id|**int**|The index (if any) the rowset is opened against.|  
|scan_count|**bigint**|Number of table/index scans so far.|  
|logical_read_count|**bigint**|Number of logical reads so far.|  
|physical_read_count|**bigint**|Number of physical reads so far.|  
|read_ahead_count|**bigint**|Number of read-aheads so far.|  
|write_page_count|**bigint**|Number of page-writes so far due to spilling.|  
|lob_logical_read_count|**bigint**|Number of LOB logical reads so far.|  
|lob_physical_read_count|**bigint**|Number of LOB physical reads so far.|  
|lob_read_ahead_count|**bigint**|Number of LOB read-aheads so far.|  
|segment_read_count|**int**|Number of segment read-aheads so far.|  
|segment_skip_count|**int**|Number of segments skipped so far.| 
|actual_read_row_count|**bigint**|Number of rows read by an operator before the residual predicate was applied.| 
|estimated_read_row_count|**bigint**|**Applies to:** Beginning with [!INCLUDE[ssSQL15_md](../../includes/sssql16-md.md)] SP1. <br/>Number of rows estimated to be read by an operator before the residual predicate was applied.|  
  
## Remarks

The same remarks in [sys.dm_exec_query_profiles](./sys-dm-exec-query-profiles-transact-sql.md) apply.  

## Permissions  
 Requires `VIEW SERVER STATE` permission on the server.  

## See also

 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
   

 ## Next steps 

Azure Synapse Analytics development overview](/azure/sql-data-warehouse/sql-data-warehouse-overview-develop).
