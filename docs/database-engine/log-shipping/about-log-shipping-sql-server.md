---
title: "About log shipping (SQL Server)"
description: Learn about SQL Server log shipping, which sends transaction log backups from a primary database on a primary server instance to secondary databases.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/05/2023
ms.service: sql
ms.subservice: log-shipping
ms.topic: conceptual
helpviewer_keywords:
  - "secondary servers [SQL Server]"
  - "log shipping [SQL Server], jobs"
  - "copy jobs [SQL Server]"
  - "primary databases [SQL Server]"
  - "log shipping [SQL Server], monitoring"
  - "log shipping [SQL Server], about log shipping"
  - "alert jobs [SQL Server]"
  - "availability [SQL Server]"
  - "jobs [SQL Server], log shipping"
  - "monitor servers [SQL Server]"
  - "restore jobs [SQL Server]"
  - "log shipping [SQL Server]"
  - "backup jobs [SQL Server]"
  - "primary servers [SQL Server]"
monikerRange: ">=sql-server-2016"
---
# About log shipping (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Log shipping allows you to automatically send transaction log backups from a *primary database* on a *primary server* instance to one or more *secondary databases* on separate *secondary server* instances. The transaction log backups are applied to each of the secondary databases individually. An optional third server instance, known as the *monitor server*, records the history and status of backup and restore operations and, optionally, raises alerts if these operations fail to occur as scheduled.

## Benefits

- Provides a disaster-recovery solution for a single primary database and one or more secondary databases, each on a separate instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Supports limited read-only access to secondary databases (during the interval between restore jobs).

- Allows a user-specified delay between when the primary server backs up the log of the primary database and when the secondary servers must restore (apply) the log backup. A longer delay can be useful, for example, if data is accidentally changed on the primary database. If the accidental change is noticed quickly, a delay can let you retrieve still unchanged data from a secondary database before the change is reflected there.

## Terms and definitions

- **primary server**: The instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that is your production server.

- **primary database**: The database on the primary server that you want to back up to another server. All administration of the log shipping configuration through [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is performed from the primary database.

- **secondary server**: The instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] where you want to keep a warm standby copy of your primary database.

- **secondary database**: The warm standby copy of the primary database. The secondary database may be in either the RECOVERING state or the STANDBY state, which leaves the database available for limited read-only access.

- **monitor server**: An optional instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that tracks all of the details of log shipping, including:

  - When the transaction log on the primary database was last backed up.
  - When the secondary servers last copied and restored the backup files.
  - Information about any backup failure alerts.

  > [!IMPORTANT]  
  > Once the monitor server has been configured, it cannot be changed without removing log shipping first.

- **backup job**: A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job that  performs the backup operation, logs history to the local server and the monitor server, and deletes old backup files and history information. When log shipping is enabled, the job category "Log Shipping Backup" is created on the primary server instance.

- **copy job**: A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job that copies the backup files from the primary server to a configurable destination on the secondary server and logs history on the secondary server and the monitor server. When log shipping is enabled on a database, the job category "Log Shipping Copy" is created on each secondary server in a log shipping configuration.

- **restore job**: A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job that restores the copied backup files to the secondary databases. It logs history on the local server and the monitor server, and deletes old files and old history information. When log shipping is enabled on a database, the job category "Log Shipping Restore" is created on the secondary server instance.

- **alert job**: A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job that raises alerts for primary and secondary databases when a backup or restore operation does not complete successfully within a specified threshold. When log shipping is enabled on a database, job category "Log Shipping Alert" is created on the monitor server instance.

  > [!TIP]  
  > For each alert, you need to specify an alert number. Also, be sure to configure the alert to notify an operator when an alert is raised.

## <a id="ComponentsAndConcepts"></a> Log shipping overview

Log shipping consists of three operations:

1. Back up the transaction log at the primary server instance.
1. Copy the transaction log file to the secondary server instance.
1. Restore the log backup on the secondary server instance.

The log can be shipped to multiple secondary server instances. In such cases, operations 2 and 3 are duplicated for each secondary server instance.

A log shipping configuration does not automatically fail over from the primary server to the secondary server. If the primary database becomes unavailable, any of the secondary databases can be brought online manually.

You can use a secondary database for reporting purposes.

In addition, you can configure alerts for your log shipping configuration.

### A typical log shipping configuration

The following figure shows a log shipping configuration with the primary server instance, three secondary server instances, and a monitor server instance. The figure illustrates the steps performed by backup, copy, and restore jobs, as follows:

1. The primary server instance runs the backup job to back up the transaction log on the primary database. This server instance then places the log backup into a primary log-backup file, which it sends to the backup folder.  In this figure, the backup folder is on a shared directory-the *backup share*.

1. Each of the three secondary server instances runs its own copy job to copy the primary log-backup file to its own local destination folder.

1. Each secondary server instance runs its own restore job to restore the log backup from the local destination folder onto the local secondary database.

The primary and secondary server instances send their own history and status to the monitor server instance.

:::image type="content" source="media/about-log-shipping-sql-server/log-shipping-typical-configuration.png" alt-text="Diagram of configuration showing backup, copy, and restore jobs.":::

## Interoperability

Log shipping can be used with the following features or components of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

- [Prerequisites to convert log shipping to Always On availability groups](../availability-groups/windows/prereqs-migrating-log-shipping-to-always-on-availability-groups.md)
- [Database Mirroring and Log Shipping (SQL Server)](../database-mirroring/database-mirroring-and-log-shipping-sql-server.md)
- [Log Shipping and Replication (SQL Server)](log-shipping-and-replication-sql-server.md)

> [!NOTE]  
> [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] and database mirroring are mutually exclusive. A database that is configured for one of these features cannot be configured for the other.

> [!CAUTION]  
> [!INCLUDE [known-issue-memory-optimized](../../includes/paragraph-content/known-issue-memory-optimized.md)]

## Related tasks

- [Upgrading Log Shipping to SQL Server 2016 (Transact-SQL)](../../database-engine/log-shipping/upgrading-log-shipping-to-sql-server-2016-transact-sql.md)
- [Configure Log Shipping (SQL Server)](configure-log-shipping-sql-server.md)
- [Add a Secondary Database to a Log Shipping Configuration (SQL Server)](add-a-secondary-database-to-a-log-shipping-configuration-sql-server.md)
- [Remove a Secondary Database from a Log Shipping Configuration (SQL Server)](remove-a-secondary-database-from-a-log-shipping-configuration-sql-server.md)
- [Remove Log Shipping (SQL Server)](remove-log-shipping-sql-server.md)
- [View the Log Shipping Report (SQL Server Management Studio)](view-the-log-shipping-report-sql-server-management-studio.md)
- [Monitor Log Shipping (Transact-SQL)](../../database-engine/log-shipping/monitor-log-shipping-transact-sql.md)
- [Fail Over to a Log Shipping Secondary (SQL Server)](fail-over-to-a-log-shipping-secondary-sql-server.md)
- [Management of Logins and Jobs After Role Switching (SQL Server)](../../sql-server/failover-clusters/management-of-logins-and-jobs-after-role-switching-sql-server.md)

## Related content

- [What is an Always On availability group?](../availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
