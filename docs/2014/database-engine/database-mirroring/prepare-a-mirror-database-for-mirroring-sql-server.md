---
title: "Prepare a Mirror Database for Mirroring (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], preparing for mirroring"
  - "logins [SQL Server], database mirroring"
  - "mirror database [SQL Server]"
ms.assetid: 8676f9d8-c451-419b-b934-786997d46c2b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Prepare a Mirror Database for Mirroring (SQL Server)
  Before a database mirroring session can start, the database owner or system administrator must make sure that the mirror database has been created and is ready for mirroring. Creating a new mirror database minimally requires taking a full backup of the principal database and a subsequent log backup and restoring them both onto the mirror server instance, using WITH NORECOVERY.  
  
 This topic describes how to prepare a mirror database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Requirements"></a> Requirements  
  
-   The principal and mirror server instances must be running on the same version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. While it is possible for the mirror server to have a higher version of SQL Server, this configuration is only recommended during a carefully planned upgrade process. In such a configuration, you run the risk of an automatic failover, in which data movement is automatically suspended because data cannot move to a lower version of SQL Server. For more information, see [Minimize Downtime for Mirrored Databases When Upgrading Server Instances](upgrading-mirrored-instances.md).  
  
-   The principal and mirror server instances must be running on the same edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For information about support for database mirroring in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
-   The database must use the full recovery model.  
  
     For more information, see [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server.md) or [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql) and [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql).  
  
-   The name of the mirror database must be the same as the name of the principal database.  
  
-   The mirror database must be in the RESTORING state for mirroring to work. When preparing a mirror database, you must use RESTORE WITH NORECOVERY for every restore operation. Minimally, you will need to restore WITH NORECOVERY a full backup of the principal database, followed by all subsequent log backups.  
  
-   The system where you plan to create the mirror database must possesses a disk drive with sufficient space to hold the mirror database.  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   You cannot mirror the **master**, **msdb**, **temp**, or **model** system databases.  
  
-   You cannot mirror a database that belongs to an [AlwaysOn Availability Groups (SQL Server)](../availability-groups/windows/always-on-availability-groups-sql-server.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   Use a very recent full database backup or a recent differential database backup of the principal database.  
  
-   If a log backup job is scheduled to run very frequently on the principal database, you might have to disable the backup job until mirroring has started.  
  
-   If possible, the path (including the drive letter) of the mirror database should be identical to the path of the principal database.  
  
     If the file paths must differ, for example, if the principal database is on drive 'F:' but the mirror system lacks an F: drive, you must include the MOVE option in the RESTORE STATEMENT.  
  
    > [!IMPORTANT]  
    >  Adding a file during a mirroring session without impacting the session requires that the path of the file exists on both servers. Therefore, if you move the database files when creating the mirror database, a later add-file operation might fail on the mirror database and cause mirroring to be suspended. For information about dealing with a failed create-file operation, see [Troubleshoot Database Mirroring Configuration &#40;SQL Server&#41;](troubleshoot-database-mirroring-configuration-sql-server.md).  
  
-   If the principal database has any full-text catalogs, we recommend that you see [Database Mirroring and Full-Text Catalogs &#40;SQL Server&#41;](database-mirroring-and-full-text-catalogs-sql-server.md).  
  
-   For a production database, always back up to a separate device.  
  
###  <a name="Security"></a> Security  
 TRUSTWORTHY is set to OFF when a database is backed up. Therefore, TRUSTWORTHY is always OFF on a new mirror database. If the database needs to be trustworthy after a failover, additional setup steps are necessary. For more information, see [Set Up a Mirror Database to Use the Trustworthy Property &#40;Transact-SQL&#41;](set-up-a-mirror-database-to-use-the-trustworthy-property-transact-sql.md).  
  
 For information about enabling automatic decryption of the database master key of a mirror database, see [Set Up an Encrypted Mirror Database](set-up-an-encrypted-mirror-database.md).  
  
####  <a name="Permissions"></a> Permissions  
 Database owner or system administrator.  
  
##  <a name="PrepareToRestartMirroring"></a> To Prepare an Existing Mirror Database to Restart Mirroring  
 If mirroring has been removed and the mirror database is still in the RECOVERING state, you can restart mirroring.  
  
1.  Take at least one log backup on the principal database. For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md).  
  
2.  On the mirror database, use RESTORE WITH NORECOVERY to restore all log backups taken on the principal database since mirroring was removed. For more information, see [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md).  
  
##  <a name="CombinedProcedure"></a> To Prepare a New Mirror Database  
 **To prepare a mirror database**  
  
> [!NOTE]  
>  For a [!INCLUDE[tsql](../../includes/tsql-md.md)] example of this procedure, see [Example (Transact-SQL)](#TsqlExample), later in this section.  
  
1.  Connect to principal server instance.  
  
2.  Create either a full database backup or a differential database backup of the principal database.  
  
    -   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
    -   [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md).  
  
3.  Typically, you need to take at least one log backup on the principal database. However, a log backup might be unnecessary, if the database has just been created and no log backup has been taken yet, or if the recovery model has just been changed from SIMPLE to FULL.  
  
    -   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
4.  Unless the backups are on a network drive that is accessible from both systems, copy the database and log backups to the system that will host the mirror server instance.  
  
5.  Connect to mirror server instance.  
  
6.  Using RESTORE WITH NORECOVERY, create the mirror database by restoring the full database backup and, optionally, the most recent differential database backup, onto the mirror server instance.  
  
    > [!NOTE]  
    >  If you restore the database filegroup by filegroup, be sure to restore the whole database.  
  
    -   [Restore a Database Backup &#40;SQL Server Management Studio&#41;](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)  
  
    -   [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql) and [RESTORE Arguments &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-arguments-transact-sql).  
  
7.  Using RESTORE WITH NORECOVERY, apply any outstanding log backup or backups to the mirror database.  
  
    -   [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 Before you can start a database mirroring session, you must create the mirror database. You should do this just before starting the mirroring session.  
  
 This example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database, which uses the simple recovery model by default.  
  
1.  To use database mirroring with the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, modify it to use the full recovery model:  
  
    ```  
    USE master;  
    GO  
    ALTER DATABASE AdventureWorks   
    SET RECOVERY FULL;  
    GO  
    ```  
  
2.  After modifying the recovery model of the database from SIMPLE to FULL, create a full backup, which can be used to create the mirror database. Because the recovery model has just been changed, the WITH FORMAT option is specified to create a new media set. This is useful to separate the backups under the full recovery model from any previous backups made under the simple recovery model. For the purpose of this example, the backup file (`C:\AdventureWorks.bak`) is created on the same drive as the database.  
  
    > [!NOTE]  
    >  For a production database, you should always back up to a separate device.  
  
     On the principal server instance (on `PARTNERHOST1`), create a full backup of the principal database as follows:  
  
    ```  
    BACKUP DATABASE AdventureWorks   
        TO DISK = 'C:\AdventureWorks.bak'   
        WITH FORMAT  
    GO  
    ```  
  
3.  Copy the full backup to the mirror server.  
  
4.  Using RESTORE WITH NORECOVERY, restore the full backup onto the mirror server instance. The restore command depends on whether the paths of principal and mirror databases are identical.  
  
    -   **If the paths are identical:**  
  
         On the mirror server instance (on `PARTNERHOST5`), restore the full backup as follows:  
  
        ```  
        RESTORE DATABASE AdventureWorks   
            FROM DISK = 'C:\AdventureWorks.bak'   
            WITH NORECOVERY  
        GO  
        ```  
  
    -   **If the paths differ:**  
  
         If the path of the mirror database differs from the path of the principal database (for instance, their drive letters differ), creating the mirror database requires that the restore operation include a MOVE clause.  
  
        > [!IMPORTANT]  
        >  If the path names of the principal and mirror databases differ, you cannot add a file. This is because on receiving the log for the add file operation, the mirror server instance attempts to place the new file in the location used by the principal database.  
  
         For example, the following command restores a backup of a principal database residing in C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\Data\ to a different location, D:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\Dat`a\`, where the mirror database is to reside.  
  
        ```  
        RESTORE DATABASE AdventureWorks  
           FROM DISK='C:\AdventureWorks.bak'  
           WITH NORECOVERY,   
              MOVE 'AdventureWorks_Data' TO   
                 'D:\Program Files\Microsoft SQL Server\MSSQL.n\MSSQL\Data\AdventureWorks_Data.mdf',   
              MOVE 'AdventureWorks_Log' TO  
                 'D:\Program Files\Microsoft SQL Server\MSSQL.n\MSSQL\Data\AdventureWorks_Log.ldf';  
        GO  
        ```  
  
5.  After you create the full backup, you must create a log backup on the principal database. For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement backs up the log to the same file used by the preceding full backup:  
  
    ```  
    BACKUP LOG AdventureWorks   
        TO DISK = 'C:\AdventureWorks.bak'   
    GO  
    ```  
  
6.  Before you can start mirroring, you must apply the required log backup (and any subsequent log backups).  
  
     For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement restores the first log from `C:\AdventureWorks.bak`:  
  
    ```  
    RESTORE LOG AdventureWorks   
        FROM DISK = 'C:\AdventureWorks.bak'   
        WITH FILE=1, NORECOVERY  
    GO  
    ```  
  
7.  If any additional log backups occur before you start mirroring, you must also restore all of those log backups, in sequence, to the mirror server using WITH NORECOVERY.  
  
     For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement restores two additional logs from `C:\AdventureWorks.bak`:  
  
    ```  
    RESTORE LOG AdventureWorks   
        FROM DISK = 'C:\AdventureWorks.bak'   
        WITH FILE=2, NORECOVERY  
    GO  
    RESTORE LOG AdventureWorks   
        FROM DISK = 'C:\AdventureWorks.bak'   
        WITH FILE=3, NORECOVERY  
    GO  
    ```  
  
 For a complete example of setting up database mirroring, showing security setup, preparing the mirror database, setting up the partners, and adding a witness, see [Setting Up Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After Preparing a Mirror Database  
  
1.  If any additional log backups have been taken since your most recent RESTORE LOG operation, you must manually apply every additional log backup, using RESTORE WITH NORECOVERY.  
  
2.  Start the mirroring session. For more information, see [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](establish-database-mirroring-session-windows-authentication.md) or [Establish a Database Mirroring Session Using Windows Authentication &#40;Transact-SQL&#41;](database-mirroring-establish-session-windows-authentication.md).  
  
3.  If you disabled the backup job on the principal database, reenable the job.  
  
4.  If the database needs to be trustworthy after a failover, extra setup steps are necessary after mirroring begins. For more information, see [Set Up a Mirror Database to Use the Trustworthy Property &#40;Transact-SQL&#41;](set-up-a-mirror-database-to-use-the-trustworthy-property-transact-sql.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](establish-database-mirroring-session-windows-authentication.md)  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;Transact-SQL&#41;](database-mirroring-establish-session-windows-authentication.md)  
  
-   [Set Up an Encrypted Mirror Database](set-up-an-encrypted-mirror-database.md)  
  
-   [Set Up a Mirror Database to Use the Trustworthy Property &#40;Transact-SQL&#41;](set-up-a-mirror-database-to-use-the-trustworthy-property-transact-sql.md)  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Transport Security for Database Mirroring and AlwaysOn Availability Groups &#40;SQL Server&#41;](transport-security-database-mirroring-always-on-availability.md)   
 [Setting Up Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Back Up and Restore Full-Text Catalogs and Indexes](../../relational-databases/indexes/indexes.md)   
 [Database Mirroring and Full-Text Catalogs &#40;SQL Server&#41;](database-mirroring-and-full-text-catalogs-sql-server.md)   
 [Database Mirroring and Replication &#40;SQL Server&#41;](database-mirroring-and-replication-sql-server.md)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [RESTORE Arguments &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-arguments-transact-sql)  
  
  
