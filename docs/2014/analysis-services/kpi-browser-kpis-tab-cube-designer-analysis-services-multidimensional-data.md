---
title: "KPI Browser (KPIs Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.kpibrowserpane.f1"
ms.assetid: 2f61bde6-e6ec-4511-8645-c272374014ad
author: minewiskan
ms.author: owend
manager: craigg
---
# KPI Browser (KPIs Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **KPI Browser** pane on the **KPIs** tab in Cube Designer to view and test the results of Key Performance Indicators (KPIs). KPIs must first be deployed to a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance before browsing.  
  
> [!NOTE]  
>  This pane is displayed only in browser view.  
  
## Options  
 **Subcube grid**  
 Use to define a subcube and restrict the results of the KPIs to be displayed in the **Results** pane. The grid contains the following columns:  
  
 **Dimension**  
 Select the dimension to which this filter applies.  
  
 **Hierarchy**  
 Select the hierarchy to which this filter applies.  
  
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
 Type the expression that is to be evaluated by **Operator**, which restricts the KPI results to be browsed.  
  
> [!NOTE]  
>  This field is a dynamic data entry element, changing appearance to reflect the types of data necessary for the selected operator.  
  
 **Results grid**  
 Displays the evaluated value, goal, status, and trend for the KPIs, based on the filter defined in **Filter grid**. The grid contains the following columns:  
  
 **Display Structure**  
 Displays the KPIs contained by the cube, hierarchically organized according to the **Display folder** or **Parent KPI** values for each KPI.  
  
 **Value**  
 Displays the value of the KPI.  
  
 **Goal**  
 Displays the goal value of the KPI.  
  
 **Status**  
 Displays the status graphic of the KPI.  
  
 **Trend**  
 Displays the trend graphic of the KPI.  
  
 **Weight**  
 Displays the weight factor of the KPI.  
  
 **(Description)**  
 Displays an information icon if a description is provided for a KPI.  
  
 Hover the mouse over the information icon to display a ToolTip containing the description for the KPI.  
  
> [!NOTE]  
>  If an error occurs while calculating a KPI on the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance, this option displays information associated with the error.  
  
## See Also  
 [KPIs &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](kpis-cube-designer-analysis-services-multidimensional-data.md)  
  
  
