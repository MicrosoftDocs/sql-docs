---
title: "Editions and Supported Features of SQL Server 2017 - Linux"
description: This article describes editions, features, and components supported by the various editions of SQL Server 2017 on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 11/27/2025
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
  - build-2025
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
# Editions and supported features of SQL Server 2017 on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides details of features supported by the various editions of [!INCLUDE [sssql17](../includes/sssql17-md.md)] on Linux.

For editions and supported features of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Windows, see [Editions and supported features of SQL Server 2017](../sql-server/editions-and-components-of-sql-server-2017.md). For more information on what's new in [!INCLUDE [sssql17](../includes/sssql17-md.md)] on Windows, see [What's new in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md).

Installation requirements vary based on your application needs. The different editions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

For more information, see [Release information for SQL Server on Linux](sql-server-linux-release-notes.md).

For a list of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] features not available on Linux, see [Unsupported features and services](#unsupported-features-and-services).

## Try SQL Server

- [Download SQL Server 2017](https://www.microsoft.com/sql-server/sql-server-2017)

## SQL Server editions

[!INCLUDE [sql-server-editions](../includes/paragraph-content/sql-server-editions-1.md)]

## Use SQL Server with client/server applications

You can install just the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] client components on a computer running client/server applications that connect directly to an instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] applications.

## SQL Server components

[!INCLUDE [sssql17](../includes/sssql17-md.md)] on Linux supports the [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)]. The following table describes the features in the [!INCLUDE [ssDE](../includes/ssde-md.md)].

| Server components | Description |
| --- | --- |
| SQL Server Database Engine | [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE [ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, Full-Text Search, tools for managing relational and XML data, and in-database analytics integration. |

### Developer, Enterprise Core, and Evaluation editions

For features supported by Developer, Enterprise Core, and Evaluation editions, see features listed for the SQL Server Enterprise edition in the following tables.

The Developer edition continues to support only one client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md).

## Scale limits

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Maximum compute capacity used by a single instance - SQL Server Database Engine <sup>1</sup> | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum memory for buffer pool per instance of SQL Server Database Engine | Operating system maximum | 128&nbsp;GB | 64&nbsp;GB | 1,410&nbsp;MB |
| Maximum memory for columnstore segment cache per instance of SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 16&nbsp;GB | 352&nbsp;MB |
| Maximum memory-optimized data size per database in SQL Server Database Engine | Unlimited memory | 32&nbsp;GB | 16&nbsp;GB | 352&nbsp;MB |
| Maximum relational database size | 524&nbsp;PB | 524&nbsp;PB | 524&nbsp;PB | 10&nbsp;GB |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL)-based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).

<a id="rdbms-high-availability"></a>

## High availability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Log shipping | Yes | Yes | Yes | No |
| Backup compression | Yes | Yes | No | No |
| Database snapshot | Yes | No | No | No |
| Always On failover cluster instances <sup>1</sup> | Yes | Yes | No | No |
| Always On availability groups <sup>2</sup> | Yes | No | No | No |
| Basic availability groups <sup>3</sup> | No | Yes | No | No |
| Minimum replica commit availability group | Yes | Yes | No | No |
| Clusterless availability group | Yes | Yes | No | No |
| Online page and file restore | Yes | No | No | No |
| Online indexing | Yes | No | No | No |
| Resumable online index rebuilds | Yes | No | No | No |
| Online schema change | Yes | No | No | No |
| Fast recovery | Yes | No | No | No |
| Mirrored backups | Yes | No | No | No |
| Hot add memory and CPU | Yes | No | No | No |
| Encrypted backup | Yes | Yes | No | No |
| Hybrid backup to Azure (backup to URL) | Yes | Yes | No | No |

<sup>1</sup> On Enterprise edition, the number of nodes is the operating system maximum. On Standard edition, there's support for two nodes.

<sup>2</sup> Enterprise edition supports up to 8 secondary replicas, including 2 synchronous secondary replicas.

<sup>3</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic Always On availability groups for a single database](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

## Scalability and performance

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Columnstore <sup>1</sup> | Yes | Yes | Yes | Yes |
| Large object binaries in clustered columnstore indexes | Yes | Yes | Yes | Yes |
| Online nonclustered columnstore index rebuild | Yes | No | No | No |
| In-Memory OLTP <sup>1</sup> | Yes | Yes | Yes | Yes |
| Persistent main memory | Yes | Yes | Yes | Yes |
| Table and index partitioning | Yes | Yes | Yes | Yes |
| Data compression | Yes | Yes | Yes | Yes |
| Resource governor | Yes | No | No | No |
| Partitioned table parallelism | Yes | No | No | No |
| NUMA-aware large page memory and buffer array allocation | Yes | No | No | No |
| I/O resource governance | Yes | No | No | No |
| Delayed durability | Yes | Yes | Yes | Yes |
| Bulk insert improvements | Yes | Yes | Yes | Yes |

<sup>1</sup> In-Memory OLTP data size and columnstore segment cache are limited to the amount of memory specified by edition in the [Scale limits](#scale-limits) section. The max degree of parallelism is limited. The degree of process parallelism (DOP) for an index build is limited to 2 DOP for the Standard edition and 1 DOP for the Web and Express editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

## Intelligent query processing

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Automatic tuning | Yes | No | No | No |
| Batch mode adaptive joins | Yes | No | No | No |
| Batch mode memory grant feedback | Yes | No | No | No |
| Interleaved execution for multi-statement table-valued functions | Yes | Yes | Yes | Yes |

## Security

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Row-level security | Yes | Yes | Yes | Yes |
| Always Encrypted | Yes | Yes | Yes | Yes |
| Dynamic data masking | Yes | Yes | Yes | Yes |
| Basic auditing | Yes | Yes | Yes | Yes |
| Fine-grained auditing | Yes | Yes | Yes | Yes |
| Transparent data encryption (TDE) | Yes | No | No | No |
| User-defined roles | Yes | Yes | Yes | Yes |
| Contained databases | Yes | Yes | Yes | Yes |
| Encryption for backups | Yes | Yes | No | No |

<a id="rdbms-manageability"></a>

## Manageability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Dedicated admin connection | Yes | Yes | Yes | Yes <sup>1</sup> |
| PowerShell scripting support | Yes | Yes | Yes | Yes |
| Support for data-tier application component operations (extract, deploy, upgrade, delete) | Yes | Yes | Yes | Yes |
| Policy automation (check on schedule and change) | Yes | Yes | Yes | No |
| Performance data collector | Yes | Yes | Yes | No |
| Standard performance reports | Yes | Yes | Yes | No |
| Plan guides and plan freezing for plan guides | Yes | Yes | Yes | No |
| Direct query of indexed views (using `NOEXPAND` hint) | Yes | Yes | Yes | Yes |
| Automatic indexed views maintenance | Yes | Yes | Yes | No |
| Distributed partitioned views | Yes | No | No | No |
| Parallel index maintenance operations | Yes | No | No | No |
| Automatic use of indexed view by query optimizer | Yes | No | No | No |
| Parallel consistency check | Yes | No | No | No |
| SQL Server Utility Control Point | Yes | No | No | No |

<sup>1</sup> With trace flag.

## Programmability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| JSON | Yes | Yes | Yes | Yes |
| Query Store | Yes | Yes | Yes | Yes |
| Temporal | Yes | Yes | Yes | Yes |
| Native XML support | Yes | Yes | Yes | Yes |
| XML indexing | Yes | Yes | Yes | Yes |
| `MERGE` and upsert capabilities | Yes | Yes | Yes | Yes |
| Date and time data types | Yes | Yes | Yes | Yes |
| Internationalization support | Yes | Yes | Yes | Yes |
| Full-text and semantic search | Yes | Yes | Yes | Yes |
| Specification of language in query | Yes | Yes | Yes | Yes |
| Service Broker (messaging and queuing) | Yes | Yes | No <sup>1</sup> | No <sup>1</sup> |
| Transact-SQL endpoints | Yes | Yes | Yes | No |
| Graph | Yes | Yes | Yes | Yes |

<sup>1</sup> Client only.

## Integration Services

For info about the Integration Services (SSIS) features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services features supported by the editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

## Spatial and location services

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Spatial indexes | Yes | Yes | Yes | Yes |
| Planar and geodetic data types | Yes | Yes | Yes | Yes |
| Advanced spatial libraries | Yes | Yes | Yes | Yes |
| Import/export of industry-standard spatial data formats | Yes | Yes | Yes | Yes |

## Unsupported features and services

The following features and services aren't available for [!INCLUDE [sssql17](../includes/sssql17-md.md)] on Linux.

| Area | Unsupported feature or service | Comments |
| --- | --- | --- |
| **Database Engine** | Merge replication | |
| | Stretch DB | This feature is [deprecated](/previous-versions/sql/sql-server/stretch-database/stretch-database) in [!INCLUDE [sssql22](../includes/sssql22-md.md)], and isn't supported. |
| | PolyBase | Supported on [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and later versions. |
| | Distributed query with third-party connections | |
| | Linked servers to data sources other than [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] | [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md) to query other data sources from [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], using T-SQL syntax. For scenarios where PolyBase isn't helpful, submit feedback to the [Microsoft Azure forum](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0). |
| | System extended stored procedures (`xp_cmdshell`, etc.) | This feature is [deprecated](../relational-databases/extended-stored-procedures-programming/database-engine-extended-stored-procedures-programming.md). If you have specific requirements, submit feedback to the [Microsoft Azure forum](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0). |
| | FileTable, FILESTREAM | If you have specific requirements, submit feedback to the [Microsoft Azure forum](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0). |
| | CLR assemblies with the `EXTERNAL_ACCESS` or `UNSAFE` permission set | |
| | Buffer Pool Extension | |
| | Backup to URL - page blob | Backup to URL is supported for block blobs, using the [Shared Access Signature](../relational-databases/backup-restore/sql-server-backup-to-url.md#SAS). |
| **SQL Server Agent** | Subsystems: CmdExec, PowerShell, Queue Reader, SSIS, SSAS, SSRS | |
| | Alerts | |
| | Log Reader Agent | |
| | Managed Backup | |
| **High Availability** | Database mirroring | This feature is [deprecated](../database-engine/database-mirroring/database-mirroring-sql-server.md). Use Always On availability groups instead. |
| **Security** | Extensible Key Management (EKM) | |
| | Windows integrated authentication for linked servers | |
| | Windows integrated authentication for availability group (AG) endpoints | Create and use certificate-based endpoint authentication for availability groups. For more information, see [Configure SQL Server availability group for high availability on Linux](business-continuity/availability-groups/configure.md). |
| | SQL Server on Linux deployments aren't FIPS compliant | |
| **Services** | SQL Server Browser | The SQL Server Browser service isn't required on Linux because only a single default instance is supported per host. Unlike on Windows, there are no named instances to resolve, and the port is explicitly configured during setup. |
| | SQL Server R Services | SQL Server R is supported within [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], but [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] R Services as a separate package isn't supported.<br /><br />You can install Machine Learning Services on Linux for [SQL Server 2019](install-upgrade/setup-machine-learning.md) and [SQL Server 2022](install-upgrade/setup-machine-learning-sql-2022.md). |
| | Analysis Services | |
| | Reporting Services | On [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and later versions, [configure Power BI Report Server catalog databases for SQL Server on Linux](configure/power-bi-report-server-catalog.md). Run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Reporting Services (SSRS) on Windows, and host the catalog databases for SSRS on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux deployments. |
| | Data Quality Services | Deprecated feature. |
| | Master Data Services | Deprecated feature. |

[!INCLUDE [editions-supported-features-windows](../includes/editions-supported-features-windows.md)]

## Related content

- [What's new in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)
- [Installation guidance for SQL Server on Linux](install-upgrade/setup.md)
- [SQL Server technical documentation](../sql-server/index.yml)
