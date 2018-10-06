---
title: "Calculation Tools (Actions Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.actionsview.calculationtoolspane.f1"
ms.assetid: a3370370-43cd-4cc2-bb9f-c0d988b96f05
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculation Tools (Actions Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Calculation Tools** pane on the **Actions** tab in Cube Designer to explore metadata, functions, and templates available for use in actions, drillthrough actions, and report actions.  
  
## Options  
 **Metadata**  
 Displays the metadata for the selected cube.  
  
 Drag a selected element to the **Action Form Editor**, **Drillthrough Action Form Editor**, or **Report Action Form Editor** pane to include the Multidimensional Expressions (MDX) syntax for that element at the selected location in the pane.  
  
 **Functions**  
 Displays the functions available for expressions and conditions.  
  
 Drag a selected element to the **Action Form Editor**, **Drillthrough Action Form Editor**, or **Report Action Form Editor** pane to include the MDX syntax for that element at the selected location in the pane.  
  
> [!NOTE]  
>  In project mode, the **Calculation Tools** dialog box reads information for this option from an XML file named MDXFunctions.xml, included with [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. In online mode, information for this option is retrieved from the MDSCHEMA_FUNCTIONS schema rowset for the instance.  
  
 **Templates**  
 Displays the predefined templates available for actions.  
  
 Drag a selected element to the **Action Form Editor**, **Drillthrough Action Form Editor**, or **Report Action Form Editor** pane to include the MDX syntax for that element at the selected location in the pane.  
  
## Context Menu  
 The following options are available in the context menu displayed by right-clicking an element in the **Calculation Tools** pane:  
  
|Option|Definition|  
|------------|----------------|  
|**Copy**|Select to copy the selected element in **Metadata** or **Functions** to the Clipboard.<br /><br /> Note that this option is not displayed if **Templates** is selected. Also note that this option is disabled if the selected member cannot be copied, such as the **Sets** folder of a dimension displayed in **Metadata** or the function group folder for a function displayed in **Functions**.|  
|**Filter Members**|Select to display the **Filter Members** dialog box and filter members displayed for the selected element in **Metadata**. For more information about the **Filter Members** dialog box, see [Filter Members Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](filter-members-dialog-box-analysis-services-multidimensional-data.md).<br /><br /> Note that this option is displayed only if **Metadata** is selected. Also note that this option is enabled only if a level for an attribute is selected in **Metadata**.|  
|**Add Template**|Select to add a new action, drillthrough action, or report action based on the selected template to the cube and display, respectively, the **Action Form Editor**, **Drillthrough Action Form Editor**, or **Report Action Form Editor**.<br /><br /> Note: This option is displayed only if **Metadata** is selected.|  
  
## See Also  
 [MDX Scripting Fundamentals &#40;Analysis Services&#41;](multidimensional-models/mdx/mdx-scripting-fundamentals-analysis-services.md)   
 [Actions &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](actions-cube-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-actions-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Action Organizer &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](action-organizer-cube-designer-analysis-services-multidimensional-data.md)   
 [Action Form Editor &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](action-form-editor-cube-designer-analysis-services-multidimensional-data.md)   
 [Drillthrough Action Form Editor &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](drillthrough-action-form-editor-cube-designer-analysis-services-multidimensional-data.md)   
 [Report Action Form Editor &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](report-action-form-editor-cube-designer-analysis-services-multidimensional-data.md)  
  
  
