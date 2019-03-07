---
title: "Validate a DAC Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "data-tier application [SQL Server], validate"
  - "data-tier application [SQL Server], compare"
  - "validate DAC"
  - "compare DACs"
  - "data-tier application [SQL Server], view"
  - "view DAC"
ms.assetid: 726ffcc2-9221-424a-8477-99e3f85f03bd
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Validate a DAC Package
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  It is a good practice to review the contents of a DAC package before deploying it in production, and to validate the upgrade actions before upgrading an existing DAC. This is especially true when deploying packages that were not developed in your organization.  
  
1.  **Before you begin:**  [Prerequisites](#Prerequisites)  
  
2.  **To upgrade a DAC, using:**  [View the Contents of a DAC](#ViewDACContents), [View Database Changes](#ViewDBChanges), [View Upgrade Actions](#ViewUpgradeActions), [Compare DACs](#CompareDACs)  
  
##  <a name="Prerequisites"></a> Prerequisites  
 We recommend that you do not deploy a DAC package from unknown or untrusted sources. Such DACs could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema. Before you use a DAC from an unknown or untrusted source, deploy it on an isolated test instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], run [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database, and also examine the code, such as stored procedures or other user-defined code, in the database.  
  
##  <a name="ViewDACContents"></a> View the Contents of a DAC  
 There are two mechanisms for viewing the contents of a data-tier application (DAC) package. You can import the DAC package to a DAC project in SQL Server Developer Tools. You can unpack the contents of the package to a folder.  
  
 **View a DAC in SQL Server Developer Tools**  
  
1.  Open the **File** menu, select **New**, and then select **Project...**.  
  
2.  Select the **SQL Server** project template, and specify a **Name**, **Location**, and **Solution name**.  
  
3.  In **Solution Explorer**, right click the project node and select **Properties...**.  
  
4.  On the **Project Settings** tab, in the **Output Types** section, select the **Data-tier Application (.dacpac File)** check box, and then close the properties dialog.  
  
5.  In **Solution Explorer**, right click the project node and select **Import Data-tier Application...**.  
  
6.  Use **Solution Explorer** to open all of the files in the DAC, such as the server selection policy and the pre- and post-deployment scripts.  
  
7.  Use the **Schema View** to review all of the objects in the schema, particularly reviewing the code in objects such as functions or stored procedures.  
  
 **View a DAC in a Folder**  
  
-   Unpack the DAC package into a folder by following the instructions in [Unpack a DAC Package](../../relational-databases/data-tier-applications/unpack-a-dac-package.md).  
  
-   View the contents of the [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts by opening them in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
-   View the contents of the text files in tools such as notepad.  
  
##  <a name="ViewDBChanges"></a> View Database Changes  
 After the current version of a DAC was deployed to production, changes may have been made directly to the associated database that might conflict with the schema defined in a new version of the DAC. Before upgrading to a new version of the DAC, check to see if such changes have been made to the database.  
  
 **View Database Changes by Using a Wizard**  
  
1.  Run the **Upgrade Data-tier Application** wizard, specifying the currently deployed DAC and the DAC package containing the new version of the DAC.  
  
2.  On the **Detect Change** page, review the report of the changes that have been made to the database.  
  
3.  Select **Cancel** if you do not want to continue with the upgrade.  
  
4.  For more information on using the wizard, see [Upgrade a Data-tier Application](../../relational-databases/data-tier-applications/upgrade-a-data-tier-application.md).  
  
 **View Database Changes by Using PowerShell**  
  
1.  Create a SMO Server object and set it to the instance that contains the DAC to be viewed.  
  
2.  Open a **ServerConnection** object and connect to the same instance.  
  
3.  Specify the DAC name in a variable.  
  
4.  Use the **GetDatabaseChanges()** method to retrieve a **ChangeResults** object, and pipe the object to a text file to generate a simple report of new, deleted, and changed objects.  
  
### View Database Changes Example (PowerShell)  
 **View Database Changes Example (PowerShell)**  
  
 The following example reports any database changes that have been made in a deployed DAC named MyApplicaiton.  
  
```  
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$srv = get-item .  
  
## Open a Common.ServerConnection to the same instance.  
$serverconnection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($srv.ConnectionContext.SqlConnectionObject)  
$serverconnection.Connect()  
$dacstore = New-Object Microsoft.SqlServer.Management.Dac.DacStore($serverconnection)  
  
## Specify the DAC instance name.  
$dacName  = "MyApplication"  
  
## Generate the change list and save to file.  
$dacChanges = $dacstore.GetDatabaseChanges($dacName) | Out-File -Filepath C:\DACScripts\MyApplicationChanges.txt  
```  
  
##  <a name="ViewUpgradeActions"></a> View Upgrade Actions  
 Before using a new version of a DAC package to upgrade a DAC that was deployed from an earlier DAC package, you can generate a report that contains the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that will be run during the upgrade, and then review the statements.  
  
 **Report Upgrade Actions by Using a Wizard**  
  
1.  Run the **Upgrade Data-tier Application** wizard, specifying the currently deployed DAC and the DAC package containing the new version of the DAC.  
  
2.  On the **Summary** page, review the report of the upgrade actions.  
  
3.  Select **Cancel** if you do not want to continue with the upgrade.  
  
4.  For more information on using the wizard, see [Upgrade a Data-tier Application](../../relational-databases/data-tier-applications/upgrade-a-data-tier-application.md).  
  
 **Report Upgrade Actions by Using PowerShell**  
  
1.  Create a SMO Server object and set it to the instance that contains the deployed DAC.  
  
2.  Open a **ServerConnection** object and connect to the same instance.  
  
3.  Use **System.IO.File** to load the DAC package file.  
  
4.  Specify the DAC name in a variable.  
  
5.  Use the **GetIncrementalUpgradeScript()** method to get a list of the Transact-SQL statements an upgrade would run, and pipe the list to a text file.  
  
6.  Close the file stream used to read the DAC package file.  
  
### View Upgrade Actions Example (PowerShell)  
 **View Upgrade Actions Example (PowerShell)**  
  
 The following example reports the Transact-SQL statements that would be run to upgrading a DAC named MyApplicaiton to the schema defined in a MyApplication2017.dacpac file.  
  
```  
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$srv = get-item .  
  
## Open a Common.ServerConnection to the same instance.  
$serverconnection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($srv.ConnectionContext.SqlConnectionObject)  
$serverconnection.Connect()  
$dacstore = New-Object Microsoft.SqlServer.Management.Dac.DacStore($serverconnection)  
  
## Load the DAC package file.  
$dacpacPath = "C:\MyDACs\MyApplication2017.dacpac"  
$fileStream = [System.IO.File]::Open($dacpacPath,[System.IO.FileMode]::OpenOrCreate)  
$dacType = [Microsoft.SqlServer.Management.Dac.DacType]::Load($fileStream)  
  
## Specify the DAC instance name.  
$dacName  = "MyApplication"  
  
## Generate the upgrade script and save to file.  
$dacstore.GetIncrementalUpgradeScript($dacName, $dacType) | Out-File -Filepath C:\DACScripts\MyApplicationUpgrade.sql  
  
## Close the filestream to the new DAC package.  
$fileStream.Close()  
```  
  
##  <a name="CompareDACs"></a> Compare DACs  
 Before upgrading a DAC, it is a good practice to review the differences in the database and instance-level objects between the current and new DACs. If you do not have a copy of the package for the current DAC, you can extract a package from the current database.  
  
 If you import both DAC packages into DAC projects in SQL Server Developer Tools, you can use the Schema Compare tool to analyze the differences between the two DACs.  
  
 Alternatively, unpack the DACs into separate folders. You can then use a difference tool, such as the WinDiff utility, to analyze the differences.  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Deploy a Data-tier Application](../../relational-databases/data-tier-applications/deploy-a-data-tier-application.md)   
 [Upgrade a Data-tier Application](../../relational-databases/data-tier-applications/upgrade-a-data-tier-application.md)  
  
  
