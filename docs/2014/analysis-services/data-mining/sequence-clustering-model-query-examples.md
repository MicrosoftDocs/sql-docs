---
title: "Sequence Clustering Model Query Examples | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "sequence clustering algorithms [Analysis Services]"
  - "content queries [DMX]"
  - "sequence [Analysis Services]"
ms.assetid: 64bebcdc-70ab-43fb-8d40-57672a126602
author: minewiskan
ms.author: owend
manager: craigg
---
# Sequence Clustering Model Query Examples
  When you create a query against a data mining model, you can create either a content query, which provides details about the information stored in the model, or you can create a prediction query, which uses the patterns in the model to make predictions based on new data that you provide. For a sequence clustering model, content queries typically provide additional details about the clusters that were found, or the transitions within those clusters. You can also retrieve metadata about the model by using a query.  
  
 Prediction queries on a sequence clustering model typically make recommendations based either on the sequences and transitions, on non-sequence attributes that were included in the model, or on a combination of sequence and non-sequence attributes.  
  
 This section explains how to create queries for models that are based on the Microsoft Sequence Clustering algorithm. For general information about creating queries, see [Data Mining Queries](data-mining-queries.md).  
  
 **Content Queries**  
  
 [Using the Data Mining Schema Rowset to return model parameters](#bkmk_Query1)  
  
 [Getting a list of sequences for a state](#bkmk_Query2)  
  
 [Using system stored procedures](#bkmk_Query3)  
  
 **Prediction Queries**  
  
 [Predict next state or states](#bkmk_Query4)  
  
##  <a name="bkmk_ContentQueries"></a> Finding Information about the Sequence Clustering Model  
 To create meaningful queries on the content of a mining model, you must understand the structure of the model content, and which node types store what kind of information. For more information, see [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-sequence-clustering-models.md).  
  
###  <a name="bkmk_Query1"></a> Sample Query 1: Using the Data Mining Schema Rowset to Return Model Parameters  
 By querying the data mining schema rowset, you can find various kinds of information about the model, including basic metadata, the date and time that the model was created and last processed, the name of the mining structure that the model is based on, and the column used as the predictable attribute.  
  
 The following query returns the parameters that were used to build and train the model, `[Sequence Clustering]`. You can create this model in Lesson 5 of the [Basic Data Mining Tutorial](../../tutorials/basic-data-mining-tutorial.md).  
  
```  
SELECT MINING_PARAMETERS   
from $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'Sequence Clustering'  
```  
  
 Example results:  
  
|MINING_PARAMETERS|  
|------------------------|  
|CLUSTER_COUNT=15,MINIMUM_SUPPORT=10,MAXIMUM_STATES=100,MAXIMUM_SEQUENCE_STATES=64|  
  
 Note that this model was built by using the default value of 10 for CLUSTER_COUNT. When you specify a non-zero number of clusters for CLUSTER_COUNT, the algorithm treats this number as a hint for the approximate number of clusters to find. However, in the process of analysis, the algorithm may find more or fewer clusters. In this case, the algorithm found that 15 clusters best fit the training data. Therefore, the list of parameter values for the completed model reports the count of clusters as determined by the algorithm, not the value passed in when creating the model.  
  
 How does this behavior differ from letting the algorithm determine the best number of clusters? As an experiment, you can create another clustering model that uses this same data, but set CLUSTER_COUNT to 0. When you do this, the algorithm detects 32 clusters. Therefore, by using the default value of 10 for CLUSTER_COUNT, you constrain the number of results.  
  
 The value of 10 is used by default because reducing the number of clusters makes it easier for most people to browse and understand groupings in the data. However, each model and set of data is different. You may wish to experiment with different numbers of clusters to see which parameter value yields the most accurate model.  
  
###  <a name="bkmk_Query2"></a> Sample Query 2: Getting a List of Sequences for a State  
 The mining model content stores the sequences that are found in the training data as a first state coupled with a list of all related second states. The first state is used as the label for the sequence, and the related second states are called transitions.  
  
 For example, the following query returns the complete list of first states in the model, before the sequences are grouped into clusters.  You can get this list by returning the list of sequences (NODE_TYPE = 13) that have the model root node as parent (PARENT_UNIQUE_NAME = 0). The FLATTENED keyword makes the results easier to read.  
  
> [!NOTE]  
>  The name of the columns, PARENT_UNIQUE_NAME, Support, and Probability must be enclosed in brackets to distinguish them from the reserved keywords of the same name.  
  
```  
SELECT FLATTENED NODE_UNIQUE_NAME,  
(SELECT ATTRIBUTE_VALUE AS [Product 1],  
[Support] AS [Sequence Support],   
[Probability] AS [Sequence Probability]  
FROM NODE_DISTRIBUTION) AS t  
FROM [Sequence Clustering].CONTENT  
WHERE NODE_TYPE = 13  
AND [PARENT_UNIQUE_NAME] = 0  
```  
  
 Partial results:  
  
|NODE_UNIQUE_NAME|Product 1|Sequence Support|Sequence Probability|  
|------------------------|---------------|----------------------|--------------------------|  
|1081327|Missing|0|#######|  
|1081327|All-Purpose Bike Stand|17|0.00111|  
|1081327|Bike Wash|64|0.00418|  
|1081327|(rows 4-36 omitted)|||  
|1081327|Women's Mountain Shorts|506|0.03307|  
  
 The list of sequences in the model is always sorted alphabetically in ascending order. The ordering of the sequences is important because you can find the related transitions by looking at the order number of the sequence. The `Missing` value is always transition 0.  
  
 For example, in the previous results, the product "Women's Mountain Shorts" is the sequence number 37 in the model. You can use that information to view all of the products that were ever purchased after "Women's Mountain Shorts."  
  
 To do this, first, you reference the value returned for NODE_UNIQUE_NAME in the previous query, to get the ID of the node that contains all sequences for the model. You pass this value to the query as the ID of the parent node, to get only the transitions included in this node, which happens to contain a list of al sequences for the model. However, if you wanted to see the list of transitions for a specific cluster, you could pass in the ID of the cluster node, and see only the sequences associated with that cluster.  
  
```  
SELECT NODE_UNIQUE_NAME  
FROM [Sequence Clustering].CONTENT  
WHERE NODE_DESCRIPTION = 'Transition row for sequence state 37'  
AND [PARENT_UNIQUE_NAME] = '1081327'  
```  
  
 Example results:  
  
|NODE_UNIQUE_NAME|  
|------------------------|  
|1081365|  
  
 The node represented by this ID contains a list of the sequences that follow the "Women's Mountain Shorts" product, together with the support and probability values.  
  
```  
SELECT FLATTENED  
(SELECT ATTRIBUTE_VALUE AS Product2,  
[Support] AS [P2 Support],  
[Probability] AS [P2 Probability]  
FROM NODE_DISTRIBUTION) AS t  
FROM [Sequence Clustering].CONTENT  
WHERE NODE_UNIQUE_NAME = '1081365'  
```  
  
 Example results:  
  
|t.Product2|t.P2 Support|t.P2 Probability|  
|----------------|------------------|----------------------|  
|Missing|230.7419|0.456012|  
|Classic Vest|8.16129|0.016129|  
|Cycling Cap|60.83871|0.120235|  
|Half-Finger Gloves|30.41935|0.060117|  
|Long-Sleeve Logo Jersey|86.80645|0.171554|  
|Racing Socks|28.93548|0.057185|  
|Short-Sleeve Classic Jersey|60.09677|0.118768|  
  
 Note that support for the various sequences related to Women's Mountain Shorts is 506 in the model. The support values for the transitions also add up to 506. However, the numbers are not whole numbers, which seems a bit odd if you expect support to simply represent a count of cases that contain each transition. However, because the method for creating clusters calculates partial membership, the probability of any transition within a cluster must be weighted by its probability of belonging to that particular cluster.  
  
 For example, if there are four clusters, a particular sequence might have a 40% chance of belonging to cluster 1, a 30% chance of belonging to cluster 2, a 20% chance of belonging to cluster 3, and a 10% chance of belonging to cluster 4. After the algorithm determines the cluster that the transition is mostly likely to belong to, it weights the probabilities within the cluster by the cluster prior probability.  
  
###  <a name="bkmk_Query3"></a> Sample Query 3: Using System Stored Procedures  
 You can see from these query samples that the information stored in the model is complex, and that you might need to create multiple queries to get the information that you need. However, the Microsoft Sequence Clustering viewer provides a powerful set of tools for graphically browsing the information contained in a sequence clustering model, and you can also use the viewer to query and drill down into the model.  
  
 In most cases, the information that is presented in the Microsoft Sequence Clustering viewer is created by using Analysis Services system stored procedures to query the model. You can write Data Mining Extensions (DMX) queries against the model content to retrieve the same information, but the Analysis Services system stored procedures provide a convenient shortcut when for exploration or for testing models.  
  
> [!NOTE]  
>  System stored procedures are used for internal processing by the server and by the clients that Microsoft provides for interacting with the Analysis Services server. Therefore, Microsoft reserves the right to change them at any time. Although they are described here for your convenience, we do not support their use in a production environment. To ensure stability and compatibility in a production environment, you should always write your own queries by using DMX.  
  
 This section provides some samples of how to use the system stored procedures to create queries against a sequence clustering model:  
  
#### Cluster Profiles and Sample Cases  
 The Cluster Profiles tab shows you a list of the clusters in the model, the size of each cluster, and a histogram that indicates the states included in the cluster. There are two system stored procedures that you can use in queries to retrieve similar information:  
  
-   `GetClusterProfile` returns the characteristics of the cluster, with all the information that is found in the NODE_DISTRIBUTION table for the cluster.  
  
-   `GetNodeGraph` returns nodes and edges that can be used to construct a mathematical graph representation of the clusters, corresponding to what you see on the first tab of the Sequence Clustering view. The nodes are clusters, and the edges represent weights or strength.  
  
 The following example illustrates how to use the system stored procedure, `GetClusterProfiles`, to return all of the clusters in the model, with their respective profiles. This stored procedure executes a series of DMX statements that return the complete set of profiles in the model. However, to use this stored procedure you must know the address of the model.  
  
 `CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterProfiles('Sequence Clustering', 2147483647, 0)`  
  
 The following example illustrates how to retrieve the profile for a specific cluster, Cluster 12, by using the system stored procedure `GetNodeGraph`, and specifying the cluster ID, which is usually the same as the number in the cluster name.  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetNodeGraph('Sequence Clustering','12',0)  
```  
  
 If you omit the cluster ID, as shown in the following query, `GetNodeGraph` returns an ordered, flattened list of all cluster profiles:  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetNodeGraph('Sequence Clustering','',0)  
```  
  
 The **Cluster Profile** tab also displays a histogram of model sample cases. These sample cases represent the idealized cases for the model. These cases are not stored in the model the same way that the training data is; you must use a special syntax to retrieve the sample cases for the model.  
  
```  
SELECT * FROM [Sequence Clustering].SAMPLE_CASES WHERE IsInNode('12')  
```  
  
 For more information, see [SELECT FROM &#60;model&#62;.SAMPLE_CASES &#40;DMX&#41;](/sql/dmx/select-from-model-dmx).  
  
#### Cluster Characteristics and Cluster Discrimination  
 The **Cluster Characteristics** tab summarizes the main attributes of each cluster, ranked by probability. You can find out how many cases belong to a cluster, and what the distribution of cases is like in the cluster: Each characteristic has certain support. To see the characteristics of a particular cluster, you must know the ID of the cluster.  
  
 The following examples uses the system stored procedure, `GetClusterCharacteristics`, to return all the characteristics of Cluster 12 that have a probability score over the specified threshold of 0.0005.  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterCharacteristics('Sequence Clustering','12',0.0005)  
```  
  
 To return the characteristics of all clusters, you can leave the cluster ID empty.  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterCharacteristics('Sequence Clustering','',0.0005)  
```  
  
 The following example calls the system stored procedure `GetClusterDiscrimination` to compare the characteristics of Cluster 1 and Cluster 12.  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterDiscrimination('Sequence Clustering','1','12',0.0005,true)  
```  
  
 If you want to write your own query in DMX to compare two clusters, or compare a cluster with its complement, you must first retrieve one set of characteristics, and then retrieve the characteristics for the specific cluster that you are interested in, and compare the two sets. This scenario is more complicated and typically requires some client processing.  
  
#### States and Transitions  
 The **State Transitions** tab of the Microsoft Sequence Clustering performs complicated queries on the back end to retrieve and compare the statistics for different clusters. To reproduce these results requires a more complex query and some client processing.  
  
 However, you can use the DMX queries described in Example 2 of the section, [Content Queries](#bkmk_ContentQueries), to retrieve probabilities and states for sequences or for individual transitions.  
  
## Using the Model to Make Predictions  
 Prediction queries on a sequence clustering model can use many of the prediction functions that are used with other clustering models. In addition, you can use the special prediction function, [PredictSequence &#40;DMX&#41;](/sql/dmx/predictsequence-dmx), to make recommendations or to predict next states.  
  
###  <a name="bkmk_Query4"></a> Sample Query 4: Predict Next State or States  
 You can use the [PredictSequence &#40;DMX&#41;](/sql/dmx/predictsequence-dmx) function to predict the next most likely state, given a value. You can also predict multiple next states: for example, you can return a list of the top three products that a customer is likely to purchase, to present a list of recommendations.  
  
 The following sample query is a singleton prediction query that returns the top five predictions, together with their probability. Because the model includes a nested table, you must use the nested table, `[v Assoc Seq Line Items]`, as the column reference when making predictions. Also, when you supply values as input, you must join both the case table and the nested table columns, as shown by the nested SELECT statements.  
  
```  
SELECT FLATTENED PredictSequence([v Assoc Seq Line Items], 7)  
FROM [Sequence Clustering]  
NATURAL PREDICTION JOIN  
(SELECT  (SELECT 1 as [Line Number],  
   'All-Purpose Bike Stand' as [Model]) AS [v Assoc Seq Line Items])   
AS t  
```  
  
 Example results:  
  
|Expression.$Sequence|Expression.Line Number|Expression.Model|  
|--------------------------|----------------------------|----------------------|  
|1||Cycling Cap|  
|2||Cycling Cap|  
|3||Sport-100|  
|4||Long-Sleeve logo Jersey|  
|5||Half-Finger Gloves|  
|6||All-Purpose Bike Stand|  
|7||All-Purpose Bike Stand|  
  
 There are three columns in the results, even though you might only expect one column, because the query always returns a column for the case table. Here the results are flattened; otherwise, the query would return a single column that contains two nested table columns.  
  
 The column $sequence is a column returned by default by the `PredictSequence` function to order the prediction results. The column, `[Line Number]`, is required to match the sequence keys in the model, but the keys are not output.  
  
 Interestingly, the top predicted sequences after All-Purpose Bike Stand are Cycling Cap and Cycling Cap. This is not an error. Depending on how the data is presented to the customer, and how it is grouped when training the model, it is very possible to have sequences of this kind. For example, a customer might purchase a cycling cap (red) and then another cycling cap (blue), or purchase two in a row if there were no way to specify quantity.  
  
 The values in rows 6 and 7 are placeholders. When you reach the end of the chain of possible transitions, rather than terminating the prediction results, the value that was passed as an input is added to the results. For example, if you increased the number of predictions to 20, the values for rows 6-20 would all be the same, All-Purpose Bike Stand.  
  
## Function List  
 All [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms support a common set of functions. However, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm supports the additional functions that are listed in the following table.  
  
|||  
|-|-|  
|Prediction Function|Usage|  
|[Cluster &#40;DMX&#41;](/sql/dmx/cluster-dmx)|Returns the cluster that is most likely to contain the input case|  
|[ClusterDistance &#40;DMX&#41;](/sql/dmx/clusterdistance-dmx)|Returns the distance of the input case from the specified cluster, or if no cluster is specified, the distance of the input case from the most likely cluster.<br /><br /> This function can be used with any kind of clustering model (EM, K-Means, etc.), but the results differ depending on the algorithm.|  
|[ClusterProbability &#40;DMX&#41;](/sql/dmx/clusterprobability-dmx)|Returns the probability that the input case belongs to the specified cluster.|  
|[IsInNode &#40;DMX&#41;](/sql/dmx/isinnode-dmx)|Indicates whether the specified node contains the current case.|  
|[PredictAdjustedProbability &#40;DMX&#41;](/sql/dmx/predictadjustedprobability-dmx)|Returns the adjusted probability of a specified state.|  
|[PredictAssociation &#40;DMX&#41;](/sql/dmx/predictassociation-dmx)|Predicts associative membership.|  
|[PredictCaseLikelihood &#40;DMX&#41;](/sql/dmx/predictcaselikelihood-dmx)|Returns the likelihood that an input case will fit in the existing model.|  
|[PredictHistogram &#40;DMX&#41;](/sql/dmx/predicthistogram-dmx)|Returns a table that represents a histogram for the prediction of a given column.|  
|[PredictNodeId &#40;DMX&#41;](/sql/dmx/predictnodeid-dmx)|Returns the Node_ID of the node to which the case is classified.|  
|[PredictProbability &#40;DMX&#41;](/sql/dmx/predictprobability-dmx)|Returns the probability for a specified state.|  
|[PredictSequence &#40;DMX&#41;](/sql/dmx/predictsequence-dmx)|Predicts future sequence values for a specified set of sequence data.|  
|[PredictStdev &#40;DMX&#41;](/sql/dmx/predictstdev-dmx)|Returns the predicted standard deviation for the specified column.|  
|[PredictSupport &#40;DMX&#41;](/sql/dmx/predictsupport-dmx)|Returns the support value for a specified state.|  
|[PredictVariance &#40;DMX&#41;](/sql/dmx/predictvariance-dmx)|Returns the variance of a specified column.|  
  
 For a list of the functions that are common to all [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms, see [General Prediction Functions &#40;DMX&#41;](/sql/dmx/general-prediction-functions-dmx). For the syntax of specific functions, see [Data Mining Extensions &#40;DMX&#41; Function Reference](/sql/dmx/data-mining-extensions-dmx-function-reference).  
  
## See Also  
 [Data Mining Queries](data-mining-queries.md)   
 [Microsoft Sequence Clustering Algorithm Technical Reference](microsoft-sequence-clustering-algorithm-technical-reference.md)   
 [Microsoft Sequence Clustering Algorithm](microsoft-sequence-clustering-algorithm.md)   
 [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-sequence-clustering-models.md)  
  
  
