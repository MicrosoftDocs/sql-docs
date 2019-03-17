---
title: "Define a New Attribute Automatically | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "automatic attribute creation"
  - "attributes [Analysis Services], creating"
ms.assetid: 208a050a-5e2f-470c-b645-8d835e123db7
author: minewiskan
ms.author: owend
manager: craigg
---
# Define a New Attribute Automatically
  You can create a new attribute in a dimension by using drag-and-drop editing in the Dimension Designer.  
  
### To create a new attribute automatically  
  
1.  In Dimension Designer, open the dimension in which you want to create the attribute.  
  
2.  On the **Dimension Structure** tab, in the **Data Source View** pane, in the table to which you want to bind the attribute, select the column, and then drag the column to the **Attributes** pane.  
  
     [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] creates the new attribute that has the same name as the column to which it is bound. If there are multiple attributes that use the same column, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] appends a number to the attribute name.  
  
## See Also  
 [Dimensions in Multidimensional Models](dimensions-in-multidimensional-models.md)   
 [Dimension Attribute Properties Reference](dimension-attribute-properties-reference.md)  
  
  
