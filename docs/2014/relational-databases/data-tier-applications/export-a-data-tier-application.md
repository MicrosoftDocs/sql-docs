---
title: "Export a Data-tier Application | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.exportdac.settings.f1"
  - "sql12.swb. exportdac.settings.f1"
  - "sql12.swb.exportdac.welcome.f1"
  - "sql12.swb. exportdac.summary.f1"
  - "sql12.swb.exportdac.progress.f1"
  - "sql12.swb.exportdac.summary.f1"
  - "sql12.swb.exportdac.results.f1"
  - "sql12.swb. exportdac.results.f1"
helpviewer_keywords: 
  - "How to [DAC], export"
  - "wizard [DAC], export"
  - "export DAC"
  - "data-tier application [SQL Server], export"
ms.assetid: 61915bc5-0f5f-45ac-8cfe-3452bc185558
author: stevestein
ms.author: sstein
manager: craigg
---
# Export a Data-tier Application
  Exporting a deployed data-tier application (DAC) or database creates an export file that includes both the definitions of the objects in the database and all of the data contained in the tables. The export file can then be imported to another instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. The export-import operations can be combined to migrate a DAC between instances, to create a logical backup, or to create an on-premise copy of a database deployed in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
## Before You Begin  
 The export process builds a DAC export file in two stages.  
  
1.  The export builds a DAC definition in the export file - BACPAC file - in the same way a DAC extract builds a DAC definition in a DAC package file. The exported DAC definition includes all of the objects in the current database. If the export process is run against a database that was originally deployed from a DAC, and changes were made directly to the database after deployment, the exported definition matches the object set in the database, not what was defined in the original DAC.  
  
2.  The export bulk copies out the data from all of the tables in the database and incorporates the data into the export file.  
  
 The export process sets the DAC version to 1.0.0.0 and the DAC description in the export file to an empty string. If the database was deployed from a DAC, the DAC definition in the export file contains the name given to the original DAC, otherwise the DAC name is set to the database name.  
  
 
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 A DAC or database can only be exported from a database in [!INCLUDE[ssSDS](../../includes/sssds-md.md)], or [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later.  
  
 You cannot export a database that has objects that are not supported in a DAC, or contained users. For more information about the types of objects supported in a DAC, see [DAC Support For SQL Server Objects and Versions](dac-support-for-sql-server-objects-and-versions.md).  
  
###  <a name="Permissions"></a> Permissions  
 Exporting a DAC requires at least ALTER ANY LOGIN and database scope VIEW DEFINITION permissions, as well as SELECT permissions on **sys.sql_expression_dependencies**. Exporting a DAC can be done by members of the securityadmin fixed server role who are also members of the database_owner fixed database role in the database from which the DAC is exported. Members of the sysadmin fixed server role or the built-in SQL Server system administrator account named **sa** can also export a DAC.  
  
##  <a name="UsingDeployDACWizard"></a> Using the Export Data-tier Application Wizard  
 **To Export a DAC Using a Wizard**  
  
1.  Connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], whether on-premise or in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
2.  In **Object Explorer**, expand the node for the instance from which you want to export the DAC.  
  
3.  Right-click the database name.  
  
4.  Click **Tasks** and then select **Export Data-tier Application...**  
  
5.  Complete the wizard dialogs:  
  
    -   [Introduction Page](#Introduction)  
  
    -   [Export Settings Page](#Export_settings)  
  
    -   [Validation Page](#Validation)  
  
    -   [Summary Page](#Summary)  
  
    -   [Progress Page](#Progress)  
  
    -   [Results Page](#Results)  
  
##  <a name="Introduction"></a> Introduction Page  
 This page describes the steps for the Export Data-tier Application Wizard.  
  
 **Options**  
  
 **Do not show this page again.** - Click the check box to stop the Introduction page from being displayed in the future.  
  
 **Next** - Proceeds to the **Select DAC Package** page.  
  
 **Cancel** - Cancels the operation and closes the Wizard.  
  
##  <a name="Export_settings"></a> Export Settings Page  
 Use this page to specify the location where you want the BACPAC file to be created.  
  
-   **Save to local disk** - Creates a BACPAC file in a directory on the local computer. Click **Browse...** to navigate the local computer, or specify the path in the space provided. The path name must include a file name and the .bacpac extension.  
  
-   **Save to Windows Azure** - Creates a BACPAC file in a Windows Azure container. You must connect to a Windows Azure container in order to validate this option. Note that this option also requires that you specify a local directory for the temporary file. Note that the temporary file will be created at the specified location and will remain there after the operation completes.  
  
 To specify a subset of tables to export, use the **Advanced** option.  
  
##  <a name="Validation"></a> Validation Page  
 Use the validation page to review any issues that block the operation. To continue, resolve blocking issues and then click **Re-run Validation** to ensure that validation is successful.  
  
 To continue, click **Next**.  
  
##  <a name="Summary"></a> Summary Page  
 Use this page to review the specified source and target settings for the operation. To complete the export operation using the specified settings, click **Finish**. To cancel the export operation and exit the Wizard, click **Cancel**.  
  
##  <a name="Progress"></a> Progress Page  
 This page displays a progress bar that indicates the status of the operation. To view detailed status, click the **View details** option.  
  
##  <a name="Results"></a> Results Page  
 This page reports the success or failure of the export operation, showing the results of each action. Any action that encountered an error will have a link in the **Result** column. Click the link to view a report of the error for that action.  
  
 Click **Finish** to close the Wizard.  
  
##  <a name="NetApp"></a> Using a .Net Framework Application  
 **To export a DAC using the Export() method in a .Net Framework application.**  
  
 To view a code example, download the DAC sample application on [Codeplex](https://go.microsoft.com/fwlink/?LinkId=219575)  
  
1.  Create a SMO Server object and set it to the instance that contains the DAC to be exported.  
  
2.  Open a `ServerConnection` object and connect to the same instance.  
  
3.  Use the `Export` method of the `Microsoft.SqlServer.Management.Dac.DacStore` type to export the DAC. Specify the name of the DAC to be exported, and the path to the folder where the export file is to be placed.  
  
## See Also  
 [Data-tier Applications](data-tier-applications.md)   
 [Extract a DAC From a Database](extract-a-dac-from-a-database.md)  
  
  
