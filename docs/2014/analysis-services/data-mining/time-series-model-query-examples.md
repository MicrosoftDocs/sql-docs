---
title: "Time Series Model Query Examples | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "time series algorithms [Analysis Services]"
  - "MISSING_VALUE_SUBSTITUTION"
  - "time series [Analysis Services]"
  - "predictions [Analysis Services], time series"
  - "EXTEND_MODEL_CASES parameter"
  - "REPLACE_MODEL_CASES parameter"
  - "prediction queries [DMX]"
  - "PREDICTION_SMOOTHING"
  - "content queries [DMX]"
ms.assetid: 9a1c527e-2997-493b-ad6a-aaa71260b018
author: minewiskan
ms.author: owend
manager: craigg
---
# Time Series Model Query Examples
  When you create a query against a data mining model, you can create either a content query, which provides details about the patterns discovered in analysis, or you can create a prediction query, which uses the patterns in the model to make predictions for new data. For example, a content query for a time series model might provide additional details about the periodic structures that were detected, while a prediction query might give you predictions for the next 5-10 time slices. You can also retrieve metadata about the model by using a query.  
  
 This section explains how to create both kinds of queries for models that are based on the Microsoft Time Series algorithm.  
  
 **Content Queries**  
  
 [Retrieving Periodicity Hints for the Model](#bkmk_Query1)  
  
 [Retrieving the Equation for an ARIMA Model](#bkmk_Query2)  
  
 [Retrieving the Equation for an ARTxp Model](#bkmk_Query3)  
  
 **Prediction Queries**  
  
 [Understanding When to Replace and When to Extend Time Series Data](#bkmk_ReplaceExtend)  
  
 [Making Predictions with EXTEND_MODEL_CASES](#bkmk_EXTEND)  
  
 [Making Predictions with REPLACE_MODEL_CASES](#bkmk_REPLACE)  
  
 [Missing Value Substitution in Time Series Models](#bkmk_MissingValues)  
  
## Getting Information about a Time Series Model  
 A model content query can provide basic information about the model, such as the parameters that were used when the model was created, the time the model was last processed. The following example illustrates the basic syntax for querying the model content by using the data mining schema rowsets.  
  
###  <a name="bkmk_Query1"></a> Sample Query 1: Retrieving Periodicity Hints for the Model  
 You can retrieve the periodicities that were found within the time series by querying the ARIMA tree or the ARTXP tree. However, the periodicities in the completed model might not be the same as the periods that you specified as hints when you created the model. To retrieve the hints that were supplied as parameters when you created the model, you can query the mining model content schema rowset by using the following DMX statement:  
  
```  
SELECT MINING_PARAMETERS   
FROM $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = '<model name>'  
```  
  
 Partial results:  
  
|MINING_PARAMETERS|  
|------------------------|  
|COMPLEXITY_PENALTY=0.1,MINIMUM_SUPPORT=10,PERIODICITY_HINT={1,3},....|  
  
 The default periodicity hint is {1} and appears in all models; this sample model was created with an additional hint that might not be present in the final model.  
  
> [!NOTE]  
>  The results have been truncated here for readability.  
  
###  <a name="bkmk_Query2"></a> Sample Query 2: Retrieving the Equation for an ARIMA Model  
 You can retrieve the equation for an ARIMA model by querying any node in an individual tree. Remember that each tree within an ARIMA model represents a different periodicity, and if there are multiple data series, each data series will have its own set of periodicity trees. Therefore, to retrieve the equation for a specific data series you must identify the tree first.  
  
 For example, the TA prefix tells you that the node is part of an ARIMA tree, whereas the TS prefix is used for ARTXP trees. You can find all ARIMA root trees by querying the model content for nodes with a NODE_TYPE value of 27. You can also use the value of ATTRIBUTE_NAME to find the ARIMA root node for a particular data series. This query example finds the ARIMA nodes that represent quantities sold of the R250 model in the Europe region.  
  
```  
SELECT NODE_UNIQUE_NAME  
FROM Forecasting.CONTENT  
WHERE ATTRIBUTE_NAME = 'R250 Europe: Quantity"  
AND NODE_TYPE = 27  
```  
  
 By using this node ID, you can retrieve details about the ARIMA equation for this tree. The following DMX statement retrieves the short form of the ARIMA equation for the data series. It also retrieves the intercept from the nested table, NODE_DISTRIBUTION. In this example, the equation is obtained by referencing the node unique ID TA00000007. However, you may have to use a different node ID, and you may obtain slightly different results from your model.  
  
```  
SELECT FLATTENED NODE_CAPTION as [Short equation],   
(SELECT ATTRIBUTE_NAME, ATTRIBUTE_VALUE   
FROM NODE_DISTRIBUTION) as t  
FROM Forecasting.CONTENT  
WHERE NODE_NAME = 'TA00000007'  
```  
  
 Example results:  
  
|Short equation|t.ATTRIBUTE_NAME|t.ATTRIBUTE_VALUE|  
|--------------------|-----------------------|------------------------|  
|ARIMA (2,0,7)x(1,0,2)(12)|R250 Europe:Quantity(Intercept)|15.24....|  
|ARIMA (2,0,7)x(1,0,2)(12)|R250 Europe:Quantity(Periodicity)|1|  
|ARIMA (2,0,7)x(1,0,2)(12)|R250 Europe:Quantity(Periodicity)|12|  
  
 For more information about how to interpret this information, see [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-time-series-models-analysis-services-data-mining.md).  
  
###  <a name="bkmk_Query3"></a> Sample Query 3: Retrieving the Equation for an ARTXP Model  
 For an ARTxp model, different information is stored at each level of the tree. For more information about the structure of an ARTxp model, and how to interpret the information in the equation, see [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-time-series-models-analysis-services-data-mining.md).  
  
 The following DMX statement retrieves information a part of the ARTxp tree that represents the quantity of sales for the R250 model in Europe.  
  
> [!NOTE]  
>  The name of the nested table column, VARIANCE, must be enclosed in brackets to distinguish it from the reserved keyword of the same name. The nested table columns, PROBABILITY and SUPPORT, are not included because they are blank in most cases.  
  
```  
SELECT NODE_CAPTION as [Split information],   
(SELECT ATTRIBUTE_NAME, ATTRIBUTE_VALUE,  
   [VARIANCE]  
   FROM NODE_DISTRIBUTION) AS t  
FROM Forecasting.CONTENT  
WHERE NODE_ATTRIBUTE_NAME = 'R250 Europe:Quantity'  
AND NODE_TYPE = 15  
```  
  
 For more information about how to interpret this information, see [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-time-series-models-analysis-services-data-mining.md).  
  
## Creating Predictions on a Time Series Model  
 Beginning in [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)], you can add new data to a time series model and automatically incorporate the new data into the model. You add new data to a time series mining model in one of two ways:  
  
-   Use a `PREDICTION JOIN` to join data in an external source to the training data.  
  
-   Use a singleton prediction query to provide data one slice at a time. For information about how to create a singleton prediction query, see [Data Mining Query Interfaces](data-mining-query-tools.md).  
  
###  <a name="bkmk_ReplaceExtend"></a> Understanding the Behavior of Replace and Extend Operations  
 When you add new data to a time series model, you can specify whether to extend or replace the training data:  
  
-   **Extend:** When you extend a data series, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] adds the new data at the end of the existing training data. The number of training cases also increases.  
  
     Extending the model cases is useful for continuously updating the model with new data. For example, if you want to make the training set grow over time, you would simply extend the model.  
  
     To extend the data, you create a `PREDICTION JOIN` on a time series model, specify the source of the new data, and use the `EXTEND_MODEL_CASES` argument.  
  
-   **Replace:** When you replace the data in the data series, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] keeps the trained model, but uses the new data values to replace some or all of the existing training cases. Therefore, the size of the training data never changes, but the cases themselves are continually being replaced with newer data. If you supply enough new data, you can replace the training data with a completely new series.  
  
     Replacing the model cases is useful when you want to train a model on one set of cases and then apply that model to a different data series.  
  
     To replace the data, you create a `PREDICTION JOIN` on a time series model, specify the source of the new data, and use the `REPLACE_MODEL_CASES` argument.  
  
> [!NOTE]  
>  You cannot make historical predictions when you add new data.  
  
 Regardless of whether you extend or replace the training data, predictions always begin at the time stamp that ends the original training set. In other words, if your new data contains n time slices, and you request predictions for time steps 1 through n, the predictions will coincide with the same period as the new data, and you will not get any new predictions.  
  
 To get new predictions for time periods that do not overlap with the new data, you must either start predictions at the time slice n+1, or make sure that you request additional time slices.  
  
 For example, assume that the existing model has six months' worth of data. You want to extend this model by adding the sales figures from the last three months. At the same time, you want to make a prediction about the next three months. To obtain only the new predictions when you add the new data, specify the starting point as time slice 4, and the ending point as time slice 7. You could also request a total of six predictions, but the time slices for the first three would overlap with the new data just added.  
  
 For query examples and more information about the syntax for using `REPLACE_MODEL_CASES` and `EXTEND_MODEL_CASES`, see [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx).  
  
###  <a name="bkmk_EXTEND"></a> Making Predictions with EXTEND_MODEL_CASES  
 Prediction behavior differs depending on whether you extend or replace the model cases. When you extend a model, the new data is attached to the end of the series and the size of the training set increases. However, the time slices used for prediction queries always start at the end of the original series. Therefore, if you add three new data points and request six predictions, the first three predictions returned overlap with the new data. In this case, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns the actual new data points instead of making a prediction, until all the new data points are used up. Then, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] makes predictions based on the composite series.  
  
 This behavior lets you add new data, and then show your actual sales figures in the prediction chart, instead of seeing projections.  
  
 For example, to add three new data points and make three new predictions, you would do the following:  
  
-   Create a `PREDICTION JOIN` on a time series model, and specify the source of three months' of new data.  
  
-   Request predictions for six time slices. To do this, specify 6 time slices, where the starting point is time slice 1, and the ending point is time slice 7. This is true only for EXTEND_MODEL_CASES.  
  
-   To get only the new predictions, you specify the starting point as 4 and the ending point as 7.  
  
-   You must use the argument `EXTEND_MODEL_CASES`.  
  
     The actual sales figures are returned for the first three time slices, and predictions based on the extended model are returned for the next three time slices.  
  
###  <a name="bkmk_REPLACE"></a> Making Predictions with REPLACE_MODEL_CASES  
 When you replace the cases in a model, the size of the model stays the same but [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] replaces the individual cases in the model. This is useful for cross-prediction and scenarios in which maintaining the training data set at a consistent size is important.  
  
 For example, one of your stores has insufficient sales data. You could create a general model by averaging sales for all stores in a particular region and then training a model. Then, to make predictions for the store without sufficient sales data, you create a `PREDICTION JOIN` on the new sales data for just that store. When you do this, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] keeps the patterns derived from the regional model, but replaces the existing training cases with the data from the individual store. As a result, your prediction values will be closer to the trend lines for the individual store.  
  
 When you use the `REPLACE_MODEL_CASES` argument, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] continually adds new cases to the end of the case set and deletes a corresponding number from the beginning of the case set. If you add more new data than was in the original training set, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] discards the earliest data. If you supply sufficient new values, the predictions can be based on completely new data.  
  
 For example, you trained your model on a case data set that contained 1000 rows. You then add 100 rows of new data. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] drops the first 100 rows from the training set and adds the 100 rows of new data to the end of the set for a total of 1000 rows. If you add 1100 rows of new data, only the most recent 1000 rows are used.  
  
 Here is another example. To add three new month's worth of data and make three new predictions, you would do the following actions:  
  
-   Create a `PREDICTION JOIN` on a time series model and use the `REPLACE_MODEL_CASE` argument.  
  
-   Specify the source of three months' of new data. This data might be from a completely different source than the original training data.  
  
-   Request predictions for six time slices. To do this, specify 6 time slices, or specify the starting point as time slice 1, and the ending point as time slice 7.  
  
    > [!NOTE]  
    >  Unlike `EXTEND_MODEL_CASES`, you cannot return the same values that you added as input data. All six values returned are predictions that are based on the updated model, which includes both old and new data.  
  
    > [!NOTE]  
    >  With REPLACE_MODEL_CASES, starting at timestamp 1 you get new predictions based on the new data, which replaces the old training data.  
  
 For query examples and more information about the syntax for using `REPLACE_MODEL_CASES` and `EXTEND_MODEL_CASES`, see [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx).  
  
###  <a name="bkmk_MissingValues"></a> Missing Value Substitution in Time Series Models  
 When you add new data to a time series model by using a `PREDICTION JOIN` statement, the new dataset cannot have any missing values. If any series is incomplete, the model must supply the missing values by using either a null, a numeric means, a specific numeric mean, or a predicted value. If you specify `EXTEND_MODEL_CASES`, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] replaces the missing values with predictions based on the original model. If you use `REPLACE_MODEL_CASES`, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] replaces the missing values with the value that you specify in the *MISSING_VALUE_SUBSTITUTION* parameter.  
  
## List of Prediction Functions  
 All [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms support a common set of functions. However, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm supports the additional functions, listed in the following table.  
  
|||  
|-|-|  
|Prediction Function|Usage|  
|[Lag &#40;DMX&#41;](/sql/dmx/lag-dmx)|Returns a number of time slices between the date of the current case and the last date of the training set.<br /><br /> A typical use of this function is to identify recent training cases so that you can retrieve detailed data about the cases.|  
|[PredictNodeId &#40;DMX&#41;](/sql/dmx/predictnodeid-dmx)|Returns the node ID for the specified predictable column.<br /><br /> A typical use of this function is to identify the node that generated a particular predicted value so that you can review the cases associated with the node, or retrieve the equation and other details.|  
|[PredictStdev &#40;DMX&#41;](/sql/dmx/predictstdev-dmx)|Returns the standard deviation of the predictions in the specified predictable column.<br /><br /> This function replaces the INCLUDE_STATISTICS argument, which is not supported for time series models.|  
|[PredictVariance &#40;DMX&#41;](/sql/dmx/predictvariance-dmx)|Returns the variance of the predictions for the specified predictable column.<br /><br /> This function replaces the INCLUDE_STATISTICS argument, which is not supported for time series models.|  
|[PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx)|Returns predicted historical values or future predicted values for a time series.<br /><br /> You can also query time series models by using the general prediction function, [Predict &#40;DMX&#41;](/sql/dmx/predict-dmx).|  
  
 For a list of the functions that are common to all [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms, see [General Prediction Functions &#40;DMX&#41;](/sql/dmx/general-prediction-functions-dmx). For the syntax of specific functions, see [Data Mining Extensions &#40;DMX&#41; Function Reference](/sql/dmx/data-mining-extensions-dmx-function-reference).  
  
## See Also  
 [Data Mining Queries](data-mining-queries.md)   
 [Microsoft Time Series Algorithm](microsoft-time-series-algorithm.md)   
 [Microsoft Time Series Algorithm Technical Reference](microsoft-time-series-algorithm-technical-reference.md)   
 [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-time-series-models-analysis-services-data-mining.md)  
  
  
