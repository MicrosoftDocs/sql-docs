---
title: "Query and Filter (Browser Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.browsecube.filterpane.f1"
ms.assetid: f5cf0bb1-3afb-4856-a2ef-614deb4e7e49
author: minewiskan
ms.author: owend
manager: craigg
---
# Query and Filter (Browser Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  This area of the **Browser** tab in Cube Designer contains a query and filter area, to help you choose data from the cube to use in browsing or in queries. You can add as many cube objects as you want, and then view the results in the data area, or export the results to a report using Analyze in Excel to visualize how the data would be viewed by end users.  
  
> [!WARNING]  
>  When you are working with data in this area, by default the **Browser** uses the graphical design mode. However, you can edit the query directly using MDX, by clicking the **Design Mode** toggle button. When you do so, the pane that lets you design filters on dimensions disappears. If you want to add a filter, you can switch back to graphical design mode.  
  
 By default, the credentials of the current user, not the credentials specified in the **Impersonation Information** page, are used to connect to the data source when a query is executed. However, you can also change the user context for the query or report by clicking **Change User** on the **Toolbar**.  
  
## Options  
 **Dimension**  
 Select the dimension on which to slice the subcube.  
  
 **Hierarchy**  
 Select the hierarchy on which to slice the subcube.  
  
 **Operator**  
 Select the operator that defines how the expression in **Filter Expression** is applied to the selected hierarchy. The following table describes the available operators.  
  
|Value|Description|  
|-----------|-----------------|  
|**Equal**|The results are restricted to the set defined in **Filter Expression**.|  
|**Not Equal**|The results are restricted to the members excluded by the set defined in **Filter Expression**.|  
|**In**|The results are restricted to the named set chosen in **Filter Expression**.|  
|**Not In**|The results are restricted to the members excluded by the named set chosen in **Filter Expression**.|  
|**Contains**|The results are restricted to members whose member names contain the string in **Filter Expression**.|  
|**Begins With**|The results are restricted to members whose member names begin with the string in **Filter Expression**.|  
|**Range (Inclusive)**|The results are restricted to the range chosen in **Filter Expression**.|  
|**Range (Exclusive)**|The results are restricted to the members excluded by the range chosen in **Filter Expression**.|  
|**MDX**|The results are restricted to the Multidimensional Expressions (MDX) expression set in **Filter Expression**.|  
  
 **Filter Expression**  
 Type the expression that is to be evaluated by **Operator**, which restricts the results to be browsed.  
  
> [!NOTE]  
>  This field is a dynamic data entry element, changing appearance to reflect the types of data necessary for the selected operator.  
  
## See Also  
 [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](cube-designer-analysis-services-multidimensional-data.md)   
 [Browser &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](browser-cube-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Browser Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-browser-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Analyze in Excel &#40;Browser Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](analyze-in-excel-browser-cube-designer-analysis-services-multidimensional-data.md)   
 [Metadata &#40;Browser Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](metadata-browser-tab-cube-designer-analysis-services-multidimensional-data.md)  
  
  
