---
description: "Delete a Data-tier Application"
title: "Delete a Data-tier Application | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.deletedacwizard.deletedac.f1"
  - "sql13.swb.deletedacwizard.summary.f1"
  - "sql13.swb.deletedacwizard.introduction.f1"
  - "sql13.swb.deletedacwizard.choosemethod.f1"
helpviewer_keywords: 
  - "How to [DAC], delete"
  - "data-tier application [SQL Server], delete"
  - "wizard [DAC], delete"
  - "delete DAC"
ms.assetid: 16fe1c18-4486-424d-81d6-d276ed97482f
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Delete a Data-tier Application
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  You can delete a data-tier application by using either the Delete Data-tier Application wizard or a Windows PowerShell script. You can specify whether the associated database is retained, detached, or dropped.  
  
-   **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions), [Permissions](#Permissions)  
  
-   **To upgrade a DAC, using:**  [The Register Data-tier Application Wizard](#UsingDeleteDACWizard), [PowerShell](#DeleteDACPowerShell)  
  
## Before You Begin  
 When you delete a data-tier application (DAC) instance, you choose one of three options specifying what is to be done with the database associated with the data-tier application. All three options delete the DAC definition metadata. The options differ in what they do with the database associated with the data-tier application. The wizard does not delete any of the instance-level objects associated with the DAC or database, such as logins.  
  
|Option|Database actions|  
|------------|----------------------|  
|Delete registration|The associated database remains intact.|  
|Detach database|The associated database is detached. The instance of the Database Engine cannot reference the database, but the data and log files are intact.|  
|Delete database|The associated database is dropped. The data and log files are deleted.|  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 There is no automatic mechanism to restore the DAC definition metadata or the database after you delete a DAC. How you can manually rebuild the DAC instance depends on the delete option.  
  
|Option|How to Rebuild the DAC Instance|  
|------------|-------------------------------------|  
|Delete registration|Register a DAC from the database left in place.|  
|Detach database|Re-attach the database by using **sp_attachdb** or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then register a new DAC instance from the database.|  
|Delete database|Restore the database from a full backup made before the DAC was deleted, and then register a new DAC instance from the database.|  
  
> [!WARNING]  
>  Rebuilding a DAC instance by registering a DAC from a restored or re-attached database will not recreate some parts of the original DAC, such as the server selection policy.  
  
###  <a name="Permissions"></a> Permissions  
 A DAC can only be deleted by members of the **sysadmin** or **serveradmin** fixed server roles, or by the database owner. The built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account named **sa** can also launch the wizard.  
  
##  <a name="UsingDeleteDACWizard"></a> Using the Delete Data-tier Application Wizard  
 **To Delete a DAC Using a Wizard**  
  
1.  In **Object Explorer**, expand the node for the instance containing the DAC to be deleted.  
  
2.  Expand the **Management** node.  
  
3.  Expand the **Data-tier Applications** node.  
  
4.  Right-click the DAC to be deleted, and then select **Delete Data-tier Application...**  
  
5.  Complete the wizard dialogs:  
  
    1.  [Introduction](#Introduction)  
  
    2.  [Choose Method](#Choose_method)  
  
    3.  [Summary](#Summary)  
  
    4.  [Delete Data-tier Application](#Delete_datatier_application)  
  
##  <a name="Introduction"></a> Introduction Page  
 This page describes the steps for deleting a data-tier application.  
  
 **Do not show this page again.** - Click the check box to stop the page from being displayed in the future.  
  
 **Next >** - Proceeds to the **Choose Method** page.  
  
 **Cancel** - Ends the wizard without deleting a data-tier application or database.  
  
 [Using the Delete Data-tier Application Wizard](#UsingDeleteDACWizard)  
  
##  <a name="Choose_method"></a> Choose Method Page  
 Use this page to specify the option for handling the database associated with the DAC to be deleted.  
  
 **Delete registration** - Removes the metadata defining the data-tier application, but leaves the associated database intact.  
  
 **Detach database** - Removes the metadata defining the data-tier application and detaches the associated database.  
  
 The database can no longer be referenced by that instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], but the data and log files remain intact.  
  
 **Delete database** - Removes the metadata defining the DAC and drops the associated database.  
  
 The data and log files for the database are permanently deleted.  
  
 **< Previous** - Returns to the **Introduction** page.  
  
 **Next >** - Proceeds to the **Summary** page.  
  
 **Cancel** - Ends the wizard without deleting the DAC or database.  
  
 [Using the Delete Data-tier Application Wizard](#UsingDeleteDACWizard)  
  
##  <a name="Summary"></a> Summary Page  
 Use this page to review the actions the wizard will take when deleting the DAC instance.  
  
 **Review your selection summary** - Review the DAC, database, and deletion method displayed in the box. If the information is correct, select either **Next** or **Finish** to delete the DAC. If the DAC and database information is not correct, select **Cancel** and select the correct DAC. If the deletion method is not correct, select **Previous** to return to the **Choose Method** page and select a different method.  
  
 **< Previous** - Returns to the **Choose Method** page to choose a different delete method.  
  
 **Next >** - Deletes the DAC instance using the method you chose on the previous page, and proceeds to the **Delete Data-tier Application** page.  
  
 **Cancel** - Ends the wizard without deleting the DAC instance.  
  
 [Using the Delete Data-tier Application Wizard](#UsingDeleteDACWizard)  
  
##  <a name="Delete_datatier_application"></a> Delete Data-tier Application Page  
 This page reports the success or failure of the delete operation.  
  
 **Deleting the DAC** - Reports the success or failure of each action taken to delete the DAC instance. Review the information to determine the success or failure of each action. Any action that encountered an error will have a link in the **Result** column. Select the link to view a report of the error for that action.  
  
 **Save Report** - Select this button to save the deletion report to an HTML file. The file reports the status of each action, including all errors generated by any of the actions. The default folder is a SQL Server Management Studio\DAC Packages folder in the Documents folder of your Windows account..  
  
 **Finish** - Ends the wizard.  
  
 [Using the Delete Data-tier Application Wizard](#UsingDeleteDACWizard)  
  
##  <a name="DeleteDACPowerShell"></a> Using PowerShell  

1. Create a SMO Server object and set it to the instance that contains the DAC to be deleted.  
  
1. Open a **ServerConnection** object and connect to the same instance.  
  
1. Use `add_DacActionStarted` and `add_DacActionFinished` to subscribe to the DAC upgrade events.  
  
1. Specify the DAC to delete.  
  
1. Use one of three examples, depending on which delete option is appropriate:  
  
   - To delete the DAC registration and leave the database intact, use the `Unmanage` method.  
   - To delete the DAC registration and detach the database, use the `Uninstall` method and specify `DetachDatabase`.  
   - To delete the DAC registration and drop the database, use the `Uninstall` method and specify `DropDatabase`.
  
### Delete the DAC and leave the database

The following example deletes a DAC named `<myApplication>` using the `Unmanage` method to delete the DAC but leave the database intact.  
  
```powershell
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$server = Get-Item .  
  
## Open a Common.ServerConnection to the same instance.  
$serverConnection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($server.ConnectionContext.SqlConnectionObject)  
$serverConnection.Connect()  
$dacStore = New-Object Microsoft.SqlServer.Management.Dac.DacStore($serverConnection)  
  
## Subscribe to the DAC delete events.  
$dacStore.add_DacActionStarted({Write-Host `n`nStarting at $(Get-Date) :: $_.Description})  
$dacStore.add_DacActionFinished({Write-Host Completed at $(Get-Date) :: $_.Description})  
  
## Specify the DAC to delete.  
$dacName  = "<myApplication>"  
  
## Only delete the DAC definition from msdb, the associated database remains active.  
$dacStore.Unmanage($dacName)  
```
  
### Delete the DAC and detach the database

The following example deletes a DAC named `<myApplication>` using the `Uninstall` method to delete the DAC and detach the database.  
  
```powershell
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$server = Get-Item .  
  
## Open a Common.ServerConnection to the same instance.  
$serverConnection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($server.ConnectionContext.SqlConnectionObject)  
$serverConnection.Connect()  
$dacStore = New-Object Microsoft.SqlServer.Management.Dac.DacStore($serverConnection)  
  
## Subscribe to the DAC delete events.  
$dacStore.add_DacActionStarted({Write-Host `n`nStarting at $(Get-Date) :: $_.Description})  
$dacStore.add_DacActionFinished({Write-Host Completed at $(Get-Date) :: $_.Description})  
  
## Specify the DAC to delete.  
$dacName  = "<myApplication>"  
  
## Delete the DAC definition from msdb and detach the associated database.  
$dacStore.Uninstall($dacName, [Microsoft.SqlServer.Management.Dac.DacUninstallMode]::DetachDatabase)  
```
  
### Delete the DAC and drop the database

The following example deletes a DAC named `<myApplication>` using the `Uninstall` method to delete the DAC and drop the database.  
  
```powershell
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$server = Get-Item .  
  
## Open a Common.ServerConnection to the same instance.  
$serverConnection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($server.ConnectionContext.SqlConnectionObject)  
$serverConnection.Connect()  
$dacStore = New-Object Microsoft.SqlServer.Management.Dac.DacStore($serverConnection)  
  
## Subscribe to the DAC delete events.  
$dacStore.add_DacActionStarted({Write-Host `n`nStarting at $(Get-Date) :: $_.Description})  
$dacStore.add_DacActionFinished({Write-Host Completed at $(Get-Date) :: $_.Description})  
  
## Specify the DAC to delete.  
$dacName  = "<myApplication>"  
  
## Delete the DAC definition from msdb and drop the associated database.  
$dacStore.Uninstall($dacName, [Microsoft.SqlServer.Management.Dac.DacUninstallMode]::DropDatabase)  
```
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Deploy a Data-tier Application](../../relational-databases/data-tier-applications/deploy-a-data-tier-application.md)   
 [Register a Database As a DAC](../../relational-databases/data-tier-applications/register-a-database-as-a-dac.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)  
  
  
