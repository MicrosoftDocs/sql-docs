---
title: "sys.dm_pdw_component_health_status (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 68cc3f7a-693c-4d5d-a76b-455352af8d7f
caps.latest.revision: 6
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# sys.dm_pdw_component_health_status (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw_md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Holds information about the current health of appliance components.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|pdw_node_id|**int**||Not NULL|  
|component_id|int|The ID of the component. See [sys.pdw_health_components &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-health-components-transact-sql.md).<br /><br /> pdw_node_id, component_id, property_id, and component_instance_id form the key for this view.|Not NULL|  
|property_id|**int**|The ID of the property. See [sys.pdw_health_component_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-health-component-properties-transact-sql.md).|NOT NULL|  
|component_instance_id|**nvarchar(255)**|Identifies an instance of a component. For example, an instance of a CPU might be identified by component_instance_id='CPU1'.<br /><br /> pdw_node_id, component_id, property_id, and component_instance_id form the key for this view.|NOT NULL|  
|property_value|**nvarchar(255)**|The current property value.|NULL|  
|update_time|**datetime**|The last time the metric was updated.|NOT NULL|  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  