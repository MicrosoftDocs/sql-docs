---
title: "Defining a Cube | Microsoft Docs"
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
# Lesson 2-2 - Defining a Cube
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

The Cube Wizard helps you define the measure groups and dimensions for a cube. In the following task, you will use the Cube Wizard to build a cube.  
  
### To define a cube and its properties  
  
1.  In Solution Explorer, right-click **Cubes**, and then click **New Cube**. The Cube Wizard appears.  
  
2.  On the **Welcome to the Cube Wizard** page, click **Next**.  
  
3.  On the **Select Creation Method** page, verify that the **Use existing tables** option is selected, and then click **Next**.  
  
4.  On the **Select Measure Group Tables** page, verify that the **Adventure Works DW 2012** data source view is selected.  
  
5.  Click **Suggest** to have the cube wizard suggest tables to use to create measure groups.  
  
    The wizard examines the tables and suggests **InternetSales** as a measure group table. Measure group tables, also called fact tables, contain the measures you are interested in, such as the number of units sold.  
  
6.  Click **Next**.  
  
7.  On the **Select Measures** page, review the selected measures in the **Internet Sales** measure group, and then clear the check boxes for the following measures:  
  
    -   **Promotion Key**  
  
    -   **Currency Key**  
  
    -   **Sales Territory Key**  
  
    -   **Revision Number**  
  
    By default, the wizard selects as measures all numeric columns in the fact table that are not linked to dimensions. However, these four columns are not actual measures. The first three are key values that link the fact table with dimension tables that are not used in the initial version of this cube.  
  
8.  Click **Next**.  
  
9. On the **Select Existing Dimensions** page, make sure the **Date** dimension that you created earlier is selected, and then click **Next**.  
  
10. On the **Select New Dimensions** page, select the new dimensions to be created. To do this, verify that the **Customer**, **Geography**, and **Product** check boxes are selected, and then clear the **InternetSales** check box.  
  
11. Click **Next**.  
  
12. On the **Completing the Wizard** page, change the name of the cube to **Analysis Services Tutorial**. In the Preview pane, you can see the **InternetSales** measure group and its measures. You can also see the **Date**, **Customer,** and **Product** dimensions.  
  
13. Click **Finish** to complete the wizard.  
  
    In Solution Explorer, in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project, the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube appears in the **Cubes** folder, and the Customer and Product database dimensions appear in the **Dimensions** folder. Additionally, in the center of the development environment, the Cube Structure tab displays the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube.  
  
14. On the toolbar of the Cube Structure tab, change the **Zoom** level to 50 percent, so that you can more easily see the dimensions and fact tables in the cube. Notice that the fact table is yellow and the dimension tables are blue.  
  
15. On the **File** menu, click **Save All**.  
  
## Next Task in Lesson  
[Adding Attributes to Dimensions](../analysis-services/lesson-2-3-adding-attributes-to-dimensions.md)  
  
## See Also  
[Cubes in Multidimensional Models](../analysis-services/multidimensional-models/cubes-in-multidimensional-models.md)  
[Dimensions in Multidimensional Models](../analysis-services/multidimensional-models/dimensions-in-multidimensional-models.md)  
  
  
  
