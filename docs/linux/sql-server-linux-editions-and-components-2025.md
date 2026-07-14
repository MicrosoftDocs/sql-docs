---
title: Editions and Supported Features
titleSuffix: SQL Server 2025 on Linux
description: This article describes editions, features, and components supported by the various editions of SQL Server 2025 on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 01/08/2026
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
  - ignite-2025
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
---
# Editions and supported features of SQL Server 2025 on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides details of features supported by the various editions of [!INCLUDE [sssql25](../includes/sssql25-md.md)] on Linux.

For editions and supported features of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Windows, see [Editions and supported features of SQL Server 2025](../sql-server/editions-and-components-of-sql-server-2025.md).

For more information on what's new in [!INCLUDE [sssql25](../includes/sssql25-md.md)], see:

- [What's new for SQL Server 2025 on Linux](sql-server-linux-whats-new-2025.md)
- [What's new in SQL Server 2025](../sql-server/what-s-new-in-sql-server-2025.md)

Installation requirements vary based on your application needs. The different editions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

For the latest release notes and what's new information, see [Release notes for SQL Server 2025 on Linux](sql-server-linux-release-notes-2025.md).

For a list of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] features not available on Linux, see [Unsupported features and services](#unsupported-features-and-services).

## SQL Server editions

[!INCLUDE [sql-server-editions](../includes/paragraph-content/sql-server-editions-linux.md)]

## Use SQL Server with client/server applications

You can install just the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] client components on a computer running client/server applications that connect directly to an instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] applications.

## SQL Server components

[!INCLUDE [sssql25](../includes/sssql25-md.md)] on Linux supports the [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)]. The following table describes the features in the [!INCLUDE [ssDE](../includes/ssde-md.md)].

| Server components | Description |
| --- | --- |
| SQL Server Database Engine | [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE [ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, Full-Text Search, tools for managing relational and XML data, and in database analytics integration. |

**Enterprise Developer, Standard Developer, Enterprise Core, and Evaluation editions**

For features supported by Enterprise Developer, Standard Developer, Enterprise Core, and Evaluation editions, see features listed for the SQL Server Enterprise edition in the following tables.

The Developer editions continue to support only one client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md).

> [!NOTE]  
> [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] introduces separate Enterprise Developer and Standard Developer editions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

## Scale limits

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Maximum compute capacity used by a single instance - SQL Server Database Engine <sup>1</sup> | Operating system maximum | Limited to lesser of 4 sockets or 32 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum compute capacity used by a single instance - Analysis Services or Reporting Services | Operating system maximum | Limited to lesser of 4 sockets or 32 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum memory for buffer pool per instance of SQL Server Database Engine | Operating system maximum | 256&nbsp;GB | 1,410&nbsp;MB |
| Maximum capacity for the [buffer pool extension](../database-engine/configure-windows/buffer-pool-extension.md) per instance of SQL Server Database Engine | 32 * (max server memory configuration) | 4 * (max server memory configuration) | N/A |
| Maximum memory for columnstore segment cache per instance of SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 352&nbsp;MB |
| Maximum memory-optimized data size per database in SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 352&nbsp;MB |
| Maximum relational database size | 524&nbsp;PB | 524&nbsp;PB | 10&nbsp;GB |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).

<a id="rdbms-high-availability"></a>

## Azure connected services

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Azure extension for SQL Server | Yes | Yes | No |
| Link feature for SQL Managed Instance <sup>1</sup> | Yes | Yes | No |
| Failover servers for disaster recovery in Azure | Yes | Yes | No |
| Microsoft Entra integration | Yes | Yes | Yes |
| Pay-as-you-go billing | Yes | Yes | No |

<sup>1</sup> These features are governed by their respective [Lifecycle Policies](/lifecycle/products/sql-server-2025).

## High availability

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Log shipping | Yes | Yes | No |
| Backup compression | Yes | Yes | No |
| Database snapshot | Yes | Yes | No |
| Always On failover cluster instances <sup>1</sup> | Yes | Yes | No |
| Always On availability groups <sup>2</sup> | Yes | No | No |
| Basic availability groups <sup>3</sup> | No | Yes | No |
| Minimum replica commit availability group | Yes | Yes | No |
| Clusterless availability group | Yes | Yes | No |
| Online page and file restore | Yes | No | No |
| Online indexing | Yes | No | No |
| Resumable online index rebuilds | Yes | No | No |
| Online schema change | Yes | No | No |
| Fast recovery | Yes | No | No |
| Mirrored backups | Yes | No | No |
| Hot add memory and CPU | Yes | No | No |
| Encrypted backup | Yes | Yes | No |
| Hybrid backup to Azure (backup to URL) | Yes | Yes | No |

<sup>1</sup> On Enterprise edition, the number of nodes is the operating system maximum. On Standard edition, there's support for two nodes.

<sup>2</sup> On Enterprise edition, provides support for up to 8 secondary replicas - including 2 synchronous secondary replicas.

<sup>3</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic Always On availability groups for a single database](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

## Scalability and performance

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Columnstore <sup>1</sup> | Yes | Yes | Yes |
| Large object binaries in clustered columnstore indexes | Yes | Yes | Yes |
| Online nonclustered columnstore index rebuild | Yes | No | No |
| In-Memory OLTP <sup>1</sup> | Yes | Yes | Yes |
| Persistent main memory | Yes | Yes | Yes |
| Table and index partitioning | Yes | Yes | Yes |
| Data compression | Yes | Yes | Yes |
| Resource governor | Yes | Yes | No |
| Partitioned table parallelism | Yes | No | No |
| NUMA aware large page memory and buffer array allocation | Yes | No | No |
| I/O resource governance | Yes | No | No |
| Delayed durability | Yes | Yes | Yes |
| Bulk insert improvements | Yes | Yes | Yes |
| `tempdb` database files on **tmpfs** filesystem | Yes | Yes | Yes |

<sup>1</sup> In-Memory OLTP data size and columnstore segment cache are limited to the amount of memory specified by edition in the [Scale limits](#scale-limits) section. The max degree of parallelism is limited. The degree of process parallelism (DOP) for an index build is limited to 2 DOP for the Standard edition and 1 DOP for Express edition. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

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
| Row-level security | Yes | Yes | Yes |
| Always Encrypted | Yes | Yes | Yes |
| Dynamic data masking | Yes | Yes | Yes |
| Basic auditing | Yes | Yes | Yes |
| Fine-grained auditing | Yes | Yes | Yes |
| Transparent data encryption (TDE) | Yes | Yes | No |
| Extensible Key Management (EKM) using Azure Key Vault | Yes | Yes | Yes |
| User-defined roles | Yes | Yes | Yes |
| Contained databases | Yes | Yes | Yes |
| Encryption for backups | Yes | Yes | No |

<a id="rdbms-manageability"></a>

## Manageability

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Dedicated admin connection | Yes | Yes | Yes <sup>1</sup> |
| PowerShell scripting support | Yes | Yes | Yes |
| Support for data-tier application component operations (extract, deploy, upgrade, delete) | Yes | Yes | Yes |
| Policy automation (check on schedule and change) | Yes | Yes | No |
| Performance data collector | Yes | Yes | No |
| Standard performance reports | Yes | Yes | No |
| Plan guides and plan freezing for plan guides | Yes | Yes | No |
| Direct query of indexed views (using `NOEXPAND` hint) | Yes | Yes | Yes |
| Automatic indexed views maintenance | Yes | Yes | No |
| Distributed partitioned views | Yes | No | No |
| Parallel index maintenance operations | Yes | No | No |
| Automatic use of indexed view by query optimizer | Yes | No | No |
| Parallel consistency check | Yes | No | No |
| SQL Server Utility Control Point | Yes | No | No |

<sup>1</sup> With trace flag.

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
| Basic R integration <sup>1</sup> | Yes | Yes | Yes |
| Advanced R integration <sup>2</sup> | Yes | No | No |
| Basic Python integration | Yes | Yes | Yes |
| Advanced Python integration | Yes | No | No |
| Java language runtime integration | Yes | Yes | Yes |
| Specification of external language in query | Yes | Yes | Yes |
| Distributed queries with linked servers <sup>5</sup> | Yes | Yes | Yes |
| External REST endpoint invocation support | Yes | Yes | Yes |
| Query Store | Yes | Yes | Yes |
| Query Store on by default for new databases | Yes | Yes | Yes |
| Query Store hints | Yes | Yes | Yes |
| Query Store on secondary replicas | Yes | No | No |
| Service Broker (messaging and queuing) | Yes | Yes | No <sup>3</sup> |
| Transact-SQL endpoints | Yes | Yes | No |
| Database mail | Yes | Yes | No |

<sup>1</sup> Basic integration is limited to 2 cores and in-memory data sets.

<sup>2</sup> Advanced integration can use all available cores for parallel processing of data sets at any size subject to hardware limits.

<sup>3</sup> Client only.

<sup>4</sup> Requires [PREVIEW_FEATURES database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).

<sup>5</sup> Using SQL Server authentication for SQL Server linked servers as target and source only.

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


## Integration Services

For info about the Integration Services (SSIS) features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services features supported by the editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

## Spatial and location services

| Feature | Enterprise | Standard | Express |
| --- | :---: | :---: | :---: |
| Spatial indexes | Yes | Yes | Yes |
| Planar and geodetic data types | Yes | Yes | Yes |
| Advanced spatial libraries | Yes | Yes | Yes |
| Import/export of industry-standard spatial data formats | Yes | Yes | Yes |

## Control group (cgroup) v2 support

[!INCLUDE [cgroup-support](includes/cgroup-support.md)]

## Unsupported features and services

The following features and services aren't available for [!INCLUDE [sssql25](../includes/sssql25-md.md)] on Linux. The support of these features will be increasingly enabled over time.

| Area | Unsupported feature or service | Comments |
| --- | --- | --- |
| **Database engine** | Merge replication | |
| | Distributed query with third-party connections | |
| | Linked servers to data sources other than [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] | [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md) to query other data sources from [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] using Transact-SQL syntax. For scenarios where PolyBase isn't helpful, submit feedback to the [Microsoft Azure forum](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0). |
| | System extended stored procedures (`xp_cmdshell`, etc.) | This feature is [deprecated](../relational-databases/extended-stored-procedures-programming/database-engine-extended-stored-procedures-programming.md). If you have specific requirements, submit feedback to the [Microsoft Azure forum](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0). |
| | FileTable, FILESTREAM | If you have specific requirements, submit feedback to the [Microsoft Azure forum](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0). |
| | CLR assemblies with the `EXTERNAL_ACCESS` or `UNSAFE` permission set | |
| | Buffer Pool Extension | |
| | Backup to URL - page blob | Backup to URL is supported for block blobs, using the [Shared Access Signature](../relational-databases/backup-restore/sql-server-backup-to-url.md#SAS). |
| **SQL Server Agent** | Subsystems: CmdExec, PowerShell, Queue Reader, SSIS, SSAS, SSRS | |
| | Alerts | |
| | Managed Backup | |
| **High Availability** | Database mirroring | This feature is [deprecated](../database-engine/database-mirroring/database-mirroring-sql-server.md). Use Always On availability groups instead. |
| **Security** | Extensible Key Management (EKM) | Extensible Key Management using Azure Key Vault is available for SQL Server on Linux environments, starting with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] CU 12. Follow the instructions from [Step 5: Configure SQL Server](../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md#step-5-configure-sql-server) onward. |
| | Windows integrated authentication for linked servers | |
| | Windows integrated authentication for availability group (AG) endpoints | Create and use certificate based endpoint authentication for availability groups. For more information, see [Configure SQL Server availability group for high availability on Linux](business-continuity/availability-groups/configure.md). |
| | Always Encrypted with secure enclaves | |
| | SQL Server on Linux deployments aren't FIPS compliant | |
| **Services** | SQL Server Browser | The SQL Server Browser service isn't required on Linux because only a single default instance is supported per host. Unlike on Windows, there are no named instances to resolve, and the port is explicitly configured during setup. |
| | SQL Server R services | SQL Server R is supported within [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], but [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] R services as a separate package isn't supported.<br /><br />You can install Machine Learning Services on Linux for [SQL Server 2019](install-upgrade/setup-machine-learning.md) and [SQL Server 2022](install-upgrade/setup-machine-learning-sql-2022.md). |
| | Analysis Services | |
| | Reporting Services | [Configure Power BI Report Server catalog databases for SQL Server on Linux](configure/power-bi-report-server-catalog.md). Run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Reporting Services (SSRS) on Windows, and host the catalog databases for SSRS on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux deployments. |

> [!NOTE]  
> The latest [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] features that depend on Azure Arc agent, including Microsoft Entra Authentication (previously known as Azure Active Directory authentication), Microsoft Purview, Pay-as-you-go for SQL Server, and Defender integration, are currently not supported for SQL Server deployed in containers. [!INCLUDE [ssazurearc-md](../includes/ssazurearc.md)] [doesn't support SQL Server running in containers](../sql-server/azure-arc/overview.md#unsupported-configurations).

[!INCLUDE [editions-supported-features-windows](../includes/editions-supported-features-windows.md)]

## Related content

- [What's new in SQL Server 2025](../sql-server/what-s-new-in-sql-server-2025.md)
- [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md)
- [SQL Server technical documentation](../sql-server/index.yml)
