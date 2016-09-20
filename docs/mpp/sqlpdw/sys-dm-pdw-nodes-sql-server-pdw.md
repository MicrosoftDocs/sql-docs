---
title: "sys.dm_pdw_nodes (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b63948eb-c946-4874-b29e-d322152c1a64
caps.latest.revision: 14
author: BarbKess
---
# sys.dm_pdw_nodes (SQL Server PDW)
Holds information about all of the nodes in Analytics Platform System. It lists one row per node in the appliance.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|pdw_node_id|**int**|Unique numeric id associated with the node.<br /><br />Key for this view.|Unique across the appliance, regardless of type.|  
|type|**nvarchar(32)**|Type of the node.|'COMPUTE', 'CONTROL',  'MANAGEMENT'|  
|name|**nvarchar(32)**|Logical name of the node.|Any string of appropriate length.|  
|address|**nvarchar(32)**|IP address of this node.|In the format of [0-255].[0-255].[0-255].[0-255].|  
|is_passive|**int**|Indicates whether the virtual machine running the node is running on the assigned server or has failed over to the spare server.|0 – node VM is running on the original server.<br /><br />1 – node VM is running on the spare server.|  
|region|**nvarchar(32)**|The region where the node is running.|'PDW', 'HDINSIGHT'|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
