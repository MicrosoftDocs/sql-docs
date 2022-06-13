---
title: "sys.pdw_column_distribution_properties (Transact-SQL)"
description: sys.pdw_column_distribution_properties (Transact-SQL)
author: ronortloff
ms.author: rortloff
ms.date: "03/03/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
ms.assetid: 46b74f99-2e22-4dbd-872a-533fce0e239c
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.pdw_column_distribution_properties (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Holds distribution information for columns.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|**object_id**|**int**|ID of the object to which the column belongs.||  
|**column_id**|**int**|ID of the column.||  
|**distribution_ordinal**|**tinyint**|Ordinal (1-based) within set of distribution.|0 = Not a distribution column. 1 = [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] is using this column to distribute the parent table.|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
