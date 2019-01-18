---
title: SQL Server Replication on Linux | Microsoft Docs
description: This article describes SQL Server replication on Linux.
author: MikeRayMSFT 
ms.author: mikeray
manager: craigg
ms.date: 10/17/2018
ms.topic: article
ms.prod: sql
ms.prod_service: "database-engine"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# SQL Server Replication on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] introduces SQL Server Replication for instances of SQL Server on Linux.

Configure replication on Linux with SQL Server Management Studio (SSMS) [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

An instance of SQL Server can participate in any replication role:

* Publisher
* Distributor
* Subscriber

A replication schema can mix and match operating system platforms. For example, a replication schema may include an instance of SQL Server on Linux for publisher and distributor, and the subscribers include instances of SQL Server on Windows as well as Linux.

SQL Server instances on Linux can participate in any type of replication.

* Transactional
* Merge
* Snapshot

For detailed information about replication, see [SQL Server replication documentation](../relational-databases/replication/sql-server-replication.md).

## Supported features

For [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] the following replication features are supported:

* Snapshot replication
* Transactional replication
* Merge replication
* Peer-to-Peer replication
* Replication with non-default ports <!--Add link to explanation-->
* Replication with AD authentication
* Replication configurations across Windows and Linux
* Immediate updates for transactional replication

## Limitations

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] does not support the following features:

* Immediate update subscribers
* Oracle publishing

## Next steps

[Configure SQL Server replication on Linux](sql-server-linux-replication-tutorial-tsql.md)

[Sample: Configure SQL Server replication on Linux](sql-server-linux-replication-configure.md)
