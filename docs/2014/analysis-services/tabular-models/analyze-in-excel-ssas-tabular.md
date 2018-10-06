---
title: "Analyze in Excel (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 2f17b4df-eea2-48c7-a1f2-a3fb7748c15f
author: minewiskan
ms.author: owend
manager: craigg
---
# Analyze in Excel (SSAS Tabular)
  The Analyze in Excel feature, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], provides tabular model authors a way to quickly analyze model projects during development. The Analyze in Excel feature opens Microsoft Excel, creates a data source connection to the model workspace database, and automatically adds a PivotTable to the worksheet. Workspace database objects (tables, columns, and measures) are included as fields in the PivotTable Field List. Objects and data can then be viewed within the context of the effective user or role and perspective.  
  
 This topic assumes you are already familiar with Microsoft Excel, PivotTables, and PivotCharts. To learn more about using Excel, see Excel Help.  
  
 Sections in this topic:  
  
-   [Benefits](#bkmk_benefits)  
  
-   [Related Tasks](#bkmk_rt)  
  
##  <a name="bkmk_benefits"></a> Benefits  
 The Analyze in Excel feature provides model authors the ability to test the efficacy of a model project by using the common data analysis application, Microsoft Excel. In order to use the Analyze in Excel feature, you must have Microsoft Office 2003 or later installed on the same computer as [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
> [!NOTE]  
>  If Office is not installed on the same computer as [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], you can use Excel on another computer to connect to the workspace database as a data source. You can then manually add a PivotTable to the worksheet.  
  
 The Analyze in Excel feature opens Excel and creates a new Excel workbook (.xls). A data connection from the workbook to the model workspace database is created. A blank PivotTable is added to the worksheet and model object metadata is populated in the PivotTable Field list. You can then add viewable data and slicers to the PivotTable.  
  
 When using the Analyze in Excel feature, by default, the user account currently logged on is the effective user. This account is typically an Administrator with no view restrictions to model objects or data. You can, however, specify a different effective username or role. This allows you to browse a model project in Excel within the context of a specific user or role. Specifying the effective user includes the following options:  
  
 **Current Windows User**  
 Uses the user account with which you are currently logged on.  
  
 **Other Windows User**  
 Uses a specified Windows username rather than the user currently logged on. Using a different Windows user does not require a password. Objects and data can only be viewed in Excel within the context of the effective username. No changes to model objects or data can be made in Excel.  
  
 **Role**  
 A role is used to define user permissions on the object metadata and data. Roles are usually defined for a particular Windows user or Windows user group. Certain roles can include additional row-level filters defined in a DAX formula. When using the Analyze in Excel feature, you can optionally select a role to be used. Object metadata and data views will be restricted by the permission and filters defined for the role. For more information, see [Create and Manage Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md).  
  
 In addition to the effective user or role, you can specify a perspective. Perspectives enable model authors to define particular business scenario views of model objects and data. By default, no perspective is used. In order to use a perspective with Analyze in Excel, perspectives must already be defined by using the Perspectives dialog box in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. If a perspective is specified, the PivotTable Field List will contain only those objects selected in the perspective. For more information, see [Create and Manage Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md).  
  
##  <a name="bkmk_rt"></a> Related Tasks  
  
|**Topic**|**Description**|  
|---------------|---------------------|  
|[Analyze a Tabular Model in Excel &#40;SSAS Tabular&#41;](analyze-a-tabular-model-in-excel-ssas-tabular.md)|This topic describes how to use the Analyze in Excel feature in the model designer to open Excel, create a data source connection to the model workspace database, and add a PivotTable to the worksheet.|  
  
## See Also  
 [Analyze a Tabular Model in Excel &#40;SSAS Tabular&#41;](analyze-a-tabular-model-in-excel-ssas-tabular.md)   
 [Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md)   
 [Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)  
  
  
