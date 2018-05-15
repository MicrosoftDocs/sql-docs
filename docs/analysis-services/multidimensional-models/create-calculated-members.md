---
title: "Create Calculated Members | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create Calculated Members
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can create customized measures or dimension members, called calculated members, by combining cube data, arithmetic operators, numbers, and functions. For example, you can create a calculated member named Euros that converts dollars to euros by multiplying an existing dollar measure by a conversion rate. Euros can then be displayed to end users in a separate row or column.  
  
 Calculated member definitions are stored, but their values exist only in memory. In the preceding example, values in marks are displayed to end users but are not stored as cube data.  
  
 You create calculated members in cubes. To create a calculated member, in Cube Designer, on the **Calculations** tab, click the **New Calculated Member** icon on the toolbar. This command displays a form to specify the following options for the calculated member:  
  
 **Name**  
 Select the name of the calculated member. This name appears as the column or row heading for the calculated member values when end users browse the cube.  
  
 **Parent hierarchy**  
 Select the parent hierarchy to include in the calculated member. Hierarchies are descriptive categories of a dimension by which the numeric data (that is, measures) in a cube can be separated for analysis. In tabular browsers, hierarchies provide the column and row headings displayed to end users when they browse data in a cube. (In graphical browsers, they provide other types of descriptive labels, but they have the same function as in tabular browsers.) A calculated member provides a new heading (or label) in the parent dimension you select.  
  
 Alternatively, you can include the calculated member in the measures instead of in a dimension. This option also provides a new column or row heading, but it is attached to measures in the browser.  
  
 **Parent member**  
 Click **Change** to select a parent member to include the calculated member. This option is unavailable if you select a one-level hierarchy or MEASURES as the parent dimension.  
  
 Hierarchies are divided into levels that contain members. Each member produces a heading. While browsing data in a cube, end users can drill down from a selected heading to previously undisplayed subordinate headings. The heading for the calculated member is added at the level directly below the parent member you select.  
  
 **Expression**  
 Specify the expression that produces the values of the calculated member. This expression can be written in Multidimensional Expressions (MDX). The expression can contain any of the following:  
  
-   Data expressions that represent cube components such as dimensions, levels, measures, and so on  
  
-   Arithmetic operators  
  
-   Numbers  
  
-   Functions  
  
 You can drag or copy cube components from the **Metadata** tab of the **Calculation Tools** pane to quickly add them to an expression.  
  
> [!IMPORTANT]  
>  Any calculated member that is to be used in the value expression of another calculated member must be created before the calculated member that uses it.  
  
 Format String  
 Specifies the format of cell values that are based on the calculated member. This property accepts the same values as the **Display Format** property for measures. For more information about display formats, see [Configure Measure Properties](../../analysis-services/multidimensional-models/configure-measure-properties.md).  
  
 Visible  
 Determines whether the calculated member is visible or hidden when cube metadata is retrieved. If the calculated member is hidden, it can still be used in MDX expressions, statements, and scripts, but it is not displayed as a selectable object in client user interfaces.  
  
 Non-empty Behavior  
 Stores the names of measures used to resolve NON EMPTY queries in MDX. If this property is blank, the calculated member must be evaluated repeatedly to determine whether a member is empty. If this property contains the name of one or more measures, the calculated member is treated as empty if all the specified measures are empty. This property is an optimization hint to Analysis Services to return only non-NULL records. Returning only non-NULL records improves the performance of MDX queries that utilize the NON EMPTY operator or the NonEmpty function, or that require the calculation of cell values. For best performance with cell calculations, specify only a single member when possible.  
  
 Color Expressions  
 Specifies MDX expressions that dynamically set the foreground and background colors of cells based on the value of the calculated member. This property is ignored if unsupported by client applications.  
  
 Font Expressions  
 Specifies MDX expressions that dynamically set the font, font size, and font attributes for cells based on the value of the calculated member. This property is ignored if unsupported by client applications.  
  
 You can copy or drag cube components from the **Metadata** tab of the **Calculation Tools** pane to the **Expression** box in the Calculation Expressions pane. You can copy or drag functions from the **Functions** tab in the **Calculation Tools** pane to the **Expression** box in the Calculation Expressions pane.  
  
## Addressing Calculated Members  
 When you create a calculated member on the **Calculations** tab of **Cube Designer**, you specify the parent hierarchy in which the calculated member is stored. The parent hierarchy determines how a calculated member can be addressed, according to the following rules:  
  
-   If a calculated member is created in the measures dimension, the calculated member is addressable in that dimension.  
  
## See Also  
 [Calculations in Multidimensional Models](../../analysis-services/multidimensional-models/calculations-in-multidimensional-models.md)  
  
  
