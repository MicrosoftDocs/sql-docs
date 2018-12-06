---
title: "Work with Diagrams in Data Source View Designer (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Work with Diagrams in Data Source View Designer (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A data source view (DSV) diagram is a visual representation of the objects in a DSV. You can work with the diagram interactively to add, hide, delete or modify specific objects. You can also create multiple diagrams on the same DSV to focus attention on a subset of the objects.  
  
 To change the area of the diagram that appears in the diagram pane, click the four-headed arrow in the lower right corner of the pane, and then drag the selection box over the thumbnail diagram until you select the area that is to appear in the diagram pane.  
  
 This topic includes the following sections:  
  
 [Add a Diagram](#bkmk_add)  
  
 [Edit or Delete a Diagram](#bkmk_edit)  
  
 [Find Tables in a Diagram](#bkmk_findtables)  
  
 [Arrange Objects in a Diagram](#bkmk_arrangeobjects)  
  
 [Preserve object arrangement](#bkmk_preserve)  
  
##  <a name="bkmk_add"></a> Add a Diagram  
 DSV diagrams are created automatically when you create the DSV. After the DSV exists, you can create additional diagrams, remove them, or hide specific objects to create a more manageable representation of the DSV.  
  
 To create a new diagram, right-click anywhere in the **Diagram Organizer** pane, click **New Diagram**.  
  
 When you initially define a data source view (DSV) in an Analysis Services project, all tables and views added to the data source view are added to the \<All Tables> diagram. This diagram appears in the Diagram Organizer pane in Data Source View Designer, the tables in this diagram (and their columns and relationships) are listed in the Tables pane, and the tables in this diagram (and their columns and relationships) are displayed graphically in the schema pane. However, as you add tables, views and named queries to the \<All Tables> diagram, the sheer number of objects in this diagram makes it difficult to visualize relationships-particularly as multiple fact tables are added to the diagram, and dimension tables relate to multiple fact tables.  
  
 To reduce the visual clutter when you only want to view a subset of the tables in the data source view, you can define sub-diagrams (simply called diagrams) consisting of selected subsets of the tables, views, and named queries in the data source view. You can use diagrams to group items in the data source view according to business or solution needs.  
  
 You can group related tables and named queries in separate diagrams for business purposes, and to make it easier to understand a data source view that contains many tables, views, and named queries. The same table or named query can be included in multiple diagrams except for the \<All Tables> diagram. In the \<All Tables> diagram, all objects that are contained in the data source view are shown exactly once.  
  
##  <a name="bkmk_edit"></a> Edit or Delete a Diagram  
 When working with a diagram, pay close attention to the commands used for adding and removing objects. For example, deleting an object from a diagram will delete it from the DSV. If you only want to delete it from the diagram, use **Hide Table** instead.  
  
 ![Screenshot of diagram workspace, right-click menu](../../analysis-services/multidimensional-models/media/ssas-olapdsv-diagram.gif "Screenshot of diagram workspace, right-click menu")  
  
 Although you can hide objects individually, bringing them back using the Show Related Tables command returns all related objects to the diagram. To control which objects are returned to the workspace, drag them from the Tables pane instead.  
  
##  <a name="bkmk_findtables"></a> Find Tables in a Diagram  
 If your schema is large, scrolling to a particular table in the **Diagram** pane may be difficult. However, the following tools make it easy to find a table in a diagram.  
  
-   Scroll the list of tables in the **Tables** pane.  
  
     To include a table in the currently displayed diagram, drag the table from the **Tables** pane to the diagram pane.  
  
     To center the display on a table that is already included in the diagram, select the table in the **Tables** pane.  
  
-   Table locator in **Diagram** pane-The table locator is a 4-way arrow icon located at the intersection of the vertical and horizontal scroll bars in the lower right corner of the **Diagram** pane. It opens a thumbnail representation of the current diagram in the Diagram pane. You can use this thumbnail to change the view in the Diagram pane to any location on the diagram.  
  
-   Use the **Find Table** dialog box- Right-click on open area in the Diagram pane and click **Find Table**. Or, click the **Find Table** command on the toolbar or the **Data Source View** menu.  
  
     You can type strings and wildcard characters in the Filter box to view subsets of the tables in the diagram.  
  
##  <a name="bkmk_arrangeobjects"></a> Arrange Objects in a Diagram  
 Although Data Source View Designer can define multiple diagrams to make a DSV more understandable, diagrams that contain dozens of tables can be hard to read and manually rearranging table layouts is a tedious process. The Data Source View Designer can automatically rearrange tables in the current diagram using either a rectangular or diagonal layout based on the relationships between tables in the current diagram.  
  
-   In a rectangular layout, the relationship lines are drawn between tables instead of between columns. Relationship lines are drawn horizontally and vertically between tables.  
  
-   In a diagonal layout, relationship lines are drawn as directly as possible between related columns in tables. A relationship to multiple columns attaches to the first related column in the table. If the columns in a table are not visible, the lines are drawn to the top of the table.  
  
##  <a name="bkmk_preserve"></a> Preserve object arrangement  
 After manually arranging tables the way you want, adding more tables to the diagram can result in a diagram refresh that removes any recent modifications you made to the object layout.  
  
 This behavior is more likely to occur when you add a table, causing the Diagram Organizer to move other tables in order to accommodate the new one. It then redraws the diagram to ensure all tables and connection lines represented correctly. At this point, any manual adjustments to the placement of specific objects might be lost.  
  
 To avoid this problem, add all the tables first, before making any final adjustments. Objects should now retain their position in the diagram when you open it again later.  
  
## See Also  
 [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)   
 [Data Source View Designer &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/6f40a074-761f-440b-a999-09b755bd86ce)  
  
  
