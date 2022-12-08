---
title: Server Configuration Options (SQL Server)
description: Find out how to manage and optimize SQL Server resources. View available configuration options, possible settings, default values, and restart requirements.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 09/14/2022
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

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You can manage and optimize SQL Server resources through configuration options by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the `sp_configure` system stored procedure. The most commonly used server configuration options are available through [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]; all configuration options are accessible through `sp_configure`. Consider the effects on your system carefully before setting these options. For more information, see [View or Change Server Properties &#40;SQL Server&#41;](view-or-change-server-properties-sql-server.md).

> [!IMPORTANT]  
> Advanced options should be changed only by an experienced database administrator or certified SQL Server technician.

## Categories of configuration options

If you don't see the effect of a configuration change, it may not be installed. Check to see that the `run_value` of the configuration option has changed.

Configuration options take effect either:

- Immediately after setting the option and issuing the `RECONFIGURE` (or in some cases, `RECONFIGURE WITH OVERRIDE`) statement. Reconfiguring certain options will invalidate plans in the plan cache, causing new plans to be compiled. For more information, see [DBCC FREEPROCCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-freeproccache-transact-sql.md).

  \- or -

- After performing the above actions and restarting the instance of SQL Server.

You can use the `sys.configurations` catalog view to determine the `config_value` (the `value` column) and the `run_value` (the `value_in_use` column), and whether the configuration option requires a [!INCLUDE [ssde-md](../../includes/ssde-md.md)] restart (the `is_dynamic` column).

If SQL Server needs to restart, options will initially show the changed value only in the `value` column. After restart, the new value will appear in both the `value` column and the `value_in_use` column.

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

- **max server memory (MB)** - The default configured value of `0` will display as `2147483647` in the `value_in_use` column.

- **min server memory (MB)** - The default configured value of `0` might display as `8` on 32-bit systems, or `16` on 64-bit systems,  in the `value_in_use` column. In some cases, if the `value_in_use` shows as `0`, the true `value_in_use` is `8` (32-bit) or `16` (64-bit).

The `is_dynamic` column can be used to determine if the configuration option requires a restart. A value of `1` in the `is_dynamic` column means that, when the `RECONFIGURE` command is run, the new value will take effect immediately. In some cases, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] might not evaluate the new value immediately but will do so in the normal course of its execution. A value of `0` in the `is_dynamic` column means that the changed configuration value won't take effect until the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] is restarted, even though the `RECONFIGURE` command was run.

For a configuration option that isn't dynamic there is no way to tell if the `RECONFIGURE` command has been run to apply the configuration change. Before you restart SQL Server to apply the configuration change, run the `RECONFIGURE` command to ensure all configuration changes will take effect when SQL Server next restarts.

## Configuration options

The following table lists all available configuration options, the range of possible settings, and default values. Configuration options are marked with letter codes as follows:

- **A** = Advanced options, which should be changed only by an experienced database administrator or a certified SQL Server professional, and which require setting `show advanced options` to `1`.

- **RR** = Options requiring a restart of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

- **RP** = Options that require a restart of the PolyBase Engine.

- **SC** = Self-configuring options.

| Configuration&nbsp;option | Minimum value | Maximum value | Default |
|--|--|--|--|
| [access check cache bucket count](access-check-cache-server-configuration-options.md) (A) | 0 | 16384 | 0 |
| [access check cache quota](access-check-cache-server-configuration-options.md) (A) | 0 | 2147483647 | 0 |
| [ad hoc distributed queries](ad-hoc-distributed-queries-server-configuration-option.md) (A) | 0 | 1 | 0 |
| [ADR cleaner retry timeout (min)](adr-cleaner-retry-timeout-configuration-option.md)<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later.| 0 | 32767 | 15 |
| [ADR Preallocation Factor](adr-preallocation-factor-server-configuration-option.md)<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later.| 0 | 32767 | 4 |
| [affinity I/O mask](affinity-input-output-mask-server-configuration-option.md) (A, RR) | -2147483648 | 2147483647 | 0 |
| [affinity mask](affinity-mask-server-configuration-option.md) (A) | -2147483648 | 2147483647 | 0 |
| [affinity64 I/O mask](affinity64-input-output-mask-server-configuration-option.md) (A, only available on 64-bit version of SQL Server) | -2147483648 | 2147483647 | 0 |
| [affinity64 mask](affinity64-mask-server-configuration-option.md) (A, RR), only available on 64-bit version of SQL Server | -2147483648 | 2147483647 | 0 |
| [Agent XPs](agent-xps-server-configuration-option.md) (A) | 0 | 1 | 0<br /><br />(Changes to 1 when SQL Server Agent is started. Default value is 0 if SQL Server Agent is set to automatic start during Setup.) |
| [allow polybase export](allow-polybase-export.md)<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later.| 0 | 1 | 0 |
| [allow updates](allow-updates-server-configuration-option.md) (Obsolete. Don't use. Will cause an error during reconfigure.) | 0 | 1 | 0 |
| [automatic soft-NUMA disabled](soft-numa-sql-server.md) | 0 | 1 | 0 |
| [backup checksum default](backup-checksum-default.md) | 0 | 1 | 0 |
| [backup compression default](view-or-configure-the-backup-compression-default-server-configuration-option.md) | 0 | 1 - versions prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]<br /><br />2 - [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later | 0 |
| [backup compression algorithm](view-or-configure-the-backup-compression-algorithm-server-configuration-option.md) (A)<br /><br />**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later. | 0 | 1 | 0 |
| [blocked process threshold](blocked-process-threshold-server-configuration-option.md) (A) | 5 | 86400 | 0 |
| [c2 audit mode](c2-audit-mode-server-configuration-option.md) (A, RR) | 0 | 1 | 0 |
| [clr enabled](clr-enabled-server-configuration-option.md) | 0 | 1 | 0 |
| [clr strict security](clr-strict-security.md) (A)<br /><br />**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later.| 0 | 1 | 0 |
| [column encryption enclave type](configure-column-encryption-enclave-type.md) (A, RR) | 0 | 2 | 0 |
| [common criteria compliance enabled](common-criteria-compliance-enabled-server-configuration-option.md) (A, RR) | 0 | 1 | 0 |
| [contained database authentication](contained-database-authentication-server-configuration-option.md) | 0 | 1 | 0 |
| [cost threshold for parallelism](configure-the-cost-threshold-for-parallelism-server-configuration-option.md) (A) | 0 | 32767 | 5 |
| [cross db ownership chaining](cross-db-ownership-chaining-server-configuration-option.md) | 0 | 1 | 0 |
| [cursor threshold](configure-the-cursor-threshold-server-configuration-option.md) (A) | -1 | 2147483647 | -1 |
| [Database Mail XPs](database-mail-xps-server-configuration-option.md) (A) | 0 | 1 | 0 |
| [default full-text language](configure-the-default-full-text-language-server-configuration-option.md) (A) | 0 | 2147483647 | 1033 |
| [default language](configure-the-default-language-server-configuration-option.md) | 0 | 9999 | 0 |
| [default trace enabled](default-trace-enabled-server-configuration-option.md) (A) | 0 | 1 | 1 |
| [disallow results from triggers](disallow-results-from-triggers-server-configuration-option.md) (A) | 0 | 1 | 0 |
| [EKM provider enabled](ekm-provider-enabled-server-configuration-option.md) | 0 | 1 | 0 |
| [external scripts enabled](external-scripts-enabled-server-configuration-option.md) (SC) (RR)<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later.| 0 | 1 | 0 |
| [filestream access level](filestream-access-level-server-configuration-option.md) | 0 | 2 | 0 |
| [fill factor](configure-the-fill-factor-server-configuration-option.md) (A, RR) | 0 | 100 | 0 |
| [ft crawl bandwidth (max)](ft-crawl-bandwidth-server-configuration-option.md)(A) | 0 | 32767 | 100 |
| [ft crawl bandwidth (min)](ft-crawl-bandwidth-server-configuration-option.md)(A) | 0 | 32767 | 0 |
| [ft notify bandwidth (max)](ft-notify-bandwidth-server-configuration-option.md)(A) | 0 | 32767 | 100 |
| [ft notify bandwidth (min)](ft-notify-bandwidth-server-configuration-option.md)(A) | 0 | 32767 | 0 |
| [hardware offload enabled](hardware-offload-enable-configuration-option.md) (A) <br /><br />**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later. | 0 | 1  | 0 |
| [hadoop connectivity](polybase-connectivity-configuration-transact-sql.md) (RP)<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later.| 0 | 7 | 0 |
| [in-doubt xact resolution](in-doubt-xact-resolution-server-configuration-option.md) (A) | 0 | 2 | 0 |
| [index create memory](configure-the-index-create-memory-server-configuration-option.md) (A, SC) | 704 | 2147483647 | 0 |
| [lightweight pooling](lightweight-pooling-server-configuration-option.md) (A, RR) | 0 | 1 | 0 |
| [locks](configure-the-locks-server-configuration-option.md) (A, RR, SC) | 5000 | 2147483647 | 0 |
| [max degree of parallelism](configure-the-max-degree-of-parallelism-server-configuration-option.md) (A) | 0 | 32767 | 0 |
| [max full-text crawl range](max-full-text-crawl-range-server-configuration-option.md) (A) | 0 | 256 | 4 |
| [max server memory](server-memory-server-configuration-options.md) (A, SC) | 16 | 2147483647 | 2147483647 |
| [max text repl size](configure-the-max-text-repl-size-server-configuration-option.md) | 0 | 2147483647 | 65536 |
| [max worker threads](configure-the-max-worker-threads-server-configuration-option.md) (A) | 128 | 32767<br /><br />1024 is the maximum recommended for 32-bit SQL Server, and 2048 for 64-bit SQL Server.<br /><br />**Note:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] was the last version available on a 32-bit operating system. | 0<br /><br />Zero auto-configures the number of max worker threads depending on the number of logical processors, using the formula (256 + (*\<logical processors>* - 4) * 8) for 32-bit SQL Server and (512 + (*\<logical processors>* - 4) * 8) for 64-bit SQL Server.<br /><br />**Note:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] was the last version available on a 32-bit operating system. |
| [media retention](configure-the-media-retention-server-configuration-option.md) (A, RR) | 0 | 365 | 0 |
| [min memory per query](configure-the-min-memory-per-query-server-configuration-option.md) (A) | 512 | 2147483647 | 1024 |
| [min server memory](server-memory-server-configuration-options.md) (A, SC) | 0 | 2147483647 | 0 |
| [nested triggers](configure-the-nested-triggers-server-configuration-option.md) | 0 | 1 | 1 |
| [network packet size](configure-the-network-packet-size-server-configuration-option.md) (A) | 512 | 32767 | 4096 |
| [Ole Automation Procedures](ole-automation-procedures-server-configuration-option.md) (A) | 0 | 1 | 0 |
| [open objects](open-objects-server-configuration-option.md) (A, RR, obsolete) | 0 | 2147483647 | 0 |
| [optimize for ad hoc workloads](optimize-for-ad-hoc-workloads-server-configuration-option.md) (A) | 0 | 1 | 0 |
| [PH_timeout](ph-timeout-server-configuration-option.md) (A) | 1 | 3600 | 60 |
| [polybase enabled](../../relational-databases/polybase/polybase-installation.md#enable) (RR)<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later.| 0 | 1 | 0 |
| [polybase network encryption](../../relational-databases/polybase/polybase-installation.md#enable) | 0 | 1 | 1 |
| [precompute rank](../discontinued-database-engine-functionality-in-sql-server.md) (A) | 0 | 1 | 0 |
| [priority boost](configure-the-priority-boost-server-configuration-option.md) (A, RR) | 0 | 1 | 0 |
| [query governor cost limit](configure-the-query-governor-cost-limit-server-configuration-option.md) (A) | 0 | 2147483647 | 0 |
| [query wait](configure-the-query-wait-server-configuration-option.md) (A) | -1 | 2147483647 | -1 |
| [recovery interval](configure-the-recovery-interval-server-configuration-option.md) (A, SC) | 0 | 32767 | 0 |
| [remote access](configure-the-remote-access-server-configuration-option.md) (RR) | 0 | 1 | 1 |
| [remote admin connections](remote-admin-connections-server-configuration-option.md) | 0 | 1 | 0 |
| [remote data archive](configure-the-remote-data-archive-server-configuration-option.md) | 0 | 1 | 0 |
| [remote login timeout](configure-the-remote-login-timeout-server-configuration-option.md) | 0 | 2147483647 | 10 |
| [remote proc trans](configure-the-remote-proc-trans-server-configuration-option.md) | 0 | 1 | 0 |
| [remote query timeout](configure-the-remote-query-timeout-server-configuration-option.md) | 0 | 2147483647 | 600 |
| [Replication XPs Option](replication-xps-server-configuration-option.md) (A) | 0 | 1 | 0 |
| [scan for startup procs](configure-the-scan-for-startup-procs-server-configuration-option.md) (A, RR) | 0 | 1 | 0 |
| [server trigger recursion](server-trigger-recursion-server-configuration-option.md) | 0 | 1 | 1 |
| [set working set size](set-working-set-size-server-configuration-option.md) (A, RR, obsolete) | 0 | 1 | 0 |
| [show advanced options](show-advanced-options-server-configuration-option.md) | 0 | 1 | 0 |
| [SMO and DMO XPs](smo-and-dmo-xps-server-configuration-option.md) (A) | 0 | 1 | 1 |
| [suppress recovery model errors](suppress-recovery-model-errors-server-configuration-option.md) (A)<br /><br />**Applies to:** [!INCLUDE [ssazuremi_md](../../includes/ssazuremi_md.md)].| 0 | 1 | 0 |
| [tempdb metadata memory-optimized](../../relational-databases/databases/tempdb-database.md#memory-optimized-tempdb-metadata) (A)<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later.| 0 | 1 | 0 |
| [transform noise words](transform-noise-words-server-configuration-option.md) (A) | 0 | 1 | 0 |
| [two digit year cutoff](configure-the-two-digit-year-cutoff-server-configuration-option.md) (A) | 1753 | 9999 | 2049 |
| [user connections](configure-the-user-connections-server-configuration-option.md) (A, RR, SC) | 0 | 32767 | 0 |
| [user options](configure-the-user-options-server-configuration-option.md) | 0 | 32767 | 0 |
| [xp_cmdshell](xp-cmdshell-server-configuration-option.md) (A) | 0 | 1 | 0 |

## See also

- [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [DBCC FREEPROCCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-freeproccache-transact-sql.md)
