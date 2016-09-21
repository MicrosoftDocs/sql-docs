---
title: "sys.pdw_loader_backup_runs (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3d7e945c-ea98-49a3-ad47-b5af93f25866
caps.latest.revision: 20
author: BarbKess
---
# sys.pdw_loader_backup_runs (SQL Server PDW)
Contains information about ongoing and completed backup, restore, and load operations in SQL Server PDW. The information persists across system restarts.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|run_id|**int**|Unique identifier for a specific backup, restore, or load run.<br /><br />Key for this view.||  
|name|**nvarchar(255)**|Null for load. Optional name for backup or restore.||  
|submit_time|**datetime**|Time the request was submitted.||  
|start_time|**datetime**|Time the operation started.||  
|end_time|**datetime**|Time the operation completed, failed, or was cancelled.||  
|total_elapsed_time|**int**|Total time elapsed between start_time and current time, or between start_time and end_time for completed, cancelled, or failed runs.|If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
|operation_type|**nvarchar(16)**|The load type.|'BACKUP', 'LOAD', 'RESTORE'|  
|mode|**nvarchar(16)**|The mode within the run type.|**For operation_type = BACKUP**<br /><br />DIFFERENTIAL<br /><br />FULL<br /><br />**For operation_type = LOAD**<br /><br />APPEND<br /><br />RELOAD<br /><br />UPSERT<br /><br />**For operation_type = RESTORE**<br /><br />DATABASE<br /><br />HEADER_ONLY|  
|database_name|**nvarchar(255)**|Name of the database that is the context of this operation||  
|table_name|**nvarchar(255)**|Information not available.||  
|Principal_id|**int**|ID of the user requesting the operation.||  
|session_id|**nvarchar(32)**|ID of the session performing the operation.|See session_id in [sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md).|  
|request_id|**nvarchar(32)**|ID of the request performing the operation. For loads, this is the current or last request associated with this load..|See request_id in [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md).|  
|status|**nvarchar(16)**|Status of the run.|'CANCELLED','COMPLETED','FAILED','QUEUED','RUNNING'|  
|progress|**int**|Percentage completed.|0 to 100|  
|command|**nvarchar(4000)**|Full text of the command submitted by the user.|Will be truncated if longer than 4000 characters (counting spaces).|  
|rows_processed|**bigint**|Number of rows processed as part of this operation.||  
|rows_rejected|**bigint**|Number of rows rejected as part of this operation.||  
|rows_inserted|**bigint**|Number of rows inserted into the database table(s) as part of this operation.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
