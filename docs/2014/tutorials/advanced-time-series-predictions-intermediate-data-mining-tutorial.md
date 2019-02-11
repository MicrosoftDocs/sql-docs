---
title: "Advanced Time Series Predictions (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: b614ebdb-07ca-44af-a0ff-893364bd4b71
author: minewiskan
ms.author: owend
manager: kfile
---
# Advanced Time Series Predictions (Intermediate Data Mining Tutorial)
  You saw from exploring the forecasting model that although sales in most of the regions follow a similar pattern, some regions and some models, such as the M200 model in the Pacific region, exhibit very different trends. This does not surprise you, as you know that differences among regions are common and can be caused by many factors, including marketing promotions, inaccurate reporting, or geopolitical events.  
  
 However, your users are asking for a model that can be applied worldwide. Therefore, to minimize the effect of individual factors on projections, you decide to build a model that is based on aggregated measures of worldwide sales. You can then use this model to make predictions for each individual region.  
  
 In this task, you will build all the data sources that you need to perform the advanced prediction tasks. You will create two data source views for use as inputs to the prediction query, and one data source view to use in building a new model.  
  
 **Steps**  
  
1.  [Prepare the extended sales data (for prediction)](#bkmk_newExtendData)  
  
2.  [Prepare the aggregated data (for building the model)](#bkmk_newReplaceData)  
  
3.  [Prepare the series data (for cross-prediction)](#bkmk_CrossData2)  
  
4.  [Predict using EXTEND](../../2014/tutorials/time-series-predictions-using-updated-data-intermediate-data-mining-tutorial.md)  
  
5.  [Create the cross-prediction model](../../2014/tutorials/time-series-predictions-replacement-data-intermediate-data-mining.md)  
  
6.  [Predict using REPLACE](../../2014/tutorials/time-series-predictions-replacement-data-intermediate-data-mining.md)  
  
7.  [Review the new predictions](../../2014/tutorials/comparing-predictions-for-forecasting-models-intermediate-data-mining-tutorial.md)  
  
##  <a name="bkmk_newExtendData"></a> Creating the New Extended Sales Data  
 To update your sales data, you will need to get the latest sales figures. Of particular interest are the data just in from the Pacific region, which launched a regional sales promotion to call attention to the new stores and raise awareness of their products.  
  
 For this scenario, we'll assume that the data has been imported from an Excel workbook that contains just three months of new data for a couple of regions. You'll create a table for the data using a Transact-SQL script, and then define a data source view to use for prediction.  
  
#### Create the table with new sales data  
  
1.  In a Transact-SQL query window, execute the following statement to add the sales data to the AdventureWorksDW database (or any other database).  
  
    ```  
    USE [database name];  
    GO  
    IF OBJECT_ID ([dbo].[NewSalesData]) IS NOT NULL   
        DROP TABLE [dbo].[NewSalesData];  
    GO  
    CREATE TABLE [dbo].[NewSalesData]([Series] [nvarchar](255) NULL,  
    [NewDate] [datetime] NULL,  
    [NewQty] [float] NULL,  
    [NewAmount] [money] NULL) ON [PRIMARY]  
  
    GO  
    ```  
  
2.  Insert the new values using the following script.  
  
    ```  
    INSERT INTO [NewSalesData]  
    (Series,NewDate,NewQty,NewAmount)  
    VALUES('T1000 Pacific', '7/25/08', 55, '$130,170.22'),  
    ('T1000 Pacific', '8/25/08', 50, '$114,435.36 '),  
    ('T1000 Pacific', '9/25/08', 50, '$117,296.24 '),  
    ('T1000 Europe', '7/25/08', 37, '$88,210.00 '),  
    ('T1000 Europe', '8/25/08', 41, '$97,746.00 '),  
    ('T1000 Europe', '9/25/08', 37, '$88,210.00 '),  
    ('T1000 North America', '7/25/08', 69, '$164,500.00 '),  
    ('T1000 North America', '8/25/08', 66, '$157,348.00 '),  
    ('T1000 North America', '9/25/08', 58, '$138,276.00 '),  
    ('M200 Pacific', '7/25/08', 65, '$149,824.35'),  
    ('M200 Pacific', '8/25/08', 54,  '$124,619.46'),  
    ('M200 Pacific', '9/25/08', 61, '$141,143.39'),  
    ('M200 Europe', '7/25/08', 75, '$173,026.00'),  
    ('M200 Europe', '8/25/08', 76, '$175,212.00'),  
    ('M200 Europe', '9/25/08', 84, '$193,731.00'),  
    ('M200 North America', '7/25/08', 94, '$216,916.00'),  
    ('M200 North America', '8/25/08', 94, '$216,891.00'),  
    ('M200 North America', '9/25/08', 91,'$209,943.00');  
    ```  
  
    > [!WARNING]  
    >  The quotation marks are used with the currency values to prevent problems with the comma separator and the currency symbol. You could also pass in the currency values in this format: `130170.22`  
    >   
    >  Note that the dates used in the sample database have changed for this release. If you are using an earlier edition of AdventureWorks, you might need to adjust the inserted dates accordingly.  
  
###  <a name="bkmk_newReplaceData"></a> Create a data source view using the new sales data  
  
1.  In **Solution Explorer**, right-click **Data Source Views**, and then select **New Data Source View**.  
  
2.  In the Data Source View wizard, make the following selections:  
  
     **Data Source**: [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)]  
  
     **Select Tables and Views**: Select the table that you just created, NewSalesData.  
  
3.  Click **Finish**.  
  
4.  In the Data Source View design surface, right-click NewSalesData, and then select **Explore Data** to verify the data.  
  
> [!WARNING]  
>  You will use this data for prediction only, so it does not matter that the data is incomplete.  
  
##  <a name="bkmk_CrossData2"></a> Creating the Data for the Cross-Prediction Model  
 The data that was used in the original forecasting model was already grouped somewhat by the view vTimeSeries, which collapsed several bike models into a smaller number of categories, and merged results from individual countries into regions. To create a model that can be used for world-wide projections, you will create some additional simple aggregations directly in the Data Source View Designer. The new data source view will contain just a sum and an average of the sales of all products for all regions.  
  
 After you have created the data source used for the model, you must create a new data source view to use for prediction. For example, if you want to predict sales for Europe using the new worldwide model, you must feed in data for the Europe region only. So you will set up a new data source view that filters the original data, and change the filter condition for each set of prediction queries.  
  
#### To create the model data using a custom data source view  
  
1.  In **Solution Explorer**, right-click **Data Source Views**, and then select **New Data Source View**.  
  
2.  On the welcome page of the wizard, click **Next**.  
  
3.  On the **Select Data Source** page, select [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)], and then click **Next**.  
  
4.  In the page, **Select Tables and Views**, do not add any tables-just click **Next**.  
  
5.  On the page, **Completing the Wizard**, type the name `AllRegions`, and then click **Finish**.  
  
6.  Next, right-click the blank data source view design surface, and then select **New Named Query**.  
  
7.  In the **Create Named Query** dialog box, for **Name**, type `AllRegions`, and for **Description**, type **Sum and average of sales for all models and regions**.  
  
8.  In the SQL text pane, type the following statement and then click OK:  
  
    ```  
    SELECT ReportingDate,   
    SUM([Quantity]) as SumQty, AVG([Quantity]) as AvgQty,  
    SUM([Amount]) AS SumAmt, AVG([Amount]) AS AvgAmt,  
    'All Regions' as [Region]  
    FROM dbo.vTimeSeries   
    GROUP BY ReportingDate  
    ```  
  
9. Right-click the `AllRegions` table, and then select **Explore Data**.  
  
###  <a name="bkmk_CrossData"></a> To create the series data for cross-prediction  
  
1.  In **Solution Explorer**, right-click **Data Source Views**, and then select **New Data Source View**.  
  
2.  In the Data Source View wizard, make the following selections:  
  
     **Data Source**: [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)]  
  
     **Select Tables and Views**: Do not select any tables  
  
     **Name**: `T1000 Pacific Region`  
  
3.  Click **Finish**.  
  
4.  Right-click the empty design surface for **T1000 Pacific Region.dsv**, and then select **New Named Query**.  
  
     The **Create Named Query** dialog box appears. Retype the name, and then add the following description:  
  
     **Name**: `T1000 Pacific Region`  
  
     **Description**: **Filter`vTimeSeries`by region and model**  
  
5.  In the text pane, type the following query, and then click OK:  
  
    ```  
    SELECT ReportingDate, ModelRegion, Quantity, Amount  
    FROM dbo.vTimeSeries  
    WHERE (ModelRegion = N'T1000 Pacific')  
    ```  
  
    > [!NOTE]  
    >  Since you will need to create predictions for each series separately, you might want to copy the query text and save it to a text file so that you can re-use it for the other data series.  
  
6.  In the Data Source View design surface, right-click T1000 Pacific, and then select **Explore Data** to verify that the data is filtered correctly.  
  
     You will use this data as the input to the model when creating cross-prediction queries.  
  
## Next Task in Lesson  
 [Time Series Predictions using Updated Data &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/time-series-predictions-using-updated-data-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Microsoft Time Series Algorithm](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm.md)   
 [Microsoft Time Series Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)   
 [Data Source Views in Multidimensional Models](../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)  
  
  
