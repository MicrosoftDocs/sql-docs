---
title: "sys.pdw_table_distribution_properties (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5c25280d-077b-4c77-99e9-f030654bc17f
caps.latest.revision: 8
author: BarbKess
---
# sys.pdw_table_distribution_properties (SQL Server PDW)
Holds distribution information for tables.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|**object_id**|**int**|ID of the table for which thee properties were specified.||  
|**distribution_policy**|**tinyint**|0 = Undefined<br /><br />1 = None<br /><br />2 = Hash<br /><br />3 = Replicate|SQL Server PDW returns either 2 or 3.|  
|**distribution_policy_desc**|**nvarchar(60)**|UNDEFINED, NONE, HASH, REPLICATE, SEGMENTED_HEAP|SQL Server PDW returns either HASH or REPLICATE.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
