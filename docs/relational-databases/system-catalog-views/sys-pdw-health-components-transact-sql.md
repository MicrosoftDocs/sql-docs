---
title: "sys.pdw_health_components (Transact-SQL)"
description: See a reference for the system catalog view sys.pdw_health_components (Transact-SQL) for Analytics Platform System.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/12/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom: kr2b-contr-experiment
dev_langs:
  - "TSQL"
ms.assetid: d5c7589b-09b0-4f12-ab84-feb3ec3fbaaa
monikerRange: ">=aps-pdw-2016"
---
# sys.pdw_health_components (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

This view stores information about all components and devices that exist in the system. These components and devices include hardware, storage devices, and network devices.

|Column Name|Data Type|Description|Range|
|-----------------|---------------|-----------------|-----------|
|component_id|**int**|Unique identifier of a component or device.<br /><br /> Key for this view.|NOT NULL|
|group_id|**int**|The logical component group to which this component belongs. See [sys.pdw_health_component_groups (Parallel Data Warehouse)](../../relational-databases/system-catalog-views/sys-pdw-health-component-groups-transact-sql.md).|NOT NULL|
|component_name|**nvarchar(255)**|Name of the component.|NOT NULL|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
