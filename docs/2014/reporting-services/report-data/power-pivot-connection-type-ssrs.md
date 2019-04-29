---
title: "PowerPivot Connection Type (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a104c3c7-f118-4d02-9a0f-6859f1469d11
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# PowerPivot Connection Type (SSRS)
  You can use SQL Server Analysis Services data processing extension to retrieve data from a PowerPivot workbook that is published in a SharePoint PowerPivot Gallery.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
## Prerequisites  
 The PowerPivot data source must be published in a PowerPivot Gallery on a SharePoint site.  
  
 To support connections from Report Builder to a PowerPivot workbook, you must have SQL Server 2008 R2 ADOMD.NET on your workstation computer. This client library is installed with PowerPivot for Excel, but if you are using a computer that does not have this application, you must download and install ADOMD.NET from the [SQL Server 2008 R2 Feature Pack](https://go.microsoft.com/fwlink/?LinkId=192565).  
  
## Data Source Type  
 Use report data source type **Microsoft SQL Server Analysis Services**.  
  
## Connection String  
 The connection string is the URL to PowerPivot workbook published on SharePoint in the PowerPivot Gallery or other library, for example, http://contoso-srv/subsite/PowerPivotLibrary/ContosoSales.xlsx.  
  
## Credentials  
 Specify the credentials that you need to access the PowerPivot workbook and SharePoint site, for example, Windows Authentication (Integrated Security). For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md) or [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  
## Queries  
 After you connect to the PowerPivot data source, use the MDX graphical query to build a query by browsing and selecting from the underlying data structures. After you build a query, run the query to see sample data in the results pane.  
  
 The query designer analyzes the query to determine the dataset fields. You can also manually edit the dataset field collection in the **Report Data** pane. For more information, see [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).  
  
## Filters  
 In the Filters pane, specify dimensions and members to filter out or to include in the query results.  
  
## Parameters  
 In the Filters pane, select the **Parameters** option for a filter to automatically create a report parameter with available values that correspond to the filter selections.  
  
## Remarks  
 If you open Report Builder from the PowerPivot workbook in a PowerPivot Gallery, the PivotTables, PivotCharts, slicers, and other layout and analytical features from the PowerPivot workbook are not re-created in the report. Instead, the blank report includes a preconfigured data source that points to the data in the PowerPivot workbook. Designing reports based on a PowerPivot workbook can be labor-intensive and time-consuming depending on the number of slicers, filters, and tables or charts that you want to re-create in the report. A better approach is to envision the presentation of the data that you want in a report independently from the PowerPivot design.  
  
 The data in a PowerPivot workbook is highly compressed; data retrieved from the PowerPivot workbook for a report is not compressed. Use the query designer to specify filters and parameters to limit the data to just what is needed in the report.  
  
 Unlike connecting to an Analysis Services cube, a PowerPivot model has no hierarchies. To provide similar functionality to related slicers in the workbook, you must create cascading parameters in the report. For more information, see [Add Cascading Parameters to a Report &#40;Report Builder and SSRS&#41;](../report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md).  
  
 In some cases, you might need to adjust expressions to accommodate the underlying data values from the PowerPivot model. You might need to modify expressions to convert data to the right data type or to add or remove an aggregate function. For example, to convert data type from String to Integer, use `=CInt`. Always verify that the report displays the expected values from the data in the PowerPivot model before you publish the report.  
  
 Preview images of a report in a PowerPivot Gallery are generated only if the following conditions are met:  
  
-   The report and the PowerPivot workbook that provides the data must be stored together in the same PowerPivot Gallery.  
  
-   The report contains only PowerPivot data from a PowerPivot data source.  
  
## See Also  
 [Analysis Services MDX Query Designer User Interface &#40;Report Builder&#41;](../analysis-services-mdx-query-designer-user-interface-report-builder.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../report-design/expressions-report-builder-and-ssrs.md)  
  
  
