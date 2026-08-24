---
title: "sys.sp_change_feed_configure_parameters (Transact-SQL)"
description: "The sys.sp_change_feed_configure_parameters system stored procedure is used to increase the batch size with higher transactions."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, ajayj, randolphwest
ms.date: 06/19/2026
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.sp_change_feed_configure_parameters_TSQL"
  - "sys.sp_change_feed_configure_parameters"
  - "sp_change_feed_configure_parameters_TSQL"
  - "sp_change_feed_configure_parameters"
helpviewer_keywords:
  - "sp_change_feed_configure_parameters"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_change_feed_configure_parameters (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Configures optional performance settings for the change feed for the current database context.

This system stored procedure is used to fine tune the operational performance for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_configure_parameters
    [ [ @maxtrans = ] maxtrans ]
    [ , [ @pollinterval = ] pollinterval ]
    [ , [ @autoreseed = ] autoreseed ]
    [ , [ @autoreseedthreshold = ] autoreseedthreshold ]
    [ , [ @dynamicmaxtrans = ] dynamicmaxtrans ]
    [ , [ @dynamicmaxtranslowerbound = ] dynamicmaxtranslowerbound ]
[ ; ]
```

## Arguments

#### [ @maxtrans = ] *maxtrans*

Data type is **int**. Indicates the maximum number of transactions to process in each scan cycle.

Used to reduce latency by decreasing change batch size with `@maxtrans`, or to reduce the cost by increasing the batch size. As the batch size increases, fewer I/O operations are performed.

- For Azure Synapse Link, the default value if not specified is `10000`. If specified, the value must be a positive integer.
- For Fabric mirroring, this value is dynamically determined and automatically set.

#### [ @pollinterval = ] *pollinterval*

Data type is **int**. Describes the frequency that the log is scanned for any new changes, in seconds.

- For Azure Synapse Link, the default interval if not specified is 5 seconds. The value must be `5` or larger.
- For Fabric mirroring, this value is dynamically determined and automatically set.

#### [ @autoreseed = ] *autoreseed*

**Applies to**: Fabric Mirroring only

The `autoreseed` argument defines the setting of the autoreseed option. `0` = disabled, `1` = enabled at the provided *autoreseed_threshold_percent*.

The autoreseed feature is disabled by default in SQL Server 2025 (Preview). The autoreseed feature is enabled and can't be managed or disabled in Azure SQL Database and Azure SQL Managed Instance.

During reseed, the mirrored database item in Microsoft Fabric is available but will not receive incremental changes.

For more information, see [Configure automatic reseed for Fabric mirrored databases](/fabric/database/mirrored-database/sql-server-configure-automatic-reseed).

#### [ @autoreseedthreshold = ] *autoreseedthreshold*

**Applies to**: Fabric Mirroring only

The `autoreseedthreshold` argument defines the log usage percentage threshold when an autoreseed event triggers. By default, `70`.

#### [ @dynamicmaxtrans = ] *dynamicmaxtrans*

**Applies to**: Fabric Mirroring only

Whether or not the dynamic maximum transactions setting for Fabric Mirroring is enabled. `0` = disabled, `1` = enabled. Fabric follows a maximum number of transactions to process in each scan cycle. For more information, [Mirrored databases from SQL Server performance](/fabric/database/mirrored-database/sql-server-performance#control-scan-performance).

The dynamic maximum transactions feature is enabled by default in SQL Server 2025 (Preview). The dynamic maximum transactions feature is enabled and can't be managed or disabled in Azure SQL Database and Azure SQL Managed Instance.

#### [ @dynamicmaxtranslowerbound = ] *dynamicmaxtranslowerbound*

**Applies to**: Fabric Mirroring only

The lower bound for dynamic maxtrans setting for Fabric Mirroring. By default, the lower bound value is `200`.

## Returns

`0` (success) or `1` (failure).

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Examples

### A. Enable automatic reseed

**Applies to**: Fabric Mirroring only

Use the following T-SQL sample to enable automatic reseed on the current mirrored database. Specify a log usage percentage threshold to trigger an autoreseed event, for example 70%. For more information, see [Configure automatic reseed for Fabric mirrored databases](/fabric/database/mirrored-database/sql-server-configure-automatic-reseed).

```sql
USE <Mirrored database name>
GO
EXECUTE sys.sp_change_feed_configure_parameters
  @autoreseed = 1
, @autoreseedthreshold = 70;
```

### B. Disable automatic reseed

**Applies to**: Fabric Mirroring only

Use the following T-SQL sample to disable automatic reseed on the current mirrored database.

```sql
USE <Mirrored database name>
GO
EXECUTE sys.sp_change_feed_configure_parameters @autoreseed = 0;
```

### C. Enable dynamic maximum transactions

**Applies to**: Fabric Mirroring only

To enable the dynamic maximum transactions feature, set `@dynamicmaxtrans` to `1`. For example:

```sql
USE <Mirrored database name>
GO
EXECUTE sys.sp_change_feed_configure_parameters
  @dynamicmaxtrans=1;
```

To disable the dynamic maximum transactions feature, set `@dynamicmaxtrans` to `0`. For example:

```sql
USE <Mirrored database name>
GO
EXECUTE sys.sp_change_feed_configure_parameters
  @dynamicmaxtrans=0;
```

Verify the setting of the dynamic maximum transactions feature with [sys.sp_help_change_feed_settings](sp-help-change-feed-settings.md).

### D. Configure the dynamic maximum transactions maximum and lower bound

**Applies to**: Fabric Mirroring only

To modify the maximum and lower bounds for the dynamic maximum transactions feature, use `@maxtrans` and `@dynamicmaxtranslowerbound` respectively. For example:

```sql
USE <Mirrored database name>
GO
EXECUTE sys.sp_change_feed_configure_parameters
  @dynamicmaxtrans=1
, @dynamicmaxtranslowerbound=5
, @maxtrans=5000;
```

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_help_change_feed_table_groups (Transact-SQL)](sp-help-change-feed-table-groups.md)
- [sys.sp_help_change_feed_settings (Transact-SQL)](sp-help-change-feed-settings.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
