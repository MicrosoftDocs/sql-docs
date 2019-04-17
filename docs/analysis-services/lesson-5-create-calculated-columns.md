---
title: "Lesson 5: Create Calculated Columns | Microsoft Docs"
ms.date: 08/22/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 5: Create Calculated Columns
[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

In this lesson, you will create new data in your model by adding calculated columns. A calculated column is based on data that already exists in the model. To learn more, see [Calculated Columns](../analysis-services/tabular-models/ssas-calculated-columns.md).  
  
You will create five new calculated columns in three different tables. The steps are slightly different for each task. This is to show you there are several ways to create new columns, rename them, and place them in various locations in a table.  
  
Estimated time to complete this lesson: **15 minutes**  
  
## Prerequisites  
This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 4: Create Relationships](../analysis-services/lesson-4-create-relationships.md). 
  
## Create calculated columns  
  
#### Create a MonthCalendar calculated column in the DimDate table  
  
1.  Click the **Model** menu > **Model View** > **Data View**.  
  
    Calculated columns can only be created by using the model designer in Data View.  
  
2.  In the model designer, click the **DimDate** table (tab).  
  
3.  Right-click the **CalendarQuarter** column header, and then click **Insert Column**.  
  
    A new column named **Calculated Column 1** is inserted to the left of the **Calendar Quarter** column.  
  
4.  In the formula bar above the table, type the following formula. AutoComplete helps you type the fully qualified names of columns and tables, and lists the functions that are available.  
  
    ```  
    =RIGHT(" " & FORMAT([MonthNumberOfYear],"#0"), 2) & " - " & [EnglishMonthName]  
    ``` 
  
    Values are then populated for all the rows in the calculated column. If you scroll down through the table, you will see that rows can have different values for this column, based on the data that is in each row.    
  
5.  Rename this column to **MonthCalendar**. 

    ![as-tabular-lesson5-newcolumn](../analysis-services/media/as-tabular-lesson5-newcolumn.png) 
  
The MonthCalendar calculated column provides a sortable name for Month.  
  
#### Create a DayOfWeek calculated column in the DimDate table  
  
1.  With the **DimDate** table still active, click on the **Column** menu, and then click **Add Column**.  
  
2.  In the formula bar, type the following formula:  
    
    ```
    =RIGHT(" " & FORMAT([DayNumberOfWeek],"#0"), 2) & " - " & [EnglishDayNameOfWeek]  
    ```
    
    When you've finished building the formula, press ENTER. The new column is added to the far right of the table.  
  
3.  Rename the column to **DayOfWeek**.  
  
4.  Click on the column heading, and then drag the column between the **EnglishDayNameOfWeek** column and the **DayNumberOfMonth** column.  
  
    > [!TIP]  
    > Moving columns in your table makes it easier to navigate.  
  
The DayOfWeek calculated column provides a sortable name for the day of week.  
  
#### Create a ProductSubcategoryName calculated column in the DimProduct table  
  
  
1.  In the **DimProduct** table, scroll to the far right of the table. Notice the right-most column is named **Add Column** (italicized), click the column heading.  
  
2.  In the formula bar, type the following formula.  
    
    ```
    =RELATED('DimProductSubcategory'[EnglishProductSubcategoryName])  
    ```
  
3.  Rename the column to **ProductSubcategoryName**.  
  
The ProductSubcategoryName calculated column is used to create a hierarchy in the DimProduct table which includes data from the EnglishProductSubcategoryName column in the DimProductSubcategory table. Hierarchies cannot span more than one table. You will create hierarchies later in Lesson 9.  
  
#### Create a ProductCategoryName calculated column in the DimProduct table  
  
1.  With the **DimProduct** table still active, click the **Column** menu, and then click **Add Column**.  
  
2.  In the formula bar, type the following formula:  
  
    ```
    =RELATED('DimProductCategory'[EnglishProductCategoryName]) 
    ```
    
3.  Rename the column to **ProductCategoryName**.  
  
The ProductCategoryName calculated column is used to create a hierarchy in the DimProduct table which includes data from the EnglishProductCategoryName column in the DimProductCategory table. Hierarchies cannot span more than one table.  
  
#### Create a Margin calculated column in the FactInternetSales table  
  
1.  In the model designer, select the **FactInternetSales** table.  
  
2.  Add a new column.  
  
3.  In the formula bar, type the following formula:  
  
    ```
    =[SalesAmount]-[TotalProductCost]
    ``` 

4.  Rename the column to **Margin**.  
  
5.  Drag the column between the **SalesAmount** column and the **TaxAmt** column. 
 
      ![as-tabular-lesson5-newmargin](../analysis-services/media/as-tabular-lesson5-newmargin.png)
      
    The Margin calculated column is used to analyze profit margins for each sale.  
  
## What's next?
Go to the next lesson: [Lesson 6: Create Measures](../analysis-services/lesson-6-create-measures.md).
  
  
  
