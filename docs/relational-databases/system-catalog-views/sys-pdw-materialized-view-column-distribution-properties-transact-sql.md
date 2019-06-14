---
title: "sys.pdw_materialized_view_column_distribution_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: d62b0e25-3226-4f87-a10a-b3a0d9555e19
author: 
ms.author: 
manager: craigg
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_materialized_view_column_distribution_properties (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Displays distribution information for columns in a materialized view.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|object_id|**int**|ID of the object to which the column belongs. ||  
|column_id|**int**|The ID of the column.||  
|distribution_ordinal|**tinyint**|Ordinal (1-based) within set of distribution.|0 = Not a distribution column.</br> 1 = SQL Data Warehouse is using this column to distribute the materialized view. |  
  
## See also

 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
 [sys.pdw_table_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-table-mappings-transact-sql.md)   
 [sys.pdw_database_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-database-mappings-transact-sql.md)  
