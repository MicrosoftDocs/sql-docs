---
title: VM vCore Customization
description: Learn how to configure VM vCore customization for SQL Server on Azure Virtual Machines to optimize performance and reduce licensing costs.
author: dplessMSFT
ms.author: dpless
ms.reviewer: vanto
ms.date: 05/27/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
ms.custom: references_regions
tags: azure-resource-manager
---
# VM vCore customization for SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article shows you how to optimize performance and reduce licensing costs for your SQL Server on Azure Virtual Machines (VMs) by using the [VM vCore customization](/azure/virtual-machines/vm-customization) of Azure VMs, which includes Configurable Constrained Cores (CCC) and the ability to disable Simultaneous Multithreading (SMT) settings.

## Overview

Configurable Constrained Cores (CCC) improves upon the original Constrained vCPU model of Azure VMs. It gives you finer control over the number of active vCores (also known as vCPUs) than the parent virtual machine. CCC gives SQL Server customers the ability to allocate vCores **independent of VM memory and I/O**. This configuration provides meaningful savings for per core licenses, while still preserving necessary memory and IO headroom for the buffer pool, columnstore, `tempdb`, backup/restore throughput, and storage bandwidth.

You can optionally combine CCC with *disabling* [**Simultaneous Multithreading (SMT) / hyperthreading**](/sql/sql-server/compute-capacity-limits-by-edition-of-sql-server) (set *Threads per core = 1*) for latency sensitive workloads. 

Together, these VM customization options give you granular control over CPU presentation to the guest OS without sacrificing other VM size capabilities.

## Original constrained vCPU model

The original [constrained vCPU](/azure/virtual-machines/constrained-vcpu) model is a feature of Azure VMs designed to optimize cost-efficiency for workloads that don't need to utilize the full compute capacity of a VM. Constrained vCPUs allow you to select VM sizes with a reduced number of vCPUs while still maintaining the same memory, storage, and network resources as the original VM size. This feature is especially useful for applications that are more memory- or IO-intensive rather than compute bound.

For example, imagine you deployed an M-series family VM with a high memory configuration for an application that uses a fraction of the available CPU resources. By using the constrained vCPU feature, you can deploy the same M-series VM, but with fewer active vCPUs. This reduction in vCPUs can lead to significant cost savings, particularly for software licensing that is based on the number of vCores, such as SQL Server.

Constrained vCPUs are built into the VM size selection where the name of the VM indicates the reduced core count. For instance, the VM size `E96-24ads_v5` has 24 active vCPUs instead of the full 96 vCPUs available in the parent `E96ads_v5` VM size. It still retains 672 GB RAM and the storage and network performance of the parent virtual machine. For more information, see [Full list of available sizes with constrained vCPUS](/azure/virtual-machines/constrained-vcpu#list-of-available-sizes-with-constrained-vcpus).

Constrained vCPUs are ideal for: 
- Database servers where memory and storage performance is critical, but CPU usage is lower.
- Applications with higher per-core licensing costs.

With constrained vCPUs, you pay for fewer vCores, but you still pay for the compute costs and Windows Server licensing for the parent or host virtual machine.

### Key benefits and features of the original constrained vCPU model

The following list describes some of the key benefits and features of using constrained vCPUs for SQL Server on Azure VMs:

- **Price-performance advantages**: You can choose VM sizes with 50% or 75% of the original vCPUs, which significantly reduces the SQL Server licensing costs.
- **Memory**: The memory allocation is the same as the parent VM configuration, so memory-heavy workloads aren't compromised.
- **Storage**: The storage performance is the same as the parent VM configuration, so storage intensive workloads aren't affected.
- **Performance**: Reducing vCPUs can improve performance by lowering thread contention and aligning CPU usage with your application's needs.

> [!NOTE]
> SQL Server doesn't support [configurations with more than 64 logical processors per NUMA node](/sql/sql-server/compute-capacity-limits-by-edition-of-sql-server). If you attempt to install SQL Server on a server that exceeds this limit, starting with SQL Server 2022 CU15, the installation fails, and the error log indicates an unsupported NUMA configuration. 

### Drawbacks of the original constrained vCPU model

While the original constrained core model provides significant cost savings and flexibility, it has some drawbacks:
- **Inconsistent support across VM families**: Not all Azure VM families support constrained vCPUs.
- **Limited flexibility**: The original constrained vCPU model restricts you to only 25% or 50% of the parent VM's vCores, with no finer granularity. This limitation is problematic for workloads that require the full resources of a parent VM, but only a specific, smaller number of vCores.
- **Scaling challenges**: As VM sizes increase, the lack of granularity becomes more pronounced. For example, the `Standard_M416ms_v2` VM offers 416 vCores, and even the constrained option (208 vCores) might far exceed the needs of many SQL Server deployments.

## Improvements introduced with Configurable Constrained Cores (CCC)

The Azure VM **Configurable Constrained Cores (CCC)** model resolves the drawbacks of the original constrained vCPU model by introducing true flexibility and consistency for SQL Server VM deployments, such as the following features:
- **Granular core selection**: CCC allows you to specify the number of active vCPUs for your VM, rather than being limited to preset fractions. This means you can select any supported vCPU count, such as 16, 32, or 48, while still retaining the full memory, storage, and I/O bandwidth of the parent VM size.
- **Consistent availability**: CCC is available across a broad set of VM families, reducing the risk of inconsistent support. You can confidently select the best VM for your workload and licensing needs.
- **Optimized licensing and resource utilization**: By aligning the number of active vCPUs with your SQL Server license entitlements and workload requirements, CCC helps you avoid paying for unused cores, which ensures you can fully use the performance characteristics of larger VM sizes.
- **Improved customer satisfaction**: By using CCC, you gain the flexibility to optimize both cost and performance, reducing the likelihood of unexpected expenses or suboptimal configurations while improving your overall satisfaction with SQL Server VM deployments.

CCC empowers SQL Server customers to tailor their Azure VM deployments, maximizing licensing efficiency and resource utilization while minimizing cost and complexity.

## CCC best practices for SQL Server on Azure VMs

Configurable Constrained Cores (CCC) is a particularly useful feature for SQL Server on Azure VMs. You can optimize your deployment for both performance and cost.

- **Align SQL Server per core licensing with actual CPU need**: If your SQL Server workload is memory or I/O bound rather than CPU bound, select a larger VM size for memory and disk throughput but constrain active vCPUs to match your licensed cores. This choice can reduce SQL Server licensing costs while maintaining the same VM memory and I/O characteristics.
- **Improve latency consistency**: For some OLTP and latency sensitive database patterns, **disabling SMT** (Threads per core = 1) can reduce logical core contention and tail latency for critical queries or synchronization workloads. You can combine SMT Off with CCC in a single deployment.
- **Plan the deployment**: Before deploying, analyze your SQL Server workload to determine the optimal number of vCPUs needed. You can change CCC and SMT settings only during initial VM creation or during a resize operation. You can't change these options on a running VM, and resizing requires a reboot.
- **Appropriately allocate vCPUs**: When specifying values, the number of active vCPUs (`vCPUsAvailable`) must not exceed the default vCPU count for the selected VM size. For VM sizes that use hyperthreading (two threads per core), valid values for `vCPUsAvailable` are even numbers (such as 2, 4, 6, and so on), while non-hyperthreaded sizes allow increments of one. CCC and SMT settings persist if you resize the VM to another compatible size. If the target VM size doesn't support these features, the resize operation is blocked. If you activate the Azure Hybrid Benefit or use per core licensing, set `vCPUsAvailable` to the **licensed core count** you intend to run. SQL Server only sees that number of logical processors. This choice enables selecting a larger memory or I/O VM (for buffer pool or throughput) without paying for unneeded SQL cores. Validate the core count inside Windows with **Task Manager** and [sys.dm_os_sys_info](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql) on SQL Server.
- **Disable SMT**: Some OLTP workloads benefit from disabling SMT due to reduced contention on shared core resources. Other workloads, such as analytics or highly parallel workloads, can benefit from leaving SMT enabled. Test both for your workload by using representative load tests before finalizing.
- **NUMA and MAXDOP**: CCC only changes the logical CPU count, not the underlying VM memory or I/O throughput. Review your [MAXDOP](/sql/database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option) and [cost threshold for parallelism](/sql/database-engine/configure-windows/configure-the-cost-threshold-for-parallelism-server-configuration-option) after constraining cores to ensure optimal parallelism on the reduced CPUs. Apply general performance best practices by tuning per SQL Server workload.
- **Availability groups and backups**: Constraining cores doesn't change disk throughput entitlements of the VM size. You can continue to use the larger VM's storage bandwidth for backup and restore and availability group synchronization. This throughput is a common driver for choosing larger memory or I/O sizes with fewer active cores.
- **No extra charge**: There's no *additional* Azure VM cost for using CCC or the SMT / HT controls as VM compute pricing remains the same as the selected size, which includes compute, memory, and storage costs. The potential savings come from **per core licensed software (such as SQL Server) recognizing the reduced vCPU count**. Review your licensing terms to estimate savings and overall costs.

### Sample SQL Server deployment patterns

Consider the following sample SQL Server workload deployment patterns:
- **Memory-intensive OLTP**: Select an E-series or Ebdsv5 VM for high memory. Set `vCPUsAvailable` to match your SQL Server license (such as eight cores), and optionally set `vCPUsPerCore = 1` to disable SMT for latency-sensitive workloads. This configuration preserves the VM's memory and I/O while reducing licensing costs.
- **Memory and storage intensive OLAP/DW**: Select an M-Series VM for high memory and storage. Set `vCPUsAvailable` to match your SQL Server license (such as 32 cores), and optionally set `vCPUsPerCore = 1` to disable SMT for latency-sensitive workloads.
- **Backup and restore throughput**: Choose a storage-optimized VM for maximum bandwidth to blob or managed disks. Set `vCPUsAvailable` to the number of SQL cores required, maintaining high disk throughput while licensing fewer cores.

## Get started with CCC

You can set the CCC and SMT settings for your Azure VM by using the Azure portal, Azure CLI, PowerShell, and ARM templates. To learn more, review [VM vCore customization](/azure/virtual-machines/vm-customization).

After deploying or resizing your Azure VM, validate the CPU configuration inside the guest operating system and within SQL Server to ensure your settings are applied correctly.

### Validate CPU configuration in Windows

Use Task Manager (Performance → CPU) to check logical processor count.

Run `wmic cpu get NumberOfCores,NumberOfLogicalProcessors` within an administrative command prompt to confirm SMT status.

### Validate CPU configuration in SQL Server

Execute `SELECT cpu_count, hyperthread_ratio FROM sys.dm_os_sys_info;` to verify logical CPU count and SMT.

Review and adjust [max degree of parallelism (`MAXDOP`)](/sql/database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option) and [cost threshold for parallelism](/sql/database-engine/configure-windows/configure-the-cost-threshold-for-parallelism-server-configuration-option)   for the new CPU count.

## Limitations

Consider the following limitations of using VM vCore customization for SQL Server on Azure VMs:
- Supported images include first-party images such as Windows Server, Ubuntu, RHEL, SUSE, and custom images. Third-party Marketplace images aren't supported. Deprecated Marketplace **SQL VM Linux** images aren't supported.
- CCC is currently available in selected regions such as West Central US, North Europe, East Asia, and UK South.
- You can set CCC/SMT only during **create** or **resize** operations. Dynamic runtime changes aren't supported. Resizing triggers a **reboot**.

## Frequently asked questions (FAQ)

Consider the following frequently asked questions:

### Does CCC change my VM's memory or storage throughput?
No. CCC only changes how many vCPUs are presented to the guest OS. You keep the memory and I/O of the selected VM size.

### Can I turn CCC on or off without downtime?

No. You must enable CCC at VM creation time or during a resize. A reboot occurs.

### Will SQL Server recognize the reduced core count for licensing?

Yes. SQL Server and other per core licensed software see only the `vCPUsAvailable` value that the OS receives. Check your licensing terms to confirm.

## Related content

- [VM size best practices](performance-guidelines-best-practices-vm-size.md)
- [VM vCore customization](/azure/virtual-machines/vm-customization)