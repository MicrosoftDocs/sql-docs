---
description: "sys.dm_pdw_exec_requests (Transact-SQL)"
title: "sys.dm_pdw_exec_requests (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 11/05/2019
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 390225cc-23e8-4051-a5f6-221e33e4c0b4
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.dm_pdw_exec_requests (Transact-SQL)

[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds information about all requests currently or recently active in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. It lists one row per request/query.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|request_id|**nvarchar(32)**|Key for this view. Unique numeric ID associated with the request.|Unique across all requests in the system.|  
|session_id|**nvarchar(32)**|Unique numeric ID associated with the session in which this query was run. See [sys.dm_pdw_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md).||  
|status|**nvarchar(32)**|Current status of the request.|'Running', 'Suspended', 'Completed', 'Canceled', 'Failed'.|  
|submit_time|**datetime**|Time at which the request was submitted for execution.|Valid **datetime** smaller or equal to the current time and start_time.|  
|start_time|**datetime**|Time at which the request execution was started.|NULL for queued requests; otherwise, valid **datetime** smaller or equal to current time.|  
|end_compile_time|**datetime**|Time at which the engine completed compiling the request.|NULL for requests that haven't been compiled yet; otherwise a valid **datetime** less than start_time and less than or equal to the current time.|
|end_time|**datetime**|Time at which the request execution completed, failed, or was canceled.|Null for queued or active requests; otherwise, a valid **datetime** smaller or equal to current time.|  
|total_elapsed_time|**int**|Time elapsed in execution since the request was started, in milliseconds.|Between 0 and the difference between submit_time and end_time.</br></br> If total_elapsed_time exceeds the maximum value for an integer, total_elapsed_time will continue to be the maximum value. This condition will generate the warning "The maximum value has been exceeded."</br></br> The maximum value in milliseconds is the same as 24.8 days.|  
|label|**nvarchar(255)**|Optional label string associated with some SELECT query statements.|Any string containing 'a-z','A-Z','0-9','_'.|  
|error_id|**nvarchar(36)**|Unique ID of the error associated with the request, if any.|See [sys.dm_pdw_errors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-errors-transact-sql.md); set to NULL if no error occurred.|  
|database_id|**int**|Identifier of database used by explicit context (for example, USE DB_X).|See ID in [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).|  
|command|**nvarchar(4000)**|Holds the full text of the request as submitted by the user.|Any valid query or request text. Queries that are longer than 4000 bytes are truncated.|  
|resource_class|**nvarchar(20)**|The workload group used for this request. |Static Resource Classes</br>staticrc10</br>staticrc20</br>staticrc30</br>staticrc40</br>staticrc50</br>staticrc60</br>staticrc70</br>staticrc80</br>            </br>Dynamic Resource Classes</br>SmallRC</br>MediumRC</br>LargeRC</br>XLargeRC|
|importance|**nvarchar(128)**|The Importance setting the request executed at.  This is the relative importance of a request in this workload group and across workload groups for shared resources.  Importance specified in the classifier overrides the workload group importance setting.</br>Applies to: Azure SQL Data Warehouse|NULL</br>low</br>below_normal</br>normal (default)</br>above_normal</br>high|
|group_name|**sysname** |For requests utilizing resources, group_name is the name of the workload group the request is running under.  If the request does not utilize resources, group_name is null.</br>Applies to: Azure SQL Data Warehouse|
|classifier_name|**sysname**|For requests utilizing resources, The name of the classifier used for assigning resources and importance.||
|resource_allocation_percentage|**decimal(5,2)**|The percentage amount of resources allocated to the request.</br>Applies to: Azure SQL Data Warehouse|
|result_cache_hit|**int**|Details whether a completed query used result set cache.  </br>Applies to: Azure SQL Data Warehouse| 1 = Result set cache hit </br> 0 = Result set cache miss </br> Negative values = Reasons why result set caching was not used.  See remarks section for details.|
||||
  
## Remarks 
 For information about the maximum rows retained by this view, see the Metadata section in the [Capacity limits](/azure/sql-data-warehouse/sql-data-warehouse-service-capacity-limits#metadata) topic.

 The result_cache_hit is a bitmask of a query's use of result set cache.  This column can be the [| (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) product of one or more of these values:  
  
|Value Hex (Decimal)|Description|  
|-----------|-----------------|  
|**1**|Result set cache hit|  
|**0x00** (**0**)|Result set cache miss|  
|-**0x01** (**-1**)|Result set caching is disabled on the database.|  
|-**0x02** (**-2**)|Result set caching is disabled on the session. | 
|-**0x04** (**-4**)|Result set caching is disabled due to no data sources for the query.|  
|-**0x08** (**-8**)|Result set caching is disabled due to row level security predicates.|  
|-**0x10** (**-16**)|Result set caching is disabled due to the use of system table, temporary table, or external table in the query.|  
|-**0x20** (**-32**)|Result set caching is disabled because the query contains runtime constants, user-defined functions, or non-deterministic functions.|  
|-**0x40** (**-64**)|Result set caching is disabled due to estimated result set size is >10GB.|  
|-**0x80** (**-128**)|Result set caching is disabled because the result set contains rows with large size (>64kb).|  
  
## Permissions

 Requires VIEW SERVER STATE permission.  
  
## Security

 sys.dm_pdw_exec_requests doesn't filter query results according to database-specific permissions. Logins with VIEW SERVER STATE permission can obtain results query results for all databases  
  
>[!WARNING]  
>An attacker can use sys.dm_pdw_exec_requests to retrieve information about specific database objects by simply having VIEW SERVER STATE permission and by not having database-specific permission.  
  
## See Also

 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)
