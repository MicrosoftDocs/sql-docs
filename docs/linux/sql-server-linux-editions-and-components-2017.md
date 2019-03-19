---
title: "Editions and supported features of SQL Server 2017 ~ Linux | Microsoft Docs"
ms.custom: "sql-linux"
ms.date: "09/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: linux
ms.topic: conceptual
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
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Editions and supported features of SQL Server 2017 on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article provides details of features supported by the various editions of SQL Server 2017 on Linux. For editions and supported features of SQL Server on Windows, see [SQL Server 2017 - Windows](../sql-server/editions-and-components-of-sql-server-2017.md).  
  
Installation requirements vary based on your application needs. The different editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  

For the latest release notes and what's new information, see the following:
- [SQL Server on Linux release notes](sql-server-linux-release-notes.md)
- [What's new in SQL Server on Linux](sql-server-linux-whats-new.md)

For a list of SQL Server features not available on Linux, see [Unsupported features and services](sql-server-linux-release-notes.md#Unsupported).

### Try SQL Server!    
    
[Download SQL Server 2017](https://www.microsoft.com/sql-server/sql-server-2017)

## [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] editions  
 The following table describes the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. 
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|Definition|  
|---------------------------------------|----------------|  
|Enterprise|The premium offering, [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Enterprise edition delivers comprehensive high-end datacenter capabilities with blazing-fast performance enabling high service levels for mission-critical workloads.|  
|Standard|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Standard edition delivers basic data management for departments and small organizations to run their applications and supports common development tools for on-premise and cloud - enabling effective database management with minimal IT resources.|  
|Web|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Web edition is a low total-cost-of-ownership option for Web hosters and Web VAPs to provide scalability, affordability, and manageability capabilities for small to large scale Web properties.|  
|Developer|[!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Developer edition lets developers build any kind of application on top of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. It includes all the functionality of Enterprise edition, but is licensed for use as a development and test system, not as a production server. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Developer is an ideal choice for people who build and test applications.|  
|Express edition|Express edition is the entry-level, free database and is ideal for learning and building desktop and small server data-driven applications. It is the best choice for independent software vendors, developers, and hobbyists building client applications. If you need more advanced database features, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express can be seamlessly upgraded to other higher end versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].|  
  
## Using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with client/server applications  

You can install just the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] client components on a computer that is running client/server applications that connect directly to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] applications.  
  
## [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components  

SQL Server 2017 on Linux supports the SQL Server database engine. The following table describes the features in the database engine.   
  
|Server components|Description|  
|-----------------------|-----------------|  
|[!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|[!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE[ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data, and in database analytics integration.|  

**Developer, Enterprise Core, and  Evaluation editions**  
For features supported by Developer, Enterprise Core, and Evaluation editions, see features listed for the SQL Server Enterprise edition in the following tables.

The Developer edition continues to support only 1 client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md). 
  
##  <a name="Cross-BoxScaleLimits"></a> Scale limits  
  
|Feature|Enterprise|Standard|Web|Express| 
|-------------|----------------|--------------|---------|------------------------|
|Maximum compute capacity used by a single instance - [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]<sup>1</sup>|Operating system maximum|Limited to lesser of 4 sockets or 24 cores|Limited to lesser of 4 sockets or 16 cores|Limited to lesser of 1 socket or 4 cores| 
|Maximum compute capacity used by a single instance - [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] or [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]|Operating system maximum|Limited to lesser of 4 sockets or 24 cores|Limited to lesser of 4 sockets or 16 cores|Limited to lesser of 1 socket or 4 cores|
|Maximum memory for buffer pool per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Operating System Maximum|128 GB|64 GB|1410 MB|
|Maximum memory for Columnstore segment cache per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Unlimited memory| 32 GB| 16 GB| 352 MB|  
|Maximum memory-optimized data size per database in [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]|Unlimited memory| 32 GB| 16 GB| 352 MB|
|Maximum relational database size|524 PB|524 PB|524 PB|10 GB|  
  
<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).  
 
##  <a name="RDBMSHA"></a> RDBMS high availability  
  
|Feature|Enterprise|Standard|Web|Express|  
|-------------|----------------|--------------|---------|------------------------|  
|Log shipping|Yes|Yes|Yes|No|  
|Backup compression|Yes|Yes|No|No| 
|Database snapshot|Yes|No|No|No|
|Always On failover cluster instance<sup>1</sup>|Yes|Yes|No|No| 
|Always On availability groups<sup>2</sup>|Yes|No|No|No|
|Basic availability groups <sup>3</sup>|No|Yes|No|No|
|Minimum replica commit availability group|Yes|Yes|No|No|
|Clusterless availability group|Yes|Yes|No|No|
|Online page and file restore|Yes|No|No|No|
|Online indexing|Yes|No|No|No|
|Resumable online index rebuilds|Yes|No|No|No|
|Online schema change|Yes|No|No|No|
|Fast recovery|Yes|No|No|No|
|Mirrored backups|Yes|No|No|No|
|Hot add memory and CPU|Yes|No|No|No|
|Encrypted backup|Yes|Yes|No|No|
|Hybrid backup to Windows Azure (backup to URL)|Yes|Yes|No|No|
  
<sup>1</sup> On Enterprise edition, the number of nodes is the operating system maximum. On Standard edition there is support for two nodes. 

<sup>2</sup> On Enterprise edition, provides support for up to 8 secondary replicas - including 2 synchronous secondary replicas. 

<sup>3</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic Availability Groups](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).    

##  <a name="RDBMSSP"></a> RDBMS scalability and performance  
  
|Feature|Enterprise|Standard|Web|Express|  
|-------------|----------------|--------------|---------|------------------------| 
|Columnstore <sup>1</sup>|Yes|Yes|Yes|Yes|  
|Large object binaries in clustered columnstore indexes|Yes|Yes|Yes|Yes|  
|Online non-clustered columnstore index rebuild|Yes|No|No|No|
|In-Memory OLTP <sup>1</sup>|Yes|Yes|Yes|Yes|
|Persistent Main Memory|Yes|Yes|Yes|Yes|
|Table and index partitioning|Yes|Yes|Yes|Yes|  
|Data compression|Yes|Yes|Yes|Yes|
|Resource Governor|Yes|No|No|No|  
|Partitioned Table Parallelism|Yes|No|No|No|
|NUMA Aware and Large Page Memory and Buffer Array Allocation|Yes|No|No|No|
|IO Resource Governance|Yes|No|No|No|  
|Delayed Durability|Yes|Yes|Yes|Yes|
|Automatic Tuning|Yes|No|No|No|
|Batch Mode Adaptive Joins|Yes|No|No|No|
|Batch Mode Memory Grant Feedback|Yes|No|No|No|
|Interleaved Execution for Multi-Statement Table Valued Functions|Yes|Yes|Yes|Yes|
|Bulk insert improvements|Yes|Yes|Yes|Yes|


<sup>1</sup> In-Memory OLTP data size and Columnstore segment cache are limited to the amount of memory specified by edition in the Scale Limits section. The max degrees of parallelism is limited. The degrees of process parallelism (DOP) for an index build is limited to 2 DOP for the Standard edition and 1 DOP for the Web and Express editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

##  <a name="RDBMSS"></a> RDBMS security  
  
|Feature|Enterprise|Standard|Web|Express|
|-------------|----------------|--------------|---------|------------------------------------| 
|Row-level security|Yes|Yes|Yes|Yes|  
|Always Encrypted|Yes|Yes|Yes|Yes| 
|Dynamic data masking|Yes|Yes|Yes|Yes|   
|Basic auditing|Yes|Yes|Yes|Yes| 
|Fine grained auditing|Yes|Yes|Yes|Yes| 
|Transparent database encryption|Yes|No|No|No|   
|User-defined roles|Yes|Yes|Yes|Yes| 
|Contained databases|Yes|Yes|Yes|Yes| 
|Encryption for backups|Yes|Yes|No|No|  

##  <a name="RDBMSM"></a> RDBMS manageability  
  
|Feature|Enterprise|Standard|Web|Express|   
|-------------|----------------|--------------|---------|------------------------|  
|Dedicated admin connection|Yes|Yes|Yes|Yes with trace flag|Yes with trace flag|   
|PowerShell scripting support|Yes|Yes|Yes|Yes| 
|Support for data-tier application component operations - extract, deploy, upgrade, delete|Yes|Yes|Yes|Yes| 
|Policy automation (check on schedule and change)|Yes|Yes|Yes|No|No|   
|Performance data collector|Yes|Yes|Yes|No|No| 
|Standard performance reports|Yes|Yes|Yes|No|No| 
|Plan guides and plan freezing for plan guides|Yes|Yes|Yes|No|No|   
|Direct query of indexed views (using NOEXPAND hint)|Yes|Yes|Yes|Yes| 
|Automatic indexed views maintenance|Yes|Yes|Yes|No|No| 
|Distributed partitioned views|Yes|No|No|No| 
|Parallel indexed operations|Yes|No|No|No|  
|Automatic use of indexed view by query optimizer|Yes|No|No|No| 
|Parallel consistency check|Yes|No|No|No| 
|SQL Server Utility Control Point|Yes|No|No|No|    

##  <a name="Programmability"></a> Programmability  
  
|Feature|Enterprise|Standard|Web|Express 
|-------------|----------------|--------------|---------|------------------------|  
|JSON|Yes|Yes|Yes|Yes|   
|Query Store|Yes|Yes|Yes|Yes|   
|Temporal|Yes|Yes|Yes|Yes|   
|Native XML support|Yes|Yes|Yes|Yes| 
|XML indexing|Yes|Yes|Yes|Yes| 
|MERGE & UPSERT capabilities|Yes|Yes|Yes|Yes|   
|Date and Time datatypes|Yes|Yes|Yes|Yes|  
|Internationalization support|Yes|Yes|Yes|Yes| 
|Full-text and semantic search|Yes|Yes|Yes|Yes|No| 
|Specification of language in query|Yes|Yes|Yes|Yes|No|   
|Service Broker (messaging)|Yes|Yes|No (Client only)|No (Client only)|No (Client only)|   
|Transact-SQL endpoints|Yes|Yes|Yes|No|No| 
|Graph|Yes|Yes|Yes|Yes|  


<sup>1</sup> Scale out with multiple compute nodes requires a head node.

## <a name="IS"></a> Integration Services

For info about the Integration Services (SSIS) features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services features supported by the editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

##  <a name="SLS"></a> Spatial and location services  
  
|Feature Name|Enterprise|Standard|Web|Express|  
|------------------|----------------|--------------|---------|------------------------------------|------------------------|
|Spatial indexes|Yes|Yes|Yes|Yes|   
|Planar and geodetic datatypes|Yes|Yes|Yes|Yes| 
|Advanced spatial libraries|Yes|Yes|Yes|Yes|   
|Import/export of industry-standard spatial data formats|Yes|Yes|Yes|Yes|   

  
## Next steps 
 [Editions and supported features for SQL Server 2017 - Windows](../sql-server/editions-and-components-of-sql-server-2017.md)  
 [Editions and supported features for SQL Server 2016 - Windows](../sql-server/editions-and-components-of-sql-server-2016.md)  
 [Editions and supported features for SQL Server 2014 - Windows](https://msdn.microsoft.com/library/cc645993(v=sql.120).aspx)  
 [Installation for SQL Server](../database-engine/install-windows/installation-for-sql-server-2016.md)  
 [Product Specifications for SQL Server](https://msdn.microsoft.com/library/6445fd53-6844-4170-a86b-7fe76a9f64cb) 

  
  
