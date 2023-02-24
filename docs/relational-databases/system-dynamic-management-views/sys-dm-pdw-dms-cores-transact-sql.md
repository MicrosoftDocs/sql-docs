---
title: "sys.dm_pdw_dms_cores (Transact-SQL)"
description: sys.dm_pdw_dms_cores (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: b3f09b15-0863-4418-9347-a4f5fd2ab7c7
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_dms_cores (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds information about all DMS services running on the Compute nodes of the appliance. It lists one row per service instance, which is currently one row per node.

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|dms_core_id|**int**|Unique numeric id associated with this DMS core.<br /><br /> Key for this view.|Set to the pdw_node_id of the node that this DMS core is running on.|  
|pdw_node_id|**int**|ID of the node on which this DMS service is running.|See node_id in [sys.dm_pdw_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-transact-sql.md).|  
|status|**nvarchar(32)**|Current status of the DMS service.|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|  
  
 For information about the maximum rows retained by this view, see the Metadata section in the [Capacity limits](/azure/sql-data-warehouse/sql-data-warehouse-service-capacity-limits#metadata) topic.  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
