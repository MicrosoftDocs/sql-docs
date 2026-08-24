---
title: "sys.stats (Transact-SQL)"
description: The sys.stats system catalog view contains a row for each statistics object that exists for the tables, indexes, and indexed views in a SQL Server database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/02/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.stats"
  - "stats_TSQL"
  - "sys.stats_TSQL"
  - "stats"
helpviewer_keywords:
  - "sys.stats catalog view"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.stats (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Contains a row for each statistics object that exists for the tables, indexes, and indexed views in the database in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Every index has a corresponding statistics row with the same name and ID (`index_id` = `stats_id`), but not every statistics row has a corresponding index.

The catalog view [sys.stats_columns](sys-stats-columns-transact-sql.md) provides statistics information for each column in the database.

For more information about statistics, see [Statistics](../statistics/statistics.md).

::: moniker range="=fabric"

> [!NOTE]  
> For more information on statistics in [!INCLUDE [fabric](../../includes/fabric.md)], see [Statistics in Fabric Data Warehouse](/fabric/data-warehouse/statistics).

::: moniker-end

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object to which these statistics belong. |
| `name` | **sysname** | Name of the statistics. Is unique within the object. |
| `stats_id` | **int** | ID of the statistics. Is unique within the object.<br /><br />If statistics correspond to an index, the *stats_id* value is the same as the *index_id* value in the [sys.indexes](sys-indexes-transact-sql.md) catalog view. |
| `auto_created` | **bit** | Indicates whether the statistics were automatically created by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />0 = Statistics weren't automatically created by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />1 = Statistics were automatically created by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `user_created` | **bit** | Indicates whether the statistics were created by a user.<br /><br />0 = Statistics weren't created by a user.<br /><br />1 = Statistics were created by a user. |
| `no_recompute` | **bit** | Indicates whether the statistics were created with the `NORECOMPUTE` option.<br /><br />0 = Statistics weren't created with the `NORECOMPUTE` option.<br /><br />1 = Statistics were created with the `NORECOMPUTE` option. |
| `has_filter` | **bit** | 0 = Statistics don't have a filter and are computed on all rows.<br /><br />1 = Statistics have a filter and are computed only on rows that satisfy the filter definition. |
| `filter_definition` | **nvarchar(max)** | Expression for the subset of rows included in filtered statistics.<br /><br />`NULL` = Nonfiltered statistics. |
| `is_temporary` | **bit** | Indicates whether the statistics is temporary. Temporary statistics support [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] secondary databases that are enabled for read-only access.<br /><br />0 = The statistics isn't temporary.<br /><br />1 = The statistics is temporary.<br /><br />**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] |
| `is_incremental` | **bit** | Indicate whether the statistics are created as incremental statistics.<br /><br />0 = The statistics aren't incremental.<br /><br />1 = The statistics are incremental.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions. |
| `has_persisted_sample` | **bit** | Indicates whether the statistics were created or updated with the `PERSIST_SAMPLE_PERCENT` option.<br /><br />`0` = Statistics aren't persisting the sample percentage.<br /><br />1 = Statistics were created or updated with the `PERSIST_SAMPLE_PERCENT` option.<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. |
| `stats_generation_method` | **int** | Indicates the method by which statistics are created.<br /><br />`0` = Sort based statistics<br /><br />1 = Internal use only<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. |
| `stats_generation_method_desc` | **varchar(255)** | The text description of the method by which statistics are created.<br /><br />Sort based statistics<br /><br />Internal use only<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. |
| `auto_drop` | **bit** | Indicates whether or not the auto drop feature is enabled for this statistics object. The `AUTO_DROP` property allows the creation of statistics objects in a mode such that a subsequent schema change isn't blocked by the statistic object, but instead the statistics are dropped as necessary. In this way, manually created statistics with `AUTO_DROP` enabled behave like autocreated statistics. For more information, see [AUTO_DROP option](../statistics/statistics.md#auto_drop-option).<br /><br />**Applies to**: [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. |
| `replica_role_id` | **tinyint** | Indicates the replica in which auto stats were last updated from.<br /><br />1 = Primary<br /><br />2 = Secondary<br /><br />3 = Geo Secondary<br /><br />4 = Geo HA Secondary<br /><br />**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. |
| `replica_role_desc` | **nvarchar(60)** | Primary, Secondary, Geo Secondary, Geo HA Secondary<br /><br />**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. |
| `replica_name` | **sysname** | Instance name of the replica in the availability group. `NULL` for the primary replica<br /><br />**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Examples

The following examples return all the statistics and statistics columns for the `HumanResources.Employee` table.

```sql
USE AdventureWorks2022;
GO

SELECT s.name AS statistics_name,
       c.name AS column_name,
       sc.stats_column_id
FROM sys.stats AS s
     INNER JOIN sys.stats_columns AS sc
         ON s.object_id = sc.object_id
        AND s.stats_id = sc.stats_id
     INNER JOIN sys.columns AS c
         ON sc.object_id = c.object_id
        AND c.column_id = sc.column_id
WHERE s.object_id = OBJECT_ID('HumanResources.Employee');
```

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [sys.dm_db_stats_properties (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-stats-properties-transact-sql.md)
- [sys.dm_db_stats_histogram (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-stats-histogram-transact-sql.md)
- [sys.stats_columns (Transact-SQL)](sys-stats-columns-transact-sql.md)
- [Statistics](../statistics/statistics.md)
- [sys.sp_updatestats (Transact-SQL)](../system-stored-procedures/sp-updatestats-transact-sql.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [Create statistics](../statistics/create-statistics.md)
