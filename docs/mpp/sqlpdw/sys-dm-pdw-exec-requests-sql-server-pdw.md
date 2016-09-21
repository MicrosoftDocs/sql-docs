---
title: "sys.dm_pdw_exec_requests (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a7c68dd6-1191-49fb-a77e-dddb1c0a32f2
caps.latest.revision: 18
author: BarbKess
---
# sys.dm_pdw_exec_requests (SQL Server PDW)
Holds information about all requests currently or recently active in SQL Server PDW. It lists one row per request/query.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|request_id|**nvarchar(32)**|Key for this view. Unique numeric id associated with the request.|Unique across all requests in the system.|  
|session_id|**nvarchar(32)**|Unique numeric id associated with the session in which this query was run. See [sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md).||  
|status|**nvarchar(32)**|Current status of the request.|'Pending', 'Authorizing', 'AcquireSystemResources', 'Initializing', 'Plan', 'Parsing', 'AquireResources', 'Running', 'Cancelling', 'Complete', 'Failed', 'Cancelled'.|  
|submit_time|**datetime**|Time at which the request was submitted for execution.|Valid **datetime** smaller or equal to the current time and start_time.|  
|start_time|**datetime**|Time at which the request execution was started.|0 for queued requests; otherwise, valid **datetime** smaller or equal to current time.|  
|end_compile_time|**datetime**|Time at which the engine completed compiling the request.|0 for requests that have not been compiled yet; otherwise a valid **datetime** greater than start_time and less than or equal to the current time.|  
|end_time|**datetime**|Time at which the request execution completed, failed, or was cancelled.|Null for queued or active requests; otherwise, a valid **datetime** smaller or equal to current time.|  
|total_elapsed_time|**int**|Time elapsed in execution since the request was started, in milliseconds.|Between 0 and the difference between start_time and end_time.<br /><br />If total_elapsed_time exceeds the maximum value for an integer, total_elapsed_time will continue to be the maximum value. This condition will generate the warning “The maximum value has been exceeded.”<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
|label|**nvarchar(255)**|Optional label string associated with some SELECT query statements.|Any string containing 'a-z','A-Z','0-9','_'.|  
|error_id|**nvarchar(36)**|Unique id of the error associated with the request, if any.|See [sys.dm_pdw_errors &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-errors-sql-server-pdw.md); set to NULL if no error occurred.|  
|database_id|**int**|Identifier of database used by explicit context (e.g., USE DB_X).|See id in [sys.databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-databases-sql-server-pdw.md).|  
|command|**nvarchar(4000)**|Holds the full text of the request as submitted by the user.|Any valid query or request text. Queries that are longer than 4000 bytes are truncated.|  
|resource_class|**nvarchar(20)**|The resource class for this request. See related **concurrency_slots_used** in [sys.dm_pdw_resource_waits &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-resource-waits-sql-server-pdw.md).|SmallRC<br /><br />MediumRC<br /><br />LargeRC<br /><br />XLargeRC|  
  
For information about the maximum rows retained by this view, see the Maximum System View Values section in the [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md) topic.  
  
## Permissions  
Requires VIEW SERVER STATE permission.  
  
## Security  
sys.dm_pdw_exec_requests does not filter query results according to database-specific permissions. Logins with VIEW SERVER STATE permission can obtain results query results for all databases  
  
> [!WARNING]  
> An attacker can use sys.dm_pdw_exec_requests to retrieve information about specific database objects by simply having VIEW SERVER STATE permission and by not having a database-specific permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
