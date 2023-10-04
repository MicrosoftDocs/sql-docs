---
title: "sys.dm_exec_external_work (Transact-SQL)"
description: sys.dm_exec_external_work (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/20/2021
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "DM_EXEC_EXTERNAL_WORK"
  - "DM_EXEC_EXTERNAL_WORK_TSQL"
  - "SYS.DM_EXEC_EXTERNAL_WORK_TSQL"
helpviewer_keywords:
  - "sys.dm_exec_external_work management view"
  - "dm_exec_external_work management view"
  - "PolyBase,views"
  - "PolyBase"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_external_work (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Returns information about the workload per worker, on each compute node.  
  
Query `sys.dm_exec_external_work` to identify the work spun up to communicate with the external data source (for example, Hadoop or MongoDB).  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|execution_id|`nvarchar(32)`|Unique identifier for associated PolyBase query.|See *request_ID* in [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).|  
|step_index|`int`|The request this worker is performing.|See *step_index* in  [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).|  
|dms_step_index|`int`|Step in the DMS plan that this worker is executing.|See [sys.dm_exec_dms_workers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-dms-workers-transact-sql.md).|  
|compute_node_id|`int`|The node the worker is running on.|See [sys.dm_exec_compute_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md).|  
|type|`nvarchar(60)`|The type of external work.|'File Split' (for Hadoop and Azure storage)<br/><br/>'ODBC Data Split' (for other external data sources) |  
|work_id|`int`|ID of the actual split.|Greater than or equal to 0.|  
|input_name|`nvarchar(4000)`|Name of the input to be read|File name (with path) when using Hadoop or Azure storage. For other external data sources, it is the concatenation of the external data source location and the external table location: `scheme://DataSourceHostname[:port]/[DatabaseName.][SchemaName.]TableName`|  
|read_location|`bigint`|Offset of read location.| `0` to the number of bytes in the file minus 1.<br/><br/>`NULL` for non-Hadoop or non-Azure storage. |  
|read_command|`nvarchar(4000)`|The query that is sent to the external data source. Introduced in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)].|Text representing the query. For Hadoop and Azure storage returns `NULL`.|
|bytes_processed|`bigint`|Total bytes allocated for processing data by this worker. This value may not necessarily represent the total data being returned by the query |Greater than or equal to 0.|  
|length|`bigint`|Length of the split or, HDFS block for Hadoop|User-definable. The default is 64M|  
|status|`nvarchar(32)`|Status of the worker|Pending, Processing, Done, Failed, Aborted|  
|start_time|`datetime`|Beginning of the work||  
|end_time|`datetime`|End of the work||  
|total_elapsed_time|`int`|Total time in milliseconds||
|compute_pool_id|`int`|Unique identifier for the pool where the worker is running. Only applies to SQL Server Big Data Cluster. See [sys.dm_exec_compute_pools (Transact-SQL)](sys-dm-exec-compute-pools.md).|Returns `0` for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Windows and Linux.|

## Remarks

Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], you can use `sys.dm_exec_external_work` to view the remote query passed to an external data source in PolyBase pushdown computation. For more information, see [How to tell if external pushdown occurred](../polybase/polybase-how-to-tell-pushdown-computation.md).

## See also  
 [PolyBase troubleshooting with dynamic management views](/previous-versions/sql/sql-server-2016/mt146389(v=sql.130))   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
