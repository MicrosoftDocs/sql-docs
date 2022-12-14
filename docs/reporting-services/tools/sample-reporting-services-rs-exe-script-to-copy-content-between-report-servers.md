---
title: "Sample Reporting Services rs.exe Script to Copy Content between Report Servers | Microsoft Docs"
description: Learn how to copy content items and settings from one SQL Server Reporting Services report server to another by using the RS.exe utility to run a Reporting Services RSS script.
ms.date: 01/18/2022
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.assetid: d81bb03a-a89e-4fc1-a62b-886fb5338150
author: maggiesMSFT
ms.author: maggies
---
# Sample Reporting Services rs.exe Script to Copy Content between Report Servers

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2008r2-and-later](../../includes/ssrs-appliesto-2008r2-and-later.md)] [!INCLUDE [ssrs-appliesto-sharepoint-2013-2016](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE [ssrs-appliesto-pbirs](../../includes/ssrs-appliesto-pbirs.md)]

This article includes and describes a sample [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] RSS script that copies content items and settings from one [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server to another report server, using the **RS.exe** utility. RS.exe is installed with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], both native and SharePoint mode. The script copies [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] items, for example reports and subscriptions, from server to another server. The script supports both SharePoint mode and Native mode report servers.

> [!NOTE]  
>The open-source [ReportingServicesTools PowerShell module](https://www.powershellgallery.com/packages/ReportingServicesTools) is also available.

##  <a name="bkmk_download_script"></a> Download the ssrs_migration.rss Script  
 Download the script from the GitHub site [Reporting Services RS.exe migration script](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/reporting-services/ssrs-migration-rss) to a local folder. For more information, see the section [How to use the script](#bkmk_how_to_use_the_script) in this article.  
  
##  <a name="bkmk_supported_scenarios"></a> Supported Scenarios  
 The script supports both SharePoint mode and Native mode report servers. The script supports the report server versions [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] and later, and Power BI Report Server.  
  
The script can be used to copy content between report servers of the same mode or different modes. For example, you can run the script to copy content from a [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] native mode report server to a [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] SharePoint mode report server. You can run the script from any server where RS.exe is installed. For example, in the following deployment, you can:  
  
-   Run RS.exe and the script **ON** Server A.  
  
-   To copy content **FROM** Server B  
  
-   **TO** Server C  
  
|Server name|Report Server Mode|  
|-----------------|------------------------|  
|Server A|Native|  
|Server B|SharePoint|  
|Server C|SharePoint|  
  
 For more information on the RS.exe utility, see [RS.exe Utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md).  
  
###  <a name="bkmk_what_is_migrated"></a> Items and resources the script migrates  
 The script won't write over existing content items of the same name.  If the script detects items with the same name on the destination server that are on the source server, the individual items will result in a "failure" message and the script will continue. The following table lists the types of content and resources the script can migrate to target report server modes.  
  
|Item|Migrated|SharePoint|Description|  
|----------|--------------|----------------|-----------------|  
|Passwords|**No**|**No**|Passwords are **NOT** migrated. After content items are migrated, update the credential information on the destination server. For example, data sources with stored credentials.|  
|My Reports|**No**|**No**|The Native mode "My Reports" feature is based on individual user sign-ins therefore the scripting service does not have access to content in "My Reports" folders for users other than the **-u** parameter used to run the rss script. Also, "My Reports" isn't a feature of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode and items in the folders cannot be copied to a SharePoint environment. Therefore, the script does not copy report items that are in the "My Reports" folders on a source native mode report server<br /><br /> To migrate the content in "My Reports" folders with this script, complete the following steps:<br /><br /> 1.  Create new folder(s) in the web portal. Optionally, you can create folders or subfolders for each user.<br />2.  Sign in as one of the users with "My Reports" content.<br />3.  In the web portal, select the **My Reports** folder.<br />4.  Select the **Details** view for the folder.<br />5.  Select each report that you want to copy.<br />6.  Select **Move** in the web portal toolbar.<br />7.  Select the desired destination folder.<br />8.  Repeat steps 2-7 for each user.<br />9. Run the script.|  
|History|**No**|**No**||  
|History settings|Yes|Yes|The history settings are migrated however the history details are NOT migrated.|  
|Schedules|Yes|Yes|To migrate schedules, it is required that SQL Server Agent is running on the target server. If SQL Server Agent isn't running on the target, you will see an error message similar to this one:<br /><br /> `Migrating schedules: 1 items found. Migrating schedule: theMondaySchedule ... FAILURE:  The SQL Agent service isn't running. This operation requires the SQL Agent service. ---> Microsoft.ReportingServices.Diagnostics.Utilities.SchedulerNotResponding Exception: The SQL Agent service isn't running. This operation requires the SQL Agent service.`|  
|Roles and system policies|Yes|Yes|By default the script won't copy custom permission schema between servers. The default behavior is the items will be copied to the destination server with the 'inherit parent permissions' flag set to TRUE. If you want the script to copy permissions for individual items, use the SECURITY switch.<br /><br /> If the source and target servers are **not the same report server mode**, for example from native mode to SharePoint mode, and you use the SECURITY switch, the script will attempt to map default roles and groups based on the comparison in the article [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md). Custom roles and groups aren't copied to the destination server.<br /><br /> When the script is copying between servers **that are the same mode**, and you use the SECURITY switch, the script will create new roles (native mode) or groups (SharePoint mode) on the destination server.<br /><br /> If a role already exists on the destination server, the script will create a "Failure" message similar to the following, and continue migrating other items. After the script completes, verify that the roles on the destination server are configured to meet your needs. the Migrating roles: 8 items found.<br /><br /> `Migrating role: Browser ... FAILURE: The role 'Browser' already exists and cannot be created. ---> Microsoft.ReportingServices.Diagnostics.Utilities.RoleAlreadyExistsException: The role 'Browser' already exists and cannot be created.`<br /><br /> For more information, see [Grant User Access to a Report Server](../../reporting-services/security/grant-user-access-to-a-report-server.md)<br /><br /> **Note:** if a user that exists on the source server does not exist on the destination server, the script cannot apply role assignments on the destination server, the script cannot apply role assignments, even if the SECURITY switch is used.|  
|Shared data source|Yes|Yes|The script won't overwrite existing items on the target server. If an item on the target server already exists with the same name, you will see an error message similar to this one:<br /><br /> `Migrating DataSource: /Data Sources/Aworks2012_oltp ... FAILURE:The item '/Data Sources/Aworks2012_oltp' already exists. ---> Microsoft.ReportingServices.Diagnostics.Utilities.ItemAlreadyExistsException: The item '/Data Source s/Aworks2012_oltp' already exists.`<br /><br /> Credentials are **NOT** copied over as part of the data source. After content items are migrated, update the credential information on the destination server.|  
|Shared dataset|Yes|Yes|| 
|Folder|Yes|Yes|The script won't overwrite existing items on the target server. If an item on the target server already exists with the same name, you will see an error message similar to this one:<br /><br /> `Migrating Folder: /Reports ... FAILURE: The item '/Reports' already exists. ---> Microsoft.ReportingServices.Diagnostics.Utilities.ItemAlreadyExistsException: The item '/Reports' already exists.`|  
|Report|Yes|Yes|The script won't overwrite existing items on the target server. If an item on the target server already exists with the same name, you will see an error message similar to this one:<br /><br /> `Migrating Report: /Reports/testThe item '/Reports/test' already exists. ---> Microsoft.ReportingServices.Diagnostics.Utilities.ItemAlreadyExistsException: The item '/Reports/test' already exists.`|  
|Parameters|Yes|Yes||  
|Subscriptions|Yes|Yes||  
|History Settings|Yes|Yes|The history settings are migrated however the history details are NOT migrated.|  
|Processing options|Yes|Yes||  
|Cache refresh options|Yes|Yes|Dependent settings are migrated as part of a catalog item. The following is the sample out of the script as it migrates a report (.rdl) and related settings such as cache refresh options:<br /><br /> -   Migrating parameters for report TitleOnly.rdl 0 items found.<br />-   Migrating subscriptions for report TitleOnly.rdl: 1 items found.<br />-   Migrating subscription Save in \\\server\public\savedreports as TitleOnly ... SUCCESS<br />-   Migrating history settings for report TitleOnly.rdl ... SUCCESS<br />-   Migrating processing options for report TitleOnly.rdl ... 0 items found.<br />-   Migrating cache refresh options for report TitleOnly.rdl ... SUCCESS<br />-   Migrating cache refresh plans for report TitleOnly.rdl: 1 items found.<br />-   Migrating cache refresh plan titleonly_refresh735amM2F ... SUCCESS|  
|Cache refresh plans|Yes|Yes||  
|Images|Yes|Yes||  
|Report parts|Yes|Yes||  

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]
  
##  <a name="bkmk_required_permissions"></a> Required permissions  
 The permissions required to read or write items and resources isn't the same for all of the methods used in the script. The following table summarizes the methods used for each item or resource and links to related content. Navigate to the individual article to see the required permissions. For example the ListChildren method topic notes the required permissions of:  
  
-   **Native Mode Required Permissions:** ReadProperties on Item  
  
-   **SharePoint Mode Required Permissions:** ViewListItems  
  
|Item or Resource|Source|Target|  
|----------------------|------------|------------|  
|Catalog items|<xref:ReportService2010.ReportingService2010.ListChildren%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetProperties%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetItemDataSources%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetItemReferences%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetDataSourceContents%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetItemLink%2A>|<xref:ReportService2010.ReportingService2010.CreateCatalogItem%2A><br /><br /> <xref:ReportService2010.ReportingService2010.SetItemDataSources%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetItemReferences%2A><br /><br /> <xref:ReportService2010.ReportingService2010.CreateDataSource%2A><br /><br /> <xref:ReportService2010.ReportingService2010.CreateLinkedItem%2A><br /><br /> <xref:ReportService2010.ReportingService2010.CreateFolder%2A>|  
|Role|<xref:ReportService2010.ReportingService2010.ListRoles%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetRoleProperties%2A>|<xref:ReportService2010.ReportingService2010.CreateRole%2A>|  
|System Policy|<xref:ReportService2010.ReportingService2010.GetSystemPolicies%2A>|<xref:ReportService2010.ReportingService2010.SetSystemPolicies%2A>|  
|Schedule|<xref:ReportService2010.ReportingService2010.ListSchedules%2A>|<xref:ReportService2010.ReportingService2010.CreateSchedule%2A>|  
|Subscription|<xref:ReportService2010.ReportingService2010.ListSubscriptions%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetSubscriptionProperties%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetDataDrivenSubscriptionProperties%2A>|<xref:ReportService2010.ReportingService2010.CreateSubscription%2A><br /><br /> <xref:ReportService2010.ReportingService2010.CreateDataDrivenSubscription%2A>|  
|Cache refresh plan|<xref:ReportService2010.ReportingService2010.ListCacheRefreshPlans%2A><br /><br /> <xref:ReportService2010.ReportingService2010.GetCacheRefreshPlanProperties%2A>|<xref:ReportService2010.ReportingService2010.CreateCacheRefreshPlan%2A>|  
|Parameters|<xref:ReportService2010.ReportingService2010.GetItemParameters%2A>|<xref:ReportService2010.ReportingService2010.SetItemParameters%2A>|  
|Execution options|<xref:ReportService2010.ReportingService2010.GetExecutionOptions%2A>|<xref:ReportService2010.ReportingService2010.SetExecutionOptions%2A>|  
|Cache options|<xref:ReportService2010.ReportingService2010.GetCacheOptions%2A>|<xref:ReportService2010.ReportingService2010.SetCacheOptions%2A>|  
|History settings|<xref:ReportService2010.ReportingService2010.GetItemHistoryOptions%2A>|<xref:ReportService2010.ReportingService2010.SetItemHistoryOptions%2A>|  
|Item Policy|<xref:ReportService2010.ReportingService2010.GetPolicies%2A>|<xref:ReportService2010.ReportingService2010.SetPolicies%2A>|  
  
 For more information, see [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md).  
  
##  <a name="bkmk_how_to_use_the_script"></a> How to use the script  
  
1.  Download the script file to a local folder, for example **c:\rss\ssrs_migration.rss**.  
  
2.  Open a command prompt **with administrative privileges**.  
  
3.  Navigate to the folder containing the ssrs_migration.rss file.  
  
4.  Run the command with the parameters appropriate for your scenario.  
  
 **Basic Example, native mode report server to native mode report server:**  
  
 The following example migrates content from the native mode **Sourceserver** to the native mode **Targetserver**.  
  
 `rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://SourceServer/ReportServer -u Domain\User -p password -v ts="https://TargetServer/reportserver" -v tu="Domain\Userser" -v tp="password"`  
  
 **Usage notes:**  
  
-   The script runs in two steps.  
  
     The first step is an audit, to return a list of items that will be migrated and the second step is the migration process.  
  
     You can **cancel the script after step** one if you only want to see the possible migration list or you want to modify the parameters. Dependent settings aren't listed in step one. For example, the cache options of a report aren't listed but the report itself is.  
  
    > [!TIP]  
    > If you want to just audit a single server, use the same server for source and destination and cancel after step 1.  
  
     A good use of the step 1 audit information is to review existing roles on both the source and target Native mode server. The following is an example of the step one audit list. Notice the list includes a "roles" section because the switch-v security="True" was used:  
  
    -   `Retrieve and report the list of items that will be migrated. You can cancel the script after step 1 if you do not want to start the actual migration.`  
  
         `Retrieving roles:`  
  
         `Role: Browser`  
  
         `Role: Content Manager`  
  
         `Role: Model Item Browser`  
  
         `Retrieve and report the list of items that will be migrated. You can cancel the script after step 1 if you do not want to start the actual migration.`  
  
         `Retrieving roles:`  
  
         `Role: Browser`  
  
         `Role: Content Manager`  
  
         `Role: CustomRole`  
  
         `Role: Model Item Browser`  
  
         `Role: My Reports`  
  
         `Role: Publisher`  
  
         `Role: Report Builder`  
  
         `Role: System Administrator`  
  
         `Role: System User`  
  
         `Retrieving system policies:`  
  
         `Retrieving system policies:`  
  
         `System policy: BUILTIN\Administrators`  
  
         `System policy: domain\user1`  
  
         `System policy: domain\ueser2`  
  
         `Retrieving schedules:`  
  
         `Schedule: theMondaySchedule`  
  
         `Retrieving catalog items. This may take a while.`  
  
         `Folder: /Data Sources`  
  
         `DataSource: /Data Sources/Aworks2012_oltp`  
  
         `Folder: /images`  
  
         `Resource: /images/Boba Fett.png`  
  
         `Resource: /images/R2-D2.png`  
  
         `Folder: /Reports`  
  
         `Report: /Reports/products`  
  
         `Report: /Reports/test`  
  
         `Report: /Reports/TitleOnly`  
  
-   The SOURCE_URL and TARGET_URL must be valid report server URLs that point to the source and target [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server. In native mode, a report server URL looks like this URL:  
  
    -   `https://servername/reportserver`  
  
     In SharePoint mode the URL looks like this URL:  
  
    -   `https://servername/_vti_bin/reportserver`  
  
-   The virtual folder structure presented to the user in SharePoint might be different than the underlying one. Open `https://servername/_vti_bin/reportserver` or `https://servername/sites/site_name/_vti_bin/reportserver` in a browser to see the non-virtual folder structure. This is helpful for setting source folder and target folder to something other than "/", for a server in SharePoint mode.  
  
-   Passwords aren't migrated, and must be re-entered, for example data sources with stored credentials.  
  
##  <a name="bkmk_parameter_description"></a> Parameter description  
  
|Parameter|Description|Required|  
|---------------|-----------------|--------------|  
|**-s** Source_URL|URL of the source report server|Yes|  
|**-u** Domain\password **-p** password|Credentials for source server.|OPTIONAL, default credentials are used if missing|  
|**-v st**="SITE"||OPTIONAL. This parameter is only used for SharePoint mode report servers.|  
|**- v f**="SOURCEFOLDER"|Set to "/" for migrating everything, or to something like "/folder/subfolder" for partial migration. Everything within this folder will be copied|OPTIONAL, default is "/".|  
|**-v ts**="TARGET_URL"|'URL of the target RS server"||  
|**-v tu**="domain\username" **-v tp**="password"|'Credentials for target server.|OPTIONAL, default credentials are used if missing. **Note:** the user will be listed as the "creator" of shared schedules and "modified by" account for report items, in the target server.|  
|**-v tst**="SITE"||OPTIONAL. This parameter is only used for SharePoint mode report servers.|  
|**-v tf** ="TARGETFOLDER"|'Set to "/" for migrating into the root level. Set to "/folder/subfolder" to copy into a folder that already exists. Everything within "SOURCEFOLDER" will be copied into "TARGETFOLDER.|OPTIONAL, default is "/".|  
|**-v security**= "True/False"|If set to "False", destination catalog items will inherit security setting according to the settings of the target system. This setting is recommended for migrations between different report server types, for example native mode to SharePoint mode. If set to "True", the script attempts to migrate security settings.|OPTIONAL, default is "False".|  
  
##  <a name="bkmk_more_examples"></a> More examples  
  
###  <a name="bkmk_native_2_native"></a> Native mode report server to native mode report server  
 The following example migrates content from the native mode **Sourceserver** to the native mode **Targetserver**.  
  
```  
rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://SourceServer/ReportServer -u Domain\User -p password -v ts="https://TargetServer/reportserver" -v tu="Domain\Userser" -v tp="password"  
```  
  
 The following example adds the security switch:  
  
```  
rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://SourceServer/ReportServer -u Domain\User -p password -v ts="https://TargetServer/reportserver" -v tu="Domain\Userser" -v tp="password" -v security="True"  
```  
  
###  <a name="bkmk_native_2_sharepoint_root"></a> Native mode to SharePoint mode - root site  
 The following example migrates content from a native mode **SourceServer** to the "root site " on a SharePoint mode server **TargetServer**. The "Reports" and "Data Sources" folders on the native mode server are migrated as new libraries on the SharePoint deployment.  
  
 ![ssrs_rss_migrate_root_site](../../reporting-services/tools/media/ssrs-rss-migrate-root-site.gif "ssrs_rss_migrate_root_site")  
  
```  
rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://SourceServer/ReportServer -u Domain\User -p Password -v ts="https://TargetServer/_vti_bin/ReportServer" -v tu="Domain\User" -v tp="Password"  
```  
  
###  <a name="bkmk_native_2_sharepoint_with_site"></a> Native mode to SharePoint mode -'bi' site collection  
 The following example migrates content from a native mode server to a SharePoint server that contains a site collection of "sites/bi" and a shared documents library. The script creates folders in document the destination library. For example, the script will create a "Reports" and "Data Sources" folders in the target document library.  
  
```  
rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://SourceServer/ReportServer -u Domain\User -p Password -v ts="https://TargetServer/sites/bi/_vti_bin/reportserver" -v tst="sites/bi" -v tf="Shared Documents" -v tu="Domain\User" -v tp="Password"  
```  
  
###  <a name="bkmk_sharepoint_2_sharepoint"></a> SharePoint mode to SharePoint Mode -'bi' site collection  
 The following example migrates content:  
  
-   From a SharePoint server **SourceServer** that contains a site collection of "sites/bi" and a shared documents library.  
  
-   To a **TargetServer** SharePoint server that contains a site collection of "sites/bi" and a shared documents library.  
  
```  
rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://SourceServer/_vti_bin/reportserver -v st="sites/bi" -v f="Shared Documents" -u Domain\User1 -p Password -v ts="https://TargetServer/sites/bi/_vti_bin/reportserver" -v tst="sites/bi" -v tf="Shared Documents" -v tu="Domain\User" -v tp="Password"  
```  
  
###  <a name="bkmk_native_to_native_Azure_vm"></a> Native mode to native mode - Azure virtual machine  
 The following example migrates content:  
  
-   From a Native mode report server **SourceServer**.  
  
-   To a **TargetServer** Native mode report server running on an Azure virtual machine. The **TargetServer** isn't joined to the domain of the **SourceServer** and the **User2** is an administrator on the Azure virtual machine **TargetServer**.  
  
```  
rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://SourceServer/ReportServer -u Domain\user1 -p Password -v ts="https://ssrsnativeazure.cloudapp.net/ReportServer" -v tu="user2" -v tp="Password2"  
```  
  
> [!TIP]  
> For information on how to use Windows PowerShell to create [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report servers on Azure virtual machines, see [Use PowerShell to Create an Azure VM With a Native Mode Report Server](/azure/virtual-machines/windows/sqlclassic/virtual-machines-windows-classic-ps-sql-report).  
  
##  <a name="bkmk_sharepoint_site_to_native_Azure_vm"></a> SharePoint mode -'bi' site collection to a native mode server on an Azure virtual machine. 
 The following example migrates content:  
  
-   From a SharePoint mode report server **SourceServer** that contains a site collection of "sites/bi" and a shared documents library.  
  
-   To a **TargetServer** Native mode report server running on an Azure virtual machine. The **TargetServer** isn't joined to the domain of the **SourceServer** and the **User2** is an administrator on the Azure virtual machine **TargetServer**.  
  
```  
rs.exe -i ssrs_migration.rss -e Mgmt2010 -s https://uetesta02/_vti_bin/reportserver -u user1 -p Password -v ts="https://ssrsnativeazure.cloudapp.net/ReportServer" -v tu="user2" -v tp="Passowrd2"  
```  
  
##  <a name="bkmk_verification"></a> Verification  
 The section summarizes some of the steps to take on the destination server to verify content and policies were successfully migrated.  
  
### Schedules  
 To verify schedules on the target server:  
  
 **Native Mode**  
  
1.  Open the web portal on the destination server.  
  
2.  Select **Site Settings** on the top menu.  
  
3.  Select **Schedules** in the left pane.  
  
 **SharePoint mode:**  
  
1.  Browse to **Site settings**.  
  
2.  In the **Reporting Services** group, click **Manage Shared Schedules**.  
  
### Roles and groups  
 **Native Mode**  
  
1.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to your native mode report server.  
  
2.  In **Object Explorer** click **Security**.  
  
3.  Click **Roles**.  
  
##  <a name="bkmk_troubleshoot"></a> Troubleshooting  
 Use the trace flag **-t** to receive more information. For example, if you run the script and see a message similar to the following  
  
-   Could not connect to server: https://\<servername>/ReportServer/ReportService2010.asmx  
  
 Run the script again with the **-t** flag, to see a message similar to this one:  
  
-   System.Exception: Could not connect to server: https://\<servername>/ReportServer/ReportService2010.asmx ---> System.Net.WebException: **The request failed with HTTP status 401: Unauthorized**.   at System.Web.Services.Protocols.SoapHttpClientProtocol.ReadResponse(SoapClientMessage message, WebResponse response, Stream responseStream, Boolean asyncCall)   at System.Web.Services.Protocols.SoapHttpClientProtocol.Invoke(String methodName, Object[] parameters)   at Microsoft.SqlServer.ReportingServices2010.ReportingService2010.IsSSLRequired()   at Microsoft.ReportingServices.ScriptHost.Management2010Endpoint.PingService(String url, String userName, String password, String domain, Int32 timeout)   at Microsoft.ReportingServices.ScriptHost.ScriptHost.DetermineServerUrlSecurity()   --- End of inner exception stack trace ---  
  
## See also  
 [RS.exe Utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md)   
 [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)  
  
