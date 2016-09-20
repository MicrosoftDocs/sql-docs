---
title: "sys.dm_pdw_errors (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 41e7340e-692c-49fc-8997-301047d97cdc
caps.latest.revision: 16
author: BarbKess
---
# sys.dm_pdw_errors (SQL Server PDW)
Holds information about all errors encountered during execution of a request or query.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|error_id|**nvarchar(36)**|Key for this view.<br /><br />Unique numeric id associated with the error.|Unique across all query errors in the system.|  
|source|**nvarchar(64)**|Information not available.|Information not available.|  
|type|**nvarchar(4000)**|Type of error that occurred.|Information not available.|  
|create_time|**datetime**|Time at which the error occurred.|Smaller or equal to current time.|  
|pwd_node_id|**int**|Identifier of the specific node involved, if any. For additional information on node ids, see [sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).||  
|session_id|**nvarchar(32)**|Identifier of the session involved, if any. For additional information on session ids, see  [sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md).||  
|request_id|**nvarchar(32)**|Identifier of the request involved, if any. For additional information on request ids, see [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md).||  
|spid|**int**|spid of the SQL Server session involved, if any.||  
|thread_id|**int**|Information not available.||  
|details|**nvarchar(4000)**|Holds the full error text description.||  
  
For information about the maximum rows retained by this view, see the Maximum System View Values section in the [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/minimum-and-maximum-values-sql-server-pdw.md) topic.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
