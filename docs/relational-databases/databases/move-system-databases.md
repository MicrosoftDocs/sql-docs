---
description: "Move System Databases"
title: "Move System Databases"
ms.custom: ""
ms.date: "02/24/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "moving system databases"
  - "disaster recovery [SQL Server], moving database files"
  - "database files [SQL Server], moving"
  - "data files [SQL Server], moving"
  - "tempdb database [SQL Server], moving"
  - "system databases [SQL Server], moving"
  - "scheduled disk maintenace [SQL Server]"
  - "moving databases"
  - "msdb database [SQL Server], moving"
  - "moving database files"
  - "relocating database files"
  - "planned database relocations [SQL Server]"
  - "master database [SQL Server], moving"
  - "model database [SQL Server], moving"
  - "Resource database [SQL Server]"
  - "databases [SQL Server], moving"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Move system databases
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This article describes how to move system databases in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Moving system databases may be useful in the following situations:  

  
-   Failure recovery. For example, the database is in suspect mode or has shut down because of a hardware failure.  
  
-   Planned relocation.  
  
-   Relocation for scheduled disk maintenance.  
  
 The following procedures apply to moving database files within the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To move a database to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or to another server, use the [backup and restore](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md) operation.  

 The procedures in this article require the logical name of the database files. To obtain the name, query the name column in the [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md) catalog view.  

  
> [!IMPORTANT]  
>  If you move a system database and later rebuild the `master` database, you must move the system database again because the rebuild operation installs all system databases to their default location.     
  
## <a name="Planned"></a> Move system databases

 To move a system database data or log file as part of a planned relocation or scheduled maintenance operation, follow these steps. This includes the `model`, `msdb`, and `tempdb` system databases.
 
 > [!IMPORTANT]
 > This procedure applies to all system databases except the `master` and `Resource` databases. See later in this article for steps to move the `master` database. The `Resource` database cannot be moved. 
  
1.  Record the existing location of the database files you intend to move, by reviewing the [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md) catalog view. 

2.  Verify that the service account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service account cannot control the files in their new location, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance will not start.

4.  For each database file to be moved, run the following statement.  
  
    ```sql  
    ALTER DATABASE database_name MODIFY FILE ( NAME = logical_name , FILENAME = 'new_path\os_file_name' );
    ```  
    
    Until the service is restarted, the database continues to use the data and log files in the existing location.  

    
4.  Stop the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to perform maintenance. For more information, see [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  

5.  Copy the database file or files to the new location. Note that this is not a necessary step for the `tempdb` system database, those files will be created in the new location automatically.

6.  Restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the server. For more information, see [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  
  
7.  Verify the file change by running the following query. The system databases should report the new physical file locations.
  
    ```sql  
    SELECT name, physical_name AS CurrentLocation, state_desc  
    FROM sys.master_files  
    WHERE database_id = DB_ID(N'<database_name>');  
    ```  
    
8. Since in Step 5 you copied the database files instead of moving them, now you can safely delete the unused database files from their previous location. 

### Follow-up: After moving the `msdb` system database

If the `msdb` database is moved and [Database Mail](../../relational-databases/database-mail/database-mail.md) is configured, complete the following additional steps.
  
1.  Verify that [!INCLUDE[ssSB](../../includes/sssb-md.md)] is enabled for the `msdb` database by running the following query.  
  
    ```sql 
    SELECT is_broker_enabled   
    FROM sys.databases  
    WHERE name = N'msdb';  
    ```  
    
    If the [!INCLUDE[ssSB](../../includes/sssb-md.md)] is not enabled for `msdb`, it must be re-enabled for Database Mail to function. For more information, see [ALTER DATABASE ... SET ENABLE_BROKER](../../t-sql/statements/alter-database-transact-sql-set-options.md#service_broker_option-).
    
    ```sql
    ALTER DATABASE msdb 
    SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;
    ```
    
    Confirm that the value of `is_broker_enabled` is now 1.
  
2.  Verify that Database Mail is working by sending a test mail.  
  
##  <a name="Failure"></a> Failure Recovery Procedure  
 If a file must be moved because of a hardware failure, follow these steps to relocate the file to a new location. This procedure applies to all system databases except the `master` and `Resource` databases. The following examples use the Windows command-line prompt and [sqlcmd Utility](../../ssms/scripting/sqlcmd-use-the-utility.md).  
  
> [!IMPORTANT]  
>  If the database cannot be started, if it is in suspect mode or in an unrecovered state, only members of the sysadmin fixed role can move the file.  

1.  Verify that the service account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service account cannot control the files in their new location, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance will not start.

2.  Stop the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if it is started.  
  
3.  Start the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in master-only recovery mode by entering one of the following commands at the command prompt. Using startup parameter 3608 prevents SQL Server from automatically starting and recovering any database except the `master` database. For more information, see [Startup Parameters](../../tools/configuration-manager/sql-server-properties-startup-parameters-tab.md#optional-parameters) and [TF3608](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf3608).

    The parameters specified in these commands are case sensitive. The commands fail when the parameters are not specified as shown.  

    For the default (MSSQLSERVER) instance, run the following command:  
  
    ```cmd  
    NET START MSSQLSERVER /f /T3608
    ```  
  
    For a named instance, run the following command:  
  
    ```cmd  
    NET START MSSQL$instancename /f /T3608
    ```  
  
     For more information, see [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  
  
4.  Promptly after service startup with trace flag 3608 and `/f`, start a **sqlcmd** connection to the server, to claim the single connection that is available. For example, when executing **sqlcmd** locally on the same server as the default (MSSQLSERVER) instance, and to connect with Active Directory integration authentication, run the following command: 


    ```cmd
    sqlcmd
    ```

    To connect to a named instance on the local server, with Active Directory integration authentication:

    ```cmd
    sqlcmd -S localhost\instancename
    ```

    For more information on **sqlcmd** syntax, see [sqlcmd utility](../../tools/sqlcmd-utility.md). 
    
    For each file to be moved, use **sqlcmd** commands or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to run the following statement. For more information about using the **sqlcmd** utility, see [Use the sqlcmd Utility](../../ssms/scripting/sqlcmd-use-the-utility.md). Once the **sqlcmd** session is open, run the following statement once for each file to be moved:

    ```cmd  
    ALTER DATABASE database_name MODIFY FILE( NAME = logical_name , FILENAME = 'new_path\os_file_name' )  
    GO
    ```  
  
5.  Exit the **sqlcmd** utility or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
6.  Stop the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example, run `NET STOP MSSQLSERVER` in the command-line prompt.  
  
7.  Copy the file or files to the new location.  
  
8.  Restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example, run `NET START MSSQLSERVER` in the command-line prompt.  
  
9.  Verify the file change by running the following query.  
  
    ```sql  
    SELECT name, physical_name AS CurrentLocation, state_desc  
    FROM sys.master_files  
    WHERE database_id = DB_ID(N'<database_name>');  
    ```  
    
10. Since in Step 7 you copied the database files instead of moving them, now you can safely delete the unused database files from their previous location. 

##  <a name="master"></a> Moving the master Database  
 To move the `master` database, follow these steps.  
 
1. Verify that the service account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service account cannot control the files in their new location, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance will not start.
 
2.  From the **Start** menu, locate and launch **SQL Server Configuration Manager**. For more information on the expected location, see [SQL Server Configuration Manager](../sql-server-configuration-manager.md).

  
3.  In the **SQL Server Services** node, right-click the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (for example, **SQL Server (MSSQLSERVER)**) and choose **Properties**.  
  
4.  In the **SQL Server (**_instance_name_**) Properties** dialog box, select the **Startup Parameters** tab.  
  
5.  In the **Existing parameters** box, select the `-d` parameter. In the **Specify a startup parameter** box, change the parameter to the new path of the `master` *data* file. Select **Update** to save the change.
  
6.  In the **Existing parameters** box, select the `-l` parameter. In the **Specify a startup parameter** box, change the parameter to the new path of the `master` *log* file. Select **Update** to save the change.

     The parameter value for the data file must follow the `-d` parameter and the value for the log file must follow the `-l` parameter. The following example shows the parameter values for the default location of the `master` data file.  
  
     `-dC:\Program Files\Microsoft SQL Server\MSSQL<version>.MSSQLSERVER\MSSQL\DATA\master.mdf`  
  
     `-lC:\Program Files\Microsoft SQL Server\MSSQL<version>.MSSQLSERVER\MSSQL\DATA\mastlog.ldf`  
  
     If the planned relocation for the `master` data file is `E:\SQLData`, the parameter values would be changed as follows:  
  
     `-dE:\SQLData\master.mdf`  
  
     `-lE:\SQLData\mastlog.ldf`  

7.  Select **OK** to save the changes permanently and close the **SQL Server (**_instance_name_**) Properties** dialog box.

8.  Stop the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by right-clicking the instance name and choosing **Stop**.  
  
9.  Copy the `master.mdf` and `mastlog.ldf` files to the new location.  
  
10. Restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
11. Verify the file change for the `master` database by running the following query.  
  
    ```sql  
    SELECT name, physical_name AS CurrentLocation, state_desc  
    FROM sys.master_files
    WHERE database_id = DB_ID('master');  
    ```  

12. At this point [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should run normally. However Microsoft recommends also adjusting the registry entry at `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\instance_ID\Setup`, where *instance_ID* is like `MSSQL13.MSSQLSERVER`. In that hive, change the `SQLDataRoot` value to the new path of the new location of the `master` database files. Failure to update the registry can cause patching and upgrading to fail.

13. Since in Step 9 you copied the database files instead of moving them, now you can safely delete the unused database files from their previous location. 
  
##  <a name="Resource"></a> Moving the Resource database  
 The location of the `Resource` database is `\<*drive*>:\Program Files\Microsoft SQL Server\MSSQL\<version>.\<*instance_name*>\MSSQL\Binn\\`. The database cannot be moved.  
  
##  <a name="Follow"></a> Follow-up: After moving all system databases  

If you have moved all of the system databases to a new drive or volume or to another server with a different drive letter, make the following updates.  
  
-   Change the SQL Server Agent log path. If you do not update this path, SQL Server Agent will fail to start.  
  
-   Change the database default location. Creating a new database may fail if the drive letter and path specified as the default location do not exist.  
  
#### Change the SQL Server Agent Log Path  
  
If you have moved all of the system databases to a new volume or have migrated to another server with a different drive letter, and the path of the SQL Agent error log file `SQLAGENT.OUT` no longer exists, make the following updates.
  
1.  From SQL Server Management Studio, in **Object Explorer**, expand **SQL Server Agent**.  
  
2.  Right-click **Error Logs** and select **Configure**.  
  
3.  In the **Configure SQL Server Agent Error Logs** dialog box, specify the new location of the SQLAGENT.OUT file. The default location is `C:\Program Files\Microsoft SQL Server\MSSQL\<version>.<instance_name>\MSSQL\Log\\`.  

#### Change the database default location  
  
1.  From SQL Server Management Studio, in **Object Explorer**, connect to the desired [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Right-click the instance and select **Properties**.  
  
2.  In the **Server Properties** dialog box, select **Database Settings**.  
  
3.  Under **Database Default Locations**, browse to the new location for both the data and log files.  
  
4.  Stop and start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service to complete the change.  
  
##  <a name="Examples"></a> Examples  

### A. Moving the tempdb database  
 The following example moves the `tempdb` data and log files to a new location as part of a planned relocation.  
 
> [!TIP]  
> Take this opportunity to review your `tempdb` files for optimal size and placement. For more information, see [Optimizing tempdb performance in SQL Server](tempdb-database.md#optimizing-tempdb-performance-in-sql-server).
  
 Because `tempdb` is re-created each time the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started, you do not have to physically move the data and log files. The files are created in the new location when the service is restarted in step 4. Until the service is restarted, `tempdb` continues to use the data and log files in the existing location.  

  
1.  Determine the logical file names of the `tempdb` database and their current location on the disk.  
  
    ```sql  
    SELECT name, physical_name AS CurrentLocation  
    FROM sys.master_files  
    WHERE database_id = DB_ID(N'tempdb');  
    GO  
    ```  

2. Verify that the service account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service account cannot control the files in their new location, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance will not start.
  
3. Change the location of each file by using `ALTER DATABASE`.  
  
    ```sql  
    USE master;  
    GO  
    ALTER DATABASE tempdb   
    MODIFY FILE (NAME = tempdev, FILENAME = 'E:\SQLData\tempdb.mdf');  
    GO  
    ALTER DATABASE tempdb   
    MODIFY FILE (NAME = templog, FILENAME = 'F:\SQLLog\templog.ldf');  
    GO  
    ```  
    
    Until the service is restarted, `tempdb` continues to use the data and log files in the existing location.  

  
4.  Stop and restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
5.  Verify the file change.  
  
    ```sql  
    SELECT name, physical_name AS CurrentLocation, state_desc  
    FROM sys.master_files  
    WHERE database_id = DB_ID(N'tempdb');  
    ```  
  
6.  Delete the unused `tempdb` files from their original location.  
  
## See Also  

- [Resource Database](../../relational-databases/databases/resource-database.md)   
- [tempdb Database](../../relational-databases/databases/tempdb-database.md)   
- [master Database](../../relational-databases/databases/master-database.md)   
- [msdb Database](../../relational-databases/databases/msdb-database.md)   
- [model Database](../../relational-databases/databases/model-database.md)   

## Next steps

- [Move User Databases](../../relational-databases/databases/move-user-databases.md)   
- [Move Database Files](../../relational-databases/databases/move-database-files.md)   
- [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)   
- [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
- [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md)  
   
