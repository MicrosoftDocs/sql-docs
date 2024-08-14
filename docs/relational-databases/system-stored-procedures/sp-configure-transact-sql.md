---
title: "sp_configure (Transact-SQL)"
description: sp_configure displays or changes global configuration settings for the current server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_configure"
  - "sp_configure_TSQL"
helpviewer_keywords:
  - "sp_configure"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-mi-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# sp_configure (Transact-SQL)

[!INCLUDE [tsql-appliesto-ss2008-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-pdw-md.md)]

Displays or changes global configuration settings for the current server.

> [!NOTE]  
> For database-level configuration options, see [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). To configure Soft-NUMA, see [Soft-NUMA (SQL Server)](../../database-engine/configure-windows/soft-numa-sql-server.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server.

```syntaxsql
sp_configure
    [ [ @configname = ] 'configname' ]
    [ , [ @configvalue = ] configvalue ]
[ ; ]
```

Syntax for Parallel Data Warehouse.

```syntaxsql
sp_configure
[ ; ]
```

## Arguments

#### [ @configname = ] '*configname*'

The name of a configuration option. *@configname* is **varchar(35)**, with a default of `NULL`. The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] recognizes any unique string that is part of the configuration name. If not specified, the complete list of options is returned.

For information about the available configuration options and their settings, see [Server configuration options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md).

#### [ @configvalue = ] *configvalue*

The new configuration setting. *@configvalue* is **int**, with a default of `NULL`.

The maximum value depends on the individual option. To see the maximum value for each option, see the `maximum` column of the `sys.configurations` catalog view.

## Return code values

`0` (success) or `1` (failure).

## Result set

When executed with no parameters, `sp_configure` returns a result set with five columns and orders the options alphabetically in ascending order, as shown in the following table.

The values for `config_value` and `run_value` aren't automatically equivalent. After you update a configuration setting by using `sp_configure`, you must also update the running configuration value, by using either `RECONFIGURE` or `RECONFIGURE WITH OVERRIDE`. For more information, see the [Remarks](#remarks) section.

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **nvarchar(35)** | Name of the configuration option. |
| `minimum` | **int** | Minimum value of the configuration option. |
| `maximum` | **int** | Maximum value of the configuration option. |
| `config_value` | **int** | Value to which the configuration option was set using `sp_configure` (value in `sys.configurations.value`).<br /><br />For more information about these options, see [Server configuration options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md) and [sys.configurations](../system-catalog-views/sys-configurations-transact-sql.md). |
| `run_value` | **int** | Currently running value of the configuration option (value in `sys.configurations.value_in_use`).<br /><br />For more information, see [sys.configurations](../system-catalog-views/sys-configurations-transact-sql.md). |

## Remarks

Use `sp_configure` to display or change server-level settings. To change database-level settings, use `ALTER DATABASE`. To change settings that affect only the current user session, use the `SET` statement.

Some server configuration options are only available through [ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md).

### [!INCLUDE [ssbigdataclusters-ss-nover](../../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE [big-data-clusters-master-instance-ha-endpoint-requirement](../../includes/big-data-clusters-master-instance-ha-endpoint-requirement.md)]

## Update the running configuration value

When you specify a new *@configvalue* for a *@configname*, the result set shows this value in the `config_value` column. This value initially differs from the value in the `run_value` column, which shows the currently running configuration value. To update the running configuration value in the `run_value` column, the system administrator must run either `RECONFIGURE` or `RECONFIGURE WITH OVERRIDE`.

Both `RECONFIGURE` and `RECONFIGURE WITH OVERRIDE` work with every configuration option. However, the basic `RECONFIGURE` statement rejects any option value that is outside a reasonable range or that might cause conflicts among options. For example, `RECONFIGURE` generates an error if the **recovery interval** value is larger than 60 minutes or if the **affinity mask** value overlaps with the **affinity I/O mask** value. `RECONFIGURE WITH OVERRIDE`, in contrast, accepts any option value with the correct data type and forces reconfiguration with the specified value.

> [!CAUTION]  
> An inappropriate option value can adversely affect the configuration of the server instance. Use `RECONFIGURE WITH OVERRIDE` cautiously.

The `RECONFIGURE` statement updates some options dynamically; other options require a server stop and restart. For example, the **min server memory** and **max server memory** server memory options are updated dynamically in the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. Therefore, you can change them without restarting the server. By contrast, reconfiguring the running value of the **fill factor** option requires restarting the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

After running `RECONFIGURE` on a configuration option, you can see whether the option was updated dynamically by executing `sp_configure '<configname>'`. The values in the `run_value` and `config_value` columns should match for a dynamically updated option. You can also check to see which options are dynamic by looking at the `is_dynamic` column of the `sys.configurations` catalog view.

The change is also written to the SQL Server error log.

If a specified *@configvalue* is too high for an option, the `run_value` column reflects the fact that the [!INCLUDE [ssDE](../../includes/ssde-md.md)] defaults to dynamic memory, rather than use a setting that isn't valid.

For more information, see [RECONFIGURE](../../t-sql/language-elements/reconfigure-transact-sql.md).

## Advanced options

Some configuration options, such as **affinity mask** and **recovery interval**, are designated as advanced options. By default, these options aren't available for viewing and changing. To make them available, set the **show advanced options** configuration option to `1`.

> [!CAUTION]  
> When the option **show advanced options** is set to `1`, this setting applies to all users. It's recommended to only use this state temporarily and switch back to `0` when done with the task that required viewing the advanced options.

For more information about the configuration options and their settings, see [Server configuration options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md).

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, you must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

## Examples

### A. List the advanced configuration options

The following example shows how to set and list all configuration options. You can display advanced configuration options by first setting `show advanced options` to `1`. After this option changes, you can display all configuration options by executing `sp_configure` with no parameters.

```sql
USE master;
GO
EXEC sp_configure 'show advanced options', '1';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Configuration option 'show advanced options' changed from 0 to 1. Run the `RECONFIGURE` statement to install.
```

Run `RECONFIGURE` and show all configuration options:

```sql
RECONFIGURE;
EXEC sp_configure;
```

### B. Change a configuration option

The following example sets the system `recovery interval` configuration option to `3` minutes.

```sql
USE master;
GO
EXEC sp_configure 'recovery interval', '3';
RECONFIGURE WITH OVERRIDE;
```

## Examples: [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### C. List all available configuration settings

The following example shows how to list all configuration options.

```sql
EXEC sp_configure;
```

The result returns the option name followed by the minimum and maximum values for the option. The `config_value` is the value that [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] uses when reconfiguration is complete. The `run_value` is the value that is currently being used. The `config_value` and `run_value` are usually the same unless the value is in the process of being changed.

### D. List the configuration settings for one configuration name

```sql
EXEC sp_configure @configname = 'hadoop connectivity';
```

### E. Set Hadoop connectivity

Setting Hadoop connectivity requires a few more steps in addition to running `sp_configure`. For the full procedure, see [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).

```syntaxsql
sp_configure [ @configname = ] 'hadoop connectivity',
             [ @configvalue = ] { 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 }
[ ; ]
RECONFIGURE;
[ ; ]
```

## Related content

- [ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
- [Server configuration options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.configurations (Transact-SQL)](../system-catalog-views/sys-configurations-transact-sql.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
- [Soft-NUMA (SQL Server)](../../database-engine/configure-windows/soft-numa-sql-server.md)
