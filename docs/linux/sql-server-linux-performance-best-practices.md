---
title: Performance best practices for SQL Server on Linux
description: This article provide performance best practices and guidelines for running SQL Server on Linux.
author: tejasaks 
ms.author: tejasaks
ms.reviewer: vanto
ms.date: 10/13/2020
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

### Storage configuration recommendation

#### Use storage subsystem with appropriate IOPS, throughput and redundancy

The storage subsystem hosting data, transaction log and other associated files (such as checkpoint files for in-memory OLTP) should be capable of managing both average and peak workload gracefully. Normally in on-premise environments the storage vendor support appropriate hardware RAID configuration with striping across multiple disks to ensure appropriate IOPS, Throughput and redundancy. Though this differs across different storage vendors and different storage offerings with varying architectures.

For SQL Server on Linux deployed on Azure Virtual Machines, you should consider using software RAID to ensure appropriate IOPS and Throughput requirements are achieved. Please refer to following article when configuring SQL Server on Azure virtual machines for similar storage considerations.

[Storage configuration for SQL Server VMs](https://docs.microsoft.com/en-us/azure/azure-sql/virtual-machines/windows/storage-configuration)

Following is an example of how to create software raid in Linux on Azure Virtual Machines. Please note that following is just an example and you should use appropriate number of data disks for the required throughput and IOPS for volumes for based on the data, transaction log and tempdb IO requirements. In this example, 8 data disks were attached to the Azure Virtual Machine - 4 to host data files, 2 for transaction log and 2 for tempdb workload.

```bash
# To locate the devices (for example /dev/sdc) for RAID creation, use the lsblk command
# For Data volume, using 4 devices, in RAID 5 configuration with 8KB stripes
mdadm --create --verbose /dev/md0 --level=raid5 --chunk=8K --raid-devices=4 /dev/sdc /dev/sdd /dev/sde /dev/sdf

# For Log volume, using 2 devices in RAID 10 configuration with 64KB stripes
mdadm --create --verbose /dev/md1 --level=raid10 --chunk=64K --raid-devices=2 /dev/sdg /dev/sdh


# For tempdb volume, using 2 devices in RAID 0 configuration with 64KB stripes
mdadm --create --verbose /dev/md2 --level=raid0 --chunk=64K --raid-devices=2 /dev/sdi /dev/sdj
```

#### File System Configuration recommendation

SQL Server supports both EXT4 and XFS file systems to host the database, transaction log and additional files such as checkpoint files for in-memory OLTP in SQL Server. Microsoft recommends using XFS file system for hosting the SQL Server data and transaction log files.

```bash
# Formatting the volume with XFS filesystem
mkfs.xfs /dev/md0 -f -L datavolume
mkfs.xfs /dev/md1 -f -L logvolume
mkfs.xfs /dev/md2 -f -L tempdb
```

> [!NOTE]
> It is possible to configure the XFS file system to be case insensitive when creating and formatting the XFS volume. It is not the frequently used configuration in Linux ecosystem but can be used for compatibility reasons.
>
> Example: mkfs.xfs /dev/md0 -f -n version=ci -L datavolume
> 
> In the example, parameters "-n version=ci" are used to configure the XFS filesystem to be case insensitive.

##### Persistent Memory filesystem recommendation

For the filesystem configuration on Persistent Memory devices, the block allocation for the underlying filesystem should be 2MB. For additional information on this topic, please review following article.

[Technical considerations](sql-server-linux-configure-pmem.md#technical-considerations)

#### Disable last accessed date/time on file systems for SQL Server data and log files

To ensure that the drive(s) attached to the system are remounted automatically after a reboot, they must be added to the /etc/fstab file. It is also highly recommended that the UUID (Universally Unique Identifier) is used in /etc/fstab to refer to the drive rather than just the device name (such as, /dev/sdc1).

Use of **noatime** attribute with any file system that is used to store SQL Server data and log files is highly recommended. Refer to your Linux documentation on how to set this attribute. An example of how to enable **noatime** option for a volume mounted in Azure Virtual Machine is below.

The mount point entry in ***/etc/fstab***

```bash
UUID="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" /data1  xfs     rw,attr2,noatime  0 0
```

In the example above, UUID represents the device which you can find using ***blkid*** command.

#### SQL Server and FUA I/O subsystem capability

There are certain versions of supported Linux distributions which provide support for FUA I/O subsystem capability to provide data durability. SQL Server uses FUA capability to provide highly efficient and reliable I/O for SQL server workload. For additional information on FUA support by Linux distribution and it's impact for SQL Server, please read following blog.

[SQL Server On Linux: Forced Unit Access (Fua) Internals](https://bobsql.com/sql-server-on-linux-forced-unit-access-fua-internals/)

SUSE Linux Enterprise Server 12 SP5 and Red Hat Enterprise Linux 8.0 onwards support FUA capability in the I/O subsystem. If you are using SQL Server 2017 CU6 and above or SQL Server 2019, you should use following configuration for high performing and efficient I/O implementation with FUA by SQL Server.

Use recommended configuration listed below if following conditions are met.

- Using SQL Server 2017 CU6 or newer, or SQL Server 2019
- Using a Linux distribution and version that supports FUA capability, (Red Hat Enterprise Linux 8.0 or higher or SUSE Linux Enterprise Server 12 SP5)
- On storage subsystem and/or hardware that supports and is configured for FUA capability

Recommended configuration:
1. Enable trace flag 3979 as a startup parameter
2. Use mssql-conf to configure control.writethrough = 1 and control.alternatewritethrough = 0

For almost all other configuration that does not meet previous conditions, the recommended configuration is as following.

1. Enable trace flag 3982 as a startup parameter (which is default behavior for SQL Server in Linux ecosystem), while ensuring trace flag 3979 is not enabled as a startup parameter
2. Use mssql-conf to configure control.writethrough = 1 and control.alternatewritethrough = 1

### Kernel and CPU settings for high performance

Following section describes the recommended Linux Operating System settings related to high performance and throughput for a SQL Server installation. See your Linux Operating System documentation for the process to configure these settings. Using ***tuned*** as described helps configure many CPU and kernel configuration described below.

#### Using ***tuned*** to configure Kernel settings

 For Red Hat Enterprise Linux (RHEL) users, the [tuned](https://tuned-project.org) throughput-performance profile configure some kernel and CPU settings automatically (except for C-States). Starting with RHEL 8.0, a ***tuned*** profile named **mssql** was co-developed with Red Hat and offers finer Linux performance related tunings for SQL Server workloads. This profile includes the RHEL throughput-performance profile and we present its definitions below for your review with other Linux distros and RHEL releases without this profile.

 For SUSE Linux Enterprise Server 12 SP5, Ubuntu 18.04 and Red Hat Enterprise Linux 7.x, the ***tuned*** package can be installed manually. It can be used to create and configure the **mssql** profile as described below.

##### Proposed Linux settings using a tuned mssql profile

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
# vm.transparent_hugepages=madvise
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

Verify its enabled with,

```bash
tuned-adm active
```
or
```bash
tuned-adm list
```

#### CPU settings recommendation

The following table provides recommendations for CPU settings:

| Setting | Value | More information |
|---|---|---|
| CPU frequency governor | performance | See the **cpupower** command |
| ENERGY_PERF_BIAS | performance | See the **x86_energy_perf_policy** command |
| min_perf_pct | 100 | See your documentation on intel p-state |
| C-States | C1 only | See your Linux or system documentation on how to ensure C-States is set to C1 only |

Using ***tuned*** as described earlier automatically configures CPU frequency governor, ENERGY_PERF_BIAS and min_perf_pct settings appropriately due to the throughput-performance profile being used as base for **mssql** profile. C-States parameter must be configured manually according to the documentation provided by Linux or system distributor.

#### Disk settings recommendations

The following table provides recommendations for disk settings:

| Setting | Value | More information |
|---|---|---|
| disk readahead | 4096 | See the **blockdev** command |
| sysctl settings | kernel.sched_min_granularity_ns = 10000000<br/>kernel.sched_wakeup_granularity_ns = 15000000<br/>vm.dirty_ratio = 40<br/>vm.dirty_background_ratio = 10<br/>vm.swappiness = 1 | See the **sysctl** command |

**Description:**

**vm.swappiness**:  this parameters controls relative weight given to swapping out runtime memory by limiting the the kernel to swap out SQL Server process memory pages.

**vm.dirty_\***: SQL Server file write accesses are uncached satisfying its data integrity requirements. These parameters allow efficient asynchronous write performance and lower the storage IO impact of Linux caching writes by allowing large enough caching while throttling flushing.

**kernel.sched_\***: These parameter values represent the current recommendation for tweaking the Completely Fair Scheduling (CFS) algorithm in Linux Kernel, to improve throughput of network and storage IO calls with respect to inter-process preemption and resumption of threads.

Using the **mssql** ***tuned*** profile configures the **vm.swappiness**, **vm.dirty_\*** and **kernel.sched_\*** settings. The disk readahead configuration using ***blockdev*** command are per device and must be performed manually.

#### Kernel setting auto numa balancing for multi-node NUMA systems

If you install SQL Server on a multi-node **NUMA** systems, the following **kernel.numa_balancing** kernel setting is enabled by default. To allow SQL Server to operate at maximum efficiency on a **NUMA** system, disable auto numa balancing on a multi-node NUMA system:

```bash
sysctl -w kernel.numa_balancing=0
```

Using the **mssql** ***tuned*** profile configures the **kernel.numa_balancing** option.

#### Kernel settings for Virtual Address Space

The default setting of **vm.max_map_count** (which is 65536) may not be high enough for a SQL Server installation. For this reason, change the **vm.max_map_count** value to at least 262144 for a SQL Server deployment, and refer to the [Proposed Linux settings using a tuned mssql profile](#proposed-linux-settings-using-a-tuned-mssql-profile) section for further tunings of these kernel parameters. The max value for vm.max_map_count is 2147483647.

```bash
sysctl -w vm.max_map_count=1600000
```

Using the **mssql** ***tuned*** profile configures the **vm.max_map_count** option.

#### Leave Transparent Huge Pages (THP) enabled

Most Linux installations should have this option on by default. We recommend for the most consistent performance experience to leave this configuration option enabled. However, in case of high memory paging activity in SQL Server deployments with multiple instances for example or SQL Server execution with other memory demanding applications on the server, we suggest testing your applications performance after executing the following command 

```bash
echo madvise > /sys/kernel/mm/transparent_hugepage/enabled
```
or modifying the **mssql** ***tuned*** profile with the line

```bash
vm.transparent_hugepages=madvise
```
and make the **mssql** active after the modification
```bash
tuned-adm off
tuned-adm profile mssql
```

Using the **mssql** ***tuned*** profile configures the **transparent_hugepage** option.

#### Additional advanced Kernel/OS configuration

1. For best storage IO performance, the use of Linux multiqueue scheduling for block devices is recommended. This enables the block layer performance to scale well with fast solid-state drives (SSDs) and multi-core systems. Please check the documentation if it is enableed by default in your Linux distributions. In most other cases booting the kernel with **scsi_mod.use_blk_mq=y** enables it, though documentation of the Linux distribution in use may have additional guidance on it. This is consistent to the upstream Linux kernel.
2. As multipath IO is often used for SQL Server deployments, the device mapper (DM) multipath target should also be configured to use the blk-mq infrastructure by enabling the **dm_mod.use_blk_mq=y** kernel boot option. The default value is n (disabled). This setting, when the underlying SCSI devices are using blk-mq, reduces locking overhead at the DM layer. Please refer to the documentation of the Linux distribution in use for additional guidance on how to configure it.

#### Configure swapfile

Ensure you have a properly configured swapfile to avoid any out of memory issues. Consult your Linux documentation for how to create and properly size a swapfile.

#### Virtual Machines and Dynamic Memory

If you are running SQL Server on Linux in a virtual machine, ensure you select options to fix the amount of memory reserved for the virtual machine. Do not use features like Hyper-V Dynamic Memory.

## Next steps

To learn more about SQL Server features that improve performance, see [Get started with Performance features](sql-server-linux-performance-get-started.md).

For more information about SQL Server on Linux, see [Overview of SQL Server on Linux](sql-server-linux-overview.md).
