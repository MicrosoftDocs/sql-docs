---
title: "Reporting Services Query Designers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "query designers [Reporting Services]"
ms.assetid: 07efd3f1-804f-45f7-b62a-3e727a3d9835
author: markingmyname
ms.author: maghan
manager: kfile
---
# Reporting Services Query Designers
  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] provides graphical and text-based query designers to help you build queries for each data source type in your report.  
  
 Some data sources support graphical designers that help you build a query interactively. Other data sources use a text-based query designer. By using a graphical query designer, you can drag metadata items that represent the underlying data on a data source to the query design surface. By using a text-based query designer, you can type command text into a query pane. You can change from a graphical query designer to a text-based query designer by clicking the text-based query designer icon on the toolbar.  
  
 The data source types that are available in your report are determined by the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data extensions installed on your client or report server. For more information, see [RSReportDesigner Configuration File](report-server/rsreportdesigner-configuration-file.md) and [RSReportServer Configuration File](report-server/rsreportserver-config-configuration-file.md).  
  
 A data processing extension and its associated query designer can differ in support for data sources in the following ways:  
  
-   **By query designer type.** For example, a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data source supports both the graphical and text-based query designers.  
  
-   **By query language variation.** For example, a query language such as [!INCLUDE[tsql](../includes/tsql-md.md)] can differ in syntax depending on the data source type. The [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[tsql](../includes/tsql-md.md)] language and the Oracle SQL language have some variation in syntax for a query command.  
  
-   **By support for the schema part of a database object name.** When a data source uses schemas as part of the database object identifier, the schema name must be supplied as part of the query for any names that do not use the default schema. For example, `SELECT FirstName, LastName FROM [Person].[Person]`.  
  
-   **By support for query parameters.** Data providers differ in support for parameters. Some data providers support named parameters; for example, `SELECT Col1, Col2 FROM Table WHERE <parameter identifier><parameter name> = <value>`. Some data providers support unnamed parameters; for example, `SELECT Col1, Col2 FROM Table WHERE <column name> = ?`. The parameter identifier might differ by data provider; for example, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses the "at" (@) symbol, Oracle uses the colon (:). Some data providers do not support parameters.  
  
-   **By ability to import queries.** For example, for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data source, you can import a query from a report definition file (.rdl) or from a .sql file.  
  
## Query Designers  
 The following topics describe the user interface for each query designer.  
  
-   [Analysis Services MDX Query Designer User Interface](report-data/analysis-services-mdx-query-designer-user-interface.md)  
  
-   [Analysis Services DMX Query Designer User Interface](report-data/analysis-services-dmx-query-designer-user-interface.md)  
  
-   [Graphical Query Designer User Interface](report-data/graphical-query-designer-user-interface.md)  
  
-   [Relational Query Designer User Interface](../../2014/reporting-services/relational-query-designer-user-interface.md)  
  
-   [Hyperion Essbase Query Designer User Interface](report-data/hyperion-essbase-query-designer-user-interface.md)  
  
-   [Report Model Query Designer User Interface](report-data/report-model-query-designer-user-interface.md)  
  
-   [SAP NetWeaver BI Query Designer User Interface](report-data/sap-netweaver-bi-query-designer-user-interface.md)  
  
-   [SharePoint List Query Designer](../../2014/reporting-services/sharepoint-list-query-designer.md)  
  
-   [Text-based Query Designer User Interface](../../2014/reporting-services/text-based-query-designer-user-interface.md)  
  
## See Also  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md)   
 [Add Data from External Data Sources &#40;SSRS&#41;](report-data/add-data-from-external-data-sources-ssrs.md)   
 [Data Processing Extensions and .NET Framework Data Providers &#40;SSRS&#41;](report-data/data-processing-extensions-and-net-framework-data-providers-ssrs.md)   
 [Extensions &#40;SSRS&#41;](extensions-ssrs.md)  
  
  
