---
title: "SQL Server Managed Backup to Windows Azure: Interoperability and Coexistence | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: 78fb78ed-653f-45fe-a02a-a66519bfee1b
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Managed Backup to Windows Azure: Interoperability and Coexistence
  This topic describes [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] interoperability and coexistence with several features in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. These features include the following: AlwaysOn Availability Groups, Database Mirroring, Backup Maintenance Plans, Log Shipping, Ad hoc backups, Detach Database, and Drop Database.  
  
### AlwaysOn Availability Groups  
 AlwaysOn Availability Groups that are configured as a Windows Azure-only solution supported for [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)]. On-premises only, or Hybrid AlwaysOn Availability Group configurations are not supported. For more information and other considerations, see [Setting up SQL Server Managed Backup to Windows Azure for Availability Groups](../../2014/database-engine/setting-up-sql-server-managed-backup-to-windows-azure-for-availability-groups.md)  
  
### Database Mirroring  
 [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is supported only on the principal database. If both the principal and the mirror are configured to use [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)], the mirrored database is skipped and will not be backed up. However, in the event of a failover, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will start the backup process after the mirror has completed role switching and is online. The backups will be stored in a new container in this case. If the mirror is not configured to use [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)], in the event of a failover, no backups are taken. We recommend that you configure [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] on both the principal and the mirror so backups continue in the event of a failover.  
  
> [!TIP]  
>  If you are creating a mirrored database on an instance with [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] default settings, it may be preferable to disable [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] instance defaults, so they are not applied to the mirrored database, and then re-enable the instance defaults after configuring the Principal and the Mirror.  
  
### Maintenance Plan  
 Using Maintenance Plans for creating backups for a database when [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is enabled is not supported. Maintenance plans will cause broken log chain and [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] may not be able to support a guaranteed recoverability of the database during restore. This also applies when [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is enabled at the instance level.  
  
> [!TIP]  
>  Maintenance Plans with Copy Only backups is supported with [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] configured for the same database or instance.  
  
### Log Shipping  
 You cannot configure Log Shipping and [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for the same database at the same time. Doing so will affect the recoverability of the database using either functionality.  
  
### Ad Hoc Backups Using Transact-SQL and SQL Server Management Studio  
 Ad hoc or one time backups created outside [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] using Transact-SQL or SQL Server Management Studio may affect the [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] process depending on the type of backup and the storage media used. Log backups to a different Windows Azure storage account than what [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is using, or any other destination than the Windows Azure Blob storage service, will cause a log chain break. We recommend that you use the [smart_admin.sp_backup_on_demand &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-backup-on-demand-transact-sql) stored procedure to initiate a backup on databases that have [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] enabled. You can initiate either a full database or log backup using this stored procedure.  
  
### Drop Database and Detach Database  
 If a database that has [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] enabled is detached or dropped, although no additional backups are possible, the previous backups remain in the storage until the retention period has elapsed, at which point the backups will be purged.  
  
### Changes to Recovery Model  
  
-   If you change the recovery model of a database from **Simple** to **Full** or **Bulk-Logged**, you have the option of configuring [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for the database. This will be considered like a new database from [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] perspective.  
  
-   If you change the recovery model of a database from **Full** or **Bulk-Logged** to **Simple**, that has [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] enabled, backup operations will no longer be scheduled. The retention period setting will still be active and backup files will remain in the storage account until the retention period has elapsed. If you want to retain the backups, we recommend that you download the files either to a different storage account or to an on-premises location. The configuration settings are retained and can be reused if the recovery model is set back to **Full** or **Bulk-Logged** again.  
  
### Log Backups Using Other Backup Tools or Custom Scripts  
 Any two backups that are configured to perform log backups on the same database will cause break in the backup log chain. Although [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] will attempt to remedy the break in the backup chain by scheduling full backups when a break in chain is detected, this means keeping up continuously with periodic breaks and log backups performed by two competing tools. This can also potentially affect the recoverability of the database since no one tool can be expected to have a full set of backup in sequence. Although this applies to any two features or tools performing log backups,, it is useful to call out specific examples as described below. This is also the basis for the issues with configuring maintenance plans or log shipping as described in earlier sections of this topic.  
  
 **Data Protection Manager (DPM) based backups:** Microsoft Data Protection Manager allows you to do full and incremental backups. The incremental backups are log backups that do perform a log truncation after creating a T-log backup. So configuring both DPM and [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] for the same database is not supported.  
  
 **Third Party Tools or Scripts:** Any third party tool or scripts that perform log backups causing log truncation is incompatible with [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)], and is not supported.  
  
 If you have [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] enabled for a database instance, and you want to take an ad hoc backup you can either use the [smart_admin.sp_backup_on_demand &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/managed-backup-sp-backup-on-demand-transact-sql) stored procedure as described in the earlier section. If you also have a need to schedule or un backups periodically outside of [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)], you can use the Copy Only Backup.  For more information, see [Copy-Only Backups &#40;SQL Server&#41;](../relational-databases/backup-restore/copy-only-backups-sql-server.md).  
  
  
