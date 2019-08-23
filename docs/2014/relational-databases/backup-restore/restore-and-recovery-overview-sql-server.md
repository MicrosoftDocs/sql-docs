---
title: "Restore and Recovery Overview (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "restoring tables [SQL Server]"
  - "backups [SQL Server], restore scenarios"
  - "database backups [SQL Server], restore scenarios"
  - "database restores [SQL Server]"
  - "restoring [SQL Server]"
  - "restores [SQL Server]"
  - "table restores [SQL Server]"
  - "restoring databases [SQL Server], about restoring databases"
  - "database restores [SQL Server], scenarios"
ms.assetid: e985c9a6-4230-4087-9fdb-de8571ba5a5f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore and Recovery Overview (SQL Server)
  To recover a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database from a failure, a database administrator has to restore a set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups in a logically correct and meaningful restore sequence. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] restore and recovery supports restoring data from backups of a whole database, a data file, or a data page, as follows:  
  
-   The database (a *complete database restore*)  
  
     The whole database is restored and recovered, and the database is offline for the duration of the restore and recovery operations.  
  
-   The data file (a *file restore*)  
  
     A data file or a set of files is restored and recovered. During a file restore, the filegroups that contain the files are automatically offline for the duration of the restore. Any attempt to access an offline filegroup causes an error.  
  
-   The data page (a *page restore*)  
  
     Under the full recovery model or bulk-logged recovery model, you can restore individual databases. Page restores can be performed on any database, regardless of the number of filegroups.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore work across all supported operating systems, whether they are 64-bit or 32-bit systems. For information about the supported operating systems, see [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md). For information about support for backups from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the "Compatibility Support" section of [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql).  
  
 **In this Topic:**  
  
-   [Overview of Restore Scenarios](#RestoreScenariosOv)  
  
-   [Recovery Models and Supported Restore Operations](#RMsAndSupportedRestoreOps)  
  
-   [Restore Restrictions Under the Simple Recovery Model](#RMsimpleScenarios)  
  
-   [Restore Under the Bulk-Logged Recovery Model](#RMblogRestore)  
  
-   [Database Recovery Advisor (SQL Server Management Studio)](#DRA)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="RestoreScenariosOv"></a> Overview of Restore Scenarios  
 A *restore scenario* in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is the process of restoring data from one or more backups and then recovering the database. The supported restore scenarios depend on the recovery model of the database and the edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The following table introduces the possible restore scenarios that are supported for different recovery models.  
  
|Restore scenario|Under simple recovery model|Under full/bulk-logged recovery models|  
|----------------------|---------------------------------|----------------------------------------------|  
|Complete database restore|This is the basic restore strategy. A complete database restore might involve simply restoring and recovering a full database backup. Alternatively, a complete database restore might involve restoring a full database backup followed by restoring and recovering a differential backup.<br /><br /> For more information, see [Complete Database Restores &#40;Simple Recovery Model&#41;](complete-database-restores-simple-recovery-model.md).|This is the basic restore strategy. A complete database restore involve restoring a full database backup and, optionally, a differential backup (if any), followed by restoring all subsequent log backups (in sequence). The complete database restore is finished by recovering the last log backup and also restoring it (RESTORE WITH RECOVERY).<br /><br /> For more information, see [Complete Database Restores &#40;Full Recovery Model&#41;](complete-database-restores-full-recovery-model.md)|  
|File restore **\***|Restore one or more damaged read-only files, without restoring the entire database. File restore is available only if the database has at least one read-only filegroup.|Restores one or more files, without restoring the entire database. File restore can be performed while the database is offline or, for some editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], while the database remains online. During a file restore, the filegroups that contain the files that are being restored are always offline.|  
|Page restore|Not applicable|Restores one or more damaged pages. Page restore can be performed while the database is offline or, for some editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], while the database remains online. During a page restore, the pages that are being restored are always offline.<br /><br /> An unbroken chain of log backups must be available, up to the current log file, and they must all be applied to bring the page up to date with the current log file.<br /><br /> For more information, see [Restore Pages &#40;SQL Server&#41;](restore-pages-sql-server.md).|  
|Piecemeal restore **\***|Restore and recover the database in stages at the filegroup level, starting with the primary and all read/write, secondary filegroups.|Restore and recover the database in stages at the filegroup level, starting with the primary filegroup.|  
  
 **\*** Online restore is supported only in the Enterprise edition.  
  
 Regardless of how data is restored, before a database can be recovered, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] guarantees that the whole database is logically consistent. For example, if you restore a file, you cannot recover it and bring it online until it has been rolled far enough forward to be consistent with the database.  
  
### Advantages of a File or Page Restore  
 Restoring and recovering files or pages, instead of the whole database, provides the following advantages:  
  
-   Restoring less data reduces the time required to copy and recover it.  
  
-   On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] restoring files or pages might allow other data in the database to remain online during the restore operation.  
  
##  <a name="RMsAndSupportedRestoreOps"></a> Recovery Models and Supported Restore Operations  
 The restore operations that are available for a database depend on its recovery model. The following table summarizes whether and to what extent each of the recovery models supports a given restore scenario.  
  
|Restore operation|Full recovery model|Bulk-logged recovery model|Simple recovery model|  
|-----------------------|-------------------------|---------------------------------|---------------------------|  
|Data recovery|Complete recovery (if the log is available).|Some data-loss exposure.|Any data since last full or differential backup is lost.|  
|Point-in-time restore|Any time covered by the log backups.|Disallowed if the log backup contains any bulk-logged changes.|Not supported.|  
|File restore **\***|Full support.|Sometimes.**\*\***|Available only for read-only secondary files.|  
|Page restore **\***|Full support.|Sometimes.**\*\***|None.|  
|Piecemeal (filegroup-level) restore **\***|Full support.|Sometimes.**\*\***|Available only for read-only secondary files.|  
  
 **\*** Available only in the Enterprise edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
 **\*\*** For the required conditions, see [Restore Restrictions Under the Simple Recovery Model](#RMsimpleScenarios), later in this topic.  
  
> [!IMPORTANT]  
>  Regardless of the recovery model of a database, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup cannot be restored by a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is older than the version that created the backup.  
  
##  <a name="RMsimpleScenarios"></a> Restore Scenarios Under the Simple Recovery Model  
 The simple recovery model imposes the following restrictions on restore operations:  
  
-   File restore and piecemeal restore are available only for read-only secondary filegroups. For information about these restore scenarios, see [File Restores &#40;Simple Recovery Model&#41;](file-restores-simple-recovery-model.md) and [Piecemeal Restores &#40;SQL Server&#41;](piecemeal-restores-sql-server.md).  
  
-   Page restore is not allowed.  
  
-   Point-in-time restore is not allowed.  
  
 If any of these restrictions are inappropriate for your recovery needs, we recommend that you consider using the full recovery model. For more information, see [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md).  
  
> [!IMPORTANT]  
>  Regardless of the recovery model of a database, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup cannot be restored by a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is older than the version that created the backup.  
  
##  <a name="RMblogRestore"></a> Restore Under the Bulk-Logged Recovery Model  
 This section discusses restore considerations that are unique to bulk-logged recovery model, which is intended exclusively as a supplement to the full recovery model.  
  
> [!NOTE]  
>  For an introduction to the bulk-logged recovery model, see [The Transaction Log &#40;SQL Server&#41;](../logs/the-transaction-log-sql-server.md).  
  
 Generally, the bulk-logged recovery model is similar to the full recovery model, and the information described for the full recovery model also applies to both. However, point-in-time recovery and online restore are affected by the bulk-logged recovery model.  
  
### Restrictions for Point-in-time Recovery  
 If a log backup taken under the bulk-logged recovery model contains bulk-logged changes, point-in-time recovery is not allowed. Trying to perform point-in-time recovery on a log backup that contains bulk changes will cause the restore operation to fail.  
  
### Restrictions for Online Restore  
 An online restore sequence works only if the following conditions are met:  
  
-   All required log backups must have been taken before the restore sequence starts.  
  
-   Bulk changes must be backed before starting the online restore sequence.  
  
-   If bulk changes exist in the database, all files must be either online or[defunct](remove-defunct-filegroups-sql-server.md). (This means that it is no longer part of the database.)  
  
 If these conditions are not met, the online restore sequence fails.  
  
> [!NOTE]  
>  We recommend switching to the full recovery model before starting an online restore. For more information, see [Recovery Models &#40;SQL Server&#41;](recovery-models-sql-server.md).  
  
 For information about how to perform an online restore, see [Online Restore &#40;SQL Server&#41;](online-restore-sql-server.md).  
  
##  <a name="DRA"></a> Database Recovery Advisor (SQL Server Management Studio)  
 The Database Recovery Advisor facilitates constructing restore plans that implement optimal correct restore sequences. Many known database restore issues and enhancements requested by customers have been addressed. Major enhancements introduced by the Database Recovery Advisor include the following:  
  
-   **Restore-plan algorithm:**  The algorithm used to construct restore plans has improved significantly, particularly for complex restore scenarios. Many edge cases, including forking scenarios in point-in-time restores, are handled more efficiently than in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Point-in-time restores:**  The Database Recovery Advisor greatly simplifies restoring a database to a given point in time. A visual backup timeline significantly enhances support for point-in-time restores. This visual timeline allows you to identify a feasible point in time as the target recovery point for restoring a database. The timeline facilitates traversing a forked recovery path (a path that spans recovery forks). A given point-in-time restore plan automatically includes the backups that are relevant to the restoring to your target point in time (date and time). For more information, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).  
  
 For more information, see about the Database Recovery Advisor, see the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Manageability blogs:  
  
-   [Recovery Advisor: An Introduction](https://blogs.msdn.com/b/managingsql/archive/2011/07/13/recovery-advisor-an-introduction.aspx)  
  
-   [Recovery Advisor: Using SSMS to create/restore split backups](https://blogs.msdn.com/b/managingsql/archive/2011/07/13/recovery-advisor-using-ssms-to-create-restore-split-backups.aspx)  
  
##  <a name="RelatedContent"></a> Related Content  
 None.  
  
## See Also  
 [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md)  
  
  
