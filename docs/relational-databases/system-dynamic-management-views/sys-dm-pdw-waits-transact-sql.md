---
title: "sys.dm_pdw_waits (Transact-SQL)"
description: sys.dm_pdw_waits (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 5130e498-1c77-4ae3-a80b-9aae396494e9
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_waits (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds information about all wait states encountered during execution of a request or query, including locks, waits on transmission queues, and so on.

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|wait_id|**bigint**|Unique numeric id associated with the wait state.<br /><br /> Key for this view.|Unique across all waits in the system.|  
|session_id|**nvarchar(32)**|ID of the session on which the wait state occurred.|See session_id in [sys.dm_pdw_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md).|  
|type|**nvarchar(255)**|Type of wait this entry represents.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
|object_type|**nvarchar(255)**|Type of object that is affected by the wait.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
|object_name|**nvarchar(386)**|Name or GUID of the specified object that was affected by the wait.||  
|request_id|**nvarchar(32)**|ID of the request on which the wait state occurred.|See request_id in [sys.dm_pdw_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md).|  
|request_time|**datetime**|Time at which the wait state was requested.||  
|acquire_time|**datetime**|Time at which the lock or resource was acquired.||  
|state|**nvarchar(50)**|State of the wait state.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
|priority|**int**|Priority of the waiting item.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)   
 [sys.dm_pdw_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-wait-stats-transact-sql.md)  
  
  
