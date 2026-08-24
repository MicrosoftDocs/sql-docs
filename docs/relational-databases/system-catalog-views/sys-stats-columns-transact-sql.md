---
title: "sys.stats_columns (Transact-SQL)"
description: sys.stats_columns (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "12/18/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "stats_columns_TSQL"
  - "stats_columns"
  - "sys.stats_columns"
  - "sys.stats_columns_TSQL"
helpviewer_keywords:
  - "sys.stats_columns catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.stats_columns (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

  Contains a row for each column that is part of **sys.stats** statistics.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object of which this column is part.|  
|**stats_id**|**int**|ID of the statistics of which this column is part.<br /><br />If statistics correspond to an index, the *stats_id* value is the same as the *index_id* value in the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.|  
|**stats_column_id**|**int**|1-based ordinal within set of stats columns.|  
|**column_id**|**int**|ID of the column from **sys.columns**.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [Statistics](../statistics/statistics.md)
- [sys.dm_db_stats_properties (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-stats-properties-transact-sql.md)
- [sys.dm_db_stats_histogram (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-stats-histogram-transact-sql.md)
- [sys.stats (Transact-SQL)](sys-stats-transact-sql.md)
- [Statistics in Microsoft Fabric](/fabric/data-warehouse/statistics)
