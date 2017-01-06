---
title: "sys.stats (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4299100b-3f6e-4a42-b83c-6e1a49c11780
caps.latest.revision: 11
author: BarbKess
---
# sys.stats (SQL Server PDW)
Contains a row for each statistic of a tabular object of the type user-defined table or view. Every index will have a corresponding statistics row with the same name and ID (**index_id**=**stats_id**), but not every statistics row has a corresponding index.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|**object_id**|**int**|ID of the object to which these statistics belong.||  
|**name**|**sysname**|Name of the statistic. Is unique within the object.||  
|**stats_id**|**int**|ID of the statistics. Is unique within the object.||  
|**auto_created**|**bit**|Statistics were auto-created by the query processor.||  
|**user_created**|**bit**|Statistics were explicitly created by the user.||  
|**no_recompute**|**bit**|Statistics were created with the NORECOMPUTE option.||  
|**has_filter**|**bit**|1 = Statistics have a filter and are computed only on rows that satisfy the filter definition.||  
|**filter_definition**|**nvarchar(max)**|Expression for the subset of rows included in filtered statistics.<br /><br />NULL = Non-filtered statistics.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
