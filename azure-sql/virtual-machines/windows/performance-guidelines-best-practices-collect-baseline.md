---
title: "Collect Baseline: Performance Best Practices & Guidelines"
description: Provides steps to collect a performance baseline as guidelines to optimize the performance of your SQL Server on Azure Virtual Machine (VM).
author: dplessMSFT
ms.author: dpless
ms.reviewer: vanto
ms.date: 03/31/2026
ms.service: azure-vm-sql-server
ms.subservice: performance
ms.topic: best-practice
tags: azure-service-management
---
# Collect baseline: Performance best practices for SQL Server on Azure VM

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides information to collect a performance baseline as a series of best practices and guidelines to optimize performance for your SQL Server on Azure Virtual Machines (VMs).

Typically, there's a trade-off between optimizing for costs and optimizing for performance. This performance best practices series focuses on getting the *best* performance for SQL Server on Azure Virtual Machines. If your workload is less demanding, you might not require every recommended optimization. Consider your performance needs, costs, and workload patterns as you evaluate these recommendations.

## Overview

For a prescriptive approach, gather performance counters by using PerfMon or LogMan and capture SQL Server wait statistics to better understand general pressures and potential bottlenecks of the source environment.

Start by collecting the CPU, memory, [IOPS](/azure/virtual-machines/premium-storage-performance#iops), [throughput](/azure/virtual-machines/premium-storage-performance#throughput), and [latency](/azure/virtual-machines/premium-storage-performance#latency) of the source workload at peak times following the [application performance checklist](/azure/virtual-machines/premium-storage-performance#application-performance-requirements-checklist).

Gather data during peak hours such as workloads during your typical business day, but also other high load processes such as end-of-day processing, and weekend ETL workloads. Consider scaling up your resources for atypically heavy workloads, such as end-of-quarter processing, and then scale down once the workload completes.

Use the performance analysis to select the [VM Size](/azure/virtual-machines/sizes-memory) that can scale to your workload's performance requirements.

## Storage

SQL Server performance depends heavily on the I/O subsystem. Measure storage performance by IOPS and throughput. Unless your database fits into physical memory, SQL Server constantly brings database pages in and out of the buffer pool. Treat the data files for SQL Server differently. Access to log files is sequential except when a transaction needs to be rolled back where data files, including `tempdb`, are randomly accessed. If you have a slow I/O subsystem, your users might experience performance problems such as slow response times and tasks that don't complete due to time-outs.

The Azure Marketplace virtual machines have log files on a physical disk that is separate from the data files by default. The `tempdb` data files count and size meet best practices and are targeted to the ephemeral `D:\` drive.

The following PerfMon counters can help validate the IO throughput required by your SQL Server:
- **\LogicalDisk\Disk Reads/Sec** (read IOPS)
- **\LogicalDisk\Disk Writes/Sec** (write IOPS)
- **\LogicalDisk\Disk Read Bytes/Sec** (read throughput requirements for the data, log, and `tempdb` files)
- **\LogicalDisk\Disk Write Bytes/Sec** (write throughput requirements for the data, log, and `tempdb` files)

Using IOPS and throughput requirements at peak levels, evaluate VM sizes that match the capacity from your measurements.

If your workload requires 20,000 read IOPS and 10,000 write IOPS, you can either choose **E16s_v3** (with up to 32,000 cached and 25,600 uncached IOPS) or **M16_s** (with up to 20,000 cached and 10,000 uncached IOPS) with two P30 disks striped by using Storage Spaces.

Make sure to understand both throughput and IOPS requirements of the workload as VMs have different scale limits for IOPS and throughput.

## Memory

Track both external memory used by the OS as well as the memory used internally by SQL Server. Identifying pressure for either component helps you size virtual machines and identify opportunities for tuning.

The following PerfMon counters can help validate the memory health of a SQL Server virtual machine:
- \Memory\Available MBytes
- [\SQLServer:Memory Manager\Target Server Memory (KB)](/sql/relational-databases/performance-monitor/sql-server-memory-manager-object)
- [\SQLServer:Memory Manager\Total Server Memory (KB)](/sql/relational-databases/performance-monitor/sql-server-memory-manager-object)
- [\SQLServer:Buffer Manager\Lazy writes/sec](/sql/relational-databases/performance-monitor/sql-server-buffer-manager-object)
- [\SQLServer:Buffer Manager\Page life expectancy](/sql/relational-databases/performance-monitor/sql-server-buffer-manager-object)

## Compute

Compute in Azure is managed differently than on-premises environments. On-premises servers are designed to last several years without an upgrade because of the management overhead and cost of acquiring new hardware. Virtualization reduces some of these problems, but applications are optimized to take full advantage of the underlying hardware. Any significant change to resource consumption requires rebalancing the entire physical environment.

This problem doesn't exist in Azure. You can easily create a new virtual machine on a different series of hardware, or even in a different region.

In Azure, you want to take advantage of as much of the virtual machine's resources as possible. Configure Azure virtual machines to keep the average CPU as high as possible without affecting the workload.

The following PerfMon counters can help validate the compute health of a SQL Server virtual machine:
- **\Processor Information(_Total)\% Processor Time**
- **\Process(sqlservr)\% Processor Time**

> [!NOTE]  
> Aim to use 80% of your compute, with peaks above 90% but don't reach 100% for any sustained period of time. Fundamentally, only provision the compute the application needs. Plan to scale up or down as the business requires.


## Related content

For detailed guidance on each optimization area:

- **[Quick checklist](performance-guidelines-best-practices-checklist.md)** - Review the full best practices checklist
- **[VM size](performance-guidelines-best-practices-vm-size.md)** - Choose the right VM series and configuration
- **[Storage](performance-guidelines-best-practices-storage.md)** - Optimize disk configuration and performance
- **[Security](security-considerations-best-practices.md)** - Implement security best practices
- **[HADR settings](hadr-cluster-best-practices.md)** - Configure high availability and disaster recovery
- **[Updating SQL Server](servicing-updates-guidelines.md)** - Keep SQL Server up to date

Review other SQL Server Virtual Machine articles at [SQL Server on Azure Virtual Machines Overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).
