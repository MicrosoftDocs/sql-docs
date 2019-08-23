---
title: "Register a Database As a DAC | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.registerdacwizard.registerdac.f1"
  - "sql12.swb.registerdacwizard.summary.f1"
  - "sql12.swb.registerdacwizard.introduction.f1"
  - "sql12.swb.registerdacwizard.setproperties.f1"
helpviewer_keywords: 
  - "wizard [DAC], register"
  - "How to [DAC], register"
  - "register DAC"
  - "data-tier application [SQL Server], register"
ms.assetid: 08e52aa6-12f3-41dd-a793-14b99a083fd5
author: stevestein
ms.author: sstein
manager: craigg
---
# Register a Database As a DAC
  Use either the **Register Data-tier Application Wizard** or a Windows PowerShell script to build a data-tier application (DAC) definition that describes the objects in an existing database, and register the DAC definition in the `msdb` system database (**master** in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]).  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To upgrade a DAC, using:**  [The Register Data-tier Application Wizard](#UsingRegisterDACWizard), [PowerShell](#RegisterDACPowerShell)  
  
## Before You Begin  
 The registration process creates a DAC definition that defines the objects in the database. The combination of the DAC definition and the database form a DAC instance. If you register a database as a DAC on a managed instance of the Database Engine, the registered DAC will be incorporated into the SQL Server Utility the next time the utility collection set is sent from the instance to the Utility Control Point. The DAC will then be present in the **Deployed Data-tier Applications** node of the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] **Utility Explorer** and reported in the **Deployed Data-tier Applications** details page.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 DAC registration can only be performed on a database in [!INCLUDE[ssSDS](../../includes/sssds-md.md)], or [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later. DAC registration cannot be performed if a DAC is already registered for the database. For example, if the database was created by deploying a DAC, you cannot run the **Register Data-tier Application Wizard**.  
  
 You cannot register a DAC if the database has objects that are not supported in a DAC, or contained users. For more information about the types of objects supported in a DAC, see [DAC Support For SQL Server Objects and Versions](dac-support-for-sql-server-objects-and-versions.md).  
  
###  <a name="Permissions"></a> Permissions  
 Registering a DAC in an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] requires at least ALTER ANY LOGIN and database scope VIEW DEFINITION permissions, SELECT permissions on **sys.sql_expression_dependencies**, and membership in the **dbcreator** fixed server role. Members of the **sysadmin** fixed server role or the built-in SQL Server system administrator account named **sa** can also register a DAC. Registering a DAC that does not contain logins in [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the **dbmanager** or **serveradmin** roles. Registering a DAC that contains logins in [!INCLUDE[ssSDS](../../includes/sssds-md.md)] requires membership in the **loginmanager** or **serveradmin** roles.  
  
##  <a name="UsingRegisterDACWizard"></a> Using the Register Data-tier Application Wizard  
 **To Register a DAC Using a Wizard**  
  
1.  In **Object Explorer**, expand the node for the instance containing the database to be registered as a DAC.  
  
2.  Expand the **Databases** node.  
  
3.  Right-click the database to be registered, point to **Tasks**, and then select **Register As Data-tier Application...**  
  
4.  Complete the wizard dialogs:  
  
    1.  [Introduction Page](#Introduction)  
  
    2.  [Set Properties Page](#Set_properties)  
  
    3.  [Validation and Summary Page](#Summary)  
  
    4.  [Register DAC Page](#Register)  
  
##  <a name="Introduction"></a> Introduction Page  
 This page describes the steps for registering a data-tier application.  
  
 **Do not show this page again.** - Click the check box to stop the page from being displayed in the future.  
  
 **Next >** - Proceeds to the **Set Properties** page.  
  
 **Cancel** - Terminates the wizard without registering a DAC.  
  
##  <a name="Set_properties"></a> Set Properties Page  
 Use this page to specify DAC-level properties such as the application name and version.  
  
 **Application name.** - A string that specifies the name used to identify the DAC definition, the field is been populated with the database name.  
  
 **Version.** - A numeric value that identifies the version of the DAC. The DAC version is used in Visual Studio to identify the version of the DAC that developers are working on. When deploying a DAC, the version is stored in the `msdb` database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **Description.** - Optional. Text that explains the purpose of the DAC. When deploying a DAC, the description is stored in the `msdb` database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
 **\< Previous** - Returns you to the **Introduction** page.  
  
 **Next >** - Verifies that a DAC can be built from the objects in the database, and displays the results in the **Validation and Summary** page.  
  
 **Cancel** - Terminates the wizard without registering the DAC.  
  
##  <a name="Summary"></a> Validation and Summary Page  
 Use this page to review the actions the wizard will take when registering the DAC. The page transitions through three states as it verifies that a DAC can be built from the objects in the database.  
  
### Retrieving Objects  
 **Retrieving database and server objects.** - Displays a progress bar as the wizard retrieves all of the required objects from the database and the instance of the Database Engine.  
  
 **\< Previous** - Returns you to the **Set Properties** page to change your entries.  
  
 **Next >** - Registers the DAC and displays the results in the **Register DAC** page.  
  
 **Cancel** - Terminates the wizard without registering the DAC.  
  
### Validating Objects  
 **Checking**  _SchemaName_ **.** _ObjectName_ **.** - Displays a progress bar as the wizard verifies the dependencies of the retrieved objects, and verifies that they are all valid objects for a DAC. _SchemaName_**.**_ObjectName_ identify which object is currently being verified.  
  
 **\< Previous** - Returns you to the **Set Properties** page to change your entries.  
  
 **Next >** - Registers the DAC and displays the results in the **Register DAC** page.  
  
 **Cancel** - Terminates the wizard without registering the DAC.  
  
### Summary  
 **The following setting will be used to register your DAC.** - Displays a report of the properties and objects that will be included in the DAC.  
  
 **Save Report** - Select this button to save a copy of the validation report to an HTML file. The default folder is a **SQL Server Management Studio\DAC Packages** folder in the Documents folder of your Windows account.  
  
 **\< Previous** - Returns you to the **Set Properties** page to change your entries.  
  
 **Next >** - Registers the DAC and displays the results in the **Register DAC** page.  
  
 **Cancel** - Terminates the wizard without registering the DAC.  
  
##  <a name="Register"></a> Register DAC Page  
 This page reports the success or failure of the registration.  
  
 **Registering the DAC** - Reports the success or failure of each action taken to register the DAC. Review the information to determine the success or failure of each action. Any action that encountered an error will have a link in the **Result** column. Select the link to view a report of the error for that action.  
  
 **Save Report** - Select this button to save the registration report to an HTML file. The file reports the status of each action, including all errors generated by any of the actions. The default folder is a **SQL Server Management Studio\DAC Packages** folder in the Documents folder of your Windows account. The file name is in the format \<DACPackageName>_RegisterDACReport_yyyymmdd.html, where \<*DACPackageName*> is the name of the package being deployed, *yyyy* = the current year, *mm* = the current month, and *dd* = the current day.  
  
 **Finish** - Terminates the wizard.  
  
##  <a name="RegisterDACPowerShell"></a> Register a DAC Using PowerShell  
 **To register a database as a DAC using the Register() method in a PowerShell script**  
  
1.  Create a SMO Server object and set it to the instance that contains the database to be registered as a DAC.  
  
2.  Add a variable that specifies the name of the database.  
  
3.  Specify the metadata for the DAC, such as the DAC name, version, and description.  
  
4.  Run the Register method with the information specified above.  
  
### Example (PowerShell)  
 The following example registers a database named MyDB as a DAC.  
  
```  
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$srv = get-item .  
  
## Specify the database to register as a DAC.  
$dbname = "MyDB"  
  
## Specify the DAC metadata.  
$applicationname = "MyApplication"  
$version = "1.0.0.0"  
$description = "This DAC defines the database used by my application."  
  
## Register the DAC.  
$registerunit = New-Object Microsoft.SqlServer.Management.Dac.DacExtractionUnit($srv, $dbname, $applicationname, $version)  
$registerunit.Description = $description  
$registerunit.Register()  
```  
  
## See Also  
 [Data-tier Applications](data-tier-applications.md)  
  
  
