---
title: "sys.partition_range_values (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1874c68f-52eb-439a-a258-65a492d25ced
caps.latest.revision: 6
author: BarbKess
---
# sys.partition_range_values (SQL Server PDW)
Contains a row for each range boundary value of a partition function of type R. SQL Server PDW does not support partition functions, but it does support this view for integration of tools and applications. This view will return 0 rows.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**function_id**|**int**|id of the partition function for this range boundary value.|  
|**boundary_id**|**int**|id (1-based ordinal) of the boundary value tuple, with left-most boundary starting at an id of 1.|  
|**parameter_id**|**int**|ID of the parameter of the function to which this value corresponds. The values in this column correspond with those in the **parameter_id** column of the **sys.partition_parameters** catalog view for any particular **function_id**.|  
|**value**|**sql_variant**|The actual boundary value.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
