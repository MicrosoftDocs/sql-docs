---
title: "Power Pivot Connection Type (SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
ms.assetid: a104c3c7-f118-4d02-9a0f-6859f1469d11
author: maggiesMSFT
ms.author: maggies
---
# Power Pivot Connection Type (SSRS)
  You can use SQL Server Analysis Services data processing extension to retrieve data from a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook that is published in a SharePoint [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
## Prerequisites  
 The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data source must be published in a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery on a SharePoint site.  
  
 To support connections from Report Builder to a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook, you must have SQL Server 2008 R2 ADOMD.NET on your workstation computer. This client library is installed with [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel, but if you are using a computer that does not have this application, you must download and install ADOMD.NET from the [SQL Server 2008 R2 Feature Pack](https://go.microsoft.com/fwlink/?LinkId=192565).  
  
## Data Source Type  
 Use report data source type **Microsoft SQL Server Analysis Services**.  
  
## Connection String  
 The connection string is the URL to [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook published on SharePoint in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery or other library, for example, `https://contoso-srv/subsite/PowerPivotLibrary/ContosoSales.xlsx`.  
  
## Credentials  
 Specify the credentials that you need to access the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook and SharePoint site, for example, Windows Authentication (Integrated Security). For more information, see [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md) or [Specify Credentials in Report Builder](https://msdn.microsoft.com/library/7412ce68-aece-41c0-8c37-76a0e54b6b53).  
  
## Queries  
 After you connect to the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data source, use the MDX graphical query to build a query by browsing and selecting from the underlying data structures. After you build a query, run the query to see sample data in the results pane.  
  
 The query designer analyzes the query to determine the dataset fields. You can also manually edit the dataset field collection in the **Report Data** pane. For more information, see [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).  
  
## Filters  
 In the Filters pane, specify dimensions and members to filter out or to include in the query results.  
  
## Parameters  
 In the Filters pane, select the **Parameters** option for a filter to automatically create a report parameter with available values that correspond to the filter selections.  
  
## Remarks  
 If you open Report Builder from the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook in a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery, the PivotTables, PivotCharts, slicers, and other layout and analytical features from the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook are not re-created in the report. Instead, the blank report includes a preconfigured data source that points to the data in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook. Designing reports based on a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook can be labor-intensive and time-consuming depending on the number of slicers, filters, and tables or charts that you want to re-create in the report. A better approach is to envision the presentation of the data that you want in a report independently from the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] design.  
  
 The data in a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook is highly compressed; data retrieved from the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook for a report is not compressed. Use the query designer to specify filters and parameters to limit the data to just what is needed in the report.  
  
 Unlike connecting to an Analysis Services cube, a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] model has no hierarchies. To provide similar functionality to related slicers in the workbook, you must create cascading parameters in the report. For more information, see [Add Cascading Parameters to a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md).  
  
 In some cases, you might need to adjust expressions to accommodate the underlying data values from the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] model. You might need to modify expressions to convert data to the right data type or to add or remove an aggregate function. For example, to convert data type from String to Integer, use `=CInt`. Always verify that the report displays the expected values from the data in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] model before you publish the report.  
  
 Preview images of a report in a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery are generated only if the following conditions are met:  
  
-   The report and the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook that provides the data must be stored together in the same [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery.  
  
-   The report contains only [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data from a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data source.  
  
## See Also  
 [Analysis Services MDX Query Designer User Interface &#40;Report Builder&#41;](https://msdn.microsoft.com/library/7e288eee-2d37-485e-a6a0-dbba5e041e26)   
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)  
  
  
