---
title: "sys.pdw_diag_events (Transact-SQL)"
description: sys.pdw_diag_events (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 59bb3e9c-2829-49a0-b382-652ed1f54f88
monikerRange: ">=aps-pdw-2016"
---
# sys.pdw_diag_events (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

  Holds information about events that can be included in diagnostic sessions on the system.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|**name**|**nvarchar(255)**|Name of the specific diagnostics event.||  
|**source**|**nvarchar(255)**|Source of the event (engine, general, dms, etc.)||  
|**is_enabled**|**bit**|Whether the event is being published.||  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
