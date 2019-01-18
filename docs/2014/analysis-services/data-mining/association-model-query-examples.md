---
title: "Association Model Query Examples | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "itemsets [Analysis Services]"
  - "association algorithms [Analysis Services]"
  - "rules [Data Mining]"
  - "association rules"
  - "content queries [DMX]"
ms.assetid: 68b39f5c-c439-44ac-8046-6f2d36649059
author: minewiskan
ms.author: owend
manager: craigg
---
# Association Model Query Examples
  When you create a query against a data mining model, you can create either a content query, which provides details about the rules and itemsets discovered during analysis, or you can create a prediction query, which uses the associations discovered in the data to make predictions. For an association model, predictions typically are based on rules, and can be used to make recommendations, whereas queries on content typically explore the relationship among itemsets. You can also retrieve metadata about the model.  
  
 This section explains how to create these kinds of queries for models that are based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm.  
  
 **Content Queries**  
  
 [Getting model metadata data by using DMX](#bkmk_Query1)  
  
 [Getting metadata from the schema rowset](#bkmk_Query2)  
  
 [Retrieving the original parameters for the model](#bkmk_Query3)  
  
 [Retrieving a list of itemsets and products](#bkmk_Query4)  
  
 [Returning the top 10 itemsets](#bkmk_Query5)  
  
 **Prediction Queries**  
  
 [Predicting associated items](#bkmk_Query6)  
  
 [Determining confidence for related itemsets](#bkmk_Query7)  
  
##  <a name="bkmk_top2"></a> Finding Information about the Model  
 All mining models expose the content learned by the algorithm according to a standardized schema, which is named the mining model schema rowset. You can create queries against the mining model schema rowset either by using Data Mining Extensions (DMX) statements, or by using [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] stored procedures. In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can also query the schema rowsets directly as system tables, by using a SQL-like syntax.  
  
###  <a name="bkmk_Query1"></a> Sample Query 1: Getting Model Metadata by Using DMX  
 The following query returns basic metadata about the association model, `Association`, such as the name of the model, the database where the model is stored, and the number of child nodes in the model. This query uses a DMX content query to retrieve the metadata from the parent node of the model:  
  
```  
SELECT MODEL_CATALOG, MODEL_NAME, NODE_CAPTION,   
NODE_SUPPORT, [CHILDREN_CARDINALITY], NODE_DESCRIPTION  
FROM Association.CONTENT  
WHERE NODE_TYPE = 1  
```  
  
> [!NOTE]  
>  You must enclose the name of the column, CHILDREN_CARDINALITY, in brackets to distinguish it from the MDX reserved keyword of the same name.  
  
 Example results:  
  
|||  
|-|-|  
|MODEL_CATALOG|Association Test|  
|MODEL_NAME|Association|  
|NODE_CAPTION|Association Rules Model|  
|NODE_SUPPORT|14879|  
|CHILDREN_CARDINALITY|942|  
|NODE_DESCRIPTION|Association Rules Model; ITEMSET_COUNT=679; RULE_COUNT=263; MIN_SUPPORT=14; MAX_SUPPORT=4334; MIN_ITEMSET_SIZE=0; MAX_ITEMSET_SIZE=3; MIN_PROBABILITY=0.400390625; MAX_PROBABILITY=1; MIN_LIFT=0.14309369632511; MAX_LIFT=1.95758227647523|  
  
 For a definition of what these columns mean in an association model, see [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-association-models-analysis-services-data-mining.md).  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query2"></a> Sample Query 2: Getting Additional Metadata from the Schema Rowset  
 By querying the data mining schema rowset, you can find the same information that is returned in a DMX content query. However, the schema rowset provides some additional columns, such as the date the model was last processed, the mining structure, and the name of the column used as the predictable attribute.  
  
```  
SELECT MODEL_CATALOG, MODEL_NAME, SERVICE_NAME, PREDICTION_ENTITY,   
MINING_STRUCTURE, LAST_PROCESSED  
FROM $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'Association'  
```  
  
 Example results:  
  
|||  
|-|-|  
|MODEL_CATALOG|Adventure Works DW Multidimensional 2012|  
|MODEL_NAME|Association|  
|SERVICE_NAME|Association Rules Model|  
|PREDICTION_ENTITY|v Assoc Seq Line Items|  
|MINING_STRUCTURE|Association|  
|LAST_PROCESSED|9/29/2007 10:21:24 PM|  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query3"></a> Sample Query 3: Retrieving Original Parameters for Model  
 The following query returns a single column that contains details about the parameter settings that were used when the model was created.  
  
```  
SELECT MINING_PARAMETERS   
from $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'Association'  
```  
  
 Example results:  
  
 MAXIMUM_ITEMSET_COUNT=200000,MAXIMUM_ITEMSET_SIZE=3,MAXIMUM_SUPPORT=1,MINIMUM_SUPPORT=9.40923449156529E-04,MINIMUM_IMPORTANCE=-999999999,MINIMUM_ITEMSET_SIZE=0,MINIMUM_PROBABILITY=0.4  
  
 [Return to Top](#bkmk_top2)  
  
## Finding Information about Rules and Itemsets  
 There are two common uses of an association model: to discover information about frequent itemsets, and to extract details about particular rules and itemsets. For example, you might want to extract a list of rules that were scored as being especially interesting, or create a list of the most common itemsets. You retrieve such information by using a DMX content query. You can also browse this information by using the **Microsoft Association Viewer**.  
  
###  <a name="bkmk_Query4"></a> Sample Query 4: Retrieving List of Itemsets and Products  
 The following query retrieves all of the itemsets, together with a nested table that lists the products included in each itemset. The NODE_NAME column contains the unique ID of the itemset within the model, whereas the NODE_CAPTION provides a text description of the items. In this example, the nested table is flattened, so that an itemset that contains two products generates two rows in the results. You can omit the FLATTENED keyword if your client supports hierarchical data.  
  
```  
SELECT FLATTENED NODE_NAME, NODE_CAPTION,  
NODE_PROBABILITY, NODE_SUPPORT,  
(SELECT ATTRIBUTE_NAME FROM NODE_DISTRIBUTION) as PurchasedProducts  
FROM Association.CONTENT  
WHERE NODE_TYPE = 7  
```  
  
 Example results:  
  
|||  
|-|-|  
|NODE_NAME|37|  
|NODE_CAPTION|Sport-100 = Existing|  
|NODE_PROBABILITY|0.291283016331743|  
|NODE_SUPPORT|4334|  
|PURCHASEDPRODUCTS.ATTRIBUTE_NAME|v Assoc Seq Line Items(Sport-100)|  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query5"></a> Sample Query 5: Returning Top 10 Itemsets  
 This example demonstrates how to use some of the grouping and ordering functions that DMX provides by default. The query returns the top 10 itemsets when ordered by the support for each node. Note that you do not need to explicitly group the results, as you would in Transact-SQL; however, you can use only one aggregate function in each query.  
  
```  
SELECT TOP 10 (NODE_SUPPORT),NODE_NAME, NODE_CAPTION  
FROM Association.CONTENT  
WHERE NODE_TYPE = 7  
```  
  
 Example results:  
  
|||  
|-|-|  
|NODE_SUPPORT|4334|  
|NODE_NAME|37|  
|NODE_CAPTION|Sport-100 = Existing|  
  
 [Return to Top](#bkmk_top2)  
  
## Making Predictions using the Model  
 An association rules model is often used to generate recommendations, which are based on correlations discovered in the itemsets. Therefore, when you create a prediction query based on an association rules model, you are typically using the rules in the model to make guesses based on new data.  [PredictAssociation &#40;DMX&#41;](/sql/dmx/predictassociation-dmx) is the function that returns recommendations, and has several arguments that you can use to customize the query results.  
  
 Another example of where queries on an association model might be useful is to return the confidence for various rules and itemsets so that you can compare the effectiveness of different cross-sell strategies. The following examples illustrate how to create such queries.  
  
###  <a name="bkmk_Query6"></a> Sample Query 6: Predicting Associated Items  
 This example uses the Association model created in the [Intermediate Data Mining Tutorial &#40;Analysis Services - Data Mining&#41;](../../tutorials/intermediate-data-mining-tutorial-analysis-services-data-mining.md). It demonstrates how to create a prediction query that tells you what products to recommend to a customer who has purchased a particular product. This type of query, where you provide values to the model in a `SELECT...UNION` statement, is called a singleton query. Because the predictable model column that corresponds to the new values is a nested table, you must use one `SELECT` clause to map the new value to the nested table column, `[Model]`, and another `SELECT` clause to map the nested table column to the case-level column, `[v Assoc Seq Line Items]`. Adding the keyword INCLUDE-STATISTICS to the query lets you see the probability and support for the recommendations.  
  
```  
SELECT PredictAssociation([Association].[vAssocSeqLineItems],INCLUDE_STATISTICS, 3)  
FROM [Association]  
NATURAL PREDICTION JOIN   
(SELECT  
(SELECT 'Classic Vest' as [Model])  
AS [v Assoc Seq Line Items])  
AS t  
```  
  
 Example results:  
  
|Model|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|  
|-----------|--------------|------------------|--------------------------|  
|Sport-100|4334|0.291283|0.252696|  
|Water Bottle|2866|0.19262|0.175205|  
|Patch kit|2113|0.142012|0.132389|  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query7"></a> Sample Query 7: Determining Confidence for Related Itemsets  
 Whereas rules are useful for generating recommendations, itemsets are more interesting for deeper analysis of the patterns in the data set. For example, if you were not satisfied with the recommendation that are returned by the previous sample query, you could examine other itemsets that contain Product A, to can get a better idea of whether Product A is an accessory that people tend to buy with all kinds of products, or whether A is strongly correlated with purchases of particular products. The easiest way to explore these relationships is by filtering the itemsets in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Viewer; however, you can retrieve the same information with a query.  
  
 The following sample query returns all itemsets that include the Water Bottle item, including the single item Water bottle.  
  
```  
SELECT TOP 100 FROM   
(  
SELECT FLATTENED NODE_CAPTION, NODE_SUPPORT,   
(SELECT ATTRIBUTE_NAME from NODE_DISTRIBUTION  
WHERE ATTRIBUTE_NAME = 'v Assoc Seq Line Items(Water Bottle)') as D  
FROM Association.CONTENT  
WHERE NODE_TYPE = 7  
) AS Items  
WHERE [D.ATTRIBUTE_NAME] <> NULL  
ORDER BY NODE_SUPPORT DESC  
```  
  
 Example results:  
  
|NODE_CAPTION|NODE_SUPPORT|D.ATTRIBUTE_NAME|  
|-------------------|-------------------|-----------------------|  
|Water Bottle = Existing|2866|v Assoc Seq Line Items(Water Bottle)|  
|Mountain Bottle Cage = Existing, Water Bottle = Existing|1136|v Assoc Seq Line Items(Water Bottle)|  
|Road Bottle Cage = Existing, Water Bottle = Existing|1068|v Assoc Seq Line Items(Water Bottle)|  
|Water Bottle = Existing, Sport-100 = Existing|734|v Assoc Seq Line Items(Water Bottle)|  
  
 This query returns both the rows from the nested table that match the criteria, and all the rows from the outside or case table. Therefore, you must add a condition that eliminates the case table rows that have a null value for the target attribute name.  
  
 [Return to Top](#bkmk_top2)  
  
## Function List  
 All [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms support a common set of functions. However, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm supports the additional functions listed in the following table.  
  
|||  
|-|-|  
|Prediction Function|Usage|  
|[IsDescendant &#40;DMX&#41;](/sql/dmx/isdescendant-dmx)|Determines whether one node is a child of another node in the neural network graph.|  
|[IsInNode &#40;DMX&#41;](/sql/dmx/isinnode-dmx)|Indicates whether the specified node contains the current case.|  
|[PredictAdjustedProbability &#40;DMX&#41;](/sql/dmx/predictadjustedprobability-dmx)|Returns the weighted probability.|  
|[PredictAssociation &#40;DMX&#41;](/sql/dmx/predictassociation-dmx)|Predicts membership in an associative dataset.|  
|[PredictHistogram &#40;DMX&#41;](/sql/dmx/predicthistogram-dmx)|Returns a table of values related to the current predicted value.|  
|[PredictNodeId &#40;DMX&#41;](/sql/dmx/predictnodeid-dmx)|Returns the Node_ID for each case.|  
|[PredictProbability &#40;DMX&#41;](/sql/dmx/predictprobability-dmx)|Returns probability for the predicted value.|  
|[PredictSupport &#40;DMX&#41;](/sql/dmx/predictsupport-dmx)|Returns the support value for a specified state.|  
|[PredictVariance &#40;DMX&#41;](/sql/dmx/predictvariance-dmx)|Returns variance for the predicted value.|  
  
## See Also  
 [Microsoft Association Algorithm](microsoft-association-algorithm.md)   
 [Microsoft Association Algorithm Technical Reference](microsoft-association-algorithm-technical-reference.md)   
 [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-association-models-analysis-services-data-mining.md)  
  
  
