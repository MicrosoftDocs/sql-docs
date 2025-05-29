---
title: "ALTER RESOURCE GOVERNOR (Transact-SQL)"
description: ALTER RESOURCE GOVERNOR (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.reviewer: dfurman
ms.date: 02/17/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER RESOURCE GOVERNOR"
  - "ALTER_RESOURCE_GOVERNOR_TSQL"
  - "ALTER RESOURCE GOVERNOR RECONFIGURE"
  - "ALTER_RESOURCE_GOVERNOR_RECONFIGURE_TSQL"
helpviewer_keywords:
  - "ALTER RESOURCE GOVERNOR"
  - "RECONFIGURE, ALTER RESOURCE GOVERNOR"
dev_langs:
  - "TSQL"
---

# ALTER RESOURCE GOVERNOR (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

This statement performs the following resource governor actions:

- Enable or disable resource governor.
- Apply the configuration changes specified when the `CREATE | ALTER | DROP WORKLOAD GROUP` or `CREATE | ALTER | DROP RESOURCE POOL` or `CREATE | ALTER | DROP EXTERNAL RESOURCE POOL` statements are executed.
- Configure classification for incoming sessions.
- Reset workload group and resource pool statistics.
- Set the maximum queued I/O operations per disk volume.

> [!NOTE]
> To modify resource governor configuration in [!INCLUDE[ssazuremi-md.md](../../includes/ssazuremi-md.md)], you must be in the context of the `master` database on the primary replica.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER RESOURCE GOVERNOR
    { RECONFIGURE
      | DISABLE
      | RESET STATISTICS
      | WITH
              ( [ CLASSIFIER_FUNCTION = { schema_name.function_name | NULL } ]
                [ [ , ] MAX_OUTSTANDING_IO_PER_VOLUME = value ]
              )
    }
[ ; ]
```

## Arguments

#### RECONFIGURE

When resource governor isn't enabled, `RECONFIGURE` enables resource governor. Enabling resource governor has the following results:
- The classifier function, if any, is executed for new sessions, assigning them to workload groups.
- The resource reservations and limits that are specified in resource governor configuration are honored and enforced.
- Requests that existed before enabling resource governor might be affected by any configuration changes made when resource governor is enabled.

When resource governor is enabled, `RECONFIGURE` applies any configuration changes made by the `CREATE | ALTER | DROP WORKLOAD GROUP` or `CREATE | ALTER | DROP RESOURCE POOL` or `CREATE | ALTER | DROP EXTERNAL RESOURCE POOL` statements after the previous use of `RECONFIGURE` or after the last restart of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

> [!IMPORTANT]
> `ALTER RESOURCE GOVERNOR RECONFIGURE` must be executed for any resource governor configuration changes to take effect.

#### CLASSIFIER_FUNCTION = { _schema_name_**.**_function_name_ | NULL }

Registers the classification function specified by *schema_name.function_name*. This function classifies every new session and assigns the session to a workload group. When `NULL` is used, new sessions are automatically assigned to the `default` workload group.

#### MAX_OUTSTANDING_IO_PER_VOLUME = *value*

**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.

Sets the maximum queued I/O operations per disk volume. These I/O operations can be reads or writes of any size. The maximum value for `MAX_OUTSTANDING_IO_PER_VOLUME` is 100. The value isn't a percentage. This setting is designed to tune IO resource governance to the IO characteristics of a disk volume. It provides a system-level safety check that allows the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to meet the `MIN_IOPS_PER_VOLUME` setting specified for resource pools even if other pools have the `MAX_IOPS_PER_VOLUME` setting set to unlimited. For more information, see [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md).

#### DISABLE

Disables resource governor. Disabling resource governor has the following results:
- The classifier function isn't executed.
- All new user sessions are automatically classified into the `default` workload group.
- System sessions are classified into the `internal` workload group.
- All existing workload group and resource pool settings are reset to their default values. No events are fired when limits are reached.
- Normal system monitoring isn't affected.
- Resource governor configuration changes can be made, but the changes don't take effect until resource governor is enabled.
- After restarting the [!INCLUDE[ssDE](../../includes/ssde-md.md)], resource governor doesn't load its configuration, but instead uses only the `default` and `internal` workload groups and resource pools.

#### RESET STATISTICS

Resets statistics on all workload groups and resource pools exposed in [sys.dm_resource_governor_workload_groups](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md) and [sys.dm_resource_governor_resource_pools](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md).

## Remarks

`ALTER RESOURCE GOVERNOR` can't be used inside a user transaction.

The `RECONFIGURE` parameter is part of the resource governor syntax. It shouldn't be confused with [RECONFIGURE](../../t-sql/language-elements/reconfigure-transact-sql.md), which is a separate DDL statement.

For more information, see [Resource governor](../../relational-databases/resource-governor/resource-governor.md).

## Permissions

Requires the `CONTROL SERVER` permission.

## Examples

### Enable resource governor

When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, resource governor is disabled. The following example enables resource governor. After the statement executes, resource governor is enabled and uses built-in workload groups and resource pools.

```sql
ALTER RESOURCE GOVERNOR RECONFIGURE;
```

### Assign new sessions to the default group

The following example assigns all new sessions to the `default` workload group by removing any existing classifier function from the resource governor configuration. When no function is designated as a classifier function, all new user sessions are assigned to the `default` workload group. This change applies to new sessions only. Existing sessions aren't affected.

```sql
ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = NULL);
ALTER RESOURCE GOVERNOR RECONFIGURE;
```

### Create and register a classifier function

The following example creates a classifier function named `dbo.rg_classifier` in the `master` database. The function classifies every new session based on either the user name or application name and assigns the session requests and queries to a specific workload group. Sessions that do not map to the specified user or application names are assigned to the default workload group. The classifier function is then registered and the configuration change is applied.

```sql
USE master;
GO

CREATE FUNCTION dbo.rg_classifier()
RETURNS sysname
WITH SCHEMABINDING
AS
BEGIN

-- Declare the variable for the function return value.
DECLARE @grp_name AS sysname;

-- If the login name is 'sa', classify the session into the groupAdmin workload group
IF (SUSER_NAME() = 'sa')
  SET @grp_name = 'groupAdmin';

-- Classify SSMS sessions into the groupAdhoc workload group
ELSE IF UPPER(APP_NAME()) LIKE '%MANAGEMENT STUDIO%'
  SET @grp_name = 'groupAdhoc';

-- Classify SSRS sessions into groupReports workload group
ELSE IF UPPER(APP_NAME()) LIKE '%REPORT SERVER%'
  SET @grp_name = 'groupReports';

-- Otherwise, classify the session into the default workload group
ELSE
  SET @grp_name = 'default';

-- Return the name of the workload group
RETURN @grp_name;

END;
GO

-- Register the classifier function and update resource governor configuration
ALTER RESOURCE GOVERNOR WITH (CLASSIFIER_FUNCTION = dbo.rg_classifier);
ALTER RESOURCE GOVERNOR RECONFIGURE;
```

### Reset resource governor statistics

The following example resets all workload group and resource pool statistics.

```sql
ALTER RESOURCE GOVERNOR RESET STATISTICS;
```

### Configure the MAX_OUTSTANDING_IO_PER_VOLUME setting

The following example sets the `MAX_OUTSTANDING_IO_PER_VOLUME` setting to 20 IOs.

```sql
ALTER RESOURCE GOVERNOR WITH (MAX_OUTSTANDING_IO_PER_VOLUME = 20);
```

## Related content

- [Resource governor](../../relational-databases/resource-governor/resource-governor.md)
- [Resource governor configuration examples and best practices](../../relational-databases/resource-governor/resource-governor-walkthrough.md)
- [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL](../../t-sql/statements/alter-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL](../../t-sql/statements/drop-resource-pool-transact-sql.md)
- [CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
- [DROP EXTERNAL RESOURCE POOL](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)
- [ALTER EXTERNAL RESOURCE POOL](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
- [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP](../../t-sql/statements/drop-workload-group-transact-sql.md)
- [sys.dm_resource_governor_workload_groups](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)
- [sys.dm_resource_governor_resource_pools](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md)
