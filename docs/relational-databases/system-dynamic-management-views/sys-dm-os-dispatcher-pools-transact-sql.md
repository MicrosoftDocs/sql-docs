---
description: "sys.dm_os_dispatcher_pools (Transact-SQL)"
title: "sys.dm_os_dispatcher_pools (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/18/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
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
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">= sql-server-2016 || >= sql-server-linux-2017|| >= aps-pdw-2016 || = azure-sqldw-latest"
---
# sys.dm_os_dispatcher_pools (Transact-SQL)
[!INCLUDE [sql-asa-pdw](../../includes/applies-to-version/sql-asa-pdw.md)]

Returns information about session dispatcher pools. Dispatcher pools are thread pools used by system components to perform background processing.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_dispatcher_pools**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
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
On SQL Database Basic, S0, and S1 service objectives, and for databases in elastic pools, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account or the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account is required. On all other SQL Database service objectives, the `VIEW DATABASE STATE` permission is required in the database.   

## See Also  
  
