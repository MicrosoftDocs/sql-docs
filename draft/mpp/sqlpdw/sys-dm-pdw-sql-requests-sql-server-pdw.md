---
title: "sys.dm_pdw_sql_requests (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 442e7df0-4885-49c8-8929-be5534955cd5
caps.latest.revision: 16
author: BarbKess
---
# sys.dm_pdw_sql_requests (SQL Server PDW)
Holds information about all SQL Server query distributions as part of a SQL step in the query.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|request_id|**nvarchar(32)**|Unique identifier of the query to which this SQL query distribution belongs.<br /><br />request_id, step_index, and distribution_id form the key for this view.|See request_id in [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md)|  
|step_index|**int**|Index of the query step this distribution is part of.<br /><br />request_id, step_index, and distribution_id form the key for this view.|See step_index in [sys.dm_pdw_request_steps &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-request-steps-sql-server-pdw.md)|  
|pwd_node_id|**int**|Unique identifier of the node on which this query distribution is run.|See node_id in [sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).|  
|distribution_id|**int**|Unique identifier of the distribution on which this query distribution is run.<br /><br />request_id, step_index, and distribution_id form the key for this view.|See distribution_id in [sys.pdw_distributions &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-distributions-sql-server-pdw.md). Set to -1 for requests that run at the node scope, not the distribution scope.|  
|status|**nvarchar(32)**|Current status of the query distribution.|'ACTIVE','CANCELLED','COMPLETED','FAILED','QUEUED'|  
|error_id|**nvarchar(36)**|Unique identifier of the error associated with this query distribution, if any.|See error_id in [sys.dm_pdw_errors &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-errors-sql-server-pdw.md). Set to NULL if no error occurred.|  
|start_time|**datetime**|Time at which query distribution started execution.|Smaller or equal to current time and greater or equal to start_time of the query step this query distribution belongs to|  
|end_time|**datetime**|Time at which this query distribution completed execution, was cancelled, or failed.|Greater or equal to start time, or set to NULL if the query distribution is ongoing or queued.|  
|total_elapsed_time|**int**|Represents the time the query distribution has been running, in milliseconds.|Greater or equal to 0. Equal to the delta of start_time and end_time for completed, failed, or cancelled query distributions.<br /><br />If total_elapsed_time exceeds the maximum value for an integer, total_elapsed_time will continue to be the maximum value. This condition will generate the warning “The maximum value has been exceeded.”<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
|row_count|**bigint**|Number of rows changed or read by this query distribution.|0 for query distributions that do not change or return data.|  
|spid|**int**|Session id on the SQL Server instance running the query distribution.||  
|command|**nvarchar(4000)**|Full text of command for this query distribution.|Any valid query or request string.|  
  
For information about the maximum rows retained by this view, see the Maximum System View Values section in the [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md) topic.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
