---
title: "Level Naming Template Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.levelnamingtemplatedialog.f1"
helpviewer_keywords: 
  - "Level Naming Template dialog box"
ms.assetid: 96cad715-213e-4eac-9003-130a2f5fc985
author: minewiskan
ms.author: owend
manager: craigg
---
# Level Naming Template Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Level Naming Template** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to construct the level naming template for a parent attribute in a dimension. For more information about level naming templates, see [NamingTemplate Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/namingtemplate-element-assl). You can display the **Level Naming Template** dialog box by clicking the ellipsis button (**...**) on the `NamingTemplate` value of a translation for an attribute in the **Translation Details** pane on the **Translations** tab of **Dimension Designer**.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Define the level template**|Displays a grid in which you can design the hierarchy of levels in the parent attribute. The grid contains the following columns:<br /><br /> **Level**: Displays the ordinal position of the level for which the name specified in **Name** is used. To add a new naming template for a level, select **Name** on the row that contains an asterisk (\*) in **Level**.<br /><br /> **Name**: Contains the naming template used for the level indicated in **Level**. To add a placeholder for the level ordinal position in the naming template, add a single asterisk (*). To add an asterisk as part of the name created by the naming template, add two asterisks (\*\*).|  
|**Clear All**|Select to remove all rows in **Define the level template**.|  
|**Result**|Displays the level naming template constructed by the dialog box.|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Translations &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](translations-dimension-designer-analysis-services-multidimensional-data.md)  
  
  
