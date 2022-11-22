---
title: "sys.pdw_materialized_view_distribution_properties (Transact-SQL)"
description: sys.pdw_materialized_view_distribution_properties (Transact-SQL) (preview)
author: XiaoyuMSFT
ms.author: xiaoyul
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
# sys.pdw_materialized_view_distribution_properties (Transact-SQL) (preview)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Displays distribution information materialized views.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------| 
|object_id|**int**|ID of the materialized view for which thee properties were specified.| 
|distribution_policy |**tinyint**|2 = HASH</br>4 = ROUND_ROBIN|  
|distribution_policy_desc |**nvarchar(60)**|HASH, ROUND_ROBIN|  
 
## Permissions

Requires VIEW DATABASE STATE permission.
 
## See also

[CREATE MATERIALIZED VIEW AS SELECT &#40;Transact-SQL&#41;](../../t-sql/statements/create-materialized-view-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[ALTER MATERIALIZED VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/alter-materialized-view-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[EXPLAIN &#40;Transact-SQL&#41;](../../t-sql/queries/explain-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[sys.pdw_materialized_view_mappings &#40;Transact-SQL&#41;](./sys-pdw-materialized-view-mappings-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-pdw-showmaterializedviewoverhead-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
[Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
[System views supported in Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
[T-SQL statements supported in Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements)
