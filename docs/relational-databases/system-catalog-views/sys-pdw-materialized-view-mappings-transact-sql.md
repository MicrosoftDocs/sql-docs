---
title: "sys.pdw_materialized_view_mappings (Transact-SQL) | Microsoft Docs"
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
# sys.pdw_materialized_view_mappings (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

Ties the materialized view to internal object names by object_id.

The columns physical_name and object_id form the key for this catalog view.
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|physical_name |**nvarchar(36) **|The physical name for the materialized view.||  
|object_id  |**int**|The object ID for the materialized view. See sys.objects (Transact-SQL).  ||  
  
## See also

 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
 [sys.pdw_table_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-table-mappings-transact-sql.md)   
 [sys.pdw_database_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-database-mappings-transact-sql.md)  
  