---
title: "Lesson 6: Create Calculated Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d126766a-5699-4e9f-8213-8c7eea0fc14e
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 6: Create Calculated Columns
  In this lesson, you will create new data in your model by adding calculated columns. A calculated column is based on data that already exists in the model. To learn more, see [Calculated Columns &#40;SSAS Tabular&#41;](tabular-models/ssas-calculated-columns.md).  
  
 You will create five new calculated columns in three different tables. The steps are slightly different for each task. This is to show you there are several ways to create new columns, rename them, and place them in various locations in a table.  
  
 Estimated time to complete this lesson: **15 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 5: Create Relationships](lesson-4-create-relationships.md).  
  
## Create Calculated Columns  
  
#### Create a Month Calendar calculated column in the Date table  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model** menu, then point to **Model View**, and then click **Data View**.  
  
     Calculated columns can only be created by using the model designer in Data View.  
  
2.  In the model designer, click the **Date** table (tab).  
  
3.  Right-click the **Calendar Quarter** column, and then click **Insert Column**.  
  
     A new column named **CalculatedColumn1** is inserted to the left of the **Calendar Quarter** column.  
  
4.  In the formula bar above the table, type the following formula. AutoComplete helps you type the fully qualified names of columns and tables, and lists the functions that are available.  
  
     `=RIGHT(" " & FORMAT([Month],"#0"), 2) & " - " & [Month Name]`  
  
     When you have finished building the formula, press ENTER.  
  
     Values are then populated for all the rows in the calculated column. If you scroll down through the table, you will see that rows can have different values for this column, based on the data that is in each row.  
  
    > [!NOTE]  
    >  If you receive an error, verify the column names in the formula match the column names you changed in [Lesson 3: Rename Columns](rename-columns.md).  
  
5.  Rename this column to `Month Calendar`.  
  
 The Month Calendar calculated column provides a sortable name for Month.  
  
#### Create a Day of Week calculated column in the Date table  
  
1.  With the **Date** table still active, click on the **Column** menu, and then click **Add Column**.  
  
     A new column is added to the far right of the table  
  
2.  In the formula bar, type the following formula:  
  
     `=RIGHT(" " & FORMAT([Day Number Of Week],"#0"), 2) & " - " & [Day Name]`  
  
     When you have finished building the formula, press ENTER.  
  
3.  Rename the column to `Day of Week`.  
  
4.  Click on the column heading, and then drag the column between the **Day Name** column and the **Day of Month** column.  
  
    > [!TIP]  
    >  Moving columns in your table makes it easier to navigate.  
  
 The Day of Week calculated column provides a sortable name for the day of week.  
  
#### Create a Product Subcategory Name calculated column in the Product table  
  
1.  In the model designer, select the **Product** table.  
  
2.  Scroll to the far right of the table. Notice the right-most column is named **Add Column** (italicized), click the column heading.  
  
3.  In the formula bar, type the following formula.  
  
     `=RELATED('Product Subcategory'[Product Subcategory Name])`  
  
     When you have finished building the formula, press ENTER.  
  
4.  Rename the column to `Product Subcategory Name`.  
  
 The Product Subcategory Name calculated column is used to create a hierarchy in the Product table which includes data from the Product Subcategory Name column in the Product Subcategory table. Hierarchies cannot span more than one table. You will create hierarchies later in Lesson 7.  
  
#### Create a Product Category Name calculated column in the Product table  
  
1.  With the **Product** table still active, click the **Column** menu, and then click **Add Column**.  
  
2.  In the formula bar, type the following formula:  
  
     `=RELATED('Product Category'[Product Category Name])`  
  
     When you have finished building the formula, press ENTER.  
  
3.  Rename the column to `Product Category Name`.  
  
 The Product Category Name calculated column is used to create a hierarchy in the Product table which includes data from the Product Category Name column in the Product Category table. Hierarchies cannot span more than one table.  
  
#### Create a Margin calculated column in the Internet Sales table  
  
1.  In the model designer, select the **Internet Sales** table.  
  
2.  Add a new column.  
  
3.  In the formula bar, type the following formula:  
  
     `=[Sales Amount]-[Total Product Cost]`  
  
     When you have finished building the formula, press ENTER.  
  
4.  Rename the column to `Margin`.  
  
5.  Drag the column between the **Sales Amount** column and the **Tax Amt** column.  
  
 The Margin calculated column is used to analyze profit margins for each (product) row.  
  
## Next Step  
 To continue this lesson, go to the next lesson: [Lesson 7: Create Measures](lesson-6-create-measures.md).  
  
  
