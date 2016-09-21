---
title: "sys.pdw_index_mappings (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5e8f44dd-f94f-4211-bf15-124f96615523
caps.latest.revision: 12
author: BarbKess
---
# sys.pdw_index_mappings (SQL Server PDW)
Maps the logical indexes to the physical name used on Compute nodes as reflected by a unique combination of **object_id** of the table holding the index and the **index_id** of a particular index within that table.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|object_id|**int**|The object ID for the logical table on which this index exists. See [sys.objects &#40;SQL Server PDW&#41;](../sqlpdw/sys-objects-sql-server-pdw.md).<br /><br />**physical_name** and **object_id** form the key for this view.||  
|index_id|**nvarchar(32)**|The ID for the index. See [sys.indexes &#40;SQL Server PDW&#41;](../sqlpdw/sys-indexes-sql-server-pdw.md).||  
|physical_name|**nvarchar(36)**|The name of the index in the databases on the Compute nodes.<br /><br />**physical_name** and **object_id** form the key for this view.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[sys.pdw_table_mappings &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-table-mappings-sql-server-pdw.md)  
[sys.pdw_database_mappings &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-database-mappings-sql-server-pdw.md)  
  
