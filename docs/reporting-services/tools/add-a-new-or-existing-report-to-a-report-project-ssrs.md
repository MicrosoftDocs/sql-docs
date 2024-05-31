---
title: "Add a new or existing report to a report project"
description: Learn how to add a new or existing report to a report project by using the Report Wizard in SQL Server Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 10/26/2023
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "reports [Reporting Services], creating"
---
# Add a new or existing report to a report project
  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can add a new [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report by using the Report Wizard or by adding a new blank report to your project. You can also add an existing report. After you add a report, you can see the report name listed under the **Reports** folder in your project.  
  
> [!NOTE]  
>  To preview a report with existing data sources, you must have permissions to the data source from your report authoring client. For more information, see [Data connections, data sources, and connection strings](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
 After you add a report, you can define data sources, datasets, and design a report layout. To get started, see [Create a basic table report &#40;SSRS tutorial&#41;](../../reporting-services/tutorial-step01-creating-a-report-server-project-reporting-services.md) or [Tables &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md).  
  
## Add a new report by using the Report Wizard  
  
1.  In Solution Explorer, right-click the Reports folder, and then select **Add New Report**. The **Report Wizard** dialog box opens.  
  
     The wizard steps you through creating a data source, creating a dataset with a query, defining groups, specifying a layout and creating the report. The steps include:  
  
    -   **Select a Data Source.** The first step in creating a report is to define a data source. Report Wizard provides a list of all shared data sources in the report project, in addition to an option to create a new data source.  
  
    -   **Design a Query.** The next step is to design a query. You can enter the query string, build it by using Query Designer, or import a query from another report. For information about Query Designers, see [Reporting Services query designers](../../reporting-services/report-data/query-design-tools-ssrs.md).  
  
    -   **Choose a Report Type.** The next step is to select the type of report you want. You can choose a tabular or matrix report. A tabular report has a fixed number of columns. A matrix, or crosstab, report has a variable number of columns based on the results of the query. A map report displays analytical against a geographic background.  
  
    -   **Name the Report.**  The final step is to name the report and verify the fields that are included in the report. When all steps are completed, Report Designer creates the report and adds it to the report server project.  
  
## Add a new blank report  
  
1.  From the **Project** menu, select **Add New Item**.  
  
2.  In **Templates**, select **Report**.  
  
3.  Select **Add**.  
  
     A new blank report is added to the project and displayed on the design surface.  
  
## Add an existing report  
  
1.  From the **Project** menu, select **Add**, and then choose  **Existing Item**.  
  
2.  Navigate to the location of the .rdl file, choose it, and then select **Add**.  
  
     The report is added to the project under the **Reports** folder. When you close and reopen the project, reports are sorted alphabetically.  
  
## Related content
 [Reporting Services tutorials &#40;SSRS&#41;](../../reporting-services/reporting-services-tutorials-ssrs.md)  
 More questions? [Try the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
