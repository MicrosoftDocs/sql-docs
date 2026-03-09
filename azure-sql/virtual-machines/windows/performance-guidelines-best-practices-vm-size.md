---
title: "VM size: Performance best practices & guidelines"
description: Provides VM size guidelines and best practices to optimize the performance of your SQL Server on Azure Virtual Machine (VM).
author: dplessMSFT
ms.author: dpless
ms.reviewer: randolphwest, mathoma
ms.date: 02/11/2026
ms.service: azure-vm-sql-server
ms.subservice: performance
ms.topic: best-practice
ai-usage: ai-assisted
ms.custom:
  - ignite-2024
tags: azure-service-management
---
# VM size: Performance best practices for SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides VM size guidance and best practices to optimize performance for your SQL Server on Azure Virtual Machines (VMs).

There's typically a trade-off between optimizing for costs and optimizing for performance. This performance best practices series focuses on getting the *best* performance for SQL Server on Azure Virtual Machines. If your workload is less demanding, you might not need every recommended optimization. Consider your performance needs, costs, and workload patterns as you evaluate these recommendations.

For comprehensive details, see the other articles in this series: [Checklist](performance-guidelines-best-practices-checklist.md), [Storage](performance-guidelines-best-practices-storage.md), [Security](security-considerations-best-practices.md), [HADR configuration](hadr-cluster-best-practices.md), [Collect baseline](performance-guidelines-best-practices-collect-baseline.md).

[!INCLUDE [sql-vm-deployment-failure](../../includes/sql-vm-deployment-failure.md)]

## Checklist

Review the following checklist for a brief overview of the VM size best practices that the rest of the article covers in greater detail:

[!INCLUDE[vm size best practices](../../includes/virtual-machines-best-practices-vm-size.md)]

To compare the VM size checklist with the others, see the comprehensive [Performance best practices checklist](performance-guidelines-best-practices-checklist.md).

## Overview

High performing SQL Server workloads often need larger amounts of memory, IOPS, and throughput without the higher vCore counts.

Most OLTP workloads are application databases driven by large numbers of smaller transactions. With OLTP workloads, you read or modify only a small amount of the data, but the volumes of transactions driven by user counts are much higher. It's important to have the SQL Server memory available to cache plans, store recently accessed data for performance, and ensure physical reads can be read into memory quickly.

These OLTP environments need higher amounts of memory, fast storage, and the I/O bandwidth necessary to perform optimally.

Before choosing a VM size for your SQL Server VM, first get your [storage configuration](performance-guidelines-best-practices-storage.md) correct. Resizing your VM is simple, but modifying your storage configuration if it doesn't meet your IOPS or throughput needs often requires redeployment.

First, collect a [baseline](performance-guidelines-best-practices-collect-baseline.md) from your source environment under the highest stress conditions and then configure your storage based on the IOPS and throughput needs of your workload. Before settling on a storage configuration, plan for growth - typically 20% in most environments.
 
If you're using Premium SSD v2 or Ultra Disk storage, you can adjust the IOPS and throughput down, but you can't exceed the capability of the deployed storage solution.

> [!NOTE]
> - The [Azure VM size](/azure/virtual-machines/sizes/overview) documentation has the most up-to-date information on available VM sizes in Azure. If any discrepancies exist between this article and the Azure VM size documentation, the Azure VM size documentation takes precedence.
> - vCore and vCPU are used interchangeably in this and the Azure VM documentation.

When selecting a VM size for SQL Server on Azure VMs, consider the following performance best practices and guidelines.

### Migrating existing environments

When you're creating a SQL Server on Azure VM, carefully consider the type of workload necessary.

If you're migrating an existing environment, [collect a performance baseline](performance-guidelines-best-practices-collect-baseline.md) to determine your SQL Server on Azure VM requirements.

Use the vCore and memory configuration from your source system as a baseline for migrating a current on-premises SQL Server database to SQL Server on Azure VMs.

If you have Software Assurance, take advantage of the [Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/) which grants you the ability to allocate your SQL Server licenses to SQL Server on Azure VMs.

### Creating new VMs

If you're creating a new VM, create your new SQL Server VM based on your application requirements.

If you're creating a new SQL Server VM for a new application built for the cloud, you can easily scale your SQL Server VM as your data and usage requirements evolve.

Start development environments with the lower-tier D-Series or B-Series and grow your environment over time.

Consider the following VM series based on your SQL Server workloads:
- **Highest memory allocation for mission critical workloads**: The [Mdsv3-series](#msv3-and-mdsv3-high-memory-series) and [Mbdsv3-series](#mbdsv3-series) VMs offer the highest memory allocation in Azure, with the best storage performance.
- **High I/O throughput-to-vCore ratio**: Throughput matters more than IOPS for SQL Server when the workload is dominated by large, sustained, sequential data movement such as analytics, ETL, index maintenance, and backups - where IOPS are plentiful but the storage pipe (MB/s) becomes the bottleneck. The [Mbdsv3-series VMs](#mbdsv3-series) offer some of the highest throughput-to-vCore ratio of any VM series in any cloud with *78.125 Throughput / vCPU (MB/s per vCore)*. The [Ebdsv5-series VMs](#ebdsv5-series) also offer high throughput-to-vCore with *89.286 Throughput / vCPU (MB/s per vCore)*. 

   If you don't know the storage requirements for your SQL Server workload, the [Ebdsv5-series](#ebdsv5-series) is the one most likely to meet your needs. See the [storage](performance-guidelines-best-practices-storage.md) article to learn more.
- **Parallel processing for high vCore count VMs**: The [Msv3 and Mdsv3 series VMs](#msv3-and-mdsv3-medium-memory-series) offer high parallel processing, making them good options for larger data warehouse environments with higher memory requirements.

### Memory-to-vCore ratio scaling

For smaller SQL Server environment that don't require large amounts of memory, a 4:1 memory-to-vCore ratio such as the D-Series is a good starting point in Azure.
 
For mission critical OLTP, and the best starting point for SQL Server workloads, use an 8:1 memory-to-vCore ratio with the Ebdsv5 as the recommended option.
 
SQL Server data warehouse and mission critical environments often need to scale beyond the 8:1 memory-to-vCore ratio.
 
For larger data warehouse environments, choose a 16:1 memory-to-vCore ratio or larger such as the Mds_v3 high memory and very high memory options.

### Using marketplace images

Use the SQL Server VM marketplace images with the storage configuration in the portal. This approach makes it easier to properly create the storage pools necessary to get the size, IOPS, and throughput necessary for your workloads.

Choose SQL Server VMs that support premium storage performance.

See the [storage](performance-guidelines-best-practices-storage.md) article to learn more.

## Supportability

Consider the following limitations when installing SQL Server to Azure VMs: 

- SQL Server on Azure VMs don't support [Azure Virtual Machine Scale Sets](/azure/virtual-machine-scale-sets/overview). The [Automatic guest patching feature](/azure/virtual-machines/automatic-vm-guest-patching) available with Azure Virtual Machine Scale Sets replaces the OS disk when a new image version is released. If you use the Automatic guest patching feature with your SQL Server on Azure VM, you're likely to disrupt SQL Server functionality, leading to potential corruption, data loss, and availability problems. 
- SQL Server isn't supported on systems with more than 64 vCores per NUMA node. [Disable SMT or hyperthreading](/sql/sql-server/compute-capacity-limits-by-edition-of-sql-server#disable-smt-in-an-azure-virtual-machine) to use SQL Server on Azure VMs that exceed 64 vCores per NUMA node. By using the [Configurable constrained core](vm-vcore-customization-for-sql.md) feature, you can disable SMT or hyperthreading directly in the Azure portal.
- SQL Server currently supports disks with a standard native [sector sizes of 512 bytes and 4 KB](/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022#StorageTypes). Installing SQL Server to disks with 8-KB sector sizes isn't supported and can lead to [installation failures](/troubleshoot/sql/azure-sql/sql-installation-fails-sector-size-error-azure-vm), as well as [performance degradation from misaligned I/O](/troubleshoot/sql/database-engine/performance/performance-degradation-misaligned-io-sector-error). 
- SQL Server on Azure VM images fail to deploy with VM sizes that have uninitialized ephemeral disks. To learn more, review [Some SQL Server on Azure VM images fail to deploy](/troubleshoot/sql/azure-sql/sql-deployment-fails-drive-not-ready).

## Filtering by VM size 

When you deploy an Azure VM, use the [naming convention guidance](/azure/virtual-machines/vm-naming-conventions) to determine the VM size name to filter by in the portal.

The VM size name combines the family, subfamily, number of CPUs, and any additive features.

**Example:**

When you filter for an [Ebdsv5 series](/azure/virtual-machines/ebdsv5-ebsv5-series#ebdsv5-series-nvme) VM, enter the VM size name such as `E64bds` or the version such as `v5`, which the portal refers to as **Generation**.

The following screenshot demonstrates filtering the VM size list by the `v5` version in the Azure portal: 

:::image type="content" source="media/performance-guidelines-best-practices-vm-size/filter-vm-size-list.png" alt-text="Screenshot of the Azure portal VM size filter example filtering by generation v5.":::


Consider the following points:
- You can apply additional filters by using 'Add filter' to narrow your VM size list based on factors like the VM size, type (family) such as memory-optimized or general purpose, and disk controller type. 
- If you don't see a result for the VM you're searching for, it's likely due to a filter you applied to the VM size list. Clear the filter and try again.
- The disk controller filter helps you identify if the storage is iSCSI or NVMe. 

## Memory optimized M-series VMs

The memory-optimized [M-series](/azure/virtual-machines/sizes/memory-optimized/m-family) offers vCore counts and memory for some of the largest SQL Server workloads.

The following list describes the capabilities of M-series VMs:
- Support [premium storage](/azure/virtual-machines/premium-storage-performance), [premium storage caching](/azure/virtual-machines/premium-storage-performance#disk-caching), [Ultra Disks](/azure/virtual-machines/disks-enable-ultra-ssd), [write acceleration](/azure/virtual-machines/how-to-enable-write-accelerator), and accelerated networking.
- Are suitable for SQL Server workloads that require high computing capabilities with large memory footprints and less emphasis on storage performance. 


### Mbdsv3 series

| Parameter | Value |
|-----------|-------|
| **Processor** | Intel® Xeon® Scalable 4th Gen (Sapphire Rapids) |
| **Memory-to-vCore ratio** | 8:1 to 22:1 (varies by size) |
| **Max vCores** | 176 |
| **Memory** | Up to 3,892 GiB (~3.8 TiB) |
| **Max IOPS** | 650,000 |
| **Max throughput** | 10,000 MBps (Ultra Disk/Premium SSD v2) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Mission critical OLAP, data warehouse, `tempdb` optimization, reporting |
| **Ephemeral storage for `tempdb`** | Yes (capacity varies by size) |


The [Mbdsv3-series](/azure/virtual-machines/sizes/memory-optimized/mbdsv3-series) is a memory-optimized VM series designed for large in-memory databases and workloads that need a high memory-to-vCore ratio. The VMs in this series use 4th generation Intel® Xeon® Scalable (Sapphire Rapids) processors and come in different memory sizes and vCore counts to fit your SQL Server workloads. Use Mbdsv3 VMs for mission critical and data warehouse workloads.

Mbdsv3 VMs work well for large in-memory databases and workloads that need a high memory-to-vCore ratio. They're great for relational database servers, data warehousing, heavy reporting, large caches, and in-memory analytics.

Mbdsv3 VMs provide up to 176 vCores with extensive memory capacity and exceptional storage performance. This VM series delivers more than a 50% improvement in IOPS and throughput compared to the top-performing Ebdsv5 VMs. Mbdsv3 VMs are one of the highest-performing VM options available in any cloud. Mbdsv3 VMs have similar performance characteristics to Mbsv3 VMs but include strong local and ephemeral storage. This storage makes them perfect for `tempdb` performance optimization, reporting, mission critical OLAP, and data warehousing workloads.

### Msv3 and Mdsv3 Medium Memory series

| Parameter | Value |
|-----------|-------|
| **Processor** | Intel® Xeon® Scalable 4th Gen (Sapphire Rapids) |
| **Memory-to-vCore ratio** | 20:1 to 22:1 (varies by size) |
| **Max vCores** | 176 <br/> [SMT/hyperthreading must be disabled](/sql/sql-server/compute-capacity-limits-by-edition-of-sql-server#disable-smt-in-an-azure-virtual-machine) for VMs that exceed 64 vCores per NUMA node. |
| **Memory** | Up to 3,892 GiB (~3.8 TiB) |
| **Max IOPS** | 130,000 |
| **Max throughput** | 4,000 MBps (NVMe interface) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | High-performance computing, large databases |
| **Ephemeral storage for `tempdb`** | Msv3: No; Mdsv3: Yes (400 GiB capacity) |

The [Msv3](/azure/virtual-machines/sizes/memory-optimized/msv3-mm-series) and [Mdsv3](/azure/virtual-machines/sizes/memory-optimized/mdsv3-mm-series) Medium Memory VMs offer computing power and memory capabilities at memory levels starting at 240 up to 3,892 GiB. These VMs provide improved performance, scalability, and resilience to failures compared to the previous generation Mv2 VMs.

The Msv3 and Mdsv3 Medium Memory VMs use 4th generation Intel® Xeon® Scalable (Sapphire Rapids) processors and offer VM sizes of up to 176 vCores with substantial memory capacity and high remote storage performance by using the NVMe interface.

### Msv3 and Mdsv3 High Memory series

| Parameter | Value |
|-----------|-------|
| **Processor** | Intel® Xeon® Scalable 4th Gen (Sapphire Rapids) |
| **Memory-to-vCore ratio** | 14:1 to 18:1 (varies by size) |
| **Max vCores** | 832 <br/> [SMT/hyperthreading must be disabled](/sql/sql-server/compute-capacity-limits-by-edition-of-sql-server#disable-smt-in-an-azure-virtual-machine) for VMs that exceed 64 vCores per NUMA node. |
| **Memory** | 5,696 GiB to 15,200 GiB (~5.5 to 14.8 TiB) |
| **Max IOPS** | 260,000 |
| **Max throughput** | 8,000 MBps (NVMe interface) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Very large in-memory databases, SAP HANA |
| **Ephemeral storage for `tempdb`** | Msv3: No; Mdsv3: Yes (400 GiB capacity) |

The [Msv3](/azure/virtual-machines/sizes/memory-optimized/msv3-hm-series) and [Mdsv3](/azure/virtual-machines/sizes/memory-optimized/mdsv3-hm-series) High Memory VMs are designed for high memory workloads with memory ranging from 5,696 GiB to 15,200 GiB (about 5.5 to 14.8 TiB). These VMs use 4th generation Intel® Xeon® Scalable (Sapphire Rapids) processors and support up to 832 vCores with high-performance remote storage using the NVMe interface. These VMs provide improved performance, scalability, and resilience to failures compared to the previous generation Mv2 VMs.


### Mdsv3 Very High Memory series

| Parameter | Value |
|-----------|-------|
| **Processor** | Intel® Xeon® Platinum 8490H (Sapphire Rapids) |
| **Memory-to-vCore ratio** | 17:1 to 34:1 (896 vCores with 23-30 TiB) |
| **Max vCores** | 1,792 <br/> [SMT/hyperthreading must be disabled](/sql/sql-server/compute-capacity-limits-by-edition-of-sql-server#disable-smt-in-an-azure-virtual-machine) for VMs that exceed 64 vCores per NUMA node. |
| **Memory** | 23,088 GiB to 30,400 GiB (~22.5 to 29.7 TiB) |
| **Max IOPS** | 110,000 |
| **Max throughput** | 8,000 MBps |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Largest in-memory databases, SAP HANA, mission critical |
| **Ephemeral storage for `tempdb`** | Yes (4,096 GiB capacity) |

The [Mdsv3 Very High Memory Series](/azure/virtual-machines/sizes/memory-optimized/mdsv3-vhm-series) use 4th generation Intel® Xeon® Platinum 8490H (Sapphire Rapids) processors. They have the largest memory footprint of any M-series based virtual machines, offering 23,088 GiB to 30,400 GiB (~22.5 to 29.7 TiB) of memory, 1,792 vCores, and high-performance remote storage. These VMs provide improved performance, scalability, and resilience to failures compared to the previous generation Mv2 VMs.

## Memory-optimized E-series VMs

The memory-optimized [E-series](/azure/virtual-machines/sizes/memory-optimized/e-family) VMs are designed for memory-intensive workloads, such as large databases, big data analytics, and enterprise applications that require significant amounts of RAM to maintain high performance. 

### Easv7 series

| Parameter | Value |
|-----------|-------|
| **Processor** | AMD EPYC™ 9005 5th Gen (Turin) |
| **Memory-to-vCore ratio** | 8:1 |
| **Max vCores** | 160 |
| **Memory** | Up to 1,280 GiB |
| **Max IOPS** | 310,000 (Ultra Disk/Premium SSD v2); 212,000 (Premium SSD) |
| **Max throughput** | 10,356 MBps (Ultra Disk/Premium SSD v2); 10,344 MBps (Premium SSD) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | General OLTP, lighter production, cost-conscious deployments |
| **Ephemeral storage for `tempdb`** | No |

The [Easv7‑series VMs](/azure/virtual-machines/sizes/memory-optimized/easv7-series) work well for SQL Server workloads that need a balanced memory-to-vCore ratio and predictable compute performance. They're especially good when local temp storage isn't needed and overall storage demands are moderate. Built on AMD's 5th Generation EPYC™ 9005 (Turin) processors, this series offers modern CPU efficiency with stable remote storage behavior. It's a practical choice for SQL Server deployments that prioritize consistency, cost efficiency, and straightforward scalability.
 
With an 8:1 memory-to-vCore ratio, up to 160 vCores, and high storage throughput, the Easv7 VMs deliver performance without forcing you to overprovision CPU just to reach acceptable memory levels. Support for premium storage ensures compatibility with modern Azure storage options and enables predictable, scalable remote I/O performance for user databases. 
 
Although the series doesn't include local ephemeral storage, it performs well for workloads where `tempdb` activity is moderate and can be efficiently handled on remote disks.
 
The Easv7‑series is a good fit for general-purpose OLTP workloads and lighter production deployments where memory efficiency and compute predictability matter more than isolated `tempdb` performance. As workloads evolve - such as increased `tempdb` pressure or higher memory requirements - you can transition to VM families with local temp storage or higher memory density. But Easv7 VMs provide a solid and cost-conscious foundation for SQL Server workloads that don't require specialized I/O characteristics.

### Ebdsv5 series

| Parameter | Value |
|-----------|-------|
| **Processor** | Intel® Xeon® Platinum 8370C (Ice Lake) |
| **Memory-to-vCore ratio** | 8:1 |
| **Max vCores** | 112 |
| **Memory** | Up to 672 GiB |
| **Max IOPS** | 400,000 (NVMe sizes, Ultra Disk/Premium SSD v2) |
| **Max throughput** | 10,000 MBps (NVMe sizes) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Production SQL Server (most workloads), [OLTP](/azure/architecture/data-guide/relational-data/online-transaction-processing), data warehouse |
| **Ephemeral storage for `tempdb`** | Yes (75-3,800 GiB capacity) |

The [Ebdsv5-series](/azure/virtual-machines/ebdsv5-ebsv5-series#ebdsv5-series) is the recommended starting point for SQL Server workloads as it covers scenarios that benefit from local temp storage. This VM series is a balanced, memory‑optimized, and tuned option for SQL Server on Azure virtual machines. With an 8:1 memory-to-vCore ratio, predictable remote storage performance, and support for Premium SSD, Premium SSD v2, and Ultra Disk, this series aligns well with the core requirements of most production SQL Server OLTP workloads. These VMs run on the Intel® Xeon® Platinum 8370C (Ice Lake) processors.

The Ebdsv5 VMs are one of the few VMs that offers both the [SCSI and NVMe interfaces](/azure/virtual-machines/ebdsv5-ebsv5-series?branch=main#ebdsv5-series-nvme). It's recommended to use the NVMe options for maximum performance for SQL Server VMs.
 
Ebdsv5 VMs provide sufficient memory per core, strong and consistent storage throughput, and scalable I/O characteristics without requiring you to overprovision CPU simply to reach acceptable memory or storage levels. This balance makes it well‑suited for transactional workloads, mixed OLTP scenarios, and general‑purpose production databases where stability, efficiency, and cost control matter as much as peak scale. 
 
The Ebdsv5 series offers performance that works for the majority of production deployments, while still allowing you to scale up or move to more specialized VM families as workload characteristics evolve.

> [!NOTE] 
> - The larger [Ebdsv5-series](/azure/virtual-machines/ebdsv5-ebsv5-series#ebdsv5-series) sizes (48 vCores and larger) support NVMe enabled storage access. To take advantage of this high I/O performance, you must deploy your virtual machine [using NVMe](/azure/virtual-machines/enable-nvme-interface).


### ECadsv5 series (Confidential Computing)

| Parameter | Value |
|-----------|-------|
| **Processor** | AMD 3rd Generation EPYC™ 7763v (Milan) |
| **Memory-to-vCore ratio** | 7:1 to 8:1 (varies by size) <br/> Close to the 8:1 recommended for production SQL Server |
| **Max vCores** | 96 |
| **Memory** | Up to 672 GiB |
| **Max IOPS** | 80,000 (Premium SSD) |
| **Temp disk throughput** | Up to 4,000 MBps |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Confidential computing with memory-intensive workloads |
| **Ephemeral storage for `tempdb`** | Yes (75-3,600 GiB capacity) |

The [ECadsv5-series](/azure/virtual-machines/ecasv5-ecadsv5-series) virtual machine sizes are **memory-optimized Azure confidential VMs** with a temporary disk. These VMs use AMD's 3rd Generation EPYC™ 7763v (Milan) processors and offer Secure Encrypted Virtualization-Secure Nested Paging (SEV-SNP) for hardware-based VM memory encryption.

For information about the security benefits of Azure confidential VMs, see [confidential VMs](security-considerations-best-practices.md#confidential-vms). These VMs protect data from other VMs, the hypervisor, and host management code with hardware-isolated VM memory encryption.

As the security features of Azure confidential VMs might introduce performance overheads, test your workload and select a VM size that meets your performance requirements. The ECadsv5-series offers a memory-to-vCore ratio close to the recommended 8:1 for production SQL Server workloads, making it suitable for confidential computing scenarios that require both security and performance.
 
## Compute optimized 

The [compute optimized](/azure/virtual-machines/sizes/overview?tabs=breakdownseries%2Cgeneralsizelist%2Ccomputesizelist%2Cmemorysizelist%2Cstoragesizelist%2Cgpusizelist%2Cfpgasizelist%2Chpcsizelist#compute-optimized) [F-series](/azure/virtual-machines/sizes/compute-optimized/f-family) VMs use AMD EPYC™ 9005 (Turin) processors and prioritize CPU performance for SQL Server workloads with lower memory requirements.

The following list describes the capabilities of compute optimized VMs:

- Support [premium storage](/azure/virtual-machines/premium-storage-performance), [premium storage caching](/azure/virtual-machines/premium-storage-performance#disk-caching), and accelerated networking.
- Provide a 4:1 to 8:1 memory-to-vCore ratio, below the 8:1 recommended for most production SQL Server workloads.
- Are suitable for lighter OLTP workloads, dev/test environments, and CPU-bound scenarios with modest memory needs.

### Fasv7-series

| Parameter | Value |
|-----------|-------|
| **Processor** | AMD EPYC™ 9005 5th Gen (Turin) |
| **Memory-to-vCore ratio** | 4:1 |
| **Max vCores** | 80 |
| **Memory** | Up to 320 GiB |
| **Max IOPS** | 310,000 (Ultra Disk/Premium SSD v2); 212,000 (Premium SSD) |
| **Max throughput** | 10,356 MBps (Ultra Disk/Premium SSD v2); 10,344 MBps (Premium SSD) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Dev/test, lighter OLTP, CPU-bound scenarios |
| **Ephemeral storage for `tempdb`** | No |

The [Fasv7‑series](/azure/virtual-machines/sizes/compute-optimized/fasv7-series) VMs are suited for SQL Server workloads that prioritize compute and storage performance but can operate with lower memory requirements. Built on AMD's 5th Generation EPYC™ 9005 (Turin) processors, this series delivers efficient, modern compute with stable remote storage behavior. It's a practical choice for SQL Server deployments where CPU‑bound operations dominate and memory requirements remain modest.
 
With a 4:1 memory‑to‑vCore ratio, Fasv7 VMs are optimized for scenarios where compute efficiency and storage performance matter more than a larger buffer pool capacity for SQL Server. Support for premium storage ensures compatibility with modern Azure storage options and provides consistent remote I/O performance for user databases. However, the absence of local ephemeral storage means `tempdb` activity relies entirely on remote disks. The lower memory position can put even more pressure on remote storage, making this series better suited for workloads with limited `tempdb` pressure or predictable, well‑understood I/O patterns.
 
This balance makes the Fasv7‑series a good fit for lighter OLTP workloads, departmental databases, development and test environments, and targeted production scenarios where CPU utilization is the primary driver and memory or `tempdb` demands are constrained. The Fasv7 series provides a cost‑conscious and compute‑efficient option where CPU performance, storage performance, and overall costs are the primary requirements.

### Famsv7-series

| Parameter | Value |
|-----------|-------|
| **Processor** | AMD EPYC™ 9005 5th Gen (Turin) |
| **Memory-to-vCore ratio** | 8:1 |
| **Max vCores** | 80 |
| **Memory** | Up to 640 GiB |
| **Max IOPS** | 310,000 (Ultra Disk/Premium SSD v2); 212,000 (Premium SSD) |
| **Max throughput** | 10,356 MBps (Ultra Disk/Premium SSD v2); 10,344 MBps (Premium SSD) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Lighter to mid-range OLTP, dev/test, cost-conscious production |
| **Ephemeral storage for `tempdb`** | No |

The [Famsv7‑series VMs](/azure/virtual-machines/sizes/compute-optimized/famsv7-series) work well for SQL Server workloads that need a balanced memory-to-vCore ratio and efficient compute performance. They're a good choice when an 8:1 memory to core ratio up to 640 GiB is enough and local temp storage isn't a strict requirement. 
 
Built on AMD's 5th Generation EPYC™ 9005 (Turin) processors, this series delivers modern CPU efficiency with predictable remote storage behavior. It's a practical choice for SQL Server deployments that prioritize cost-effective OLTP performance over extreme scale or specialized I/O characteristics.
 
Famsv7 VMs provide a solid memory position for SQL Server buffer pool utilization without you having to overprovision vCPUs simply to meet baseline memory requirements. Support for premium storage ensures compatibility with modern Azure storage options and enables consistent, scalable remote I/O performance for user databases. While the series doesn't include local ephemeral storage, its balanced memory profile and stable remote storage characteristics make it suitable for workloads where `tempdb` activity is moderate and doesn't dominate the performance profile.

This balance makes the Famsv7‑series a good fit for lighter to mid‑range OLTP workloads, development and test environments, and targeted production scenarios where memory efficiency and compute predictability matter more than isolated `tempdb` performance. As workloads evolve, such as increased `tempdb` pressure or higher memory demand, you can transition to more specialized VM families with higher memory or local ephemeral storage, but Famsv7 provides a cost‑conscious and approachable solution for SQL Server deployments that need reliable performance without unnecessary complexity.

## General Purpose

The [General Purpose virtual machine sizes](/azure/virtual-machines/sizes-general) provide balanced memory-to-vCore ratios for smaller entry level workloads such as development and test, web servers, and smaller database servers.

Because of the smaller memory-to-vCore ratios with the General Purpose virtual machines, it's important to carefully monitor memory-based performance counters to ensure SQL Server gets the buffer cache memory it needs. For more information, see [memory performance baseline](performance-guidelines-best-practices-collect-baseline.md#memory). General Purpose VMs may not be suitable for larger production SQL Server workloads as their minimum recommended memory-to-vCore ratio is below the recommended starting point of 8:1 for production SQL Server workloads.

### Ddsv5 series

| Parameter | Value |
|-----------|-------|
| **Processor** | Intel® Xeon® Platinum 8370C (Ice Lake) |
| **Memory-to-vCore ratio** | 4:1 <br/> below the 8:1 recommended for production SQL Server; suitable for small apps and dev/test only |
| **Max vCores** | 96 |
| **Memory** | Up to 384 GiB |
| **Max IOPS** | 80,000 |
| **Max throughput** | 2,600 MBps |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Dev/test, small apps, side-by-side SQL and app deployments |
| **Ephemeral storage for `tempdb`** | Yes (75-3,600 GiB capacity) |

The [Ddsv5-series](/azure/virtual-machines/sizes/general-purpose/ddsv5-series) offers a fair combination of vCores, memory, and temporary disk but with smaller memory-to-vCore support.

The Ddsv5 VMs include lower latency and higher-speed local storage.

These machines are ideal for side-by-side SQL and app deployments that require fast access to temp storage and departmental relational databases. There's a standard memory-to-vCore ratio of 4 across all of the virtual machines in this series.

For this reason, use the Standard_D8ds_v5 as the minimum recommended VM size in this series. The largest VM size is the Standard_D96ds_v5, which has 96 vCores.

## DCadsv6 series (Confidential Computing)

| Parameter | Value |
|-----------|-------|
| **Processor** | AMD EPYC™ 9004 (Genoa) |
| **Memory-to-vCore ratio** | 4:1 <br/> Below the 8:1 commonly recommended for production SQL Server |
| **Max vCores** | 96 |
| **Memory** | Up to 384 GiB |
| **Max remote storage IOPS** | Up to 175,000 |
| **Max remote storage throughput** | Up to 4,320 MBps |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes |
| **Intended workload** | Confidential computing workloads requiring data-in-use protection and disk encryption for sensitive workloads |
| **Ephemeral storage for `tempdb`** | Yes (75-3,600 GiB capacity) |

The [DCadsv6-series](/azure/virtual-machines/sizes/general-purpose/dcadsv6-series) VMs are general purpose Azure confidential VMs with local temporary storage. These VMs use AMD EPYC™ 9004 (Genoa) processors and support Secure Encrypted Virtualization–Secure Nested Paging (SEV‑SNP) to provide hardware-isolated environments that help protect code and data during processing from the hypervisor, host management systems, and administrative users. The platform includes hardware-based VM memory encryption and supports confidential disk encryption, including OS disk encryption at boot using customer-managed keys (CMK) or platform-managed keys (PMK), integrated with Azure Key Vault and Azure Managed HSM.

DCadsv6 configurations scale up to 96 vCores and 384 GiB of memory, and provide high remote storage performance. The series also includes local temporary storage (75–3,600 GiB) that can be used for transient I/O such as tempdb, staging, or other recreatable data. As with all confidential VM offerings, security features can introduce performance overhead. Benchmark your workload and select a VM size that meets your performance requirements. Because DCadsv6 has a 4:1 memory-to-vCore ratio, it may be a better fit for confidential computing scenarios that are not memory bound. For memory-intensive SQL Server production workloads, consider a higher memory-to-vCore ratio VM series where appropriate.

Review [confidential VMs](security-considerations-best-practices.md#confidential-vms) for information about the security benefits of Azure confidential VMs. These VMs protect data from other VMs, the hypervisor, and host management code.


### B-series

| Parameter | Value |
|-----------|-------|
| **Processor** | Intel® Xeon® (varies by size) |
| **Memory-to-vCore ratio** | 0.5:1 to 4:1 (varies by size) <br/> Below the 8:1 recommended for production SQL Server |
| **Max vCores** | 32 (Bsv2-series) |
| **Memory** | Up to 128 GiB |
| **Max IOPS** | Up to 80,000 (remote storage) |
| **Max throughput** | Up to 960 MBps |
| **Premium storage** | Yes |
| **Premium storage caching** | No |
| **Intended workload** | Proof of concept, dev/test, intermittent workloads |
| **Ephemeral storage for `tempdb`** | Yes (4-160 GiB capacity) |

The [burstable B-series](/azure/virtual-machines/sizes/general-purpose/b-family) VM sizes are ideal for workloads that don't need consistent performance, such as proof of concept and very small application and development servers. This series is unique as the apps have the ability to **burst** during business hours with burstable credits varying based on VM size. When credits are exhausted, the VM returns to the baseline VM performance.

Most of the [burstable B-series](/azure/virtual-machines/sizes/general-purpose/b-family) VM sizes have a memory-to-vCore ratio of 4 or less. If you must pick a B-series VM for SQL Server, choose the [Bsv2 series](/azure/virtual-machines/sizes/general-purpose/bsv2-series), targeting machines with higher memory. The Standard_B32s_v2 has 32 vCores with high storage performance.

The benefit of the B-series is the compute savings you achieve compared to the other VM sizes in other series, especially if you need the processing power sparingly throughout the day.

> [!NOTE] 
> The [burstable B-series](/azure/virtual-machines/sizes/general-purpose/b-family) doesn't have the memory-to-vCore ratio of 8:1 that is recommended for SQL Server workloads. As such, consider using these virtual machines for smaller applications, web servers, and development workloads only.

## Storage optimized

The [storage optimized VM sizes](/azure/virtual-machines/sizes-storage) are intended for I/O‑intensive workloads that benefit from high disk throughput and low‑latency I/O, including SQL Server scenarios where fast local storage is the most important. L‑series VMs provide directly mapped local NVMe storage that is ephemeral in nature and are optimized to achieve higher IOPS and throughput than relying primarily on durable data disks.  

These virtual machines are good options for big data, data warehouse, reporting, and ETL workloads. The high throughput and IOPS of the local NVMe storage is a good use case for processing files that you load into your database and other scenarios where the data can be recreated from the source system or other repositories such as Azure Blob storage or Azure Data Lake.


### Lsv4 series
 
| Parameter | Value |
|-----------|-------|
| **Processor** | 5th Generation Intel® Xeon® Platinum 8573C (Emerald Rapids) |
| **Memory-to-vCore ratio** | 8:1 |
| **Max vCores** | 96 |
| **Memory** | Up to 768 GiB |
| **Max IOPS** | Up to 6.6M (local NVMe); up to 153,600 (remote storage) |
| **Max throughput** | Up to 36,000 MBps (local NVMe); up to 5,088 MBps (remote storage) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes (not supported on the largest size) |
| **Intended workload** | Big data, relational/NoSQL databases, data analytics, data warehousing; I/O‑intensive SQL Server scenarios (ETL/staging/tempdb-style workloads) |
| **Ephemeral storage for `tempdb`** | Yes (local NVMe temp disk; 240 GB per vCore capacity, up to ~23 TB on the largest size) |
 
The [Lsv4-series](/azure/virtual-machines/sizes/storage-optimized/lsv4-series) features high throughput, low latency, directly mapped local NVMe storage and is designed for scale-up or scale-out storage workloads that need a balance of SSD capacity, compute, and memory. These virtual machines range from 2 to 96 vCores with a consistent 8:1 memory-to-vCore ratio and include 240 GB of local NVMe temp disk capacity per vCore, scaling up to approximately 23 TB of local temp storage on the largest size.
 
The local NVMe temp disk is intended for high-performance, transient I/O (for example, tempdb, staging, ETL, and other recreatable working sets). Because it is temporary storage, it shouldn't be used for data files that require durability.

> [!WARNING]
> Storing your data files on the ephemeral NVMe storage could result in data loss when the VM is deallocated.
 
## Lasv4 series

| Parameter | Value |
|-----------|-------|
| **Processor** | AMD 4th Generation EPYC™ 9004 (Genoa) |
| **Memory-to-vCore ratio** | 8:1 |
| **Max vCores** | 96 |
| **Memory** | Up to 768 GiB |
| **Max IOPS** | Up to 6.6M (local NVMe); up to 172,800 (remote storage) |
| **Max throughput** | Up to 36,000 MBps (local NVMe); up to 4,320 MBps (remote storage) |
| **Premium storage** | Yes |
| **Premium storage caching** | Yes (except for the largest size) |
| **Intended workload** | Big data, relational/NoSQL databases, data analytics, data warehousing; I/O‑intensive SQL Server scenarios (ETL/staging/tempdb-style workloads) |
| **Ephemeral storage for `tempdb`** | Yes (local NVMe temp disk; ~240 GB per vCore, up to ~23 TB on the largest size) |
 
The [Lasv4-series](/azure/virtual-machines/sizes/storage-optimized/lasv4-series) features high throughput, low latency, directly mapped local NVMe storage and is designed for scale-up or scale-out storage workloads that need a balance of SSD capacity, compute, and memory. These virtual machines range from 2 to 96 vCores with a consistent 8:1 memory-to-vCore ratio and include 240 GB of local NVMe temp disk capacity per vCore, scaling up to approximately 23 TB of local temp storage on the largest size.
 
The local NVMe temp disk is intended for high-performance, transient I/O (for example, tempdb, staging, ETL, and other recreatable working sets). Because it is temporary storage, it shouldn't be used for data files that require durability.

> [!WARNING]
> Storing your data files on the ephemeral NVMe storage could result in data loss when the VM is deallocated.

## Constrained vCPUs

To maintain an acceptable level of performance without incurring higher SQL Server licensing costs, Azure offers VM sizes with reduced vCore counts through a feature called [constrained vCPUs](/azure/virtual-machines/constrained-vcpu).

This feature helps you control licensing costs by reducing the available vCores while maintaining the same memory, storage, and I/O bandwidth of the parent virtual machine.

You can constrain the vCore count to one-half to one-quarter of the original VM size. By reducing the vCore available to the virtual machine, you get higher memory-to-vCore ratios, but the compute and OS cost stays the same.

These new VM sizes have a suffix that specifies the number of active vCores to make them easier to identify.

For example, the [M64-32ms](/azure/virtual-machines/constrained-vcpu) requires licensing only 32 SQL Server vCores with the memory, I/O, and throughput of the [M64ms](/azure/virtual-machines/sizes/memory-optimized/m-family). The [M64-16ms](/azure/virtual-machines/constrained-vcpu) requires licensing only 16 vCores. While the [M64-16ms](/azure/virtual-machines/constrained-vcpu) has a quarter of the SQL Server licensing cost of the M64ms, the compute and OS cost of the virtual machines is the same.

Consider the following points:
- Constrained vCPUs are most beneficial when higher computing resources are unnecessary, especially for larger VM sizes, but the memory and storage are a priority to improve memory-to-vCore ratios.
- Constrained vCPU options are one-fourth to one-half of the parent Azure VM size.
- You pay the SQL Server licensing costs of the resulting constrained vCPUs, which significantly reduces the cost of the deployment. 
- The compute cost, which includes operating system licensing, stays the same as the parent virtual machine.
- Not all VM series support constrained vCPUs. Review the [constrained vCPU documentation](/azure/virtual-machines/constrained-vcpu) for the latest supported VM sizes.


## VM vCore customization

The [VM vCore customization](vm-vcore-customization-for-sql.md) feature of Azure VMs includes Configurable Constrained Cores (CCC) and the ability to disable Simultaneous Multithreading (SMT) / hyperthreading settings. This capability allows you to appropriately size the vCPU count to match your SQL Server licensing needs at a granular level rather than relying on the one-fourth to one-half limitations of the original constrained vCPU model. 
 
Configurable Constrained Cores preserves the parent VM's memory and I/O capabilities, beyond what is currently possible with constrained vCPU VM sizes.

> [!NOTE]
> [VM vCore customization](/azure/virtual-machines/vm-customization) is currently in preview for SQL Server on Azure VMs.

## Related content

To learn more, see the other articles in this best practices series:

- [Quick checklist](performance-guidelines-best-practices-checklist.md)
- [Storage](performance-guidelines-best-practices-storage.md)
- [Security](security-considerations-best-practices.md)
- [HADR settings](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md)

For other SQL Server Virtual Machine articles, see [SQL Server on Azure Virtual Machines Overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).
