---
title: "Warnings (Database Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.databasedesigner.warnings.f1"
ms.assetid: 13f58b4d-f345-4fbc-ae2d-b3c8290a797d
author: minewiskan
ms.author: owend
manager: craigg
---
# Warnings (Database Designer) (Analysis Services - Multidimensional Data)
  Use the **Warnings** tab to view and dismiss rules globally, and to view and re-enable specific instances of dismissed warnings. The **Warnings** tab displays two grids: **Design Warning Rules** and **Dismissed Warnings**.  
  
 **To display the Warnings tab**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project.  
  
2.  In **Solution Explorer**, right-click the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, click **Edit Database**, and then click the **Warnings** tab.  
  
## Design Warning Rules Grid  
 The **Design Warning Rules** grid displays all the design warning rules. This grid also controls which rules are enabled for the database. To enable or disable a warning rule, select or clear its check box.  
  
 The **Design Warning Rules** grid has the following columns:  
  
 **Description**  
 Displays the name of the rule. Rules are grouped by category.  
  
 **Importance**  
 Displays the importance assigned to the rule.  
  
 **Comments**  
 (Optional) Allows the user to type a comment that explains why you are dismissing the warning.  
  
## Dismissed Warnings Grid  
 The **Dismissed Warnings** grid displays all specific warning occurrences that have been dismissed from the **Error List**. To re-enable a warning, select it in the grid, and then click **Re-enable**.  
  
 The **Dismissed Warnings** grid has the following :  
  
 **Object**  
 Displays an icon that represents the object type and the object name.  
  
 **Type**  
 Displays the object type.  
  
 **Description**  
 Displays the name of the rule.  
  
 **Importance**  
 Displays the importance assigned to the rule.  
  
 **Comments**  
 Displays the comment that was entered when the warning was dismissed. You can add or modify a comment here.  
  
 **Re-enable**  
 Re-enables the selected warnings.  
  
> [!NOTE]  
>  If an object has a warning, but the object is in an invalid state or was manually removed from the project, an error icon appears next to the warning in the list. To dismiss the warning, select it and then click **Re-enable**.  
  
## See Also  
 [Dismiss Warning Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](dismiss-warning-dialog-box-analysis-services-multidimensional-data.md)   
 [General &#40;Database Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](general-database-designer-analysis-services-multidimensional-data.md)  
  
  
