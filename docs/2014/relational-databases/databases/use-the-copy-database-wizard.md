---
title: "Use the Copy Database Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.cdw.transfermethod.f1"
  - "sql12.swb.cdw.welcome.f1"
  - "sql12.swb.cdw.packageconfiguration.f1"
  - "sql12.swb.cdw.schedule.f1"
  - "sql12.swb.cdw.destination.f1"
  - "sql12.swb.cdw.complete.f1"
  - "sql12.swb.cdw.destinationconfiguration.f1"
  - "sql12.swb.cdw.selectdatabaseobjects.f1"
  - "sql12.swb.cdw.databases.f1"
  - "sql12.swb.cdw.source.f1"
  - "sql12.swb.cdw.locationofsourcedbfiles.f1"
helpviewer_keywords: 
  - "Copy Database Wizard"
  - "starting Copy Database Wizard"
ms.assetid: 7a999fc7-0a26-4a0d-9eeb-db6fc794f3cb
author: stevestein
ms.author: sstein
manager: craigg
---
# Use the Copy Database Wizard
  The Copy Database Wizard lets you move or copy databases and their objects easily from one server to another, with no server downtime. You can also upgrade databases from a previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. By using this wizard, you can do the following:  
  
-   Pick a source and destination server.  
  
-   Select databases to move, copy or upgrade.  
  
-   Specify the file location for the databases.  
  
-   Create logins on the destination server.  
  
-   Copy additional supporting objects, jobs, user-defined stored procedures, and error messages.  
  
-   Schedule when to move or copy the databases.  
  
 In addition to copying databases, you can copy associated metadata, for example, logins and objects from the **master** database that are required by a copied database.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **Using the Copy Database Wizard to:**  
  
     [Copy, Move, or Upgrade Databases](#Copy_Move)  
  
-   **Follow up, after upgrade:**  
  
     [After Upgrading a SQL Server Database](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The Copy Database Wizard is not available in the Express edition.  
  
-   The Copy Database Wizard cannot be used to copy or move the following databases.  
  
    -   System databases  
  
    -   Databases marked for replication.  
  
    -   Databases marked Inaccessible, Loading, Offline, Recovering, Suspect, or in Emergency Mode.  
  
-   After a database has been upgraded, it cannot be downgraded to a previous version.  
  
-   If you select the **Move** option, the wizard deletes the source database automatically after moving the database. The Copy Database Wizard does not delete a source database if you select the **Copy** option.  
  
-   If you use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Object method to move the full-text catalog, you must repopulate the index after the move.  
  
-   The detach-and-attach method detaches the database, moves or copies the database .mdf, .ndf, .ldf files and reattaches the database in the new location. For the detach-and-attach method, to avoid data loss or inconsistency, active sessions cannot be attached to the database being moved or copied. If any active sessions exist, the Copy Database Wizard does not execute the move or copy operation. For the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Object method, active sessions are allowed because the database is never taken offline.  
  
###  <a name="Prerequisites"></a> Prerequisites  
 Ensure that SQL Server Agent is started on the destination server.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   To ensure optimal performance of an upgraded database, run sp_updatestats (update statistics) against the upgraded database.  
  
-   When you copy a database to another server instance, to provide a consistent experience to users and applications, you might have to re-create some or all of the metadata for the database, such as logins and jobs, on the other server instance. For more information, see [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](manage-metadata-when-making-a-database-available-on-another-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must be a member of the **sysadmin** fixed server role on both the source and destination servers.  
  
##  <a name="Copy_Move"></a> Copy, Move or Upgrade Databases  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, expand **Databases**, right-click a database, point to **Tasks**, and then click **Copy Database**.  
  
2.  From the **Select a Source Server** page, specify the server with the database to move or copy, and to enter login information. After you select the authentication method and enter login information, click **Next** to establish the connection to the source server. This connection remains open throughout the session.  
  
     **Source server**  
     Select the name of the server on which the database or databases you want to move or copy are located, or click the browse (**...**) button to locate the server you want. The server must be at least [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
     **Use Windows Authentication**  
     Allow a user to connect through a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user account.  
  
     **Use SQL Server Authentication**  
     Allow a user to connect by providing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication user name and password.  
  
     **User name**  
     Enter the user name to connect with. This option is only available if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication .  
  
     **Password**  
     Enter the password for the login. This option is only available if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
     **Next**  
     Connect to the server and validate the user. This process checks whether the user is a member of the **sysadmin** fixed server role on the selected computer.  
  
3.  From the **Select a Destination Server** page, specify the server where the database will be moved or copied. If you set the source and destination servers to the same server instance, you will make a copy of a database. In this case you must rename the database at a later point in the wizard. The source database name can be used for the copied or moved database only if name conflicts do not exist on the destination server. If name conflicts exist, you must resolve them manually on the destination server before you can use the source database name there.  
  
     **Destination server**  
     Select the name of the server to which the database or databases will be moved or copied, or click the browse (**...**) button to locate a destination server.  
  
    > [!NOTE]  
    >  You can use a destination that is a clustered server; the Copy Database Wizard will make sure you select only shared drives on a clustered destination server.  
  
     **Use Windows Authentication**  
     Allow a user to connect through a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user account.  
  
     **Use SQL Server Authentication**  
     Allow a user to connect by providing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication user name and password.  
  
     **User name**  
     Enter the user name to connect with. This option is only available if you have selected [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
     **Password**  
     Enter the password for the login. This option is only available if you have selected [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
     **Next**  
     Connect to the server and validate the user. This process checks whether the user has the permissions listed above on the selected computers.  
  
4.  From the **Select a Transfer Method** page, select the transfer method.  
  
     **Use the detach and attach method**  
     Detach the database from the source server, copy the database files (.mdf, .ndf, and .ldf) to the destination server, and attach the database at the destination server. This method is usually the faster method because the principal work is reading the source disk and writing the destination disk. No [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logic is required to create objects within the database, or create data storage structures. This method can be slower, however, if the database contains a large amount of allocated but unused space. For instance, a new and practically empty database that is created allocating 100 MB, copies the entire 100 MB, even if only 5 MB is full.  
  
    > [!NOTE]  
    >  This method makes the database unavailable to users during the transfer.  
  
     **If a failure occurs, reattach the source database**  
     When a database is copied, the original database files are always reattached to the source server. Use this box to reattach original files to the source database if a database move cannot be completed.  
  
     **Use the SQL Management Object method**  
     This method reads the definition of each database object on the source database and creates each object in the destination database. Then it transfers the data from the source tables to the destination tables, recreating indexes and metadata.  
  
    > [!NOTE]  
    >  Database users can continue to access the database during the transfer.  
  
5.  From the **Select Database** page, select the database or databases you want to move or copy from the source server to the destination server. See [Limitations and Restrictions](#Restrictions) in the 'Before You Begin' section of this topic.  
  
     **Move**  
     Move the database to the destination server.  
  
     **Copy**  
     Copy the database to the destination server.  
  
     **Source**  
     Displays the databases that exist on the source server.  
  
     **Status**  
     Displays **OK** if the database can be moved. Otherwise displays the reason why the database cannot be moved.  
  
     **Refresh**  
     Refresh the list of databases.  
  
     **Next**  
     Start the validation process, and then move to the next screen.  
  
6.  From the **Configure Destination Database** page, change the database name if appropriate and specify the location and names of the database files. This page appears once for each database being moved or copied.  
  
7.  From the **Select Database Objects** page, select the objects to include in the move or copy operation. This page is only available when the source and destination are different servers. To include an object, click the object name in the **Available related objects** box, and then click the **>>** button to move the object to the **Selected related objects** box. To exclude an object, click the object name in the **Selected related objects** box, and then click the **<\<** button to move the object to the **Available related objects** box. By default all objects of each selected type are transferred. To choose individual objects of any type, click the ellipsis button next to any object type in the **Selected related objects** box. This opens a dialog box where you can select individual objects.  
  
     **Logins (All logins at run time)**  
     Include logins in the move or copy operation. Selected by default.  
  
     **Stored procedures from master database**  
     Include stored procedures from the **master** database in the move or copy operation.  
  
    > [!NOTE]  
    >  Extended stored procedures and their associated DLLs are not eligible for automated copy.  
  
     **SQL Server Agent jobs**  
     Include jobs from the **msdb** database in the move or copy operation.  
  
     **User-defined error messages**  
     Include user-defined error messages in the move or copy operation.  
  
     **Endpoints**  
     Include endpoints defined in the source database.  
  
     **Full-text catalog**  
     Include full-text catalogs from the source database.  
  
     **SSIS Package**  
     Include [!INCLUDE[ssIS](../../includes/ssis-md.md)] packages defined in the source database.  
  
     **Description**  
     A description of the object.  
  
8.  From the **Location of Source Database Files** page, specify a file system share that contains the database files on the source server. This is required if the source and destination server instances are on different computers.  
  
     **Database**  
     Displays the name of each database being moved.  
  
     **Folder location**  
     Specify the location of the source database files on the file system.  
  
     For example: C:\Program Files\Microsoft SQL Server\MSSQL110.MSSQLSERVER\MSSQL\DATA  
  
     **File share on source server**  
     Specify the location of the source database files as a path of a file share.  
  
     For example: "\\\\*server_name*\C$\Program Files\Microsoft SQL Server\MSSQL110.MSSQLSERVER\MSSQL\Data  
  
9. The Copy Database Wizard creates a [!INCLUDE[ssIS](../../includes/ssis-md.md)] package to transfer the database From the **Configure the Package** page, customize the package if appropriate.  
  
     **Package location**  
     Displays where the [!INCLUDE[ssIS](../../includes/ssis-md.md)] package will be written.  
  
     **Package name**  
     Enter a name for the [!INCLUDE[ssIS](../../includes/ssis-md.md)] package.  
  
     **Logging options**  
     Select whether to store the logging information in the Windows event log, or in a text file.  
  
     **Error log file path**  
     Provide a path for the location of the log file. This option is only available if the text file logging option is selected.  
  
10. From the **Schedule the Package** page, specify when you want the move or copy operation to start. If you are not a system administrator, you must specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Proxy account that has access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] (SSIS) Package execution subsystem.  
  
     **Run immediately**  
     Start the move or copy operation after you click **Next**.  
  
     **Schedule**  
     Start the move or copy operation later. The current schedule settings appear in the description box. To change the schedule, click **Change**.  
  
     **Change**  
     Open the **New Job Schedule** dialog box.  
  
     **Integration Services proxy account**  
     Select an available proxy account. To schedule the transfer, there must be at least one proxy account available to the user, configured with permission to the **SQL Server Integration Services package execution** subsystem.  
  
     To create a proxy account for [!INCLUDE[ssIS](../../includes/ssis-md.md)] package execution, in Object Explorer, expand **SQL Server Agent**, expand **Proxies**, right-click **SSIS Package Execution**, and then click **New Proxy**.  
  
     Members of the **sysadmin** fixed server role can select the **SQL Server Agent Service Account**, which has the necessary permissions.  
  
11. From the **Complete the Wizard** page, review the summary of the selected options. Click **Back** to change an option. Click **Finish** to create the database. During the transfer, the **Performing operation** page monitors status information about the execution of the **Copy Database Wizard**.  
  
     **Action**  
     Lists each action being performed.  
  
     **Status**  
     Indicates whether the action as a whole succeeded or failed.  
  
     **Message**  
     Provides any messages returned from each step.  
  
##  <a name="FollowUp"></a> Follow Up: After Upgrading a SQL Server Database  
 After you use the Copy Database Wizard to upgrade a database from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the database becomes available immediately and is automatically upgraded. If the database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt. For information about viewing or changing the setting of the **Full-Text Upgrade Option** property, see [Manage and Monitor Full-Text Search for a Server Instance](../search/manage-and-monitor-full-text-search-for-a-server-instance.md).  
  
 If the compatibility level of a user database was 100 or higher before upgrade, it remains the same after upgrade. If the compatibility level was 90 in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level).  
  
## See Also  
 [Upgrade a Database Using Detach and Attach &#40;Transact-SQL&#41;](upgrade-a-database-using-detach-and-attach-transact-sql.md)   
 [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md)  
  
  
