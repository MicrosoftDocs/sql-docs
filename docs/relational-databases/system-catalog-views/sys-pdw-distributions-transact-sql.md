---
title: "sys.pdw_distributions (Transact-SQL)"
description: sys.pdw_distributions (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 572b5187-9753-4063-adf8-65dea87d11f8
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.pdw_distributions (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds information about the distributions on the appliance. It lists one row per appliance distribution.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|distribution_id|**int**|Unique numeric id associated with the distribution.<br /><br /> Key for this view.|1 to the number of Compute nodes in appliance multiplied by the number of distributions per Compute node.|  
|pdw_node_id|**int**|ID of the node this distribution is on.|See pdw_node_id in [sys.dm_pdw_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-transact-sql.md).|  
|name|**nvarchar(32)**|String identifier associated with the distribution, used as a suffix on distributed tables.|String composed of 'A-Z','a-z','0-9','_','-'.|  
|position|**int**|Position of the distribution within a node respective to other distributions on that node.|1 to the number of distributions per node.|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
