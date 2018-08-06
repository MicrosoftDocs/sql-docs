---
title: "Create a Cube using a Data Source View | Microsoft Docs"
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
# Create a Cube using a Data Source View
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Use this method of building a new cube if you intend to use an existing data source view. With this method, you specify the data source view and select fact and dimension tables that you want to use in the data source view. You then choose the dimensions and measures that you want to include in the cube.  
  
 To create a cube with a data source, in Solution Explorer, right-click **Cubes** and select **New Cube**. The Cube Wizard opens.  
  
## Selecting the Build Method  
 On the **Select Build Method** page of the wizard, click **Build the cube using a data source**.  
  
 If you select the **Auto build** check box, the wizard analyzes the data source view to configure the cube and its dimensions for you. The wizard identifies fact and dimension tables, selects measures to include in the cube, and builds hierarchies. On each page of the wizard, you can examine and change the choices that the wizard makes when **Auto build** is selected. If you do not select **Auto build**, you make all these choices manually.  
  
 If you select **Auto build**, you can click **Finish** on any page of the wizard to jump to the last page and accept the default configurations for any remaining pages of the wizard. On the last page of the wizard, you can review the structure of the cube before finishing the wizard.  
  
 If you do not select **Auto build**, you must select the fact and dimension tables yourself. The wizard builds any dimensions that you choose to create, but you must use Dimension Designer to manually build user-defined hierarchies in the dimensions. This requirement may not make any difference if you have already created the dimensions you want to use in the cube before running the Cube Wizard.  
  
## Selecting the Data Source View  
 If you use an existing data source to create a cube, the first step is to specify the data source view on which to base the cube. On the **Select Data Source View** page of the wizard, select an existing data source view. In the preview pane, you can view the tables in a selected data source view. To display the schema for any selected data source view, click **Browse**.  
  
 If the data source view you want to use is not listed, in the Cube Wizard, click **Cancel**, and open the Data Source View Wizard. You can also click **Add New Item** on the **File** menu to add an existing data source view from another database (or other location). For more information about creating data source views, see [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md).  
  
> [!NOTE]  
>  A data source view must contain at least one table to be listed on this page. You cannot create a cube based on a data source view that does not have any tables.  
  
## Identify Fact and Dimension Tables  
 In the Cube Wizard, use the **Identify Fact and Dimension Tables** page of the wizard to select the fact and dimension tables required to create the cube. If you selected the **Auto build** check box to create the cube, the fact or dimension tables the wizard detects are selected when this page first appears. If the wizard detects a table that is both a fact table and a dimension table, both columns are selected. If the wizard detects a table that is neither, neither column is selected. If you do not need a table for your cube design, clear the **Fact** and **Dimension** check boxes.  
  
 If you did not select the **Auto build** check box, you must make all the selections manually. Use the **Tables** tab or the **Diagram** tab:  
  
-   The **Tables** tab lists the tables in table format. Select the check box in the **Fact** column or the **Diagram** column.  
  
-   The **Diagram** tab displays the data source view schema. Tables are color coded to indicate fact or dimension. Click any table in the schema, and then click **Fact** or **Dimension** to select or clear the setting on that table. Use the **Zoom** button to change the magnification.  
  
> [!NOTE]  
>  On the **Diagram** tab, you can enlarge or maximize the wizard window to view the schema.  
  
 If there is a time dimension table in the data source view, select it in the **Time dimension table** list. If there are none, leave **\<None>** selected. This is the default item on the list. Selecting a table as the time dimension table also selects it as a dimension table on the **Tables** and **Diagram** tabs.  
  
## Defining Time Periods  
 If you specified a time dimension table while selecting table types, use the **Define Time Periods** page of the wizard to specify the columns in the table that correspond to standard time periods. Look for the standard periods under **Time Property Name**. For each row that has a corresponding column in the time dimension table, choose the correct column under **Time Table Columns**. The wizard uses the associations you specify to create attributes and suggest time hierarchies that make sense for your data. These associations also set the **Type** property for the corresponding attributes in the new time dimension. The wizard then creates a time dimension based on a time dimension table.  
  
 After you create the cube, you can use the Business Intelligence Wizard to add time intelligence enhancements to the cube. These enhancements include period-to-date, rolling average, and period-to-period views.  
  
## Selecting Dimensions  
 Use the **Select Dimensions** page of the wizard to add existing dimensions to the cube. This page appears only if there are already shared dimensions that correspond to dimension tables in the new cube.  
  
 To add existing dimensions, select one or more dimensions in the **Shared dimensions** list and click the right arrow (**>**) button to move them to the **Cube dimensions** list. Click the double-arrow (**>>**) button to move all the dimensions in the list.  
  
 If an existing dimension does not appear in the list and you think it should, you can click **Back** and change the table type settings for one or more tables. An existing dimension must also be related to at least one of the fact tables in the cube to appear in the **Shared dimensions** list.  
  
## Selecting Measures  
 Use the **Select Measures** page of the wizard to select the measures you want to include in the cube. Every table marked as a fact table appears as a measure group in this list, and every numeric non-key column appears as a measure on the list. By default every measure in every measure group is selected. You can clear the check box next to measures that you do not want to include in the cube. To remove all the measures of a measure group from the cube, clear the **Measure Groups/Measures** check box.  
  
 The names of measures listed under **Measure Groups/Measures** reflect column names. You can click the cell that contains a name to edit the name.  
  
 To view data for any measure, right-click any of the measure rows in the list, and then click **View sample data**. This opens the **Data Sample Viewer** and displays up to the first 1000 records from the corresponding fact table.  
  
## Reviewing New Dimensions  
 Use the **Review New Dimensions** page of the wizard to review the structures of any dimensions created by the wizard. The dimensions are listed on this page of the wizard in the **New dimensions** tree view. You can review the dimensions in the following ways:  
  
-   Expand any dimension to view its attributes and hierarchies.  
  
-   Expand the **Attributes** folder under any dimension to view the attributes in the dimension.  
  
-   Expand the **Hierarchy** folder under any dimension to view the hierarchies in the dimension.  
  
-   Expand any hierarchy to view its levels.  
  
> [!NOTE]  
>  You can enlarge or maximize the wizard window to view the tree better.  
  
 To remove any object in the tree from the cube, clear the check box next to it. Clearing the check box next to an object also removes all the objects underneath it. Dependencies between objects are enforced, so if you remove an attribute, any hierarchy levels dependent on the attribute are also removed. For example, clearing a check box next to a hierarchy clears the check boxes next to all the levels in the hierarchy and removes the levels as well as the hierarchies. The key attribute for a dimension cannot be removed.  
  
 You can rename any dimension, attribute, hierarchy or level either by clicking the name or by right-clicking the name and then on the shortcut menu clicking **Rename \<object>**, where **\<object>** is **Dimension**, **Attribute**, or **Level**.  
  
 There is not necessarily a one-to-one relationship between the number of dimension tables defined on the **Identify Fact and Dimension Tables** page of the wizard and the number of dimensions listed on this page of the wizard. Depending on relationships between tables in the data source view, the wizard can use two or more tables to build a dimension (for example, as required by a snowflake schema).  
  
## Completing the Cube Wizard  
 On the **Completing the Wizard** page of the wizard, you can view the measure groups, measures, and dimensions in the new cube. In the **Cube name** box, type a name for the cube. Then, if you are satisfied with the cube, click **Finish**. Click **Back** to go back to any previous page of the wizard and make changes.  
  
  
