---
title: "Content Queries (Data Mining) | Microsoft Docs"
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
# Content Queries (Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A content query is a way of extracting information about the internal statistics and structure of the mining model. Sometimes a content query can provide details that are not readily available in the viewer. You can also use the results of a content query to extract information programmatically for other uses.  
  
 This section provides general information about the types of information that you can retrieve by using a content query, and the general DMX syntax for content queries.  
  
 [Basic Content Queries](#bkmk_ContentQuery)  
  
-   [Queries on Structure and Case Data](#bkmk_Structure)  
  
-   [Queries on Model Patterns](#bkmk_Patterns)  
  
 [Examples](#bkmk_Examples)  
  
-   [Content Query on an Association Model](#bkmk_Assoc)  
  
-   [Content Query on a Decision Trees Model](#bkmk_DecTree)  
  
 [Working with the Query Results](#bkmk_Results)  
  
##  <a name="bkmk_ContentQuery"></a> Basic Content Queries  
 You can create content queries by using the Prediction Query Builder, use the DMX content query templates that are provided in SQL Server Management Studio, or write queries directly in DMX. Unlike prediction queries you do not need to join external data, so content queries are easy to write.  
  
 This section provides an overview of the types of content queries you can create.  
  
-   Queries on the mining structure or case data let you view the detailed data that was used for training.  
  
-   Queries on the model can return patterns, lists of attributes, formulas, and so forth.  
  
###  <a name="bkmk_Structure"></a> Queries on Structure and Case Data  
 DMX supports queries on the cached data that is used to build mining structures and models. By default, this cache is created when you define the mining structure, and is populated when you process the structure or model.  
  
> [!WARNING]  
>  This cache cannot be cleared or deleted if you need to separate data into training and testing sets. If the cache is cleared, you cannot query the case data.  
  
 The following examples show the common patterns for creating queries on the case data, or queries on the data in the mining structure:  
  
 **Get all the cases for a model**  
 `SELECT FROM <model>.CASES`  
  
 Use this statement to retrieve specified columns from the case data used to build a model. You must have drillthrough permissions on the model to run this query.  
  
 **View all the data that is included in the structure**  
 `SELECT FROM <structure>.CASES`  
  
 Use this statement to view all the data that is included in the structure, including columns that are not included in a particular mining model. You must have drillthrough permissions on the model, as well as on the structure, to retrieve data from the mining structure.  
  
 **Get range of values**  
 `SELECT DISTINCT RangeMin(<column>), RangeMax(<column>) FROM <model>`  
  
 Use this statement to find the minimum value, maximum value, and mean of a continuous column, or of the buckets of a DISCRETIZED column.  
  
 **Get distinct values**  
 `SELECT DISTINCT <column>FROM <model>`  
  
 Use this statement to retrieve all the values of a DISCRETE column.  Do not use this statement for DISCRETIZED columns; use the **RangeMin** and **RangeMax** functions instead.  
  
 **Find the cases that were used to train a model or structure**  
 `SELECT  FROM <mining structure.CASES WHERE IsTrainingCase()`  
  
 Use this statement to get the complete set of data that was used in a training a model.  
  
 **Find the cases that are used for testing a model or structure**  
 `SELECT  FROM <mining structure.CASES WHERE IsTestingCase()`  
  
 Use this statement to get the data that has been set aside for testing mining models related to a specific structure.  
  
 **Drillthrough from a specific model pattern to underlying case data**  
 `SELECT FROM <model>.CASESWHERE IsTrainingCase() AND IsInNode(<node>)`  
  
 Use this statement to retrieve detailed case data from a trained model. You must specify a specific node: for example, you must know the node ID of the cluster, the specific branch of the decision tree, etc. Moreover, you must have drillthrough permissions on the model to run this query.  
  
###  <a name="bkmk_Patterns"></a> Queries on Model Patterns, Statistics, and Attributes  
 The content of a data mining model is useful for many purposes. With a model content query, you can:  
  
-   Extract formulas or probabilities for making your own calculations.  
  
-   For an association model, retrieve the rules that are used to generate a prediction.  
  
-   Retrieve the descriptions of specific rules so that you can use the rules in a custom application.  
  
-   View the moving averages detected by a time series model.  
  
-   Obtain the regression formula for some segment of the trend line.  
  
-   Retrieve actionable information about customers identified as being part of a specific cluster.  
  
 The following examples show some of the common patterns for creating queries on the model content:  
  
 **Get patterns from the model**  
 `SELECT FROM <model>.CONTENT`  
  
 Use this statement to retrieve detailed information about specific nodes in the model. Depending on the algorithm type, the node can contain rules and formulas, support and variance statistics, and so forth.  
  
 **Retrieve attributes used in a trained model**  
 `CALL System.GetModelAttributes(<model>)`  
  
 Use this stored procedure to retrieve the list of attributes that were used by a model. This information is useful for determining attributes that were eliminated as a result of feature selection, for example.  
  
 **Retrieve content stored in a data mining dimension**  
 `SELECT FROM <model>.DIMENSIONCONTENT`  
  
 Use this statement to retrieve the data from a data mining dimension.  
  
 This query type is principally for internal use. However, not all algorithms support this functionality. Support is indicated by a flag in the MINING_SERVICES schema rowset.  
  
 If you develop your own plug-in algorithm, you might use this statement to verify the content of your model for testing.  
  
 **Get the PMML representation of a model**  
 `SELECT * FROM <model>.PMML`  
  
 Gets an XML document that represents the model in PMML format. Not all model types are supported.  
  
##  <a name="bkmk_Examples"></a> Examples  
 Although some model content is standard across algorithms, some parts of the content vary greatly depending on the algorithm that you used to build the model. Therefore, when you create a content query, you must understand what information in the model is most useful to your specific model.  
  
 A few examples are provided in this section to illustrate how the choice of algorithm affects the kind of information that is stored in the model. For more information about mining model content, and the content that is specific to each model type, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
###  <a name="bkmk_Assoc"></a> Example 1: Content Query on an Association Model  
 The statement, `SELECT FROM <model>.CONTENT`, returns different kinds of information, depending on the type of model you are querying. For an association model, a key piece of information is the *node type*. Nodes are like containers for information in the model content. In an association model, nodes that represent rules have a NODE_TYPE value of 8, whereas nodes that represent itemsets have a NODE_TYPE value of 7.  
  
 Thus, the following query returns the top 10 itemsets, ranked by support (the default ordering).  
  
```  
SELECT TOP 10 NODE_DESCRIPTION, NODE_PROBABILITY, SUPPORT  
FROM <model>.CONTENT WHERE NODE_TYPE = 7  
```  
  
 The following query builds on this information. The query returns three columns: the ID of the node, the complete rule, and the product on the right-hand side of the itemset-that is, the product that is predicted to be associated with some other products as part of an itemset.  
  
```  
SELECT FLATTENED NODE_UNIQUE_NAME, NODE_DESCRIPTION,  
     (SELECT RIGHT(ATTRIBUTE_NAME, (LEN(ATTRIBUTE_NAME)-LEN('Association model name')))   
FROM NODE_DISTRIBUTION  
WHERE LEN(ATTRIBUTE_NAME)>2  
)   
AS RightSideProduct  
FROM [<Association model name>].CONTENT  
WHERE NODE_TYPE = 8   
ORDER BY NODE_SUPPORT DESC  
```  
  
 The FLATTENED keyword indicates that the nested rowset should be converted into a flattened table. The attribute that represents the product on the right-hand side of the rule is contained in the NODE_DISTRIBUTION table; therefore, we retrieve only the row that contains an attribute name, by adding a requirement that the length be greater than 2.  
  
 A simple string function is used to remove the name of the model from the third column. (Usually the model name is prefixed to the values of nested columns.)  
  
 The WHERE clause specifies that the value of NODE_TYPE should be 8, to retrieve only rules.  
  
 For more examples, see [Association Model Query Examples](../../analysis-services/data-mining/association-model-query-examples.md).  
  
###  <a name="bkmk_DecTree"></a> Example 2: Content Query on a Decision Trees Model  
 A decision tree model can be used for prediction as well as for classification.  This example assumes that you are using the model to predict an outcome, but you also want to find out which factors or rules can be used to classify the outcome.  
  
 In a decision tree model, nodes are used to represent both trees and leaf nodes. The caption for each node contains the description of the path to the outcome. Therefore, to trace the path for any particular outcome, you need to identify the node that contains it, and get the details for that node.  
  
 In your prediction query, you add the prediction function [PredictNodeId &#40;DMX&#41;](../../dmx/predictnodeid-dmx.md), to get the ID of the related node, as shown in the following example:  
  
```  
SELECT  Predict([Bike Buyer]), PredictNodeID([Bike Buyer])   
FROM [<decision tree model name>]  
PREDICTION JOIN   
<input rowset>   
```  
  
 Once you have the ID of the node that contains the outcome, you can retrieve the rule or path that explains the prediction by creating a content query that includes the NODE_CAPTION, as follows:  
  
```  
SELECT NODE_CAPTION  
FROM [<decision tree model name>]   
WHERE NODE_UNIQUE_NAME= '<node id>'  
```  
  
 For more examples, see [Decision Trees Model Query Examples](../../analysis-services/data-mining/decision-trees-model-query-examples.md).  
  
##  <a name="bkmk_Results"></a> Working with the Query Results  
 As the examples demonstrate, content queries mostly return tabular rowsets, but can also include information from nested columns. You can flatten the rowset that is returned, but this can make working with results more complex. The content of the NODE_DISTRIBUTION node in particular is nested, but contains much interesting information about the model.  
  
 For more information about how to work with hierarchical rowsets, see the OLEDB specification on MSDN.  
  
## See Also  
 [Understanding the DMX Select Statement](../../dmx/understanding-the-dmx-select-statement.md)   
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)  
  
  
