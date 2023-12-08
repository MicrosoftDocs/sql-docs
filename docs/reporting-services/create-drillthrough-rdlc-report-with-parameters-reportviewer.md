---
title: "Create drillthrough (RDLC) reports with parameters - ReportViewer"
description: Learn about creating a drillthrough (RDLC) report with parameters and a query in local mode reporting.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/18/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Create drillthrough (RDLC) reports with parameters - ReportViewer
A [drillthrough](./report-design/drillthrough-reports-report-builder-and-ssrs.md) report is a report that a user opens by selecting a link within another report. Drillthrough reports commonly contain details about an item that is contained in an original summary report. This tutorial will walk you through the following lessons of creating a drillthrough report with parameters and a query, in [local mode reporting](report-server-sharepoint/local-mode-vs-connected-mode-reports-in-the-report-viewer.md).  
  
## Requirements  

[!INCLUDE [article-uses-adventureworks](../includes/article-uses-adventureworks.md)]
  
This walkthrough assumes that you are familiar with Transaction-SQL queries and ADO.NET [DataSet](/dotnet/api/system.data.dataset) and [DataTable](/dotnet/api/system.data.datatable) objects.  
  
Use Visual Studio 2015, and the ASP.NET Web Application, to create an ASP.NET webpage with a ReportViewer control. The control is configured to view a report that you create. For this walkthrough, you create the application in Microsoft Visual C#.  
  
## Tasks  
[Lesson 1: Create a new web site](../reporting-services/lesson-1-create-a-new-web-site.md)  
[Lesson 2: Define a data connection and data table for parent report](../reporting-services/lesson-2-define-a-data-connection-and-data-table-for-parent-report.md)  
[Lesson 3: Design the parent report by using the Report Wizard](../reporting-services/lesson-3-design-the-parent-report-using-the-report-wizard.md)  
[Lesson 4: Define a data connection and data table for child report](../reporting-services/lesson-4-define-a-data-connection-and-data-table-for-child-report.md)  
[Lesson 5: Design the child report by using the Report Wizard](../reporting-services/lesson-5-design-the-child-report-using-the-report-wizard.md)  
[Lesson 6: Add a ReportViewer control to the application](../reporting-services/lesson-6-add-a-reportviewer-control-to-the-application.md)  
[Lesson 7: Add drillthrough action on parent report](../reporting-services/lesson-7-add-drillthrough-action-on-parent-report.md)  
[Lesson 8: Create a data filter](../reporting-services/lesson-8-create-a-data-filter.md)  
[Lesson 9: Build and run the application](../reporting-services/lesson-9-build-and-run-the-application.md)  
  
## Related content

- [Reporting Services tutorials &#40;SSRS&#41;](../reporting-services/reporting-services-tutorials-ssrs.md)  
- [Design reports with Report Designer &#40;SSRS&#41;](../reporting-services/tools/design-reporting-services-paginated-reports-with-report-designer-ssrs.md)  
