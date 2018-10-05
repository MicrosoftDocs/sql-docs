---
title: "Measures (Cube Structure Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.cubebuilder.measurespane.f1"
ms.assetid: be70f63b-58f2-4eff-81bc-c86d8229e617
author: minewiskan
ms.author: owend
manager: craigg
---
# Measures (Cube Structure Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Measures** pane to manipulate measure groups and members on the **Cube Structure** tab in Cube Designer.  
  
## Options  
 Measures  
 Displays the measure groups and measures included in the cube, depending on the selected view:  
  
 Tree  
 Displays a tree view of measure groups, with measures as child nodes.  
  
 Expand measure groups to view measures. Click a selected measure group or measure to rename the measure group or measure, respectively.  
  
 Grid  
 Displays a grid of measures and their most commonly accessed properties. Click **Add new measure** to display the **New Measure** dialog box and add a new measure to the grid.  
  
 The grid contains the following columns:  
  
 Measure  
 Type the name of the measure.  
  
 Measure Group  
 Displays the measure group containing the measure.  
  
 Data Type  
 Select the data type of the measure.  
  
 Aggregation  
 Select the aggregation function of the measure.  
  
## Context Menu  
 The following options are available in the context menu displayed by right-clicking the **Measures** pane:  
  
 **New Measure**  
 Select to display the **New Measure** dialog box and add a new measure to the measure group currently selected in the **Measures** pane.  
  
 **New Measure Group**  
 Select to display the **New Measure Group** dialog box and add a new measure group to the **Measures** pane.  
  
 **Show Measures In**  
 Select to cycle through the following options, or select one of the following options to change the view of the **Measures** pane:  
  
|Option|Definition|  
|------------|----------------|  
|**Tree**|Display measure groups and measures in a tree view.|  
|**Grid**|Display measure groups and measures in a grid.|  
  
 **Rename**  
 Select to rename the selected measure group or measure.  
  
 **Delete**  
 Select to display the **Delete Objects** dialog box and delete the selected objects in the **Measures** pane.  
  
 **Move Up**  
 Select to move the selected measure group or measure up one place.  
  
> [!NOTE]  
>  This option is disabled if the selected item cannot be moved up.  
  
 **Move Down**  
 Select to move the selected measure group or measure down one place.  
  
> [!NOTE]  
>  This option is disabled if the selected item cannot be moved down.  
  
 **New Linked Object**  
 Click to display the **Linked Object Wizard** and link measure groups and dimensions from other cubes, and to import actions, KPIs, and calculations, to the selected cube.  
  
 **Properties**  
 Select to display the **Properties** window of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the selected measure group or measure.  
  
## See Also  
 [Configure Measure Properties](multidimensional-models/configure-measure-properties.md)   
 [Measures and Measure Groups](multidimensional-models/measures-and-measure-groups.md)  
  
  
