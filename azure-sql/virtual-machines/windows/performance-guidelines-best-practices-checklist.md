---
title: "Checklist: Best Practices and Guidelines"
description: Provides a quick checklist to review your best practices and guidelines to optimize the performance of your SQL Server on Azure Virtual Machines (VM).
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma, randolphwest
ms.date: 03/31/2026
ms.service: azure-vm-sql-server
ms.subservice: performance
ms.topic: best-practice
tags: azure-service-management
---
# Checklist: Best practices for SQL Server on Azure VMs

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides checklists as part of a series of best practices and guidelines to optimize the performance of your SQL Server on Azure Virtual Machines (VMs). Use this guide to improve your VM configuration, storage setup, security posture, and troubleshoot common performance problems.

The checklists in this article provide a brief overview of the more comprehensive details found in the following articles of this series:

- [VM size](performance-guidelines-best-practices-vm-size.md)
- [Storage](performance-guidelines-best-practices-storage.md)
- [Security](security-considerations-best-practices.md)
- [HADR configuration](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md).

[!INCLUDE [unified-inventory](../../includes/sql-virtual-machines/unified-inventory.md)]

If you enable [SQL Assessment for SQL Server on Azure VMs](sql-assessment-for-sql-vm.md), the service evaluates your SQL Server against known best practices and displays the results on the [SQL VM management page](manage-sql-vm-portal.md) of the Azure portal.

For videos about the latest features to optimize SQL Server VM performance and automate management, see the following Data Exposed videos:

- [Caching and Storage Capping](/shows/data-exposed/azure-sql-vm-caching-and-storage-capping-ep-1-data-exposed)
- [Automate Management with the SQL Server IaaS Agent extension](/shows/data-exposed/azure-sql-vm-automate-management-with-the-sql-server-iaas-agent-extension-ep-2)
- [Use Azure Monitor Metrics to Track VM Cache Health](/shows/data-exposed/azure-sql-vm-use-azure-monitor-metrics-to-track-vm-cache-health-ep-3)
- [Get the best price-performance for your SQL Server workloads on Azure VM](/shows/data-exposed/azure-sql-vm-get-the-best-price-performance-for-your-sql-server-workloads-on-azure-vm)
- [Using PerfInsights to Evaluate Resource Health and Troubleshoot](/shows/data-exposed/azure-sql-vm-using-perfinsights-to-evaluate-resource-health-and-troubleshoot-ep-5)
- [Best Price-Performance with Ebdsv5 Series](/shows/data-exposed/azure-sql-vm-best-price-performance-with-ebdsv5-series)
- [Optimally Configure SQL Server on Azure Virtual Machines with SQL Assessment](/shows/data-exposed/optimally-configure-sql-server-on-azure-virtual-machines-with-sql-assessment)
- [New and Improved SQL Server on Azure VM deployment and management experience](/shows/data-exposed/new-and-improved-sql-on-azure-vm-deployment-and-management-experience)

## Overview

When you run SQL Server on Azure Virtual Machines, use the same database performance tuning options that you use for SQL Server in on-premises server environments. However, the performance of a relational database in a public cloud depends on many factors, such as the size of a virtual machine and the configuration of the data disks.

There's typically a trade-off between optimizing for costs and optimizing for performance. This performance best practices series focuses on getting the *best* performance for SQL Server on Azure Virtual Machines.

**Next steps:** Start with the first [VM size recommendations](#vm-size) section, then proceed through [Storage](#storage), [Security](#security), and [SQL Server features](#sql-server-features) sections for a complete optimization approach.

If your workload is less demanding, you might not need every recommended optimization. Consider your performance needs, costs, and workload patterns as you evaluate these recommendations.

## Manually install SQL Server to an Azure VM

If you plan to manually install SQL Server on an Azure VM, follow these essential steps to avoid common configuration issues:

- Make sure you have a product key ready for your installation.
- Avoid [unsupported](performance-guidelines-best-practices-vm-size.md#supportability) configurations such as:
  - More than 64 vCores per NUMA node.
  - Storage with 8-KB sector size.
  - Azure Virtual Machine Scale Sets.
- If they don't already exist, create the folders for your SQL Server installation and data files before launching the installation media.
- Copy the installation media to a local drive instead of installing directly from the mounted ISO.
- After installation, register your SQL Server VM with the [SQL Server IaaS Agent Extension](sql-server-iaas-agent-extension-automate-management.md) to automate management tasks.
- Place the `tempdb` database on the [local SSD ephemeral storage](tempdb-ephemeral-storage.md) when possible.

## VM size

[!INCLUDE [sql-vm-deployment-failure](../../includes/sql-vm-deployment-failure.md)]

The checklist in this section covers the [VM size best practices](performance-guidelines-best-practices-vm-size.md) for SQL Server on Azure VMs.

[!INCLUDE [vm size best practices](../../includes/virtual-machines-best-practices-vm-size.md)]

## Storage

The checklist in this section covers the [storage best practices](performance-guidelines-best-practices-storage.md) for SQL Server on Azure VMs.

[!INCLUDE [storage best practices](../../includes/virtual-machines-best-practices-storage.md)]

## Security

The checklist in this section covers the [security best practices](security-considerations-best-practices.md) for SQL Server on Azure VMs.

[!INCLUDE [security best practices](../../includes/virtual-machines-best-practices-security.md)]

## SQL Server features

The following checklist summarizes best practices for SQL Server configuration settings when running your SQL Server instances in an Azure virtual machine in production:

- Enable [database page compression](/sql/relational-databases/data-compression/data-compression) where appropriate.
- Enable [backup compression](/sql/relational-databases/backup-restore/backup-compression-sql-server).
- Enable [instant file initialization](/sql/relational-databases/databases/database-instant-file-initialization) for data files.
- Limit [autogrowth](/troubleshoot/sql/admin/considerations-autogrow-autoshrink#considerations-for-autogrow) of the database.
- Disable [autoshrink](/troubleshoot/sql/admin/considerations-autogrow-autoshrink#considerations-for-auto_shrink) of the database.
- Disable autoclose of the database.
- Move all databases to data disks, including [system databases](/sql/relational-databases/databases/move-system-databases).
- Move SQL Server error log and trace file directories to data disks.
- Configure default backup and database file locations.
- Set max [SQL Server memory limit](/sql/database-engine/configure-windows/server-memory-server-configuration-options#use-) to leave enough memory for the operating system. ([Use Memory\Available Bytes](/sql/relational-databases/performance-monitor/monitor-memory-usage) to monitor the operating system memory health).
- Enable [lock pages in memory](/sql/database-engine/configure-windows/enable-the-lock-pages-in-memory-option-windows).
- Enable [optimize for adhoc workloads](/sql/database-engine/configure-windows/optimize-for-ad-hoc-workloads-server-configuration-option) for OLTP heavy environments.
- Evaluate and apply the [latest cumulative updates](/sql/database-engine/install-windows/latest-updates-for-microsoft-sql-server) for the installed versions of SQL Server. For more information, see [Updating SQL Server on Azure VMs](servicing-updates-guidelines.md).
- Enable [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) on all production SQL Server databases [following best practices](/sql/relational-databases/performance/best-practice-with-the-query-store).
- Enable [automatic tuning](/sql/relational-databases/automatic-tuning/automatic-tuning) on mission critical application databases.
- Ensure that all [tempdb best practices](/sql/relational-databases/databases/tempdb-database#optimizing-tempdb-performance-in-sql-server) are followed.
- [Use the recommended number of files](/troubleshoot/sql/performance/recommendations-reduce-allocation-contention#resolution), using multiple `tempdb` data files starting with one file per core, up to eight files.
- If available, configure the `tempdb` [data and log files on the D: local SSD volume](manage-sql-vm-portal.md#storage). The SQL IaaS Agent extension handles the folder and permissions needed upon reprovisioning.
- Schedule SQL Server Agent jobs to run [DBCC CHECKDB](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql#a-checking-both-the-current-and-another-database), [index reorganize](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes#reorganize-an-index), [index rebuild](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes#rebuild-an-index), and [update statistics](/sql/t-sql/statements/update-statistics-transact-sql#examples) jobs.
- Monitor and manage the health and size of the SQL Server [transaction log file](/sql/relational-databases/logs/manage-the-size-of-the-transaction-log-file#Recommendations).
- Take advantage of any new [SQL Server features](/sql/sql-server/what-s-new-in-sql-server-2025) available for the version you're using.
- Be aware of the differences in [supported features](/sql/sql-server/editions-and-components-of-sql-server-latest) between the editions you're considering deploying.
- [Exclude SQL Server files](/troubleshoot/sql/database-engine/security/antivirus-and-sql-server) from antivirus software scanning. This exclusion includes data files, log files, and backup files.

## Azure features

The following checklist covers best practices for Azure-specific guidance when running your SQL Server on Azure VM:

- Register with [the SQL IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md) to unlock a number of [feature benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits).
- Use the best [backup and restore strategy](backup-restore.md#decision-matrix) for your SQL Server workload.
- Ensure [Accelerated Networking is enabled](/azure/virtual-network/create-vm-accelerated-networking-cli#portal-creation) on the virtual machine.
- Use [Microsoft Defender for Cloud](/azure/security-center/azure-defender) for specific [SQL Server VM coverage](/azure/security-center/defender-for-sql-introduction) including [vulnerability assessments](/azure/security-center/defender-for-sql-on-machines-vulnerability-assessment), and [just-in-time access](/azure/security-center/just-in-time-explained), which reduces the attack surface while allowing legitimate users to access virtual machines when necessary.
- Use [Azure Advisor](/azure/advisor/advisor-overview) to address [performance](/azure/advisor/advisor-performance-recommendations), [cost](/azure/advisor/advisor-cost-recommendations), [reliability](/azure/advisor/advisor-high-availability-recommendations), [operational excellence](/azure/advisor/advisor-operational-excellence-recommendations), and [security recommendations](/azure/advisor/advisor-security-recommendations).
- Use [Azure Monitor](/azure/azure-monitor/vm/monitor-virtual-machine) to collect, analyze, and act on telemetry data from your SQL Server environment. This includes identifying infrastructure problems by using [VM insights](/azure/azure-monitor/vm/vminsights-overview) and monitoring data by using [Log Analytics](/azure/azure-monitor/logs/log-query-overview) for deeper diagnostics.
- Enable [Autoshutdown](/azure/automation/automation-solution-vm-management) for development and test environments.
- Implement a high availability and disaster recovery (HADR) solution that meets your business continuity SLAs. See the [HADR options](business-continuity-high-availability-disaster-recovery-hadr-overview.md#business-continuity-features) available for SQL Server on Azure VMs.
- Use the Azure portal (support + troubleshooting) to evaluate [resource health](/azure/service-health/resource-health-overview) and history. Submit new support requests when needed.


## HADR configuration

The checklist in this section covers the [HADR best practices](hadr-cluster-best-practices.md) for SQL Server on Azure VMs.

[!INCLUDE [HADR best practices](../../includes/virtual-machines-best-practices-hadr.md)]

## Performance troubleshooting

When you encounter SQL Server performance problems, use these diagnostic resources to identify and resolve specific problems:

- [Troubleshoot high-CPU-usage issues](/troubleshoot/sql/database-engine/performance/troubleshoot-high-cpu-usage-issues)
- [Understand and resolve blocking problems](/troubleshoot/sql/database-engine/performance/understand-resolve-blocking)
- [Troubleshoot slow-running queries](/troubleshoot/sql/database-engine/performance/troubleshoot-slow-running-queries)
- [Troubleshoot slow performance caused by I/O issues](/troubleshoot/sql/database-engine/performance/troubleshoot-sql-io-performance)
- [Troubleshoot query time-out errors](/troubleshoot/sql/database-engine/performance/troubleshoot-query-timeouts)
- [Troubleshoot out of memory or low memory](/troubleshoot/sql/database-engine/performance/troubleshoot-memory-issues)
- [Performance dashboard](/sql/relational-databases/performance/performance-dashboard) provides fast insight into SQL Server performance state.

## Related content

For detailed guidance on each optimization area, see:

- **[VM size](performance-guidelines-best-practices-vm-size.md)** - Choose the right VM series and configuration
- **[Storage](performance-guidelines-best-practices-storage.md)** - Optimize disk configuration and performance
- **[Security](security-considerations-best-practices.md)** - Implement security best practices
- **[HADR settings](hadr-cluster-best-practices.md)** - Configure high availability and disaster recovery
- **[Collect baseline](performance-guidelines-best-practices-collect-baseline.md)** - Establish performance baselines
- **[Updating SQL Server](servicing-updates-guidelines.md)** - Keep SQL Server up to date

**Recommended tool:** [Enable SQL Assessment for SQL Server on Azure VMs](sql-assessment-for-sql-vm.md) to automatically evaluate your configuration against these best practices.

Review other SQL Server Virtual Machine articles at [SQL Server on Azure Virtual Machines Overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).
