---
title: "sys.dm_pdw_dms_workers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 0a284d18-3c46-4ffa-bcc9-689e660ee8b4
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.dm_pdw_dms_workers (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Holds information about all workers completing DMS steps.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|request_id|**nvarchar(32)**|Query that this DMS worker is part of.<br /><br /> request_id, step_index, and dms_step_index form the key for this view.|See request_id in [sys.dm_pdw_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md).|  
|step_index|**int**|Query step this DMS worker is part of.<br /><br /> request_id, step_index, and dms_step_index form the key for this view.|See step_index in [sys.dm_pdw_request_steps &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql.md).|  
|dms_step_index|**int**|Step in the DMS plan that this worker is running.<br /><br /> request_id, step_index, and dms_step_index form the key for this view.||  
|pdw_node_id|**int**|Node that the worker is running on.|See node_id in [sys.dm_pdw_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-transact-sql.md).|  
|distribution_id|**Int**|Distribution that the worker is running on, if any.|See distribution_id in [sys.pdw_distributions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-distributions-transact-sql.md).|  
|type|**nvarchar(32)**|Type of DMS worker thread this entry represents.|'DIRECT_CONVERTER', 'DIRECT_READER', 'FILE_READER', 'HASH_CONVERTER', 'HASH_READER', 'ROUNDROBIN_CONVERTER', 'EXPORT_READER', 'EXTERNAL_READER', 'EXTERNAL_WRITER', 'PARALLEL_COPY_READER', 'REJECT_WRITER', 'WRITER'|  
|status|**nvarchar(32)**|Status of the DMS worker.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
|bytes_per_sec|**bigint**|Read or write throughput in the last second.|Greater than or equal to 0. NULL if the query was cancelled or failed before the worker could execute.|  
|bytes_processed|**bigint**|Total bytes processed by this worker.|Greater than or equal to 0. NULL if the query was cancelled or failed before the worker could execute.|  
|rows_processed|**bigint**|Number of rows read or written for this worker.|Greater than or equal to 0. NULL if the query was cancelled or failed before the worker could execute.|  
|start_time|**datetime**|Time at which execution of this worker started.|Greater than or equal to start time of the query step this worker belongs to. See [sys.dm_pdw_request_steps &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql.md).|  
|end_time|**datetime**|Time at which execution ended, failed, or was cancelled.|NULL for ongoing or queued workers. Otherwise, greater than start_time.|  
|total_elapsed_time|**int**|Total time spent in execution, in milliseconds.|Greater than or equal to 0.<br /><br /> Total time elapsed since system start or restart. If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br /> The maximum value in milliseconds is equivalent to 24.8 days.|  
|cpu_time|**bigint**|CPU time consumed by this worker, in milliseconds.|Greater than or equal to 0.|  
|query_time|**int**|Period of time before SQL starts returning rows to the thread, in milliseconds.|Greater than or equal to 0.|  
|buffers_available|**int**|Number of unused buffers.| NULL if the query was cancelled or failed before the worker could execute.|  
|sql_spid|**int**|Session id on the SQL Server instance performing the work for this DMS worker.||  
|dms_cpid|**int**|Process ID of the actual thread running.||  
|error_id|**nvarchar(36)**|Unique identifier of the error that occurred during execution of this worker, if any.|See error_id in [sys.dm_pdw_request_steps &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql.md).|  
|source_info|**nvarchar(4000)**|For a reader, specification of the source tables and columns.||  
|destination_info|**nvarchar(4000)**|For a writer, specification of the destination tables.||  
  
 For information about the maximum rows retained by this view, see the Metadata section in the [Capacity limits](/azure/sql-data-warehouse/sql-data-warehouse-service-capacity-limits#metadata) topic..  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
