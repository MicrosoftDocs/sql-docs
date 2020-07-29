---
title: "sys.pdw_materialized_view_column_distribution_properties (Transact-SQL)"
ms.custom: seo-dt-2019
ms.date: "07/03/2019"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: jrasnick
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: d62b0e25-3226-4f87-a10a-b3a0d9555e19
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_materialized_view_column_distribution_properties (Transact-SQL) 

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Displays distribution information for columns in a materialized view.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object to which the column belongs. |  
|column_id|**int**|The ID of the column.|  
|distribution_ordinal|**tinyint**|0 = Not a distribution column.</br> 1 = SQL Data Warehouse is using this column to distribute the materialized view.|
 
## Permissions 

Requires VIEW DATABASE STATE permission.

## See also

[Performance tuning with Materialized View](/azure/sql-data-warehouse/performance-tuning-materialized-views)   
[CREATE MATERIALIZED VIEW AS SELECT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-materialized-view-as-select-transact-sql?view=azure-sqldw-latest)   
[ALTER MATERIALIZED VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-materialized-view-transact-sql?view=azure-sqldw-latest)   
[EXPLAIN &#40;Transact-SQL&#41;](/sql/t-sql/queries/explain-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_mappings &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-mappings-transact-sql?view=azure-sqldw-latest)   
[DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-pdw-showmaterializedviewoverhead-transact-sql?view=azure-sqldw-latest)   
[SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
[System views supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
[T-SQL statements supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements)
