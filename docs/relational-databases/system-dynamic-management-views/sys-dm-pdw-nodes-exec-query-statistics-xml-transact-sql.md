---
title: "sys.dm_pdw_nodes_exec_query_statistics_xml  (Transact-SQL) | Microsoft Docs"
description: Dynamic management view that returns query execution plan for in-flight requests. Use this DMV to retrieve showplan XML with transient statistics.
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

# dm_pdw_nodes_exec_query_statistics_xml (Transact-SQL)
[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Returns query execution plan for in-flight requests. Use this DMV to retrieve showplan XML with transient statistics.

## Table returned

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|
|node_id|**int**|Unique numeric ID associated with the node.|
|session_id|**smallint**|ID of the session. Not nullable.|
|request_id|**int**|ID of the request. Not nullable.|
|sql_handle|**varbinary(64)**|Is a token that uniquely identifies the batch or stored procedure that the query is part of. Nullable.|
|plan_handle|**varbinary(64)**|Is a token that uniquely identifies a query execution plan for a batch that is currently executing. Nullable.|
|query_plan|**xml**|Contains the runtime Showplan representation of the query execution plan that is specified with *plan_handle* containing partial statistics. The Showplan is in XML format. One plan is generated for each batch that contains, for example ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, stored procedure calls, and user-defined function calls. Nullable.|

## Remarks
The same remarks in [sys.dm_exec_query_statistics_xml](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql?view=sql-server-ver15) apply.   

## Permissions  
 Requires `VIEW SERVER STATE` permission on the server.  

## See also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  

 ## Next steps
 For more development tips, see [SQL Data Warehouse development overview](https://docs.microsoft.com/azure/sql-data-warehouse/sql-data-warehouse-overview-develop).

