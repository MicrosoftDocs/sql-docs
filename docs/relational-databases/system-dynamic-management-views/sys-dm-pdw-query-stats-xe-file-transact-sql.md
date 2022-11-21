---
title: "sys.dm_pdw_query_stats_xe_file (Transact-SQL)"
description: sys.dm_pdw_query_stats_xe_file (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: e0cd402f-04d0-4a5b-b725-88b31bb7862e
monikerRange: ">=aps-pdw-2016"
---
# sys.dm_pdw_query_stats_xe_file (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

  This DMV is deprecated and will be removed in a future release. In this release, it returns 0 rows.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|event|**nvarchar(60)**|Key for this view.||  
|data|**xml**|||  
|pdw_node_id|**int**|Node on which this Xevent instance is running.||  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
