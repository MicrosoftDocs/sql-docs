---
title: "Defining a Dimension | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 112696db-3838-4b50-91bd-d2ce5fa04ee5
author: minewiskan
ms.author: owend
manager: craigg
---
# Defining a Dimension
  In the following task, you will use the Dimension Wizard to build a Date dimension.  
  
> [!NOTE]  
>  This lesson requires that you have completed all the procedures in Lesson 1.  
  
### To define a dimension  
  
1.  In Solution Explorer (on the right side of Microsoft Visual Studio), right-click **Dimensions**, and then click **New Dimension**. The Dimension Wizard appears.  
  
2.  On the **Welcome to the Dimension Wizard** page, click **Next**.  
  
3.  On the **Select Creation Method** page, verify that the **Use an existing table** option is selected, and then click **Next**.  
  
4.  On the **Specify Source Information** page, verify that the **Adventure Works DW 2012** data source view is selected.  
  
5.  In the **Main table** list, select **Date**.  
  
6.  Click **Next**.  
  
7.  On the **Select Dimension Attributes** page, select the check boxes next to the following attributes:  
  
    -   **Date Key**  
  
    -   **Full Date Alternate Key**  
  
    -   **English Month Name**  
  
    -   **Calendar Quarter**  
  
    -   **Calendar Year**  
  
    -   **Calendar Semester**  
  
8.  Change the setting of the **Full Date Alternate Key** attribute's **Attribute Type** column from **Regular** to **Date**. To do this, click **Regular** in the **Attribute Type** column. Then click the arrow to expand the options. Next, click **Date** > **Calendar** > **Date**. Click **OK**. Repeat these steps to change the attribute type of the attributes as follows:  
  
    -   **English Month Name** to **Month**  
  
    -   **Calendar Quarter** to **Quarter**  
  
    -   **Calendar Year** to **Year**  
  
    -   **Calendar Semester** to **Half Year**  
  
9. Click **Next**.  
  
10. On the **Completing the Wizard** page, in the Preview pane, you can see the **Date** dimension and its attributes.  
  
11. Click **Finish** to complete the wizard.  
  
     In Solution Explorer, in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project, the Date dimension appears in the **Dimensions** folder. In the center of the development environment, Dimension Designer displays the Date dimension.  
  
12. On the **File** menu, click **Save All**.  
  
## Next Task in Lesson  
 [Defining a Cube](lesson-2-2-defining-a-cube.md)  
  
## See Also  
 [Dimensions in Multidimensional Models](multidimensional-models/dimensions-in-multidimensional-models.md)   
 [Create a Dimension by Using an Existing Table](multidimensional-models/create-a-dimension-by-using-an-existing-table.md)   
 [Create a Dimension Using the Dimension Wizard](multidimensional-models/create-a-dimension-using-the-dimension-wizard.md)  
  
  
