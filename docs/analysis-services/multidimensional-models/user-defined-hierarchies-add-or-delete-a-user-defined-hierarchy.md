---
title: "Add or Delete a User-Defined Hierarchy | Microsoft Docs"
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
# User-Defined Hierarchies - Add or Delete a User-Defined Hierarchy
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You add a user-defined hierarchy to or remove a user-defined hierarchy from a dimension on the **Dimension Structure** tab in Dimension Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 When you add a user-defined hierarchy, it is not available to users until it is instantiated in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and the dimension is processed. For more information, see [Multidimensional Model Databases ](../../analysis-services/multidimensional-models/multidimensional-model-databases-ssas.md) and [Processing a multidimensional model &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
### To add a user-defined hierarchy to a dimension  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the appropriate [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, and then open the dimension with which you want to work.  
  
     The dimension opens in Dimension Designer on the **Dimension Structure** tab.  
  
2.  From the **Attributes** pane, drag an attribute that you want to use in the user-defined hierarchy to the **Hierarchies** pane.  
  
3.  Drag additional attributes to form levels in the user-defined hierarchy.  
  
     The order in which attributes are listed in the hierarchy is important. For example, in a time hierarchy, years should appear higher in the hierarchy list than days.  
  
4.  Optionally, rearrange the levels in the user-defined hierarchy by dragging them to the correct positions.  
  
5.  Optionally, modify properties of the user-defined hierarchy or its levels.  
  
     For example, you might want to specify a name for the user-defined hierarchy, rename one or more of its levels, and define a custom name for the All level. For more information, see [User Hierarchy Properties](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md), and [Level Properties - user hierarchies](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies-level-properties.md).  
  
    > [!NOTE]  
    >  By default, a user-defined hierarchy is just a path that enables users to drill down for information. However, if relationships exist between levels, you can increase query performance by configuring attribute relationships between levels. For more information, see [Attribute Relationships](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md) and [Define Attribute Relationships](../../analysis-services/multidimensional-models/attribute-relationships-define.md).  
  
### To remove a user-defined hierarchy from a dimension  
  
-   On the **Dimension Structure** tab, click the user-defined hierarchy that you want to remove in the **Hierarchies** pane. On the toolbar, click **Delete**.  
  
     - or -  
  
-   Right-click the user-defined hierarchy that you want to remove in the **Hierarchies** pane and then click **Delete**.  
  
     - or -  
  
-   Drag the user-defined hierarchy off of the design surface.  
  
## See Also  
 [User Hierarchies](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies.md)   
 [Create User-Defined Hierarchies](../../analysis-services/multidimensional-models/user-defined-hierarchies-create.md)  
  
  
