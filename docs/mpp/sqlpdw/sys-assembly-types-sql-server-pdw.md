---
title: "sys.assembly_types (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5812eabb-da81-4122-8df8-cd847ad5c7d5
caps.latest.revision: 6
author: BarbKess
---
# sys.assembly_types (SQL Server PDW)
Contains a row for each user-defined type that is defined by a CLR assembly. The following **sys.assembly_types** appear in the list of inherited columns (see [sys.types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-types-sql-server-pdw.md)) after **rule_object_id**.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**assembly_id**|**int**|ID of the assembly from which this type was created.|  
|**assembly_class**|**sysname**|Name of the class within the assembly that defines this type.|  
|**is_binary_ordered**|**bit**|Sorting the bytes of this type is equivalent to sorting using comparison operators on the type.|  
|**is_fixed_length**|**bit**|Length of the type is always the same as max_length.|  
|**prog_id**|**nvarchar(40)**|ProgID of the type as exposed to COM.|  
|**assembly_qualified_name**|**nvarchar(4000)**|Assembly qualified type name. The name is in a format suitable to be passed to Type.GetType().|  
  
## Permissions  
The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
[sys.all_views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-all-views-sql-server-pdw.md)  
  
