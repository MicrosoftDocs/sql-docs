---
title: Server configuration options (SQL Server)
description: Find out how to manage and optimize SQL Server resources. View available configuration options, possible settings, default values, and restart requirements.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 03/04/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "surface area configuration [SQL Server], sp_configure"
  - "configuration options [SQL Server], when take effect"
  - "server management [SQL Server], configuration options"
  - "SQL Server Management Studio [SQL Server], servers"
  - "servers [SQL Server], configuring"
  - "configuration options [SQL Server], setting"
  - "options [SQL Server], configuration"
  - "RECONFIGURE statement"
  - "performance [SQL Server], servers"
  - "configuration options [SQL Server]"
  - "RECONFIGURE WITH OVERRIDE statement"
  - "SQL Server, configuring"
  - "sp_configure"
  - "stored procedures [SQL Server], configuration options"
  - "server configuration [SQL Server]"
  - "administering SQL Server, configuration options"
keywords: server configuration (SQL Server)
---
# Server configuration options (SQL Server)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

You can manage and optimize [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] resources through configuration options by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the `sp_configure` system stored procedure. The most commonly used server configuration options are available through [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]; all configuration options are accessible through `sp_configure`. Consider the effects on your system carefully before setting these options. For more information, see [View or change server properties (SQL Server)](view-or-change-server-properties-sql-server.md).

> [!IMPORTANT]  
> Advanced options should be changed only by an experienced database administrator or certified SQL Server technician.

## Categories of configuration options

If you don't see the effect of a configuration change, it might not be installed. Check to see that the `run_value` of the configuration option has changed.

Configuration options take effect immediately after setting the option and issuing the `RECONFIGURE` (or in some cases, `RECONFIGURE WITH OVERRIDE`) statement. Reconfiguring certain options invalidates plans in the plan cache, causing new plans to be compiled. For more information, see [DBCC FREEPROCCACHE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-freeproccache-transact-sql.md).

You can use the `sys.configurations` catalog view to determine the `config_value` (the `value` column) and the `run_value` (the `value_in_use` column), and whether the configuration option requires a [!INCLUDE [ssde-md](../../includes/ssde-md.md)] restart (the `is_dynamic` column).

If SQL Server needs to restart, options show the changed value only in the `value` column. After restart, the new value will appear in both the `value` column and the `value_in_use` column.

Some options require a server restart before the new configuration value takes effect. If you set the new value and run `sp_configure` before restarting the server, the new value appears in the `value` column of the `sys.configurations` catalog view, but not in the `value_in_use` column. When you restart the server, the new value appears in the `value_in_use` column.

> [!NOTE]  
> The `config_value` in the result set of `sp_configure` is equivalent to the `value` column of the `sys.configurations` catalog view, and the `run_value` is equivalent to the `value_in_use` column.

Self-configuring options are options that SQL Server adjusts according to the needs of the system. In most cases, this eliminates the need for setting the values manually. Examples include the **max worker threads** option and the **user connections** option.

The following query can be used to determine if any configured values haven't been installed:

```sql
SELECT *
FROM sys.configurations
WHERE [value] <> [value_in_use];
```

If the value is the change for the configuration option you made but the `value_in_use` isn't the same, either the `RECONFIGURE` command wasn't run or has failed, or the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] must be restarted.

There are two configuration options where the `value` and `value_in_use` might not be the same, which is the expected behavior:

- **max server memory (MB)** - The default configured value of `0` displays as `2147483647` in the `value_in_use` column.

- **min server memory (MB)** - The default configured value of `0` might display as `8` on 32-bit systems, or `16` on 64-bit systems, in the `value_in_use` column. In some cases, if the `value_in_use` shows as `0`, the true `value_in_use` is `8` (32-bit) or `16` (64-bit).

The `is_dynamic` column can be used to determine if the configuration option requires a restart. A value of `1` in the `is_dynamic` column means that, when the `RECONFIGURE` command is run, the new value takes effect immediately. In some cases, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] might not evaluate the new value immediately, but does so in the normal course of its execution. A value of `0` in the `is_dynamic` column means that the changed configuration value doesn't take effect until the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] is restarted, even though the `RECONFIGURE` command was run.

For a configuration option that isn't dynamic there's no way to tell if the `RECONFIGURE` command has been run to apply the configuration change. Before you restart SQL Server to apply the configuration change, run the `RECONFIGURE` command to ensure all configuration changes will take effect when SQL Server next restarts.

## Configuration options

The following table lists all available configuration options, the range of possible settings, the default values, and the supported product ([!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]). Configuration options are marked with letter codes as follows:

- **A** = Advanced options, which should be changed only by an experienced database administrator or a certified SQL Server professional, and which require setting `show advanced options` to `1`.

- **RR** = Options requiring a restart of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

- **RP** = Options that require a restart of the PolyBase Engine.

- **SC** = Self-configuring options.

> [!NOTE]  
> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] was the last version available on a 32-bit operating system.

| Configuration&nbsp;option | Possible values | SQL&nbsp;Server | Azure&nbsp;SQL Managed Instance |
| --- | --- | --- | --- |
| [access check cache bucket count](access-check-cache-server-configuration-options.md) (A) | **Minimum**: `0`<br />**Maximum**: `16384`<br />**Default**: `0` | Yes | Yes |
| [access check cache quota](access-check-cache-server-configuration-options.md) (A) | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes | Yes |
| [Ad Hoc Distributed Queries](ad-hoc-distributed-queries-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [ADR cleaner retry timeout (min)](adr-cleaner-retry-timeout-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `120` | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions | Yes |
| [ADR Preallocation Factor](adr-preallocation-factor-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `4` | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions | Yes |
| [affinity I/O mask](affinity-input-output-mask-server-configuration-option.md) (A, RR) | **Minimum**: `-2147483648`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes (64-bit only) | No |
| [affinity mask](affinity-mask-server-configuration-option.md) (A) | **Minimum**: `-2147483648`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes (64-bit only) | Yes |
| [affinity64 I/O mask](affinity64-input-output-mask-server-configuration-option.md) (A, RR) | **Minimum**: `-2147483648`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes (64-bit only) | Yes |
| [affinity64 mask](affinity64-mask-server-configuration-option.md) (A) | **Minimum**: `-2147483648`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes (64-bit only) | No |
| [Agent XPs](agent-xps-server-configuration-option.md) (A) <sup>1</sup> | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [allow polybase export](allow-polybase-export.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions | No |
| [allow updates](allow-updates-server-configuration-option.md)<br /><br />**Warning:** Obsolete. Don't use. Causes an error during reconfigure. | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [automatic soft-NUMA disabled](soft-numa-sql-server.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [backup checksum default](backup-checksum-default.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [backup compression algorithm](view-or-configure-the-backup-compression-algorithm-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | Yes |
| [backup compression default](view-or-configure-the-backup-compression-default-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1` (prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]), or `2` ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions)<br />**Default**: `0` | Yes | Yes |
| [blocked process threshold (s)](blocked-process-threshold-server-configuration-option.md) (A) | **Minimum**: `5`<br />**Maximum**: `86400`<br />**Default**: `0` | Yes | Yes |
| [c2 audit mode](c2-audit-mode-server-configuration-option.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [clr enabled](clr-enabled-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [clr strict security](clr-strict-security.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions | Yes |
| [column encryption enclave type](configure-column-encryption-enclave-type.md) (RR) | **Minimum**: `0`<br />**Maximum**: `2`<br />**Default**: `0` | Yes | No |
| [common criteria compliance enabled](common-criteria-compliance-enabled-server-configuration-option.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [contained database authentication](contained-database-authentication-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [cost threshold for parallelism](configure-the-cost-threshold-for-parallelism-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `5` | Yes | Yes |
| [cross db ownership chaining](cross-db-ownership-chaining-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [cursor threshold](configure-the-cursor-threshold-server-configuration-option.md) (A) | **Minimum**: `-1`<br />**Maximum**: `2147483647`<br />**Default**: `-1` | Yes | Yes |
| [Database Mail XPs](database-mail-xps-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [default full-text language](configure-the-default-full-text-language-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `1033` | Yes | Yes |
| [default language](configure-the-default-language-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `9999`<br />**Default**: `0` | Yes | Yes |
| [default trace enabled](default-trace-enabled-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `1` | Yes | Yes |
| [disallow results from triggers](disallow-results-from-triggers-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [EKM provider enabled](ekm-provider-enabled-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [external scripts enabled](external-scripts-enabled-server-configuration-option.md) (SC) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions | Yes |
| [filestream access level](filestream-access-level-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `2`<br />**Default**: `0` | Yes | No |
| [fill factor (%)](configure-the-fill-factor-server-configuration-option.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `100`<br />**Default**: `0` | Yes | No |
| [ft crawl bandwidth (max)](ft-crawl-bandwidth-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `100` | Yes | Yes |
| [ft crawl bandwidth (min)](ft-crawl-bandwidth-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `0` | Yes | Yes |
| [ft notify bandwidth (max)](ft-notify-bandwidth-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `100` | Yes | Yes |
| [ft notify bandwidth (min)](ft-notify-bandwidth-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `0` | Yes | Yes |
| [hadoop connectivity](polybase-connectivity-configuration-transact-sql.md) (RP) | **Minimum**: `0`<br />**Maximum**: `7`<br />**Default**: `0` | [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions | Yes |
| [hardware offload enabled](hardware-offload-enable-configuration-option.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | Yes |
| [in-doubt xact resolution](in-doubt-xact-resolution-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `2`<br />**Default**: `0` | Yes | Yes |
| [index create memory (KB)](configure-the-index-create-memory-server-configuration-option.md) (A, SC) | **Minimum**: `704`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes | Yes |
| [lightweight pooling](lightweight-pooling-server-configuration-option.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [locks](configure-the-locks-server-configuration-option.md) (A, RR, SC) | **Minimum**: `5000`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes | No |
| [max degree of parallelism](configure-the-max-degree-of-parallelism-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `0` | Yes | No |
| [max full-text crawl range](max-full-text-crawl-range-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `256`<br />**Default**: `4` | Yes | Yes |
| [max server memory (MB)](server-memory-server-configuration-options.md) (A, SC) | **Minimum**: `16`<br />**Maximum**: `2147483647`<br />**Default**: `2147483647` | Yes | Yes |
| [max text repl size (B)](configure-the-max-text-repl-size-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `65536` | Yes | Yes |
| [max worker threads](configure-the-max-worker-threads-server-configuration-option.md) (A) <sup>2</sup> | **Minimum**: `128`<br />**Maximum**: `32767`<br />**Default**: `0`<br /><br />`2048` is the [recommended maximum](configure-the-max-worker-threads-server-configuration-option.md#recommendations) for 64-bit SQL Server (`1024` for 32-bit) | Yes | Yes |
| [media retention](configure-the-media-retention-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `365`<br />**Default**: `0` | Yes | No |
| [min memory per query (KB)](configure-the-min-memory-per-query-server-configuration-option.md) (A) | **Minimum**: `512`<br />**Maximum**: `2147483647`<br />**Default**: `1024` | Yes | No |
| [min server memory (MB)](server-memory-server-configuration-options.md) (A, SC) | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes | No |
| [nested triggers](configure-the-nested-triggers-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `1` | Yes | Yes |
| [network packet size (B)](configure-the-network-packet-size-server-configuration-option.md) (A) | **Minimum**: `512`<br />**Maximum**: `32767`<br />**Default**: `4096` | Yes | Yes |
| [Ole Automation Procedures](ole-automation-procedures-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [open objects](open-objects-server-configuration-option.md) (A, RR)<br /><br />**Warning:** Obsolete. Don't use. | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes | No |
| [optimize for ad hoc workloads](optimize-for-ad-hoc-workloads-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [PH timeout](ph-timeout-server-configuration-option.md) (A) | **Minimum**: `1`<br />**Maximum**: `3600`<br />**Default**: `60` | Yes | Yes |
| [polybase enabled](../../relational-databases/polybase/polybase-installation.md#enable) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions | No |
| [polybase network encryption](../../relational-databases/polybase/polybase-installation.md#enable) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `1` | Yes | Yes |
| [precompute rank](../discontinued-database-engine-functionality-in-sql-server.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [priority boost](configure-the-priority-boost-server-configuration-option.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [query governor cost limit](configure-the-query-governor-cost-limit-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `0` | Yes | Yes |
| [query wait (s)](configure-the-query-wait-server-configuration-option.md) (A) | **Minimum**: `-1`<br />**Maximum**: `2147483647`<br />**Default**: `-1` | Yes | Yes |
| [recovery interval (min)](configure-the-recovery-interval-server-configuration-option.md) (A, SC) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `0` | Yes | Yes |
| [remote access](configure-the-remote-access-server-configuration-option.md) (RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `1` | Yes | No |
| [remote admin connections](remote-admin-connections-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [remote data archive](configure-the-remote-data-archive-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [remote login timeout (s)](configure-the-remote-login-timeout-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `10` | Yes | Yes |
| [remote proc trans](configure-the-remote-proc-trans-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [remote query timeout (s)](configure-the-remote-query-timeout-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `2147483647`<br />**Default**: `600` | Yes | Yes |
| [Replication XPs](replication-xps-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [scan for startup procs](configure-the-scan-for-startup-procs-server-configuration-option.md) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [server trigger recursion](server-trigger-recursion-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `1` | Yes | Yes |
| [set working set size](set-working-set-size-server-configuration-option.md) (A, RR)<br /><br />**Warning:** Obsolete. Don't use. | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | No |
| [show advanced options](show-advanced-options-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [SMO and DMO XPs](smo-and-dmo-xps-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `1` | Yes | Yes |
| [suppress recovery model errors](suppress-recovery-model-errors-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | No | Yes |
| [tempdb metadata memory-optimized](../../relational-databases/databases/tempdb-database.md#memory-optimized-tempdb-metadata) (A, RR) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions | No |
| [transform noise words](transform-noise-words-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |
| [two digit year cutoff](configure-the-two-digit-year-cutoff-server-configuration-option.md) (A) | **Minimum**: `1753`<br />**Maximum**: `9999`<br />**Default**: `2049` | Yes | Yes |
| [user connections](configure-the-user-connections-server-configuration-option.md) (A, RR, SC) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `0` | Yes | No |
| [user options](configure-the-user-options-server-configuration-option.md) | **Minimum**: `0`<br />**Maximum**: `32767`<br />**Default**: `0` | Yes | Yes |
| [xp_cmdshell](xp-cmdshell-server-configuration-option.md) (A) | **Minimum**: `0`<br />**Maximum**: `1`<br />**Default**: `0` | Yes | Yes |

<sup>1</sup> Changes to `1` when SQL Server Agent is started. Default value is `0` if SQL Server Agent is set to automatic start during Setup.

<sup>2</sup> Zero (`0`) autoconfigures the number of max worker threads depending on the number of logical processors. For more information, see the [automatically configured number of max worker threads](configure-the-max-worker-threads-server-configuration-option.md#recommendations).

## Related content

- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [DBCC FREEPROCCACHE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-freeproccache-transact-sql.md)
