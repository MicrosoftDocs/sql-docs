---
title: Performance best practices for SQL Server on Linux | Microsoft Docs
description: This article provide performance best practices and guidelines for running SQL Server on Linux.
author: rgward 
ms.author: bobward 
manager: craigg
ms.date: 09/14/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
---

# Performance best practices and configuration guidelines for SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

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
> For Red Hat Enterprise Linux (RHEL) users, the throughput-performance profile will configure these settings automatically (except for C-States).

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
| sysctl settings | kernel.sched_min_granularity_ns = 10000000<br/>kernel.sched_wakeup_granularity_ns = 15000000<br/>vm.dirty_ratio = 40<br/>vm.dirty_background_ratio = 10<br/>vm.swappiness=10 | See the **sysctl** command |

### Kernel setting auto numa balancing for multi-node NUMA systems

If you install SQL Server on a multi-node **NUMA** systems, the following **kernel.numa_balancing** kernel setting is enabled by default. To allow SQL Server to operate at maximum efficiency on a **NUMA** system, disable auto numa balancing on a multi-node NUMA system:

```bash
sysctl -w kernel.numa_balancing=0
```

### Kernel settings for Virtual Address Space

The default setting of **vm.max_map_count** (which is 65536) may not be high enough for a SQL Server installation. Change this value (which is an upper limit) to 256K.

```bash
sysctl -w vm.max_map_count=262144
```

### Disable last accessed date/time on file systems for SQL Server data and log files

Use the **noatime** attribute with any file system that is used to store SQL Server data and log files. Refer to your Linux documentation on how to set this attribute.

### Leave Transparent Huge Pages (THP) enabled

Most Linux installations should have this option on by default. We recommend for the most consistent performance experience to leave this configuration option enabled.

### swapfile

Ensure you have a properly configured swapfile to avoid any out of memory issues. Consult your Linux documentation for how to create and properly size a swapfile.

### Virtual Machines and Dynamic Memory

If you are running SQL Server on Linux in a virtual machine, ensure you select options to fix the amount of memory reserved for the virtual machine. Do not use features like Hyper-V Dynamic Memory.

## Next steps

To learn more about SQL Server features that improve performance, see [Get started with Performance features](sql-server-linux-performance-get-started.md).

For more information about SQL Server on Linux, see [Overview of SQL Server on Linux](sql-server-linux-overview.md).
