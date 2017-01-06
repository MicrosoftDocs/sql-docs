---
title: "sys.dm_pdw_dms_external_work (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: cc198c7a-9443-4a98-b05d-038548e7407a
caps.latest.revision: 4
author: BarbKess
---
# sys.dm_pdw_dms_external_work (SQL Server PDW)
SQL Server PDW system view that holds information about all Data Movement Service (DMS) steps for external operations.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|request_id|**nvarchar(32)**|Query that is using this DMS worker.<br /><br />request_id, step_index, and dms_step_index form the key for this view.|Same as request_id in [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md)|  
|step_index|**int**|Query step that is invoking this DMS worker.<br /><br />request_id, step_index, and dms_step_index form the key for this view.|Same as step_index in [sys.dm_pdw_request_steps &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-request-steps-sql-server-pdw.md)|  
|dms_step_index|**int**|Current step in the DMS plan.<br /><br />request_id, step_index, and dms_step_index form the key for this view.|Same as dms___step_index in[sys.dm_pdw_dms_workers &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-dms-workers-sql-server-pdw.md)|  
|pdw_node_id|**int**|Node that is running the DMS worker.|Same as node_id in [sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).|  
|type|**nvarchar(60)**|Type of external operation this node is running.<br /><br />FILE SPLIT is an operation on an external Hadoop file that has been split into multiple smaller falls.|'FILE SPLIT'|  
|work_id|**int**|The file split ID.|Greater than or equal to 0.<br /><br />Unique per Compute node.|  
|input_name|**nvarchar(60)**|String name for the input being read.|For a Hadoop file, this is the Hadoop file name.|  
|read_location|**bigint**|Offset of read location.||  
|estimated_bytes_processed|**bigint**|Number of bytes processed by this worker.|Greater than or equal to 0.|  
|length|**bigint**|Number of bytes in the file split.<br /><br />For Hadoop, this is the size of the HDFS block.|User-defined. The default is 64 MB.|  
|status|**nvarchar(32)**|State of the worker.|Pending, Processing, Done, Failed, Aborted|  
|start_time|**datetime**|Time at which execution of this worker started.|Greater than or equal to start time of the query step this worker belongs to. See [sys.dm_pdw_request_steps &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-request-steps-sql-server-pdw.md).|  
|end_time|**datetime**|Time at which execution ended, failed, or was cancelled.|NULL for ongoing or queued workers. Otherwise, greater than start_time.|  
|total_elapsed_time|**int**|Total time spent in execution, in milliseconds.|Greater than or equal to 0.<br /><br />If total_elapsed_time exceeds the maximum value for an integer, total_elapsed_time will continue to be the maximum value. This condition will generate the warning “The maximum value has been exceeded.”<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
  
For information about the maximum rows retained by this view, see [Maximum System View Values](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md#SystemViews).  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
