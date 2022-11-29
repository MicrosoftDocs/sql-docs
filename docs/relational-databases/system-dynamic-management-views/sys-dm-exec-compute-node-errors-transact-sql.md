---
title: "sys.dm_exec_compute_node_errors (Transact-SQL)"
description: sys.dm_exec_compute_node_errors (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/04/2019
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "SYS.DM_EXEC_COMPUTE_NODE_ERRORS_TSQL"
  - "DM_EXEC_COMPUTE_NODE_ERRORS"
  - "DM_EXEC_COMPUTE_NODE_ERRORS_TSQL"
helpviewer_keywords:
  - "PolyBase"
  - "PolyBase, views"
  - "dm_exec_compute_node_errors"
  - "sys.dm_exec_compute_node_errors management view"
dev_langs:
  - "TSQL"
ms.assetid: 9a03c039-70e4-4974-95d8-d3fa45984ffb
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_compute_node_errors (Transact-SQL)

[!INCLUDE [sqlserver2016-asdbmi](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

  Returns errors that occur on PolyBase compute nodes.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|error_id|`nvarchar(36)`|Unique numeric id associated with the error .|Unique across all query errors in the system|  
|source|`nvarchar(255)`|Source thread or process description||  
|type|`nvarchar(255)`|Type of error.||  
|create_time|`datetime`|The time of the error occurrence||  
|compute_node_id|`int`|Identifier of the specific compute node|See compute_node_id of [sys.dm_exec_compute_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md)|  
|rexecution_id|`nvarchar(36)`|Identifier of the PolyBase query, if any.||  
|spid|`int`|Identifier of the SQL Server session||  
|thread_id|`int`|Numeric identifier of the thread on which the error occurred.||  
|details|nvarchar(4000)|Full description of the details of the error.||
|compute_pool_id|`int`|Unique identifier for the pool.|

  
## See Also  
 [PolyBase troubleshooting with dynamic management views](/previous-versions/sql/sql-server-2016/mt146389(v=sql.130))   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
