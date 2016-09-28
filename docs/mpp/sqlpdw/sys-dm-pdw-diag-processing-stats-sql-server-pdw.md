---
title: "sys.dm_pdw_diag_processing_stats (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a0c47504-4be3-4477-88ec-71690a88efb9
caps.latest.revision: 8
author: BarbKess
---
# sys.dm_pdw_diag_processing_stats (SQL Server PDW)
Displays information related to all internal diagnostic events that could be incorporated into diagnostic sessions defined by the administrator. Query this view to understand the statistics behind the diagnostics and eventing subsystems that drive the population of all the other DMVs. There are a group of queues for each process on each node.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**pdw_node_id**|**int**|Appliance node this log is from.|  
|**process_id**|**int**|Identifier of the process running submitting this statistic.|  
|**target_name**|**nvarchar(255)**|The name of the queue.|  
|**queue_size**|**int**|The number of items in the process queue. The queue size is usually 0. A positive number indicates that the system is under stress and is building backlog of events. A positive count in the other columns means system has become corrupted for that particular queue and any related DMVs.|  
|**lost_events_count**|**bigint**|The number of events lost.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
