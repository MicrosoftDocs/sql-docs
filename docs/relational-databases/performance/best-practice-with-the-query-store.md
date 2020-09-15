---
title: "Best practices with Query Store | Microsoft Docs"
description: Learn best practices for using SQL Server Query Store with your workload, such as using the latest SQL Server Management Studio and Query Performance Insight.
ms.custom: ""
ms.date: "09/02/2020"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Store, best practices"
ms.assetid: 5b13b5ac-1e4c-45e7-bda7-ebebe2784551
author: pmasl
ms.author: jrasnick
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||= azure-sqldw-latest||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Best practices with Query Store

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article outlines the best practices for using SQL Server Query Store with your workload.

## <a name="SSMS"></a> Use the latest [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]

[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] has a set of user interfaces designed for configuring Query Store and for consuming collected data about your workload. Download the latest version of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] [here](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

For a quick description on how to use Query Store in troubleshooting scenarios, see [Query Store @Azure blogs](https://azure.microsoft.com/blog/query-store-a-flight-data-recorder-for-your-database/).

## <a name="Insight"></a> Use Query Performance Insight in Azure SQL Database

If you run Query Store in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you can use [Query Performance Insight](https://docs.microsoft.com/azure/sql-database/sql-database-query-performance) to analyze resource consumption over time. While you can use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and [Azure Data Studio](../../azure-data-studio/what-is.md) to get detailed resource consumption for all your queries, such as CPU, memory, and I/O, Query Performance Insight gives you a quick and efficient way to determine their impact on overall DTU consumption for your database. For more information, see [Azure SQL Database Query Performance Insight](https://azure.microsoft.com/documentation/articles/sql-database-query-performance/).

This section describes optimal configuration defaults that are designed to ensure reliable operation of the Query Store and dependent features. Default configuration is optimized for continuous data collection, that is minimal time spent in OFF/READ_ONLY states. For more information about all available Query Store options, see [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md#query-store).

| Configuration | Description | Default | Comment |
| --- | --- | --- | --- |
| MAX_STORAGE_SIZE_MB |Specifies the limit for the data space that Query Store can take inside the customer database |100 |Enforced for new databases |
| INTERVAL_LENGTH_MINUTES |Defines size of time window during which collected runtime statistics for query plans are aggregated and persisted. Every active query plan has at most one row for a period of time defined with this configuration |60 |Enforced for new databases |
| STALE_QUERY_THRESHOLD_DAYS |Time-based cleanup policy that controls the retention period of persisted runtime statistics and inactive queries |30 |Enforced for new databases and databases with previous default (367) |
| SIZE_BASED_CLEANUP_MODE |Specifies whether automatic data cleanup takes place when Query Store data size approaches the limit |AUTO |Enforced for all databases |
| QUERY_CAPTURE_MODE |Specifies whether all queries or only a subset of queries are tracked |AUTO |Enforced for all databases |
| FLUSH_INTERVAL_SECONDS |Specifies maximum period during which captured runtime statistics are kept in memory, before flushing to disk |900 |Enforced for new databases |
| | | | |

> [!IMPORTANT]
> These defaults are automatically applied in the final stage of Query Store activation in all [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. After it's enabled, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] won't change configuration values that are set by customers, unless they negatively impact primary workload or reliable operations of the Query Store.

> [!NOTE]  
> Query Store cannot be disabled in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] single database and Elastic Pool. Executing `ALTER DATABASE [database] SET QUERY_STORE = OFF` will return the warning `'QUERY_STORE=OFF' is not supported in this version of SQL Server.`. 

If you want to stay with your custom settings, use [ALTER DATABASE with Query Store options](../../t-sql/statements/alter-database-transact-sql-set-options.md#query-store) to revert configuration to the previous state. Check out [Best Practices with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md) in order to learn how to choose optimal configuration parameters.

## Use Query Store with Elastic Pool databases

You can use Query Store in all databases without concerns, in even densely packed pools. All issues related to excessive resource usage that might have occurred when Query Store was enabled for the large number of databases in the elastic pools have been resolved.

## <a name="Configure"></a> Keep Query Store adjusted to your workload

Configure Query Store based on your workload and performance troubleshooting requirements.
The default parameters are good enough to start, but you should monitor how Query Store behaves over time and adjust its configuration accordingly.

 ![Query Store properties](../../relational-databases/performance/media/query-store-properties.png "query-store-properties")

 Here are guidelines to follow for setting parameter values:

**Max Size (MB)**: Specifies the limit for the data space that Query Store takes inside your database. This is the most important setting that directly affects the operation mode of Query Store.

While Query Store collects queries, execution plans, and statistics, its size in the database grows until this limit is reached. When that happens, Query Store automatically changes the operation mode to read-only and stops collecting new data, which means that your performance analysis is no longer accurate.

The default value in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] is 100 MB. This size might not be sufficient if your workload generates a large number of different queries and plans or if you want to keep query history for a longer period of time. Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)], the default value is 1 GB. Keep track of current space usage and increase the **Max Size (MB)** value to prevent Query Store from transitioning to read-only mode.

> [!IMPORTANT]
> The **Max Size (MB)** limit isn't strictly enforced. Storage size is checked only when Query Store writes data to disk. This interval is set by the **Data Flush Interval (Minutes)** option. If Query Store has breached the maximum size limit between storage size checks, it transitions to read-only mode. If **Size Based Cleanup Mode** is enabled, the cleanup mechanism to enforce the maximum size limit is also triggered.

Use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or execute the following script to get the latest information about Query Store size:

```sql
USE [QueryStoreDB];
GO

SELECT actual_state_desc, desired_state_desc, current_storage_size_mb,
 max_storage_size_mb, readonly_reason
FROM sys.database_query_store_options;
```

The following script sets a new value for **Max Size (MB)**:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (MAX_STORAGE_SIZE_MB = 1024);
```

 **Data Flush Interval (Minutes)**: It defines the frequency to persist collected runtime statistics to disk. It's expressed in minutes in the graphical user interface (GUI), but in [!INCLUDE[tsql](../../includes/tsql-md.md)] it's expressed in seconds. The default is 900 seconds, which is 15 minutes in the graphical user interface. Consider using a higher value if your workload doesn't generate a large number of different queries and plans, or if you can withstand longer time to persist data before a database shutdown.

> [!NOTE]
> Using trace flag 7745 prevents Query Store data from being written to disk in case of a failover or shutdown command. For more information, see the [Use trace flags on mission-critical servers](#Recovery) section.

Use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] to set a different value for **Data Flush Interval**:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (DATA_FLUSH_INTERVAL_SECONDS = 900);
```

**Statistics Collection Interval**: Defines the level of granularity for the collected runtime statistic, expressed in minutes. The default is 60 minutes. Consider using a lower value if you require finer granularity or less time to detect and mitigate issues. Keep in mind that the value directly affects the size of Query Store data. Use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] to set a different value for **Statistics Collection Interval**:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (INTERVAL_LENGTH_MINUTES = 60);
```

**Stale Query Threshold (Days)**: Time-based cleanup policy that controls the retention period of persisted runtime statistics and inactive queries, expressed in days. By default, Query Store is configured to keep the data for 30 days, which might be unnecessarily long for your scenario.

Avoid keeping historical data that you don't plan to use. This practice reduces changes to read-only status. The size of Query Store data and the time to detect and mitigate the issue will be more predictable. Use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or the following script to configure time-based cleanup policy:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 90));
```

**Size Based Cleanup Mode**: Specifies whether automatic data cleanup takes place when Query Store data size approaches the limit. Activate size-based cleanup to make sure that Query Store always runs in read-write mode and collects the latest data.

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (SIZE_BASED_CLEANUP_MODE = AUTO);
```

**Query Store Capture Mode**: Specifies the query capture policy for Query Store.

- **All**: Captures all queries. This option is the default in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].
- **Auto**: Infrequent queries and queries with insignificant compile and execution duration are ignored. Thresholds for execution count, compile, and runtime duration are internally determined. Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)], this is the default option.
- **None**: Query Store stops capturing new queries.
- **Custom**: Allows additional control and the capability to fine-tune the data collection policy. The new custom settings define what happens during the internal capture policy time threshold. This is a time boundary during which the configurable conditions are evaluated and, if any are true, the query is eligible to be captured by Query Store.

> [!IMPORTANT]
> Cursors, queries inside stored procedures, and natively compiled queries are always captured when Query Store Capture Mode is set to **All**, **Auto**, or **Custom**. To capture natively compiled queries, enable collection of per-query statistics by using [sys.sp_xtp_control_query_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-query-exec-stats-transact-sql.md).

 The following script sets QUERY_CAPTURE_MODE to AUTO:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (QUERY_CAPTURE_MODE = AUTO);
```

### Examples

The following example sets QUERY_CAPTURE_MODE to AUTO and sets other recommended options in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      QUERY_CAPTURE_MODE = AUTO,
      MAX_STORAGE_SIZE_MB = 1000,
      INTERVAL_LENGTH_MINUTES = 60
    );
```

The following example sets QUERY_CAPTURE_MODE to AUTO and sets other recommended options in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] to include wait statistics:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      QUERY_CAPTURE_MODE = AUTO,
      MAX_STORAGE_SIZE_MB = 1000,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON
    );
```

The following example sets QUERY_CAPTURE_MODE to AUTO and sets other recommended options in [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)], and *optionally* sets the CUSTOM capture policy with its defaults, instead of the new default AUTO capture mode:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      MAX_STORAGE_SIZE_MB = 1000,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON,
      QUERY_CAPTURE_MODE = CUSTOM,
      QUERY_CAPTURE_POLICY = (
        STALE_CAPTURE_POLICY_THRESHOLD = 24 HOURS,
        EXECUTION_COUNT = 30,
        TOTAL_COMPILE_CPU_TIME_MS = 1000,
        TOTAL_EXECUTION_CPU_TIME_MS = 100
      )
    );
```

## Start with query performance troubleshooting

The troubleshooting workflow with Query Store is simple, as shown in the following diagram:

![Query Store troubleshooting](../../relational-databases/performance/media/query-store-troubleshooting.png "query-store-troubleshooting")

Enable Query Store by using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], as described in the previous section, or execute the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:

```sql
ALTER DATABASE [DatabaseOne] SET QUERY_STORE = ON;
```

It takes some time until Query Store collects the data set that accurately represents your workload. Usually, one day is enough even for very complex workloads. However, you can start exploring the data and identify queries that need your attention immediately after you enable the feature. Go to the Query Store subfolder under the database node in Object Explorer of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to open troubleshooting views for specific scenarios.

[!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] Query Store views operate with the set of execution metrics, each expressed as any of the following statistic functions:

|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version|Execution metric|Statistic function|
|----------------------|----------------------|------------------------|
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|CPU time, Duration, Execution count, Logical reads, Logical writes, Memory consumption, Physical reads, CLR time, Degree of parallelism (DOP), and Row count|Average, Maximum, Minimum, Standard Deviation, Total|
|[!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]|CPU time, Duration, Execution count, Logical reads, Logical writes, Memory consumption, Physical reads, CLR time, Degree of parallelism, Row count, Log memory, TempDB memory, and Wait times|Average, Maximum, Minimum, Standard Deviation, Total|

The following graphic shows how to locate Query Store views:

![Query Store views](../../relational-databases/performance/media/objectexplorerquerystore_sql17.png "Query Store views")

The following table explains when to use each of the Query Store views:

|SQL Server Management Studio view|Scenario|
|---------------|--------------|
|**Regressed Queries**|Pinpoint queries for which execution metrics have recently regressed (for example, changed to worse). <br />Use this view to correlate observed performance problems in your application with the actual queries that need to be fixed or improved.|
|**Overall Resource Consumption**|Analyze the total resource consumption for the database for any of the execution metrics.<br />Use this view to identify resource patterns (daily vs. nightly workloads) and optimize overall consumption for your database.|
|**Top Resource Consuming Queries**|Choose an execution metric of interest, and identify queries that had the most extreme values for a provided time interval. <br />Use this view to focus your attention on the most relevant queries that have the biggest impact to database resource consumption.|
|**Queries With Forced Plans**|Lists previously forced plans using Query Store. <br />Use this view to quickly access all currently forced plans.|
|**Queries With High Variation**|Analyze queries with high-execution variation as it relates to any of the available dimensions, such as Duration, CPU time, IO, and Memory usage, in the desired time interval.<br />Use this view to identify queries with widely variant performance that can be affecting user experience across your applications.|
|**Query Wait Statistics**|Analyze wait categories that are most active in a database and which queries contribute most to the selected wait category.<br />Use this view to analyze wait statistics and identify queries that might be affecting user experience across your applications.<br /><br />Applies to: Starting with [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] v18.0 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].|
|**Tracked Queries**|Track the execution of the most important queries in real time. Typically, you use this view when you have queries with forced plans and you want to make sure that query performance is stable.|

> [!TIP]
> For a detailed description of how to use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to identify the top resource-consuming queries and fix those that regressed due to the change of a plan choice, see [Query Store @Azure Blogs](https://azure.microsoft.com/blog/query-store-a-flight-data-recorder-for-your-database/).

When you identify a query with suboptimal performance, your action depends on the nature of the problem.

- If the query was executed with multiple plans and the last plan is significantly worse than the previous plan, you can use the plan forcing mechanism to force it. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to force the plan in the optimizer. If plan forcing fails, an XEvent is fired and the optimizer is instructed to optimize in the normal way.

  ![Query Store force plan](../../relational-databases/performance/media/query-store-force-plan.png "query-store-force-plan")

  > [!NOTE]
  > The previous graphic might feature different shapes for specific query plans, with the following meanings for each possible status:<br />
  >
  > |Shape|Meaning|
  > |-------------------|-------------|
  > |Circle|Query completed, which means that a regular execution successfully finished.|
  > |Square|Cancelled, which means that a client-initiated aborted execution.|
  > |Triangle|Failed, which means that an exception aborted execution.|
  >
  > Also, the size of the shape reflects the query execution count within the specified time interval. The size increases with a higher number of executions.

- You might conclude that your query is missing an index for optimal execution. This information is surfaced within the query execution plan. Create the missing index, and check the query performance by usingQuery Store.

   ![Query Store show plan](../../relational-databases/performance/media/query-store-show-plan.png "query-store-show-plan")

If you run your workload on [!INCLUDE[ssSDS](../../includes/sssds-md.md)], sign up for [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Index Advisor to automatically receive index recommendations.

- In some cases, you might enforce statistic recompilation if you see that the difference between the estimated and the actual number of rows in the execution plan is significant.
- Rewrite problematic queries, for example, to take advantage of query parameterization or to implement more optimal logic.

## <a name="Verify"></a> Verify that Query Store collects query data continuously

Query Store can silently change the operation mode. Regularly monitor the state of Query Store to ensure that Query Store is operating, and to take action to avoid failures due to preventable causes. Execute the following query to determine the operation mode and view the most relevant parameters:

```sql
USE [QueryStoreDB];
GO

SELECT actual_state_desc, desired_state_desc, current_storage_size_mb,
    max_storage_size_mb, readonly_reason, interval_length_minutes,
    stale_query_threshold_days, size_based_cleanup_mode_desc,
    query_capture_mode_desc
FROM sys.database_query_store_options;
```

The difference between the `actual_state_desc` and `desired_state_desc` indicates that a change of the operation mode occurred automatically. The most common change is for Query Store to silently switch to read-only mode. In extremely rare circumstances, Query Store can end up in the ERROR state because of internal errors.

When the actual state is read-only, use the **readonly_reason** column to determine the root cause. Typically, you find that Query Store transitioned to read-only mode because the size quota was exceeded. In that case, the **readonly_reason** is set to 65536. For other reasons, see [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md).

Consider the following steps to switch Query Store to read-write mode and activate data collection:

- Increase the maximum storage size by using the **MAX_STORAGE_SIZE_MB** option of **ALTER DATABASE**.
- Clean up Query Store data by using the following statement:

  ```sql
  ALTER DATABASE [QueryStoreDB] SET QUERY_STORE CLEAR;
  ```

You can apply one or both of these steps by executing the following statement that explicitly changes the operation mode back to read-write:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (OPERATION_MODE = READ_WRITE);
```

Take the following steps to be proactive:

- You can prevent silent changes of operation mode by applying best practices. Ensure that Query Store size is always below the maximally allowed value to dramatically reduce a chance of transitioning to read-only mode. Activate size-based policy as described in the [Configure Query Store](#Configure) section so that Query Store automatically cleans data when the size approaches the limit.
- To make sure that most recent data is retained, configure time-based policy to remove stale information regularly.
- Finally, consider setting **Query Store Capture Mode** to **Auto** because it filters out queries that are usually less relevant for your workload.

### ERROR state

To recover Query Store, try explicitly setting the read-write mode and check the actual state again.

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (OPERATION_MODE = READ_WRITE);
GO

SELECT actual_state_desc, desired_state_desc, current_storage_size_mb,
    max_storage_size_mb, readonly_reason, interval_length_minutes,
    stale_query_threshold_days, size_based_cleanup_mode_desc,
    query_capture_mode_desc
FROM sys.database_query_store_options;
```

If the problem persists, it indicates that corruption of Query Store data is persisted on the disk.

Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], Query Store can be recovered by executing the **sp_query_store_consistency_check** stored procedure within the affected database. Query Store must be disabled before you attempt the recovery operation. For [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you need to clear the data from Query Store as shown.

If the recovery was unsuccessful, you can try clearing Query Store before you set the read-write mode.

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE CLEAR;
GO

ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (OPERATION_MODE = READ_WRITE);
GO

SELECT actual_state_desc, desired_state_desc, current_storage_size_mb,
    max_storage_size_mb, readonly_reason, interval_length_minutes,
    stale_query_threshold_days, size_based_cleanup_mode_desc,
    query_capture_mode_desc
FROM sys.database_query_store_options;
```

## Set the optimal Query Store Capture Mode

Keep the most relevant data in Query Store. The following table describes typical scenarios for each Query Store Capture Mode:

|Query Store Capture Mode|Scenario|
|------------------------|--------------|
|**All**|Analyze your workload thoroughly in terms of all queries' shapes and their execution frequencies and other statistics.<br /><br /> Identify new queries in your workload.<br /><br /> Detect if ad-hoc queries are used to identify opportunities for user or auto parameterization.<br /><br />Note: This is the default capture mode in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].|
|**Auto**|Focus your attention on relevant and actionable queries. An example is those queries that execute regularly or that have significant resource consumption.<br /><br />Note: Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)], this is the default capture mode.|
|**None**|You've already captured the query set that you want to monitor in runtime and you want to eliminate the distractions that other queries might introduce.<br /><br /> None is suitable for testing and benchmarking environments.<br /><br /> None is also appropriate for software vendors who ship Query Store configuration configured to monitor their application workload.<br /><br /> None should be used with caution because you might miss the opportunity to track and optimize important new queries. Avoid using None unless you have a specific scenario that requires it.|
|**Custom**|[!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] introduces a Custom capture mode under the `ALTER DATABASE SET QUERY_STORE` command. When enabled, additional Query Store configurations are available under a new Query Store capture policy setting to fine-tune data collection in a specific server.<br /><br />The new custom settings define what happens during the internal capture policy time threshold. This is a time boundary during which the configurable conditions are evaluated and, if any are true, the query is eligible to be captured by Query Store. For more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).|

> [!NOTE]
> Cursors, queries inside stored procedures, and natively compiled queries are always captured when Query Store Capture Mode is set to **All**, **Auto**, or **Custom**. To capture natively compiled queries, enable collection of per-query statistics by using [sys.sp_xtp_control_query_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-query-exec-stats-transact-sql.md).

## Keep the most relevant data in Query Store

Configure Query Store to contain only the relevant data so that it runs continuously and provides a great troubleshooting experience with a minimal impact on your regular workload.
The following table provides best practices:

|Best practice|Setting|
|-------------------|-------------|
|Limit retained historical data.|Configure time-based policy to activate autocleanup.|
|Filter out nonrelevant queries.|Configure **Query Store Capture Mode** to **Auto**.|
|Delete less relevant queries when the maximum size is reached.|Activate size-based cleanup policy.|

## <a name="Parameterize"></a> Avoid using non-parameterized queries

Using non-parameterized queries when that isn't necessary isn't a best practice. An example is in the case of ad-hoc analysis. Cached plans can't be reused, which forces Query Optimizer to compile queries for every unique query text. For more information, see [Guidelines for using forced parameterization](../../relational-databases/query-processing-architecture-guide.md#ForcedParamGuide).

Also, Query Store can rapidly exceed the size quota because of a potentially large number of different query texts and consequently a large number of different execution plans with similar shape. As a result, performance of your workload is suboptimal, and Query Store might switch to read-only mode or constantly delete data to try to keep up with the incoming queries.

Consider the following options:

- Parameterize queries where applicable. For example, wrap queries inside a stored procedure or sp_executesql. For more information, see [Parameters and execution plan reuse](../../relational-databases/query-processing-architecture-guide.md#PlanReuse).
- Use the [optimize for ad hoc workloads](../../database-engine/configure-windows/optimize-for-ad-hoc-workloads-server-configuration-option.md) option if your workload contains many single-use ad-hoc batches with different query plans.
  - Compare the number of distinct query_hash values with the total number of entries in sys.query_store_query. If the ratio is close to 1, your ad-hoc workload generates different queries.
- Apply [forced parameterization](../../relational-databases/query-processing-architecture-guide.md#ForcedParam) for the database or for a subset of queries if the number of different query plans isn't large.
  - Use a [plan guide](../../relational-databases/performance/specify-query-parameterization-behavior-by-using-plan-guides.md) to force parameterization only for the selected query.
  - Configure forced parameterization by using the [parameterization database option](../../relational-databases/databases/database-properties-options-page.md#miscellaneous) command, if there are a small number of different query plans in your workload. An example is when the ratio between the count of distinct query_hash and the total number of entries in sys.query_store_query is much less than 1.
- Set QUERY_CAPTURE_MODE to AUTO to automatically filter out ad-hoc queries with small resource consumption.

## <a name="Drop"></a> Avoid a DROP and CREATE pattern for containing objects

Query Store associates query entry with a containing object, such as stored procedure, function, and trigger. When you re-create a containing object, a new query entry is generated for the same query text. This prevents you from tracking performance statistics for that query over time and using a plan forcing mechanism. To avoid this situation, use the `ALTER <object>` process to change a containing object definition whenever it's possible.

## <a name="CheckForced"></a> Check the status of forced plans regularly

Plan forcing is a convenient mechanism to fix performance for the critical queries and make them more predictable. As with plan hints and plan guides, forcing a plan isn't a guarantee that it will be used in future executions. Typically, when database schema changes in a way that objects referenced by the execution plan are altered or dropped, plan forcing starts failing. In that case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] falls back to query recompilation while the actual forcing failure reason is surfaced in [sys.query_store_plan](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md). The following query returns information about forced plans:

```sql
USE [QueryStoreDB];
GO

SELECT p.plan_id, p.query_id, q.object_id as containing_object_id,
    force_failure_count, last_force_failure_reason_desc
FROM sys.query_store_plan AS p
JOIN sys.query_store_query AS q on p.query_id = q.query_id
WHERE is_forced_plan = 1;
```

For a full list of reasons, see [sys.query_store_plan](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md). You can also use the **query_store_plan_forcing_failed** XEvent to track and troubleshoot plan forcing failures.

## <a name="Renaming"></a> Avoid renaming databases for queries with forced plans

Execution plans reference objects by using three-part names like `database.schema.object`.

If you rename a database, plan forcing fails, which causes recompilation in all subsequent query executions.

## <a name="Recovery"></a> Using Query Store in mission-critical servers

The global trace flags 7745 and 7752 can be used to improve availability of databases by using Query Store. For more information, see [Trace flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

- Trace flag 7745 prevents the default behavior where Query Store writes data to disk before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be shut down. This means that Query Store data that has been collected but not yet persisted to disk will be lost, up to the time window defined with `DATA_FLUSH_INTERVAL_SECONDS`.
- Trace flag 7752 enables asynchronous load of Query Store. This allows a database to become online and queries to be executed before Query Store has been fully recovered. The default behavior is to do a synchronous load of Query Store. The default behavior prevents queries from executing before Query Store has been recovered but also prevents any queries from being missed in the data collection.

> [!NOTE]
> Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)], this behavior is controlled by the engine, and trace flag 7752 has no effect.

> [!IMPORTANT]
> If you're using Query Store for just-in-time workload insights in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], plan to install the performance scalability improvements in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 CU2 ([KB 4340759](https://support.microsoft.com/help/4340759)) as soon as possible. Without these improvements, when the database is under heavy workloads, spinlock contention may occur and server performance may become slow. In particular, you may see heavy contention on the `QUERY_STORE_ASYNC_PERSIST` spinlock or `SPL_QUERY_STORE_STATS_COOKIE_CACHE` spinlock. After this improvement is applied, Query Store will no longer cause spinlock contention.

> [!IMPORTANT]
> If you're using Query Store for just-in-time workload insights in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], plan to install the performance scalability improvement in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU22 as soon as possible. Without this improvement, when the database is under heavy ad-hoc workloads, the Query Store may use a large amount of memory and server performance may become slow. After this improvement is applied, Query Store imposes internal limits to the amount of memory its various components can use, and can automatically change the operation mode to read-only until enough memory has been returned to the [!INCLUDE[ssde_md](../../includes/ssde_md.md)]. Note that Query Store internal memory limits are not documented because they are subject to change.  

## See also

- [ALTER DATABASE SET options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [Query Store catalog views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Query Store stored procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)
- [Use Query Store with In-Memory OLTP](../../relational-databases/performance/using-the-query-store-with-in-memory-oltp.md)
- [Monitor performance by using Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Query processing architecture guide](../../relational-databases/query-processing-architecture-guide.md)
