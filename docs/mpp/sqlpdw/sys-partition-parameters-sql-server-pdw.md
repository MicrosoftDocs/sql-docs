---
title: "sys.partition_parameters (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 42fd4597-8257-4dd5-b0c0-093675aff6c5
caps.latest.revision: 7
author: BarbKess
---
# sys.partition_parameters (SQL Server PDW)
Contains a row for each parameter of a partition function. SQL Server PDW does not support partition functions, but it does support this view for integration of tools and applications. This view will return 0 rows.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**function_id**|**int**|ID of the partition function to which this parameter belongs.|  
|**parameter_id**|**int**|ID of the parameter. Is unique within the partition function, beginning with 1.|  
|**system_type_id**|**tinyint**|ID of the system type of the parameter. Corresponds to the **system_type_id** column of the **sys.types** catalog view.|  
|**max_length**|**smallint**|Maximum length of the parameter in bytes.|  
|**precision**|**tinyint**|Precision of the parameter if numeric-based; otherwise, 0.|  
|**scale**|**tinyint**|Scale of the parameter if numeric-based; otherwise, 0.|  
|**collation_name**|**sysname**|Name of the collation of the parameter if character-based; otherwise, NULL.|  
|**user_type_id**|**int**|ID of the type. Is unique within the database. For system data types, **user_type_id** = **system_type_id**.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
