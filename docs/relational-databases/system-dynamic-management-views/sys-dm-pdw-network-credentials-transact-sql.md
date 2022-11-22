---
title: "sys.dm_pdw_network_credentials (Transact-SQL)"
description: sys.dm_pdw_network_credentials (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: d4fee3ad-6285-4ea5-8513-5e6eb617abb0
monikerRange: ">=aps-pdw-2016"
---
# sys.dm_pdw_network_credentials (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

  Returns a list of all network credentials stored in the [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] appliance for all target servers. Results are listed for the Control node, and every Compute node.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|pdw_node_id|**int**|Unique numeric id associated with the node.|  
|target_server_name|**nvarchar(32)**|IP address of the target server that [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will access by using the username and password credentials.|  
|username|**nvarchar(32)**|Username for which the password is stored.|  
|last_modified|**datetime**|Datetime of the last operation that modified the credential.|  
  
## Permissions  
 Requires VIEW SERVER STATE.  
  
## General Remarks  
 The key for this dynamic management view is *pdw_node_id* plus *target_server_name*.  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
