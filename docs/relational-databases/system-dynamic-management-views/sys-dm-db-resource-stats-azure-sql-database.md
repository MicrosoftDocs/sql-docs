---
title: "sys.dm_db_resource_stats (Azure SQL Database)"
description: sys.dm_db_resource_stats returns CPU, I/O, and memory consumption for a database in Azure SQL Database.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: 11/21/2023
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_resource_stats"
  - "sys.dm_db_resource_stats_TSQL"
  - "dm_db_resource_stats"
  - "dm_db_resource_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_resource_stats"
  - "dm_db_resource_stats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current"
---
# sys.dm_db_resource_stats (Azure SQL Database)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

Returns CPU, I/O, and memory consumption for a database in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. One row exists for every 15 seconds, even if there's no activity. Historical data is maintained for approximately one hour.

> [!NOTE]  
> `sys.dm_db_resource_stats` isn't supported in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. Use the [sys.server_resource_stats](../system-catalog-views/sys-server-resource-stats-azure-sql-database.md) catalog view instead.

| Columns | Data Type | Description |
| --- | --- | --- |
| `end_time` | **datetime** | UTC time indicates the end of the current reporting interval. |
| `avg_cpu_percent` | **decimal (5,2)** | Average compute utilization in percentage of the limit of the service tier. |
| `avg_data_io_percent` | **decimal (5,2)** | Average data I/O utilization in percentage of the limit of the service tier. For Hyperscale databases, see [Data IO in resource utilization statistics](/azure/sql-database/sql-database-hyperscale-performance-diagnostics#data-io-in-resource-utilization-statistics). |
| `avg_log_write_percent` | **decimal (5,2)** | Average transaction log writes (in MB/s) as percentage of the service tier limit. |
| `avg_memory_usage_percent` | **decimal (5,2)** | Average memory utilization in percentage of the limit of the service tier.<br /><br />This includes memory used for buffer pool pages and storage of In-Memory OLTP objects. |
| `xtp_storage_percent` | **decimal (5,2)** | Storage utilization for In-Memory OLTP as a percentage of pool limit at the end of the reporting interval. This includes memory used for storage of the following In-Memory OLTP objects: memory-optimized tables, indexes, and table variables. It also includes memory used for processing ALTER TABLE operations on memory-optimized tables.<br /><BR />Returns 0 if In-Memory OLTP isn't used in the database. |
| `max_worker_percent` | **decimal (5,2)** | Maximum concurrent workers (requests) in percentage of the limit of the database's service tier. |
| `max_session_percent` | **decimal (5,2)** | Maximum concurrent sessions in percentage of the limit of the database's service tier. |
| `dtu_limit` | **int** | Current max database DTU setting for this database during this interval. For databases using the vCore-based model, this column is `NULL`. |
| `cpu_limit` | **decimal (5,2)** | Number of vCores for this database during this interval. For databases using the DTU-based model, this column is `NULL`. |
| `avg_instance_cpu_percent` | **decimal (5,2)** | Average CPU utilization by the database engine instance hosting the pool, as a percentage of instance limit. Reported at one minute granularity and includes CPU utilization by both user and internal workloads. |
| `avg_instance_memory_percent` | **decimal (5,2)** | Average memory usage for the SQL Server instance hosting the database. Includes memory utilization by both user and internal workloads. |
| `avg_login_rate_percent` | **decimal (5,2)** | Identified for informational purposes only. Not supported. Future compatibility isn't guaranteed. |
| `replica_role` | **int** | Represents the current replica role.<br /><br />0 - Primary<br />1 - High availability (HA) secondary<br />2 - Geo-replication forwarder<br />3 - Named replica<br /><br />Reports 1 when connected with `ReadOnly` intent to any [readable secondary](/azure/azure-sql/database/read-scale-out). If connecting to a geo-secondary without specifying `ReadOnly` intent, reports 2 to reflect a connection to a geo-replication forwarder. If connecting to a named replica without specifying `ReadOnly` intent, reports 3. |

> [!TIP]  
> For more context about these limits and service tiers, see the topics [Service Tiers](/azure/azure-sql/database/purchasing-models), [Manually tune query performance in Azure SQL Database](/azure/azure-sql/database/performance-guidance), and [SQL Database resource limits and resource governance](/azure/sql-database/sql-database-resource-limits-database-server).

## Permissions

This view requires `VIEW DATABASE STATE` permission.

## Remarks

The data returned by `sys.dm_db_resource_stats` is expressed as a percentage of the maximum allowed limits for the service tier/performance level that you're running.

If the database was failed over to another server within the last 60 minutes, the view will only return data for the time since that failover.

For a less granular view of this data with longer retention period, use the `sys.resource_stats` catalog view in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. This view captures data every 5 minutes and maintains historical data for 14 days. For more information, see [sys.resource_stats](../system-catalog-views/sys-resource-stats-azure-sql-database.md).

When a database is a member of an elastic pool, resource statistics presented as percent values are expressed as the percent of the max limit for the databases as set in the elastic pool configuration.

## Examples

The following example returns resource utilization data ordered by the most recent time for the currently connected database in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] or [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

```sql
SELECT * FROM sys.dm_db_resource_stats ORDER BY end_time DESC;
```

The following example identifies the average DTU consumption in terms of a percentage of the maximum allowed DTU limit in the performance level for the user database over the past hour. Consider increasing the performance level as these percentages near 100% on a consistent basis.

```sql
SELECT end_time,
  (SELECT Max(v)
   FROM (VALUES (avg_cpu_percent), (avg_data_io_percent), (avg_log_write_percent)) AS
   value(v)) AS [avg_DTU_percent]
FROM sys.dm_db_resource_stats;
```

The following example returns the average and maximum values for CPU percent, data and log I/O, and memory consumption over the last hour.

```sql
SELECT
    AVG(avg_cpu_percent) AS 'Average CPU Utilization In Percent',
    MAX(avg_cpu_percent) AS 'Maximum CPU Utilization In Percent',
    AVG(avg_data_io_percent) AS 'Average Data IO In Percent',
    MAX(avg_data_io_percent) AS 'Maximum Data IO In Percent',
    AVG(avg_log_write_percent) AS 'Average Log Write I/O Throughput Utilization In Percent',
    MAX(avg_log_write_percent) AS 'Maximum Log Write I/O Throughput Utilization In Percent',
    AVG(avg_memory_usage_percent) AS 'Average Memory Usage In Percent',
    MAX(avg_memory_usage_percent) AS 'Maximum Memory Usage In Percent'
FROM sys.dm_db_resource_stats;
```

## Related content

- [sys.resource_stats](../system-catalog-views/sys-resource-stats-azure-sql-database.md)
- [sys.server_resource_stats](../system-catalog-views/sys-server-resource-stats-azure-sql-database.md)
- [sys.elastic_pool_resource_stats (Azure SQL Database)](../system-catalog-views/sys-elastic-pool-resource-stats-azure-sql-database.md)
- [sys.dm_elastic_pool_resource_stats (Azure SQL Database)](sys-dm-elastic-pool-resource-stats-azure-sql-database.md)
- [Service Tiers](/azure/azure-sql/database/purchasing-models)
