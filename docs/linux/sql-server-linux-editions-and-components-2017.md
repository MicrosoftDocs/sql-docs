---
title: "Editions and supported features of SQL Server 2017 ~ Linux | Microsoft Docs"
ms.custom: 
 - "SQL2017_New_Updated"
ms.date: "09/14/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-linux"
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Enterprise Edition [SQL Server]"
  - "Developer Edition [SQL Server]"
  - "default components"
  - "installing SQL Server, components"
  - "Setup [SQL Server], components"
  - "SQL Server, editions"
  - "SQL Server, components"
  - "editions [SQL Server]"
  - "versions [SQL Server]"
  - "Setup [SQL Server], editions"
  - "SQL Server Installation Wizard"
  - "components [SQL Server]"
  - "Standard Edition [SQL Server]"
  - "installing SQL Server, editions"
  - "editions [SQL Server], about edition options"
  - "Setup [SQL Server]"
ms.assetid: 
caps.latest.revision: 121
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Editions and supported features of SQL Server 2017 on Linux

This article provides details of features supported by the various editions of SQL Server 2017 on Linux. For editions and supported features of SQL Server on Windows, see [SQL Server 2017 - Windows](../sql-server/editions-and-components-of-sql-server-2017.md).  
  
Installation requirements vary based on your application needs. The different editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  

For the latest release notes and what's new information, see the following:
- [SQL Server on Linux release notes](sql-server-linux-release-notes.md)
- [What's new in SQL Server on Linux](sql-server-linux-whats-new.md)

For a list of SQL Server features not available on Linux, see [Unsupported features and services](sql-server-linux-release-notes.md#Unsupported).

### Try SQL Server!    
    
[Download SQL Server 2017](http://www.microsoft.com/sql-server/sql-server-2017)

## [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] editions  
 The following table describes the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. 
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|Definition|  
|---------------------------------------|----------------|  
|Enterprise|The premium offering, [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Enterprise edition delivers comprehensive high-end datacenter capabilities with blazing-fast performance enabling high service levels for mission-critical workloads.|  
|Standard|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Standard edition delivers basic data management for departments and small organizations to run their applications and supports common development tools for on-premise and cloud — enabling effective database management with minimal IT resources.|  
|Web|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Web edition is a low total-cost-of-ownership option for Web hosters and Web VAPs to provide scalability, affordability, and manageability capabilities for small to large scale Web properties.|  
|Developer|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Developer edition lets developers build any kind of application on top of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. It includes all the functionality of Enterprise edition, but is licensed for use as a development and test system, not as a production server. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Developer is an ideal choice for people who build and test applications.|  
|Express editions|Express edition is the entry-level, free database and is ideal for learning and building desktop and small server data-driven applications. It is the best choice for independent software vendors, developers, and hobbyists building client applications. If you need more advanced database features, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express can be seamlessly upgraded to other higher end versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express LocalDB, a lightweight version of Express that has all of its programmability features, yet runs in user mode and has a fast, zero-configuration installation and a short list of prerequisites.|  
  
  
## Using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with client/server applications  

You can install just the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] client components on a computer that is running client/server applications that connect directly to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] applications.  
  
## Deciding among [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components  
Use the Feature Selection page of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Installation Wizard to select the components to include in an installation of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. By default, none of the features in the tree are selected.  
  
 Use the information in the following tables to determine the set of features that best fits your needs.  
  
|Server components|Description|  
|-----------------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|[!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE[ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data, in database analytics integration, and Polybase integration for access to Hadoop and other heterogeneous data sources, and the [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) server.|  

**Developer and Evaluation Editions**  
For features supported by Developer and Evaluation editions, see features listed for the SQL Server Enterprise Edition in the tables below.

The Developer edition continues to support only 1 client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md). 
  
##  <a name="Cross-BoxScaleLimits"></a> Scale limits  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express| 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|
|Maximum compute capacity used by a single instance - [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]<sup>1</sup>|Operating system maximum|Limited to lesser of 4 sockets or 24 cores|Limited to lesser of 4 sockets or 16 cores|Limited to lesser of 1 socket or 4 cores|Limited to lesser of 1 socket or 4 cores| 
|Maximum compute capacity used by a single instance - [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] or [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]|Operating system maximum|Limited to lesser of 4 sockets or 24 cores|Limited to lesser of 4 sockets or 16 cores|Limited to lesser of 1 socket or 4 cores|Limited to lesser of 1 socket or 4 cores|  
|Maximum memory for buffer pool per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Operating System Maximum|128 GB|64 GB|1410 MB|1410 MB|
|Maximum memory for Columnstore segment cache per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Unlimited memory| 32 GB| 16 GB| 352 MB| 352 MB|  
|Maximum memory-optimized data size per database in [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Unlimited memory| 32 GB| 16 GB| 352 MB| 352 MB|  
|Maximum memory utilized per instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]|Operating System Maximum|Tabular: 16 GB<br /><br /> MOLAP: 64 GB|N/A|N/A|N/A|  
|Maximum memory utilized per instance of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]|Operating System Maximum|64 GB|64 GB|4 GB|N/A|
|Maximum relational database size|524 PB|524 PB|524 PB|10 GB|10 GB|  
  
<sup>1</sup> Enterprise Edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute Capacity Limits by Edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).  
 
##  <a name="RDBMSHA"></a> RDBMS high availability  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Server core support <sup>1</sup>|Yes|Yes|Yes|Yes|Yes|  
|Log shipping|Yes|Yes|Yes|No|No|  
|Database mirroring|Yes|Yes<br /><br /> Full safety only|Witness only|Witness only|Witness only| 
|Backup compression|Yes|Yes|No|No|No| 
|Database snapshot|Yes|Yes|Yes|Yes|Yes|
|Always On failover cluster instances|Yes<br /><br /> Number of nodes is the operating system maximum|Yes<br /><br /> Support for 2 nodes|No|No|No|  
|Always On availability groups|Yes<br /><br /> Up to 8 secondary replicas, including 2 synchronous secondary replicas|No|No|No|No|
|Basic availability groups <sup>2</sup>|No|Yes<br /><br /> Support for 2 nodes|No|No|No|
|Online page and file restore|Yes|No|No|No|No|
|Online indexing|Yes|No|No|No|No|
|Resumable online index rebuilds|Yes|No|No|No|No|
|Online schema change|Yes|No|No|No|No|
|Fast recovery|Yes|No|No|No|No|
|Mirrored backups|Yes|No|No|No|No|
|Hot add memory and CPU|Yes|No|No|No|No|
|Database recovery advisor|Yes|Yes|Yes|Yes|Yes|
|Encrypted backup|Yes|Yes|No|No|No|
|Hybrid backup to Windows Azure (backup to URL)|Yes|Yes|No|No|No|
|Clusterless availability group|Yes|Yes|Yes|No|No|No|
|Minimum replica commit availability group|Yes|Yes|Yes|No|No|No|
  
 <sup>1</sup> For more information on installing SQL Server on Server Core,  see [Install SQL Server on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md). 

<sup>2</sup> For more information about Basic availability groups, see [Basic Availability Groups](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).  

##  <a name="RDBMSSP"></a> RDBMS scalability and performance  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------| 
|Columnstore <sup>1</sup>|Yes|Yes|Yes|Yes|Yes|  
|Large object binaries in clustered columnstore indexes|Yes|Yes|Yes|Yes|Yes|  
|Online non-clustered columnstore index rebuild|Yes|No|No|No|No|
|In-Memory OLTP <sup>1</sup>|Yes|Yes|Yes|Yes|Yes|
|Stretch Database|Yes|Yes|Yes|Yes|Yes|
|Persistent Main Memory|Yes|Yes|Yes|Yes|Yes|
|Multi-instance support|50|50|50|50|50|
|Table and index partitioning|Yes|Yes|Yes|Yes|Yes|  
|Data compression|Yes|Yes|Yes|Yes|Yes|
|Resource Governor|Yes|No|No|No|No|  
|Partitioned Table Parallelism|Yes|No|No|No|No|
|Multiple Filestream containers|Yes|Yes|Yes|Yes|Yes|
|NUMA Aware and Large Page Memory and Buffer Array Allocation|Yes|No|No|No|No|
|Buffer Pool Extension|Yes|Yes|No|No|No|
|IO Resource Governance|Yes|No|No|No|No|  
|Delayed Durability|Yes|Yes|Yes|Yes|Yes|
|Automatic Tuning|Yes|No|No|No|No|
|Batch Mode Adaptive Joins|Yes|No|No|No|No|
|Batch Mode Memory Grant Feedback|Yes|No|No|No|No|
|Interleaved Execution for Multi-Statement Table Valued Functions|Yes|Yes|Yes|Yes|Yes|
|Bulk insert improvements|Yes|Yes|Yes|Yes|Yes|


<sup>1</sup> In-Memory OLTP data size and Columnstore segment cache are limited to the amount of memory specified by edition in the Scale Limits section. The max degrees of parallelism is limited. The degrees of process parallelism (DOP) for an index build is limited to 2 DOP for the Standard Edition and 1 DOP for the Web and Express Editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

##  <a name="RDBMSS"></a> RDBMS security  
  
|Feature|Enterprise|Standard|Web|Express|Express with Advanced Services|  
|-------------|----------------|--------------|---------|-------------|------------------------------------| 
|Row-level security|Yes|Yes|Yes|Yes|Yes|  
|Always Encrypted|Yes|Yes|Yes|Yes|Yes| 
|Dynamic data masking|Yes|Yes|Yes|Yes|Yes|   
|Basic auditing|Yes|Yes|Yes|Yes|Yes| 
|Fine grained auditing|Yes|Yes|Yes|Yes|Yes| 
|Transparent database encryption|Yes|No|No|No|No|   
|User-defined roles|Yes|Yes|Yes|Yes|Yes| 
|Contained databases|Yes|Yes|Yes|Yes|Yes| 
|Encryption for backups|Yes|Yes|No|No|No|  

##  <a name="Replication"></a> Replication  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|-------------|----------------|--------------|---------|------------------------------------|------------------------| 
|Merge replication|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|   
|Peer to peer transactional replication|Yes|No|No|No|No|   
|Snapshot replication|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|   
|SQL Server change tracking|Yes|Yes|Yes|Yes|Yes| 
|Transactional replication|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|   
|Transactional replication to Azure|Yes|Yes|No|No|No|   
|Transactional replication updateable subscription|Yes|No|No|No|No|  
  
##  <a name="SSMS"></a> Management tools  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express| 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|SQL Management Objects (SMO)|Yes|Yes|Yes|Yes|Yes|  
|SQL CMD (Command Prompt tool)|Yes|Yes|Yes|Yes|Yes|      
|Distributed Replay - Admin Tool|Yes|Yes|Yes|Yes|No|  
|Distribute Replay - Client|Yes|Yes|Yes|No|No|  
|Distributed Replay - Controller|Yes (Up to 16 clients)|Yes (1 client)|Yes (1 client)|No|No|   
|SQL Profiler|Yes|Yes|No <sup>1</sup>|No <sup>1</sup>|No <sup>1</sup>|  
|SQL Server Agent|Yes|Yes|Yes|No|No| 
  
 <sup>1</sup> SQL Server Web, SQL Server Express, SQL Server Express with Tools, and SQL Server Express with Advanced Services can be profiled using SQL Server Standard and SQL Server Enterprise editions.  
  
 <sup>2</sup> Tuning enabled only on Standard edition features  
  
##  <a name="RDBMSM"></a> RDBMS manageability  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|User instances|No|No|No|Yes|Yes| 
|Dedicated admin connection|Yes|Yes|Yes|Yes with trace flag|Yes with trace flag|   
|PowerShell scripting support|Yes|Yes|Yes|Yes|Yes| 
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

##  <a name="Programmability"></a> Programmability  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Basic R integration|Yes|Yes|Yes|Yes|No|   
|Advanced R integration|Yes|No|No|No|No| 
|Basic Python integration|Yes|Yes|Yes|Yes|No|
|Advanced Python integration|Yes|No|No|No|No| 
|Machine Learning Server (Standalone)|Yes|No|No|No|No|   
|Polybase compute node|Yes|Yes <sup>1</sup>|Yes <sup>1</sup>, <sup>2</sup>|Yes <sup>1</sup>,|Yes <sup>1</sup>, | 
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
|Graph|Yes|Yes|Yes|Yes|Yes|  


<sup>1</sup> Scale out with multiple compute nodes requires a head node.

## <a name="IS"></a> Integration Services

For info about the Integration Services (SSIS) features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services Features Supported by the Editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

##  <a name="SLS"></a> Spatial and location services  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|------------------|----------------|--------------|---------|------------------------------------|------------------------|
|Spatial indexes|Yes|Yes|Yes|Yes|Yes|   
|Planar and geodetic datatypes|Yes|Yes|Yes|Yes|Yes| 
|Advanced spatial libraries|Yes|Yes|Yes|Yes|Yes|   
|Import/export of industry-standard spatial data formats|Yes|Yes|Yes|Yes|Yes|   
  
##  <a name="ADS"></a> Additional database services  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|------------------|----------------|--------------|---------|------------------------------------|------------------------| 
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Migration Assistant|Yes|Yes|Yes|Yes|Yes|   
|Database mail|Yes|Yes|Yes|No|No| 
  
##  <a name="Other"></a> Other components  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express|   
|------------------|----------------|--------------|---------|------------------------------------|------------------------|  
|StreamInsight|StreamInsight Premium Edition|StreamInsight Standard Edition|StreamInsight Standard Edition|No|No| 
|StreamInsight HA|StreamInsight Premium Edition|No|No|No|No|   
  
> [![Download SSMS](../analysis-services/media/download.png)](https://msdn.microsoft.com/library/mt238290.aspx) **[Download the latest version of SQL Server Management Studio](https://msdn.microsoft.com/library/mt238290.aspx)**    
  
## Next steps 
 [SQL Server 2017 - Windows](../sql-server/editions-and-components-of-sql-server-2017.md).  
 [Editions and supported features for SQL Server 2016 - Windows](../sql-server/editions-and-components-of-sql-server-2016.md).  
 [SQL Server 2014 - Windows](http://msdn.microsoft.com/library/cc645993(v=sql.120).aspx).
 [Product Specifications for SQL Server](http://msdn.microsoft.com/library/6445fd53-6844-4170-a86b-7fe76a9f64cb)   
 [Installation for SQL Server](../database-engine/install-windows/installation-for-sql-server-2016.md)  
 
  
  
