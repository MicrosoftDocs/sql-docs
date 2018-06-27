---
title: "Define a New Attribute Automatically | Microsoft Docs"
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
# Attribute Properties - Define a New Attribute Automatically
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can create a new attribute in a dimension by using drag-and-drop editing in the Dimension Designer.  
  
### To create a new attribute automatically  
  
1.  In Dimension Designer, open the dimension in which you want to create the attribute.  
  
2.  On the **Dimension Structure** tab, in the **Data Source View** pane, in the table to which you want to bind the attribute, select the column, and then drag the column to the **Attributes** pane.  
  
     [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] creates the new attribute that has the same name as the column to which it is bound. If there are multiple attributes that use the same column, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] appends a number to the attribute name.  
  
## See Also  
 [Dimensions in Multidimensional Models](../../analysis-services/multidimensional-models/dimensions-in-multidimensional-models.md)   
 [Dimension Attribute Properties Reference](../../analysis-services/multidimensional-models/dimension-attribute-properties-reference.md)  
  
  
