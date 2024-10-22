---
title: "Storage: Performance best practices & guidelines"
description: Provides storage best practices and guidelines to optimize the performance of your SQL Server on Azure Virtual Machines (VM).
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma, randolphwest
ms.date: 03/01/2024
ms.service: azure-vm-sql-server
ms.subservice: performance
ms.topic: conceptual
tags: azure-service-management
---
# Storage: Performance best practices for SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides storage best practices and guidelines to optimize performance for your SQL Server on Azure Virtual Machines (VM).

There's typically a trade-off between optimizing for costs and optimizing for performance. This performance best practices series is focused on getting the *best* performance for SQL Server on Azure VMs. If your workload is less demanding, you might not require every recommended optimization. Consider your performance needs, costs, and workload patterns as you evaluate these recommendations.

To learn more, see the other articles in this series: [Checklist](performance-guidelines-best-practices-checklist.md), [VM size](performance-guidelines-best-practices-vm-size.md), [Security](security-considerations-best-practices.md), [HADR configuration](hadr-cluster-best-practices.md), and [Collect baseline](performance-guidelines-best-practices-collect-baseline.md).

## Checklist

Review the following checklist for a brief overview of the storage best practices that the rest of the article covers in greater detail:

[!INCLUDE[storage best practices](../../includes/virtual-machines-best-practices-storage.md)]

To compare the storage checklist with the other best practices, see the comprehensive [Performance best practices checklist](performance-guidelines-best-practices-checklist.md).

## Overview

To find the most effective configuration for SQL Server workloads on an Azure VM, start by [measuring the storage performance of your business application](performance-guidelines-best-practices-collect-baseline.md#storage). Once storage requirements are known, select a virtual machine that supports the necessary IOPS and throughput with the appropriate memory-to-vCore ratio.

Choose a VM size with enough storage scalability for your workload and a mixture of disks (usually in a storage pool) that meet the capacity and performance requirements of your business.

The type of disk depends on both the file type that's hosted on the disk and your peak performance requirements.

> [!TIP]  
> Provisioning a SQL Server VM through the Azure portal helps guide you through the storage configuration process and implements most storage best practices such as creating separate storage pools for your data and log files, targeting `tempdb` to the `D:\` drive, and enabling the optimal caching policy. For more information about provisioning and configuring storage, see [SQL VM storage configuration](storage-configuration.md).

## VM disk types

You have a choice in the performance level for your disks. The types of managed disks available as underlying storage (listed by increasing performance capabilities) are Standard hard disk drives (HDD), Standard solid-state drives (SSD), Premium SSDs, Premium SSD v2, and Ultra Disks. 

For Standard HDDs, Standard SSDs, and Premium SSDs, the performance of the disk increases with the size of the disk, grouped by [premium disk labels](/azure/virtual-machines/disks-types#premium-ssds) such as the P1 with 4 GiB of space and 120 IOPS to the P80 with 32 TiB of storage and 20,000 IOPS. Premium storage supports a storage cache that helps improve read and write performance for some workloads. For more information, see [Managed disks overview](/azure/virtual-machines/managed-disks-overview).

The performance of Premium SSD v2 and Ultra Disks can be changed independently of the size of the disk, for details see [Ultra disk performance](/azure/virtual-machines/disks-types#ultra-disk-performance) and [Premium SSD v2 performance](/azure/virtual-machines/disks-types#premium-ssd-v2-performance).

There are also three main [disk roles](/azure/virtual-machines/managed-disks-overview#disk-roles) to consider for your SQL Server on Azure VM -  an OS disk, a temporary disk, and your data disks. Carefully choose what is stored on the operating system drive `(C:\)` and the ephemeral temporary drive `(D:\)`.

### Operating system disk

An operating system disk is a VHD that can be booted and mounted as a running version of an operating system and is labeled as the `C:\` drive. When you create an Azure VM, the platform attaches at least one disk to the VM for the operating system disk. The `C:\` drive is the default location for application installs and file configuration.

For production SQL Server environments, don't use the operating system disk for data files, log files, error logs.

### Temporary disk

Many Azure VMs contain another disk type called the temporary disk (labeled as the `D:\` drive). Depending on the VM series and size the capacity of this disk will vary. The temporary disk is ephemeral, which means the disk storage is recreated (as in, it's deallocated and allocated again), when the VM is restarted, or moved to a different host (for [service healing](/troubleshoot/azure/virtual-machines/understand-vm-reboot), for example).

The temporary storage drive isn't persisted to remote storage and therefore shouldn't store user database files, transaction log files, or anything that must be preserved. For example, you can use it for buffer pool extensions, the page file, and `tempdb`.

Place `tempdb` on the local temporary SSD `D:\` drive for SQL Server workloads unless consumption of local cache is a concern. If you're using a VM that [doesn't have a temporary disk](/azure/virtual-machines/azure-vms-no-temp-disk) then it's recommended to place `tempdb` on its own isolated disk or storage pool with caching set to read-only. To learn more, see [tempdb data caching policies](performance-guidelines-best-practices-storage.md#data-file-caching-policies).

### Data disks

Data disks are remote storage disks that are often created in [storage pools](/windows-server/storage/storage-spaces/overview) in order to exceed the capacity and performance that any single disk could offer to the VM.

Attach the minimum number of disks that satisfies the IOPS, throughput, and capacity requirements of your workload. Don't exceed the maximum number of data disks of the smallest VM you plan to resize to.

Place data and log files on data disks provisioned to best suit performance requirements.

Format your data disk to use 64-KB allocation unit size for all data files placed on a drive other than the temporary `D:\` drive (which has a default of 4 KB). SQL Server VMs deployed through Azure Marketplace come with data disks formatted with allocation unit size and interleave for the storage pool set to 64 KB.

> [!NOTE]  
> It's also possible to host your SQL Server database files directly on [Azure Blob storage](/sql/relational-databases/databases/sql-server-data-files-in-microsoft-azure) or on [SMB storage](/sql/database-engine/install-windows/install-sql-server-with-smb-fileshare-as-a-storage-option) such as [Azure premium file share](/azure/storage/files/storage-how-to-create-file-share), but we recommend using [Azure managed disks](/azure/virtual-machines/managed-disks-overview) for the best performance, reliability, and feature availability.

## Premium SSD v2

You should use [Premium SSD v2](/azure/virtual-machines/disks-types#premium-ssd-v2) disks when running SQL Server workloads in [supported regions](/azure/virtual-machines/disks-types#regional-availability), if the [current limitations](/azure/virtual-machines/disks-types#premium-ssd-v2-limitations) are suitable for your environment. Depending on your configuration, Premium SSD v2 can be cheaper than Premium SSDs, while also providing performance improvements. With Premium SSD v2, you can individually adjust your throughput or IOPS independently from the size of your disk. Being able to individually adjust performance options allows for this larger cost savings and allows you to script changes to meet performance requirements during anticipated or known periods of need. 

We recommend using Premium SSD v2 when using the [Ebdsv5 or Ebsv5 virtual machine series](/azure/virtual-machines/ebdsv5-ebsv5-series) as it is a more cost-effective solution for these high I/O throughput machines. 

You can [deploy your SQL Server VMs with Premium SSD v2](storage-configuration-premium-ssd-v2.md) by using the Azure portal (currently in preview).

If you're deploying your SQL Server VM by using the Azure portal and want to use Premium SSD v2, you're currently limited to the [Ebdsv5 or Ebsv5 series virtual machines](/azure/virtual-machines/ebdsv5-ebsv5-series). However, if you manually create your VM with Premium SSD v2 storage and then manually install SQL Server on the VM, you can use any VM series that supports Premium SSD v2. Be sure to [register](sql-agent-extension-manually-register-single-vm.md) your SQL Server VM with the SQL IaaS Agent extension so you can take advantage of all the [benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits) provided by the extension. 

## Azure Elastic SAN

[Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-introduction) is a network-attached storage offering that provides customers a flexible and scalable solution with the potential to reduce cost through storage consolidation. Azure Elastic SAN delivers a cost-effective, performant, and reliable block storage solution that connects to a variety of Azure compute services over iSCSI protocol. Elastic SAN enables a seamless transition from an existing SAN storage estate to the cloud without having to refactor customer application architecture. 

This solution can scale up to millions of IOPS, double-digit GB/s of throughput, and low single-digit millisecond latencies, with built-in resiliency to minimize downtime. Use Azure Elastic SAN if you need to consolidate storage, work with multiple compute services, or have workloads that require high throughput levels when driving storage over network bandwidth. However, since achieving desired IOPS/throughput for SQL Server workloads often requires overprovisioning capacity, _it's not typically appropriate for single SQL Server workloads_. Combining other low-performance workloads with SQL Server might be necessary to attain the most cost-effective solution. 

When considering VM sizing and performance for Azure Elastic SAN, it's important to understand that storage communication occurs over the network. For example, the VM size E4d_v5 does not support Azure Premium Storage but works well with Azure Elastic SAN as it supports up to 12,500-Mbps network throughput. When using Azure Elastic SAN with this VM size, you must ensure the network and storage throughput requirements for your workload fall under the 12,500-Mbps network throughput limit. 

Determine your network and storage requirements before deploying your SQL Server VM with an Azure Elastic SAN, and then carefully monitor network and storage utilization to confirm the chosen VM can accommodate the workload. To learn more, review [VM performance with Elastic SAN volumes](/azure/storage/elastic-san/elastic-san-performance) and [Elastic SAN metrics](/azure/storage/elastic-san/elastic-san-metrics). 

> [!CAUTION]
> - VM sizing with Elastic SAN must accommodate production (VM to VM) network throughput requirements in addition to storage throughput. When using Elastic SAN, VM sizes that optimize for IO throughput might not be as cost-effective as VM sizes that optimize for network bandwidth. 


Consider placing SQL Server workloads on Elastic SAN for better cost efficiency because: 

- **Storage consolidation and dynamic performance sharing**: Normally for SQL Server on Azure VM workloads, disk storage is provisioned on a per VM basis based on your capacity and peak performance requirements for that VM. This overprovisioned performance is available when needed but the unused performance can't be shared with workloads on other VMs. Elastic SAN, similar to on-premises SAN, allows consolidating storage needs of multiple SQL and non-SQL workloads to achieve better cost efficiency, with the ability to dynamically share provisioned performance across the volumes provisioned to these different workloads based on IO demands. For example, in the East US region, say you have 10 workloads that require 2-TiB capacity and 10K IOPS each, but collectively they don't need more than 60,000 IOPS at any point in time. You can configure an Elastic SAN with 12 base units (1 base unit = $0.08 per GiB/month) that will give you 12 TiB capacity and the needed 60K IOPS, and 8 capacity-only units (1 capacity-only unit = $0.06 per GiB/month), which gives you the remaining 8-TiB capacity at a lower cost. This optimal storage configuration provides better cost efficiency while providing the necessary performance (10K IOPS) to each of these workloads. For more information on Elastic SAN base and capacity-only provisioning units, see [Planning for an Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-planning#storage-and-performance) and for pricing, visit [Azure Elastic SAN - Pricing](https://azure.microsoft.com/pricing/details/elastic-san/).
- **To drive higher storage throughput**: SQL Server on Azure VM deployments occasionally require overprovisioning a VM due to disk throughput limits for that VM. You can avoid this with Elastic SAN, since you drive higher storage throughput over compute network bandwidth with the iSCSI protocol. For example, a Standard_E32ds_v5 VM is capped at 51,200 IOPS and 865 MBps for disk/storage throughput, but it can achieve up to a maximum of 2,000 MBps network throughput. If the storage throughput requirement for your workload is greater than 865 MBps, you won't have to upgrade the VM to a larger SKU since it can now support up to 2,000 MBps by using Elastic SAN.


## Premium SSD

Use Premium SSDs for data and log files for production SQL Server workloads. Premium SSD IOPS and bandwidth vary based on the [disk size and type](/azure/virtual-machines/disks-types).

For production workloads, use the P30 and/or P40 disks for SQL Server data files to ensure caching support and use the P30 up to P80 for SQL Server transaction log files. For the best total cost of ownership, start with P30s (5000 IOPS/200 MBps) for data and log files and only choose higher capacities when you need to control the VM disk count. For dev/test or small systems you can choose to use sizes smaller than P30 as these do support caching, but they don't offer reserved pricing.

For OLTP workloads, match the target IOPS per disk (or storage pool) with your performance requirements using workloads at peak times and the `Disk Reads/sec` + `Disk Writes/sec` performance counters. For data warehouse and reporting workloads, match the target throughput using workloads at peak times and the `Disk Read Bytes/sec` + `Disk Write Bytes/sec`.

Use Storage Spaces to achieve optimal performance, configure two pools, one for the log file(s) and the other for the data files. If you aren't using disk striping, use two premium SSD disks mapped to separate drives, where one drive contains the log file and the other contains the data.

The [provisioned IOPS and throughput](/azure/virtual-machines/disks-types#premium-ssds) per disk that is used as part of your storage pool. The combined IOPS and throughput capabilities of the disks is the maximum capability up to the throughput limits of the VM.

The best practice is to use the least number of disks possible while meeting the minimal requirements for IOPS (and throughput) and capacity. However, the balance of price and performance tends to be better with a large number of small disks rather than a small number of large disks.

### Scale premium disks

The size of your Premium SSD determines the initial performance tier of your disk. Designate the performance tier at deployment or change it afterwards, without changing the size of the disk. If demand increases, you can increase the performance level to meet your business needs.

Changing the performance tier allows administrators to prepare for and meet higher demand without relying on [disk bursting](/azure/virtual-machines/disk-bursting#credit-based-bursting).

Use the higher performance for as long as needed where billing is designed to meet the storage performance tier. Upgrade the tier to match the performance requirements without increasing the capacity. Return to the original tier when the extra performance is no longer required.

This cost-effective and temporary expansion of performance is a strong use case for targeted events such as shopping, performance testing, training events and other brief windows where greater performance is needed only for a short term.

For more information, see [Performance tiers for managed disks](/azure/virtual-machines/disks-change-performance).

## Azure ultra disk

If there's a need for submillisecond response times with reduced latency consider using [Azure ultra disk](/azure/virtual-machines/disks-types#ultra-disks) for the SQL Server log drive, or even the data drive for applications that are extremely sensitive to I/O latency.

Ultra disk can be configured where capacity and IOPS can scale independently. With ultra disk administrators can provision a disk with the capacity, IOPS, and throughput requirements based on application needs.

Ultra disk isn't supported on all VM series and has other limitations such as region availability, redundancy, and support for Azure Backup. To learn more, see [Using Azure ultra disks](/azure/virtual-machines/disks-enable-ultra-ssd) for a full list of limitations.

## Standard HDDs and SSDs

[Standard HDDs](/azure/virtual-machines/disks-types#standard-hdds) and SSDs have varying latencies and bandwidth and are only recommended for dev/test workloads. Production workloads should use Premium SSD v2 or Premium SSDs. If you're using Standard SSD (dev/test scenarios), the recommendation is to add the maximum number of data disks supported by your [VM size](/azure/virtual-machines/sizes?toc=/azure/virtual-machines/windows/toc.json) and use disk striping with Storage Spaces for the best performance.

## Caching

VMs that support premium storage caching can take advantage of an additional feature called the Azure BlobCache or host caching to extend the IOPS and throughput capabilities of a VM. VMs enabled for both premium storage and premium storage caching have these two different storage bandwidth limits that can be used together to improve storage performance.

The IOPS and MBps throughput without caching counts against a VM's uncached disk throughput limits. The maximum cached limits provide another buffer for reads that helps address growth and unexpected peaks.

Enable premium caching whenever the option is supported to significantly improve performance for reads against the data drive without extra cost.

Reads and writes to the Azure BlobCache (cached IOPS and throughput) don't count against the uncached IOPS and throughput limits of the VM.

> [!NOTE]  
> Disk Caching is not supported for disks 4 TiB and larger (P50 and larger). If multiple disks are attached to your VM, each disk that is smaller than 4 TiB will support caching. For more information, see [Disk caching](/azure/virtual-machines/premium-storage-performance#disk-caching).

### Uncached throughput

The max uncached disk IOPS and throughput is the maximum remote storage limit that the VM can handle. This limit is defined at the VM and isn't a limit of the underlying disk storage. This limit applies only to I/O against data drives remotely attached to the VM, not the local I/O against the temp drive (`D:\` drive) or the OS drive.

The amount of uncached IOPS and throughput that is available for a VM can be verified in the documentation for your VM.

For example, the [M-series](/azure/virtual-machines/m-series) documentation shows that the max uncached throughput for the Standard_M8ms VM is 5000 IOPS and 125 MBps of uncached disk throughput.

:::image type="content" source="./media/performance-guidelines-best-practices/m-series-table.png" alt-text="Screenshot showing M-series uncached disk throughput documentation." lightbox="./media/performance-guidelines-best-practices/m-series-table.png":::

Likewise, you can see that the Standard_M32ts supports 20,000 uncached disk IOPS and 500-MBps uncached disk throughput. This limit is governed at the VM level regardless of the underlying premium disk storage.

For more information, see [uncached and cached limits](/azure/virtual-machines/disks-performance#virtual-machine-uncached-vs-cached-limits).

### Cached and temp storage throughput

The max cached and temp storage throughput limit is a separate limit from the uncached throughput limit on the VM. The Azure BlobCache consists of a combination of the VM host's random-access memory and locally attached SSD. The temp drive (`D:\` drive) within the VM is also hosted on this local SSD.

The max cached and temp storage throughput limit governs the I/O against the local temp drive (`D:\` drive) and the Azure BlobCache **only if** host caching is enabled.

When caching is enabled on premium storage, VMs can scale beyond the limitations of the remote storage uncached VM IOPS and throughput limits.

Only certain VMs support both premium storage and premium storage caching (which needs to be verified in the virtual machine documentation). For example, the [M-series](/azure/virtual-machines/m-series) documentation indicates that both premium storage, and premium storage caching is supported:

:::image type="content" source="./media/performance-guidelines-best-practices/m-series-table-premium-support.png" alt-text="Screenshot showing M-Series Premium Storage support.":::

The limits of the cache vary based on the VM size. For example, the Standard_M8ms VM supports 10000 cached disk IOPS and 1000-MBps cached disk throughput with a total cache size of 793 GiB. Similarly, the Standard_M32ts VM supports 40000 cached disk IOPS and 400-MBps cached disk throughput with a total cache size of 3,174 GiB.

:::image type="content" source="./media/performance-guidelines-best-practices/m-series-table-cached-temp.png" alt-text="Screenshot showing M-series cached disk throughput documentation." lightbox="./media/performance-guidelines-best-practices/m-series-table-cached-temp.png":::

You can manually enable host caching on an existing VM. Stop all application workloads and the SQL Server services before any changes are made to your VM's caching policy. Changing any of the VM cache settings results in the target disk being detached and reattached after the settings are applied.

### Data file caching policies

Your storage caching policy varies depending on the type of SQL Server data files that are hosted on the drive.

The following table provides a summary of the recommended caching policies based on the type of SQL Server data:

| SQL Server disk | Recommendation |
| --- | --- |
| **Data disk** | Enable `Read-only` caching for the disks hosting SQL Server data files.<br />Reads from cache will be faster than the uncached reads from the data disk.<br />Uncached IOPS and throughput plus Cached IOPS and throughput yield the total possible performance available from the VM within the VMs limits, but actual performance varies based on the workload's ability to use the cache (cache hit ratio).<br />|
| **Transaction log disk** | Set the caching policy to `None` for disks hosting the transaction log. There's no performance benefit to enabling caching for the Transaction log disk, and in fact having either `Read-only` or `Read/Write` caching enabled on the log drive can degrade performance of the writes against the drive and decrease the amount of cache available for reads on the data drive. |
| **Operating OS disk** | The default caching policy is `Read/write` for the OS drive.<br />It isn't recommended to change the caching level of the OS drive. |
| `tempdb` | If `tempdb` can't be placed on the ephemeral drive `D:\` due to capacity reasons, either resize the VM to get a larger ephemeral drive or place `tempdb` on a separate data drive with `Read-only` caching configured.<br />The VM cache and ephemeral drive both use the local SSD, so keep this in mind when sizing as `tempdb` I/O will count against the cached IOPS and throughput VM limits when hosted on the ephemeral drive. |

> [!IMPORTANT]  
> Changing the cache setting of an Azure disk detaches and reattaches the target disk. When changing the cache setting for a disk that hosts SQL Server data, log, or application files, be sure to stop the SQL Server service along with any other related services to avoid data corruption.

To learn more, see [Disk caching](/azure/virtual-machines/premium-storage-performance#disk-caching).

## Disk striping

Analyze the throughput and bandwidth required for your SQL data files to determine the number of data disks, including the log file and `tempdb`. Throughput and bandwidth limits vary by VM size. For more information, see [VM sizes](/azure/virtual-machines/sizes).

Add more data disks and use disk striping for more throughput. For example, an application that needs 12,000 IOPS and 180-MB/s throughput can use three striped P30 disks to deliver 15,000 IOPS and 600-MB/s throughput.

To configure disk striping, see [disk striping](storage-configuration.md#disk-striping).

## Disk capping

There are throughput limits at both the disk and VM level. The maximum IOPS limits per VM and per disk differ and are independent of each other.

Applications that consume resources beyond these limits will be throttled (also known as capped). Select a VM and disk size in a disk stripe that meets application requirements and won't face capping limitations. To address capping, use caching, or tune the application so that less throughput is required.

For example, an application that needs 12,000 IOPS and 180 MB/s can:

- Use the [Standard_M32ms](/azure/virtual-machines/m-series), which has a maximum uncached disk throughput of 20,000 IOPS and 500 MBps.
- Stripe three P30 disks to deliver 15,000 IOPS and 600-MB/s throughput.
- Use a [Standard_M16ms](/azure/virtual-machines/m-series) VM and use host caching to utilize local cache over consuming throughput.

VMs configured to scale up during times of high utilization should provision storage with enough IOPS and throughput to support the maximum VM size while keeping the overall number of disks less than or equal to the maximum number supported by the smallest VM SKU targeted to be used.

For more information on disk capping limitations and using caching to avoid capping, see [Disk IO capping](/azure/virtual-machines/disks-performance).

> [!NOTE]  
> Some disk capping may still result in satisfactory performance to users; tune and maintain workloads rather than resize to a larger VM to balance managing cost and performance for the business.

## Write Acceleration

Write Acceleration is a disk feature that is only available for the [M-Series](/azure/virtual-machines/m-series) VMs. The purpose of Write Acceleration is to improve the I/O latency of writes against Azure Premium Storage when you need single digit I/O latency due to high volume mission critical OLTP workloads or data warehouse environments.

Use Write Acceleration to improve write latency to the drive hosting the log files. Don't use Write Acceleration for SQL Server data files.

Write Accelerator disks share the same IOPS limit as the VM. Attached disks can't exceed the Write Accelerator IOPS limit for a VM.

The following table outlines the number of data disks and IOPS supported per VM:

| VM SKU | # Write Accelerator disks | Write Accelerator disk IOPS per VM |
| --- | --- | --- |
| M416ms_v2, M416s_v2 | 16 | 20000 |
| M128ms, M128s | 16 | 20000 |
| M208ms_v2, M208s_v2 | 8 | 10000 |
| M64ms, M64ls, M64s | 8 | 10000 |
| M32ms, M32ls, M32ts, M32s | 4 | 5000 |
| M16ms, M16s | 2 | 2500 |
| M8ms, M8s | 1 | 1250 |

There are several restrictions to using Write Acceleration. To learn more, see [Restrictions when using Write Accelerator](/azure/virtual-machines/how-to-enable-write-accelerator#restrictions-when-using-write-accelerator).

### Compare to Azure ultra disk

The biggest difference between Write Acceleration and Azure ultra disks is that Write Acceleration is a VM feature only available for the M-Series and Azure ultra disks is a storage option. Write Acceleration is a write-optimized cache with its own limitations based on the VM size. Azure ultra disks are a low latency disk storage option for Azure VMs.

If possible, use Write Acceleration over ultra disks for the transaction log disk. For VMs that don't support Write Acceleration but require low latency to the transaction log, use Azure ultra disks.

## Monitor storage performance

To assess storage needs, and determine how well storage is performing, you need to understand what to measure, and what those indicators mean.

[IOPS (Input/Output per second)](/azure/virtual-machines/premium-storage-performance#iops) is the number of requests the application is making to storage per second. Measure IOPS using Performance Monitor counters `Disk Reads/sec` and `Disk Writes/sec`. [OLTP (Online transaction processing)](/azure/architecture/data-guide/relational-data/online-transaction-processing) applications need to drive higher IOPS in order to achieve optimal performance. Applications such as payment processing systems, online shopping, and retail point-of-sale systems are all examples of OLTP applications.

[Throughput](/azure/virtual-machines/premium-storage-performance#throughput) is the volume of data that is being sent to the underlying storage, often measured by megabytes per second. Measure throughput with the Performance Monitor counters `Disk Read Bytes/sec` and `Disk Write Bytes/sec`. [Data warehousing](/azure/architecture/data-guide/relational-data/data-warehousing) is optimized around maximizing throughput over IOPS. Applications such as data stores for analysis, reporting, ETL workstreams, and other business intelligence targets are all examples of data warehousing applications.

I/O unit sizes influence IOPS and throughput capabilities as smaller I/O sizes yield higher IOPS and larger I/O sizes yield higher throughput. SQL Server chooses the optimal I/O size automatically. For more information about, see [Optimize IOPS, throughput, and latency for your applications](/azure/virtual-machines/premium-storage-performance#optimize-iops-throughput-and-latency-at-a-glance).

There are specific Azure Monitor metrics that are invaluable for discovering capping at the VM and disk level as well as the consumption and the health of the AzureBlob cache. To identify key counters to add to your monitoring solution and Azure portal dashboard, see [Storage utilization metrics](/azure/virtual-machines/disks-metrics#storage-io-utilization-metrics).

> [!NOTE]  
> Azure Monitor doesn't currently offer disk-level metrics for the ephemeral temp drive `(D:\)`. VM Cached IOPS Consumed Percentage and VM Cached Bandwidth Consumed Percentage will reflect IOPS and throughput from both the ephemeral temp drive `(D:\)` and host caching together.

## Monitor transaction log growth

Since a full transaction log can lead to performance issues and outages, it's important to monitor the available space in your transaction log, as well as the utilized disk space of the drive that holds your transaction log. Address transaction log issues before they impact your workload. 

Review [Troubleshoot a full transaction log](/sql/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002) if your log becomes full. 

If you need to extend your disk, you can do so on the [Storage pane](storage-configuration.md#modify-existing-drives) of the [SQL virtual machines resource](manage-sql-vm-portal.md) if you deployed a SQL Server image from Azure Marketplace, or on the [Disks pane](/azure/virtual-machines/windows/expand-os-disk#expand-the-volume-in-the-operating-system) for your Azure virtual machine and self-installed SQL Server. 


## Next steps

To learn more, see the other articles in this best practices series:

- [Quick checklist](performance-guidelines-best-practices-checklist.md)
- [VM size](performance-guidelines-best-practices-vm-size.md)
- [Security](security-considerations-best-practices.md)
- [HADR settings](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md)

- For security best practices, see [Security considerations for SQL Server on Azure Virtual Machines](security-considerations-best-practices.md).

- For detailed testing of SQL Server performance on Azure VMs with TPC-E and TPC_C benchmarks, refer to the blog [Optimize OLTP performance](https://techcommunity.microsoft.com/t5/sql-server/optimize-oltp-performance-with-sql-server-on-azure-vm/ba-p/916794).

- Review other SQL Server Virtual Machine articles at [SQL Server on Azure Virtual Machines Overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).
