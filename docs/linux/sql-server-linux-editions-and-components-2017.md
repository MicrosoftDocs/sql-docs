---
title: "Editions and supported features of SQL Server 2017 - Linux"
description: This article describes features supported by the various editions of SQL Server 2017 on Linux. It helps you choose from available editions and components.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 11/16/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
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

This article provides details of features supported by the various editions of SQL Server 2017 on Linux. For editions and supported features of SQL Server on Windows, see [Editions and supported features of SQL Server 2017](../sql-server/editions-and-components-of-sql-server-2017.md).

Installation requirements vary based on your application needs. The different editions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)].

For the latest release notes and what's new information, see the following:

- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md)
- [What's new for SQL Server 2017 on Linux](sql-server-linux-whats-new.md)

For a list of SQL Server features not available on Linux, see [Unsupported features and services](#Unsupported).

### Try SQL Server

[Download SQL Server 2017](https://www.microsoft.com/sql-server/sql-server-2017)

## SQL Server editions

[!INCLUDE [sql-server-editions](../includes/paragraph-content/sql-server-editions.md)]

## Use SQL Server with client/server applications

You can install just the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] client components on a computer that is running client/server applications that connect directly to an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] applications.

## SQL Server components

SQL Server 2017 on Linux supports the SQL Server database engine. The following table describes the features in the database engine.

| Server components | Description |
| --- | --- |
| [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] | [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE [ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data, and in database analytics integration. |

**Developer, Enterprise Core, and  Evaluation editions**  
For features supported by Developer, Enterprise Core, and Evaluation editions, see features listed for the SQL Server Enterprise edition in the following tables.

The Developer edition continues to support only one client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md).

## <a id="Cross-BoxScaleLimits"></a> Scale limits

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Maximum compute capacity used by a single instance - [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] <sup>1</sup> | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum compute capacity used by a single instance - [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)] or [!INCLUDE [ssRSnoversion](../includes/ssrsnoversion-md.md)] | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum memory for buffer pool per instance of [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] | Operating System Maximum | 128 GB | 64 GB | 1410 MB |
| Maximum capacity for [Buffer pool extension](../database-engine/configure-windows/buffer-pool-extension.md) per instance of [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] | 32 * (max server memory configuration) | 4 * (max server memory configuration) | N/A | N/A |
| Maximum memory for Columnstore segment cache per instance of [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] | Unlimited memory | 32 GB | 16 GB | 352 MB |
| Maximum memory-optimized data size per database in [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] | Unlimited memory | 32 GB | 16 GB | 352 MB |
| Maximum relational database size | 524 PB | 524 PB | 524 PB | 10 GB |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).

## <a id="RDBMSHA"></a> RDBMS high availability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Log shipping | Yes | Yes | Yes | No |
| Backup compression | Yes | Yes | No | No |
| Database snapshot | Yes | No | No | No |
| Always On failover cluster instance <sup>1</sup> | Yes | Yes | No | No |
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

<sup>2</sup> On Enterprise edition, provides support for up to 8 secondary replicas - including 2 synchronous secondary replicas.

<sup>3</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic Always On availability groups for a single database](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

## <a id="RDBMSSP"></a> RDBMS scalability and performance

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Columnstore <sup>1</sup> | Yes | Yes | Yes | Yes |
| Large object binaries in clustered columnstore indexes | Yes | Yes | Yes | Yes |
| Online nonclustered columnstore index rebuild | Yes | No | No | No |
| In-Memory OLTP <sup>1</sup> | Yes | Yes | Yes | Yes |
| Persistent Main Memory | Yes | Yes | Yes | Yes |
| Table and index partitioning | Yes | Yes | Yes | Yes |
| Data compression | Yes | Yes | Yes | Yes |
| Resource Governor | Yes | No | No | No |
| Partitioned Table Parallelism | Yes | No | No | No |
| NUMA Aware and Large Page Memory and Buffer Array Allocation | Yes | No | No | No |
| IO Resource Governance | Yes | No | No | No |
| Delayed Durability | Yes | Yes | Yes | Yes |
| Automatic tuning | Yes | No | No | No |
| Batch Mode Adaptive Joins | Yes | No | No | No |
| Batch Mode Memory Grant Feedback | Yes | No | No | No |
| Interleaved Execution for Multi-Statement Table Valued Functions | Yes | Yes | Yes | Yes |
| Bulk insert improvements | Yes | Yes | Yes | Yes |

<sup>1</sup> In-Memory OLTP data size and Columnstore segment cache are limited to the amount of memory specified by edition in the Scale Limits section. The max degree of parallelism is limited. The degree of process parallelism (DOP) for an index build is limited to 2 DOP for the Standard edition and 1 DOP for the Web and Express editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

## <a id="RDBMSS"></a> RDBMS security

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Row-level security | Yes | Yes | Yes | Yes |
| Always Encrypted | Yes | Yes | Yes | Yes |
| Dynamic data masking | Yes | Yes | Yes | Yes |
| Basic auditing | Yes | Yes | Yes | Yes |
| Fine-grained auditing | Yes | Yes | Yes | Yes |
| Transparent database encryption | Yes | No | No | No |
| User-defined roles | Yes | Yes | Yes | Yes |
| Contained databases | Yes | Yes | Yes | Yes |
| Encryption for backups | Yes | Yes | No | No |

## <a id="RDBMSM"></a> RDBMS manageability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Dedicated admin connection | Yes | Yes | Yes | Yes. <sup>1</sup> |
| PowerShell scripting support | Yes | Yes | Yes | Yes |
| Support for data-tier application component operations - extract, deploy, upgrade, delete | Yes | Yes | Yes | Yes |
| Policy automation (check on schedule and change) | Yes | Yes | Yes | No |
| Performance data collector | Yes | Yes | Yes | No |
| Standard performance reports | Yes | Yes | Yes | No |
| Plan guides and plan freezing for plan guides | Yes | Yes | Yes | No |
| Direct query of indexed views (using NOEXPAND hint) | Yes | Yes | Yes | Yes |
| Automatic indexed views maintenance | Yes | Yes | Yes | No |
| Distributed partitioned views | Yes | No | No | No |
| Parallel index operations | Yes | No | No | No |
| Automatic use of indexed view by query optimizer | Yes | No | No | No |
| Parallel consistency check | Yes | No | No | No |
| SQL Server Utility Control Point | Yes | No | No | No |

<sup>1</sup> With trace flag

## <a id="Programmability"></a> Programmability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| JSON | Yes | Yes | Yes | Yes |
| Query Store | Yes | Yes | Yes | Yes |
| Temporal | Yes | Yes | Yes | Yes |
| Native XML support | Yes | Yes | Yes | Yes |
| XML indexing | Yes | Yes | Yes | Yes |
| MERGE and UPSERT capabilities | Yes | Yes | Yes | Yes |
| Date and Time datatypes | Yes | Yes | Yes | Yes |
| Internationalization support | Yes | Yes | Yes | Yes |
| Full-text and semantic search | Yes | Yes | Yes | Yes |
| Specification of language in query | Yes | Yes | Yes | Yes |
| Service Broker (messaging) | Yes | Yes | No <sup>1</sup> | No <sup>1</sup> |
| Transact-SQL endpoints | Yes | Yes | Yes | No |
| Graph | Yes | Yes | Yes | Yes |

<sup>1</sup> Client only

## <a id="IS"></a> Integration Services

For info about the Integration Services (SSIS) features supported by the editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services features supported by the editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

## <a id="SLS"></a> Spatial and location services

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Spatial indexes | Yes | Yes | Yes | Yes |
| Planar and geodetic datatypes | Yes | Yes | Yes | Yes |
| Advanced spatial libraries | Yes | Yes | Yes | Yes |
| Import/export of industry-standard spatial data formats | Yes | Yes | Yes | Yes |

## <a id="Unsupported"></a> Unsupported features and services

The following features and services aren't available for SQL Server 2017 on Linux. The support of these features will be increasingly enabled over time.

| Area | Unsupported feature or service |
| --- | --- |
| **Database engine** | Merge replication |
| | Stretch DB |
| | PolyBase |
| | Distributed query with 3rd-party connections |
| | Linked Servers to data sources other than [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] |
| | System extended stored procedures (XP_CMDSHELL, etc.) |
| | FileTable, FILESTREAM |
| | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| | Buffer Pool Extension |
| | Backup to URL - page blob <sup>1</sup> |
| **SQL Server Agent** | Subsystems: CmdExec, PowerShell, Queue Reader, SSIS, SSAS, SSRS |
| | Alerts |
| | Log Reader Agent |
| | Managed Backup |
| **High Availability** | Database mirroring |
| **Security** | Extensible Key Management |
| | Active Directory Authentication for Linked Servers |
| | Active Directory Authentication for Availability Group (AG) Endpoints |
| **Services** | SQL Server Browser |
| | SQL Server R services |
| | StreamInsight |
| | Analysis Services |
| | Reporting Services |
| | Data Quality Services |
| | Master Data Services |

<sup>1</sup> Backup to URL is supported for block blobs, using the [Shared Access Signature](../relational-databases/backup-restore/sql-server-backup-to-url.md#SAS).

[!INCLUDE [editions-supported-features-windows](../includes/editions-supported-features-windows.md)]

## Related content

- [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md)
- [Product Specifications for SQL Server](../sql-server/index.yml)
