---
title: "sys.dm_pdw_network_credentials (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fe9d9e9b-64a4-44f5-8f32-a7d5d1d671e0
caps.latest.revision: 7
author: BarbKess
---
# sys.dm_pdw_network_credentials (SQL Server PDW)
Returns a list of all network credentials stored in the SQL Server PDW appliance for all target servers. Results are listed for the Control node, and every Compute node.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|pdw_node_id|**int**|Unique numeric id associated with the node.|  
|target_server_name|**nvarchar(32)**|IP address of the target server that SQL Server PDW will access by using the username and password credentials.|  
|username|**nvarchar(32)**|Username for which the password is stored.|  
|last_modified|**datetime**|Datetime of the last operation that modified the credential.|  
  
## Permissions  
Requires VIEW SERVER STATE.  
  
## General Remarks  
The key for this dynamic management view is *pdw_node_id* plus *target_server_name*.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
