---
title: "Lesson 7: Create Measures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 01bd2ad7-09b7-49ae-ad80-83f25da301aa
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 7: Create Measures
  In this lesson, you will create measures to be included in your model. Similar to the calculated columns you created in the previous lesson, a measure is essentially a calculation created using a DAX formula. However, unlike calculated columns, measures are evaluated based on a user selected *filter*; for example, a particular column or slicer added to the Row Labels field in a PivotTable.   A value for each cell in the filter is then calculated by the applied measure. Measures are powerful, flexible calculations that you will want to include in almost all tabular models, to perform dynamic calculations on numerical data. To learn more, see [Measures &#40;SSAS Tabular&#41;](tabular-models/measures-ssas-tabular.md).  
  
 To create measures, you will use the Measure Grid. By default, each table has an empty measure grid; however, you typically will not create measures for every table. The Measure Grid appears below a table in the model designer when in Data View. To hide or show the measure grid for a table, click the **Table** menu, and then click **Show Measure Grid**.  
  
 You can create a measure by clicking on an empty cell in the measure grid, and then typing a DAX formula in the formula bar. When you click ENTER to complete the formula, the measure will then appear in the cell. You can also create measures using a standard aggregation function by clicking on a column, and then clicking on the AutoSum button (**∑**) on the toolbar. Measures created using the AutoSum feature will appear in the measure grid cell directly beneath the column, but can be moved if necessary.  
  
 In this lesson, you will create measures by both entering a DAX formula in the formula bar and by using the AutoSum feature.  
  
 Estimated time to complete this lesson: **30 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 6: Create Calculated Columns](lesson-5-create-calculated-columns.md).  
  
## Create Measures  
  
#### To create a Days Current Quarter to Date measure in the Date table  
  
1.  In the model designer, click the **Date** table.  
  
2.  If an empty measure grid does not already appear beneath the table, click on the **Table** menu, and then click **Show Measure Grid**.  
  
3.  In the measure grid, click the top-left empty cell.  
  
4.  In the formula bar, above the table, type the following formula:  
  
     `=COUNTROWS( DATESQTD( 'Date'[Date]))`  
  
     When you have finished building the formula, press ENTER.  
  
     Notice the top-left cell now contains a measure name, **Measure 1**, followed by the result, **30**. The measure name also precedes the formula in the formula bar.  
  
5.  To rename the measure, in the formula bar, highlight the name, **Measure  1**, then type `Days Current Quarter to Date`, and then press ENTER.  
  
    > [!TIP]  
    >  When typing a formula in the formula bar, you can also first type the measure name followed by a colon (:), followed by a space, and then followed by the formula. Using this method, you do not have to rename the measure.  
  
#### To create a Days in Current Quarter measure in the Date table  
  
1.  With the **Date** table still active in the model designer, in the measure grid, click the empty cell below the measure you just created.  
  
2.  In the formula bar, type the following formula:  
  
     `Days in Current Quarter :=COUNTROWS( DATESBETWEEN( 'Date'[Date], STARTOFQUARTER( LASTDATE('Date'[Date])), ENDOFQUARTER('Date'[Date])))`  
  
     Notice in this formula you first included the measure name followed by a colon (:).  
  
     When you have finished building the formula, press ENTER.  
  
 When creating a comparison ratio between one incomplete period and the previous period; the formula must take into account the proportion of the period that has elapsed, and compare it to the same proportion in the previous period. In this case, [Days Current Quarter to Date]/[Days in Current Quarter] gives the proportion elapsed in the current period.  
  
#### To create an Internet Distinct Count Sales Order measure in the Internet Sales table  
  
1.  In the model designer, click the **Internet Sales** table (tab).  
  
     If the measure grid does not already appear, right-click the **Internet Sales** table (tab), and then click **Show Measure Grid**.  
  
2.  Click on the **Sales Order Number** column heading.  
  
3.  On the toolbar, click the down-arrow next to the AutoSum (**∑**) button, and then select **DistinctCount**.  
  
     The AutoSum feature automatically creates a measure for the selected column using the DistinctCount standard aggregation formula.  
  
     Notice the top cell below the column in the measure grid now contains a measure name, **Distinct Count Sales Order Number**. Measures created using the AutoSum feature are automatically placed in the top-most cell in the measure grid below the associated column.  
  
4.  In the measure grid, click the new measure, and then in the **Properties** window, in **Measure Name**, rename the measure to **Internet Distinct Count Sales Order**.  
  
#### To create additional measures in the Internet Sales table  
  
1.  By using the AutoSum feature, create and name the following measures:  
  
    |Measure Name|Column|AutoSum (∑)|Formula|  
    |------------------|------------|-------------------|-------------|  
    |Internet Order Lines Count|Sales Order Line Number|Count|=COUNT([Sales Order Line Number])|  
    |Internet Total Units|Order Quantity|Sum|=SUM([Order Quantity])|  
    |Internet Total Discount Amount|Discount Amount|Sum|=SUM([Discount Amount])|  
    |Internet Total Product Cost|Total Product Cost|Sum|=SUM([Total Product Cost])|  
    |Internet Total Sales|Sales Amount|Sum|=SUM([Sales Amount])|  
    |Internet Total Margin|Margin|Sum|=SUM([Margin])|  
    |Internet Total Tax Amt|Tax Amt|Sum|=SUM([Tax Amt])|  
    |Internet Total Freight|Freight|Sum|=SUM([Freight])|  
  
2.  By clicking on an empty cell in the measure grid, and by using the formula bar, create and name the following measures:  
  
    > [!IMPORTANT]  
    >  You must create the following measures in order; formulas in later measures refer to earlier measures.  
  
    |Measure Name|Formula|  
    |------------------|-------------|  
    |Internet Previous Quarter Margin|=CALCULATE([Internet Total Margin],PREVIOUSQUARTER('Date'[Date]))|  
    |Internet Current Quarter Margin|=TOTALQTD([Internet Total Margin],'Date'[Date])|  
    |Internet Previous Quarter Margin Proportion to QTD|=[Internet Previous Quarter Margin]*([Days Current Quarter to Date]/[Days In Current Quarter])|  
    |Internet Previous Quarter Sales|=CALCULATE([Internet Total Sales],PREVIOUSQUARTER('Date'[Date]))|  
    |Internet Current Quarter Sales|=TOTALQTD([Internet Total Sales],'Date'[Date])|  
    |Internet Previous Quarter Sales Proportion to QTD|=[Internet Previous Quarter Sales]*([Days Current Quarter to Date]/[Days In Current Quarter])|  
  
 Measures created for the Internet Sales table can be used to analyze critical financial data such as sales, costs, and profit margin for items defined by the user selected filter.  
  
## Next Step  
 To continue this tutorial, go to the next lesson: [Lesson 8: Create Key Performance Indicators](lesson-7-create-key-performance-indicators.md).  
  
  
