---
title: "sys.pdw_nodes_partitions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: ""
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: b4216752-4813-4b2c-b259-7d8ffc6cc190
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_nodes_partitions (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Contains a row for each partition of all the tables, and most types of indexes in a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] database. All tables and indexes contain at least one partition, whether or not they are explicitly partitioned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|partition_id|`bigint`|id of the partition. Is unique within a database.|  
|object_id|`int`|id of the object to which this partition belongs. Every table or view is composed of at least one partition.|  
|index_id|`int`|id of the index within the object to which this partition belongs.|  
|partition_number|`int`|1-based partition number within the owning index or heap. For [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], the value of this column is 1.|  
|hobt_id|`bigint`|ID of the data heap or B-tree that contains the rows for this partition.|  
|rows|`bigint`|Approximate number of rows in this partition. |  
|data_compression|`int`|Indicates the state of compression for each partition:<br /><br /> 0 = NONE<br /><br /> 1 = ROW<br /><br /> 2 = PAGE<br /><br /> 3 = COLUMNSTORE|  
|data_compression_desc|`nvarchar(60)`|Indicates the state of compression for each partition. Possible values are NONE, ROW, and PAGE.|  
|pdw_node_id|`int`|Unique identifier of a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] node.|  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  

### Example A: Display rows in each partition within each distribution 

Applies to: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]
 
To display the number of rows in each partition within each distribution, use [DBCC PDW_SHOWPARTITIONSTATS (SQL Server PDW)](../../t-sql/database-console-commands/dbcc-pdw-showpartitionstats-transact-sql.md) .

### Example B: Uses system views to view rows in each partition of each distribution of a table

Applies to: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]
 
This query returns the number of rows in each partition of each distribution of the table `myTable`.  
 
```  
SELECT o.name, pnp.index_id, pnp.partition_id, pnp.rows,   
    pnp.data_compression_desc, pnp.pdw_node_id  
FROM sys.pdw_nodes_partitions AS pnp  
JOIN sys.pdw_nodes_tables AS NTables  
    ON pnp.object_id = NTables.object_id  
AND pnp.pdw_node_id = NTables.pdw_node_id  
JOIN sys.pdw_table_mappings AS TMap  
    ON NTables.name = TMap.physical_name 
    AND substring(TMap.physical_name,40, 10) = pnp.distribution_id 
JOIN sys.objects AS o  
    ON TMap.object_id = o.object_id  
WHERE o.name = 'myTable'  
ORDER BY o.name, pnp.index_id, pnp.partition_id;  
```    
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  

