---
title: "Best practices for managing the Query Store"
description: Learn best practices for managing the SQL Server Query Store, including version specific details, managing custom capture policies, and other performance features.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/12/2022
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "Query Store, best practices"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Best practices for managing the Query Store

[!INCLUDE [SQL Server ASDB, ASDBMI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article outlines the management of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Query Store and the surrounding features.

- For more information on configuring and administering with the Query Store, see [Monitoring performance by using the Query Store](monitoring-performance-by-using-the-query-store.md).

> [!NOTE]  
> In [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], Query Store is now enabled by default for all newly created SQL Server databases to help better track performance history, troubleshoot query plan–related issues, and enable new query processor capabilities.

### <a id="QueryStoreOptions"></a> Query Store defaults in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

This section describes optimal configuration defaults in Azure SQL Database that are designed to ensure reliable operation of the Query Store and dependent features. Default configuration is optimized for continuous data collection, that is minimal time spent in OFF/READ_ONLY states. For more information about all available Query Store options, see [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md#query_store_option-).

| Configuration | Description | Default | Comment |
| --- | --- | --- | --- |
| MAX_STORAGE_SIZE_MB |Specifies the limit for the data space that Query Store can take inside the customer database |100 prior to [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]<br />1000 starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] |Enforced for new databases |
| INTERVAL_LENGTH_MINUTES |Defines size of time window during which collected runtime statistics for query plans are aggregated and persisted. Every active query plan has at most one row for a period of time defined with this configuration |60 |Enforced for new databases |
| STALE_QUERY_THRESHOLD_DAYS |Time-based cleanup policy that controls the retention period of persisted runtime statistics and inactive queries |30 |Enforced for new databases and databases with previous default (367) |
| SIZE_BASED_CLEANUP_MODE |Specifies whether automatic data cleanup takes place when Query Store data size approaches the limit |AUTO |Enforced for all databases |
| QUERY_CAPTURE_MODE |Specifies whether all queries or only a subset of queries are tracked |AUTO |Enforced for all databases |
| DATA_FLUSH_INTERVAL_SECONDS |Specifies maximum period during which captured runtime statistics are kept in memory, before flushing to disk |900 |Enforced for new databases |

> [!IMPORTANT]  
> These defaults are automatically applied in the final stage of Query Store activation in an [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. After it's enabled, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] won't change configuration values that are set by customers, unless they negatively impact primary workload or reliable operations of the Query Store.

> [!NOTE]  
> Query Store cannot be disabled in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] single database and Elastic Pool. Executing `ALTER DATABASE [database] SET QUERY_STORE = OFF` will return the warning `'QUERY_STORE=OFF' is not supported in this version of SQL Server.`

If you want to stay with your custom settings, use [ALTER DATABASE with Query Store options](../../t-sql/statements/alter-database-transact-sql-set-options.md#query-store) to revert configuration to the previous state. Check out [Best Practices with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md) in order to learn how to choose optimal configuration parameters.

## Set the optimal Query Store Capture Mode

Keep the most relevant data in Query Store. The following table describes typical scenarios for each Query Store Capture Mode:

|Query Store Capture Mode|Scenario|
|------------------------|--------------|
|**All**|Analyze your workload thoroughly in terms of all queries' shapes and their execution frequencies and other statistics.<br /><br />Identify new queries in your workload.<br /><br />Detect if ad-hoc queries are used to identify opportunities for user or auto parameterization.<br /><br />Note: This is the default capture mode in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].|
|**Auto**|Focus your attention on relevant and actionable queries. An example is those queries that execute regularly or that have significant resource consumption.<br /><br />Note: Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], this is the default capture mode.|
|**None**|You've already captured the query set that you want to monitor in runtime and you want to eliminate the distractions that other queries might introduce.<br /><br />None is suitable for testing and benchmarking environments.<br /><br />None is also appropriate for software vendors who ship Query Store configuration configured to monitor their application workload.<br /><br />None should be used with caution because you might miss the opportunity to track and optimize important new queries. Avoid using None unless you have a specific scenario that requires it.|
|**Custom**|[!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] introduced a custom capture mode under the `ALTER DATABASE ... SET QUERY_STORE` command. While Auto is default and recommended, if there is still any concern about the overhead Query Store may introduce, database administrators can leverage custom capture policies to further tune the Query Store capture behavior. For more information and recommendations, see [Custom capture policies](#custom-capture-policies) later in this article. For more information on this syntax, see [ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md#query_capture_mode--all--auto--custom--none-).|

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

## Custom capture policies

When the CUSTOM Query Store Capture Mode is enabled, additional Query Store configurations are available under a new Query Store capture policy setting to fine-tune data collection in a specific server.<br /><br />The new custom settings define what happens during the internal capture policy time threshold. This is a time boundary during which the configurable conditions are evaluated and, if any are true, the query is eligible to be captured by Query Store.

The **Query Store Capture Mode** specifies the query capture policy for Query Store.

- **All**: Captures all queries. This option is the default in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].
- **Auto**: Infrequent queries and queries with insignificant compile and execution duration are ignored. Thresholds for execution count, compile, and runtime duration are internally determined. Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], this is the default option.
- **None**: Query Store stops capturing new queries.
- **Custom**: Allows additional control and the capability to fine-tune the data collection policy. The new custom settings define what happens during the internal capture policy time threshold. This is a time boundary during which the configurable conditions are evaluated and, if any are true, the query is eligible to be captured by Query Store.

Tuning an appropriate custom capture policy for your environment should be considered when:

- The database is very large.
- The database has a large number of unique, ad hoc queries.
- The database has specific size or growth limitations.

### [SSMS](#tab/ssms)

:::image type="icon" source="../../includes/media/download.svg" border="false":::**[Use the latest version of SQL Server Management Studio (SSMS)](https://aka.ms/ssms)**

To view current settings in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]:

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, right-click on the database.
1. Select **Properties**.
1. Select **Query Store**. On the **Query Store** page, verify that the **Operation Mode (Requested)** is **Read write**.
1. Change **Query Store Capture Mode** to **Custom**.
1. Note the four capture policy fields under **Query Store Capture Policy** are now enabled and configurable.

### [T-SQL](#tab/tsql)

Use the ALTER DATABASE .. SET syntax to set `QUERY_CAPTURE_MODE = CUSTOM` and then specify options for `QUERY_CAPTURE_POLICY`. See the T-SQL examples below and the [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md#query_capture_policy_option_list--) for details on the custom capture policy options.

---

### Example custom capture policies

The following example sets QUERY_CAPTURE_MODE to AUTO and sets a custom capture mode. Each of the following sets the custom capture policies to its default value in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. You may consider adjusting these values to reduce the number of queries captured, and therefore reduce the on-disk footprint of the Query Store. It is recommended to gradually change these values by small increments.

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
      QUERY_CAPTURE_MODE = CUSTOM,
      QUERY_CAPTURE_POLICY = (
        STALE_CAPTURE_POLICY_THRESHOLD = 24 HOURS,
        EXECUTION_COUNT = 30,
        TOTAL_COMPILE_CPU_TIME_MS = 1000,
        TOTAL_EXECUTION_CPU_TIME_MS = 100
      )
    );
```

The following sample query alters an existing Query Store to use a custom capture policy that overrides the default settings for `EXECUTION_COUNT` and `TOTAL_COMPILE_CPU_TIME_MS`.

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE = ON
    (
      QUERY_CAPTURE_MODE = CUSTOM,
      QUERY_CAPTURE_POLICY = (
        EXECUTION_COUNT = 100,
        TOTAL_COMPILE_CPU_TIME_MS = 10000
      )
    );
```

## Query Store maximum size

The default maximum size value of the Query Store is 1000 MB, starting in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]. In previous versions, the default was 100 MB. Increasing the size of Query Store may be appropriate in busy instance with many unique query plans. Adjusting the capture policy (see previous section) is a more important consideration to limit the on-disk size of the Query Store and to prevent the Query Store from entering READ_ONLY mode. While Query Store collects queries, execution plans, and statistics, its size in the database grows until this limit is reached. When that happens, Query Store automatically changes the operation mode to READ_ONLY and stops collecting new data, which means that your performance analysis is no longer accurate.

`MAX_STORAGE_SIZE_MB` limit isn't strictly enforced. Storage size is checked only when Query Store writes data to disk. This interval is set by the `DATA_FLUSH_INTERVAL_SECONDS` option or the [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)] Query Store dialog option **Data Flush Interval**. The interval default value is 900 seconds (or 15 minutes). If the Query Store has breached the `MAX_STORAGE_SIZE_MB` limit between storage size checks, it will transition to read-only mode. If `SIZE_BASED_CLEANUP_MODE` is enabled, the cleanup mechanism to enforce the `MAX_STORAGE_SIZE_MB` limit is also triggered. Once enough space has been cleared, the Query Store mode will automatically switch back to READ_WRITE mode.

For more information, see the [ALTER DATABASE SET OPTION MAX_STORAGE_SIZE_MB](../../t-sql/statements/alter-database-transact-sql-set-options.md#max_storage_size_mb) option.


## Data Flush Interval (minutes)

The Data Flush Interval defines the frequency before collected runtime statistics are persisted to disk. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the value is in minutes, but in [!INCLUDE[tsql](../../includes/tsql-md.md)] it's expressed in seconds. The default is 15 minutes (900 seconds).

- Increasing the data flush interval may reduce overall Query Store storage I/O impact, but cause the storage I/O workload to be more *spiky*, with fewer but heavier impacts to disk utilization.  Consider using a higher value if your workload doesn't generate a large number of different queries and plans, or if you can withstand longer time to persist data before a database shutdown. 
- Decreasing the data flush interval decreases the amount of Query Store data that would be lost in the event of a shutdown, power loss, or failover. It may also smoothen the storage I/O impact from Query Store by writing to disk more often, but with less data.

> [!NOTE]  
> Using trace flag 7745 prevents Query Store data from being written to disk in case of a failover or shutdown command. For more information, see [Use Query Store in mission-critical servers](best-practice-with-the-query-store.md#Recovery).

## Modify Query Store defaults

Configure Query Store based on your workload and performance troubleshooting requirements. The default parameters are good enough to start, but you should monitor how Query Store behaves over time and adjust its configuration accordingly.

### View Query Store current settings

View the current Query Store settings in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) or T-SQL.

### [SSMS](#tab/ssms)

:::image type="icon" source="../../includes/media/download.svg" border="false":::**[Use the latest version of SQL Server Management Studio (SSMS)](https://aka.ms/ssms)**

To view current settings in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]:

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, right-click on the database.
1. Select **Properties**.
1. Select **Query Store**.

### [T-SQL](#tab/tsql)

Execute the following T-SQL script to get the latest information about Query Store size:

```sql
USE [QueryStoreDB];
GO

SELECT actual_state_desc, desired_state_desc, current_storage_size_mb,
 max_storage_size_mb, readonly_reason
FROM sys.database_query_store_options;
```

---

The following script sets a new value for **Max Size (MB)**:

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (MAX_STORAGE_SIZE_MB = 1024);
```

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

**Size Based Cleanup Mode**: Specifies whether automatic data cleanup takes place when Query Store data size approaches the limit. Activate size-based cleanup to make sure that Query Store always runs in read-write mode and collects the latest data.  Note that there's no guarantee under heavy workloads that Query Store cleanup will consistently maintain the data size under the limit. It's possible for the automatic data cleanup to fall behind and to switch (temporarily) into read-only mode.

```sql
ALTER DATABASE [QueryStoreDB]
SET QUERY_STORE (SIZE_BASED_CLEANUP_MODE = AUTO);
```

**Query Store Capture Mode**: Specifies the query capture policy for Query Store.

- **All**: Captures all queries. This option is the default in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].
- **Auto**: Infrequent queries and queries with insignificant compile and execution duration are ignored. Thresholds for execution count, compile, and runtime duration are internally determined. Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], this is the default option.
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

The following example sets QUERY_CAPTURE_MODE to AUTO and sets other recommended options in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]:

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

The following example sets the CUSTOM capture policy to the [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] defaults, instead of the new default AUTO capture mode. For more information on custom capture policy options and defaults, see [<query_capture_policy_option_list>](../../t-sql/statements/alter-database-transact-sql-set-options.md#query_capture_policy_option_list--).

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

## <a id="Scenarios"></a> <a id="OptionMgmt"></a> Query Store maintenance

This section provides some guidelines on managing Query Store feature itself.

#### Query Store state

Query Store stores its data inside the user database and that is why it has size limit (configured with `MAX_STORAGE_SIZE_MB`). If data in Query Store hits that limit Query Store will automatically change state from read-write to read-only and stop collecting new data.

Query [sys.database_query_store_options](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md) to determine if Query Store is currently active, and whether it is currently collects runtime stats or not.

```sql
SELECT actual_state, actual_state_desc, readonly_reason,
    current_storage_size_mb, max_storage_size_mb
FROM sys.database_query_store_options;
```

Query Store status is determined by the `actual_state` column. If it's different than the desired status, the `readonly_reason` column can give you more information. When Query Store size exceeds the quota, the feature will switch to read_only mode and provide a reason. For information on reasons, see [sys.database_query_store_options](../system-catalog-views/sys-database-query-store-options-transact-sql.md).

#### Get Query Store options

To find out detailed information about Query Store status, execute following in a user database.

```sql
SELECT * FROM sys.database_query_store_options;
```

#### Set the Query Store interval

You can override interval for aggregating query runtime statistics (default is 60 minutes). New value for interval is exposed through `sys.database_query_store_options` view.

```sql
ALTER DATABASE <database_name>
SET QUERY_STORE (INTERVAL_LENGTH_MINUTES = 15);
```

Arbitrary values are not allowed for `INTERVAL_LENGTH_MINUTES`. Use one of the following: 1, 5, 10, 15, 30, 60, or 1440 minutes.

> [!NOTE]  
> For Azure Synapse Analytics, customizing Query Store configuration options, as demonstrated in this section, is not supported.

#### Query Store space usage

To check current the Query Store size and limit execute the following statement in the user database.

```sql
SELECT current_storage_size_mb, max_storage_size_mb
FROM sys.database_query_store_options;
```

If the Query Store storage is full use the following statement to extend the storage.

```sql
ALTER DATABASE <database_name>
SET QUERY_STORE (MAX_STORAGE_SIZE_MB = <new_size>);
```

#### Set Query Store options

You can set multiple Query Store options at once with a single ALTER DATABASE statement.

```sql
ALTER DATABASE <database name>
SET QUERY_STORE (
    OPERATION_MODE = READ_WRITE,
    CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30),
    DATA_FLUSH_INTERVAL_SECONDS = 3000,
    MAX_STORAGE_SIZE_MB = 500,
    INTERVAL_LENGTH_MINUTES = 15,
    SIZE_BASED_CLEANUP_MODE = AUTO,
    QUERY_CAPTURE_MODE = AUTO,
    MAX_PLANS_PER_QUERY = 1000,
    WAIT_STATS_CAPTURE_MODE = ON
);
```

For the full list of configuration options, see [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md).

#### Clean up the space

Query Store internal tables are created in the PRIMARY filegroup during database creation and that configuration cannot be changed later. If you are running out of space, you might want to clear older Query Store data by using the following statement.

```sql
ALTER DATABASE <db_name> SET QUERY_STORE CLEAR;
```

Alternatively, you might want to clear up only ad-hoc query data, since it is less relevant for query optimizations and plan analysis but takes up just as much space.

In Azure Synapse Analytics, clearing the query store is not available. Data is automatically retained for the past seven days.

#### Delete ad-hoc queries

This purges adhoc and internal queries from the Query Store so that the Query Store does not run out of space and remove queries we really need to track.

```sql
SET NOCOUNT ON
-- This purges adhoc and internal queries from
-- the Query Store in the current database
-- so that the Query Store does not run out of space
-- and remove queries we really need to track

DECLARE @id int;
DECLARE adhoc_queries_cursor CURSOR
FOR
    SELECT q.query_id
    FROM sys.query_store_query_text AS qt
    JOIN sys.query_store_query AS q
    ON q.query_text_id = qt.query_text_id
    JOIN sys.query_store_plan AS p
    ON p.query_id = q.query_id
    JOIN sys.query_store_runtime_stats AS rs
    ON rs.plan_id = p.plan_id
    WHERE q.is_internal_query = 1  -- is it an internal query then we dont care to keep track of it
       OR q.object_id = 0 -- if it does not have a valid object_id then it is an adhoc query and we don't care about keeping track of it
    GROUP BY q.query_id
    HAVING MAX(rs.last_execution_time) < DATEADD (minute, -5, GETUTCDATE())  -- if it has been more than 5 minutes since the adhoc query ran
    ORDER BY q.query_id;
OPEN adhoc_queries_cursor ;
FETCH NEXT FROM adhoc_queries_cursor INTO @id;
WHILE @@fetch_status = 0
BEGIN
    PRINT 'EXEC sp_query_store_remove_query ' + str(@id);
    EXEC sp_query_store_remove_query @id;
    FETCH NEXT FROM adhoc_queries_cursor INTO @id;
END
CLOSE adhoc_queries_cursor;
DEALLOCATE adhoc_queries_cursor;
```

You can define your own procedure with different logic for clearing up data you no longer want.

The example above uses the `sp_query_store_remove_query` extended stored procedure for removing unnecessary data. You can also:

- Use `sp_query_store_reset_exec_stats` to clear runtime statistics for a given plan.
- Use `sp_query_store_remove_plan` to remove a single plan.

## See also

- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [Query Store catalog views (Transact-SQL)](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)
- [Use Query Store with In-Memory OLTP](../../relational-databases/performance/using-the-query-store-with-in-memory-oltp.md)
- [Query processing architecture guide](../../relational-databases/query-processing-architecture-guide.md)
- [Query Store Hints](query-store-hints.md)

## Next steps

- [Monitoring performance by using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [Tuning performance by using the Query Store](tune-performance-with-the-query-store.md)
- [Historical query storage and analysis in Azure Synapse Analytics](/azure/synapse-analytics/sql/query-history-storage-analysis)
