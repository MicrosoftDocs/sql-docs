---
title: "Database Snapshots (SQL Server) | Microsoft Docs"
description: "Find out how to use database snapshots to create read-only, static views of a database in SQL Server. See their benefits, prerequisites, and limitations."
ms.custom: ""
ms.date: "08/08/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "static database views"
  - "snapshots [SQL Server database snapshots]"
  - "source databases [SQL Server]"
  - "snapshots [SQL Server database snapshots], about database snapshots"
  - "database snapshots [SQL Server]"
  - "read-only database views"
  - "database snapshots [SQL Server], about database snapshots"
ms.assetid: 00179314-f23e-47cb-a35c-da6f180f86d3
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Database Snapshots (SQL Server)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A database snapshot is a read-only, static view of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database (the *source database*). The database snapshot is transactionally consistent with the source database as of the moment of the snapshot's creation. A database snapshot always resides on the same server instance as its source database. While database snapshots provide a read-only view of the data in the same state as when the snapshot was created, the size of the snapshot file grows as changes are made to the source database. For details, see the [Feature Overview](#FeatureOverview) section below.
  
 Multiple snapshots can exist on a given source database. Each database snapshot persists until it is explicitly dropped by the database owner.  
  
> [!NOTE]  
>  Database snapshots are unrelated to snapshot backups, snapshot isolation of transactions, or snapshot replication.  
  
 **In this Topic:**  
  
-   [Feature Overview](#FeatureOverview)  
  
-   [Benefits of Database Snapshots](#Benefits)  
  
-   [Terms and Definitions](#TermsAndDefinitions)  
  
-   [Prerequisites for and Limitations on Database Snapshots](#LimitationsRequirements)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="FeatureOverview"></a> Feature Overview  
 Database snapshots operate at the data-page level. Before a page of the source database is modified for the first time, the original page is copied from the source database to the snapshot. The snapshot stores the original page, preserving the data records as they existed when the snapshot was created. The same process is repeated for every page that is being modified for the first time. To the user, a database snapshot appears never to change, because read operations on a database snapshot always access the original data pages, regardless of where they reside.  
  
 To store the copied original pages, the snapshot uses one or more *sparse files*. Initially, a sparse file is an essentially empty file that contains no user data and has not yet been allocated disk space for user data. As more and more pages are updated in the source database, the size of the file grows. The following figure illustrates the effects of two contrasting update patterns on the size of a snapshot. Update pattern A reflects an environment in which only 30 percent of the original pages are updated during the life of the snapshot. Update pattern B reflects an environment in which 80 percent of the original pages are updated during the life of the snapshot.  
  
 ![Alternative update patterns and snapshot size](../../relational-databases/databases/media/dbview-04.gif "Alternative update patterns and snapshot size")  
  
##  <a name="Benefits"></a> Benefits of Database Snapshots  
  
-   Snapshots can be used for reporting purposes.  
  
     Clients can query a database snapshot, which makes it useful for writing reports based on the data at the time of snapshot creation.  
  
-   Maintaining historical data for report generation.  
  
     A snapshot can extend user access to data from a particular point in time. For example, you can create a database snapshot at the end of a given time period (such as a financial quarter) for later reporting. You can then run end-of-period reports on the snapshot. If disk space permits, you can also maintain end-of-period snapshots indefinitely, allowing queries against the results from these periods; for example, to investigate organizational performance.  
  
-   Using a mirror database that you are maintaining for availability purposes to offload reporting.  
  
     Using database snapshots with database mirroring permits you to make the data on the mirror server accessible for reporting. Additionally, running queries on the mirror database can free up resources on the principal. For more information, see [Database Mirroring and Database Snapshots &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-database-snapshots-sql-server.md).  
  
-   Safeguarding data against administrative error.  
  
-   In the event of a user error on a source database, you can revert the source database to the state it was in when a given database snapshot was created. Data loss is confined to updates to the database since the snapshot's creation.  
  
     For example, before doing major updates, such as a bulk update or a schema change, create a database snapshot on the database protects data. If you make a mistake, you can use the snapshot to recover by reverting the database to the snapshot. Reverting is potentially much faster for this purpose than restoring from a backup; however, you cannot roll forward afterward.  
  
    > [!IMPORTANT]  
    >  Reverting does not work in an offline or corrupted database. Therefore, taking regular backups and testing your restore plan are necessary to protect a database.  
  
    > [!NOTE]  
    >  Database snapshots are dependent on the source database. Therefore, using database snapshots for reverting a database is not a substitute for your backup and restore strategy. Performing all your scheduled backups remains essential. If you must restore the source database to the point in time at which you created a database snapshot, implement a backup policy that enables you to do that.  
  
-   Safeguarding data against user error.  
  
     By creating database snapshots on a regular basis, you can mitigate the impact of a major user error, such as a dropped table. For a high level of protection, you can create a series of database snapshots spanning enough time to recognize and respond to most user errors. For instance, you might maintain 6 to 12 rolling snapshots spanning a 24-hour interval, depending on your disk resources. Then, each time a new snapshot is created, the earliest snapshot can be deleted.  
  
    -   To recover from a user error, you can revert the database to the snapshot immediately before the error. Reverting is potentially much faster for this purpose than restoring from a backup; however, you cannot roll forward afterward.  
  
    -   Alternatively, you may be able to manually reconstruct a dropped table or other lost data from the information in a snapshot. For instance, you could bulk copy the data out of the snapshot into the database and manually merge the data back into the database.  
  
    > [!NOTE]  
    >  Your reasons for using database snapshots determine how many concurrent snapshots you need on a database, how frequently to create a new snapshot, and how long to keep it.  
  
-   Managing a test database  
  
     In a testing environment, it can be useful when repeatedly running a test protocol for the database to contain identical data at the start of each round of testing. Before running the first round, an application developer or tester can create a database snapshot on the test database. After each test run, the database can be quickly returned to its prior state by reverting the database snapshot.  
  
##  <a name="TermsAndDefinitions"></a> Terms and Definitions  
 database snapshot  
 A transactionally consistent, read-only, static view of a database (the source database).  
  
 source database  
 For a database snapshot, the database on which the snapshot was created. Database snapshots are dependent on the source database. The snapshots of a database must be on the same server instance as the database. Furthermore, if that database becomes unavailable for any reason, all of its database snapshots also become unavailable.  
  
 sparse file  
 A file provided by the NTFS file system that requires much less disk space than would otherwise be needed. A sparse file is used to store pages copied to a database snapshot. When first created, a sparse file takes up little disk space. As data is written to a database snapshot, NTFS allocates disk space gradually to the corresponding sparse file.  
  
##  <a name="LimitationsRequirements"></a> Prerequisites for and Limitations on Database Snapshots  
 **In This Section:**  
  
-   [Prerequisites](#Prerequisites)  
  
-   [Limitations on the Source Database](#LimitsOnSourceDb)  
  
-   [Limitations on Database Snapshots](#LimitsOnDbSS)  
  
-   [Disk Space Requirements](#DiskSpace)  
  
-   [Database Snapshots with Offline Filegroups](#OfflineFGs)  
  
###  <a name="Prerequisites"></a> Prerequisites  
 The source database, which can use any recovery model, must meet the following prerequisites:  
  
-   The server instance must be running on an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that supports database snapshots. For more information, see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
-   The source database must be online, unless the database is a mirror database within a database mirroring session.  
  
-   You can create a database snapshot on any primary or secondary database in an availability group. The replica role must be either PRIMARY or SECONDARY, not in the RESOLVING state.  
  
     We recommend that the database synchronization state be SYNCHRONIZING or SYNCHRONIZED when you create a database snapshot. However, database snapshots can be created when the database synchronization state is NOT SYNCHRONIZING.  
  
     For more information, see [Database Snapshots with Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/database-snapshots-with-always-on-availability-groups-sql-server.md).  
  
-   To create a database snapshot on a mirror database, the database must be in the SYNCHRONIZED mirroring state.  
  
-   The source database cannot be configured as a scalable shared database.  

-   Prior to SQL Server 2019, the source database could not contain a MEMORY_OPTIMIZED_DATA filegroup. Support for in-memory database snapshots was added in SQL Server 2019.
  
> [!NOTE]  
>  All recovery models support database snapshots.  
  
###  <a name="LimitsOnSourceDb"></a> Limitations on the Source Database  
 As long as a database snapshot exists, the following limitations exist on the snapshot's source database:  
  
-   The database cannot be dropped, detached, or restored.  
  
    > [!NOTE]  
    >  Backing up the source database works normally; it is unaffected by database snapshots.  
  
-   Performance is reduced, due to increased I/O on the source database resulting from a copy-on-write operation to the snapshot every time a page is updated.  
  
-   Files cannot be dropped from the source database or from any snapshots.  
  
###  <a name="LimitsOnDbSS"></a> Limitations on Database Snapshots  
 The following limitations apply to database snapshots:  
  
-   A database snapshot must be created and remain on the same server instance as the source database.  
  
-   Database snapshots always work on an entire database.  
  
-   Database snapshots are dependent on the source database and are not redundant storage. They do not protect against disk errors or other types of corruption. Therefore, using database snapshots for reverting a database is not a substitute for your backup and restore strategy. Performing all your scheduled backups remains essential. If you must restore the source database to the point in time at which you created a database snapshot, implement a backup policy that enables you to do that.  
  
-   When a page getting updated on the source database is pushed to a snapshot, if the snapshot runs out of disk space or encounters some other error, the snapshot becomes suspect and must be deleted.  
  
-   Snapshots are read-only. Since they are read only, they cannot be upgraded. Therefore, database snapshots are not expected to be viable after an upgrade.  
  
-   Snapshots of the **model**, **master**, and **tempdb** databases are prohibited.  
  
-   You cannot change any of the specifications of the database snapshot files.  
  
-   You cannot drop files from a database snapshot.  
  
-   You cannot back up or restore database snapshots.  
  
-   You cannot attach or detach database snapshots.  
  
-   You cannot create database snapshots on FAT32 file system or RAW partitions. The sparse files used by database snapshots are provided by the NTFS file system.  
  
-   Full-text indexing is not supported on database snapshots. Full-text catalogs are not propagated from the source database.  
  
-   A database snapshot inherits the security constraints of its source database at the time of snapshot creation. Because snapshots are read-only, inherited permissions cannot be changed and permission changes made to the source will not be reflected in existing snapshots.  
  
-   A snapshot always reflects the state of filegroups at the time of snapshot creation: online filegroups remain online, and offline filegroups remain offline. For more information, see "Database Snapshots with Offline Filegroups" later in this topic.  
  
-   If a source database becomes RECOVERY_PENDING, its database snapshots may become inaccessible. After the issue on the source database is resolved, however, its snapshots should become available again.  
  
-   Reverting is unsupported for any NTFS read-only or NTFS compressed files  in the database.  Attempts to revert a database containing either of these types of filegroups will fail.  
  
-   In a log shipping configuration, database snapshots can be created only on the primary database, not on a secondary database. If you switch roles between the primary server instance and a secondary server instance, you must drop all the database snapshots before you can set the primary database up as a secondary database.  
  
-   A database snapshot cannot be configured as a scalable shared database.  
  
-   FILESTREAM filegroups are not supported by database snapshots. If FILESTREAM filegroups exist in a source database, they are marked as offline in its database snapshots, and the database snapshots cannot be used for reverting the database.  
  
    > [!NOTE]  
    >  A SELECT statement that is executed on a database snapshot must not specify a FILESTREAM column; otherwise, the following error message will be returned: `Could not continue scan with NOLOCK due to data movement.`  
  
-   When statistics on a read-only snapshot are missing or stale, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates and maintains temporary statistics in tempdb. For more information, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
###  <a name="DiskSpace"></a> Disk Space Requirements  
 Database snapshots consume disk space. If a database snapshot runs out of disk space, it is marked as suspect and must be dropped. (The source database, however, is not affected; actions on it continue normally.) Compared to a full copy of a database, however, snapshots are highly space efficient. A snapshot requires only enough storage for the pages that change during its lifetime. Generally, snapshots are kept for a limited time, so their size is not a major concern.  
  
 The longer you keep a snapshot, however, the more likely it is to use up available space. The maximum size to which a sparse file can grow is the size of the corresponding source database file at the time of the snapshot creation. If a database snapshot runs out of disk space, it must be deleted (dropped).  
  
> [!NOTE]  
>  Except for file space, a database snapshot consumes roughly as many resources as a database.  
  
###  <a name="OfflineFGs"></a> Database Snapshots with Offline Filegroups  
 Offline filegroups in the source database affect database snapshots when you try to do any of the following:  
  
-   Create a snapshot  
  
     When a source database has one or more offline filegroups, snapshot creation succeeds with the filegroups offline. Sparse files are not created for the offline filegroups.  
  
-   Take a filegroup offline  
  
     You can take a file offline in the source database. However, the filegroup remains online in database snapshots if it was online when the snapshot was created. If the queried data has changed since snapshot creation, the original data page will be accessible in the snapshot. However, queries that use the snapshot to access unmodified data in the filegroup are likely to fail with input/output (I/O) errors.  
  
-   Bring a filegroup online  
  
     You cannot bring a filegroup online in a database that has any database snapshots. If a filegroup is offline at the time of snapshot creation or is taken offline while a database snapshot exists, the filegroup remains offline. This is because bringing a file back online involves restoring it, which is not possible if a database snapshot exists on the database.  
  
-   Revert the source database to the snapshot  
  
     Reverting a source database to a database snapshot requires that all of the filegroups are online except for filegroups that were offline when the snapshot was created.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md)  
  
-   [View a Database Snapshot &#40;SQL Server&#41;](../../relational-databases/databases/view-a-database-snapshot-sql-server.md)  
  
-   [View the Size of the Sparse File of a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md)  
  
-   [Revert a Database to a Database Snapshot](../../relational-databases/databases/revert-a-database-to-a-database-snapshot.md)  
  
-   [Drop a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md)  
  
## See Also  
 [Database Mirroring and Database Snapshots &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-database-snapshots-sql-server.md)  
  
  

