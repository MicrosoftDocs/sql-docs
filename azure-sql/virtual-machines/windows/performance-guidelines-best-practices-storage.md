---
title: "Storage: Performance Best Practices & Guidelines"
description: Provides storage best practices and guidelines to optimize the performance of your SQL Server on Azure Virtual Machines (VM).
author: dplessMSFT
ms.author: dpless
ms.reviewer: vanto
ms.date: 04/23/2026
ms.service: azure-vm-sql-server
ms.subservice: performance
ms.topic: best-practice
tags: azure-service-management
---
# Storage: Performance best practices for SQL Server on Azure VMs

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides storage best practices and guidelines to optimize performance for your SQL Server on Azure Virtual Machines (VMs). Learn how to select the right disk types, configure storage pools, and implement caching strategies to maximize your database performance.

There's typically a trade-off between optimizing for costs and optimizing for performance. This performance best practices series focuses on getting the *best* performance for SQL Server on Azure VMs. If your workload is less demanding, you might not require every recommended optimization. Consider your performance needs, costs, and workload patterns as you evaluate these recommendations.

To learn more, see the other articles in this series: [Checklist](performance-guidelines-best-practices-checklist.md), [VM size](performance-guidelines-best-practices-vm-size.md), [Security](security-considerations-best-practices.md), [HADR configuration](hadr-cluster-best-practices.md), and [Collect baseline](performance-guidelines-best-practices-collect-baseline.md).

## Checklist

Review the following checklist for a brief overview of the storage best practices that the rest of the article covers in greater detail:

[!INCLUDE [storage best practices](../../includes/virtual-machines-best-practices-storage.md)]

To compare the storage checklist with the other best practices, see the comprehensive [Performance best practices checklist](performance-guidelines-best-practices-checklist.md).

## Overview

To find the most effective configuration for SQL Server workloads on an Azure VM, start by [measuring the storage performance of your business application](performance-guidelines-best-practices-collect-baseline.md#storage). Once you know your storage requirements, select a virtual machine that supports the necessary IOPS and throughput with the appropriate memory-to-vCore ratio.

Choose a VM size with enough storage scalability for your workload and a mixture of disks (usually in a storage pool) that meets the capacity and performance requirements of your business.

The type of disk depends on both the file type that the disk hosts and your peak performance requirements.

> [!TIP]  
> When you provision a SQL Server VM through the Azure portal, you get guidance through the storage configuration process. The portal also implements most storage best practices, such as creating separate storage pools for your data and log files, targeting `tempdb` to the `D:\` drive, and enabling the optimal caching policy. For more information about provisioning and configuring storage, see [SQL VM storage configuration](storage-configuration.md).

## VM disk types

You can choose the performance level for your disks. The types of managed disks available as underlying storage, listed by increasing performance capabilities, are Standard hard disk drives (HDD), Standard solid-state drives (SSD), Premium SSDs, Premium SSD v2, and Ultra Disks.

For Standard HDDs, Standard SSDs, and Premium SSDs, the performance of the disk increases with the size of the disk. The disks are grouped by [premium disk labels](/azure/virtual-machines/disks-types#premium-ssds), such as the P1 with 4 GiB of space and 120 IOPS to the P80 with 32 TiB of storage and 20,000 IOPS. Premium storage supports a storage cache that helps improve read and write performance for some workloads. For more information, see [Managed disks overview](/azure/virtual-machines/managed-disks-overview).

You can change the performance of Premium SSD v2 and Ultra Disks independently of the size of the disk. For details, see [Ultra Disk performance](/azure/virtual-machines/disks-types#ultra-disk-performance) and [Premium SSD v2 performance](/azure/virtual-machines/disks-types#premium-ssd-v2-performance). If your workload requires more than 160,000 IOPS, consider using Premium SSD v2 or Ultra Disks.

For your SQL Server on Azure VM, consider the three main [disk roles](/azure/virtual-machines/managed-disks-overview#disk-roles) - an operating system (OS) disk, a temporary disk, and your data disks. Carefully choose what you store on the operating system drive `(C:\)` and the ephemeral temporary drive `(D:\)`.

### Operating system disk

An operating system disk is a VHD that you can boot and mount as a running version of an operating system. It's labeled as the `C:\` drive. When you create an Azure VM, the platform attaches at least one disk to the VM for the operating system disk. The `C:\` drive is the default location for application installs and file configuration.

For production SQL Server environments, don't use the operating system disk for data files, log files, or error logs.

### Temporary disk

Many Azure VMs contain another disk type called the temporary disk, which is labeled as the `D:\` drive. Depending on the VM series and size, the capacity of this disk varies. The temporary disk is ephemeral, which means the disk storage is recreated (deallocated and allocated again) when the VM restarts or moves to a different host. This process can happen during [service healing](/troubleshoot/azure/virtual-machines/understand-vm-reboot).

The temporary storage drive doesn't persist to remote storage. Therefore, don't store user database files, transaction log files, or anything that must be preserved on this drive. For example, you can use it for buffer pool extensions, the page file, and `tempdb`.

Place `tempdb` on the local temporary SSD `D:\` drive for SQL Server workloads unless consumption of local cache is a concern. If you're using a VM that [doesn't have a temporary disk](/azure/virtual-machines/azure-vms-no-temp-disk), place `tempdb` on its own isolated disk or storage pool with caching set to read-only. To learn more, see [tempdb data caching policies](performance-guidelines-best-practices-storage.md#data-file-caching-policies).

### Data disks

Data disks are remote storage disks that you often create in [storage pools](/windows-server/storage/storage-spaces/overview) to exceed the capacity and performance that any single disk can offer to the VM.

Attach the minimum number of disks that satisfies the IOPS, throughput, and capacity requirements of your workload. Don't exceed the maximum number of data disks of the smallest VM you plan to resize to.

Place data and log files on data disks that you provision to best suit performance requirements.

Format your data disk to use 64-KB allocation unit size for all data files placed on a drive other than the temporary `D:\` drive (which has a default of 4 KB). SQL Server VMs deployed through Azure Marketplace come with data disks formatted with allocation unit size and interleave for the storage pool set to 64 KB.

> [!NOTE]  
> You can also host your SQL Server database files directly on [Azure Blob storage](/sql/relational-databases/databases/sql-server-data-files-in-microsoft-azure) or on [SMB storage](/sql/database-engine/install-windows/install-sql-server-with-smb-fileshare-as-a-storage-option) such as [Azure premium file share](/azure/storage/files/storage-how-to-create-file-share). For the best performance, reliability, and feature availability, use [Azure managed disks](/azure/virtual-machines/managed-disks-overview).

## Premium SSD v2

Use [Premium SSD v2](/azure/virtual-machines/disks-types#premium-ssd-v2) disks when running SQL Server workloads in [supported regions](/azure/virtual-machines/disks-types#regional-availability), if the [current limitations](/azure/virtual-machines/disks-types#premium-ssd-v2-limitations) are suitable for your environment. Depending on your configuration, Premium SSD v2 can be cheaper than Premium SSDs, while also providing performance improvements. By using Premium SSD v2, you can individually adjust your throughput or IOPS independently from the size of your disk. Individually adjusting performance options provides greater cost savings and allows you to script changes to meet performance requirements during anticipated or known periods of need.

Use Premium SSD v2 when using the [Ebdsv5 or Ebsv5 virtual machine series](/azure/virtual-machines/ebdsv5-ebsv5-series) because it's a more cost-effective solution for these high I/O throughput machines. If your workload requires more than 160,000 IOPS, consider using Premium SSD v2 or Ultra Disks.

You can [deploy your SQL Server VMs with Premium SSD v2](storage-configuration-premium-ssd-v2.md) by using the Azure portal (currently in preview).

If you're deploying your SQL Server VM by using the Azure portal and want to use Premium SSD v2, you're currently limited to the [Ebdsv5 or Ebsv5 series virtual machines](/azure/virtual-machines/ebdsv5-ebsv5-series). However, if you manually create your VM with Premium SSD v2 storage and then manually install SQL Server on the VM, you can use any VM series that supports Premium SSD v2. Be sure to [register](sql-agent-extension-manually-register-single-vm.md) your SQL Server VM with the SQL IaaS Agent extension so you can take advantage of all the [benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits) provided by the extension.

## Azure Elastic SAN

[Azure Elastic SAN](storage-configuration-azure-elastic-san.md) is a network-attached storage offering that provides a flexible and scalable solution that can reduce cost through storage consolidation. Azure Elastic SAN is a high-performance, reliable block storage solution that connects to Azure compute services over the iSCSI protocol. Elastic SAN supports transitioning from an existing SAN storage estate to the cloud without refactoring your application architecture.

Elastic SAN supports millions of IOPS, double-digit GB/s of throughput, and low single-digit millisecond latencies. Use Azure Elastic SAN if you need to consolidate storage, work with multiple compute services, or have workloads that require high throughput levels when driving storage over network bandwidth. However, since achieving desired IOPS and throughput for SQL Server workloads often requires overprovisioning capacity, *it's not typically appropriate for **single** SQL Server workloads*. To get the best value from Elastic SAN, consider using it as storage for multiple SQL Server workloads, or a combination of SQL Server and other low-performance workloads.

Consider placing SQL Server workloads on Elastic SAN for cost efficiency, storage consolidation, shared performance across workloads, and higher storage throughput.

## Premium SSD

Use Premium SSDs for data and log files for production SQL Server workloads. Premium SSD IOPS and bandwidth vary based on the [disk size and type](/azure/virtual-machines/disks-types).

For production workloads, use P30 or P40 disks for SQL Server data files for the best balance of performance and reserved pricing. Use P30 or higher performance tier disks for SQL Server transaction log files. For the best total cost of ownership, start with P30s (5,000 IOPS/200 MBps) for data and log files and only choose higher capacities when you need to control the VM disk count. For dev/test or small systems, you can choose to use sizes smaller than P30, as these disks support caching, but they don't offer reserved pricing.

For OLTP workloads, match the target IOPS per disk (or storage pool) with your performance requirements using workloads at peak times and the `Disk Reads/sec` + `Disk Writes/sec` performance counters. For data warehouse and reporting workloads, match the target throughput using workloads at peak times and the `Disk Read Bytes/sec` + `Disk Write Bytes/sec`.

Use Storage Spaces to achieve optimal performance. Configure two pools, one for the log files and the other for the data files. If you aren't using disk striping, use two Premium SSDs mapped to separate drives, where one drive contains the log file and the other contains the data.

The [provisioned IOPS and throughput](/azure/virtual-machines/disks-types#premium-ssds) per disk determine the overall capability of your storage pool. The combined IOPS and throughput capabilities of the disks are the maximum capability up to the throughput limits of the VM.

> [!NOTE]  
> The stripe column count (`NumberOfColumns`) is fixed at pool creation, so adding disks later increases capacity only, not IOPS or throughput. To improve performance, recreate the pool with the desired number of disks.

When selecting disks, prefer a larger number of small disks over a smaller number of large disks for better price-to-performance, while keeping the total count within your workload's requirements.

### Scale premium disks

The size of your Premium SSD determines the initial performance tier of your disk. Designate the performance tier at deployment or change it afterwards, without changing the size of the disk. If demand increases, increase the performance level to meet your business needs.

By changing the performance tier, you can prepare for and meet higher demand without relying on [disk bursting](/azure/virtual-machines/disk-bursting#credit-based-bursting).

Use the higher performance tier for as long as needed. You're billed at the current performance tier, so you can upgrade to match your performance requirements without increasing capacity. Return to the original tier when the extra performance is no longer required.

This temporary expansion of performance is suited for targeted events such as peak shopping seasons, performance testing, training events, and other brief windows where you need greater performance for a short period.

For more information, see [Performance tiers for managed disks](/azure/virtual-machines/disks-change-performance).

## Azure Ultra Disk

If you need consistently low latency with submillisecond response times, consider using [Azure Ultra Disk](/azure/virtual-machines/disks-types#ultra-disks) for the SQL Server log drive, or even the data drive for applications that are extremely sensitive to I/O latency.

You can configure Ultra Disk so that capacity and IOPS scale independently. By using Ultra Disk, you can provision a disk with the capacity, IOPS, and throughput requirements based on application needs.

Ultra Disk isn't supported on all VM series and has other limitations such as region availability, redundancy, and support for Azure Backup. To learn more, see [Using Azure Ultra Disks](/azure/virtual-machines/disks-enable-ultra-ssd) for a full list of limitations.

## Standard HDDs and SSDs

[Standard HDDs](/azure/virtual-machines/disks-types#standard-hdds) and SSDs have varying latencies and bandwidth. Use them only for dev/test workloads. Use Premium SSD v2 or Premium SSDs for production workloads. If you're using Standard SSD (dev/test scenarios), add the maximum number of data disks supported by your [VM size](/azure/virtual-machines/sizes?toc=/azure/virtual-machines/windows/toc.json) and use disk striping by using Storage Spaces for the best performance.

<a id="caching"></a>

## Cache

VMs that support premium storage caching can use Azure BlobCache or host caching to extend the IOPS and throughput capabilities of a VM. VMs enabled for both premium storage and premium storage caching use these two different storage bandwidth limits together to improve storage performance.

The IOPS and MBps throughput without caching count against a VM's uncached disk throughput limits. The maximum cached limits provide another buffer for reads that helps address growth and unexpected peaks.

Enable `Read-only` premium caching on data drives to significantly improve read performance without extra cost. Don't enable caching on transaction log drives. For recommended settings per disk type, see [Data file caching policies](#data-file-caching-policies).

Reads and writes to the Azure BlobCache (cached IOPS and throughput) don't count against the uncached IOPS and throughput limits of the VM.

> [!NOTE]  
> Disk caching isn't supported for disks 4 TiB and larger (P50 and larger). If you attach multiple disks to your VM, each disk that is smaller than 4 TiB supports caching. For more information, see [Disk caching](/azure/virtual-machines/premium-storage-performance#disk-caching).

### Uncached throughput

The max uncached disk IOPS and throughput is the maximum remote storage limit that the VM can handle. This limit is defined at the VM and isn't a limit of the underlying disk storage. This limit applies only to I/O against data drives remotely attached to the VM, not the local I/O against the temp drive (`D:\` drive) or the OS drive.

For VMs that support premium storage caching, the uncached limit is applied before the limit of the underlying disk storage. Not all VMs publish this metric. For more information about Azure VM disk performance, see [Virtual machine and disk performance](/azure/virtual-machines/disks-performance).

Verify the uncached IOPS and throughput available for a VM in the documentation for your VM series.

For example, the [M-series](/azure/virtual-machines/m-series) documentation shows that the max uncached throughput for the Standard_M8ms VM is 5,000 IOPS and 125 MBps of uncached disk throughput.

:::image type="content" source="media/performance-guidelines-best-practices/m-series-table.png" alt-text="Screenshot showing M-series uncached disk throughput documentation." lightbox="media/performance-guidelines-best-practices/m-series-table.png":::

Likewise, you can see that the Standard_M32ts supports 20,000 uncached disk IOPS and 500-MBps uncached disk throughput. This limit is governed at the VM level regardless of the underlying premium disk storage.

For more information, see [uncached and cached limits](/azure/virtual-machines/disks-performance#virtual-machine-uncached-vs-cached-limits).

### Cached and temp storage throughput

The max cached and temp storage throughput limit is separate from the uncached throughput limit on the VM. The Azure BlobCache uses a combination of the VM host's random-access memory and locally attached SSD. The temp drive (`D:\` drive) within the VM also uses this local SSD.

The max cached and temp storage throughput limit controls the I/O against the local temp drive (`D:\` drive) and the Azure BlobCache **only if** host caching is enabled. Some VM series publish these as separate metrics, namely **Temp ReadWrite Storage IOPS** and **Temp ReadWrite Storage Speed (MBps)**. Check your VM series documentation for the applicable metric names.

When you enable caching on premium storage, VMs can scale beyond the limitations of the remote storage uncached VM IOPS and throughput limits.

Check the virtual machine documentation to verify that a VM supports both premium storage and premium storage caching. For example, the [M-series](/azure/virtual-machines/m-series) documentation indicates that it supports both premium storage and premium storage caching:

:::image type="content" source="media/performance-guidelines-best-practices/m-series-table-premium-support.png" alt-text="Screenshot showing M-Series Premium Storage support.":::

The limits of the cache vary based on the VM size. Check the VM series documentation for specific cached and temp storage IOPS, throughput, and cache size values for each VM SKU.

:::image type="content" source="media/performance-guidelines-best-practices/m-series-table-cached-temp.png" alt-text="Screenshot showing M-series cached disk throughput documentation." lightbox="media/performance-guidelines-best-practices/m-series-table-cached-temp.png":::

You can manually enable host caching on an existing VM. Stop all application workloads and the SQL Server services before you make any changes to your VM's caching policy. Changing any of the VM cache settings detaches the target disk and then reattaches it after the settings are applied.

### Data file caching policies

Your storage caching policy varies depending on the type of SQL Server data files that the drive hosts.

The following table provides a summary of the recommended caching policies based on the type of SQL Server data:

| SQL Server disk | Recommendation |
| --- | --- |
| **Data disk** | Set to `Read-only`. Don't use `Read/Write` caching, which risks data integrity by buffering writes.<br />`Read-only` caching improves read performance without affecting write durability.<br />Remote disk IOPS and throughput plus cached IOPS and throughput contribute to the total possible performance available from the VM, but actual performance varies based on the workload's ability to use the cache (cache hit ratio). |
| **Transaction log disk** | Set to `None`. Don't enable `Read-only` or `Read/Write` caching on log disks. Either setting risks breaking ACID durability guarantees, because the host cache might acknowledge log writes before they reach stable media. Caching also degrades write performance and reduces cache available for data drive reads. |
| **OS disk** | The default caching policy is `Read/write` for the OS drive.<br />Don't change the caching level of the OS drive. |
| `tempdb` | If you can't place `tempdb` on the ephemeral drive `D:\` due to capacity reasons, either resize the VM to get a larger ephemeral drive or place `tempdb` on a separate data drive with `Read-only` caching configured.<br />The VM cache and ephemeral drive both use the local SSD, so keep this in mind when sizing as `tempdb` I/O counts against the cached IOPS and throughput VM limits when hosted on the ephemeral drive. |

> [!IMPORTANT]  
> Changing the cache setting of an Azure disk detaches and reattaches the target disk. When you change the cache setting for a disk that hosts SQL Server data, log, or application files, stop the SQL Server service along with any other related services to avoid data corruption.

To learn more, see [Disk caching](/azure/virtual-machines/premium-storage-performance#disk-caching).

### Caching requirements for SQL Server drives

SQL Server depends on sequential log writes reaching stable media immediately on every commit. Enabling the wrong Azure host cache setting on SQL Server drives can break durability guarantees and degrade performance.

- **Transaction log disks**: Set the Azure host cache to `None`. Enabling `Read-only` or `Read/Write` caching on log disks can risk breaking durability guarantees required for ACID compliance, because the host cache might acknowledge log writes before they reach stable media. Caching also degrades write performance and reduces the amount of cache available for reads on data drives.
- **Data disks**: Set the Azure host cache to `Read-only`. Don't use `Read/Write` caching, which risks data integrity by buffering writes. `Read-only` caching improves read performance without affecting write durability.
- **OS disk**: Keep the default `Read/Write` caching policy. Don't change this setting.

> [!IMPORTANT]
> Set the Azure host cache for transaction log disks to `None`, and for data disks to `Read-only`. Don't use `Read/Write` caching on any disk that hosts SQL Server data or log files.

For general guidance on write caching, controller safety, and durability guarantees for SQL Server drives, see [SQL Server I/O fundamentals](/sql/relational-databases/sql-server-storage-guide#write-caching-in-storage-controllers).

Before deployment:

- Review caching requirements for each disk type (data, transaction log, and `tempdb`).
- To avoid corruption, stop the SQL Server service before you change VM disk caching settings.

## Disk striping

Analyze the throughput and bandwidth required for your SQL data files to determine the number of data disks, including the log file and `tempdb`. Throughput and bandwidth limits vary by VM size. For more information, see [VM sizes](/azure/virtual-machines/sizes).

Add more data disks and use disk striping for more throughput. For example, an application that needs 12,000 IOPS and 180-MB/s throughput can use three striped P30 disks to deliver 15,000 IOPS and 600-MB/s throughput.

To configure disk striping, see [disk striping](storage-configuration.md#disk-striping).

## Disk capping

Both the disk and the VM have throughput limits. The maximum IOPS limits per VM and per disk differ and work independently.

The system throttles (or caps) applications that consume resources beyond these limits. Select a VM and disk size in a disk stripe that meets application requirements and avoids capping limitations. To address capping, use caching or tune the application so that it requires less throughput.

For example, an application that needs 12,000 IOPS and 180 MB/s can:

- Use the [Standard_M32ms](/azure/virtual-machines/m-series), which has a maximum uncached disk throughput of 20,000 IOPS and 500 MBps.
- Stripe three P30 disks to deliver 15,000 IOPS and 600-MB/s throughput.
- Use a [Standard_M16ms](/azure/virtual-machines/m-series) VM and use host caching to utilize local cache over consuming throughput.

Configure VMs to scale up during times of high utilization. Provision storage with enough IOPS and throughput to support the maximum VM size while keeping the overall number of disks less than or equal to the maximum number supported by the smallest VM SKU you target to use.

For more information about disk capping limitations and using caching to avoid capping, see [Disk IO capping](/azure/virtual-machines/disks-performance).

> [!NOTE]  
> Some disk capping still results in satisfactory performance to users. Tune and maintain workloads rather than resize to a larger VM to balance managing cost and performance for the business.

## Write Accelerator

Write Accelerator is a disk feature that's available only for the [M-Series](/azure/virtual-machines/m-series) VMs. The purpose of Write Accelerator is to improve the I/O latency of writes against Azure Premium Storage when you need single-digit millisecond latency due to high-volume, mission-critical OLTP workloads or data warehouse environments.

Use Write Accelerator to improve write latency to the drive hosting the log files. Don't use Write Accelerator for SQL Server data files.

Write Accelerator disks share the same IOPS limit as the VM. Attached disks can't exceed the Write Accelerator IOPS limit for a VM.

The following table outlines the number of data disks and IOPS supported per VM:

| VM SKU | # Write Accelerator disks | Write Accelerator disk IOPS per VM |
| --- | --- | --- |
| M416ms_v2, M416s_8_v2, M416s_v2 | 16 | 20000 |
| M208ms_v2, M208s_v2 | 8 | 10000 |
| M192ids_v2, M192idms_v2, M192is_v2, M192ims_v2 | 16 | 20000 |
| M128ms, M128s, M128ds_v2, M128dms_v2, M128s_v2, M128ms_v2 | 16 | 20000 |
| M64ms, M64ls, M64s, M64ds_v2, M64dms_v2, M64s_v2, M64ms_v2 | 8 | 10000 |
| M32ms, M32ls, M32ts, M32s, M32dms_v2, M32ms_v2 | 4 | 5000 |
| M16ms, M16s | 2 | 2500 |
| M8ms, M8s | 1 | 1250 |
| Standard_M176s_3_v3, Standard_M176ds_3_v3, Standard_M176s_4_v3, Standard_M176ds_4_v3 | 16 | 20000 |
| Standard_M96s_1_v3, Standard_M96ds_1_v3, Standard_M96s_2_v3, Standard_M96ds_2_v3 | 8 | 10000 |
| Standard_M48s_1_v3, Standard_M48ds_1_v3 | 4 | 5000 |
| Standard_M24s_v3, Standard_M24ds_v3 | 2 | 5000 |
| Standard_M12s_v3, Standard_M12ds_v3 | 1 | 5000 |

There are several restrictions to using Write Accelerator. To learn more, see [Restrictions when using Write Accelerator](/azure/virtual-machines/how-to-enable-write-accelerator#restrictions-when-using-write-accelerator).

### Compare to Azure Ultra Disk

The biggest difference between Write Accelerator and Azure Ultra Disks is that Write Accelerator is a VM feature only available for the M-Series, and Azure Ultra Disks is a storage option. Write Accelerator is a write-optimized cache with its own limitations based on the VM size. Azure Ultra Disks are a low latency disk storage option for Azure VMs.

If possible, use Write Accelerator over Ultra Disks for the transaction log disk. For VMs that don't support Write Accelerator but require low latency to the transaction log, use Azure Ultra Disks.

## Resize storage pools appropriately

In Azure, resize a storage pool by changing the number of disks in the pool. Don't modify the size of the disks already in the pool. Changing the sizes of the virtual or physical disks in a storage pool doesn't increase the available space of the volume when it's inside the storage pool. The extra disk space is unused and wasted.

> [!NOTE]  
> The stripe column count (`NumberOfColumns`) is fixed at pool creation. Adding disks to an existing pool increases capacity, but doesn't improve IOPS or throughput. To gain performance, recreate the pool with the desired number of disks.

For SQL Server on Azure VMs with Premium SSD (v1) disks that you deploy from Azure Marketplace, the deployment automatically adds disks to the storage pool. Use the [Storage pane](storage-configuration.md#modify-existing-drives) of the SQL virtual machines resource in the Azure portal to resize the disks in the storage pool.

For SQL Server on Azure VM Marketplace images that use Premium SSD v2 disks or Ultra Disks, or for virtual machines with self-installed SQL Server instances, manually modify the number of disks in the storage pool to change the volume size.

To expand a storage pool, follow these steps:

1. Add a new disk:
   1. [Attach a managed disk in the Azure portal](/azure/virtual-machines/windows/attach-managed-disk-portal#add-a-data-disk)
   1. [Attach a managed disk with PowerShell](/azure/virtual-machines/windows/attach-disk-ps)
   1. [Attach an unmanaged disk with PowerShell](/powershell/module/az.compute/add-azvmdatadisk#example-2--add-a-data-disk-to-an-existing-virtual-machine)
1. Connect to the virtual machine.
1. Add the disks to the storage pool from [Server Manager](/windows-server/administration/server-manager/server-manager) > File and Storage Services > Volumes > Storage Pools. Use **Tasks** to select the **Add Physical Disk** option.
1. After the disk is added, right-click the target virtual disk and select **Extend virtual disk**.
1. Open [Disk Management](/windows-server/storage/disk-management/overview-of-disk-management), right-click the target volume, and select **Extend Volume**.

## Monitor storage performance

To assess storage needs and determine how well storage is performing, you need to understand what to measure and what those indicators mean.

| Metric | Description | How to Measure | Example Workloads |
| --- | --- | --- | --- |
| [IOPS (Input/Output operations per second)](/azure/virtual-machines/premium-storage-performance#iops) | Number of requests the application makes to storage per second | Performance Monitor counters: `Disk Reads/sec` and `Disk Writes/sec` | [OLTP (Online transaction processing)](/azure/architecture/data-guide/relational-data/online-transaction-processing) applications such as payment processing systems, online shopping, and retail point-of-sale systems |
| [Throughput](/azure/virtual-machines/premium-storage-performance#throughput) | Volume of data sent to the underlying storage, often measured in megabytes per second | Performance Monitor counters: `Disk Read Bytes/sec` and `Disk Write Bytes/sec` | [Data warehousing](/azure/architecture/data-guide/relational-data/data-warehousing) applications such as data stores for analysis, reporting, ETL workloads, and other business intelligence targets |

I/O unit sizes influence IOPS and throughput capabilities. Smaller I/O sizes yield higher IOPS, and larger I/O sizes yield higher throughput. SQL Server chooses the optimal I/O size automatically. For more information, see [Optimize IOPS, throughput, and latency for your applications](/azure/virtual-machines/premium-storage-performance#optimize-iops-throughput-and-latency-at-a-glance).

Specific Azure Monitor metrics are invaluable for discovering capping at the VM and disk level, as well as the consumption and the health of the AzureBlob cache. To identify key counters to add to your monitoring solution and Azure portal dashboard, see [Storage utilization metrics](/azure/virtual-machines/disks-metrics#storage-io-utilization-metrics).

> [!NOTE]  
> Azure Monitor doesn't currently offer disk-level metrics for the ephemeral temp drive `(D:\)`. VM Cached IOPS Consumed Percentage and VM Cached Bandwidth Consumed Percentage reflect IOPS and throughput from both the ephemeral temp drive `(D:\)` and host caching together.

## Monitor transaction log growth

Since a full transaction log can lead to performance problems and outages, monitor the available space in your transaction log and the utilized disk space of the drive that holds your transaction log. Address transaction log problems before they affect your workload.

Review [Troubleshoot a full transaction log](/sql/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002) if your log becomes full.

If you need to extend your disk, you can do so on the [Storage pane](storage-configuration.md#modify-existing-drives) of the [SQL virtual machines resource](manage-sql-vm-portal.md) if you deployed a SQL Server image from Azure Marketplace, or on the [Disks pane](/azure/virtual-machines/windows/expand-os-disk#expand-the-volume-in-the-operating-system) for your Azure virtual machine and self-installed SQL Server.

## Related content

- [Quick checklist](performance-guidelines-best-practices-checklist.md)
- [VM size](performance-guidelines-best-practices-vm-size.md)
- [Security](security-considerations-best-practices.md)
- [HADR settings](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md)
- [Updating SQL Server](servicing-updates-guidelines.md)
- [Optimize OLTP performance](https://techcommunity.microsoft.com/t5/sql-server/optimize-oltp-performance-with-sql-server-on-azure-vm/ba-p/916794)
- [SQL Server on Azure Virtual Machines Overview](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [Frequently Asked Questions](frequently-asked-questions-faq.yml)
