---
title: "sys.pdw_materialized_view_column_distribution_properties (Transact-SQL)"
description: sys.pdw_materialized_view_column_distribution_properties (Transact-SQL)
author: XiaoyuMSFT
ms.author: xiaoyul
ms.reviewer: wiassaf
ms.date: "07/03/2019"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
ms.assetid: d62b0e25-3226-4f87-a10a-b3a0d9555e19
monikerRange: "=azure-sqldw-latest"
---
# sys.pdw_materialized_view_column_distribution_properties (Transact-SQL) 

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Displays distribution information for columns in a materialized view.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object to which the column belongs. |  
|column_id|**int**|The ID of the column.|  
|distribution_ordinal|**tinyint**|0 = Not a distribution column.</br> 1 = Azure Synapse Analytics is using this column to distribute the materialized view.|
 
## Permissions 

Requires VIEW DATABASE STATE permission.

## See also

[Performance tuning with Materialized View](/azure/sql-data-warehouse/performance-tuning-materialized-views)   
[CREATE MATERIALIZED VIEW AS SELECT &#40;Transact-SQL&#41;](../../t-sql/statements/create-materialized-view-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[ALTER MATERIALIZED VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/alter-materialized-view-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[EXPLAIN &#40;Transact-SQL&#41;](../../t-sql/queries/explain-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[sys.pdw_materialized_view_distribution_properties &#40;Transact-SQL&#41;](./sys-pdw-materialized-view-distribution-properties-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[sys.pdw_materialized_view_mappings &#40;Transact-SQL&#41;](./sys-pdw-materialized-view-mappings-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-pdw-showmaterializedviewoverhead-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
[System views supported in Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
[T-SQL statements supported in Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements)
