---
title: "Performance Best Practices: Storage, Kernel, CPU, and Network for SQL Server on Linux"
description: Storage, kernel, CPU, and network configuration recommendations for SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 05/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: best-practice
ms.custom:
  - linux-related-content
ai-usage: ai-assisted
---
# Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This article covers operating system and hardware configuration recommendations to maximize performance for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux, including storage, kernel, CPU, and network settings.

> [!NOTE]  
> For memory configuration and container memory limits, see [Performance best practices: SQL Server memory on Linux](performance-best-practices-sql-server-memory.md).

- [Storage configuration recommendation](#storage-configuration-recommendation)
- [Kernel and CPU settings for high performance](#kernel-and-cpu-settings-for-high-performance)
- [SQL Server configuration](#sql-server-configuration)

## Storage configuration recommendation

The storage subsystem that hosts data, transaction logs, and other associated files (such as checkpoint files for in-memory OLTP) should manage both average and peak workloads gracefully.

### Use storage subsystem with appropriate IOPS, throughput, and redundancy

In on-premises environments, the storage vendor normally supports appropriate hardware RAID configuration with striping across multiple disks to ensure appropriate IOPS, throughput, and redundancy. However, this support can differ across different storage vendors and different storage offerings with varying architectures.

For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux deployed on Azure Virtual Machines, consider using software RAID to ensure appropriate IOPS and throughput. For storage considerations when configuring [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Azure virtual machines, see [Configure storage for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/storage-configuration).

The following example shows how to create software RAID in Linux on an Azure Virtual Machine. Use the appropriate number of data disks for the required throughput and IOPS for volumes based on the data, transaction log, and `tempdb` I/O requirements. In the following example, eight data disks are attached to the VM: four to host data files, two for transaction logs, and two for `tempdb` workload.

To locate the devices (for example, `/dev/sdc`) for RAID creation, use the `lsblk` command.

```bash
# For Data volume, using 4 devices, in RAID 5 configuration with 8KB stripes
mdadm --create --verbose /dev/md0 --level=raid5 --chunk=8K --raid-devices=4 /dev/sdc /dev/sdd /dev/sde /dev/sdf

# For Log volume, using 2 devices in RAID 10 configuration with 64KB stripes
mdadm --create --verbose /dev/md1 --level=raid10 --chunk=64K --raid-devices=2 /dev/sdg /dev/sdh

# For tempdb volume, using 2 devices in RAID 0 configuration with 64KB stripes
mdadm --create --verbose /dev/md2 --level=raid0 --chunk=64K --raid-devices=2 /dev/sdi /dev/sdj
```

### Disk partitioning and configuration recommendations

For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], use a RAID configuration. The deployed filesystem stripe unit (`sunit`) and stripe width match the RAID geometry. For example, the following example shows an XFS-based configuration for a log volume.

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

The log array is a six-drive RAID-10 with a 64-KB stripe. As you see:

- For `sunit=16 blks`, 16 * 4096 block size = 64 KB, matches the stripe size.
- For `swidth=48 blks`, `swidth` / `sunit` = 3, which is the number of data drives in the array, excluding parity drives.

### Recommended file system configuration

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] supports both ext4 and XFS filesystems to host the database, transaction logs, and other files such as checkpoint files for in-memory OLTP in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Use the XFS filesystem for hosting [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data and transaction log files.

Format the volume using the XFS filesystem:

```bash
mkfs.xfs /dev/md0 -f -L datavolume
mkfs.xfs /dev/md1 -f -L logvolume
mkfs.xfs /dev/md2 -f -L tempdb
```

You can configure the XFS filesystem to be case insensitive when you create and format the XFS volume. This configuration isn't frequently used in the Linux ecosystem but you can use it for compatibility reasons.

For example, run the following command. Use `-n version=ci` to configure the XFS filesystem to be case insensitive.

```bash
mkfs.xfs /dev/md0 -f -n version=ci -L datavolume
```

#### Persistent memory filesystem recommendation

For the filesystem configuration on Persistent Memory devices, set the block allocation for the underlying filesystem to 2 MB. For more information, see [Technical considerations](../sql-server-linux-configure-pmem.md#technical-considerations).

#### Open file limitation

Your production environment might require more connections than the default open file limit of `1024` (1,024). You can set soft and hard limits to `1048576` (1,048,576). For example, in [RHEL](https://access.redhat.com/solutions/61334), edit the `/etc/security/limits.d/99-mssql-server.conf` file to have the following values:

```ini
mssql - nofile 1048576
```

> [!NOTE]  
> This setting doesn't apply to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] services started by `systemd`. For more information, see [How to set limits for services in RHEL and systemd](https://access.redhat.com/solutions/1257953).

### Disable last accessed date and time on filesystems for SQL Server data and log files

To ensure that the system automatically remounts the drives after a restart, add them to the `/etc/fstab` file. Use the UUID (Universally Unique Identifier) in `/etc/fstab` to refer to the drive, rather than just the device name (such as `/dev/sdc1`).

Use the `noatime` attribute with any filesystem that stores [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data and log files. Refer to your Linux documentation on how to set this attribute. The following example shows how to enable the `noatime` option for a volume mounted in an Azure Virtual Machine.

The mount point entry in `/etc/fstab`:

```bash
UUID="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" /data1 xfs rw,attr2,noatime 0 0
```

In the preceding example, UUID represents the device that you can find using the `blkid` command.

### SQL Server and Forced Unit Access (FUA) I/O subsystem capability

[!INCLUDE [linux-forced-unit-access](../includes/linux-forced-unit-access.md)]

## Kernel and CPU settings for high performance

The following section describes the recommended Linux OS settings related to high performance and throughput for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] installation. See your Linux distribution's documentation for the process to configure these settings. You can use [TuneD](https://tuned-project.org) as described, to configure many CPUs and kernel configurations, described in the next section.

### Use *TuneD* to configure kernel settings

For Red Hat Enterprise Linux (RHEL) users, the [TuneD](https://tuned-project.org) throughput-performance profile automatically configures some kernel and CPU settings (except for C-States). Starting with RHEL 8.0, you can use a TuneD profile named `mssql` that offers finer Linux performance-related tunings for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] workloads. This profile builds on the RHEL throughput-performance profile. Because the `mssql` profile exposes all of its settings, you can review and adapt them for other Linux distributions or RHEL releases that don't include this profile.

For SUSE Linux Enterprise Server 12 SP5, Ubuntu 18.04, and Red Hat Enterprise Linux 7.x, you can manually install the `tuned` package. Use it to create and configure the `mssql` profile as described in the following section.

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

#### Proposed Linux settings using a TuneD `mssql` profile

The following example provides a TuneD configuration for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux.

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

If you use Linux distributions with kernel versions greater than 4.18, comment the following options as shown. Otherwise, uncomment the following options if you use distributions with kernel versions earlier than 4.18.

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

Verify that the profile is active using the following command:

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

When you use TuneD as described, it automatically configures the CPU frequency governor, `ENERGY_PERF_BIAS`, and `min_perf_pct` settings. It uses the throughput-performance profile as the base for the `mssql` profile. You must manually configure the C-States parameter according to the documentation provided by Linux or your system distributor.

### Disk settings recommendations

The following table provides recommendations for disk settings:

| Setting | Value | More information |
| --- | --- | --- |
| Disk `readahead` | 4096 | See the `blockdev` command |
| **sysctl** settings | `kernel.sched_min_granularity_ns = 15000000`<br />`kernel.sched_wakeup_granularity_ns = 2000000`<br />`vm.dirty_ratio = 80`<br />`vm.dirty_background_ratio = 3`<br />`vm.swappiness = 1` | See the **sysctl** command |

#### Description

- `vm.swappiness`: This parameter controls the relative weight given to swapping out runtime process memory compared to the filesystem cache. The default value for this parameter is 60, which indicates swapping runtime process memory pages compared to removing filesystem cache pages at a ratio of 60:140. Setting the value to 1 indicates a strong preference for keeping runtime process memory in physical memory at the expense of the filesystem cache. Since [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses the buffer pool as a data page cache and strongly prefers to write through to physical hardware bypassing the filesystem cache for reliable recovery, an aggressive swappiness configuration can be beneficial for high-performing and dedicated [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

  You can find additional information at [Documentation for /proc/sys/vm/ - #swappiness](https://www.kernel.org/doc/html/latest/admin-guide/sysctl/vm.html#swappiness).

- `vm.dirty_*`: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] file write accesses are uncached, satisfying its data integrity requirements. These parameters allow efficient asynchronous write performance and lower the storage I/O effect of Linux caching writes by allowing large enough caching while throttling flushing.

- `kernel.sched_*`: These parameter values represent the current recommendation for tweaking the Completely Fair Scheduling (CFS) algorithm in the Linux kernel. They improve throughput of network and storage I/O calls with respect to inter-process preemption and resumption of threads.

Using the `mssql` TuneD profile configures the `vm.swappiness`, `vm.dirty_*`, and `kernel.sched_*` settings. You must manually configure the disk `readahead` setting by using the `blockdev` command for each device.

### Kernel setting for auto NUMA balancing on multinode NUMA systems

If you install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on a multinode NUMA system, the following `kernel.numa_balancing` kernel setting is enabled by default. To allow [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to operate at maximum efficiency on a NUMA system, disable auto NUMA balancing on a multinode NUMA system:

```bash
sysctl -w kernel.numa_balancing=0
```

Using the `mssql` TuneD profile configures the `kernel.numa_balancing` option.

### Kernel settings for virtual address space

The default setting of `vm.max_map_count` is `65536` (65,536), which might not be high enough for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] installation. For this reason, change the `vm.max_map_count` value to at least `262144` (262,144) for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployment. For further tuning of these kernel parameters, see the [Proposed Linux settings using a TuneD mssql profile](#proposed-linux-settings-using-a-tuned-mssql-profile) section. The maximum value for `vm.max_map_count` is `2147483647` (2,147,483,647).

```bash
sysctl -w vm.max_map_count=1600000
```

Using the `mssql` TuneD profile configures the `vm.max_map_count` option.

### Leave Transparent Huge Pages (THP) enabled

Most Linux installations have this option on by default. For the most consistent performance experience, leave this configuration option enabled. However, if there's high memory paging activity in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployments with multiple instances, or [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] execution with other memory demanding applications on the server, test your application's performance after executing the following command:

```bash
echo madvise > /sys/kernel/mm/transparent_hugepage/enabled
```

Or modify the `mssql` TuneD profile with the line:

```bash
vm.transparent_hugepages=madvise
```

And make sure the `mssql` profile is active after the modification:

```bash
tuned-adm off
tuned-adm profile mssql
```

Using the `mssql` TuneD profile configures the `transparent_hugepage` option.

### Network setting recommendations

Along with storage and CPU recommendations, consider the following network-specific recommendations. Different NICs offer different settings. Refer to NIC vendors for guidance for each of these options. Test and configure these settings on development environments before applying them to production environments. The following options are explained with examples, and the commands used are specific to NIC type and vendor.

1. **Configuring network port buffer size**. In the example, the NIC is named `eth0`, which is an Intel-based NIC. For Intel based NIC, the recommended buffer size is 4 KB (4096). Verify the preset maximums and then configure it using the following example:

   Check the preset maximums with the following command. Replace `eth0` with your NIC name:

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

1. **Enable jumbo frames**. Before enabling jumbo frames, verify that all the network switches, routers, and anything else essential in the network packet path between the clients and the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] support jumbo frames. Only then can enabling jumbo frames improve performance. After you enable jumbo frames, connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and change the network packet size to 8060 using `sp_configure`, as shown in the following example:

   ```bash
   # command to set jumbo frame to 9014 for a Intel NIC named eth0 is
   ifconfig eth0 mtu 9014
   # verify the setting using the command:
   ip addr | grep 9014
   ```

   ```sql
   EXECUTE sp_configure 'network packet size', '8060';
   GO

   RECONFIGURE WITH OVERRIDE;
   GO
   ```

1. **Configure adaptive IRQ coalescing**. By default, set the port for adaptive RX/TX IRQ coalescing, meaning interrupt delivery is adjusted to improve latency when packet rate is low and improve throughput when packet rate is high. This setting might not be available across your network infrastructure, so review the existing network infrastructure and confirm that this setting is supported. The example is for the NIC named `eth0`, which is an Intel-based NIC:

   Set the port for adaptive RX/TX IRQ coalescing:

   ```bash
   ethtool -C eth0 adaptive-rx on
   ethtool -C eth0 adaptive-tx on
   ```

   Confirm the setting:

   ```bash
   ethtool -c eth0
   ```

   > [!NOTE]  
   > For predictable behavior in high-performance environments, like environments for benchmarking, disable the adaptive RX/TX IRQ coalescing and then set specifically the RX/TX interrupt coalescing. See the example commands to disable the RX/TX IRQ coalescing and then specifically set the values:

   Disable adaptive RX/TX IRQ coalescing:

   ```bash
   ethtool -C eth0 adaptive-rx off
   ethtool -C eth0 adaptive-tx off
   ```

   Confirm the change:

   ```bash
   ethtool -c eth0
   ```

   Set the `rx-usecs` and `irq` parameters. `rx-usecs` specifies how many microseconds after at least one packet is received before generating an interrupt. The `irq` parameter specifies the corresponding delays in updating the status when the interrupt is disabled. For Intel-based NICs, you can use the following settings:

   ```bash
   ethtool -C eth0 rx-usecs 100 tx-frames-irq 512
   ```

   Confirm the change:

   ```bash
   ethtool -c eth0
   ```

1. **Enable receive-side scaling (RSS)** and by default, combine the RX and TX side of RSS queues. There are specific scenarios, when working with Microsoft Support, where disabling RSS improves the performance as well. Test this setting in test environments before applying it on production environments. The following example is for Intel NICs.

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

1. **Configure NIC port IRQ affinity**. To achieve expected performance by tweaking the IRQ affinity, consider few important parameters like Linux handling of the server topology, NIC driver stack, default settings, and `irqbalance` setting. You can optimize the NIC port IRQ affinities settings by using your knowledge of server topology, disabling the `irqbalance`, and using the NIC vendor-specific settings.

   The following example of Mellanox specific network infrastructure helps to explain the configuration. For more information, and to download the Mellanox **mlnx** tools, see [​​Performance Tuning tools for Mellanox Network Adapters](https://enterprise-support.nvidia.com/s/article/MLNX2-117-2523kn). The commands change based on the environment. Contact the NIC vendor for further guidance.

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

   Set hardware affinity to the NUMA node that physically hosts the NIC and its port:

   ```bash
   ./set_irq_affinity_bynode.sh `\cat /sys/class/net/eth0/device/numa_node` eth0
   ```

   Verify the IRQ affinity:

   ```bash
   ./show_irq_affinity.sh eth0
   ```

   Add IRQ coalescing optimizations:

   ```bash
   ethtool -C eth0 adaptive-rx off
   ethtool -C eth0 adaptive-tx off
   ethtool -C eth0  rx-usecs 750 tx-frames-irq 2048
   ```

   Verify the settings:

   ```bash
   ethtool -c eth0
   ```

1. **Verify NIC speed**. After you make the preceding changes, verify the speed of the NIC to ensure it matches your expectations by using the following command:

   ```bash
   ethtool eth0 | grep -i Speed
   ```

### Advanced kernel and OS configuration

- For the best storage I/O performance, use Linux multiqueue scheduling for block devices. This scheduling method enables the block layer performance to scale well with fast solid-state drives (SSDs) and multicore systems. Check the documentation to see if your Linux distribution enables it by default. In most other cases, you can boot the kernel with `scsi_mod.use_blk_mq=y` to enable it. The documentation for your Linux distribution might have further guidance on this setting. This setting is consistent with the upstream Linux kernel.

- Because multipath I/O is often used for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployments, configure the device mapper (DM) multiqueue target to use the `blk-mq` infrastructure by enabling the `dm_mod.use_blk_mq=y` kernel boot option. The default value is `n` (disabled). This setting reduces locking overhead at the DM layer when the underlying SCSI devices use `blk-mq`. For more information about how to configure multipath I/O, see your Linux distribution's documentation.

### Configure swapfile

Ensure you have a properly configured swapfile to avoid any out of memory issues. Consult your Linux documentation for how to create and properly size a swapfile. If you plan to run containers, [enable swap space at the host level](performance-best-practices-sql-server-memory.md#swap-space-considerations).

### Virtual machines and dynamic memory

If you're running [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux in a virtual machine, make sure you select options that fix the amount of memory reserved for the virtual machine. Don't use features like Hyper-V Dynamic Memory.

## SQL Server configuration

Perform the following configuration tasks after you install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux to achieve the best performance for your application.

### Best practices

The following practices apply to all [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux deployments.

#### Use PROCESS AFFINITY for node and CPUs

Use `ALTER SERVER CONFIGURATION` to set `PROCESS AFFINITY` for all the `NUMANODE`s and CPUs you're using for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] (which is typically for all `NODE`s and CPUs) on a Linux OS. Processor affinity helps maintain efficient Linux and SQL scheduling behavior. Using the `NUMANODE` option is the simplest method. Use `PROCESS AFFINITY` even if you have only a single NUMA node on your computer. For more information on how to set `PROCESS AFFINITY`, see the [ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md) article.

#### Configure multiple `tempdb` data files

Because a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux installation doesn't offer an option to configure multiple `tempdb` files, consider creating multiple `tempdb` data files after installation. For more information, see [Recommendations to reduce allocation contention in SQL Server tempdb database](/troubleshoot/sql/database-engine/performance/recommendations-reduce-allocation-contention).

### Advanced configuration

For memory configuration options including `mssql-conf` memory limits, cgroup settings, Docker container memory examples, and swap space considerations, see [Performance best practices: SQL Server memory on Linux](performance-best-practices-sql-server-memory.md).

## Related content

- [Performance best practices: SQL Server memory on Linux](performance-best-practices-sql-server-memory.md)
- [Linux related dynamic management views and functions (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/linux-related-dynamic-management-views-and-functions-transact-sql.md)
- [Walkthrough for the performance features of SQL Server on Linux](../sql-server-linux-performance-get-started.md)
- [What is SQL Server on Linux?](../sql-server-linux-overview.md)
