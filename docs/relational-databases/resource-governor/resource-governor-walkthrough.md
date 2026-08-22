---
title: Create and Validate Resource Governor Configuration
description: Use examples to learn how to create and validate resource governor configuration.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 06/12/2026
ms.service: sql
ms.subservice: performance
ms.topic: tutorial
helpviewer_keywords:
  - "Resource Governor, classifier function create"
  - "classifier function [SQL Server], test"
  - "classifier function [SQL Server], create"
  - "Resource Governor, classifier function test"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Tutorial: Resource governor configuration examples and best practices

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article contains walkthrough examples to help you configure resource governor and validate that your configuration works as expected. It starts with a simple example and progresses to the more complex ones.

The article also includes examples of resource governor [monitoring queries](#monitor-resource-governor-using-system-views) and a list of resource governor [best practices](#resource-governor-best-practices).

All examples assume that initially, resource governor is disabled and uses default settings, and that no user-defined resource pools, workload groups, and classifier functions exist.

> [!NOTE]
> To modify resource governor configuration in [!INCLUDE[ssazuremi-md.md](../../includes/ssazuremi-md.md)], you must be in the context of the `master` database on the primary replica.

## Modify the default group

This example uses resource governor to limit the maximum size of a memory grant for all user queries. This is done by reducing the `REQUEST_MAX_MEMORY_GRANT_PERCENT` setting for the `default` workload group from the default 25% to 10%. The example does not use a [classifier function](resource-governor-classifier-function.md). This means that login processing is not affected and all user sessions continue to be classified in the `default` workload group.

You might need to limit the size of memory grants if queries are waiting for memory because other queries reserved too much memory. For more information, see [Troubleshoot slow performance or low memory issues caused by memory grants in SQL Server](/troubleshoot/sql/database-engine/performance/troubleshoot-memory-grant-issues).

1. Modify the default workload group.
    ```sql
    ALTER WORKLOAD GROUP [default] WITH (REQUEST_MAX_MEMORY_GRANT_PERCENT = 10);
    ```
1. Enable resource governor to make our configuration effective.

    ```sql
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```

1. Validate the new setting, including the new maximum size of a memory grant.
    ```sql
    SELECT group_id,
           wg.name AS workload_group_name,
           rp.name AS resource_pool_name,
           wg.request_max_memory_grant_percent_numeric AS request_max_memory_grant_percent,
           rp.max_memory_kb * wg.request_max_memory_grant_percent_numeric AS request_max_memory_grant_size_kb
    FROM sys.resource_governor_workload_groups AS wg
    INNER JOIN sys.dm_resource_governor_resource_pools AS rp
    ON wg.pool_id = rp.pool_id;
    ```
1. To revert to the initial configuration, execute the following script:
    ```sql
    ALTER WORKLOAD GROUP [default] WITH (REQUEST_MAX_MEMORY_GRANT_PERCENT = 25);
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ALTER RESOURCE GOVERNOR DISABLE;
    ```

## Use a user-defined workload group

This example uses resource governor to ensure that all requests on sessions with a specific application name don't execute with the degree of parallelism (DOP) higher than four. This is done by classifying sessions into a workload group with the `MAX_DOP` setting set to 4.

For more information about configuring the maximum degree of parallelism, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

1. Create a workload group that limits DOP. The group uses the `default` resource pool because we only want to limit DOP for a specific application, but not reserve or limit CPU, memory, or I/O resources.

    ```sql
    CREATE WORKLOAD GROUP limit_dop
    WITH (
         MAX_DOP = 4
         )
    USING [default];
    ```

1. Create the classifier function. The function uses the built-in [APP_NAME()](../../t-sql/functions/app-name-transact-sql.md) function to determine the application name specified in the client connection string. If the application name is set to `limited_dop_application`, the function returns the name of the workload group that limits DOP. Otherwise, the function returns `default` as the workload group name.
    ```sql
    USE master;
    GO

    CREATE FUNCTION dbo.rg_classifier()
    RETURNS sysname
    WITH SCHEMABINDING
    AS
    BEGIN

    DECLARE @WorkloadGroupName sysname = N'default';

    IF APP_NAME() = N'limited_dop_application'
        SELECT @WorkloadGroupName = N'limit_dop';

    RETURN @WorkloadGroupName;

    END;
    GO
    ```

1. Modify resource governor configuration to make our configuration effective, and enable resource governor.
    ```sql
    ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = dbo.rg_classifier);
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```

1. <a id="ValidateRg"></a> Query [sys.resource_governor_configuration](../system-catalog-views/sys-resource-governor-configuration-transact-sql.md) to validate that resource governor is enabled and is using the classifier function we created.

    ```sql
    SELECT OBJECT_SCHEMA_NAME(classifier_function_id) AS classifier_schema_name,
           OBJECT_NAME(classifier_function_id) AS classifier_object_name,
           is_enabled
    FROM sys.resource_governor_configuration;
    ```

    ```output
    classifier_schema_name      classifier_object_name      is_enabled
    ----------------------      ----------------------      ----------
    dbo                         rg_classifier               1
    ```

1. Validate that sessions with a specific application name are classified into the `limit_dop` workload group, while other sessions continue to be classified in the `default` workload group. We'll use a query that uses [sys.dm_exec_sessions](../system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md) and [sys.resource_governor_workload_groups](../system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md) system views to return the application name and workload group name for the current session.
    1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS), select **File** on the main menu, **New**, **Database Engine Query**.
    1. In the **Connect to Database Engine** dialog, specify the same [!INCLUDE[ssde-md](../../includes/ssde-md.md)] instance where you created the workload group and the classifier function. Select the **Additional Connection Parameters** tab, and enter `App=limited_dop_application`. This makes SSMS use `limited_dop_application` as the application name when connecting to the instance.
    1. Select **Connect** to open a new connection.
    1. In the same query window, execute the following query:
        ```sql
        SELECT s.program_name AS application_name,
               wg.name AS workload_group_name,
               wg.max_dop
        FROM sys.dm_exec_sessions AS s
        INNER JOIN sys.resource_governor_workload_groups AS wg
        ON s.group_id = wg.group_id
        WHERE s.session_id = @@SPID;
        ```

        You should see the following output, showing that the session was classified into the `limit_dop` workload group with the maximum DOP set to four:
        ```output
        application_name            workload_group_name     max_dop
        ----------------            -------------------     -------
        limited_dop_application     limit_dop               4
        ```

    1. Repeat the above steps, but don't enter anything in the box on the **Additional Connection Parameters** tab. The output changes, showing the default SSMS application name and the `default` workload group with the default `0` value for maximum DOP.

        ```output
        application_name                                    workload_group_name     max_dop
        ----------------                                    -------------------     -------
        Microsoft SQL Server Management Studio - Query      default                 0
        ```

1. To revert to the initial configuration of this example, disconnect all sessions using the `limit_dop` workload group, and execute the following T-SQL script. The script includes the following steps:
   1. Disable resource governor so that the classifier function can be dropped.
   1. Drop the workload group. This requires that no sessions are using this workload group.
   1. Reconfigure resource governor to reload the effective configuration without the classifier function and the workload group. This enables resource governor.
   1. Disable resource governor to revert to the initial configuration.
   
    ```sql
    /* Disable resource governor so that the classifier function can be dropped. */
    ALTER RESOURCE GOVERNOR DISABLE;
    ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = NULL);
    DROP FUNCTION IF EXISTS dbo.rg_classifier;

    /* Drop the workload group. This requires that no sessions are using this workload group. */
    DROP WORKLOAD GROUP limit_dop;

    /* Reconfigure resource governor to reload the effective configuration without the classifier function and the workload group. This enables resource governor. */
    ALTER RESOURCE GOVERNOR RECONFIGURE;

    /* Disable resource governor to revert to the initial configuration. */
    ALTER RESOURCE GOVERNOR DISABLE;
    ```

## Use multiple resource pools and workload groups

This example uses resource governor to classify sessions from an order processing application into different workload groups and resource pools depending on the time of day. This configuration allocates more resources to the application during peak processing times, and limits its resources during off hours. The example assumes that the application doesn't use long-running sessions.

1. Create two resource pools for peak hours and off hours processing.

    - The `peak_hours_pool` pool guarantees (reserves) a minimum of 20% of average CPU bandwidth via `MIN_CPU_PERCENT`, and doesn't limit CPU bandwidth by setting `MAX_CPU_PERCENT` to `100`.
    - The `off_hours_pool` pool doesn't reserve any CPU bandwidth by setting `MIN_CPU_PERCENT` to `0`, but limits CPU bandwidth to 50% when CPU contention is present by setting `MAX_CPU_PERCENT` to `50`.

    ```sql
    CREATE RESOURCE POOL peak_hours_pool
    WITH (
         MIN_CPU_PERCENT = 20,
         MAX_CPU_PERCENT = 100
         );

    CREATE RESOURCE POOL off_hours_pool
    WITH (
         MIN_CPU_PERCENT = 0,
         MAX_CPU_PERCENT = 50
         );
    ```

    Resource pools can reserve and limit system resources such as CPU, memory, and I/O. For more information, see [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md).

1. Create two workload groups, one for each resource pool respectively.

    - The `peak_hours_group` doesn't limit the number of concurrent requests by setting `GROUP_MAX_REQUESTS` to the default value of `0`.
    - The `off_hours_group` limits the number of concurrent requests across all sessions classified into this group, by setting `GROUP_MAX_REQUESTS` to `200`.

    ```sql
    CREATE WORKLOAD GROUP peak_hours_group
    WITH (
         GROUP_MAX_REQUESTS = 0
         )
    USING peak_hours_pool;

    CREATE WORKLOAD GROUP off_hours_group
    WITH (
         GROUP_MAX_REQUESTS = 200
         )
    USING off_hours_pool;
    ```

    Workload groups define policies such as the maximum number of requests, the maximum degree of parallelism, and the maximum memory grant size. For more information, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).

1. Create and populate a table that defines the peak and off hours time intervals.
    
    - Each row in the table defines the start and end time of the interval, and the name of the workload group to use during the interval.
    - The start and end time of each interval is inclusive.
    - The table is created in the `master` database so that it can be used in a schema-bound classifier function.

    ```sql
    USE master;
    GO

    CREATE TABLE dbo.workload_interval
    (
    workload_group_name sysname NOT NULL,
    start_time time(7) NOT NULL,
    end_time time(7) NOT NULL,
    CONSTRAINT pk_workload_interval PRIMARY KEY (start_time, workload_group_name),
    CONSTRAINT ak_workload_interval_1 UNIQUE (end_time, workload_group_name),
    CONSTRAINT ck_workload_interval_1 CHECK (start_time < end_time)
    );
    GO

    INSERT INTO dbo.workload_interval
    VALUES (N'off_hours_group', '00:00', '06:29:59.9999999'),
           (N'peak_hours_group', '06:30', '18:29:59.9999999'),
           (N'off_hours_group', '18:30', '23:59:59.9999999');
    ```

1. Create the classifier function.

    - The data in the table is expected to have a single matching row for any given time of day. If the data violates that rule, the function returns `default` as the workload group name.
    - The following example function also returns `default` if the application name returned by the built-in [APP_NAME()](../../t-sql/functions/app-name-transact-sql.md) function is anything other than `order_processing`.
    
    ```sql
    USE master;
    GO

    CREATE OR ALTER FUNCTION dbo.rg_classifier()
    RETURNS sysname
    WITH SCHEMABINDING
    AS
    BEGIN

    DECLARE @WorkloadGroupName sysname = N'default';

    SELECT @WorkloadGroupName = workload_group_name
    FROM dbo.workload_interval
    WHERE APP_NAME() = N'order_processing'
          AND
          CAST(GETDATE() AS time(7)) BETWEEN start_time AND end_time;

    IF @@ROWCOUNT > 1
        SELECT @WorkloadGroupName = N'default';

    RETURN @WorkloadGroupName;

    END;
    GO
    ```

1. <a id="TableValuedConstructorInClassifier"></a>This is an optional step. Instead of creating a table in the `master` database, you can use a [table-valued constructor](../../t-sql/queries/table-value-constructor-transact-sql.md) to define the time intervals directly in the classifier function. This is the recommended approach when data size is small and the classifier function criteria isn't changed frequently. Here is an example of the same classifier that uses a table-valued constructor instead of a table in `master`.

    ```sql
    USE master;
    GO

    CREATE OR ALTER FUNCTION dbo.rg_classifier()
    RETURNS sysname
    WITH SCHEMABINDING
    AS
    BEGIN

    DECLARE @WorkloadGroupName sysname = N'default';

    SELECT @WorkloadGroupName = workload_group_name
    FROM (
         VALUES (CAST(N'off_hours_group' AS sysname),  CAST('00:00' AS time(7)), CAST('06:29:59.9999999' AS time(7))),
                (CAST(N'peak_hours_group' AS sysname), CAST('06:30' AS time(7)), CAST('18:29:59.9999999' AS time(7))),
                (CAST(N'off_hours_group' AS sysname),  CAST('18:30' AS time(7)), CAST('23:59:59.9999999'AS time(7)))
         ) AS wg (workload_group_name, start_time, end_time)
    WHERE APP_NAME() = N'order_processing'
          AND
          CAST(GETDATE() AS time(7)) BETWEEN start_time AND end_time;

    IF @@ROWCOUNT > 1
        SELECT @WorkloadGroupName = N'default';

    RETURN @WorkloadGroupName;

    END;
    GO
    ```

1. Modify resource governor configuration to make our configuration effective, and enable resource governor.

    ```sql
    ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = dbo.rg_classifier);
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```

1. Validate that resource governor is enabled, is using the specified classifier function, and that the classifier function works as expected using similar steps as in the [previous example](#ValidateRg). This time, we enter `App=order_processing` on the **Additional Connection Parameters** tab in the SSMS connect dialog to match the application name in the classifier function. Execute the following query to determine the application name, workload group, resource pool, and the CPU reservation and limit for the current session:

    ```sql
    SELECT s.program_name AS application_name,
           wg.name AS workload_group_name,
           wg.group_max_requests,
           rp.name AS resource_pool_name,
           rp.min_cpu_percent,
           rp.max_cpu_percent
    FROM sys.dm_exec_sessions AS s
    INNER JOIN sys.resource_governor_workload_groups AS wg
    ON s.group_id = wg.group_id
    INNER JOIN sys.resource_governor_resource_pools AS rp
    ON wg.pool_id = rp.pool_id
    WHERE s.session_id = @@SPID;
    ```

    The results depend on the time of day. For example, if the current time is 14:30, the result shows that `peak_hours_group` and `peak_hours_pool` are used:

    ```output
    application_name    workload_group_name     group_max_requests      resource_pool_name      min_cpu_percent     max_cpu_percent
    -----------------   --------------------    ------------------      -------------------     ---------------     ---------------
    order_processing    peak_hours_group        0                       peak_hours_pool         20                  100
    ```

1. To revert to the initial configuration of this example, disconnect all sessions using the `peak_hours_group` and `off_hours_group` workload groups, and execute the following T-SQL script. The script includes the following steps:
   1. Disable resource governor so that the classifier function can be dropped.
   1. Drop the workload groups. This requires that no sessions are using these workload groups.
   1. Once the workload groups are dropped, drop the resource pools.
   1. Reconfigure resource governor to reload the effective configuration without the classifier function and user-defined workload groups and resource pools. This enables resource governor.
   1. Disable resource governor to revert to the initial configuration.
   
    ```sql
    /* Disable resource governor so that the classifier function can be dropped. */
    ALTER RESOURCE GOVERNOR DISABLE;
    ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = NULL);
    DROP FUNCTION IF EXISTS dbo.rg_classifier;
    DROP TABLE IF EXISTS dbo.workload_interval;

    /* Drop the workload groups. This requires that no sessions are using these workload groups. */
    DROP WORKLOAD GROUP peak_hours_group;
    DROP WORKLOAD GROUP off_hours_group;

    /* Once the workload groups are dropped, drop the resource pools. */
    DROP RESOURCE POOL peak_hours_pool;
    DROP RESOURCE POOL off_hours_pool;

    /* Reconfigure resource governor to reload the effective configuration without the classifier function and user-defined workload groups and resource pools. This enables resource governor. */
    ALTER RESOURCE GOVERNOR RECONFIGURE;

    /* Disable resource governor to revert to the initial configuration. */
    ALTER RESOURCE GOVERNOR DISABLE;
    ```

## Monitor resource governor using system views

Example queries in this section show how you can monitor resource governor runtime statistics and behavior.

Resource governor statistics are cumulative since the last server restart. If you need to collect statistics starting from a certain time, you can reset statistics using the `ALTER RESOURCE GOVERNOR RESET STATISTICS` statement.

### Resource pool runtime statistics

For each resource pool, resource governor tracks CPU and memory utilization, out-of-memory events, memory grants, I/O, and other statistics. For more information, see [sys.dm_resource_governor_resource_pools](../system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md).

The following query returns a subset of available statistics for all resource pools:

```sql
SELECT rp.pool_id,
       rp.name AS resource_pool_name,
       wg.workload_group_count,
       rp.statistics_start_time,
       rp.total_cpu_usage_ms,
       rp.target_memory_kb,
       rp.used_memory_kb,
       rp.out_of_memory_count,
       rp.active_memgrant_count,
       rp.total_memgrant_count,
       rp.total_memgrant_timeout_count,
       rp.read_io_completed_total,
       rp.write_io_completed_total,
       rp.read_bytes_total,
       rp.write_bytes_total,
       rp.read_io_stall_total_ms,
       rp.write_io_stall_total_ms
FROM sys.dm_resource_governor_resource_pools AS rp
OUTER APPLY (
            SELECT COUNT(1) AS workload_group_count
            FROM sys.dm_resource_governor_workload_groups AS wg
            WHERE wg.pool_id = rp.pool_id
            ) AS wg;
```

### Workload group runtime statistics

For each workload group, resource governor tracks CPU time, the number of requests, blocked tasks, lock wait time, query optimizations, and other statistics. For more information, see [sys.resource_governor_workload_groups](../system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md).

The following query returns a subset of available statistics for all workload groups:

```sql
SELECT wg.name AS workload_group_name,
       rp.name AS resource_pool_name,
       wg.statistics_start_time,
       wg.total_request_count,
       wg.total_cpu_usage_ms,
       wg.blocked_task_count,
       wg.total_lock_wait_time_ms,
       wg.total_query_optimization_count,
       wg.max_request_grant_memory_kb,
       wg.active_parallel_thread_count,
       wg.effective_max_dop,
       wg.request_max_memory_grant_percent_numeric
FROM sys.dm_resource_governor_workload_groups AS wg
INNER JOIN sys.dm_resource_governor_resource_pools AS rp
ON wg.pool_id = rp.pool_id
```

### Aggregate sessions by workload group and session attributes

The following query returns a distribution of sessions across workload groups and aggregate session statistics for each workload group.

A high number of sessions with the `preconnect` status might indicate slowness in the classifier execution.

```sql
SELECT wg.name AS workload_group_name,
       rp.name AS resource_pool_name,
       s.program_name AS application_name,
       s.login_name,
       s.host_name,
       s.status,
       d.name AS database_name,
       MIN(s.login_time) AS first_login_time,
       MAX(s.login_time) AS last_login_time,
       MAX(s.last_request_start_time) AS last_request_start_time,
       COUNT(1) AS session_count
FROM sys.dm_exec_sessions AS s
INNER JOIN sys.dm_resource_governor_workload_groups AS wg
ON s.group_id = wg.group_id
INNER JOIN sys.dm_resource_governor_resource_pools AS rp
ON wg.pool_id = rp.pool_id
INNER JOIN sys.databases AS d
ON s.database_id = d.database_id
GROUP BY wg.name,
         rp.name,
         s.program_name,
         s.login_name,
         s.host_name,
         s.status,
         d.name;
```

### Aggregate requests by workload group and request attributes

The following query returns a distribution of requests across workload groups and aggregate request statistics for each workload group:

```sql
SELECT wg.name AS workload_group_name,
       rp.name AS resource_pool_name,
       r.command,
       r.status,
       d.name AS database_name,
       COUNT(1) AS request_count,
       MIN(r.start_time) AS first_request_start_time,
       MAX(r.start_time) AS last_request_start_time,
       SUM(CAST(r.total_elapsed_time AS bigint)) AS total_elapsed_time_ms
FROM sys.dm_exec_requests AS r
INNER JOIN sys.dm_resource_governor_workload_groups AS wg
ON r.group_id = wg.group_id
INNER JOIN sys.dm_resource_governor_resource_pools AS rp
ON wg.pool_id = rp.pool_id
INNER JOIN sys.databases AS d
ON r.database_id = d.database_id
GROUP BY wg.name,
         rp.name,
         r.command,
         r.status,
         d.name;
```

## Resource governor best practices

- Configure Dedicated Administrator Connection (DAC), and learn how to use it. For more information, see [Diagnostic connection for database administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md). If your resource governor configuration malfunctions, you can use DAC to troubleshoot it or to [disable resource governor](disable-resource-governor.md).
- When configuring resource pools, be careful specifying large values for `MIN_CPU_PERCENT`, `MIN_MEMORY_PERCENT`, and `MIN_IOPS_PER_VOLUME`. A `MIN` configuration setting reserves resources for a resource pool and makes them unavailable to other resource pools, including the `default` pool. For more information, see [Create a resource pool](create-a-resource-pool.md).
- Similarly, be careful specifying very small values for `MAX_CPU_PERCENT` and `CAP_CPU_PERCENT`, or setting `MAX_IOPS_PER_VOLUME` to a much smaller value than the I/O subsystem can provide. A strongly constrained workload that uses shared resources can affect other workloads using the same resources. For example, a CPU-constrained query can hold locks for longer and cause long blocking chains. An I/O-constrained workload can hold latches on shared [system pages](../pages-and-extents-architecture-guide.md#system-pages) for longer, resulting in latch timeouts for other workloads.
- The classifier function extends login processing time. Avoid complex logic and long-running or resource-intensive queries in the classifier, particularly if queries use large tables. An overly complex function can cause login delays or connection timeouts.
- If you need to use a table in the classifier, and the table is small and mostly static, consider using a [table-valued constructor](../../t-sql/queries/table-value-constructor-transact-sql.md) instead, as shown in an [example](#TableValuedConstructorInClassifier) earlier in this article.
- Avoid using a frequently modified table in the classifier. That increases the risk of blocking that can delay logins and cause connection timeouts. The following workarounds can mitigate the risk, however they have downsides, including the risk of incorrect classification:
    - Consider using the `NOLOCK` table hint, or the equivalent `READUNCOMMITTED` hint. For more information, see [READUNCOMMITTED](../../t-sql/queries/hints-transact-sql-table.md#readuncommitted).
    - Consider using the `LOCK_TIMEOUT` setting at the start of the classifier function, setting it to a low value such as 1,000 milliseconds. For more information, see [SET LOCK_TIMEOUT](../../t-sql/statements/set-lock-timeout-transact-sql.md).
- If you execute `ALTER RESOURCE GOVERNOR RECONFIGURE` after deleting an unused resource pool on a machine with a large amount of memory, the command might take a long time. If a classifier function is in effect before reconfiguration starts, new connection attempts made during that time might time out.

    If you delete an unused resource pool, we recommend that you reconfigure resource governor during a maintenance window or a period of low activity.
- You can't modify a classifier function while it is referenced in the resource governor configuration. However, you can modify the configuration to use a different classifier function. If you want to make changes to the classifier, consider creating a pair of classifier functions. For example, you might create `dbo.rg_classifier_A()` and `dbo.rg_classifier_B()`. When a change to the classifier logic is needed, follow these steps:
    1. Use the [ALTER FUNCTION](../../t-sql/statements/alter-function-transact-sql.md) statement to make the changes in the function *not* currently used in the resource governor configuration.
    1. Use the [ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md) statement to make the modified classifier active, and then reconfigure resource governor. For example:
        ```sql
        ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = dbo.rg_classifier_B);
        ALTER RESOURCE GOVERNOR RECONFIGURE;
        ```
    1. If a change is needed again, follow the same steps using the other function (`dbo.rg_classifier_A()`).
- Resource governor configuration is stored in the `master` database. Make sure to periodically back up `master`, and know how to restore it. For more information, see [Back up and restore: System databases](../backup-restore/back-up-and-restore-of-system-databases-sql-server.md). Because there are limitations around restoring `master`, we recommend that you also save a copy of resource governor configuration scripts separately. You can recreate resource governor configuration from scripts if the `master` database needs to be rebuilt.

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Configure resource governor using a template](configure-resource-governor-using-a-template.md)
- [View and modify resource governor properties](view-resource-governor-properties.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [CREATE RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-resource-pool-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/create-workload-group-transact-sql.md)
- [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md)
