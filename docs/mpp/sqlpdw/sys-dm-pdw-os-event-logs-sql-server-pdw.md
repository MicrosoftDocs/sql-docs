---
title: "sys.dm_pdw_os_event_logs (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 93d91de8-a437-4d22-babc-67feb2b4b0a6
caps.latest.revision: 12
author: BarbKess
---
# sys.dm_pdw_os_event_logs (SQL Server PDW)
Holds information regarding the different Windows Event logs on the different nodes.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|pdw_node_id|**int**|Appliance node this log is from.<br /><br />pdw_node_id and log_name form the key for this view.||  
|log_name|**nvarchar(255)**|Windows event log name.<br /><br />pdw_node_id and log_name form the key for this view.||  
|log_source|**nvarchar(255)**|Windows event log source name.||  
|event_id|**int**|ID of the event. Not unique.||  
|event_type|**nvarchar(255)**|Type of the event, identifying severity.|'Information', 'Warning', 'Error'|  
|event_message|**nvarchar(4000)**|Details of the event.||  
|generate_time|**datetime**|Time the event was created.||  
|write_time|**datetime**|Time the event was actually written to the log.||  
  
For information about the maximum rows retained by this view, see the Maximum System View Values section in the [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/minimum-and-maximum-values-sql-server-pdw.md) topic.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
