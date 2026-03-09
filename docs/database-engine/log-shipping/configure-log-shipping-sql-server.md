---
title: "Configure Log Shipping (SQL Server)"
description: Learn how to configure log shipping by using SQL Server Management Studio or Transact-SQL in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: log-shipping
ms.topic: how-to
helpviewer_keywords:
  - "log shipping [SQL Server], enabling"
  - "log shipping [SQL Server], configuring"
---
# Configure log shipping (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure log shipping in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

> [!NOTE]  
> [!INCLUDE [ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] and later versions support backup compression. When creating a log shipping configuration, you can control the backup compression behavior of log backups. For more information, see [Backup Compression (SQL Server)](../../relational-databases/backup-restore/backup-compression-sql-server.md).

<a id="Prerequisites"></a>

## Prerequisites

- The primary database must use the full or bulk-logged recovery model; switching the database to simple recovery will cause log shipping to stop functioning.

- Before you configure log shipping, you must create a share to make the transaction log backups available to the secondary server. This is a share of the directory where the transaction log backups will be generated. For example, if you back up your transaction logs to the directory `C:\data\tlogs\`, you could create the `\\<primaryserver>\tlogs` share of that directory.

> [!IMPORTANT]  
> - [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] uses [OLEDB version 19](../../connect/oledb/oledb-driver-for-sql-server.md) as the default version for linked servers, which has a default `Encrypt` value of `Mandatory`. Changes to the linked server configuration might be required when adding a [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instance as a replica or monitor.
> - Log shipping monitoring can break if the monitor is a remote [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instance when other SQL Server instances in the log shipping topology use a previous version.

<a id="Permissions"></a>

## Permissions

The log shipping stored procedures require membership in the **sysadmin** fixed server role.

## Configure log shipping

You can configure log shipping by using either [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The tabs in this section describe how to configure log shipping by using each method.

### [SQL Server Management Studio](#tab/ssms)

To configure log shopping by using [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)], follow these steps:

1. Right-click the database you want to use as your primary database in the log shipping configuration, and then select **Properties**.

1. Under **Select a page**, select **Transaction Log Shipping**.

1. Select the **Enable this as a primary database in a log shipping configuration** check box.

1. Under **Transaction log backups**, select **Backup Settings**.

1. In the **Network path to the backup folder** box, type the network path to the share you created for the transaction log backup folder.

1. **If the backup folder is located on the primary server, type a local path in the backup folder** box. (If the backup folder isn't on the primary server, you can leave this box empty.)

   > [!IMPORTANT]  
   > If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account on your primary server runs under the local system account, you must create your backup folder on the primary server and specify a local path to that folder.

1. Configure the **Delete files older than** and **Alert if no backup occurs within** parameters.

1. Note the backup schedule listed in the **Schedule** box under **Backup job**. If you want to customize the schedule for your installation, then select **Schedule** and adjust the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedule as needed.

1. [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] supports [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md). When creating a log shipping configuration, you can control the backup compression behavior of log backups by choosing one of the following options: **Use the default server setting**, **Compress backup**, or **Do not compress backup**. For more information, see [Log Shipping Transaction Log Backup Settings](../../relational-databases/databases/log-shipping-transaction-log-backup-settings.md).

1. Select **OK**.

1. Under **Secondary server instances and databases**, select **Add**.

1. Select **Connect** and connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that you want to use as your secondary server.

1. In the **Secondary Database** box, choose a database from the list or type the name of the database you want to create.

1. On the **Initialize Secondary database** tab, choose the option that you want to use to initialize the secondary database.

   > [!NOTE]  
   > If you choose to have [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] initialize the secondary database from a database backup, the data and log files of the secondary database are placed in the same location as the data and log files of the `master` database. This location is likely to be different than the location of the data and log files of the primary database.

1. On the **Copy Files** tab, in the **Destination folder for copied files** box, type the path of the folder into which the transaction logs backups should be copied. This folder is often located on the secondary server.

1. Note the copy schedule listed in the **Schedule** box under **Copy job**. If you want to customize the schedule for your installation, select **Schedule** and then adjust the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedule as needed. This schedule should approximate the backup schedule.

1. On the **Restore** tab, under **Database state when restoring backups**, choose the **No recovery mode** or **Standby mode** option.

   > [!IMPORTANT]  
   > **Standby mode** is only an option when the version of the primary and secondary server are the same. When the major version of the secondary server is higher than the primary, only **No recovery mode** is allowed

1. If you chose the **Standby mode** option, choose if you want to disconnect users from the secondary database while the restore operation is underway.

1. If you want to delay the restore process on the secondary server, choose a delay time under **Delay restoring backups at least**.

1. Choose an alert threshold under **Alert if no restore occurs within**.

1. Note the restore schedule listed in the **Schedule** box under **Restore job**. If you want to customize the schedule for your installation, select **Schedule** and then adjust the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedule as needed. This schedule should approximate the backup schedule.

1. Select **OK**.

1. Under **Monitor server instance**, select the **Use a monitor server instance** check box, and then select **Settings**.

   > [!IMPORTANT]  
   > To monitor this log shipping configuration, you must add the monitor server now. To add the monitor server later, you would need to remove this log shipping configuration and then replace it with a new configuration that includes a monitor server.

1. Select **Connect** and connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that you want to use as your monitor server.

1. Under **Monitor connections**, choose the connection method to be used by the backup, copy, and restore jobs to connect to the monitor server.

1. Under **History retention**, choose the length of time you want to retain a record of your log shipping history.

1. Select **OK**.

1. On the **Database Properties** dialog box, select **OK** to begin the configuration process.

### [Transact-SQL](#tab/tsql)

1. Initialize the secondary database by restoring a full backup of the primary database on the secondary server.

1. On the primary server, execute [sp_add_log_shipping_primary_database](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md) to add a primary database. The stored procedure returns the backup job ID and primary ID.

1. On the primary server, execute [sp_add_jobschedule](../../relational-databases/system-stored-procedures/sp-add-jobschedule-transact-sql.md) to add a schedule for the backup job.

1. On the monitor server, execute [sp_add_log_shipping_alert_job](../../relational-databases/system-stored-procedures/sp-add-log-shipping-alert-job-transact-sql.md) to add the alert job.

1. On the primary server, enable the backup job.

1. On the secondary server, execute [sp_add_log_shipping_secondary_primary](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-primary-transact-sql.md) supplying the details of the primary server and database. This stored procedure returns the secondary ID and the copy and restore job IDs.

1. On the secondary server, execute [sp_add_jobschedule](../../relational-databases/system-stored-procedures/sp-add-jobschedule-transact-sql.md) to set the schedule for the copy and restore jobs.

1. On the secondary server, execute [sp_add_log_shipping_secondary_database](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-database-transact-sql.md) to add a secondary database.

1. On the primary server, execute [sp_add_log_shipping_primary_secondary](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-secondary-transact-sql.md) to add the required information about the new secondary database to the primary server.

1. On the secondary server, enable the copy and restore jobs. For more information, see [Disable or Enable a Job](/ssms/agent/disable-or-enable-a-job).

---

<a id="RelatedTasks"></a>

## Related tasks

- [Upgrade SQL Server with log shipping (Transact-SQL)](upgrade-sql-server-log-shipping-transact-sql.md)
- [Add a Secondary Database to a Log Shipping Configuration (SQL Server)](../../database-engine/log-shipping/add-a-secondary-database-to-a-log-shipping-configuration-sql-server.md)
- [Remove a Secondary Database from a Log Shipping Configuration (SQL Server)](../../database-engine/log-shipping/remove-a-secondary-database-from-a-log-shipping-configuration-sql-server.md)
- [Remove Log Shipping (SQL Server)](../../database-engine/log-shipping/remove-log-shipping-sql-server.md)
- [View the Log Shipping Report (SQL Server Management Studio)](../../database-engine/log-shipping/view-the-log-shipping-report-sql-server-management-studio.md)
- [Monitor Log Shipping (Transact-SQL)](../../database-engine/log-shipping/monitor-log-shipping-transact-sql.md)
- [Fail Over to a Log Shipping Secondary (SQL Server)](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)

## Related content

- [About log shipping (SQL Server)](about-log-shipping-sql-server.md)
- [Log Shipping Tables and Stored Procedures](log-shipping-tables-and-stored-procedures.md)
