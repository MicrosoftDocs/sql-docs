---
title: "Deployment Checklist: Scale-out by adding PowerPivot Servers to a SharePoint 2010 farm | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 2dbddcc7-427a-4537-a8e2-56d99b9d967d
author: markingmyname
ms.author: maghan
manager: craigg
---
# Deployment Checklist: Scale-out by adding PowerPivot Servers to a SharePoint 2010 farm
  If you anticipate a high volume of requests for PowerPivot query processing in a SharePoint farm, you can add an extra PowerPivot for SharePoint instance to seamlessly add new query and data processing support.  
  
 After you install a new instance, you will have additional capacity for querying PowerPivot data or processing PowerPivot data refresh jobs. Optionally, you will have the choice of configuring each server to handle one type of request: query or data refresh.  
  
## Prerequisites  
 SharePoint Server 2010 is installed and configured.  
  
 SharePoint Server 2010 SP1 is applied and the farm is upgraded.  
  
 The existing PowerPivot for SharePoint instance in the farm is [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] (either a new installation or upgraded from SQL Server 2008 R2).  
  
 The computer on which you are installing the new [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] PowerPivot for SharePoint server is joined to the farm. The computer and the other servers in the farm must be in the same domain.  
  
 Review the following additional topics to understand system and version requirements:  
  
-   [Guidance for Using SQL Server BI Features in a SharePoint 2010 Farm](../../../2014/sql-server/install/guidance-for-using-sql-server-bi-features-in-a-sharepoint-2010-farm.md)  
  
> [!NOTE]  
>  In a multi-server farm, all PowerPivot for SharePoint instances must be at the same version. If you applied service packs or updates to other PowerPivot servers that are already in the farm, the new instance you are adding must be updated to the same version as the existing instance that is already in the farm. The new instance will be unavailable until the updates have been applied.  
  
## Steps  
 Use this checklist to add additional PowerPivot servers to a SharePoint farm. These instructions assume that you already have a PowerPivot for SharePoint server in the farm, and that you are adding a second server to handle additional processing load. Except for differences in installation requirements, post-install configuration, and verification, the steps for deploying a scale-out solution are identical to adding a single PowerPivot server to an existing farm.  
  
|Step|Link|  
|----------|----------|  
|Determine the service account of the Analysis Services instance that is already in the farm|Each additional instance you install must run under the same account as the first instance. Use either approach to determine the service account:<br /><br /> In Central Administration, in the Security section, click **Configure Service Accounts**. Select **Windows Service - SQL Server Analysis Services**. After you select the service, the service account name will appear in the page.<br /><br /> On a server that already has a PowerPivot service installation, open the **Services** console application in Administrative Tools. Double-click **SQL Server Analysis Services**. Click the **Log On** tab to view the service account.<br />**\*\* Important \*\*** Only use Central Administration to change service accounts. If you use another tool or approach, permissions will not be updated correctly in the farm.|  
|Run Setup to install a second instance of PowerPivot for SharePoint|[Install PowerPivot for SharePoint 2010](../../../2014/sql-server/install/install-powerpivot-for-sharepoint-2010.md)<br /><br /> Choose an application server that is joined to the farm, but does not have an existing PowerPivot instance on the server.<br /><br /> During Setup, when prompted to specify a service account, enter the account from the previous step. All instances of the Analysis Services service must run under the same domain account. This requirement enables the use of the managed accounts feature in SharePoint that lets you update the password in one place for all service instances of the same type.|  
|Configure the second instance|You can use either approach to configure the instance: [PowerPivot Configuration Tools](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-tools.md) or [PowerPivot Configuration using Windows PowerShell](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-using-windows-powershell.md)<br /><br /> When configuring a second instance, you only need to provision the local services. All other configuration tasks (such as creating service applications or configuring data refresh) are performed during the initial configuration, and used by subsequent instances that you install.|  
|Post-installation tasks|No further steps are specifically required. You do not need to create service applications, activate features, deploy solutions, or change service application identity. Existing Web applications and service applications will discover and use the new server software automatically.<br /><br /> Optionally, if you installed a second server for the purpose of using one server for queries and one for data refresh, you can configure server instance properties now to specify the type of requests handled by each server. For more information, see [Configure Dedicated Data Refresh or Query-Only Processing &#40;PowerPivot for SharePoint&#41;](../../analysis-services/configure-dedicated-data-refresh-query-only-processing-powerpivot-sharepoint.md).|  
|Verify installation of the second instance|You can use the following steps to verify PowerPivot query processing on the server you just installed.<br /><br /> 1) In Central Administration, open the Manage services on server page to confirm that the server and its services appear.<br />-In Server, click the down arrow, click Change Server, and then select the server that has the new PowerPivot for SharePoint installation.<br />-Verify that SQL Server Analysis Services and SQL Server PowerPivot System Service are started.<br /><br /> 2) In Central Administration, stop other PowerPivot for SharePoint servers so that the server you just installed is the only one available. For more information, see [Start or Stop a PowerPivot for SharePoint Server](../../analysis-services/power-pivot-sharepoint/start-or-stop-a-power-pivot-for-sharepoint-server.md).<br /><br /> 3) Click a PowerPivot workbook to open it from the library.<br /><br /> 4) Click a slicer or pivot the data to initiate a query. The server will load PowerPivot data in the background. In the next step, you will connect to the server to verify the data is loaded and cached.<br /><br /> 5) Start SQL Server Management Studio from the Microsoft SQL Server program group in the Start menu. If this tool is not installed on your server, you can skip to the last step to confirm the presence of cached files.<br /><br /> 6) In Server Type, select **Analysis Services**.<br /><br /> 7) In Server Name, enter **\<server-name>\powerpivot**, where **\<server-name>** is the name of the computer that has the new PowerPivot for SharePoint installation.<br /><br /> 8) Click **Connect**.<br /><br /> 9) In Object Explorer, click **Databases** to view the list of PowerPivot data files that are loaded.<br /><br /> 10) On the computer file system, check the following folder to determine whether files are cached to disk. The presence of cached files is further verification that your deployment is operational. To view the file cache, go to the \Program Files\Microsoft SQL Server\MSAS11.POWERPIVOT\OLAP\Backup folder.<br /><br /> 11) Restart the services that you stopped earlier.|  
  
## See Also  
 [Initial Configuration &#40;PowerPivot for SharePoint&#41;](../../../2014/sql-server/install/initial-configuration-powerpivot-for-sharepoint.md)   
 [PowerPivot for SharePoint 2010 Installation](../../../2014/sql-server/install/powerpivot-for-sharepoint-2010-installation.md)   
 [PowerPivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md)  
  
  
