---
title: Database watcher data collection and datasets
titleSuffix: Azure SQL Database & SQL Managed Instance
description: A detailed description of SQL monitoring data collected by database watcher
author: dimitri-furman
ms.author: dfurman
ms.date: 07/18/2024
ms.service: azure-sql
ms.subservice: monitoring
ms.topic: conceptual
ms.custom:
  - subject-monitoring
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---

# Database watcher data collection and datasets (preview)

[!INCLUDE [sqldb-sqlmi](./includes/appliesto-sqldb-sqlmi.md)]

Database watcher collects monitoring data from SQL system views and ingests it into the data store in the form of **datasets**. Each dataset is formed using the data from one or more SQL system views. For each dataset, there is a separate table in the data store.

## Data collection

Database watcher collects monitoring data at periodic intervals using T-SQL queries. Data collected in each execution of a query is called a **sample**. Sample collection frequency varies by dataset. For example, frequently changing data such as SQL performance counters might be collected every 10 seconds, while mostly static data such as database configuration might be collected every five minutes. For more information, see [Datasets](#datasets).

Database watcher takes advantage of [streaming ingestion](/azure/data-explorer/ingest-data-streaming) in [Azure Data Explorer](/azure/data-explorer/data-explorer-overview) and [Real-Time Analytics in Microsoft Fabric](/fabric/real-time-analytics/overview) to provide near real time monitoring. Typically, collected SQL monitoring data becomes available for reporting and analysis in less than 10 seconds. You can monitor data ingestion latency on the database watcher [dashboards](database-watcher-overview.md#dashboards), using the **Ingestion statistics** link.

### Interaction between database watcher and application workloads

Enabling database watcher is not likely to have an observable impact on the application workloads. More frequent monitoring queries typically execute in the sub-second range, whereas queries that might require more time, for example to return large datasets, run at infrequent intervals.

To further reduce the risk of impact to application workloads, all database watcher queries in Azure SQL Database are resource-governed as an [internal workload](./database/resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes). When resource contention is present, resource consumption by monitoring queries is limited to a small fraction of total resources available to a database or an elastic pool. This prioritizes application workloads over monitoring queries.

In Azure SQL Managed Instance, you can enable [Resource Governor](/sql/relational-databases/resource-governor/resource-governor) to manage resource consumption by the monitoring queries in a similar fashion, if necessary.

The following example configures Resource Governor on a SQL managed instance. It limits CPU consumption by database watcher queries to 30% when there is no CPU contention. When there is CPU contention, this configuration reserves 5% of CPU for the monitoring queries and limits their CPU usage to 10%. It also limits the memory grant size for each monitoring query to 10% of the available memory.

```sql
USE master;
GO

CREATE OR ALTER FUNCTION dbo.dbw_classifier()
RETURNS sysname
WITH SCHEMABINDING
AS
BEGIN

DECLARE @WorkloadGroupName sysname = 'default';

IF APP_NAME() IN (N'SQLExternalMonitoring',N'x_ms_reserved_sql_external_monitoring')
    SET @WorkloadGroupName = N'database_watcher_workload_group'

RETURN @WorkloadGroupName;

END;
GO

BEGIN TRY

IF EXISTS (
          SELECT 1
          FROM sys.resource_governor_configuration
          WHERE classifier_function_id <> 0 OR is_enabled <> 0
          )
    THROW 50001, 'Resource Governor is already configured. No changes were made.', 1;

CREATE RESOURCE POOL database_watcher_resource_pool
WITH (MIN_CPU_PERCENT = 5, MAX_CPU_PERCENT = 10, CAP_CPU_PERCENT = 30);

CREATE WORKLOAD GROUP database_watcher_workload_group
WITH (REQUEST_MAX_MEMORY_GRANT_PERCENT = 10)
USING database_watcher_resource_pool;

ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = dbo.dbw_classifier);

ALTER RESOURCE GOVERNOR RECONFIGURE;

END TRY
BEGIN CATCH
    THROW;
END CATCH;
```

> [!NOTE]
> To make Resource Governor configuration immediately effective on a high availability secondary replica of a SQL managed instance, connect to the replica and execute `ALTER RESOURCE GOVERNOR RECONFIGURE;`.

To avoid concurrency conflicts such as blocking and deadlocks between data collection and database workloads running on your Azure SQL resources, monitoring queries use short [lock timeouts](/sql/relational-databases/sql-server-transaction-locking-and-row-versioning-guide#customizing-the-lock-time-out) and low [deadlock priority](/sql/t-sql/statements/set-deadlock-priority-transact-sql). If there is a concurrency conflict, priority is given to the application workload queries. Depending on the application workload patterns, this might cause occasional gaps in collected data for some datasets.

### Data collection in elastic pools

To monitor an elastic pool, you must designate one database in the pool as the **anchor database**. Database watcher connects to the anchor database. Because the watcher [holds](database-watcher-overview.md#watcher-authorization) the `VIEW SERVER PERFORMANCE STATE` permission, system views in the anchor database provide monitoring data for the pool as a whole.

> [!TIP]
> You can add an empty database to each elastic pool you want to monitor, and designate it as the anchor database. This way, you can move other databases in and out of the pool, or between pools, without interrupting elastic pool monitoring.

Data collected from the anchor database contains pool-level metrics, and certain database-level performance metrics for every database in the pool. For example, this includes resource utilization and request rate metrics for each database. For some scenarios, monitoring an elastic pool as a whole makes it unnecessary to monitor each individual database in the pool.

Certain monitoring data such as pool-level CPU, memory, and storage utilization, and wait statistics is only collected at the elastic pool level because it cannot be attributed to an individual database in a pool. Conversely, certain other data such as query runtime statistics, database properties, table and index metadata is available only at the database level.

If you add individual databases from an elastic pool as database watcher targets, you should add the elastic pool as a target as well. This way, you get a more complete view of the database and pool performance.

#### Monitor dense elastic pools

A [dense elastic pool](./database/elastic-pool-resource-management.md) contains a large number of databases, but has a relatively small compute size. This configuration lets customers achieve substantial cost savings by keeping the compute resource allocation to a minimum on the assumption that only a small number of databases in the pool are active at the same time.

Compute resources available to database watcher queries in a dense elastic pool are further limited to avoid affecting application queries. Because of this, database watcher might not be able to collect monitoring data from every database in a dense elastic pool.

> [!TIP]
> To monitor a dense elastic pool, enable monitoring at the pool level by adding the elastic pool as a target.
>
> It is not recommended to monitor more than a few individual databases in a dense elastic pool. You might see gaps in the collected data or larger than expected intervals between data samples due to insufficient compute resources available to database watcher queries.

## Datasets

This section describes the datasets available for each target type, including collection frequencies and table names in the data store.

> [!NOTE]
> During preview, datasets might be added and removed. Dataset properties such as name, description, collection frequency, and available columns are subject to change.

# [SQL database](#tab/sqldb)

| Dataset name | Table name | Collection frequency (hh:mm:ss) | Description |
|:--|:--|--:|:--|
| Active sessions | `sqldb_database_active_sessions` | `00:00:30` | Each row represents a session that is running a request, is a blocker, or has an open transaction. |
| Backup history | `sqldb_database_sql_backup_history` | `00:05:00` | Each row represents a successfully completed database backup. |
| Change processing | `sqldb_database_change_processing` | `00:01:00` | Each row represents a snapshot of aggregate log scan statistics for a change processing feature such as Change Data Capture or Change Feed (Azure Synapse Link). |
| Change processing errors | `sqldb_database_change_processing_errors` | `00:01:00` | Each row represents an error that occurred during change processing, when using a change processing feature such as Change Data Capture or Change Feed (Azure Synapse Link). |
| Connectivity | `sqldb_database_connectivity` | `00:00:30` | Each row represents a connectivity probe (a login and a query) for a database. |
| Geo-replicas | `sqldb_database_geo_replicas` | `00:00:30` | Each row represents a primary or a secondary geo-replica, including geo-replication metadata and statistics. |
| Index metadata | `sqldb_database_index_metadata` | `00:30:00` | Each row represents an index partition and includes index definition, properties, and operational statistics. |
| Memory utilization | `sqldb_database_memory_utilization` | `00:00:10` | Each row represents a memory clerk and includes memory consumption by the clerk on the database engine instance. |
| Missing indexes | `sqldb_database_missing_indexes` | `00:15:00` | Each row represents an index that might improve query performance if created. |
| Out-of-memory events | `sqldb_database_oom_events` | `00:01:00` | Each row represents an out-of-memory event in the database engine. |
| Performance counters (common) | `sqldb_database_performance_counters_common` | `00:00:10` | Each row represents a performance counter of the database engine instance. This dataset includes commonly used counters. |
| Performance counters (detailed) | `sqldb_database_performance_counters_detailed` | `00:01:00` | Each row represents a performance counter of the database engine instance. This dataset includes counters that might be needed for detailed monitoring and troubleshooting. |
| Properties | `sqldb_database_properties` | `00:05:00` | Each row represents a database and includes database options, resource governance limits, and other database metadata. |
| Query runtime statistics | `sqldb_database_query_runtime_stats` | `00:15:00` | Each row represents a Query Store runtime interval and includes query execution statistics. |
| Query wait statistics | `sqldb_database_query_wait_stats` | `00:15:00` | Each row represents a Query Store runtime interval and includes wait category statistics. |
| Replicas | `sqldb_database_replicas` | `00:00:10` | Each row represents a database replica, including replication metadata and statistics. Includes the primary replica and geo-replicas when collected on the primary, and secondary replicas when collected on a secondary. |
| Resource utilization | `sqldb_database_resource_utilization` | `00:00:15` | Each row represents CPU, Data IO, Log IO, and other resource consumption statistics for a database in a time interval. |
| Session statistics | `sqldb_database_session_stats` | `00:01:00` | Each row represents a summary of session statistics for a database, aggregated by non-additive session attributes such as login name, host name, application name, etc. |
| SOS schedulers | `sqldb_database_sos_schedulers` | `00:01:00` | Each row represents a SOS scheduler and includes statistics for the scheduler, CPU node, and memory node. |
| Storage IO | `sqldb_database_storage_io` | `00:00:10` | Each row represents a database file and includes cumulative IOPS, throughput, and latency statistics for the file. |
| Storage utilization | `sqldb_database_storage_utilization` | `00:01:00` | Each row represents a database and includes its storage usage, including `tempdb`, Query Store, and Persistent Version Store. |
| Table metadata | `sqldb_database_table_metadata` | `00:30:00` | Each row represents a table or an indexed view, and includes metadata such as row count, space usage, data compression, columns, and constraints.  |
| Wait statistics | `sqldb_database_wait_stats` | `00:00:10` | Each row represents a wait type and includes cumulative wait statistics of the database engine instance. For databases in an elastic pool, only database-scoped wait statistics are collected. |

> [!NOTE]
> For databases in an elastic pool, the **SQL database** datasets containing pool-level data are not collected. This includes the **Memory utilization**, **Out-of-memory events**, **Performance counters (common)**, and **Performance counters (detailed)** datasets. The **Wait statistics** dataset is collected but contains only database-scoped waits. This avoids collection of the same data from every database in the pool.
>
> Pool-level data is collected in the **SQL elastic pool** datasets. For a given elastic pool, the **Performance counters (common)** and **Performance counters (detailed)** datasets contain pool-level metrics and certain database-level metrics such as **CPU**, **Data IO**, **Log write**, **Requests**, **Transactions**, etc.

# [SQL elastic pool](#tab/sqlep)

| Dataset name | Table name | Collection frequency (hh:mm:ss) | Description |
|:--|:--|--:|:--|
| Memory utilization | `sqldb_elastic_pool_memory_utilization` | `00:00:10` | Each row represents a memory clerk and includes memory consumption by the clerk on the database engine instance. |
| Out-of-memory events | `sqldb_elastic_pool_oom_events` | `00:01:00` | Each row represents an out-of-memory event in the database engine. |
| Performance counters (common) | `sqldb_elastic_pool_performance_counters_common` | `00:00:10` | Each row represents a performance counter of the database engine instance. This dataset includes commonly used counters, including workload group resource usage statistics for each database in the elastic pool. |
| Performance counters (detailed) | `sqldb_elastic_pool_performance_counters_detailed` | `00:01:00` | Each row represents a performance counter of the database engine instance. This dataset includes counters that might be needed for detailed monitoring and troubleshooting. |
| Properties | `sqldb_elastic_pool_properties` | `00:05:00` | Each row represents an elastic pool, and includes resource governance limits and other metadata for the elastic pool. |
| Resource utilization | `sqldb_elastic_pool_resource_utilization` | `00:00:20` | Each row represents CPU, Data IO, Log IO, and other resource consumption statistics for an elastic pool in a time interval. |
| SOS schedulers | `sqldb_elastic_pool_sos_schedulers` | `00:01:00` | Each row represents a SOS scheduler and includes statistics for the scheduler, CPU node, and memory node. |
| Storage IO | `sqldb_elastic_pool_storage_io` | `00:00:10` | Each row represents a database file and includes cumulative IOPS, throughput, and latency statistics for the file. Files for all databases in the elastic pool are included. |
| Storage utilization | `sqldb_elastic_pool_storage_utilization` | `00:01:00` | Each row represents an elastic pool and includes its storage usage statistics, including `tempdb`. |
| Wait statistics | `sqldb_elastic_pool_wait_stats` | `00:00:10` | Each row represents a wait type and includes wait statistics of the database engine instance. |

# [SQL managed instance](#tab/sqlmi)

| Dataset name | Table name | Collection frequency (hh:mm:ss) | Description |
|:--|:--|--:|:--|
| Active sessions | `sqlmi_active_sessions` | `00:00:30` | Each row represents a session that is running a request, is a blocker, or has an open transaction. |
| Backup history | `sqlmi_sql_backup_history` | `00:05:00` | Each row represents a successfully completed database backup. |
| Change processing | `sqlmi_change_processing` | `00:01:00` | Each row represents a snapshot of aggregate log scan statistics for a change processing feature such as Change Data Capture. |
| Change processing errors | `sqlmi_change_processing_errors` | `00:01:00` | Each row represents an error that occurred during change processing, when using a change processing feature such as Change Data Capture. |
| Connectivity | `sqlmi_connectivity` | `00:00:30` | Each row represents a connectivity probe (a login and a query) for a SQL managed instance. |
| Database geo-replicas | `sqlmi_database_geo_replicas` | `00:05:00` | Each row represents a primary or a secondary database geo-replica, including geo-replication metadata and statistics. |
| Database properties | `sqlmi_database_properties` | `00:05:00` | Each row represents a database and includes database options and other database metadata. |
| Database replicas | `sqlmi_database_replicas` | `00:05:00` | Each row represents a database replica, including replication metadata and statistics. Includes the primary replica and geo-replicas when collected on the primary, and secondary replicas when collected on a secondary. |
| Database storage utilization | `sqlmi_database_storage_utilization` | `00:05:00` | Each row represents a database and includes its storage usage, including Query Store and Persistent Version Store. |
| Index metadata | `sqlmi_index_metadata` | `00:30:00` | Each row represents an index partition and includes index definition, properties, and operational statistics. |
| Instance properties | `sqlmi_instance_properties` | `00:05:00` | Each row represents a SQL managed instance and includes its properties and other instance metadata. |
| Memory utilization | `sqlmi_memory_utilization` | `00:00:10` | Each row represents a memory clerk and includes memory consumption by the clerk. |
| Missing indexes | `sqlmi_missing_indexes` | `00:15:00` | Each row represents an index that might improve query performance if created. |
| Out-of-memory events | `sqlmi_oom_events` | `00:01:00` | Each row represents an out-of-memory event in the database engine. |
| Performance counters (common) | `sqlmi_performance_counters_common` | `00:00:10` | Each row represents a performance counter. This dataset includes commonly used counters. |
| Performance counters (detailed) | `sqlmi_performance_counters_detailed` | `00:01:00` | Each row represents a performance counter. This dataset includes counters that might be needed for detailed monitoring and troubleshooting. |
| Query runtime statistics | `sqlmi_query_runtime_stats` | `00:15:00` | Each row represents a Query Store runtime interval and includes query execution statistics for a database. |
| Query wait statistics | `sqlmi_query_wait_stats` | `00:15:00` | Each row represents a Query Store runtime interval and includes wait category statistics for a database. |
| Resource utilization | `sqlmi_resource_utilization` | `00:00:20` | Each row represents CPU, Data IO, Log IO and other resource consumption statistics in a time interval. |
| Session statistics | `sqlmi_session_stats` | `00:01:00` | Each row represents a summary of session statistics for a managed instance, aggregated by non-additive session attributes such as login name, host name, application name, etc. |
| SOS schedulers | `sqlmi_sos_schedulers` | `00:01:00` | Each row represents a SOS scheduler and includes statistics for the scheduler, CPU node, and memory node. |
| SQL Agent job history | `sqlmi_sqlagent_job_history` | `00:01:00` | Each row represents a SQL Agent job history entry. |
| SQL Agent job state | `sqlmi_sqlagent_job_state` | `00:00:20` | Each row represents the state of a SQL Agent job at a point in time. |
| Storage IO | `sqlmi_storage_io` | `00:00:10` | Each row represents a database file and includes cumulative IOPS, throughput, and latency statistics for the file. |
| Table metadata | `sqlmi_table_metadata` | `01:00:00` | Each row represents a table or an indexed view, and includes metadata such as row count, space usage, data compression, columns, and constraints. |
| Wait statistics | `sqlmi_wait_stats` | `00:00:10` | Each row represents a wait type and includes cumulative wait statistics of the database engine instance. |

---

### Common columns

For each target type, datasets have common columns, as described in the following tables.

# [SQL database](#tab/sqldb)

| Column name | Description |
|:--|:--|
| `subscription_id` | The Azure subscription ID of the SQL database. |
| `resource_group_name` | The resource group name of the SQL database. |
| `resource_id` | The Azure resource ID of the SQL database. |
| `sample_time_utc` | The time when the values in the row were observed, in UTC. |
| `collection_time_utc` | The time when the row was collected by the watcher, in UTC. This column is present in datasets where collection time might be different from sample time. |
| `replica_type` | One of: **Primary**, **HA secondary**, **Geo-replication forwarder**, **Named secondary**. |
| `logical_server_name` | The name of the [logical server](./database/logical-servers.md) in Azure SQL Database containing the monitored database or elastic pool. |
| `database_name` | The name of the monitored database. |
| `database_id` | Database ID of the monitored database, unique within the logical server. |
| `logical_database_id` | A unique database identifier that remains unchanged over the lifetime of the user database. Renaming the database or changing its service objective do not change this value. |
| `physical_database_id` | A unique database identifier for the current physical database corresponding to the user database. Changing database service objective causes this value to change. |
| `replica_id` | A unique identifier for a Hyperscale [compute replica](./database/hyperscale-architecture.md). |

# [SQL elastic pool](#tab/sqlep)

| Column name | Description |
|:--|:--|
| `subscription_id` | The Azure subscription ID of the SQL elastic pool. |
| `resource_group_name` | The resource group name of the SQL elastic pool. |
| `resource_id` | The Azure resource ID of the SQL elastic pool. |
| `sample_time_utc` | The time when the values in the row were observed, in UTC. |
| `collection_time_utc` | The time when the row was collected by the watcher, in UTC. This column is present in datasets where collection time might be different from sample time. |
| `replica_type` | One of: **Primary**, **HA secondary**. |
| `logical_server_name` | The name of the [logical server](./database/logical-servers.md) in Azure SQL Database containing the monitored database or elastic pool. |
| `elastic_pool_name` | The name of the monitored elastic pool. |
| `anchor_database_name` | The name of the anchor database for an elastic pool. |
| `anchor_database_id` | Database ID of the anchor database for an elastic pool, unique within the logical server. |
| `anchor_logical_database_id` | A unique database identifier that remains unchanged over the lifetime of the anchor database. |
| `anchor_physical_database_id` | A unique database identifier for the current physical database corresponding to the anchor database. |
| `anchor_replica_id` | A unique identifier for a Hyperscale compute replica of the anchor database. |

# [SQL managed instance](#tab/sqlmi)

| Column name | Description |
|:--|:--|
| `subscription_id` | The Azure subscription ID of the SQL managed instance. |
| `resource_group_name` | The resource group name of the SQL managed instance. |
| `resource_id` | The Azure resource ID of the SQL managed instance. |
| `sample_time_utc` | The time when the values in the row were observed, in UTC. |
| `collection_time_utc` | The time when the row was collected by the watcher, in UTC. This column is present in datasets where collection time might be different from sample time. |
| `replica_type` | One of: **Primary**, **HA secondary**, **Geo-replication forwarder**. |
| `managed_instance_name` | The name of the monitored SQL managed instance. |

---

A dataset has both `sample_time_utc` and `collection_time_utc` columns if it contains samples observed before the row was collected by database watcher. Otherwise, the observation time and collection time are the same, and the dataset contains only the `sample_time_utc` column.

For example, the `sqldb_database_resource_utilization` dataset is derived from the [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database) dynamic management view (DMV). The DMV contains the `end_time` column, which is the observation time for each row reporting aggregate resource statistics for a 15-second interval. This time is reported in the `sample_time_utc` column. When database watcher queries this DMV, the result set might contain multiple rows, each with a different `end_time`. All of these rows have the same `collection_time_utc` value.

## Related content

- [Monitor Azure SQL workloads with database watcher (preview)](database-watcher-overview.md)
- [Quickstart: Create a database watcher to monitor Azure SQL (preview)](database-watcher-quickstart.md)
- [Create and configure a database watcher (preview)](database-watcher-manage.md)
- [Analyze database watcher monitoring data (preview)](database-watcher-analyze.md)
- [Database watcher FAQ](database-watcher-faq.yml)
