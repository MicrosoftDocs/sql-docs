---
title: "What's new in SQL Server 2017 | Microsoft Docs"
description: Find out what's new for SQL Server 2017, which brings the power of SQL Server to Linux and Linux-based Docker containers as well as Windows.
ms.custom:
  - intro-whats-new
ms.date: "10/20/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: release-landing
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">= sql-server-2016"
---
# What's new in SQL Server 2017
[!INCLUDE[SQL Server 2017](../includes/applies-to-version/sqlserver2017.md)]
SQL Server 2017 represents a major step towards making SQL Server a platform that gives you choices of development languages, data types, on-premises or cloud, and operating systems by bringing the power of SQL Server to Linux, Linux-based Docker containers, and Windows. This topic summarizes what is new for specific feature areas and includes links to additional details. For more information related to SQL Server on Linux, see [SQL Server on Linux](../linux/sql-server-linux-overview.md).

:::image type="icon" source="../includes/media/download.svg"::: **Try it out:** [Download SQL Server 2017 Release - October 2017](https://go.microsoft.com/fwlink/?LinkID=829477).

> [!NOTE]
> In addition to the changes below, cumulative updates are released at regular intervals after the GA release. These cumulative updates provide many improvements and fixes. For information about the latest CU release, see [SQL Server 2017 Cumulative updates](https://aka.ms/sql2017cu).

## SQL Server 2017 Database Engine

SQL Server 2017 includes many new Database Engine features, enhancements, and performance improvements. 
- **CLR assemblies** can now be added to a list of trusted assemblies, as a workaround for the `clr strict security` feature described in CTP 2.0. [sp_add_trusted_assembly](../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md), [sp_drop_trusted_assembly](../relational-databases/system-stored-procedures/sys-sp-drop-trusted-assembly-transact-sql.md), and [sys.trusted_asssemblies](../relational-databases/system-catalog-views/sys-trusted-assemblies-transact-sql.md) are added to support the list of trusted assemblies (RC1).  
- **Resumable online index rebuild** resumes an online index rebuild operation from where it stopped after a failure (such as a failover to a replica or insufficient disk space), or pauses and later resumes an online index rebuild operation. See [ALTER INDEX](../t-sql/statements/alter-index-transact-sql.md) and [Guidelines for online index operations](../relational-databases/indexes/guidelines-for-online-index-operations.md). (CTP 2.0)
- The **IDENTITY_CACHE** option for ALTER DATABASE SCOPED CONFIGURATION allows you to avoid gaps in the values of identity columns if a server restarts unexpectedly or fails over to a secondary server. See [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). (CTP 2.0)
- A new generation of query processing improvements that will adapt optimization strategies to your application workload's runtime conditions. For this first version of the **adaptive query processing** feature family, we have three new improvements: **batch mode adaptive joins**, **batch mode memory grant feedback**, and **interleaved execution** for multi-statement table valued functions.  See [Intelligent query processing in SQL databases](../relational-databases/performance/intelligent-query-processing.md).
- **Automatic database tuning** provides insight into potential query performance problems, recommends solutions, and can automatically fix identified problems. See [Automatic tuning](../relational-databases/automatic-tuning/automatic-tuning.md). (CTP 2.0)
- New **graph database capabilities** for modeling many-to-many relationships include new [CREATE TABLE](../t-sql/statements/create-table-sql-graph.md) syntax for creating node and edge tables, and the keyword [MATCH](../t-sql/queries/match-sql-graph.md) for queries. See [Graph Processing with SQL Server 2017](../relational-databases/graphs/sql-graph-overview.md). (CTP 2.0)
- An sp_configure option called `clr strict security` is enabled by default to enhance the security of CLR assemblies. See [CLR strict security](../database-engine/configure-windows/clr-strict-security.md). (CTP 2.0)
- Setup now allows specifying initial tempdb file size up to **256 GB** (262,144 MB) per file, with a warning if the file size is set greater than 1GB with IFI not enabled. (CTP 2.0)
- The **modified_extent_page_count** column in [sys.dm_db_file_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md) tracks differential changes in each database file, enabling smart backup solutions that perform differential backup or full backup based on percentage of changed pages in the database. (CTP 2.0)
- [SELECT INTO](../t-sql/queries/select-into-clause-transact-sql.md) T-SQL syntax now supports loading a table into a FileGroup other than the user's default by using the **ON** keyword. (CTP 2.0)
- Cross database transactions are now supported among all databases that are part of an **Always On Availability Group**, including databases that are part of same instance. See [Transactions - Always On Availability Groups and Database Mirroring](../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md) (CTP 2.0)
- New **Availability Groups** functionality includes read-scale support without a cluster, Minimum Replica Commit Availability Groups setting, and Windows-Linux cross-OS migrations and testing. (CTP 1.3)
- New dynamic management views:
    - [sys.dm_db_log_stats](../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md) exposes summary level attributes and information on transaction log files, helpful for monitoring transaction log health. (CTP 2.1)
    - [sys.dm_tran_version_store_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-tran-version-store-space-usage.md) tracks version store usage per database, useful for proactively planning tempdb sizing based on the version store usage per database. (CTP 2.0)
    - [sys.dm_db_log_info](../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md) exposes VLF information to monitor, alert, and avert potential transaction log issues. (CTP 2.0)
    - [sys.dm_db_stats_histogram](../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) is a new dynamic management view for examining statistics. (CTP 1.3)
    - **sys.dm_os_host_info** provides operating system information for both Windows and Linux. (CTP 1.0)
- The **Database Tuning Advisor (DTA)** has additional options and improved performance. (CTP 1.2)
- **In-memory enhancements** include support for computed columns in memory-optimized tables, full support for JSON functions in natively compiled modules, and the CROSS APPLY operator in natively compiled modules. (CTP 1.1)
- New **string functions** are CONCAT_WS, TRANSLATE, and TRIM, and WITHIN GROUP is now supported for the STRING_AGG function. (CTP 1.1)
- There are new **bulk access options** (BULK INSERT and OPENROWSET(BULK...) ) for CSV and Azure Blob files. (CTP 1.1)
- **Memory-optimized object enhancements** include sp_spaceused and elimination of the 8 index limitation for memory-optimized tables, sp_rename for memory-optimized tables and natively compiled T-SQL modules, and CASE and TOP (N) WITH TIES for natively compiled T-SQL modules. Memory-optimized filegroup files can now be stored, backed up and restored on Azure Storage. (CTP 1.0)
- **DATABASE SCOPED CREDENTIAL** is a new class of securable, supporting CONTROL, ALTER, REFERENCES, TAKE OWNERSHIP, and VIEW DEFINITION permissions. ADMINISTER DATABASE BULK OPERATIONS is now visible in sys.fn_builtin_permissions. (CTP 1.0)
- Database **COMPATIBILITY_LEVEL 140** is added. (CTP 1.0).  

## SQL Server 2017 Integration Services (SSIS)
- The new **Scale Out** feature in SSIS has the following new and changed features. For more info, see [What's New in Integration Services in SQL Server 2017](~/integration-services/what-s-new-in-integration-services-in-sql-server-2017.md). (RC1)
    -   Scale Out Master now supports high availability.
    -   The failover handling of the execution logs from Scale Out Workers is improved.
    -   The parameter *runincluster* of the stored procedure **[catalog].[create_execution]** is renamed to *runinscaleout* for consistency and readability.
    -   The SSIS Catalog has a new global property to specify the default mode for executing SSIS packages.
- In the new **Scale Out for SSIS** feature, you can now use the **Use32BitRuntime** parameter when you trigger execution. (CTP 2.1)
- SQL Server 2017 Integration Services (SSIS) now supports **SQL Server on Linux**, and a new package lets you run SSIS packages on Linux from the command line. For more information, see the [blog post announcing SSIS support for Linux](https://blogs.msdn.microsoft.com/ssis/2017/05/17/ssis-helsinki-is-available-in-sql-server-vnext-ctp2-1/). (CTP 2.1)
- The new **Scale Out for SSIS** feature makes it much easier to run SSIS on multiple machines. See [Integration Services Scale Out](~/integration-services/scale-out/integration-services-ssis-scale-out.md). (CTP 1.0)
- OData Source and OData Connection Manager now support connecting to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online. (CTP 1.0)

For more info, see [What's New in Integration Services in SQL Server 2017](~/integration-services/what-s-new-in-integration-services-in-sql-server-2017.md).

## SQL Server 2017 Master Data Services (MDS)
- Experience and performance are improved when upgrading from SQL Server 2012, SQL Server 2014, and SQL Server 2016 to SQL Server 2017 Master Data Services. 
- You can now view the sorted lists of entities, collections and hierarchies in the **Explorer** page of the Web application.
- Performance is improved for staging millions of records using the staging stored procedure.
- Performance is improved when expanding the **Entities** folder on the **Manage Groups** page to assign model permissions. The **Manage Groups** page is located in the **Security** section of the Web application. For more information about the performance improvement, see [https://support.microsoft.com/help/4023865?preview](https://support.microsoft.com/help/4023865?preview). For more information about assigning permissions, see [Assign Model Object Permissions (Master Data Services)](../master-data-services/assign-model-object-permissions-master-data-services.md).

## SQL Server 2017 Analysis Services (SSAS) 
SQL Server Analysis Services 2017 introduces many enhancements for tabular models. These include:
- Tabular mode as the default installation option for Analysis Services. (CTP 2.0)
- Object-level security to secure the metadata of tabular models. (CTP 2.0)
- Date relationships to easily create relationships based on date fields. (CTP 2.0)
- New **Get Data** (Power Query) data sources, and existing DirectQuery data sources support for M queries. (CTP 2.0) 
- DAX Editor for SSDT. (CTP 2.0)
- Encoding hints, an advanced feature for optimizing data refresh of large in-memory tabular models. (CTP 1.3)
- Support for the **1400 Compatibility level** for tabular models. To create new or upgrade existing tabular model projects to the 1400 compatibility level, download and install [SQL Server Data Tools (SSDT) 17.0 RC2](https://go.microsoft.com/fwlink?LinkId=837939). (CTP 1.1)
- A modern **Get Data** experience for tabular models at the 1400 compatibility level. See the [Analysis Services Team Blog](/archive/blogs/analysisservices/introducing-a-modern-get-data-experience-for-sql-server-vnext-on-windows-ctp-1-1-for-analysis-services). (CTP 1.1)
- **Hide Members** property to hide blank members in ragged hierarchies. (CTP 1.1)
- New **Detail Rows** end-user action to **Show Details** for aggregated information. [SELECTCOLUMNS](/dax/selectcolumns-function-dax) and **DETAILROWS** functions for creating Detail Rows expressions. (CTP 1.1)
- DAX **IN** operator for specifying multiple values. (CTP 1.1)

For more information, see [What's new in SQL Server Analysis Services](/analysis-services/what-s-new-in-analysis-services).

## SQL Server 2017 Reporting Services (SSRS)
SQL Server Reporting Services is no longer available to install through SQL Server setup. Go to the Microsoft Download Center to [download Microsoft SQL Server 2017 Reporting Services](https://www.microsoft.com/download/details.aspx?id=55252). 
- Comments are now available for reports, to add perspective and collaborate with others. You can also include attachments with comments.
- In the latest releases of Report Builder and SQL Server Data Tools, you can create native DAX queries against supported SQL Server Analysis Services tabular data models by dragging and dropping desired fields in the query designers. See the [Reporting Services blog](/archive/blogs/sqlrsteamblog/query-designer-support-for-dax-now-available-in-report-builder-and-sql-server-data-tools).
- To enable development of modern applications and customization, SSRS now supports a fully OpenAPI compliant RESTful API. The full API specification and documentation can now be found on [swaggerhub](https://app.swaggerhub.com/apis/microsoft-rs/SSRS/2.0).

For more information, see [What's new in SQL Server Reporting Services (SSRS)](~/reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md).

## Machine Learning in SQL Server 2017 

SQL Server R Services has been renamed **SQL Server Machine Learning Services**, to reflect support for Python in addition to the R language. You can use Machine Learning Services (In-Database) to run R or Python scripts in SQL Server, or install **Microsoft Machine Learning Server (Standalone)** to deploy and consume R and Python models that don't require SQL Server. 

SQL Server developers now have access to the extensive Python ML and AI libraries available in the open-source ecosystem, along with the latest innovations from Microsoft:

- **revoscalepy** - This Python equivalent of RevoScaleR includes parallel algorithms for linear and logistic regressions, decision tree, boosted trees and random forests, as well as a rich set of APIs for data transformation and data movement, remote compute contexts, and data sources.
- **microsoftml** - This state-of-the-art package of machine learning algorithms and transforms with Python bindings includes deep neural networks, fast decision trees and decision forests, and optimized algorithms for linear and logistic regressions. You also get pre-trained models based on ResNet models that you can use for image extraction or sentiment analysis.
- **Python operationalization with T-SQL** - Deploy Python code easily by using the stored procedure `sp_execute_external_script`. Get great performance by streaming data from SQL to Python processes and using MPI ring parallelization.
- **Python in SQL Server compute contexts** - Data scientists and developers can execute Python code remotely from their development environments to explore data and develop models without moving data around.
- **Native scoring** -  The PREDICT function in Transact-SQL can be used to perform scoring in any instance of SQL Server 2017, even if R isn't installed. All that's required is that you train the model using one of the supported RevoScaleR and revoscalepy algorithms and save the model in a new, compact binary format.
- **Package management** - T-SQL now supports the CREATE EXTERNAL LIBRARY statement, to give DBAs greater management over R packages. Use roles to control private or shared package access, store R packages in the database and share them among users.
- **Performance improvements** - The stored procedure `sp_execute_external_script` has been optimized to support batch mode execution for columnstore data.


For more information, see [What's new in SQL Server Machine Learning Services](~/machine-learning/what-s-new-in-sql-server-machine-learning-services.md).

## Next steps
- See the [SQL Server 2017 Release Notes](sql-server-2017-release-notes.md).
- Find out [What's new for SQL Server 2017 on Linux](../linux/sql-server-linux-whats-new.md).
- Find out [What's new in SQL Server 2016](what-s-new-in-sql-server-2016.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)
