---
title: "What&#39;s new in Reporting Services (SSRS) | Microsoft Docs"
ms.date: "03/16/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
  - "reporting-services-sharepoint"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
helpviewer_keywords: 
  - "what's new [Reporting Services]"
  - "Reporting Services, what's new"
  - "SQL Server Reporting Services, what's new"
  - "SSRS, what's new"
ms.assetid: bc909063-6b84-4b3a-80d2-e93fc04b4b9d
caps.latest.revision: 206
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# What&#39;s new in SQL Server Reporting Services (SSRS)
Learn about what's new in SQL Server [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. This covers the major feature areas and is updated as new items are released.
  
  For information on what's new in other areas of SQL Server, see [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md) or [What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md).
  
 **Download** ![download](../analysis-services/media/download.png "download")
 
- To download the January 2017 Technical Preview of Power BI reports in SQL Server Reporting Services, along with the Power BI Desktop release (SQL Server Reporting Services), go to the **[Microsoft download center](https://go.microsoft.com/fwlink/?linkid=839351)**.
  
-   To download [!INCLUDE[ssSQL15](../includes/sssql15-md.md)], go to  **[Evaluation Center](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016)**.  
  
-   Have an Azure account?  Then go **[Here](https://azure.microsoft.com/en-us/marketplace/partners/microsoft/sqlserver2016sp1enterprisewindowsserver2016/)** to spin up a Virtual Machine with [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] already installed.  
  
 ![note](../analysis-services/instances/install-windows/media/ssrs-fyi-note.png "note") For the current release notes, see [Technical Preview of Power BI reports in SSRS - Release notes](../reporting-services/reporting-services-release-notes.md) or  [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md).  

## Query designer support for DAX now in Report Builder and SQL Server Data Tools

In the latest releases of Report Builder and SQL Server Data Tools – Release Candidate, you can now create native DAX queries against supported SQL Server Analysis Services tabular data models. You can use the query designer in both tools to drag and drop the fields you want and have the DAX query generated for you instead of writing it yourself.  
 
Read more on the [Reporting Services blog](https://blogs.msdn.microsoft.com/sqlrsteamblog/2017/03/09/query-designer-support-for-dax-now-available-in-report-builder-and-sql-server-data-tools/).

* Download [SQL Server 2016 Report Builder](https://go.microsoft.com/fwlink/?LinkId=734968).
* Download [SQL Server Data Tools - Release Candidate](https://docs.microsoft.com/sql/ssdt/sql-server-data-tools-ssdt-release-candidate).

> **Note**: You can only use the query designer for DAX with SSAS tabular data sources built in SQL Server 2016+.

## What's new in the January 2017 Technical Preview of Power BI reports in SQL Server Reporting Services

### Reporting Services has a new setup experience

Reporting Services can now be installed outside of the SQL Server setup. The setup experience has fewer steps and is more lightweight. Setup will install the files for Reporting Services. You can then configure the database for your report server. This does require that you have access to a SQL Server Database Engine server in your environment.

![ssrs-standalone-setup1](../reporting-services/media/ssrs-standalone-setup1.png)

For more information, see [Install the January 2017 Technical Preview of Power BI reports in SQL Server Reporting Services](../reporting-services/install-windows/install-january-2017-technical-preview-power-bi-reports.md).

### Host Power BI reports within the web portal

You can now upload your Power BI Desktop files to Reporting Services and view reports directly in the web portal.

![ssrs-powerbi-report-config-render](../reporting-services/media/ssrs-powerbi-report-config-render.png)

You will need to download the Power BI Desktop release (SQL Server Reporting Services). Currently the preview only supports reports created in this version of Power BI Desktop. Power BI Desktop (SQL Server Reporting Services) can exist side by side with the Power BI Desktop for the Power BI service. To download Power BI Desktop (SQL Server Reporting services), go to the Microsoft download center. 

Currently, you are limited to creating a Power BI report against live connections to Analysis Services (tabular or multidimensional). We plan on adding support for other data sources and using Power Query features in a future preview.

For more information, see [Power BI reports in Reporting Services](../reporting-services/power-bi-reports-in-reporting-services.md).
 
## What's new in SQL Server 2016
  
### Reporting Services [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)]  
 A new [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] is available. This is an updated, modern, portal which incorporates KPIs, Mobile Reports, Paginated Reports, Excel and Power BI Desktop files. The [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] replaces Report Manager from previous releases. You can also download Mobile Report Publisher and Report Builder from the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] without the need of ClickOnce technology.
 
 To create Mobile Reports, you will need the [!INCLUDE[SS_MobileReptPub_Short](../includes/ss-mobilereptpub-short-md.md)].  
  
 For more information about the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)], see [Web portal (SSRS Native Mode)](../reporting-services/web-portal-ssrs-native-mode.md).  
  
 ![ssRSPortal](../reporting-services/media/ssrsportal.png "ssRSPortal")  
 
 #### Custom branding for the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] 
  You can customize the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] with your organization's logo and colors by using a branding pack.  
  
  For more information about custom branding, see [Branding the web portal](http://msdn.microsoft.com/en-us/6dac97f7-02a6-4711-81a3-e850a6b40bf1)
 
 #### Key performance indicators (KPI) in the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] 

You can create KPIs direct in the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] that are contextual to the folder you are in. When creating KPIs, you can choose dataset fields and summarize those values. You can also select related content to drill-through to more details.
  
 ![ssrs-webportal-kpi](../reporting-services/media/ssrs-webportal-kpi.png)
 
 For more information, see [Working with KPIs in the web portal](http://msdn.microsoft.com/en-us/a28cf500-6d47-4268-a248-04837e7a09eb)
  
 
 ### Mobile Reports
 
Reporting Services mobile reports are dedicated reports optimized for a wide variety of form factors and provide an optimal experience for users accessing reports on mobile devices. Mobile reports feature a assortment of visualizations, from time, category, and comparison charts, to treemaps and custom maps. Connect your mobile reports to a range of data sources, including on-premises SQL Server Analysis Services multidimensional and tabular data. Lay out your mobile reports on a design surface with adjusting grid rows and columns, and flexible mobile report elements that scale well to any screen size. Then save these mobile reports to a Reporting Service server, and view and interact with them in a browser or in the Power BI mobile app on iPads, iPhones, Android phones and Windows 10 devices.
  
#### Mobile Report Publisher  
 The [!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long-md.md)]allows you to create and publish SQL Server mobile reports to your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)].  
  
 ![SS_MRP_LayoutTabSmall](../reporting-services/media/ss-mrp-layouttabsm.png "SS_MRP_LayoutTabSmall")  
  
 For more information, see [Create mobile reports with SQL Server Mobile Report Publisher](../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md).  
  
#### SQL Server mobile reports hosted in Reporting Services available in Power BI Mobile app  
 The Power BI Mobile app for iOS on iPad and iPhone can now display SQL Server mobile reports hosted on your local report server.  
  
 ![SS_MRP_iPad_HomeSm](../reporting-services/media/ss-mrp-ipad-homesm.png "SS_MRP_iPad_HomeSm")  
  
 You can't connect by default without some configuration changes. For more information on how to allow the Power BI Mobile app to connect to your report server, see [Enable a report server for Power BI Mobile access](../reporting-services/report-server/enable-a-report-server-for-power-bi-mobile-access.md).
  
### Support of SharePoint mode and SharePoint 2016  
 [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports integration with SharePoint 2013 and SharePoint 2016.
 
For more information, see:  
  
-   [Supported Combinations of SharePoint and Reporting Services Server and Add-in &#40;SQL Server 2016&#41;](../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md)  
  
-   [Where to find the Reporting Services add-in for SharePoint Products](../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)  
  
-   [Install Reporting Services SharePoint Mode](../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)  

### Microsoft .NET Framework 4 Support  
 [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] supports the current versions of Microsoft .NET Framework 4. This includes version 4.0 and 4.5.1. If no version of .Net Framework 4.x is installed, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] setup installs .NET 4.0 during the feature installation step.  

### Report improvements

**HTML 5 Rendering Engine:** A new  HTML5 rendering engine that targets modern web "full" standards mode and modern browsers.  The new rendering engine no longer relies on quirks mode used by a few older browsers.
  
 For more information on browser support, see [Browser Support for Reporting Services and Power View](../reporting-services/browser-support-for-reporting-services-and-power-view.md).  

**Modern paginated reports:** Design beautifully modern paginated reports with new, modern styles for charts, gauges, maps and other data visualizations.
  
**Tree Map and Sunburst Charts:** Enhance your reports with Tree Map ![ssrs_treemap_icon](../reporting-services/media/ssrs-treemap-icon.png "ssrs_treemap_icon") and Sunburst ![ssrs_sunburst_icon](../reporting-services/media/ssrs-sunburst-icon.png "ssrs_sunburst_icon") charts, great ways to display hierarchal data. For more information, see [Tree Map and Sunburst Charts in Reporting Services](../reporting-services/report-design/tree-map-and-sunburst-charts-in-reporting-services.md).  

**Report embedding:** You can now embed mobile and paginated reports in other web pages, and applications, by using an iframe along with URL parameters.  

**Pin Report Items to a Power BI Dashboard:** While viewing a report in the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], you can select report items and pin them to a [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboard.   The items you can pin are charts, gauge panels, maps, and images. You can **(1)** select the group that contains the dashboard you want to pin to, **(2)** select the dashboard you want to pin the item too and **(3)** select how frequently you want the tile updated in the dashboard.   ![note](../analysis-services/instances/install-windows/media/ssrs-fyi-note.png "note") The refresh is managed by  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscriptions and after the item is pinned, you can edit the subscription and configure a different  refresh schedule.  
  
 ![ssRS_Pin_to_PowerBI](../reporting-services/media/ssrs-pin-to-powerbi.png) 
  
 For more information, see [Power BI Report Server Integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md) and [Pin Reporting Services items to Power BI Dashboards](../reporting-services/pin-reporting-services-items-to-power-bi-dashboards.md).  
 
 **PowerPoint Rendering and Export:** The Microsoft PowerPoint (PPTX) format is a new [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] rendering extension. You can export reports in the PPTX format from the usual applications; Report Builder, Report Designer (in SSDT), and the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)]. For the example the following image shows the export menu from the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)]. 
  
 ![ssrs-export-powerpoint](../reporting-services/media/ssrs-export-powerpoint.png) 
  
 You can also select the PPTX format for subscription output and use Report Server URL access to render and export a report. For example the following  URL command in your browser exports a report from a named instance of the report server.  
  
```  
http://servername/ReportServer_THESQLINSTANCE/Pages/ReportViewer.aspx?%2freportfolder%2freport+name+with+spaces&rs:Format=pptx  
```  
  
 For more information, see [Export a Report Using URL Access](../reporting-services/export-a-report-using-url-access.md).  
 
 **PDF Replaces ActiveX for Remote Printing:** The report viewer toolbar ActiveX print experience has been replaced with a modern PDF based experience that works across the matrix of supported browsers, including Microsoft Edge. No more ActiveX controls to download! Depending on the browser you use and the PDF viewing applications and services you have installed, [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] will either open  a print dialog to print your report or prompt you to download a .PDF file of your report.  As an administrator, you can still disable client side printing from [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)]. For more information, see [Enable and Disable Client-Side Printing for Reporting Services](../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md).

![ssrs-pdf-printing](../reporting-services/media/ssrs-pdf-printing.png)

### Subscription Improvements  
 
|Feature|Supported server mode|  
|-------------|---------------------------|  
|**Enable and disable subscriptions**. New user interface options to quickly disable and enable subscriptions. The disabled subscriptions maintain their other configuration properties such as schedule and can be easily enabled.<br /><br /> ![ssrs-enable-disable-subscriptions](../reporting-services/media/ssrs-enable-disable-subscriptions.png)<br /><br /> For more information, see [Disable or Pause Report and Subscription Processing](../reporting-services/subscriptions/disable-or-pause-report-and-subscription-processing.md).|Native mode|  
|**Subscription description**. When you create a new subscription, you can now include a description of the report as part of the subscription properties. The description is included on the subscription summary page.|SharePoint and Native mode|  
|**Change subscription owner**. Enhanced user interface to quickly change the owner of a subscription. Previous versions of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] allow administrators to change subscription owners using script. Starting with the [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] release, you can change subscription owners using the user interface or script. Changing the subscription owner is a common administrative task when users leave or change roles in your organization.|SharePoint and Native mode|  
|**Shared credential for file share subscriptions**. Two workflows now exist with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] file share subscriptions:<br /><br /> New in this release, your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] administrator can configure a single file share account, that is used for one to many subscriptions. The file share account is configured in the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] native mode configuration manager **Specify a file share account**, and then on the subscription configuration page, users select **Use file share account**.<br /><br /> Configure individual subscriptions with specific credentials for the destination file share.<br /><br /> You can also mix the two approaches and have some file share subscriptions use the central file share account while other subscriptions use specific credentials.|Native mode|  

### SQL Server Data Tools (SSDT)  
 The new release of SSDT includes the project templates for [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)]: Report Server Project Wizard and Report Server Project. For information about downloading SSDT, see [SQL Server Data Tools for Visual Studio 2015](http://go.microsoft.com/fwlink/?LinkId=827542).  

### Report Builder improvements

**New Report Builder User Interface:** The core [!INCLUDE[ssRBnoversion](../includes/ssrbnoversion-md.md)] user interface is now a modern look and feel with streamlined UI elements.  
  
|||  
|-|-|  
|New|Previous|  
|![ssrs_rbfacelift_new](../reporting-services/media/ssrs-rbfacelift-new.png "ssrs_rbfacelift_new")|![ssrs_rbfacelift_old](../reporting-services/media/ssrs-rbfacelift-old.png "ssrs_rbfacelift_old")|  

**Custom Parameters Pane:** You can now customize the parameters pane. Using the design surface in Report Builder, you can drag a parameter to a specific column and row in the parameters pane. You can add and remove columns to change the layout of the pane.   For more information, see [Customize the Parameters Pane in a Report &#40;Report Builder&#41;](../reporting-services/report-design/customize-the-parameters-pane-in-a-report-report-builder.md).  
  
 ![Parameter list in Report Data pane and in parameters pane](../reporting-services/media/ssrs-customizeparameter-parameterlist-reportdatapane.png "Parameter list in Report Data pane and in parameters pane")  

  
**High DPI Support:** [!INCLUDE[ssRBnoversion](../includes/ssrbnoversion-md.md)] for [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] supports High DPI (Dots Per Inch) scaling and devices.  For more information on High DPI, see the following:  
  
-   [Windows 8.1 DPI Scaling Enhancements](https://blogs.windows.com/windowsexperience/2013/07/15/windows-8-1-dpi-scaling-enhancements/)  
  
-   [High DPI and Windows 8.1](http://technet.microsoft.com/library/dn528848.aspx)  
  
## See Also  
 [What's New in Analysis Services](http://msdn.microsoft.com/en-us/aa69c299-b8f4-4969-86d8-b3292fe13f08)  
 [Technical Preview of Power BI reports in SSRS - Release notes](../reporting-services/reporting-services-release-notes.md)  
 [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md)   
 [Backward Compatibility](http://msdn.microsoft.com/en-us/675b0e0e-cfee-4790-9675-80fc3ea6d30f)   
 [Reporting Services Features Supported by the Editions of SQL Server 2016](http://msdn.microsoft.com/en-us/39f03d2d-6e48-4b34-a9d3-07f86313b937)   
 [Upgrade and Migrate Reporting Services](../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)   
 [Reporting Services &#40;SSRS&#41;](../reporting-services/create-deploy-and-manage-mobile-and-paginated-reports.md)  
  
  
