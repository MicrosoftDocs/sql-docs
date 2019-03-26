---
title: "Revert a Database to a Database Snapshot | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "database snapshots [SQL Server], reverting to"
  - "reverting databases"
ms.assetid: 8f74dd31-c9ca-4537-8760-0c7648f0787d
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Revert a Database to a Database Snapshot
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  If data in an online database becomes damaged, in some cases, reverting the database to a database snapshot that predates the damage might be an appropriate alternative to restoring the database from a backup. For example, reverting a database might be useful for reverse a recent serious user error, such as a dropped table. However, all changes made after the snapshot was created are lost.  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To Revert a Database to a Database Snapshot, using:**  [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Reverting is unsupported under the following conditions:  
  
-   There are multiple snapshots for the database. For reverting, there must only be one snapshot for the database, to which you plan to revert.  
  
-   Any read-only or compressed filegroups exist in the database.  
  
-   Any files are now offline but were online when the snapshot was created.  
  
 Before reverting a database, consider the following limitations:  
  
-   Reverting is not intended for media recovery. A database snapshot is an incomplete copy of the database files, so if either the database or the database snapshot is corrupted, reverting from a snapshot is likely to be impossible. Furthermore, even when it is possible, reverting in the event of corruption is unlikely to correct the problem. Therefore, taking regular backups and testing your restore plan are essential to protect a database. For more information, see [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).  
  
    > [!NOTE]  
    >  If you need to be able to restore the source database to the point in time at which you created a database snapshot, use the full recovery model and implement a backup policy that enables you to do that.  
  
-   The original source database is overwritten by the reverted database, so any updates to the database since the snapshot's creation are lost.  
  
-   The revert operation also overwrites the old log file and rebuilds the log. Consequently, you cannot roll the reverted database forward to the point of user error. Therefore, we recommend that you back up the log before reverting a database.  
  
    > [!NOTE]  
    >  Although you cannot restore the original log to roll forward the database, the information in the original log file can be useful for reconstructing lost data.  
  
-   Reverting breaks the log backup chain. Therefore, before you can take log backups of the reverted database, you must first take a full database backup or file backup. We recommend a full database backup.  
  
-   During a revert operation, both the snapshot and the source database are unavailable. The source database and snapshot are both marked "In restore." If an error occurs during the revert operation, when the database starts up again, the revert operation will try to finish reverting.  
  
-   The metadata of a reverted database is the same as the metadata at the time of the snapshot.  
  
-   Reverting drops all the full-text catalogs.  
  
###  <a name="Prerequisites"></a> Prerequisites  
 Ensure that the source database and database snapshot meet the following prerequisites:  
  
-   Verify that the database has not become corrupted.  
  
    > [!NOTE]  
    >  If the database has been corrupted, you will need to restore it from backups. For more information, see [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md) or [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md).  
  
-   Identify a recent snapshot that was created before the error. For more information, see [View a Database Snapshot &#40;SQL Server&#41;](../../relational-databases/databases/view-a-database-snapshot-sql-server.md).  
  
-   Drop any other snapshots that currently exist on the database. For more information, see [Drop a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Any user who has RESTORE DATABASE permissions on the source database can revert it to its state when a database snapshot was created.  
  
##  <a name="TsqlProcedure"></a> How to Revert a Database to a Database Snapshot (Using Transact-SQL)  
 **To revert a database to a database snapshot**  
  
> [!NOTE]  
>  For an example of this procedure, see [Examples (Transact-SQL)](#TsqlExample), later in this section.  
  
1.  Identify the database snapshot to which you want to revert the database. You can view the snapshots on a database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (see [View a Database Snapshot &#40;SQL Server&#41;](../../relational-databases/databases/view-a-database-snapshot-sql-server.md)). Also, you can identify the source database of a view from the **source_database_id** column of the [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
2.  Drop any other database snapshots.  
  
     For information on dropping snapshots, see [Drop a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md). If the database uses the full recovery model, before reverting, you should back up the log. For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md) or [Back Up the Transaction Log When the Database Is Damaged &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md).  
  
3.  Perform the revert operation.  
  
     A revert operation requires RESTORE DATABASE permissions on the source database. To revert the database, use the following Transact-SQL statement:  
  
     RESTORE DATABASE *database_name* FROM DATABASE_SNAPSHOT **=**_database_snapshot_name_  
  
     Where *database_name* is the source database and *database_snapshot_name* is the name of the snapshot to which you want to revert the database. Notice that in this statement, you must specify a snapshot name rather than a backup device.  
  
     For more information, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
    > [!NOTE]  
    >  During the revert operation, both the snapshot and the source database are unavailable. The source database and snapshot are both marked as "In restore." If an error occurs during the revert operation, it will try to finish reverting when the database starts up again.  
  
4.  If the database owner changed since creation of the database snapshot, you may want to update the database owner of the reverted database.  
  
    > [!NOTE]  
    >  The reverted database retains the permissions and configuration (such as database owner and recovery model) of the database snapshot.  
  
5.  Start the database.  
  
6.  Optionally, back up the reverted database, especially if it uses the full (or bulk-logged) recovery model. To back up a database, see [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This section contains the following examples of reverting a database to a database snapshot:  
  
-   A. [Reverting a snapshot on the AdventureWorks database](#Reverting_AW)  
  
-   B. [Reverting a snapshot on the Sales database](#Reverting_Sales)  
  
####  <a name="Reverting_AW"></a> A. Reverting a snapshot on the AdventureWorks database  
 This example assumes that only one snapshot currently exists on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. For the example that creates the snapshot to which the database is reverted here, see [Create a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md).  
  
```  
USE master;  
-- Reverting AdventureWorks to AdventureWorks_dbss1800  
RESTORE DATABASE AdventureWorks from   
DATABASE_SNAPSHOT = 'AdventureWorks_dbss1800';  
GO  
```  
  
####  <a name="Reverting_Sales"></a> B. Reverting a snapshot on the Sales database  
 This example assumes that two snapshots currently exist on the **Sales** database: **sales_snapshot0600** and **sales_snapshot1200**. The example deletes the older of the snapshots and reverts the database to the more recent snapshot.  
  
 For the code for creating the sample database and snapshots on which this example depends, see:  
  
-   For the **Sales** database and the **sales_snapshot0600** snapshot, see "Creating a database with filegroups" and "Creating a database snapshot" in [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md).  
  
-   For the **sales_snapshot1200** snapshot, see "Creating a snapshot on the Sales database" in [Create a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md).  
  
```  
--Test to see if sales_snapshot0600 exists and if it   
-- does, delete it.  
IF EXISTS (SELECT dbid FROM sys.databases  
    WHERE NAME='sales_snapshot0600')  
    DROP DATABASE SalesSnapshot0600;  
GO  
-- Reverting Sales to sales_snapshot1200  
USE master;  
RESTORE DATABASE Sales FROM DATABASE_SNAPSHOT = 'sales_snapshot1200';  
GO  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md)  
  
-   [View a Database Snapshot &#40;SQL Server&#41;](../../relational-databases/databases/view-a-database-snapshot-sql-server.md)  
  
-   [Drop a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md)  
  
## See Also  
 [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [Database Mirroring and Database Snapshots &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-database-snapshots-sql-server.md)  
  
  
