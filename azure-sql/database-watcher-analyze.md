---
title: Analyze database watcher monitoring data
titleSuffix: Azure SQL Database & SQL Managed Instance
description: Examples of analytical queries that use database watcher monitoring data
author: dimitri-furman
ms.author: dfurman
ms.date: 07/10/2024
ms.service: sql-db-mi
ms.subservice: monitoring
ms.topic: how-to
ms.custom:
  - subject-monitoring
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---

# Analyze database watcher monitoring data (preview)

[!INCLUDE [sqldb-sqlmi](./includes/appliesto-sqldb-sqlmi.md)]

In addition to using [dashboards in Azure portal](database-watcher-overview.md#dashboards), or building visualizations to view and analyze SQL monitoring data in [Power BI](/azure/data-explorer/power-bi-data-connector), [Grafana](/azure/data-explorer/grafana), [Azure Data Explorer](/azure/data-explorer/data-explorer-overview) or [Real-Time Analytics in Microsoft Fabric](/fabric/real-time-analytics/overview), you can query your monitoring data store directly.

This article contains examples of [KQL](#use-kql-to-analyze-monitoring-data) and [T-SQL](#use-t-sql-to-analyze-monitoring-data) queries that help you get started with analyzing collected monitoring data.

## Use KQL to analyze monitoring data

To analyze collected monitoring data, the recommended method is to use the [Kusto Query Language](/azure/data-explorer/kusto/query/) (KQL). KQL is optimal for querying telemetry, metrics, and logs. It provides extensive support for text search and parsing, time-series operators and functions, analytics and aggregation, and many other language constructs that facilitate data analysis.

KQL is conceptually similar to SQL. It operates on schema entities such as tables and columns, and supports relational operations such as project, restrict, join, and summarize, corresponding to the `SELECT`, `JOIN`, `WHERE`, and `GROUP BY` clauses in SQL.

To write and execute KQL queries, you can use either [Kusto Explorer](/azure/data-explorer/kusto/tools/kusto-explorer) or the [Azure Data Explorer](/azure/data-explorer/data-explorer-overview) [web UI](/azure/data-explorer/web-ui-query-overview). Kusto Explorer is a full-featured Windows desktop software, while the Azure Data Explorer web UI lets you execute KQL queries and visualize results in the browser on any platform.

You can also use these tools to query a database in Real-Time Analytics in Microsoft Fabric. To connect, add a new connection using the **Query URI** of your Real-Time Analytics database. Additionally, if you use Real-Time Analytics, you can analyze monitoring data using [KQL querysets](/fabric/real-time-analytics/create-query-set). A KQL queryset can be saved as a shareable Fabric artifact and used to create Power BI reports.

If you are new to KQL, the following resources can help you get started:

- [Write your first query with Kusto Query Language](/training/modules/write-first-query-kusto-query-language/)
- [Quickstart: Query sample data](/azure/data-explorer/web-query-data)
- [Tutorial: Learn common operators](/azure/data-explorer/kusto/query/tutorials/learn-common-operators)

The following examples can help you write your own KQL queries to view and analyze collected SQL monitoring data. You can also use these examples as a starting point in building your own data visualizations and dashboards.

### Use KQL to query resource consumption over time

In this example, the query returns resource consumption metrics (CPU, workers, log write throughput, etc.) for the primary replica of a database, an elastic pool, or a SQL managed instance over the last one hour. In addition to returning the result set, it visualizes it as a time chart.

In this and other examples, change the variables in the [let](/azure/data-explorer/kusto/query/letstatement) statements to match the names of your server, database, elastic pool, or SQL managed instance. To use a different time interval, change the `duration` variable. For more information, see [timespan literals](/azure/data-explorer/kusto/query/scalar-data-types/timespan#timespan-literals).

# [SQL database](#tab/sqldb)

```kusto
let logicalServer = @"your-server-name";
let databaseName = @"your-database-name";
let replicaType = "Primary";
let duration = 1h;
sqldb_database_resource_utilization
| where sample_time_utc > ago(duration)
| where logical_server_name =~ logicalServer
| where database_name =~ databaseName
| where replica_type =~ replicaType
| project sample_time_utc,
          avg_cpu_percent,
          avg_instance_cpu_percent,
          avg_data_io_percent,
          avg_log_write_percent,
          max_worker_percent
| sort by sample_time_utc desc
| render timechart;
```

# [SQL elastic pool](#tab/sqlep)

```kusto
let logicalServer = @"your-server-name";
let elasticPoolName = @"your-elastic-pool-name";
let replicaType = "Primary";
let duration = 1h;
sqldb_elastic_pool_resource_utilization
| where sample_time_utc > ago(duration)
| where logical_server_name =~ logicalServer
| where replica_type =~ replicaType
| project sample_time_utc,
          avg_cpu_percent,
          avg_instance_cpu_percent,
          avg_data_io_percent,
          avg_log_write_percent,
          max_worker_percent
| sort by sample_time_utc desc
| render timechart;
```

# [SQL managed instance](#tab/sqlmi)

```kusto
let instanceFqdnName = @"your-instance-name.unique-dns-prefix.database.windows.net";
let replicaType = "Primary";
let duration = 1h;
sqlmi_resource_utilization
| where sample_time_utc > ago(duration)
| where managed_instance_name =~ instanceFqdnName
| where replica_type =~ replicaType
| project sample_time_utc,
          avg_instance_cpu_percent,
          avg_instance_log_write_percent,
          instance_worker_percent
| sort by sample_time_utc desc
| render timechart;
```

---

### Use KQL to view database, elastic pool, or SQL managed instance properties

In this example, the query returns a set of all databases, elastic pools, or SQL managed instances from which at least one sample in the corresponding **Properties** [dataset](database-watcher-data.md#datasets) was collected in the last one day. In other words, each row represents a monitoring target with its most recently observed properties.

The [arg_max()](/azure/data-explorer/kusto/query/arg-max-aggfunction) function aggregates data to return the latest row for the specified set of columns that identify a target. For example, for Azure SQL databases, this set is `logical_server_name`, `database_name`, `replica_type`.

# [SQL database](#tab/sqldb)

```kusto
let duration = 1d;
sqldb_database_properties
| where sample_time_utc > ago(duration)
| summarize arg_max(sample_time_utc, *) by logical_server_name, database_name, replica_type
| project-rename last_sample_time_utc = sample_time_utc
| sort by tolower(logical_server_name) asc,
          tolower(database_name) asc,
          case(
              replica_type == "Primary", 0,
              replica_type == "Geo-replication forwarder", 1,
              replica_type == "Named secondary", 2,
              replica_type == "HA secondary", 3,
              4) asc;
```

# [SQL elastic pool](#tab/sqlep)

```kusto
let duration = 1d;
sqldb_elastic_pool_properties
| where sample_time_utc > ago(duration)
| summarize arg_max(sample_time_utc, *) by logical_server_name, elastic_pool_name, replica_type
| project-rename last_sample_time_utc = sample_time_utc
| sort by tolower(logical_server_name) asc,
          tolower(elastic_pool_name) asc,
          case(
              replica_type == "Primary", 0,
              replica_type == "HA secondary", 1,
              2) asc;
```

# [SQL managed instance](#tab/sqlmi)

```kusto
let duration = 1d;
sqlmi_instance_properties
| where sample_time_utc > ago(duration)
| summarize arg_max(sample_time_utc, *) by managed_instance_name, replica_type
| project-rename last_sample_time_utc = sample_time_utc
| sort by tolower(managed_instance_name),
          case(
              replica_type == "Primary", 0,
              replica_type == "Geo-replication forwarder", 1,
              replica_type == "HA secondary", 2,
              3) asc;
```

---

### Use KQL to view query runtime statistics

This query returns the top resource consuming queries in your Azure SQL estate. Change a variable to rank queries by any [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) metric, including CPU time, elapsed time, execution count, etc. You can also set variables to filter by a time interval, query execution type, and query text. Set variables to focus on a specific logical server, elastic pool, SQL managed instance, or database.

The query uses the **Query runtime statistics** [dataset](database-watcher-data.md#datasets) to return the number of top queries you specify, and includes their ranking by every other resource consumption metric.

# [SQL database](#tab/sqldb)

```kusto
let topQueriesBy = "cpu_time"; // Set to one of the following metrics to return the top resource consuming queries:
// count_executions, duration, cpu_time, logical_io_reads, logical_io_writes, physical_io_reads, 
// num_physical_io_reads, clr_time, dop, query_max_used_memory, rowcount, log_bytes_used, tempdb_space_used 
let topQueries = 10; // Set the number of top queries to return
let endTime = now();
let startTime = endTime - 1d;
let logicalServerName = @""; // Optionally filter by logical server name
let elasticPoolName = @""; // Optionally filter by elastic pool name, if any databases are in elastic pools
let databaseName = @""; // Optionally filter by database name
let executionType = ""; // Optionally filter by execution type. Use Regular, Aborted, Exception.
let queryHash = ""; // Optionally filter by query hash (example: 0xBAAA461A6C93EA88)
let queryTextFragment = ""; // Optionally filter by a query text fragment
sqldb_database_query_runtime_stats
| where interval_start_time >= startTime and interval_end_time <= endTime
| where isempty(executionType) or execution_type_desc =~ executionType
| where isempty(logicalServerName) or logical_server_name =~ logicalServerName
| where isempty(elasticPoolName) or elastic_pool_name =~ elasticPoolName
| where isempty(databaseName) or database_name =~ databaseName
| summarize dcount_logical_servers = dcount(logical_server_name),
            any_logical_server_name = take_any(logical_server_name),
            dcount_elastic_pools = dcount(strcat(logical_server_name, "|", elastic_pool_name)),
            any_elastic_pool_name = take_any(elastic_pool_name),
            dcount_databases = dcount(strcat(logical_server_name, "|", database_name)),
            any_database_name = take_any(database_name),
            dcount_sql_module_name = dcount(sql_module_name),
            any_sql_module_name = take_any(sql_module_name),
            dcount_context_settings_id = dcount(context_settings_id),
            any_context_settings_id = take_any(context_settings_id),
            query_sql_text = take_any(query_sql_text),
            count_executions = sum(toreal(count_executions)),
            count_successful_executions = sumif(toreal(count_executions), execution_type_desc == "Regular"),
            count_aborted_executions = sumif(toreal(count_executions), execution_type_desc == "Aborted"),
            count_exception_executions = sumif(toreal(count_executions), execution_type_desc == "Exception"),
            duration_us = sum(avg_duration_us * count_executions),
            cpu_time_us = sum(avg_cpu_time_us * count_executions),
            logical_io_reads = sum(avg_logical_io_reads * count_executions),
            logical_io_writes = sum(avg_logical_io_writes * count_executions),
            physical_io_reads = sum(avg_physical_io_reads * count_executions),
            num_physical_io_reads = sum(avg_num_physical_io_reads * count_executions),
            clr_time_us = sum(avg_clr_time_us * count_executions),
            dop = sumif(avg_dop * count_executions, is_parallel_plan),
            query_max_used_memory = sum(avg_query_max_used_memory * count_executions),
            rowcount = sum(avg_rowcount * count_executions),
            log_bytes_used = sum(avg_log_bytes_used * count_executions),
            tempdb_space_used = sum(avg_tempdb_space_used * count_executions)
            by query_hash
| project logical_server_name = iif(dcount_logical_servers == 1, any_logical_server_name, strcat(any_logical_server_name, " (+", tostring(dcount_logical_servers - 1), ")")),
          elastic_pool_name = iif(dcount_elastic_pools == 1, any_elastic_pool_name, strcat(any_elastic_pool_name, " (+", tostring(dcount_elastic_pools - 1), ")")),
          database_name = iif(dcount_databases == 1, any_database_name, strcat(any_database_name, " (+", tostring(dcount_databases - 1), ")")),
          query_sql_text,
          count_executions,
          count_successful_executions,
          count_aborted_executions,
          count_exception_executions,
          duration_us,
          cpu_time_us,
          logical_io_reads,
          logical_io_writes,
          physical_io_reads,
          num_physical_io_reads,
          clr_time_us,
          dop,
          query_max_used_memory_kb = query_max_used_memory * 8,
          rowcount,
          log_bytes_used,
          tempdb_space_used_kb = tempdb_space_used * 8,
          sql_module_name = iif(dcount_sql_module_name == 1, any_sql_module_name, strcat(any_sql_module_name, " (+", tostring(dcount_sql_module_name - 1), ")")),
          context_settings_id = iif(dcount_context_settings_id == 1, tostring(any_context_settings_id), strcat(any_context_settings_id, " (+", tostring(dcount_context_settings_id - 1), ")")),
          query_hash
| sort by count_executions desc | extend count_executions_rank = row_rank_dense(count_executions)
| sort by duration_us desc | extend duration_rank = row_rank_dense(duration_us)
| sort by cpu_time_us desc | extend cpu_time_rank = row_rank_dense(cpu_time_us)
| sort by logical_io_reads desc | extend logical_io_reads_rank = row_rank_dense(logical_io_reads)
| sort by logical_io_writes desc | extend logical_io_writes_rank = row_rank_dense(logical_io_writes)
| sort by physical_io_reads desc | extend physical_io_reads_rank = row_rank_dense(physical_io_reads)
| sort by num_physical_io_reads desc | extend num_physical_io_reads_rank = row_rank_dense(num_physical_io_reads)
| sort by clr_time_us desc | extend clr_time_rank = row_rank_dense(clr_time_us)
| sort by dop desc | extend dop_rank = row_rank_dense(dop)
| sort by query_max_used_memory_kb desc | extend query_max_used_memory_rank = row_rank_dense(query_max_used_memory_kb)
| sort by rowcount desc | extend rowcount_rank = row_rank_dense(rowcount)
| sort by log_bytes_used desc | extend log_bytes_used_rank = row_rank_dense(log_bytes_used)
| sort by tempdb_space_used_kb desc | extend tempdb_space_used_rank = row_rank_dense(tempdb_space_used_kb)
| sort by case(
              topQueriesBy =~ "count_executions", toreal(count_executions),
              topQueriesBy =~ "duration", toreal(duration_us),
              topQueriesBy =~ "cpu_time", toreal(cpu_time_us),
              topQueriesBy =~ "logical_io_reads", toreal(logical_io_reads),
              topQueriesBy =~ "logical_io_writes", toreal(logical_io_writes),
              topQueriesBy =~ "physical_io_reads", toreal(physical_io_reads),
              topQueriesBy =~ "num_physical_io_reads", toreal(num_physical_io_reads),
              topQueriesBy =~ "clr_time", toreal(clr_time_us),
              topQueriesBy =~ "dop", toreal(dop),
              topQueriesBy =~ "query_max_used_memory", toreal(query_max_used_memory_kb),
              topQueriesBy =~ "rowcount", toreal(rowcount),
              topQueriesBy =~ "log_bytes_used", toreal(log_bytes_used),
              topQueriesBy =~ "tempdb_space_used", toreal(tempdb_space_used_kb),
              real(null)
              ) desc,
          count_executions desc
| project-away count_executions
| where isempty(queryHash) or query_hash == queryHash
| where isempty(queryTextFragment) or query_sql_text contains queryTextFragment
| take topQueries;
```
# [SQL elastic pool](#tab/sqlep)

The **Query runtime statistics** [dataset](database-watcher-data.md#datasets) is available only for databases and SQL managed instances. This example does not apply to elastic pools.

The *SQL database* example also applies to databases in elastic pools.

# [SQL managed instance](#tab/sqlmi)

```kusto
let topQueriesBy = "cpu_time"; // Set to one of the following metrics to return top resource consuming queries:
// count_executions, duration, cpu_time, logical_io_reads, logical_io_writes, physical_io_reads, 
// num_physical_io_reads, clr_time, dop, query_max_used_memory, rowcount, log_bytes_used, tempdb_space_used 
let topQueries = 10; // Set the number of top queries to return
let endTime = now();
let startTime = endTime - 1d;
let instanceFqdnName = @""; // Optionally filter by managed instance name
let databaseName = @""; // Optionally filter by database name
let executionType = @""; // Optionally filter by execution type. Use Regular, Aborted, Exception.
let queryHash = ""; // Optionally filter by query hash (example: 0xBAAA461A6C93EA88)
let queryTextFragment = @""; // Optionally filter by a query text fragment
sqlmi_query_runtime_stats
| where interval_start_time >= startTime and interval_end_time <= endTime
| where isempty(executionType) or execution_type_desc =~ executionType
| where isempty(instanceFqdnName) or managed_instance_name =~ instanceFqdnName
| where isempty(databaseName) or database_name =~ databaseName
| summarize dcount_managed_instances = dcount(managed_instance_name),
            any_managed_instance_name = take_any(managed_instance_name),
            dcount_databases = dcount(strcat(managed_instance_name, "|", database_name)),
            any_database_name = take_any(database_name),
            dcount_sql_module_name = dcount(sql_module_name),
            any_sql_module_name = take_any(sql_module_name),
            dcount_context_settings_id = dcount(context_settings_id),
            any_context_settings_id = take_any(context_settings_id),
            query_sql_text = take_any(query_sql_text),
            count_executions = sum(toreal(count_executions)),
            count_successful_executions = sumif(toreal(count_executions), execution_type_desc == "Regular"),
            count_aborted_executions = sumif(toreal(count_executions), execution_type_desc == "Aborted"),
            count_exception_executions = sumif(toreal(count_executions), execution_type_desc == "Exception"),
            duration_us = sum(avg_duration_us * count_executions),
            cpu_time_us = sum(avg_cpu_time_us * count_executions),
            logical_io_reads = sum(avg_logical_io_reads * count_executions),
            logical_io_writes = sum(avg_logical_io_writes * count_executions),
            physical_io_reads = sum(avg_physical_io_reads * count_executions),
            num_physical_io_reads = sum(avg_num_physical_io_reads * count_executions),
            clr_time_us = sum(avg_clr_time_us * count_executions),
            dop = sumif(avg_dop * count_executions, is_parallel_plan),
            query_max_used_memory = sum(avg_query_max_used_memory * count_executions),
            rowcount = sum(avg_rowcount * count_executions),
            log_bytes_used = sum(avg_log_bytes_used * count_executions),
            tempdb_space_used = sum(avg_tempdb_space_used * count_executions)
            by query_hash
| project logical_server_name = iif(dcount_managed_instances == 1, any_managed_instance_name, strcat(any_managed_instance_name, " (+", tostring(dcount_managed_instances - 1), ")")),
          database_name = iif(dcount_databases == 1, any_database_name, strcat(any_database_name, " (+", tostring(dcount_databases - 1), ")")),
          query_sql_text,
          count_executions,
          count_successful_executions,
          count_aborted_executions,
          count_exception_executions,
          duration_us,
          cpu_time_us,
          logical_io_reads,
          logical_io_writes,
          physical_io_reads,
          num_physical_io_reads,
          clr_time_us,
          dop,
          query_max_used_memory_kb = query_max_used_memory * 8,
          rowcount,
          log_bytes_used,
          tempdb_space_used_kb = tempdb_space_used * 8,
          sql_module_name = iif(dcount_sql_module_name == 1, any_sql_module_name, strcat(any_sql_module_name, " (+", tostring(dcount_sql_module_name - 1), ")")),
          context_settings_id = iif(dcount_context_settings_id == 1, tostring(any_context_settings_id), strcat(any_context_settings_id, " (+", tostring(dcount_context_settings_id - 1), ")")),
          query_hash
| sort by count_executions desc | extend count_executions_rank = row_rank_dense(count_executions)
| sort by duration_us desc | extend duration_rank = row_rank_dense(duration_us)
| sort by cpu_time_us desc | extend cpu_time_rank = row_rank_dense(cpu_time_us)
| sort by logical_io_reads desc | extend logical_io_reads_rank = row_rank_dense(logical_io_reads)
| sort by logical_io_writes desc | extend logical_io_writes_rank = row_rank_dense(logical_io_writes)
| sort by physical_io_reads desc | extend physical_io_reads_rank = row_rank_dense(physical_io_reads)
| sort by num_physical_io_reads desc | extend num_physical_io_reads_rank = row_rank_dense(num_physical_io_reads)
| sort by clr_time_us desc | extend clr_time_rank = row_rank_dense(clr_time_us)
| sort by dop desc | extend dop_rank = row_rank_dense(dop)
| sort by query_max_used_memory_kb desc | extend query_max_used_memory_rank = row_rank_dense(query_max_used_memory_kb)
| sort by rowcount desc | extend rowcount_rank = row_rank_dense(rowcount)
| sort by log_bytes_used desc | extend log_bytes_used_rank = row_rank_dense(log_bytes_used)
| sort by tempdb_space_used_kb desc | extend tempdb_space_used_rank = row_rank_dense(tempdb_space_used_kb)
| sort by case(
              topQueriesBy =~ "count_executions", toreal(count_executions),
              topQueriesBy =~ "duration", toreal(duration_us),
              topQueriesBy =~ "cpu_time", toreal(cpu_time_us),
              topQueriesBy =~ "logical_io_reads", toreal(logical_io_reads),
              topQueriesBy =~ "logical_io_writes", toreal(logical_io_writes),
              topQueriesBy =~ "physical_io_reads", toreal(physical_io_reads),
              topQueriesBy =~ "num_physical_io_reads", toreal(num_physical_io_reads),
              topQueriesBy =~ "clr_time", toreal(clr_time_us),
              topQueriesBy =~ "dop", toreal(dop),
              topQueriesBy =~ "query_max_used_memory", toreal(query_max_used_memory_kb),
              topQueriesBy =~ "rowcount", toreal(rowcount),
              topQueriesBy =~ "log_bytes_used", toreal(log_bytes_used),
              topQueriesBy =~ "tempdb_space_used", toreal(tempdb_space_used_kb),
              real(null)
              ) desc,
          count_executions desc
| project-away count_executions
| where isempty(queryHash) or query_hash == queryHash
| where isempty(queryTextFragment) or query_sql_text contains queryTextFragment
| take topQueries;
```

---

### Use KQL to analyze performance counters over time

In this example, the query returns performance counter values for a time interval that starts 30 minutes before the specified end time.

This example uses cumulative performance counters such as `Total request count` and `Query optimizations/sec`. Cumulative means that the counter value keeps increasing as SQL query activity occurs. The query in this example calculates the difference, or delta, between the counter value in each sample and its value in the previous sample to obtain the number of requests and optimizations that occurred since the previous sample, and then visualizes these metrics on a time chart.

# [SQL database](#tab/sqldb)

```kusto
let logicalServer = @"your-server-name";
let databaseName = @"your-database-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
sqldb_database_performance_counters_common
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where database_name =~ databaseName
| where replica_type =~ replicaType
| where cntr_type == 272696576 // restrict to cumulative counters
| where object_name =~ "Workload Group Stats" and counter_name in ("Total request count","Query optimizations/sec")
| project replica_id, sample_time_utc, object_name, counter_name, cntr_value
| sort by replica_id asc, counter_name asc, sample_time_utc asc
| extend delta_cntr_value = iif(cntr_value >= prev(cntr_value) and counter_name == prev(counter_name) and replica_id == prev(replica_id), cntr_value - prev(cntr_value), real(null)),
         delta_sample_time_utc = iif(sample_time_utc >= prev(sample_time_utc), datetime_diff("Millisecond", sample_time_utc, prev(sample_time_utc)), long(null))
| where isnotempty(delta_sample_time_utc)
| extend value = delta_cntr_value / delta_sample_time_utc * 1000
| summarize requests_per_sec = take_anyif(value, counter_name =~ "Total request count"),
            query_optimizations_per_sec = take_anyif(value, counter_name =~ "Query optimizations/sec")
            by sample_time_utc
| sort by sample_time_utc desc
| project sample_time_utc, requests_per_sec, query_optimizations_per_sec
| render timechart;
```

# [SQL elastic pool](#tab/sqlep)

```kusto
let logicalServer = @"your-server-name";
let elasticPoolName = @"your-elastic-pool-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
sqldb_elastic_pool_performance_counters_common
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where elastic_pool_name =~ elasticPoolName
| where replica_type =~ replicaType
| where cntr_type == 272696576 // restrict to cumulative counters
| where (object_name =~ "SQL Statistics" and counter_name in ("Batch Requests/sec"))
        or
        (object_name =~ "Workload Group Stats" and counter_name in ("Query optimizations/sec"))
| project anchor_database_replica_id, sample_time_utc, object_name, counter_name, database_name, cntr_value
| sort by anchor_database_replica_id asc, counter_name asc, database_name asc, sample_time_utc asc
| extend delta_cntr_value = iif(cntr_value >= prev(cntr_value) and counter_name == prev(counter_name) and database_name == prev(database_name) and anchor_database_replica_id == prev(anchor_database_replica_id), cntr_value - prev(cntr_value), real(null)),
         delta_sample_time_utc = iif(sample_time_utc >= prev(sample_time_utc), datetime_diff("Millisecond", sample_time_utc, prev(sample_time_utc)), long(null))
| where isnotempty(delta_sample_time_utc)
| extend value = delta_cntr_value / delta_sample_time_utc * 1000
// summarize at the database level
| summarize requests_per_sec = take_anyif(value, counter_name =~ "Batch Requests/sec"),
            query_optimizations_per_sec = take_anyif(value, counter_name =~ "Query optimizations/sec")
            by sample_time_utc, database_name
// summarize at the pool level
| summarize requests_per_sec = sum(requests_per_sec),
            query_optimizations_per_sec = sum(query_optimizations_per_sec)
            by sample_time_utc
| sort by sample_time_utc desc
| project sample_time_utc, requests_per_sec, query_optimizations_per_sec
| render timechart;
```

# [SQL managed instance](#tab/sqlmi)

```kusto
let instanceFqdnName = @"your-instance-name.unique-dns-prefix.database.windows.net";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
sqlmi_performance_counters_common
| where sample_time_utc between (startTime .. endTime)
| where managed_instance_name =~ instanceFqdnName
| where replica_type =~ replicaType
| where cntr_type == 272696576 // restrict to cumulative counters
| where (object_name =~ "SQL Statistics" and counter_name in ("Batch Requests/sec"))
        or
        (object_name =~ "Workload Group Stats" and counter_name in ("Query optimizations/sec"))
| project sample_time_utc, object_name, counter_name, database_name, cntr_value
| sort by counter_name asc, database_name asc, sample_time_utc asc
| extend delta_cntr_value = iif(cntr_value >= prev(cntr_value) and counter_name == prev(counter_name) and database_name == prev(database_name), cntr_value - prev(cntr_value), real(null)),
         delta_sample_time_utc = iif(sample_time_utc >= prev(sample_time_utc), datetime_diff("Millisecond", sample_time_utc, prev(sample_time_utc)), long(null))
| where isnotempty(delta_sample_time_utc)
| extend value = delta_cntr_value / delta_sample_time_utc * 1000
| summarize requests_per_sec = take_anyif(value, counter_name =~ "Batch Requests/sec"),
            query_optimizations_per_sec = take_anyif(value, counter_name =~ "Query optimizations/sec")
            by sample_time_utc
| sort by sample_time_utc desc
| project sample_time_utc, requests_per_sec, query_optimizations_per_sec
| render timechart;
```

---

The following example is for point-in-time performance counters that report the most recently observed value, such as `Active memory grants count`, `Pending memory grants count`, and `Processes blocked`. The time interval is the last 30 minutes.

# [SQL database](#tab/sqldb)

```kusto
let logicalServer = @"your-server-name";
let databaseName = @"your-database-name";
let replicaType = "Primary";
let duration = 30m;
sqldb_database_performance_counters_common
| where sample_time_utc > ago(duration)
| where logical_server_name =~ logicalServer
| where database_name =~ databaseName
| where replica_type =~ replicaType
| where cntr_type == 65792 // restrict to point-in-time counters
| where (object_name =~ "General Statistics" and counter_name in ("Processes blocked"))
        or
        (object_name =~ "Resource Pool Stats" and counter_name in ("Active memory grants count","Pending memory grants count"))
| project sample_time_utc, counter_name, cntr_value
| render timechart;
```

# [SQL elastic pool](#tab/sqlep)

```kusto
let logicalServer = @"your-server-name";
let elasticPoolName = @"your-elastic-pool-name";
let replicaType = "Primary";
let duration = 30m;
sqldb_elastic_pool_performance_counters_common
| where sample_time_utc > ago(duration)
| where logical_server_name =~ logicalServer
| where elastic_pool_name =~ elasticPoolName
| where replica_type =~ replicaType
| where cntr_type == 65792 // restrict to point-in-time counters
| where (object_name =~ "General Statistics" and counter_name in ("Processes blocked"))
        or
        (object_name =~ "Resource Pool Stats" and counter_name in ("Active memory grants count","Pending memory grants count"))
| project sample_time_utc, counter_name, cntr_value
| render timechart;
```

# [SQL managed instance](#tab/sqlmi)

```kusto
let instanceFqdnName = @"your-instance-name.unique-dns-prefix.database.windows.net";
let replicaType = "Primary";
let duration = 30m;
sqlmi_performance_counters_common
| where sample_time_utc > ago(duration)
| where managed_instance_name =~ instanceFqdnName
| where replica_type =~ replicaType
| where cntr_type == 65792 // restrict to point-in-time counters
| where (object_name =~ "General Statistics" and counter_name in ("Processes blocked"))
        or
        (object_name =~ "Resource Pool Stats" and counter_name in ("Active memory grants count","Pending memory grants count"))
| project sample_time_utc, counter_name, cntr_value
| render timechart;
```

---

The following example uses the **Performance counters (detailed)** dataset to chart CPU utilization for user and internal resource pools and workload groups in Azure SQL Database. For more information, see [Resource consumption by user workloads and internal processes](./database/resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes).

The user workloads are running in the `SloSharedPool1` or `UserPool` resource pools, while all other resource pools are used for various system workloads.

Similarly, the user workloads are running in the workload groups named starting with `UserPrimaryGroup.DBId`, while all other workload groups are used for various system workloads. For example, database watcher monitoring queries are running in the `SQLExternalMonitoringGroup` workload group.

# [SQL database](#tab/sqldb)

```kusto
let logicalServer = @"your-server-name";
let databaseName = @"your-database-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
sqldb_database_performance_counters_detailed
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where database_name =~ databaseName
| where replica_type =~ replicaType
| where cntr_type == 537003264 // restrict to ratio percentage counters
| where object_name =~ "Resource Pool Stats" and counter_name in ("CPU usage %")
| project sample_time_utc, resource_pool = instance_name, cpu_percentage = cntr_value
| render timechart;

let logicalServer = @"your-server-name";
let databaseName = @"your-database-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
sqldb_database_performance_counters_detailed
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where database_name =~ databaseName
| where replica_type =~ replicaType
| where cntr_type == 537003264 // restrict to ratio percentage counters
| where object_name =~ "Workload Group Stats" and counter_name in ("CPU usage %")
| project sample_time_utc, workload_group = instance_name, cpu_percentage = cntr_value
| render timechart;
```

# [SQL elastic pool](#tab/sqlep)

```kusto
let logicalServer = @"your-server-name";
let elasticPoolName = @"your-elastic-pool-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
sqldb_elastic_pool_performance_counters_detailed
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where elastic_pool_name =~ elasticPoolName
| where replica_type =~ replicaType
| where cntr_type == 537003264 // restrict to ratio percentage counters
| where object_name =~ "Resource Pool Stats" and counter_name in ("CPU usage %")
| project sample_time_utc, resource_pool = instance_name, cpu_percentage = cntr_value
| render timechart;

let logicalServer = @"your-server-name";
let elasticPoolName = @"your-elastic-pool-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
sqldb_elastic_pool_performance_counters_detailed
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where elastic_pool_name =~ elasticPoolName
| where replica_type =~ replicaType
| where cntr_type == 537003264 // restrict to ratio percentage counters
| where object_name =~ "Workload Group Stats" and counter_name in ("CPU usage %")
| project sample_time_utc, workload_group = instance_name, cpu_percentage = cntr_value
| render timechart;
```

# [SQL managed instance](#tab/sqlmi)

This example is not applicable to Azure SQL Managed Instance because it does not have built-in resource governance that uses resource pools and workload groups.

---

### Use KQL to analyze cumulative waits over time

This example shows how to chart top SQL wait types over a time interval. The query calculates the cumulative wait time for each wait type, in milliseconds per second of elapsed time. You can adjust query variables to set the interval start and end time, the number of top wait types to include, and the step between the data points on the chart.

The query uses two techniques to improve performance:

- The [partition](/azure/data-explorer/kusto/query/partitionoperator) KQL operator with the `shuffle` strategy to spread query processing over multiple cluster nodes, if present.
- The [materialize()](/azure/data-explorer/kusto/query/materializefunction) function to persist an intermediate result set that is reused for calculating the top waits and for building the time series to be charted.

# [SQL database](#tab/sqldb)

```kusto
let logicalServer = @"your-server-name";
let databaseName = @"your-database-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
let top_wait_types = 10;
let chart_step = 30s;
let wait_type_sample = materialize (
sqldb_database_wait_stats
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where database_name =~ databaseName
| where replica_type =~ replicaType
| project replica_id, sample_time_utc, wait_type, wait_time_ms
| partition hint.strategy=shuffle by wait_type
(
sort by replica_id asc, sample_time_utc asc
| extend delta_wait_time_ms = iif(wait_time_ms >= prev(wait_time_ms) and replica_id == prev(replica_id), wait_time_ms - prev(wait_time_ms), long(null)),
         delta_sample_time_utc = iif(sample_time_utc >= prev(sample_time_utc), datetime_diff("Millisecond", sample_time_utc, prev(sample_time_utc)), long(null))
| where isnotempty(delta_sample_time_utc)
| extend wait_ms_per_s = toreal(delta_wait_time_ms) / delta_sample_time_utc * 1000
| project sample_time_utc, wait_type, wait_ms_per_s
)
);
let top_wait = (
wait_type_sample
| summarize total_wait_ms_per_s = sum(wait_ms_per_s) by wait_type
| top top_wait_types by total_wait_ms_per_s desc
| project-away total_wait_ms_per_s
);
wait_type_sample
| join kind=inner top_wait on wait_type
| project-away wait_type1
| make-series wait_ms_per_s = avgif(wait_ms_per_s, isfinite(wait_ms_per_s)) default = long(null) on sample_time_utc from startTime to endTime step chart_step by wait_type
| project wait_type, sample_time_utc, wait_ms_per_s
| render timechart;
```

# [SQL elastic pool](#tab/sqlep)

```kusto
let logicalServer = @"your-server-name";
let elasticPoolName = @"your-elastic-pool-name";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
let top_wait_types = 10;
let chart_step = 30s;
let wait_type_sample = materialize (
sqldb_elastic_pool_wait_stats
| where sample_time_utc between (startTime .. endTime)
| where logical_server_name =~ logicalServer
| where elastic_pool_name =~ elasticPoolName
| where replica_type =~ replicaType
| project anchor_database_replica_id, sample_time_utc, wait_type, wait_time_ms
| partition hint.strategy=shuffle by wait_type
(
sort by anchor_database_replica_id asc, sample_time_utc asc
| extend delta_wait_time_ms = iif(wait_time_ms >= prev(wait_time_ms) and anchor_database_replica_id == prev(anchor_database_replica_id), wait_time_ms - prev(wait_time_ms), long(null)),
         delta_sample_time_utc = iif(sample_time_utc >= prev(sample_time_utc), datetime_diff("Millisecond", sample_time_utc, prev(sample_time_utc)), long(null))
| where isnotempty(delta_sample_time_utc)
| extend wait_ms_per_s = toreal(delta_wait_time_ms) / delta_sample_time_utc * 1000
| project sample_time_utc, wait_type, wait_ms_per_s
)
);
let top_wait = (
wait_type_sample
| summarize total_wait_ms_per_s = sum(wait_ms_per_s) by wait_type
| top top_wait_types by total_wait_ms_per_s desc
| project-away total_wait_ms_per_s
);
wait_type_sample
| join kind=inner top_wait on wait_type
| project-away wait_type1
| make-series wait_ms_per_s = avgif(wait_ms_per_s, isfinite(wait_ms_per_s)) default = long(null) on sample_time_utc from startTime to endTime step chart_step by wait_type
| project wait_type, sample_time_utc, wait_ms_per_s
| render timechart;
```

# [SQL managed instance](#tab/sqlmi)

```kusto
let instanceFqdnName = @"your-instance-name.unique-dns-prefix.database.windows.net";
let replicaType = "Primary";
let endTime = datetime("2023-12-19 22:10:00");
let startTime = endTime - 30m;
let top_wait_types = 10;
let chart_step = 30s;
let wait_type_sample = materialize (
sqlmi_wait_stats
| where sample_time_utc between (startTime .. endTime)
| where managed_instance_name =~ instanceFqdnName
| where replica_type =~ replicaType
| project sample_time_utc, wait_type, wait_time_ms
| partition hint.strategy=shuffle by wait_type
(
sort by sample_time_utc asc
| extend delta_wait_time_ms = iif(wait_time_ms >= prev(wait_time_ms), wait_time_ms - prev(wait_time_ms), long(null)),
         delta_sample_time_utc = iif(sample_time_utc >= prev(sample_time_utc), datetime_diff("Millisecond", sample_time_utc, prev(sample_time_utc)), long(null))
| where isnotempty(delta_sample_time_utc)
| extend wait_ms_per_s = toreal(delta_wait_time_ms) / delta_sample_time_utc * 1000
| project sample_time_utc, wait_type, wait_ms_per_s
)
);
let top_wait = (
wait_type_sample
| summarize total_wait_ms_per_s = sum(wait_ms_per_s) by wait_type
| top top_wait_types by total_wait_ms_per_s desc
| project-away total_wait_ms_per_s
);
wait_type_sample
| join kind=inner top_wait on wait_type
| project-away wait_type1
| make-series wait_ms_per_s = avgif(wait_ms_per_s, isfinite(wait_ms_per_s)) default = long(null) on sample_time_utc from startTime to endTime step chart_step by wait_type
| project wait_type, sample_time_utc, wait_ms_per_s
| render timechart;
```

---

## Use T-SQL to analyze monitoring data

If you are already familiar with T-SQL, you can start querying and analyzing SQL monitoring data right away without having to learn KQL. However, [KQL](#use-kql-to-analyze-monitoring-data) is the recommended language for querying data in Azure Data Explorer or Real-Time Analytics because it provides unparalleled support for querying telemetry data.

You can connect to your Azure Data Explorer or Real-Time Analytics database from [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms), [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio), and other [common tools](/azure/data-explorer/connect-common-apps). You can query an Azure Data Explorer or a KQL database as if it were a SQL Server or an Azure SQL database. For more information, see [Query data in Azure Data Explorer using SQL Server emulation](/azure/data-explorer/sql-server-emulation-overview).

> [!NOTE]
> Not every T-SQL construct is supported in Azure Data Explorer and Real-Time Analytics. For details, see [Query data using T-SQL](/azure/data-explorer/t-sql).
>
> The [SQL to Kusto Query Language cheat sheet](/azure/data-explorer/kusto/query/sql-cheat-sheet) can help you translate your T-SQL queries to KQL if you find that T-SQL support is insufficient for your needs, or if you want to convert your T-SQL queries to KQL to use its advanced analytical capabilities.

The following examples show you how to query monitoring data in the database watcher data store using T-SQL.

### Use T-SQL to analyze resource consumption over time

In this example, the query returns resource consumption metrics (CPU, workers, log write throughput, etc.) for the primary replica of a database, an elastic pool, or a SQL managed instance over the last one hour.

In this and other examples, change the variables in the `DECLARE` statement to match the names of your server, database, elastic pool, or SQL managed instance.

# [SQL database](#tab/sqldb)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @DatabaseName sysname = 'your-database-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 60;

SELECT sample_time_utc,
       avg_cpu_percent,
       avg_instance_cpu_percent,
       avg_data_io_percent,
       avg_log_write_percent,
       max_worker_percent
FROM sqldb_database_resource_utilization
WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
      AND
      logical_server_name = @LogicalServerName
      AND
      database_name = @DatabaseName
      AND
      replica_type = @ReplicaType
ORDER BY sample_time_utc DESC;
```

# [SQL elastic pool](#tab/sqlep)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @ElasticPoolName sysname = 'your-elastic-pool-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 60;

SELECT sample_time_utc,
       avg_cpu_percent,
       avg_instance_cpu_percent,
       avg_data_io_percent,
       avg_log_write_percent,
       max_worker_percent
FROM sqldb_elastic_pool_resource_utilization
WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
      AND
      logical_server_name = @LogicalServerName
      AND
      elastic_pool_name = @ElasticPoolName
      AND
      replica_type = @ReplicaType
ORDER BY sample_time_utc DESC;
```

# [SQL managed instance](#tab/sqlmi)

```sql
DECLARE @InstanceFqdnName sysname = 'your-instance-name.unique-dns-prefix.database.windows.net',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 60;

SELECT sample_time_utc,
       avg_instance_cpu_percent,
       avg_instance_log_write_percent,
       instance_worker_percent
FROM sqlmi_resource_utilization
WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
      AND
      managed_instance_name = @InstanceFqdnName
      AND
      replica_type = @ReplicaType
ORDER BY sample_time_utc DESC;
```

---

### Use T-SQL to view database, elastic pool, or SQL managed instance properties

In this example, the query returns a set of all databases, elastic pools, or SQL managed instances from which at least one sample in the corresponding **Properties** [dataset](database-watcher-data.md#datasets) was collected in the last 24 hours. In other words, each row represents a monitoring target with its most recently observed properties.

# [SQL database](#tab/sqldb)

```sql
DECLARE @DurationHours int = 24;

SELECT p.sample_time_utc,
       p.logical_server_name,
       p.database_name,
       p.replica_type,
       p.database_id,
       p.elastic_pool_name,
       p.service_tier,
       p.service_level_objective,
       p.logical_cpu_count,
       p.database_engine_memory_mb,
       p.compatibility_level,
       p.updateability,
       p.database_engine_build_time,
       p.database_engine_start_time_utc
FROM sqldb_database_properties AS p
INNER JOIN (
           SELECT logical_server_name,
                  database_name,
                  replica_type,
                  MAX(sample_time_utc) AS last_sample_time_utc
           FROM sqldb_database_properties
           WHERE sample_time_utc > DATEADD(hour, -@DurationHours, SYSUTCDATETIME())
           GROUP BY logical_server_name,
                    database_name,
                    replica_type
           ) AS ls
ON p.logical_server_name = ls.logical_server_name
   AND
   p.database_name = ls.database_name
   AND
   p.replica_type = ls.replica_type
   AND
   p.sample_time_utc = ls.last_sample_time_utc
WHERE p.sample_time_utc > DATEADD(hour, -@DurationHours, SYSUTCDATETIME())
ORDER BY LOWER(logical_server_name) ASC,
         LOWER(database_name) ASC,
         CASE replica_type
              WHEN 'Primary' THEN 0
              WHEN 'Geo-replication forwarder' THEN 1
              WHEN 'Named secondary' THEN 2
              WHEN 'HA secondary' THEN 3
         END ASC;
```

# [SQL elastic pool](#tab/sqlep)

```sql
DECLARE @DurationHours int = 24;

SELECT p.sample_time_utc,
       p.logical_server_name,
       p.elastic_pool_name,
       p.replica_type,
       p.service_tier,
       p.logical_cpu_count,
       p.database_engine_memory_mb,
       p.database_engine_build_time,
       p.database_engine_start_time_utc
FROM sqldb_elastic_pool_properties AS p
INNER JOIN (
           SELECT logical_server_name,
                  elastic_pool_name,
                  replica_type,
                  MAX(sample_time_utc) AS last_sample_time_utc
           FROM sqldb_elastic_pool_properties
           WHERE sample_time_utc > DATEADD(hour, -@DurationHours, SYSUTCDATETIME())
           GROUP BY logical_server_name,
                    elastic_pool_name,
                    replica_type
           ) AS ls
ON p.logical_server_name = ls.logical_server_name
   AND
   p.elastic_pool_name = ls.elastic_pool_name
   AND
   p.replica_type = ls.replica_type
   AND
   p.sample_time_utc = ls.last_sample_time_utc
WHERE p.sample_time_utc > DATEADD(hour, -@DurationHours, SYSUTCDATETIME())
ORDER BY LOWER(logical_server_name) ASC,
         LOWER(elastic_pool_name) ASC,
         CASE replica_type
              WHEN 'Primary' THEN 0
              WHEN 'HA secondary' THEN 1
         END ASC;
```

# [SQL managed instance](#tab/sqlmi)

```sql
DECLARE @DurationHours int = 24;

SELECT p.sample_time_utc,
       p.managed_instance_name,
       p.replica_type,
       p.service_tier,
       p.hardware_generation,
       p.product_update_level,
       p.logical_cpu_count,
       p.storage_space_used_mb,
       p.reserved_storage_mb,
       p.database_engine_memory_mb,
       p.database_engine_build_time,
       p.database_engine_start_time_utc
FROM sqlmi_instance_properties AS p
INNER JOIN (
           SELECT managed_instance_name,
                  replica_type,
                  MAX(sample_time_utc) AS last_sample_time_utc
           FROM sqlmi_instance_properties
           WHERE sample_time_utc > DATEADD(hour, -@DurationHours, SYSUTCDATETIME())
           GROUP BY managed_instance_name,
                    replica_type
           ) AS ls
ON p.managed_instance_name = ls.managed_instance_name
   AND
   p.replica_type = ls.replica_type
   AND
   p.sample_time_utc = ls.last_sample_time_utc
WHERE p.sample_time_utc > DATEADD(hour, -@DurationHours, SYSUTCDATETIME())
ORDER BY LOWER(managed_instance_name) ASC,
         CASE replica_type
              WHEN 'Primary' THEN 0
              WHEN 'Geo-replication forwarder' THEN 1
              WHEN 'HA secondary' THEN 3
         END ASC;
```

---

### Use T-SQL to view query runtime statistics

This query returns the top resource consuming queries across your Azure SQL estate. Change the `@TopQueriesBy` variable to find top queries by any [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) metric, including CPU time, elapsed time, execution count, etc. You can also set variables to filter by a time interval, query execution type, and query hash of a specific query, or to focus on databases from a specific logical server, elastic pool, or SQL managed instance.

The query uses the **Query runtime statistics** [dataset](database-watcher-data.md#datasets) to return the top queries you specify. It also returns their rank by every other resource consumption metric.

# [SQL database](#tab/sqldb)

```sql
DECLARE @EndTime datetime2 = SYSUTCDATETIME(),
        @StartTime datetime2 = DATEADD(hour, -24, SYSUTCDATETIME()),
        /* 
        Set the next variable to one of the following metrics to return the top resource consuming queries:
        executions, cpu_time, duration, logical_io_reads, physical_io_reads, num_physical_io_reads, 
        clr_time, query_max_used_memory, log_bytes_used, tempdb_space_used, row_count, dop
        */
        @TopQueriesBy varchar(30) = 'cpu_time',
        @TopQueries int = 10,
        @LogicalServerName sysname = '', -- Optionally filter by logical server name
        @ElasticPoolName sysname = '', -- Optionally filter by elastic pool name, if any databases are in elastic pools
        @DatabaseName sysname = '', -- Optionally filter by database name
        @ExecutionType varchar(30) = '', -- Optionally filter by execution type. Use Regular, Aborted, Exception.
        @QueryHash varchar(18) = ''; -- Optionally filter by query hash (example: 0xBAAA461A6C93EA88)

SELECT TOP (@TopQueries) 
       CONCAT(logical_server_name, IIF(count_logical_servers > 1, CONCAT(' (+', CAST(count_logical_servers - 1 AS varchar(11)), ')'), '')) AS logical_server_name,
       CONCAT(database_name, IIF(count_databases > 1, CONCAT(' (+', CAST(count_databases - 1 AS varchar(11)), ')'), '')) AS database_name,
       query_sql_text,
       CONCAT(CAST(query_id AS varchar(11)), IIF(count_queries > 1, CONCAT(' (+', CAST(count_queries - 1 AS varchar(11)), ')'), '')) AS query_id,
       CONCAT(CAST(plan_id AS varchar(11)), IIF(count_plans > 1, CONCAT(' (+', CAST(count_plans - 1 AS varchar(11)), ')'), '')) AS plan_id,
       regular_executions,
       aborted_executions,
       exception_executions,
       cpu_time_us,
       duration_us,
       logical_io_reads,
       physical_io_reads,
       num_physical_io_reads,
       clr_time_us,
       query_max_used_memory_kb,
       log_bytes_used,
       tempdb_space_used_kb,
       row_count,
       dop,
       query_hash,
       executions_rank,
       cpu_time_rank,
       duration_rank,
       logical_io_reads_rank,
       physical_io_reads_rank,
       num_physical_io_reads_rank,
       clr_time_rank,
       query_max_used_memory_rank,
       log_bytes_used_rank,
       tempdb_space_used_rank,
       row_count_rank,
       dop_rank
FROM (
     SELECT *,
            DENSE_RANK() OVER (ORDER BY executions DESC) AS executions_rank,
            DENSE_RANK() OVER (ORDER BY cpu_time_us DESC) AS cpu_time_rank,
            DENSE_RANK() OVER (ORDER BY duration_us DESC) AS duration_rank,
            DENSE_RANK() OVER (ORDER BY logical_io_reads DESC) AS logical_io_reads_rank,
            DENSE_RANK() OVER (ORDER BY physical_io_reads DESC) AS physical_io_reads_rank,
            DENSE_RANK() OVER (ORDER BY num_physical_io_reads DESC) AS num_physical_io_reads_rank,
            DENSE_RANK() OVER (ORDER BY clr_time_us DESC) AS clr_time_rank,
            DENSE_RANK() OVER (ORDER BY query_max_used_memory_kb DESC) AS query_max_used_memory_rank,
            DENSE_RANK() OVER (ORDER BY log_bytes_used DESC) AS log_bytes_used_rank,
            DENSE_RANK() OVER (ORDER BY tempdb_space_used_kb DESC) AS tempdb_space_used_rank,
            DENSE_RANK() OVER (ORDER BY row_count DESC) AS row_count_rank,
            DENSE_RANK() OVER (ORDER BY dop DESC) AS dop_rank
     FROM (
          SELECT query_hash,
                 COUNT(DISTINCT(logical_server_name)) AS count_logical_servers,
                 MAX(logical_server_name) AS logical_server_name,
                 COUNT(DISTINCT(database_name)) AS count_databases,
                 MAX(database_name) AS database_name,
                 COUNT(DISTINCT(query_id)) AS count_queries,
                 MAX(query_id) AS query_id,
                 COUNT(DISTINCT(plan_id)) AS count_plans,
                 MAX(plan_id) AS plan_id,
                 MAX(query_sql_text) AS query_sql_text,
                 SUM(IIF(execution_type_desc = 'Regular', count_executions, 0)) AS regular_executions,
                 SUM(IIF(execution_type_desc = 'Aborted', count_executions, 0)) AS aborted_executions,
                 SUM(IIF(execution_type_desc = 'Exception', count_executions, 0)) AS exception_executions,
                 SUM(count_executions) AS executions,
                 SUM(avg_cpu_time_us * count_executions) AS cpu_time_us,
                 SUM(avg_duration_us * count_executions) AS duration_us,
                 SUM(avg_logical_io_reads * count_executions) AS logical_io_reads,
                 SUM(avg_physical_io_reads * count_executions) AS physical_io_reads,
                 SUM(avg_num_physical_io_reads * count_executions) AS num_physical_io_reads,
                 SUM(avg_clr_time_us * count_executions) AS clr_time_us,
                 SUM(avg_query_max_used_memory * count_executions) * 8 AS query_max_used_memory_kb,
                 SUM(avg_log_bytes_used * count_executions) AS log_bytes_used,
                 SUM(avg_tempdb_space_used * count_executions) * 8 AS tempdb_space_used_kb,
                 SUM(avg_rowcount * count_executions) AS row_count,
                 SUM(IIF(is_parallel_plan = 1, avg_dop * count_executions, NULL)) AS dop
          FROM sqldb_database_query_runtime_stats
          WHERE interval_start_time >= @StartTime AND interval_end_time <= @EndTime
                AND
                (@ExecutionType = '' OR LOWER(execution_type_desc) = LOWER(@ExecutionType))
                AND
                (@LogicalServerName = '' OR LOWER(logical_server_name) = LOWER(@LogicalServerName))
                AND
                (@ElasticPoolName = '' OR LOWER(elastic_pool_name) = LOWER(@ElasticPoolName))
                AND
                (@DatabaseName = '' OR LOWER(database_name) = LOWER(@DatabaseName))
          GROUP BY query_hash
          ) AS rsa
     ) AS rsar
WHERE @QueryHash = '' OR LOWER(query_hash) = LOWER(@QueryHash)
ORDER BY CASE @TopQueriesBy
              WHEN 'executions' THEN executions_rank
              WHEN 'cpu_time' THEN cpu_time_rank
              WHEN 'duration' THEN duration_rank
              WHEN 'logical_io_reads' THEN logical_io_reads_rank
              WHEN 'physical_io_reads' THEN physical_io_reads_rank
              WHEN 'num_physical_io_reads' THEN num_physical_io_reads_rank
              WHEN 'clr_time' THEN clr_time_rank
              WHEN 'query_max_used_memory' THEN query_max_used_memory_rank
              WHEN 'log_bytes_used' THEN log_bytes_used_rank
              WHEN 'tempdb_space_used' THEN tempdb_space_used_rank
              WHEN 'row_count' THEN row_count_rank
              WHEN 'dop' THEN dop_rank
         END ASC;
```

# [SQL elastic pool](#tab/sqlep)

The **Query runtime statistics** [dataset](database-watcher-data.md#datasets) is available only for databases and SQL managed instances. This example does not apply to elastic pools.

The *Databases* example also applies to databases in elastic pools.

# [SQL managed instance](#tab/sqlmi)

```sql
DECLARE @EndTime datetime2 = SYSUTCDATETIME(),
        @StartTime datetime2 = DATEADD(hour, -24, SYSUTCDATETIME()),
        /* 
        Set the next variable to one of the following metrics to return the top resource consuming queries:
        executions, cpu_time, duration, logical_io_reads, physical_io_reads, num_physical_io_reads, 
        clr_time, query_max_used_memory, log_bytes_used, tempdb_space_used, row_count, dop
        */
        @TopQueriesBy varchar(30) = 'cpu_time',
        @TopQueries int = 10,
        @InstanceFqdnName sysname = '', -- Optionally filter by managed instance name
        @DatabaseName sysname = '', -- Optionally filter by database name
        @ExecutionType varchar(30) = '', -- Optionally filter by execution type. Use Regular, Aborted, Exception.
        @QueryHash varchar(18) = ''; -- Optionally filter by query hash (example: 0xBAAA461A6C93EA88)

SELECT TOP (@TopQueries)
       CONCAT(managed_instance_name, IIF(count_managed_instances > 1, CONCAT(' (+', CAST(count_managed_instances - 1 AS varchar(11)), ')'), '')) AS managed_instance_name,
       CONCAT(database_name, IIF(count_databases > 1, CONCAT(' (+', CAST(count_databases - 1 AS varchar(11)), ')'), '')) AS database_name,
       query_sql_text,
       CONCAT(CAST(query_id AS varchar(11)), IIF(count_queries > 1, CONCAT(' (+', CAST(count_queries - 1 AS varchar(11)), ')'), '')) AS query_id,
       CONCAT(CAST(plan_id AS varchar(11)), IIF(count_plans > 1, CONCAT(' (+', CAST(count_plans - 1 AS varchar(11)), ')'), '')) AS plan_id,
       regular_executions,
       aborted_executions,
       exception_executions,
       cpu_time_us,
       duration_us,
       logical_io_reads,
       physical_io_reads,
       num_physical_io_reads,
       clr_time_us,
       query_max_used_memory_kb,
       log_bytes_used,
       tempdb_space_used_kb,
       row_count,
       dop,
       query_hash,
       executions_rank,
       cpu_time_rank,
       duration_rank,
       logical_io_reads_rank,
       physical_io_reads_rank,
       num_physical_io_reads_rank,
       clr_time_rank,
       query_max_used_memory_rank,
       log_bytes_used_rank,
       tempdb_space_used_rank,
       row_count_rank,
       dop_rank
FROM (
     SELECT *,
            DENSE_RANK() OVER (ORDER BY executions DESC) AS executions_rank,
            DENSE_RANK() OVER (ORDER BY cpu_time_us DESC) AS cpu_time_rank,
            DENSE_RANK() OVER (ORDER BY duration_us DESC) AS duration_rank,
            DENSE_RANK() OVER (ORDER BY logical_io_reads DESC) AS logical_io_reads_rank,
            DENSE_RANK() OVER (ORDER BY physical_io_reads DESC) AS physical_io_reads_rank,
            DENSE_RANK() OVER (ORDER BY num_physical_io_reads DESC) AS num_physical_io_reads_rank,
            DENSE_RANK() OVER (ORDER BY clr_time_us DESC) AS clr_time_rank,
            DENSE_RANK() OVER (ORDER BY query_max_used_memory_kb DESC) AS query_max_used_memory_rank,
            DENSE_RANK() OVER (ORDER BY log_bytes_used DESC) AS log_bytes_used_rank,
            DENSE_RANK() OVER (ORDER BY tempdb_space_used_kb DESC) AS tempdb_space_used_rank,
            DENSE_RANK() OVER (ORDER BY row_count DESC) AS row_count_rank,
            DENSE_RANK() OVER (ORDER BY dop DESC) AS dop_rank
     FROM (
          SELECT query_hash,
                 COUNT(DISTINCT(managed_instance_name)) AS count_managed_instances,
                 MAX(managed_instance_name) AS managed_instance_name,
                 COUNT(DISTINCT(database_name)) AS count_databases,
                 MAX(database_name) AS database_name,
                 COUNT(DISTINCT(query_id)) AS count_queries,
                 MAX(query_id) AS query_id,
                 COUNT(DISTINCT(plan_id)) AS count_plans,
                 MAX(plan_id) AS plan_id,
                 MAX(query_sql_text) AS query_sql_text,
                 SUM(IIF(execution_type_desc = 'Regular', count_executions, 0)) AS regular_executions,
                 SUM(IIF(execution_type_desc = 'Aborted', count_executions, 0)) AS aborted_executions,
                 SUM(IIF(execution_type_desc = 'Exception', count_executions, 0)) AS exception_executions,
                 SUM(count_executions) AS executions,
                 SUM(avg_cpu_time_us * count_executions) AS cpu_time_us,
                 SUM(avg_duration_us * count_executions) AS duration_us,
                 SUM(avg_logical_io_reads * count_executions) AS logical_io_reads,
                 SUM(avg_physical_io_reads * count_executions) AS physical_io_reads,
                 SUM(avg_num_physical_io_reads * count_executions) AS num_physical_io_reads,
                 SUM(avg_clr_time_us * count_executions) AS clr_time_us,
                 SUM(avg_query_max_used_memory * count_executions) * 8 AS query_max_used_memory_kb,
                 SUM(avg_log_bytes_used * count_executions) AS log_bytes_used,
                 SUM(avg_tempdb_space_used * count_executions) * 8 AS tempdb_space_used_kb,
                 SUM(avg_rowcount * count_executions) AS row_count,
                 SUM(IIF(is_parallel_plan = 1, avg_dop * count_executions, NULL)) AS dop
          FROM sqlmi_query_runtime_stats
          WHERE interval_start_time >= @StartTime AND interval_end_time <= @EndTime
                AND
                (@ExecutionType = '' OR LOWER(execution_type_desc) = LOWER(@ExecutionType))
                AND
                (@InstanceFqdnName = '' OR LOWER(managed_instance_name) = LOWER(@InstanceFqdnName))
                AND
                (@DatabaseName = '' OR LOWER(database_name) = LOWER(@DatabaseName))
          GROUP BY query_hash
          ) AS rsa
     ) AS rsar
WHERE @QueryHash = '' OR LOWER(query_hash) = LOWER(@QueryHash)
ORDER BY CASE @TopQueriesBy
              WHEN 'executions' THEN executions_rank
              WHEN 'cpu_time' THEN cpu_time_rank
              WHEN 'duration' THEN duration_rank
              WHEN 'logical_io_reads' THEN logical_io_reads_rank
              WHEN 'physical_io_reads' THEN physical_io_reads_rank
              WHEN 'num_physical_io_reads' THEN num_physical_io_reads_rank
              WHEN 'clr_time' THEN clr_time_rank
              WHEN 'query_max_used_memory' THEN query_max_used_memory_rank
              WHEN 'log_bytes_used' THEN log_bytes_used_rank
              WHEN 'tempdb_space_used' THEN tempdb_space_used_rank
              WHEN 'row_count' THEN row_count_rank
              WHEN 'dop' THEN dop_rank
         END ASC;
```

---

### Use T-SQL to analyze performance counters over time

In this example, the query returns performance counter values for the last 30 minutes.

This example uses cumulative performance counters such as `Total request count` and `Query optimizations/sec`. Cumulative means that the counter value keeps increasing as query activity occurs. The query uses the [LAG()](/sql/t-sql/functions/lag-transact-sql) analytic function to calculate the difference, or delta, between the counter value in each sample and its value in the previous sample to obtain the number of requests and optimizations that occurred since the previous sample.

# [SQL database](#tab/sqldb)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @DatabaseName sysname = 'your-database-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT sample_time_utc,
       SUM(IIF(
              counter_name = 'Total request count',
              CAST((cntr_value - prev_cntr_value) AS decimal) / DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc) * 1000,
              NULL
              )) AS requests_per_second,
       SUM(IIF(
              counter_name = 'Query optimizations/sec',
              CAST((cntr_value - prev_cntr_value) AS decimal) / DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc) * 1000,
              NULL
              )) AS query_optimizations_per_second
FROM (
     SELECT sample_time_utc,
            LAG(sample_time_utc) OVER (PARTITION BY replica_id, object_name, counter_name ORDER BY sample_time_utc ASC) AS prev_sample_time_utc,
            counter_name,
            cntr_value,
            LAG(cntr_value) OVER (PARTITION BY replica_id, object_name, counter_name ORDER BY sample_time_utc ASC) AS prev_cntr_value
     FROM sqldb_database_performance_counters_common
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
           AND
           logical_server_name = @LogicalServerName
           AND
           database_name = @DatabaseName
           AND
           replica_type = @ReplicaType
           AND
           cntr_type = 272696576 /* restrict to cumulative counters */
           AND
           object_name = 'Workload Group Stats'
           AND
           counter_name IN ('Total request count','Query optimizations/sec')
     ) AS pc
WHERE cntr_value >= prev_cntr_value
      AND
      sample_time_utc >= prev_sample_time_utc
GROUP BY sample_time_utc
ORDER BY sample_time_utc DESC;
```

# [SQL elastic pool](#tab/sqlep)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @ElasticPoolName sysname = 'your-elastic-pool-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT sample_time_utc,
       SUM(IIF(
              counter_name = 'Batch Requests/sec',
              CAST((cntr_value - prev_cntr_value) AS decimal) / DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc) * 1000,
              NULL
              )) AS requests_per_second,
       SUM(IIF(
              counter_name = 'Query optimizations/sec',
              CAST((cntr_value - prev_cntr_value) AS decimal) / DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc) * 1000,
              NULL
              )) AS query_optimizations_per_second
FROM (
     SELECT sample_time_utc,
            LAG(sample_time_utc) OVER (PARTITION BY anchor_database_replica_id, database_name, object_name, counter_name ORDER BY sample_time_utc ASC) AS prev_sample_time_utc,
            object_name,
            counter_name,
            cntr_value,
            LAG(cntr_value) OVER (PARTITION BY anchor_database_replica_id, database_name, object_name, counter_name ORDER BY sample_time_utc ASC) AS prev_cntr_value
     FROM sqldb_elastic_pool_performance_counters_common
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
           AND
           logical_server_name = @LogicalServerName
           AND
           elastic_pool_name = @ElasticPoolName
           AND
           replica_type = @ReplicaType
           AND
           cntr_type = 272696576 /* restrict to cumulative counters */
           AND
           (
           (object_name = 'Workload Group Stats' AND counter_name = 'Query optimizations/sec')
           OR
           (object_name = 'SQL Statistics' AND counter_name = 'Batch Requests/sec')
           )
     ) AS pc
WHERE cntr_value >= prev_cntr_value
      AND
      sample_time_utc >= prev_sample_time_utc
GROUP BY sample_time_utc
ORDER BY sample_time_utc DESC;
```

# [SQL managed instance](#tab/sqlmi)

```sql
DECLARE @InstanceFqdnName sysname = 'your-instance-name.unique-dns-prefix.database.windows.net',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT sample_time_utc,
       SUM(IIF(
              counter_name = 'Batch Requests/sec',
              CAST((cntr_value - prev_cntr_value) AS decimal) / DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc) * 1000,
              NULL
              )) AS requests_per_second,
       SUM(IIF(
              counter_name = 'Query optimizations/sec',
              CAST((cntr_value - prev_cntr_value) AS decimal) / DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc) * 1000,
              NULL
              )) AS query_optimizations_per_second
FROM (
     SELECT sample_time_utc,
            LAG(sample_time_utc) OVER (PARTITION BY object_name, counter_name ORDER BY sample_time_utc ASC) AS prev_sample_time_utc,
            counter_name,
            cntr_value,
            LAG(cntr_value) OVER (PARTITION BY object_name, counter_name ORDER BY sample_time_utc ASC) AS prev_cntr_value
     FROM sqlmi_performance_counters_common
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
           AND
           managed_instance_name = @InstanceFqdnName
           AND
           replica_type = @ReplicaType
           AND
           cntr_type = 272696576 /* restrict to cumulative counters */
           AND
           (
           (object_name = 'Workload Group Stats' AND counter_name = 'Query optimizations/sec')
           OR
           (object_name = 'SQL Statistics' AND counter_name = 'Batch Requests/sec')
           )
     ) AS pc
WHERE cntr_value >= prev_cntr_value
      AND
      sample_time_utc >= prev_sample_time_utc
GROUP BY sample_time_utc
ORDER BY sample_time_utc DESC;
```

---

### Use T-SQL to analyze point-in-time performance counters

The next example is for point-in-time performance counters that report the most recently observed value, such as `Active memory grants count`, `Pending memory grants count`, and `Processes blocked`.

# [SQL database](#tab/sqldb)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @DatabaseName sysname = 'your-database-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT sample_time_utc,
       SUM(IIF(
              counter_name = 'Processes blocked',
              cntr_value,
              NULL
              )) AS processes_blocked,
       SUM(IIF(
              counter_name = 'Active memory grants count',
              cntr_value,
              NULL
              )) AS active_memory_grants,
       SUM(IIF(
              counter_name = 'Pending memory grants count',
              cntr_value,
              NULL
              )) AS pending_memory_grants
FROM (
     SELECT sample_time_utc,
            counter_name,
            cntr_value
     FROM sqldb_database_performance_counters_common
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
         AND
         logical_server_name = @LogicalServerName
         AND
         database_name = @DatabaseName
         AND
         replica_type = @ReplicaType
         AND
         cntr_type = 65792 /* restrict to point-in-time counters */
         AND
         (
         (object_name = 'General Statistics' AND counter_name IN ('Processes blocked'))
         OR
         (object_name = 'Resource Pool Stats' AND counter_name IN ('Active memory grants count','Pending memory grants count'))
         )
     ) AS pc
GROUP BY sample_time_utc
ORDER BY sample_time_utc DESC;
```

# [SQL elastic pool](#tab/sqlep)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @ElasticPoolName sysname = 'your-elastic-pool-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT sample_time_utc,
       SUM(IIF(
              counter_name = 'Processes blocked',
              cntr_value,
              NULL
              )) AS processes_blocked,
       SUM(IIF(
              counter_name = 'Active memory grants count',
              cntr_value,
              NULL
              )) AS active_memory_grants,
       SUM(IIF(
              counter_name = 'Pending memory grants count',
              cntr_value,
              NULL
              )) AS pending_memory_grants
FROM (
     SELECT sample_time_utc,
            counter_name,
            cntr_value
     FROM sqldb_elastic_pool_performance_counters_common
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
         AND
         logical_server_name = @LogicalServerName
         AND
         elastic_pool_name = @ElasticPoolName
         AND
         replica_type = @ReplicaType
         AND
         cntr_type = 65792 /* restrict to point-in-time counters */
         AND
         (
         (object_name = 'General Statistics' AND counter_name IN ('Processes blocked'))
         OR
         (object_name = 'Resource Pool Stats' AND counter_name IN ('Active memory grants count','Pending memory grants count'))
         )
     ) AS pc
GROUP BY sample_time_utc
ORDER BY sample_time_utc DESC;
```

# [SQL managed instance](#tab/sqlmi)

```sql
DECLARE @InstanceFqdnName sysname = 'your-instance-name.unique-dns-prefix.database.windows.net',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT sample_time_utc,
       SUM(IIF(
              counter_name = 'Processes blocked',
              cntr_value,
              NULL
              )) AS processes_blocked,
       SUM(IIF(
              counter_name = 'Active memory grants count',
              cntr_value,
              NULL
              )) AS active_memory_grants,
       SUM(IIF(
              counter_name = 'Pending memory grants count',
              cntr_value,
              NULL
              )) AS pending_memory_grants
FROM (
     SELECT sample_time_utc,
            counter_name,
            cntr_value
     FROM sqlmi_performance_counters_common
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
         AND
         managed_instance_name = @InstanceFqdnName
         AND
         replica_type = @ReplicaType
         AND
         cntr_type = 65792 /* restrict to point-in-time counters */
         AND
         (
         (object_name = 'General Statistics' AND counter_name IN ('Processes blocked'))
         OR
         (object_name = 'Resource Pool Stats' AND counter_name IN ('Active memory grants count','Pending memory grants count'))
         )
     ) AS pc
GROUP BY sample_time_utc
ORDER BY sample_time_utc DESC;
```

---

### Use T-SQL to analyze cumulative waits over time

In this example, the query returns the top 10 wait types by the average cumulative wait time over a 30-minute interval. Cumulative means that the query calculates the total time, in milliseconds, spent waiting under each wait type by all requests in every second. Because multiple requests can execute (and wait) concurrently, the cumulative wait time in each second can be more than one second.

# [SQL database](#tab/sqldb)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @DatabaseName sysname = 'your-database-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT TOP (10) wait_type,
                SUM(CAST((wait_time_ms - prev_wait_time_ms) AS decimal)) * 1000
                /
                SUM(DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc))
                AS wait_time_ms_per_sec
FROM (
     SELECT sample_time_utc,
            LAG(sample_time_utc) OVER (PARTITION BY replica_id, wait_type ORDER BY sample_time_utc ASC) AS prev_sample_time_utc,
            wait_type,
            wait_time_ms,
            LAG(wait_time_ms) OVER (PARTITION BY replica_id, wait_type ORDER BY sample_time_utc ASC) AS prev_wait_time_ms
     FROM sqldb_database_wait_stats
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
         AND
         logical_server_name = @LogicalServerName
         AND
         database_name = @DatabaseName
         AND
         replica_type = @ReplicaType
     ) AS w
WHERE sample_time_utc >= prev_sample_time_utc
      AND
      wait_time_ms >= prev_wait_time_ms
GROUP BY wait_type
ORDER BY wait_time_ms_per_sec DESC;
```

# [SQL elastic pool](#tab/sqlep)

```sql
DECLARE @LogicalServerName sysname = 'your-server-name',
        @ElasticPoolName sysname = 'your-elastic-pool-name',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT TOP (10) wait_type,
                SUM(CAST((wait_time_ms - prev_wait_time_ms) AS decimal)) * 1000
                /
                SUM(DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc))
                AS wait_time_ms_per_sec
FROM (
     SELECT sample_time_utc,
            LAG(sample_time_utc) OVER (PARTITION BY anchor_database_replica_id, wait_type ORDER BY sample_time_utc ASC) AS prev_sample_time_utc,
            wait_type,
            wait_time_ms,
            LAG(wait_time_ms) OVER (PARTITION BY anchor_database_replica_id, wait_type ORDER BY sample_time_utc ASC) AS prev_wait_time_ms
     FROM sqldb_elastic_pool_wait_stats
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
         AND
         logical_server_name = @LogicalServerName
         AND
         elastic_pool_name = @ElasticPoolName
         AND
         replica_type = @ReplicaType
     ) AS w
WHERE sample_time_utc >= prev_sample_time_utc
      AND
      wait_time_ms >= prev_wait_time_ms
GROUP BY wait_type
ORDER BY wait_time_ms_per_sec DESC;
```

# [SQL managed instance](#tab/sqlmi)

```sql
DECLARE @InstanceFqdnName sysname = 'your-instance-name.unique-dns-prefix.database.windows.net',
        @ReplicaType sysname = 'Primary',
        @DurationMinutes int = 30;

SELECT TOP (10) wait_type,
                SUM(CAST((wait_time_ms - prev_wait_time_ms) AS decimal)) * 1000
                /
                SUM(DATEDIFF(millisecond, prev_sample_time_utc, sample_time_utc))
                AS wait_time_ms_per_sec
FROM (
     SELECT sample_time_utc,
            LAG(sample_time_utc) OVER (PARTITION BY wait_type ORDER BY sample_time_utc ASC) AS prev_sample_time_utc,
            wait_type,
            wait_time_ms,
            LAG(wait_time_ms) OVER (PARTITION BY wait_type ORDER BY sample_time_utc ASC) AS prev_wait_time_ms
     FROM sqlmi_wait_stats
     WHERE sample_time_utc > DATEADD(minute, -@DurationMinutes, SYSUTCDATETIME())
         AND
         managed_instance_name = @InstanceFqdnName
         AND
         replica_type = @ReplicaType
     ) AS w
WHERE sample_time_utc >= prev_sample_time_utc
      AND
      wait_time_ms >= prev_wait_time_ms
GROUP BY wait_type
ORDER BY wait_time_ms_per_sec DESC;
```

---

## Related content

- [Monitor Azure SQL workloads with database watcher (preview)](database-watcher-overview.md)
- [Quickstart: Create a database watcher to monitor Azure SQL (preview)](database-watcher-quickstart.md)
- [Create and configure a database watcher (preview)](database-watcher-manage.md)
- [Database watcher FAQ](database-watcher-faq.yml)
- [Kusto Query Language learning resources](/azure/data-explorer/kql-learning-resources)
