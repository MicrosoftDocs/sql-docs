---
title: "Restore a Database to a New Location (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "restoring databases [SQL Server], moving"
  - "database restores [SQL Server], creating new databases"
  - "restoring [SQL Server], with move"
  - "restoring databases [SQL Server], creating new databases"
  - "database backups [SQL Server], creating new database from"
  - "backing up databases [SQL Server], creating new database from"
  - "restoring databases [SQL Server], renaming"
  - "database creation [SQL Server], restoring with move"
ms.assetid: 4da76d61-5e11-4bee-84f5-b305240d9f42
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore a Database to a New Location (SQL Server)
  This topic describes how to restore a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to a new location, and optionally rename the database, in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You can move a database to a new directory path or create a copy of a database on either the same server instance or a different server instance.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To restore a database to a new location, and optionally rename the database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The system administrator restoring a full database backup must be the only person currently using the database to be restored.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Under the full or bulk-logged recovery model, before you can restore a database, you must back up the active transaction log. For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   To restore a database that is encrypted, you must have access to the certificate or asymmetric key that was used to encrypt the database. Without the certificate or asymmetric key, the database cannot be restored. As a result, the certificate that is used to encrypt the database encryption key must be retained as long as the backup is needed. For more information, see [SQL Server Certificates and Asymmetric Keys](../security/sql-server-certificates-and-asymmetric-keys.md).  
  
-   For information about additional considerations for moving a database, see [Copy Databases with Backup and Restore](../databases/copy-databases-with-backup-and-restore.md).  
  
-   If you restore a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or higher  database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the database is automatically upgraded. Typically, the database becomes available immediately. However, if a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the  **upgrade_option** server property. If the upgrade option is set to import (**upgrade_option** = 2) or rebuild (**upgrade_option** = 0), the full-text indexes will be unavailable during the upgrade. Depending the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to import, the associated full-text indexes are rebuilt if a full-text catalog is not available. To change the setting of the **upgrade_option** server property, use [sp_fulltext_service](/sql/relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql).  
  
###  <a name="Security"></a> Security  
 For security purposes, we recommend that you do not attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database.  
  
####  <a name="Permissions"></a> Permissions  
 If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database.  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To restore a database to a new location, and optionally rename the database  
  
1.  Connect to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then in Object Explorer, click the server name to expand the server tree.  
  
2.  Right-click **Databases**, and then click **Restore Database**. The **Restore Database** dialog box opens.  
  
3.  On the **General** page, use the **Source** section to specify the source and location of the backup sets to restore. Select one of the following options:  
  
    -   **Database**  
  
         Select the database to restore from the drop-down list. The list contains only databases that have been backed up according to the **msdb** backup history.  
  
    > [!NOTE]  
    >  If the backup is taken from a different server, the destination server will not have the backup history information for the specified database. In this case, select **Device** to manually specify the file or device to restore.  
  
    1.  **Device**  
  
         Click the browse (**...**) button to open the **Select backup devices** dialog box. In the **Backup media type** box, select one of the listed device types. To select one or more devices for the **Backup media** box, click **Add**.  
  
         After you add the devices you want to the **Backup media** list box, click **OK** to return to the **General** page.  
  
         In the **Source: Device: Database** list box, select the name of the database which should be restored.  
  
         **Note** This list is only available when **Device** is selected. Only databases that have backups on the selected device will be available.  
  
4.  In the **Destination** section, the **Database** box is automatically populated with the name of the database to be restored. To change the name of the database, enter the new name in the **Database** box.  
  
5.  In the **Restore to** box, leave the default as **To the last backup taken** or click on **Timeline** to access the **Backup Timeline** dialog box to manually select a point in time to stop the recovery action. See [Backup Timeline](backup-timeline.md) for more information on designating a specific point in time.  
  
6.  In the **Backup sets to restore** grid, select the backups to restore. This grid displays the backups available for the specified location. By default, a recovery plan is suggested. To override the suggested recovery plan, you can change the selections in the grid. Backups that depend on the restoration of an earlier backup are automatically deselected when the earlier backup is deselected.  
  
     For information about the columns in the **Backup sets to restore** grid, see [Restore Database &#40;General Page&#41;](../../integration-services/general-page-of-integration-services-designers-options.md).  
  
7.  To specify the new location of the database files, select the **Files** page, and then click **Relocate all files to folder**. Provide a new location for the **Data file folder** and **Log file folder**. For more information about this grid, see [Restore Database &#40;Files Page&#41;](restore-database-files-page.md).  
  
8.  On the **Options** page, adjust the options if you want. For more information about these options, see [Restore Database &#40;Options Page&#41;](restore-database-options-page.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To restore a database to a new location, and optionally rename the database  
  
1.  Optionally, determine the logical and physical names of the files in the backup set that contains the full database backup that you want to restore. This statement returns a list of the database and log files contained in the backup set. The basic syntax is as follows:  
  
     RESTORE FILELISTONLY FROM *<backup_device>* WITH FILE = *backup_set_file_number*  
  
     Here, *backup_set_file_number* indicates the position of the backup in the media set. You can obtain the position of a backup set by using the [RESTORE HEADERONLY](/sql/t-sql/statements/restore-statements-headeronly-transact-sql) statement. For more information, see "Specifying a Backup Set" in [RESTORE Arguments &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-arguments-transact-sql).  
  
     This statement also supports a number of WITH options. For more information, see [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-filelistonly-transact-sql).  
  
2.  Use the [RESTORE DATABASE](/sql/t-sql/statements/restore-statements-transact-sql) statement to restore the full database backup. By default, data and log files are restored to their original locations. To relocate a database, use the MOVE option to relocate each of the database files and to avoid collisions with existing files.  
  
     The basic [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax for restoring the database to a new location and a new name is:  
  
     RESTORE DATABASE *new_database_name*  
  
     FROM *backup_device* [ ,...*n* ]  
  
     [ WITH  
  
     {  
  
     [ **RECOVERY** | NORECOVERY ]  
  
     [ , ] [ FILE ={ *backup_set_file_number* | @*backup_set_file_number* } ]  
  
     [ , ] MOVE '*logical_file_name_in_backup*' TO '*operating_system_file_name*' [ ,...*n* ]  
  
     }  
  
     ;  
  
    > [!NOTE]  
    >  When preparing to relocate a database on a different disk, you should verify that sufficient space is available and identify any potential collisions with existing files. This involves using a [RESTORE VERIFYONLY](/sql/t-sql/statements/restore-statements-verifyonly-transact-sql) statement that specifies the same MOVE parameters that you plan to use in your RESTORE DATABASE statement.  
  
     The following table describes arguments of this RESTORE statement in terms of restoring a database to a new location. For more information about these arguments, see [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql).  
  
     *new_database_name*  
     The new name for the database.  
  
    > [!NOTE]  
    >  If you are restoring the database to a different server instance, you can use the original database name instead of a new name.  
  
     *backup_device* [ `,`...*n* ]  
     Specifies a comma-separated list of from 1 to 64 backup devices from which the database backup is to be restored. You can specify a physical backup device, or you can specify a corresponding logical backup device, if defined. To specify a physical backup device, use the DISK or TAPE option:  
  
     { DISK | TAPE } `=`*physical_backup_device_name*  
  
     For more information, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md).  
  
     { **RECOVERY** | NORECOVERY }  
     If the database uses the full recovery model, you might need to apply transaction log backups after you restore the database. In this case, specify the NORECOVERY option.  
  
     Otherwise, use the RECOVERY option, which is the default.  
  
     FILE = { *backup_set_file_number* | @*backup_set_file_number* }  
     Identifies the backup set to be restored. For example, a *backup_set_file_number* of **1** indicates the first backup set on the backup medium and a *backup_set_file_number* of **2** indicates the second backup set. You can obtain the *backup_set_file_number* of a backup set by using the [RESTORE HEADERONLY](/sql/t-sql/statements/restore-statements-headeronly-transact-sql) statement.  
  
     When this option is not specified, the default is to use the first backup set on the backup device.  
  
     For more information, see "Specifying a Backup Set," in [RESTORE Arguments &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-arguments-transact-sql).  
  
     MOVE **'*`logical_file_name_in_backup`*'** TO **'*`operating_system_file_name`*'** [ `,`...*n* ]  
     Specifies that the data or log file specified by *logical_file_name_in_backup* is to be restored to the location specified by *operating_system_file_name*. Specify a MOVE statement for every logical file you want to restore from the backup set to a new location.  
  
    |Option|Description|  
    |------------|-----------------|  
    |*logical_file_name_in_backup*|Specifies the logical name of a data or log file in the backup set. The logical file name of a data or log file in a backup set matches its logical name in the database when the backup set was created.<br /><br /> Note: To obtain a list of the logical files from the backup set, use [RESTORE FILELISTONLY](/sql/t-sql/statements/restore-statements-filelistonly-transact-sql).|  
    |*operating_system_file_name*|Specifies a new location for the file specified by *logical_file_name_in_backup*. The file will be restored to this location.<br /><br /> Optionally, *operating_system_file_name* specifies a new file name for the restored file. This is necessary if you are creating a copy of an existing database on the same server instance.|  
    |*n*|Is a placeholder indicating that you can specify additional MOVE statements.|  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example creates a new database named `MyAdvWorks` by restoring a backup of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database, which includes two files: [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]_Data and [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]_Log. This database uses the simple recovery model. The [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database already exists on the server instance, so the files in the backup must be restored to a new location. The RESTORE FILELISTONLY statement is used to determine the number and names of the files in the database being restored. The database backup is the first backup set on the backup device.  
  
> [!NOTE]  
>  The examples of backing up and restoring the transaction log, including point-in-time restores, use the `MyAdvWorks_FullRM` database that is created from [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] just like the following `MyAdvWorks` example. However, the resulting `MyAdvWorks_FullRM` database must be changed to use the full recovery model by using the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement: ALTER DATABASE <database_name> SET RECOVERY FULL.  
  
```sql  
USE master;  
GO  
-- First determine the number and names of the files in the backup.  
-- AdventureWorks2012_Backup is the name of the backup device.  
RESTORE FILELISTONLY  
   FROM AdventureWorks2012_Backup;  
-- Restore the files for MyAdvWorks.  
RESTORE DATABASE MyAdvWorks  
   FROM AdventureWorks2012_Backup  
   WITH RECOVERY,  
   MOVE 'AdventureWorks2012_Data' TO 'D:\MyData\MyAdvWorks_Data.mdf',   
   MOVE 'AdventureWorks2012_Log' TO 'F:\MyLog\MyAdvWorks_Log.ldf';  
GO  
  
```  
  
 For an example of how to create a full database backup of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, see [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md)  
  
-   [Restore a Database Backup &#40;SQL Server Management Studio&#41;](restore-a-database-backup-using-ssms.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](restore-a-transaction-log-backup-sql-server.md)  
  
## See Also  
 [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../databases/manage-metadata-when-making-a-database-available-on-another-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [Copy Databases with Backup and Restore](../databases/copy-databases-with-backup-and-restore.md)  
  
  
