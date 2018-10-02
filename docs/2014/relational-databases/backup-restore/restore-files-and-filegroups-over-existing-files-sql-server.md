---
title: "Restore Files and Filegroups over Existing Files (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "restoring files [SQL Server], how-to topics"
  - "restoring files [SQL Server], steps"
  - "file restores [SQL Server], how-to topics"
  - "filegroups [SQL Server], restoring"
  - "restoring filegroups [SQL Server]"
  - "overwriting filegroups"
  - "overwriting files"
ms.assetid: 517e07eb-9685-4b06-90af-b1cc496700b7
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore Files and Filegroups over Existing Files (SQL Server)
  This topic describes how to restore files and filegroups over existing files in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To restore files and filegroups over existing files, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The system administrator who is restoring the files and filegroups must be the only person currently using the database to be restored.  
  
-   RESTORE is not allowed in an explicit or implicit transaction.  
  
-   Under the full or bulk-logged recovery model, before you can restore files, you must back up the active transaction log (known as the tail of the log). For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md).  
  
-   To restore a database that is encrypted, you must have access to the certificate or asymmetric key that was used to encrypt the database. Without the certificate or asymmetric key, the database cannot be restored. As a result, the certificate that is used to encrypt the database encryption key must be retained as long as the backup is needed. For more information, see [SQL Server Certificates and Asymmetric Keys](../security/sql-server-certificates-and-asymmetric-keys.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database (for the FROM DATABASE_SNAPSHOT option, the database always exists).  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To restore files and filegroups over existing files  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], expand that instance, and then expand **Databases**.  
  
2.  Right-click the database that you want, point to **Tasks**, point to **Restore**, and then click **Files and Filegroups**.  
  
3.  On the **General** page, in the **To database** list box, enter the database to restore. You can enter a new database or choose an existing database from the drop-down list. The list includes all databases on the server, excluding the system databases **master** and **tempdb**.  
  
4.  To specify the source and location of the backup sets to restore, click one of the following options:  
  
    -   **From database**  
  
         Enter a database name in the list box. This list contains only databases that have been backed up according to the **msdb** backup history.  
  
    -   **From device**  
  
         Click the browse button. In the **Specify backup devices** dialog box, select one of the listed device types in the **Backup media type** list box. To select one or more devices for the **Backup media** list box, click **Add**.  
  
         After you add the devices you want to the **Backup media** list box, click **OK** to return to the **General** page.  
  
5.  In the **Select the backup sets to restore** grid, select the backups to restore. This grid displays the backups available for the specified location. By default, a recovery plan is suggested. To override the suggested recovery plan, you can change the selections in the grid. Any backups that depend on a deselected backup are deselected automatically.  
  
    |Column head|Values|  
    |-----------------|------------|  
    |**Restore**|The selected check boxes indicate the backup sets to be restored.|  
    |**Name**|The name of the backup set.|  
    |**File Type**|Specifies the type of data in the backup: **Data**, **Log**, or **Filestream Data**. Data that is contained in tables is in **Data** files. Transaction log data is in **Log** files. Binary large object (BLOB) data that is stored on the file system is in **Filestream Data** files.|  
    |**Type**|The type of backup performed: **Full**, **Differential**, or **Transaction Log**.|  
    |**Server**|The name of the Database-Engine instance that performed the backup operation.|  
    |**File Logical Name**|The logical name of the file.|  
    |**Database**|The name of the database involved in the backup operation.|  
    |**Start Date**|The date and time when the backup operation began, presented in the regional setting of the client.|  
    |**Finish Date**|The date and time when the backup operation finished, presented in the regional setting of the client.|  
    |**Size**|The size of the backup set in bytes.|  
    |**User Name**|The name of the user who performed the backup operation.|  
  
6.  In the **Select a page** pane, click the **Options** page.  
  
7.  In the **Restore options** panel, select **Overwrite the existing database (WITH REPLACE)**. The restore operation overwrites any existing databases and their related files, even if another database or file already exists with the same name.  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To restore files and filegroups over existing files  
  
1.  Execute the RESTORE DATABASE statement to restore the file and filegroup backup, specifying:  
  
    -   The name of the database to restore.  
  
    -   The backup device from where the full database backup will be restored.  
  
    -   The FILE clause for each file to restore.  
  
    -   The FILEGROUP clause for each filegroup to restore.  
  
    -   The REPLACE option to specify that each file can be restored over existing files of the same name and location.  
  
        > [!CAUTION]  
        >  Use the REPLACE option cautiously. For more information, see .  
  
    -   The NORECOVERY option. If the files have not been modified after the backup was created, specify the RECOVERY clause.  
  
2.  If the files have been modified after the file backup was created, execute the RESTORE LOG statement to apply the transaction log backup, specifying:  
  
    -   The name of the database to which the transaction log will be applied.  
  
    -   The backup device from where the transaction log backup will be restored.  
  
    -   The NORECOVERY clause if you have another transaction log backup to apply after the current one; otherwise, specify the RECOVERY clause.  
  
         The transaction log backups, if applied, must cover the time when the files and filegroups were backed up.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example restores the files and filegroups for the `MyNwind` database, and replaces any existing files of the same name. Two transaction logs will also be applied to restore the database to the current time.  
  
```tsql  
USE master;  
GO  
-- Restore the files and filesgroups for MyNwind.  
RESTORE DATABASE MyNwind  
   FILE = 'MyNwind_data_1',  
   FILEGROUP = 'new_customers',  
   FILE = 'MyNwind_data_2',  
   FILEGROUP = 'first_qtr_sales'  
   FROM MyNwind_1  
   WITH NORECOVERY,  
   REPLACE;  
GO  
-- Apply the first transaction log backup.  
RESTORE LOG MyNwind  
   FROM MyNwind_log1  
   WITH NORECOVERY;  
GO  
-- Apply the last transaction log backup.  
RESTORE LOG MyNwind  
   FROM MyNwind_log2  
   WITH RECOVERY;  
GO  
```  
  
## See Also  
 [Restore a Database Backup &#40;SQL Server Management Studio&#41;](restore-a-database-backup-using-ssms.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [Restore Files and Filegroups &#40;SQL Server&#41;](restore-files-and-filegroups-sql-server.md)   
 [Copy Databases with Backup and Restore](../databases/copy-databases-with-backup-and-restore.md)  
  
  
