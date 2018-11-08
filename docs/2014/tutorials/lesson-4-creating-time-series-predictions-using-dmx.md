---
title: "Lesson 4: Creating Time Series Predictions Using DMX | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 6b883e43-209d-489a-8dc3-9349f88acae8
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 4: Creating Time Series Predictions Using DMX
  In this lesson and the following lesson, you will use Data Mining Extensions (DMX) to create different types of predictions based on the time series models that you created in [Lesson 1: Creating a Time Series Mining Model and Mining Structure](../../2014/tutorials/lesson-1-creating-a-time-series-mining-model-and-mining-structure.md) and [Lesson 2: Adding Mining Models to the Time Series Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-time-series-mining-structure.md).  
  
 With a time series model, you have many options for making predictions:  
  
-   Use the existing patterns and data in the mining model  
  
-   Use the existing patterns in the mining model but supply new data  
  
-   Add new data to the model or update the model.  
  
 The syntax for making these prediction types is summarized below:  
  
 Default time series prediction  
 Use [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx) to return the specified number of predictions from the trained mining model.  
  
 For example, see [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx) or [Time Series Model Query Examples](../../2014/analysis-services/data-mining/time-series-model-query-examples.md).  
  
 EXTEND_MODEL_CASES  
 Use [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx) with the EXTEND_MODEL_CASES argument to add new data, extend the series, and create predictions based on the updated mining model.  
  
 This tutorial contains an example of how to use EXTEND_MODEL_CASES.  
  
 REPLACE_MODEL_CASES  
 Use [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx) with the REPLACE_MODEL_CASES argument to replace the original data with a new data series, and then create predictions based on applying the patterns in the mining model to the new data series.  
  
 For an example of how to use REPLACE_MODEL_CASES, see [Lesson 2: Building a Forecasting Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-2-building-a-forecasting-scenario-intermediate-data-mining-tutorial.md).  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Create a query to get the default predictions based on existing data.  
  
 In the following lesson you will perform the following related tasks:  
  
-   Create a query to supply new data and get updated predictions.  
  
 In addition to creating queries manually by using DMX, you can also create predictions by using the prediction query builder in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
## Simple Time Series Prediction Query  
 The first step is to use the `SELECT FROM` statement together with the `PredictTimeSeries` function to create time series predictions. Time series models support a simplified syntax for creating predictions: you do not need to supply any inputs, but only have to specify the number of predictions to create. The following is a generic example of the statement you will use:  
  
```  
SELECT <select list>   
FROM [<mining model name>]   
WHERE [<criteria>]  
```  
  
 The select list can contain columns from the model, such as the name of the product line that you are creating the predictions for, or prediction functions, such as [Lag &#40;DMX&#41;](/sql/dmx/lag-dmx) or [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx), which are specifically for time series mining models.  
  
#### To create a simple time series prediction query  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <select list>   
    ```  
  
     with:  
  
    ```  
    [Forecasting_MIXED].[ModelRegion],  
    PredictTimeSeries([Forecasting_MIXED].[Quantity],6) AS PredictQty,  
    PredictTimeSeries ([Forecasting_MIXED].[Amount],6) AS PredictAmt  
    ```  
  
     The first line retrieves a value from the mining model that identifies the series.  
  
     The second and third lines use the `PredictTimeSeries` function. Each line predicts a different attribute, `[Quantity]` or `[Amount]`. The numbers after the names of the predictable attributes specify the number of time steps to predict.  
  
     The `AS` clause is used to provide a name for the column that is returned by each prediction function. If you do not supply an alias, by default both columns are returned with the label, `Expression`.  
  
4.  Replace the following:  
  
    ```  
    [<mining model>]   
    ```  
  
     with:  
  
    ```  
    [Forecasting_MIXED]  
    ```  
  
5.  Replace the following:  
  
    ```  
    WHERE [criteria>]   
    ```  
  
     with:  
  
    ```  
    WHERE [ModelRegion] = 'M200 Europe' OR  
    [ModelRegion] = 'M200 Pacific'  
    ```  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT  
    [Forecasting_MIXED].[ModelRegion],  
    PredictTimeSeries([Forecasting_MIXED].[Quantity],6) AS PredictQty,  
    PredictTimeSeries ([Forecasting_MIXED].[Amount],6) AS PredictAmt  
    FROM   
    [Forecasting_MIXED]  
    WHERE [ModelRegion] = 'M200 Europe' OR  
    [ModelRegion] = 'M200 Pacific'  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `SimpleTimeSeriesPrediction.dmx`.  
  
8.  On the toolbar, click the **Execute** button.  
  
     The query returns 6 predictions for each of the two combinations of product and region that are specified in the `WHERE` clause.  
  
 In the next lesson, you will create a query that supplies new data to the model, and compare the results of that prediction with the one you just created.  
  
## Next Task in Lesson  
 [Lesson 5: Extending the Time Series Model](../../2014/tutorials/lesson-5-extending-the-time-series-model.md)  
  
## See Also  
 [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx)   
 [Lag &#40;DMX&#41;](/sql/dmx/lag-dmx)   
 [Time Series Model Query Examples](../../2014/analysis-services/data-mining/time-series-model-query-examples.md)   
 [Lesson 2: Building a Forecasting Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-2-building-a-forecasting-scenario-intermediate-data-mining-tutorial.md)  
  
  
