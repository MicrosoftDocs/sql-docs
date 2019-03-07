---
title: "PowerPivot Server Administration and Configuration in Central Administration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 2cdbfdc5-45a9-4000-a03d-318cc7ac8fe9
author: minewiskan
ms.author: owend
manager: craigg
---
# PowerPivot Server Administration and Configuration in Central Administration
  PowerPivot server administration and configuration is performed by SharePoint service application administrators, using SharePoint Central Administration.  
  
 PowerPivot for SharePoint must be configured before it can be used. After you install PowerPivot for SharePoint using SQL Server Setup, you can configure it using any of the following approaches:  
  
-   PowerPivot Configuration Tool or PowerPivot for SharePoint 2013 Configuration tool  
  
-   SharePoint Central Administration  
  
-   PowerShell cmdlets  
  
 All three approaches deliver a fully configured server.  
  
 This section includes tasks for configuring the software using Central Administration. At a minimum, you must perform all three of the required configuration tasks noted in the list below.  
  
> [!IMPORTANT]  
>  For SharePoint 2010, SharePoint 2010 Service Pack 1 (SP1) must be installed before you can configure either PowerPivot for SharePoint, or a SharePoint farm that uses a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database server. If you have not yet installed the service pack, do so now before you begin configuring the server.  
  
## Benefits of Configuring PowerPivot for SharePoint Using Central Administration  
 SharePoint Central Administration is the administrative application of a SharePoint farm. If you are farm administrator, you might prefer to use a familiar tool when adding a PowerPivot for SharePoint instance to your farm.  
  
 In contrast with the PowerPivot Configuration Tools or PowerShell cmdlets, Central Administration provides pages that fully specify all of the options you might set when configuring an application or server. Other approaches either condense the configuration workflow into a fewer number of steps, or require prior knowledge of how to configure a SharePoint server using PowerShell.  
  
## Related Content  
 [PowerPivot Configuration using Windows PowerShell](power-pivot-configuration-using-windows-powershell.md)  
  
 [PowerPivot Configuration Tools](power-pivot-configuration-tools.md)  
  
## Related Tasks  
  
|Link|Type|Task Description|  
|----------|----------|----------------------|  
|[Deploy PowerPivot Solutions to SharePoint](deploy-power-pivot-solutions-to-sharepoint.md)|Required|This step installs the solution files that add program files and application pages to the farm and to site collections.|  
|[Create and Configure a PowerPivot Service Application in Central Administration](create-and-configure-power-pivot-service-application-in-ca.md)|Required|This step provisions the PowerPivot System Service.|  
|[Activate PowerPivot Feature Integration for Site Collections in Central Administration](activate-power-pivot-integration-for-site-collections-in-ca.md)|Required|This step turns on PowerPivot features at the site collection level.|  
|[Add MSOLAP.5 as a Trusted Data Provider in Excel Services](add-msolap-5-as-a-trusted-data-provider-in-excel-services.md)|Required|This step adds the Analysis Services OLE DB provider as a trusted provider in Excel Services.|  
|[PowerPivot Data Refresh with SharePoint 2010](../powerpivot-data-refresh-with-sharepoint-2010.md)|Recommended|Data refresh is optional but recommended. It allows you to schedule unattended updates to the PowerPivot data in published Excel workbooks.|  
|[Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../configure-unattended-data-refresh-account-powerpivot-sharepoint.md)|Recommended|This step provisions a special-purpose account that can be used to run data refresh jobs on the server.|  
|[Configure Usage Data Collection for &#40;PowerPivot for SharePoint](configure-usage-data-collection-for-power-pivot-for-sharepoint.md)|Optional|Usage data collection is configured by default. You can use these steps to modify the default settings.|  
|[Configure Dedicated Data Refresh or Query-Only Processing &#40;PowerPivot for SharePoint&#41;](../configure-dedicated-data-refresh-query-only-processing-powerpivot-sharepoint.md)|Optional|A PowerPivot instance can be dedicated to just data refresh jobs or queries. In addition, you can modify default settings for parallel data refresh jobs.|  
|[Configure PowerPivot Service Accounts](configure-power-pivot-service-accounts.md)|Optional|Explains how to update passwords or change service accounts.|  
|[Connect a PowerPivot Service Application to a SharePoint Web Application in Central Administration](connect-power-pivot-service-app-to-sharepoint-web-app-in-ca.md)|Optional|Explains how to modify service associations.|  
|[Create a trusted location for PowerPivot sites in Central Administration](create-a-trusted-location-for-power-pivot-sites-in-central-administration.md)|Optional|Explains how to add the PowerPivot Gallery as a trusted location.|  
|[Configure and View SharePoint Log Files  and Diagnostic Logging &#40;PowerPivot for SharePoint&#41;](configure-and-view-sharepoint-and-diagnostic-logging.md)|Optional|Event logging is configured by default. You can use these steps to modify the default settings.|  
|[PowerPivot Health Rules - Configure](configure-power-pivot-health-rules.md)|Optional|Server health rules are configured by default. You can use these steps to modify some of the default settings.|  
|[Create and Customize PowerPivot Gallery](create-and-customize-power-pivot-gallery.md)|Optional|For installations that you are configuring manually, this procedure explains how to create a PowerPivot Gallery library that shows image thumbnails of the PowerPivot workbooks it contains.|  
|[Add a BI Semantic Model Connection Content Type to a Library &#40;PowerPivot for SharePoint&#41;](add-bi-semantic-model-connection-content-type-to-library.md)|Optional|Explains how to extend a document library to support the creation of BI semantic model connection files.|  
  
## See Also  
 [PowerPivot for SharePoint 2010 Installation](../../sql-server/install/powerpivot-for-sharepoint-2010-installation.md)   
 [Configuration Setting Reference &#40;PowerPivot for SharePoint&#41;](configuration-setting-reference-power-pivot-for-sharepoint.md)   
 [Disaster Recovery for PowerPivot for SharePoint](https://go.microsoft.com/fwlink/p/?LinkId=389570)  
  
  
