---
title: "What's new in Database Engine - SQL Server 2017 | Microsoft Docs"
ms.custom: ""
ms.date: 10/24/2017
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
ms.assetid: 42f45b23-6509-45e8-8ee7-76a78f99a920
author: rothja
ms.author: jroth
manager: craigg
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# What's new in Database Engine - SQL Server 2017
[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

This topic describes improvements made to the [!INCLUDE[ssdenoversion-md](../includes/ssdenoversion-md.md)] for [!INCLUDE[sssqlv14-md](../includes/sssqlv14-md.md)]. Click the links for more information on each item.

> [!NOTE]  
> SQL Server 2017 also includes the features added in SQL Server 2016 service packs. For those items, see [What's New in SQL Server 2016 (Database Engine)](../database-engine/configure-windows/what-s-new-in-sql-server-2016-database-engine.md).

**Enhancements**  

- CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssqlv14-md](../includes/sssqlv14-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this is not recommended. Microsoft recommends that all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database. CLR assemblies can be added to a whitelist, as a work-around for the `clr strict security` feature. [sp_add_trusted_assembly](../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md), [sp_drop_trusted_assembly](../relational-databases/system-stored-procedures/sys-sp-drop-trusted-assembly-transact-sql.md), and [sys.trusted_asssemblies](../relational-databases/system-catalog-views/sys-trusted-assemblies-transact-sql.md) are added to support the white list of trusted assemblies. For more information, see [CLR strict security](configure-windows/clr-strict-security.md).  
- A new, DMF [sys.dm_db_log_stats](../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md), is introduced to expose summary level attributes and information on transaction log files; useful for monitoring the health of the transaction log.  
- Resumable online index rebuild. Resumable online index rebuild allows you to resume an online index rebuild operation from where it stopped after a failure (such as a failover to a replica or insufficient disk space). You can also pause and later resume an online index rebuild operation. For example, you might need to temporarily free up systems resources in order to execute a high priority task or complete the index rebuild in another maintenance window if the available maintenance windows is too short for a large table. Finally, resumable online index rebuild does not require significant log space, which allows you to perform log truncation while the resumable rebuild operation is running. See [ALTER INDEX](../t-sql/statements/alter-index-transact-sql.md) and [Guidelines for online index operations](../relational-databases/indexes/guidelines-for-online-index-operations.md).
- **IDENTITY_CACHE option for ALTER DATABASE SCOPED CONFIGURATION**. A new option IDENTITY_CACHE was added to `ALTER DATABASE SCOPED CONFIGURATION` T-SQL statement. When this option is set to `OFF`, it allows the Database Engine to avoid gaps in the values of identity columns in case a server restarts unexpectedly or fails over to a secondary server. See [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).   
-  [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)] now offers graph database capabilities to model more meaninginful relationship-oriented data. This includes new [CREATE TABLE](../t-sql/statements/create-table-sql-graph.md) syntax for creating node and edge tables, and the keyword [MATCH](../t-sql/queries/match-sql-graph.md) for queries. For more information, see [Graph Processing with SQL Server 2017](../relational-databases/graphs/sql-graph-overview.md).   
- A new generation of query processing improvements that will adapt optimization strategies to your application workload's runtime conditions. For this first version of the **adaptive query processing** feature family, we have three new improvements: **batch mode adaptive joins**, **batch mode memory grant feedback**, and **interleaved execution** for multi-statement table valued functions.  See [Adaptive query processing in SQL databases](../relational-databases/performance/adaptive-query-processing.md).
- Automatic tuning is a database feature that provides insight into potential query performance problems, recommend solutions, and automatically fix identified problems. Automatic tuning in [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)], notifies you whenever a potential performance issue is detected, and lets you apply corrective actions, or lets the [!INCLUDE[ssde-md](../includes/ssde-md.md)] automatically fix performance problems. For more information, see [Automatic tuning](../relational-databases/automatic-tuning/automatic-tuning.md).
- PERFORMANCE ENHANCEMENT FOR NON CLUSTERED INDEX BUILD ON MEMORY-OPTIMIZED TABLES. Performance of bwtree (non-clustered) index rebuild for MEMORY_OPTIMIZED tables during database recovery has been significantly optimized. This improvement substantially reduces the database recovery time when non-clustered indexes are used.  
- [sys.dm_os_sys_info](../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) has three new columns: socket_count, cores_per_socket, numa_node_count. This is useful when you run your server in a VM, as exceding NUMA can lead overcommited hosts that in the end transforms to performance issues.
- A new column modified_extent_page_count\, is introduced in [sys.dm_db_file_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md) to track differential changes in each database file of the database. The new column modified_extent_page_count allows you to build smart backup solution, which performs differential backup if percentage changed pages in the database is below a threshold (say 70-80%) else perform full database backup.
- SELECT INTO ... ON FileGroup - [SELECT INTO](../t-sql/queries/select-into-clause-transact-sql.md) now supports loading a table into a filegroup other than a default filegroup of the user using the **ON** keyword support added in SELECT INTO TSQL syntax.
- Tempdb Setup Improvements - The setup allows specifying initial tempdb file size up to **256 GB (262,144 MB)** per file with a warning to customers if the file size is set to value greater than 1 GB and if IFI is not enabled. It is important to understand the implication of not enabling instant file initialization (IFI) where setup time can increase exponentially depending on the initial size of tempdb data file specified. IFI is not applicable to transaction log size so specifying larger value of transaction log can invariably increase the setup time while starting up tempdb during setup irrespective of the IFI setting for SQL Server service account.
- A new dmv [sys.dm_tran_version_store_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-tran-version-store-space-usage.md) is introduced to track version store usage per database. This new dmv is useful in monitoring tempdb for version store usage for you to proactively plan tempdb sizing based on the version store usage requirement per database without any performance toll or overheads of running it on production servers.
- A new DMF [sys.dm_db_log_info](../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md) is introduced to expose the VLF information similar to DBCC LOGINFO to monitor, alert, and avert potential transaction log issues caused due to number of VLFs, VLF size or shrinkfile issues experienced by customers.
- Improved Backup performance for small databases on high end servers - While performing backup of databases in SQL Server, the backup process requires multiple iterations of buffer pool to drain the on-going I/Os. As a result, the backup time is not just the function of database size but also a function of active buffer pool size. In SQL Server 2017, the backup is optimized to avoid multiple iterations of buffer pool resulting in dramatic gains in backup performance for small to medium databases. The performance gain reduces as the database size increases as the pages to backup and backup IO takes more time compared to iterating buffer pool.  
- Query Store now tracks wait stats summary information. Tracking wait stats categories per query in Query Store enables the next level of performance troubleshooting experience, providing, even more, insight into the workload performance and its bottlenecks while preserving the key Query Store advantages.  
- System-versioned temporal tables now support CASCADE DELETE and CASCADE UPDATE.  
- Indirect checkpoint performance improvements.
- Cluster-less Availability Groups support added.
- Minimum Replica Commit Availability Groups setting added.
- Availability Groups can now work across Windows-Linux to enable cross-OS migrations and testing.
- Temporal Tables Retention Policy support added,
- New DMV SYS.DM_DB_STATS_HISTOGRAM
- Online non-clustered columnstore index build and rebuild support added
- [sys.dm_db_stats_histogram (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) is added for examining statistics.
- The Database Tuning Advisor (DTA) released with SQL Server Management Studio version 16.4, when analyzing SQL Server 2016 and later, has additional options.    
   - Improved performance. For more information, see [Performance Improvements using Database Engine Tuning Advisor (DTA) recommendations](../relational-databases/performance/performance-improvements-using-dta-recommendations.md).
   - The `-fc` option for allowing recommendations of columnstore indexes. For more information, see [DTA Utility](../tools/dta/dta-utility.md) and [Columnstore index recommendations in Database Engine Tuning Advisor (DTA)](../relational-databases/performance/columnstore-index-recommendations-in-database-engine-tuning-advisor-dta.md).  
   - The `-iq` option for allowing the DTA to review a workload from the Query Store. For more information, see [Tuning Database Using Workload from Query Store](../relational-databases/performance/tuning-database-using-workload-from-query-store.md).  
- For In-Memory functionality, additional enhancements to memory-optimized tables and natively compiled functions are available. For code samples that illustrate these enhancements, see [Optimize JSON processing with in-memory OLTP](../relational-databases/json/optimize-json-processing-with-in-memory-oltp.md).
    - Support for computed columns in memory-optimized tables, including indexes on computed columns.
    - Full support for JSON functions in natively compiled modules, and in check constraints.  
    - `CROSS APPLY` operator in natively compiled modules.   
- New string functions [CONCAT_WS](../t-sql/functions/concat-ws-transact-sql.md), [TRANSLATE](../t-sql/functions/translate-transact-sql.md), and [TRIM](../t-sql/functions/trim-transact-sql.md) are added.   
- The `WITHIN GROUP` clause is now supported for the [STRING_AGG](../t-sql/functions/string-agg-transact-sql.md) function.
- Two new Japanese collation families (Japanese_Bushu_Kakusu_140 and Japanese_XJIS_140) were added, and the collation option Variation-selector-sensitive (_VSS) was added for use in these new Japanese collations. Also, all of the new collations automatically support supplementary characters without needing to specify the _SC option. For more detail see [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md)   
- New bulk access options ([BULK INSERT](../t-sql/statements/bulk-insert-transact-sql.md) and [OPENROWSET(BULK...)](../t-sql/functions/openrowset-transact-sql.md)) enable access data directly from a file specified as CSV format, and from files stored in Azure Blob storage through the new `BLOB_STORAGE` option of [EXTERNAL DATA SOURCE](../t-sql/statements/create-external-data-source-transact-sql.md).
- Database **COMPATIBILITY_LEVEL** 140 has been added.   Customers running in this level will get the latest language features and query optimizer behaviors. This includes changes in each pre-release version Microsoft releases.
- Improvements to the way incremental statistics update thresholds are computed (140 compatibility mode required).
- [sys.dm_exec_query_statistics_xml](../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md) is added.
- We have made several performance and language enhancements to Memory-Optimized objects:
    - `sp_spaceused` is now supported for memory-optimized tables.
    - `sp_rename` is now supported for memory-optimized tables and natively compiled T-SQL modules.
    - `CASE` expressions are now supported for natively compiled T-SQL modules.
    - The limitation of 8 indexes on memory-optimized tables has been eliminated.
    - `TOP (N) WITH TIES` is now supported in natively compiled T-SQL modules.
    - `ALTER TABLE` against memory-optimized tables is now usually substantially faster.
    - Transaction log redo of memory-optimized tables is now done in parallel. This bolsters faster recovery times and significantly increases the sustained throughput of AlwaysOn availability group configuration.
    - Memory-optimized filegroup files can now be stored on Azure Storage. Backup/Restore of memory-optimized files on Azure Storage is also available now.
- Clustered Columnstore Indexes now support LOB columns (nvarchar(max), varchar(max), varbinary(max)).
- The [STRING_AGG](../t-sql/functions/string-agg-transact-sql.md) aggregate function has been added.  
- New Permissions: `DATABASE SCOPED CREDENTIAL` is now a class of securable, supporting `CONTROL`, `ALTER`, `REFERENCES`, `TAKE OWNERSHIP`, and `VIEW DEFINITION` permissions. `ADMINISTER DATABASE BULK OPERATIONS`, which is restricted to SQL Database, is now visible in `sys.fn_builtin_permissions`.   
- The [sys.dm_os_host_info](../relational-databases/system-dynamic-management-views/sys-dm-os-host-info-transact-sql.md) DMV is added to provide operating system information for both Windows and Linux.   
- The database roles are created with R Services for managing permissions associated with packages. For more information, see [R Package management for SQL Server](../advanced-analytics/r-services/r-package-management-for-sql-server-r-services.md).

