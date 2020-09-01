---
title: "sys.dm_pdw_nodes_exec_query_plan (Transact-SQL) | Microsoft Docs"
description: Dynamic management view that returns the Showplan in XML format for the batch specified by the plan handle. The plan specified by the plan handle can either be cached or currently executing.  
ms.custom: ""
ms.date: "10/14/2019"
ms.prod: sql 
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "=azure-sqldw-latest || = sqlallproducts-allversions"
---

# sys.pdw_nodes_dm_exec_query_plan (Transact-SQL)
[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Returns the Showplan in XML format for the batch specified by the plan handle. The plan specified by the plan handle can either be cached or currently executing.  

## Table returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pdw_node_id**|**int**|Unique numeric ID associated with the node.| 
|**dbid**|**smallint**|ID of the context database that was in effect when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement corresponding to this plan was compiled. For unplanned and prepared SQL statements, the ID of the database where the statements were compiled.<br /><br /> Column is nullable.|  
|**objectid**|**int**|ID of the object (for example, stored procedure or user-defined function) for this query plan. For ad hoc and prepared batches, this column is **null**.<br /><br /> Column is nullable.|  
|**number**|**smallint**|Numbered stored procedure integer. For ad hoc and prepared batches, this column is **null**.<br /><br /> Column is nullable.| 
|**encrypted**|**bit**|Indicates whether the corresponding stored procedure is encrypted.<br /><br /> 0 = not encrypted<br /><br /> 1 = encrypted<br /><br /> Column is not nullable.|  
|**query_plan**|**xml**|Contains the compile-time Showplan representation of the query execution plan that is specified with *plan_handle*. The Showplan is in XML format. One plan is generated for each batch that contains, for example ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, stored procedure calls, and user-defined function calls.<br /><br /> Column is nullable.|  
  
## Remarks  
The same remarks in [sys.dm_exec_query_plan](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql?view=sql-server-ver15) apply.  
  
## Permissions  
 Require **sysadmin** server role or `VIEW SERVER STATE` permission on the server.  
  
## See also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  

 ## Next steps
 For more development tips, see [SQL Data Warehouse development overview](https://docs.microsoft.com/azure/sql-data-warehouse/sql-data-warehouse-overview-develop).