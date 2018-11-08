---
title: "Reporting Services site collection features | Microsoft Docs"
ms.date: 09/25/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server-sharepoint


ms.topic: conceptual
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Reporting Services site collection features

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

Reporting Services SharePoint mode provides three SharePoint site collection features. The features support the general Reporting Services SharePoint mode reporting environment, [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)], a feature of the SQL Server 2016 Reporting Services Add-in, and management operations for Reporting Services in SharePoint Central Administration.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
## Site collection features

 The following table describes the Reporting Services site collection features.  
  
|Feature|Description|  
|-------------|-----------------|  
|**Report Server Central Administration Feature**|Enables Features for managing integration with a Reporting Services report server. This feature is only installed and used in the SharePoint Central Administration site collection.<br /><br /> The Report server integration feature is automatically activated in SharePoint Central Administration Site collection after you install the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] Add-in for SharePoint products. In some situations, you need to manually activate the feature. To activate the report server feature, use the Reporting Services pages in SharePoint Central Administration's Site Settings page.<br /><br /> The [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Reporting Services version and later of the Add-in for SharePoint products activate the report server integration feature for all existing site collections when the Add-in is installed. Additionally, the feature is automatically active for new site collections.|  
|**Report Server Integration Feature**|Enables rich reporting using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Reporting Services<br /><br /> This feature is Active by default.|  
|**Power View Integration Feature**|Enables interactive data exploration and visual presentation against [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] work books and Analysis services tabular databases.<br /><br /> The feature can be accessed by the context menus of the following data sources:<br /><br /> **.rdlx**<br /><br /> **.rsds**<br /><br /> **.bism** connection file<br /><br /> <br /><br /> If [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] does not appear in the context menus, verify the **Power View Integration Feature** is activated.<br /><br /> This feature is deactivated by default.|  

## Next steps

[Activate the Report Server and Power View Integration Features in SharePoint](../../reporting-services/report-server-sharepoint/site-collection-features-report-server-and-power-view.md)   
[Reporting Services Site Settings and Site Features&#40;SharePoint Mode&#41;](../../reporting-services/report-server-sharepoint/site-settings-and-features-reporting-services.md)   
[Activate the Report Server File Sync Feature in SharePoint Central Administration](../../reporting-services/report-server-sharepoint/activate-the-report-server-file-sync-feature-in-sharepoint-ca.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
