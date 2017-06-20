---
title: "What&#39;s New in SQL Server 2017 | Microsoft Docs"
ms.custom: ""
ms.date: "06/19/2017"
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
# What&#39;s New in SQL Server 2017
SQL Server 2017 represents a major step towards making SQL Server a platform that gives you choices of development languages, data types, on-premises and in the cloud, and across operating systems by bringing the power of SQL Server to Linux, Linux-based Docker containers, and Windows.

This topic is a summary of what is new in the most recent Community Technical Preview (CTP) release and links to more detailed what's new information for specific feature areas.

![info_tip](../sql-server/media/info-tip.png) Run SQL Server on Linux! For more information, see:
-  [What's new for SQL Server 2017 on Linux](https://docs.microsoft.com/sql/linux/sql-server-linux-whats-new)
-  [SQL Server on Linux Documentation](https://docs.microsoft.com/sql/linux/)


**Try it out:**    
   -   [![Download from Evaluation Center](../analysis-services/media/download.png)](http://go.microsoft.com/fwlink/?LinkID=829477) **[Download the SQL Server 2017 Community Technology Preview](http://go.microsoft.com/fwlink/?LinkID=829477)**

## What's New in SQL Server 2017 CTP 2.1 (May 2017)
### SQL Server Database Engine  
- A new DMF, [sys.dm_db_log_stats](../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md), is introduced to expose summary level attributes and information on transaction log files; useful for monitoring the health of the transaction log.  
- This CTP contains bug fixes and performance improvements for the Database Engine.
- For a detailed list of 2017 CTP enhancements in previous CTP releases, see [What's New in SQL Server 2017 (Database Engine)](../database-engine/configure-windows/what-s-new-in-sql-server-2017-database-engine.md).

### SQL Server Reporting Services (SSRS)
- SQL Server Reporting Services is no longer available to install through SQL Server's setup as of CTP 2.1.
- Comments are now available for reports. Comments allow you to add perspective to what is in a report and collaborate with others in your organization. You can also include attachments with your comment.
- For more detailed SSRS what's new information, including details from previous releases, see [What's new in Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md). 
- For information about Power BI Report Server, see [Get started with Power BI Report Server](https://powerbi.microsoft.com/documentation/reportserver-get-started/).

### SQL Server Machine Learning Services
- There are no new Machine Learning Services features in this CTP.
- For more detailed Machine Learning Services what's new information, including details from previous CTPs, see [What's New in SQL Server Machine Learning Services](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md).  

### SQL Server Analysis Services (SSAS)
- There are no new SSAS features in this CTP.  
- For more details about improvements and bug fixes in this release, see [What's New in SQL Server 2017 Analysis Services](../analysis-services/what-s-new-in-sql-server-analysis-services-2017.md).  

### SQL Server Integration Services (SSIS)
-   You can now use the **Use32BitRuntime** parameter.
-   The performance of logging has been improved.
- For more detailed SSIS what's new information, including details from previous CTPs, see [What's New in SQL Server 2017 Integration Services](../integration-services/what-s-new-in-integration-services-in-sql-server-2017.md).  

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## What's New in SQL Server 2017 CTP 2.0 (April 2017)
### SQL Server Database Engine
- **Resumable online index rebuild**. Resumable online index rebuild allows you to resume an online index rebuild operation from where it stopped after a failure. For example, a failover to a replica or insufficient disk space situation. You can also pause and later resume an online index rebuild operation. For example, you might need to temporarily free up systems resources in order to execute a high priority task or complete the index rebuild in another miniatous window if the available maintenance windows is too short for a large table. Finally, resumable online index rebuild does not require significant log space, which allows you to perform log truncation while the resumable rebuild operation is running. See [ALTER INDEX](../t-sql/statements/alter-index-transact-sql.md) and [Guidelines for online index operations](../relational-databases/indexes/guidelines-for-online-index-operations.md).
- **IDENTITY_CACHE option for ALTER DATABASE SCOPED CONFIGURATION**. A new option IDENTITY_CACHE was added to ALTER DATABASE SCOPED CONFIGURATION T-SQL statement. When this option is set to OFF, gaps can be avoided in the values of identity columns in case a server restarts unexpectedly or fails over to a secondary server. See [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).  
- CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssqlv14-md](../includes/sssqlv14-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but is not recommended. Microsoft recommends all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database. For more information, see [CLR strict security](../database-engine/configure-windows/clr-strict-security.md).  
- Graph database capabilities to model many-to-many relationships. This includes new [CREATE TABLE](../t-sql/statements/create-table-sql-graph.md) syntax for creating node and edge tables, and the keyword [MATCH](../t-sql/queries/match-sql-graph.md) for queries. For more information, see [Graph Processing with SQL Server 2017](../relational-databases/graphs/sql-graph-overview.md).   
- Automatic tuning is a database feature that provides insight into potential query performance problems, it can recommend solutions, and automatically fix identified problems. Automatic tuning in [!INCLUDE[ssnoversion](../includes/ssnoversion.md)], notifies you whenever a potential performance issue is detected, and lets you apply corrective actions, or lets the [!INCLUDE[ssde](../includes/ssde-md.md)] automatically fix performance problems. For more information, see [Automatic tuning](../relational-databases/automatic-tuning/automatic-tuning.md).  
-	Batch Mode Adaptive Join to improve plan quality (under db compatability 140).
-	Interleaved Execution for multi-statement T-SQL TVFs to improve plan quality (under db compatability 140).
- Query Store now also tracks wait stats summary information. Tracking wait stats categories per query in Query Store enables the next level of performance troubleshooting experience, providing, even more, insight into the workload performance and its bottlenecks while preserving the key Query Store advantages.
- DTC support for Always On Availability Groups for all cross database transactions among databases that are part of the availability group, including for databases that are part of same instance. For more information, see [Transactions - Always On Availability Groups and Database Mirroring](../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md)
- A new column **modified_extent_page_count** is introduced in [sys.dm_db_file_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md) to track differential changes in each database file of the database.
- [SELECT INTO](../t-sql/queries/select-into-clause-transact-sql.md) now supports loading a table into a filegroup other than a default filegroup of the user using the **ON** keyword.
- SQL Server setup supports specifying initial tempdb file size up to **256 GB (262,144 MB)** per file with a warning if the file size is set to value greater than **1 GB** and if IFI is not enabled.
- A new Dynamic Management View (DMV) [sys.dm_tran_version_store_space_usage](../relational-databases/system-dynamic-management-views/sys-dm-tran-version-store-space-usage.md) is introduced to track version store usage per database.
- A new DMV [sys.dm_db_log_info](../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md) is introduced to expose the VLF information similar to DBCC LOGINFO.
- System-versioned temporal tables now support CASCADE DELETE and CASCADE UPDATE.
- This CTP contains bug fixes for the Database Engine.
- For a detailed list of 2017 CTP enhancements in previous CTP releases, see [What's New in SQL Server 2017 (Database Engine)](../database-engine/configure-windows/what-s-new-in-sql-server-2017-database-engine.md).   

### SQL Server Machine Learning Services
- SQL Server R Services has a new name, to reflect support for the Python language in CTP 2.0. 
You can now use SQL Server Machine Learning Services (In-Database) to run either R or Python scripts in SQL Server. 
Or, install Microsoft Machine Learning Server (Standalone) to deploy and consume R and Python models that don’t require SQL Server. 
- Both platforms include new MicrosoftML algorithms for distributed machine learning, and the latest version of Microsoft R (version 9.1.0).
- For more information, see [What’s new for Machine Learning](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md).

![horizontal_bar](../sql-server/media/horizontal-bar.png)

## What's New in SQL Server 2017 CTP 1.4 (March 2017)
### SQL Server Database Engine
- There are no new Database Engine features in this CTP.
- This CTP contains bug fixes for the Database Engine.
- For a detailed list of 2017 CTP enhancements in previous CTP releases, see [What's New in SQL Server 2017 (Database Engine)](../database-engine/configure-windows/what-s-new-in-sql-server-2017-database-engine.md).

### SQL Server R Services
- There are no new R Services features in this CTP.
- For more detailed R Services what's new information, including details from previous CTPs, see [What's New in SQL Server R Services](../advanced-analytics/r-services/what-s-new-in-sql-server-r-services.md).  

### SQL Server Reporting Services (SSRS)
- There are no new SSRS features in this CTP.
- For more detailed SSRS what's new information, including details from previous releases, see [What's new in Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md). 

### SQL Server Analysis Services (SSAS)
- There are no new SSAS features in this CTP.  
- For details, including what's new for Analysis Services in the latest preview releases of SSDT and SSMS, see [What's New in Analysis Services 2017](../analysis-services/what-s-new-in-sql-server-analysis-services-2017.md).  

### SQL Server Integration Services (SSIS)
- There are no new SSIS features in this CTP.
- For more detailed SSIS what's new information, including details from previous CTPs, see [What's New in Integration Services 2017](../integration-services/what-s-new-in-integration-services-in-sql-server-2017.md).  

![horizontal_bar](../sql-server/media/horizontal-bar.png)

## What's New in SQL Server 2017 CTP 1.3 (February 2017)
### SQL Server Database Engine
- Indirect checkpoint performance improvements.
- Cluster-less Availability Groups support added.
- Minimum Replica Commit Availability Groups setting added.
- Availability Groups can now work across Windows-Linux to enable cross-OS migrations and testing.
- Temporal Tables Retention Policy support added. For more information, see [Manage Retention of Historical Data in System-Versioned Temporal Tables](../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md#using-temporal-history-retention-policy-approach).
- New DMV SYS.DM_DB_STATS_HISTOGRAM
- Online non-clustered columnstore index build and rebuild support added
- 5 new dynamic management views to return information about Linux process. For more information, see [Linux Process Dynamic Management Views](../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md).   
- [sys.dm_db_stats_histogram (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) is added for examining statistics.

### SQL Server Analysis Services (SSAS) (CTP 1.3)
- Encoding hints - an advanced feature used to optimize processing (data refresh) of large in-memory tabular models. To learn more, see [What's New in Analysis Services 2017](../analysis-services/what-s-new-in-sql-server-analysis-services-2017.md). 


![horizontal_bar](../sql-server/media/horizontal-bar.png)

##  ![info_tip](../sql-server/media/info-tip.png) Engage with the SQL Server engineering team 
- [Stack Overflow (tag sql-server)](http://stackoverflow.com/questions/tagged/sql-server)
- [MSDN Forums](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
- [Microsoft Connect - report bugs and request features](https://connect.microsoft.com/SQLServer/Feedback)
- [Reddit - general discussion about R](https://www.reddit.com/r/SQLServer/)

## See also    
- ![Release Notes](../analysis-services/instances/install-windows/media/ssrs-fyi-note.png) [SQL Server 2017 Release Notes](../sql-server/sql-server-2017-release-notes.md). 
- [Features supported by Edition](https://msdn.microsoft.com/library/cc645993.aspx)
- [Installation hardware and software requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
- [SQL Server Installation Wizard](../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)
- [Install SQL Server Servicing Updates](http://msdn.microsoft.com/library/6df72a78-6b36-4bc1-948e-04b4ebe46094)
 
 ![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)
