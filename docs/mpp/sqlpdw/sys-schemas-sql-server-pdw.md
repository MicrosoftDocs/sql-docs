---
title: "sys.schemas (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 26445f77-f9f0-46ad-8a45-c6181dfb1b23
caps.latest.revision: 13
author: BarbKess
---
# sys.schemas (SQL Server PDW)
Contains a row for each database schema.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|name|**sysname**|Name of the schema.|Built-in schemas are: 'dbo','INFORMATION_SCHEMA','sys', 'sysdiag.' User-defined schemas are available beginning with APS AU2.|  
|schema_id|**int**|id of the schema.|1= dbo, 3 = INFORMATION_SCHEMA, 4 = sys, 5=sysdiag|  
|principal_id|**int**|id of principal that owns this schema.|See principal_id in [sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md).|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[CREATE SCHEMA &#40;SQL Server PDW&#41;](../sqlpdw/create-schema-sql-server-pdw.md)  
[ALTER SCHEMA &#40;SQL Server PDW&#41;](../sqlpdw/alter-schema-sql-server-pdw.md)  
[sys.schemas &#40;SQL Server PDW&#41;](../sqlpdw/sys-schemas-sql-server-pdw.md)  
  
