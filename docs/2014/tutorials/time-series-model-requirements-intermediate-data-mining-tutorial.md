---
title: "Understanding the Requirements for a Time Series Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 1ce2b3e3-108a-4f7e-985f-a20b816d0da7
author: minewiskan
ms.author: owend
manager: craigg
---
# Understanding the Requirements for a Time Series Model (Intermediate Data Mining Tutorial)
  When you are preparing data for use in a forecasting model, you must ensure that your data contains a column that can be used to identify the steps in the time series. That column will be designated as the `Key Time` column. Because it is a key, the column must contain unique numeric values.  
  
 Choosing the right unit for the `Key Time` column is an important part of analysis. For example, suppose your sales data is refreshed on a minute by minute basis. You would not necessarily use minutes as the unit for the time series; you might find it more meaningful to roll up sales data by the day, week, or even month. If you are unsure which unit of time to use, you can create a new data source view for each aggregation, and build related models, to see if different trends emerge at each level of aggregation.  
  
 For this tutorial, sales data is collected on a daily basis in the transactional sales database, but for data mining, the data has been pre-aggregated by the month, using a view.  
  
 Additionally, it is desirable for analysis that the data have as few gaps as possible. If you plan to analyze multiple series of data, all series should preferably start and end on the same date. If the data has gaps, but the gaps are not at the beginning or end of a series, you can use the MISSING_VALUE_SUBSTITUTION parameter to fill in the series. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] also provides several options for replacing missing data with values, such as using means or constants.  
  
> [!WARNING]  
>  The PivotChart and PivotTable tools that were included in earlier versions of the data source view designer are no longer provided. We recommend that you identify gaps in time series data beforehand, by using tools such as the Data Profiler included in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].  
  
### To identify the time key for the forecasting model  
  
1.  In the pane, **SalesByRegion.dsv [Design]**, right-click the table vTimeSeries, and then select **Explore Data**.  
  
     A new tab opens, titled **Explore vTimeSeries Table**.  
  
2.  On the **Table** tab, review the data that is used in the TimeIndex and Reporting Date columns.  
  
     Both are sequences with unique values and can both be used as the time series key; however, the data types of the columns are different. The Microsoft Time Series algorithm does not require a `datetime` data type, only that the values be distinct and ordered. Therefore, either column can be used as the time key for the forecasting model.  
  
3.  In the data source view design surface, select the column, Reporting Date and select **Properties**. Next, click the column TimeIndex and select **Properties**.  
  
     The field TimeIndex has the data type System.Int32, whereas the field Reporting Date has the data type System.DateTime. Many data warehouses convert date/time values to integers and use the integer column as the key, to improve indexing performance. However, if you use this column, the Microsoft Time Series algorithm will make predictions using future values such as 201014, 201014, and so forth. Because you want to represent your sales data forecast by using calendar dates, you will use the Reporting Date column as the unique series identifier.  
  
### To set the key in the data source view  
  
1.  In the pane **SalesByRegion.dsv**, select the vTimeSeries table.  
  
2.  Right-click the column, Reporting Date, and select **Set Logical Primary Key**.  
  
## Handling Missing Data (Optional)  
 If any series has missing data, you might get an error when you try to process the model. You have several ways to work around missing data:  
  
-   You can have Analysis Services fill in missing values, either by calculating a mean, or by using a previous value. You do this by setting the MISSING_VALUE_SUBSTITUTION parameter on the mining model. For more information about this parameter, see [Microsoft Time Series Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md). For information about how to change parameters on an existing mining model, see [View or Change Algorithm Parameters](../../2014/analysis-services/data-mining/view-or-change-algorithm-parameters.md).  
  
-   You can alter the data source or filter the underlying view to eliminate ragged series or to replace values. You can do this in the relational data source, or you can modify the data source view by creating custom named queries or named calculations. For more information, see [Data Source Views in Multidimensional Models](../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md). A later task in this lesson provides an example of how to build both a named query and a custom calculation.  
  
 For this scenario, some data is missing at the beginning of one series: that is, there is no data for the T1000 product line until July 2007. Otherwise, all series end on the same date, and there are no missing values.  
  
 The requirement of the Microsoft Time Series algorithm is that any series that you include in a single model should have the same **ending** point. Because the T1000 bicycle model was introduced in 2007, the data for this series starts later than for other bicycle models, but the series ends on the same date; therefore the data is usable.  
  
#### To close the data source view designer  
  
-   Right-click the tab, **Explore vTimeSeries Table**, and select **Close**.  
  
## Next Task in Lesson  
 [Creating a Forecasting Structure and Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/creating-a-forecasting-structure-and-model-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Microsoft Time Series Algorithm](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm.md)  
  
  
