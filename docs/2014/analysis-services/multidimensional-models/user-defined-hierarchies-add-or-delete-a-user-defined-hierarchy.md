---
title: "Add or Delete a User-Defined Hierarchy | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "hierarchies [Analysis Services], adding"
  - "removing hierarchies"
  - "adding hierarchies"
  - "deleting hierarchies"
  - "hierarchies [Analysis Services], removing"
ms.assetid: 953818b4-9543-4c01-bb20-1d45ec6dfb91
author: minewiskan
ms.author: owend
manager: craigg
---
# Add or Delete a User-Defined Hierarchy
  You add a user-defined hierarchy to or remove a user-defined hierarchy from a dimension on the **Dimension Structure** tab in Dimension Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 When you add a user-defined hierarchy, it is not available to users until it is instantiated in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and the dimension is processed. For more information, see [Multidimensional Model Databases &#40;SSAS&#41;](multidimensional-model-databases-ssas.md) and [Multidimensional Model Object Processing](processing-a-multidimensional-model-analysis-services.md).  
  
### To add a user-defined hierarchy to a dimension  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the appropriate [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, and then open the dimension with which you want to work.  
  
     The dimension opens in Dimension Designer on the **Dimension Structure** tab.  
  
2.  From the **Attributes** pane, drag an attribute that you want to use in the user-defined hierarchy to the **Hierarchies** pane.  
  
3.  Drag additional attributes to form levels in the user-defined hierarchy.  
  
     The order in which attributes are listed in the hierarchy is important. For example, in a time hierarchy, years should appear higher in the hierarchy list than days.  
  
4.  Optionally, rearrange the levels in the user-defined hierarchy by dragging them to the correct positions.  
  
5.  Optionally, modify properties of the user-defined hierarchy or its levels.  
  
     For example, you might want to specify a name for the user-defined hierarchy, rename one or more of its levels, and define a custom name for the All level. For more information, see [User Hierarchy Properties](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md), and [Level Properties &#91;Paved Over&#93;](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies-level-properties.md).  
  
    > [!NOTE]  
    >  By default, a user-defined hierarchy is just a path that enables users to drill down for information. However, if relationships exist between levels, you can increase query performance by configuring attribute relationships between levels. For more information, see [Attribute Relationships](../multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md) and [Define Attribute Relationships](attribute-relationships-define.md).  
  
### To remove a user-defined hierarchy from a dimension  
  
-   On the **Dimension Structure** tab, click the user-defined hierarchy that you want to remove in the **Hierarchies** pane. On the toolbar, click **Delete**.  
  
     - or -  
  
-   Right-click the user-defined hierarchy that you want to remove in the **Hierarchies** pane and then click **Delete**.  
  
     - or -  
  
-   Drag the user-defined hierarchy off of the design surface.  
  
## See Also  
 [User Hierarchies](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies.md)   
 [Create User-Defined Hierarchies](user-defined-hierarchies-create.md)  
  
  
