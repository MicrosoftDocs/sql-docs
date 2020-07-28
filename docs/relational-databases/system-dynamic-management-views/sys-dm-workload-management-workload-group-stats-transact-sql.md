---
title: "sys.dm_workload_management_workload_groups_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/02/2019"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: ronortloff
ms.author: rortloff
monikerRange: "= azure-sqldw-latest||= sqlallproducts-allversions"
---
# sys.dm_workload_management_workload_groups_stats (Transact-SQL)
[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Returns workload group statistics and the effective values of the workload group in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|group_id|**int**|Unique ID of the workload group.||
|name|**sysname**|Name of the workload group.||
|statistics_start_time|**datetime**|Time that statistics collection began for the workload group.  The value  is either when the workload group was created or when instance is paused or scaled.||
|total_request_count|**bigint**|Cumulative count of completed requests in the workload group.||
|total_shared_resource_reqeusts|**bigint**|Cumulative count of completed requests in the workload group that used resources from the shared pool.||
|total_queued_request_count|**bigint**|Cumulative count of requests queued after the max_concurrency limit was reached.||
|total_request_execution_timeouts|**bigint**|Cumulative count of requests in the workload group that timed out before completion based on the query_execution_timeout_sec setting.||
|effective_min_percentage_resource|**tinyint**|The effective min_percentage_resource setting allowed considering the service level and the workload group settings. The effective min_percentage_resource can be adjusted higher on lower service levels.  For example, on DW100c, the lowest min_percentage_resource allowable is 25%.  The min_percentage_resource is adjusted to 0% if the value cannot be granted at the service level.  For example, min_percentage_resource set to 10% at DW6000c, would have an effective_min_percentage_resource of 0% when scaled down to DW100c.||
|effective_cap_percentage_resource|**tinyint**|The effective cap_percentage_resource for the workload group.  If there are other workload groups with min_percentage_resource > 0, the effective_cap_percentage_resource is lowered proportionally.||
|effective_request_min_resource_grant_percent|**decimal(5,2)**|The effective runtime value for request_min_resource_grant_percent of the workload group. The effective value considering the service level and how the workload group is configured.  If min_percentage_resource is adjusted because of the service level, effective_request_min_resource_grant_percent will be adjusted accordingly.||
|effective_request_max_resource_grant_percent|**decimal(5,2)**|The effective runtime value for request_max_resource_grant_percent of the workload group considering the configuration of the all workload groups.||
|||||

## See also

 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
