---
title: Add Log Shipping Secondary
description: Describes how to add a secondary database to an existing log shipping configuration by using SQL Server Management Studio or Transact-SQL in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: log-shipping
ms.topic: how-to
helpviewer_keywords:
  - "adding secondary databases"
  - "secondary databases [SQL Server], in log shipping"
  - "secondary data files [SQL Server], adding"
  - "log shipping [SQL Server], secondary databases"
---
# Add a secondary database to a log shipping configuration (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to add a secondary database to an existing log shipping configuration in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. Right-click the database you want to use as your primary database in the log shipping configuration, and then select **Properties**.

1. Under **Select a page**, select **Transaction Log Shipping**.

1. Under **Secondary server instances and databases**, select **Add**.

1. Select **Connect** and connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that you want to use as your secondary server.

1. In the **Secondary database** box, choose a database from the list or type the name of the database you want to create.

1. On the **Initialize Secondary database** tab, choose the option that you want to use to initialize the secondary database.

1. On the **Copy Files tab**, in the **Destination folder for copied files** box, type the path of the folder into which the transaction logs backups should be copied. This folder is often located on the secondary server.

1. Note the copy schedule listed in the **Schedule** box under **Copy job**. If you want to customize the schedule for your installation, select **Schedule** and then adjust the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedule as needed. This schedule should approximate the backup schedule.

1. On the **Restore** tab, under **Database state when restoring backups**, choose the **No recovery mode** or **Standby mode** option.

1. If you chose the **Standby mode** option, choose if you want to disconnect users from the secondary database while the restore operation is underway.

1. If you want to delay the restore process on the secondary server, choose a delay time under **Delay restoring backups at least**.

1. Choose an alert threshold under **Alert if no restore occurs within**.

1. Note the restore schedule listed in the **Schedule** box under **Restore job**. If you want to customize the schedule for your installation, select **Schedule** and then adjust the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedule as needed. This schedule should approximate the backup schedule.

1. Select **OK**.

1. Select **OK** on the Database Properties dialog box to begin the configuration process.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. On the secondary server, execute [sp_add_log_shipping_secondary_primary](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-primary-transact-sql.md) supplying the details of the primary server and database. This stored procedure returns the secondary ID and the copy and restore job IDs.

1. On the secondary server, execute [sp_add_jobschedule](../../relational-databases/system-stored-procedures/sp-add-jobschedule-transact-sql.md) to set the schedule for the copy and restore jobs.

1. On the secondary server, execute [sp_add_log_shipping_secondary_database](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-database-transact-sql.md) to add a secondary database.

1. On the primary server, execute [sp_add_log_shipping_primary_secondary](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-secondary-transact-sql.md) to add the required information about the new secondary database to the primary server.

1. On the secondary server, enable the copy and restore jobs. For more information, see [Disable or Enable a Job](/ssms/agent/disable-or-enable-a-job).

## Permissions

The log-shipping stored procedures require membership in the **sysadmin** fixed server role.

<a id="RelatedTasks"></a>

## Related tasks

- [Upgrade SQL Server with log shipping (Transact-SQL)](upgrade-sql-server-log-shipping-transact-sql.md)
- [Configure Log Shipping (SQL Server)](../../database-engine/log-shipping/configure-log-shipping-sql-server.md)
- [Remove a Secondary Database from a Log Shipping Configuration (SQL Server)](../../database-engine/log-shipping/remove-a-secondary-database-from-a-log-shipping-configuration-sql-server.md)
- [Remove Log Shipping (SQL Server)](../../database-engine/log-shipping/remove-log-shipping-sql-server.md)
- [View the Log Shipping Report (SQL Server Management Studio)](../../database-engine/log-shipping/view-the-log-shipping-report-sql-server-management-studio.md)
- [Monitor Log Shipping (Transact-SQL)](../../database-engine/log-shipping/monitor-log-shipping-transact-sql.md)
- [Fail Over to a Log Shipping Secondary (SQL Server)](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)

## Related content

- [About log shipping (SQL Server)](about-log-shipping-sql-server.md)
- [Log Shipping Tables and Stored Procedures](log-shipping-tables-and-stored-procedures.md)
