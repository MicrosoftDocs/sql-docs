---
title: "In-Memory Database | Microsoft Docs"
ms.date: 05/22/2019
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "in-memory database"
  - "feature, in-memory database"
  - "in-memory"
ms.assetid: 11f8017e-5bc3-4bab-8060-c16282cfbac1
author: "briancarrig"
ms.author: "brcarrig"
manager: "amitban"
---

# In-Memory Database

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In-Memory Database is an umbrella term for the features within SQL Server that leverage in-memory based technologies. As new in-memory based features are developed this page will continue to be updated.

## Hybrid Buffer Pool

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

[Hybrid Buffer Pool](../database-engine/configure-windows/hybrid-buffer-pool.md) allows the database engine to directly access data pages in database files stored on Persistent Memory (PMEM) devices.

## Memory-Optimized TempDB Metadata

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces a new feature that is [memory-optimized tempdb metadata](./databases/tempdb-database.md#memory-optimized-tempdb-metadata), which effectively removes some contention bottlenecks and unlocks a new level of scalability for tempdb-heavy workloads.

## In-Memory OLTP

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

[In-Memory OLTP](./in-memory-oltp/in-memory-oltp-in-memory-optimization.md) is the premier technology available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../includes/sssds-md.md)] for optimizing performance of transaction processing, data ingestion, data load, and transient data scenarios.

**Applies to:** [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

## Persistent Memory Support for Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

[!INCLUDE[sqlv15](../includes/sssqlv15-md.md)] adds support for Persistent Memory (PMEM) devices to Linux, providing full enlightenment of data and transaction log files placed on [persistent memory](../linux/sql-server-linux-configure-pmem.md).

**Applies to:** [!INCLUDE[sqlv15](../includes/sssqlv15-md.md)] to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].
