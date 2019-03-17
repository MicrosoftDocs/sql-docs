---
title: "Reporting Services Report Server (SharePoint Mode) | Microsoft Docs"
ms.date: 09/26/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server-sharepoint


ms.topic: conceptual
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---

# Reporting Services Report Server (SharePoint Mode)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

  A Reporting Services report server configured for **SharePoint Mode** can run within a deployment of a SharePoint product. A report server in SharePoint mode can use the collaboration and management features of SharePoint for reports and other [!INCLUDE[ssRSnfoversion_md](../../includes/ssrsnoversion-md.md)] content types. SharePoint mode requires installing the appropriate version of the Reporting Services add-in for SharePoint products on your SharePoint Web Front Ends.  
  
> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

 For more information on installing and configuring, see the following:  
  
-   [Install Reporting Services SharePoint mode for SharePoint 2010](https://msdn.microsoft.com/47efa72e-1735-4387-8485-f8994fb08c8c).  
  
-   [Add an additional Report Server to a farm](../../reporting-services/install-windows/add-an-additional-report-server-to-a-farm-ssrs-scale-out.md).  
  
## Feature summary

 Configuring a report server to run in SharePoint integrated mode provides the following additional functionality that is only available when you deploy a report server in this mode:  
  
-   Use SharePoint document management and collaboration features, including alerts. A SharePoint site provides a unified portal for accessing and managing all report items in one place.  
  
-   Use SharePoint permissions and authentication providers to control access to reports, models, and other items.  
  
-   Use SharePoint deployment topologies to distribute reports over an Internet connection outside the firewall. A report server provides report and data processing services in the context of a larger SharePoint deployment that is configured for Internet access.  
  
-   Manage reports, models, data sources, schedules, and report history in custom application pages on a SharePoint site. You can set properties, define schedules and subscriptions, and create and manage report history on a SharePoint site the same way you create and manage them from other tools in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Publish or upload reports, report models, resources, and shared data source files to a SharePoint library, including Report Center in Office SharePoint Server.  
  
     Use Report Designer, Model Designer, and Report Builder to create reports and data sources to be published directly to a SharePoint library. You can also use the Upload action on a SharePoint site to add any report definitions and report models to a SharePoint library.  
  
     Because the report server processes report definitions in the same way regardless of the server mode you use, the report data and layout is unaffected by server mode. Any report that you can run in a native mode report server can run on a report server that is configured for SharePoint integrated mode.  
  
-   Subscribe to and deliver reports to a SharePoint library using a new SharePoint delivery extension. You can also deliver reports through e-mail or to a shared folder. The report server delivery extensions are used to deliver reports. You can create data-driven subscriptions for large-scale report distribution using subscriber data queried at run time.  
  
-   A Report Viewer web part you can add to SharePoint pages to view a report inside your SharePoint web application. The web part includes page navigation, search, print, and export features.  
  
-   Program against a new SOAP endpoint to create custom applications that integrate with a SharePoint site. You can also use the updated Windows Management Instrumentation (WMI) provider to programmatically configure a report server instance that runs in SharePoint integrated mode.  
  
-   Microsoft Access services reporting, in connected mode.  
  
-   AAM zones, internet facing deployments, and SharePoint user tokens for SharePoint lists.  
  
## Connected mode and local mode

 The SQL Server 2008 R2 release introduced a new *local mode* for viewing reports from a SharePoint 2010 server that has the Microsoft SQL Server 2008 R2 or later Reporting Services Add-In for SharePoint 2010 products installed.  
  
-   *Local Mode*: Local mode allows reports to be rendered locally from the SharePoint document library, without integration with a Reporting Services report server. The Reporting Services add-in for SharePoint products is required but a Reporting Services report server is not. The add-in can be installed several different ways, including the SharePoint 2010 products preparation tool. For more information on local mode, see [Local mode vs. connected mode reports in the Report Viewer](../../reporting-services/report-server-sharepoint/local-mode-vs-connected-mode-reports-in-the-report-viewer.md) and [Where to find the Reporting Services add-in for SharePoint products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
-   *Connected Mode*: Connected mode is supported by integrating a Reporting Services report server into the SharePoint farm using SharePoint Central Administration. The integration with a report server enables full end-to-end reporting, providing the collaboration features of SharePoint 2010 and the server based features of a report server including: Subscriptions, Snapshots, and server based processing.  
  
## Unsupported sharePoint features

 Not all SharePoint features are available for integrated operations. The following is a list of the SharePoint features Reporting Services does not directly integrate with:  
  
-   Secure Store Service.  
  
-   You cannot use the SharePoint Outlook Calendar integration or the SharePoint scheduling for reporting services files in a document library.  
  
-   SharePoint Business Data catalog.  
  
-   SharePoint personalization is also not supported on the Reporting Services pages. Report Server integration is not supported if the SharePoint Web application is enabled for Anonymous access.  
  
-   SQL Server Reporting Services does **not** support SharePoint document library version control. If you save report items in a document library that is configured with "Document Version History" enabled, Reporting Services features will not function correctly and generate errors in the ULS log. The following is an example of an error in the ULS log:  
  
    -   "...a data source associated with the report has been disabled".  
  
     Document library version history is configured on the "Versioning Settings" page of "Library Settings".  
  
## Supported combinations of the SharePoint add-in and report server

 Not all features are supported in all combinations of report server, Reporting Services add-in for SharePoint, and SharePoint Products. For more information, see [Supported combinations of SharePoint and Reporting Services Server and add-in](../../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md)  
  
> [!NOTE]  
>  The correct version of the Reporting Services add-in must be used with the corresponding version of SharePoint Products.  
  
## Components that provide integration

 To combine the servers in a single deployment, you integrate an installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Reporting Services with an instance of SharePoint products  
  
 Integration is provided through [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the Reporting Services Add-in for SharePoint Products. The Reporting Services Add-in is a freely distributable component that you can download and then install on a server that is running the appropriate version of SharePoint.  
  
> [!TIP]  
>  Not all features are supported in all combinations of report server, Reporting Services add-in for SharePoint, and SharePoint Products. For more information see, [Supported combinations of SharePoint and Reporting Services Server and add-in](../../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md).  
  
-   On SharePoint, the Reporting Services Add-in provides the ReportServer proxy endpoint, a Report Viewer web part, and application pages so that you can view, store, and manage report server content on a SharePoint site or farm.  
  
-   On Reporting Services provides updated program files, a SOAP endpoint, and custom security and delivery extensions. The report server must be configured to run in SharePoint integrated mode, dedicated exclusively to supporting report access and delivery through your SharePoint site.  
  
 After you install the Reporting Services Add-in on SharePoint and configure the two servers for integration, you can upload or publish report server content types to a SharePoint library, and then view and manage those documents from a SharePoint site. Uploading or publishing report server content is an important first step; the web part and pages become available when you select report definitions (.rdl), report models (.smdl) and shared data sources (.rsds) on a SharePoint site.  
  
##  Language considerations

 [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] and [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] products are available in many more languages than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
 When you configure a report server to run within a deployment of a SharePoint product, you might see a combination of languages. The user interface, documentation, and messages will appear in the following languages:  
  
-   All application pages, tools, errors, warnings, and messages that originate from Reporting Services will appear in the language used by the Reporting Services instance in one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] languages.  
  
-   Application pages that you open on a SharePoint site, the Report Viewer web part, and Report Builder will appear in one of the supported languages for the Reporting Services Add-in. To view the list of supported languages, go to [SQL Server downloads](https://msdn.microsoft.com/sql/downloads/) and find the download page for the SQL Server 2016 Reporting Services Add-in.  
  
-   SharePoint sites, SharePoint Central Administration, online help, and messages are available in the languages supported by Office Server products.  
  
 If the language of your SharePoint product or technology differs from the report server language, Reporting Services will try to use a language from the same language family that provides the closest match. If a close substitute is not available, the report server uses English.  
  
## Related tasks

 The following table summarizes tasks related to a Reporting Services SharePoint mode report server:  
  
|**Task**|**Link**|  
|--------------|--------------|  
|Detailed steps for installing and configuring Reporting Services in SharePoint mode.|[Install Reporting Services SharePoint mode for SharePoint 2010](https://msdn.microsoft.com/47efa72e-1735-4387-8485-f8994fb08c8c) and [Add an additional Report Server to a farm](../../reporting-services/install-windows/add-an-additional-report-server-to-a-farm-ssrs-scale-out.md).|  
|Scale-out your Reporting Services SharePoint deployment by adding additional report servers.|[Add an additional Report Server to a Farm](../../reporting-services/install-windows/add-an-additional-report-server-to-a-farm-ssrs-scale-out.md) and [Deployment topologies for SQL Server BI features in SharePoint](https://msdn.microsoft.com/library/39f76bc7-94e6-4dbc-bfa5-d56f4430bb26).|  
|Add additional SharePoint web front-ends that have the Reporting Services components installed for viewing and report items.|[Add an additional Reporting Services web front-end to a farm](../../reporting-services/install-windows/add-an-additional-reporting-services-web-front-end-to-a-farm.md)|  
|Configure e-mail for your report server within SharePoint.|[Configure e-mail for a Reporting Services service application](../install-windows/configure-e-mail-for-a-reporting-services-service-application.md)|
|Recent information for this release, found on the TechNet Wiki.|[SQL Server 2012 Reporting Services tips, tricks, and troubleshooting](https://go.microsoft.com/fwlink/?LinkId=221297).|  

## Next steps

[Install or uninstall the Reporting Services sdd-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)   
[Report Viewer web part on a SharePoint site](../../reporting-services/report-server-sharepoint/report-viewer-web-part-on-a-sharepoint-site.md)   
[Quiz: Configuring SSRS 2012 for SharePoint integration](https://go.microsoft.com/fwlink/?LinkId=306443)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
