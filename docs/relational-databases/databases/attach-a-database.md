---
title: "Attach a Database"
description: Learn how to attach a database in SQL Server by using SQL Server Management Studio or Transact-SQL. Use this feature to copy, move, or upgrade a database.
ms.custom: ""
ms.date: "01/31/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.attachdatabase.f1"
helpviewer_keywords: 
  - "database attaching [SQL Server]"
  - "attaching databases [SQL Server]"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Attach a Database
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to attach a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] with [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You can use this feature to copy, move, or upgrade a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
###  <a name="Restrictions"></a> Limitations and restrictions  
 For a list of limitations and restrictions, see [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md).  

##  <a name="Prerequisites"></a> Prerequisites  
  
Review all of the following prerequisites before proceeding:

-   In the case where you are moving a database from one instance to another, the database must first be detached from any existing SQL instance. If you attempt to attach a database that has not been detached returns an error. For more information, see [Detach a Database](../../relational-databases/databases/detach-a-database.md).  
  
-   When you attach a database, all data files for the database must be available. Often, these files have extensions .mdf or .ndf (for data files) and .ldf (for transaction log files). Additionally, any filegroups for FILESTREAM data must be present and available. For more information to attach a FILESTREAM-enabled database, see [Move a FILESTREAM-Enabled Database](../blob/move-a-filestream-enabled-database.md).

- If any data file has a different path from when the database was first created or last attached, you must specify the current path of the file. 
 
- The Database Engine service account must have permissions to read the files in their new location.
  
- If MDF and LDF files are in different directories and one of the paths includes `\\?\GlobalRoot`, when you attach a database the operation will fail.  
  
###  <a name="Recommendations"></a> Is Attach the best choice?  
We recommend that you move databases within an instance with the `ALTER DATABASE` planned relocation procedure instead of detach and attach, when moving database files within the same instance. For more information, see [Move User Databases](../../relational-databases/databases/move-user-databases.md). 
 
It is not recommended to use detach and attach for Backup and Recovery. There are no transaction log backups or point-in-time recovery available when detaching files to be backed up externally from SQL Server.
  
###  <a name="Security"></a> Security  

File access permissions are set during many database operations, including when a database is detached and attached. When a database is detached or attached, the Database Engine tries to impersonate the Windows account of the connection performing the operation to guarantee that the account has permission to access the database and log files. For mixed security accounts that use SQL Server logins, the impersonation might fail.

The following table shows the permissions set on the database and log files after an attach or detach operation is completed, and whether the connecting account can be impersonated by the Database Engine.

|Operation|Connecting account can be impersonated|Files permissions are granted to|
|:--|:--|:--|
|Detach|Yes|Only the account performing the operation. Additional accounts can be added by an operating system administrator, if they are needed after the database is detached.|
|Detach|No|The SQL Server (MSSQLSERVER) service account and members of the local Windows Administrators group.|
|Attach|Yes|The SQL Server (MSSQLSERVER) service account and members of the local Windows Administrators group.|
|Attach|No|The SQL Server (MSSQLSERVER) service account.|

For more information on file system permissions granted to the per-service SIDs for the SQL Server service, see [Configure File System Permissions for Database Engine Access](../../database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access.md).

> [!CAUTION]  
> We recommend that you do not attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database. For more information about attaching databases and information about changes that are made to metadata when you attach a database, see [Database Detach and Attach (SQL Server)](../../relational-databases/databases/database-detach-and-attach-sql-server.md).  

####  <a name="Permissions"></a> Permissions  
Requires `CREATE DATABASE`, `CREATE ANY DATABASE`, or `ALTER ANY DATABASE` permission.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio (SSMS)

### Before moving a database

If you are moving a database, before you detach it from its existing SQL Server instance, use the **Database properties** page to review the files associated with the database and their current locations.

1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, connect to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand the instance.  
  
2.  Expand **Databases**, and select the name of the user database you want to detach.  
  
3.  Right-click the database name, select **Properties**. Select the **Files** page and review the entries in the **Database files:** table.

Be sure to account for all files associated with the database before you detach, move, and attach. Then, proceed with the detach, file copy, and attach database steps in the next section. For more information, see [Detach a Database](detach-a-database.md).

### Attach a database  

1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **Object Explorer**, connect to an instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then select to expand that instance view in SSMS.  
  
2.  Right-click **Databases** and select **Attach**.  
  
3.  In the **Attach Databases** dialog box, to specify the database to be attached, select **Add**. In the **Locate Database Files** dialog box, select the location where the database resides and expand the directory tree to find and select the .mdf file of the database; for example:

     `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\AdventureWorks2012_Data.mdf`  
  
    > [!IMPORTANT]  
    > Trying to select a database that is already attached generates an error.  
  
     **Databases to attach**  
     Displays information about the selected databases.  
  
     \<no column header>  
     Displays an icon indicating the status of the attach operation. The possible icons are described in the **Status** description.  
  
     **MDF File Location**  
     Displays the path and file name of the selected MDF file.  
  
     **Database Name**  
     Displays the name of the database.  
  
     **Attach As**  
     Optionally, specifies a different name for the database to attach as.  
  
     **Owner**  
     Provides a drop-down list of possible database owners from which you can optionally select a different owner.  
  
     **Status**  
     Displays the status of the database according to the following table:
  
    |Icon|Status text|Description|  
    |----------|-----------------|-----------------|  
    |(No icon)|(No text)|Attach operation has not been started or might be pending for this object. This is the default when the dialog is opened.|  
    |Green, right-pointing triangle|In progress|Attach operation has been started but it is not complete.|  
    |Green check mark|Success|The object has been attached successfully.|  
    |Red circle containing a white cross|Error|Attach operation encountered an error and did not complete successfully.|  
    |Circle containing two black quadrants (on left and right) and two white quadrants (on top and bottom)|Stopped|Attach operation was not completed successfully because you stopped the operation.|  
    |Circle containing a curved arrow pointing counter-clockwise|Rolled Back|Attach operation was successful but it has been rolled back due to an error during attachment of another object.|  
  
     **Message**  
     Displays either a blank message or a "File not found" hyperlink.  
  
     **Add**  
     Find the necessary main database files. When you select an .mdf file, applicable information is automatically filled in the respective fields of the **Databases to attach** grid.  
  
     **Remove**  
     Removes the selected file from the **Databases to attach** grid.  
  
     **"** *<database_name>* **" database details**  
     Displays the names of the files to be attached. To verify or change the pathname of a file, select the **Browse** button (**...**).  
  
    > [!NOTE]  
    > If a file does not exist, the **Message** column displays "Not found." If a log file is not found, it exists in another directory or has been deleted. You need to either update the file path in the **database details** grid to point to the correct location or remove the log file from the grid. If an .ndf data file is not found, you need to update its path in the grid to point to the correct location.  
  
     **Original File Name**  
     Displays the name of the attached file belonging to the database.  
  
     **File Type**  
     Indicates the type of file, **Data** or **Log**.  
  
     **Current File Path**  
     Displays the path to the selected database file. The path can be edited manually.  
  
     **Message**  
     Displays either a blank message or a "**File not found**" hyperlink.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  

### Before moving a database

If you are moving a database, before it is detached from its existing SQL Server instance, use the `sys.database_files` system catalog view to review the files associated with the database and their current locations. For more information, see [sys.database_files (Transact-SQL)](../system-catalog-views/sys-database-files-transact-sql.md).

1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select **New Query** to open the Query Editor.  
  
2.  Copy the following [!INCLUDE[tsql](../../includes/tsql-md.md)] script into the Query Editor, and then select **Execute**. This script displays the location of the physical database files. Be sure to account for all files when you move the database via detach/attach.  

    ```sql  
    USE [database_name] 
    GO  
    SELECT type_desc, name, physical_name from sys.database_files;
    ```  

Be sure to account for all files associated with the database before you detach, move, and attach. Then, proceed with the detach, file copy, and attach database steps in the next section. For more information, see [Detach a Database](detach-a-database.md).

### To attach a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Use the [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md) statement with the `FOR ATTACH` clause.  
  
     Copy and paste the following example into the query window and select **Execute**. This example attaches the all files of the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database and renames the database to `MyAdventureWorks`. 
  
    ```sql  
    CREATE DATABASE MyAdventureWorks   
        ON (FILENAME = 'C:\MySQLServer\AdventureWorks_Data.mdf'),   
        (FILENAME = 'C:\MySQLServer\AdventureWorks_Log.ldf')   
        FOR ATTACH;  
    ```  
  
    Your database might have additional data files (commonly, .mdf or .ndf), and require additional files to include in the `CREATE DATABASE ... FOR ATTACH` statement. Additionally, any filegroups for FILESTREAM data must also be included in the statement. For more information on attaching a FILESTREAM-enabled database, see [Move a FILESTREAM-Enabled Database](../blob/move-a-filestream-enabled-database.md).

    > [!NOTE]  
    > Alternatively, you can use the [sp_attach_db](../../relational-databases/system-stored-procedures/sp-attach-db-transact-sql.md) or [sp_attach_single_file_db](../../relational-databases/system-stored-procedures/sp-attach-single-file-db-transact-sql.md) stored procedure. However, these procedures will be removed in a future version of Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you use `CREATE DATABASE ... FOR ATTACH` instead.      
  
##  <a name="FollowUp"></a> After upgrading a SQL Server database  

### Database compatibility level

After you upgrade a database by using the attach method, the database becomes available. The database will be automatically upgraded to the internal version level of the new instance. If the database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes are unavailable during the upgrade. Depending on the amount of data being indexed, importing can take several hours, and rebuilding can take up to 10 times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt.  
  
After the upgrade, the database compatibility level remains at the compatibility level before the upgrade, unless the previous compatibility level is not supported on the new version. In this case, the upgraded database compatibility level is set to the lowest supported compatibility level. For example, if you attach a database that was compatibility level 90 before attaching it to an instance of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], after the upgrade the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  

### Change Data Capture (CDC)
  
If you are attaching a database from an instance of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] or earlier, which had Change Data Capture (CDC) enabled, you must execute the following command to upgrade the Change Data Capture (CDC) metadata:
  
```sql
USE <database name>
EXEC sys.sp_cdc_vupgrade  
``` 

For more information, see [Error when you attach a CDC-enabled database to an instance of SQL Server 2016 or SQL Server 2017 on Windows](/troubleshoot/sql/replication/attach-change-data-capture-database).

## See also  

- [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
- [Detach a Database](../../relational-databases/databases/detach-a-database.md)  
- [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md) 
- [Configure File System Permissions for Database Engine Access](../../database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access.md)  

## Next steps

- [Manage metadata when making a database available on another server](manage-metadata-when-making-a-database-available-on-another-server.md)  
- [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
