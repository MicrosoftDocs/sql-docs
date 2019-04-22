---
title: "Add a New or Existing Report to a Report Project (SSRS) | Microsoft Docs"
ms.date: 03/17/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: tools


ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], creating"
ms.assetid: 8bc0bb53-ad8a-464d-bb6a-7fea5fa62c5c
author: maggiesMSFT
ms.author: maggies
---
# Add a New or Existing Report to a Report Project (SSRS)
  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can add a new [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report by using the Report Wizard or by adding a new blank report to your project. You can also add an existing report. After you add a report, you can see the report name listed under the **Reports** folder in your project.  
  
> [!NOTE]  
>  To preview a report with existing data sources, you must have permissions to the data source from your report authoring client. For more information, see [Data Connections, Data Sources, and Connection Strings](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
 After you add a report, you can define data sources, datasets, and design a report layout. To get started, see [Create a Basic Table Report &#40;SSRS Tutorial&#41;](../../reporting-services/create-a-basic-table-report-ssrs-tutorial.md) or [Tables &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md).  
  
## To add a new report using the Report Wizard  
  
1.  In Solution Explorer, right-click the Reports folder, and then click **Add New Report**. The **Report Wizard** dialog box opens.  
  
     The wizard steps you through creating a data source, creating a dataset with a query, defining groups, specifying a layout and creating the report. The steps include:  
  
    -   **Select a Data Source.** The first step in creating a report is to define a data source. Report Wizard provides a list of all shared data sources in the report project, in addition to an option to create a new data source.  
  
    -   **Design a Query.** The next step is to design a query. You can type the query string, build it using Query Designer, or import a query from another report. For information about Query Designers, see [Reporting Services Query Designers](https://msdn.microsoft.com/library/07efd3f1-804f-45f7-b62a-3e727a3d9835).  
  
    -   **Choose a Report Type.** The next step is to select the type of report you want. You can choose a tabular or matrix report. A tabular report has a fixed number of columns. A matrix, or crosstab, report has a variable number of columns based on the results of the query. A map report displays analytical against a geographic background.  
  
    -   **Name the Report.**  The final step is to name the report and verify the fields that will be included in the report. When all steps are completed, Report Designer creates the report and adds it to the report server project.  
  
## To add a new blank report  
  
1.  From the **Project** menu, click **Add New Item**.  
  
2.  In **Templates**, click **Report**.  
  
3.  Click **Add**.  
  
     A new blank report is added to the project and displayed on the design surface.  
  
## To add an existing report  
  
1.  From the **Project** menu, click **Add**, and then  **Existing Item**.  
  
2.  Navigate to the location of the .rdl file, select it, and then click **Add**.  
  
     The report is added to the project under the **Reports** folder. When you close and re-open the project, reports are sorted alphabetically.  
  
## See Also  
 [Reporting Services Tutorials &#40;SSRS&#41;](../../reporting-services/reporting-services-tutorials-ssrs.md)  
 More questions? [Try the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
  
  
