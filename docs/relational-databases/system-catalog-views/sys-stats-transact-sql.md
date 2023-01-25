---
title: "sys.stats (Transact-SQL)"
description: sys.stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/13/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.stats"
  - "stats_TSQL"
  - "sys.stats_TSQL"
  - "stats"
helpviewer_keywords:
  - "sys.stats catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.stats (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains a row for each statistics object that exists for the tables, indexes, and indexed views in the database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Every index will have a corresponding statistics row with the same name and ID (`index_id` = `stats_id`), but not every statistics row has a corresponding index.

 The catalog view [sys.stats_columns](../../relational-databases/system-catalog-views/sys-stats-columns-transact-sql.md) provides statistics information for each column in the database. For more information about statistics, see [Statistics](../../relational-databases/statistics/statistics.md).

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**object_id**|**int**|ID of the object to which these statistics belong.|
|**name**|**sysname**|Name of the statistics. Is unique within the object.|
|**stats_id**|**int**|ID of the statistics. Is unique within the object.<br /><br />If statistics correspond to an index, the *stats_id* value is the same as the *index_id* value in the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.|
|**auto_created**|**bit**|Indicates whether the statistics were automatically created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />0 = Statistics were not automatically created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />1 = Statistics were automatically created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|
|**user_created**|**bit**|Indicates whether the statistics were created by a user.<br /><br />0 = Statistics were not created by a user.<br /><br />1 = Statistics were created by a user.|
|**no_recompute**|**bit**|Indicates whether the statistics were created with the **NORECOMPUTE** option.<br /><br />0 = Statistics were not created with the **NORECOMPUTE** option.<br /><br />1 = Statistics were created with the **NORECOMPUTE** option.|
|**has_filter**|**bit**|0 = Statistics do not have a filter and are computed on all rows.<br /><br />1 = Statistics have a filter and are computed only on rows that satisfy the filter definition.|
|**filter_definition**|**nvarchar(max)**|Expression for the subset of rows included in filtered statistics.<br /><br />NULL = Non-filtered statistics.|
|**is_temporary**|**bit**|Indicates whether the statistics is temporary. Temporary statistics support [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] secondary databases that are enabled for read-only access.<br /><br />0 = The statistics is not temporary.<br /><br />1 = The statistics is temporary.<br /><br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])|
|**is_incremental**|**bit**|Indicate whether the statistics are created as incremental statistics.<br /><br />0 = The statistics are not incremental.<br /><br />1 = The statistics are incremental.<br /><br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)])|
|**has_persisted_sample**|**bit**|Indicates whether the statistics were created or updated with the PERSIST_SAMPLE_PERCENT option.<br /><br />**0** = Statistics are not persisting the sample percentage.<br /><br />1 = Statistics were created or updated with the PERSIST_SAMPLE_PERCENT option.<br /><br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)])|
|**stats_generation_method**|**int**|Indicates the method by which statistics are created.<br /><br />**0** = Sort based statistics<br /><br />1 = Internal use only<br /><br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)])|
|**stats_generation_method_desc**|**varchar(255)**|The text description of the method by which statistics are created.<br /><br />Sort based statistics<br /><br />Internal use only<br /><br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)])|
|**auto_drop**|**bit**|Indicates whether or not the auto drop feature is enabled for this statistics object. The AUTO_DROP property allows the creation of statistics objects in a mode such that a subsequent schema change will not be blocked by the statistic object, but instead the statistics will be dropped as necessary. In this way, manually created statistics with AUTO_DROP enabled behave like auto-created statistics. For more information, see [AUTO_DROP option](../statistics/statistics.md#auto_drop-option).<br /><br />**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], and starting with [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)]. |

## Permissions

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## Examples

 The following examples return all the statistics and statistics columns for the `HumanResources.Employee` table.

```sql
USE AdventureWorks2019;
GO
SELECT s.name AS statistics_name
      ,c.name AS column_name
      ,sc.stats_column_id
FROM sys.stats AS s
INNER JOIN sys.stats_columns AS sc
    ON s.object_id = sc.object_id AND s.stats_id = sc.stats_id
INNER JOIN sys.columns AS c
    ON sc.object_id = c.object_id AND c.column_id = sc.column_id
WHERE s.object_id = OBJECT_ID('HumanResources.Employee');
```

## See also

- [Object Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)
- [Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)
- [sys.dm_db_stats_properties (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)
- [sys.dm_db_stats_histogram (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md)
- [sys.stats_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-stats-columns-transact-sql.md)

## Next steps

- [Statistics](../../relational-databases/statistics/statistics.md)
- [sp_updatestats (Transact-SQL)](../system-stored-procedures/sp-updatestats-transact-sql.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [Create statistics in SSMS or T-SQL](../statistics/create-statistics.md)
