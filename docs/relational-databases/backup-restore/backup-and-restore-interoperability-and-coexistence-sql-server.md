---
title: "Backup & restore: feature interoperability"
description: This article describes backup-and-restore features in SQL Server, including database startup, online restore and disabled indexes, and database mirroring.
ms.custom: seo-lt-2019
ms.date: "12/17/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "file restores [SQL Server], related features"
  - "restoring [SQL Server], files"
  - "restoring files [SQL Server], related features"
  - "backups [SQL Server], files or filegroups"
  - "file backups [SQL Server], related features"
ms.assetid: 69f212b8-edcd-4c5d-8a8a-679ced33c128
author: MashaMSFT
ms.author: mathoma
---
# Backup and Restore: Interoperability and Coexistence (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes backup-and-restore considerations for several features in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. These features include: file restore and database startup, online restore and disabled indexes, database mirroring, and piecemeal restore and full-text indexes.  
  
 **In this Topic:**  
  
-   [File Restore and Database Startup](#FileRestoreAndDbStartup)  
  
-   [Online Restore and Disabled Indexes](#OnlineRestoreAndDisabledIndexes)  
  
-   [Database Mirroring and Backup and Restore](#DbMandBnR)  
  
-   [Piecemeal Restore and Full-Text Indexes](#PiecemealAndFTIndexes)  
  
-   [File Backup and Restore and Compression](#FileBnRandCompression)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="FileRestoreAndDbStartup"></a> File Restore and Database Startup  
 This section is relevant only for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that have multiple filegroups.  
  
> [!NOTE]  
>  When a database is started, only filegroups whose files were online when the database was closed are recovered and brought online.  
  
 If a problem is encountered during database startup, recovery fails, and the database is marked as SUSPECT. If the problem can be isolated to a file or files, the database administrator can take the files offline and try to restart the database. To take a file offline, you can use the following [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement:  
  
 ALTER DATABASE *database_name* MODIFY FILE (NAME **='***filename***'**, OFFLINE)  
  
 If startup succeeds, any filegroup that contains an offline file remains offline.  
  
##  <a name="OnlineRestoreAndDisabledIndexes"></a> Online Restore and Disabled Indexes  
 This section is relevant only for databases that have multiple filegroups and, for the simple recovery model, at least one read-only filegroup.  
  
 In these cases, when a database is online, the index can be created, dropped, enabled or disabled only if all filegroups holding any part of the index are online.  
  
 For information about restoring offline filegroups, see [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md).  
  
##  <a name="DbMandBnR"></a> Database Mirroring and Backup and Restore  
 This section is relevant only for full-model databases that have multiple filegroups.  
  
> [!NOTE]  
>  The database mirroring feature will be removed in a future version of Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] instead.  
  
 Database mirroring is a solution for increasing database availability. Mirroring is implemented on a per-database basis and works only with databases that use the full recovery model. For more information, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).  
  
> [!NOTE]  
>  To distribute copies of a subset of the filegroups in a database, use replication: replicate only those objects in the filegroups you want to copy to other servers. For more information about replication, see [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md).  
  
### Creating the Mirror Database  
 The mirror database is created by restoring, WITH NORECOVERY, backups of the principal database on the mirror server. The restore must keep the same database name. For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
 You can create the mirror database by using use a piecemeal restore sequence, where supported. However, you cannot start mirroring until you have restored all the filegroups and, typically, restored log backups to get the mirror database close enough in time with the principal database. For more information, see [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md).  
  
### Restrictions on Backup and Restore During Mirroring  
 While a database mirroring session is active, the following restrictions apply:  
  
-   Backup and restore of the mirror database are not allowed.  
  
-   Backup of the principal database is allowed, but BACKUP LOG WITH NORECOVERY is not allowed.  
  
-   Restoring the principal database is not allowed.  
  
##  <a name="PiecemealAndFTIndexes"></a> Piecemeal Restore and Full-Text Indexes  
 This section is relevant only for databases that contain multiple filegroups and, for the simple-model databases, only for read-only filegroups.  
  
 Full-text indexes are stored in database filegroups and can be affected by a piecemeal restore. If the full-text index resides in the same filegroup as any of the associated table data, piecemeal restore works as expected.  
  
> [!NOTE]  
>  To view the filegroup ID of the filegroup that contains a full-text index, select the data_space_id column of [sys.fulltext_indexes](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md).  
  
### Full-Text Indexes and Tables in Separate Filegroups  
 If a full-text index resides in a separate filegroup from all of the associated table data, the behavior of piecemeal restore depends on which of the filegroups is restored and brought online first:  
  
-   If the filegroup that contains the full-text index is restored and brought online before the filegroups that contain the associated table data, full-text search works as expected as soon as the full-text index is online.  
  
-   If the filegroup that contains the table data is restored and brought online before the filegroup that contains the full-text index, full-text behavior might be affected. This is because [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that trigger a population, rebuild the catalog, or reorganize the catalog fail until the index is brought online. These statements include CREATE FULLTEXT INDEX, ALTER FULLTEXT INDEX, DROP FULLTEXT INDEX, and ALTER FULLTEXT CATALOG.  
  
     In this case, the following factors are significant:  
  
    -   If the full-text index has change tracking, user DML will fail until the index filegroup is brought online. Delete operation will also fail until the index filegroup is online.  
  
    -   Regardless of change tracking, full-text queries fail because the index is not available. If a full-text query is tried when the filegroup that contains the full-text index is offline, an error is returned.  
  
    -   Status functions (such as FULLTEXTCATALOGPROPERTY) succeed only when they do not have to access full-text index. For example, access to any online full-text metadata would succeed, but **uniquekeycount, itemcount** would fail.  
  
     After the full-text index filegroup is restored and brought online, the index data and table data are consistent.  
  
 As soon as both the base table filegroup and the full-text index filegroup are online, any paused full-text population is resumed.  
  
##  <a name="FileBnRandCompression"></a> File Backup and Restore and Compression  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports NTFS file system data compression of read-only filegroups and read-only databases.  
  
 Restoring files in a read-only filegroup is supported on compressed NTFS files. Backup and restore of these filegroups works essentially as it would for any read-only filegroup, with the following exceptions:  
  
-   Restoring a read-write file (including the primary or log files of a read-write database) to a compressed volume fails and displays an error.  
  
-   Restoring a read-only database to a compressed volume is allowed.  
  
> [!NOTE]  
>  Log files of read/write databases should never be placed on compressed file systems.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)  
  
-   [Back Up and Restore Full-Text Catalogs and Indexes](../../relational-databases/search/back-up-and-restore-full-text-catalogs-and-indexes.md)  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md)   
[Active Secondaries: Backup on Secondary Replicas \(Always On Availability Groups\)](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md)  
  
  
