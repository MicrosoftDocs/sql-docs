---
description: "Deploy a Data-tier Application"
title: "Deploy a Data-tier Application | Microsoft Docs"
ms.custom:
  - intro-deployment
ms.date: "01/31/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.deploydacwizard.introduction.f1"
  - "sql13.swb.deploydacwizard.deploydac.f1"
  - "sql13.swb.deploydacwizard.summary.f1"
  - "sql13.swb.deploydacwizard.updateconfiguration.f1"
  - "sql13.swb.deploydacwizard.selectdac.f1"
helpviewer_keywords:
  - "Deploy data-tier application"
  - "deploy DAC"
  - "data-tier application [SQL Server], deploy"
  - "How to [DAC], deploy"
  - "wizard [DAC], deploy"
ms.assetid: c117af35-aa53-44a5-8034-fa8715dc735f
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Deploy a Data-tier Application
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Deploy a data-tier application (DAC) from a DAC package to an existing instance of the database engine or Azure SQL Database  using a wizard or a PowerShell script. 
  
 The deployment process registers a DAC instance by storing the DAC definition in the **msdb** system database (**master** in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]); creates a database, then populates that database with all the database objects defined in the DAC.  
 
  
## Deploy the same DAC package multiple times 
 You can deploy the same DAC package to a single instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] multiple times, but you must run the deployments one at a time. The DAC instance name specified for each deployment must be unique within the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 If you deploy a DAC to an instance of the Database Engine, the deployed DAC is incorporated into the **SQL Server Utility** the next time the utility collection set is sent from the instance to the utility control point. The DAC will then be present in the **Deployed Data-tier Applications** node of the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] **Utility Explorer** and reported in the **Deployed Data-tier Applications** details page.  
  
###  Database Options and Settings  
 By default, the database created during the deployment will have all of the default settings from the CREATE DATABASE statement, except:  
  
-   The database collation and compatibility level are set to the values defined in the DAC package. A DAC package built from a database project in the SQL Server Developer Tools uses the values set in the database project. A package extracted from an existing database uses the values from the original database.  
  
-   You can adjust some of the database settings, such as database name and file paths, in the **Update Configuration** page. You cannot set the file paths when deploying to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 Some database options, such as TRUSTWORTHY, DB_CHAINING, and HONOR_BROKER_PRIORITY, cannot be adjusted as part of the deployment process. Physical properties, such as the number of filegroups, or the numbers and sizes of files cannot be altered as part of the deployment process. After the deployment completes, you can use the ALTER DATABASE statement, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell to tailor the database.  
  
## Limitations and restrictions  
 A DAC can be deployed to [!INCLUDE[ssSDS](../../includes/sssds-md.md)], or an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later. If you create a DAC using a later version, the DAC may contain objects not supported by [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. You cannot deploy those DACs to instances of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
    
## Security and permissions
Authentication logins are stored in a DAC package without a password. When the package is deployed or upgraded, the login is created as a disabled login with a generated password. To enable the logins, log in using a login with the ALTER ANY LOGIN permission and use ALTER LOGIN to enable the login and assign a new password that can be communicated to the user. This is not required for Windows Authentication logins because their passwords are not managed by SQL Server.  
  
 A DAC can only be deployed by members of the **sysadmin** or **serveradmin** fixed server roles, or by logins in the **dbcreator** fixed server role with ALTER ANY LOGIN permissions. The built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account named **sa** can also deploy a DAC. 

Deploying a DAC with logins to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the loginmanager or serveradmin roles. Deploying a DAC without logins to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the dbmanager or serveradmin roles.  
  
## Deploy a DAC using the wizard  
  
1.  In **Object Explorer**, expand the node for the instance to which you want to deploy the DAC.  
  
2.  Right-click the **Databases** node, then select **Deploy Data-tier Application...**  
  
3.  Complete the wizard dialogs and click finish.

More about some of the wizard pages below: 
     
### Select DAC Package Page  
 Specify the DAC package that contains the data-tier application to be deployed. The page transitions through three states.  
    
### Select the DAC Package  
 Choose the DAC package to deploy. The DAC package must be a valid DAC package file and must have a .dacpac extension.  
  
 **DAC Package** - Specify the path and file name of the DAC package that contains the data-tier application to be deployed. You can select the **Browse** button at the right of the box to browse to the location of the DAC package.  
  
 **Application Name** - A read-only box that displays the DAC name assigned when the DAC was authored or extracted from a database.  
  
 **Version** - A read-only box that displays the version assigned when the DAC was authored or extracted from a database.  
  
 **Description** - A read-only box that displays the description written when the DAC was authored or extracted from a database.  
  
### Validating the DAC Package  
 Displays a progress bar as the wizard confirms that the selected file is a valid DAC package. If the DAC package is validated, the wizard proceeds to the final version of the **Select Package** page where you can review the results of the validation. If the file is not a valid DAC package, the wizard remains on the **Select DAC Package**. Either select another valid DAC package or cancel the wizard and generate a new DAC package.  
  
  ### Review Policy Page  
 Review the results of evaluating the DAC server selection policy (if used). The DAC server selection policy is optional, and is assigned to the DAC when it is created in Visual Studio. The policy uses the server selection policy facets to specify conditions an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should meet to host the DAC.  
  
 **Evaluation results of policy conditions** - Shows whether the DAC deployment policy conditions succeeded. The results of evaluating each condition are reported on a separate line.  
  
 The following server selection policies always evaluate to false when deploying a DAC to [!INCLUDE[ssSDS](../../includes/sssds-md.md)]: operating system version, language, named pipes enabled, platform, and tcp enabled.  
  
 **Ignore policy violations** - Use this check box to proceed with the deployment if one or more of the policy conditions failed. Only select this option if you are sure that all of the conditions which failed will not prevent the successful operation of the DAC.  
   
### Update Configuration Page  
 Specify the names of the deployed DAC instance and the database created by the deployment, and to set database options.  
  
 **Database Name:** - Specify the name of the database to be created by the deployment. The default is the name of the source database the DAC was extracted from. The name must be unique within the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and comply with the rules for [!INCLUDE[ssDE](../../includes/ssde-md.md)] identifiers.  
  
 If you change the database name, the names of the data file and log files will change to match the new value.  
  
 The database name is also used as the name of the DAC instance. The instance name is displayed on the node for the DAC under the **Data-tier Applications** node in **Object Explorer**, or the **Deployed Data-tier Applications** node in the **Utility Explorer**.  
  
 The following options do not apply to [!INCLUDE[ssSDS](../../includes/sssds-md.md)], and are not displayed when deploying to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 **Use the default database location** - Select this option to create the database data and log files in the default location for the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The file names will be built using the database name.  
  
 **Specify database files** - Select this option to specify a different location or name for the data and log files.  
  
 **Data file path and name: -** Specify the full path and file name for the data file. The box is populated with the default path and file name. Edit the string in the box to change the default, or use the Browse button to navigate to the folder where the data file is to be placed.  
  
 **Log file path and name:** - Specify the full path and file name for the log file. The box is populated with the default path and file name. Edit the string in the box to change the default, or use the **Browse** button to navigate to the folder where the log file is to be placed.  
  
###  <a name="Summary"></a> Summary Page  
 Use this page to review the actions the wizard will take when deploying the DAC.  
  
 **The following settings will be used to deploy your DAC.** - Review the information displayed to ensure the actions taken will be correct. The window displays the DAC package you selected, and the name you selected for the deployed DAC instance. The window also displays the settings that will be used when creating the database associated with the DAC.  
   
### Deploy Page  
 This page reports the success or failure of the deploy operation.  
  
 **Deploying the DAC** - Reports the success or failure of each action taken to deploy the DAC. Review the information to determine the success or failure of each action. Any action that encountered an error will have a link in the **Result** column. Select the link to view a report of the error for that action.  
  
 **Save Report** - Select this button to save the deployment report to an HTML file. The file reports the status of each action, including all errors generated by any of the actions. The default folder is the SQL Server Management Studio\DAC Packages folder in the Documents folder of your Windows account.  
  
  
##  Using PowerShell  
  
1.  Create a SMO Server object and set it to the instance to which you want to deploy the DAC.  
  
2.  Open a **ServerConnection** object and connect to the same instance.  
  
3.  Use **System.IO.File** to load the DAC package file.  
  
4.  Use **add_DacActionStarted** and **add_DacActionFinished** to subscribe to the DAC deployment events.  
  
5.  Set the **DatabaseDeploymentProperties**.  
  
6.  Use the **DacStore.Install** method to deploy the DAC.  
  
7.  Close the file stream used to read the DAC package file.  

The following example deploys a DAC named MyApplication on a default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], using a DAC definition from a MyApplication.dacpac package.  
  
## PowerShell examples  

The following example deploys a DAC named MyApplication on a default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], using a DAC definition from a MyApplication.dacpac package.  

```powershell
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$server = Get-Item .  
  
## Open a Common.ServerConnection to the same instance.  
$serverConnection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($server.ConnectionContext.SqlConnectionObject)  
$serverConnection.Connect()  
$dacStore = New-Object Microsoft.SqlServer.Management.Dac.DacStore($serverConnection)  
  
## Load the DAC package file.  
$dacpacPath = "C:\MyDACs\MyApplication.dacpac"  
$fileStream = [System.IO.File]::Open($dacpacPath,[System.IO.FileMode]::OpenOrCreate)  
$dacType = [Microsoft.SqlServer.Management.Dac.DacType]::Load($fileStream)  
  
## Subscribe to the DAC deployment events.  
$dacStore.add_DacActionStarted({Write-Host `n`nStarting at $(Get-Date) :: $_.Description})  
$dacStore.add_DacActionFinished({Write-Host Completed at $(Get-Date) :: $_.Description})  
  
## Deploy the DAC and create the database.  
$dacName  = "MyApplication"  
$evaluateTSPolicy = $true  
$deployProperties = New-Object Microsoft.SqlServer.Management.Dac.DatabaseDeploymentProperties($serverConnection,$dacName)  
$dacStore.Install($dacType, $deployProperties, $evaluateTSPolicy)  
$fileStream.Close()  
```  
  
## More information 
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Extract a DAC From a Database](../../relational-databases/data-tier-applications/extract-a-dac-from-a-database.md)   
 [Database Identifiers](../../relational-databases/databases/database-identifiers.md)  
  
  
