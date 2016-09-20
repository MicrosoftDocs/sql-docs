---
title: "sys.pdw_column_distribution_properties (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fdabfe3e-415a-4993-82d9-e7efc138eccf
caps.latest.revision: 7
author: BarbKess
---
# sys.pdw_column_distribution_properties (SQL Server PDW)
Holds distribution information for columns.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|**object_id**|**int**|ID of the object to which the column belongs.||  
|**column_id**|**int**|ID of the column.||  
|**distribution_ordinal**|**tinyint**|Ordinal (1-based) within set of distribution.|0 = Not a distribution column. 1 = SQL Server PDW is using this column to distribute the parent table.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
