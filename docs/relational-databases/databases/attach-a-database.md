---
title: "Attach a Database | Microsoft Docs"
ms.custom: ""
ms.date: "10/24/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.attachdatabase.f1"
helpviewer_keywords: 
  - "database attaching [SQL Server]"
  - "attaching databases [SQL Server]"
ms.assetid: b4efb0ae-cfe6-4d81-a4b4-6e4916885caa
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Attach a Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This topic describes how to attach a database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You can use this feature to copy, move, or upgrade a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   The database must first be detached. Attempting to attach a database that has not been detached will return an error. For more information, see [Detach a Database](../../relational-databases/databases/detach-a-database.md).  
  
-   When you attach a database, all data files (MDF and LDF files) must be available. If any data file has a different path from when the database was first created or last attached, you must specify the current path of the file.  
  
-   When you attach a database, if MDF and LDF files are located in different directories and one of the paths includes \\\\?\GlobalRoot, the operation will fail.  
  
###  <a name="Recommendations"></a> Is Attach the best choice?  
We recommend that you move databases by using the `ALTER DATABASE` planned relocation procedure instead of using detach and attach, when moving database files within the same instance. For more information, see [Move User Databases](../../relational-databases/databases/move-user-databases.md). 
 
We don't recommend using detach and attach for Backup and Recovery. There are no transaction log backups, and it's possible to accidently delete files.
  
###  <a name="Security"></a> Security  
File access permissions are set during a number of database operations, including detaching or attaching a database. For information about file permissions that are set whenever a database is detached and attached, see [Securing Data and Log Files](https://technet.microsoft.com/library/ms189128.aspx) from [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Books Online (Still a valid read!) 
  
We recommend that you do not attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database. For more information about attaching databases and information about changes that are made to metadata when you attach a database, see [Database Detach and Attach (SQL Server)](../../relational-databases/databases/database-detach-and-attach-sql-server.md).  
  
####  <a name="Permissions"></a> Permissions  
Requires `CREATE DATABASE`, `CREATE ANY DATABASE`, or `ALTER ANY DATABASE` permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  

### To Attach a Database  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then click to expand that instance view in SSMS.  
  
2.  Right-click **Databases** and click **Attach**.  
  
3.  In the **Attach Databases** dialog box, to specify the database to be attached, click **Add**; and in the **Locate Database Files** dialog box, select the disk drive where the database resides and expand the directory tree to find and select the .mdf file of the database; for example:  
  
     `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\AdventureWorks2012_Data.mdf`  
  
    > [!IMPORTANT]  
    > Trying to select a database that is already attached generates an error.  
  
     **Databases to attach**  
     Displays information about the selected databases.  
  
     \<no column header>  
     Displays an icon indicating the status of the attach operation. The possible icons are described in the **Status** description, below).  
  
     **MDF File Location**  
     Displays the path and file name of the selected MDF file.  
  
     **Database Name**  
     Displays the name of the database.  
  
     **Attach As**  
     Optionally, specifies a different name for the database to attach as.  
  
     **Owner**  
     Provides a drop-down list of possible database owners from which you can optionally select a different owner.  
  
     **Status**  
     Displays the status of the database according to the following table.  
  
    |Icon|Status text|Description|  
    |----------|-----------------|-----------------|  
    |(No icon)|(No text)|Attach operation has not been started or may be pending for this object. This is the default when the dialog is opened.|  
    |Green, right-pointing triangle|In progress|Attach operation has been started but it is not complete.|  
    |Green check mark|Success|The object has been attached successfully.|  
    |Red circle containing a white cross|Error|Attach operation encountered an error and did not complete successfully.|  
    |Circle containing two black quadrants (on left and right) and two white quadrants (on top and bottom)|Stopped|Attach operation was not completed successfully because the user stopped the operation.|  
    |Circle containing a curved arrow pointing counter-clockwise|Rolled Back|Attach operation was successful but it has been rolled back due to an error during attachment of another object.|  
  
     **Message**  
     Displays either a blank message or a "File not found" hyperlink.  
  
     **Add**  
     Find the necessary main database files. When the user selects an .mdf file, applicable information is automatically filled in the respective fields of the **Databases to attach** grid.  
  
     **Remove**  
     Removes the selected file from the **Databases to attach** grid.  
  
     **"** *<database_name>* **" database details**  
     Displays the names of the files to be attached. To verify or change the pathname of a file, click the **Browse** button (**...**).  
  
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
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
### To attach a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Use the [CREATE DATABASE](../../t-sql/statements/create-database-sql-server-transact-sql.md) statement with the `FOR ATTACH` clause.  
  
     Copy and paste the following example into the query window and click **Execute**. This example attaches the files of the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database and renames the database to `MyAdventureWorks`.  
  
    ```sql  
    CREATE DATABASE MyAdventureWorks   
        ON (FILENAME = 'C:\MySQLServer\AdventureWorks_Data.mdf'),   
        (FILENAME = 'C:\MySQLServer\AdventureWorks_Log.ldf')   
        FOR ATTACH;  
    ```  
  
    > [!NOTE]  
    > Alternatively, you can use the [sp_attach_db](../../relational-databases/system-stored-procedures/sp-attach-db-transact-sql.md) or [sp_attach_single_file_db](../../relational-databases/system-stored-procedures/sp-attach-single-file-db-transact-sql.md) stored procedure. However, these procedures will be removed in a future version of Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you use `CREATE DATABASE ... FOR ATTACH` instead.  
  
##  <a name="FollowUp"></a> Follow Up: After Upgrading a SQL Server Database  
After you upgrade a database by using the attach method, the database becomes available immediately and is automatically upgraded. If the database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt.  
  
If the compatibility level of a user database is 100 or higher before upgrade, it remains the same after upgrade. If the compatibility level is 90 before upgrade, in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
> [!NOTE]
> If you are attaching a database from an instance running [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] or below which had Change Data Capture (CDC) enabled, you will also need to execute the command below to upgrade the Change Data Capture (CDC) metadata.
  
```sql
USE <database name>
EXEC sys.sp_cdc_vupgrade  
``` 
 
## See Also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md) 
 <br>[Manage metadata when making a database available on another server](manage-metadata-when-making-a-database-available-on-another-server.md)  
 [Detach a Database](../../relational-databases/databases/detach-a-database.md)  
  
  
