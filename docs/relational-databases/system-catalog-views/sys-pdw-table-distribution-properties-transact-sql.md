---
title: "sys.pdw_table_distribution_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 639a7475-7c92-41e0-a8ab-ad630eb5aea3
caps.latest.revision: 9
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# sys.pdw_table_distribution_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Holds distribution information for tables.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|**object_id**|**int**|ID of the table for which thee properties were specified.||  
|**distribution_policy**|**tinyint**|0 = UNDEFINED<br /><br /> 1 = NONE<br /><br /> 2 = HASH<br /><br /> 3 = REPLICATE<br /><br /> 4 = ROUND_ROBIN|REPLICATE only applies to [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].|  
|**distribution_policy_desc**|**nvarchar(60)**|UNDEFINED, NONE, HASH, REPLICATE, SEGMENTED_HEAP|[!INCLUDE[ssSDW](../../includes/sssdw-md.md)] returns either HASH or REPLICATE.|  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  