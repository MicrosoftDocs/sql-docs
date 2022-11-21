---
title: "PredictTimeSeries (DMX)"
description: "PredictTimeSeries (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# PredictTimeSeries (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns predicted future values for time series data. Time series data is continuous and can be stored in a nested table or in a case table. The **PredictTimeSeries** function always returns a nested table.  
  
## Syntax  
  
```  
  
PredictTimeSeries(<table column reference>)  
PredictTimeSeries(<table column reference>, n)  
PredictTimeSeries(<table column reference>, n-start, n-end)  
PredictTimeSeries(<scalar column reference>)  
PredictTimeSeries(<scalar column reference>, n)  
PredictTimeSeries(<scalar column reference>, n-start, n-end)  
PredictTimeSeries(<table column reference>, n, REPLACE_MODEL_CASES | EXTEND_MODEL_CASES) PREDICTION JOIN <source query>  
PredictTimeSeries(<table column reference>, n-start, n-end, REPLACE_MODEL_CASES | EXTEND_MODEL_CASES) PREDICTION JOIN <source query>  
PredictTimeSeries(<scalar column reference>, n, REPLACE_MODEL_CASES | EXTEND_MODEL_CASES) PREDICTION JOIN <source query>  
PredictTimeSeries(<scalar column reference>, n-start, n-end, REPLACE_MODEL_CASES | EXTEND_MODEL_CASES) PREDICTION JOIN <source query>  
```  
  
## Arguments  
 *\<table column reference>*, *\<scalar column referenc>*  
 Specifies the name of the column to predict. The column can contain either scalar or tabular data.  
  
 *n*  
 Specifies the number of next steps to predict. If a value is not specified for *n*, the default is 1.  
  
 *n* cannot be 0. The function returns an error if you do not make at least one prediction.  
  
 *n-start, n-end*  
 Specifies a range of time series steps.  
  
 *n-start* must be an integer and cannot be 0.  
  
 *n-end* must be an integer greater than *n-start*.  
  
 *\<source query>*  
 Defines the external data that is used for making predictions.  
  
 REPLACE_MODEL_CASES | EXTEND_MODEL_CASES  
 Indicates how to handle new data.  
  
 REPLACE_MODEL_CASES specifies that the data points in the model should be replaced with new data. However, predictions are based on the patterns in the existing mining model.  
  
 EXTEND_MODEL_CASES specifies that the new data should be added to the original training data set. Future predictions are made on the composite data set only after the new data has been used up.  
  
 These arguments can be used only when new data is added by using a PREDICTION JOIN statement. If you use a PREDICTION JOIN query and do not specify an argument, the default is EXTEND_MODEL_CASES.  
  
## Return Type  
 A \<*table expression*>.  
  
## Remarks  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm does not support historical prediction when you use the PREDICTION JOIN statement to add new data.  
  
 In a PREDICTION JOIN, the prediction process always starts at the time step immediately after the end of the original training series. This is true even if you add new data. Therefore, the *n* parameter and *n-start* parameter values must be an integer greater than 0.  
  
> [!NOTE]  
>  The length of the new data does not affect the starting point for prediction. Therefore, if you want to add new data and also make new predictions, make sure that you either set the prediction start point to a value greater than the length of the new data, or extend the prediction end point by the length of the new data.  
  
## Examples  
 The following examples show how to make predictions against an existing time series model:  
  
-   The first example shows how to make a specified number of predictions based on the current model.  
  
-   The second example shows how to use the REPLACE_MODEL_CASES parameter to apply the patterns in the specified model to a new set of data.  
  
-   The third example shows how to use the EXTEND_MODEL_CASES parameter to update a mining model with fresh data.  
  
 To learn more about working with time series models, see the data mining tutorial, [Lesson 2: Building a Forecasting Scenario &#40;Intermediate Data Mining Tutorial&#41;](/previous-versions/sql/sql-server-2016/ms169846(v=sql.130)) and [Time Series Prediction DMX Tutorial](/previous-versions/sql/sql-server-2016/cc879270(v=sql.130)).  
  
> [!NOTE]  
>  You might obtain different results from your model; the results of the examples below are provided only to illustrate the result format.  
  
### Example 1: Predicting a Number of Time Slices  
 The following example uses the **PredictTimeSeries** function to return a prediction for the next three time steps, and restricts results to the M200 series in the Europe and Pacific regions. In this particular model, the predictable attribute is Quantity, so you must use `[Quantity]` as the first argument to the PredictTimeSeries function.  
  
```  
SELECT FLATTENED  
    [Forecasting].[Model Region],  
    PredictTimeSeries([Forecasting].[Quantity],3)AS t   
FROM  
    [Forecasting]  
WHERE [Model Region] = 'M200 Europe'  
OR [Model Region] = 'M200 Pacific'  
```  
  
 Expected results:  
  
|Model Region|t.$TIME|t.Quantity|  
|------------------|-------------|----------------|  
|M200 Europe|7/25/2008 12:00:00 AM|121|  
|M200 Europe|8/25/2008 12:00:00 AM|142|  
|M200 Europe|9/25/2008 12:00:00 AM|152|  
|M200 Pacific|7/25/2008 12:00:00 AM|46|  
|M200 Pacific|8/25/2008 12:00:00 AM|44|  
|M200 Pacific|9/25/2008 12:00:00 AM|42|  
  
 In this example, the FLATTENED keyword has been used to make the results easier to read.  If you do not use the FLATTENED keyword and instead return a hierarchical rowset, this query returns two columns. The first column contains the value for [ModelRegion], and the second column contains a nested table with two columns: $TIME, which shows the time slices that are being predicted, and Quantity, which contains the predicted values.  
  
### Example 2: Adding New Data and Using REPLACE_MODEL_CASES  
 Suppose you find that the data was incorrect for a particular region, and want to use the patterns in the model, but to adjust the predictions to match the new data. Or, you might find that another region has more reliable trends and you want to apply the most reliable model to data from a different region.  
  
 In such scenarios, you can use the REPLACE_MODEL_CASES parameter and specify a new set of data to use as historical data. That way, the projections will be based on the patterns in the specified model, but will continue smoothly from the end of the new data points. For a complete walkthrough of this scenario, see [Advanced Time Series Predictions &#40;Intermediate Data Mining Tutorial&#41;](/previous-versions/sql/sql-server-2016/cc879290(v=sql.130)).  
  
 The following PREDICTION JOIN query illustrates the syntax for replacing data and making new predictions. For the replacement data, the example retrieves the value of the Amount and Quantity columns and multiplies each by two:  
  
```  
SELECT [Forecasting].[Model Region],  
    PredictTimeSeries([Forecasting].[Quantity], 3, REPLACE_MODEL_CASES)   
FROM  
    [Forecasting]  
PREDICTION JOIN  
  OPENQUERY([Adventure Works DW Multidimensional 2012],  
    'SELECT [ModelRegion],   
    ([Quantity] * 2) as Quantity,  
    ([Amount] * 2) as Amount,  
      [ReportingDate]  
    FROM [dbo].vTimeSeries  
    WHERE ModelRegion = N''M200 Pacific''  
    ') AS t  
ON  
  [Forecasting].[Model Region] = t.[ Model Region] AND  
[Forecasting].[Reporting Date] = t.[ReportingDate] AND  
[Forecasting].[Quantity] = t.[Quantity] AND  
[Forecasting].[Amount] = t.[Amount]  
```  
  
 The following tables compare the results of prediction.  
  
 Original predictions:  
  
|Model Region|ReportingDate|Quantity|  
|-|-|-|  
|M200 Pacific|7/25/2008 12:00:00 AM|46|  
|M200 Pacific|8/25/2008 12:00:00 AM|44|  
|M200 Pacific|9/25/2008 12:00:00 AM|42|  
  
 Updated predictions:  
  
|Model Region|ReportingDate|Quantity|  
|-|-|-|  
|M200 Pacific|7/25/2008 12:00:00 AM|91|  
|M200 Pacific|8/25/2008 12:00:00 AM|89|  
|M200 Pacific|9/25/2008 12:00:00 AM|84|  
  
### Example 3: Adding New Data and Using EXTEND_MODEL_CASES  
 Example 3 illustrates the use of the *EXTEND_MODEL_CASES* option to provide new data, which is added to the end of an existing data series. Rather than replacing the existing data points, the new data is added onto the model.  
  
 In the following example, the new data is provided in the SELECT statement that follows NATURAL PREDICTION JOIN. You can supply multiple rows of new input with this syntax, but each new row of input must have a unique time stamp:  
  
```  
SELECT [Model Region],  
    PredictTimeSeries([Forecasting].[Quantity], 5, EXTEND_MODEL_CASES)   
FROM  
    [Forecasting]  
NATURAL PREDICTION JOIN  
    (SELECT  
        1 as [Reporting Date],  
        10 as [Quantity],  
        'M200 Europe' AS [Model Region]  
    UNION SELECT   
        2 as [Reporting Date],  
        15 as [Quantity],  
        'M200 Europe' AS [Model Region]  
) AS T  
WHERE ([Model Region] = 'M200 Europe'  
 OR [Model Region] = 'M200 Pacific')  
```  
  
 Because the query uses the *EXTEND_MODEL_CASES* option, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] takes the following actions for its predictions:  
  
-   Increases the total size of the training cases by adding the two new months of data to the model.  
  
-   Starts the predictions at the end of the previous case data. Therefore, the first two predictions represent the new actual sales data that you just added to the model.  
  
-   Returns new predictions for the remaining three time slices based on the newly expanded model.  
  
 The following table lists the results of the Example 2 query. Notice that the first two values returned for M200 Europe are exactly the same as the new values that you provided. This behavior is by design; if you want to start predictions after the end of the new data, you must specify a starting and ending time step.  
  
 Also, notice that you did not supply new data for the Pacific region. Therefore, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] returns new predictions for all five time slices.  
  
 Quantity: M200 Europe. EXTEND_MODEL_CASES:  
  
|$TIME|Quantity|  
|-----------|--------------|  
|7/25/2008 0:00|10|  
|8/25/2008 0:00|15|  
|9/25/2008 0:00|72|  
|10/25/2008 0:00|69|  
|11/25/2008 0:00|68|  
  
 Quantity:  M200 Pacific. EXTEND_MODEL_CASES:  
  
|$TIME|Quantity|  
|-----------|--------------|  
|7/25/2008 0:00|46|  
|8/25/2008 0:00|44|  
|9/25/2008 0:00|42|  
|10/25/2008 0:00|42|  
|11/25/2008 0:00|38|  
  
## Example 4: Returning Statistics in a Time Series Prediction  
 The **PredictTimeSeries** function does not support *INCLUDE_STATISTICS* as a parameter. However, the following query can be used to return the prediction statistics for a time series query. This approach can also be used with models that have nested table columns.  
  
 In this particular model, the predictable attribute is Quantity, so you must use `[Quantity]` as the first argument to the PredictTimeSeries function. If your model uses a different predictable attribute, you can substitute a different column name.  
  
```  
SELECT FLATTENED [Model Region],  
(SELECT   
     $Time,  
     [Quantity] as [PREDICTION],   
     PredictVariance([Quantity]) AS [VARIANCE],  
     PredictStdev([Quantity]) AS [STDEV]  
FROM  
      PredictTimeSeries([Quantity], 3) AS t  
) AS t  
FROM Forecasting  
WHERE [Model Region] = 'M200 Europe'  
OR [Model Region] = 'M200 North America'  
```  
  
 Sample results:  
  
|Model Region|t.$TIME|t.PREDICTION|t.VARIANCE|t.STDEV|  
|------------------|-------------|------------------|----------------|-------------|  
|M200 Europe|7/25/2008 12:00:00 AM|121|11.6050581415597|3.40661975300439|  
|M200 Europe|8/25/2008 12:00:00 AM|142|10.678201866621|3.26775180615374|  
|M200 Europe|9/25/2008 12:00:00 AM|152|9.86897842568614|3.14149302493037|  
|M200 North America|7/25/2008 12:00:00 AM|163|1.20434529288162|1.20434529288162|  
|M200 North America|8/25/2008 12:00:00 AM|178|1.65031343900634|1.65031343900634|  
|M200 North America|9/25/2008 12:00:00 AM|156|1.68969399185442|1.68969399185442|  
  
> [!NOTE]  
>  The FLATTENED keyword was used in this example to make the results easier to present in a table; however, if your provider supports hierarchical rowsets you can omit the FLATTENED keyword. If you omit the FLATTENED keyword, the query returns two columns, the first column containing the value that identifies the `[Model Region]` data series, and the second column containing the nested table of statistics.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Time Series Model Query Examples](/analysis-services/data-mining/time-series-model-query-examples)   
 [Predict &#40;DMX&#41;](../dmx/predict-dmx.md)  
  
