---
title: "sys.stats_columns (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8657aeea-5057-498e-a245-a8bf10f58d8f
caps.latest.revision: 8
author: BarbKess
---
# sys.stats_columns (SQL Server PDW)
Contains a row for each column that is part of [sys.stats &#40;SQL Server PDW&#41;](../sqlpdw/sys-stats-sql-server-pdw.md) statistics.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|**object_id**|**int**|id of the object of which this column is part.||  
|**stats_id**|**int**|id of the statistics of which this column is part.||  
|**stats_column_id**|**bit**|1-based ordinal within set of statistics columns.||  
|**column_id**|**bit**|id of the column from [sys.columns &#40;SQL Server PDW&#41;](../sqlpdw/sys-columns-sql-server-pdw.md).||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
