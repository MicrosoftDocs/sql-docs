---
title: "Server Configuration Options (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "02/29/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
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
ms.assetid: 9f38eba6-39b1-4f1d-ba24-ee4f7e2bc969
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Server Configuration Options (SQL Server)
  You can manage and optimize [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources through configuration options by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the sp_configure system stored procedure. The most commonly used server configuration options are available through [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]; all configuration options are accessible through sp_configure. Consider the effects on your system carefully before setting these options. For more information, see [View or Change Server Properties &#40;SQL Server&#41;](view-or-change-server-properties-sql-server.md).  
  
> [!IMPORTANT]  
>  Advanced options should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technician.  
  
## Categories of Configuration Options  
 Configuration options take effect either:  
  
-   Immediately after setting the option and issuing the RECONFIGURE (or in some cases, RECONFIGURE WITH OVERRIDE) statement.  
  
     -or-  
  
-   After performing the above actions and restarting the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Options that require [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to restart will initially show the changed value only in the value column. After restart, the new value will appear in both the value column and the value_in_use column.  
  
 Some options require a server restart before the new configuration value takes effect. If you set the new value and run sp_configure before restarting the server, the new value appears in the configuration options value column, but not in the value_in_use column. After restarting the server, the new value appears in the value_in_use column.  
  
 Self-configuring options are those that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adjusts according to the needs of the system. In most cases, this eliminates the need for setting the values manually. Examples include the min server memory and max server memory options and the user connections option.  
  
## Configuration Options Table  
 The following table lists all available configuration options, the range of possible settings, and default values. Configuration options are marked with letter codes as follows:  
  
-   A= Advanced options, which should be changed only by an experienced database administrator or a certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technician, and which require setting show advanced options to 1.  
  
-   RR = Options requiring a restart of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   SC = Self-configuring options.  
  
    |Configuration option|Minimum value|Maximum value|Default|  
    |--------------------------|-------------------|-------------------|-------------|  
    |[access check cache bucket count](access-check-cache-server-configuration-options.md) (A)|0|16384|0|  
    |[access check cache quota](access-check-cache-server-configuration-options.md) (A)|0|2147483647|0|  
    |[ad hoc distributed queries](ad-hoc-distributed-queries-server-configuration-option.md) (A)|0|1|0|  
    |[affinity I/O mask](affinity-input-output-mask-server-configuration-option.md) (A, RR)|-2147483648|2147483647|0|  
    |[affinity64 I/O mask](affinity64-input-output-mask-server-configuration-option.md) (A, only available on 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)])|-2147483648|2147483647|0|  
    |[affinity mask](affinity-mask-server-configuration-option.md) (A)|-2147483648|2147483647|0|  
    |[affinity64 mask](affinity64-mask-server-configuration-option.md) (A, RR), only available on 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|-2147483648|2147483647|0|  
    |[Agent XPs](agent-xps-server-configuration-option.md) (A)|0|1|0<br /><br /> (Changes to 1 when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is started. Default value is 0 if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is set to automatic start during Setup.)|  
    |[allow updates](allow-updates-server-configuration-option.md) (Obsolete. Do not use. Will cause an error during reconfigure.)|0|1|0|  
    |[backup checksum default](../backup-checksum-default.md)|0|1|0|  
    |[backup compression default](view-or-configure-the-backup-compression-default-server-configuration-option.md)|0|1|0|  
    |[blocked process threshold](blocked-process-threshold-server-configuration-option.md) (A)|0|86400|0|  
    |[c2 audit mode](c2-audit-mode-server-configuration-option.md) (A, RR)|0|1|0|  
    |[clr enabled](clr-enabled-server-configuration-option.md)|0|1|0|  
    |[common criteria compliance enabled](common-criteria-compliance-enabled-server-configuration-option.md) (A, RR)|0|1|0|  
    |[contained database authentication](contained-database-authentication-server-configuration-option.md)|0||0|  
    |[cost threshold for parallelism](configure-the-cost-threshold-for-parallelism-server-configuration-option.md) (A)|0|32767|5|  
    |[cross db ownership chaining](cross-db-ownership-chaining-server-configuration-option.md)|0|1|0|  
    |[cursor threshold](configure-the-cursor-threshold-server-configuration-option.md) (A)|-1|2147483647|-1|  
    |[Database Mail XPs](database-mail-xps-server-configuration-option.md) (A)|0|1|0|  
    |[default full-text language](configure-the-default-full-text-language-server-configuration-option.md) (A)|0|2147483647|1033|  
    |[default language](configure-the-default-language-server-configuration-option.md)|0|9999|0|  
    |[default trace enabled](default-trace-enabled-server-configuration-option.md) (A)|0|1|1|  
    |[disallow results from triggers](disallow-results-from-triggers-server-configuration-option.md) (A)|0|1|0|  
    |[EKM provider enabled](ekm-provider-enabled-server-configuration-option.md)|0|1|0|  
    |[filestream_access_level](filestream-access-level-server-configuration-option.md)|0|2|0|  
    |[fill factor](configure-the-fill-factor-server-configuration-option.md) (A, RR)|0|100|0|  
    |ft crawl bandwidth (max), see [ft crawl bandwidth](ft-crawl-bandwidth-server-configuration-option.md)(A)|0|32767|100|  
    |ft crawl bandwidth (min), see [ft crawl bandwidth](ft-crawl-bandwidth-server-configuration-option.md)(A)|0|32767|0|  
    |ft notify bandwidth (max), see [ft notify bandwidth](ft-notify-bandwidth-server-configuration-option.md)(A)|0|32767|100|  
    |ft notify bandwidth (min), see [ft notify bandwidth](ft-notify-bandwidth-server-configuration-option.md)(A)|0|32767|0|  
    |[index create memory](configure-the-index-create-memory-server-configuration-option.md) (A, SC)|704|2147483647|0|  
    |[in-doubt xact resolution](in-doubt-xact-resolution-server-configuration-option.md) (A)|0|2|0|  
    |[lightweight pooling](lightweight-pooling-server-configuration-option.md) (A, RR)|0|1|0|  
    |[locks](configure-the-locks-server-configuration-option.md) (A, RR, SC)|5000|2147483647|0|  
    |[max degree of parallelism](configure-the-max-degree-of-parallelism-server-configuration-option.md) (A)|0|32767|0|  
    |[max full-text crawl range](max-full-text-crawl-range-server-configuration-option.md) (A)|0|256|4|  
    |[max server memory](server-memory-server-configuration-options.md) (A, SC)|16|2147483647|2147483647|  
    |[max text repl size](configure-the-max-text-repl-size-server-configuration-option.md)|0|2147483647|65536|  
    |[max worker threads](configure-the-max-worker-threads-server-configuration-option.md) (A)|128|32767<br /><br /> (1024 is the maximum recommended for 32-bit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], 2048 for 64-bit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].)|0<br /><br /> Zero auto-configures the number of max worker threads depending on the number of processors, using the formula (256+(*\<processors>* -4) * 8) for 32-bit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and twice that for 64-bit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
    |[media retention](configure-the-media-retention-server-configuration-option.md) (A, RR)|0|365|0|  
    |[min memory per query](configure-the-min-memory-per-query-server-configuration-option.md) (A)|512|2147483647|1024|  
    |[min server memory](server-memory-server-configuration-options.md) (A, SC)|0|2147483647|0|  
    |[nested triggers](configure-the-nested-triggers-server-configuration-option.md)|0|1|1|  
    |[network packet size](configure-the-network-packet-size-server-configuration-option.md) (A)|512|32767|4096|  
    |[Ole Automation Procedures](ole-automation-procedures-server-configuration-option.md) (A)|0|1|0|  
    |[open objects](open-objects-server-configuration-option.md) (A, RR, obsolete)|0|2147483647|0|  
    |[optimize for ad hoc workloads](optimize-for-ad-hoc-workloads-server-configuration-option.md) (A)|0|1|0|  
    |[PH_timeout](ph-timeout-server-configuration-option.md) (A)|1|3600|60|  
    |[precompute rank](precompute-rank-server-configuration-option.md) (A)|0|1|0|  
    |[priority boost](configure-the-priority-boost-server-configuration-option.md) (A, RR)|0|1|0|  
    |[query governor cost limit](configure-the-query-governor-cost-limit-server-configuration-option.md) (A)|0|2147483647|0|  
    |[query wait](configure-the-query-wait-server-configuration-option.md) (A)|-1|2147483647|-1|  
    |[recovery interval](configure-the-recovery-interval-server-configuration-option.md) (A, SC)|0|32767|0|  
    |[remote access](configure-the-remote-access-server-configuration-option.md) (RR)|0|1|1|  
    |[remote admin connections](remote-admin-connections-server-configuration-option.md)|0|1|0|  
    |[remote login timeout](configure-the-remote-login-timeout-server-configuration-option.md)|0|2147483647|10|  
    |[remote proc trans](configure-the-remote-proc-trans-server-configuration-option.md)|0|1|0|  
    |[remote query timeout](configure-the-remote-query-timeout-server-configuration-option.md)|0|2147483647|600|  
    |[Replication XPs Option](replication-xps-server-configuration-option.md) (A)|0|1|0|  
    |[scan for startup procs](configure-the-scan-for-startup-procs-server-configuration-option.md) (A, RR)|0|1|0|  
    |[server trigger recursion](server-trigger-recursion-server-configuration-option.md)|0|1|1|  
    |[set working set size](set-working-set-size-server-configuration-option.md) (A, RR, obsolete)|0|1|0|  
    |[show advanced options](show-advanced-options-server-configuration-option.md)|0|1|0|  
    |[SMO and DMO XPs](smo-and-dmo-xps-server-configuration-option.md) (A)|0|1|1|  
    |[transform noise words](transform-noise-words-server-configuration-option.md) (A)|0|1|0|  
    |[two digit year cutoff](configure-the-two-digit-year-cutoff-server-configuration-option.md) (A)|1753|9999|2049|  
    |[user connections](configure-the-user-connections-server-configuration-option.md) (A, RR, SC)|0|32767|0|  
    |[user options](configure-the-user-options-server-configuration-option.md)|0|32767|0|  
    |[xp_cmdshell](xp-cmdshell-server-configuration-option.md) (A)|0|1|0|  
  
## See Also  
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)   
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)  
  
  
