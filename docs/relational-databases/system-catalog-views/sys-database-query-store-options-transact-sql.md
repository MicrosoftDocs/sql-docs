---
title: sys.database_query_store_options (Transact-SQL)
description: sys.database_query_store_options returns the Query Store options for this database.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, randolphwest
ms.date: 05/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "DATABASE_QUERY_STORE_OPTIONS_TSQL"
  - "DATABASE_QUERY_STORE_OPTIONS"
  - "SYS.DATABASE_QUERY_STORE_OPTIONS_TSQL"
  - "SYS.DATABASE_QUERY_STORE_OPTIONS"
helpviewer_keywords:
  - "sys.database_query_store_options catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || =azure-sqldw-latest || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# sys.database_query_store_options (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

Returns the Query Store options for this database.

| Column name | Data type | Description |
| --- | --- | --- |
| `desired_state` | **smallint** | Indicates the desired operation mode of Query Store, explicitly set by user.<br /><br />`0` = `OFF`<br />`1` = `READ_ONLY`<br />`2` = `READ_WRITE`<br />`4` = `READ_CAPTURE_SECONDARY` |
| `desired_state_desc` | **nvarchar(60)** | Textual description of the desired operation mode of Query Store:<br /><br />`OFF`<br />`READ_ONLY`<br />`READ_WRITE`<br />`READ_CAPTURE_SECONDARY` |
| `actual_state` | **smallint** | Indicates the operation mode of Query Store. In addition to list of desired states required by the user, actual state can be an error state.<br /><br />`0` = `OFF`<br />`1` = `READ_ONLY`<br />`2` = `READ_WRITE`<br />`3` = `ERROR`<br />`4` = `READ_CAPTURE_SECONDARY` |
| `actual_state_desc` | **nvarchar(60)** | Textual description of the actual operation mode of Query Store.<br /><br />`OFF`<br />`READ_ONLY`<br />`READ_WRITE`<br />`ERROR`<br />`READ_CAPTURE_SECONDARY`<br /><br />There are situations when actual state is different from the desired state:<br />- If the database is set to read-only mode, or if Query Store size exceeds its configured quota, Query Store might operate in read-only mode even if you specify read-write.<br />- In extreme scenarios Query Store can enter an ERROR state because of internal errors. In [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, if this happens, Query Store can be recovered by executing the `sp_query_store_consistency_check` stored procedure in the affected database. If running `sp_query_store_consistency_check` doesn't work, or if you're using [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you need to clear the data by running `ALTER DATABASE [YourDatabaseName] SET QUERY_STORE CLEAR ALL;` |
| `readonly_reason` | **int** | When the `desired_state_desc` is `READ_WRITE` and the `actual_state_desc` is `READ_ONLY`, `readonly_reason` returns a bit map to indicate why the Query Store is in readonly mode.<br /><br />`1` - database is in read-only mode<br />`2` - database is in single-user mode<br />`4` - database is in emergency mode<br />`8` - database is secondary replica (applies to availability groups and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] geo-replication). This value can be effectively observed only on `readable` secondary replicas<br />`65536` - the Query Store reached the size limit set by the `max_storage_size_mb` option. For more information about this option, see [ALTER DATABASE SET options](../../t-sql/statements/alter-database-transact-sql-set-options.md).<br />`131072` - The number of different statements in Query Store reached the internal memory limit. Consider removing queries that you don't need or upgrading to a higher service tier to enable transferring Query Store to read-write mode.<br />`262144` - Size of in-memory items waiting to be persisted on disk reached the internal memory limit. Query Store is in read-only mode temporarily until the in-memory items are persisted on disk.<br />`524288` - Database reached disk size limit. Query Store is part of user database, so if there's no more available space for a database, that means that Query Store can't grow further anymore.<br /><br />To switch the Query Store operations mode back to read-write, see [Verify that Query Store collects query data continuously](../../relational-databases/performance/best-practice-with-the-query-store.md#Verify). |
| `current_storage_size_mb` | **bigint** | Size of Query Store on disk in megabytes. |
| `flush_interval_seconds` | **bigint** | The period for regular flushing of Query Store data to disk in seconds. Default value is `900` (15 min).<br /><br />Change by using the `ALTER DATABASE <database> SET QUERY_STORE (DATA_FLUSH_INTERVAL_SECONDS = <interval>)` statement. |
| `interval_length_minutes` | **bigint** | The statistics aggregation interval in minutes. Arbitrary values aren't allowed. Use one of the following values: `1`, `5`, `10`, `15`, `30`, `60`, and `1440` minutes. The default value is 60 minutes. |
| `max_storage_size_mb` | **bigint** | Maximum disk size for the Query Store in megabytes (MB). Default value is 100 MB up to [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)], and 1 GB in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions.<br /><br />For [!INCLUDE [sssds](../../includes/sssds-md.md)] Premium edition, the default is 1 GB, and for [!INCLUDE [sssds](../../includes/sssds-md.md)] Basic edition, the default is 10 MB.<br /><br />Change by using the `ALTER DATABASE <database> SET QUERY_STORE (MAX_STORAGE_SIZE_MB = <size>)` statement. |
| `stale_query_threshold_days` | **bigint** | Number of days that the information for a query is kept in the Query Store. Default value is `30`. Set to `0` to disable the retention policy.<br />For [!INCLUDE [sssds](../../includes/sssds-md.md)] Basic edition, the default is 7 days.<br /><br />Change by using the `ALTER DATABASE <database> SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = <value>))` statement. |
| `max_plans_per_query` | **bigint** | Limits the maximum number of stored plans. Default value is `200`. If the maximum value is reached, Query Store stops capturing new plans for that query. Setting to `0` removes the limitation for the number of captured plans.<br /><br />Change by using the `ALTER DATABASE<database> SET QUERY_STORE (MAX_PLANS_PER_QUERY = <n>)` statement. |
| `query_capture_mode` | **smallint** | The currently active query capture mode:<br /><br />`1` = `ALL` - all queries are captured. This is the default configuration value for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.<br /><br />`2` = `AUTO` - capture relevant queries based on execution count and resource consumption. This is the default configuration value for [!INCLUDE [sssds](../../includes/sssds-md.md)].<br /><br />`3` = `NONE` - stop capturing new queries. Query Store continues to collect compile and runtime statistics for queries that were captured already. Use this configuration cautiously since you might miss capturing important queries.<br /><br />`4` = `CUSTOM` - Allows more control over the query capture policy using the [QUERY_CAPTURE_POLICY options](../../t-sql/statements/alter-database-transact-sql-set-options.md#SettingOptions).<br /><br />**Applies to**: [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)] and later versions. |
| `query_capture_mode_desc` | **nvarchar(60)** | Textual description of the actual capture mode of Query Store:<br /><br />`ALL` (default for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions)<br /><br />`AUTO` (default for [!INCLUDE [sssds](../../includes/sssds-md.md)])<br /><br />`NONE`<br /><br />`CUSTOM` |
| `capture_policy_execution_count` | **int** | Query capture mode `CUSTOM` policy option. Defines the number of times a query is executed over the evaluation period. The default is `30`.<br /><br />**Applies to**: [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)] and later versions. |
| `capture_policy_total_compile_cpu_time_ms` | **bigint** | Query capture mode `CUSTOM` policy option. Defines total elapsed compile CPU time used by a query over the evaluation period. The default is 1000.<br /><br />**Applies to**: [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)] and later versions. |
| `capture_policy_total_execution_cpu_time_ms` | **bigint** | Query capture mode `CUSTOM` policy option. Defines total elapsed execution CPU time used by a query over the evaluation period. The default is `100`.<br /><br />**Applies to**: [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)] and later versions. |
| `capture_policy_stale_threshold_hours` | **int** | Query capture mode `CUSTOM` policy option. Defines the evaluation interval period to determine if a query should be captured. The default is 24 hours.<br /><br />**Applies to**: [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)] and later versions. |
| `size_based_cleanup_mode` | **smallint** | Controls whether cleanup is automatically activated when total amount of data gets close to maximum size:<br /><br />`0` = `OFF` - size-based cleanup aren't automatically activated.<br />`1` = `AUTO` - size-based cleanup is automatically activated when size on disk reaches *90 percent* of `max_storage_size_mb`. This is the default configuration value.<br /><br />Size-based cleanup removes the least expensive and oldest queries first. It stops when approximately *80 percent* of `max_storage_size_mb` is reached. |
| `size_based_cleanup_mode_desc` | **nvarchar(60)** | Textual description of the actual size-based cleanup mode of Query Store:<br /><br />`OFF`<br />`AUTO` (default) |
| `wait_stats_capture_mode` | **smallint** | Controls whether Query Store performs capture of wait statistics:<br /><br />`0` = `OFF`<br />`1` = `ON`<br /><br />**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions. |
| `wait_stats_capture_mode_desc` | **nvarchar(60)** | Textual description of the actual wait statistics capture mode:<br /><br />`OFF`<br />`ON` (default)<br /><br />**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions. |
| `actual_state_additional_info` | **nvarchar(4000)** | Currently unused. |

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Remarks

An `actual_state_desc` value of `READ_CAPTURE_SECONDARY` is the expected state when Query Store for secondary replicas is enabled. For more information, see [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md).

## Related content

- [sys.query_context_settings (Transact-SQL)](sys-query-context-settings-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [sys.query_store_query_text (Transact-SQL)](sys-query-store-query-text-transact-sql.md)
- [sys.query_store_runtime_stats (Transact-SQL)](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_wait_stats (Transact-SQL)](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats_interval (Transact-SQL)](sys-query-store-runtime-stats-interval-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL)](../system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](../system-stored-procedures/query-store-stored-procedures-transact-sql.md)
