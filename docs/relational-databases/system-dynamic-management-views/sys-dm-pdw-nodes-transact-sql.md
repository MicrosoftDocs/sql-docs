---
title: "sys.dm_pdw_nodes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: ""
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 93966909-d758-4d50-950b-f5066d104fa6
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.dm_pdw_nodes (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Holds information about all of the nodes in [!INCLUDE[ssAPS](../../includes/ssaps-md.md)]. It lists one row per node in the appliance.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|pdw_node_id|**int**|Unique numeric id associated with the node.<br /><br /> Key for this view.|Unique across the appliance, regardless of type.|  
|type|**nvarchar(32)**|Type of the node.|'COMPUTE', 'CONTROL',  'MANAGEMENT'|  
|name|**nvarchar(32)**|Logical name of the node.|Any string of appropriate length.|  
|address|**nvarchar(32)**|IP address of this node.|In the format of [0-255].[0-255].[0-255].[0-255].|  
|is_passive|**int**|Indicates whether the virtual machine running the node is running on the assigned server or has failed over to the spare server.|0 - node VM is running on the original server.<br /><br /> 1 - node VM is running on the spare server.|  
|region|**nvarchar(32)**|The region where the node is running.|'PDW', 'HDINSIGHT'|  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
