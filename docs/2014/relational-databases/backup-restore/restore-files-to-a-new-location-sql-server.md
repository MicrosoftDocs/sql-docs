---
title: "Restore Files to a New Location (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "restoring files [SQL Server], how-to topics"
  - "restoring databases [SQL Server], moving"
  - "restoring files [SQL Server], steps"
  - "file restores [SQL Server], how-to topics"
  - "filegroups [SQL Server], restoring"
  - "restoring filegroups [SQL Server]"
ms.assetid: b4f4791d-646e-4632-9980-baae9cb1aade
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore Files to a New Location (SQL Server)
  This topic describes how to restore files to a new location in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To restore files to a new location, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The system administrator restoring the files must be the only person currently using the database to be restored.  
  
-   RESTORE is not allowed in an explicit or implicit transaction.  
  
-   Under the full or bulk-logged recovery model, before you can restore files, you must back up the active transaction log (known as the tail of the log). For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md).  
  
-   To restore a database that is encrypted, you must have access to the certificate or asymmetric key that was used to encrypt the database. Without the certificate or asymmetric key, the database cannot be restored. As a result, the certificate that is used to encrypt the database encryption key must be retained as long as the backup is needed. For more information, see [SQL Server Certificates and Asymmetric Keys](../security/sql-server-certificates-and-asymmetric-keys.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database (for the FROM DATABASE_SNAPSHOT option, the database always exists).  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To restore files to a new location  
  
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
  
7.  In the **Restore database files as** grid, specify a new location for the file or files that you want to move.  
  
    |Column head|Values|  
    |-----------------|------------|  
    |**Original File Name**|The full path of a source backup file.|  
    |**File Type**|Specifies the type of data in the backup: **Data**, **Log**, or **Filestream Data**. Data that is contained in tables is in **Data** files. Transaction log data is in **Log** files. Binary large object (BLOB) data that is stored on the file system is in **Filestream Data** files.|  
    |**Restore As**|The full path of the database file to be restored. To specify a new restore file, click the text box and edit the suggested path and file name. Changing the path or file name in the **Restore As** column is equivalent to using the MOVE option in a [!INCLUDE[tsql](../../includes/tsql-md.md)] RESTORE statement.|  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To restore files to a new location  
  
1.  Optionally, execute the RESTORE FILELISTONLY statement to determine the number and names of the files in the full database backup.  
  
2.  Execute the RESTORE DATABASE statement to restore the full database backup, specifying:  
  
    -   The name of the database to restore.  
  
    -   The backup device from where the full database backup will be restored.  
  
    -   The MOVE clause for each file to restore to a new location.  
  
    -   The NORECOVERY clause.  
  
3.  If the files have been modified after the file backup was created, execute the RESTORE LOG statement to apply the transaction log backup, specifying:  
  
    -   The name of the database to which the transaction log will be applied.  
  
    -   The backup device from where the transaction log backup will be restored.  
  
    -   The NORECOVERY clause if you have another transaction log backup to apply after the current one; otherwise, specify the RECOVERY clause.  
  
         The transaction log backups, if applied, must cover the time when the files and filegroups were backed up.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example restores two of the files for the `MyNwind` database that were originally located on Drive C to new locations on Drive D. Two transaction logs will also be applied to restore the database to the current time. The `RESTORE FILELISTONLY` statement is used to determine the number and logical and physical names of the files in the database being restored.  
  
```tsql  
USE master;  
GO  
-- First determine the number and names of the files in the backup.  
RESTORE FILELISTONLY  
   FROM MyNwind_1;  
-- Restore the files for MyNwind.  
RESTORE DATABASE MyNwind  
   FROM MyNwind_1  
   WITH NORECOVERY,  
   MOVE 'MyNwind_data_1' TO 'D:\MyData\MyNwind_data_1.mdf',   
   MOVE 'MyNwind_data_2' TO 'D:\MyData\MyNwind_data_2.ndf';  
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
 [Copy Databases with Backup and Restore](../databases/copy-databases-with-backup-and-restore.md)   
 [Restore Files and Filegroups &#40;SQL Server&#41;](restore-files-and-filegroups-sql-server.md)  
  
  
