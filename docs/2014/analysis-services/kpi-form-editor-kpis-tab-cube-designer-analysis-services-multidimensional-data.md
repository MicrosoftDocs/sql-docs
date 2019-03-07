---
title: "KPI Form Editor (KPIs Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.kpidefinitionpane.f1"
ms.assetid: 45c6453a-bbe2-4ca5-836e-c7ef11cfcb65
author: minewiskan
ms.author: owend
manager: craigg
---
# KPI Form Editor (KPIs Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **KPI Form Editor** pane on the **KPIs** tab in Cube Designer to create or modify the selected Key Performance Indicator (KPI).  
  
> [!NOTE]  
>  This pane is displayed only in form view.  
  
## Options  
 **Name**  
 Type the name of the KPI.  
  
 **Associated measure group**  
 Select the measure group associated with the KPI. The client application can use this information to determine which dimensions are available when the user browses this KPI.  
  
 **Value Expression**  
 Expand to view or edit the Multidimensional Expressions (MDX) expression for the value of the KPI.  
  
 Type the MDX expression that returns the value of the KPI.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 **Goal Expression**  
 Expand to view or edit the MDX expression for the goal value of the KPI.  
  
 Type the MDX expression that returns the goal value of the KPI when the KPI is run.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 **Status**  
 Expand to view the **Status graphic** and **Status expression** options.  
  
 **Status graphic**  
 Select the graphic to be used by the client application to represent the status value in graphical form.  
  
> [!NOTE]  
>  The client application can support the selected graphic or replace it with an appropriate alternative.  
  
 **Status expression**  
 Type the MDX expression that returns the status value of the KPI when the KPI is run.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 It is recommended that this expression returns a decimal number between -1 and 1. A lower number represents a negative situation, while a higher number represents a positive situation.  
  
> [!NOTE]  
>  Values below -1 and above 1 are possible but may not be interpreted correctly by third-party client applications.  
  
 **Trend**  
 Expand to view the **Trend graphic** and **Trend expression** options.  
  
 **Trend graphic**  
 Select the graphic to be used by the client application to represent the trend value in graphical form.  
  
> [!NOTE]  
>  The client application can support the selected graphic or replace it with an appropriate alternative.  
  
 **Trend expression**  
 Type the MDX expression that returns the trend value of the KPI.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 The trend expression can be based on any time-based criteria that makes sense in a given business context. It is recommended that this expression returns a decimal number between -1 and 1. A lower number represents a negative trend over time; a higher number represents a positive trend over time.  
  
> [!NOTE]  
>  Values below -1 and above 1 are possible but may not be interpreted correctly by third-party client applications.  
  
 **Additional Properties**  
 Expand to view the **Display folder**, **Parent KPI**, **Current time member**, **Weight**, and **Description** options.  
  
 **Display folder**  
 Type the categorization of the KPI for use by the client application for display purposes.  
  
 Use a backward slash (\\) to separate folder names in a display folder and a semicolon (;) to separate multiple display folders. For example, type `Category\Goal\Scientific;Category\Goal\Metric`.  
  
 **Parent KPI**  
 Select an existing KPI under which to categorize the KPI for use by the client application.  
  
> [!NOTE]  
>  If this option is set to an existing KPI, **Display folder** is ignored.  
  
 **Current time member**  
 Type the MDX expression that returns the member that identifies the temporal context of the KPI.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
> [!IMPORTANT]  
>  The MDX expression must return the unique name of a member within a time dimension associated with the measure group specified in **Associated measure group**.  
  
 **Weight**  
 Expand to view or edit the MDX expression for the weighting factor of the KPI.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 **Description**  
 Type the optional description of the KPI.  
  
## See Also  
 [KPIs &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](kpis-cube-designer-analysis-services-multidimensional-data.md)  
  
  
