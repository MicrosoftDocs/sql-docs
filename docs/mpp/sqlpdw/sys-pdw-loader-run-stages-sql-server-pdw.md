---
title: "sys.pdw_loader_run_stages (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0c27f341-a8ce-40d2-b637-9d1ff6f9bf68
caps.latest.revision: 19
author: BarbKess
---
# sys.pdw_loader_run_stages (SQL Server PDW)
Contains information about ongoing and completed load operations in SQL Server PDW. The information persists across system restarts.  
  
|||||  
|-|-|-|-|  
|Column Name|Data Type|Description|Range|  
|run_id|**int**|Unique identifier of a loader run.||  
|stage|**nvarchar(30)**|The current stage for the run.|'CREATE_STAGING', 'DMS_LOAD', 'LOAD_INSERT', 'LOAD_CLEANUP'|  
|request_id|**nvarchar(32)**|ID of the request running this stage.||  
|status|**nvarchar(16)**|Status of this phase.||  
|start_time|**datetime**|Time at which the stage was started.||  
|end_time|**datetime**|Time at which the stage ended, if any.|NULL if not started or in progress.|  
|total_elapsed_time|**int**|Total time this stage spent (or spent so far) running.|If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
