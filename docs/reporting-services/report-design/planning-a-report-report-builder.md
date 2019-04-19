---
title: "Planning a Report (Report Builder) | Microsoft Docs"
ms.date: 03/03/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
helpviewer_keywords: 
  - "getting started"
  - "report design"
ms.assetid: 79113505-1ce8-4f8c-9260-d861838f7813
author: maggiesMSFT
ms.author: maggies
---
# Planning a Report (Report Builder)
  Report Builder lets you create many kinds of paginated reports. For example, you can create reports that show summary or detailed sales data, marketing and sales trends, operational reports, or dashboards. You can also create reports that take advantage of richly formatted text, such as for sales orders, product catalogs, or form letters. All these reports are created by using different combinations of the same basic building blocks in Report Builder. To create a useful, easily understood report, it helps to plan first. Here are some things you might want to consider before you get started:  
  
-   **What format do you want the report to appear in?**  
  
     You can render reports online in a browser such as the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or export them to other formats such as Excel, Word, or PDF. The final form your report takes is an important consideration because not all features are available in all export formats. For more information, see [Export Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md).  
  
-   **What structure do you want to use to present the data in the report?**  
  
     You have a choice among tabular, matrix (similar to a cross-tab or PivotTable report), chart, free-form structures, or any combination of these to present data. For more information, see [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md) and [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md).  
  
-   **What do you want your report to look like?**  
  
     Report Builder provides a lot of report items that you can add to your report to make it easier to read, highlight key information, help your audience navigate the report, and so on. Knowing how you want the report to appear can determine whether you need report items such as text boxes, rectangles, images, and lines. You might also want to show or hide items, add a document map, include drillthrough reports or subreports, or link to other reports. For more information, see [Images, Text Boxes, Rectangles, and Lines &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/images-text-boxes-rectangles-and-lines-report-builder-and-ssrs.md) and [Interactive Sort, Document Maps, and Links &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/interactive-sort-document-maps-and-links-report-builder-and-ssrs.md).  
  
-   **What data do you want your readers to see? Should the data or format be filtered for different audiences?**  
  
     You might want to narrow the scope of the report to specific users or locations, or to a particular time period. To filter the report data, use parameters to retrieve and display only the data you want. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
-   **Do you need to create your own calculations?**  
  
     Sometimes, your data source and datasets do not contain the exact fields that you need for your report. In that situation, you might have to create your own calculated fields. For example, you might want to multiply the price per unit times the quantity to get a line item sales amount. Expressions are also used to provide conditional formatting and other advanced features. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
-   **Do you want to hide report items initially?**  
  
     Consider whether you want to hide report items, including data regions, groups and columns, when the report is first run. For example, you can initially present a summary table, and then drill down into the detailed data. For more information, see [Drilldown Action &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/drilldown-action-report-builder-and-ssrs.md).  
  
-   **How are you going to deliver your report?**  
  
     You can save your report to your local computer and continue to work on it, or run it locally for your own information. However, to share your report with others, you need to save the report to a report server that is configured in native mode or a report server in SharePoint integrated mode. Saving it to a server lets others run it whenever they want to. Alternatively, the report server administrator can set up a subscription to the report or set up e-mail delivery of the report to other individuals. You can have the report delivered in a specific export format if you prefer. For more information, see [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md).  
  
## See Also  
 [Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md)   
 [Report Authoring Concepts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-authoring-concepts-report-builder-and-ssrs.md)   
 [Report Builder Tutorials](../../reporting-services/report-builder-tutorials.md)  
  
  
