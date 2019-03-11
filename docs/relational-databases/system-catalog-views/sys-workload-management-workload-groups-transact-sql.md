---
title: "sys.workload_management_workload_groups (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/08/2019"
ms.prod: ""
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: "ronortloff"
ms.author: "rortloff"
manager: craigg
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.workload_management_workload_groups (Transact-SQL) (Preview)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Returns information for each workload group.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|
|group_id|**int**|Unique ID of the workload group. Is not nullable.||
|name|**sysname**|Name of the workload group. Must be unique to the instance. Is not nullable.||
|importance|**sysname**|Is the relative importance of a request in this workload group and across workload groups for shared resources. Is not nullable.|low, below_normal, normal, above_normal, high|
|min_percentage_resource|**tinyint**|Guaranteed amount of resources for all requests in the workload group.  Resources are not shared with other resource pools. Is not nullable.||
|cap_percentage_resource|**tinyint**|Hard cap on the resource percentage allocation for all requests in the workload group. Limits the maximum resources allocated to the specified level. The allowed range for value is from 1 through 100.||
|request_min_resource_grant_percent|**decimal (5,2)**|The minimum amount of resources allocated to a request. This value is a decimal with a default value of 10.  The allowed range for this value is from 0.08 to 100.</br>More resources may be allocated to the request based on availability of unreserved resources, the value of cap_percentage_resource and the number of requests executing in the workload group.||
|query_execution_timeout_sec|**int**|The amount of execution time, in seconds, allowed before the query is canceled.  Queries can't be canceled once they've reached the return phase of execution.  query_execution_timeout_sec doesn't include time spent queued.||
|query_wait_timeout_sec|**int**|The amount of time a request can queue, in seconds, before it's canceled.||
|create_time|**datetime**|Time the workload group was created||
|modify_time|**datetime**|Time the workload group was last modified||
  
## Permissions

Requires Control Database permission.

## Next steps

 For a list of all the catalog views for SQL Data Warehouse and Parallel Data Warehouse, see [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md). To create a workload classifier, see [CREATE WORKLOAD CLASSIFIER](../../t-sql/statements/create-workload-classifier-transact-sql.md) For more information on classification and importance see [SQL Data Warehouse Workload Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification) and [SQL Data Warehouse Workload Importance](/azure/sql-data-warehouse/sql-data-warehouse-workload-importance)
