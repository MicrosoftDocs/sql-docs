---
title: "Troubleshoot Report Design Issues with Reporting Services | Microsoft Docs"
ms.date: 02/27/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: troubleshooting


ms.topic: conceptual
ms.assetid: a0d103da-5a3e-475c-a71a-9e23476095e2
author: maggiesMSFT
ms.author: maggies
---
# Troubleshoot Report Design Issues with Reporting Services
Report design issues may occur when you are creating the report layout in Design view in an report authoring application. Use this topic to help troubleshoot these issues.   
  
## Why does my text box show only a single value and not repeat for every row?  
A text box with a dataset field reference renders only once and displays the first value in the dataset.   
  
**Text Box Parent is the Report Body**  
  
  
A text box added directly to the design surface can only display an aggregate value for a dataset.  
  
To verify the parent container of a text box, select the text box, and in the Properties pane, scroll to **Parent**.   
  
If you want text boxes that show each value in a dataset, use a data region, such as a table or matrix. By default, each cell in a table or matrix contains a text box. Drag dataset fields to each cell.   
  
## Why can't I add Total Pages to my Report?  
The built-in fields `[&PageNumber]` and `[&TotalPages]` are not valid in the report body.   
  
PageNumber and TotalPages are Valid Only in the Page Header and Page Footer.  
  
  
The built-in fields [&PageNumber] and [&TotalPages] are valid only in the page header and page footer.   
  
To add [&PageNumber] or [&TotalPages] to a report, you must first add a page header or page footer. For more information, see [Add or emove a page header](../../reporting-services/report-design/add-or-remove-a-page-header-or-footer-report-builder-and-ssrs.md).  
  
> [!NOTE]  
> Including [&TotalPages] in the page header or page footer can have consequences for report processing. For more information, see Troubleshooting Reports: Reports Exported to a Specific File Format.  
[Troubleshoot processing of Reporting Services Reports](../../reporting-services/troubleshooting/troubleshoot-processing-of-reporting-services-reports.md).  
  
## How do I design two tables or a chart and a table to display side-by-side?  
Designing a report is not a WYSISYG ("what you see is what you get") experience. The report processor combines data, report items, report layout information such as white space, containers, and expressions to produce a compiled report which is then passed to a report renderer that "lays out" that report for the specified viewing experience: interactive for an HTML browser or as a file format. The automatic layout algorithms may produce a report layout that you want to change.   
  
### Rendering Rules Use Page Size, Containers, Peer Objects, Relative Placement, and White Space to Determine Layout  
In general, a report grows to accommodate its data and pushes other report items aside.   
  
To group multiple data regions or report items together, place them in the same parent container. For example, place a chart and table in a rectangle container and align their top edges to display them side by side. For more information, see [Rendering Behaviors in Report Builder](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md).  
  
## See Also  
[Troubleshoot Data Retrieval issues with Reporting Services Reports](../../reporting-services/troubleshooting/troubleshoot-data-retrieval-issues-with-reporting-services-reports.md)  
[Troubleshoot Reporting Services Subscriptions and Delivery](../../reporting-services/troubleshooting/troubleshoot-reporting-services-subscriptions-and-delivery.md)  
  
  
  

[!INCLUDE[feedback_stackoverflow_msdn_connect](../../includes/feedback-stackoverflow-msdn-connect-md.md)]

