---
title: "sys.pdw_table_mappings (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 41d65dd4-3df6-435d-af69-2c7268cc8d5b
caps.latest.revision: 10
author: BarbKess
---
# sys.pdw_table_mappings (SQL Server PDW)
Ties user tables to internal object names by **object_id**.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|physical_name|**nvarchar(36)**|The physical name for the table.<br /><br />**physical_name** and **object_id** form the key for this view.||  
|object_id|**int**|The object ID for the table. See [sys.objects &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-objects-sql-server-pdw.md).<br /><br />**physical_name** and **object_id** form the key for this view.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
[sys.pdw_index_mappings &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-index-mappings-sql-server-pdw.md)  
[sys.pdw_database_mappings &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-database-mappings-sql-server-pdw.md)  
  
