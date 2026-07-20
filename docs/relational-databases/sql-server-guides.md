---
title: SQL Server Guides
description: SQL Server internals and architecture guides.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "guide"
  - "guide, list"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# SQL Server internals and architecture guides

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

The following guides are available. They discuss general concepts and apply to all platforms that use the SQL Database Engine, unless stated otherwise in the respective guide.

- [Concurrency, locking, and contention](#concurrency-locking-and-contention)
- [Storage engine architecture and I/O](#storage-engine-architecture-and-io)
- [Query execution and optimization](#query-execution-and-optimization)
- [Memory, threads, and internal scheduling](#memory-threads-and-internal-scheduling)
- [High availability, migration, and validation](#high-availability-migration-and-validation)
- [Connectivity and authentication](#connectivity-and-authentication)

## Concurrency, locking, and contention

Use these guides to understand how SQL Server manages concurrent access to data and internal structures, and how to diagnose contention-related issues.

| Guide | Description |
| --- | --- |
| [Transaction locking and row versioning guide](sql-server-transaction-locking-and-row-versioning-guide.md) | Explains the locking and row versioning mechanisms that SQL Server uses to preserve transaction integrity. Describes how applications can efficiently control transactions. |
| [Deadlocks guide](sql-server-deadlocks-guide.md) | Deep dive on Database Engine deadlocks that competing locks cause. Explains how deadlocks form and how SQL Server detects and breaks them. |
| [Diagnose and resolve latch contention on SQL Server](diagnose-resolve-latch-contention.md) | Focuses on identifying and resolving latch contention (notably page latch contention) in high-concurrency SQL Server workloads. |
| [Diagnose and resolve spinlock contention on SQL Server](diagnose-resolve-spinlock-contention.md) | In-depth guide on identifying and resolving spinlock contention in high-concurrency SQL Server workloads. |

## Storage engine architecture and I/O

Use these guides to understand how SQL Server stores, accesses, and maintains data on disk.

| Guide | Description |
| --- | --- |
| [Page and extent architecture guide](pages-and-extents-architecture-guide.md) | Describes page and extent structures and how pages and extents are organized within data files. |
| [SQL Server I/O fundamentals](sql-server-storage-guide.md) | Explains why I/O is core to the engine and discusses efficiency articles such as drive caching principles. It also discusses I/O reliability requirements. |
| [SQL Server transaction log architecture and management guide](sql-server-transaction-log-architecture-and-management-guide.md) | Explains the transaction log's role and provides details on the physical and logical architecture of the log. |
| [Ghost cleanup process guide](ghost-record-cleanup-process-guide.md) | Describes ghost cleanup as a background process that physically removes rows previously marked for deletion. |

## Query execution and optimization

Use these guides to understand how SQL Server compiles, optimizes, and executes queries.

| Guide | Description |
| --- | --- |
| [Query processing architecture guide](query-processing-architecture-guide.md) | Describes how the Database Engine processes queries across storage architectures. It covers optimization and reuse through execution plan caching. |
| [Index architecture and design guide](sql-server-index-design-guide.md) | Covers index architecture and fundamentals. It provides best practices for designing effective indexes. |

## Memory, threads, and internal scheduling

Use these guides to understand how SQL Server manages memory and CPU resources internally.

| Guide | Description |
| --- | --- |
| [Memory management architecture guide](memory-management-architecture-guide.md) | Describes SQL Server memory architecture and how SQL Server acquires and uses memory. It includes background on OS virtual memory. |
| [Thread and task architecture guide](thread-and-task-architecture-guide.md) | Describes threading and task concepts in the context of OS scheduling. It explains how work is executed through threads and tasks. |
| [Security cache concepts](security-cache.md) | Explains how SQL Server uses a security cache to validate permissions for principals accessing securables. |

## High availability, migration, and validation

Use these guides when deploying, migrating, or operating SQL Server in production environments.

| Guide | Description |
| --- | --- |
| [Always On availability groups troubleshooting and monitoring guide](/previous-versions/sql/sql-server-guides/dn135328(v=sql.110)) | A troubleshooting and monitoring guide that also explicitly serves as a landing page pointing to other published resources for common AG scenarios and tools. |
| [Post-migration validation and optimization guide](post-migration-validation-and-optimization-guide.md) | Frames post-migration as reconciling data accuracy and completeness and uncovering performance issues, then enumerates common post-migration performance scenarios. |

## Connectivity and authentication

Use this guide to understand how clients authenticate and connect to SQL Server.

| Guide | Description |
| --- | --- |
| [Trace the network authentication process to the Database Engine](database-engine-connection-open-network-trace.md) | Walks through network traces that capture TCP connection establishment handshakes and authentication sequences between client and server. |
