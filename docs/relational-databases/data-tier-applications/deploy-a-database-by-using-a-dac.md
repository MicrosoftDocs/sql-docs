---
title: "Deploy a Database By Using a DAC | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dbdeployment.settings.f1"
  - "sql13.swb.dbdeployment.progress.f1"
  - "sql13.swb.dbdeployment.summary.f1"
  - "sql13.swb.dbdeployment.results.f1"
  - "sql13.swb.dbdeployment.welcome.f1"
helpviewer_keywords: 
  - "deploy database wizard"
  - "database deploy [SQL Server]"
ms.assetid: 08c506e8-4ba0-4a19-a066-6e6a5c420539
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Deploy a Database By Using a DAC
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Use the **Deploy a Database to SQL Azure** Wizard to deploy a database between an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and a [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] server, or between two [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]servers.  
  
##  <a name="BeforeBegin"></a> Before You Begin  
 The wizard uses a Data-tier Application (DAC) BACPAC archive file to deploy both the data and the definitions of database objects. It performs a DAC export operation from the source database, and a DAC import to the destination.  
  
###  <a name="DBOptSettings"></a> Database Options and Settings  
 By default, the database created during the deployment will have the default settings from the CREATE DATABASE statement. The exception is that the database collation and compatibility level are set to the values from the source database.  
  
 Database options, such as TRUSTWORTHY, DB_CHAINING and HONOR_BROKER_PRIORITY, cannot be adjusted as part of the deployment process. Physical properties, such as the number of filegroups, or the numbers and sizes of files cannot be altered as part of the deployment process. After the deployment completes, you can use the ALTER DATABASE statement, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell to tailor the database.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 The **Deploy Database** wizard supports deploying a database:  
  
-   From an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)].  
  
-   From [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   Between two [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] servers.  
  
 The wizard does not support deploying databases between two instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 An instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] must be running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later to work with the wizard. If a database on an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] contains objects not supported on [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)], you cannot use the wizard to deploy the database to [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]. If a database on [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] contains objects not supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you cannot use the wizard to deploy the database to instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
###  <a name="Security"></a> Security  
 To improve security, SQL Server Authentication logins are stored in a DAC BACPAC file without a password. When the BACPAC is imported, the login is created as a disabled login with a generated password. To enable the logins, log in using a login that has ALTER ANY LOGIN permission and use ALTER LOGIN to enable the login and assign a new password that can be communicated to the user. This is not needed for Windows Authentication logins because their passwords are not managed by SQL Server.  
  
#### Permissions  
 The wizard requires DAC export permissions on the source database. The login requires at least ALTER ANY LOGIN and database scope VIEW DEFINITION permissions, as well as SELECT permissions on **sys.sql_expression_dependencies**. Exporting a DAC can be done by members of the securityadmin fixed server role who are also members of the database_owner fixed database role in the database from which the DAC is exported. Members of the sysadmin fixed server role or the built-in SQL Server system administrator account named **sa** can also export a DAC.  
  
 The wizard requires DAC import permissions on the destination instance or server. The login must be a member of the **sysadmin** or **serveradmin** fixed server roles, or in the **dbcreator** fixed server role and have ALTER ANY LOGIN permissions. The built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account named **sa** can also import a DAC. Importing a DAC with logins to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the loginmanager or serveradmin roles. Importing a DAC without logins to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the dbmanager or serveradmin roles.  
  
##  <a name="UsingDeployDACWizard"></a> Using the Deploy Database Wizard  
 **To migrate a database using the Deploy Database Wizard**  
  
1.  Connect to the location of the database you want to deploy. You can specify either an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] or a [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] server.  
  
2.  In **Object Explorer**, expand the node for the instance that has the database.  
  
3.  Expand the **Databases** node.  
  
4.  Right click the database you want to deploy, select **Tasks**, and then select **Deploy Database to SQL Azure...**  
  
5.  Complete the Wizard dialogs:  
  
    -   [Introduction Page](#Introduction)  
  
    -   [Deployment Settings](#Deployment_settings)  
  
    -   [Summary Page](#Summary)  
  
    -   [Progress](#Progress)  
    
    -   [Results](#Results)  
  
##  <a name="Introduction"></a> Introduction Page  
 This page describes the steps for the **Deploy Database** Wizard.  
  
 **Options**  
  
-   **Do not show this page again.** - Click the check box to stop the Introduction page from being displayed in the future.  
  
-   **Next** - Proceeds to the **Deployment Settings** page.  
  
-   **Cancel** - Cancels the operation and closes the Wizard.  
  
##  <a name="Deployment_settings"></a> Deployment Settings Page  
 Use this page to specify the destination server and to provide details about your new database.  
  
 **Local host:**  
  
-   **Server connection** - Specify server connection details and then click **Connect** to verify the connection.  
  
-   **New database name** - Specify a name for the new database.  
  
 **[!INCLUDE[ssSDS](../../includes/sssds-md.md)] database settings:**  
  
-   **[!INCLUDE[ssSDS](../../includes/sssds-md.md)] edition** - Select the edition of [!INCLUDE[ssSDS](../../includes/sssds-md.md)] from the drop-down menu.  
  
-   **Maximum database size** - Select the maximum database size from the drop-down menu.  
  
 **Other settings:**  
  
-   Specify a local directory for the temporary file, which is the BACPAC archive file. Note that the file will be created at the specified location and will remain there after the operation completes.  
  
##  <a name="Summary"></a> Summary Page  
 Use this page to review the specified source and target settings for the operation. To complete the deploy operation using the specified settings, click **Finish**. To cancel the deploy operation and exit the Wizard, click **Cancel**.  
  
##  <a name="Progress"></a> Progress Page  
 This page displays a progress bar that indicates the status of the operation. To view detailed status, click the **View details** option.  
  
##  <a name="Results"></a> Results Page  
 This page reports the success or failure of the deploy operation, showing the results of each action. Any action that encountered an error will have a link in the **Result** column. Click the link to view a report of the error for that action.  
  
 Click **Finish** to close the Wizard.  
  
## Using a .Net Framework Application  
 **To deploy a database using the DacStoreExport() and Import() methods in a .Net Framework application.**  
  
 To view a code example, download the DAC sample application on [Codeplex](https://go.microsoft.com/fwlink/?LinkId=219575)  
  
1.  Create a SMO Server object and set it to the instance or server that contains the database to be deployed.  
  
2.  Open a **ServerConnection** object and connect to the same instance.  
  
3.  Use the **Export** method of the **Microsoft.SqlServer.Management.Dac.DacStore** type to export the database to a BACPAC file. Specify the name of the database to be exported, and the path to the folder where the BACPAC file is to be placed.  
  
4.  Create a SMO Server object and set it to the destination instance or server.  
  
5.  Open a **ServerConnection** object and connect to the same instance.  
  
6.  Use the **Import** method of the **Microsoft.SqlServer.Management.Dac.DacStore** type to import the BACPAC. Specify the BACPAC file created by the export.  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Export a Data-tier Application](../../relational-databases/data-tier-applications/export-a-data-tier-application.md)   
 [Import a BACPAC File to Create a New User Database](../../relational-databases/data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database.md)  
  
  
