---
title: "Report Action Form Editor (Actions Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.actionexpression.reportaction.f1"
ms.assetid: cebfdd07-e376-46d6-86ef-b6f816a2f360
author: minewiskan
ms.author: owend
manager: craigg
---
# Report Action Form Editor (Actions Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Report Action Form Editor** pane on the **Actions** tab in Cube Designer to modify the report action selected in the **Action Organizer** pane.  
  
## Options  
 **Name**  
 Type the name of the action.  
  
 **Action Target**  
 Expand to view the **Target type** and **Target object** options.  
  
 **Target type**  
 Select the type of object to which the action is to be associated. The server returns to the client only those actions that apply to the object of the specified type. The action is available to the client if the **Condition** is met and if the objects specified in the following table are selected.  
  
|Value|Selected Object|  
|-----------|---------------------|  
|Attribute members|A member is selected from a level based on the attribute in **Target object**.<br /><br /> Note: Other user hierarchies that use the selected attribute inherit the report action.|  
|Cells|The named set in **Target object** is selected. Select **All cells** to select all of the cells in the cube.|  
|Cube|The cube in **Target object** is selected. Select CURRENTCUBE to use the current cube.<br /><br /> Note: Using CURRENTCUBE provides additional portability in cases where the cube may be renamed or the action copied to other cubes. It is recommended that you use CURRENTCUBE to represent the current cube.|  
|Dimension members|A member of the dimension in **Target object** is selected.|  
|Hierarchy|The hierarchy in **Target object** is selected.|  
|Hierarchy members|A member within the hierarchy in **Target object** is selected.|  
|Level|The level in **Target object** is selected.|  
|Level members|A member within the level in **Target object** is selected.|  
  
 **Target object**  
 Select the object to which the action is to be associated. The [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance returns to the client only those actions that apply to the selected object. The list of available objects is constrained by the choice of **Target type**.  
  
 **Condition (Optional)**  
 Enter the Multidimensional Expressions (MDX) expression that describes an optional condition, used in conjunction with **Target object**, which further restricts when the action is available. The expression must return a Boolean value that, if true, indicates the action is available.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 **Report Server**  
 Expand to view the **Server name**, **Server path**, and **Report format** options.  
  
 **Server name**  
 Type the name of the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] instance on which the action runs the report.  
  
 **Server path**  
 Type the path to the report on the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] instance. For example, type **Sales/YearlySalesByCategory**.  
  
 **Report format**  
 Select the format in which the report is returned. The following table describes the available formats.  
  
|Value|Description|  
|-----------|-----------------|  
|HTML5|Report is returned in an HTML 5.0-compliant format.|  
|HTML3|Report is returned in an HTML 3.2-compliant format.|  
|Excel|Report is returned as a [!INCLUDE[msCoName](../includes/msconame-md.md)] Office Excel workbook (.xls) file.|  
|PDF|Report is returned as an Adobe Portable Document Format (.pdf) file.|  
  
 **Parameters (Optional)**  
 Expand to view a grid in which report parameters can be supplied for the report specified in **Report**. The grid contains the following columns:  
  
|Column|Description|  
|------------|-----------------|  
|**Parameter Name**|Type the name of the report parameter to be passed to the report.|  
|**Parameter Value**|Type the value of the report parameter to be passed to the report.<br /><br /> Click the ellipsis button (**...**) to display the **MDX Builder** dialog box and create an MDX expression that provides the value of the report parameter. For more information about the **MDX Builder** dialog box, see [MDX Builder &#40;Analysis Services - Multidimensional Data&#41;](mdx-builder-analysis-services-multidimensional-data.md).<br /><br /> If the parameter is set to an MDX expression, then the expression is evaluated when the action is run, otherwise it is passed to the report without modification.|  
  
 **Additional Properties**  
 Expand to view the **Invocation**, **Application**, **Description**, **Caption**, and **Caption Is MDX** options.  
  
 **Invocation**  
 Select the setting that indicates when the action should be carried out.  
  
> [!NOTE]  
>  This option only provides a recommendation to a client application as to when to execute an action, and does not directly control the invocation of the action.  
  
 The following table describes the available settings.  
  
|Value|Description|  
|-----------|-----------------|  
|Batch|The action should run as part of a batch operation or a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] task.|  
|Interactive|The action runs when the user invokes the action.|  
|On Open|The action runs when the cube is first opened.|  
  
 **Application**  
 Type the name of the application that can interpret the string returned by **Action expression**.  
  
 You can also use this option to identify which client application most commonly uses this action, as well as to display appropriate icons next to the action in a pop-up menu.  
  
> [!NOTE]  
>  This option only provides a recommendation to a client application as to what client application should execute an action, and does not directly control access to the action. Client applications should hide any actions that are associated with other client applications.  
  
 **Description**  
 Type the optional description of the action.  
  
 **Caption**  
 Type the caption to be displayed for the action in the client application if **Caption Is MDX** is set to **False**.  
  
 Type the MDX expression that returns a string for the caption if **Caption Is MDX** is set to **True**.  
  
 **Caption is MDX**  
 Select **False** to indicate that **Caption** contains a literal string representing a caption to be displayed for the action in the client application.  
  
 Select **True** to indicate that **Caption** contains an MDX expression that returns a string representing a caption to be displayed for the action in the client application. The MDX expression must be resolved before the action is returned to the client application.  
  
## See Also  
 [Actions &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](actions-cube-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-actions-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Action Organizer &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](action-organizer-cube-designer-analysis-services-multidimensional-data.md)   
 [Calculation Tools &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](calculation-tools-actions-cube-designer-analysis-services-multidimensional-data.md)   
 [Action Form Editor &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](action-form-editor-cube-designer-analysis-services-multidimensional-data.md)   
 [Drillthrough Action Form Editor &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](drillthrough-action-form-editor-cube-designer-analysis-services-multidimensional-data.md)  
  
  
