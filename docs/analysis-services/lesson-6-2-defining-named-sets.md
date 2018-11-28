---
title: "Defining Named Sets | Microsoft Docs"
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
# Lesson 6-2 - Defining Named Sets
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

A named set is a Multidimensional Expressions (MDX) expression that returns a set of dimension members. You can define named sets and save them as part of the cube definition; you can also create named sets in client applications. You create named sets by combining cube data, arithmetic operators, numbers, and functions. Named sets can be used by users in MDX queries in client applications and can also be used to define sets in subcubes. A subcube is a collection of crossjoined sets that restricts the cube space to the defined subspace for subsequent statements. Defining a restricted cube space is a fundamental concept to MDX scripting.  
  
Named sets simplify MDX queries and provide useful aliases for complex, typically used, set expressions. For example, you can define a named set called Large Resellers that contains the set of members in the Reseller dimension that have the most employees. End users could then use the Large Resellers named set in queries, or you could use the named set to define a set in a subcube. Named set definitions are stored in cubes, but their values exist only in memory. To create a named set, use the **New Named Set** command on the **Calculations** tab of Cube Designer. For more information, see [Calculations](../analysis-services/multidimensional-models-olap-logical-cube-objects/calculations.md), [Create Named Sets](../analysis-services/multidimensional-models/create-named-sets.md).  
  
In the tasks in this topic, you will define two named sets: a Core Products named set and a Large Resellers named set.  
  
## Defining a Core Products Named Set  
  
1.  Switch to the **Calculations** tab of Cube Designer for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube, and then click **Form View** on the toolbar.  
  
2.  Click **[Total Sales Ratio to All Products]** in the **Script Organizer** pane, and then click **New Named Set** on the toolbar of the **Calculations** tab.  
  
    When you define a new calculation on the **Calculations** tab, remember that calculations are resolved in the order in which they appear in the **Script Organizer** pane. Your focus within that pane when you create a new calculation determines the order of the execution of the calculation; a new calculation is defined immediately after the calculation on which you are focused.  
  
3.  In the **Name** box, change the name of the new named set to **[Core Products]**.  
  
    In the **Script Organizer** pane, notice the unique icon that differentiates a named set from a script command or a calculated member.  
  
4.  On the **Metadata** tab in the **Calculation Tools** pane, expand **Product**, expand **Category**, expand **Members**, and then expand **All Products**.  
  
    > [!NOTE]  
    > If you cannot view any metadata in the **Calculation Tools** pane, click **Reconnect** on the toolbar. If this does not work, you may have to process the cube or start the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
5.  Drag **Bikes** into the **Expression** box.  
  
    You now have created a set expression that will return the set of members that are in the Bike category in the Product dimension.  
  
## Defining a Large Resellers Named Set  
  
1.  Right-click **[Core Products]** in the **Script Organizer** pane, and then click **New Named Set**.  
  
2.  In the **Name** box, change the name of this named set to **[Large Resellers]**.  
  
3.  In the **Expression** box, type **Exists()**.  
  
    You will use the Exists function to return the set of members from the Reseller Name attribute hierarchy that intersects with the set of members in the Number of Employees attribute hierarchy that has the largest number of employees.  
  
4.  On the **Metadata** tab in the **Calculation Tools** pane, expand the **Reseller** dimension, and then expand the **Reseller Name** attribute hierarchy.  
  
5.  Drag the **Reseller Name** level into the parenthesis for the Exists set expression.  
  
    You will use the Members function to return all members of this set. For more information, see [Members &#40;Set&#41; &#40;MDX&#41;](../mdx/members-set-mdx.md).  
  
6.  After the partial set expression, type a period, and then add the Members function. Your expression should look like the following:  
  
    ```  
    Exists([Reseller].[Reseller Name].[Reseller Name].Members)  
    ```  
  
    Now that you have defined the first set for the Exists set expression, you are ready to add the second set-the set of members of the Reseller dimension that contains the largest number of employees.  
  
7.  On the **Metadata** tab in the **Calculation Tools** pane, expand **Number of Employees** in the Reseller dimension, expand **Members**, and then expand **All Resellers**.  
  
    Notice that the members of this attribute hierarchy are not grouped.  
  
8.  Open Dimension Designer for the **Reseller** dimension, and then click **Number of Employees** in the **Attributes** pane.  
  
9. In the Properties window, change the **DiscretizationMethod** property to **Automatic**, and then change the **DiscretizationBucketCount** property to **5**. For more information, see [Group Attribute Members &#40;Discretization&#41;](../analysis-services/multidimensional-models/attribute-properties-group-attribute-members.md).  
  
10. On the **Build** menu of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click **Deploy Analysis Services Tutorial**.  
  
11. When deployment has successfully completed, switch to Cube Designer for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube, and then click **Reconnect** on the toolbar of the **Calculations** tab.  
  
12. On the **Metadata** tab in the **Calculation Tools** pane, expand **Number of Employees** in the **Reseller** dimension, expand **Members**, and then expand **All Resellers**.  
  
    Notice that the members of this attribute hierarchy are now contained in five groups, numbered 0 through 4. To view the number of a group, pause the pointer over that group to view an InfoTip. For the range `2 -17`, the InfoTip should contain `[Reseller].[Number of Employees].&[0]`.  
  
    The members of this attribute hierarchy are grouped because the DiscretizationBucketCount property is set to **5** and the DiscretizationMethod property is set to **Automatic**.  
  
13. In the **Expression** box, add a comma in the Exists set expression after the Members function and before the closing parenthesis, and then drag **83 - 100** from the **Metadata** pane and position it after the comma.  
  
    You have now completed the Exists set expression that will return the set of members that intersects with these two specified sets, the set of all resellers and the set of resellers who have 83 to 100 employees, when the Large Resellers named set is put on an axis.  
  
    The following image shows the **Calculation Expressions** pane for the **[Large Resellers]** named set.  
  
    ![Calculation Expressions pane for [Large Resellers]](../analysis-services/media/l6-named-set-02.gif "Calculation Expressions pane for [Large Resellers]")  
  
14. On the toolbar of the **Calculations** tab, click **Script View**, and then review the two named sets that you have just added to the calculation script.  
  
15. Add a new line in the calculation script immediately before the first CREATE SET command, and then add the following text to the script on its own line:  
  
    ```  
    /* named sets */  
    ```  
  
    You have now defined two named sets, which are visible in the **Script Organizer** pane. You are now ready to deploy these named sets, and then to browse these measures in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube.  
  
## Browsing the Cube by Using the New Named Sets  
  
1.  On the **Build** menu of [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click **Deploy Analysis Services Tutorial**.  
  
2.  When deployment has successfully completed, click the **Browser** tab, and then click **Reconnect**.  
  
3.  Clear the grid in the data pane.  
  
4.  Add the **Reseller Sales-Sales Amount** measure to the data area.  
  
5.  Expand the Product dimension, and then add Category and Subcategory to the row area, as shown in the following image.  
  
    ![Members of the Subcategory attribute](../analysis-services/media/l6-named-set-03.gif "Members of the Subcategory attribute")  
  
6.  In the **Metadata** pane, in the **Product** dimension, drag **Core Products** to the filter area.  
  
    Notice that only the **Bike** member of the **Category** attribute and members of the **Bike** subcategories remain in the cube. This is because the **Core Products** named set is used to define a subcube. This subcube limits the members of the **Category** attribute in the **Product** dimension within the subcube to those members of the **Core Product** named set, as shown in the following image.  
  
    ![Members of the Core Product named set](../analysis-services/media/l6-named-set-04.gif "Members of the Core Product named set")  
  
7.  In the **Metadata** pane, expand **Reseller**, add **Large Resellers** to the filter area.  
  
    Notice that the Reseller Sales Amount measure in the Data pane only displays sales amounts for large resellers of bikes. Notice also that the Filter pane now displays the two named sets that are used to define this particular subcube, as shown in the following image.  
  
    ![Filter pane containing two named sets](../analysis-services/media/l6-named-set-05.gif "Filter pane containing two named sets")  
  
## Next Lesson  
[Lesson 7: Defining Key Performance Indicators &#40;KPIs&#41;](../analysis-services/lesson-7-defining-key-performance-indicators-kpis.md)  
  
## See Also  
[Calculations](../analysis-services/multidimensional-models-olap-logical-cube-objects/calculations.md)  
[Create Named Sets](../analysis-services/multidimensional-models/create-named-sets.md)  
  
  
  
