---
title: "sys.dm_pdw_nodes (Transact-SQL)"
description: sys.dm_pdw_nodes (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 93966909-d758-4d50-950b-f5066d104fa6
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_nodes (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds information about all of the nodes in [!INCLUDE[ssAPS](../../includes/ssaps-md.md)]. It lists one row per node in the appliance.

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|pdw_node_id|**int**|Unique numeric id associated with the node.<br /><br /> Key for this view.|Unique across the appliance, regardless of type.|  
|type|**nvarchar(32)**|Type of the node.|'COMPUTE', 'CONTROL',  'MANAGEMENT'|  
|name|**nvarchar(32)**|Logical name of the node.|Any string of appropriate length.|  
|address|**nvarchar(32)**|IP address of this node.|In the format of [0-255].[0-255].[0-255].[0-255].|  
|is_passive|**int**|Indicates whether the virtual machine running the node is running on the assigned server or has failed over to the spare server.|0 - node VM is running on the original server.<br /><br /> 1 - node VM is running on the spare server.|  
|region|**nvarchar(32)**|The region where the node is running.|'PDW', 'HDINSIGHT'|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
