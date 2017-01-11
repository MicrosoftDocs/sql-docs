---
title: "sys.dm_pdw_os_performance_counters (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d1d7ba59-d58e-47f2-b091-88d8b19e30dc
caps.latest.revision: 7
author: BarbKess
---
# sys.dm_pdw_os_performance_counters (SQL Server PDW)
Contains information about Windows performance counters for the nodes in SQL Server PDW.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|pdw_node_id|**int**|The ID of the node that contains the counter.<br /><br />pdw_node_id and counter_name form the key for this view.|See node_id in sys.[sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).|  
|counter_name|**nvarchar(255)**|Name of Windows performance counter.||  
|counter_category|**nvarchar(255)**|Name of Windows performance counter category.||  
|instance_name|**nvarchar(255)**|Name of the specific instance of the counter.||  
|counter_value|**Decimal(38,10)**|Current value of the counter.||  
|last_update_time|**Datetime2(3)**|Timestamp of last time the value was updated.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
