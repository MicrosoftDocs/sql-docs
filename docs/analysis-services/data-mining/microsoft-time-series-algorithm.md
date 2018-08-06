---
title: "Microsoft Time Series Algorithm | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Microsoft Time Series Algorithm
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm provides multiple algorithms that are optimized for forecasting  continuous values, such as product sales, over time. Whereas other [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms, such as decision trees, require additional columns of new information as input to predict a trend, a time series model does not. A time series model can predict trends based only on the original dataset that is used to create the model. You can also add new data to the model when you make a prediction and automatically incorporate the new data in the trend analysis.  
  
 The following diagram shows a typical model for forecasting sales of a product in four different sales regions over time. The model that is shown in the diagram shows sales for each region plotted as red, yellow, purple, and blue lines. The line for each region has two parts:  
  
-   Historical information appears to the left of the vertical line and represents the data that the algorithm uses to create the model.  
  
-   Predicted information appears to the right of the vertical line and represents the forecast that the model makes.  
  
 The combination of the source data and the prediction data is called a *series*.  
  
 ![An example of a time series](../../analysis-services/data-mining/media/time-series.gif "An example of a time series")  
  
 An important feature of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm is that it can perform cross prediction. If you train the algorithm with two separate, but related, series, you can use the resulting model to predict the outcome of one series based on the behavior of the other series. For example, the observed sales of one product can influence the forecasted sales of another product.  Cross prediction is also useful for creating a general model that can be applied to multiple series. For example, the predictions for a particular region are unstable because the series lacks good quality data.  You could train a general model on an average of all four regions, and then apply the model to the individual series to create more stable predictions for each region.  
  
## Example  
 The management team at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] wants to predict monthly bicycle sales for the coming year. The company is especially interested in whether the sale of one bike model can be used to predict the sale of another model. By using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm on historical data from the past three years, the company can produce a data mining model that forecasts future bike sales. Additionally, the company can perform cross predictions to see whether the sales trends of individual bike models are related.  
  
 Each quarter, the company plans to update the model with recent sales data and update their predictions to model recent trends. To correct for stores that do not accurately or consistently update sales data, they will create a general prediction model, and use that to create predictions for all regions.  
  
## How the Algorithm Works  
 In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm used a single auto-regressive time series method, named ARTXP. The ARTXP algorithm was optimized for short-term predictions, and therefore, excelled at predicting the next likely value in a series. Beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm added a second algorithm, ARIMA, which was optimized for long-term prediction. For a detailed explanation about the implementation of the ARTXP and ARIMA algorithms, see [Microsoft Time Series Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md).  
  
 By default, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm uses a mix of the algorithms when it analyzes patterns and making predictions. The algorithm trains two separate models on the same data: one model uses the ARTXP algorithm, and one model uses the ARIMA algorithm. The algorithm then blends the results of the two models to yield the best prediction over a variable number of time slices. Because ARTXP is best for short-term predictions, it is weighted more heavily at the beginning of a series of predictions. However, as the time slices that you are predicting move further into the future, ARIMA is weighted more heavily.  
  
 You can also control the mix of algorithms to favor either short- or long-term prediction in the times series. Beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Standard, you can specify that  which algorithm to use:  
  
-   Use only ARTXP for short-term prediction.  
  
-   Use only ARIMA for long-term prediction.  
  
-   Use the default blending of the two algorithms.  
  
 Beginning in [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)], you can also customize how the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm blends the models for prediction. When you use a mixed model, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm blends the two algorithms in the following way:  
  
-   Only ARTXP is always used for making the first couple of predictions.  
  
-   After the first couple of predictions, a combination of ARIMA and ARTXP is used.  
  
-   As the number of prediction steps increases, predictions rely more heavily on ARIMA until ARTXP is no longer used.  
  
-   You control the mixing point, the rate at which the weight of ARTXP is decreased, and the weight of ARIMA is increased by setting the PREDICTION_SMOOTHING parameter.  
  
 Both algorithms can detect seasonality in data at multiple levels. For example, your data might contain monthly cycles nested within yearly cycles. To detect these seasonal cycles, you can either provide a periodicity hint or specify that the algorithm should automatically detect periodicity.  
  
 In addition to periodicity, there are several other parameters that control the behavior of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm when it detects periodicity, makes predictions, or analyzes cases. For information about how to set algorithm parameters, see [Microsoft Time Series Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md).  
  
## Data Required for Time Series Models  
 When you prepare data for use in training any data mining model, make sure that you understand the requirements for the particular model and how the data is used.  
  
 Each forecasting model must contain a case series, which is the column that specifies the time slices or other series over which change occurs. For example, the data in the previous diagram shows the series for historical and forecasted bicycle sales over a period of several months. For this model, each region is a series, and the date column contains the time series, which is also the case series. In other models, the case series can be a text field or some identifier such as a customer ID or transaction ID. However, a time series model must always use a date, time, or some other unique numeric value for its case series.  
  
 The requirements for a time series model are as follows:  
  
-   **A single key time column** Each model must contain one numeric or date column that is used as the case series, which defines the time slices that the model will use. The data type for the key time column can be either a datetime data type or a numeric data type. However, the column must contain continuous values, and the values must be unique for each series. The case series for a time series model cannot be stored in two columns, such as a Year column and a Month column.  
  
-   **A predictable column** Each model must contain at least one predictable column around which the algorithm will build the time series model. The data type of the predictable column must have continuous values. For example, you can predict how numeric attributes, such as income, sales, or temperature, change over time. However, you cannot use a column that contains discrete values, such as purchasing status or level of education, as the predictable column.  
  
-   **An optional series key column** Each model can have an additional key column that contains unique values that identify a series. The optional series key column must contain unique values. For example, a single model can contain sales for many product models, as long as there is only one record for each product name for every time slice.  
  
 You can define input data for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series model in several different ways. However, because the format of the input cases affects the definition of the mining model, you must consider your business needs and prepare your data accordingly. The following two examples illustrate how the input data affects the model. In both examples, the completed mining model contains patterns for four distinct series:  
  
-   Sales for Product A  
  
-   Sales for Product B  
  
-   Volume for Product A  
  
-   Volume for Product B  
  
 In both examples, you can predict new future sales and volume for each product. You cannot predict new values for product or for time.  
  
### Example 1: Time Series Data Set with Series Represented as Column Values  
 This example uses the following table of input cases:  
  
|TimeID|Product|Sales|Volume|  
|------------|-------------|-----------|------------|  
|1/2001|A|1000|600|  
|2/2001|A|1100|500|  
|1/2001|B|500|900|  
|2/2001|B|300|890|  
  
 The TimeID column in the table contains a time identifier, and has two entries for each day. The TimeID column becomes the case series. Therefore, you would designate this column as the key time column for the time series model.  
  
 The Product column defines a product in the database. This column contains the product series. Therefore, you would designate this column as a second key for the time series model.  
  
 The Sales column describes the gross profits of the specified product for one day, and the Volume column describes the quantity of the specified product that remains in the warehouse. These two columns contain the data that is used to train the model. Both Sales and Volume  can be predictable attributes for each series in the Product column.  
  
### Example 2: Time Series Data Set with Each Series in Separate Column  
 Although this example uses basically the same input data as the first example, the input data is structured differently, as shown in the following table:  
  
|TimeID|A_Sales|A_Volume|B_Sales|B_Volume|  
|------------|--------------|---------------|--------------|---------------|  
|1/2001|1000|600|500|900|  
|2/2001|1100|500|300|890|  
  
 In this table, the TimeID column still contains the case series for the time series model, which you designate as the key time column. However, the previous Sales and Volume columns are now split into two columns and each of those columns are preceded by the product name. As a result, only a single entry exists for each day in the TimeID column. This creates a time series model that would contain four predictable columns: A_Sales, A_Volume, B_Sales, and B_Volume.  
  
 Furthermore, because you have separated the products into different columns, you do not have to specify an additional series key column. All the columns in the model are either a case series column or a predictable column.  
  
## Viewing a Time Series Model  
 After the model has been trained, the results are stored as a set of patterns, which you can explore or use to make predictions.  
  
 To explore the model, you can use the [Time Series Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-time-series-viewer.md). The viewer includes a chart that displays future predictions, and a tree view of the periodic structures in the data.  
  
 If you want to know more about how the predictions are calculated, you can browse the model in the [Microsoft Generic Content Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md). The content stored for the model includes details such as the periodic structures detected by the ARIMA and ARTXP algorithms, the equation used to blend the algorithms, and other statistics.  
  
## Creating Time Series Predictions  
 By default, when you view a time series model, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] shows you five predictions for the series. However, you can create queries to return a variable number of predictions, and you can extra columns to the predictions to return descriptive statistics. For information about how to create queries against a time series model, see [Time Series Model Query Examples](../../analysis-services/data-mining/time-series-model-query-examples.md). For examples of how to use Data Mining Extensions (DMX) to make time series predictions, see [PredictTimeSeries &#40;DMX&#41;](../../dmx/predicttimeseries-dmx.md).  
  
 When using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm to make predictions, you should consider the following additional restrictions and requirements:  
  
-   Cross-prediction is only available when you use a mixed model, or when you use a model based solely on the ARTXP algorithm. If you use a model based only on the ARIMA algorithm, cross-prediction is not possible.  
  
-   A time series model can make predictions that differ, sometimes significantly, depending on the 64-bit operating system that the server uses. These differences occur due to the way that an [!INCLUDE[vcpritanium](../../includes/vcpritanium-md.md)]-based system represents and handles numbers for floating-point arithmetic, which differs from the way that an [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]-based system does these calculations. Because prediction results can be specific to the operating system, we recommend that you evaluate models on the same operating system that you will use in production.  
  
## Remarks  
  
-   Does not support using the Predictive Model Markup Language (PMML) to create mining models.  
  
-   Supports the use of OLAP mining models.  
  
-   Does not support the creation of data mining dimensions.  
  
-   Supports drillthrough.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Browse a Model Using the Microsoft Time Series Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-time-series-viewer.md)   
 [Microsoft Time Series Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)   
 [Time Series Model Query Examples](../../analysis-services/data-mining/time-series-model-query-examples.md)   
 [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-time-series-models-analysis-services-data-mining.md)  
  
  
