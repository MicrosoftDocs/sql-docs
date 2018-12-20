---
title: "Rebuild System Databases | Microsoft Docs"
ms.custom: ""
ms.date: "06/06/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "master database [SQL Server], rebuilding"
  - "REBUILDDATABASE parameter"
  - "rebuilding databases, master"
  - "system databases [SQL Server], rebuilding"
ms.assetid: af457ecd-523e-4809-9652-bdf2e81bd876
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Rebuild System Databases
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  System databases must be rebuilt to fix corruption problems in the [master](../../relational-databases/databases/master-database.md), [model](../../relational-databases/databases/model-database.md), [msdb](../../relational-databases/databases/msdb-database.md), or [resource](../../relational-databases/databases/resource-database.md) system databases or to modify the default server-level collation. This topic provides step-by-step instructions to rebuild system databases in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
-   **Procedures:**  
  
     [Rebuild System Databases](#RebuildProcedure)  
  
     [Rebuild the resource Database](#Resource)  
  
     [Create a New msdb Database](#CreateMSDB)  
  
-   **Follow Up:**  
  
     [Troubleshoot Rebuild Errors](#Troubleshoot)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 When the master, model, msdb, and tempdb system databases are rebuilt, the databases are dropped and re-created in their original location. If a new collation is specified in the rebuild statement, the system databases are created using that collation setting. Any user modifications to these databases are lost. For example, you may have user-defined objects in the master database, scheduled jobs in msdb, or changes to the default database settings in the model database.  
  
###  <a name="Prerequisites"></a> Prerequisites  
 Perform the following tasks before you rebuild the system databases to ensure that you can restore the system databases to their current settings.  
  
1.  Record all server-wide configuration values.  
  
    ```  
    SELECT * FROM sys.configurations;  
    ```  
  
2.  Record all service packs and hotfixes applied to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the current collation. You must reapply these updates after rebuilding the system databases.  
  
    ```  
    SELECT  
    SERVERPROPERTY('ProductVersion ') AS ProductVersion,  
    SERVERPROPERTY('ProductLevel') AS ProductLevel,  
    SERVERPROPERTY('ResourceVersion') AS ResourceVersion,  
    SERVERPROPERTY('ResourceLastUpdateDateTime') AS ResourceLastUpdateDateTime,  
    SERVERPROPERTY('Collation') AS Collation;  
    ```  
  
3.  Record the current location of all data and log files for the system databases. Rebuilding the system databases installs all system databases to their original location. If you have moved system database data or log files to a different location, you must move the files again.  
  
    ```  
    SELECT name, physical_name AS current_file_location  
    FROM sys.master_files  
    WHERE database_id IN (DB_ID('master'), DB_ID('model'), DB_ID('msdb'), DB_ID('tempdb'));  
    ```  
  
4.  Locate the current backup of the master, model, and msdb databases.  
  
5.  If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured as a replication Distributor, locate the current backup of the distribution database.  
  
6.  Ensure you have appropriate permissions to rebuild the system databases. To perform this operation, you must be a member of the **sysadmin** fixed server role. For more information, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md).  
  
7.  Verify that copies of the master, model, msdb data and log template files exist on the local server. The default location for the template files is C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\Templates. These files are used during the rebuild process and must be present for Setup to succeed. If they are missing, run the Repair feature of Setup, or manually copy the files from your installation media. To locate the files on the installation media, navigate to the appropriate platform directory (x86 or x64) and then navigate to setup\sql_engine_core_inst_msi\Pfiles\SqlServr\MSSQL.X\MSSQL\Binn\Templates.  
  
##  <a name="RebuildProcedure"></a> Rebuild System Databases  
 The following procedure rebuilds the master, model, msdb, and tempdb system databases. You cannot specify the system databases to be rebuilt. For clustered instances, this procedure must be performed on the active node and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource in the corresponding cluster application group must be taken offline before performing the procedure.  
  
 This procedure does not rebuild the resource database. See the section, "Rebuild the resource Database Procedure" later in this topic.  
  
#### To rebuild system databases for an instance of SQL Server:  
  
1.  Insert the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation media into the disk drive, or, from a command prompt, change directories to the location of the setup.exe file on the local server. The default location on the server is C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\SQLServer2016.  
  
2.  From a command prompt window, enter the following command. Square brackets are used to indicate optional parameters. Do not enter the brackets. When using a Windows operating system that has User Account Control (UAC) enabled, running Setup requires elevated privileges. The command prompt must be run as Administrator.  
  
     **Setup /QUIET /ACTION=REBUILDDATABASE /INSTANCENAME=InstanceName /SQLSYSADMINACCOUNTS=accounts [ /SAPWD= StrongPassword ] [ /SQLCOLLATION=CollationName]**  
  
    |Parameter name|Description|  
    |--------------------|-----------------|  
    |/QUIET or /Q|Specifies that Setup run without any user interface.|  
    |/ACTION=REBUILDDATABASE|Specifies that Setup re-create the system databases.|  
    |/INSTANCENAME=*InstanceName*|Is the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For the default instance, enter MSSQLSERVER.|  
    |/SQLSYSADMINACCOUNTS=*accounts*|Specifies the Windows groups or individual accounts to add to the **sysadmin** fixed server role. When specifying more than one account, separate the accounts with a blank space. For example, enter **BUILTIN\Administrators MyDomain\MyUser**. When you are specifying an account that contains a blank space within the account name, enclose the account in double quotation marks. For example, enter **NT AUTHORITY\SYSTEM**.|  
    |[ /SAPWD=*StrongPassword* ]|Specifies the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **sa** account. This parameter is required if the instance uses Mixed Authentication ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication) mode.<br /><br /> **&#42;&#42; Security Note &#42;&#42;**The **sa** account is a well-known [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account and it is often targeted by malicious users. It is very important that you use a strong password for the **sa** login.<br /><br /> Do not specify this parameter for Windows Authentication mode.|  
    |[ /SQLCOLLATION=*CollationName* ]|Specifies a new server-level collation. This parameter is optional. When not specified, the current collation of the server is used.<br /><br /> **\*\* Important \*\***Changing the server-level collation does not change the collation of existing user databases. All newly created user databases will use the new collation by default.<br /><br /> For more information, see [Set or Change the Server Collation](../../relational-databases/collations/set-or-change-the-server-collation.md).|  
    |[ /SQLTEMPDBFILECOUNT=NumberOfFiles ]|Specifies the number of tempdb data files. This value can be increased up to 8 or the number of cores, whichever is higher.<br /><br /> Default value: 8 or the number of cores, whichever is lower.|  
    |[ /SQLTEMPDBFILESIZE=FileSizeInMB ]|Specifies the initial size of each tempdb data file in MB. Setup allows the size up to 1024 MB.<br /><br /> Default value: 8|  
    |[ /SQLTEMPDBFILEGROWTH=FileSizeInMB ]|Specifies the file growth increment of each tempdb data file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024 MB.<br /><br /> Default value: 64|  
    |[ /SQLTEMPDBLOGFILESIZE=FileSizeInMB ]|Specifies the initial size of the tempdb log file in MB. Setup allows the size up to 1024 MB.<br /><br /> Default value: 8.<br /><br /> Allowed range: Min = 8, max = 1024.|  
    |[ /SQLTEMPDBLOGFILEGROWTH=FileSizeInMB ]|Specifies the file growth increment of the tempdb log file in MB. A value of 0 indicates that automatic growth is off and no additional space is allowed. Setup allows the size up to 1024 MB.<br /><br /> Default value: 64<br /><br /> Allowed range: Min = 8, max = 1024.|  
    |[ /SQLTEMPDBDIR=Directories ]|Specifies the directories for tempdb data files. When specifying more than one directory, separate the directories with a blank space. If multiple directories are specified the tempdb data files will be spread across the directories in a round-robin fashion.<br /><br /> Default value: System Data Directory|  
    |[ /SQLTEMPDBLOGDIR=Directory ]|Specifies the directory for the tempdb log file.<br /><br /> Default value: System Data Directory|  
  
3.  When Setup has completed rebuilding the system databases, it returns to the command prompt with no messages. Examine the Summary.txt log file to verify that the process completed successfully. This file is located at C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\Logs.  
  
4.  RebuildDatabase scenario deletes system databases and installs them again in clean state. Because the setting of tempdb file count does not persist, the value of number of tempdb files is not known during setup. Therefore, RebuildDatabase scenario does not know the count of tempdb files to be re-added. You can provide the value of the number of tempdb files again with the SQLTEMPDBFILECOUNT parameter. If the parameter is not provided, RebuildDatabase will add a default number of tempdb files, which is as many tempdb files as the CPU count or 8, whichever is lower.  
  
## Post-Rebuild Tasks  
 After rebuilding the database you may need to perform the following additional tasks:  
  
-   Restore your most recent full backups of the master, model, and msdb databases. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).  
  
    > [!IMPORTANT]  
    >  If you have changed the server collation, do not restore the system databases. Doing so will replace the new collation with the previous collation setting.  
  
     If a backup is not available or if the restored backup is not current, re-create any missing entries. For example, re-create all missing entries for your user databases, backup devices, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins, end points, and so on. The best way to re-create entries is to run the original scripts that created them.  
  
> [!IMPORTANT]  
>  We recommend that you secure your scripts to prevent their being altered by unauthorized by individuals.  
  
-   If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured as a replication Distributor, you must restore the distribution database. For more information, see [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md).  
  
-   Move the system databases to the locations you recorded previously. For more information, see [Move System Databases](../../relational-databases/databases/move-system-databases.md).  
  
-   Verify the server-wide configuration values match the values you recorded previously.  
  
##  <a name="Resource"></a> Rebuild the resource Database  
 The following procedure rebuilds the resource system database. When you rebuild the resource database, all service packs and hot fixes are lost, and therefore must be reapplied.  
  
#### To rebuild the resource system database:  
  
1.  Launch the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup program (setup.exe) from the distribution media.  
  
2.  In the left navigation area, click **Maintenance**, and then click **Repair**.  
  
3.  Setup support rule and file routines run to ensure that your system has prerequisites installed and that the computer passes Setup validation rules. Click **OK** or **Install** to continue.  
  
4.  On the Select Instance page, select the instance to repair, and then click **Next**.  
  
5.  The repair rules will run to validate the operation. To continue, click **Next**.  
  
6.  From the **Ready to Repair** page, click **Repair**. The Complete page indicates that the operation is finished.  
  
##  <a name="CreateMSDB"></a> Create a New msdb Database  
 If the **msdb** database is damaged and you do not have a backup of the **msdb** database, you can create a new **msdb** by using the **instmsdb** script.  
  
> [!WARNING]  
>  Rebuilding the **msdb** database using the **instmsdb** script will eliminate all the information stored in **msdb** such as jobs, alert, operators, maintenance plans, backup history, Policy-Based Management settings, Database Mail, Performance Data Warehouse, etc.  
  
1.  Stop all services connecting to the [!INCLUDE[ssDE](../../includes/ssde-md.md)], including [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, [!INCLUDE[ssRS](../../includes/ssrs.md)], [!INCLUDE[ssIS](../../includes/ssis-md.md)], and all applications using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as data store.  
  
2.  Start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the command line using the command: `NET START MSSQLSERVER /T3608`  
  
     For more information, see [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  
  
3.  In another command line window, detach the **msdb** database by executing the following command, replacing *\<servername>* with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: `SQLCMD -E -S<servername> -dmaster -Q"EXEC sp_detach_db msdb"`  
  
4.  Using the Windows Explorer, rename the **msdb** database files. By default these are in the DATA sub-folder for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
5.  Using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, stop and restart the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service normally.  
  
6.  In a command line window, connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and execute the command: `SQLCMD -E -S<servername> -i"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Install\instmsdb.sql" -o"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Install\instmsdb.out"`  
  
     Replace *\<servername>* with the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Use the file system path of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
7.  Using the Windows Notepad, open the **instmsdb.out** file and check the output for any errors.  
  
8.  Re-apply any service packs or hotfix installed on the instance.  
  
9. Recreate the user content stored in the **msdb** database, such as jobs, alert, etc.  
  
10. Backup the **msdb** database.  
  
##  <a name="Troubleshoot"></a> Troubleshoot Rebuild Errors  
 Syntax and other run-time errors are displayed in the command prompt window. Examine the Setup statement for the following syntax errors:  
  
-   Missing slash mark (/) in front of each parameter name.  
  
-   Missing equal sign (=) between the parameter name and the parameter value.  
  
-   Presence of blank spaces between the parameter name and the equal sign.  
  
-   Presence of commas (,) or other characters that are not specified in the syntax.  
  
 After the rebuild operation is complete, examine the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logs for any errors. The default log location is C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\Logs. To locate the log file that contains the results of the rebuild process, change directories to the Logs folder from a command prompt, and then run `findstr /s RebuildDatabase summary*.*`. This search will point you to any log files that contain the results of rebuilding system databases. Open the log files and examine them for relevant error messages.  
  
## See Also  
 [System Databases](../../relational-databases/databases/system-databases.md)  
  
  
