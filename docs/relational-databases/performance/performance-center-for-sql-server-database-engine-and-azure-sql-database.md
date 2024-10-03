---
title: "Performance Center"
description: Find the information that you need about performance in the SQL Server Database Engine and Azure SQL Database.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 06/29/2023
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
f1_keywords:
  - "Performance (SQL Server)"
  - "Performance (SQL Database)"
helpviewer_keywords:
  - "SQL Server, performance"
  - "performance (SQL Server)"
  - "database performance (SQL Server)"
  - "SQL Database (Performance)"
  - "performance (SQL Database)"
  - "database performance (SQL Database)"
---
# Performance Center for SQL Server Database Engine and Azure SQL Database

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

This page provides links to help you locate the information that you need about performance in the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

**Legend**

:::image type="content" source="media/performance-center-for-sql-server-database-engine-and-azure-sql-database/security-center-legend.png" alt-text="Screenshot of the legend that explains the feature availability icons.":::

## Configuration options for performance

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides the ability to affect database engine performance  through a number of configuration options at the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] level. With [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], Microsoft performs most, but not all, of these optimizations for you.

| Options | Description |
| - | - |
| **Disk configuration options** | :::image type="icon" source="media/security-center-sqlserver.png"::: [Disk striping and RAID](https://technet.microsoft.com/library/ms190764\(v=sql.105\).aspx) |
| **Data and log file configuration options** | :::image type="icon" source="media/security-center-sqlserver.png"::: [Place Data and Log Files on Separate Drives](../policy-based-management/place-data-and-log-files-on-separate-drives.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [View or Change the Default Locations for Data and Log Files (SQL Server Management Studio)](../../database-engine/configure-windows/view-or-change-the-default-locations-for-data-and-log-files.md) |
| `tempdb` configuration options** | :::image type="icon" source="media/security-center-sqlserver.png"::: [Performance Improvements in TempDB](../databases/tempdb-database.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Database Engine Configuration - TempDB](../../database-engine/install-windows/install-sql-server.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Using SSDs in Azure VMs to store SQL Server TempDB and Buffer Pool Extensions](https://cloudblogs.microsoft.com/sqlserver/2014/09/25/using-ssds-in-azure-vms-to-store-sql-server-tempdb-and-buffer-pool-extensions/)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Disk and performance best practices for temporary disk for SQL Server in Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-storage) |
| **(server configuration option)s** | **Processor configuration options**<br /><br />:::image type="icon" source="media/security-center-sqlserver.png"::: [affinity mask (server configuration option)](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [affinity Input-Output mask (server configuration option)](../../database-engine/configure-windows/affinity-input-output-mask-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [affinity64 mask (server configuration option)](../../database-engine/configure-windows/affinity64-mask-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [affinity64 Input-Output mask (server configuration option)](../../database-engine/configure-windows/affinity64-input-output-mask-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Configure the max worker threads (server configuration option)](../../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md)<br /><br />**Memory configuration options**<br /><br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Server Memory (server configuration option)s](../../database-engine/configure-windows/server-memory-server-configuration-options.md)<br /><br />**Index configuration options**<br /><br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Configure the fill factor (server configuration option)](../../database-engine/configure-windows/configure-the-fill-factor-server-configuration-option.md)<br /><br />**Query configuration options**<br /><br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Configure the min memory per query (server configuration option)](../../database-engine/configure-windows/configure-the-min-memory-per-query-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Configure the query governor cost limit (server configuration option)](../../database-engine/configure-windows/configure-the-query-governor-cost-limit-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Configure the max degree of parallelism (server configuration option)](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Configure the cost threshold for parallelism (server configuration option)](../../database-engine/configure-windows/configure-the-cost-threshold-for-parallelism-server-configuration-option.md)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [optimize for ad hoc workloads (server configuration option)](../../database-engine/configure-windows/optimize-for-ad-hoc-workloads-server-configuration-option.md)<br /><br />**Backup configuration options**<br /><br />:::image type="icon" source="media/security-center-sqlserver.png"::: [View or Configure the backup compression default (server configuration option)](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md) |
| **Database configuration optimization options** | :::image type="icon" source="media/security-center-sqlserver.png"::: [Data Compression](../data-compression/data-compression.md)<br />:::image type="icon" source="media/security-center-both.png"::: [View or Change the Compatibility Level of a Database](../databases/view-or-change-the-compatibility-level-of-a-database.md)<br />:::image type="icon" source="media/security-center-both.png"::: [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) |
| **Table configuration optimization** | :::image type="icon" source="media/security-center-sqlserver.png"::: [Partitioned Tables and Indexes](../partitions/partitioned-tables-and-indexes.md) |
| **Database Engine Performance in an Azure Virtual Machine** | :::image type="icon" source="media/security-center-sqlserver.png"::: [Quick check list](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Virtual machine size and storage account considerations](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-vm-size)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Disks and performance considerations](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-storage)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Collect baseline: Performance best practices](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-collect-baseline)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Feature specific performance considerations](https://techcommunity.microsoft.com/t5/fasttrack-for-azure/feature-comparison-of-azure-sql-database-azure-sql-managed/ba-p/3154789) |
| **Performance best practices and configuration guidelines for SQL Server on Linux** | :::image type="icon" source="media/security-center-sqlserver.png"::: [SQL Server configuration](../../linux/sql-server-linux-performance-best-practices.md#sql-server-configuration)<br />:::image type="icon" source="media/security-center-sqlserver.png"::: [Linux OS Configuration](../../linux/sql-server-linux-performance-best-practices.md) |

> [!IMPORTANT]  
> Additional considerations are available in:  
> -  [Recommended updates and configuration options for SQL Server 2012 and SQL Server 2014 with high-performance workloads](https://support.microsoft.com/help/2964518)
> -  [Recommended updates and configuration options for SQL Server 2017 and 2016 with high-performance workloads](https://support.microsoft.com/help/4465518)

## Query Performance Options

| Option | Description |
| - | - |
| :::image type="icon" source="media/security-center-both.png"::: **[Indexes](../indexes/indexes.md)** | [Reorganize and Rebuild Indexes](../indexes/reorganize-and-rebuild-indexes.md)<br />[Specify Fill Factor for an Index](../indexes/specify-fill-factor-for-an-index.md)<br />[Configure Parallel Index Operations](../indexes/configure-parallel-index-operations.md)<br />[SORT_IN_TEMPDB Option For Indexes](../indexes/sort-in-tempdb-option-for-indexes.md)<br />[Improve the Performance of Full-Text Indexes](../search/improve-the-performance-of-full-text-indexes.md)<br />[Configure the min memory per query (server configuration option)](../../database-engine/configure-windows/configure-the-min-memory-per-query-server-configuration-option.md)<br />[Configure the index create memory (server configuration option)](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md) |
| :::image type="icon" source="media/security-center-both.png"::: **[Partitioned Tables and Indexes](../partitions/partitioned-tables-and-indexes.md)** | [Benefits of Partitioning](../partitions/partitioned-tables-and-indexes.md) |
| :::image type="icon" source="media/security-center-both.png"::: **[Joins](joins.md)** | [Join Fundamentals](joins.md#fundamentals)<br />[Nested Loops join](joins.md#nested_loops)<br />[Merge join](joins.md#merge)<br />[Hash join](joins.md#hash) |
| :::image type="icon" source="media/security-center-both.png"::: **[Subqueries](subqueries.md)** | [Subquery Fundamentals](subqueries.md#fundamentals)<br />[Correlated subqueries](subqueries.md#correlated)<br />[Subquery types](subqueries.md#types) |
| :::image type="icon" source="media/security-center-both.png"::: **[Stored Procedures](../stored-procedures/stored-procedures-database-engine.md)** | [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md#best-practices) |
| :::image type="icon" source="media/security-center-both.png"::: **[User-Defined Functions](../user-defined-functions/user-defined-functions.md)** | [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md#best-practices)<br />[Create User-defined Functions (Database Engine)](../user-defined-functions/create-user-defined-functions-database-engine.md) |
| :::image type="icon" source="media/security-center-both.png"::: **Parallelism optimization** | [Configure the max worker threads (server configuration option)](../../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md)<br />[ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) |
| :::image type="icon" source="media/security-center-both.png"::: **Query optimizer optimization** | [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)<br />[USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) |
| :::image type="icon" source="media/security-center-both.png"::: **[Statistics](../statistics/statistics.md)** | [When to Update Statistics](../statistics/statistics.md)<br />[Update Statistics](../statistics/update-statistics.md) |
| :::image type="icon" source="media/security-center-both.png"::: **[In-Memory OLTP (In-Memory Optimization)](../in-memory-oltp/overview-and-usage-scenarios.md)** | [Memory-Optimized Tables](../in-memory-oltp/sample-database-for-in-memory-oltp.md)<br />[Natively Compiled Stored Procedures](../in-memory-oltp/a-guide-to-query-processing-for-memory-optimized-tables.md)<br />[Create and Access Tables in TempDB from Stored Procedures](../in-memory-oltp/create-and-access-tables-in-tempdb-from-stored-procedures.md)<br />[Troubleshooting Common Performance Problems with Memory-Optimized Hash Indexes](/previous-versions/sql/sql-server-2016/dn589805(v=sql.130))<br />[Demonstration: Performance Improvement of In-Memory OLTP](../in-memory-oltp/demonstration-performance-improvement-of-in-memory-oltp.md) |
| :::image type="icon" source="media/security-center-both.png"::: **[Intelligent query processing](intelligent-query-processing.md)** | [Intelligent query processing](intelligent-query-processing.md) |

## See also

- [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)
- [Monitoring Performance By Using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [Azure SQL Database performance guidance for single databases](/azure/azure-sql/database/performance-guidance)
- [Optimizing Azure SQL Database Performance using Elastic Pools](/azure/azure-sql/database/elastic-pool-overview)
- [Query Performance Insight for Azure SQL Database](/azure/azure-sql/database/query-performance-insight-use)
- [Index Design Guide](../sql-server-index-design-guide.md)
- [Memory Management Architecture Guide](../memory-management-architecture-guide.md)
- [Pages and Extents Architecture Guide](../pages-and-extents-architecture-guide.md)
- [Post-migration Validation and Optimization Guide](../post-migration-validation-and-optimization-guide.md)
- [Query Processing Architecture Guide](../query-processing-architecture-guide.md)
- [SQL Server Transaction Locking and Row Versioning Guide](../sql-server-transaction-locking-and-row-versioning-guide.md)
- [SQL Server Transaction Log Architecture and Management Guide](../sql-server-transaction-log-architecture-and-management-guide.md)
- [Thread and Task Architecture Guide](../thread-and-task-architecture-guide.md)
