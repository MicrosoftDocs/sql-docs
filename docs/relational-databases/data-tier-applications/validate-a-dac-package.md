---
description: "Validate a DAC Package"
title: "Validate a DAC Package | Microsoft Docs"
ms.custom: ""
ms.date: 7/12/2022
ms.service: sql
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "data-tier application [SQL Server], validate"
  - "data-tier application [SQL Server], compare"
  - "validate DAC"
  - "compare DACs"
  - "data-tier application [SQL Server], view"
  - "view DAC"
ms.assetid: 726ffcc2-9221-424a-8477-99e3f85f03bd
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Validate a DAC package
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
It is a good practice to review the contents of a DAC package before deploying it in production, and to validate the upgrade actions before upgrading an existing DAC. This is especially true when deploying packages that were not developed in your organization.  

Methods to upgrade a DAC package include:
- [View the Contents of a DAC](#ViewDACContents)
- [View Database Changes](#ViewDBChanges)
- [View Upgrade Actions](#ViewUpgradeActions)
- [Compare DACs](#CompareDACs)

## Untrusted DAC packages  
We recommend that you do not deploy a DAC package from unknown or untrusted sources. Such DACs could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema. Before you use a DAC from an unknown or untrusted source, deploy it on an isolated test instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], run [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database, and also examine the code, such as stored procedures or other user-defined code, in the database.  
  
##  <a name="ViewDACContents"></a> View the Contents of a DAC  
There are two mechanisms for viewing the contents of a data-tier application (DAC) package:
- import the DAC package to a SQL project in SQL Server Developer Tools
- publish the DAC package to a test instance

### Import a DAC in SQL Server Developer Tools

1.  Open the **File** menu, select **New**, and then select **Project...**.  

2.  Select the **SQL Server** project template, and specify a **Name**, **Location**, and **Solution name**.

3.  In **Solution Explorer**, right click the project node and select **Properties...**.
  
4.  On the **Project Settings** tab, in the **Output Types** section, select the **Data-tier Application (.dacpac File)** check box, and then close the properties dialog.  

5.  In **Solution Explorer**, right click the project node and select **Import Data-tier Application...**.  

6.  Use **Solution Explorer** to open all of the files in the DAC, such as the server selection policy and the pre- and post-deployment scripts.  

7.  Use the **Schema View** to review all of the objects in the schema, particularly reviewing the code in objects such as functions or stored procedures.  
  
### Publish the DAC package to a test instance
Multiple tools are available to publish a DAC package to a test instance. The [SQL Server Dacpac extension](../../azure-data-studio/extensions/sql-server-dacpac-extension.md) for Azure Data Studio contains the **Data-tier Application Wizard**. We will walk through publishing a DAC package to a test instance.

1. If needed, deploy a test instance using the [deployment wizard](../../azure-data-studio/deploy-sql-container.md).

2. Connect to your test instance in Azure Data Studio and right-click on the server node.  Select **Data-tier application wizard** from the context menu.

3. On step 1 of the wizard, select **Deploy a data-tier application .dacpac file to an instance of SQL Server**

4. One step 2 of the wizard, input the file location and select **New Database** for target database.  Enter a database name.

5. On step 3 of the wizard, review the summary before selecting **Deploy**.

6. Once the deployment has completed, review the contents of the database in object explorer.

7. OPTIONAL: Right-click the database in object explorer and select **Create project from database** to generate a SQL project from the database.
  
##  <a name="ViewDBChanges"></a> View database changes
After the current version of a DAC was deployed to production, changes may have been made directly to the associated database that might conflict with the schema defined in a new version of the DAC. Before upgrading to a new version of the DAC, check to see if such changes have been made to the database.
Under several scenarios you may want to view the difference between a database and a DAC package. For example, changes may have been made directly to the associated database that might conflict with the schema defined in a new version of the DAC.

### All databases
**View database changes by using schema compare**
- Using the [Schema Compare extension in Azure Data Studio](../../azure-data-studio/extensions/schema-compare-extension.md#compare-schemas), the schema differences between an existing *.dacpac* and a database or two of the same can be viewed on Windows, macOS, and Linux.

- Using [SQL Server Data Tools in Visual Studio](../../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md), the schema differences between an existing *.dacpac* and a database or two of the same can be viewed on Windows.

**View database changes by using SqlPackage CLI**

The SqlPackage CLI can be used with the [DeployReport action](../../tools/sqlpackage/sqlpackage-deploy-drift-report.md) to view the differences between a *.dacpac* and a database through the actions that would be taken if the *.dacpac* were published to the database.

### Databases registered as a data-tier application
**View database changes by using a wizard**  
  
1.  Run the **Upgrade Data-tier Application** wizard, specifying the currently deployed DAC and the DAC package containing the new version of the DAC.  
  
2.  On the **Detect Change** page, review the report of the changes that have been made to the database.  
  
3.  Select **Cancel** if you do not want to continue with the upgrade.  
  
4.  For more information on using the wizard, see [Upgrade a Data-tier Application](../../relational-databases/data-tier-applications/upgrade-a-data-tier-application.md).  
  
**View database changes by using PowerShell**  

1.  Create a SMO Server object and set it to the instance that contains the DAC to be viewed.

2.  Open a **ServerConnection** object and connect to the same instance.

3.  Specify the DAC name in a variable.

4.  Use the **GetDatabaseChanges()** method to retrieve a **ChangeResults** object, and pipe the object to a text file to generate a simple report of new, deleted, and changed objects.  

The following example reports any database changes that have been made in a deployed DAC named MyApplication.

```powershell
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

**View database changes by using SqlPackage CLI**

The SqlPackage CLI can be used with the [DriftReport action](../../tools/sqlpackage/sqlpackage-deploy-drift-report.md) to view the changes that were made to a database since it was last registered.

##  <a name="ViewUpgradeActions"></a> View upgrade actions  

### All databases

**View database changes by using SQL project publish**
- Using the [SQL Database Projects extension in Azure Data Studio](../../azure-data-studio/extensions/sql-database-project-extension-build.md), the actions to be taken when a SQL project will be published to a database can be viewed on Windows, macOS, and Linux by selecting "Generate Script" during the publish process.

- Using [SQL Server Data Tools in Visual Studio](../../ssdt/how-to-change-target-platform-and-publish-a-database-project.md#to-publish-a-database-project), the actions to be taken when a SQL project will be published to a database can be viewed on Windows as a deployment script.

**View upgrade actions by using SqlPackage CLI**
The SqlPackage CLI can be used with the [DeployReport action](../../tools/sqlpackage/sqlpackage-deploy-drift-report.md) to view the differences between a *.dacpac* and a database through the actions that would be taken if the *.dacpac* were published to the database.

### Databases registered as a data-tier application
 Before using a new version of a DAC package to upgrade a DAC that was deployed from an earlier DAC package, you can generate a report that contains the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that will be run during the upgrade, and then review the statements.  
  
 **Report upgrade actions by using a wizard**  
  
1.  Run the **Upgrade Data-tier Application** wizard, specifying the currently deployed DAC and the DAC package containing the new version of the DAC.  
  
2.  On the **Summary** page, review the report of the upgrade actions.  
  
3.  Select **Cancel** if you do not want to continue with the upgrade.  
  
4.  For more information on using the wizard, see [Upgrade a Data-tier Application](../../relational-databases/data-tier-applications/upgrade-a-data-tier-application.md).  
  
 **Report upgrade actions by using PowerShell**  
  
1.  Create a SMO Server object and set it to the instance that contains the deployed DAC.  
  
2.  Open a **ServerConnection** object and connect to the same instance.  
  
3.  Use **System.IO.File** to load the DAC package file.  
  
4.  Specify the DAC name in a variable.  
  
5.  Use the **GetIncrementalUpgradeScript()** method to get a list of the Transact-SQL statements an upgrade would run, and pipe the list to a text file.  
  
6.  Close the file stream used to read the DAC package file.  

  
 The following example reports the Transact-SQL statements that would be run to upgrading a DAC named MyApplication to the schema defined in a MyApplication2017.dacpac file.  
  
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
  
