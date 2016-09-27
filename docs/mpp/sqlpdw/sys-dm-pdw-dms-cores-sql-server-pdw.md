---
title: "sys.dm_pdw_dms_cores (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 63b92444-6c5e-4af3-b94f-c7a13df7450d
caps.latest.revision: 16
author: BarbKess
---
# sys.dm_pdw_dms_cores (SQL Server PDW)
Holds information about all DMS services running on the Compute nodes of the appliance. It lists one row per service instance, which is currently one row per node.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|dms_core_id|**int**|Unique numeric id associated with this DMS core.<br /><br />Key for this view.|Set to the pdw_node_id of the node that this DMS core is running on.|  
|pdw_node_id|**int**|ID of the node on which this DMS service is running.|See node_id in [sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).|  
|status|**nvarchar(32)**|Current status of the DMS service.|Information not available.|  
  
For information about the maximum rows retained by this view, see the Maximum System View Values section in the [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md) topic.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
