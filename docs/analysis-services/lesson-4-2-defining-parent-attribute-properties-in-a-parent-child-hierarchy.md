---
title: "Defining Parent Attribute Properties in a Parent-Child Hierarchy | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 4-2 - Defining Parent Attribute Properties in a Parent-Child Hierarchy
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

A parent-child hierarchy is a hierarchy in a dimension that is based on two table columns. Together, these columns define the hierarchical relationships among the members of the dimension. The first column, called the *member key column*, identifies each dimension member. The other column, called the *parent column*, identifies the parent of each dimension member. The **NamingTemplate** property of a parent attribute determines the name of each level in the parent-child hierarchy, and the **MembersWithData** property determines whether data for parent members should be displayed.  
  
For more information, see [Parent-Child Dimensions](../analysis-services/multidimensional-models/parent-child-dimension.md), [Attributes in Parent-Child Hierarchies](../analysis-services/multidimensional-models/parent-child-dimension-attributes.md)  
  
> [!NOTE]  
> When you use the Dimension Wizard to create a dimension, the wizard recognizes the tables that have parent-child relationships and automatically defines the parent-child hierarchy for you.  
  
In the tasks in this topic, you will create a naming template that defines the name for each level in the parent-child hierarchy in the **Employee** dimension. You will then configure the parent attribute to hide all parent data, so that only the sales for leaf-level members are displayed.  
  
## Browsing the Employee Dimension  
  
1.  In Solution Explorer, double-click **Employee.dim** in the **Dimensions** folder to open Dimension Designer for the Employee dimension.  
  
2.  Click the **Browser** tab, verify that **Employees** is selected in the **Hierarchy** list, and then expand the **All Employees** member.  
  
    Notice that **Ken J. Sánchez** is the top-level manager in this parent-child hierarchy.  
  
3.  Select the **Ken J. Sánchez** member.  
  
    Notice that the level name for this member is **Level 02**. (The level name appears after **Current level:** immediately above the **All Employees** member.) In the next task, you will define more descriptive names for each level.  
  
4.  Expand **Ken J. Sánchez** to view the names of the employees who report to this manager, and then select **Brian S. Welcker** to view the name of this level.  
  
    Notice that the level name for this member is **Level 03**.  
  
5.  In Solution Explorer, double-click **Analysis Services Tutorial.cube** in the **Cubes** folder to open Cube Designer for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube.  
  
6.  Click the **Browser** tab.  
  
7.  Click the Excel icon, and then click **Enable** when prompted to enable connections.  
  
8.  In the PivotTable Field List, expand **Reseller Sales**. Drag **Reseller Sales-Sales Amount** to the Values area.  
  
9. In the PivotTable Field List, expand **Employee**, and then drag the **Employees** hierarchy to the **Rows** area.  
  
    All the members of the Employees hierarchy are added to column A of the PivotTable report.  
  
    The following image shows the Employees hierarchy expanded.  
  
10. ![PivotTable showing Employees hierarchy](../analysis-services/media/l4-employee-1.gif "PivotTable showing Employees hierarchy")  
  
    Notice that the sales made by each manager in Level 03 are also displayed in Level 04. This is because each manager is also an employee of another manager. In the next task, you will hide these sale amounts.  
  
## Modifying Parent Attribute Properties in the Employee Dimension  
  
1.  Switch to Dimension Designer for the **Employee** dimension.  
  
2.  Click the **Dimension Structure** tab, and then select the **Employees** attribute hierarchy in the **Attributes** pane.  
  
    Notice the unique icon for this attribute. This icon signifies that the attribute is the parent key in a parent-child hierarchy. Notice also, in the Properties window, that the **Usage** property for the attribute is defined as **Parent**. This property was set by the Dimension Wizard when the dimension was designed. The wizard automatically detected the parent-child relationship.  
  
3.  In the Properties window, click the ellipsis button (**...**) in the **NamingTemplate** property cell.  
  
    In the **Level Naming Template** dialog box, you define the level naming template that determines the level names in the parent-child hierarchy that are displayed to users as they browse cubes.  
  
4.  In the second row, the **\*** row, type **Employee Level \*** in the **Name** column, and then click the third row.  
  
    Notice under **Result** that each level will now be named "Employee Level" followed by a sequentially increasing number.  
  
    The following image shows the changes in the **Level Naming Template** dialog box.  
  
    ![Level Naming Template dialog box](../analysis-services/media/l4-namingtemplate.gif "Level Naming Template dialog box")  
  
5.  Click **OK**.  
  
6.  In the Properties window for the **Employees** attribute, in the **MembersWithData** property cell, select **NonLeafDataHidden** to change this value for the **Employees** attribute.  
  
    This will cause data that is related to non-leaf level members in the parent-child hierarchy to be hidden.  
  
## Browsing the Employee Dimension with the Modified Attributes  
  
1.  On the **Build** menu of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click **Deploy Analysis Services Tutorial**.  
  
2.  When deployment has successfully completed, switch to Cube Designer for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube, and then click **Reconnect** on the toolbar of the **Browser** tab.  
  
3.  Click the Excel icon, and then click **Enable**.  
  
4.  Drag **Reseller Sales-Sales Amount** to the Values area.  
  
5.  Drag the **Employees** hierarchy to the Row Labels area.  
  
    The following image shows the changes that you made to the Employees hierarchy. Notice that Stephen Y. Jiang no longer appears as an employee of himself.  
  
    ![Modified Employees hierarchy](../analysis-services/media/l4-employee-2.png "Modified Employees hierarchy")  
  
## Next Task in Lesson  
[Automatically Grouping Attribute Members](../analysis-services/lesson-4-3-automatically-grouping-attribute-members.md)  
  
## See Also  
[Parent-Child Dimensions](../analysis-services/multidimensional-models/parent-child-dimension.md)  
[Attributes in Parent-Child Hierarchies](../analysis-services/multidimensional-models/parent-child-dimension-attributes.md)  
  
  
  
