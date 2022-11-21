---
title: "Failed availability group add-file operation"
description: Troubleshoot failed add-file operations in an Always On availability group. File paths can differ between systems that host the primary and secondary replicas.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: troubleshooting
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "secondary databases [SQL Server], Availability Groups"
  - "Availability Groups [SQL Server], troubleshooting"
---
# Troubleshoot a Failed Add-File Operation (Always On Availability Groups)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  In some Always On availability group deployments, file paths differ between the system that hosts the primary replica and systems that host a secondary replica. If the file path of an add-file operation does not exist on a secondary replica, the add-file operation will succeed on the primary database. But the add-file operation will cause the secondary database to be suspended. This, in turn, causes the secondary replica to enter the NOT SYNCHRONIZING state.  
  
> [!NOTE]  
>  We recommend that, if possible, the file path (including the drive letter) of a given secondary database be identical to the path of the corresponding primary database.  
  
## Problem Resolution  
 To resolve this problem the database owner must complete the following steps:  
  
1.  Remove the secondary database from the availability group. For more information, see [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-database-from-an-availability-group-sql-server.md).  
  
2.  On the existing secondary database, restore a full backup of the filegroup that contains the added file to the secondary database, using WITH NORECOVERY and WITH MOVE (specifying the file path on the server instance that hosts the secondary replica). For more information, see [Restore a Database to a New Location &#40;SQL Server&#41;](../../../relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md).  
  
3.  Back up the transaction log that contains the add-file operation on the primary database, and manually restore the log backup on the secondary database using WITH NORECOVERY and WITH MOVE.  
  
4.  Prepare the secondary database for re-joining the availability group, by restoring, WITH NO RECOVERY, any other outstanding log backups from the primary database.  
  
5.  Rejoin the secondary database to the availability group. For more information, see [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)   
 [Troubleshoot Orphaned Users &#40;SQL Server&#41;](../../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md)   
 [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)
