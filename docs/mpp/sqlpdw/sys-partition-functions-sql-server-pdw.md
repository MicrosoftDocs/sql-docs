---
title: "sys.partition_functions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 96560a28-0430-4322-ab20-f3b129150cd8
caps.latest.revision: 7
author: BarbKess
---
# sys.partition_functions (SQL Server PDW)
Contains a row for each partition function. SQL Server PDW does not support partition functions, but it does support this view for integration of tools and applications. This view will return 0 rows.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**name**|**sysname**|Name of the partition function. Is unique within the database.|  
|**function_id**|**int**|Partition function id. Is unique within the database.|  
|**type**|**char(2)**|Function type.<br /><br />R = Range|  
|**type_desc**|**nvarchar(60)**|Function type<br /><br />RANGE|  
|**fanout**|**int**|Number of partitions created by the function.|  
|**boundary_value_on_right**|**bit**|For range partitioning.<br /><br />1 = Boundary value is included in the RIGHT range of the boundary.<br /><br />0 = LEFT.|  
|**create_date**|**datetime**|Date the function was created.|  
|**modify_date**|**datetime**|Date the function was last modified using an ALTER statement.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
