---
title: "sys.pdw_distributions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 267ed230-637a-425c-94b9-8a7d190bdafe
caps.latest.revision: 13
author: BarbKess
---
# sys.pdw_distributions (SQL Server PDW)
Holds information about the distributions on the appliance. It lists one row per appliance distribution.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|distribution_id|**int**|Unique numeric id associated with the distribution.<br /><br />Key for this view.|1 to the number of Compute nodes in appliance multiplied by the number of distributions per Compute node.|  
|pdw_node_id|**int**|ID of the node this distribution is on.|See pdw_node_id in [sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).|  
|name|**nvarchar(32)**|String identifier associated with the distribution, used as a suffix on distributed tables.|String composed of 'A-Z','a-z','0-9','_','-'.|  
|position|**int**|Position of the distribution within a node respective to other distributions on that node.|1 to the number of distributions per node.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
