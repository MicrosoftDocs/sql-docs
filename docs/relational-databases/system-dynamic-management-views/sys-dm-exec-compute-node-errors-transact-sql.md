---
title: "sys.dm_exec_compute_node_errors (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "SYS.DM_EXEC_COMPUTE_NODE_ERRORS_TSQL"
  - "DM_EXEC_COMPUTE_NODE_ERRORS"
  - "DM_EXEC_COMPUTE_NODE_ERRORS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PolyBase"
  - "PolyBase, views"
  - "dm_exec_compute_node_errors"
  - "sys.dm_exec_compute_node_errors management view"
ms.assetid: 9a03c039-70e4-4974-95d8-d3fa45984ffb
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_compute_node_errors (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Returns errors that occur on PolyBase compute nodes.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|error_id|**nvarchar(36)**|Unique numeric id associated with the error .|Unique across all query errors in the system|  
|source|**nvarchar(255)**|Source thread or process description||  
|type|**nvarchar(255)**|Type of error.||  
|create_time|**datetime**|The time of the error occurrence||  
|compute_node_id|**int**|Identifier of the specific compute node|See compute_node_id of [sys.dm_exec_compute_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md)|  
|rexecution_id|**nvarchar(36)**|Identifier of the PolyBase query, if any.||  
|spid|**int**|Identifier of the SQL Server session||  
|thread_id|**int**|Numeric identifier of the thread on which the error occurred.||  
|details|nvarchar(4000)|Full description of the details of the error.||  
  
## See Also  
 [PolyBase troubleshooting with dynamic management views](https://msdn.microsoft.com/library/ce9078b7-a750-4f47-b23e-90b83b783d80)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
  
