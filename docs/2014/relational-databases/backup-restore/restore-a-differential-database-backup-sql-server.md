---
title: "Restore a Differential Database Backup (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "full differential backups [SQL Server]"
  - "restoring databases [SQL Server], full differential backups"
  - "database backups [SQL Server], full differential backups"
  - "database restores [SQL Server], full differential backups"
  - "backing up databases [SQL Server], full differential backups"
ms.assetid: 0dd971a4-ee38-4dd3-9f30-ef77fc58dd11
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore a Differential Database Backup (SQL Server)
  This topic describes how to restore a differential database backup in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To restore a differential database backup, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   RESTORE is not allowed in an explicit or implicit transaction.  
  
-   Backups that are created by more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be restored in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can restore a user database from a database backup that was created by using [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Under the full or bulk-logged recovery model, before you can restore a database, you must back up the active transaction log (known as the tail of the log). For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database (for the FROM DATABASE_SNAPSHOT option, the database always exists).  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To restore a differential database backup  
  
1.  After you connect to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**. Depending on the database, either select a user database or expand **System Databases**, and then select a system database.  
  
3.  Right-click the database, point to **Tasks**, point to **Restore**, and then click **Database**.  
  
4.  On the **General** page, use the **Source** section to specify the source and location of the backup sets to restore. Select one of the following options:  
  
    -   **Database**  
  
         Select the database to restore from the drop-down list. The list contains only databases that have been backed up according to the **msdb** backup history.  
  
    > [!NOTE]  
    >  If the backup is taken from a different server, the destination server will not have the backup history information for the specified database. In this case, select **Device** to manually specify the file or device to restore.  
  
    -   **Device**  
  
         Click the browse (**...**) button to open the **Select backup devices** dialog box. In the **Backup media type** box, select one of the listed device types. To select one or more devices for the **Backup media** box, click **Add**.  
  
         After you add the devices you want to the **Backup media** list box, click **OK** to return to the **General** page.  
  
         In the **Source: Device: Database** list box, select the name of the database which should be restored.  
  
         **Note** This list is only available when **Device** is selected. Only databases that have backups on the selected device will be available.  
  
5.  In the **Destination** section, the **Database** box is automatically populated with the name of the database to be restored. To change the name of the database, enter the new name in the **Database** box.  
  
    > [!NOTE]  
    >  To stop the restore at a specific point in time, click **Timeline** to access the **Backup Timeline** dialog box. For help with stopping a database restore at a specific point in time, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).  
  
6.  In the **Backup sets to restore** grid, select the backups through the differential backup that you wish to restore.  
  
     For information about the columns in the **Backup sets to restore** grid, see [Restore Database &#40;General Page&#41;](../../integration-services/general-page-of-integration-services-designers-options.md).  
  
7.  On the **Options** page, in the **Restore options** panel, you can select any of the following options, if appropriate for your situation:  
  
    -   **Overwrite the existing database (WITH REPLACE)**  
  
    -   **Preserve the replication settings (WITH KEEP_REPLICATION)**  
  
    -   **Prompt before restoring each backup**  
  
    -   **Restrict access to the restored database (WITH RESTRICTED_USER)**  
  
     For more information about these options, see [Restore Database &#40;Options Page&#41;](restore-database-options-page.md).  
  
8.  Select an option for the **Recovery state** box. This box determines the state of the database after the restore operation.  
  
    -   **RESTORE WITH RECOVERY** is the default behavior which leaves the database ready for use by rolling back the uncommitted transactions. Additional transaction logs cannot be restored. Select this option if you are restoring all of the necessary backups now.  
  
    -   **RESTORE WITH NORECOVERY** which leaves the database non-operational, and does not roll back the uncommitted transactions. Additional transaction logs can be restored. The database cannot be used until it is recovered.  
  
    -   **RESTORE WITH STANDBY** which leaves the database in read-only mode. It undoes uncommitted transactions, but saves the undo actions in a standby file so that recovery effects can be reverted.  
  
     For descriptions of the options, see [Restore Database &#40;Options Page&#41;](restore-database-options-page.md).  
  
9. Restore operations will fail if there are active connections to the database. Check the **Close existing connections option** to ensure that all active connections between [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and the database are closed.  
  
10. Select **Prompt before restoring each backup** if you wish to be prompted between each restore operation. This is not usually necessary unless the database is large and you wish to monitor the status of the restore operation.  
  
11. Optionally, use the **Files** page to restore the database to a new location. For help with moving a database, see [Restore a Database to a New Location &#40;SQL Server&#41;](restore-a-database-to-a-new-location-sql-server.md).  
  
12. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To restore a differential database backup  
  
1.  Execute the RESTORE DATABASE statement, specifying the NORECOVERY clause, to restore the full database backup that comes before the differential database backup. For more information, see [How to: Restore a Full Backup](restore-a-database-backup-under-the-simple-recovery-model-transact-sql.md).  
  
2.  Execute the RESTORE DATABASE statement to restore the differential database backup, specifying:  
  
    -   The name of the database to which the differential database backup is applied.  
  
    -   The backup device where the differential database backup is restored from.  
  
    -   The NORECOVERY clause if you have transaction log backups to apply after the differential database backup is restored. Otherwise, specify the RECOVERY clause.  
  
3.  With the full or bulk-logged recovery model, restoring a differential database backup restores the database to the point at which the differential database backup was completed. To recover to the point of failure, you must apply all transaction log backups created after the last differential database backup was created. For more information, see [Apply Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md).  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
  
#### A. Restoring a differential database backup  
 This example restores a database and differential database backup of the `MyAdvWorks` database.  
  
```sql  
-- Assume the database is lost, and restore full database,   
-- specifying the original full database backup and NORECOVERY,   
-- which allows subsequent restore operations to proceed.  
RESTORE DATABASE MyAdvWorks  
   FROM MyAdvWorks_1  
   WITH NORECOVERY;  
GO  
-- Now restore the differential database backup, the second backup on   
-- the MyAdvWorks_1 backup device.  
RESTORE DATABASE MyAdvWorks  
   FROM MyAdvWorks_1  
   WITH FILE = 2,  
   RECOVERY;  
GO  
```  
  
#### B. Restoring a database, differential database, and transaction log backup  
 This example restores a database, differential database, and transaction log backup of the `MyAdvWorks` database.  
  
```sql  
-- Assume the database is lost at this point. Now restore the full   
-- database. Specify the original full database backup and NORECOVERY.  
-- NORECOVERY allows subsequent restore operations to proceed.  
RESTORE DATABASE MyAdvWorks  
   FROM MyAdvWorks_1  
   WITH NORECOVERY;  
GO  
-- Now restore the differential database backup, the second backup on   
-- the MyAdvWorks_1 backup device.  
RESTORE DATABASE MyAdvWorks  
   FROM MyAdvWorks_1  
   WITH FILE = 2,  
   NORECOVERY;  
GO  
-- Now restore each transaction log backup created after  
-- the differential database backup.  
RESTORE LOG MyAdvWorks  
   FROM MyAdvWorks_log1  
   WITH NORECOVERY;  
GO  
RESTORE LOG MyAdvWorks  
   FROM MyAdvWorks_log2  
   WITH RECOVERY;  
GO  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](create-a-differential-database-backup-sql-server.md)  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](restore-a-transaction-log-backup-sql-server.md)  
  
## See Also  
 [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)  
  
  
