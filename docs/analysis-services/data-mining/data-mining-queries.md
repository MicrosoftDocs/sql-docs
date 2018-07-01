---
title: "Data Mining Queries | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Queries
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Data mining queries are useful for many purposes. You can:  
  
-   Apply the model to new data, to make single or multiple predictions. You can provide input values as parameters, or in a batch.  
  
-   Get a statistical summary of the data used for training.  
  
-   Extract patterns and rules, or generate a profile of the typical case representing a pattern in the model.  
  
-   Extract regression formulas and other calculations that explain patterns.  
  
-   Get the cases that fit a particular pattern.  
  
-   Retrieve details about individual cases used in the model, including data not used in analysis.  
  
-   Retrain a model by adding new data, or perform cross-prediction.  
  
 This section provides an overview of the information you need to get started with data mining queries. It describes the types of queries you can create against data mining objects, introduces the query tools and query languages, and provides links to examples of queries that you can create against models that were built using the algorithms provided in SQL Server Data Mining.  
  
 [Understanding Data Mining Queries](#bkmk_Understand)  
  
 [Query Tools and Interfaces](#bkmk_Interfaces)  
  
 [Queries for Different Model Types](#bkmk_ModelTypes)  
  
 [Requirements](#bkmk_Reqs)  
  
##  <a name="bkmk_Understand"></a> Understanding Data Mining Queries  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Data Mining supports the following types of queries:  
  
-   [Prediction Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/prediction-queries-data-mining.md)  
  
     Queries that make inferences based on patterns in the model, and from input data.  
  
-   [Content Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/content-queries-data-mining.md)  
  
     Queries that return metadata, statistics, and other information about the model itself.  
  
-   [Drillthrough Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/drillthrough-queries-data-mining.md)  
  
     Queries that can retrieve the underlying case data for the model, or even data from the structure that was not used in the model.  
  
-   [Data Definition Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/data-definition-queries-data-mining.md)  
  
     Queries that do not return information from the model, but rather are used to build models and structures or to update the data in a model or structure.  
  
 Before you create queries, we recommend that you familiarize yourself with the differences between models created with each of the data mining algorithms provided by SQL Server.  
  
-   Browse and explore each model type by using the custom data mining viewers that are provided for each algorithm type. For more information, see [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md).  
  
-   Review the model content for each model type, by using the **Microsoft Generic Content Tree Viewer**. To interpret this information, refer to [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
##  <a name="bkmk_Interfaces"></a> Query Tools and Interfaces  
 You can build data mining queries interactively by using one of the query tools provided by SQL Server. The graphical Prediction Query Builder is provided in both [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If you have not used the Prediction Query Builder before, we recommend that you follow the steps in the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c) to familiarize yourself with the interface. For q quick overview of the steps, see Create a Query using the [Create a Prediction Query Using the Prediction Query Builder](../../analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md).  
  
 The Prediction Query Builder is helpful for starting queries that you will customize later. You can easily add data sources and map them to columns, and then switch to DMX view and customize the query by adding a WHERE clause or other functions.  
  
 Once you are familiar with data mining models and how to build queries, you can also write queries directly by using Data Mining Extensions (DMX). DMX is a query language that is similar to Transact-SQL, and that you can use from many different clients. DMX is the tool of choice for creating both custom predictions and complex queries. For an introduction to DMX, see [Creating and Querying Data Mining Models with DMX: Tutorials &#40;Analysis Services - Data Mining&#41;](http://msdn.microsoft.com/library/145b81a7-c0c3-4ca3-bb32-0b482423b9a0).  
  
 DMX editors are provided in both [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can also use the Prediction Query Builder to start your queries, then change the view to the text editor and copy the DMX statement to another client. For more information, see [Data Mining Query Tools](../../analysis-services/data-mining/data-mining-query-tools.md).  
  
 You can compose DMX statements programmatically and send them from your client to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server by using AMO or XMLA. However, DMX is the language that you must use to create queries against a mining model.  
  
 You can also query the metadata, statistics, and some content of the model by using Dynamic Management Views (DMVs) that are based on the data mining schema rowsets. These DMVs make it easy to retrieve information about the model by typing SELECT statements; however, you cannot create predictions. For more information about DMVs supported by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [Use Dynamic Management Views &#40;DMVs&#41; to Monitor Analysis Services](../../analysis-services/instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md).  
  
 Finally, you can create data mining queries for use in Integration Services packages, by using the [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md), or the [Data Mining Query Transformation](../../integration-services/data-flow/transformations/data-mining-query-transformation.md). The control flow task supports multiple types of DMX queries, whereas the data flow transformation supports only queries that work with data in the data flow, meaning queries that use the PREDICTION JOIN syntax.  
  
##  <a name="bkmk_ModelTypes"></a> Queries for Different Model Types  
 The algorithm that was used when the model was created greatly influences the type of information that you can get from a data mining query. The reason for the differences is that each algorithm processes the data in a different way, and stores different kinds of patterns. For example, some algorithms create clusters; others create trees. Therefore, you might need to use specialized prediction and query functions, depending on the type of model that you are working with.  
  
 The following list provides a summary of the functions that you can use in queries:  
  
-   **General prediction functions:** The **Predict** function is polymorphic, meaning it works with all model types. This function will automatically detect the type of model you are working with and prompt you for additional parameters. For more information, see [Predict &#40;DMX&#41;](../../dmx/predict-dmx.md).  
  
    > [!WARNING]  
    >  Not all models are used to make predictions. For example, you can create a clustering model that does not have a predictable attribute. However, even if a model does not have a predictable attribute, you can create prediction queries that return other types of useful information from the model.  
  
-   **Custom prediction functions:** Each model type provides a set of prediction functions designed for working with the patterns created by that algorithm.  
  
     For example, the **Lag** function is provided for time series models, to let you view the historical data used for the model. For clustering models, functions such as **ClusterDistance** are more meaningful.  
  
     For more information about the functions that are supported for each model type, see the following links:  
  
    |||  
    |-|-|  
    |[Association Model Query Examples](../../analysis-services/data-mining/association-model-query-examples.md)|[Microsoft Naive Bayes Algorithm](../../analysis-services/data-mining/microsoft-naive-bayes-algorithm.md)|  
    |[Clustering Model Query Examples](../../analysis-services/data-mining/clustering-model-query-examples.md)|[Neural Network Model Query Examples](../../analysis-services/data-mining/neural-network-model-query-examples.md)|  
    |[Decision Trees Model Query Examples](../../analysis-services/data-mining/decision-trees-model-query-examples.md)|[Sequence Clustering Model Query Examples](../../analysis-services/data-mining/sequence-clustering-model-query-examples.md)|  
    |[Linear Regression Model Query Examples](../../analysis-services/data-mining/linear-regression-model-query-examples.md)|[Time Series Model Query Examples](../../analysis-services/data-mining/time-series-model-query-examples.md)|  
    |[Logistic Regression Model Query Examples](../../analysis-services/data-mining/logistic-regression-model-query-examples.md)||  
  
     You can also call VBA functions, or create your own functions. For more information, see [Functions &#40;DMX&#41;](../../dmx/functions-dmx.md).  
  
-   **General statistics:** There are a number of functions that can be used with almost any model type, which return a standard set of descriptive statistics, such as standard deviation.  
  
     For example, the **PredictHistogram** function returns a table that lists all the states of the specified column.  
  
     For more information, see [General Prediction Functions &#40;DMX&#41;](../../dmx/general-prediction-functions-dmx.md).  
  
-   **Custom statistics:** Additional supporting functions are provided for each model type, to generate statistics that are relevant to the specific analytical task.  
  
     For example, when you are working with a clustering model, you can use the function, **PredictCaseLikelihood**, to return the likelihood score associated with a certain case and cluster. However, if you created a linear regression model, you would be more interested in retrieving the coefficient and intercept, which you can do using a content query.  
  
-   **Model content functions:** The *content* of all models is represented in a standardized format that lets you retrieve information with a simple query. You create queries on the model content by using DMX. You can also get some type of model content by using the data mining schema rowsets.  
  
     In the model content, the meaning of each row or node of the table that is returned differs depending on the type of algorithm that was used to build the model, as well as the data type of the column. For more information, see [Content Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/content-queries-data-mining.md).  
  
##  <a name="bkmk_Reqs"></a> Requirements  
 Before you can create a query against a model, the data mining model must have been processed. Processing of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects requires special permissions. For more information on processing mining models, see [Processing Requirements and Considerations &#40;Data Mining&#41;](../../analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md).  
  
 To execute queries against a data mining model requires different levels of permissions, depending on the type of query that you run. For example, drillthrough to case or structure data typically requires additional permissions which can be set on the mining structure object or mining model object.  
  
 However, if your query uses external data, and includes statements such as OPENROWSET or OPENQUERY, the database that you are querying must enable these statements, and you must have permission on the underlying database objects.  
  
 For more information on the security contexts required to run data mining queries, see [Security Overview &#40;Data Mining&#41;](../../analysis-services/data-mining/security-overview-data-mining.md)  
  
## In This Section  
 The topics in this section introduce each type of data mining query in more detail, and provide links to detailed examples of how to create queries against  data mingin models.  
  
 [Prediction Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/prediction-queries-data-mining.md)  
  
 [Content Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/content-queries-data-mining.md)  
  
 [Drillthrough Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/drillthrough-queries-data-mining.md)  
  
 [Data Definition Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/data-definition-queries-data-mining.md)  
  
 [Data Mining Query Tools](../../analysis-services/data-mining/data-mining-query-tools.md)  
  
## Related Tasks  
 Use these links to learn how to create and work with data mining queries.  
  
|Tasks|Links|  
|-----------|-----------|  
|View tutorials and walkthroughs on data mining queries|[Lesson 6: Creating and Working with Predictions &#40;Basic Data Mining Tutorial&#41;](http://msdn.microsoft.com/library/b213cb58-2c40-4c89-b08b-d3c36a4afad3)<br /><br /> [Time Series Prediction DMX Tutorial](http://msdn.microsoft.com/library/38ea7c03-4754-4e71-896a-f68cc2c98ce2)|  
|Use data mining query tools in SQL Server Management Studio and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]|[Create a DMX Query in SQL Server Management Studio](../../analysis-services/data-mining/create-a-dmx-query-in-sql-server-management-studio.md)<br /><br /> [Create a Prediction Query Using the Prediction Query Builder](../../analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md)<br /><br /> [Apply Prediction Functions to a Model](../../analysis-services/data-mining/apply-prediction-functions-to-a-model.md)<br /><br /> [Manually Edit a Prediction Query](../../analysis-services/data-mining/manually-edit-a-prediction-query.md)|  
|Work with external data used in prediction queries|[Choose and Map Input Data for a Prediction Query](../../analysis-services/data-mining/choose-and-map-input-data-for-a-prediction-query.md)<br /><br /> [Choose and Map Input Data for a Prediction Query](../../analysis-services/data-mining/choose-and-map-input-data-for-a-prediction-query.md)|  
|Work with the results of queries|[View and Save the Results of a Prediction Query](../../analysis-services/data-mining/view-and-save-the-results-of-a-prediction-query.md)|  
|Use DMX and XMLA query templates provided in Management Studio|[Create a Singleton Prediction Query from a Template](../../analysis-services/data-mining/create-a-singleton-prediction-query-from-a-template.md)<br /><br /> [Create a Data Mining Query by Using XMLA](../../analysis-services/data-mining/create-a-data-mining-query-by-using-xmla.md)<br /><br /> [Use Analysis Services Templates in SQL Server Management Studio](../../analysis-services/instances/use-analysis-services-templates-in-sql-server-management-studio.md)|  
|Learn more about content queries and see examples|[Create a Content Query on a Mining Model](../../analysis-services/data-mining/create-a-content-query-on-a-mining-model.md)<br /><br /> [Query the Parameters Used to Create a Mining Model](../../analysis-services/data-mining/query-the-parameters-used-to-create-a-mining-model.md)<br /><br /> [Content Queries &#40;Data Mining&#41;](../../analysis-services/data-mining/content-queries-data-mining.md)|  
|Set query options and troubleshoot query permissions and problems|[Change the Time-out Value for Data Mining Queries](../../analysis-services/data-mining/change-the-time-out-value-for-data-mining-queries.md)|  
|Use the data mining components in Integration Services|[Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md)<br /><br /> [Data Mining Query Transformation](../../integration-services/data-flow/transformations/data-mining-query-transformation.md)|  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md)  
  
  
