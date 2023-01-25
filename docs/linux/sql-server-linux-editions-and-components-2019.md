---
title: "Editions and supported features of SQL Server 2019 - Linux"
description: "Editions and supported features of SQL Server 2019 on Linux"
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto, randolphwest
ms.date: 12/01/2022
ms.service: sql
ms.subservice: linux
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
---
# Editions and supported features of SQL Server 2019 on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides details of features supported by the various editions of SQL Server 2019 on Linux. For editions and supported features of SQL Server on Windows, see [SQL Server 2019 - Windows](../sql-server/editions-and-components-of-sql-server-2019.md).

Installation requirements vary based on your application needs. The different editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] accommodate the unique performance, runtime, and price requirements of organizations and individuals. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components that you install also depend on your specific requirements. The following sections help you understand how to make the best choice among the editions and components available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

For the latest release notes and what's new information, see the following:
- [SQL Server 2019 on Linux release notes](sql-server-linux-release-notes-2019.md)
- [What's new in SQL Server 2019 on Linux](sql-server-linux-whats-new-2019.md)

For a list of SQL Server features not available on Linux, see [Unsupported features and services](#Unsupported).

### Try SQL Server

[Download SQL Server 2019](https://www.microsoft.com/sql-server/sql-server-2019)

## [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] editions

 The following table describes the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

| [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition | Definition |
| --- | --- |
| Enterprise | The premium offering, [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Enterprise edition delivers comprehensive high-end datacenter capabilities with blazing-fast performance enabling high service levels for mission-critical workloads. |
| Standard | [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Standard edition delivers basic data management for departments and small organizations to run their applications and supports common development tools for on-premises and cloud - enabling effective database management with minimal IT resources. |
| Web | [!INCLUDE[ssNoVersion](../includes/ssNoVersion-md.md)] Web edition is a low total-cost-of-ownership option for Web hosters and Web VAPs to provide scalability, affordability, and manageability capabilities for small to large scale Web properties. |
| Developer | [!INCLUDE [ssdeveloper-md](../includes/ssdeveloper-md.md)] edition lets developers build any kind of application on top of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. It includes all the functionality of Enterprise edition, but is licensed for use as a development and test system, not as a production server. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Developer is an ideal choice for people who build and test applications. |
| Express edition | Express edition is the entry-level, free database and is ideal for learning and building desktop and small server data-driven applications. It is the best choice for independent software vendors, developers, and hobbyists building client applications. If you need more advanced database features, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Express can be seamlessly upgraded to other higher end versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. |

## Use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with client/server applications

You can install just the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] client components on a computer that is running client/server applications that connect directly to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. A client components installation is also a good option if you administer an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a database server, or if you plan to develop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] applications.

## [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components

SQL Server 2019 on Linux supports the SQL Server database engine. The following table describes the features in the database engine.

| Server components | Description |
| --- | --- |
| [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] includes the [!INCLUDE[ssDE](../includes/ssde-md.md)], the core service for storing, processing, and securing data, replication, full-text search, tools for managing relational and XML data, and in database analytics integration. |

**Developer, Enterprise Core, and  Evaluation editions**  
For features supported by Developer, Enterprise Core, and Evaluation editions, see features listed for the SQL Server Enterprise edition in the following tables.

The Developer edition continues to support only one client for [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md).

## <a id="Cross-BoxScaleLimits"></a> Scale limits

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Maximum compute capacity used by a single instance - [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)]<sup>1</sup> | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum compute capacity used by a single instance - [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] or [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] | Operating system maximum | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 16 cores | Limited to lesser of 1 socket or 4 cores |
| Maximum memory for buffer pool per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | Operating System Maximum | 128 GB | 64 GB | 1410 MB |
| Maximum capacity for [buffer pool extension](../database-engine/configure-windows/buffer-pool-extension.md) per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | 32 * (max server memory configuration) | 4 * (max server memory configuration) | N/A | N/A |
| Maximum memory for Columnstore segment cache per instance of [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | Unlimited memory | 32 GB | 16 GB | 352 MB |
| Maximum memory-optimized data size per database in [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] | Unlimited memory | 32 GB | 16 GB | 352 MB |
| Maximum relational database size | 524 PB | 524 PB | 524 PB | 10 GB |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per SQL Server instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute capacity limits by edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).

## <a id="RDBMSHA"></a> RDBMS high availability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Log shipping | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Backup compression | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Database snapshot | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Always On failover cluster instance<sup>1</sup> | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Always On availability groups<sup>2</sup> | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Basic availability groups <sup>3</sup> | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Minimum replica commit availability group | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Clusterless availability group | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Online page and file restore | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Online indexing | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Resumable online index rebuilds | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Online schema change | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Fast recovery | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Mirrored backups | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Hot add memory and CPU | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Encrypted backup | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Hybrid backup to Azure (backup to URL) | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |

<sup>1</sup> On Enterprise edition, the number of nodes is the operating system maximum. On Standard edition, there is support for two nodes.

<sup>2</sup> On Enterprise edition, provides support for up to 8 secondary replicas - including 2 synchronous secondary replicas.

<sup>3</sup> Standard edition supports basic availability groups. A basic availability group supports two replicas, with one database. For more information about basic availability groups, see [Basic Availability Groups](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

## <a id="RDBMSSP"></a> RDBMS scalability and performance

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Columnstore <sup>1</sup> | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Large object binaries in clustered columnstore indexes | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Online nonclustered columnstore index rebuild | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| In-Memory OLTP <sup>1</sup> | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Persistent Main Memory | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Table and index partitioning | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Data compression | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Resource Governor | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Partitioned Table Parallelism | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| NUMA Aware and Large Page Memory and Buffer Array Allocation | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| IO Resource Governance | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Delayed Durability | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Automatic Tuning | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Batch Mode Adaptive Joins | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Batch Mode Memory Grant Feedback | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Interleaved Execution for Multi-Statement Table Valued Functions | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Bulk insert improvements | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |

<sup>1</sup> In-Memory OLTP data size and Columnstore segment cache are limited to the amount of memory specified by edition in the Scale Limits section. The max degree of parallelism is limited. The degree of process parallelism (DOP) for an index build is limited to 2 DOP for the Standard edition and 1 DOP for the Web and Express editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

## <a id="RDBMSS"></a> RDBMS security

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Row-level security | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Always Encrypted | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Dynamic data masking | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Basic auditing | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Fine grained auditing | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Transparent database encryption (TDE) | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| User-defined roles | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Contained databases | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Encryption for backups | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |

## <a id="RDBMSM"></a> RDBMS manageability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Dedicated admin connection | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | Yes <sup>1</sup> |
| PowerShell scripting support | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Support for data-tier application component operations - extract, deploy, upgrade, delete | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Policy automation (check on schedule and change) | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Performance data collector | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Standard performance reports | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Plan guides and plan freezing for plan guides | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Direct query of indexed views (using NOEXPAND hint) | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Automatic indexed views maintenance | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Distributed partitioned views | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Parallel indexed operations | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Automatic use of indexed view by query optimizer | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Parallel consistency check | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| SQL Server Utility Control Point | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |

<sup>1</sup> With trace flag.

## <a id="Programmability"></a> Programmability

| Feature | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| JSON | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Query Store | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Temporal | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Native XML support | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| XML indexing | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| MERGE & UPSERT capabilities | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Date and Time datatypes | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Internationalization support | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Full-text and semantic search | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Specification of language in query | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Service Broker (messaging) | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | No <sup>1</sup> | No <sup>1</sup> |
| Transact-SQL endpoints | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/no-icon.svg" border="false" alt-text="No"::: |
| Graph | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |

<sup>1</sup> Client only.

## <a id="IS"></a> Integration Services

For info about the Integration Services (SSIS) features supported by the editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)], see [Integration Services features supported by the editions of SQL Server](../integration-services/integration-services-features-supported-by-the-editions-of-sql-server.md).

## <a id="SLS"></a> Spatial and location services

| Feature Name | Enterprise | Standard | Web | Express |
| --- | :---: | :---: | :---: | :---: |
| Spatial indexes | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Planar and geodetic datatypes | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Advanced spatial libraries | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Import/export of industry-standard spatial data formats | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |

## <a id="Unsupported"></a> Unsupported features & services

The following features and services aren't available for SQL Server 2019 on Linux. The support of these features will be increasingly enabled over time.

| Area | Unsupported feature or service |
| --- | --- |
| **Database engine** | Merge replication |
| | Stretch DB |
| | Distributed query with 3rd-party connections |
| | Linked Servers to data sources other than [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] |
| | System extended stored procedures (XP_CMDSHELL, etc.) |
| | Filetable, FILESTREAM |
| | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| | Buffer Pool Extension |
| | Backup to URL - page blob<sup>2</sup> |
| **SQL Server Agent** | Subsystems: CmdExec, PowerShell, Queue Reader, SSIS, SSAS, SSRS |
| | Alerts |
| | Managed Backup |
| **High Availability** | Database mirroring |
| **Security** | Extensible Key Management (EKM) |
| | Active Directory Authentication for Linked Servers |
| | Active Directory Authentication for Availability Group (AG) Endpoints |
| **Services** | SQL Server Browser |
| | SQL Server R services<sup>1</sup> |
| | StreamInsight |
| | Analysis Services |
| | Reporting Services |
| | Data Quality Services |
| | Master Data Services |

<sup>1</sup> SQL Server R is supported within SQL Server, but SQL Server R services as a separate package isn't supported.

<sup>2</sup> Backup to URL is supported for block blobs, using the [Shared Access Signature](../relational-databases/backup-restore/sql-server-backup-to-url.md#SAS).

## Next steps

- [Editions and supported features of SQL Server 2017 - Linux](sql-server-linux-editions-and-components-2017.md)
- [Editions and supported features of SQL Server 2019 - Windows](../sql-server/editions-and-components-of-sql-server-2019.md)
- [Editions and supported features of SQL Server 2017 - Windows](../sql-server/editions-and-components-of-sql-server-2017.md)
- [Editions and supported features of SQL Server 2016 - Windows](../sql-server/editions-and-components-of-sql-server-2016.md)
- [Installation for SQL Server](../database-engine/install-windows/install-sql-server.md)
- [Product Specifications for SQL Server](../sql-server/index.yml)
