---
title: "Prediction Queries (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: e5e6686c-1360-480e-8c0d-8a56204fbed9
author: minewiskan
ms.author: owend
manager: craigg
---
# Prediction Queries (Data Mining)
  The goal of a typical data mining project is to use the mining model to make predictions. For example, you might want to predict the amount of expected downtime for a certain cluster of servers, or generate a score that indicates whether segments of customers are likely to respond to an advertising campaign. To do all these things, you would create a prediction query.  
  
 Functionally, there are different types of prediction queries supported in SQL Server, depending on the type of inputs to the query:  
  
|Query Type|Query Options|  
|----------------|-------------------|  
|Singleton prediction queries|Use a singleton query when you want to predict outcomes for a single new case, or multiple new cases. You provide the input values directly in the query, and the query is executed as a single session.|  
|Batch predictions|Use batch predictions when you have external data that you want to feed into the model, to use as the basis for predictions. To make predictions for an entire set of data, you map the data in the external source to the columns in the model, and then specify the type of predictive data you want to output.<br /><br /> The query for the entire dataset is executed in a single session, making this option much more efficient than sending multiple repeated queries.|  
|Time Series predictions|Use a time series query when you want to predict a value over some number of future steps. SQL Server Data Mining also provides the following functionality in time series queries:<br /><br /> You can extend an existing model by adding new data as part of the query, and make predictions based on the composite series.<br /><br /> You can apply an existing model to a new data series by using the REPLACE_MODEL_CASES option.<br /><br /> You can perform cross-prediction.|  
  
 The following sections describe the general syntax of prediction queries, the different types of prediction queries, and how to work with the results of prediction queries.  
  
 [Basic Prediction Query Design](#bkmk_PredQuery)  
  
-   [Adding Prediction Functions](#bkmk_PredFunc)  
  
-   [Singleton Queries](#bkmk_SingletonQuery)  
  
-   [Batch Prediction Queries](#bkmk_BatchQuery)  
  
 [Working with the Results of Queries](#bkmk_WorkResults)  
  
##  <a name="bkmk_PredQuery"></a> Basic Prediction Query Design  
 When you create a prediction, you typically provide some piece of new data and ask the model to generate a prediction based on the new data.  
  
-   In a batch prediction query, you map the model to an external source of data by using a *prediction join*.  
  
-   In a singleton prediction query, you type one or more values to use as inputs. You can create multiple predictions using a singleton prediction query. However, if you need to create many predictions, performance is better when you use a batch query.  
  
 Both singleton and batch prediction queries use the PREDICTION JOIN syntax to define the new data. The difference is in how the input side of the prediction join is specified.  
  
-   In a batch prediction query, the data comes from an external data source that is specified by using the OPENQUERY syntax.  
  
-   In a singleton prediction query, the data is supplied inline as part of the query.  
  
 For time series models, input data is not always required; it is possible to make predictions using just the data already in the model. However, if you do specify new input data, you must decide whether you will use the new data to update and extend the model, or to replace the original series of data that was used in the model.  For more information about these options, see [Time Series Model Query Examples](time-series-model-query-examples.md).  
  
###  <a name="bkmk_PredFunc"></a> Adding Prediction Functions  
 In addition to predicting a value, you can customize a prediction query to return various kinds of information that are related to the prediction. For example, if the prediction creates a list of products to recommend to a customer, you might also want to return the probability for each prediction, so that you can rank them and present only the top recommendations to the user.  
  
 To do this, you add *prediction functions* to the query. Each model or query type supports specific functions. For example, clustering models support special prediction functions that provide extra detail about the clusters created by the model, whereas time series models have functions that calculate differences over time. There are also general prediction functions that work with almost all model types. For a list of the prediction functions supported in different types of queries, see this topic the DMX reference:  [General Prediction Functions &#40;DMX&#41;](/sql/dmx/general-prediction-functions-dmx).  
  
###  <a name="bkmk_SingletonQuery"></a> Creating Singleton Prediction Queries  
 A singleton prediction query is useful when you want to create quick predictions in real time. A common scenario might be that you have obtained information from a customer, perhaps by using a form on a Web site, and you want to submit that data as the input to a singleton prediction query. For example, when a customer chooses a product from a list, you could use that selection as the input to a query that predicts the best products to recommend.  
  
 Singleton prediction queries do not require a separate table that contains input. Instead, you provide one or multiple rows of values as input to the model, and the prediction or predictions are returned in real time.  
  
> [!WARNING]  
>  Despite the name, singleton prediction queries do not just make single predictions-you can generate multiple predictions for each set of inputs. You provide multiple input cases by creating a SELECT statement for each input case and combining them with the UNION operator.  
  
 When you create a singleton prediction query, you must provide the new data to the model in the form of a PREDICTION JOIN. This means that even though you are not mapping to an actual table, you must make sure that the new data matches the existing columns in the mining model. If the new data columns and the new data match exactly, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will map the columns for you. This is called a *NATURAL PREDICTION JOIN*. However, if the columns do not match, or if the new data does not contain the same kind and amount of data that is in the model, you must specify which columns in the model map to the new data, or specify the missing values.  
  
###  <a name="bkmk_BatchQuery"></a> Batch Prediction Queries  
 A batch prediction query is useful when you have external data that you want to use in making predictions. For example, you might have built a model that categorizes customers by their online activity and purchasing history. You could apply that model to a list of newly acquired leads, to create projections for sales, or to identify targets for proposed campaigns.  
  
 When you perform a prediction join, you must map the columns the model to the columns in the new data source. Therefore, the data source that you choose for an input must data that is somewhat similar to the data in the model. The new information does not have to match exactly, and can be incomplete. For example, suppose the model was trained using information about income and age, but the customer list you are using for predictions has age but nothing about income. In this scenario, you could still map the new data to the model and create a prediction for each customer. However, if income was an important predictor for the model, the lack of complete information would affect the quality of predictions.  
  
 To get the best results, you should join as many of the matching columns as possible between the new data and the model. However, the query will succeed even if there are no matches. If no columns are joined, the query will return the marginal prediction, which is equivalent to the statement `SELECT <predictable-column> FROM <model>` without a PREDICTION JOIN clause.  
  
 After you have successfully mapped all relevant columns, you run the query, and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] makes predictions for each row in the new data based on patterns in the model. You can save the results back to a new table in the data source view that contains the external data, or you can copy and paste the data is you are using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
> [!WARNING]  
>  If you use the designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the external data source must first be defined as a data source view.  
  
 If you use DMX to create a prediction join, you can specify the external data source by using the OPENQUERY, OPENROWSET, or SHAPE commands. The default data access method in the DMX templates is OPENQUERY. For information about these methods, see [&#60;source data query&#62;](/sql/dmx/source-data-query).  
  
###  <a name="bkmk_TSQuery"></a> Predictions in Time Series Mining Models  
 Time series models are different from other models types; you can either use the model as it is to create predictions, or you can provide new data to the model to update the model and create predictions based on recent trends. If you add new data, you can specify the way the new data should be used.  
  
-   *Extending the model cases* means that you add the new data onto the existing series of data in the time series model. Henceforth, predictions are based on the new, combined series. This option is good when you want to simply add a few data points to an existing model.  
  
     For example, suppose that you have an existing time series model that has been trained on the sales data from the previous year. After you have collected several months of new sales data, you decide to update your sales forecasts for the current year. You can create a prediction join that updates the model by adding new data and extends the model to make new predictions.  
  
-   *Replacing the model cases* means that you keep the trained model, but replace the underlying cases with a new set of case data. This option is useful when you want to keep the trend in the model, but apply it to a different set of data.  
  
     For example, your original model might have been trained on a set of data with very high sales volumes; when you replace the underlying data with a new series (perhaps from a store with lower sales volume), you preserve the trend, but the predictions begin from the values in the replacement series.  
  
 Regardless of which approach you use, the starting point for predictions is always the end of the original series.  
  
 For more information about how to create prediction joins on time series models, see [Time Series Model Query Examples](time-series-model-query-examples.md) or [PredictTimeSeries &#40;DMX&#41;](/sql/dmx/predicttimeseries-dmx).  
  
##  <a name="bkmk_WorkResults"></a> Working with the Results of a Prediction Query  
 Your options for saving the results of a data mining prediction query are different depending on how you create the query.  
  
-   When you build a query using Prediction Query Builder in either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can save the results of a prediction query to an existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source. For more information, see [View and Save the Results of a Prediction Query](view-and-save-the-results-of-a-prediction-query.md).  
  
-   When you create prediction queries using DMX in the Query pane of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can use the query output options to save the results to a file, or to the Query Results pane as text or in a grid. For more information, see [Query and Text Editors &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/query-and-text-editors-sql-server-management-studio.md).  
  
-   When you run a prediction query using the Integration Services components, the tasks provides the ability to write the results to a database by using an available ADO.NET connection manager or OLEDB connection manager. For more information, see [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md).  
  
 It is important to understand that the results of a prediction query are not like the results of a query on a relational database, which always returns a single row of related values. Each DMX prediction function that you add to a query returns its own rowset. Therefore, when you make a prediction on a single case, the result might be a predicted value together with several columns of nested tables containing additional detail.  
  
 If you combine multiple functions in one query, the return results are combined as a hierarchical rowset. For example, say you use a time series model to predict future values for sales amount and sales quantity, using a query such as this DMX statement:  
  
```  
SELECT  
  PredictTimeSeries([Forecasting].[Amount]) as [PredictedAmount]  
, PredictTimeSeries([Forecasting].[Quantity]) as [PredictedQty]  
FROM  
  [Forecasting]  
  
```  
  
 The results of this query are two columns, one for each predicted series, where each row contains a nested table with the predicted values:  
  
 **PredictedAmount**  
  
|$TIME|Amount|  
|-----------|------------|  
|201101|172067.11|  
  
|$TIME|Amount|  
|-----------|------------|  
|201102|363390.68|  
  
 **PredictedQty**  
  
|$TIME|Quantity|  
|-----------|--------------|  
|201101|77|  
  
|$TIME|Quantity|  
|-----------|--------------|  
|201102|260|  
  
 If your provider cannot handle hierarchical rowsets, you can flatten the results by using the FLATTEN keyword in the prediction query. For more information, including examples of flattened rowsets, see [SELECT &#40;DMX&#41;](/sql/dmx/select-dmx).  
  
## See Also  
 [Content Queries &#40;Data Mining&#41;](content-queries-data-mining.md)   
 [Data Definition Queries &#40;Data Mining&#41;](data-definition-queries-data-mining.md)  
  
  
