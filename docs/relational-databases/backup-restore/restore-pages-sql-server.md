---
title: "Restore Pages (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.restorepage.general.f1"
helpviewer_keywords: 
  - "restoring pages [SQL Server]"
  - "pages [SQL Server], restoring"
  - "databases [SQL Server], damaged"
  - "page restores [SQL Server]"
  - "pages [SQL Server], damaged"
  - "restoring [SQL Server], pages"
ms.assetid: 07e40950-384e-4d84-9ac5-84da6dd27a91
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Restore Pages (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic describes how to restore pages in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The goal of a page restore is to restore one or more damaged pages without restoring the whole database. Typically, pages that are candidates for restore have been marked as "suspect" because of an error that is encountered when accessing the page. Suspect pages are identified in the [suspect_pages](../../relational-databases/system-tables/suspect-pages-transact-sql.md) table in the **msdb** database.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [When is a Page Restore Useful?](#WhenUseful)  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To restore pages, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="WhenUseful"></a> When is a Page Restore Useful?  
 A page restore is intended for repairing isolated damaged pages. Restoring and recovering a few individual pages might be faster than a file restore, reducing the amount of data that is offline during a restore operation. However, if you have to restore more than a few pages in a file, it is generally more efficient to restore the whole file. For example, if lots of pages on a device indicate a pending device failure, consider restoring the file, possibly to another location, and repairing the device.  
  
 Furthermore, not all page errors require a restore. A problem can occur in cached data, such as a secondary index, that can be resolved by recalculating the data. For example, if the database administrator drops a secondary index and rebuilds it, the corrupted data, although fixed, is not indicated as such in the [suspect_pages](../../relational-databases/system-tables/suspect-pages-transact-sql.md) table.  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Page restore applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that are using the full or bulk-logged recovery models. Page restore is supported only for read/write filegroups.  
  
-   Only database pages can be restored. Page restore cannot be used to restore the following:  
  
    -   Transaction log  
  
    -   Allocation pages: Global Allocation Map (GAM) pages, Shared Global Allocation Map (SGAM) pages, and Page Free Space (PFS) pages.  
  
    -   Page 0 of all data files (the file boot page)  
  
    -   Page 1:9 (the database boot page)  
  
    -   Full-text catalog  
  
-   For a database that uses the bulk-logged recovery model, page restore has the following additional conditions:  
  
    -   Backing up while filegroup or page data is offline is problematic for bulk-logged data, because the offline data is not recorded in the log. Any offline page can prevent backing up the log. In this cases, consider using DBCC REPAIR, because this might cause less data loss than restoring to the most recent backup.  
  
    -   If a log backup of a bulk-logged database encounters a bad page, it fails unless WITH CONTINUE_AFTER_ERROR is specified.  
  
    -   Page restore generally does not work with bulk-logged recovery.  
  
         A best practice for performing page restore is to set the database to the full recovery model, and try a log backup. If the log backup works, you can continue with the page restore. If the log backup fails, you either have to lose work since the previous log backup or you have to try running DBCC must be run with the REPAIR_ALLOW_DATA_LOSS option.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   Page restore scenarios:  
  
     Offline page restore  
     All editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support restoring pages when the database is offline. In an offline page restore, the database is offline while damaged pages are restored. At the end of the restore sequence, the database comes online.  
  
     Online page restore  
     [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise edition supports online page restores, though they use offline restore if the database is currently offline. In most cases, a damaged page can be restored while the database remains online, including the filegroup to which a page is being restored. When the primary filegroup is online, even if one or more of its secondary filegroups are offline, page restores are usually performed online. Occasionally, however, a damaged page can require an offline restore. For example, damage to certain critical pages might prevent the database from starting.  
  
    > [!WARNING]  
    >  If damaged pages are storing critical database metadata, required updates to metadata might fail during an online page restore attempt. In this case, you can perform an offline page restore, but first you must create a [tail log backup](../../relational-databases/backup-restore/tail-log-backups-sql-server.md) (by backing up the transaction log using RESTORE WITH NORECOVERY).  
  
-   Page restore takes advantage of the improved page-level error reporting (including page checksums) and tracking. Pages that are detected as corrupted by check-summing or a torn write, *damaged pages*, can be restored by a page restore operation. Only explicitly specified pages are restored. Each specified page is replaced by the copy of that page from the specified data backup.  
  
     When you restore the subsequent log backups, they are applied only to database files that contain at least one page that is being recovered. An unbroken chain of log backups must be applied to the last full or differential restore to bring the filegroup that contains the page forward to the current log file. As in a file restore, the roll forward set is advanced with a single log redo pass. For a page restore to succeed, the restored pages must be recovered to a state consistent with the database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database (for the FROM DATABASE_SNAPSHOT option, the database always exists).  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Starting in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] supports page restores.  
  
#### To restore pages  
  
1.  Connect to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**. Depending on the database, either select a user database or expand **System Databases**, and then select a system database.  
  
3.  Right-click the database, point to **Tasks**, point to **Restore**, and then click **Page**, which opens the **Restore Page** dialog box.  
  
     **Restore**  
     This section performs the same function as that of **Restore to** on the [Restore Database (General Page)](../../relational-databases/backup-restore/restore-database-general-page.md).  
  
     **Database**  
     Specifies the database to restore. You can enter a new database or select an existing database from the drop-down list. The list includes all databases on the server, except the system databases **master** and **tempdb**.  
  
    > [!WARNING]  
    >  To restore a password-protected backup, you must use the [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) statement.  
  
     **Tail-Log backup**  
     Enter or select a file name in **Backup device** where there tail-log backup will be stored for the database.  
  
     **Backup Sets**  
     This section displays the backup sets involved in the restoration.  
  
    |Header|Values|  
    |------------|------------|  
    |**Name**|The name of the backup set.|  
    |**Component**|The backed-up component: **Database**, **File**, or **\<blank>** (for transaction logs).|  
    |**Type**|The type of backup performed: **Full**, **Differential**, or **Transaction Log**.|  
    |**Server**|The name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that performed the backup operation.|  
    |**Database**|The name of the database involved in the backup operation.|  
    |**Position**|The position of the backup set in the volume.|  
    |**First LSN**|The log sequence number (LSN) of the first transaction in the backup set. Blank for file backups.|  
    |**Last LSN**|The log sequence number (LSN) of the last transaction in the backup set. Blank for file backups.|  
    |**Checkpoint LSN**|The log sequence number (LSN) of the most recent checkpoint at the time the backup was created.|  
    |**Full LSN**|The log sequence number (LSN) of the most recent full database backup.|  
    |**Start Date**|The date and time when the backup operation began, presented in the regional setting of the client.|  
    |**Finish Date**|The date and time when the backup operation finished, presented in the regional setting of the client.|  
    |**Size**|The size of the backup set in bytes.|  
    |**User Name**|The name of the user who performed the backup operation.|  
    |**Expiration**|The date and time the backup set expires.|  
  
     Click **Verify** to check the integrity of the backup files needed to perform the page restore operation.  
  
4.  To identify corrupted pages, with the correct database selected in the **Database** box, click **Check Database Pages**. This is a long running operation.  
  
    > [!WARNING]  
    >  To restore specific pages that are not corrupted, click **Add** and enter the **File ID** and **Page ID** of the pages to be restored.  
  
5.  The pages grid is used to identify the pages to be restored. Initially, this grid is populated from the [suspect_pages](../../relational-databases/system-tables/suspect-pages-transact-sql.md) system table. To add or remove pages from the grid, click **Add** or **Remove**. For more information, see [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md).  
  
6.  The **Backup sets** grid lists the backup sets in the default restore plan. Optionally, click **Verify** to verify that the backups are readable and that the backup sets are complete, without restoring them. For more information, see [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
     **Pages**  
  
7.  To restore the pages listed in the pages grid, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 To specify a page in a RESTORE DATABASE statement, you need the file ID of the file containing the page and the page ID of the page. The required syntax is as follows:  
  
 `RESTORE DATABASE <database_name>`  
  
 `PAGE = '<file: page> [ ,... n ] ' [ ,... n ]`  
  
 `FROM <backup_device> [ ,... n ]`  
  
 `WITH NORECOVERY`  
  
 For more information about the parameters of the PAGE option, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md). For more information about the RESTORE DATABASE syntax, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
#### To restore pages  
  
1.  Obtain the page IDs of the damaged pages to be restored. A checksum or torn write error returns page ID, providing the information required for specifying the pages. To look up page ID of a damaged page, use any of the following sources.  
  
    |Source of page ID|Topic|  
    |-----------------------|-----------|  
    |**msdb..suspect_pages**|[Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)|  
    |Error log|[View the SQL Server Error Log &#40;SQL Server Management Studio&#41;](../../relational-databases/performance/view-the-sql-server-error-log-sql-server-management-studio.md)|  
    |Event traces|[Monitor and Respond to Events](../../ssms/agent/monitor-and-respond-to-events.md)|  
    |DBCC|[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)|  
    |WMI provider|[WMI Provider for Server Events Concepts](../../relational-databases/wmi-provider-server-events/wmi-provider-for-server-events-concepts.md)|  
  
2.  Start a page restore with a full database, file, or filegroup backup that contains the page. In the RESTORE DATABASE statement, use the PAGE clause to list the page IDs of all of the pages to be restored.  
  
3.  Apply the most recent differentials .  
  
4.  Apply the subsequent log backups.  
  
5.  Create a new log backup of the database that includes the final LSN of the restored pages, that is, the point at which the last restored page is taken offline. The final LSN, which is set as part of the first restore in the sequence, is the redo target LSN. Online roll forward of the file containing the page is able to stop at the redo target LSN. To learn the current redo target LSN of a file, see the **redo_target_lsn** column of **sys.master_files**. For more information, see [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md).  
  
6.  Restore the new log backup. After this new log backup is applied, the page restore is completed and the pages are now usable.  
  
    > [!NOTE]  
    >  This sequence is analogous to a file restore sequence. In fact, page restore and file restores can both be performed as part of the same sequence.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example restores four damaged pages of file `B` with `NORECOVERY`. Next, two log backups are applied with `NORECOVERY`, followed with the tail-log backup, which is restored with `RECOVERY`. This example performs an online restore. In the example, the file ID of file `B` is `1`, and the page IDs of the damaged pages are `57`, `202`, `916`, and `1016`.  
  
```sql  
RESTORE DATABASE <database> PAGE='1:57, 1:202, 1:916, 1:1016'  
   FROM <file_backup_of_file_B>   
   WITH NORECOVERY;  
RESTORE LOG <database> FROM <log_backup>   
   WITH NORECOVERY;  
RESTORE LOG <database> FROM <log_backup>   
   WITH NORECOVERY;   
BACKUP LOG <database> TO <new_log_backup>;   
RESTORE LOG <database> FROM <new_log_backup> WITH RECOVERY;  
GO  
```  
  
## See Also  
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)  
  
  
