---
title: "Create a Database Snapshot for an Availability Group"
description: "Describes how to create a database snapshot for a database within an Always On availability group on either the primary or secondary database."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 01/19/2026
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
helpviewer_keywords:
  - "database snapshots [SQL Server], AlwaysOn Availability Groups"
  - "database snapshots [SQL Server], Always On Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
---
# Database snapshots with Always On availability groups (SQL Server)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

You can create a database snapshot on a primary or secondary database in an availability group. The replica role must be either `PRIMARY` or `SECONDARY`, and can't be in the `RESOLVING` state.

> [!NOTE]  
> Creating database snapshots on any database adds CPU and I/O overhead because of copy-on-write activity. On database replicas, this overhead can reduce redo throughput and affect other operations, especially as the number of snapshots increases.

You should create database snapshots when the database synchronization state is `SYNCHRONIZING` or `SYNCHRONIZED`. However, you can still create database snapshots when the database synchronization state is `NOT SYNCHRONIZING`.

A database snapshot on a secondary replica continues to work if the replica is `DISCONNECTED` from the primary replica.

Some [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] conditions cause both the source database and its database snapshots to restart, temporarily disconnecting users. These conditions are as follows:

- The primary replica changes roles. This change can happen because the current primary replica goes offline and comes back online on the same server instance, or because the availability group fails over.

- The database enters the secondary role.

If the availability replica that hosts database snapshots fails over, the database snapshots remain on the server instance where you created them. You can continue to use the snapshots after the failover. If performance is a concern in your environment, create database snapshots only on secondary databases hosted by a secondary replica that is configured for manual failover mode.

If you ever manually fail over the availability group to this secondary replica, you can create a new set of database snapshots on another secondary replica, redirect clients to the new database snapshots, and drop all of the database snapshots from the now primary databases.

## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Database snapshots (SQL Server)](../../../relational-databases/databases/database-snapshots-sql-server.md)
