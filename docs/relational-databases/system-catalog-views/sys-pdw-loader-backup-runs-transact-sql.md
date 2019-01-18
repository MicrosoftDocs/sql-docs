---
title: "sys.pdw_loader_backup_runs (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: ""
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 2b72034c-6a11-46b9-a76c-7a88b2bea360
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_loader_backup_runs (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Contains information about ongoing and completed backup and restore operations in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], and about ongoing and completed backup, restore, and load operations in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]. The information persists across system restarts.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|run_id|**int**|Unique identifier for a specific backup, restore, or load run.<br /><br /> Key for this view.||  
|name|**nvarchar(255)**|Null for load. Optional name for backup or restore.||  
|submit_time|**datetime**|Time the request was submitted.||  
|start_time|**datetime**|Time the operation started.||  
|end_time|**datetime**|Time the operation completed, failed, or was cancelled.||  
|total_elapsed_time|**int**|Total time elapsed between start_time and current time, or between start_time and end_time for completed, cancelled, or failed runs.|If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br /> The maximum value in milliseconds is equivalent to 24.8 days.|  
|operation_type|**nvarchar(16)**|The load type.|'BACKUP', 'LOAD', 'RESTORE'|  
|mode|**nvarchar(16)**|The mode within the run type.|For operation_type = **BACKUP**<br />**DIFFERENTIAL**<br />**FULL**<br /><br /> For operation_type = **LOAD**<br />**APPEND**<br />**RELOAD**<br />**UPSERT**<br /><br /> For operation_type = **RESTORE**<br />**DATABASE**<br />**HEADER_ONLY**|  
|database_name|**nvarchar(255)**|Name of the database that is the context of this operation||  
|table_name|**nvarchar(255)**|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]||  
|Principal_id|**int**|ID of the user requesting the operation.||  
|session_id|**nvarchar(32)**|ID of the session performing the operation.|See session_id in [sys.dm_pdw_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md).|  
|request_id|**nvarchar(32)**|ID of the request performing the operation. For loads, this is the current or last request associated with this load..|See request_id in [sys.dm_pdw_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md).|  
|status|**nvarchar(16)**|Status of the run.|'CANCELLED','COMPLETED','FAILED','QUEUED','RUNNING'|  
|progress|**int**|Percentage completed.|0 to 100|  
|command|**nvarchar(4000)**|Full text of the command submitted by the user.|Will be truncated if longer than 4000 characters (counting spaces).|  
|rows_processed|**bigint**|Number of rows processed as part of this operation.||  
|rows_rejected|**bigint**|Number of rows rejected as part of this operation.||  
|rows_inserted|**bigint**|Number of rows inserted into the database table(s) as part of this operation.||  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
