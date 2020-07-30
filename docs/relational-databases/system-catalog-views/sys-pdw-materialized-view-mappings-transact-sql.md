---
title: "sys.pdw_materialized_view_mappings (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/03/2019"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: d62b0e25-3226-4f87-a10a-b3a0d9555e19
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_materialized_view_mappings (Transact-SQL)  

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Ties the materialized view to internal object names by object_id.

The columns physical_name and object_id form the key for this catalog view.
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|physical_name |**nvarchar(36)**|The physical name for the materialized view.|  
|object_id  |**int**|The object ID for the materialized view. See [sys.objects (Transact-SQL)](/sql/relational-databases/system-catalog-views/sys-objects-transact-sql?view=azure-sqldw-latest).| 

## Permissions

Requires VIEW DATABASE STATE permission.
  
## See also

[Performance tuning with Materialized View](/azure/sql-data-warehouse/performance-tuning-materialized-views)   
[CREATE MATERIALIZED VIEW AS SELECT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-materialized-view-as-select-transact-sql?view=azure-sqldw-latest)   
[ALTER MATERIALIZED VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-materialized-view-transact-sql?view=azure-sqldw-latest)   
[EXPLAIN &#40;Transact-SQL&#41;](/sql/t-sql/queries/explain-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_column_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-column-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-pdw-showmaterializedviewoverhead-transact-sql?view=azure-sqldw-latest)   
[SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
[System views supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
[T-SQL statements supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements) 
