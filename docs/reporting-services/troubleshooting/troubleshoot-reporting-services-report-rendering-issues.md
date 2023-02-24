---
title: "Troubleshoot Reporting Services Report Rendering Issues"
description: In this article, diagnose and fix display problems when the compiled report and layout data is sent to a report renderer in SQL Server Reporting Services.
ms.date: 02/27/2016
ms.service: reporting-services
ms.subservice: troubleshooting


ms.topic: conceptual
ms.assetid: 1e0fb399-4c16-438a-92cb-db3e877896d0
author: maggiesMSFT
ms.author: maggies
---
# Troubleshoot Reporting Services Report Rendering Issues
After the report data and layout information is combined, the compiled report is sent to a report renderer. For example, when you preview a report locally, you are using the HTML renderer to view the compiled report. Use this topic to help troubleshoot issues specific to report rendering.   
  
## Why do I have extra white space, including blank pages, in my report?  
Report items are automatically adjusted during report processing to preserve white space that is defined as part of the report. White space in the report design view is preserved. On the report design surface, the white background represents white space that is preserved when a report is viewed, exported, or printed, depending on target medium.  
  
### White Space and Page Breaks Interact During Rendering  
When you view a report or export the report to a file format, the associated rendering extension processes the report and saves it to the specified file format. Each rendering extension processes the white space in a report according to specific rules. White space is also affected by the page setting properties, page breaks set on report items, the relative position of report items placed in the report body, the KeepTogether property for certain report items, and whether report items are in parent containers.   
  
To eliminate extra pages because of report width, drag the edge of the report design surface to align with the outermost report item. For a left-to-right report layout, drag the right edge to be aligned with the outermost report item. For more information, see [Rendering Behaviors](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md).  
  
### White Space is Not Preserved at the End of a Report  
Reporting Services provides an option that lets you control whether to preserve or eliminate white space at the end of a report.   
  
To preserve white space at the end of a report, select the report and in the Properties pane, scroll to ConsumeContainerWhitespace, and type False.   
  
## Why do my reports look different when exported to different formats?  
After you run a report, you can export it to another format such as Excel, Word, or PDF. Depending on the format to which you export the report certain rules and limitations might apply. You can address many limitations by considering them when you create the report. You might need to use a slightly different layout in your report, carefully align items within the report, confine report footers to a single line of text, and so forth. You can also use the RenderFormat built-in global to conditionally use a different report layout for different renderers. Other built-in globals can help you manage pagination in the exported format and name worksheet tabs in Excel. For more information, see [Export Reports](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md) and [Use Built-in Globals an Users Reference](../../reporting-services/report-design/built-in-collections-built-in-globals-and-users-references-report-builder.md).  
  
## How can I view all my report data on one page?  
For an interactive viewing experience for reports that do not have excessive amounts of data, you might want to see all the data on one page.   
  
For soft page-break renderers, to view all the data on one page, in Report properties, set InteractiveHeight to 0. In soft page-break renderers, existing page breaks are the ignored.   
  
> [!NOTE]  
> When a report has no page breaks, the entire report must be processed before you can view the first page.   
  
For more information about categories of renderers, see [Rendering Behaviors](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md).  
  
## Reports do not run when your browser is configured to prompt for credentials  
Viewing your reports may fail with an error message when your browser is configured to prompt for credentials and your data source is configured for integrated Windows authentication. This occurs when your data source is on a separate computer than the report server, the data source is configured to use Windows Authentication, and the browser is set to prompt for credentials. The following are examples of messages you will see.  
  
When the data source is configured for a Microsoft SQL Server connection type:  
`An error has occurred during report processing.`  
`Cannot create a connection to data source 'localhost'.`  
`Login failed for user '(null)'. Reason: Not associated with a trusted SQL Server connection.`  
  
When the data source is configured for a Microsoft SharePoint List connection type:  
`An error occurred during client rendering.`   
`An error has occurred during report processing.`   
`Query execution failed for dataset 'DataSet1'.`   
`The request failed with HTTP status 401: Unauthorized.`  
  
**To work around this issue:** Modify the data source to use stored credentials instead of Windows credentials.  
  
**This issue applies to:** Browsers configured to prompt for credentials.  
  
## See Also  
[Errors and events (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)  
[Troubleshoot Data Retrieval issues with Reporting Services Reports](../../reporting-services/troubleshooting/troubleshoot-data-retrieval-issues-with-reporting-services-reports.md)  
[Troubleshoot Reporting Services Subscriptions and Delivery](../../reporting-services/troubleshooting/troubleshoot-reporting-services-subscriptions-and-delivery.md)  
  
  
  
  

[!INCLUDE[feedback_stackoverflow_msdn_connect](../../includes/feedback-stackoverflow-msdn-connect-md.md)]
