---
title: "sys.dm_exec_compute_nodes (Transact-SQL)"
description: sys.dm_exec_compute_nodes (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "DM_EXEC_COMPUTE_NODES_TSQL"
  - "DM_EXEC_COMPUTE_NODES"
  - "SYS.DM_EXEC_COMPUTE_NODES_TSQL"
helpviewer_keywords:
  - "sys.dm_exec_compute_nodes management view"
  - "PolyBase, views"
  - "PolyBase management views"
  - "dm_exec_compute_nodes management view"
dev_langs:
  - "TSQL"
ms.assetid: 0de4b7a4-401f-4e2d-9ab0-c54587e05154
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_compute_nodes (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Holds information about nodes used with PolyBase data management. It lists one row per node.  
  
 Use this DMV to see the list of all nodes in the scale-out cluster with their role, name and IP address.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|compute_node_id|**int**|Unique numeric id associated with the node. Key for this view.|Unique across scale-out cluster regardless of type.|  
|type|**nvarchar(32)**|Type of the node.|'COMPUTE', 'HEAD'|  
|name|**nvarchar(32)**|Logical name of the node.|Any string of appropriate length.|  
|address|**nvarchar(32)**|IP address of this node.|IP address range|  
  
## See Also  
 [PolyBase troubleshooting with dynamic management views](/previous-versions/sql/sql-server-2016/mt146389(v=sql.130))   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
