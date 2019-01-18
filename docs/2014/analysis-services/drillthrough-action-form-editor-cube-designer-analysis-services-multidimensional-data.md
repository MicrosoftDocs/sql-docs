---
title: "Drillthrough Action Form Editor (Actions Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.actionexpression.drillthroughaction.f1"
ms.assetid: 225fd818-b5ea-494f-b67b-66e09798274a
author: minewiskan
ms.author: owend
manager: craigg
---
# Drillthrough Action Form Editor (Actions Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Drillthrough Action Form Editor** pane on the **Actions** tab in Cube Designer to modify the drillthrough action selected in the **Action Organizer** pane. For more information about drillthrough actions, see [Actions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models/actions-analysis-services-multidimensional-data.md).  
  
> [!NOTE]  
>  Drillthrough actions no longer drill down to the underlying data store. The information that drillthrough actions access must be modeled within the cube by using dimension or hierarchy members.  
  
## Options  
 **name**  
 Type the name of the action.  
  
 **Action Target**  
 Expand to view the **Measure group members** option.  
  
 **Measure group members**  
 Select the measure group to which the drillthrough action is to be associated.  
  
 **Condition (Optional)**  
 Enter the Multidimensional Expressions (MDX) expression that describes an optional condition, used in conjunction with **Measure group members**, which further restricts when the action is available. The expression must return a Boolean value that, if true, indicates the action is available.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 **Drillthrough Columns**  
 Expand to display a grid in which to indicate the attributes that are returned when the user runs this action.  
  
> [!NOTE]  
>  You can select more than one dimension, but no dimension can be used more than once in a drillthrough action.  
  
 The grid contains the following columns:  
  
|Column|Description|  
|------------|-----------------|  
|**Dimensions**|Select the dimension from which the returned attribute is derived. Select MEASURES to drillthrough on measures.|  
|**Return Columns**|Select the attribute or measure from the selected dimension to be returned when the action is run.|  
  
 **Additional Properties**  
 Expand to view the **Default**, **Maximum rows**, **Invocation**, **Application**, **Description**, **Caption**, and **Caption Is MDX** options.  
  
 **Default**  
 Select **True** to include this drillthrough action as a default drillthrough action, otherwise, select **False**.  
  
 If the `RETURN` clause is omitted from an MDX `DRILLTHROUGH` statement executed by a client application, the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance evaluates all default drillthrough actions and runs the first default drillthrough action that returns a non-empty set. For more information about the MDX `DRILLTHROUGH` statement, see [DRILLTHROUGH Statement &#40;MDX&#41;](/sql/mdx/mdx-data-manipulation-drillthrough).  
  
> [!NOTE]  
>  This option is used for backwards compatibility purposes.  
  
 **Maximum rows**  
 Type the maximum number of rows to be returned by the drillthrough action. Setting this option to zero or an empty value indicates that the drillthrough action returns all rows retrieved by the action to the client application.  
  
 **Invocation**  
 Select the setting that indicates when the action should be carried out.  
  
> [!NOTE]  
>  This option only provides a recommendation to a client application as to when to execute an action, and does not directly control the invocation of the action.  
  
 The following table describes the available settings.  
  
|Value|Description|  
|-----------|-----------------|  
|Batch|The action should run as part of a batch operation or an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] task.|  
|Interactive|The action runs when the user invokes the action.|  
|On Open|The action runs when the cube is first opened.|  
  
 **Application**  
 Type the name of the application that can execute the drillthrough action.  
  
 You can also use this option to identify which client application most commonly uses this action, as well as to display appropriate icons next to the action in a pop-up menu.  
  
> [!NOTE]  
>  This option only provides a recommendation to a client application as to what client application should execute an action, and does not directly control access to the action. Client applications should hide any actions that are associated with other client applications.  
  
 **Description**  
 Type the optional description of the action.  
  
 **Caption**  
 Type the caption to be displayed for the action in the client application if **Caption Is MDX** is set to **False**.  
  
 Type the Multidimensional Expressions (MDX) expression that returns a string for the caption if **Caption Is MDX** is set to **True**.  
  
 **Caption Is MDX**  
 Select **False** to indicate that **Caption** contains a literal string representing a caption to be displayed for the action in the client application.  
  
 Select **True** to indicate that **Caption** contains an MDX expression that returns a string representing a caption to be displayed for the action in the client application. The MDX expression must be resolved before the action is returned to the client application.  
  
## See Also  
 [Multidimensional Expressions &#40;MDX&#41; Reference](/sql/mdx/multidimensional-expressions-mdx-reference)   
 [Actions &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](actions-cube-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-actions-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Action Organizer &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](action-organizer-cube-designer-analysis-services-multidimensional-data.md)   
 [Calculation Tools &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](calculation-tools-actions-cube-designer-analysis-services-multidimensional-data.md)   
 [Action Form Editor &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](action-form-editor-cube-designer-analysis-services-multidimensional-data.md)   
 [Report Action Form Editor &#40;Actions Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](report-action-form-editor-cube-designer-analysis-services-multidimensional-data.md)  
  
  
