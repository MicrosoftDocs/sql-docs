---
title: "Editions and Supported Features for SQL Server 2016 | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "sql edition"
ms.assetid: 5da61ff5-12b9-48e6-b3c8-0dacca1751c4
caps.latest.revision: 175
author: "sabotta"
ms.author: "carlasab"
manager: "jhubbard"
---
# Editions and Supported Features for SQL Server 2016
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

This topic provides details of features supported by the different editions of SQL Server.  At this time there are no changes to features supported by editions for SQL Server 2017.
  
 The SQL Server Evaluation edition is available for a 180-day trial period.  
  
 For the latest release notes and what's new information, see the following:
- [SQL Server 2016 release notes](../sql-server/sql-server-2016-release-notes.md)
- [What's new in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)

    
 **Try SQL Server 2016!**    
    
 > [![Download from Evaluation Center](../analysis-services/media/download.png)](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016) **[Download SQL Server 2016  from the Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016)**    
    
> ![Azure Virtual Machine small](../analysis-services/media/azure-virtual-machine-small.png) **[Spin up a Virtual Machine with SQL Server 2016 already installed](https://azuremarketplace.microsoft.com/en-us/marketplace/apps/Microsoft.SQL2016SP1-WS2016?tab=Overview?wt.mc_id=sqL16_vm)**    
    
**Developer and Evaluation Editions**  
 For features supported by Developer and Evaluation editions, see features listed for the SQL Server Enterprise Edition in the tables below.
 
For a list of features that were added to the Developer edition for [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] SP1, see [SQL Server 2016 SP1 editions](https://aka.ms/uw6cw4).   

The Developer edition continues to support only 1 client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md). 
  
##  <a name="Cross-BoxScaleLimits"></a> Scale Limits  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express| 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|
|Maximum compute capacity used by a single instance - [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]<sup>1</sup>|Operating system maximum|Limited to lesser of 4 sockets or 24 cores|Limited to lesser of 4 sockets or 16 cores|Limited to lesser of 1 socket or 4 cores|Limited to lesser of 1 socket or 4 cores| 
|Maximum compute capacity used by a single instance - [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] or [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]|Operating system maximum|Limited to lesser of 4 sockets or 24 cores|Limited to lesser of 4 sockets or 16 cores|Limited to lesser of 1 socket or 4 cores|Limited to lesser of 1 socket or 4 cores|  
|Maximum memory for buffer pool per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Operating System Maximum|128 GB|64 GB|1410 MB|1410 MB|
|Maximum memory for Columnstore segment cache per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Unlimited memory| 32 GB<sup>2</sup>| 16 GB<sup>2</sup>| 352 MB<sup>2</sup>| 352 MB<sup>2</sup>|  
|Maximum memory-optimized data size per database in [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Unlimited memory| 32 GB<sup>2</sup>| 16 GB<sup>2</sup>| 352 MB<sup>2</sup>| 352 MB<sup>2</sup>|  
|Maximum memory utilized per instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]|Operating System Maximum|Tabular: 16 GB<br /><br /> MOLAP: 64 GB|N/A|N/A|N/A|  
|Maximum memory utilized per instance of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]|Operating System Maximum|64 GB|64 GB|4 GB|N/A|
|Maximum relational database size|524 PB|524 PB|524 PB|10 GB|10 GB|  
  
<sup>1</sup> Enterprise Edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute Capacity Limits by Edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).  
  
<sup>2</sup> Applies to [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] SP1. 

##  <a name="RDBMSHA"></a> RDBMS High Availability  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Server core support <sup>1</sup>|Yes|Yes|Yes|Yes|Yes|  
|Log shipping|Yes|Yes|Yes|No|No|  
|Database mirroring|Yes|Yes<br /><br /> Full safety only|Witness only|Witness only|Witness only| 
|Backup compression|Yes|Yes|No|No|No| 
|Database snapshot|Yes|Yes <sup>3</sup>|Yes <sup>3</sup>|Yes <sup>3</sup>|Yes <sup>3</sup>|
|Always On failover cluster instances|Yes<br /><br /> Number of nodes is the operating system maximum|Yes<br /><br /> Support for 2 nodes|No|No|No|  
|Always On availability groups|Yes<br /><br /> Up to 8 secondary replicas, including 2 synchronous secondary replicas|No|No|No|No|
|Basic availability groups <sup>2</sup>|No|Yes<br /><br /> Support for 2 nodes|No|No|No|
|Online page and file restore|Yes|No|No|No|No|
|Online indexing|Yes|No|No|No|No|
|Online schema change|Yes|No|No|No|No|
|Fast recovery|Yes|No|No|No|No|
|Mirrored backups|Yes|No|No|No|No|
|Hot add memory and CPU|Yes|No|No|No|No|
|Database recovery advisor|Yes|Yes|Yes|Yes|Yes|
|Encrypted backup|Yes|Yes|No|No|No|
|Hybrid backup to Windows Azure (backup to URL)|Yes|Yes|No|No|No|  
  
 <sup>1</sup> For more information on installing SQL Server 2016 on Server Core,  see [Install SQL Server 2016 on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md). 

<sup>2</sup> For more information about Basic availability groups, see [Basic Availability Groups](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).  

<sup>3</sup> Applies to [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)] 2016 SP1.
  
##  <a name="RDBMSSP"></a> RDBMS Scalability and Performance  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------| 
|Columnstore <sup>1</sup>|Yes|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes<sup>2</sup>|Yes<sup>2</sup>|  
|In-Memory OLTP <sup>1</sup>|Yes|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes <sup>2</sup>, <sup>3</sup>|Yes <sup>2</sup>|
|Stretch Database|Yes|Yes|Yes|Yes|Yes|
|Persistent Main Memory|Yes|Yes|Yes|Yes|Yes|
|Multi-instance support|50|50|50|50|50|
|Table and index partitioning|Yes|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes <sup>2</sup>|  
|Data compression|Yes|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes <sup>2</sup>|
|Resource Governor|Yes|No|No|No|No|  
|Partitioned Table Parallelism|Yes|No|No|No|No|
|Multiple Filestream containers|Yes|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes <sup>2</sup>|Yes <sup>2</sup>|
|NUMA Aware and Large Page Memory and Buffer Array Allocation|Yes|No|No|No|No|
|Buffer Pool Extension|Yes|Yes|No|No|No|
|IO Resource Governance|Yes|No|No|No|No|  
|Delayed Durability|Yes|Yes|Yes|Yes|Yes|

<sup>1</sup> In-Memory OLTP data size and Columnstore segment cache are limited to the amount of memory specified by edition in the Scale Limits section. The max degrees of parallelism is limited. The degrees of process parallelism (DOP) for an index build is limited to 2 DOP for the Standard Edition and 1 DOP for the Web and Express Editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

<sup>2</sup> Applies to [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] SP1. 

<sup>3</sup> This feature is not included in the LocalDB installation option.
##  <a name="RDBMSS"></a> RDBMS Security  
  
|Feature|Enterprise|Standard|Web|Express|Express with Advanced Services|  
|-------------|----------------|--------------|---------|-------------|------------------------------------| 
|Row-level security|Yes|Yes|Yes <sup>1</sup>|Yes <sup>1</sup>|Yes <sup>1</sup>|  
|Always Encrypted|Yes|Yes <sup>1</sup>|Yes <sup>1</sup>|Yes <sup>1</sup>|Yes <sup>1</sup>| 
|Dynamic data masking|Yes|Yes|Yes <sup>1</sup>|Yes <sup>1</sup>|Yes <sup>1</sup>|   
|Basic auditing|Yes|Yes|Yes|Yes|Yes| 
|Fine grained auditing|Yes|Yes <sup>1</sup>|Yes <sup>1</sup>|Yes <sup>1</sup>|Yes <sup>1</sup>| 
|Transparent database encryption|Yes|No|No|No|No|   
|Extensible key management|Yes|No|No|No|No| 
|User-defined roles|Yes|Yes|Yes|Yes|Yes| 
|Contained databases|Yes|Yes|Yes|Yes|Yes| 
|Encryption for backups|Yes|Yes|No|No|No|  

<sup>1</sup> Applies to [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)] 2016 SP1.  
##  <a name="Replication"></a> Replication  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|-------------|----------------|--------------|---------|------------------------------------|------------------------| 
|Heterogeneous subscribers|Yes|Yes|No|No|No|  
|Merge replication|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|   
|Oracle publishing|Yes|No|No|No|No| 
|Peer to peer transactional replication|Yes|No|No|No|No|   
|Snapshot replication|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|   
|SQL Server change tracking|Yes|Yes|Yes|Yes|Yes| 
|Transactional replication|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|   
|Transactional replication to Azure|Yes|Yes|Yes|No|No|   
|Transactional replication updateable subscription|Yes|No|No|No|No|  
  
##  <a name="SSMS"></a> Management Tools  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express| 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|SQL Management Objects (SMO)|Yes|Yes|Yes|Yes|Yes|  
|SQL Configuration Manager|Yes|Yes|Yes|Yes|Yes|   
|SQL CMD (Command Prompt tool)|Yes|Yes|Yes|Yes|Yes|      
|Distributed Replay - Admin Tool|Yes|Yes|Yes|Yes|No|  
|Distribute Replay - Client|Yes|Yes|Yes|No|No|  
|Distributed Replay - Controller|Yes (Up to 16 clients)|Yes (1 client)|Yes (1 client)|No|No|   
|SQL Profiler|Yes|Yes|No <sup>1</sup>|No <sup>1</sup>|No <sup>1</sup>|  
|SQL Server Agent|Yes|Yes|Yes|No|No| 
|Microsoft System Center Operations Manager Management Pack|Yes|Yes|Yes|No|No|  
|Database Tuning Advisor (DTA)|Yes|Yes <sup>2</sup>|Yes <sup>2</sup>|No|No|      
  
 <sup>1</sup> SQL Server Web, SQL Server Express, SQL Server Express with Tools, and SQL Server Express with Advanced Services can be profiled using SQL Server Standard and SQL Server Enterprise editions.  
  
 <sup>2</sup> Tuning enabled only on Standard edition features  
  
##  <a name="RDBMSM"></a> RDBMS Manageability  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|User instances|No|No|No|Yes|Yes| 
|LocalDB|No|No|No|Yes|No| 
|Dedicated admin connection|Yes|Yes|Yes|Yes with trace flag|Yes with trace flag|   
|PowerShell scripting support|Yes|Yes|Yes|Yes|Yes| 
|SysPrep support <sup>1</sup>|Yes|Yes|Yes|Yes|Yes| 
|Support for data-tier application component operations - extract, deploy, upgrade, delete|Yes|Yes|Yes|Yes|Yes| 
|Policy automation (check on schedule and change)|Yes|Yes|Yes|No|No|   
|Performance data collector|Yes|Yes|Yes|No|No| 
|Able to enroll as a managed instance in multi-instance management|Yes|Yes|Yes|No|No|   
|Standard performance reports|Yes|Yes|Yes|No|No| 
|Plan guides and plan freezing for plan guides|Yes|Yes|Yes|No|No|   
|Direct query of indexed views (using NOEXPAND hint)|Yes|Yes|Yes|Yes|Yes| 
|Automatic indexed views maintenance|Yes|Yes|Yes|No|No| 
|Distributed partitioned views|Yes|No|No|No|No| 
|Parallel indexed operations|Yes|No|No|No|No|  
|Automatic use of indexed view by query optimizer|Yes|No|No|No|No| 
|Parallel consistency check|Yes|No|No|No|No| 
|SQL Server Utility Control Point|Yes|No|No|No|No|    
|Buffer pool extension|Yes|Yes|No|No|No| 
  
 <sup>1</sup> For more information, see [Considerations for Installing SQL Server Using SysPrep](../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md).  
 
<sup>2</sup> Applies to [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)] 2016 SP1. 
  
##  <a name="DevTools"></a> Development Tools  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express| 
|-------------|----------------|--------------|---------|------------------------------------|------------------------| 
|Microsoft Visual Studio integration|Yes|Yes|Yes|Yes|Yes| 
|Intellisense (Transact-SQL and MDX)|Yes|Yes|Yes|Yes|Yes| 
|SQL Server Data Tools (SSDT)|Yes|Yes|Yes|Yes|No|    
|MDX edit, debug, and design tools|Yes|Yes|No|No|No|   
  
##  <a name="Programmability"></a> Programmability  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Basic R integration|Yes|Yes|Yes|Yes|No|   
|Advanced R integration|Yes|No|No|No|No| 
|R Server (Standalone)|Yes|No|No|No|No|   
|Polybase compute node|Yes|Yes <sup>1</sup>|Yes <sup>1</sup>, <sup>2</sup>|Yes <sup>1</sup>, <sup>2</sup>|Yes <sup>1</sup>, <sup>2</sup>| 
|Polybase head node|Yes|No|No|No|No| 
|JSON|Yes|Yes|Yes|Yes|Yes|   
|Query Store|Yes|Yes|Yes|Yes|Yes|   
|Temporal|Yes|Yes|Yes|Yes|Yes|   
|Common Language Runtime (CLR) Integration|Yes|Yes|Yes|Yes|Yes|   
|Native XML support|Yes|Yes|Yes|Yes|Yes| 
|XML indexing|Yes|Yes|Yes|Yes|Yes| 
|MERGE & UPSERT capabilities|Yes|Yes|Yes|Yes|Yes|   
|FILESTREAM support|Yes|Yes|Yes|Yes|Yes| 
|FileTable|Yes|Yes|Yes|Yes|Yes| 
|Date and Time datatypes|Yes|Yes|Yes|Yes|Yes|  
|Internationalization support|Yes|Yes|Yes|Yes|Yes| 
|Full-text and semantic search|Yes|Yes|Yes|Yes|No| 
|Specification of language in query|Yes|Yes|Yes|Yes|No|   
|Service Broker (messaging)|Yes|Yes|No (Client only)|No (Client only)|No (Client only)|   
|Transact-SQL endpoints|Yes|Yes|Yes|No|No| 

<sup>1</sup> Scale out with multiple compute nodes requires a head node.

<sup>2</sup> Applies to [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)] 2016 SP1.
  
## <a name="IS"></a> Integration Services

For info about the Integration Services (SSIS) features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services Features Supported by the Editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

##  <a name="MDS"></a> Master Data Services  
 For information about the [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] and Data Quality Services features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Master Data Services and Data Quality Services Features Supported by the Editions of SQL Server 2016](../master-data-services/master-data-services-and-data-quality-services-features-support.md). 

  
##  <a name="DW"></a> Data Warehouse  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|-------------|----------------|--------------|---------|------------------------------------|------------------------| 
|Create cubes without a database|Yes|Yes|No|No|No |   
|Auto-generate staging and data warehouse schema|Yes|Yes|No|No|No| 
|Change data capture|Yes|Yes <sup>1</sup>|Yes <sup>1</sup>|No|No| 
|Star join query optimizations|Yes|No|No|No|No| 
|Scalable read-only Analysis Services configuration|Yes|No|No|No|No| 
|Parallel query processing on partitioned tables and indexes|Yes|No|No|No|No|   
|Global batch aggregation|Yes|No|No|No|No| 

<sup>1</sup> Applies to [!INCLUDE[ssSQL15_md](../includes/sssql15-md.md)] SP1.  
##  <a name="SSAS"></a> Analysis Services  
  
For information about the Analysis Services features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server 2016](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md). 
  
##  <a name="BIMD"></a> BI Semantic Model (Multi Dimensional)  
  
For information about the Analysis Services features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server 2016](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md).
   
##  <a name="BIT"></a> BI Semantic Model (Tabular)  
  
For information about the Analysis Services features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server 2016](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md).
  
##  <a name="PPSP"></a> Power Pivot for SharePoint  
  
For information about the Power Pivot for SharePoint features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server 2016](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md).
  
##  <a name="DM"></a> Data Mining  
  
For information about the Data Mining features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server 2016](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md).
  
##  <a name="SSRS"></a> Reporting Services  
  
For information about the Reporting Services features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Reporting Services Features Supported by the Editions of SQL Server 2016](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).

##  <a name="BIC"></a> Business Intelligence Clients  

For information about the Business Intelligence Client features supported by the editions of [!INCLUDE[ssCurrent_md](../includes/sscurrent-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server 2016](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md) or [Reporting Services Features Supported by the Editions of SQL Server 2016](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).
  
##  <a name="SLS"></a> Spatial and Location Services  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|------------------|----------------|--------------|---------|------------------------------------|------------------------|
|Spatial indexes|Yes|Yes|Yes|Yes|Yes|   
|Planar and geodetic datatypes|Yes|Yes|Yes|Yes|Yes| 
|Advanced spatial libraries|Yes|Yes|Yes|Yes|Yes|   
|Import/export of industry-standard spatial data formats|Yes|Yes|Yes|Yes|Yes|   
  
##  <a name="ADS"></a> Additional Database Services  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|------------------|----------------|--------------|---------|------------------------------------|------------------------| 
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Migration Assistant|Yes|Yes|Yes|Yes|Yes|   
|Database mail|Yes|Yes|Yes|No|No| 
  
##  <a name="Other"></a> Other Components  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|------------------|----------------|--------------|---------|------------------------------------|------------------------|  
|StreamInsight|StreamInsight Premium Edition|StreamInsight Standard Edition|StreamInsight Standard Edition|No|No| 
|StreamInsight HA|StreamInsight Premium Edition|No|No|No|No|   
  
> [![Download SSMS](../analysis-services/media/download.png)](https://msdn.microsoft.com/library/mt238290.aspx) **[Download the latest version of SQL Server Management Studio](https://msdn.microsoft.com/library/mt238290.aspx)**    
  
## See Also  
 [Product Specifications for SQL Server 2016](http://msdn.microsoft.com/library/6445fd53-6844-4170-a86b-7fe76a9f64cb)   
 [Installation for SQL Server 2016](../database-engine/install-windows/installation-for-sql-server-2016.md)  
  
  
