---
title: "sys.dm_pdw_lock_waits (Transact-SQL)"
description: sys.dm_pdw_lock_waits (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 8ef966f8-d14e-40d3-9626-3508ada9b8fb
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_lock_waits (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds information about the requests that are waiting for locks.

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|wait_id|**bigint**|Position of the request in the waiting list.|0-based ordinal. This is not unique across all wait entries.|  
|session_id|**nvarchar(32)**|ID of the session in which the wait state occurred.|See session_id in [sys.dm_pdw_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md).|  
|type|**nvarchar(255)**|Type of wait this entry represents.|Possible values:<br /><br /> Shared<br /><br /> SharedUpdate<br /><br /> ExclusiveUpdate<br /><br /> Exclusive|  
|object_type|**nvarchar(255)**|Type of object that is affected by the wait.|Possible values:<br /><br /> OBJECT<br /><br /> DATABASE<br /><br /> SYSTEM<br /><br /> SCHEMA<br /><br /> APPLICATION|  
|object_name|**nvarchar(386)**|Name or GUID of the specified object that was affected by the wait.|Tables and views are displayed with three-part names.<br /><br /> Indexes and statistics are displayed with four-part names.<br /><br /> Names, principals, and databases are string names.|  
|request_id|**nvarchar(32)**|ID of the request on which the wait state occurred.|ID of the request.<br /><br /> This is a GUID for load requests.|  
|request_time|**datetime**|Time at which the lock or resource was requested.||  
|acquire_time|**datetime**|Time at which the lock or resource was acquired.||  
|state|**nvarchar(50)**|State of the wait state.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
|priority|**int**|Priority of the waiting item.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
