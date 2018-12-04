---
title: "Report Builder in SQL Server | Microsoft Docs"
ms.date: 11/29/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-builder


ms.topic: conceptual
f1_keywords: 
  - "10428"
helpviewer_keywords: 
  - "overview of Report Builder"
  - "getting started"
ms.assetid: 55bf4f9c-d037-412f-ae57-3fc39ce32fa5
author: maggiesMSFT
ms.author: maggies
---
# Report Builder in SQL Server
  [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] is a tool for authoring paginated reports, for business users who prefer to work in a stand-alone environment instead of using Report Designer in Visual Studio.  When you design a paginated report, you're creating a report definition that specifies where to get the data, which data to get, and how to display the data. When you run the report, the report processor takes the report definition you have specified, retrieves the data, and combines it with the report layout to generate the report. You can preview your report in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]. Then publish your report to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in native mode or in SharePoint integrated mode (2016 and earlier). You can also publish a paginated report to the Power BI service. Read more about [paginated reports in Power BI Premium](https://docs.microsoft.com/power-bi/paginated-reports-report-builder-power-bi) (Preview).
  
 ![rs_GettingStartedReport](../../reporting-services/report-builder/media/rs-gettingstartedreport.png "rs_GettingStartedReport")  
  
 This paginated report features a matrix with row and column groups, sparklines, indicators, and a summary pie chart in the corner cell, accompanied by a map with two sets of geographic data represented by color and by circle size.  
  
##  <a name="JumpStartReptCreation"></a> Jump-Start Report Creation  
  
-   **Start with a shared dataset**. Shared datasets are queries based on a shared data source and saved to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in native mode or  in SharePoint integrated mode.  
  
-   **Start with the Table, Matrix, or Chart wizard**. Choose a data source connection, drag and drop fields to create a dataset query, select a layout and style, and customize your report.  
  
-   **Start with the Map wizard** to create reports that display aggregated data against a geographic or geometric background. Map data can be spatial data from a [!INCLUDE[tsql](../../includes/tsql-md.md)] query or an Environmental Systems Research Institute, Inc. (ESRI) shapefile. You can also add a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Bing map tile background.  
  
-   **Start your report with report parts**. Report parts are report items that have been published separately to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in native mode or  in SharePoint integrated mode. Report parts can be reused in other reports. Report items such as tables, matrices, charts, and images can be published as report parts.  
  
##  <a name="DesignRept"></a> Design Your Report  
  
-   **Create paginated reports with table, matrix, chart, and free-form report layouts.** Create table reports for column-based data, matrix reports (like cross-tab or PivotTable reports) for summarized data, chart reports for graphical data, and free-form reports for anything else. Reports can embed other reports and charts, together with lists, graphics, and controls for dynamic Web-based applications.  
  
-   **Report from a variety of data sources.** Build reports using data from any data source type that has a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]-managed data provider, OLE DB provider, or ODBC data source. You can create reports that use relational and multidimensional data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], Oracle, Hyperion, and other databases. You can use an XML data processing extension to retrieve data from any XML data source. You can use table-valued functions to design custom data sources.  
  
-   **Modify existing reports.** By using [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)], you can customize and update reports that were created in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]Report Designer.  
  
-   **Modify your data** by filtering, grouping, and sorting data, or by adding formulas or expressions.  
  
-   **Add charts, gauges, sparklines, and indicators** to summarize data in a visual format, and present large volumes of aggregated information at a glance.  
  
-   **Add interactive features** such as document maps, show/hide buttons, and drillthrough links to subreports and drillthrough reports. Use parameters and filters to filter data for customized views.  
  
-   **Embed or reference images** and other resources, including external content.  
  
##  <a name="ManageRpt"></a> Manage Your Report  
  
-   **Save the definition of the report** to your computer or to the report server, where you can manage it and share it with others.  
  
-   **Choose a presentation format** when you open the report, or after you open the report. You can select Web-oriented, page-oriented, and desktop application formats. Formats include HTML, MHTML, PDF, XML, CSV, TIFF, Word, and Excel.  
  
-   **Set up subscriptions.** After you publish the report to the report server or a report server in SharePoint integrated mode, you can configure your report to run at a specific time, create a report history, and set up e-mail subscriptions.  
  
-   **Generate data feeds** from your report by using the Reporting Services Atom rendering extension.  
  
> [!NOTE]  
>  Published reports are managed on a report server or a report server in SharePoint integrated mode by a report server administrator. Report server administrators can define security, set properties, and schedule operations such as report history and e-mail report delivery. They can create shared schedules and shared data sources and make them available for general use. Administrators also manage all of the report server folders. The ability to perform management tasks depends on user permissions.  
  
## See Also  
  [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md)  
  
  [Install Report Builder](../../reporting-services/install-windows/install-report-builder.md)

  [What's New in SQL Server Reporting Services and Report Builder](~/reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md)  
  Describes the new features in this version of [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] and [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)].   
  [Tutorial: Creating a Quick Chart Report Offline](../../reporting-services/report-builder/tutorial-create-a-quick-chart-report-offline-report-builder.md)  
 Introduces [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] and the wizards available to help you create reports. The tutorial provides a set of data for you to work with so you do not need to connect to a data source to get started.  
  
 [Planning a Report &#40;Report Builder&#41;](../../reporting-services/report-design/planning-a-report-report-builder.md)  
 Provides information on what you should consider before you start to build your report.  
  
 [Report Authoring Concepts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-authoring-concepts-report-builder-and-ssrs.md)  
 Defines key concepts used in throughout [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] documentation.  
  
 [Report Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/report-design-view-report-builder.md)  
 Explains the different panes and regions of report design view.  
  
 [Shared Dataset Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/shared-dataset-design-view-report-builder.md)  
 Explains the different panes and regions of shared dataset design view.  
  
 [Keyboard Shortcuts &#40;Report Builder&#41;](../../reporting-services/report-builder/keyboard-shortcuts-report-builder.md)  
 Outlines the shortcut keys available for navigating and designing reports in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)].  
  

