---
title: Performance best practices for SQL Server on Linux
description: This article provides performance best practices and guidelines for running SQL Server on Linux.
author: tejasaks
ms.author: tejasaks
ms.reviewer: vanto, randolphwest
ms.date: 04/19/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Performance best practices and configuration guidelines for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides best practices and recommendations to maximize performance for database applications that connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. These recommendations are specific to running on the Linux platform. All normal [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] recommendations, such as index design, still apply.

The following guidelines contain recommendations for configuring both [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and the Linux operating system (OS). Consider using these configuration settings to experience the best performance for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installation.

- [Storage configuration recommendation](#storage-configuration-recommendation)
- [Kernel and CPU settings for high performance](#kernel-and-cpu-settings-for-high-performance)
- [SQL Server configuration](#sql-server-configuration)

## Storage configuration recommendation

The storage subsystem hosting data, transaction logs, and other associated files (such as checkpoint files for in-memory OLTP) should be capable of managing both average and peak workload gracefully.

### Use storage subsystem with appropriate IOPS, throughput, and redundancy

Normally, in on-premises environments, the storage vendor supports appropriate hardware RAID configuration with striping across multiple disks to ensure appropriate IOPS, throughput, and redundancy. Though, this can differ across different storage vendors and different storage offerings with varying architectures.

For [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux deployed on Azure Virtual Machines, consider using software RAID to ensure appropriate IOPS and throughput requirements are achieved. When configuring [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Azure virtual machines with similar storage considerations, see [Storage configuration for SQL Server VMs](/azure/azure-sql/virtual-machines/windows/storage-configuration).

The following example shows how to create software RAID in Linux on Azure Virtual Machines. Keep in mind that you should use the appropriate number of data disks for the required throughput and IOPS for volumes based on the data, transaction log, and `tempdb` I/O requirements. In the following example, eight data disks were attached to the Azure Virtual Machine; 4 to host data files, 2 for transaction logs, and 2 for `tempdb` workload.

To locate the devices (for example /dev/sdc) for RAID creation, use the `lsblk` command.

```bash
# For Data volume, using 4 devices, in RAID 5 configuration with 8KB stripes
mdadm --create --verbose /dev/md0 --level=raid5 --chunk=8K --raid-devices=4 /dev/sdc /dev/sdd /dev/sde /dev/sdf

# For Log volume, using 2 devices in RAID 10 configuration with 64KB stripes
mdadm --create --verbose /dev/md1 --level=raid10 --chunk=64K --raid-devices=2 /dev/sdg /dev/sdh

# For tempdb volume, using 2 devices in RAID 0 configuration with 64KB stripes
mdadm --create --verbose /dev/md2 --level=raid0 --chunk=64K --raid-devices=2 /dev/sdi /dev/sdj
```

### Disk partitioning and configuration recommendations

For [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you should use a RAID configuration. The deployed filesystem stripe unit (`sunit`) and stripe width should match the RAID geometry. For example, this is an XFS-based example for a log volume.

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

### filesystem configuration recommendation

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] supports both ext4 and XFS filesystems to host the database, transaction logs, and additional files such as checkpoint files for in-memory OLTP in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Microsoft recommends using XFS filesystem for hosting the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data and transaction log files.

Format the volume with the XFS filesystem:

```bash
mkfs.xfs /dev/md0 -f -L datavolume
mkfs.xfs /dev/md1 -f -L logvolume
mkfs.xfs /dev/md2 -f -L tempdb
```

It's possible to configure the XFS filesystem to be case insensitive when creating and formatting the XFS volume. It isn't the frequently used configuration in the Linux ecosystem, but can be used for compatibility reasons.

For example, you can run the following command. `-n version=ci` is used to configure the XFS filesystem to be case insensitive.

```bash
mkfs.xfs /dev/md0 -f -n version=ci -L datavolume
```

#### Persistent memory filesystem recommendation

For the filesystem configuration on Persistent Memory devices, the block allocation for the underlying filesystem should be 2 MB. For more information on this article, review the article [Technical considerations](sql-server-linux-configure-pmem.md#technical-considerations).

#### Open file limitation

Your production environment might require more connections than the default open file limit of 1,024. You can set soft and hard limits of 1,048,576. For example, in [RHEL](https://access.redhat.com/solutions/61334), edit the `/etc/security/limits.d/99-mssql-server.conf` file to have the following values:

```ini
mssql - nofile 1048576
```

> [!NOTE]
> This setting doesn't apply to SQL Server services started by `systemd`. For more information, see [How to set limits for services in RHEL and systemd](https://access.redhat.com/solutions/1257953).

### Disable last accessed date/time on filesystems for SQL Server data and log files

To ensure that the drive(s) attached to the system remount automatically after a restart, add them to the `/etc/fstab` file. You should also use the UUID (Universally Unique Identifier) in `/etc/fstab` to refer to the drive, rather than just the device name (such as `/dev/sdc1`).

Use the `noatime` attribute with any filesystem that stores [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data and log files. Refer to your Linux documentation on how to set this attribute. An example of how to enable `noatime` option for a volume mounted in Azure Virtual Machine follows.

The mount point entry in `/etc/fstab`:

```bash
UUID="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" /data1 xfs rw,attr2,noatime 0 0
```

In the previous example, UUID represents the device that you can find using the `blkid` command.

### SQL Server and Forced Unit Access (FUA) I/O subsystem capability

[!INCLUDE [linux-forced-unit-access](includes/linux-forced-unit-access.md)]

## Kernel and CPU settings for high performance

The following section describes the recommended Linux OS settings related to high performance and throughput for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installation. See your Linux distribution's documentation for the process to configure these settings. You can use [TuneD](https://tuned-project.org) as described, to configure many CPUs and kernel configurations, described in the next section.

### Use *TuneD* to configure kernel settings

For Red Hat Enterprise Linux (RHEL) users, the [TuneD](https://tuned-project.org) throughput-performance profile configures some kernel and CPU settings automatically (except for C-States). Starting with RHEL 8.0, a TuneD profile named `mssql` was codeveloped with Red Hat and offers finer Linux performance-related tunings for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] workloads. This profile includes the RHEL throughput-performance profile, and we present its definitions in this article for your review with other Linux distributions and RHEL releases without this profile.

For SUSE Linux Enterprise Server 12 SP5, Ubuntu 18.04, and Red Hat Enterprise Linux 7.x, the `tuned` package can be installed manually. It can be used to create and configure the `mssql` profile as described in the following section.

#### Proposed Linux settings using a TuneD `mssql` profile

The following example provides a TuneD configuration for SQL Server on Linux.

```ini
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
```

If you are using Linux distributions with kernel versions greater than 4.18, comment the following options as shown; otherwise, uncomment the following options if you are using distributions with kernel versions earlier than 4.18.

```ini
# kernel.sched_latency_ns = 60000000
# kernel.sched_migration_cost_ns = 500000
# kernel.sched_min_granularity_ns = 15000000
# kernel.sched_wakeup_granularity_ns = 2000000
```

To enable this TuneD profile, save these definitions in a `tuned.conf` file under the `/usr/lib/tuned/mssql` folder, and enable the profile using the following commands:

```bash
chmod +x /usr/lib/tuned/mssql/tuned.conf
tuned-adm profile mssql
```

Verify that the profile is active, with the following command:

```bash
tuned-adm active
```

Or:

```bash
tuned-adm list
```

### CPU settings recommendation

The following table provides recommendations for CPU settings:

| Setting | Value | More information |
| --- | --- | --- |
| CPU frequency governor | performance | See the **cpupower** command |
| ENERGY_PERF_BIAS | performance | See the **x86_energy_perf_policy** command |
| min_perf_pct | 100 | See your documentation on Intel p-state |
| C-States | C1 only | See your Linux or system documentation on how to ensure C-States is set to C1 only |

Using TuneD as described earlier automatically configures CPU frequency governor, `ENERGY_PERF_BIAS`, and `min_perf_pct` settings appropriately due to the throughput-performance profile being used as base for the `mssql` profile. C-States parameter must be configured manually according to the documentation provided by Linux or the system distributor.

### Disk settings recommendations

The following table provides recommendations for disk settings:

| Setting | Value | More information |
| --- | --- | --- |
| Disk `readahead` | 4096 | See the `blockdev` command |
| **sysctl** settings | `kernel.sched_min_granularity_ns = 15000000`<br />`kernel.sched_wakeup_granularity_ns = 2000000`<br />`vm.dirty_ratio = 80`<br />`vm.dirty_background_ratio = 3`<br />`vm.swappiness = 1` | See the **sysctl** command |

#### Description

- `vm.swappiness`: This parameter controls relative weight given to swapping out runtime process memory as compared to filesystem cache. The default value for this parameter is 60, which indicates swapping runtime process memory pages as compared to removing filesystem cache pages at ratio of 60:140. Setting the value 1 indicates strong preference for keeping runtime process memory in physical memory at expense of filesystem cache. Since [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] uses buffer pool as a data page cache and strongly prefers to write through to physical hardware bypassing filesystem cache for reliable recovery, aggressive swappiness configuration can be beneficial for high performing and dedicated [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].
You can find additional information at [Documentation for /proc/sys/vm/ - #swappiness](https://www.kernel.org/doc/html/latest/admin-guide/sysctl/vm.html#swappiness)

- `vm.dirty_*`: [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] file write accesses are uncached, satisfying its data integrity requirements. These parameters allow efficient asynchronous write performance and lower the storage I/O effect of Linux caching writes by allowing large enough caching while throttling flushing.

- `kernel.sched_*`: These parameter values represent the current recommendation for tweaking the Completely Fair Scheduling (CFS) algorithm in the Linux Kernel, to improve throughput of network and storage I/O calls with respect to inter-process preemption and resumption of threads.

Using the `mssql` TuneD profile configures the `vm.swappiness`, `vm.dirty_*` and `kernel.sched_*` settings. The disk `readahead` configuration using `blockdev` command is per device and must be performed manually.

### Kernel setting auto NUMA balancing for multi-node NUMA systems

If you install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on a multi-node NUMA system, the following `kernel.numa_balancing` kernel setting is enabled by default. To allow [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to operate at maximum efficiency on a NUMA system, disable auto NUMA balancing on a multi-node NUMA system:

```bash
sysctl -w kernel.numa_balancing=0
```

Using the `mssql` TuneD profile configures the `kernel.numa_balancing` option.

### Kernel settings for virtual address space

The default setting of `vm.max_map_count` (which is 65536) might not be high enough for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installation. For this reason, change the `vm.max_map_count` value to at least 262144 for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] deployment, and refer to the [Proposed Linux settings using a TuneD mssql profile](#proposed-linux-settings-using-a-tuned-mssql-profile) section for further tunings of these kernel parameters. The maximum value for `vm.max_map_count` is 2147483647.

```bash
sysctl -w vm.max_map_count=1600000
```

Using the `mssql` TuneD profile configures the `vm.max_map_count` option.

### Leave Transparent Huge Pages (THP) enabled

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

### Network setting recommendations

Like there are storage and CPU recommendations, there are Network specific recommendations as well listed below for reference. Not all settings in the following examples are available across different NICs. Refer and consult with NIC vendors for guidance for each of these options. Test and configure this on development environments before applying them on production environments. The following options are explained with examples, and the commands used are specific to NIC type and vendor.

1. **Configuring network port buffer size**. In the example below, the NIC is named `eth0`, which is an Intel-based NIC. For Intel based NIC, the recommended buffer size is 4 KB (4096). Verify the preset maximums and then configure it using the following example:

   Check the pre-set maximums with the following command. Replace `eth0` with your NIC name:

   ```bash
   ethtool -g eth0
   ```

   Set both the `rx` (receive) and `tx` (transmit) buffer size to 4 KB:

   ```bash
   ethtool -G eth0 rx 4096 tx 4096
   ```

   Check that the value is properly configured:

   ```bash
   ethtool -g eth0
   ```

1. **Enable jumbo frames**. Before enabling jumbo frames, verify that all the network switches, routers, and anything else essential in the network packet path between the clients and the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] support jumbo frames. Only then, enabling jumbo frames can improve performance. After jumbo frames are enabled, connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and change the network packet size to 8060 using `sp_configure` as shown below:

   ```bash
   # command to set jumbo frame to 9014 for a Intel NIC named eth0 is
   ifconfig eth0 mtu 9014
   # verify the setting using the command:
   ip addr | grep 9014
   ```

   ```sql
   EXEC sp_configure 'network packet size', '8060';
   GO
   RECONFIGURE WITH OVERRIDE;
   GO
   ```

1. By default, we recommend setting the port for adaptive RX/TX IRQ coalescing, meaning interrupt delivery is adjusted to improve latency when packet rate is low and improve throughput when packet rate is high. This setting might not be available across all the different network infrastructure, so review the existing network infrastructure and confirm that this is supported. The example below is for the NIC named `eth0`, which is an Intel-based NIC:

   1. Set the port for adaptive RX/TX IRQ coalescing:

      ```bash
      ethtool -C eth0 adaptive-rx on
      ethtool -C eth0 adaptive-tx on
      ```

   1. Confirm the setting:

      ```bash
      ethtool -c eth0
      ```

   > [!NOTE]  
   > For a predictable behavior for high-performance environments, like environments for benchmarking, disable the adaptive RX/TX IRQ coalescing and then set specifically the RX/TX interrupt coalescing. See the example commands to disable the RX/TX IRQ coalescing and then specifically set the values:

   Disable adaptive RX/TX IRQ coalescing:

   ```bash
   ethtool -C eth0 adaptive-rx off
   ethtool -C eth0 adaptive-tx off
   ```

   Confirm the change:

   ```bash
   ethtool -c eth0
   ```

   Set the `rx-usecs` and `irq` parameters. `rx-usecs` specifies how many microseconds after at least 1 packet is received before generating an interrupt. The `irq` parameter specifies the corresponding delays in updating the status when the interrupt is disabled. For Intel bases NICs, you can use the following settings:

   ```bash
   ethtool -C eth0 rx-usecs 100 tx-frames-irq 512
   ```

   Confirm the change:

   ```bash
   ethtool -c eth0
   ```

1. We also recommend receive-side scaling (RSS) enabled and by default, combining the RX and TX side of RSS queues. There have been specific scenarios, when working with Microsoft Support, where disabling RSS has improved the performance as well. Test this setting in test environments before applying it on production environments. The following example is for Intel NICs.

   Get the preset maximum values:

   ```bash
   ethtool -l eth0
   ```

   Combine the queues with the value reported in the preset "Combined" maximum value. In this example, the value is set to `8`:

   ```bash
   ethtool -L eth0 combined 8
   ```

   Verify the setting:

   ```bash
   ethtool -l eth0
   ```

1. Working with NIC port IRQ affinity. To achieve expected performance by tweaking the IRQ affinity, consider few important parameters like Linux handling of the server topology, NIC driver stack, default settings, and irqbalance setting. Optimizations of the NIC port IRQ affinities settings are done with the knowledge of server topology, disabling the irqbalance, and using the NIC vendor-specific settings.

   The following example of Mellanox specific network infrastructure helps to explain the configuration. For more information, and to download the Mellanox **mlnx** tools, see [​​Performance Tuning tools for Mellanox Network Adapters](https://support.mellanox.com/s/article/MLNX2-117-2523kn). The commands change based on the environment. Contact the NIC vendor for further guidance.

   Disable `irqbalance`, or get a snapshot of the IRQ settings and force the daemon to exit:

   ```bash
   systemctl disable irqbalance.service
   ```

   Or:

   ```bash
   irqbalance --oneshot
   ```

   Make sure that `common_irq_affinity.sh` is executable:

   ```bash
   chmod +x common_irq_affinity.sh
   ```

   Display IRQ affinity for Mellanox NIC port (for example, `eth0`):

   ```bash
   ./show_irq_affinity.sh eth0
   ```

   Optimize for best throughput performance with a Mellanox tool:

   ```bash
   ./mlnx_tune -p HIGH_THROUGHPUT
   ```

   Set hardware affinity to the NUMA node hosting physically the NIC and its port:

   ```bash
   ./set_irq_affinity_bynode.sh `\cat /sys/class/net/eth0/device/numa_node` eth0
   ```

   Verify the IRQ affinity:

   ```bash
   ./show_irq_affinity.sh eth0
   ```

   Add IRQ coalescing optimizations

   ```bash
   ethtool -C eth0 adaptive-rx off
   ethtool -C eth0 adaptive-tx off
   ethtool -C eth0  rx-usecs 750 tx-frames-irq 2048
   ```

   Verify the settings:

   ```bash
   ethtool -c eth0
   ```

1. After the above changes are done, verify the speed of the NIC to ensure it matches the expectation using the following command:

   ```bash
   ethtool eth0 | grep -i Speed
   ```

### Advanced kernel and OS configuration

- For best storage I/O performance, use Linux multiqueue scheduling for block devices, which enables the block layer performance to scale well with fast solid-state drives (SSDs) and multi-core systems. Check the documentation if it is enabled by default in your Linux distribution. In most other cases, booting the kernel with `scsi_mod.use_blk_mq=y` enables it, though documentation of the Linux distribution in use might have further guidance on it. This is consistent with the upstream Linux kernel.

- As multipath I/O is often used for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] deployments, configure the device mapper (DM) multi-queue target to use the `blk-mq` infrastructure, by enabling the `dm_mod.use_blk_mq=y` kernel boot option. The default value is `n` (disabled). This setting, when the underlying SCSI devices are using `blk-mq`, reduces locking overhead at the DM layer. For more information on how to configure multipath I/O, refer to your Linux distribution's documentation.

### Configure swapfile

Ensure you have a properly configured swapfile to avoid any out of memory issues. Consult your Linux documentation for how to create and properly size a swapfile.

### Virtual machines and dynamic memory

If you're running [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux in a virtual machine, make sure you select options to fix the amount of memory reserved for the virtual machine. Don't use features like Hyper-V Dynamic Memory.

## SQL Server configuration

Perform the following configuration tasks after you install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux to achieve best performance for your application.

### Best practices

#### Use PROCESS AFFINITY for node and/or CPUs

Use `ALTER SERVER CONFIGURATION` to set `PROCESS AFFINITY` for all the `NUMANODE`s and/or CPUs you're using for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] (which is typically for all NODEs and CPUs) on a Linux OS. Processor affinity helps maintain efficient Linux and SQL Scheduling behavior. Using the `NUMANODE` option is the simplest method. Use `PROCESS AFFINITY` even if you have only a single NUMA Node on your computer. For more information on how to set `PROCESS AFFINITY`, see the [ALTER SERVER CONFIGURATION](../t-sql/statements/alter-server-configuration-transact-sql.md) article.

#### Configure multiple `tempdb` data files

Because a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux installation doesn't offer an option to configure multiple `tempdb` files, we recommend that you consider creating multiple `tempdb` data files after installation. For more information, see the guidance in the article, [Recommendations to reduce allocation contention in SQL Server tempdb database](https://support.microsoft.com/help/2154845/recommendations-to-reduce-allocation-contention-in-sql-server-tempdb-d).

### Advanced configuration

The following recommendations are optional configuration settings that you might choose to perform after installation of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. These choices are based on the requirements of your workload and configuration of your Linux OS.

#### Set a memory limit with mssql-conf

In order to ensure There's enough free physical memory for the Linux OS, the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] process uses only 80% of the physical RAM by default. For some systems with large amount of physical RAM, 20% might be a significant number. For example, on a system with 1 TB of RAM, the default setting would leave around 200 GB of RAM unused. In this situation, you might want to configure the memory limit to a higher value. See the documentation on the **mssql-conf** tool and the [memory.memorylimitmb](sql-server-linux-configure-mssql-conf.md#memorylimit) setting that controls the memory visible to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] (in units of MB).

When changing this setting, be careful not to set this value too high. If you don't leave enough memory, you could experience problems with the Linux OS and other Linux applications.

## Related content

- [Get started with Performance features](sql-server-linux-performance-get-started.md)
- [Overview of SQL Server on Linux](sql-server-linux-overview.md)
