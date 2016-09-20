---
title: "sys.partition_schemes (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d74cf05f-6aac-4fc7-8f22-574eef26cb3b
caps.latest.revision: 9
author: BarbKess
---
# sys.partition_schemes (SQL Server PDW)
Contains a row for each Data Space that is a partition scheme, with **type** = PS. SQL Server PDW does not support partition schemes, but it does support this view for integration of tools and applications. This view will return 0 rows.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**<inherited columns>**||Inherits columns from [sys.data_spaces &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys.data_spaces-sql-server-pdw.md).|  
|**function_id**|**int**|ID of partition function used in the scheme.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
