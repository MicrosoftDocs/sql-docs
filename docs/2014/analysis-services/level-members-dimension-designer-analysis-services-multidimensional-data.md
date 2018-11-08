---
title: "Level and Members (Browser Tab, Dimension Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensiondesigner.browsertab.levelsandmembers.f1"
ms.assetid: 3f61e384-5b4e-4480-a7ed-b408de2fdea7
author: minewiskan
ms.author: owend
manager: craigg
---
# Level and Members (Browser Tab, Dimension Designer) (Analysis Services - Multidimensional Data)
  Use this pane to browse the members of the currently selected hierarchy and language. To select a hierarchy or language to browse, use the **Hierarchy** and **Language** options on the **Toolbar** pane. For more information about the Toolbar pane, see [Toolbar &#40;Browser Tab, Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-browser-tab-dimension-designer-analysis-services-multidimensional-data.md).  
  
## Writeback Mode  
 The functionality of this pane changes if writeback mode is enabled. The selected dimension must be write-enabled (in other words, the `WriteEnabled` property of the dimension must be set to true) and the dimension must be deployed to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance in order to enable writeback mode.  
  
 To enable writeback mode, you can either select **Writeback** from the **Toolbar** pane, or right-click the **Level and Members** pane and select **Writeback** from the context menu.  
  
 If writeback mode is enabled, you can perform the following additional actions in the **Level and Members** pane:  
  
|To do|Perform|  
|-----------|-------------|  
|Create sibling and child members within the selected hierarchy.|Right-click the selected member and select either **Create Sibling**, to create a sibling member, or **Create Child**, to create a child member, from the context menu.|  
|Move a selected member up or down the hierarchy.|Either drag the selected member to the appropriate parent or child member, or click **Increase Indent** or **Decrease Indent** on the **Toolbar** pane to move the selected member up or down the levels of the hierarchy.|  
|Rename a selected member.|Either right-click the selected member and select **Rename**, or click an already-selected member.|  
|Edit member property values|Double-click the value in the selected member property for the selected member to edit the value.|  
  
## Options  
 **Current level**  
 Displays the level to which the currently selected member in **Tree** belongs.  
  
 **Tree**  
 Displays the members of the currently selected hierarchy and language.  
  
 If member properties are selected from the **Member Properties** option of the Toolbar pane, each member property is displayed as a column.  
  
 If writeback mode is enabled, one column for each key column associated with the key attribute in the dimension is displayed.  
  
## Context Menu  
 The following options are available in the context menu displayed by right-clicking any part of the **Level and Members** pane for the selected member:  
  
 **Create sibling**  
 Creates a new member at the same level as the selected member.  
  
> [!NOTE]  
>  This option is enabled only if a member at a level other than the (All) level is selected.  
  
> [!NOTE]  
>  This option is displayed only if writeback mode is enabled.  
  
 **Create child**  
 Creates a new member at the next lower level as the selected member, as a child of the selected member.  
  
> [!NOTE]  
>  This option is enabled only if a member at a level other than the lowest level is selected.  
  
> [!NOTE]  
>  This option is displayed only if writeback mode is enabled.  
  
 **Cut**  
 Copies the selected members to the clipboard and removes them from the hierarchy.  
  
> [!NOTE]  
>  This option is enabled only if a member other than the All member is selected.  
  
> [!NOTE]  
>  This option is displayed only if writeback mode is enabled.  
  
 **Paste**  
 Pastes members previously removed using **Cut** immediately after the selected member.  
  
> [!NOTE]  
>  This option is displayed only if writeback mode is enabled.  
  
 **Delete**  
 Removes the selected members from the hierarchy.  
  
> [!NOTE]  
>  This option is enabled only if a member other than the All member is selected.  
  
> [!NOTE]  
>  This option is displayed only if writeback mode is enabled.  
  
 **Rename**  
 Select to edit the name of the selected member.  
  
> [!NOTE]  
>  This option is enabled only if a member other than the All member is selected.  
  
> [!NOTE]  
>  This option is displayed only if writeback mode is enabled.  
  
 **Filter Members**  
 Displays the **Filter Members** dialog box to filter members displayed in **Level and Members** for the selected hierarchy. For more information about the **Filter Members** dialog box, see [Filter Members Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](filter-members-dialog-box-analysis-services-multidimensional-data.md).  
  
 **Expand All**  
 Expands all members in **Tree**.  
  
> [!NOTE]  
>  This option is enabled only if a member at a level other than the lowest level is selected.  
  
 **Collapse All**  
 Collapse all members in **Tree**.  
  
 **Expand Member**  
 Expand the selected member in **Tree**.  
  
 **Collapse Member**  
 Collapse the selected member in **Tree**.  
  
 **Writeback**  
 Select to enable writeback mode.  
  
## See Also  
 [Toolbar &#40;Browser Tab, Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-browser-tab-dimension-designer-analysis-services-multidimensional-data.md)   
 [Browser &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](browser-dimension-designer-analysis-services-multidimensional-data.md)  
  
  
