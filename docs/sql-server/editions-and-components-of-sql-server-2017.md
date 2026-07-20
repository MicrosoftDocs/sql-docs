---
title: "Editions and Supported Features"
titleSuffix: SQL Server 2017
description: This article describes features supported by the various editions of SQL Server 2017, which accommodate different performance, runtime, and price requirements.
author: "rwestMSFT"
ms.author: "randolphwest"
ms.reviewer: randolphwest
ms.date: 11/27/2025
ms.service: sql
ms.subservice: release-landing
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Enterprise Edition [SQL Server]"
  - "Developer Edition [SQL Server]"
  - "32-bit vs. 64-bit editions [SQL Server]"
  - "default components"
  - "Workgroup Edition [SQL Server]"
  - "Internet servers [SQL Server]"
  - "installing SQL Server, components"
  - "Setup [SQL Server], components"
  - "SQL Server, editions"
  - "SQL Server, components"
  - "client/server applications [SQL Server]"
  - "editions [SQL Server]"
  - "versions [SQL Server]"
  - "Setup [SQL Server], editions"
  - "SQL Server Installation Wizard"
  - "components [SQL Server]"
  - "Standard Edition [SQL Server]"
  - "64-bit edition [SQL Server]"
  - "IIS [SQL Server]"
  - "installing SQL Server, editions"
  - "editions [SQL Server], about edition options"
  - "Setup [SQL Server]"
monikerRange: ">=sql-server-2017"
---
# Editions and supported features of SQL Server 2017

[!INCLUDE [SQL Server 2017](../includes/applies-to-version/sqlserver2017.md)]

This article provides details of features supported by the various editions of SQL Server 2017.

For information about other versions, see:

- [Editions and supported features of SQL Server 2022](editions-and-components-of-sql-server-2022.md)
- [Editions and supported features of SQL Server 2019](editions-and-components-of-sql-server-2019.md)
- [Editions and supported features of SQL Server 2016](editions-and-components-of-sql-server-2016.md)

Installation requirements vary based on your application needs. The different editions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)].

For the latest release notes and what's new information, see the following:

- [SQL Server 2017 release notes](sql-server-2017-release-notes.md)
- [What's new in SQL Server 2017](what-s-new-in-sql-server-2017.md)

### Try SQL Server

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server 2017 from the Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2017-rtm)**

## SQL Server editions

[!INCLUDE [sql-server-editions](../includes/paragraph-content/sql-server-editions-1.md)]

## Use SQL Server with an Internet Server

On an Internet server, such as a server that is running Internet Information Services (IIS), you'll typically install the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] client tools. Client tools include the client connectivity components used by an application connecting to an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)].

> [!NOTE]  
> Although you can install an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on a computer that is running IIS, this is typically done only for small Web sites that have a single server computer. Most Web sites have their middle-tier IIS systems on one server or a cluster of servers, and their databases on a separate server or federation of servers.

## Use SQL Server with client/server applications

You can install just the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] client components on a computer running client/server applications that connect directly to an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] applications.

The client tools option installs the following [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] features: backward compatibility components, [!INCLUDE [ssBIDevStudio](../includes/ssbidevstudio-md.md)], connectivity components, management tools, software development kit, and [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Books Online components. For more information, see [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md).

## Decide among SQL Server components

Use the Feature Selection page of the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Installation Wizard to select the components to include in an installation of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. By default, none of the features in the tree are selected.

Use the information in the following tables to determine the set of features that best fits your needs.

| Server components | Description |
| --- | --- |
| SQL Server Database Engine | [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE [ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data, in database analytics integration, and PolyBase integration for access to Hadoop and other heterogeneous data sources, and the [!INCLUDE [ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) server. |
| Analysis Services | [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)] includes the tools for creating and managing online analytical processing (OLAP) and data mining applications. |
| Reporting Services | [!INCLUDE [ssRSnoversion](../includes/ssrsnoversion-md.md)] includes server and client components for creating, managing, and deploying tabular, matrix, graphical, and free-form reports. [!INCLUDE [ssRSnoversion](../includes/ssrsnoversion-md.md)] is also an extensible platform that you can use to develop report applications. |
| Integration Services | [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] is a set of graphical tools and programmable objects for moving, copying, and transforming data. It also includes the [!INCLUDE [ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) component for [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)]. |
| Master Data Services | [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)] (MDS) is the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] solution for master data management. MDS can be configured to manage any domain (products, customers, accounts) and includes hierarchies, granular security, transactions, data versioning, and business rules, as well as an [!INCLUDE [ssMDSXLS](../includes/ssmdsxls-md.md)] that can be used to manage data. |
| Machine Learning Services (In-Database) | Machine Learning Services (In-Database) supports distributed, scalable machine learning solutions using enterprise data sources. In SQL Server 2016, the R language was supported. SQL Server 2017 supports R and Python. |
| Machine Learning Server (Standalone) | Machine Learning Server (Standalone) supports deployment of distributed, scalable machine learning solutions on multiple platforms and using multiple enterprise data sources, including Linux and Hadoop. In SQL Server 2016, the R language was supported. SQL Server 2017 supports R and Python. |

| Management tools | Description |
| --- | --- |
| SQL Server Management Studio | [!INCLUDE [ssManStudioFull](../includes/ssmanstudiofull-md.md)] is an integrated environment to access, configure, manage, administer, and develop components of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)] lets developers and administrators of all skill levels use [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)].<br /><br />Install [SQL Server Management Studio](/ssms/install/install). |
| SQL Server Configuration Manager | [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager provides basic configuration management for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] services, server protocols, client protocols, and client aliases. |
| SQL Server Profiler | [!INCLUDE [ssSqlProfiler](../includes/sssqlprofiler-md.md)] provides a graphical user interface to monitor an instance of the [!INCLUDE [ssDE](../includes/ssde-md.md)] or [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)]. |
| Database Tuning Advisor (DTA) | [!INCLUDE [ssDE](../includes/ssde-md.md)] Tuning Advisor helps create optimal sets of indexes, indexed views, and partitions. |
| Data Quality Services client | Provides a highly simple and intuitive graphical user interface to connect to the DQS server, and perform data cleansing operations. It also allows you to centrally monitor various activities performed during the data cleansing operation. |
| SQL Server Data Tools (SSDT) | [!INCLUDE [ssBIDevStudio](../includes/ssbidevstudio-md.md)] provides an IDE for building solutions for the Business Intelligence components: [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)], [!INCLUDE [ssRSnoversion](../includes/ssrsnoversion-md.md)], and [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)].<br /><br />(Formerly called Business Intelligence Development Studio).<br /><br />[!INCLUDE [ssBIDevStudio](../includes/ssbidevstudio-md.md)] also includes "Database Projects", which provides an integrated environment for database developers to carry out all their database design work for any [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] platform (both on and off premise) within Visual Studio. Database developers can use the enhanced Server Explorer in Visual Studio to easily create or edit database objects and data, or execute queries. |
| Connectivity components | Installs components for communication between clients and servers, and network libraries for DB-Library, ODBC, and OLE DB. |

| Documentation | Description |
| --- | --- |
| Microsoft SQL documentation | Core documentation for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. |

### Developer and Evaluation editions

For features supported by Developer and Evaluation editions, see features listed for the SQL Server Enterprise edition in the following tables.

The Developer edition continues to support only 1 client for [SQL Server Distributed Replay overview](../tools/distributed-replay/sql-server-distributed-replay.md).

<a id="Cross-BoxScaleLimits"></a>

## Scale limits

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Maximum compute capacity used by a single instance - SQL Server Database Engine <sup>1</sup> | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum compute capacity used by a single instance - Analysis Services or Reporting Services | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum memory for buffer pool per instance of SQL Server Database Engine | Operating system maximum | 128&nbsp;GB | 64&nbsp;GB | 1,410&nbsp;MB | 1,410&nbsp;MB |
| Maximum capacity for the [buffer pool extension](../database-engine/configure-windows/buffer-pool-extension.md) per instance of SQL Server Database Engine | 32 * (max server memory configuration) | 4 * (max server memory configuration) | N/A | N/A | N/A |
| Maximum memory for columnstore segment cache per instance of SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 16&nbsp;GB | 352&nbsp;MB | 352&nbsp;MB |
| Maximum memory-optimized data size per database in SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 16&nbsp;GB | 352&nbsp;MB | 352&nbsp;MB |
| Maximum memory utilized per instance of Analysis Services | Operating system maximum | Tabular: 16&nbsp;GB<br /><br />MOLAP: 64&nbsp;GB | N/A | N/A | N/A |
| Maximum memory utilized per instance of Reporting Services | Operating system maximum | 64&nbsp;GB | 64&nbsp;GB | 4&nbsp;GB | N/A |
| Maximum relational database size | 524&nbsp;PB | 524&nbsp;PB | 524&nbsp;PB | 10&nbsp;GB | 10&nbsp;GB |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](compute-capacity-limits-by-edition-of-sql-server.md).

<a id="RDBMSHA"></a>

<a id="rdbms-high-availability"></a>

## High availability

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Server core support <sup>1</sup> | Yes | Yes | Yes | Yes | Yes |
| Log shipping | Yes | Yes | Yes | No | No |
| Database mirroring | Yes | Yes <sup>6</sup> | Yes <sup>5</sup> | Yes <sup>5</sup> | Yes <sup>5</sup> |
| Backup compression | Yes | Yes | No | No | No |
| Database snapshot | Yes | Yes | Yes | Yes | Yes |
| Always On failover cluster instances <sup>2</sup> | Yes | Yes | No | No | No |
| Always On availability groups <sup>3</sup> | Yes | No | No | No | No |
| Basic availability groups <sup>4</sup> | No | Yes | No | No | No |
| Online page and file restore | Yes | No | No | No | No |
| Online index create and rebuild | Yes | No | No | No | No |
| Resumable online index rebuilds | Yes | No | No | No | No |
| Online schema change | Yes | No | No | No | No |
| Fast recovery | Yes | No | No | No | No |
| Mirrored backups | Yes | No | No | No | No |
| Hot add memory and CPU | Yes | No | No | No | No |
| Database recovery advisor | Yes | Yes | Yes | Yes | Yes |
| Encrypted backup | Yes | Yes | No | No | No |
| Hybrid backup to Azure (backup to URL) | Yes | Yes | No | No | No |
| Read-scale availability group <sup>3, 4</sup> | Yes | No | No | No | No |

<sup>1</sup> For more information on installing SQL Server on Server Core, see [Install SQL Server on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md).

<sup>2</sup> On Enterprise edition, the maximum number of nodes is 16. On Standard edition there's support for two nodes.

<sup>3</sup> On Enterprise edition, provides support for up to 8 secondary replicas - including 2 synchronous secondary replicas.

<sup>4</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic Always On availability groups for a single database](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

<sup>5</sup> Witness only.

<sup>6</sup> Full Safety only.

<a id="RDBMSSP"></a>

## Scalability and performance

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Columnstore <sup>1, 2</sup> | Yes | Yes | Yes | Yes | Yes |
| Large object binaries in clustered columnstore indexes | Yes | Yes | Yes | Yes | Yes |
| Online nonclustered columnstore index rebuild | Yes | No | No | No | No |
| In-Memory OLTP <sup>1, 2</sup> | Yes | Yes | Yes | Yes <sup>3</sup> | Yes |
| Stretch Database | Yes | Yes | Yes | Yes | Yes |
| Persistent main memory | Yes | Yes | Yes | Yes | Yes |
| Multi-instance support | 50 | 50 | 50 | 50 | 50 |
| Table and index partitioning <sup>2</sup> | Yes | Yes | Yes | Yes | Yes |
| Data compression <sup>2</sup> | Yes | Yes | Yes | Yes | Yes |
| Resource governor | Yes | No | No | No | No |
| Partitioned table parallelism <sup>2</sup> | Yes | Yes | Yes | No | No |
| Multiple filestream containers <sup>2</sup> | Yes | Yes | Yes | Yes | Yes |
| NUMA aware large page memory and buffer array allocation | Yes | No | No | No | No |
| Buffer pool extension | Yes | Yes | No | No | No |
| I/O resource governance | Yes | No | No | No | No |
| Read-ahead | Yes | No | No | No | No |
| Advanced scanning | Yes | No | No | No | No |
| Delayed durability | Yes | Yes | Yes | Yes | Yes |
| Bulk insert improvements | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> In-Memory OLTP data size and columnstore segment cache are limited to the amount of memory specified by edition in the [Scale Limits](#Cross-BoxScaleLimits) section. The degree of parallelism (DOP) for [batch mode](../relational-databases/query-processing-architecture-guide.md#batch-mode-execution) operations is limited to 2 for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Standard edition and 1 for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Web and Express Editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

<sup>2</sup> Aggregate pushdown, string predicate pushdown, and SIMD optimizations are [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Enterprise edition scalability enhancements. For more detail, see [What's new in columnstore indexes](../relational-databases/indexes/columnstore-indexes-what-s-new.md).

<sup>3</sup> This feature isn't included in the LocalDB installation option.

## Intelligent query processing

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Automatic tuning | Yes | No | No | No | No |
| Batch mode adaptive joins | Yes | No | No | No | No |
| Batch mode memory grant feedback | Yes | No | No | No | No |
| Interleaved execution for multi-statement table valued functions | Yes | Yes | Yes | Yes | Yes |

<a id="RDBMSS"></a>

## Security

| Feature | Enterprise | Standard | Web | Express | Express with<br />Advanced Services |
| --- | ---: | ---: | ---: | ---: | ---: |
| Row-level security | Yes | Yes | Yes | Yes | Yes |
| Always Encrypted | Yes | Yes | Yes | Yes | Yes |
| Dynamic data masking | Yes | Yes | Yes | Yes | Yes |
| Server audit | Yes | Yes | Yes | Yes | Yes |
| Database audit | Yes | Yes | Yes | Yes | Yes |
| Transparent data encryption (TDE) | Yes | No | No | No | No |
| Extensible Key Management (EKM) | Yes | No | No | No | No |
| User-defined roles | Yes | Yes | Yes | Yes | Yes |
| Contained databases | Yes | Yes | Yes | Yes | Yes |
| Encryption for backups | Yes | Yes | No | No | No |

<a id="RDBMSM"></a>

<a id="rdbms-manageability"></a>

## Manageability

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| User instances | No | No | No | Yes | Yes |
| LocalDB | No | No | No | Yes | No |
| Dedicated admin connection | Yes | Yes | Yes | Yes <sup>3</sup> | Yes <sup>3</sup> |
| Create new endpoints | Yes | Yes | Yes | No | No |
| SysPrep support <sup>1</sup> | Yes | Yes | Yes | Yes | Yes |
| PowerShell scripting support <sup>2</sup> | Yes | Yes | Yes | Yes | Yes |
| Support for data-tier application component operations (extract, deploy, upgrade, delete) | Yes | Yes | Yes | Yes | Yes |
| Policy automation (check on schedule and change) | Yes | Yes | Yes | No | No |
| Performance data collector | Yes | Yes | Yes | No | No |
| Able to enroll as a managed instance in multi-instance management | Yes | Yes | Yes | No | No |
| Standard performance reports | Yes | Yes | Yes | No | No |
| Plan guides and plan freezing for plan guides | Yes | Yes | Yes | No | No |
| Direct query of indexed views (using `NOEXPAND` hint) | Yes | Yes | Yes | Yes | Yes |
| Automatic indexed views maintenance | Yes | Yes | Yes | No | No |
| Distributed partitioned views | Yes | No | No | No | No |
| Parallel index maintenance operations | Yes | No | No | No | No |
| Automatic use of indexed view by query optimizer | Yes | No | No | No | No |
| Parallel consistency check | Yes | No | No | No | No |
| SQL Server Utility Control Point | Yes | No | No | No | No |
| Buffer pool extension | Yes | Yes | No | No | No |

<sup>1</sup> For more information, see [Considerations for installing SQL Server using SysPrep](../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md).

<sup>2</sup> On Linux, PowerShell scripts are supported, from Windows computers targeting SQL Servers on Linux.

<sup>3</sup> With trace flag.

<a id="Programmability"></a>

## Programmability

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Basic R integration <sup>1</sup> | Yes | Yes | Yes | Yes | No |
| Advanced R integration <sup>2</sup> | Yes | No | No | No | No |
| Basic Python integration | Yes | Yes | Yes | Yes | No |
| Advanced Python integration | Yes | No | No | No | No |
| Machine Learning Server (Standalone) | Yes | No | No | No | No |
| PolyBase compute node | Yes | Yes <sup>3</sup> | Yes <sup>3</sup> | Yes <sup>3</sup> | Yes <sup>3</sup> |
| PolyBase head node | Yes | No | No | No | No |
| JSON | Yes | Yes | Yes | Yes | Yes |
| Query Store | Yes | Yes | Yes | Yes | Yes |
| Temporal | Yes | Yes | Yes | Yes | Yes |
| Common language runtime (CLR) integration | Yes | Yes | Yes | Yes | Yes |
| Native XML support | Yes | Yes | Yes | Yes | Yes |
| XML indexing | Yes | Yes | Yes | Yes | Yes |
| `MERGE` and upsert capabilities | Yes | Yes | Yes | Yes | Yes |
| FILESTREAM support | Yes | Yes | Yes | Yes | Yes |
| FileTable | Yes | Yes | Yes | Yes | Yes |
| Date and time data types | Yes | Yes | Yes | Yes | Yes |
| Internationalization support | Yes | Yes | Yes | Yes | Yes |
| Full-text and semantic search | Yes | Yes | Yes | Yes | No |
| Specification of language in query | Yes | Yes | Yes | Yes | No |
| Service Broker (messaging and queuing) | Yes | Yes | No <sup>4</sup> | No <sup>4</sup> | No <sup>4</sup> |
| Transact-SQL endpoints | Yes | Yes | Yes | No | No |
| Graph | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> Basic integration is limited to 2 cores and in-memory data sets.

<sup>2</sup> Advanced integration can use all available cores for parallel processing of data sets at any size subject to hardware limits.

<sup>3</sup> Scale out with multiple compute nodes requires a head node.

<sup>4</sup> Client only.

<a id="SLS"></a>

## Spatial and location services

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Spatial indexes | Yes | Yes | Yes | Yes | Yes |
| Planar and geodetic datatypes | Yes | Yes | Yes | Yes | Yes |
| Advanced spatial libraries | Yes | Yes | Yes | Yes | Yes |
| Import/export of industry-standard spatial data formats | Yes | Yes | Yes | Yes | Yes |

<a id="Replication"></a>

## Replication

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Heterogeneous subscribers | Yes | Yes | No | No | No |
| Merge replication | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> | Yes <sup>1</sup> |
| Oracle publishing | Yes | No | No | No | No |
| Peer to peer transactional replication | Yes | No | No | No | No |
| Snapshot replication | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> | Yes <sup>1</sup> |
| SQL Server change tracking | Yes | Yes | Yes | Yes | Yes |
| Transactional replication | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> | Yes <sup>1</sup> |
| Transactional replication to Azure | Yes | Yes | No | No | No |
| Transactional replication updatable subscription | Yes | Yes | No | No | No |

<sup>1</sup> Subscriber only

<a id="SSMS"></a>

## Management tools

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| SQL Management Objects (SMO) | Yes | Yes | Yes | Yes | Yes |
| SQL Server Configuration Manager | Yes | Yes | Yes | Yes | Yes |
| **sqlcmd** utility (command line tool) | Yes | Yes | Yes | Yes | Yes |
| Distributed Replay: Admin tool | Yes | Yes | Yes | Yes | No |
| Distributed Replay: Client | Yes | Yes | Yes | No | No |
| Distributed Replay: Controller | Up to 16 clients | 1&nbsp;client | 1&nbsp;client | No | No |
| SQL Profiler | Yes | Yes | No <sup>1</sup> | No <sup>1</sup> | No <sup>1</sup> |
| SQL Server Agent | Yes | Yes | Yes | No | No |
| Microsoft System Center Operations Manager Management Pack | Yes | Yes | Yes | No | No |
| Database Tuning Advisor (DTA) | Yes | Yes <sup>2</sup> | Yes <sup>2</sup> | No | No |

<sup>1</sup> SQL Server Web, SQL Server Express, SQL Server Express with Tools, and SQL Server Express with Advanced Services can be profiled using SQL Server Standard and SQL Server Enterprise editions.

<sup>2</sup> Tuning enabled only on Standard edition features

<a id="DevTools"></a>

## Development tools

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Microsoft Visual Studio integration | Yes | Yes | Yes | Yes | Yes |
| IntelliSense (Transact-SQL and MDX) | Yes | Yes | Yes | Yes | Yes |
| SQL Server Data Tools (SSDT) | Yes | Yes | Yes | Yes | No |
| MDX edit, debug, and design tools | Yes | Yes | No | No | No |

<a id="IS"></a>

## Integration Services

For info about SQL Server Integration Services (SSIS) features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services features supported by the editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

<a id="MDS"></a>

## Master Data Services

For information about the [!INCLUDE [ssMDSshort_md](../includes/ssmdsshort-md.md)] and Data Quality Services features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Master Data Services and Data Quality Services Features Support](../master-data-services/master-data-services-and-data-quality-services-features-support.md).

<a id="DW"></a>

## Data warehouse

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| Create cubes without a database | Yes | Yes | No | No | No |
| Autogenerate staging and data warehouse schema | Yes | Yes | No | No | No |
| Change data capture | Yes | Yes | No | No | No |
| Star join query optimizations | Yes | No | No | No | No |
| Scalable read-only Analysis Services configuration | Yes | No | No | No | No |
| Parallel query processing on partitioned tables and indexes | Yes | No | No | No | No |
| Global batch aggregation | Yes | No | No | No | No |

<a id="SSAS"></a>

## Analysis Services

For information about the Analysis Services features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server](/analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016).

<a id="BIMD"></a>

## BI semantic model (multidimensional)

For information about the Analysis Services features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server](/analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016).

<a id="BIT"></a>

## BI semantic model (tabular)

For information about the Analysis Services features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server](/analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016).

<a id="PPSP"></a>

## Power Pivot for SharePoint

For information about the Power Pivot for SharePoint features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server](/analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016).

<a id="DM"></a>

## Data mining

For information about the data mining features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server](/analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016).

<a id="SSRS"></a>

## Reporting Services

For information about the Reporting Services features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [SQL Server Reporting Services features supported by editions](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server.md).

<a id="BIC"></a>

## Business intelligence clients

For information about the Business Intelligence Client features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the Editions of SQL Server](/analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016) or [SQL Server Reporting Services features supported by editions](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server.md).

<a id="ADS"></a>

## Additional database services

| Feature | Enterprise | Standard | Web | Express with<br />Advanced Services | Express |
| --- | ---: | ---: | ---: | ---: | ---: |
| SQL Server Migration Assistant (SSMA) | Yes | Yes | Yes | Yes | Yes |
| Database mail | Yes | Yes | Yes | No | No |

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Install the latest version of SQL Server Management Studio](/ssms/install/install)**

## Related content

- [Product Specifications for SQL Server](index.yml)
- [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
