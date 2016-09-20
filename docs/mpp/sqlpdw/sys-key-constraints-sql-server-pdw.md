---
title: "sys.key_constraints (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9ba655d7-5818-43ec-bec3-b9374fce59b1
caps.latest.revision: 9
author: BarbKess
---
# sys.key_constraints (SQL Server PDW)
SQL Server PDW does not support primary keys or unique constraints, but it does support this view for integration of tools and applications. This view will return 0 rows.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|<Columns inherited from sys.objects>||Return 0 rows.|  
|unique_index_id|**int**||  
|is_system_named|**bit**||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
