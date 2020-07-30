---
title: SQL Server Replication on Linux
description: This article describes SQL Server replication on Linux.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: vanto
ms.date: 12/09/2019
ms.topic: article
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: linux
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=sqlallproducts-allversions"
---
# SQL Server Replication on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

[!INCLUDE[SQL Server 2017](../includes/sssqlv14-md.md)] ([CU18](https://support.microsoft.com/help/4527377)) and later support SQL Server Replication for instances of SQL Server on Linux.

Configure replication on Linux with SQL Server Management Studio (SSMS) [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

An instance of SQL Server can participate in any replication role:

* Publisher
* Distributor
* Subscriber

A replication schema can mix and match operating system platforms. For example, a replication schema may include an instance of SQL Server on Linux for publisher and distributor, and the subscribers include instances of SQL Server on Windows as well as Linux.

SQL Server instances on Linux can participate in any type of replication.

* Transactional
* Snapshot

For detailed information about replication, see [SQL Server replication documentation](../relational-databases/replication/sql-server-replication.md).

## Supported features

The following replication features are supported:

* Snapshot replication
* Transactional replication
* Replication with non-default ports <!--Add link to explanation-->
* Replication with AD authentication
* Replication configurations across Windows and Linux
* Immediate updates for transactional replication

## Limitations

The following features are not supported:

* Merge replication
* Peer-to-Peer replication
* Oracle publishing

## Next steps

[Configure SQL Server replication on Linux](sql-server-linux-replication-tutorial-tsql.md)

[Sample: Configure SQL Server replication on Linux](sql-server-linux-replication-configure.md)
