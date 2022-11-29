---
title: "What's new in Reporting Services | Microsoft Docs"
description: Learn about what's new in the different versions of SQL Server Reporting Services, including changes to the major feature areas.
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
ms.reviewer: ""
ms.custom:
  - intro-whats-new
ms.date: 09/16/2022
---

# What's new in SQL Server Reporting Services (SSRS)

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../includes/ssrs-appliesto-not-pbirs.md)]

Learn about what's new in the different versions of SQL Server [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. This article covers the major feature areas and is updated as new items are released.

For information about Power BI Report Server, see [What's new in Power BI Report Server?](/power-bi/report-server/whats-new)

::: moniker range="=sql-server-ver16"

## SQL Server 2022 Reporting Services

This release introduces the new [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Reporting Services (SSRS). We continue to innovate, create, and design in a way that gives everyone the ability to achieve more. Designing for inclusivity reflects how people adapt to the world around them. In this new release of SSRS, we've done a lot of accessibility work to make sure we're empowering people to achieve more. The release includes enhanced Windows Narrator support for the new Windows OS (Operating Systems) and Windows Server, security enhancements, browser performance improvements with Angular, accessibility bug fixes, support for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] instances report server catalog and reliability updates.

Download [**SQL Server 2022 Reporting Services**](https://www.microsoft.com/download/details.aspx?id=104502) from the Microsoft Download Center.

### Updated web portal

The web portal received a face lift.

:::image type="content" source="../reporting-services/media/report-server-2022-web-portal.png" alt-text="Screenshot showing new updated [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Reporting Services web portal.":::

### Deprecated features

In 2020, we announced the upcoming deprecation of Report Server features [Pin to Power BI, Mobile Reports, and Mobile Report Publisher](deprecated-features-in-sql-server-reporting-services-ssrs.md). These features will be removed from versions of SQL Server starting with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and will no longer be supported. Only the last three releases, SQL Server 2016, SQL Server 2017, and SQL Server 2019, will be supported in maintenance mode until EOL (End of Life) for existing customers.

When we mark a feature as deprecated, it means:

- The feature is in maintenance mode only. We'll make no new changes, including changes related to interoperability with new features.
- We strive not to remove a deprecated feature from future releases, to make upgrades easier. However, in rare situations, we may choose to permanently remove the feature from Reporting Services if it limits future innovations.
- For new development work, we don't recommend using deprecated features.

::: moniker-end

::: moniker range=">=sql-server-ver15"

## SQL Server 2019 Reporting Services

**Download** :::image type="icon" source="../includes/media/download.svg" border="false":::

[SQL Server 2019 Reporting Services](https://www.microsoft.com/download/details.aspx?id=100122) is available for download from the Microsoft Download Center.

### Azure SQL Managed Instance support

You can now host a database catalog used for SQL Server Reporting Services (SSRS) in an Azure SQL Managed Instance (MI) that's hosted either in a VM or in your data center. Support is limited to using database credentials for the connection to SQL MI.

### Power BI Premium dataset support

You can connect to Power BI datasets using either Microsoft Report Builder or SQL Server Data Tools (SSDT). Then you can publish those reports to SSRS 2019 using SQL Server Analysis Services connectivity. Users need to use a stored Windows user name and password to enable the scenario.

### AltText (alternative text) support for report elements

When authoring reports, you can use tooltips to specify text for each element on the report. Screen reader technology identifies these tooltips properly.

### Azure Active Directory Application Proxy support

With Azure Active Directory Application Proxy, you no longer need to manage your own web application proxy in order to allow secure access via the web or mobile apps.

### Custom headers

Sets header values for all URLs matching the specified regex pattern. Users can update the custom header value with valid XML to set header values for selected request URLs. Admins can add any number of headers in the XML. See [Custom headers](tools/server-properties-advanced-page-reporting-services.md#customheaders) in the **Server Properties Advanced Page** article for details.

### Transparent Database Encryption

SQL Server 2019 now supports Transparent Database Encryption for the SSRS catalog database for Enterprise and Standard editions. 

### Microsoft Report Builder update

The newly released version of Report Builder is fully compatible with the 2016, 2017, and 2019 versions of Reporting Services. It's also compatible with all released and supported versions of Power BI Report Server.

::: moniker-end

::: moniker range=">=sql-server-2017"

## SQL Server 2017 Reporting Services

**Download** :::image type="icon" source="../includes/media/download.svg" border="false":::

To download SQL Server 2017 Reporting Services, go to the  **[Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=55252)**.

### Comments on reports

Comments are now available for reports, to add perspective, and collaborate with others. You can also include attachments with comments.

![Comments within a report server](media/what-s-new-in-sql-server-reporting-services-ssrs/report-server-comments.png)

For more information, see [Add comments to a report in a report server](https://powerbi.microsoft.com/documentation/reportserver-add-comments/).

### REST API support

To enable development of modern applications and customization, SQL Server Reporting Services now supports a fully OpenAPI compliant RESTful API. The full API specification and documentation can now be found on [SwaggerHub](https://app.swaggerhub.com/apis/microsoft-rs/SSRS/2.0).

### Query designer support for DAX now in Report Builder and SQL Server Data Tools

In Report Builder and SQL Server Data Tools, you can now create native DAX queries against supported SQL Server Analysis Services tabular data models. You can use the query designer in both tools to drag and drop the fields you want. The DAX query is then generated for you.

Read more on the [Reporting Services blog](/archive/blogs/sqlrsteamblog/query-designer-support-for-dax-now-available-in-report-builder-and-sql-server-data-tools).

* Download [SQL Server Report Builder](https://go.microsoft.com/fwlink/?LinkId=734968).
* Download [SQL Server Data Tools - Release Candidate](../ssdt/download-sql-server-data-tools-ssdt.md).

> [!NOTE]
> You can only use the query designer for DAX with SSAS tabular data sources built in SQL Server 2016+.
::: moniker-end

## SSRS 2016

### Reporting Services [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)]  

A new [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] is available. The updated web portal includes
- KPIs
- Mobile Reports
- Paginated Reports
- Excel files
- Power BI Desktop files

The [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] replaces Report Manager from previous releases.

To create Mobile Reports, you need the [!INCLUDE[SS_MobileReptPub_Short](../includes/ss-mobilereptpub-short.md)].

[!INCLUDE [ssrs-mobile-report-deprecated](../includes/ssrs-mobile-report-deprecated.md)]

For more information about the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)], see [Web portal (SSRS Native Mode)](../reporting-services/web-portal-ssrs-native-mode.md).  

![Screenshot showing the SQL Server Reporting Services portal.](../reporting-services/media/ssrsportal.png "ssRSPortal")  

#### Custom branding for the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] 

You can customize the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] with your organization's logo and colors by using a branding pack.  

For more information about custom branding, see [Branding the web portal](./branding-the-web-portal.md)

#### Key performance indicators (KPI) in the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] 

You create KPIs directly in the [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)] that are contextual to the current folder. When creating KPIs, you can choose dataset fields, and summarize their values. You can also select related content to drill-through to expose more details.

![Screenshot showing KPIS in the SQL Server Reporting Services portal.](../reporting-services/media/ssrs-webportal-kpi.png)

For more information, see [Working with KPIs in the web portal](./working-with-kpis-in-reporting-services.md)

### Mobile Reports

Reporting Services mobile reports are dedicated reports optimized for a wide variety of form factors and provide an optimal experience for users accessing reports on mobile devices. Mobile reports feature an assortment of visualizations, from time, category, and comparison charts, to tree maps and custom maps. Connect your mobile reports to a range of data sources, including on-premises SQL Server Analysis Services multidimensional and tabular data. You can place fields for mobile reports on a design surface with adjusting grid rows and columns. The flexible mobile report elements automatically scale to fit any screen size. You save the mobile reports to a Reporting Service server, and can view and interact with them in a browser, or the Power BI mobile app. Devices supported include:

- iPad
- iPhones
- Android phones
- or any Windows device

#### Mobile Report Publisher  

The [!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long.md)] allows you to create and publish SQL Server mobile reports to your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] [!INCLUDE[ssRSWebPortal-Non-Markdown](../includes/ssrswebportal-non-markdown-md.md)].  

![SS_MRP_LayoutTabSmall](../reporting-services/media/ss-mrp-layouttabsm.png "SS_MRP_LayoutTabSmall")  

For more information, see [Create mobile reports with SQL Server Mobile Report Publisher](../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md).  

#### SQL Server mobile reports hosted in Reporting Services available in Power BI Mobile app  

The Power BI Mobile app for iOS on iPad and iPhone can now display SQL Server mobile reports hosted on your local report server.  

![SS_MRP_iPad_HomeSm](../reporting-services/media/ss-mrp-ipad-homesm.png "SS_MRP_iPad_HomeSm")  

You can't connect by default without some configuration changes. For more information on how to allow the Power BI Mobile app to connect to your report server, see [Enable a report server for Power BI Mobile access](../reporting-services/report-server/enable-a-report-server-for-power-bi-mobile-access.md).

### Support of SharePoint mode and SharePoint 2016  

[!INCLUDE[sssql15-md](../includes/sssql16-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports integration with SharePoint 2013 and SharePoint 2016.

For more information, see:  

- [Supported Combinations of SharePoint and Reporting Services Server and Add-in &#40;SQL Server 2016&#41;](../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md)  

- [Where to find the Reporting Services add-in for SharePoint Products](../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)  

- [Install Reporting Services SharePoint Mode](../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)  

### Microsoft .NET Framework 4 Support  

[!INCLUDE[ssRSCurrent-md](../includes/ssrscurrent-md.md)] supports the current versions of Microsoft .NET Framework 4, including version 4.0 and 4.5.1. If a 4.x version of .NET Framework isn't already installed, [!INCLUDE[ssNoVersion-md](../includes/ssnoversion-md.md)] setup installs .NET 4.0 during the feature installation step.  

### Report improvements

**HTML 5 Rendering Engine:** A new  HTML5 rendering engine that targets modern web "full" standards mode and modern browsers.  The new rendering engine no longer relies on quirks mode used by a few older browsers.

For more information on browser support, see [Browser Support for Reporting Services](../reporting-services/browser-support-for-reporting-services-and-power-view.md).  

**Modern paginated reports:** Design beautifully modern paginated reports with new, modern styles for charts, gauges, maps, and other data visualizations.

**Tree Map and Sunburst Charts:** Enhance your reports with Tree Map ![ssrs_treemap_icon](../reporting-services/media/ssrs-treemap-icon.png "ssrs_treemap_icon") and Sunburst ![ssrs_sunburst_icon](../reporting-services/media/ssrs-sunburst-icon.png "ssrs_sunburst_icon") charts, great ways to display hierarchical data. For more information, see [Tree Map and Sunburst Charts in Reporting Services](../reporting-services/report-design/tree-map-and-sunburst-charts-in-reporting-services.md).  

**Report embedding:** You can now embed mobile and paginated reports in other web pages, and applications by using an IFrame, along with URL parameters.  

**Pin Report Items to a Power BI Dashboard:** While viewing a report in the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], you can select report items and pin them to a [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboard.   The items you can pin are charts, gauge panels, maps, and images. You can:

1. Select the group that contains the dashboard you want to pin to.
2. Select the dashboard you want to pin the item to.
3. Select how frequently you want the tile updated in the dashboard.

![note](/analysis-services/analysis-services/instances/install-windows/media/ssrs-fyi-note.png "note") The refresh is managed by  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscriptions and after the item is pinned, you can edit the subscription and configure a different  refresh schedule.

![Screenshot showing the Pin to Power BI Dashboard dialog box.](../reporting-services/media/ssrs-pin-to-powerbi.png) 

For more information, see [Power BI Report Server Integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md) and [Pin Reporting Services items to Power BI Dashboards](../reporting-services/pin-reporting-services-items-to-power-bi-dashboards.md).  

**PowerPoint Rendering and Export:** The Microsoft PowerPoint (PPTX) format is a new [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] rendering extension. You can export reports in the PPTX format from the usual applications; Report Builder, Report Designer (in SSDT), and the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)]. For the example, the following image shows the export menu from the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)]. 

![Screenshot showing the Export dropdown list with the PowerPoint option called out.](../reporting-services/media/ssrs-export-powerpoint.png) 

You can also select the PPTX format for subscription output and use Report Server URL access to render and export a report. For example, the following  URL command in your browser exports a report from a named instance of the report server.  

```https
https://servername/ReportServer_THESQLINSTANCE/Pages/ReportViewer.aspx?%2freportfolder%2freport+name+with+spaces&rs:Format=pptx  
```  

For more information, see [Export a Report Using URL Access](../reporting-services/export-a-report-using-url-access.md).

**PDF Replaces ActiveX for Remote Printing:** The report viewer toolbar now prints Via PDF instead of ActiveX controls. The new report viewer is supported by most modern browsers, including Microsoft Edge. No more ActiveX controls to download! Depending on the browser you use and the PDF viewing applications and services you've installed, [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] either a print dialog box opens to print your report, or you're prompted to download a .PDF file. As an administrator, you can still disable client-side printing from [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].

For more information, see [Enable and Disable Client-Side Printing for Reporting Services](../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md).

![Screenshot of the Print dialog box.](../reporting-services/media/ssrs-pdf-printing.png)

### Subscription Improvements  

|Feature|Supported server mode|  
|-------------|---------------------------|  
|**Enable and disable subscriptions**. New user interface options to quickly disable and enable subscriptions. The disabled subscriptions maintain their other configuration properties such as schedule and can be easily enabled.<br /><br /> ![Screenshot showing the Enable, Disable, and Delete options.](../reporting-services/media/ssrs-enable-disable-subscriptions.png)<br /><br /> For more information, see [Disable or Pause Report and Subscription Processing](../reporting-services/subscriptions/disable-or-pause-report-and-subscription-processing.md).|Native mode|  
|**Subscription description**. When you create a new subscription, you can now include a description of the report as part of the subscription properties. The description is included on the subscription summary page.|SharePoint and Native mode|  
|**Change subscription owner**. Enhanced user interface to quickly change the owner of a subscription. Previous versions of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] allow administrators to change subscription owners using script. Starting with the [!INCLUDE[sssql15-md](../includes/sssql16-md.md)] release, you can change subscription owners using the user interface or script. Changing the subscription owner is a common administrative task when users leave or change roles in your organization.|SharePoint and Native mode|  
|**Shared credential for file share subscriptions**. Two workflows now exist with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] file share subscriptions:<br /><br /> New in this release, your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] administrator can configure a single file share account that you can use for multiple subscriptions. The file share account is configured in the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] native mode configuration manager **Specify a file share account**. On the subscription configuration page, users select **Use file share account**.<br /><br /> You configure individual subscriptions with specific credentials for the destination file share.<br /><br /> You can also mix the two approaches and have some file share subscriptions use the central file share account while other subscriptions use specific credentials.|Native mode|

### SQL Server Data Tools (SSDT)

The new release of SSDT includes the project templates for [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)]: Report Server Project Wizard and Report Server Project. For information about downloading SSDT, see [SQL Server Data Tools for Visual Studio 2015](../ssdt/download-sql-server-data-tools-ssdt.md).  

### Report Builder improvements

**New Report Builder User Interface:** The core [!INCLUDE[ssRBnoversion](../includes/ssrbnoversion.md)] user interface is now a modern look and feel with streamlined UI elements.  

|New|Previous|  
|-|-|  
|![ssrs_rbfacelift_new](../reporting-services/media/ssrs-rbfacelift-new.png "ssrs_rbfacelift_new")|![ssrs_rbfacelift_old](../reporting-services/media/ssrs-rbfacelift-old.png "ssrs_rbfacelift_old")|  

**Custom Parameters Pane:** You can now customize the parameters pane. Using the design surface in Report Builder, you can drag a parameter to a specific column and row in the parameters pane. You can add and remove columns to change the layout of the pane. For more information, see [Customize the Parameters Pane in a Report &#40;Report Builder&#41;](../reporting-services/report-design/customize-the-parameters-pane-in-a-report-report-builder.md).  

![Parameter list in Report Data pane and in parameters pane](../reporting-services/media/ssrs-customizeparameter-parameterlist-reportdatapane.png "Parameter list in Report Data pane and in parameters pane")  

**High DPI Support:** [!INCLUDE[ssRBnoversion](../includes/ssrbnoversion.md)] supports High DPI (Dots Per Inch) scaling and devices.  For more information on High DPI, see the following:  

- [Windows 8.1 DPI Scaling Enhancements](https://blogs.windows.com/windowsexperience/2013/07/15/windows-8-1-dpi-scaling-enhancements/)  

- [High DPI and Windows 8.1](/previous-versions/windows/it-pro/windows-8.1-and-8/dn528848(v=win.10))  

## Next steps

[What's New in Analysis Services](/analysis-services/what-s-new-in-sql-server-analysis-services?viewFallbackFrom=sql-server-ver15)  
[Backward Compatibility](reporting-services-backward-compatibility.md)  
[Reporting Services Features supported by the Editions of SQL Server](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md)  
[Upgrade and Migrate Reporting Services](../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)  
[Reporting Services](../reporting-services/create-deploy-and-manage-mobile-and-paginated-reports.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)