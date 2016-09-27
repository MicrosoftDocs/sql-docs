---
title: "sys.data_spaces (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f7bde8cd-a836-4961-a9e6-6e3cec52904c
caps.latest.revision: 12
author: BarbKess
---
# sys.data_spaces (SQL Server PDW)
Contains a row for each data space. This can be a filegroup or partition scheme.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|name|**sysname**|Name of data space, unique within the database.||  
|data_space_id|**int**|Data space ID number, unique within the database.||  
|type|**char(2)**|Data space type:<br /><br />FG = Filegroup<br /><br />PS = Partition scheme||  
|type_desc|**nvarchar(60)**|Description of data space type:<br /><br />ROWS_FILEGROUP<br /><br />PARTITION_SCHEME||  
|is_default|**bit**|1 = This is the default data space. <br />0 = This is not the default data space.||  
|is_system|**bit**|1 = Data space is used for full-text index fragments. <br />0 = Data space is not used for full-text index fragments.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
  
