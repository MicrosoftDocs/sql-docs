---
title: "sys.dm_exec_dms_services (Transact-SQL)"
description: sys.dm_exec_dms_services (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/04/2019
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "DM_EXEC_DMS_SERVICES_TSQL"
  - "SYS.DM_EXEC_DMS_SERVICES_TSQL"
  - "DM_EXEC_DMS_SERVICES"
helpviewer_keywords:
  - "PolyBase,views"
  - "PolyBase"
  - "dm_exec_dms_services management view"
  - "sys.dm_exec_dms_services management view"
dev_langs:
  - "TSQL"
ms.assetid: 6ac47eef-4293-46b8-8555-07a614837504
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_dms_services (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Holds information about all of the DMS services running on the PolyBase compute nodes. It lists one row per service instance.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|dms_core_id|`int`|Unique numeric id associated with the DMS core. Key for this view.|Unique ID.|  
|compute_node_id|`int`|ID of the node on which this DMS service is running|See *compute_node_id* in [sys.dm_exec_compute_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md).|  
|status|`nvarchar(32)`|Current status of the DMS service||
|compute_pool_id|`int`|Unique identifier for the pool.|

## See Also  
 [PolyBase troubleshooting with dynamic management views](/previous-versions/sql/sql-server-2016/mt146389(v=sql.130))   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
