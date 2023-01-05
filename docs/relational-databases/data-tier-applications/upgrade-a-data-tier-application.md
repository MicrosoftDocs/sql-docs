---
description: "Upgrade a Data-tier Application"
title: "Upgrade a Data-tier Application | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice:
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.upgradedacwizard.summary.f1"
  - "sql13.swb.upgradedacwizard.reviewplan.f1"
  - "sql13.swb.upgradedacwizard.upgradedac.f1"
  - "sql13.swb.upgradedacwizard.selectpackage.f1"
  - "sql13.swb.upgradedacwizard.reviewpolicy.f1"
  - "sql13.swb.upgradedacwizard.selectoptions.f1"
  - "sql13.swb.upgradedacwizard.checkdrift.f1"
  - "sql13.swb.upgradedacwizard.introduction.f1"
helpviewer_keywords: 
  - "upgrade DAC"
  - "data-tier application [SQL Server], upgrade"
  - "wizard [DAC], upgrade"
  - "How to [DAC], upgrade"
ms.assetid: c117df94-f02b-403f-9383-ec5b3ac3763c
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Upgrade a Data-tier Application
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Use either the Upgrade Data-tier Application Wizard or a Windows PowerShell script to change the schema and properties of a currently deployed data-tier application (DAC) to match the schema and properties defined in a new version of the DAC.  
  
-   **Before you begin:**  [Choosing DAC Upgrade Options](#ChoseDACUpgOptions), [Limitations and Restrictions](#LimitationsRestrictions), [Prerequisites](#Prerequisites), [Security](#Security), [Permissions](#Permissions)  
  
-   **To upgrade a DAC, using:**  [The Upgrade Data-tier Application Wizard](#UsingDACUpgradeWizard), [PowerShell](#UpgradeDACPowerShell)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 A DAC upgrade is an in-place process that alters the schema of the existing database to match the schema defined in a new version of the DAC. The new version of the DAC is supplied in a DAC package file. For more information about creating a DAC package, see [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md).  
  
###  <a name="ChoseDACUpgOptions"></a> Choosing DAC Upgrade Options  
 There are four upgrade options for an in-place upgrade:  
  
-   **Ignore Data Loss** - If **True**, the upgrade will proceed even if some of the operations result in the loss of data. If **False**, these operations will terminate the upgrade. For example, if a table in the current database is not present in the schema of the new DAC, the table will be dropped if **True** is specified. The default setting is **True**.  
  
-   **Block on Changes** - If **True**, the upgrade is terminated if the database schema is different than that defined in the previous DAC. If **False**, the upgrade continues even if changes are detected. The default setting is **False**.  
  
-   **Rollback on Failure** - If **True**, the upgrade is enclosed in a transaction, and if errors are encountered a rollback will be attempted. If **False**, all changes are committed as they are made and if errors occur you may have to restore a previous backup of the database. The default setting is **False**.  
  
-   **Skip Policy Validation** - If **True**, the DAC server selection policy is not evaluated. If **False**, the policy is evaluated and the upgrade terminates if there is a validation error. The default setting is **False**.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 DAC uprades can only be performed in [!INCLUDE[ssSDS](../../includes/sssds-md.md)], or [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later.  
  
###  <a name="Prerequisites"></a> Prerequisites  
 It is prudent to take a full database backup before starting the upgrade. If an upgrade encounters an error and cannot roll back all of its changes, you may need to restore the backup.  
  
 Before starting the upgrade, there are several actions that you should take to validate the DAC package and the upgrade actions. For more information about how to perform these checks, see [Validate a DAC Package](../../relational-databases/data-tier-applications/validate-a-dac-package.md).  
  
-   We recommend that you do not upgrade by using a DAC package from unknown or untrusted sources. Such packages could contain malicious code that might execute unintended Transact-SQL code or cause errors by modifying the schema. Before you use a package from an unknown or untrusted source, unpack the DAC and examine the code, such as stored procedures or other user-defined code.  
  
-   If changes have been made to the current database after the last version of the DAC was deployed, some of the changes may prevent the successful completion of the upgrade, or be removed by the upgrade. You should first generate and review a report of any such changes made in the database.  
  
-   It is prudent to generate a list of the schema changes the upgrade will perform, and review the list for any problems.  
  
 The application name in the DAC package must match the application name of the currently deployed DAC. For example, if the current DAC has an application name of **GeneralLedger**, you can only upgrade by using a DAC package that also has an application name of **GeneralLedger**.  
  
 Ensure there is enough transaction log space available to log all of the modifications.  
  
###  <a name="Security"></a> Security  
 To improve security, SQL Server Authentication logins are stored in a DAC package without a password. When the package is deployed or upgraded, the login is created as a disabled login with a generated password. To enable the logins, log in using a login that has ALTER ANY LOGIN permission and use ALTER LOGIN to enable the login and assign a new password that can be communicated to the user. This is not needed for Windows Authentication logins because their passwords are not managed by SQL Server.  
  
####  <a name="Permissions"></a> Permissions  
 A DAC can only be upgraded by members of the **sysadmin** or **serveradmin** fixed server roles, or by logins that are in the **dbcreator** fixed server role and have ALTER ANY LOGIN permissions. The login must be the owner of the existing database. The built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account named **sa** can also upgrade a DAC.  
  
##  <a name="UsingDACUpgradeWizard"></a> Using the Upgrade Data-tier Application Wizard  
 **To Upgrade a DAC Using a Wizard**  
  
1.  In **Object Explorer**, expand the node for the instance containing the DAC to be upgraded.  
  
2.  Expand the **Management** node, and then expand the **Data-tier Applications** node.  
  
3.  Right-click the node for the DAC to be upgraded, and then select **Upgrade Data-tier Application...**  
  
4.  Complete the wizard dialogs:  
  
    1.  [Introduction Page](#Introduction)  
  
    2.  [Select Package Page](#Select_dac_package)  
  
    3.  [Review Policy Page](#Review_policy)  
  
    4.  [Detect Change Page](#Detect_change)  
  
    5.  [Review the Upgrade Plan](#ReviewUpgPlan)  
  
    6.  [Summary Page](#Summary)  
  
    7.  [Upgrade DAC Page](#Upgrade)  
  
##  <a name="Introduction"></a> Introduction Page  
 This page describes the steps for upgrading a data-tier application.  
  
 **Do not show this page again.** - Click the check box to stop the page from being displayed in the future.  
  
 **Next >** - Proceeds to the **Select Package** page.  
  
 **Cancel** - Terminates the wizard without upgrading the DAC.  
  
##  <a name="Select_dac_package"></a> Select Package Page  
 Use this page to specify the DAC package that contains the new version of the data-tier application. The page transitions through two states.  
  
### Select the DAC Package  
 Use the initial state of the page to choose the DAC package to deploy. The DAC package must be a valid DAC package file and must have a .dacpac extension. The DAC application name in the DAC package must be the same as the application name of the current DAC.  
  
 **DAC Package** - Specify the path and file name of the DAC package that contains the new version of the data-tier application. You can select the **Browse** button at the right of the box to browse to the location of the DAC package.  
  
 **Application Name** - A read-only box that displays the DAC application name assigned when the DAC was authored or extracted from a database.  
  
 **Version** - A read-only box that displays the version assigned when the DAC was authored or extracted from a database.  
  
 **Description** - A read-only box that displays the description written when the DAC was authored or extracted from a database.  
  
 **< Previous** - Returns to the **Introduction** page.  
  
 **Next >** - Displays a progress bar as the wizard confirms that the selected file is a valid DAC package.  
  
 **Cancel** - Terminates the wizard without upgrading the DAC.  
  
### Validating the DAC Package  
 Displays a progress bar as the wizard confirms that the selected file is a valid DAC package. If the DAC package is validated, the wizard proceeds to the **Review Policy** page. If the file is not a valid DAC package, the wizard remains on the **Select DAC Package** page. Either select another valid DAC package or cancel the wizard and generate a new DAC package.  
  
 **Validating the contents of the DAC** - The progress bar that reports the current status of the validation process.  
  
 **< Previous** - Returns to the initial state of the **Select Package** page.  
  
 **Next >** - Proceeds to the final version of the **Select Package** page.  
  
 **Cancel** - Terminates the wizard without deploying the DAC.  
  
##  <a name="Review_policy"></a> Review Policy Page  
 Use this page to review the results of evaluating the DAC server selection policy, if the DAC has a policy. The DAC server selection policy is optional, and is assigned to a DAC authored in Microsoft Visual Studio. The policy uses the server selection policy facets to specify conditions an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should meet to host the DAC.  
  
 **Evaluation results of policy conditions** - A read-only report showing whether the evaluations of the conditions in the DAC server selection policy succeeded. The results of evaluating each condition are reported on a separate line.  
  
 **Ignore policy violations** - Use this check box to proceed with the upgrade if one or more of the policy conditions failed. Only select this option if you are sure that all of the conditions which failed will not prevent the successful operation of the DAC.  
  
 **< Previous** - Returns to the **Select Package** page.  
  
 **Next >** - Proceeds to the **Detect Change** page.  
  
 **Cancel** - Terminates the wizard without upgrading the DAC.  
  
##  <a name="Detect_change"></a> Detect Change Page  
 Use this page reports the results of the wizards check for changes made to the database that make it's schema different than the schema definition stored in the DAC metadata in **msdb**. For example, if CREATE, ALTER, or DROP statements have been used to add, change, or remove objects from the database after the DAC was originally deployed. The page first displays a progress bar, and then reports the results of the analysis.  
  
 **Detecting change, this may take a few minutes** - Displays a progress bar as the wizard checks for differences between the current schema of the database and the objects in the DAC definition.  
  
 **Change detection results:** - Indicates that the analysis has completed and the results are reported below.  
  
 **The database DatabaseName has not changed** - The wizard detected no differences in the objects defined in the database and their counterparts in the DAC definition.  
  
 **The database DatabaseName has changed** - The wizard detected changes between the objects in the database and their counterparts in the DAC definition.  
  
 **Proceed despite possible loss of changes** - Specifies that you understand some of the objects or data in the current database will not be present in the new database, and that you are willing to proceed with the upgrade. You should select this button only if you have analyzed the change report and understand the steps you must perform to manually transfer any objects or data required in the new database. If you are not sure, click the **Save Report** button to save the change report, then click **Cancel**. Analyze the report, plan how to transfer any required objects and data after the upgrade completes, then restart the wizard.  
  
 **Save Report** - Click the button to save a report of the changes the wizard detected between the objects in the database and their counterparts in the DAC definition. You can then review the report to determine if you need to take actions after the upgrade completes to incorporate some or all of the objects listed in the report into the new database.  
  
 **< Previous** - Returns to the **Select DAC Package** page.  
  
 **Next >** - Proceeds to the **Options** page.  
  
 **Cancel** - Terminates the wizard without deploying the DAC.  
  
## Options Page  
 Use this page to select the rollback on failure option for the upgrade.  
  
 **Rollback on failure** - Select this option to enclose the upgrade in a transaction which the wizard can attempt to roll back if errors occur. For more information about the option, see [Choosing DAC Upgrade Options](#ChoseDACUpgOptions).  
  
 **Restore Defaults** - Returns the option to its default setting of false.  
  
 **< Previous** - Returns to the **Detect Change** page.  
  
 **Next >** - Proceeds to the **Review the Upgrade Plan** page.  
  
 **Cancel** - Terminates the wizard without deploying the DAC.  
  
##  <a name="ReviewUpgPlan"></a> Review the Upgrade Plan Page  
 Use this page to do review the actions that will be taken by the upgrade process. Only proceed when you are confident the upgrade will not create problems.  
  
 **The following actions will be used to upgrade the DAC.** - Review the information displayed to ensure the actions taken will be correct. The **Action** column displays the actions, such as Transact-SQL statements, that will be run to perform the upgrade. The **Data Loss** column will contain a warning if the associated action could delete data.  
  
 **Refresh** - refreshes the action list.  
  
 **Save Action Report** - saves the contents of the action window to an HTML file.  
  
 **Proceed despite possible loss of changes** - Specifies that you understand some of the objects or data in the current database will not be present in the new database, and that you are willing to proceed with the upgrade. You should select this button only if you have analyzed the change report and understand the steps you must perform to manually transfer any objects or data required in the new database. If you are not sure, click the **Save Action Report** button to save the change report and the **Save Scripts** button to save the Transact-SQL script, then click **Cancel**. Analyze the report and script, and then plan how to transfer any required objects and data after the upgrade completes, then restart the wizard.  
  
 **Save Scripts** - saves the Transact-SQL statements that will be used to perform the upgrade to a text file.  
  
 **Restore Defaults** - Returns the option to its default setting of false.  
  
 **< Previous** - Returns to the **Detect Change** page.  
  
 **Next >** - Proceeds to the **Summary** page.  
  
 **Cancel** - Terminates the wizard without deploying the DAC.  
  
##  <a name="Summary"></a> Summary Page  
 Use this page to do a final review of the actions the wizard will take when upgrading the DAC.  
  
 **The following settings will be used to upgrade your DAC.** - Review the information displayed to ensure the actions taken will be correct. The window displays the DAC you selected to be upgraded, and the DAC package containing the new version of the DAC. The window also displays whether the current version of the database is the same as the current DAC definition, or if the database has changed.  
  
 **< Previous** - Returns you to the **Review the Upgrade Plan** page.  
  
 **Next >** - Deploys the DAC and displays the results in the **Upgrade DAC** page.  
  
 **Cancel** - Terminates the wizard without deploying the DAC.  
  
##  <a name="Upgrade"></a> Upgrade DAC Page  
 This page reports the success or failure of the upgrade operation.  
  
 **Upgrading the DAC** - Reports the success or failure of each action taken to upgrade the DAC. Review the information to determine the success or failure of each action. Any action that encountered an error will have a link in the **Result** column. Select the link to view a report of the error for that action.  
  
 **Save Report** - Select this button to save the upgrade report to an HTML file. The file reports the status of each action, including all errors generated by any of the actions. The default folder is a SQL Server Management Studio\DAC Packages folder in the Documents folder of your Windows account.  
  
 **Finish** - Terminates the wizard.  
  
##  <a name="UpgradeDACPowerShell"></a> Using PowerShell  
 **To upgrade a DAC using the IncrementalUpgrade() method in a PowerShell script**  
  
1.  Create a SMO Server object and set it to the instance that contains the DAC to be upgraded.  
  
2.  Open a **ServerConnection** object and connect to the same instance.  
  
3.  Use **System.IO.File** to load the DAC package file.  
  
4.  Use **add_DacActionStarted** and **add_DacActionFinished** to subscribe to the DAC upgrade events.  
  
5.  Set the **DacUpgradeOptions**.  
  
6.  Use the **IncrementalUpgrade** method to upgrade the DAC.  
  
7.  Close the file stream used to read the DAC package file.  
  
### Example (PowerShell)  
 The following example upgrades a DAC named MyApplication on a default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], using a new DAC version in a MyApplication2017.dacpac package.  
  
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
  
## Subscribe to the DAC upgrade events.  
$dacstore.add_DacActionStarted({Write-Host `n`nStarting at $(get-date) :: $_.Description})  
$dacstore.add_DacActionFinished({Write-Host Completed at $(get-date) :: $_.Description})  
  
## Upgrade the DAC and close the package.  
$dacName  = "MyApplication"  
  
## Set the upgrade options.  
$upgradeProperties = New-Object Microsoft.SqlServer.Management.Dac.DacUpgradeOptions  
$upgradeProperties.blockonchanges = $true  
$upgradeProperties.ignoredataloss = $false  
$upgradeProperties.rollbackonfailure = $true  
$ upgradeProperties.skippolicyvalidation = $false  
  
## Upgrade the DAC  
$dacstore.IncrementalUpgrade($dacName, $dacType, $upgradeProperties)  
## Close the package file.  
$fileStream.Close()  
```  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [SQL Server PowerShell](../../powershell/sql-server-powershell.md)  
  
