---
title: sys.fulltext_indexes (Transact-SQL)
description: sys.fulltext_indexes contains a row per full-text index of a tabular object.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_indexes_TSQL"
  - "sys.fulltext_indexes"
  - "fulltext_indexes_TSQL"
  - "fulltext_indexes"
helpviewer_keywords:
  - "sys.fulltext_indexes catalog view"
  - "full-text indexes [SQL Server], properties"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_indexes (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Contains a row per full-text index of a tabular object.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object to which this full-text index belongs. |
| `unique_index_id` | **int** | ID of the corresponding unique, non-full-text index that is used to relate the full-text index to the rows. |
| `index_version` | **int** | Version of full-text filter and wordbreaker components that are used to populate and query this index. If you perform an in-place upgrade from [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and earlier versions to [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] and later versions, existing indexes are assigned `index_version = 1`. This value is controlled by the [FULLTEXT_INDEX_VERSION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#fulltext_index_version) database scoped configuration option.<br /><br />**Applies to**: [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] and later versions. |
| `fulltext_catalog_id` | **int** | ID of the full-text catalog in which the full-text index resides. |
| `is_enabled` | **bit** | `1` = Full-text index is currently enabled. |
| `change_tracking_state` | **char(1)** | State of change-tracking.<br /><br />`M` = Manual<br />`A` = Auto<br />`O` = Off |
| `change_tracking_state_desc` | **nvarchar(60)** | Description of the state of change-tracking.<br /><br />`MANUAL`<br />`AUTO`<br />`OFF` |
| `has_crawl_completed` | **bit** | Last crawl (population) that the full-text index has completed. |
| `crawl_type` | **char(1)** | Type of the current or last crawl.<br /><br />`F` = Full crawl<br />`I` = Incremental, timestamp-based crawl<br />`U` = Update crawl, based on notifications<br />`P` = Full crawl is paused. |
| `crawl_type_desc` | **nvarchar(60)** | Description of the current or last crawl type.<br /><br />`FULL_CRAWL`<br />`INCREMENTAL_CRAWL`<br />`UPDATE_CRAWL`<br />`PAUSED_FULL_CRAWL` |
| `crawl_start_date` | **datetime** | Start of the current or last crawl.<br /><br />`NULL` = None. |
| `crawl_end_date` | **datetime** | End of the current or last crawl.<br /><br />`NULL` = None. |
| `incremental_timestamp` | **binary(8)** | Timestamp value to use for the next incremental crawl.<br /><br />`NULL` = None. |
| `stoplist_id` | **int** | ID of the [stoplist](../search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md) that is associated with this full-text index. |
| `property_list_id` | **int** | ID of the search property list that is associated with this full-text index. `NULL` indicates that no search property list is associated with the full-text index. To obtain more information about this search property list, use the [sys.registered_search_property_lists](sys-registered-search-property-lists-transact-sql.md) catalog view. |
| `data_space_id` | **int** | Filegroup where this full-text index resides. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Examples

The following example uses a full-text index on the `HumanResources.JobCandidate` table of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database. The example returns the object ID of the table, the search property list ID, and the stoplist ID of the stoplist used by the full-text index.

> [!NOTE]  
> For the code example that creates this full-text index, see the [Examples](../../t-sql/statements/create-fulltext-index-transact-sql.md#examples) section of [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md).

```sql
USE AdventureWorks2025;
GO

SELECT object_id,
       property_list_id,
       stoplist_id
FROM sys.fulltext_indexes
WHERE object_id = object_id('HumanResources.JobCandidate');
```

## Related content

- [sys.fulltext_index_fragments](sys-fulltext-index-fragments-transact-sql.md)
- [sys.fulltext_index_columns](sys-fulltext-index-columns-transact-sql.md)
- [sys.fulltext_index_catalog_usages](sys-fulltext-index-catalog-usages-transact-sql.md)
- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Create and manage full-text indexes](../search/create-and-manage-full-text-indexes.md)
- [DROP FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/drop-fulltext-index-transact-sql.md)
- [CREATE FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/create-fulltext-index-transact-sql.md)
- [ALTER FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/alter-fulltext-index-transact-sql.md)
