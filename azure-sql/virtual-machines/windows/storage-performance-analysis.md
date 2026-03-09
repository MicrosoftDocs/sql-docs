---
title: Analyze I/O Performance
description: "Identify I/O performance issues associated with VM and data disk throttling for your SQL Server on Azure VM workloads."
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma, randolphwest
ms.date: 02/19/2026
ms.service: azure-vm-sql-server
ms.topic: how-to
---
# I/O Performance Analysis -  SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article teaches you to analyze I/O performance for SQL Server on Azure Virtual Machines (VMs) to find issues that result from exceeding virtual machines and data disks limits.  

## Overview  

Although various tools help you troubleshoot SQL Server performance problems, to do so effectively on an Azure VM, it's important to understand what's happening at both the host level and your SQL Server instance where, often, correlating host metrics with SQL Server workloads can be a challenge. SQL Server on Azure VMs make it easy to identify performance issues stemming from IOPS (Input / Output per second) and throughput throttling caused by exceeding virtual machine and data disks limits. 

Performance metrics that demonstrate the issue, and potential steps to address it, are available in the Azure portal, and queryable with the Azure CLI.

The **Storage** pane of your [SQL virtual machines](manage-sql-vm-portal.md) resource in the Azure portal helps you:

- [Manage your storage configuration](storage-configuration.md)
- Identify I/O throttling 
- Evaluate your system for I/O related best practices

## Understanding the metrics  

The I/O Analysis tab relies on Azure Metrics to identify disk latency, and VM or disk I/O throttling. Azure Metrics are sampled every 30 seconds and aggregated to a minute. 

The system monitors for throttling and disk latency. Some throttling is expected, and is ignored unless there's also disk latency. If more than 500 milliseconds of disk latency is observed in a consecutive 5-minute period, then the system: 
- Digs deeper into performance metrics
- Identifies the throttled resource 
- Provides potential root causes and mitigation steps

The following table explains the Azure Metrics used to identify problematic throttling issues: 

| Azure Metric | Metric description | Problematic condition | I/O throttling conclusions |
|--|--| ---| --- | 
| [Disk latency (preview)](/azure/virtual-machines/disks-metrics) | The average time to complete IOs for the data disk during the monitored period. Values are in milliseconds. | > 500 milliseconds in a consecutive 5-minute period | **There is a latency problem** for the system to further investigate potential throttling. |
| [VM Cached IOPS Consumed Percentage](/azure/virtual-machines/disks-metrics) | The percentage calculated by the total IOPS completed over the max cached virtual machine IOPS limit. | >= 95% in a consecutive 5-minute period | **There is VM throttling.** The application running on the SQL virtual machine is fully utilizing the maximum cached IOPS capacity available to the virtual machine -  the storage demands of the application exceed the cached IOPS  provided by the virtual machine's underlying storage configuration. |
| [VM Cached Bandwidth Consumed Percentage](/azure/virtual-machines/disks-metrics) | The percentage calculated by the total disk throughput completed over the max cached virtual machine throughput. | >= 95% in a consecutive 5-minute period | **There is VM throttling.** The application running on the SQL virtual machine is utilizing the maximum available cached disk bandwidth for data transfer - the data transfer demands of the application exceed the cached bandwidth resources provided by the virtual machine's underlying storage configuration.  | 
| [VM Uncached IOPS Consumed Percentage](/azure/virtual-machines/disks-metrics) | The percentage calculated by the total IOPS on a virtual machine completed over the max uncached virtual machine IOPS limit. | >= 95% in a consecutive 5-minute period | **There is VM throttling.** The application running on the SQL virtual Machine is utilizing the maximum allowable uncached IOPS capacity available to the virtual machine- the storage demands of the application exceed the uncached IOPS resources provided by the virtual machine's underlying storage configuration. |
| [VM Uncached Bandwidth Consumed Percentage](/azure/virtual-machines/disks-metrics) | The percentage calculated by the total disk throughput on a virtual machine completed over the max provisioned virtual machine throughput. | >= 95% in a consecutive 5-minute period | **There is VM throttling.** The application running on the  SQL virtual machine is utilizing the maximum allowable uncached disk bandwidth for data transfer – the data transfer demands of the application exceed the uncached bandwidth resources provided by the virtual machine's underlying storage configuration. |
| [Data Disk IOPS Consumed Percentage](/azure/virtual-machines/disks-metrics) | The percentage calculated by the data disk IOPS completed over the provisioned data disk IOPS. | >= 95% in a consecutive 5-minute period | **There is data disk throttling.** The application running on the SQL virtual machine is hitting the IOPS limit for the provisioned data disk - the storage demands of the application exceed the performance capabilities of the chosen disk configuration. |
| [Data Disk Bandwidth Consumed Percentage](/azure/virtual-machines/disks-metrics) | The percentage calculated by the data disk throughput completed over the provisioned data disk throughput. | >= 95% in a consecutive 5-minute period | **There is data disk throttling.** The application running on the SQL virtual machine is hitting the IOPS limit for the provisioned data disk - the storage demands of the application exceed the performance capabilities of the chosen disk configuration. |


## I/O analysis findings  

Based on its analysis of performance metrics in the last 24 hours, I/O analysis determines that there's: 
- No throttling 
- VM level I/O throttling  
- Disk level I/O throttling  

### No I/O throttling issue  

If you're experiencing a performance problem but there's no disk latency, then the performance problem isn't due to an I/O throttling issue. You'll need to investigate other areas. You can use the [Best practices checklist for SQL Server on Azure VMs](performance-guidelines-best-practices-checklist.md) to make sure your system is configured efficiently, or find useful links for [troubleshooting SQL Server performance](performance-guidelines-best-practices-checklist.md#performance-troubleshooting). If you enable the [SQL best practices assessment](sql-assessment-for-sql-vm.md) feature, you can see the full list of recommendations for your SQL Server VM.  

### VM level I/O throttling issue  

Azure Virtual Machines are cloud-based computing resources that come in different series and sizes for various workloads, each with different capabilities and performance characteristics. For SQL Server workloads, generally, the recommended series for SQL Server workloads are the memory-optimized ones, such as the Ebdsv5, M, and Mv2 series.

The size of the VM determines the number of vCPUs, memory, and storage available for the SQL Server instance. Compared to storage, it's relatively easy for customers to resize their virtual machines and scale their VM up and down based on application resource needs. Since it's possible for IOPS and throughput to be throttled at the VM level, choose an appropriate VM size based on performance needs and cost of the workload. 

If you're migrating to Azure, you can use the [migration readiness assessment](/sql/sql-server/azure-arc/migration-assessment), to analyze your current SQL Server configuration and usage and suggest the best VM size for your workload in Azure.

The following Azure metrics are used to determine the workload is throttled from exceeding limits imposed by the VM:   
- VM Cached IOPS Consumed Percentage 
- VM Cached Bandwidth Consumed Percentage 
- VM Uncached IOPS Consumed Percentage 
- VM Uncached Bandwidth Consumed Percentage 

Consider the following the key points about VM throttling:  
- You can increase memory, vCores, throughput, and IOPS by resizing your virtual machine within a VM series.  
- You can't reduce the VM size to a level where the number of data disks exceeds the max data disks limit for the target VM size.  
- It's important to determine the throttling pattern. For example, infrequent spikes of throttling can likely be addressed by tuning the workload, whereas sustained spikes could indicate the underlying storage is incapable of handling the workload. 

### Disk level I/O throttling issue  

For SQL virtual machine customers, storage is the most critical aspect to configure appropriately for optimized performance since modifying storage is more challenging than resizing a virtual machine. For example, making any changes to increase IOPS or throughput for Premium SSDs requires creating a new storage pool.  As such, it's crucial to optimize storage configuration for both price and performance during the planning phase to avoid performance issues after deployment.  

The following Azure metrics are used to determine the workload is throttled from exceeding limits imposed by the disk:

- Data Disk IOPS Consumed Percentage 
- Data Disk Bandwidth Consumed Percentage 
  
Consider the following key points about disk level I/O throttling:  

- The data disk is critical for SQL Server performance. Placing SQL Server data (.mdf) and log (.df) files on the data disk is recommended.  
- For throttling at the data disk level, enable read-caching, if it's available.  


#### Data Disk IOPS Consumed Percentage 

The **Data Disk IOPS Consumed Percentage** metric measures IOPS consumption at the disk level. Generally, high IOPS needs are associated with highly transactional, OLTP based applications and workloads.
  
The following scenarios or conditions can potentially exceed data disk IOPS limits:  

- **High Transaction Workload (IOPS)**: If the application is processing a high volume of database transactions that involve frequent read and write operations, it can quickly consume the allocated IOPS.  
- **Inefficient Queries**: Poorly optimized SQL queries or data retrieval operations can lead to excessive I/O activity, consuming more IOPS than anticipated.  
- **Concurrent Users**: If multiple users or sessions are concurrently accessing the database and generating I/O requests, the cumulative effect can result in reaching the IOPS limit.  
- **Contending Resources**: If the underlying physical infrastructure is heavily shared with other tenants or workloads, it can impact the available IOPS for your virtual machine.  
- **Temporary Spikes**: Temporary spikes in workload, such as batch processing or data migrations, can lead to sudden increases in I/O demand that surpass the allocated IOPS.  
- **Small disk size**: If the provisioned data disk size is relatively small, the IOPS capacity might be limited. Individual smaller disks have lower IOPS limits, and if the application's demands exceed this limit, the "Data Disk IOPS Consumed Percentage" reaches 100%.  
- **Insufficient disk type**: Choosing a lower-performance disk type (such as a Standard HDD) for an I/O-intensive application can lead to IOPS limitations.  
- **Unoptimized disk stripe size**: If the storage configuration isn't optimized for the workload, it might lead to suboptimal IOPS performance.  
 
Consider the following steps to avoid exceeding the data disk IOPS limit: 

- Optimize SQL queries and database design to minimize unnecessary I/O operations.  
- Choose an appropriate disk type (Standard SSD or Premium SSD) that matches your application's IOPS requirements.  
- Use larger disk sizes to increase the available IOPS capacity.  
- Distribute I/O across multiple data disks using RAID configurations.  


#### Data Disk Bandwidth Consumed Percentage  

The **Data Disk Bandwidth Consumed Percentage** Azure metric measures bandwidth utilization at the disk level. Generally, high throughput needs are associated with data warehousing, data mart, reporting, ETL, and other data analytic workloads.

The following scenarios or conditions can potentially exceed data disk bandwidth limits: 

- **Large data transfers**: Frequent, large-scale application data transfers between the disk and the SQL database, can quickly consume the available data disk bandwidth.  
- **Bulk data loading**: Disk transfer activities associated with  bulk data inserts, updates, or imports,  can lead to high bandwidth consumption.  
- **Data warehousing or analytics**: Applications that involve heavy data warehousing, analytics processing, or reporting can generate substantial data movement, potentially causing exceeding  bandwidth limits.
- **High data redundancy technology / replication**: Data copying associated with uses disk-based replication, data mirroring, or other redundancy mechanisms can contribute to bandwidth saturation.  
- **Data backup and restore**: Frequent data backups, snapshots, or restoration processes can consume significant data disk bandwidth.  
- **Parallel query execution**: Parallel queries that involve large data scans or joins can lead to substantial data movement that results in bandwidth utilization.  
- **Elevated network traffic**: High network activity, such as data transfers between the virtual machine and other resources, can indirectly impact data disk bandwidth availability.  
- **Insufficient disk type**: Choosing a lower-performance disk type for an application with high data transfer requirements can lead to exceed the bandwidth limit.  
- **Concurrent data-intensive operations**: The combined effect of multiple concurrent processes or sessions accessing and transferring data on the same disk can result in reaching the bandwidth limit.  
- **Unoptimized queries or ETL processes**: Poorly optimized SQL queries or Extract, Transform, Load (ETL) processes can lead to excessive data movement that results in excessive bandwidth consumption.  


Consider the following steps to avoid exceeding the data disk bandwidth limit:

- Optimize data transfer operations to minimize unnecessary data movement.  
- Consider using higher-performance disk types that offer greater bandwidth capacity, such as Premium SSD or Premium SSD v2.
- Distribute data across multiple disks using techniques such as partitioning or sharding.
- Optimize and parallelize queries and data processing to reduce data movement.
- Use compression and efficient data storage mechanisms to reduce the volume of data being transferred.
- Monitor performance metrics and scale up your storage configurations as needed. Premium SSD v2 allows customers to scale their IOPS and throughput as needed on demand. 
- It's important to monitor and analyze performance metrics regularly to identify the cause of IOPS limitations and take appropriate actions to optimize the storage performance for your SQL virtual machine.  

> [!TIP]
> Regularly monitoring performance metrics, tuning data transfer operations, and optimizing disk configurations ensures the performance of your data disk for your SQL virtual machine remains optimal without exceeding limits. 

## Latency without throttling

Latency without throttling refers to delays in data access or processing that occur even when the storage system is not hitting its maximum IOPS or throughput limits. Latency in Azure VMs can arise from various sources, including the operating system's I/O stack, SQL Server processing, network overhead, or hypervisor scheduling. Identifying the cause of the latency is important to optimizing SQL Server performance on Azure VMs.

You will see the following warning on the **I/O Analysis** tab of the **Storage** pane if latency is detected without throttling:
`Warning: High disk latency detected without throttling`. 

The following lists potential causes for latency without throttling: 

- **High CPU utilization**: Heavy CPU load can slow down I/O operations because the CPU is busy with tasks like encryption, compression, or query execution. This is especially prevalent in VMs with fewer cores. When CPU cycles aren't available, I/O requests can wait longer to be processed, increasing latency even if storage limits are not reached. For example, a VM running a CPU-intensive process, such as data encryption, might delay SQL Server I/O operations, leading to slower query response times.
- **Insufficient memory on the VM**: In SQL Server, memory is critical for caching data which reduces the need for disk I/O. If memory is constrained, SQL Server might need to read from disk more often, which increases latency. This is particularly relevant in VMs with less memory or when memory-intensive background processes compete for resources. This can indirectly increase storage latency, as more frequent disk operations are needed to handle the workload, even if IOPS limits aren't reached.
- **Background processes**: Other processes on the VM - such as antivirus software, backups, or maintenance tasks (like Windows Update) - can consume CPU, memory, or disk I/O resources, which delay SQL Server operations. Inefficient filter drivers can worsen this effect. These processes compete with SQL Server for system resources, causing I/O delays that appear as storage latency. For instance, an antivirus scan that reads numerous files simultaneously can reduce disk bandwidth available to SQL Server, which increase latency for database transactions. Additionally, not having the right antivirus exclusions can introduce latency issues without throttling in SQL Server on Azure VMs, primarily through increased disk I/O, filter driver interference, and resource competition.
- **Lower tiered storage usage**: Opting for lower-tier storage options, such as Standard HDDs, instead of Premium SSDs or Ultra Disks, introduces higher baseline latency due to the inherent design of these disks, even without hitting IOPS limits. While cost-effective, lower-tier storage is not optimized for performance-sensitive SQL Server workloads, which leads to slower data access. For example, a customer using Standard HDDs to save costs might experience slower query performance due to the disks' naturally higher latency.
- **Inadequate storage configuration**: Failing to configure your storage to optimize for SQL Server workloads can lead to latency without throttling. For example, incorrect disk caching settings can degrade performance. Microsoft recommends enabling read-only caching for data disks and disabling caching for log disks when using Premium SSD v1 with SQL Server on Azure VMs. Misconfigured caching can slow down read or write operations. For example, disabling read caching on a data disk that hosts SQL Server data files reduces the efficiency of read-heavy workloads, increasing latency.
- **SQL Server database contention**: Inefficient queries (for example, a full table scan instead of an indexed lookup) or lock contention within SQL Server can increase I/O demand or delay data access, which manifests as storage latency. Application-level issues can strain the storage subsystem without exceeding its limits, particularly with small, random I/O patterns common in transactional workloads. For example, a poorly optimized query that performs a full table scan on a large dataset will read excessive data from disk, boosting I/O load and latency compared to an indexed query.

If you're experiencing latency without throttling, consider the following steps to address the latency: 

- **Monitor and manage CPU usage**: Track CPU utilization with tools like Azure Monitor or Resource Monitor. If CPU load is high, optimize SQL queries or upgrade to a VM with more vCores.
- **Monitor memory usage**: Ensure the VM size has adequate memory for the SQL Server workload, using [VM sizes](/azure/virtual-machines/sizes/overview) like E-series or M-series with higher memory-to-vCore ratios. Monitor memory usage with Performance Monitor or Azure Monitor to identify pressure points. Consider scaling memory up if necessary.
- **Schedule background tasks carefully**: Run resource-heavy tasks (such as like backups or antivirus scans) during off-peak hours to avoid competing with SQL Server for resources.
- **Select the right storage tier**: Assess whether lower-tier storage (e.g., Standard HDDs) meets your performance needs. For critical SQL Server workloads, opt for Premium SSDs or Ultra Disks to minimize latency.
- **Configure caching correctly**: For Premium SSD (v1), set read-only caching for data disks and no caching for log disks. Verify settings after VM or storage changes, as Premium SSD v2 and Ultra Disks do not support caching.
- **Optimize SQL Server performance**: Review and tune queries to reduce I/O demand. Implement indexes, avoid full table scans, and resolve lock contention to improve efficiency. Use the [Best Practices analysis](sql-assessment-for-sql-vm.md) feature to identify configuration options that can improve performance.
- **Ensure antivirus exclusions are correct**: Implementing [proper exclusions](/troubleshoot/sql/database-engine/security/antivirus-and-sql-server), testing under load, and scheduling scans appropriately can mitigate latency issues, ensuring optimal performance with security balance.


## I/O related best practices  

Since poorly configured storage systems can lead to performance issues, you can use the **Storage** pane in the Azure portal to run a disk-specific subset of [SQL best practices assessment](sql-assessment-for-sql-vm.md) rules to identify storage configuration issues with your SQL Server on Azure VMs. The SQL best practices feature is based on the [SQL Assessment API](/sql/tools/sql-assessment-api/sql-assessment-api-overview). 

You can view a [full list of recommendations on GitHub](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-assessment-api/DefaultRuleset.csv).  By filtering on the **id** column on the rules on GitHub, you can see the SQL VM disk configuration rules that are validated on the **I/O configuration best practices** tab of the **Storage** pane for your [SQL virtual machines](manage-sql-vm-portal.md) resource in the Azure portal. 

:::row:::
    :::column:::
    - AzSqlVmSize
    - AzDataDiskCache
    - AzDataDiskStriping
    - AzDataOnDataDisks
    - AzDbDefaultLocation
    - AzDiskColumnCount
    :::column-end:::
    :::column:::
    - AzErrorLogLocation
    - AzPremSsdDataFiles
    - AzTempDbFileLocation
    - AzTranLogDiskCache
    - NtfsBlockSizeNotFormatted
    - LockedPagesInMemory
    :::column-end:::
:::row-end:::

On the **I/O related best practices** tab, use **Run assessment** to start an assessment of your configuration, which should take a few minutes to complete (unless there's a large number of databases and objects). Alternatively, if you see a timestamp for the latest available results, you can use **Fetch latest results** to review findings from previous assessments.  

## Analyze I/O with PowerShell

You can also use the [I/O Analysis](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-vm-io-analysis.ps1) PowerShell script to analyze the I/O performance of your SQL Server VM: 

:::code language="powershell" source="~/../sql-server-samples/samples/manage/sql-vm-io-analysis.ps1":::

## Next steps

- [Run a SQL best practices assessment](sql-assessment-for-sql-vm.md) to identify potential misconfigurations that could lead to performance issues.
