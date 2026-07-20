---
title: Editions and Supported Features of SQL Server 2025
description: Learn details of the features supported by the various editions of SQL Server 2025.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/19/2025
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
# Editions and supported features of SQL Server 2025

[!INCLUDE [sqlserver2025](../includes/applies-to-version/sqlserver2025.md)]

This article provides details of features supported by the various editions of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

For information about other versions, see:

- [Editions and supported features of SQL Server 2022](editions-and-components-of-sql-server-2022.md)
- [Editions and supported features of SQL Server 2019](editions-and-components-of-sql-server-2019.md)
- [Editions and supported features of SQL Server 2017](editions-and-components-of-sql-server-2017.md)

For information about Azure SQL, see [Features comparison: Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/features-comparison).

Installation requirements vary based on your application needs. The different editions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)].

For the latest release notes and what's new information, see the following articles:

- [SQL Server 2025 release notes](sql-server-2025-release-notes.md)
- [What's new in SQL Server 2025](what-s-new-in-sql-server-2025.md)

**Try SQL Server! [Download SQL Server 2025 from the Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2025)**.

## SQL Server editions

[!INCLUDE [sql-server-editions](../includes/paragraph-content/sql-server-editions-2.md)]

## Choose SQL Server features

Use the **Feature Selection** page of the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Installation Wizard to select the components to include in an installation of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. By default, none of the features in the tree are selected.

Use the information in the following tables to determine the set of features that best fits your needs.

### Server features

| Feature | Description |
| --- | --- |
| SQL Server Database Engine | [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE [ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational, XML, and JSON data, in database analytics integration, and Machine Learning Services to run Python and R scripts with relational data. |
| Data&nbsp;virtualization with PolyBase | Query storage-based data sources (for example, Azure Storage and S3), and ODBC data sources from SQL Server. |
| Analysis Services | [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)] includes the tools for creating and managing online analytical processing (OLAP) and data mining applications. |
| Integration Services | [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] is a set of graphical tools and programmable objects for moving, copying, and transforming data. |

### Enterprise Developer, Standard Developer, and Evaluation editions

For features supported by Enterprise Developer and Evaluation editions, see features listed for Enterprise edition in the following table. For features supported by Standard Developer edition, see the features listed for Standard edition.

## Scale limits

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Maximum compute capacity used by a single instance - SQL Server Database Engine <sup>1</sup> | Operating system maximum | Limited to lesser of 4 sockets or 32 cores <sup>2</sup> | Limited to lesser of 1 socket or 4 cores |
| Maximum compute capacity used by a single instance - [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)] | Operating system maximum | Limited to lesser of 4 sockets or 32 cores <sup>3</sup> | N/A |
| Maximum memory for buffer pool per instance of SQL Server Database Engine | Operating system maximum | 256&nbsp;GB | 1,410&nbsp;MB |
| Maximum memory for columnstore segment cache per instance of SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 352&nbsp;MB |
| Maximum memory-optimized data size per database in SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 352&nbsp;MB |
| Maximum memory utilized per instance of Analysis Services | Operating system maximum | 16&nbsp;GB&nbsp;<sup>4</sup><br /><br />64&nbsp;GB&nbsp;<sup>5</sup> | N/A |
| Maximum relational database size | 524&nbsp;PB | 524&nbsp;PB | 50&nbsp;GB |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](compute-capacity-limits-by-edition-of-sql-server.md).

<sup>2</sup> In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and earlier versions, the limit is the lesser of 4 sockets or 24 cores.

<sup>3</sup> [!INCLUDE [ssasnoversion-md](../includes/ssasnoversion-md.md)] limits compute capacity to 16 physical cores. With [simultaneous multithreading (SMT)](compute-capacity-limits-by-edition-of-sql-server.md) enabled, this limit is doubled.

<sup>4</sup> Tabular.

<sup>5</sup> MOLAP.

## Azure connected services

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Azure extension for SQL Server | Yes | Yes | No |
| Link feature for SQL Managed Instance <sup>1</sup> | Yes | Yes | No |
| Failover servers for disaster recovery in Azure | Yes | Yes | No |
| Microsoft Entra integration | Yes | Yes | Yes |
| Pay-as-you-go billing | Yes | Yes | No |

<sup>1</sup> These features are governed by their respective [Lifecycle Policies](/lifecycle/products/sql-server-2025).

<a id="rdbms-high-availability"></a>

## High availability

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Server core support <sup>1</sup> | Yes | Yes | Yes |
| Log shipping | Yes | Yes | No |
| Database mirroring <sup>2</sup> | Yes | Yes <sup>3</sup> | Yes <sup>4</sup> |
| Backup compression | Yes | Yes | No |
| Database snapshot | Yes | Yes | Yes |
| Always On failover cluster instances <sup>5</sup> | Yes | Yes | No |
| Always On availability groups <sup>6</sup> | Yes | No | No |
| Basic availability groups <sup>7</sup> | No | Yes | No |
| Contained availability groups | Yes | No | No |
| Distributed availability groups | Yes | No | No |
| Automatic read write connection rerouting | Yes | No | No |
| Online page and file restore | Yes | No | No |
| Online index create and rebuild | Yes | No | No |
| Resumable online index create and rebuild | Yes | No | No |
| Resumable online `ADD CONSTRAINT` | Yes | No | No |
| Online schema change | Yes | No | No |
| Fast recovery | Yes | No | No |
| Accelerated database recovery (ADR) | Yes | Yes | No |
| Mirrored backups | Yes | No | No |
| Hot add memory | Yes | No | No |
| Database recovery advisor | Yes | Yes | Yes |
| Encrypted backup | Yes | Yes | No |
| Backup and restore to S3-compatible object storage over REST API | Yes | Yes | No |
| Snapshot backup | Yes | Yes | Yes |
| Clusterless availability group <sup>6, 7</sup> | Yes | Yes | No |
| Failover servers for disaster recovery | Yes | Yes | No |
| Failover servers for high availability | Yes | Yes | No |

<sup>1</sup> For more information on installing [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Server Core, see [Install SQL Server on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md).

<sup>2</sup> Database mirroring is a deprecated feature.

<sup>3</sup> Full safety only.

<sup>4</sup> Witness only.

<sup>5</sup> On Enterprise edition, the maximum number of nodes is 16. On Standard edition, there's support for two nodes.

<sup>6</sup> On Enterprise edition, provides support for up to 8 secondary replicas, including 5 synchronous secondary replicas.

<sup>7</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic Always On availability groups for a single database](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

## Scalability and performance

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Columnstore <sup>1, 2</sup> | Yes | Yes | Yes |
| Large object binaries in clustered columnstore indexes | Yes | Yes | Yes |
| Online nonclustered columnstore index rebuild | Yes | No | No |
| In-Memory Database: In-Memory OLTP <sup>1, 2</sup> | Yes | Yes | Yes <sup>3</sup> |
| In-Memory Database: hybrid buffer pool | Yes | Yes | No |
| In-Memory Database: hybrid buffer pool support for direct write | Yes | No | No |
| In-Memory Database: Memory-optimized TempDB metadata | Yes | No | No |
| In-Memory Database: persistent memory support | Yes | Yes | Yes |
| Multi-instance support | 50 | 50 | 50 |
| Table and index partitioning <sup>2</sup> | Yes | Yes | Yes |
| Data compression <sup>2</sup> | Yes | Yes | Yes |
| Resource governor | Yes | Yes | No |
| Partitioned table parallelism <sup>2</sup> | Yes | Yes | No |
| Multiple filestream containers <sup>2</sup> | Yes | Yes | Yes |
| NUMA aware large page memory and buffer array allocation | Yes | No | No |
| Buffer pool extension | Yes | Yes | No |
| Buffer pool parallel scan | Yes | Yes | No |
| Read-ahead | Yes | No | No |
| Advanced scanning | Yes | No | No |
| Delayed durability | Yes | Yes | Yes |
| Support for Advanced Vector Extension (AVX) 512 <sup>4</sup> | Yes | No | No |
| Integrated acceleration and offloading (hardware) | Yes | No | No |
| Integrated acceleration and offloading (software) | Yes | Yes | No |
| Optimized locking | Yes | Yes | No |
| Tempdb space resource governance | Yes | Yes | No |

<sup>1</sup> In-Memory OLTP data size and columnstore segment cache are limited to the amount of memory specified by edition in the [Scale Limits](#scale-limits) section. The degree of parallelism (DOP) for [batch mode](../relational-databases/query-processing-architecture-guide.md#batch-mode-execution) operations is limited to 2 for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Standard edition and 1 for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Express edition.

<sup>2</sup> Aggregate pushdown, string predicate pushdown, and SIMD optimizations are [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Enterprise edition scalability enhancements. For more information, see [What's new in columnstore indexes](../relational-databases/indexes/columnstore-indexes-what-s-new.md).

<sup>3</sup> This feature isn't included in the LocalDB installation option.

<sup>4</sup> With trace flag.

## Intelligent query processing

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Approximate count distinct | Yes | Yes | Yes |
| Approximate percentile | Yes | Yes | Yes |
| Automatic tuning | Yes | No | No |
| Batch mode on row store <sup>1</sup> | Yes | No | No |
| Batch mode adaptive joins | Yes | No | No |
| Batch mode memory grant feedback | Yes | No | No |
| Cardinality estimate feedback | Yes | No | No |
| Cardinality estimation feedback for expressions | Yes | No | No |
| Degree of parallelism feedback | Yes | No | No |
| Interleaved execution for multi-statement table valued functions | Yes | Yes | Yes |
| Memory grant feedback persistence and percentile | Yes | No | No |
| Optimized plan forcing | Yes | Yes | Yes |
| Optional parameter plan optimization | Yes | Yes | Yes |
| Optimized `sp_executesql` | Yes | Yes | Yes |
| Parameter sensitive plan optimization | Yes | Yes | Yes |
| Row mode memory grant feedback | Yes | No | No |
| Scalar UDF inlining | Yes | Yes | Yes |
| Table variable deferred compilation | Yes | Yes | Yes |

<sup>1</sup> Batch mode on rowstore only supports disk-based heaps and [B+ tree indexes](../relational-databases/sql-server-index-design-guide.md#index-basics). It doesn't support In-Memory OLTP tables, XML columns, or sparse column sets. The degree of parallelism (DOP) for [batch mode](../relational-databases/query-processing-architecture-guide.md#batch-mode-execution) operations is limited to `2` for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Standard edition, and `1` for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Express edition.

## Security

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Always Encrypted | Yes | Yes | Yes |
| Always Encrypted with secure enclaves | Yes | Yes | Yes |
| Contained databases | Yes | Yes | Yes |
| Database audit | Yes | Yes | Yes |
| Data classification and auditing | Yes | Yes | Yes |
| Dynamic data masking | Yes | Yes | Yes |
| Encryption for backups | Yes | Yes | No |
| Extensible Key Management (EKM) | Yes | Yes | No |
| Ledger for SQL Server | Yes | Yes | Yes |
| Microsoft Entra authentication <sup>1</sup> | Yes | Yes | Yes |
| Row-level security | Yes | Yes | Yes |
| Server audit | Yes | Yes | Yes |
| Transparent data encryption (TDE) | Yes | Yes | No |
| User-defined roles | Yes | Yes | Yes |

<sup>1</sup> Requires SQL Server [enabled by Azure Arc](azure-arc/overview.md) or running on an [Azure Virtual Machine](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview).

<a id="rdbms-manageability"></a>

## Manageability

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| User instances | No | No | Yes |
| LocalDB | No | No | Yes |
| Dedicated admin connection | Yes | Yes | Yes <sup>1</sup> |
| Create new endpoints | Yes | Yes | No |
| SysPrep support <sup>2</sup> | Yes | Yes | Yes |
| PowerShell scripting support <sup>3</sup> | Yes | Yes | Yes |
| Support for data-tier application component operations (extract, deploy, upgrade, delete) | Yes | Yes | Yes |
| Policy automation (check on schedule and change) | Yes | Yes | No |
| Performance data collector | Yes | Yes | No |
| Able to enroll as a managed instance in multi-instance management | Yes | Yes | No |
| Standard performance reports | Yes | Yes | No |
| Plan guides and plan freezing for plan guides | Yes | Yes | No |
| Support for creating indexed views | Yes | Yes | Yes |
| Support for querying indexed views | Yes | Yes <sup>4</sup> | Yes <sup>4</sup> |
| Directly query SQL Server Analysis Services | Yes | Yes | Yes |
| Distributed partitioned views | Yes | No | No |
| Parallel index maintenance operations | Yes | No | No |
| Automatic use of indexed view by query optimizer | Yes | No | No |
| Parallel consistency check | Yes | No | No |
| Buffer pool extension | Yes | Yes | No |
| Compatibility certification support using database compatibility level | Yes | Yes | Yes |
| Optional preview features <sup>5</sup> | Yes | Yes | Yes |

<sup>1</sup> With trace flag.

<sup>2</sup> For more information, see [Considerations for installing SQL Server using SysPrep](../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md).

<sup>3</sup> On Linux, PowerShell scripts are supported, from Windows computers targeting [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux.

<sup>4</sup> Use the `NOEXPAND` hint to query indexed views on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Standard and Express editions.

<sup>5</sup> For more information, see [PREVIEW_FEATURES](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).

## Programmability

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Native JSON data type | Yes | Yes | Yes |
| JSON indexing | Yes | Yes | Yes |
| Native XML support | Yes | Yes | Yes |
| XML indexing | Yes | Yes | Yes |
| XML compression | Yes | Yes | Yes |
| `MERGE` and upsert capabilities | Yes | Yes | Yes |
| UTF-8 and UTF-16 support | Yes | Yes | Yes |
| Graph tables | Yes | Yes | Yes |
| Temporal tables | Yes | Yes | Yes |
| Time series support | Yes | Yes | Yes |
| Regular expressions support | Yes | Yes | Yes |
| Fuzzy string matching support <sup>4</sup> | Yes | Yes | Yes |
| Full-text and semantic search | Yes | Yes | Yes |
| Common language runtime (CLR) integration | Yes | Yes | Yes |
| Basic R integration <sup>1</sup> | Yes | Yes | Yes |
| Advanced R integration <sup>2</sup> | Yes | No | No |
| Basic Python integration | Yes | Yes | Yes |
| Advanced Python integration | Yes | No | No |
| Java language runtime integration | Yes | Yes | Yes |
| .NET Core runtime integration | Yes | Yes | Yes |
| Specification of external language in query | Yes | Yes | Yes |
| Distributed queries with linked servers | Yes | Yes | Yes |
| External REST endpoint invocation support | Yes | Yes | Yes |
| Query Store | Yes | Yes | Yes |
| Query Store on by default for new databases | Yes | Yes | Yes |
| Query Store hints | Yes | Yes | Yes |
| Query Store on secondary replicas | Yes | No | No |
| FILESTREAM support | Yes | Yes | Yes |
| FileTable | Yes | Yes | Yes |
| Service Broker (messaging and queuing) | Yes | Yes | No <sup>3</sup> |
| Transact-SQL endpoints | Yes | Yes | No |
| Database mail | Yes | Yes | No |

<sup>1</sup> Basic integration is limited to 2 cores and in-memory data sets.

<sup>2</sup> Advanced integration can use all available cores for parallel processing of data sets at any size subject to hardware limits.

<sup>3</sup> Client only.

<sup>4</sup> Requires [PREVIEW_FEATURES database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).

## AI features

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Native vector data type | Yes | Yes | Yes |
| DiskANN-based vector indexing <sup>1</sup> | Yes | Yes | Yes |
| External models support | Yes | Yes | Yes |
| Local ONNX models support <sup>1</sup> | Yes | Yes | Yes |
| Embedding generation support | Yes | Yes | Yes |
| Chunking support | Yes | Yes | Yes |

<sup>1</sup> Requires [PREVIEW_FEATURES database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).

## Spatial and location services

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Spatial indexes | Yes | Yes | Yes |
| Planar and geodetic datatypes | Yes | Yes | Yes |
| Advanced spatial libraries | Yes | Yes | Yes |
| Import/export of industry-standard spatial data formats | Yes | Yes | Yes |

## Replication

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Mirroring in Microsoft Fabric | Yes | Yes | Yes |
| Heterogeneous subscribers | Yes | Yes | No |
| Merge replication | Yes | Yes | Yes <sup>1</sup> |
| Oracle publishing | Yes | No | No |
| Peer to peer transactional replication | Yes | No | No |
| Peer to peer transactional replication (last write wins) | Yes | No | No |
| Snapshot replication | Yes | Yes | Yes <sup>1</sup> |
| Change tracking | Yes | Yes | Yes |
| Change data capture | Yes | Yes | No |
| Change event streaming <sup>2</sup> | Yes | Yes | Yes |
| Transactional replication | Yes | Yes | Yes <sup>1</sup> |
| Transactional replication to Azure | Yes | Yes | No |
| Transactional replication updatable subscription | Yes | Yes | No |

<sup>1</sup> Subscriber only.

<sup>2</sup> Requires [PREVIEW_FEATURES database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).

## Management tools

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| SQL Server Management Studio (SSMS) | Yes | Yes | Yes |
| SQL Management Objects (SMO) | Yes | Yes | Yes |
| SQL Assessment API | Yes | Yes | Yes |
| SQL Vulnerability Assessment | Yes | Yes | Yes |
| SQL Server Configuration Manager | Yes | Yes | Yes |
| **sqlcmd** utility (command line tool) | Yes | Yes | Yes |
| SQL Profiler | Yes | Yes | No <sup>1</sup> |
| SQL Server Agent | Yes | Yes | No |
| Microsoft System Center Operations Manager Management Pack | Yes | Yes | No |
| Database Tuning Advisor (DTA) | Yes | Yes <sup>2</sup> | No |

<sup>1</sup> [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Express edition can be profiled using [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Standard and [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Enterprise editions.

<sup>2</sup> Tuning enabled only on Standard edition features.

## Development tools

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Visual Studio Code integration | Yes | Yes | Yes |
| Visual Studio integration | Yes | Yes | Yes |
| IntelliSense (Transact-SQL and MDX) | Yes | Yes | No |
| SQL Server Data Tools (SSDT) | Yes | Yes | Yes |
| MDX edit, debug, and design tools | Yes | Yes | No |

## Integration Services

For info about [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Integration Services (SSIS) features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services features supported by the editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

## Data warehouse

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Autogenerate staging and data warehouse schema | Yes | Yes | No |
| Star join query optimizations | Yes | No | No |
| Parallel query processing on partitioned tables and indexes | Yes | No | No |
| Global batch aggregation | Yes | No | No |

## Analysis Services

For information about the Analysis Services features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Analysis Services features supported by SQL Server edition](/analysis-services/analysis-services-features-by-edition).

## Power BI Report Server

[!INCLUDE [ssrs-power-bi-consolidation](../reporting-services/includes/ssrs-power-bi-consolidation.md)]

## Related content

- [What's new in SQL Server 2025](what-s-new-in-sql-server-2025.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
