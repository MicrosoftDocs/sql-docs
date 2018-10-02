---
title: "Time Series Predictions using Updated Data (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: af73681d-ce1c-4b6e-b195-6df3d2fb5275
author: minewiskan
ms.author: owend
manager: craigg
---
# Time Series Predictions using Updated Data (Intermediate Data Mining Tutorial)
    
## Creating Predictions using the Extended Sales Data  
 In this lesson, you will create a prediction query that adds the new sales data to the model. By extending the model with new data, you can get up-to-date predictions that include the newest data points.  
  
 Creating time series predictions that use new data is easy: you simply add the parameter EXTEND_MODEL_CASES to the [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx) function, specify the source of the new data, and specify how many predictions you want to get.  
  
> [!WARNING]  
>  The parameter EXTEND_MODEL_CASES is optional; by default the model is extended any time that you create a time series prediction query by joining new data as inputs.  
  
#### To build the prediction query and add new data  
  
1.  If the model is not already open, double-click the Forecasting structure, and in Data Mining Designer, click the **Mining Model Prediction** tab.  
  
2.  In the **Mining Model** pane, the model Forecasting should already be selected. If it is not selected, click **Select Model**, and then select the model, Forecasting.  
  
3.  In the **Select Input Table(s)** pane, click **Select Case Table**.  
  
4.  In the **Select Table** dialog box, select the data source, [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)].  
  
     From the list of data source views, select NewSalesData and then click **OK**.  
  
5.  Right-click the surface of the design area and select **Modify Connections**.  
  
6.  Using the **Modify Mapping** dialog box, map the columns in the model to the columns in the external data as follows:  
  
    -   Map the ReportingDate column in the mining model to the NewDate column in the input data.  
  
    -   Map the Amount column in the mining model to the NewAmount column in the input data.  
  
    -   Map the Quantity column in the mining model to the NewQty column in the input data.  
  
    -   Map the ModelRegion column in the mining model to the Series column in the input data.  
  
7.  Now you will build the prediction query.  
  
     First, add a column to the prediction query to output the series the prediction applies to.  
  
    1.  In the grid, click the first empty row, under **Source**, and then select Forecasting.  
  
    2.  In the **Field** column, select Model Region and for **Alias**, type `Model Region`.  
  
8.  Next, add and edit the prediction function.  
  
    1.  Click an empty row, and under **Source**, select **Prediction Function**.  
  
    2.  For **Field**, select **PredictTimeSeries**.  
  
    3.  For **Alias**, type **Predicted Values**.  
  
    4.  Drag the field Quantity from the **Mining Model** pane into the **Criteria/Argument** column.  
  
    5.  In the **Criteria/Argument** column, after the field name, type the following text:  **5,EXTEND_MODEL_CASES**  
  
         The complete text of the **Criteria/Argument** text box should be as follows: `[Forecasting].[Quantity],5,EXTEND_MODEL_CASES`  
  
9. Click **Results** and review the results.  
  
     The predictions begin in July (the first time slice after the end of the original data) and end at November (the fifth time slice after the end of the original data).  
  
 You can see that to use this type of prediction query effectively, you need to know when the old data ends, as well as how many time slices there are in the new data.  
  
 For example, in this model, the original data series ended in June, and the data is for the months of July, August, and September.  
  
 Predictions that use EXTEND_MODEL_CASES always begin at the end of the original data series. Therefore, if you want to get only the predictions for the unknown months, you need to specify the starting point and the end point for prediction. Both values are specified as a number of time slices starting at the end of the old data.  
  
 The following procedure demonstrates how to do this.  
  
### Change the start and end points of the predictions  
  
1.  In Prediction Query Builder, click **Query** to switch to DMX view.  
  
2.  Locate the DMX statement that contains the PredictTimeSeries function and change it as follows:  
  
     `PredictTimeSeries([Forecasting 12].[Quantity],4,6,EXTEND_MODEL_CASES)`  
  
3.  Click **Results** and review the results.  
  
     Now the predictions begin at October (the fourth time slice, counting from the end of the original data) and end at December (the sixth time slice, counting from the end of the original data).  
  
## Next Task in Lesson  
 [Time Series Predictions using Replacement Data &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/time-series-predictions-replacement-data-intermediate-data-mining.md)  
  
## See Also  
 [Microsoft Time Series Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)   
 [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-time-series-models-analysis-services-data-mining.md)  
  
  
