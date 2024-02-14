---
title: "Editions and supported features of SQL Server 2022"
description: "Editions and supported features of SQL Server 2022"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, randolphwest
ms.date: 07/06/2023
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
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
monikerRange: ">= sql-server-2016"
---
# Editions and supported features of SQL Server 2022

[!INCLUDE [sqlserver2022](../includes/applies-to-version/sqlserver2022.md)]

This article provides details of features supported by the various editions of [!INCLUDE[sssql22-md](../includes/sssql22-md.md)].

For information about other versions, see:

- [SQL Server 2019](editions-and-components-of-sql-server-2019.md)
- [SQL Server 2017](editions-and-components-of-sql-server-2017.md)
- [SQL Server 2016](editions-and-components-of-sql-server-2016.md)

For information about Azure SQL, see [Features comparison: Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/features-comparison).

Installation requirements vary based on your application needs. The different editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

For the latest release notes and what's new information, see the following:

- [SQL Server 2022 release notes](sql-server-2022-release-notes.md)
- [What's new in SQL Server 2022](what-s-new-in-sql-server-2022.md)

**Try [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]! [Download [!INCLUDE[sssql22-md](../includes/sssql22-md.md)] from the Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2022)**.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## SQL Server editions

[!INCLUDE [sql-server-editions](../includes/paragraph-content/sql-server-editions.md)]

## Choose SQL Server features

Use the Feature Selection page of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Installation Wizard to select the components to include in an installation of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. By default, none of the features in the tree are selected.

Use the information in the following tables to determine the set of features that best fits your needs.

### Server features

| Feature | Description |
| --- | --- |
| [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE[ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data, in database analytics integration, and PolyBase integration for access to heterogeneous data sources, and Machine Learning Services to run Python and R scripts with relational data. |
| [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] | [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] includes the tools for creating and managing online analytical processing (OLAP) and data mining applications. |
| [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] | [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] includes server and client components for creating, managing, and deploying tabular, matrix, graphical, and free-form reports. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] is also an extensible platform that you can use to develop report applications. |
| [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] | [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is a set of graphical tools and programmable objects for moving, copying, and transforming data. It also includes the [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) component for [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. |
| [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] | [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] (MDS) is the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] solution for master data management. MDS can be configured to manage any domain (products, customers, accounts) and includes hierarchies, granular security, transactions, data versioning, and business rules, as well as an [!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)] that can be used to manage data. |
| Machine Learning Services (In-Database) | Machine Learning Services (In-Database) supports distributed, scalable machine learning solutions using enterprise data sources. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2016, the R language was supported. [!INCLUDE[sssql22-md](../includes/sssql22-md.md)] supports R and Python. |
| Data Virtualization with PolyBase | Query different types of data on different types of data sources from SQL Server. |
| Azure connected services | [!INCLUDE[sssql22-md](../includes/sssql22-md.md)] extends Azure connected services and features including Azure Synapse Link, Microsoft Purview access policies, Azure extension for SQL Server, pay-as-you-go billing, and the link feature for SQL Managed Instance. |

### Developer and Evaluation editions

For features supported by Developer and Evaluation editions, see features listed for the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise edition in the tables below.

The Developer edition continues to support only 1 client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md), which is deprecated in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)].

## Scale limits

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Maximum compute capacity used by a single instance - [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]<sup>1</sup> | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum compute capacity used by a single instance - [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] or [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum memory for buffer pool per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | Operating System Maximum | 128&nbsp;GB | 64&nbsp;GB | 1410&nbsp;MB | 1410&nbsp;MB |
| Maximum memory for Columnstore segment cache per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | Unlimited memory | 32&nbsp;GB | 16&nbsp;GB | 352&nbsp;MB | 352&nbsp;MB |
| Maximum memory-optimized data size per database in [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | Unlimited memory | 32&nbsp;GB | 16&nbsp;GB | 352&nbsp;MB | 352&nbsp;MB |
| Maximum memory utilized per instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] | Operating System Maximum | 16&nbsp;GB <sup>2</sup><br /><br />64&nbsp;GB <sup>3</sup> | N/A | N/A | N/A |
| Maximum memory utilized per instance of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] | Operating System Maximum | 64&nbsp;GB | 64&nbsp;GB | 4&nbsp;GB | N/A |
| Maximum relational database size | 524&nbsp;PB | 524&nbsp;PB | 524&nbsp;PB | 10&nbsp;GB | 10&nbsp;GB |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](compute-capacity-limits-by-edition-of-sql-server.md)

<sup>2</sup> Tabular

<sup>3</sup> MOLAP

## <a id="ACS"></a> Azure connected services

SQL Server 2022 includes features for connecting to [Azure Synapse Analytics](/azure/synapse-analytics/), [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/), and [Microsoft Purview](/purview/).

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Azure extension for SQL Server | Yes | Yes | No | No | No |
| Azure Synapse Link <sup>1</sup> | Yes | Yes | Yes | No | No |
| Link feature for SQL Managed Instance <sup>1</sup> | Yes | Yes | No | No | No |
| Failover servers for disaster recovery in Azure | Yes | Yes | No | No | No |
| Microsoft Entra integration | Yes | Yes | Yes | Yes | Yes |
| Microsoft Purview access policies <sup>1</sup> | Yes | Yes | Yes <sup>2</sup> | Yes <sup>2</sup> | Yes <sup>2</sup> |
| Pay-as-you-go billing | Yes | Yes | No | No | No |


<sup>1</sup> These features are governed by their respective [Lifecycle Policies](/lifecycle/products/sql-server-2022).

<sup>2</sup> Configure in Azure portal.

## RDBMS high availability

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Server core support <sup>1</sup> | Yes | Yes | Yes | Yes | Yes |
| Log shipping | Yes | Yes | Yes | No | No |
| Database mirroring <sup>7</sup> | Yes | Yes <sup>2</sup> | Yes <sup>3</sup> | Yes <sup>3</sup> | Yes <sup>3</sup> |
| Backup compression | Yes | Yes | No | No | No |
| Database snapshot | Yes | Yes | Yes | Yes | Yes |
| Always On failover cluster instances <sup>4</sup> | Yes | Yes | No | No | No |
| Always On availability groups <sup>5</sup> | Yes | No | No | No | No |
| Basic availability groups <sup>6</sup> | No | Yes | No | No | No |
| Contained availability groups | Yes | No | No | No | No |
| Distributed availability groups | Yes | No | No | No | No |
| Automatic read write connection re-routing | Yes | No | No | No | No |
| Online page and file restore | Yes | No | No | No | No |
| Online index create and rebuild | Yes | No | No | No | No |
| Resumable online index rebuilds | Yes | No | No | No | No |
| Resumable online ADD CONSTRAINT | Yes | No | No | No | No |
| Online schema change | Yes | No | No | No | No |
| Fast recovery | Yes | No | No | No | No |
| Accelerated database recovery | Yes | Yes | Yes | No | No |
| Mirrored backups | Yes | No | No | No | No |
| Hot add memory and CPU | Yes | No | No | No | No |
| Database recovery advisor | Yes | Yes | Yes | Yes | Yes |
| Encrypted backup | Yes | Yes | No | No | No |
| Backup and restore to S3-compatible object storage over REST API | Yes | Yes | Yes | No | No |
| Snapshot backup | Yes | Yes | Yes | Yes | Yes |
| Cluster-less availability group <sup>5,6</sup> | Yes | Yes | No | No | No |
| Failover servers for disaster recovery | Yes | Yes | No | No | No |
| Failover servers for high availability | Yes | Yes | No | No | No |

<sup>1</sup> For more information on installing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Server Core,  see [Install [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md).

<sup>2</sup> Full safety only

<sup>3</sup> Witness only

<sup>4</sup> On Enterprise edition, the number of nodes is the operating system maximum. On Standard edition there is support for two nodes.

<sup>5</sup> On Enterprise edition, provides support for up to 8 secondary replicas, including 5 synchronous secondary replicas.

<sup>6</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic availability groups](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

<sup>7</sup> Database mirroring is a deprecated feature.

## <a id="RDBMSSP"></a> RDBMS scalability and performance

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Columnstore <sup>1</sup> <sup>2</sup> | Yes | Yes | Yes | Yes | Yes |
| Large object binaries in clustered columnstore indexes | Yes | Yes | Yes | Yes | Yes |
| Online non-clustered columnstore index rebuild | Yes | No | No | No | No |
| In-Memory Database: In-Memory OLTP <sup>1</sup> | Yes | Yes | Yes | Yes <sup>3</sup> | Yes |
| In-Memory Database: hybrid buffer pool | Yes | Yes | No | No | No |
| In-Memory Database: hybrid buffer pool support for direct write | Yes | No | No | No | No |
| In-Memory Database: Memory-optimized TempDB metadata | Yes | No | No | No | No |
| In-Memory Database: persistent memory support | Yes | Yes | Yes | Yes | Yes |
| Stretch Database <sup>4</sup> | Yes | Yes | Yes | Yes | Yes |
| Multi-instance support | 50 | 50 | 50 | 50 | 50 |
| Table and index partitioning | Yes | Yes | Yes | Yes | Yes |
| Data compression | Yes | Yes | Yes | Yes | Yes |
| Resource governor | Yes | No | No | No | No |
| Partitioned table parallelism | Yes | Yes | Yes | No | No |
| Multiple filestream containers | Yes | Yes | Yes | Yes | Yes |
| NUMA aware and large page memory and buffer array allocation | Yes | No | No | No | No |
| Buffer pool extension | Yes | Yes | No | No | No |
| Buffer pool parallel scan | Yes | Yes | No | No | No |
| I/O resource governance | Yes | No | No | No | No |
| Read-ahead | Yes | No | No | No | No |
| Advanced scanning | Yes | No | No | No | No |
| Delayed durability | Yes | Yes | Yes | Yes | Yes |
| Intelligent Database: approximate count distinct | Yes | Yes | Yes | Yes | Yes |
| Intelligent Database: approximate percentile | Yes | Yes | Yes | Yes | Yes |
| Intelligent Database: automatic tuning | Yes | No | No | No | No |
| Intelligent Database: batch mode for row store <sup>1</sup> | Yes | No | No | No | No |
| Intelligent Database: cardinality estimate feedback | Yes | No | No | No | No |
| Intelligent Database: degree of parallelism feedback | Yes | No | No | No | No |
| Intelligent Database: memory grant feedback persistence and percentile | Yes | No | No | No | No |
| Intelligent Database: optimized plan forcing | Yes | Yes | Yes | Yes | Yes |
| Intelligent Database: parameter sensitive plan optimization | Yes | Yes | Yes | Yes | Yes |
| Intelligent Database: row mode memory grant feedback | Yes | No | No | No | No |
| Intelligent Database: table variable deferred compilation | Yes | Yes | Yes | Yes | Yes |
| Intelligent Database: scalar UDF inlining | Yes | Yes | Yes | Yes | Yes |
| Intelligent Database: Batch mode adaptive joins | Yes | No | No | No | No |
| Intelligent Database: Batch mode memory grant feedback | Yes | No | No | No | No |
| Intelligent Database: Interleaved execution for multi-statement table valued functions | Yes | Yes | Yes | Yes | Yes |
| Bulk insert improvements | Yes | Yes | Yes | Yes | Yes |
| Advanced Vector Extension (AVX) 512 integration | Yes | No | No | No | No |
| Integrated acceleration and offloading (hardware) | Yes | No | No | No | No |
| Integrated acceleration and offloading (software) | Yes | Yes | No | No | No |
| System page latch concurrency enhancements | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> In-Memory OLTP data size and Columnstore segment cache are limited to the amount of memory specified by edition in the [Scale Limits](#scale-limits) section. The degree of parallelism (DOP) for [batch mode](../relational-databases/query-processing-architecture-guide.md#batch-mode-execution) operations is limited to 2 for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard edition and 1 for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Web and Express editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

<sup>2</sup> Aggregate Pushdown, String Predicate Pushdown, and SIMD Optimizations are [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise edition scalability enhancements. For more detail, see [Columnstore indexes - what's new](../relational-databases/indexes/columnstore-indexes-what-s-new.md).

<sup>3</sup> This feature is not included in the LocalDB installation option.

<sup>4</sup> Stretch Database is a deprecated feature.

## <a id="RDBMSS"></a> RDBMS security

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Always Encrypted | Yes | Yes | Yes | Yes | Yes |
| Always Encrypted with secure enclaves | Yes | Yes | Yes | Yes | Yes |
| Contained databases | Yes | Yes | Yes | Yes | Yes |
| Database audit | Yes | Yes | Yes | Yes | Yes |
| Data classification and auditing | Yes | Yes | Yes | Yes | Yes |
| Dynamic data masking | Yes | Yes | Yes | Yes | Yes |
| Encryption for backups | Yes | Yes | No | No | No |
| Extensible key management (EKM) | Yes | Yes | No | No | No |
| Ledger for SQL Server | Yes | Yes | Yes | Yes | Yes |
| Microsoft Entra authentication <sup>1</sup> | Yes | Yes | Yes | Yes | Yes |
| Row-level security | Yes | Yes | Yes | Yes | Yes |
| Server audit | Yes | Yes | Yes | Yes | Yes |
| Transparent data encryption (TDE) | Yes | Yes | Yes | No | No |
| User-defined roles | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> Requires [SQL Server enabled by Azure Arc](azure-arc/overview.md)


## <a id="Replication"></a> Replication

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Heterogeneous subscribers | Yes | Yes | No | No | No |
| Merge replication | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> | Yes <sup>1</sup> |
| Oracle publishing | Yes | No | No | No | No |
| Peer to peer transactional replication | Yes | No | No | No | No |
| Peer to peer - last write wins - transactional replication | Yes | No | No | No | No |
| Snapshot replication | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> | Yes <sup>1</sup> |
| [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] change tracking | Yes | Yes | Yes | Yes | Yes |
| Transactional replication | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> | Yes <sup>1</sup> |
| Transactional replication to Azure | Yes | Yes | No | No | No |
| Transactional replication update-able subscription | Yes | Yes | No | No | No |

<sup>1</sup> Subscriber only

## <a id="SSMS"></a> Management tools

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| SQL Management Objects (SMO) | Yes | Yes | Yes | Yes | Yes |
| SQL Assessment API | Yes | Yes | Yes | Yes | Yes |
| SQL Vulnerability Assessment | Yes | Yes | Yes | Yes | Yes |
| SQL Configuration Manager | Yes | Yes | Yes | Yes | Yes |
| SQLCMD (Command Prompt tool) | Yes | Yes | Yes | Yes | Yes |
| SQL Profiler | Yes | Yes | No <sup>1</sup> | No <sup>1</sup> | No <sup>1</sup> |
| [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent | Yes | Yes | Yes | No | No |
| Microsoft System Center Operations Manager Management Pack | Yes | Yes | Yes | No | No |
| Database Tuning Advisor (DTA) | Yes | Yes <sup>2</sup> | Yes <sup>2</sup> | No | No |

<sup>1</sup> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Web, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express with Tools, and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express with Advanced Services can be profiled using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise editions.

<sup>2</sup> Tuning enabled only on Standard edition features

> [!NOTE]  
> Distributed Replay has been deprecated in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)].

## RDBMS manageability

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| User instances | No | No | No | Yes | Yes |
| LocalDB | No | No | No | Yes | No |
| Dedicated admin connection | Yes | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> |
| SysPrep support <sup>2</sup> | Yes | Yes | Yes | Yes | Yes |
| PowerShell scripting support <sup>3</sup> | Yes | Yes | Yes | Yes | Yes |
| Support for data-tier application component operations - extract, deploy, upgrade, delete | Yes | Yes | Yes | Yes | Yes |
| Policy automation (check on schedule and change) | Yes | Yes | Yes | No | No |
| Performance data collector | Yes | Yes | Yes | No | No |
| Able to enroll as a managed instance in multi-instance management | Yes | Yes | Yes | No | No |
| Standard performance reports | Yes | Yes | Yes | No | No |
| Plan guides and plan freezing for plan guides | Yes | Yes | Yes | No | No |
| Direct query of indexed views (using NOEXPAND hint) | Yes | Yes | Yes | Yes | Yes |
| Direct query SQL Server Analysis Services | Yes | Yes | No | No | Yes |
| Automatic indexed views maintenance | Yes | Yes | Yes | No | No |
| Distributed partitioned views | Yes | No | No | No | No |
| Parallel index operations | Yes | No | No | No | No |
| Automatic use of indexed view by query optimizer | Yes | No | No | No | No |
| Parallel consistency check | Yes | No | No | No | No |
| [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility Control Point | Yes | No | No | No | No |
| Buffer pool extension | Yes | Yes | No | No | No |
| Master instance for big data cluster | Yes | Yes | No | No | No |
| Compatibility certification | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> With trace flag

<sup>2</sup> For more information, see [Considerations for Installing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Using SysPrep](../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md).

<sup>3</sup> On Linux, PowerShell scripts are supported, from Windows computers targeting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux.

## Development tools

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Microsoft Visual Studio integration | Yes | Yes | Yes | Yes | Yes |
| Intellisense (Transact-SQL and MDX) | Yes | Yes | Yes | Yes | Yes |
| [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools (SSDT) | Yes | Yes | Yes | Yes | No |
| MDX edit, debug, and design tools | Yes | Yes | No | No | No |

## Programmability

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Basic R integration <sup>1</sup> | Yes | Yes | Yes | Yes | No |
| Advanced R integration <sup>2</sup> | Yes | No | No | No | No |
| Basic Python integration | Yes | Yes | Yes | Yes | No |
| Advanced Python integration | Yes | No | No | No | No |
| Machine Learning Server (Standalone) | Yes | No | No | No | No |
| Data Virtualization with PolyBase | Yes | Yes | Yes | Yes | Yes |
| JSON | Yes | Yes | Yes | Yes | Yes |
| Query Store | Yes | Yes | Yes | Yes | Yes |
| Query Store: on by default for new databases | Yes | Yes | Yes | Yes | Yes |
| Query Store: hint support | Yes | Yes | Yes | Yes | Yes |
| Query Store: read-replica support | Yes | No | No | No | No |
| Temporal | Yes | Yes | Yes | Yes | Yes |
| Common Language Runtime (CLR) Integration | Yes | Yes | Yes | Yes | Yes |
| Java Language Runtime Integration | Yes | Yes | Yes | Yes | Yes |
| Native XML support | Yes | Yes | Yes | Yes | Yes |
| XML indexing | Yes | Yes | Yes | Yes | Yes |
| XML compression | Yes | Yes | Yes | Yes | Yes |
| MERGE and UPSERT capabilities | Yes | Yes | Yes | Yes | Yes |
| FILESTREAM support | Yes | Yes | Yes | Yes | Yes |
| FileTable | Yes | Yes | Yes | Yes | Yes |
| Date and Time datatypes | Yes | Yes | Yes | Yes | Yes |
| Time series support | Yes | Yes | Yes | Yes | Yes |
| Internationalization support | Yes | Yes | Yes | Yes | Yes |
| Full-text and semantic search | Yes | Yes | Yes | Yes | No |
| Specification of language in query | Yes | Yes | Yes | Yes | No |
| Service Broker (messaging) | Yes | Yes | No <sup>3</sup> | No <sup>3</sup> | No <sup>3</sup> |
| Transact-SQL endpoints | Yes | Yes | Yes | No | No |
| Graph | Yes | Yes | Yes | Yes | Yes |
| UTF-8 support | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> Basic integration is limited to 2 cores and in-memory data sets.

<sup>2</sup> Advanced integration can use all available cores for parallel processing of data sets at any size subject to hardware limits.

<sup>3</sup> Client only.

## Integration Services

For info about [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Integration Services (SSIS) features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services Features Supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

## Master Data Services

For information about the [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] and Data Quality Services features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Master Data Services and Data Quality Services Features Supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]](../master-data-services/master-data-services-and-data-quality-services-features-support.md).

## Data warehouse

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Auto-generate staging and data warehouse schema | Yes | Yes | No | No | No |
| Change data capture | Yes | Yes | No | No | No |
| Star join query optimizations | Yes | No | No | No | No |
| Parallel query processing on partitioned tables and indexes | Yes | No | No | No | No |
| Global batch aggregation | Yes | No | No | No | No |

## Analysis Services

For information about the Analysis Services features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md).

## Reporting Services

For information about the Reporting Services features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Reporting Services Features Supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).

## Business intelligence clients

For information about the Business Intelligence Client features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services Features Supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]](../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md) or [Reporting Services Features Supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).

## Spatial and location services

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Spatial indexes | Yes | Yes | Yes | Yes | Yes |
| Planar and geodetic data types | Yes | Yes | Yes | Yes | Yes |
| Advanced spatial libraries | Yes | Yes | Yes | Yes | Yes |
| Import/export of industry-standard spatial data formats | Yes | Yes | Yes | Yes | Yes |

## Additional database services

| Feature | Enterprise | Standard | Web | Express<br />with<br />Advanced<br />Services | Express |
| :--- | :---: | :---: | :---: | :---: | :---: |
| [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Migration Assistant | Yes | Yes | Yes | Yes | Yes |
| Database mail | Yes | Yes | Yes | No | No |

## Related content

- [What's new in SQL Server 2022](what-s-new-in-sql-server-2022.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
