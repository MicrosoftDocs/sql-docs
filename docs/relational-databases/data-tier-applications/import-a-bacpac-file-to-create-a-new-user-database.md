---
description: "Import a BACPAC File to Create a New User Database"
title: "Import a BACPAC file to create a new user database"
ms.date: 06/03/2020
ms.service: sql
ms.subservice:
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb. importdac.results.f1"
  - "sql13.swb.importdac.settings.f1"
  - "sql13.swb.importdac.storagebrowser.f1"
  - "sql13.swb.importdac.results.f1"
  - "sql13.swb.importdac.progress.f1"
  - "sql13.swb. importdac.summary.f1"
  - "sql13.swb.importdac.summary.f1"
  - "sql13.swb. importdac.progress.f1"
  - "sql13.swb.importdac.welcome.f1"
  - "sql13.swb. importdac.settings.f1"
helpviewer_keywords: 
  - "Data-tier application"
  - "SQL Server DAC"
  - "Migrate database"
  - "DAC"
ms.assetid: 736d8d9a-39f1-4bf8-b81f-2e56c134d12e
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-dt-2019
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Import a BACPAC File to Create a New User Database
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Import a data-tier application (DAC) file - a .bacpac file - to create a copy of the original database, with the data, on a new instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or to [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]. Export-import operations can be combined to migrate a DAC or database between instances, or to create a logical backup, such as creating an on-premises copy of a database deployed in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
## Before You Begin  
 The import process builds a new DAC in two stages.  
  
1.  The import creates a new DAC and associated database using the DAC definition stored in the export file, the same way a DAC deploy creates a new DAC from the definition in a DAC package file.  
  
2.  The import bulk copies in the data from the export file.  

## SQL Server Utility  
 If you import a DAC to an instance of the Database Engine, the imported DAC is incorporated into the SQL Server Utility the next time the utility collection set is sent from the instance to the utility control point. The DAC will then be present in the **Deployed Data-tier Applications** node of the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] **Utility Explorer** and reported in the **Deployed Data-tier Applications** details page.  
  
## Database Options and Settings  
 By default, the database created during the import will have all of the default settings from the CREATE DATABASE statement, except that the database collation and compatibility level are set to the values defined in the DAC export file. A DAC export file uses the values from the original database.  
  
 Some database options, such as TRUSTWORTHY, DB_CHAINING, and HONOR_BROKER_PRIORITY, cannot be adjusted as part of the import process. Physical properties, such as the number of filegroups, or the numbers and sizes of files cannot be altered as part of the import process. After the import completes, you can use the ALTER DATABASE statement, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell to tailor the database. For more information, see [Databases](../../relational-databases/databases/databases.md).  
  
## Limitations and restrictions  
 A DAC can be imported to [!INCLUDE[ssSDS](../../includes/sssds-md.md)], or an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later. If you export a DAC from a higher version, the DAC may contain objects not supported by [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. You cannot deploy those DACs to instances of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
## Prerequisites  
 We recommend that you do not import a DAC export file from unknown or untrusted sources. Such files could contain malicious code that might execute unintended Transact-SQL code or cause errors by modifying the schema. Before you use an export file from an unknown or untrusted source, unpack the DAC and examine the code, like stored procedures and other user-defined code. For more information about how to perform these checks, see [Validate a DAC Package](validate-a-dac-package.md).  
  
## Security  
 To improve security, SQL Server Authentication logins are stored in a DAC export file without a password. When the file is imported, the login is created as a disabled login with a generated password. To enable the logins, sign in using a login that has ALTER ANY LOGIN permission and use ALTER LOGIN to enable the login and assign a new password that can be communicated to the user. This is not needed for Windows Authentication logins because their passwords are not managed by SQL Server.  
  
## Permissions  
 A DAC can only be imported by members of the **sysadmin** or **serveradmin** fixed server roles, or by logins that are in the **dbcreator** fixed server role and have ALTER ANY LOGIN permissions. The built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account named **sa** can also import a DAC. Importing a DAC with logins to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the loginmanager or serveradmin roles. Importing a DAC without logins to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the dbmanager or serveradmin roles.  
  
## Using the Import Data-tier Application Wizard  
 **To launch the wizard, use the following steps:**  
  
1.  Connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], whether on-premises or in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
2.  In **Object Explorer**, right-click on **Databases**, and then select the **Import Data-tier Application** menu item to launch the wizard.  
  
3.  Complete the wizard dialogs:  
  
    -   [Introduction Page](#Introduction)  
  
    -   [Import Settings Page](#Import_settings)  
  
    -   [Database Settings Page](#Database_settings)  
  
    -   [Summary Page](#Summary)  
  
    -   [Progress Page](#Progress)  
  
    -   [Results Page](#Results)  
  
###  <a name="Introduction"></a> Introduction Page  
 This page describes the steps for the Data-tier Application Import Wizard.  
  
 **Options**  
  
-   **Do not show this page again.** - Click the check box to stop the Introduction page from being displayed in the future.  
  
-   **Next** - Proceeds to the **Import Settings** page.  
  
-   **Cancel** - Cancels the operation and closes the wizard.  
  
###  <a name="Import_settings"></a> Import Settings Page  
 Use this page to specify the location of the .bacpac file to import.  
  
-   **Import from local disk** - Click **Browse...** to navigate the local computer, or specify the path in the space provided. The path name must include a file name and the .bacpac extension.  
  
-   **Import from Azure** - Imports a BACPAC file from a Microsoft Azure container. You must connect to a Microsoft Azure container to validate this option. Note that the Import from Azure option also requires that you specify a local directory for the temporary file. The temporary file will be created at the specified location and will remain there after the operation completes.  
  
     When browsing Azure, you will be able to switch between containers within a single account. You must specify a single .bacpac file to continue the import operation. You can sort columns by **Name**, **Size**, or **Date Modified**.  
  
     To continue, specify the .bacpac file to import, and then click **Open**.  
  
###  <a name="Database_settings"></a> Database Settings Page  
 Use this page to specify details for the database that will be created.  
  
 **For a local instance of SQL Server:**  
  
-   **New database name** - Provide a name for the imported database.  
  
-   **Data file path** - Provide a local directory for data files. Click **Browse...** to navigate the local computer, or specify the path in the space provided.  
  
-   **Log file path** - Provide a local directory for log files. Click **Browse...** to navigate the local computer, or specify the path in the space provided.  
  
 To continue, click **Next**.  
  
 **For an Azure SQL Database:**  
  
 - **[Import a BACPAC file to create a new Azure SQL database](/azure/azure-sql/database/database-import)** provides step by step instructions using the Azure portal, PowerShell, SSMS, or SqlPackage.  
 - Consult **[SQL Database options and performance: Understand what's available in each service tier](/azure/azure-sql/database/purchasing-models)** for a detailed look at the different service tiers.  

### Validation Page  
 Use this page to review any issues that block the operation. To continue, resolve blocking issues and then click **Re-run Validation** to ensure that validation is successful.  
  
 To continue, click **Next**.  
  
###  <a name="Summary"></a> Summary Page  
 Use this page to review the specified source and target settings for the operation. To complete the import operation using the specified settings, click **Finish**. To cancel the import operation and exit the wizard, click **Cancel**.  
  
###  <a name="Progress"></a> Progress Page  
 This page displays a progress bar that indicates the status of the operation. To view detailed status, click the **View details** option.  
  
 To continue, click **Next**.  
  
###  <a name="Results"></a> Results Page  
 This page reports the success or failure of the import and create database operations, showing the success or failure of each action. Any action that encountered an error will have a link in the **Result** column. Click the link to view a report of the error for that action.  
  
 Click **Close** to close the wizard.  
  
## See Also  
[Import a BACPAC file to create a new Azure SQL database](/azure/azure-sql/database/database-import)  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Export a Data-tier Application](../../relational-databases/data-tier-applications/export-a-data-tier-application.md)  
  
