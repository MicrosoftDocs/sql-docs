---
title: "Restore a Database to a New Location (SQL Server)"
description: This article shows you how to restore a SQL Server database to a new location and rename the database by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/17/2022
ms.service: sql
ms.subservice: backup-restore
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
---
# Restore a Database to a New Location (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This article describes how to restore a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to a new location, and optionally rename the database, in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio(SSMS) or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You can move a database to a new directory path or create a copy of a database on either the same server instance or a different server instance.  
    
##  <a name="BeforeYouBegin"></a> Before you begin!  
  
###  <a name="Restrictions"></a> Limitations and restrictions  
  
-   The system administrator restoring a full database backup must be the only person currently using the database to be restored.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Under the full or bulk-logged recovery model, before you can restore a database, you must back up the active transaction log. For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md).  

-   To restore an encrypted database, **you must have access to the certificate or asymmetric key used to encrypt the database!** Without that certificate or asymmetric key, you can't restore the database. You must retain that certificate used to encrypt the database encryption key for as long as you need the backup! For more information, see [SQL Server Certificates and Asymmetric Keys](../../relational-databases/security/sql-server-certificates-and-asymmetric-keys.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   For additional considerations for moving a database, see [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md).  
  
-   If you restore a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or higher  database to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], the database is automatically upgraded. Typically, the database becomes available immediately. However, if a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the  **upgrade_option** server property. If the upgrade option is set to import (**upgrade_option** = 2) or rebuild (**upgrade_option** = 0), the full-text indexes will be unavailable during the upgrade. Depending on the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to import, the associated full-text indexes are rebuilt if a full-text catalog isn't available. To change the setting of the **upgrade_option** server property, use [sp_fulltext_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md).  
  
###  <a name="Security"></a> Security  
 For security purposes, we recommend that you don't attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database.  
  
####  <a name="Permissions"></a> Permissions  
 If the database being restored doesn't exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database.  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which isn't always the case when RESTORE is executed, members of the **db_owner** fixed database role don't have RESTORE permissions.  
  
  
## Restore a database to a new location; optionally rename the database using SSMS 

  
1.  Connect to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then in Object Explorer, select the server name to expand the server tree.  
  
2.  Right-click **Databases**, and then select **Restore Database**. The **Restore Database** dialog box opens.  
  
3.  On the **General** page, use the **Source** section to specify the source and location of the backup sets to restore. Select one of the following options:  
  
    -   **Database**  
  
         Select the database to restore from the drop-down list. The list contains only databases that have been backed up according to the **msdb** backup history.  
  
    > [!NOTE]  
    > If the backup is taken from a different server, the destination server will not have the backup history information for the specified database. In this case, select **Device** to manually specify the file or device to restore.  
  
    1.  **Device**  
  
         Select the browse (**...**) button to open the **Select backup devices** dialog box. In the **Backup media type** box, select one of the listed device types. To select one or more devices for the **Backup media** box, select **Add**.  
  
         After you add the devices you want to the **Backup media** list box, select **OK** to return to the **General** page.  
  
         In the **Source: Device: Database** list box, select the name of the database that should be restored.  
  
         **Note** This list is only available when **Device** is selected. Only databases that have backups on the selected device will be available.  
  
4.  In the **Destination** section, the **Database** box is automatically populated with the name of the database to be restored. To change the name of the database, enter the new name in the **Database** box.  
  
5.  In the **Restore to** box, leave the default as **To the last backup taken** or select **Timeline** to access the **Backup Timeline** dialog box to manually select a point in time to stop the recovery action. See [Backup Timeline](../../relational-databases/backup-restore/backup-timeline.md) for more information on designating a specific point in time.  
  
6.  In the **Backup sets to restore** grid, select the backups to restore. This grid displays the backups available for the specified location. By default, a recovery plan is suggested. To override the suggested recovery plan, you can change the selections in the grid. Backups that depend on the restoration of an earlier backup are automatically deselected when the earlier backup is deselected.  
  
     For information about the columns in the **Backup sets to restore** grid, see [Restore Database &#40;General Page&#41;](../../relational-databases/backup-restore/restore-database-general-page.md).  
  
7.  To specify the new location of the database files, select the **Files** page, and then select **Relocate all files to folder**. Provide a new location for the **Data file folder** and **Log file folder**. For more information about this grid, see [Restore Database &#40;Files Page&#41;](../../relational-databases/backup-restore/restore-database-files-page.md).  
  
8.  On the **Options** page, adjust the options if you want. For more information about these options, see [Restore Database &#40;Options Page&#41;](../../relational-databases/backup-restore/restore-database-options-page.md).  

 ## Restore database to a new location; optionally rename the database using T-SQL
 
 
1.  Optionally, determine the logical and physical names of the files in the backup set that contains the full database backup that you want to restore. This statement returns a list of the database and log files contained in the backup set. The basic syntax is as follows:  
  
     RESTORE FILELISTONLY FROM *<backup_device>* WITH FILE = *backup_set_file_number* 
  
     Here, *backup_set_file_number* indicates the position of the backup in the media set. You can obtain the position of a backup set by using the [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md) statement. For more information, see "Specifying a Backup Set" in [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
     This statement also supports several WITH options. For more information, see [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).  
  
2.  Use the [RESTORE DATABASE](../../t-sql/statements/restore-statements-transact-sql.md) statement to restore the full database backup. By default, data and log files are restored to their original locations. To relocate a database, use the MOVE option to relocate each of the database files, and to avoid collisions with existing files.  

  The basic [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax for restoring the database to a new location and a new name is:  
  ```syntaxsql
  RESTORE DATABASE *new_database_name*  
  
  FROM *backup_device* [ ,...*n* ]  
  
  [ WITH  
 
   {  
  
      [ **RECOVERY** | NORECOVERY ]  
  
      [ , ] [ FILE ={ *backup_set_file_number* | @*backup_set_file_number* } ]  
  
      [ , ] MOVE '*logical_file_name_in_backup*' TO '*operating_system_file_name*' [ ,...*n* ]  
  
  }  
  
  ;  
  ```
  > [!NOTE] 
  > When preparing to relocate a database on a different disk, you should verify that sufficient space is available and identify any potential collisions with existing files. This involves using a [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md) statement that specifies the same MOVE parameters that you plan to use in your RESTORE DATABASE statement.  
  
  The following table describes arguments of this RESTORE statement in terms of restoring a database to a new location. For more information about these arguments, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
  *new_database_name*  
  The new name for the database.  
  
  > [!NOTE]
  > If you are restoring the database to a different server instance, you can use the original database name instead of a new name.  
  
  *backup_device* [ **,**...*n* ]  
  Specifies a comma-separated list of from 1 to 64 backup devices from which the database backup is to be restored. You can specify a physical backup device, or you can specify a corresponding logical backup device, if defined. To specify a physical backup device, use the DISK or TAPE option:  
  
  { DISK | TAPE } **=**_physical_backup_device_name_  
  
  For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
  { **RECOVERY** | NORECOVERY }  
  If the database uses the full recovery model, you might need to apply transaction log backups after you restore the database. In this case, specify the NORECOVERY option.  
  
  Otherwise, use the RECOVERY option, which is the default.  
  
  FILE = { *backup_set_file_number* | @*backup_set_file_number* }  
  Identifies the backup set to be restored. For example, a *backup_set_file_number* of **1** indicates the first backup set on the backup medium and a *backup_set_file_number* of **2** indicates the second backup set. You can obtain the *backup_set_file_number* of a backup set by using the [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md) statement.  
  
  When this option isn't specified, the default is to use the first backup set on the backup device.  
  
  For more information, see "Specifying a Backup Set," in [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
  MOVE **'**_logical_file_name_in_backup_**'** TO **'**_operating_system_file_name_**'** [ **,**...*n* ]  
  Specifies that the data or log file specified by *logical_file_name_in_backup* is to be restored to the location specified by *operating_system_file_name*. Specify a MOVE statement for every logical file you want to restore from the backup set to a new location.  
  
  |Option|Description|  
  |------------|-----------------|  
  |*logical_file_name_in_backup*|Specifies the logical name of a data or log file in the backup set. The logical file name of a data or log file in a backup set matches its logical name in the database when the backup set was created.<br /><br /> <br /><br /> Note: To obtain a list of the logical files from the backup set, use [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).|  
  |*operating_system_file_name*|Specifies a new location for the file specified by *logical_file_name_in_backup*. The file will be restored to this location.<br /><br /> Optionally, *operating_system_file_name* specifies a new file name for the restored file. This is necessary if you are creating a copy of an existing database on the same server instance.|  
  |*n*|Is a placeholder indicating that you can specify additional MOVE statements.|  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example creates a new database named `MyAdvWorks` by restoring a backup of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database, which includes two files: [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]_Data and [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]_Log. This database uses the simple recovery model. The [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database already exists on the server instance, so the files in the backup must be restored to a new location. The RESTORE FILELISTONLY statement is used to determine the number and names of the files in the database being restored. The database backup is the first backup set on the backup device.  
  
> [!NOTE]  
> The examples of backing up and restoring the transaction log, including point-in-time restores, use the `MyAdvWorks_FullRM` database that is created from [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] just like the following `MyAdvWorks` example. However, the resulting `MyAdvWorks_FullRM` database must be changed to use the full recovery model by using the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement: ALTER DATABASE <database_name> SET RECOVERY FULL.  
  
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
  
 For an example of how to create a full database backup of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, see [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related tasks  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)  
  
## See also  
 [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md)  
  
  
