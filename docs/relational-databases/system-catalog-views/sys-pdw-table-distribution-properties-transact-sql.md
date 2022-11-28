---
title: "sys.pdw_table_distribution_properties (Transact-SQL)"
description: sys.pdw_table_distribution_properties (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "12/03/2019"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 639a7475-7c92-41e0-a8ab-ad630eb5aea3
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.pdw_table_distribution_properties (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds distribution information for tables.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|**object_id**|**int**|ID of the table for which thee properties were specified.||  
|**distribution_policy**|**tinyint**|0 = UNDEFINED<br /><br /> 1 = NONE<br /><br /> 2 = HASH<br /><br /> 3 = REPLICATE<br /><br /> 4 = ROUND_ROBIN||  
|**distribution_policy_desc**|**nvarchar(60)**|UNDEFINED, NONE, HASH, REPLICATE, ROUND_ROBIN|[!INCLUDE[ssSDW](../../includes/sssdw-md.md)] returns either HASH, ROUND_ROBIN or REPLICATE.|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
