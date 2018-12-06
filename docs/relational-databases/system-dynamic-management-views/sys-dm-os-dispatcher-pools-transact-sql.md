---
title: "sys.dm_os_dispatcher_pools (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/18/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_dispatcher_pools_TSQL"
  - "dm_os_dispatcher_pools"
  - "sys.dm_os_dispatcher_pools"
  - "sys.dm_os_dispatcher_pools_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "extended events [SQL Server], views"
  - "sys.dm_os_dispatcher_pools DMV"
ms.assetid: b9edbc83-c6bc-4753-9bb5-a454cfe7d6bf
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_os_dispatcher_pools (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about session dispatcher pools. Dispatcher pools are thread pools used by system components to perform background processing.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_dispatcher_pools**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|dispatcher_pool_address|**varbinary(8)**|The address of the dispatcher pool. dispatcher_pool_address is unique. Is not nullable.|  
|type|**nvarchar(256)**|The type of the dispatcher pool. Is not nullable. There are two types of dispatcher pools:<br /><br /> DISP_POOL_XE_ENGINE<br /><br /> DISP_POOL_XE_SESSION<br /><br /> Query the DMV for the full list|  
|name|**nvarchar(256)**|The name of the dispatcher pool. Is not nullable.|  
|dispatcher_count|**int**|The number of active dispatcher threads. Is not nullable.|  
|dispatcher_ideal_count|**int**|The number of dispatcher threads that the dispatcher pool can grow to use. Is not nullable.|  
|dispatcher_timeout_ms|**int**|The time, in milliseconds, that a dispatcher will wait for new work before exiting. Is not nullable.|  
|dispatcher_waiting_count|**int**|The number of idle dispatcher threads. Is not nullable.|  
|queue_length|**int**|The number of work items waiting to be handled by the dispatcher pool. Is not nullable.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## See Also  
  
  


