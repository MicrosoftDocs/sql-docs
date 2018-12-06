---
title: "Browse data and metadata in Cube | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 5faf2a9d-df39-465f-9c81-a00d5cd63f5a
author: minewiskan
ms.author: owend
manager: craigg
---
# Browse data and metadata in Cube
  Use the **Browser** tab of Cube Designer to browse cube data. You can use this view to examine the structure of a cube and to check data, calculation, formatting, and security of database objects. You can quickly examine a cube as end users see it in reporting tools or other client applications. When you browse cube data, you can view different dimensions, drill down into members, and slice through dimensions.  
  
 Before you browse a cube, you must process it and the reconnect to it. After you process it, open the **Browser** tab of Cube Designer. Click the Reconnect button on the toolbar to refresh the connection.  
  
 The **Browser** tab has three panes - the Metadata pane, the Filter pane, and the Data pane. Use the Metadata pane to examine the structure of the cube in tree format. Use the Filter pane at the top of the **Browser** tab to define any subcube you want to browse. Use the Data pane to view the result set and drill down through dimension hierarchies.  
  
## Setting up the Browser  
 To prepare to browse a cube, you can specify a perspective or translation that you want to use. You add measures and dimensions to the Data pane and specify any filters in the Filter pane.  
  
##### Specifying a Perspective  
 Use the **Perspective** list to choose a perspective that is defined for the cube. Perspectives are defined in the **Perspectives** tab of Cube Designer. To switch to a different perspective, select any perspective in the list.  
  
##### Specifying a Translation  
 Use the **Language** list to choose a translation that is defined for the cube. Translations are defined in the **Translations** tab of Cube Designer. The **Browser** tab initially shows captions for the default language, which is specified by the **Language** property for the cube. To switch to a different language, select any language in the list.  
  
##### Formatting the Data Pane  
 You can format captions and cells in the Data pane. Right-click the selected cells or captions that you want to format, and then click **Commands and Options**. Depending on the selection, the **Commands and Options** dialog box displays settings for **Format**, **Filter and Group**, **Report**, and **Behavior**.  
  
## Setting up the Data  
  
##### Adding or Removing Measures  
 Drag the measures you want to browse from the Metadata pane to the details area of the Data pane, which is the large empty pane on the lower right side of the workspace. As you drag additional measures, they are added as columns. A vertical line indicates where each additional measure will drop. Dragging the **Measures** folder drops all the measures into the details area.  
  
 To remove a measure from the details area, either drag it out of the Data pane, or right-click it and then click **Delete** on the shortcut menu.  
  
##### Adding or Removing Dimensions  
 Drag the hierarchies or dimensions to the row or filter areas.  
  
 If you want to work with multiple dimensions, use Analyze in Excel. Analyze in Excel is a shortcut that starts Excel if it is installed on the same computer as [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. Excel will open a workbook that contains an existing connection to the current database and a PivotTable Field List that is preloaded with measures and dimensions. For more information, see [Analyze in Excel &#40;Browser Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../analyze-in-excel-browser-cube-designer-analysis-services-multidimensional-data.md).  
  
 To remove a dimension, either drag it out of the Data pane, or right-click it and then click **Delete** on the shortcut menu.  
  
##### Adding or Removing Filters  
 You can use either of two methods to add a filter:  
  
-   Expand a dimension in the Metadata pane, and then drag a hierarchy to the Filter pane.  
  
     \- or -  
  
-   In the **Dimension** column of the **Filter** pane, click **\<Select dimension>** and select a dimension from the list, then click **\<Select hierarchy>** in the **Hierarchy** column and select a hierarchy from the list.  
  
 After you specify the hierarchy, specify the operator and filter expression. The following table describes the operators and filter expressions.  
  
|Operator|Filter Expression|Description|  
|--------------|-----------------------|-----------------|  
|Equal|One or more members|Values must be equal to a specified member.<br /><br /> (Provides multiple member selection for attribute hierarchies, other than parent-child hierarchies, and single member selection for other hierarchies.)|  
|Not Equal|One or more members|Values must not equal a specified member.<br /><br /> (Provides multiple member selection for attribute hierarchies, other than parent-child hierarchies, and single member selection for other hierarchies.)|  
|In|One or more named sets|Values must be in a specified named set.<br /><br /> (Supported for attribute hierarchies only.)|  
|Not In|One or more named sets|Values must not be in a specified named set.<br /><br /> (Supported for attribute hierarchies only.)|  
|Range (Inclusive)|One or two delimiting members of a range|Values must be between or equal to the delimiting members. If the delimiting members are equal or only one member is specified, no range is imposed and all values are permitted.<br /><br /> (Supported only for attribute hierarchies. The range must be on one level of a hierarchy. Unbounded ranges are not currently supported.)|  
|Range (Exclusive)|One or two delimiting members of a range|Values must be between the delimiting members. If the delimiting members are the equal or only one member is specified, values must be either greater than or less than the delimiting member.<br /><br /> (Supported only for attribute hierarchies. The range must be on one level of a hierarchy. Unbounded ranges are not currently supported.)|  
|MDX|An MDX expression returning a member set|Values must be in the calculated member set. Selecting this option displays the **Calculated Member Builder** dialog box for creating MDX expressions.|  
  
 For user-defined hierarchies, in which multiple members may be specified in the filter expression, all the specified members must be at the same level and share the same parent. This restriction does not apply for parent-child hierarchies.  
  
## Working with Data  
  
##### Drilling Down into a Member  
 To drill down into a particular member, click the plus sign (+) next to the member or double-click the member.  
  
##### Slicing Through Cube Dimensions  
 To filter the cube data, click the drop-down list box on the top-level column heading. You can expand the hierarchy and select or clear a member on any level to show or hide it and all its descendants. Clear the check box next to **(All)** to clear all the members in the hierarchy.  
  
 After you have set this filter on dimensions, you can toggle it on or off by right-clicking anywhere in the Data pane and clicking **Auto Filter**.  
  
##### Filtering Data  
 You can use the filter area to define a subcube on which to browse. You can add a filter by either clicking a dimension in the Filter pane or by expanding a dimension in the Metadata pane and then dragging a hierarchy to the Filter pane. Then specify an **Operator** and **Filter Expression**.  
  
##### Performing Actions  
 A triangle marks any heading or cell in the Data pane for which there is an action. This might apply for a dimension, level, member, or cube cell. Move the pointer over the heading object to see a list of available actions. Click the triangle in the cell to display a menu and start the associated process.  
  
 For security, the **Browser** tab only supports the following actions:  
  
-   URL  
  
-   Rowset  
  
-   Drillthrough  
  
 Command Line, Statement, and Proprietary actions are not supported. URL actions are only as safe as the Web page to which they link.  
  
##### Viewing Member Properties and Cube Cell Information  
 To view information about a dimension object or cell value, move the pointer over the cell.  
  
##### Showing or Hiding Empty Cells  
 You can hide empty cells in the data grid by right-clicking anywhere in the Data pane and clicking **Show Empty Cells**.  
  
  
