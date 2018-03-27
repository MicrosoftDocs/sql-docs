---
title: SQL Server Replication on Linux | Microsoft Docs
description: This article describes SQL Server replication on Linux.
author: MikeRayMSFT 
ms.author: mikeray
manager: craigg
ms.date: 03/20/2018
ms.topic: article
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: database-engine
ms.assetid: 
ms.workload: "On Demand"
---
# SQL Server Replication on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

[!INCLUDE[sqlservervnext](../includes/sssqlv15-md.md)] CTP 2.0 introduces SQL Server Replication for instances of SQL Server on Linux. At this time, SQL Server Replication on Linux is a preview feature.

Configure replication on Linux with [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md). You cannot configure replication for SQL Server on Linux with SQL Server Management Studios (SSMS).  

An instance of SQL Server can participate in any replication role:
* Publisher
* Distributor
* Subscriber

A replication schema can mix and match operating system platforms. For example, a replication schema can use instances of SQL Server on Linux for publisher and subscriber, and the distributor can be hosted on an instance of SQL Server on Windows.

SQL Server instances on Linux can participate in any type of replication.
* Transactional 
* Merge
* Snapshot

## Next steps
[Sample: Configure SQL Server replication on Linux](sql-server-linux-replication-configure.md)