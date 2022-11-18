---
title: "sys.pdw_column_distribution_properties (Transact-SQL)"
description: "The sys.pdw_column_distribution_properties system catalog view returns distribution information for columns."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/25/2022
ms.reviewer: wiassaf
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.pdw_column_distribution_properties (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Returns distribution information for columns.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|**object_id**|**int**|ID of the object to which the column belongs.||  
|**column_id**|**int**|ID of the column.||  
|**distribution_ordinal**|**tinyint**|Ordinal (1-based) within set of distribution.| = 0: Not a distribution column. <br /><br /> = 1 or >1: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] is using this column to distribute the parent table.|  
  
## Next steps 

- [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
- [Distribution Advisor in Azure Synapse SQL](/azure/synapse-analytics/sql/distribution-advisor)