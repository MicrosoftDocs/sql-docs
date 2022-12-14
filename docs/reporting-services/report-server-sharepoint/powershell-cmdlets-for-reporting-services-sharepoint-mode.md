---
title: "PowerShell cmdlets for Reporting Services SharePoint mode | Microsoft Docs"
description: PowerShell cmdlets are installed to support SQL Server Reporting Services SharePoint mode, including installation and feature and application management.
ms.date: 09/25/2017
ms.service: reporting-services
ms.subservice: report-server-sharepoint


ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---
# PowerShell cmdlets for Reporting Services SharePoint mode

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

When you install SQL Server 2016 Reporting Services SharePoint mode, PowerShell cmdlets are installed to support report Servers in SharePoint mode. The cmdlets cover three categories of functionality.  
  
-   Installation of the Reporting Services SharePoint shared service and proxy.  
  
-   Provisioning and management of Reporting Services service applications and associated proxies.  
  
-   Management of Reporting Services features, for example extensions and encryption keys.  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

## Cmdlet summary

 To run the cmdlets you need to open the SharePoint Management Shell. You can also use the graphical user interface editor that is included with Microsoft Windows, **Windows PowerShell Integrated Scripting Environment (ISE)**. For more information, see [Starting Windows PowerShell on Windows Server](/powershell/scripting/getting-started/starting-windows-powershell). In the following cmdlet summaries, the references to service application 'databases', refer to all of the databases created and used by a Reporting Services service application. This includes the configuration, alerting, and temp databases.  
  
 If you see an error message similar to the following when you type the PowerShell examples:  
  
-   Install-SPRSService : The term 'Install-SPRSService' is not recognized as the  
    name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is correct and try again.  
  
 One of the following issues is occurring:  
  
-   Reporting Services SharePoint mode is not installed and therefore the Reporting Services cmdlets are not installed.  
  
-   You ran the PowerShell command in Windows PowerShell or Windows PowerShell ISE instead of the SharePoint Management Shell. Use the SharePoint Management shell or add the SharePoint Snap-in to the Windows PowerShell window with the following command:  
  
    ```  
    Add-PSSnapin Microsoft.SharePoint.PowerShell  
    ```  
  
 For more information see [Use Windows PowerShell to administer SharePoint 2013](/powershell/module/sharepoint-server/).  
  
### Open the SharePoint Management Shell and run cmdlets
  
1.  Click the **Start** button  
  
2.  Click the **Microsoft SharePoint Products** group.  
  
3.  Click the **SharePoint Management Shell**.  
  
 To view command line help for a cmdlet use the PowerShell 'Get-Help' command at the PowerShell command prompt. For example:  
  
 `Get-Help Get-SPRSServiceApplicationServers`  
  
##  Shared service and proxy cmdlets

 The following table contains the PowerShell cmdlets for the Reporting Services SharePoint shared service.  
  
|Cmdlet|Description|  
|------------|-----------------|  
|Install-SPRSService|Installs and registers, or uninstalls, the Reporting Services shared service. This can be done only on the machine that has an installation of SQL Server Reporting Services in SharePoint mode. For installation, two operations occur:<br /><br /> -The Reporting Services service is installed in the farm.<br /><br /> -The Reporting Services service instance is installed to the current machine.<br /><br /> For Uninstallation, two operations occur:<br /><br /> -The Reporting Services service is uninstalled from the current machine.<br /><br /> -The Reporting Services service is uninstalled from the farm.<br /><br /> <br /><br /> If there are any other machines in the farm that have the Reporting Services service installed, or if there are still Reporting Services service applications running in the farm, a warning message is displayed.|  
|Install-SPRSServiceProxy|Installs and registers, or uninstalls, the Reporting Services service proxy in the SharePoint farm.|  
|Get-SPRSProxyUrl|Gets the URL(s) for accessing the Reporting Services service.|  
|Get-SPRSServiceApplicationServers|Gets all servers in the local SharePoint farm that contain an installation of the Reporting Services shared service. This cmdlet is useful for Reporting Services upgrades, to determine which servers run the shared service and therefore need to be upgraded.|  
  
## Service application and proxy cmdlets

 The following table contains the PowerShell cmdlets for Reporting Services service applications and their associated proxies.  
  
|cmdlet|Description|  
|------------|-----------------|  
|Get-SPRSServiceApplication|Gets one or more Reporting Services service application objects.|  
|New-SPRSServiceApplication|Create a new Reporting Services service application and associated databases.<br /><br /> LogonType Parameter: Specifies if the report server uses the SSRS Application Pool account or a SQL Server login to access the report server database. Valid values are:<br /><br /> 0 Windows Authentication<br /><br /> 1 SQL Server<br /><br /> 2 Application Pool Account (default)|  
|Remove-SPRSServiceApplication|Removes the specified Reporting Services service application. This will also remove the associated databases.|  
|Set-SPRSServiceApplication|Edits the properties of an existing Reporting Services service application.|  
|New-SPRSServiceApplicationProxy|Creates a new Reporting Services service application proxy.|  
|Get-SPRSServiceApplicationProxy|Gets one or more Reporting Services service application proxies.|  
|Dismount-SPRSDatabase|Dismounts the service application databases for a Reporting Services service application.|  
|Remove-SPRSDatabase|Remove the service application databases for a Reporting Services service application.|  
|Set-SPRSDatabase|Sets the properties of the databases associated to a Reporting Services service application.|  
|Mount-SPRSDatabase|Mounts databases for a Reporting Services service application.|  
|New-SPRSDatabase|Create new service application databases for the specified Reporting Services service application.|  
|Get-SPRSDatabaseCreationScript|Outputs the database creation script to the screen for a Reporting Services service application. You can then run the script in SQL Server Management Studio.|  
|Get-SPRSDatabase|Gets one or more Reporting Services service application databases. Use the command to get the ID of service application database so you can use the Set-SPRSDatabase comdlet to modify properties, for example the `querytimeout`. See the example in this topic, [Get and set Properties of the Reporting Service application database](#get-and-set-properties-of-the-reporting-service-application-database).|  
|Get-SPRSDatabaseRightsScript|Outputs the database rights script to the screen for a Reporting Services service application. It will prompt for desired user and database then returns transact SQL you can run to modify permissions. You can then run this script in SQL Server Management Studio.|  
|Get-SPRSDatabaseUpgradeScript|Outputs a database upgrade script to the screen. The script will upgrade Reporting Services service application databases to the database version of the current Reporting Services installation.|  
  
## Reporting Services custom functionality cmdlets
  
|Cmdlet|Description|  
|------------|-----------------|  
|Update-SPRSEncryptionKey|Updates the encryption key for the specified Reporting Services service application and re-encrypts its data.|  
|Restore-SPRSEncryptionKey|Restores a previously backed up encryption key for a Reporting Services service application.|  
|Remove-SPRSEncryptedData|Delete the encrypted data for the specified Reporting Services service application.|  
|Backup-SPRSEncryptionKey|Backs up the encryption key for the specified Reporting Services service application.|  
|New-SPRSExtension|Registers a new extension with a Reporting Services service application.|  
|Set-SPRSExtension|Sets the properties of an existing Reporting Services extension.|  
|Remove-SPRSExtension|Removes an extension from a Reporting Services service application.|  
|Get-SPRSExtension|Gets one or more Reporting Services extensions for a Reporting Services service application.<br /><br /> Valid values are:<br /><br /> <br /><br /> Delivery<br /><br /> DeliveryUI<br /><br /> Render<br /><br /> Data<br /><br /> Security<br /><br /> Authentication<br /><br /> EventProcessing<br /><br /> ReportItems<br /><br /> Designer<br /><br /> ReportItemDesigner<br /><br /> ReportItemConverter<br /><br /> ReportDefinitionCustomization|  
|Get-SPRSSite|Gets the SharePoint sites based on whether the "ReportingService" feature is enabled. By default, sites that enable the "ReportingService" feature are returned.|  
  
## Basic samples

 Return a list of cmdlets that contain 'SPRS' in the name. This will be the full list of Reporting Services cmdlets.  
  
```  
Get-command -noun *SPRS*  
```  
  
 Or with a little more detail, piped to a text file named commandlist.txt  
  
```  
Get-command -noun *SPRS* | Select name, definition | Format-List | Out-File c:\commandlist.txt  
```  
  
 Install the Reporting Services SharePoint service and service proxy.  
  
```  
Install-SPRSService  
```  
  
```  
Install-SPRSServiceProxy  
```  
  
 Start the Reporting Services service  
  
```  
get-spserviceinstance -all |where {$_.TypeName -like "SQL Server Reporting*"} | Start-SPServiceInstance  
```  
  
 Type the following command from the SharePoint Management Shell to return a filtered list of rows from the a log file. The command will filter for lines that contain "ssrscustomactionerror". This example is looking at the log file created when the rssharepoint.msi was installed.  
  
```  
Get-content -path C:\Users\testuser\AppData\Local\Temp\rs_sp_0.log | select-string "ssrscustomactionerror"  
```  
  
## Detailed samples

 In addition to the following samples, see the section "Windows PowerShell Script" in the topic [Windows PowerShell script for Steps 1-4](../../reporting-services/install-windows/install-the-first-report-server-in-sharepoint-mode.md#bkmk_full_script).  
  
### Create a reporting services service application and proxy

 This sample script completes the following tasks:  
  
1.  Create a Reporting Services service application and proxy. The script assumes the application pool "My App Pool" already exists.  
  
2.  Add the proxy to the default proxy group  
  
3.  Grant the service app access to the port 80 web app's content database. The script assumes site `https://sitename` already exists.  
  
```  
# Create service application and service application proxy  
$appPool = Get-SPServiceApplicationPool "My App Pool"  
$serviceApp = New-SPRSServiceApplication "My RS Service App" -ApplicationPool $appPool  
$serviceAppProxy = New-SPRSServiceApplicationProxy -Name "My RS Service App Proxy" -ServiceApplication $serviceApp  
  
# Add service application proxy to default proxy group.  Any web application that uses the default proxy group will now be able to use this service application.  
Get-SPServiceApplicationProxyGroup -default | Add-SPServiceApplicationProxyGroupMember -Member $serviceAppProxy  
  
# Grant application pool account access to the port 80 web application's content database.  
$webApp = Get-SPWebApplication "https://sitename"  
$appPoolAccountName = $appPool.ProcessAccount.LookupName()  
$webApp.GrantAccessToProcessIdentity($appPoolAccountName)  
  
```  
  
### Review and update a reporting services delivery extension

 The following PowerShell script example, updates the configuration for the report server e-mail delivery extension for the service application named `My RS Service App`. Update the values for the SMTP server (`<email server name>`) and the FROM email alias (`<your FROM email address>`).  
  
```  
$app=get-sprsserviceapplication -Name "My RS Service App"  
$emailCfg = Get-SPRSExtension -identity $app -ExtensionType "Delivery" -name "Report Server Email" | select -ExpandProperty ConfigurationXml   
$emailXml = [xml]$emailCfg   
$emailXml.SelectSingleNode("//SMTPServer").InnerText = "<email server name>"  
$emailXml.SelectSingleNode("//SendUsing").InnerText = "2"  
$emailXml.SelectSingleNode("//SMTPAuthenticate").InnerText = "2"  
$emailXml.SelectSingleNode("//From").InnerText = '<your FROM email address>'  
Set-SPRSExtension -identity $app -ExtensionType "Delivery" -name "Report Server Email" -ExtensionConfiguration $emailXml.OuterXml  
```  
  
 In the above example if you did not know the exact name of the service application, you could rewrite the first statement to get the service application based on a search of the partial name. For example:  
  
```  
$app=get-sprsserviceapplication | where {$_.name -like " ssrs_testapp *"}  
```  
  
 The following script will return the current configuration values for the report server e-mail delivery extension for the service application named "Reporting Services Application". The first step sets the value of the variable $app to the object of the service application that has a name of " My RS Service App "  
  
 The second statement will Get the 'Report Server Email' delivery extension for the service application object in variable $app, and select the configurationXML  
  
```  
$app=get-sprsserviceapplication -Name "Reporting Services Application"  
Get-SPRSExtension -identity $app -ExtensionType "Delivery" -name "Report Server Email" | select -ExpandProperty ConfigurationXml  
```  
  
 You can also rewrite the above two statements as one:  
  
```  
get-sprsserviceapplication -Name "Reporting Services Application" | Get-SPRSExtension -ExtensionType "Delivery" -name "Report Server Email" | select -ExpandProperty ConfigurationXml  
```  
  
### Get and set properties of the Reporting Service application database

 The following example first returns a list of the databases and properties so you can determine the database guid (ID) that you then supply to the set command. For a full list of the properties, use `Get-SPRSDatabase | format-list`.  
  
```  
get-SPRSDatabase | select id, querytimeout,connectiontimeout, status, server, ServiceInstance   
```  
  
 The following is an example of the output. Determine the ID for the database you want to modify and use the ID in the SET cmdlet.  
  
-   `Id                : 56f8d1bc-cb04-44cf-bd41-a873643c5a14`  
  
     `QueryTimeout      : 120`  
  
     `ConnectionTimeout : 15`  
  
     `Status            : Online`  
  
     `Server            : SPServer Name=uetestb01`  
  
     `ServiceInstance   : SPDatabaseServiceInstance`  
  
```  
Set-SPRSDatabase -identity 56f8d1bc-cb04-44cf-bd41-a873643c5a14 -QueryTimeout 300  
```  
  
 To verify the value is set, run the GET cmdlet again.  
  
```  
Get-SPRSDatabase -identity 56f8d1bc-cb04-44cf-bd41-a873643c5a14 | select id, querytimeout,connectiontimeout, status, server, ServiceInstance  
```  
  
### List Reporting Services data extensions

 The following example loops through each Reporting Services service application and lists the current data extensions for each.  
  
```  
$apps = Get-SPRSServiceApplication  
foreach ($app in $apps)   
{  
Write-host -ForegroundColor "yellow" Service App Name $app.Name  
Get-SPRSExtension -identity $app -ExtensionType "Data" | select name,extensiontype | Format-Table -AutoSize  
}  
```  
  
 **Example output:**  
  
-   `Name           ExtensionType`  
  
     `----           -------------`  
  
     `SQL                     Data`  
  
     `SQLAZURE                Data`  
  
     `SQLPDW                  Data`  
  
     `OLEDB                   Data`  
  
     `OLEDB-MD                Data`  
  
     `ORACLE                  Data`  
  
     `ODBC                    Data`  
  
     `XML                     Data`  
  
     `SHAREPOINTLIST          Data`  
  
### Change and list Reporting Services subscription owners

 See [Use PowerShell to change and list Reporting Services subscription owners and run a subscription](../../reporting-services/subscriptions/manage-subscription-owners-and-run-subscription-powershell.md).  
  
## Next steps

[Use PowerShell to change and list Reporting Services subscription owners and run a subscription](../../reporting-services/subscriptions/manage-subscription-owners-and-run-subscription-powershell.md)  
[CheckList: Use PowerShell to verify Power Pivot for SharePoint](/analysis-services/instances/install-windows/checklist-use-powershell-to-verify-power-pivot-for-sharepoint)   
[Get help SQL Server PowerShell](../../powershell/sql-server-powershell.md)   

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)