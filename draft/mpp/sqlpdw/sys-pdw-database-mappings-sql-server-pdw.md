---
title: "sys.pdw_database_mappings (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6ec28184-4b60-4fbc-a8cd-bb772d112fde
caps.latest.revision: 15
author: BarbKess
---
# sys.pdw_database_mappings (SQL Server PDW)
Maps the **database_id**s of databases to the physical name used on Compute nodes, and provides the **principal id** of the database owner on the system. Join **sys.pdw_database_mappings** to **sys.databases** and **sys.pdw_nodes_pdw_physical_databases**.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|physical_name|**nvarchar(36)**|The physical name for the database on the Compute nodes.<br /><br />**physical_name** and **database_id** form the key for this view.||  
|database_id|**int**|The object ID for the database. See [sys.databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-databases-sql-server-pdw.md).<br /><br />**physical_name** and **database_id** form the key for this view.||  
  
## Example  
The following example joins sys.pdw_database_mappings to other system tables to show how databases are mapped.  
  
```  
SELECT DB.database_id, DB.name, Map.*, Phys.*   
FROM sys.databases AS DB  
JOIN sys.pdw_database_mappings AS Map  
    ON DB.database_id = Map.database_id  
JOIN sys.pdw_nodes_pdw_physical_databases AS Phys  
    ON Map.physical_name = Phys.physical_name  
ORDER BY DB.database_id, Phys.pdw_node_id;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[sys.pdw_index_mappings &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-index-mappings-sql-server-pdw.md)  
[sys.pdw_table_mappings &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-table-mappings-sql-server-pdw.md)  
[sys.pdw_nodes_pdw_physical_databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-pdw-physical-databases-sql-server-pdw.md)  
  
