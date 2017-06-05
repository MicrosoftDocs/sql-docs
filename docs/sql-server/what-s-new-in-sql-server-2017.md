---
title: "What's new in SQL Server 2017 | Microsoft Docs"
ms.custom: ""
ms.date: "06/1/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0b57f375-9242-4bb2-9d4b-c560d5a93524
caps.latest.revision: 71
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# What's new in SQL Server 2017
SQL Server 2017 represents a major step towards making SQL Server a platform that gives you choices of development languages, data types, on-premises or cloud, and operating systems by bringing the power of SQL Server to Linux, Linux-based Docker containers, and Windows. This topic summarizes what is new in the most recent SQL Server 2017 Community Technical Preview (CTP) release and in specific feature areas.

**Try it out:** [Download the SQL Server 2017 Community Technology Preview](http://go.microsoft.com/fwlink/?LinkID=829477)

>[!TIP]
>**Run SQL Server on Linux!** For more information, see [SQL Server on Linux Documentation](https://docs.microsoft.com/sql/linux/) and [What's new for SQL Server 2017 on Linux](https://docs.microsoft.com/sql/linux/sql-server-linux-whats-new).

## Latest release
New in the SQL Server 2017 CTP 2.1 release, May 2017:
- A new dynamic management view, [sys.dm_db_log_stats](../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md), exposes summary level attributes and information on transaction log files, helpful for monitoring transaction log health. 
- In SQL Server Reporting Services (SSRS), comments are now available for reports, to add perspective and collaborate with others. 
- SQL Server Integration Services (SSIS) now supports SQL Server on Linux, and a new package lets you run SSIS packages on Linux from the command line. For more information, see the [blog post announcing SSIS support for Linux](https://blogs.msdn.microsoft.com/ssis/2017/05/17/ssis-helsinki-is-available-in-sql-server-vnext-ctp2-1/).

## SQL Server Database Engine  
SQL Server 2017 includes many new Database Engine features, enhancements, and performance improvements. Some highlights are:
- **Resumable online index rebuild** resumes an online index rebuild operation from where it stopped after a failure (such as a failover to a replica or insufficient disk space), or pauses and later resumes an online index rebuild operation. See [ALTER INDEX](../t-sql/statements/alter-index-transact-sql.md) and [Guidelines for online index operations](../relational-databases/indexes/guidelines-for-online-index-operations.md). (CTP 2.0)
- A new **IDENTITY_CACHE** option for ALTER DATABASE SCOPED CONFIGURATION allows you to avoid gaps in the values of identity columns if a server restarts unexpectedly or fails over to a secondary server. See [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). (CTP 2.0)
- **Automatic database tuning** provides insight into potential query performance problems, recommends solutions, and can automatically fix identified problems. See [Automatic tuning](../relational-databases/automatic-tuning/automatic-tuning.md). (CTP 2.0)
- New **graph database capabilities** for modeling many-to-many relationships include new [CREATE TABLE](../t-sql/statements/create-table-sql-graph.md) syntax for creating node and edge tables, and the keyword [MATCH](../t-sql/queries/match-sql-graph.md) for queries. See [Graph Processing with SQL Server 2017](../relational-databases/graphs/sql-graph-overview.md). (CTP 2.0)
- An sp_configure option called `clr strict security` is enabled by default, and enhances the security of CLR assemblies. See [CLR strict security](../database-engine/configure-windows/clr-strict-security.md). (CTP 2.0)
- Setup allows specifying initial tempdb file size up to 256 GB (262,144 MB) per file, with a warning if the file size is set greater than 1GB with IFI not enabled. (CTP 2.0)
- A new dynamic management view, [sys.dm_tran_version_store_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-tran-version-store-space-usage.md), tracks version store usage per database, useful for proactively planning tempdb sizing based on the version store usage per database. (CTP 2.0)
- A new dynamic management view, [sys.dm_db_log_info](../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md), exposes VLF information to monitor, alert, and avert potential transaction log issues. (CTP 2.0)
- A new **modified_extent_page_count** column in [sys.dm_db_file_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md) tracks differential changes in each database file, enabling smart backup solutions that perform differential backup or full backup based on percentage of changed pages in the database. (CTP 2.0)
- [SELECT INTO](../t-sql/queries/select-into-clause-transact-sql.md) T-SQL syntax now supports loading a table into a FileGroup other than the user's default by using the **ON** keyword. (CTP 2.0)
- Cross database transactions are now supported among all databases that are part of an Always On Availability Group, including databases that are part of same instance. See [Transactions - Always On Availability Groups and Database Mirroring](../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md) (CTP 2.0)
- Five new dynamic management views return information about Linux processes. For more information, see [Linux Process Dynamic Management Views](../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md). (CTP 1.3)
- [sys.dm_db_stats_histogram] (../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) is a new dynamic management view for examining statistics. (CTP 1.3)
- New Availability Groups functionality includes clusterless support, Minimum Replica Commit Availability Groups setting, and Windows-Linux cross-OS migrations and testing. (CTP 1.3)
- The **Database Tuning Advisor (DTA)** has additional options and improved performance. (CTP 1.2)
- **In-memory enhancements** include support for computed columns in memory-optimized tables, full support for JSON functions in natively compiled modules, and the CROSS APPLY operator in natively compiled modules. (CTP 1.1)
- New string functions are CONCAT_WS, TRANSLATE, and TRIM. WITHIN GROUP is now supported for the STRING_AGG function. (CTP 1.1)
- There are new bulk access options (BULK INSERT and OPENROWSET(BULK...) ) for CSV and Azure Blob files. (CTP 1.1)
- **Memory-optimized object enhancements** include sp_spaceused and elimination of the 8 index limitation for memory-optimized tables, sp_rename for memory-optimized tables and natively compiled T-SQL modules, and CASE statements and TOP (N) WITH TIES for natively compiled T-SQL modules. Memory-optimized filegroup files can now be stored, backed up and restored on Azure Storage. (CTP 1.0)
- **DATABASE SCOPED CREDENTIAL** is a new class of securable, supporting CONTROL, ALTER, REFERENCES, TAKE OWNERSHIP, and VIEW DEFINITION permissions. ADMINISTER DATABASE BULK OPERATIONS is now visible in sys.fn_builtin_permissions. (CTP 1.0)
- The sys.dm_os_host_info dynamic management view provides operating system information for both Windows and Linux. (CTP 1.0)
- Database COMPATIBILITY_LEVEL 140 is added. (CTP 1.0).  
For more information, see [What's new in SQL Server 2017 database engine](~/database-engine/configure-windows/what-s-new-in-sql-server-2017-database-engine.md)

## SQL Server Machine Learning Services
- SQL Server Machine Learning Services (formerly R Services) now supports Python. You can use Machine Learning Services (in-database) to run R or Python scripts in SQL Server, or install Microsoft Machine Learning Server (standalone) to deploy and consume R and Python models that don't require SQL Server. 
- Both platforms include new MicrosoftML algorithms for distributed machine learning, and the latest version of Microsoft R (version 9.1.0). (CTP 2.0)
For more information, see [What's new in SQL Server Machine Learning Services](~/advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md)

## SQL Server Analysis Services (SSAS) 
- Encoding hints is an advanced feature used to optimize data refresh of large in-memory tabular models. (CTP 1.3)
For more information, see [What's new in SQL Server Analysis Services 2017](~/analysis-services/what-s-new-in-sql-server-analysis-services-2017.md)
analysis-services/what-s-new-in-analysis-services.md,

## SQL Server Integration Services (SSIS)
- SQL Server Integration Services (SSIS) now supports SQL Server on Linux, and a new package lets you run SSIS packages on Linux from the command line. For more information, see the blog post announcing SSIS support for Linux. (CTP 2.1)
- You can now use the **Use32BitRuntime** parameter. (CTP 2.1)
For more information, see [What's new in Integration Services in SQL Server 2017](~/integration-services/what-s-new-in-integration-services-in-sql-server-2017.md)

## SQL Server Reporting Services (SSRS)
- As of CTP 2.1, SSRS is no longer available to install through SQL Server setup. 
- To download the May 2017 Preview of Power BI Report Server and Power BI Desktop optimized for Power BI Report Server, go to the [Microsoft download center](https://www.microsoft.com/download/details.aspx?id=55253). For information about Power BI Report Server, see [Get started with Power BI Report Server](https://powerbi.microsoft.com/documentation/reportserver-get-started/).
- Comments are now available for reports, to add perspective and collaborate with others. You can also include attachments with comments. (CTP 2.1)
- In the latest releases of Report Builder and SQL Server Data Tools – Release Candidate, you can create native DAX queries against supported SQL Server Analysis Services tabular data models by dragging and dropping desired fields in the query designers. See the [Reporting Services blog](https://blogs.msdn.microsoft.com/sqlrsteamblog/2017/03/09/query-designer-support-for-dax-now-available-in-report-builder-and-sql-server-data-tools/).
For more information, see [What's new in SQL Server reporting services (SSRS)](~/reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md)

## Linux
For more information, see [What's new for SQL Server 2017 on Linux](~/linux/sql-server-linux-whats-new.md)