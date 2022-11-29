---
title: Performance best practices for SQL Server on Linux
description: This article provides performance best practices and guidelines for running SQL Server on Linux.
author: tejasaks
ms.author: tejasaks
ms.reviewer: vanto, randolphwest
ms.date: 11/24/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# Performance best practices and configuration guidelines for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides best practices and recommendations to maximize performance for database applications that connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. These recommendations are specific to running on the Linux platform. All normal [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] recommendations, such as index design, still apply.

The following guidelines contain recommendations for configuring both [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and the Linux Operating System (OS).

## Linux OS configuration

Consider using the following Linux OS configuration settings to experience the best performance for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Installation.

### Storage configuration recommendation

#### Use storage subsystem with appropriate IOPS, throughput, and redundancy

The storage subsystem hosting data, transaction logs, and other associated files (such as checkpoint files for in-memory OLTP) should be capable of managing both average and peak workload gracefully. Normally, in on-premises environments, the storage vendor support appropriate hardware RAID configuration with striping across multiple disks to ensure appropriate IOPS, throughput, and redundancy. Though, this can differ across different storage vendors and different storage offerings with varying architectures.

For [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux deployed on Azure Virtual Machines, consider using software RAID to ensure appropriate IOPS and throughput requirements are achieved. When configuring [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Azure virtual machines with similar storage considerations, see [Storage configuration for SQL Server VMs](/azure/azure-sql/virtual-machines/windows/storage-configuration).

The following is an example of how to create software raid in Linux on Azure Virtual Machines. An example is provided below, but you should use the appropriate number of data disks for the required throughput and IOPS for volumes based on the data, transaction log, and `tempdb` IO requirements. In this example, eight data disks were attached to the Azure Virtual Machine; 4 to host data files, 2 for transaction logs, and 2 for `tempdb` workload.

```bash
# To locate the devices (for example /dev/sdc) for RAID creation, use the lsblk command
# For Data volume, using 4 devices, in RAID 5 configuration with 8KB stripes
mdadm --create --verbose /dev/md0 --level=raid5 --chunk=8K --raid-devices=4 /dev/sdc /dev/sdd /dev/sde /dev/sdf

# For Log volume, using 2 devices in RAID 10 configuration with 64KB stripes
mdadm --create --verbose /dev/md1 --level=raid10 --chunk=64K --raid-devices=2 /dev/sdg /dev/sdh

# For tempdb volume, using 2 devices in RAID 0 configuration with 64KB stripes
mdadm --create --verbose /dev/md2 --level=raid0 --chunk=64K --raid-devices=2 /dev/sdi /dev/sdj
```

#### Disk partitioning and configuration recommendations

For [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], it is recommended to use RAID configurations. The deployed filesystem stripe unit (sunit) and stripe width should match the RAID geometry. Here is an XFS filesystem-based example for a log volume.

```bash
# Creating a log volume, using 6 devices, in RAID 10 configuration with 64KB stripes
mdadm --create --verbose /dev/md3 --level=raid10 --chunk=64K --raid-devices=6 /dev/sda /dev/sdb /dev/sdc /dev/sdd /dev/sde /dev/sdf

mkfs.xfs /dev/md3 -f -L log
meta-data=/dev/md3               isize=512    agcount=32, agsize=18287648 blks
         =                       sectsz=4096  attr=2, projid32bit=1
         =                       crc=1        finobt=1, sparse=1, rmapbt=0
         =                       reflink=1
data     =                       bsize=4096   blocks=585204384, imaxpct=5
         =                       sunit=16     swidth=48 blks
naming   =version 2              bsize=4096   ascii-ci=0, ftype=1
log      =internal log           bsize=4096   blocks=285744, version=2
         =                       sectsz=4096  sunit=1 blks, lazy-count=1
realtime =none                   extsz=4096   blocks=0, rtextents=0
```

The log array is a 6-drive RAID-10 with a 64-KB stripe. As you can see:

- For `sunit=16 blks`, 16 * 4096 block size = 64 KB, matches the stripe size.
- For `swidth=48 blks`, `swidth` / `sunit` = 3, which is the number of data drives in the array, excluding parity drives.

#### File system configuration recommendation

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] supports both EXT4 and XFS file systems to host the database, transaction logs, and additional files such as checkpoint files for in-memory OLTP in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Microsoft recommends using XFS file system for hosting the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data and transaction log files.

```bash
# Formatting the volume with XFS filesystem
mkfs.xfs /dev/md0 -f -L datavolume
mkfs.xfs /dev/md1 -f -L logvolume
mkfs.xfs /dev/md2 -f -L tempdb
```

> [!NOTE]  
> It's possible to configure the XFS file system to be case insensitive when creating and formatting the XFS volume. It is not the frequently used configuration in Linux ecosystem but can be used for compatibility reasons.
>  
> For example: `mkfs.xfs /dev/md0 -f -n version=ci -L datavolume`
>  
> In the example, parameters `-n version=ci` are used to configure the XFS filesystem to be case insensitive.

##### Persistent memory filesystem recommendation

For the filesystem configuration on Persistent Memory devices, the block allocation for the underlying filesystem should be 2 MB. For more information on this topic, review the article [Technical considerations](sql-server-linux-configure-pmem.md#technical-considerations).

##### Open file limitation

The default open file limit is often set at 1024. Your production environment may require more connections than the default limit. We recommend you set a soft limit of 16000, and a hard limit of 32727. For example, in [RHEL](https://access.redhat.com/solutions/61334), edit the `/etc/security/limits.d/99-mssql-server.conf` file to have the following values:

```ini
mssql hard nofile 32727
mssql soft nofile 16000
```

#### Disable last accessed date/time on file systems for SQL Server data and log files

To ensure that the drive(s) attached to the system are remounted automatically after a reboot, they must be added to the `/etc/fstab` file. It's also highly recommended that the UUID (Universally Unique Identifier) is used in `/etc/fstab` to refer to the drive rather than just the device name (such as `/dev/sdc1`).

Use of `noatime` attribute with any file system that is used to store [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data and log files is highly recommended. Refer to your Linux documentation on how to set this attribute. An example of how to enable `noatime` option for a volume mounted in Azure Virtual Machine is below.

The mount point entry in `/etc/fstab`:

```bash
UUID="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" /data1 xfs rw,attr2,noatime 0 0
```

In the example above, UUID represents the device that you can find using the **blkid** command.

#### SQL Server and Forced Unit Access (FUA) I/O subsystem capability

Certain versions of supported Linux distributions provide support for FUA I/O subsystem capability, which provides data durability. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] uses FUA capability to provide highly efficient and reliable I/O for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] workloads. For more information on FUA support by Linux distribution and its effect on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see [SQL Server On Linux: Forced Unit Access (FUA) Internals](https://techcommunity.microsoft.com/t5/sql-server-blog/sql-server-on-linux-forced-unit-access-fua-internals/ba-p/3199102).

SUSE Linux Enterprise Server 12 SP5 and Red Hat Enterprise Linux 8.0 onwards support FUA capability in the I/O subsystem. If you're using [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU6 and later versions, you should use following configuration for high performing and efficient I/O implementation with FUA by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

Use the recommended configuration listed below if the following conditions are met.

- Using [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU6 and later versions
- Using a Linux distribution and version that supports FUA capability (Red Hat Enterprise Linux 8.0 or higher, or SUSE Linux Enterprise Server 12 SP5)
- On storage subsystem and/or hardware that supports and is configured for FUA capability

Recommended configuration:

1. Enable Trace Flag 3979 as a startup parameter
1. Use **mssql-conf** to configure `control.writethrough = 1` and `control.alternatewritethrough = 0`

For almost all other configuration that doesn't meet the previous conditions, the recommended configuration is as follows:

1. Enable Trace Flag 3982 as a startup parameter (which is the default for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] in the Linux ecosystem), while ensuring Trace Flag 3979 isn't enabled as a startup parameter
1. Use **mssql-conf** to configure `control.writethrough = 1` and `control.alternatewritethrough = 1`

### Kernel and CPU settings for high performance

The following section describes the recommended Linux OS settings related to high performance and throughput for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installation. See your Linux OS documentation for the process to configure these settings. Using [TuneD](https://tuned-project.org) as described helps configure many CPUs and kernel configurations described below.

#### Use *TuneD* to configure kernel settings

For Red Hat Enterprise Linux (RHEL) users, the [TuneD](https://tuned-project.org) throughput-performance profile configures some kernel and CPU settings automatically (except for C-States). Starting with RHEL 8.0, a TuneD profile named `mssql` was codeveloped with Red Hat and offers finer Linux performance-related tunings for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] workloads. This profile includes the RHEL throughput-performance profile, and we present its definitions below for your review with other Linux distributions and RHEL releases without this profile.

For SUSE Linux Enterprise Server 12 SP5, Ubuntu 18.04, and Red Hat Enterprise Linux 7.x, the `tuned` package can be installed manually. It can be used to create and configure the `mssql` profile as described below.

##### Proposed Linux settings using a TuneD `mssql` profile

```bash
#
# A TuneD configuration for SQL Server on Linux
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
#Note: If you are using Linux distributions with kernel versions greater than 4.18, please comment the following options as shown; otherwise, uncomment the following options if you are using distributions with kernel versions less than 4.18.
# kernel.sched_latency_ns = 60000000
# kernel.sched_migration_cost_ns = 500000
# kernel.sched_min_granularity_ns = 15000000
# kernel.sched_wakeup_granularity_ns = 2000000
```

To enable this TuneD profile, save these definitions in a `tuned.conf` file under a `/usr/lib/tuned/mssql` folder, and enable the profile using the following commands:

```bash
chmod +x /usr/lib/tuned/mssql/tuned.conf
tuned-adm profile mssql
```

Verify it's enabled with the following command:

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
| --- | --- | --- |
| CPU frequency governor | performance | See the **cpupower** command |
| ENERGY_PERF_BIAS | performance | See the **x86_energy_perf_policy** command |
| min_perf_pct | 100 | See your documentation on intel p-state |
| C-States | C1 only | See your Linux or system documentation on how to ensure C-States is set to C1 only |

Using TuneD as described earlier automatically configures CPU frequency governor, `ENERGY_PERF_BIAS`, and `min_perf_pct` settings appropriately due to the throughput-performance profile being used as base for the `mssql` profile. C-States parameter must be configured manually according to the documentation provided by Linux or the system distributor.

#### Disk settings recommendations

The following table provides recommendations for disk settings:

| Setting | Value | More information |
| --- | --- | --- |
| Disk `readahead` | 4096 | See the `blockdev` command |
| **sysctl** settings | kernel.sched_min_granularity_ns = 15000000<br />kernel.sched_wakeup_granularity_ns = 2000000<br />vm.dirty_ratio = 80<br />vm.dirty_background_ratio = 3<br />vm.swappiness = 1 | See the **sysctl** command |

**Description:**

- `vm.swappiness`: This parameter controls relative weight given to swapping out runtime process memory as compared to filesystem cache. The default value for this parameter is 60, which indicates swapping runtime process memory pages as compared to removing filesystem cache pages at ratio of 60:140. Setting the value 1 indicates strong preference for keeping runtime process memory in physical memory at expense of filesystem cache. Since [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] uses buffer pool as a data page cache and strongly prefers to write through to physical hardware bypassing filesystem cache for reliable recovery, aggressive swappiness configuration can be beneficial for high performing and dedicated [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].
You can find additional information at [Documentation for /proc/sys/vm/ - #swappiness](https://www.kernel.org/doc/html/latest/admin-guide/sysctl/vm.html#swappiness)

- `vm.dirty_*`: [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] file write accesses are uncached, satisfying its data integrity requirements. These parameters allow efficient asynchronous write performance and lower the storage IO impact of Linux caching writes by allowing large enough caching while throttling flushing.

- `kernel.sched_*`: These parameter values represent the current recommendation for tweaking the Completely Fair Scheduling (CFS) algorithm in the Linux Kernel, to improve throughput of network and storage IO calls with respect to inter-process preemption and resumption of threads.

Using the `mssql` TuneD profile configures the `vm.swappiness`, `vm.dirty_*` and `kernel.sched_*` settings. The disk `readahead` configuration using `blockdev` command is per device and must be performed manually.

#### Kernel setting auto NUMA balancing for multi-node NUMA systems

If you install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on a multi-node NUMA system, the following `kernel.numa_balancing` kernel setting is enabled by default. To allow [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to operate at maximum efficiency on a NUMA system, disable auto NUMA balancing on a multi-node NUMA system:

```bash
sysctl -w kernel.numa_balancing=0
```

Using the `mssql` TuneD profile configures the `kernel.numa_balancing` option.

#### Kernel settings for virtual address space

The default setting of `vm.max_map_count` (which is 65536) may not be high enough for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installation. For this reason, change the `vm.max_map_count` value to at least 262144 for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] deployment, and refer to the [Proposed Linux settings using a TuneD mssql profile](#proposed-linux-settings-using-a-tuned-mssql-profile) section for further tunings of these kernel parameters. The max value for vm.max_map_count is 2147483647.

```bash
sysctl -w vm.max_map_count=1600000
```

Using the `mssql` TuneD profile configures the `vm.max_map_count` option.

#### Leave Transparent Huge Pages (THP) enabled

Most Linux installations should have this option on by default. We recommend for the most consistent performance experience to leave this configuration option enabled. However, if there is high memory paging activity in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] deployments with multiple instances, for example, or [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] execution with other memory demanding applications on the server, we suggest testing your applications performance after executing the following command:

```bash
echo madvise > /sys/kernel/mm/transparent_hugepage/enabled
```

Or modify the `mssql` TuneD profile with the line:

```bash
vm.transparent_hugepages=madvise
```

And make the `mssql` profile is active after the modification:

```bash
tuned-adm off
tuned-adm profile mssql
```

Using the `mssql` TuneD profile configures the `transparent_hugepage` option.

#### Network setting recommendations

Like there are storage and CPU recommendations, there are Network specific recommendations as well listed below for reference. Not all settings mentioned below are available across different NICs. Refer and consult with NIC vendors for guidance for each of these options. Test and configure this on development environments before applying them on production environments. The options mentioned below are explained with examples, and the commands used are specific to NIC type and vendor.

1. Configuring network port buffer size: In the example below, the NIC is named 'eth0', which is an Intel-based NIC. For Intel based NIC, the recommended buffer size is 4 KB (4096). Verify the pre-set maximums and then configure it using the sample commands shown below:

   ```bash
   #To check the pre-set maximums please run the command, example NIC name used here is:"eth0"
   ethtool -g eth0
   #command to set both the rx (receive) and tx (transmit) buffer size to 4 KB.
   ethtool -G eth0 rx 4096 tx 4096
   #command to check the value is properly configured is:
   ethtool -g eth0
   ```

1. Enable jumbo frames: Before enabling jumbo frames, verify that all the network switches, routers, and anything else essential in the network packet path between the clients and the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] support jumbo frames. Only then, enabling jumbo frames can improve performance. After jumbo frames are enabled, connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and change the network packet size to 8060 using `sp_configure` as shown below:

   ```bash
   #command to set jumbo frame to 9014 for a Intel NIC named eth0 is
   ifconfig eth0 mtu 9014
   #verify the setting using the command:
   ip addr | grep 9014
   ```

   ```sql
   EXEC sp_configure 'network packet size', '8060';
   GO
   RECONFIGURE WITH OVERRIDE;
   GO
   ```

1. By default, we recommend setting the port for adaptive RX/TX IRQ coalescing, meaning interrupt delivery will be adjusted to improve latency when packet rate is low and improve throughput when packet rate is high. This setting might not be available across all the different network infrastructure, so review the existing network infrastructure and confirm that this is supported. The example below is for the NIC named 'eth0', which is an intel-based NIC:

   ```bash
   #command to set the port for adaptive RX/TX IRQ coalescing
   ethtool -C eth0 adaptive-rx on
   ethtool -C eth0 adaptive-tx on
   #confirm the setting using the command:
   ethtool -c eth0
   ```

   > [!NOTE]  
   > For a predictable behavior for high-performance environments, like environments for benchmarking, disable the adaptive RX/TX IRQ coalescing and then set specifically the RX/TX interrupt coalescing. See the example commands to disable the RX/TX IRQ coalescing and then specifically set the values:

   ```bash
   #commands to disable adaptive RX/TX IRQ coalescing
   ethtool -C eth0 adaptive-rx off
   ethtool -C eth0 adaptive-tx off
   #confirm the setting using the command:
   ethtool -c eth0
   #Let us set the rx-usecs parameter which specify how many microseconds after at least 1 packet is received before generating an interrupt, and the [irq] parameters are the corresponding delays in updating the #status when the interrupt is disabled. For Intel bases NICs below are good values to start with:
   ethtool -C eth0 rx-usecs 100 tx-frames-irq 512
   #confirm the setting using the command:
   ethtool -c eth0
   ```

1. We also recommend RSS (Receive-Side Scaling) enabled and by default, combining the RX and TX side of RSS queues. There have been specific scenarios where when working with Microsoft Support, disabling RSS has improved the performance as well. Test this setting in test environments before applying it on production environments. The example command shown below is for Intel NICs.

   ```bash
   #command to get pre-set maximums
   ethtool -l eth0
   #note the pre-set "Combined" maximum value. let's consider for this example, it is 8.
   #command to combine the queues with the value reported in the pre-set "Combined" maximum value:
   ethtool -L eth0 combined 8
   #you can verify the setting using the command below
   ethtool -l eth0
   ```

1. Working with NIC port IRQ affinity. To achieve expected performance by tweaking the IRQ affinity, consider few important parameters like Linux handling of the server topology, NIC driver stack, default settings, and irqbalance setting. Optimizations of the NIC port IRQ affinities settings are done with the knowledge of server topology, disabling the irqbalance, and using the NIC vendor-specific settings.

    Below is an example of Mellanox specific network infrastructure to help explain the configuration. For more information, see [​​Performance Tuning tools for Mellanox Network Adapters](https://support.mellanox.com/s/article/MLNX2-117-2523kn). The commands will change based on the environment. Contact the NIC vendor for further guidance:

   ```bash
   #disable irqbalance or get a snapshot of the IRQ settings and force the daemon to exit
   systemctl disable irqbalance.service
   #or
   irqbalance --oneshot

   #download the Mellanox mlnx tools -- see https://support.mellanox.com/s/article/MLNX2-117-2523kn

   #be sure, common_irq_affinity.sh is executable. if not,
   #chmod +x common_irq_affinity.sh

   #display IRQ affinity for Mellanox NIC port; e.g eth0
   ./show_irq_affinity.sh eth0

   #optimize for best throughput performance with a Mellanox tool
   ./mlnx_tune -p HIGH_THROUGHPUT

   #set hardware affinity to the NUMA node hosting physically the NIC and its port
   ./set_irq_affinity_bynode.sh `\cat /sys/class/net/eth0/device/numa_node` eth0

   #verify IRQ affinity
   ./show_irq_affinity.sh eth0

   #add IRQ coalescing optimizations
   ethtool -C eth0 adaptive-rx off
   ethtool -C eth0 adaptive-tx off
   ethtool -C eth0  rx-usecs 750 tx-frames-irq 2048

   #verify the settings
   ethtool -c eth0
   ```

1. After the above changes are done, verify the speed of the NIC to ensure it matches the expectation using the following command:

   ```bash
   ethtool eth0 | grep -i Speed
   ```

#### Additional advanced kernel/OS configuration

- For best storage IO performance, the use of Linux multiqueue scheduling for block devices is recommended. This enables the block layer performance to scale well with fast solid-state drives (SSDs) and multi-core systems. Check the documentation if it is enabled by default in your Linux distributions. In most other cases, booting the kernel with `scsi_mod.use_blk_mq=y` enables it, though documentation of the Linux distribution in use may have additional guidance on it. This is consistent to the upstream Linux kernel.

- As multipath IO is often used for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] deployments, the device mapper (DM) multi-queue target should also be configured to use the `blk-mq` infrastructure by enabling the `dm_mod.use_blk_mq=y` kernel boot option. The default value is `n` (disabled). This setting, when the underlying SCSI devices are using `blk-mq`, reduces locking overhead at the DM layer. Refer to the documentation of the Linux distribution in use for additional guidance on how to configure it.

#### Configure swapfile

Ensure you have a properly configured swapfile to avoid any out of memory issues. Consult your Linux documentation for how to create and properly size a swapfile.

#### Virtual machines and dynamic memory

If you're running [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux in a virtual machine, make sure you select options to fix the amount of memory reserved for the virtual machine. Don't use features like Hyper-V Dynamic Memory.

## SQL Server configuration

It is recommended to perform the following configuration tasks after you install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux to achieve best performance for your application.

### Best practices

- **Use PROCESS AFFINITY for node and/or CPUs**

  It's recommended to use `ALTER SERVER CONFIGURATION` to set `PROCESS AFFINITY` for all the `NUMANODE`s and/or CPUs you're using for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] (which is typically for all NODEs and CPUs) on a Linux OS. Processor affinity helps maintain efficient Linux and SQL Scheduling behavior. Using the `NUMANODE` option is the simplest method. Use `PROCESS AFFINITY` even if you have only a single NUMA Node on your computer. For more information on how to set `PROCESS AFFINITY`, see the [ALTER SERVER CONFIGURATION](../t-sql/statements/alter-server-configuration-transact-sql.md) article.

- **Configure multiple `tempdb` data files**

  Because a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux installation doesn't offer an option to configure multiple `tempdb` files, we recommend that you consider creating multiple `tempdb` data files after installation. For more information, see the guidance in the article, [Recommendations to reduce allocation contention in SQL Server tempdb database](https://support.microsoft.com/help/2154845/recommendations-to-reduce-allocation-contention-in-sql-server-tempdb-d).

### Advanced configuration

The following recommendations are optional configuration settings that you may choose to perform after installation of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. These choices are based on the requirements of your workload and configuration of your Linux OS.

- **Set a memory limit with mssql-conf**

  In order to ensure There's enough free physical memory for the Linux OS, the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] process uses only 80% of the physical RAM by default. For some systems with large amount of physical RAM, 20% might be a significant number. For example, on a system with 1 TB of RAM, the default setting would leave around 200 GB of RAM unused. In this situation, you might want to configure the memory limit to a higher value. See the documentation on the **mssql-conf** tool and the [memory.memorylimitmb](sql-server-linux-configure-mssql-conf.md#memorylimit) setting that controls the memory visible to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] (in units of MB).

  When changing this setting, be careful not to set this value too high. If you don't leave enough memory, you could experience problems with the Linux OS and other Linux applications.

## Next steps

- [Get started with Performance features](sql-server-linux-performance-get-started.md)
- [Overview of SQL Server on Linux](sql-server-linux-overview.md)
