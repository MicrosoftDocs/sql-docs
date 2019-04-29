---
title: "Calculation Tools (Calculations Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.calculationsview.calculationtoolspane.f1"
ms.assetid: b1aa8a1a-6532-45d2-8f53-d3e211d7197a
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculation Tools (Calculations Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Calculation Tools** pane on the **Calculations** tab in Cube Designer to explore metadata, functions, and templates available for use in calculations.  
  
## Options  
 **Metadata**  
 Displays the metadata for the selected cube.  
  
 Drag a selected element to the Script Editor, Calculated Member Form Editor, or Named Set Form Editor pane to include the Multidimensional Expressions (MDX) syntax for that element at the selected location in the pane.  
  
 **Functions**  
 Displays the functions available for expressions and conditions.  
  
 Drag a selected element to the **Script Editor**, **Calculated Member Form Editor**, or **Named Set Form Editor** pane to include the MDX syntax for that element at the selected location in the pane.  
  
> [!NOTE]  
>  In project mode, the **Calculation Tools** dialog box reads information for this option from an XML file named MDXFunctions.xml, included with [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. In online mode, information for this option is retrieved from the MDSCHEMA_FUNCTIONS schema rowset for the instance.  
  
 **Templates**  
 Displays the predefined templates available for calculated members, named sets, and script commands.  
  
 Drag a selected element to the **Script Editor**, **Calculated Member Form Editor**, or **Named Set Form Editor** pane to include the MDX syntax for that element at the selected location in the pane.  
  
## Context Menu  
 The following options are available in the context menu displayed by right-clicking an element in the **Calculation Tools** pane:  
  
 **Copy**  
 Select to copy the selected element in **Metadata** or **Functions** to the Clipboard.  
  
> [!NOTE]  
>  This option is not displayed if **Templates** is selected.  
  
> [!NOTE]  
>  This option is disabled if the selected member cannot be copied, such as the **Sets** folder of a dimension displayed in **Metadata** or the function group folder for a function displayed in **Functions**.  
  
 **Filter Members**  
 Select to display the **Filter Members** dialog box and filter members displayed for the selected element in **Metadata**. For more information about the **Filter Members** dialog box, see [Filter Members Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](filter-members-dialog-box-analysis-services-multidimensional-data.md).  
  
> [!NOTE]  
>  This option is displayed only if **Metadata** is selected.  
  
> [!NOTE]  
>  This option is enabled only if a level for an attribute is selected in **Metadata**.  
  
 **Add Template**  
 Select to add a new calculated member, named set, or script command based on the selected template to the cube script and display the **Script Editor**, **Calculated Member Form Editor**, or **Named Set Form Editor** as appropriate for that command (in form view) or to scroll the contents of the **Script Editor** pane to the location of the command in the cube script (in script view).  
  
> [!NOTE]  
>  This option is displayed only if **Metadata** is selected.  
  
## See Also  
 [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](cube-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-calculations-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Script Organizer &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](script-organizer-cube-designer-analysis-services-multidimensional-data.md)   
 [Calculated Member Form Editor &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](calculated-member-form-editor-cube-designer-analysis-services-multidimensional-data.md)   
 [Named Set Form Editor &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](named-set-form-editor-cube-designer-analysis-services-multidimensional-data.md)   
 [Script Editor &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](script-editor-calculations-cube-designer-analysis-services-multidimensional-data.md)   
 [Calculations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](calculations-cube-designer-analysis-services-multidimensional-data.md)  
  
  
