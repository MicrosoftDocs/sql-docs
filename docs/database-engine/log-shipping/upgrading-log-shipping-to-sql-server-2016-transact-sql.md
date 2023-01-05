---
title: "Upgrade log shipping to SQL Server 2016 and newer"
description: Learn the appropriate order to preserve your log shipping disaster recovery solution when upgrading to SQL Server 2016 and newer from a previous version.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "02/01/2016"
ms.service: sql
ms.subservice: log-shipping
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "log shipping [SQL Server], upgrading"
---
# Upgrading Log Shipping to SQL Server 2016 (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

To preserve your log shipping disaster recovery solution, upgrade, or apply servicing updates in the appropriate order. Servicing updates include service packs or cumulative updates.  
  
> [!NOTE]  
> [Backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md) was introduced in [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)]. An upgraded log shipping configuration uses the **backup compression default** server-level configuration option to control whether backup compression is used for the transaction log backup files. The backup compression behavior of log backups can be specified for each log shipping configuration. For more information, see [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md).  
  
 **In This Topic:**  
  
-   [Prerequisites](#Prerequisites)  
  
-   [Protect Your Data Before the Upgrade](#ProtectData)  
  
-   [Upgrading the Monitor Server Instance](#UpgradeMonitor)  
  
-   [Upgrading the Secondary Server Instances](#UpgradeSecondaries)  
  
-   [Upgrading the Primary Instance](#UpgradePrimary)  
  
##  <a name="Prerequisites"></a> Prerequisites  
 Before you begin, review the following important information:  
  
-   [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md): Verify that you can upgrade to SQL Server 2016 from your version of the Windows operating system and version of SQL Server. For example, you cannot upgrade directly from a SQL Server 2005 instance to [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)].  
  
-   [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md): Select the appropriate upgrade method and steps based on your review of supported version and edition upgrades and also based on other components installed in your environment to upgrade components in the correct order.  
  
-   [Plan and Test the Database Engine Upgrade Plan](../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md): Review the release notes and known upgrade issues, the pre-upgrade checklist, and develop and test the upgrade plan.  
  
-   [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md):  Review the software requirements for installing [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. If additional software is required, install it on each node before you begin the upgrade process to minimize any downtime.  
  
##  <a name="ProtectData"></a> Protect Your Data Before the Upgrade  
 As a best practice, we recommend that you protect your data before a log shipping upgrade.  
  
 **To protect your data**  
  
1.  Perform a full database backup on every primary database.  
  
     For more information, see [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
2.  Run the [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) command on every primary database.  
  
> [!IMPORTANT]  
>  Ensure that you have sufficient space on your primary server to hold the log backup copies for as long as the upgrade of the secondaries is expected to take.  If you are failing over to a secondary, this same concern applies to the secondary (the new primary).  
  
##  <a name="UpgradeMonitor"></a> Upgrading the (Optional) Monitor Server Instance  
 The monitor server instance, if any, can be upgraded at any time. However, you do not need to upgrade the optional monitor server when you upgrade the primary and secondary servers.  
  
 While the monitor server is being upgraded, the log shipping configuration continues to work, but its status is not recorded in the tables on the monitor. Any alerts that have been configured will not be triggered while the monitor server is being upgraded. After the upgrade, you can update the information in the monitor tables by executing the [sp_refresh_log_shipping_monitor](../../relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql.md) system stored procedure.   For more information about a monitor server, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
##  <a name="UpgradeSecondaries"></a> Upgrading the Secondary Server Instances  

The upgrade process involves upgrading the secondary server instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before upgrading the primary server instance. Always upgrade the secondary [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances first. Log shipping continues throughout the upgrade process because the upgraded secondary server instances continue to restore the log backups from primary server instance. If the primary server instance is upgraded before the secondary server instance, log shipping will fail because a backup created on a newer version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be restored on an older version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can upgrade the secondary instances simultaneously or serially, but all secondary instance must be upgraded before the primary instance is upgraded to avoid a log shipping failure.  
  
While a secondary server instance is being upgraded, the log shipping copy and restore jobs do not run. This means that unrestored transaction log backups will accumulate on the primary and you need to have sufficient space to hold these unrestored backups. The amount of accumulation depends on the frequency of scheduled backup on the primary server instance and the sequence in which you upgrade the secondary instances. Also, if a separate monitor server has been configured, alerts might be raised indicating restores have not been performed for longer than the configured interval.  
  
 Once the secondary server instances have been upgraded, the log shipping agents jobs resume and continue to copy and restore log backups from the primary server instance to the secondary server instances. The amount of time required for the secondary server instances to bring the secondary database up to date varies, depending on the time taken to upgrade the secondary server instance and the frequency of the backups on the primary server.  
  
> [!NOTE]  
> During the server upgrade, the secondary database itself is not upgraded the new version. It will get upgraded only if it is brought online by initiating a failover of the log shipped database. In theory, this condition could persist indefinitely. The amount of time to upgrade the database metadata when a failover is initiated is small.  
  
> [!IMPORTANT]  
>  The RESTORE WITH STANDBY option is not supported for a database that requires upgrading. If an upgraded secondary database has been configured by using RESTORE WITH STANDBY, transaction logs can no longer be restored after upgrade. To resume log shipping on that secondary database, you will need to set up log shipping again on that standby server. For more information about the STANDBY option, see [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md).  
  
##  <a name="UpgradePrimary"></a> Upgrading the Primary Server Instance  
 Since log shipping is primarily a disaster recovery solution, the simplest and most common scenario is to upgrade the primary instance in-place and the database is simply unavailable during this upgrade. Once the server is upgraded, the database is automatically brought back online, which causes it to be upgraded. After the database is upgraded, the log shipping jobs resume.  
  
> [!NOTE]  
>  Log shipping also supports the option to [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md), and optionally [Change Roles Between Primary and Secondary Log Shipping Servers &#40;SQL Server&#41;](../../database-engine/log-shipping/change-roles-between-primary-and-secondary-log-shipping-servers-sql-server.md). However, since log shipping is rarely configured as a high availability solution anymore (newer options are much more robust), failing over generally will not minimize downtime because system database objects will not be synchronized and enabling clients to easily locate and connect to a promoted secondary can be an ordeal.  
  
## See Also  
 [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)   
 [Install SQL Server 2016 from the Command Prompt](../install-windows/install-sql-server-from-the-command-prompt.md)   
 [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md)   
 [Monitor Log Shipping &#40;Transact-SQL&#41;](../../database-engine/log-shipping/monitor-log-shipping-transact-sql.md)   
 [Log Shipping Tables and Stored Procedures](../../database-engine/log-shipping/log-shipping-tables-and-stored-procedures.md)  
  
