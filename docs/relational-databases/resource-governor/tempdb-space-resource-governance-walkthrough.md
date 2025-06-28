---
title: Examples to Configure Tempdb Space Resource Governance
description: Use examples to learn how to configure tempdb space resource governance.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: randolphwest
ms.date: 06/10/2025
ms.service: sql
ms.topic: tutorial
ms.custom:
  - build-2025
monikerRange: ">=sql-server-ver17 || >=sql-server-linux-ver17"
---

# Tutorial: Examples to configure tempdb space resource governance

[!INCLUDE [sqlserver2025-and-later](../../includes/applies-to-version/sqlserver2025-and-later.md)]

Examples in this article show you how to set limits on `tempdb` space consumption and view the consumption of `tempdb` space by each workload group. 

For an introduction to `tempdb` space resource governance, see [Tempdb space resource governance](tempdb-space-resource-governance.md).

These examples are intended to help you become familiar with `tempdb` space resource governance in a test, nonproduction environment.

Examples assume that resource governor isn't enabled initially and that its configuration isn't changed from the default. They also assume that any other workloads on your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance aren't materially contributing to `tempdb` space consumption while you execute the scripts.

## Set a fixed limit for the `default` workload group

This example limits the total `tempdb` space consumption by the requests (queries) in the `default` workload group to a fixed limit.

1. Modify the `default` workload group to configure a fixed 20-GB limit on `tempdb` space consumption.

    ```sql
    ALTER WORKLOAD GROUP [default]
    WITH (GROUP_MAX_TEMPDB_DATA_MB = 20480);
    ```

1. Enable resource governor to make the current configuration effective.

    ```sql
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```

1. View the limits on `tempdb` space consumption.

    ```sql
    SELECT group_id,
           name,
           group_max_tempdb_data_mb,
           group_max_tempdb_data_percent
    FROM sys.resource_governor_workload_groups
    WHERE name = 'default';
    ```

1. Check the current `tempdb` space consumption by the `default` workload group, add data in `tempdb` by creating a temporary table and inserting one row, and then check the space consumption again to see the increase.

    ```sql
    SELECT group_id,
           name,
           tempdb_data_space_kb
    FROM sys.dm_resource_governor_workload_groups
    WHERE name = 'default';

    SELECT REPLICATE('A', 1000) AS c
    INTO #t;

    SELECT group_id,
           name,
           tempdb_data_space_kb
    FROM sys.dm_resource_governor_workload_groups
    WHERE name = 'default';
    ```

1. Optionally, remove the limits for the `default` group and disable resource governor to revert to nongoverned space consumption in `tempdb`:

    ```sql
    ALTER WORKLOAD GROUP [default]
    WITH (GROUP_MAX_TEMPDB_DATA_MB = NULL, GROUP_MAX_TEMPDB_DATA_PERCENT = NULL);

    ALTER RESOURCE GOVERNOR DISABLE;
    ```

## Set a percent limit for the `default` workload group

This example configures `tempdb` data files so that the percent limit can be used, and then limits the total `tempdb` space consumption by the requests (queries) in the `default` workload group to a percent limit.

1. Set `FILEGROWTH` and `MAXSIZE` for all `tempdb` data files to meet the [requirements](tempdb-space-resource-governance.md#percent-limit-configuration), limiting the maximum size of `tempdb` to 1 GB.

    This example assumes that `tempdb` has four data files. You might need to adjust the script if your `tempdb` configuration uses a different number of files, or if the file logical names are different. You might also need to restart your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance or reduce `tempdb` usage if you get error 5040, *MODIFY FILE failed for database 'tempdb' ... Size of file ... is greater than MAXSIZE ...* when running this script.

    ```sql
    ALTER DATABASE tempdb MODIFY FILE (NAME = N'tempdev', FILEGROWTH = 64 MB, MAXSIZE = 256 MB);
    ALTER DATABASE tempdb MODIFY FILE (NAME = N'temp2', FILEGROWTH = 64 MB, MAXSIZE = 256 MB);
    ALTER DATABASE tempdb MODIFY FILE (NAME = N'temp3', FILEGROWTH = 64 MB, MAXSIZE = 256 MB);
    ALTER DATABASE tempdb MODIFY FILE (NAME = N'temp4', FILEGROWTH = 64 MB, MAXSIZE = 256 MB);
    ```

1. Modify the `default` workload group to configure a five percent limit on the `tempdb` space consumption. With the 1-GB maximum `tempdb` size, this limits the `default` group to about 51 MB of `tempdb` space.

    ```sql
    ALTER WORKLOAD GROUP [default]
    WITH (GROUP_MAX_TEMPDB_DATA_PERCENT = 5);
    ```

1. If a fixed limit is set, remove it so it doesn't override the percent limit.

    ```sql
    ALTER WORKLOAD GROUP [default]
    WITH (GROUP_MAX_TEMPDB_DATA_MB = NULL);
    ```

1. Enable resource governor to make the configuration effective.

    ```sql
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```

1. View the limits on `tempdb` space consumption.

    ```sql
    SELECT group_id,
           name,
           group_max_tempdb_data_mb,
           group_max_tempdb_data_percent
    FROM sys.resource_governor_workload_groups
    WHERE name = 'default';
    ```

1. Add data in `tempdb` to reach the limit.

    ```sql
    SELECT *
    INTO #m
    FROM sys.messages;
    ```

    The statement is aborted with error 1138.

1. Check the workload group statistics for `tempdb`.

    ```sql
    SELECT group_id,
           name,
           tempdb_data_space_kb,
           peak_tempdb_data_space_kb,
           total_tempdb_data_limit_violation_count
    FROM sys.dm_resource_governor_workload_groups
    WHERE name = 'default';
    ```

    The value in the `total_tempdb_data_limit_violation_count` column is incremented by 1, showing that one request in the `default` workload group was aborted because its `tempdb` space consumption was limited by resource governor.

1. Optionally, remove the limits for the `default` group and disable resource governor to revert to nongoverned space consumption in `tempdb`:

    ```sql
    ALTER WORKLOAD GROUP [default]
    WITH (GROUP_MAX_TEMPDB_DATA_MB = NULL, GROUP_MAX_TEMPDB_DATA_PERCENT = NULL);

    ALTER RESOURCE GOVERNOR DISABLE;
    ```

1. Optionally, revert the `tempdb` data file configuration changes made earlier in this example.

## Set a fixed limit for a user-defined workload group

This example creates a new workload group, and then creates a classifier function to assign sessions with a specific application name to this workload group.

For the purposes of this example, the fixed limit on the `tempdb` space consumption for the workload group is set to a small value of 1 MB. The example then shows that an attempt to allocate space in `tempdb` that exceeds the limit is aborted.

1. Create a workload group and limit its `tempdb` space consumption to 1 MB.

    ```sql
    CREATE WORKLOAD GROUP limited_tempdb_space_group
    WITH (GROUP_MAX_TEMPDB_DATA_MB = 1);
    ```

1. Create the classifier function in the `master` database. The classifier uses the built-in [APP_NAME](../../t-sql/functions/app-name-transact-sql.md) function to determine the application name specified in the client connection string. If the application name is set to `limited_tempdb_application`, the function returns `limited_tempdb_space_group` as the name of the workload group to use. Otherwise, the function returns `default` as the workload group name.

    ```sql
    USE master;
    GO

    CREATE FUNCTION dbo.rg_classifier()
    RETURNS sysname
    WITH SCHEMABINDING
    AS
    BEGIN

    DECLARE @WorkloadGroupName sysname = N'default';

    IF APP_NAME() = N'limited_tempdb_application'
        SELECT @WorkloadGroupName = N'limited_tempdb_space_group';

    RETURN @WorkloadGroupName;

    END;
    GO
    ```

1. Modify resource governor to use the classifier function, and reconfigure resource governor to use the new configuration.

    ```sql
    ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = dbo.rg_classifier);
    ALTER RESOURCE GOVERNOR RECONFIGURE;
    ```

1. Open a new session that is classified into the `limited_tempdb_space_group` workload group.

    1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) older than 21 release, select **File** on the main menu, **New**, **Database Engine Query**.
    1. In the **Connect to Database Engine** dialog, specify the same [!INCLUDE [ssde-md](../../includes/ssde-md.md)] instance where you created the workload group and the classifier function in the previous steps.

       Select the **Additional Connection Parameters** tab, and enter `App=limited_tempdb_application`. This makes SSMS use `limited_tempdb_application` as the application name when connecting to the instance. The `APP_NAME()` function in the classifier returns this value as well.

    1. Select **Connect** to open a new session.

       If you are using SSMS 21 and newer, then click "Advanced...", then under "Context" update "Application Name" field.

1. Execute the following statement in the query window opened in the previous step. The output should show that your session is classified into the `limited_tempdb_space_group` workload group.

    ```sql
    SELECT wg.name AS workload_group_name
    FROM sys.dm_exec_sessions AS s
    INNER JOIN sys.dm_resource_governor_workload_groups AS wg
    ON s.group_id = wg.group_id
    WHERE s.session_id = @@SPID;
    ```

1. Execute the following statement in the same query window.

    ```sql
    SELECT REPLICATE('S', 100) AS c
    INTO #t1;
    ```

    The statement completes successfully. Execute the following statement in the same query window:

    ```sql
    SELECT REPLICATE(CAST ('F' AS NVARCHAR (MAX)), 1000000) AS c
    INTO #t2;
    ```

    The statement is aborted with error 1138 because it attempts to exceed the 1-MB `tempdb` space consumption limit for the workload group.

1. See the current and peak `tempdb` space consumption by the `limited_tempdb_space_group` workload group.

    ```sql
    SELECT group_id,
           name,
           tempdb_data_space_kb,
           peak_tempdb_data_space_kb,
           total_tempdb_data_limit_violation_count
    FROM sys.dm_resource_governor_workload_groups
    WHERE name = 'limited_tempdb_space_group';
    ```

    The value in the `total_tempdb_data_limit_violation_count` column is 1, showing that one request in this workload group was aborted because its `tempdb` space consumption was limited by resource governor.

1. Optionally, to revert to the initial configuration of this example, disconnect all sessions using the `limited_tempdb_space_group` workload group, and execute the following T-SQL script:

    ```sql
    /* Disable resource governor so that the classifier function can be dropped. */
    ALTER RESOURCE GOVERNOR DISABLE;
    ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = NULL);
    DROP FUNCTION IF EXISTS dbo.rg_classifier;

    /* Drop the workload group. This requires that no sessions are using this workload group. */
    DROP WORKLOAD GROUP limited_tempdb_space_group;

    /* Reconfigure resource governor to reload the effective configuration without the classifier function and the workload group. This enables resource governor. */
    ALTER RESOURCE GOVERNOR RECONFIGURE;

    /* Disable resource governor to revert to the initial configuration. */
    ALTER RESOURCE GOVERNOR DISABLE;
    ```

    Because SSMS retains connection parameters in the **Additional Connection Parameters** tab, make sure to remove the `App` parameter the next time you connect to the same [!INCLUDE [ssde-md](../../includes/ssde-md.md)] instance. This avoids your connections getting classified into the `limited_tempdb_space_group` workload group if it exists.

## Related content

- [Tempdb space resource governance](tempdb-space-resource-governance.md)
- [Resource governor](resource-governor.md)
- [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/create-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/drop-workload-group-transact-sql.md)
- [sys.resource_governor_workload_groups](../system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md)
- [sys.dm_resource_governor_workload_groups](../system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)
