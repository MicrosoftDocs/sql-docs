---
title: "Analyze in Excel (Browser Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.browsecube.datapane.f1"
  - "sql12.asvs.ssms.analyzeinexcel.f1"
ms.assetid: 890ed457-137e-44ac-9b2c-83344a1b8fc9
author: minewiskan
ms.author: owend
manager: craigg
---
# Analyze in Excel (Browser Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  **Analyze in Excel** provides the cube developer with a way to quickly review how a project would look to the end user. The **Analyze in Excel** feature opens Microsoft Excel, creates a data source connection to the workspace database, and automatically adds a PivotTable to the worksheet. This feature replaces the Office Web Control that provided an embedded PivotTable in the Browser tab in previous releases.  
  
 **To view cube data:**  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], in Solution Explorer, double-click a cube to open it in Cube Designer.  
  
2.  Click the **Browser** tab.  
  
3.  Click **Reconnect** to validate the connection.  
  
4.  Click the Excel icon in the menu bar.  
  
5.  When asked to enable data connections, click **Enable**. Excel opens using the current data connection, adding a PivotTable to the worksheet so that you can begin browsing your data.  
  
 You can now build a PivotTable interactively by dragging measures from the fact table to the Values area, and dimension attributes to the Row and Column areas. If you have hierarchies, add them to Rows or Column areas. You can roll up or drill down the hierarchy to browse fact data at different levels.  
  
 Objects and data are viewed within the context of the effective user or role and perspective. When using Excel, the credentials of the current user, not the credentials specified in the **Impersonation Information** page, are used to connect to the data source when a query is executed.  
  
> [!NOTE]  
>  To use the Analyze in Excel feature, Excel must be installed on the same computer as [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. If Excel is not installed on the same computer, you can use Excel on another computer and connect to the cube as a data source. You can then manually add a PivotTable to the worksheet. Model objects (tables, columns, measures, and KPIs) are included as fields in the PivotTable field list.  
  
 For more information about the **Analyze in Excel** feature, see these resources:  
  
 [Analyze in Excel &#40;SSAS Tabular&#41;](tabular-models/analyze-in-excel-ssas-tabular.md)  
  
 [Analyze a Tabular Model in Excel &#40;SSAS Tabular&#41;](tabular-models/analyze-a-tabular-model-in-excel-ssas-tabular.md)  
  
 [Browse data and metadata in Cube](multidimensional-models/browse-data-and-metadata-in-cube.md)  
  
## See Also  
 [Browser &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](browser-cube-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Browser Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-browser-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Metadata &#40;Browser Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](metadata-browser-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Query and Filter &#40;Browser Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](query-filter-browser-cube-designer-analysis-services-multidimensional-data.md)  
  
  
