---
title: "Report Manager  (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], managing"
  - "Report Manager [Reporting Services], about Report Manager"
  - "customizing Report Manager"
  - "Report Manager [Reporting Services], customizing"
  - "report servers [Reporting Services], administering"
  - "browsing reports [Reporting Services]"
  - "administering reports"
  - "Report Manager [Reporting Services]"
  - "components [Reporting Services], Report Manager"
ms.assetid: 80949f9d-58f5-48e3-9342-9e9bf4e57896
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Report Manager  (SSRS Native Mode)
  Report Manager is a Web-based report access and management tool that you use to administer a single report server instance from a remote location over an HTTP connection. You can also use Report Manager for its report viewer and navigation features. In this topic:  
  
-   [What is Report Manager?](#bkmk_whatis_report_manager)  
  
-   [Start and Use Report Manager](#bkmk_start_report_manager)  
  
-   [Icon Descriptions](#bkmk_icon_descriptions)  
  
##  <a name="bkmk_whatis_report_manager"></a> What is Report Manager?  
 You can use Report Manager to perform the following tasks:  
  
-   View, search, print, and subscribe to reports.  
  
-   Create, secure, and maintain the folder hierarchy to organize items on the server.  
  
-   Configure role-based security that determines access to items and operations.  
  
-   Configure report execution properties, report history, and report parameters.  
  
-   Create report models that connect to and retrieve data from a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data source or from a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] relational data source.  
  
-   Set model item security to allow access to specific entities in the model, or map entities to predefined click through reports that you create in advance.  
  
-   Create shared schedules and shared data sources to make schedules and data source connections more manageable.  
  
-   Create data-driven subscriptions that roll out reports to a large recipient list.  
  
-   Create linked reports to reuse and repurpose an existing report in different ways.  
  
-   Launch Report Builder to create reports that you can save and run on the report server.  
  
 You can use Report Manager to browse the report server folders or search for specific reports. You can view a report, its general properties, and past copies of the report that are captured in report history. Depending on your permissions, you might also be able to subscribe to reports for delivery to an e-mail inbox or a shared folder on the file system.  
  
> [!NOTE]  
>  For information on supported browsers and versions, see [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../../2014/reporting-services/browser-support-for-reporting-services-and-power-view.md).  
  
 Report Manager is used only for a report server that runs in native mode. It is not supported for a report server that you configure for SharePoint integrated mode.  
  
 Some Report Manager features are only available in specified editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 On a new installation, only local administrators have sufficient permissions to work with content and settings. To grant permissions to other users, a local administrator must create role assignments that provide access to the report server. The application pages and tasks that a user can subsequently access will depend on the role assignments for that user. For more information, see [Grant User Access to a Report Server &#40;Report Manager&#41;](security/grant-user-access-to-a-report-server.md).  
  
 If you are using [!INCLUDE[wiprlhlong](../includes/wiprlhlong-md.md)] or Windows Server 2008, you must configure Report Manager for local administration. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
##  <a name="bkmk_start_report_manager"></a> Start and Use Report Manager  
 Report Manager is a Web application that you open by typing the Report Manager URL in the address bar of a browser window. When you start Report Manager, the pages, links, and options that you see will vary based on the permissions you have on the report server. To perform a task, you must be assigned to a role that includes the task. A user who is assigned to a role that has full permissions has access to the complete set of application menus and pages available for managing a report server. A user assigned to a role that has permissions to view and run reports sees only the menus and pages that support those activities. Each user can have different role assignments for different report servers, or even for the various reports and folders that are stored on a single report server.  
  
 For more information about roles, see [Granting Permissions on a Native Mode Report Server](security/granting-permissions-on-a-native-mode-report-server.md).  
  
> [!NOTE]  
>  If you are using Windows Vista or Windows Server 2008, you must configure the report server for local administration before you can use Report Manager to manage a local report server instance. For instructions on how to configure the server, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
## Start Report Manager  
  
#### To start Report Manager from a browser  
  
1.  Open [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Explorer 7.0 or later.  
  
2.  In the address bar of the Web browser, type the Report Manager URL.  
  
    -   By default, the URL is `http://[ComputerName]/reports`.  
  
    -   The report server might be configured to use a specific port. For example, `http:// [ComputerName]:80/reports` or `http:// [ComputerName]:8080/reports`.  
  
## Configuring Report Manager  
 Report Manager configuration consists of defining a URL for the application. Additional configuration is required if your deployment includes running Report Manager on a separate computer.  
  
 You can customize Report Manager in very limited ways. For example, you can modify the application title on the Site Settings page. If you are a Web developer, you can modify the style sheets that contain the style information used by Report Manager. Because Report Manager is not specifically designed to support customization, you must thoroughly test any modification that you make. If you find that Report Manager does not meet your needs, you can develop a custom report viewer or configure SharePoint Web parts to find and view reports in a SharePoint site. For more information, see [Configure Report Manager &#40;Native Mode&#41;](report-server/configure-web-portal.md).  
  
##  <a name="bkmk_icon_descriptions"></a> Icon Descriptions  
 The following table describes the icons that are used in Report Manager. For more information about the icons that appear in the report toolbar, see [HTML Viewer and the Report Toolbar](html-viewer-and-the-report-toolbar.md).  
  
|Icon|Description|Action|  
|----------|-----------------|------------|  
|![Report icon](media/hlp-16doc.gif "Report icon")|Report|Click the report icon or name to open the report. The report opens in a separate window.|  
|![Model icon](media/model-icon.gif "Model icon")|Report model|Click the report model icon to open model property pages.|  
|![Linked report icon](media/hlp-16linked.gif "Linked report icon")|Linked report|Click the report icon or name to open the linked report. The report opens in a separate window.|  
|![Folder icon](media/hlp-16folder.gif "Folder icon")|Folder|Click the folder icon or name to open the folder.|  
|![Subscription icon](media/hlp-16subscription.gif "Subscription icon")|Subscription|Click a subscription icon or description to edit a subscription.|  
|![Data-driven subscription icon](media/hlp-16subscriptiondd.gif "Data-driven subscription icon")|Data-driven subscription|Click a data-driven subscription icon or description to edit a subscription.|  
|![generic resource icon](media/hlp-16file.gif "generic resource icon")|Resource|Click the resource icon or name to open the resource. The resource opens in a separate window.|  
|![Shared data source icon](media/hlp-16datasource.png "Shared data source icon")|Shared data source item|Click a shared data source icon to open the property pages, report list, and subscription list of the data source.|  
|![Property Page icon](media/hlp-16prop.gif "Property Page icon")|Property page|Click the property icon to access additional pages to set properties and security.|  
  
## See Also  
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](install-windows/configure-a-url-ssrs-configuration-manager.md)   
 [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../../2014/reporting-services/browser-support-for-reporting-services-and-power-view.md)   
 [Report Builder &#40;SSRS&#41;](tools/report-builder-authoring-environment-ssrs.md)   
 [Reporting Services Tools](tools/reporting-services-tools.md)   
 [Report Server Content Management &#40;SSRS Native Mode&#41;](report-server/report-server-content-management-ssrs-native-mode.md)   
 [View and Explore Native Mode Reports Using SharePoint Web Parts &#40;SSRS&#41;](reports/view-and-explore-native-mode-reports-using-sharepoint-web-parts-ssrs.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
