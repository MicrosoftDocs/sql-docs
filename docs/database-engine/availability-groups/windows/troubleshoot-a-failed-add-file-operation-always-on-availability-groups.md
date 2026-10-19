---
title: Failed Availability Group Add-File Operation
description: Troubleshoot failed add-file operations in an Always On availability group. File paths can differ between systems that host the primary and secondary replicas.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 11/18/2025
ms.service: sql
ms.subservice: availability-groups
ms.topic: troubleshooting
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "secondary databases [SQL Server], Availability Groups"
  - "Availability Groups [SQL Server], troubleshooting"
---
# Troubleshoot a failed add-file operation (Always On availability groups)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

In some Always On availability group deployments, file paths differ between the system that hosts the primary replica and systems that host a secondary replica. If the file path of an add-file operation doesn't exist on a secondary replica, the add-file operation will succeed on the primary database. But the add-file operation will cause the secondary database to be suspended. This, in turn, causes the secondary replica to enter the `NOT SYNCHRONIZING` state.

> [!NOTE]  
> If possible, the file path (including the drive letter) of a given secondary database should be identical to the path of the corresponding primary database.

## Problem resolution

To resolve this problem the database owner must complete the following steps:

1. Remove the secondary database from the availability group. For more information, see [Remove a Secondary Database from an Availability Group (SQL Server)](remove-a-secondary-database-from-an-availability-group-sql-server.md).

1. On the existing secondary database, restore a full backup of the filegroup that contains the added file to the secondary database, using WITH `NORECOVERY` and `WITH MOVE` (specifying the file path on the server instance that hosts the secondary replica). For more information, see [Restore a database to a new location (SQL Server)](../../../relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md).

1. Back up the transaction log that contains the add-file operation on the primary database, and manually restore the log backup on the secondary database using WITH `NORECOVERY` and `WITH MOVE`.

1. Prepare the secondary database for re-joining the availability group, by restoring, `WITH NO RECOVERY`, any other outstanding log backups from the primary database.

1. Rejoin the secondary database to the availability group. For more information, see [Join a secondary database to an Always On availability group](join-a-secondary-database-to-an-availability-group-sql-server.md).

## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Prepare a secondary database for an Always On availability group](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)
- [Troubleshoot orphaned users (SQL Server)](../../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md)
- [Troubleshoot Always On Availability Groups Configuration (SQL Server)](troubleshoot-always-on-availability-groups-configuration-sql-server.md)
