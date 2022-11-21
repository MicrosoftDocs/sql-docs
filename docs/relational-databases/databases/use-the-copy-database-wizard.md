---
description: "Use the Copy Database Wizard"
title: "Use the Copy Database Wizard"
ms.custom: ""
ms.date: "10/21/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.cdw.packageconfiguration.f1"
  - "sql13.swb.cdw.schedule.f1"
  - "sql13.swb.cdw.transfermethod.f1"
  - "sql13.swb.cdw.databases.f1"
  - "sql13.swb.cdw.destinationconfiguration.f1"
  - "sql13.swb.cdw.destination.f1"
  - "sql13.swb.cdw.locationofsourcedbfiles.f1"
  - "sql13.swb.cdw.source.f1"
  - "sql13.swb.cdw.selectdatabaseobjects.f1"
  - "sql13.swb.cdw.complete.f1"
  - "sql13.swb.cdw.welcome.f1"
helpviewer_keywords: 
  - "Copy Database Wizard"
  - "starting Copy Database Wizard"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Use the Copy Database Wizard
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
The Copy Database Wizard moves or copies databases and certain server objects easily from one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  to another instance, with no server downtime. By using this wizard, you can do the following: 
  
-   Pick a source and destination server.  
  
-   Select database(s) to move or copy.  
  
-   Specify the file location for the database(s).  
  
-   Copy logins to the destination server.  
  
-   Copy additional supporting objects, jobs, user-defined stored procedures, and error messages.  
  
-   Schedule when to move or copy the database(s).  

> [!NOTE]
> Considering a migration to Azure SQL? Review the following tools instead of the Copy Database Wizard:
> - [Migrate to Azure SQL guides](/azure/azure-sql/migration-guides/)
> - [Azure Database Migration Service](/azure/dms/)
> - [Azure Data Migration Assistant](../../dma/dma-migrateonpremsqltosqldb.md)
> - [Azure Migrate Service](/azure/migrate/migrate-services-overview)

##  <a name="Restrictions"></a> Limitations and restrictions  
  
-   The Copy Database Wizard is not available in the Express edition.  
  
-   The Copy Database Wizard cannot be used to copy or move databases that:
  
    -   Are system databases (`master`,`model`,`msdb`,`tempdb`).
  
    -   Are marked for replication.
  
    -   Are marked Inaccessible, Loading, Offline, Recovering, Suspect, or in Emergency Mode.
    
    -   Have data or log files stored in Microsoft Azure storage.
  
-   When using [FileTables](../../relational-databases/blob/filetables-sql-server.md), you can't use the Copy Database Wizard on the same server because the wizard uses the same directory name.

-   A database cannot be moved or copied to an earlier version of SQL Server.
  
-   If you select the **Move** option, the wizard deletes the source database automatically after moving the database. The Copy Database Wizard does not delete a source database if you select the **Copy** option.  In addition, selected server objects are copied rather than moved to the destination; the database is the only object that is actually moved.
  
-   If you use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Object method to move the full-text catalog, you must repopulate the index after the move.  
  
-   The **detach and attach** method detaches the database, moves or copies the database .mdf, .ndf, .ldf files and reattaches the database in the new location. For the **detach and attach** method, to avoid data loss or inconsistency, active sessions cannot be attached to the database being moved or copied. For the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Object method, active sessions are allowed because the database is never taken offline.  

-    Transferring SQL Server Agent jobs that reference databases that do not already exist on the destination server will cause the entire operation to fail.  The Wizard attempts to create a SQL Server Agent job prior to creating the database.  As a workaround:
     1.    Create a shell database on the destination server with the same name as the database to be copied or moved.  See [Create a Database](../../relational-databases/databases/create-a-database.md).
     
     2.    From the **Configure Destination Database** page select **Drop any database on the destination server with the same name, then continue with the database transfer, overwriting existing database files**.

> [!WARNING]
> The **detach and attach** method will cause the source and destination database ownership to become set to the login executing the **Copy Database Wizard**. See [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md) to change the ownership of a database.
  
  
##  <a name="Prerequisites"></a> Prerequisites  
-   Ensure that SQL Server Agent is started on the destination server.  

-   Ensure the data and log file directories on the source server can be reached from the destination server.

-   Under the **detach and attach** method, a SQL Server Agent Proxy for the SQL Server Integration Services (SSIS) subsystem must exist on the destination server with a credential that can access the file system of both the source and destination servers. For more information on proxies, see [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md).


> [!IMPORTANT]
> Under the **detach and attach** method, the copy or move process will fail if an Integration Services Proxy account is not used.  Under certain situations the source database will not become re-attached to the source server and all NTFS security permissions will be stripped from the data and log files.  If this happens, navigate to your files, re-apply the relevant permissions, and then re-attach the database to your instance of SQL Server.

  
##  <a name="Recommendations"></a> Recommendations  
  
-   To ensure optimal performance of an upgraded database, run [sp_updatestats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md) (update statistics) against the upgraded database.  
  
-   When you move or copy a database to another server instance, to provide a consistent experience to users and applications, you might have to re-create some or all of the metadata for the database, such as logins and jobs, on the other server instance. For more information, see [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).  
  
  
###  <a name="Permissions"></a> Permissions  
 You must be a member of the **sysadmin** fixed server role on both the source and destination servers.  
  
##  <a name="Overview"></a> Copy Database wizard

On either the source or destination SQL Server instance, launch the **Copy Database Wizard** in SQL Server Management Studio from **Object Explorer** and expand **Databases**.  Then right-click a database, point to **Tasks**, and then select **Copy Database**.  If the **Welcome to the Copy Database Wizard** splash page appears, select **Next**.

### Select a source server
Used to specify the server with the database to move or copy, and to enter login information.  After you select the authentication method and enter login information, select **Next** to establish the connection to the source server.  This connection remains open throughout the session.

-    **Source server**  
Used to identify the name of the server on which the database(s) you want to move or copy is located.  Manually enter, or select the ellipsis to navigate to the desired server.  The server must be at least SQL Server 2005.

-    **Use Windows Authentication**  
Allows a user to connect through a Microsoft Windows user account.

-    **Use SQL Server Authentication**  
Allows a user to connect by providing a SQL Server Authentication user name and password.

     -    **User name**  
Used to enter the user name to connect with. This option is only available if you have selected to connect using **SQL Server Authentication**.

     -    **Password**  
Used to enter the password for the login. This option is only available if you have selected to connect using **SQL Server Authentication**.

###  Select a destination server
Used to specify the server where the database will be moved or copied to.  If you set the source and destination servers to the same server instance, you will make a copy of the database.  In this case, you must rename the database at a later point in the wizard.  The source database name can be used for the copied or moved database only if name conflicts do not exist on the destination server.  If name conflicts exist, you must resolve them manually on the destination server before you can use the source database name there.
  
-    **Destination server**  
Used to identify the name of the server to which the database(s) you want to move or copy to is located. Manually enter the destination server name or select the ellipsis to navigate to the desired server.  The server must be at least SQL Server 2005.

        > [!NOTE] 
        > You can use a destination that is a clustered server; the Copy Database Wizard will make sure you select only shared drives on a clustered destination server.

-    **Use Windows Authentication**  
Allows a user to connect through a Microsoft Windows user account.

-    **Use SQL Server Authentication**  
Allows a user to connect by providing a SQL Server Authentication user name and password.

     -    **User name**  
Used to enter the user name to connect with. This option is only available if you have selected to connect using **SQL Server Authentication**.

     -    **Password**  
Used to enter the password for the login. This option is only available if you have selected to connect using **SQL Server Authentication**.

###  Select the transfer method
 
Choose either the **detach and attach method** or the **SQL Management Object method**.

-    **Use the detach and attach method**  
Detach the database from the source server, copy the database files (.mdf, .ndf, and .ldf) to the destination server, and attach the database at the destination server. This method is usually the faster method because the principal work is reading the source disk and writing the destination disk. No [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logic is required to create objects within the database, or create data storage structures. This method can be slower, however, if the database contains a large amount of allocated but unused space. For instance, a new and practically empty database that is created allocating 100 MB, copies the entire 100 MB, even if only 5 MB is full.

        > [!NOTE]
        > This method makes the database unavailable to users during the transfer.

        > [!WARNING] 
        > **If a failure occurs, reattach the source database.** When a database is copied, the original database files are always reattached to the source server. Use this box to reattach original files to the source database if a database move cannot be completed. 
  
-    **Use the SQL Management Object method**  
     This method reads the definition of each database object on the source database and creates each object in the destination database. Then it transfers the data from the source tables to the destination tables, recreating indexes and metadata.
  
        > [!NOTE]
        > Database users can continue to access the database during the transfer.
      
###  Select database
Select the database(s) you want to move or copy from the source server to the destination server.  See [Limitations and Restrictions](#Restrictions).  
  
-    **Move**  
Move the database to the destination server.

-    **Copy**  
Copy the database to the destination server.

-    **Source**  
Displays the databases that exist on the source server.

-    **Status**  
Displays various information of the source database.

-    **Refresh**  
Refresh the list of databases.
  
### Configure destination database
Change the database name if appropriate and specify the location and names of the database files.  This page appears once for each database being moved or copied.

-    **Source Database**  
The name of the source database.  The text box is not editable.

-    **Destination Database**  
The name of the destination database to be created, modify as desired.

-    **Destination database files:**  
     
     -    **Filename**  
The name of the destination database file to be created, modify as desired.

     -    **Size (MB)**  
Size of the destination database file in megabytes.

     -    **Destination Folder**  
The folder on the destination server to host the destination database file, modify as desired.

     -    **Status**  
Status

-    **If the destination database already exists:**  
     Decide what action to take if the destination database already exists.

     -    **Stop the transfer if a database or file with the same name exists at the destination.**

     -    **Drop any database on the destination server with the same name, then continue with the database transfer, overwriting existing database files.**

###  Select Server Objects 
This page is only available when the source and destination are different servers.  
  
-    **Available related objects**  
Lists objects available to transfer to the destinations server.  To include an object, select the object name in the **Available related objects** box, and then select the **>>** button to move the object to the **Selected related objects** box. 

-    **Selected related objects**  
Lists objects that will be transferred to the destinations server.  To exclude an object, select the object name in the **Selected related objects** box, and then select the **<\<** button to move the object to the **Available related objects** box.  By default all objects of each selected type are transferred. To choose individual objects of any type, select the ellipsis button next to any object type in the **Selected related objects** box.  This opens a dialog box where you can select individual objects.

-    **List of Server Objects** 

     -   Logins (Selected by default.)
     -    SQL Server Agent jobs
     -    User-defined error messages
     -    Endpoints
     -    Full-text catalog
     -    SSIS Package
     -    Stored procedures from `master` database

          > [!NOTE] 
          > Extended stored procedures and their associated DLLs are not eligible for automated copy.  
    
  
###   Location of source database files
This page is only available when the source and destination are different servers.  Specify a file system share that contains the database files on the source server.
  
-    **Database**  
     Displays the name of each database being moved.  
  
-    **Folder location**  
The folder location of the database files on the source server.
For example: `C:\Program Files\Microsoft SQL Server\MSSQL110.MSSQLSERVER\MSSQL\DATA`.

  
-    **File share on source server**  
The file share containing the database files on the source server.  Manually enter the share, or select the ellipsis to navigate to the share.
For example: `\\server_name\C$\Program Files\Microsoft SQL Server\MSSQL110.MSSQLSERVER\MSSQL\Data`.

### Configure the package
The Copy Database Wizard creates an SSIS package to transfer the database.

-    **Package location**  
Displays to where the SSIS package will be written.

-    **Package name**  
A default name for the SSIS package will be created, modify as desired.

-    **Logging options**  
Select whether to store the logging information in the Windows event log, or in a text file.

-    **Error log file path**  
This option is only available if the text file logging option is selected.  Provide a path for the location of the log file. 

### Schedule the package
Specify when you want the move or copy operation to start.  If you are not a system administrator, you must specify a SQL Server Agent Proxy account that has access to the Integration Services (SSIS) Package execution subsystem.

> [!IMPORTANT]
> An Integration Services Proxy account must be used under the **detach and attach** method.  

-    **Run immediately**  
     SSIS Package will execute after completing the wizard.
  
-    **Schedule**  
     SSIS Package will execute according to a schedule. 
  
     -    **Change Schedule**   
Opens the **New Job Schedule** dialog box.  Configure as desired.  Select **OK** when finished.


-    **Integration Services Proxy account**
Select an available proxy account from the drop-down list.  To schedule the transfer, there must be at least one proxy account available to the user, configured with permission to the **SSIS package execution subsystem**.

        > [!NOTE] 
        > To create a proxy account for SSIS package execution, in **Object Explorer**, expand **SQL Server Agent**, expand **Proxies**, right-click **SSIS Package Execution**, and then select **New Proxy**.

### Complete the wizard
Displays summary of the selected options.  Select **Back** to change an option.  Select **Finish** to create the SSIS package. The **Performing operation** page monitors status information about the execution of the **Copy Database Wizard**.

-    **Action**  
 Lists each action being performed.

-    **Status**  
 Indicates whether the action as a whole succeeded or failed.

-    **Message**  
Provides any messages returned from each step.

##  <a name="Examples"></a> Examples

### **Common Steps** 
Regardless of whether you choose **Move** or **Copy**, **Detach and Attach** or **SMO**, the five steps listed below will be the same.  For brevity, the steps are listed here once and all examples will start on **Step 6**.

1.    In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

2.    Expand **Databases**, right-click the desired database, point to **Tasks**, and then select **Copy Database...**

3.    If the **Welcome to the Copy Database Wizard** splash page appears, select **Next**.

4.    **Select a Source Server** page: Specify the server with the database to move or copy.  Select the authentication method.  If **Use SQL Server Authentication** is chosen you will need to enter your login credentials.  Select **Next** to establish the connection to the source server.  This connection remains open throughout the session.

5.    **Select a Destination Server** page:  Specify the server where the database will be moved or copied to.  Select the authentication method.  If **Use SQL Server Authentication** is chosen you will need to enter your login credentials.  Select **Next** to establish the connection to the source server.  This connection remains open throughout the session.

        > [!NOTE] 
        > You can launch the Copy Database Wizard from any database. You can use the Copy Database Wizard from either the source or destination server.

### **A.  Move database using detach and attach method to an instance on a different physical server.  A login and SQL Server Agent job will be moved as well.**  
The following example will move the `Sales` database, a Windows login named `contoso\Jennie` and a SQL Server Agent job named `Jennie's Report` from a 2008 instance of SQL Server on `Server1` to a 2016 instance of SQL Server on `Server2`.  `Jennie's Report` uses the `Sales` database.  `Sales` does not already exist on the destination server, `Server2`.  `Server1` will be re-assigned to a different team after the database move.
  
6.    As noted in [Limitations and Restrictions](#Restrictions), above, a shell database will need to be created on the destination server when transferring a SQL Server Agent job that references a database that does not already exist on the destination server.  Create a shell database called `Sales` on the destination server. 

7.    Back to the **Wizard**, **Select the Transfer Method** page:  Review and maintain the default values.  Select **Next**.
  
8.    **Select Databases** page: Select the **Move** checkbox for the desired database, `Sales`.  Select **Next**.
  
9.    **Configure Destination Database** page:  The **Wizard** has identified that `Sales` already exists on the destination server, as created in **Step 6** above, and has appended `_new` to the **Destination database** name.  Delete `_new` from the **Destination database** text box.  If desired, change the **Filename**, and **Destination Folder**.  Select **Drop any database on the destination server with the same name, then continue with the database transfer, overwriting existing database files**.  Select **Next**.
  
10.    **Select Server Objects** page: In the **Selected related objects:** panel, select the ellipsis button for **Object name Logins**.  Under **Copy Options** select **Copy only the selected logins:**.  Check the box for **Show all server logins**.  Check the **Login** box for `contoso\Jennie`.  Select **OK**.  In the **Available related objects:** panel, select **SQL Server Agent jobs** and then select the **>** button.  In the **Selected related objects:** panel, select the ellipsis button for **SQL Server Agent jobs**.  Under **Copy Options** select **Copy only the selected jobs**.  Check the box for `Jennie's Report`.  Select **OK**.  Select **Next**.  
  
11.    **Location of Source Database Files** page:  Select the ellipsis button for **File share on source server** and navigate to the location for the given Folder location.  For example, for Folder location `D:\MSSQL13.MSSQLSERVER\MSSQL\DATA` use `\\Server1\D$\MSSQL13.MSSQLSERVER\MSSQL\DATA` for **File share on source server**.  Select **Next**.
  
12.    **Configure the Package** page:  In the **Package name:** text box, enter `SalesFromServer1toServer2_Move`.  Check the **Save transfer logs?** box.  In the **Logging options** drop-down list, select **Text file**.  Note the **Error log file path**; revise as desired.  Select **Next**.  
  
> [!NOTE]
> The **Error log file path** is the path on the destination server.
  
13.    **Schedule the Package** page:  Select the relevant proxy from the **Integration Services Proxy account** drop-down list.  Select **Next**.

14.    **Complete the Wizard** page:  Review the summary of the selected options.  Select **Back** to change an option.  Select **Finish** to execute the task.  During the transfer, the **Performing operation** page monitors status information about the execution of the **Wizard**.

15.    **Performing Operation** page: If operation is successful, select **Close**.  If operation is unsuccessful, review error log, and possibly **Back** for further review.  Otherwise, select **Close**.
  
16.    **Post Move Steps**
Consider executing the following T-SQL statements on the new host, `Server2`:
  
        ```tsql 
        ALTER AUTHORIZATION ON DATABASE::Sales TO sa;
        GO
        ALTER DATABASE Sales 
        SET COMPATIBILITY_LEVEL = 130;
        GO
        
        USE [Sales]
        GO
        EXEC sp_updatestats;
        GO
        ```
 
17.    **Post Move Steps Cleanup**  
Since `Server1` will be moved to a different team and the **Move** operation will not be repeated, consider executing the following steps:

   - Deleting SSIS package `SalesFromServer1toServer2_Move` on `Server2`.
   - Deleting SQL Server Agent job `SalesFromServer1toServer2_Move` on `Server2`.
   - Deleting SQL Server Agent job `Jennie's Report` on `Server1`.
   - Dropping login `contoso\Jennie` on `Server1`.




### **B.     Copy database using detach and attach method to the same instance and set recurring schedule.**  
In this example, the `Sales` database will be copied and created as `SalesCopy` on the same instance.  Thereafter, `SalesCopy`, will be re-created on a weekly basis.

6.    **Select a Transfer Method** page:  Review and maintain the default values.  Select **Next**.

7.    **Select Databases** page: Select the **Copy** checkbox for the `Sales` database.  Select **Next**.

8.    **Configure Destination Database** page: Change the **Destination database** name to `SalesCopy`.  If desired, change the **Filename**, and **Destination Folder**.  Select **Drop any database on the destination server with the same name, then continue with the database transfer, overwriting existing database files**.  Select **Next**.

9.    **Configure the Package** page:  In the **Package name:** text box, enter `SalesCopy Weekly Refresh`.  Check the **Save transfer logs?** box.  Select **Next**.

10.    **Schedule the Package** page:  Select the **Schedule:** radio button and then select the **Change Schedule** button. 
 
        1. **New Job Schedule** page: In the **Name** text box enter "Weekly on Sunday". 
              
        2. Select **OK**.

11.    Select the relevant proxy from the **Integration Services Proxy account** drop-down list.  Select **Next**.

12.    **Complete the Wizard** page:  Review the summary of the selected options.  Select **Back** to change an option.  Select **Finish** to execute the task.  During the package creation, the **Performing operation** page monitors status information about the execution of the **Wizard**.

13.    **Performing Operation** page: If operation is successful, select **Close**.  If operation is unsuccessful, review error log, and possibly **Back** for further review.  Otherwise, select **Close**.

14.    Manually start the newly created SQL Server Agent Job `SalesCopy weekly refresh`.  Review job history and ensure `SalesCopy` now exists on the instance.

  
##  <a name="FollowUp"></a> Follow up: After upgrading a database  
 After you use the Copy Database Wizard to upgrade a database from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], the database becomes available immediately and is automatically upgraded. If the database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending on the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt. For information about viewing or changing the setting of the **Full-Text Upgrade Option** property, see [Manage and Monitor Full-Text Search for a Server Instance](../../relational-databases/search/manage-and-monitor-full-text-search-for-a-server-instance.md).  
  
After the upgrade, the database compatibility level remains at the compatibility level before the upgrade, unless the previous compatibility level is not supported on the new version. In this case, the upgraded database compatibility level is set to the lowest supported compatibility level.

For example, if you attach a database that was compatibility level 90 before attaching it to an instance of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], after the upgrade the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).
 
 ## <a name="Post"></a> Post copy or move considerations
 Consider whether to perform the following steps after a **Copy** or **Move**:
-    Changing the ownership of the database(s) when the detach and attach method is used.
-    Dropping server objects on the source server after a **Move**.
-    Dropping the SSIS package created by the Wizard on the destination server.
-    Dropping the SQL Server Agent job created by the Wizard on the destination server.

## Next steps
 - [Upgrade a Database Using Detach and Attach &#40;Transact-SQL&#41;](../../relational-databases/databases/upgrade-a-database-using-detach-and-attach-transact-sql.md)   
 - [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md) 
 - [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)