---
title: Troubleshoot change tracking auto cleanup issues
description: Learn how to troubleshoot common issues with auto cleanup on change tracking for SQL Server, Azure SQL Managed Instance, and Azure SQL Database.
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest
ms.date: 01/03/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "change tracking, cleanup"
  - "change tracking, troubleshooting"
  - "change tracking, troubleshoot"
---

# Troubleshoot change tracking auto cleanup issues

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides ways to troubleshoot common issues observed in change tracking auto cleanup.

## Symptoms

Generally, if the auto cleanup isn't functioning as expected, you can see one or more of the following symptoms:

- High storage consumption by one or more change tracking side tables or the `syscommittab` system table.
- Side tables (internal tables whose name begins with prefix `change_tracking`, for example, `change_tracking_12345`) or `syscommittab` or both, show significant number of rows that are outside of the configured retention period.
- `dbo.MSChange_tracking_history` table has entries with specific cleanup errors.
- `CHANGETABLE` performance has degraded over time.
- Auto cleanup or manual cleanup reports high CPU usage.

## Debugging and mitigation

To identify the root cause of a problem with change tracking auto cleanup, use the following steps to debug and mitigate the issue.

### Auto cleanup status

Check if auto cleanup has been running. To check this, query the cleanup history table in the same database. If the cleanup has been running, the table has entries with the start and end times of the cleanup. If the cleanup hasn't been running, the table is empty or has stale entries. If the history table has entries with the tag `cleanup errors` in the column `comments`, then the cleanup is failing due to table level cleanup errors.

```sql
SELECT TOP 1000 * FROM dbo.MSChange_tracking_history ORDER BY start_time DESC;
```

Auto cleanup runs periodically with a default interval of 30 minutes. If the history table doesn't exist, most likely, auto cleanup has never run. Otherwise, check the `start_time` and `end_time` column values. If the latest entries aren't recent, that is, they're hours or days old, then auto cleanup might not be running. If that's the case, use the following steps to troubleshoot.

#### 1. Cleanup is turned off

Check if auto cleanup is turned on for the database. If it isn't, turn it on and wait for at least 30 minutes before looking at the history table for new entries. Monitor the progress in the history table thereafter.

```sql
SELECT * FROM sys.change_tracking_databases WHERE database_id=DB_ID('<database_name>')
```

A nonzero value in `is_auto_cleanup_on` indicates that auto cleanup is **enabled**. The retention period value controls the duration for which change tracking metadata is retained in the system. The default value for change tracking retention period is 2 days.

To enable or disable change tracking see [Enable and Disable Change Tracking (SQL Server)](enable-and-disable-change-tracking-sql-server.md).

#### 2. Cleanup is turned on but not running

If auto cleanup is on, the auto cleanup thread likely stopped due to unexpected errors. Currently, restarting the auto cleanup thread isn't feasible. You must initiate a failover to a secondary server (or restart the server in the absence of a secondary), and confirm that the auto cleanup setting is enabled for the database.

### Auto cleanup runs but isn't making progress

If one or more side tables show significant storage consumption, or contain a large number of records beyond configured retention, follow the steps in this section, which describe remedies for a single side table. The same steps can be repeated for more tables if necessary.

#### 1. Assess auto cleanup backlog

Identify side tables that have a large backlog of expired records, which need mitigation to be performed on them. Run the following queries to identify the side tables with large expired records counts. Remember to replace the values in the example scripts as shown.

1. Get the invalid cleanup version:

    ```sql
    SELECT * FROM sys.change_tracking_tables;
    ```

    The `cleanup_version` value from the returned rows represents the invalid cleanup version.

1. Run the following dynamic Transact-SQL (T-SQL) query, which generates the query to get the expired row count of side tables. Replace the value of `<invalid_version>` in the query with the value obtained in the previous step.

   ```sql
   SELECT 'SELECT ''' + QUOTENAME(name) + ''', count(*) FROM [sys].' + QUOTENAME(name)
       + ' WHERE sys_change_xdes_id IN (SELECT xdes_id FROM sys.syscommittab ssct WHERE ssct.commit_ts <= <invalid_version>) UNION'
   FROM sys.internal_tables
   WHERE internal_type = 209;
   ```

1. Copy the result set from the previous query, and remove the `UNION` keyword from last row. If you run the generated T-SQL query through a dedicated admin connection (DAC), the query gives the expired row counts of all side tables. Depending on the size of the `sys.syscommittab` table and the number of side tables, this query might take a long time to complete.

   > [!IMPORTANT]  
   > This step is necessary in order to move ahead with the mitigation steps. If the previous query fails to execute, identify the expired row counts for the individual side tables using the queries given next.

Perform the following mitigation steps for the side tables, having the decreasing order of expired row counts, until the expired row counts come down to a manageable state for the auto cleanup to catch up.

Once you identify the side tables with large expired record counts, gather information on the latency of the side table delete statements and the rate of deletion per second over the last few hours. Next, estimate the time required to clean up the side table by considering both the stale row count and the delete latency.

Use the following T-SQL code snippet by substituting parameter templates with appropriate values.

- Query the rate of cleanup per second:

  ```sql
  SELECT
      table_name,
      rows_cleaned_up / ISNULL(NULLIF(DATEDIFF(second, start_time, end_time), 0), 1),
      cleanup_version
  FROM dbo.MSChange_tracking_history
  WHERE table_name = '<table_name>'
  ORDER BY end_time DESC;
  ```

  You can also use minute or hour granularity for the `DATEDIFF` function.

- Find stale row count in the side table. This query helps you find the number of rows pending to be cleaned up.

  The `<internal_table_name>` and `<cleanup_version>` for the user table are in the output returned in the previous section. Using this information, execute the following T-SQL code through a dedicated admin connection (DAC):

  ```sql
  SELECT '<internal_table_name>',
      COUNT(*)
  FROM sys.<internal_table_name>
  WHERE sys_change_xdes_id IN (
          SELECT xdes_id
          FROM sys.syscommittab ssct
          WHERE ssct.commit_ts <= <cleanup_version>
  );
  ```

  This query can take some time to complete. In cases where the query times out, calculate stale rows by finding the difference between total rows and active rows that is, rows to be cleaned up.

- Find the total number of rows in the side table by executing the following query:

  ```sql
  SELECT sum(row_count) FROM sys.dm_db_partition_stats
  WHERE object_id = OBJECT_ID('sys.<internal_table_name>')
  GROUP BY partition_id;
  ```

- Find the number of active rows in the side table by executing the following query:

  ```sql
  SELECT '<internal_table_name>', COUNT(*) FROM sys.<internal_table_name> WHERE sys_change_xdes_id
  IN (SELECT xdes_id FROM sys.syscommittab ssct WHERE ssct.commit_ts > <cleanup_version>);
  ```

  You can calculate the estimated time to clean up the table using the rate of cleanup and stale row count. Consider the following formula:

  > **Time to clean up in minutes = (stale row count) / (rate of cleanup in minutes)**

  If the time to complete table cleanup is acceptable, monitor the progress and let the auto cleanup continue its work. If not, proceed with the next steps to drill down further.

#### 2. Check table lock conflicts

Determine if cleanup isn't progressing because of table lock escalation conflicts, which starve cleanup consistently from acquiring locks on the side table to delete rows.

To confirm a lock conflict, run the following T-SQL code. This query fetches records for the problematic table to determine if there are multiple entries indicating lock conflicts. A few sporadic conflicts spread over a period shouldn't qualify for the proceeding mitigation steps. The conflicts should be recurrent.

```sql
SELECT TOP 1000 *
FROM dbo.MSChange_tracking_history
WHERE table_name = '<user_table_name>'
ORDER BY start_time DESC;
```

If the history table has multiple entries in the `comments` columns with the value `Cleanup error: Lock request time out period exceeded`, it's a clear indication that multiple cleanup attempts failed due to lock conflicts or lock timeouts in succession. Consider the following remedies:

- Disable and enable change tracking on the problematic table. This causes all tracking metadata maintained for the table to be purged. The table's data remains intact. This is the quickest remedy.

- If the previous option isn't possible, then proceed to execute manual cleanup on the table by enabling **trace flag 8284** as follows:

    ```sql
    DBCC TRACEON (8284, -1);
    GO
    EXEC [sys].[sp_flush_CT_internal_table_on_demand] @TableToClean = '<table_name>';
    ```

#### 3. Check other causes

Another possible cause of cleanup lagging is the slowness of the delete statements. To determine if so, check the value of `hardened_cleanup_version`. This value can be retrieved through a dedicated admin connection (DAC) to the database under consideration.

Find the hardened cleanup version by executing the following query:

```sql
SELECT * FROM sys.sysobjvalues WHERE valclass = 7 AND objid = 1004;
```

Find the cleanup version by executing the following query:

```sql
SELECT * FROM sys.sysobjvalues WHERE valclass = 7 AND objid = 1003;
```

If `hardened_cleanup_version` and `cleanup_version` values are equal, then skip this section and proceed to the next section.

If both values are different, it means one or more side tables encountered errors. The fastest mitigation is to disable & enable change tracking on the problematic table. This causes all tracking metadata maintained for the table to be purged. The data in the table remains intact.

If the previous option isn't possible, then run manual cleanup on the table.

## Troubleshoot syscommittab issues

This section covers steps to debug and mitigate issues with the `syscommittab` system table, if it uses a lot of storage space, or if it has a large backlog of stale rows.

The `syscommittab` system table cleanup depends on side table cleanup. Only after all side tables are cleaned up, `syscommittab` can be purged. Make sure all steps in the [Auto cleanup runs but isn't making progress](#auto-cleanup-runs-but-isnt-making-progress) section are performed.

To explicitly invoke the `syscommittab` cleanup, use the [sys.sp_flush_commit_table_on_demand](../system-stored-procedures/sys-sp-flush-commit-table-on-demand-transact-sql.md) stored procedure.

> [!NOTE]  
> The `sys.sp_flush_commit_table_on_demand` stored procedure can take time if it's deleting a large backlog of rows.

As shown in the example section from the [sys.sp_flush_commit_table_on_demand](../system-stored-procedures/sys-sp-flush-commit-table-on-demand-transact-sql.md#examples) article, this stored procedure returns the value of `safe_cleanup_version()`, and the number of rows deleted. If the value returned appears to be `0`, and if snapshot isolation is turned on, the cleanup might not delete anything from `syscommittab`.

If the retention period is greater than one day, it should be safe to rerun the *`sys.sp_flush_commit_table_on_demand`* stored procedure after enabling Trace Flag 8239 globally. Using this trace flag when snapshot isolation is off is always safe but in some cases, it might not be necessary.

## High CPU utilization during cleanup

The issue described in this section might be seen on older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. If there are a large number of change-tracked tables in a database, and the auto cleanup or manual cleanup causes high CPU utilization. This issue can also be caused due to the history table, which was mentioned briefly in earlier sections.

Use the following T-SQL code to check the number of rows in the history table:

```sql
SELECT COUNT(*) from dbo.MSChange_tracking_history;
```

If the number of rows is sufficiently large, try adding the following index if it's absent. Use the following T-SQL code to add the index:

```sql
IF NOT EXISTS (
    SELECT *
    FROM sys.indexes
    WHERE name = 'IX_MSchange_tracking_history_start_time'
        AND object_id = OBJECT_ID('dbo.MSchange_tracking_history')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_MSchange_tracking_history_start_time
    ON dbo.MSchange_tracking_history (start_time)
END
```

## Run cleanup more frequently than 30 minutes

Specific tables can experience a high rate of changes, and you might find that the autocleanup job can't clean up the side tables and `syscommittab` within the 30-minute interval. If this occurs, you can run a manual cleanup job with increased frequency to facilitate the process.

For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [create a background job](../../ssms/agent/create-a-job.md) using `sp_flush_CT_internal_table_on_demand` with a shorter internal than the default 30 minutes. For [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [Azure Logic Apps](/azure/connectors/connectors-create-api-sqlazure) can be used to schedule these jobs.

The following T-SQL code can be used to create a job to help cleanup the side tables for change tracking:

```sql
-- Loop to invoke manual cleanup procedure for cleaning up change tracking tables in a database
-- Fetch the tables enabled for change tracking
SELECT IDENTITY(INT, 1, 1) AS TableID,
    (SCHEMA_NAME(tbl.Schema_ID) + '.' + OBJECT_NAME(ctt.object_id)) AS TableName
INTO #CT_Tables
FROM sys.change_tracking_tables ctt
INNER JOIN sys.tables tbl
    ON tbl.object_id = ctt.object_id;

-- Set up the variables
DECLARE @start INT = 1,
    @end INT = (
        SELECT COUNT(*)
        FROM #CT_Tables
        ),
    @tablename VARCHAR(255);

WHILE (@start <= @end)
BEGIN
    -- Fetch the table to be cleaned up
    SELECT @tablename = TableName
    FROM #CT_Tables
    WHERE TableID = @start

    -- Execute the manual cleanup stored procedure
    EXEC sp_flush_CT_internal_table_on_demand @tablename

    -- Increment the counter
    SET @start = @start + 1;
END

DROP TABLE #CT_Tables;
```

## Related content

- [About Change Tracking (SQL Server)](about-change-tracking-sql-server.md)
- [Change Tracking Functions (Transact-SQL)](../system-functions/change-tracking-functions-transact-sql.md)
- [Change Tracking stored procedures (Transact-SQL)](../system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)
- [Change Tracking tables (Transact-SQL)](../system-tables/change-tracking-tables-transact-sql.md)
- [Display data and log space information for a database](../databases/display-data-and-log-space-information-for-a-database.md)
- [Troubleshoot high-CPU-usage issues in SQL Server](/troubleshoot/sql/database-engine/performance/troubleshoot-high-cpu-usage-issues)
