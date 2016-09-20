---
title: "sys.identity_columns (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 12959c4a-ef53-4f5e-a360-34d7078116b9
caps.latest.revision: 10
author: BarbKess
---
# sys.identity_columns (SQL Server PDW)
SQL Server PDW does not support identity columns, but it supports this view to allow integration with tools and applications.  This view returns 0 rows.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|<columns inherited from sys.columns>||Returns 0 rows.|  
|seed_value|**sql_variant**||  
|increment_value|**sql_variant**||  
|last_value|**sql_variant**||  
|is_not_for_replication|**bit**||  
|is_computed|**bit**||  
|is_sparse|**bit**||  
|is_column_set|**bit**||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
