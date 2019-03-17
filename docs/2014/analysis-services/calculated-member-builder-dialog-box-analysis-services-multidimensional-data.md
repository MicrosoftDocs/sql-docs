---
title: "Calculated Member Builder Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.calculatedmemberbuilderdialog.f1"
ms.assetid: 73b89a9f-f403-4ab8-99f7-e3ceb870c260
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculated Member Builder Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Calculated Member Builder** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to build a calculated member.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Name**|Type the name of the calculated member.|  
|**Parent Hierarchy**|Select the hierarchy in which to create the calculated member.|  
|**Parent Member**|This option is enabled if you select a parent hierarchy (other than the `Measures` dimension) that has more than one level. Click the ellipsis (**...**) button to select a parent member. The parent member determines the location of the calculated member in the dimension structure.|  
|**Expression**|Type the MDX expression that will be used.|  
|**Check**|Click **Check** to test the MDX expression defined in **Expression**.|  
|**Metadata**|Displays metadata for the current [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] object that can be included in the MDX expression defined in **Expression**.<br /><br /> You can copy the MDX syntax for the selected item by right-clicking the item and selecting **Copy**, or by dragging the selected item to **Expression**.|  
|**Functions**|Displays the available MDX functions for the current [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance. The items listed are retrieved from the MDSCHEMA_FUNCTIONS schema rowset.<br /><br /> You can copy the MDX syntax for the selected item by right-clicking the item and selecting **Copy**, or by dragging the selected item to **Expression**.|  
  
## See Also  
 [Multidimensional Expressions &#40;MDX&#41; Reference](/sql/mdx/multidimensional-expressions-mdx-reference)  
  
  
