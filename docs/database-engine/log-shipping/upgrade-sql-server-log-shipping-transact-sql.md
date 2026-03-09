---
title: Upgrade SQL Server Log Shipping
description: Learn the appropriate order to preserve your log shipping disaster recovery solution when upgrading SQL Server from a previous version.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: jaferebe, randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: log-shipping
ms.topic: upgrade-and-migration-article
helpviewer_keywords:
  - "log shipping [SQL Server], upgrading"
---
# Upgrade SQL Server with log shipping (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

To preserve your log shipping disaster recovery solution, upgrade, or apply servicing updates in the appropriate order. Servicing updates include service packs or cumulative updates.

> [!NOTE]  
> An upgraded log shipping configuration uses the `backup compression default` server-level configuration option to control whether [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md) is used for the transaction log backup files. You can specify the backup compression behavior of log backups for each log shipping configuration. For more information, see [Configure Log Shipping (SQL Server)](../../database-engine/log-shipping/configure-log-shipping-sql-server.md).

## Prerequisites

Before you begin, review the following important information.

| Article | Description |
| --- | --- |
| [Supported version and edition upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md) | Verify that you can upgrade to your desired version of SQL Server from your existing Windows operating system and version of SQL Server. For example, you can't upgrade directly from a [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)] instance to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. |
| [Choose a Database Engine upgrade method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md) | Select the appropriate upgrade method and steps based on your review of supported version and edition upgrades. Also consider other components installed in your environment to upgrade components in the correct order. |
| [Plan and test Database Engine upgrade plan](../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md) | Review the release notes and known upgrade issues, the pre-upgrade checklist, and develop and test the upgrade plan. |
| [Hardware and software requirements for installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md) | Review the software requirements for installing [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. If other software is required, install it on each node before you begin the upgrade process to minimize any downtime. |
| [Contained availability group support](../../database-engine/availability-groups/windows/contained-availability-groups-overview.md#log-shipping) added in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | If you want to start using contained availability groups with log shipping, you need to drop and recreate the log shipping topology. However, if you're already using contained availability groups with log shipping, upgrades are supported. |
| [TDS 8.0 support](about-log-shipping-sql-server.md#enforce-tls-13-encryption) added in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] | If you want to use TDS 8.0 with log shipping in SQL 2025 and later versions, you first need to remove your existing log shipping configuration. |

<a id="ProtectData"></a>

## Protect your data before the upgrade

To protect your data during a log shipping upgrade, follow these steps:

1. Perform a full database backup on every primary database.

   For more information, see [Create a Full Database Backup (SQL Server)](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).

1. Run the [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) command on every primary database.

> [!IMPORTANT]  
> Make sure your primary server has enough space to hold the log backup copies for as long as the upgrade of the secondaries takes. If you're failing over to a secondary, this same concern applies to the secondary (the new primary).

<a id="UpgradeMonitor"></a>

## Upgrade the (optional) monitor server instance

You can upgrade the monitor server instance, if any, at any time. However, you don't need to upgrade the optional monitor server when you upgrade the primary and secondary servers.

While the monitor server is being upgraded, the log shipping configuration continues to work, but its status isn't recorded in the tables on the monitor. Any alerts that are configured aren't triggered while the monitor server is being upgraded. After the upgrade, you can update the information in the monitor tables by executing the [sp_refresh_log_shipping_monitor](../../relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql.md) system stored procedure.   For more information about a monitor server, see [About Log Shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md).

<a id="UpgradeSecondaries"></a>

## Upgrade the secondary server instances

The upgrade process involves upgrading the secondary server instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before upgrading the primary server instance. Always upgrade the secondary [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances first. Log shipping continues throughout the upgrade process because the upgraded secondary server instances continue to restore the log backups from primary server instance.

If you upgrade the primary server instance before the secondary server instance, log shipping fails because a backup created on a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't be restored on an older version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can upgrade the secondary instances simultaneously or serially, but you must upgrade all secondary instances before upgrading the primary instance to avoid a log shipping failure.

While upgrading a secondary server instance, the log shipping copy and restore jobs don't run. This condition means that unrestored transaction log backups accumulate on the primary replica, and you need to have sufficient space to hold these unrestored backups. The amount of accumulation depends on the frequency of scheduled backup on the primary server instance and the sequence in which you upgrade the secondary instances. Also, if a separate monitor server is configured, alerts might be raised indicating that restores haven't been performed for longer than the configured interval.

Once you upgrade the secondary server instances, the log shipping agents jobs resume and continue to copy and restore log backups from the primary server instance to the secondary server instances. The amount of time required for the secondary server instances to bring the secondary database up to date varies, depending on the time taken to upgrade the secondary server instance and the frequency of the backups on the primary server.

During the server upgrade, the secondary database itself isn't upgraded to the new version. It gets upgraded only if it's brought online by initiating a failover of the log shipped database. In theory, this condition could persist indefinitely. The amount of time to upgrade the database metadata when a failover is initiated is small.

> [!IMPORTANT]  
> The `RESTORE WITH STANDBY` option isn't supported for a database that requires upgrading. If an upgraded secondary database is configured by using `RESTORE WITH STANDBY`, transaction logs can no longer be restored after upgrade. To resume log shipping on that secondary database, you need to set up log shipping again on that standby server. For more information about the `STANDBY` option, see [Restore a Transaction Log Backup (SQL Server)](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md).

<a id="UpgradePrimary"></a>

## Upgrade the primary server instance

Since log shipping is primarily a disaster recovery solution, the simplest and most common scenario is to upgrade the primary instance in-place. The database is unavailable during this upgrade. Once the server is upgraded, the database is automatically brought back online, which causes it to be upgraded. After the database is upgraded, the log shipping jobs resume.

Log shipping also supports the option to [fail over to a log shipping secondary](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md), and optionally [change roles between primary and secondary log shipping servers](../../database-engine/log-shipping/change-roles-between-primary-and-secondary-log-shipping-servers-sql-server.md).

However, since log shipping is rarely configured as a high availability solution anymore (newer options are much more robust), failing over generally doesn't minimize downtime. System database objects aren't synchronized, and enabling clients to easily locate and connect to a promoted secondary can be challenging.

## Related content

- [Configure Log Shipping (SQL Server)](configure-log-shipping-sql-server.md)
- [Monitor Log Shipping (Transact-SQL)](monitor-log-shipping-transact-sql.md)
- [Log Shipping Tables and Stored Procedures](log-shipping-tables-and-stored-procedures.md)
