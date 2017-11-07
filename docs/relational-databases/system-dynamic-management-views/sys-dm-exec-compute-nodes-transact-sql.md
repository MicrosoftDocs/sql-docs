---
title: "sys.dm_exec_compute_nodes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DM_EXEC_COMPUTE_NODES_TSQL"
  - "DM_EXEC_COMPUTE_NODES"
  - "SYS.DM_EXEC_COMPUTE_NODES_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_compute_nodes management view"
  - "PolyBase, views"
  - "PolyBase management views"
  - "dm_exec_compute_nodes management view"
ms.assetid: 0de4b7a4-401f-4e2d-9ab0-c54587e05154
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.dm_exec_compute_nodes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Holds information about nodes used with PolyBase data management. It lists one row per node.  
  
 Use this DMV to see the list of all nodes in the scale-out cluster with their role, name and IP address.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|compute_node_id|**int**|Unique numeric id associated with the node. Key for this view.|Unique across scale-out cluster regardless of type.|  
|type|**nvarchar(32)**|Type of the node.|'COMPUTE', ‘HEAD’|  
|name|**nvarchar(32)**|Logical name of the node.|Any string of appropriate length.|  
|address|**nvarchar(32)**|P address of this node.|IP address range|  
  
## See Also  
 [PolyBase troubleshooting with dynamic management views](http://msdn.microsoft.com/library/ce9078b7-a750-4f47-b23e-90b83b783d80)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
  
