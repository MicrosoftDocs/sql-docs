---
title: "Creating Time Series Predictions (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: fb22cffa-ac99-4d34-ac4a-9c93068e33e8
author: minewiskan
ms.author: owend
manager: kfile
---
# Creating Time Series Predictions (Intermediate Data Mining Tutorial)
  In the previous tasks in this lesson, you created a time series model and explored the results. By default, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] always creates a set of five (5) predictions for a time series model and displays the predicted values as part of the forecasting chart. However, you can also create forecasts by building Data Mining Extensions (DMX) prediction queries.  
  
 In this task, you will create a prediction query that generates the same predictions that you saw in the viewer. This task assumes that you have already completed the lessons in the Basic Data Mining Tutorial and are familiar with how to use Prediction Query Builder. You will now learn how to create queries specific to time series models.  
  
## Creating Time Series Predictions  
 Typically, the first step in creating a prediction query is to select a mining model and input table. However, a time series model does not require additional input for a regular prediction. Therefore, you do not need to specify a new source of data when making predictions, unless you are adding data to the model or replacing the data.  
  
 For this lesson, you must specify the number of prediction steps. You can specify the series name, to get a prediction for a particular combination of a product and a region.  
  
#### To select a model and input table  
  
1.  On the **Mining Model Prediction** tab of the Data Mining Designer, in the **Mining Model** box, click **Select Model**.  
  
2.  In the **Select Mining Model** dialog box, expand the Forecasting structure, select the **Forecasting** model from the list, and then click **OK**.  
  
3.  Ignore the **Select Input Table(s)** box.  
  
    > [!NOTE]  
    >  For a time series model, you do not need to specify a separate input unless you are doing cross-prediction.  
  
4.  In the **Source** column, in the grid on the **Mining Model Prediction** tab, click the cell in the first empty row, and then select **Forecasting mining model**.  
  
5.  In the **Field** column, select **Model Region**.  
  
     This action adds the series identifier to the prediction query to indicate the combination of model and region to which the prediction applies.  
  
6.  Click the next empty row in the **Source** column, and then select **Prediction Function**.  
  
7.  In the **Field** column, select **PredictTimeSeries**.  
  
    > [!NOTE]  
    >  You can also use the `Predict` function with time series models. However, by default, the Predict function creates only one prediction for each series. Therefore, to specify multiple prediction steps, you must use the **PredictTimeSeries** function.  
  
8.  In the **Mining Model** pane, select the mining model column, **Amount.** Drag Amount to the **Criteria/Arguments** box for the **PredictTimeSeries** function that you added earlier.  
  
9. Click the **Criteria/Arguments** box, and type a comma, followed by **5**, after the field name.  
  
     The text in the **Criteria/Arguments** box should now display the following:  
  
     `[Forecasting].[Amount],5`  
  
10. In the **Alias** column, type `PredictAmount`.  
  
11. Click the next empty row in the **Source** column, and then select **Prediction Function** again.  
  
12. In the **Field** column, select **PredictTimeSeries**.  
  
13. In the **Mining Model** pane, select the column Quantity, and then drag it into the **Criteria/Arguments** box for the second **PredictTimeSeries** function.  
  
14. Click the **Criteria/Arguments** box, and type a comma, followed by **5**, after the field name.  
  
     The text in the **Criteria/Arguments** box should now display the following:  
  
     `[Forecasting].[ Quantity],5`  
  
15. In the **Alias** column, type `PredictQuantity`.  
  
16. Click **Switch to query result view**.  
  
     The results of the query are displayed in tabular format.  
  
 Remember that you created three different types of results in the query builder, one that uses values from a column, and two that get predicted values from a prediction function. Therefore, the results of the query contain three separate columns. The first column contains the list of product and region combinations. The second and third columns each contain a nested table of prediction results. Each nested table contains the time step and predicted values, such as the following table:  
  
 Example results (amounts are truncated to two decimal places):  
  
 **M200 Europe PredictAmount**  
  
|$TIME|Amount|  
|-----------|------------|  
|7/25/2008|99978.00|  
|8/25/2008|145575.07|  
|9/25/2008|116835.19|  
|10/25/2008|116537.38|  
|11/25/2008|107760.55|  
  
 **M200 Europe PredictQuantity**  
  
|$TIME|Quantity|  
|-----------|--------------|  
|7/25/2008|52|  
|8/25/2008|67|  
|9/25/2008|58|  
|10/25/2008|57|  
|11/25/2008|54|  
  
 **M200 North America - PredictAmount**  
  
|$TIME|Amount|  
|-----------|------------|  
|7/25/2008|348533.93|  
|8/25/2008|340097.98|  
|9/25/2008|257986.19|  
|10/25/2008|374658.24|  
|11/25/2008|379241.44|  
  
 **M200 North America - PredictQuantity**  
  
|$TIME|Quantity|  
|-----------|--------------|  
|7/25/2008|272|  
|8/25/2008|152|  
|9/25/2008|250|  
|10/25/2008|181|  
|11/25/2008|290|  
  
> [!WARNING]  
>  The dates that are used in the sample database have changed for this release. If you are using an earlier version of the sample data, you might see different results.  
  
## Saving the Prediction Results  
 You have several different options for using the prediction results. You can flatten the results, copy the data from the Results view, and paste it into an Excel worksheet or other file.  
  
 To simplify the process of saving results, Data Mining Designer also provides the ability to save the data to a data source view. The functionality for saving results to a data source view is available only in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. The results can only be stored in a flattened format.  
  
#### To flatten the results in the Results pane  
  
1.  In the Prediction Query Builder, click **Switch to query design view**.  
  
     The view changes to allow manual editing of the DMX query text.  
  
2.  Type the `FLATTENED` keyword after the `SELECT` keyword. The complete query text should be as follows:  
  
    ```  
    SELECT FLATTENED  
      [Forecasting].[Model Region],  
      (PredictTimeSeries([Forecasting].[Amount],5)) as [PredictAmount],  
      (PredictTimeSeries([Forecasting].[Quantity],5)) as [PredictQuantity]  
    FROM  
      [Forecasting]  
    ```  
  
3.  Optionally, you can type a clause to restrict the results, such as the following example:  
  
    ```  
    SELECT FLATTENED  
      [Forecasting].[Model Region],  
      (PredictTimeSeries([Forecasting].[Amount],5)) as [PredictAmount],  
      (PredictTimeSeries([Forecasting].[Quantity],5)) as [PredictQuantity]  
    FROM  
      [Forecasting]  
    WHERE [Forecasting].[Model Region] = 'M200 North America'   
    OR [Forecasting].[Model Region] = 'M200 Europe'  
  
    ```  
  
4.  Click **Switch to query result view**.  
  
#### To export prediction query results  
  
1.  Click **Save query results**.  
  
2.  In the **Save Data Mining Query Result** dialog box, for **Data Source**, select [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)]. You can also create a data source if you want to save the data to a different relational database.  
  
3.  In the **Table Name** column, type a new temporary table name, such as **Test Predictions**.  
  
4.  Click **Save**.  
  
    > [!NOTE]  
    >  To view the table that you created, create a connection to the database engine of the instance where you saved the data, and create a query.  
  
## Conclusion  
 You have learned how to build a basic time series model, interpret the forecasts, and create predictions.  
  
 The remaining tasks in this tutorial are optional, and describe advanced time series predictions. If you decide to go on, you will learn how to add new data to your model and create predictions on the extended series. You will also learn how to perform cross-prediction, by using the trend in the model but replacing the data with a new series of data.  
  
## Next Lesson  
 [Advanced Time Series Predictions &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/advanced-time-series-predictions-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Time Series Model Query Examples](../../2014/analysis-services/data-mining/time-series-model-query-examples.md)  
  
  
