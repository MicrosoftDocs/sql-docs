---
title: "Lesson 5: Extending the Time Series Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 7aad4946-c903-4e25-88b9-b087c20cb67d
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 5: Extending the Time Series Model
  In [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] Enterprise, you can add new data to a time series model and automatically incorporate the new data into the model. You add new data to a time series mining model in one of two ways:  
  
-   Use a PREDICTION JOIN to join data in an external source to the training data.  
  
-   Use a singleton prediction query to provide data one slice at a time.  
  
 For example, assume that you trained the mining model on existing sales data some months ago. When you get new sales, you might want to update the sales predictions to incorporate the new data. You can do this in one step, by supplying the new sales figures as input data and generating new predictions based on the composite data set.  
  
## Making Predictions with EXTEND_MODEL_CASES  
 The following are generic examples of a time series prediction using EXTEND_MODEL_CASES. The first example enables you to specify the number of predictions starting from the last time step of the original model:  
  
```  
SELECT [<model columns>,] PredictTimeSeries(<table column reference>, n, EXTEND_MODEL_CASES)   
FROM <mining model>  
PREDICTION JOIN <source query>  
[WHERE <criteria>]  
```  
  
 The second example enables you to specify the time step where predictions should start, and where they should end. This option is important when you extend the model cases because, by default, the time steps used for prediction queries always start at the end of the original series.  
  
```  
SELECT [<model columns>,] PredictTimeSeries(<table column reference>, n-start, n-end, EXTEND_MODEL_CASES)   
FROM <mining model>  
PREDICTION JOIN <source query>  
[WHERE <criteria>}  
```  
  
 In this tutorial, you will create both kinds of queries.  
  
#### To create a singleton prediction query on a time series model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the singleton statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    SELECT [<model columns>,] PredictTimeSeries(<table column reference>, n, EXTEND_MODEL_CASES)   
    ```  
  
     with:  
  
    ```  
    SELECT [Model Region],  
    PredictTimeSeries([Quantity],6, EXTEND_MODEL_CASES) AS PredictQty  
    ```  
  
     The first line retrieves a value from the model that identifies the series.  
  
     The second line contains the prediction function, which gets 6 predictions for Quantity. An alias, `PredictQty`, is assigned to the prediction result column to make it easier to understand the results.  
  
4.  Replace the following:  
  
    ```  
    FROM <mining model>  
    ```  
  
     with:  
  
    ```  
    FROM [Forecasting_MIXED]  
    ```  
  
5.  Replace the following:  
  
    ```  
    PREDICTION JOIN <source query>  
    ```  
  
     with:  
  
    ```  
    NATURAL PREDICTION JOIN   
    (  
       SELECT 1 AS [Reporting Date],  
       '10' AS [Quantity],  
       'M200 Europe' AS [Model Region]  
       UNION SELECT  
       2 AS [Reporting Date],  
       15 AS [Quantity]),  
       'M200 Europe' AS [Model Region]  
    ) AS t  
    ```  
  
6.  Replace the following:  
  
    ```  
    [WHERE <criteria>]  
    ```  
  
     with:  
  
    ```  
    WHERE [ModelRegion] = 'M200 Europe' OR  
    [ModelRegion] = 'M200 Pacific'  
    ```  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT [Model Region],  
    PredictTimeSeries([Quantity],6, EXTEND_MODEL_CASES) AS PredictQty  
    FROM  
       [Forecasting_MIXED]  
    NATURAL PREDICTION JOIN   
       SELECT 1 AS [ReportingDate],  
      '10' AS [Quantity],  
      'M200 Europe' AS [ModelRegion]  
    UNION SELECT  
      2 AS [ReportingDate],  
      15 AS [Quantity]),  
      'M200 Europe' AS [ModelRegion]  
    ) AS t  
    WHERE [ModelRegion] = 'M200 Europe' OR  
    [ModelRegion] = 'M200 Pacific'  
    ```  
  
7.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
8.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Singleton_TimeSeries_Query.dmx`.  
  
9. On the toolbar, click the **Execute** button.  
  
     The query returns predictions of sales quantity for the M200 bicycle in the Europe and Pacific regions.  
  
## Understanding Prediction Start with EXTEND_MODEL_CASES  
 Now that you have created predictions based on the original model, and with new data, you can compare the results to see how updating the sales data affects the predictions. Before you do so, review the code that you just created, and notice the following:  
  
-   You supplied new data for only the Europe region.  
  
-   You supplied only two months' worth of new data.  
  
 The following table shows how the new values supplied for M200 Europe affect predictions. You did not provide any new data for the M200 product in the Pacific region, but this series is presented for comparison:  
  
 **Product and Region: M200 Europe**  
  
|||||  
|-|-|-|-|  
|||Existing model (`PredictTimeSeries`)|Model with updated sales data (`PredictTimeSeries` with `EXTEND_MODEL_CASES`)|  
|M200 Europe|7/25/2008 12:00:00 AM|77|10|  
|M200 Europe|8/25/2008 12:00:00 AM|64|15|  
|M200 Europe|9/25/2008 12:00:00 AM|59|72|  
|M200 Europe|10/25/2008 12:00:00 AM|56|69|  
|M200 Europe|11/25/2008 12:00:00 AM|56|68|  
|M200 Europe|12/25/2008 12:00:00 AM|74|89|  
  
 **Product and Region: M200 Pacific**  
  
|||||  
|-|-|-|-|  
|||Existing model (`PredictTimeSeries`)|Model with updated sales data (`PredictTimeSeries` with `EXTEND_MODEL_CASES`)|  
|M200 Pacific|7/25/2008 12:00:00 AM|41|41|  
|M200 Pacific|8/25/2008 12:00:00 AM|44|44|  
|M200 Pacific|9/25/2008 12:00:00 AM|38|38|  
|M200 Pacific|10/25/2008 12:00:00 AM|41|41|  
|M200 Pacific|11/25/2008 12:00:00 AM|36|36|  
|M200 Pacific|12/25/2008 12:00:00 AM|39|39|  
  
 From these results, you can see two things:  
  
-   The first two predictions for the M200 Europe series are exactly the same as the new data you supplied. By design, Analysis Services returns the actual new data points instead of making a prediction. That is because when you extend the model cases, the time steps used for prediction queries always start at the end of the original series. Therefore, if you add two new data points, the first two predictions returned overlap with the new data.  
  
-   After all the new data points are used up, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] makes predictions based on the updated model. Therefore, starting in September 2005, you can see the difference between predictions for M200 Europe from the original model, in the left-hand column, and the model that uses EXTEND_MODEL_CASES, in the right-hand column. The predictions are different because the model has been updated with the new data.  
  
## Using Start and End Time Steps to Control Predictions  
 When you extend a model, the new data is always attached to the end of the series. However, for the purpose of prediction, the time slices used for prediction queries start at the end of the original series. If you want to obtain only the new predictions when you add the new data, you must specify the starting point as a number of time slices. For example, if you are adding two new data points and want to make four new predictions, you would do the following:  
  
-   Create a PREDICTION JOIN on a time series model, and specify two months of new data.  
  
-   Request predictions for four time slices, where the starting point is 3, and the ending point is time slice 6.  
  
 In other words, if your new data contains n time slices, and you request predictions for time steps 1 through n, the predictions will coincide with the same period as the new data. To get new predictions for a time periods not covered by your data, you must either start predictions at the time slice n+1 after the new data series, or make sure that you request additional time slices.  
  
> [!NOTE]  
>  You cannot make historical predictions when you add new data.  
  
 The following example shows the DMX statement that lets you get only the new predictions for the two series in the previous example.  
  
```  
SELECT [Model Region],  
PredictTimeSeries([Quantity],3,6, EXTEND_MODEL_CASES) AS PredictQty  
FROM  
   [Forecasting_MIXED]  
NATURAL PREDICTION JOIN   
   SELECT 1 AS [ReportingDate],  
  '10' AS [Quantity],  
  'M200 Europe' AS [ModelRegion]  
UNION SELECT  
  2 AS [ReportingDate],  
  15 AS [Quantity]),  
  'M200 Europe' AS [ModelRegion]  
) AS t  
WHERE [ModelRegion] = 'M200 Europe'  
```  
  
 The prediction results start at time slice 3, which is after the 2 months of new data that you supplied.  
  
 **Product and Region: M200 Europe**  
  
 Model with updated data (PredictTimeSeries with EXTEND_MODEL_CASES)  
  
||||  
|-|-|-|  
|M200 Europe|9/25/2008 12:00:00 AM|72|  
|M200 Europe|10/25/2008 12:00:00 AM|69|  
|M200 Europe|11/25/2008 12:00:00 AM|68|  
|M200 Europe|12/25/2008 12:00:00 AM|89|  
  
## Making Predictions with REPLACE_MODEL_CASES  
 Replacing the model cases is useful when you want to train a model on one set of cases and then apply that model to a different data series. A detailed walkthrough of this scenario is presented in [Lesson 2: Building a Forecasting Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-2-building-a-forecasting-scenario-intermediate-data-mining-tutorial.md).  
  
## See Also  
 [Time Series Model Query Examples](../../2014/analysis-services/data-mining/time-series-model-query-examples.md)   
 [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx)  
  
  
