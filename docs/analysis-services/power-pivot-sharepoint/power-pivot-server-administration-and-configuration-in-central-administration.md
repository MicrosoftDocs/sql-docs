---
title: "Power Pivot Server Administration and Configuration in Central Administration | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Power Pivot Server Administration and Configuration in Central Administration
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] server administration and configuration is performed by SharePoint service application administrators, using SharePoint Central Administration.  
  
 [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] for SharePoint must be configured before it can be used. After you install [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] for SharePoint using SQL Server Setup, you can configure it using any of the following approaches:  
  
-   [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Configuration Tool or [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration tool  
  
-   SharePoint Central Administration  
  
-   PowerShell cmdlets  
  
 All three approaches deliver a fully configured server.  
  
 This section includes tasks for configuring the software using Central Administration. At a minimum, you must perform all three of the required configuration tasks noted in the list below.  
  
> [!IMPORTANT]  
>  For SharePoint 2010, SharePoint 2010 Service Pack 1 (SP1) must be installed before you can configure either [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] for SharePoint, or a SharePoint farm that uses a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database server. If you have not yet installed the service pack, do so now before you begin configuring the server.  
  
## Benefits of Configuring Power Pivot for SharePoint Using Central Administration  
 SharePoint Central Administration is the administrative application of a SharePoint farm. If you are farm administrator, you might prefer to use a familiar tool when adding a [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] for SharePoint instance to your farm.  
  
 In contrast with the [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Configuration Tools or PowerShell cmdlets, Central Administration provides pages that fully specify all of the options you might set when configuring an application or server. Other approaches either condense the configuration workflow into a fewer number of steps, or require prior knowledge of how to configure a SharePoint server using PowerShell.  
  
## Related Content  
 [Power Pivot Configuration using Windows PowerShell](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-using-windows-powershell.md)  
  
 [Power Pivot Configuration Tools](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-tools.md)  
  
## Related Tasks  
  
|Link|Type|Task Description|  
|----------|----------|----------------------|  
|[Deploy Power Pivot Solutions to SharePoint](../../analysis-services/power-pivot-sharepoint/deploy-power-pivot-solutions-to-sharepoint.md)|Required|This step installs the solution files that add program files and application pages to the farm and to site collections.|  
|[Create and Configure a Power Pivot Service Application in Central Administration](../../analysis-services/power-pivot-sharepoint/create-and-configure-power-pivot-service-application-in-ca.md)|Required|This step provisions the [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] System Service.|  
|[Activate Power Pivot Feature Integration for Site Collections in Central Administration](../../analysis-services/power-pivot-sharepoint/activate-power-pivot-integration-for-site-collections-in-ca.md)|Required|This step turns on [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] features at the site collection level.|  
|[Add MSOLAP.5 as a Trusted Data Provider in Excel Services](../../analysis-services/power-pivot-sharepoint/add-msolap-5-as-a-trusted-data-provider-in-excel-services.md)|Required|This step adds the Analysis Services OLE DB provider as a trusted provider in Excel Services.|  
|[Power Pivot Data Refresh with SharePoint 2010](http://msdn.microsoft.com/01b54e6f-66e5-485c-acaa-3f9aa53119c9)|Recommended|Data refresh is optional but recommended. It allows you to schedule unattended updates to the [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] data in published Excel workbooks.|  
|[Configure the Power Pivot Unattended Data Refresh Account (Power Pivot for SharePoint)](http://msdn.microsoft.com/81401eac-c619-4fad-ad3e-599e7a6f8493)|Recommended|This step provisions a special-purpose account that can be used to run data refresh jobs on the server.|  
|[Configure Usage Data Collection for &#40;Power Pivot for SharePoint](../../analysis-services/power-pivot-sharepoint/configure-usage-data-collection-for-power-pivot-for-sharepoint.md)|Optional|Usage data collection is configured by default. You can use these steps to modify the default settings.|  
|[Configure Dedicated Data Refresh or Query-Only Processing (Power Pivot for SharePoint)](http://msdn.microsoft.com/5e027605-1086-4941-bb01-f315df8f829b)|Optional|A [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] instance can be dedicated to just data refresh jobs or queries. In addition, you can modify default settings for parallel data refresh jobs.|  
|[Configure Power Pivot Service Accounts](../../analysis-services/power-pivot-sharepoint/configure-power-pivot-service-accounts.md)|Optional|Explains how to update passwords or change service accounts.|  
|[Connect a Power Pivot Service Application to a SharePoint Web Application in Central Administration](../../analysis-services/power-pivot-sharepoint/connect-power-pivot-service-app-to-sharepoint-web-app-in-ca.md)|Optional|Explains how to modify service associations.|  
|[Create a trusted location for Power Pivot sites in Central Administration](../../analysis-services/power-pivot-sharepoint/create-a-trusted-location-for-power-pivot-sites-in-central-administration.md)|Optional|Explains how to add the [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Gallery as a trusted location.|  
|[Configure and View SharePoint Log Files  and Diagnostic Logging &#40;Power Pivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/configure-and-view-sharepoint-and-diagnostic-logging.md)|Optional|Event logging is configured by default. You can use these steps to modify the default settings.|  
|[Configure Power Pivot Health Rules](../../analysis-services/power-pivot-sharepoint/configure-power-pivot-health-rules.md)|Optional|Server health rules are configured by default. You can use these steps to modify some of the default settings.|  
|[Create and Customize Power Pivot Gallery](../../analysis-services/power-pivot-sharepoint/create-and-customize-power-pivot-gallery.md)|Optional|For installations that you are configuring manually, this procedure explains how to create a [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] Gallery library that shows image thumbnails of the [!INCLUDE[ssGemini_md](../../includes/ssgemini-md.md)] workbooks it contains.|  
|[Add a BI Semantic Model Connection Content Type to a Library &#40;Power Pivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/add-bi-semantic-model-connection-content-type-to-library.md)|Optional|Explains how to extend a document library to support the creation of BI semantic model connection files.|  
  
## See Also  
 [Power Pivot for SharePoint 2010 Installation](http://msdn.microsoft.com/8d47dde7-c941-4280-a934-e2fe3f9a938f)   
 [Configuration Setting Reference &#40;Power Pivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/configuration-setting-reference-power-pivot-for-sharepoint.md)   
 [Disaster Recovery for Power Pivot for SharePoint](http://go.microsoft.com/fwlink/p/?LinkId=389570)  
  
  
