---
title: Performance best practices for SQL Server on Linux
description: This article provide performance best practices and guidelines for running SQL Server on Linux.
author: tejasaks 
ms.author: tejasaks
ms.reviewer: vanto
ms.date: 09/16/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
---

# Performance best practices and configuration guidelines for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides best practices and recommendations to maximize performance for database applications that connect to SQL Server on Linux. These recommendations are specific to running on the Linux platform. All normal SQL Server recommendations, such as index design, still apply.

The following guidelines contain recommendations for configuring both SQL Server and the Linux operating system.

## SQL Server configuration

It is recommended to perform the following configuration tasks after you install SQL Server on Linux to achieve best performance for your application.

### Best practices

- **Use PROCESS AFFINITY for Node and/or CPUs**

   It is recommended to use `ALTER SERVER CONFIGURATION` to set `PROCESS AFFINITY` for all the **NUMANODEs** and/or CPUs you are using for SQL Server (which is typically for all NODEs and CPUs) on a Linux Operating System. Processor affinity helps maintain efficient Linux and SQL Scheduling behavior. Using the **NUMANODE** option is the simplest method. Note, you should use **PROCESS AFFINITY** even if you have only a single NUMA Node on your computer.  See the [ALTER SERVER CONFIGURATION](../t-sql/statements/alter-server-configuration-transact-sql.md) documentation for more information on how to set **PROCESS AFFINITY**.

- **Configure multiple tempdb data files**

   Because a SQL Server on Linux installation does not offer an option to configure multiple tempdb files, we recommend that you consider creating multiple tempdb data files after installation. For more information, see the guidance in the article, [Recommendations to reduce allocation contention in SQL Server tempdb database](https://support.microsoft.com/help/2154845/recommendations-to-reduce-allocation-contention-in-sql-server-tempdb-d).

### Advanced Configuration

The following recommendations are optional configuration settings that you may choose to perform after installation of SQL Server on Linux. These choices are based on the requirements of your workload and configuration of your Linux Operating System.

- **Set a memory limit with mssql-conf**

   In order to ensure there is enough free physical memory for the Linux Operating System, the SQL Server process uses only 80% of the physical RAM by default. For some systems which large amount of physical RAM, 20% might be a significant number. For example, on a system with 1 TB of RAM, the default setting would leave around 200 GB of RAM unused. In this situation, you might want to configure the memory limit to a higher value. See the documentation on the **mssql-conf** tool and the [memory.memorylimitmb](sql-server-linux-configure-mssql-conf.md#memorylimit) setting that controls the memory visible to SQL Server (in units of MB).

   When changing this setting, be careful not to set this value too high. If you do not leave enough memory, you could experience problems with the Linux Operating System and other Linux applications.

## Linux OS Configuration

Consider using the following Linux Operating System configuration settings to experience the best performance for a SQL Server Installation.

### Kernel settings for high performance
These are the recommended Linux Operating System settings related to high performance and throughput for a SQL Server installation. See your Linux Operating System documentation for the process to configure these settings.



> [!Note]
> For Red Hat Enterprise Linux (RHEL) users, the [tuned](https://tuned-project.org) throughput-performance profile configure these settings automatically (except for C-States). Starting with RHEL 8.0, a /usr/lib/tuned built-in mssql profile was co-developed with Red Hat and offers finer Linux performance related tunings for SQL Server workloads. This profile includes the RHEL throughput-performance profile and we present its definitions below for your review with other Linux distros and RHEL releases without this profile.

The following table provides recommendations for CPU settings:

| Setting | Value | More information |
|---|---|---|
| CPU frequency governor | performance | See the **cpupower** command |
| ENERGY_PERF_BIAS | performance | See the **x86_energy_perf_policy** command |
| min_perf_pct | 100 | See your documentation on intel p-state |
| C-States | C1 only | See your Linux or system documentation on how to ensure C-States is set to C1 only |

The following table provides recommendations for disk settings:

| Setting | Value | More information |
|---|---|---|
| disk readahead | 4096 | See the **blockdev** command |
| sysctl settings | kernel.sched_min_granularity_ns = 10000000<br/>kernel.sched_wakeup_granularity_ns = 15000000<br/>vm.dirty_ratio = 40<br/>vm.dirty_background_ratio = 10<br/>vm.swappiness = 10 | See the **sysctl** command |

### Kernel setting auto numa balancing for multi-node NUMA systems

If you install SQL Server on a multi-node **NUMA** systems, the following **kernel.numa_balancing** kernel setting is enabled by default. To allow SQL Server to operate at maximum efficiency on a **NUMA** system, disable auto numa balancing on a multi-node NUMA system:

```bash
sysctl -w kernel.numa_balancing=0
```

### Kernel settings for Virtual Address Space

The default setting of **vm.max_map_count** (which is 65536) may not be high enough for a SQL Server installation. For this reason, change the **vm.max_map_count** value to 262144 for a SQL Server deployment, and refer to the [Proposed Linux settings using a tuned mssql profile](#proposed-linux-settings-using-a-tuned-mssql-profile) section for further tunings of these kernel parameters. The max value for vm.max_map_count is 2147483647.

```bash
sysctl -w vm.max_map_count=262144
```

### Proposed Linux settings using a tuned mssql profile

```bash
#
# A tuned configuration for SQL Server on Linux
#
    
[main]
summary=Optimize for Microsoft SQL Server
include=throughput-performance
    
[cpu]
force_latency=5

[sysctl]
vm.swappiness = 1
vm.dirty_background_ratio = 3
vm.dirty_ratio = 80
vm.dirty_expire_centisecs = 500
vm.dirty_writeback_centisecs = 100
vm.transparent_hugepages=always
# For multi-instance SQL deployments, use
# vm.transparent_hugepages=madvice
vm.max_map_count=1600000
net.core.rmem_default = 262144
net.core.rmem_max = 4194304
net.core.wmem_default = 262144
net.core.wmem_max = 1048576
kernel.numa_balancing=0
kernel.sched_latency_ns = 60000000
kernel.sched_migration_cost_ns = 500000
kernel.sched_min_granularity_ns = 15000000
kernel.sched_wakeup_granularity_ns = 2000000
```

To enable this tuned profile, save these definitions in a **tuned.conf** file under a /usr/lib/tuned/mssql folder and enable the profile using

```bash
chmod +x /usr/lib/tuned/mssql/tuned.conf
tuned-adm profile mssql
```

Verify its enabling with

```bash
tuned-adm active
```
or
```bash
tuned-adm list
```

### Disable last accessed date/time on file systems for SQL Server data and log files

Use the **noatime** attribute with any file system that is used to store SQL Server data and log files. Refer to your Linux documentation on how to set this attribute.

### Leave Transparent Huge Pages (THP) enabled

Most Linux installations should have this option on by default. We recommend for the most consistent performance experience to leave this configuration option enabled. However, in case of high memory paging activity in SQL Server deployments with multiple instances for example or SQL Server execution with other memory demanding applications on the server, we suggest testing your applications performance after executing the following command 

```bash
echo madvise > /sys/kernel/mm/transparent_hugepage/enabled
```
or modifying the mssql tuned profile with the line

```bash
vm.transparent_hugepages=madvise
```
and make the mssql profile active after the modification
```bash
tuned-adm off
tuned-adm profile mssql
```

### swapfile

Ensure you have a properly configured swapfile to avoid any out of memory issues. Consult your Linux documentation for how to create and properly size a swapfile.

### Virtual Machines and Dynamic Memory

If you are running SQL Server on Linux in a virtual machine, ensure you select options to fix the amount of memory reserved for the virtual machine. Do not use features like Hyper-V Dynamic Memory.

## Next steps

To learn more about SQL Server features that improve performance, see [Get started with Performance features](sql-server-linux-performance-get-started.md).

For more information about SQL Server on Linux, see [Overview of SQL Server on Linux](sql-server-linux-overview.md).
