---
title: Tempdb Space Resource Governance
description: Use Resource Governor to prevent runaway workloads from consuming a large amount of space in tempdb.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 06/02/2026
ms.service: sql
ms.topic: concept-article
ms.custom:
  - build-2025
monikerRange: ">= sql-server-ver17 || >= sql-server-linux-ver17"
---

# Tempdb space resource governance

[!INCLUDE [sqlserver2025-and-later](../../includes/applies-to-version/sqlserver2025-and-later.md)]

When you enable `tempdb` space resource governance, you improve reliability and avoid outages by preventing runaway queries or workloads from consuming a large amount of space in `tempdb`.

Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], you can use the [resource governor](resource-governor.md) to enforce a limit on the total amount of `tempdb` space consumed by a workload group. When a request (a query) attempts to exceed the limit, resource governor aborts it with a distinct error indicating that the workload group limit is enforced.

In effect, you can partition the shared `tempdb` space among different workloads. For example, you can set a higher limit for a workload group used by a mission-critical application, and set a lower limit for the `default` workload group used by all other workloads.

For step-by-step configuration examples, see [Tutorial: Examples to configure tempdb space resource governance](tempdb-space-resource-governance-walkthrough.md).

## Get started with resource governor

Resource governor provides a flexible framework to set different `tempdb` space limits for different applications, users, user groups, etc. You can set limits based on custom logic as well.

If you're new to resource governor in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [resource governor](resource-governor.md) to learn about its concepts and capabilities.

For a resource governor configuration walkthrough and best practices, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

## Set limits on tempdb space consumption

You can limit `tempdb` space consumption by a workload group in one of two ways:

- Set a **fixed limit** by using the `GROUP_MAX_TEMPDB_DATA_MB` argument.

    Use the fixed limit when you know the workload `tempdb` usage requirements in advance or when the `tempdb` size doesn't change.
- Set a **percent limit** by using the `GROUP_MAX_TEMPDB_DATA_PERCENT` argument.

    Use the percent limit when you might change the maximum size of `tempdb` over time and you want the `tempdb` space available to each workload group to change proportionally without changing workload group configuration. For example, if you scale up an Azure VM running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and increase the maximum `tempdb` size, the `tempdb` space available to each workload group with a percent limit increases as well.

For more information about the `GROUP_MAX_TEMPDB_DATA_MB` and `GROUP_MAX_TEMPDB_DATA_PERCENT` arguments, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md#group_max_tempdb_data_mb--value) or [ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md#group_max_tempdb_data_mb--value).

If you specify both fixed and percent limits for the same workload group, the fixed limit takes precedence over the percent limit.

On a given [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance, you can have a mix of workload groups with fixed limits, percent limits, or no limits on `tempdb` space consumption. To view effective limits, see the [View effective tempdb space limits per workload group](#view-effective-tempdb-space-limits-per-workload-group) example.

### Percent limit configuration

When you run the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement, percent limits take effect according to the following table:

| Configuration | Description | Tempdb maximum size (100%) | Percent limit in effect |
| --- | --- | --- | --- |
| - `GROUP_MAX_TEMPDB_DATA_MB` isn't set<br />- For all data files, `MAXSIZE` isn't `UNLIMITED`<br />- For all data files, `FILEGROWTH` isn't zero | `tempdb` data files can autogrow up to their maximum size | The sum of `MAXSIZE` values for all data files | Yes |
| - `GROUP_MAX_TEMPDB_DATA_MB` isn't set<br />- For all data files, `MAXSIZE` is `UNLIMITED`<br />- For all data files, `FILEGROWTH` is zero | `tempdb` data files are pregrown to their intended sizes and can't grow further | The sum of `SIZE` values for all data files | Yes |
| All other configurations | | | No |

To view your `tempdb` configuration, see the [View tempdb data file configuration](#view-tempdb-data-file-configuration) example.

When using percent limits, consider the following:

- If you set `GROUP_MAX_TEMPDB_DATA_PERCENT` and execute the [ALTER RESOURCE GOVERNOR RECONFIGURE](../../t-sql/statements/alter-resource-governor-transact-sql.md) statement, but the data file configuration doesn't meet the requirements, the statement completes successfully and the percent limits are stored, but they aren't enforced. In this case, you receive warning message 10989, severity 10, which is also logged in the error log:

  ```output
  GROUP_MAX_TEMPDB_DATA_PERCENT is not in effect because tempdb
  configuration requirements aren't met.
  ```

- To make the percent limits effective, reconfigure `tempdb` data files to meet the requirements and execute `ALTER RESOURCE GOVERNOR RECONFIGURE` again. For more information about configuring `SIZE`, `FILEGROWTH`, and `MAXSIZE`, see [ALTER DATABASE File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).
- If a percent limit is in effect, and you add, remove, or resize `tempdb` data files, you must execute `ALTER RESOURCE GOVERNOR RECONFIGURE` to update resource governor with the new maximum size of `tempdb` (100%).

> [!NOTE]
> For a new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance, data file `MAXSIZE` is `UNLIMITED` and `FILEGROWTH` is greater than zero, which means that percent limits aren't effective. To use percent limits, you must either:
> - Pregrow `tempdb` data files to their intended sizes and set `FILEGROWTH` to zero.
> - Set the `MAXSIZE` of each data file to a limited value.
> - For each `tempdb` data file volume, make sure that the sum of `MAXSIZE` values for files on the volume is less than or equal to the available disk space on the volume. For example, if a volume has 100 GB of free space and has two `tempdb` data files, make the `MAXSIZE` of each file 50 GB or less.

## How it works

This section describes `tempdb` space resource governance in detail.

- As data pages in `tempdb` are allocated and deallocated, resource governor keeps the accounting of the `tempdb` space consumed by each workload group.

  If resource governor is enabled and a `tempdb` space consumption limit is set for a workload group, and a request (a query) running in the workload group attempts to bring the total `tempdb` space consumption by the group above the limit, the request is aborted with error 1138, severity 17:

  ```output
  Could not allocate a new page for database 'tempdb' because that
  would exceed the limit set for workload group 'workload-group-name'".
  ```

    When a request is aborted with error 1138, the value in the `total_tempdb_data_limit_violation_count` column of the [sys.dm_resource_governor_workload_groups](../system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md) dynamic management view (DMV) is incremented by one, and the `tempdb_data_workload_group_limit_reached` extended event fires.
- Resource governor keeps track of all `tempdb` usage that can be attributed to a workload group, including temporary tables, variables (including table variables), table-valued parameters, nontemporary tables, cursors, and `tempdb` usage during query processing, such as spools, spills, worktables, and workfiles.

    Space consumption for global temporary tables and nontemporary tables in `tempdb` is accounted under the workload group that inserts the first row into the table, even if sessions in other workload groups add, modify, or remove rows in the same table.
- The configured `tempdb` consumption limits for each workload group are exposed in the [sys.resource_governor_workload_groups](../system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md) catalog view, in the `group_max_tempdb_data_mb` and `group_max_tempdb_data_percent` columns.

    The current consumption and the peak consumption of `tempdb` space by a workload group are exposed in the [sys.dm_resource_governor_workload_groups](../system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md) DMV, in the `tempdb_data_space_kb` and `peak_tempdb_data_space_kb` columns respectively.

    > [!TIP]
    > `tempdb_data_space_kb` and `peak_tempdb_data_space_kb` columns in [sys.dm_resource_governor_workload_groups](../system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md) are maintained even if no limits on `tempdb` space consumption are set.
    >
    > You can create the classifier function and workload groups without setting any limits initially. Monitor `tempdb` usage by each group over time to establish representative usage patterns, and then set limits as required.

- `tempdb` usage by the version stores, including the persistent version store (PVS) when [accelerated database recovery (ADR)](../accelerated-database-recovery-concepts.md) is enabled in `tempdb`, isn't governed because row versions might be used by requests in multiple workload groups.
- Space consumption in `tempdb` is accounted as the number of 8-KB data pages used. Even if a page isn't filled with data fully, it adds 8 KB to the `tempdb` consumption by a workload group.
- `tempdb` space accounting is maintained for the lifetime of a workload group. If a workload group is dropped while global temporary tables or nontemporary tables with the data accounted to this workload group remain in `tempdb`, the space used by these tables isn't accounted under any other workload group.
- `tempdb` space resource governance controls the space in `tempdb` data files, but not disk space on the underlying volumes. Unless you pre-grow `tempdb` data files to their intended sizes, the space on the volumes where `tempdb` is located might be consumed by other files. If there is no remaining space for `tempdb` data files to grow, then `tempdb` might run out of space before any workload group limit on `tempdb` space consumption is reached.
- Space resource governance in `tempdb` applies to data files but not the transaction log file. To ensure that the transaction log in `tempdb` doesn't consume a large amount of space, enable [ADR](../accelerated-database-recovery-concepts.md) in `tempdb`.

### Differences with session-level space tracking

The [sys.dm_db_session_space_usage](../system-dynamic-management-views/sys-dm-db-session-space-usage-transact-sql.md) DMV provides `tempdb` space allocation and deallocation statistics for each session. Even if there's only one session in a workload group, space usage statistics from this DMV might not match exactly the statistics from the [sys.dm_resource_governor_workload_groups](../system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md) view, for the following reasons:

- Unlike `sys.dm_resource_governor_workload_groups`, `sys.dm_db_session_space_usage`:
    - Doesn't reflect `tempdb` space usage by the currently running tasks. Statistics in `sys.dm_db_session_space_usage` are updated when a task completes. Statistics in `sys.dm_resource_governor_workload_groups` are updated continuously.
  - Doesn't track index allocation map (IAM) pages. For more information, see [Page and extent architecture guide](../pages-and-extents-architecture-guide.md).
- After rows are deleted, or when a table, index, or partition is dropped or truncated, the database engine deallocates the data pages. The deallocation might be synchronous or performed by an asynchronous background process. `sys.dm_resource_governor_workload_groups` reflects these page deallocations as they occur, even if the session that caused these deallocations was closed and is no longer present in `sys.dm_db_session_space_usage`.

## Best practices for tempdb space resource governance

Before configuring `tempdb` space resource governance, consider the following best practices:

- Review the general [best practices](resource-governor-walkthrough.md#resource-governor-best-practices) for resource governor.
- For most scenarios, avoid setting the `tempdb` space consumption limit to a small value or zero, particularly for the `default` workload group. If you set this limit to a small value or zero, many common tasks might start failing if they need to allocate space in `tempdb`. For example, if you set either the fixed or the percent limit to 0 for the `default` workload group, you might not be able to open Object Explorer in SQL Server Management Studio (SSMS).
- Unless you create custom workload groups and a classifier function that places workloads in their dedicated groups, avoid limiting `tempdb` usage of the `default` workload group. If you limit `tempdb` space consumption by the `default` workload group, queries might return error 1138. This error occurs when `tempdb` still has unused space that can't be consumed by any user workload.
- The sum of `GROUP_MAX_TEMPDB_DATA_MB` values for all workload groups can exceed the maximum `tempdb` size. For example, if the maximum `tempdb` size is 100 GB, the `GROUP_MAX_TEMPDB_DATA_MB` limits for workload group *A* and workload group *B* can be 80 GB each.

    This approach still prevents each workload group from consuming all space in `tempdb` by leaving 20 GB for other workload groups. At the same time, you avoid unnecessary query aborts when free `tempdb` space is still available because workload groups *A* and *B* aren't likely to consume a large amount of `tempdb` space at the same time.

    Similarly, the sum of `GROUP_MAX_TEMPDB_DATA_PERCENT` values for all workload groups can exceed 100 percent. You can allocate more `tempdb` space to each group if you know that multiple groups are unlikely to cause high `tempdb` usage at the same time.

## Examples

### View tempdb data file configuration

The following query shows the current `tempdb` data file configuration:

```sql
SELECT file_id,
       name,
       size * 8. / 1024 AS size_mb,
       IIF (max_size = -1, NULL, max_size * 8. / 1024) AS maxsize_mb,
       IIF (is_percent_growth = 0, growth * 8. / 1024, NULL) AS filegrowth_mb,
       IIF (is_percent_growth = 1, growth, NULL) AS filegrowth_percent
FROM sys.master_files
WHERE database_id = 2
      AND type_desc = 'ROWS';
```

For a given file in the result set:

- If the `maxsize_mb` column is `NULL`, then `MAXSIZE` is `UNLIMITED`.
- When either `filegrowth_mb` or `filegrowth_percent` is zero, then `FILEGROWTH` is zero.

### View effective tempdb space limits per workload group

The following query shows the effective `tempdb` space consumption limit for each workload group. The limit is returned in megabytes for either **fixed limit** or **percent limit** configuration.

If the `group_effective_limit_mb` column is `NULL`, it means one of the following: 

- Neither fixed nor percent limit is configured.
- The [requirements](#percent-limit-configuration) for using the percent limit configuration aren't met.

```sql
SELECT wg.group_id,
       wg.name,
       tf.tempdb_max_size_mb,
       CASE
           WHEN wg.group_max_tempdb_data_mb IS NOT NULL
               THEN wg.group_max_tempdb_data_mb
           WHEN wg.group_max_tempdb_data_percent IS NOT NULL AND tf.tempdb_max_size_mb IS NOT NULL
               THEN 0.01 * wg.group_max_tempdb_data_percent * tf.tempdb_max_size_mb
       ELSE NULL END AS group_effective_limit_mb
FROM sys.resource_governor_workload_groups AS wg
CROSS APPLY (
    SELECT IIF (SUM(IIF (max_size <> -1
        AND growth > 0, 1, 0)) = COUNT(1) /* autogrow up to the maxsize */
        OR SUM(IIF (max_size = -1
        AND growth = 0, 1, 0)) = COUNT(1), /* pregrown and fixed */
        SUM(IIF (growth = 0, size, max_size)) * 8 / 1024., NULL) AS tempdb_max_size_mb
    FROM sys.master_files
    WHERE database_id = 2
        AND type_desc = 'ROWS'
) AS tf;
```

## Next step

> [!div class="nextstepaction"]
> [Tutorial: Examples to configure tempdb space resource governance](tempdb-space-resource-governance-walkthrough.md)

## Related content

- [Resource governor](resource-governor.md)
- [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/create-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/drop-workload-group-transact-sql.md)
- [sys.resource_governor_workload_groups (Transact-SQL)](../system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md)
- [sys.dm_resource_governor_workload_groups](../system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)
