---
title: "Reporting Services site settings and site features (SharePoint mode) | Microsoft Docs"
ms.date: 09/25/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server-sharepoint


ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Reporting Services site settings and site features (SharePoint mode)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

Reporting Services SharePoint mode has several site level custom features and site feature that can be managed from the SharePoint Site Settings page. The settings are site wide and affect all Reporting Services service applications. You must have Content Manager and System Administrator permissions to view this page.  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

|Site Setting|Description|  
|------------------|-----------------|  
|Reporting Services Site Settings|Site-wide settings described in this topic.|  
|Manage Data Alerts|Management of the Data Alerting feature.|  
|Report Server File Sync|A Site level feature that is deactivated by default.<br /><br /> Synchronizes Report Server files (.rdl, .rsds, .smdl, .rsd, .rsc, .rdlx) from a SharePoint document library to the report server when files are added or updated in the directly within the document library.<br /><br /> For more information, see [Activate the Report Server File Sync Feature in SharePoint Central Administration](../../reporting-services/report-server-sharepoint/activate-the-report-server-file-sync-feature-in-sharepoint-ca.md)|  
  
## Open the Reporting Services site settings page
  
1.  From the SharePoint site's **Site Actions** menu, select **Site Settings**.  
  
2.  In the **Reporting Services** section, select **Reporting Services Site Settings**.  
  
## Options for Reporting Services site settings
  
|Option|Description|  
|------------|-----------------|  
|**Enable RSClientPrint ActiveX control download**|The control displays a custom print dialog box that supports features common to other print dialog boxes, including print preview, page selections for specifying specific pages and ranges, page margins, and orientation. For more information on the control, see [Using the RSClientPrint Control in Custom Applications](../../reporting-services/report-server-web-service/net-framework/using-the-rsclientprint-control-in-custom-applications.md)|  
|**Enable remote errors in local mode**|Show or hide detailed error messages on remote computers when running in local mode. If you see an error message similar to the following, then enabling remote errors may be useful:<br /><br /> `For more information about this error navigate to the report server on the local server machine or enable remote errors`|  
|**Enable accessibility metadata for reports**|Turn on accessibility metadata in the HTML output for reports|  
|**Enable Exact Data Visualization Fit Sizing for Reports**|Configure data visualization fit behavior when inside a tablix, to fix exactly. This includes chart, gauge, and map. When disabled the behavior is for data visualizations to fit approximately, which may leave some white space. This setting only applies to rendering in the Report Viewer web part. To manage this behavior for server-side rendering, you need to modify the **rsreportserver.config** file. For more information, see the following:<br /><br /> [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).<br /><br /> [Customize Rendering Extension Parameters in RSReportServer.Config](../../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md).<br /><br /> [HTML Device Information Settings](../../reporting-services/html-device-information-settings.md).<br /><br /> Enabling Exact may have performance impact because the processing to determine the exact size may take longer than an approximate fit.|  
  
## See also

 [Manage a Reporting Services SharePoint Service Application](../../reporting-services/report-server-sharepoint/manage-a-reporting-services-sharepoint-service-application.md)  
  
  
