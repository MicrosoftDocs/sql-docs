---
title: "Restore a Transaction Log Backup (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.restoretlog.options.f1"
  - "sql12.swb.restoretlog.general.f1"
helpviewer_keywords: 
  - "restore log"
  - "backing up transaction logs [SQL Server], restoring"
  - "transaction log backups [SQL Server], restoring"
  - "restoring transaction logs [SQL Server], restoring backups"
  - "transaction log restores [SQL Server], SQL Server Management Studio"
ms.assetid: 1de2b888-78a6-4fb2-a647-ba4bf097caf3
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore a Transaction Log Backup (SQL Server)
  This topic describes how to restore a transaction log backup in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To restore a transaction log backup, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Backups must be restored in the order in which they were created. Before you can restore a particular transaction log backup, you must first restore the following previous backups without rolling back uncommitted transactions, that is WITH NORECOVERY:  
  
    -   The full database backup and the last differential backup, if any, taken before the particular transaction log backup. Before the most recent full or differential database backup was created, the database must have been using the full recovery model or bulk-logged recovery model.  
  
    -   All transaction log backups taken after the full database backup or the differential backup (if you restore one) and before the particular transaction log backup. Log backups must be applied in the sequence in which they were created, without any gaps in the log chain.  
  
         For more information about transaction log backups, see [Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md) and [Apply Transaction Log Backups &#40;SQL Server&#41;](apply-transaction-log-backups-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
> [!WARNING]  
>  The normal process of a restore is to select the log backups in the **Restore Database** dialog box along with the data and differential backups.  
  
#### To restore a transaction log backup  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, point to **Tasks**, point to **Restore**, and then click **Transaction Log**, which opens the **Restore Transaction Log** dialog box.  
  
    > [!NOTE]  
    >  If **Transaction Log** is grayed out, you may need to restore a full or differential backup first. Use the **Database** backup dialog box.  
  
4.  On the **General** page, in the **Database** list box, select the name of a database. Only databases in the restoring state are listed.  
  
5.  To specify the source and location of the backup sets to restore, click one of the following options:  
  
    -   **From previous backups of database**  
  
         Select the database to restore from the drop-down list. The list contains only databases that have been backed up according to the **msdb** backup history.  
  
    -   **From file or tape**  
  
         Click the browse (**...**) button to open the **Select backup devices** dialog box. In the **Backup media type** box, select one of the listed device types. To select one or more devices for the **Backup media** box, click **Add**.  
  
         After you add the devices you want to the **Backup media** list box, click **OK** to return to the **General** page.  
  
6.  In the **Select the transaction log backups to restore** grid, select the backups to restore. This grid lists the transaction log backups available for the selected database. A log backup is available only if its **First LSN** greater than the **Last LSN** of the database. Log backups are listed in the order of the log sequence numbers (LSN) they contain, and they must be restored in this order.  
  
     The following table lists the column headers of the grid and describes their values.  
  
    |Header|Value|  
    |------------|-----------|  
    |**Restore**|Selected check boxes indicate the backup sets to be restored.|  
    |**Name**|Name of the backup set.|  
    |**Component**|Backed-up component: **Database**, **File**, or \<blank> (for transaction logs).|  
    |**Database**|Name of the database involved in the backup operation.|  
    |**Start Date**|Date and time when the backup operation began, presented in the regional setting of the client.|  
    |**Finish Date**|Date and time when the backup operation finished, presented in the regional setting of the client.|  
    |**First LSN**|Log sequence number of the first transaction in the backup set. Blank for file backups.|  
    |**Last LSN**|Log sequence number of the last transaction in the backup set. Blank for file backups.|  
    |**Checkpoint LSN**|Log sequence number of the most recent checkpoint at the time the backup was created.|  
    |**Full LSN**|Log sequence number of the most recent full database backup.|  
    |**Server**|Name of the Database Engine instance that performed the backup operation.|  
    |**User Name**|Name of the user who performed the backup operation.|  
    |**Size**|Size of the backup set in bytes.|  
    |**Position**|Position of the backup set in the volume.|  
    |**Expiration**|Date and time the backup set expires.|  
  
7.  Select one of the following:  
  
    -   **Point in time**  
  
         Either retain the default (**Most recent possible**) or select a specific date and time by clicking the browse button, which opens the **Point in Time Restore** dialog box.  
  
    -   **Marked transaction**  
  
         Restore the database to a previously marked transaction. Selecting this option launches the **Select Marked Transaction** dialog box, which displays a grid listing the marked transactions available in the selected transaction log backups.  
  
         By default, the restore is up to, but excluding, the marked transaction. To restore the marked transaction also, select **Include marked transaction**.  
  
         The following table lists the column headers of the grid and describes their values.  
  
        |Header|Value|  
        |------------|-----------|  
        |\<blank>|Displays a checkbox for selecting the mark.|  
        |**Transaction Mark**|Name of the marked transaction specified by the user when the transaction was committed.|  
        |**Date**|Date and time of the transaction when it was committed. Transaction date and time are displayed as recorded in the **msdbgmarkhistory** table, not in the client computer's date and time.|  
        |**Description**|Description of marked transaction specified by the user when the transaction was committed (if any).|  
        |**LSN**|Log sequence number of the marked transaction.|  
        |**Database**|Name of the database where the marked transaction was committed.|  
        |**User Name**|Name of the database user who committed the marked transaction.|  
  
8.  To view or select the advanced options, click **Options** in the **Select a page** pane.  
  
9. In the **Restore options** section, the choices are:  
  
    -   **Preserve the replication settings (WITH KEEP_REPLICATION)**  
  
         Preserves the replication settings when restoring a published database to a server other than the server where the database was created.  
  
         This option is available only with the **Leave the database ready for use by rolling back the uncommitted transactions...** option (described later), which is equivalent to restoring a backup with the `RECOVERY` option.  
  
         Checking this option is equivalent to using the `KEEP_REPLICATION` option in a [!INCLUDE[tsql](../../includes/tsql-md.md)]`RESTORE` statement.  
  
    -   **Prompt before restoring each backup**  
  
         Before restoring each backup set (after the first), this option brings up the **Continue with Restore** dialog box, which asks you to indicate whether you want to continue the restore sequence. This dialog displays the name of the next media set (if available), the backup set name, and backup set description.  
  
         This option is particularly useful when you must swap tapes for different media sets. For example, you can use it when the server has only one tape device. Wait until you are ready to proceed before clicking **OK**.  
  
         Clicking **No** leaves the database in the restoring state. At your convenience, you can continue the restore sequence after the last restore that completed. If the next backup is a data or differential backup, use the **Restore Database** task again. If the next backup is a log backup, use the **Restore Transaction Log** task.  
  
    -   **Restrict access to the restored database (WITH RESTRICTED_USER)**  
  
         Makes the restored database available only to the members of **db_owner**, **dbcreator**, or **sysadmin**.  
  
         Checking this option is synonymous to using the `RESTRICTED_USER` option in a [!INCLUDE[tsql](../../includes/tsql-md.md)]`RESTORE` statement.  
  
10. For the **Recovery state** options, specify the state of the database after the restore operation.  
  
    -   **Leave the database ready for use by rolling back uncommitted transactions. Additional transaction logs cannot be restored. (RESTORE WITH RECOVERY)**  
  
         Recovers the database. This option is equivalent to the `RECOVERY` option in a [!INCLUDE[tsql](../../includes/tsql-md.md)]`RESTORE` statement.  
  
         Choose this option only if you have no log files you want to restore.  
  
    -   **Leave the database non-operational, and do not roll back uncommitted transactions. Additional transaction logs can be restored. (RESTORE WITH NORECOVERY)**  
  
         Leaves the database unrecovered, in the `RESTORING` state. This option is equivalent to using the `NORECOVERY` option in a [!INCLUDE[tsql](../../includes/tsql-md.md)]`RESTORE` statement.  
  
         When you choose this option, the **Preserve replication settings** option is unavailable.  
  
        > [!IMPORTANT]  
        >  For a mirror or secondary database, always select this option.  
  
    -   **Leave the database in read-only mode. Undo uncommitted transactions, but save the undo actions in a file so that recovery effects can be reversed. (RESTORE WITH STANDBY)**  
  
         Leaves the database in a standby state. This option is equivalent to using the `STANDBY` option in a [!INCLUDE[tsql](../../includes/tsql-md.md)]`RESTORE` statement.  
  
         Choosing this option requires that you specify a standby file.  
  
11. Optionally, specify a standby file name in the **Standby file** text box. This option is required if you leave the database in read-only mode. You can browse for the standby file or type its pathname in the text box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
> [!IMPORTANT]  
>  We recommend that you always explicitly specify either WITH NORECOVERY or WITH RECOVERY in every RESTORE statement to eliminate ambiguity. This is particularly important when writing scripts.  
  
#### To restore a transaction log backup  
  
1.  Execute the RESTORE LOG statement to apply the transaction log backup, specifying:  
  
    -   The name of the database to which the transaction log will be applied.  
  
    -   The backup device where the transaction log backup will be restored from.  
  
    -   The NORECOVERY clause.  
  
     The basic syntax for this statement is as follows:  
  
     RESTORE LOG *database_name* FROM <backup_device> WITH NORECOVERY.  
  
     Where *database_name* is the name of database and <backup_device>is the name of the device that contains the log backup being restored.  
  
2.  Repeat step 1 for each transaction log backup you have to apply.  
  
3.  After restoring the last backup in your restore sequence, to recover the database use one of the following statements:  
  
    -   Recover the database as part of the last RESTORE LOG statement:  
  
        ```  
        RESTORE LOG <database_name> FROM <backup_device> WITH RECOVERY;  
        GO  
        ```  
  
    -   Wait to recover the database by using a separate RESTORE DATABASE statement:  
  
        ```  
        RESTORE LOG <database_name> FROM <backup_device> WITH NORECOVERY;   
        RESTORE DATABASE <database_name> WITH RECOVERY;  
        GO  
        ```  
  
         Waiting to recover the database gives you the opportunity to verify that you have restored all of the necessary log backups. This approach is often advisable when you are performing a point-in-time restore.  
  
    > [!IMPORTANT]  
    >  If you are creating a mirror database, omit the recovery step. A mirror database must remain in the RESTORING state.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 By default, the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database uses the simple recovery model. The following examples require modifying the database to use the full recovery model, as follows:  
  
```tsql  
ALTER DATABASE AdventureWorks2012 SET RECOVERY FULL;  
```  
  
#### A. Applying a single transaction log backup  
 The following example starts by restoring the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database by using a full database backup that resides on a backup device named `AdventureWorks2012_1`. The example then applies the first transaction log backup that resides on a backup device named `AdventureWorks2012_log`. Finally, the example recovers the database.  
  
```tsql  
RESTORE DATABASE AdventureWorks2012  
   FROM AdventureWorks2012_1  
   WITH NORECOVERY;  
GO  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorks2012_log  
   WITH FILE = 1,  
   WITH NORECOVERY;  
GO  
RESTORE DATABASE AdventureWorks2012  
   WITH RECOVERY;  
GO  
```  
  
#### B. Applying multiple transaction log backups  
 The following example starts by restoring the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database by using a full database backup that resides on a backup device named `AdventureWorks2012_1`. The example then applies, one by one, the first three transaction log backups that reside on a backup device named `AdventureWorks2012_log`. Finally, the example recovers the database.  
  
```tsql  
RESTORE DATABASE AdventureWorks2012  
   FROM AdventureWorks2012_1  
   WITH NORECOVERY;  
GO  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorks2012_log  
   WITH FILE = 1,  
   NORECOVERY;  
GO  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorks2012_log  
   WITH FILE = 2,  
   WITH NORECOVERY;  
GO  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorks2012_log  
   WITH FILE = 3,  
   WITH NORECOVERY;  
GO  
RESTORE DATABASE AdventureWorks2012  
   WITH RECOVERY;  
GO  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
-   [Restore a Database Backup &#40;SQL Server Management Studio&#41;](restore-a-database-backup-using-ssms.md)  
  
-   [Restore a Database to the Point of Failure Under the Full Recovery Model &#40;Transact-SQL&#41;](restore-database-to-point-of-failure-full-recovery.md)  
  
-   [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)  
  
-   [Restore a Database to a Marked Transaction &#40;SQL Server Management Studio&#41;](restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)  
  
## See Also  
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](apply-transaction-log-backups-sql-server.md)  
  
  
