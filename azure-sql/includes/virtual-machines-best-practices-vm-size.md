---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/23/2026
ms.service: azure-vm-sql-server
ms.topic: include
ms.custom:
---
- Before choosing a VM size, configure your [storage](../virtual-machines/windows/performance-guidelines-best-practices-storage.md). Collect a [baseline](../virtual-machines/windows/performance-guidelines-best-practices-collect-baseline.md) from your source environment under the highest stress conditions and then configure your storage based on the IOPS and throughput needs of your workload with a 20% buffer for future growth. 
- Identify workload performance characteristics ([OLTP](/azure/architecture/data-guide/relational-data/online-transaction-processing) vs [OLAP](/azure/architecture/data-guide/relational-data/online-analytical-processing), workload size) to determine the appropriate VM size for your business.
- If you're migrating to Azure, [assess migration readiness](/sql/sql-server/azure-arc/migration-assessment) to find the right VM size for your existing SQL Server workload, and then migrate with [Azure Database Migration Service](/azure/dms/dms-overview). 
- Use Azure Marketplace images to deploy your SQL Server VMs as the SQL Server settings and storage options are configured for optimal performance.
- Use VM sizes with 4 or more vCores.
- Use memory optimized virtual machine sizes for the best performance of SQL Server workloads. 
   - The [Mbdsv3-series](../virtual-machines/windows/performance-guidelines-best-practices-vm-size.md#mbdsv3-series) offers the best overall performance for mission critical OLTP and data warehouse workloads.
   - The [Ebdsv5-series](../virtual-machines/windows/performance-guidelines-best-practices-vm-size.md#ebdsv5-series) provides the best price-performance for most production SQL Server workloads.  
   - The [Easv7-series](../virtual-machines/windows/performance-guidelines-best-practices-vm-size.md#easv7-series) and [Msv3/Mdsv3-series](../virtual-machines/windows/performance-guidelines-best-practices-vm-size.md#msv3-and-mdsv3-medium-memory-series) are optimized for memory-intensive workloads.
   - The [M-series family](../virtual-machines/windows/performance-guidelines-best-practices-vm-size.md#memory-optimized-m-series-vms) offers the highest memory configurations in Azure for the largest workloads. 
- Start development environments with the lower-tier D-Series, or B-Series, and grow your environment over time.
- Check [VM supportability](../virtual-machines/windows/performance-guidelines-best-practices-vm-size.md#supportability) to avoid unsupported configurations.
- Use [VM vCore customization](../virtual-machines/windows/performance-guidelines-best-practices-vm-size.md#vm-vcore-customization) to appropriately allocate vCPUs for your workload and VM and reduce SQL Server licensing costs, as well as disable SMT/hyperthreading settings for optimal SQL Server performance.
