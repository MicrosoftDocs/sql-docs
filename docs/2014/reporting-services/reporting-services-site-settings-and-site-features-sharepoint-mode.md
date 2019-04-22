---
title: "Reporting Services Site Settings and Site Features(SharePoint Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: e0040fec-e2b7-4099-ae01-3b9bb9128bbd
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Reporting Services Site Settings and Site Features(SharePoint Mode)
  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode has several site level custom features and site feature that can be managed from the SharePoint Site Settings page. The settings are site wide and affect all [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service applications. You must have Content Manager and System Administrator permissions to view this page.  
  
|Site Setting|Description|  
|------------------|-----------------|  
|[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Site Settings|Site wide settings described in this topic.|  
|Manage Data Alerts|Management of the Data Alerting feature.|  
|Report Server File Sync|A Site level feature that is deactivated by default.<br /><br /> Synchronizes Report Server files (.rdl, .rsds, .smdl, .rsd, .rsc, .rdlx) from a SharePoint document library to the report server when files are added or updated in the directly within the document library.<br /><br /> For more information, see [Activate the Report Server File Sync Feature in SharePoint Central Administration](../../2014/reporting-services/activate-report-server-file-sync-feature-sharepoint-central-administration.md)|  
  
## To open the Reporting Services Site Settings page  
  
1.  From the SharePoint site's **Site Actions** menu click **Site Settings**.  
  
2.  In the **Reporting Services** section, click **Reporting Services Site Settings**.  
  
## Options for Reporting Services Site Settings  
  
|Option|Description|  
|------------|-----------------|  
|**Enable RSClientPrint ActiveX control download**|The control displays a custom print dialog box that supports features common to other print dialog boxes, including print preview, page selections for specifying specific pages and ranges, page margins, and orientation. For more information on the control, see [Using the RSClientPrint Control in Custom Applications](report-server-web-service/net-framework/using-the-rsclientprint-control-in-custom-applications.md)|  
|**Enable remote errors in local mode**|Show or hide detailed error messages on remote computers when running in local mode. If you see an error message similar to the following, then enabling remote errors may be useful:<br /><br /> `For more information about this error navigate to the report server on the local server machine or enable remote errors`|  
|**Enable accessibility metadata for reports**|Turn on accessibility metadata in the HTML output for reports|  
|**Enable Exact Data Visualization Fit Sizing for Reports**|Configure data visualization fit behavior when inside a tablix, to fix exactly. This includes chart, gauge, and map. When disabled the behavior is for data visualizations to fit approximately, which may leave some white space. This setting only applies to rendering in the report viewer web part. To manage this behavior for server side rendering you need to modify the **rsreportserver.config** file. For more information, see the following:<br /><br /> [RSReportServer Configuration File](report-server/rsreportserver-config-configuration-file.md).<br /><br /> [Customize Rendering Extension Parameters in RSReportServer.Config](customize-rendering-extension-parameters-in-rsreportserver-config.md).<br /><br /> [HTML Device Information Settings](html-device-information-settings.md).<br /><br /> Enabling Exact may have performance impact because the processing to determine the exact size may take longer than an approximate fit.|  
  
## See Also  
 [Manage a Reporting Services SharePoint Service Application](../../2014/reporting-services/manage-a-reporting-services-sharepoint-service-application.md)  
  
  
