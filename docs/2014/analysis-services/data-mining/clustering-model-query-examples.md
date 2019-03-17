---
title: "Clustering Model Query Examples | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "clustering [Data Mining]"
  - "content queries [DMX]"
  - "clustering algorithms [Analysis Services]"
ms.assetid: bf2ba332-9bc6-411a-a3af-b919c52432c8
author: minewiskan
ms.author: owend
manager: craigg
---
# Clustering Model Query Examples
  When you create a query against a data mining model, you can retrieve metadata about the model, or create a content query that provides details about the patterns discovered in analysis. Alternatively, you can create a prediction query, which uses the patterns in the model to make predictions for new data. Each type of query will provide different information. For example, a content query might provide additional details about the clusters that were found, whereas a prediction query might tell you in which cluster a new data point is most likely to belong.  
  
 This section explains how to create queries for models that are based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm.  
  
 **Content Queries**  
  
 [Getting Model Metadata by Using DMX](#bkmk_Query1)  
  
 [Retrieving Model Metadata from the Schema Rowset](#bkmk_Query2)  
  
 [Returning a Cluster or a List of Clusters](#bkmk_Query3)  
  
 [Returning Attributes for a Cluster](#bkmk_Query4)  
  
 [Returning a Cluster Profile Using System Stored Procedures](#bkmk_Query5)  
  
 [Finding Discriminating Factors for a Cluster](#bkmk_Query6)  
  
 [Returning Cases that Belong to a Cluster](#bkmk_Query7)  
  
 **Prediction Queries**  
  
 [Predicting Outcomes from a Clustering Model](#bkmk_Query8)  
  
 [Determining Cluster Membership](#bkmk_Query9)  
  
 [Returning All Possible Clusters with Probability and Distance](#bkmk_Query10)  
  
##  <a name="bkmk_top2"></a> Finding Information about the Model  
 All mining models expose the content learned by the algorithm according to a standardized schema, the mining model schema rowset. You can create queries against the mining model schema rowset by using Data Mining Extension (DMX) statements. In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can also query the schema rowsets directly as system tables.  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query1"></a> Sample Query 1: Getting Model Metadata by Using DMX  
 The following query returns basic metadata about the clustering model, `TM_Clustering`, that you created in the Basic Data Mining Tutorial. The metadata available in the parent node of a clustering model includes the name of the model, the database where the model is stored, and the number of child nodes in the model. This query uses a DMX content query to retrieve the metadata from the parent node of the model:  
  
```  
SELECT MODEL_CATALOG, MODEL_NAME, NODE_CAPTION,   
NODE_SUPPORT, [CHILDREN_CARDINALITY], NODE_DESCRIPTION  
FROM TM_Clustering.CONTENT  
WHERE NODE_TYPE = 1  
```  
  
> [!NOTE]  
>  You must enclose the name of the column, CHILDREN_CARDINALITY, in brackets to distinguish it from the Multidimensional Expressions (MDX) reserved keyword of the same name.  
  
 Example results:  
  
|||  
|-|-|  
|MODEL_CATALOG|TM_Clustering|  
|MODEL_NAME|Adventure Works DW|  
|NODE_CAPTION|Cluster Model|  
|NODE_SUPPORT|12939|  
|CHILDREN_CARDINALITY|10|  
|NODE_DESCRIPTION|All|  
  
 For a definition of what these columns mean in a clustering model, see [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-clustering-models-analysis-services-data-mining.md).  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query2"></a> Sample Query 2: Retrieving Model Metadata from the Schema Rowset  
 By querying the data mining schema rowset, you can find the same information that is returned in a DMX content query. However, the schema rowset provides some additional columns. These include the parameters that were used when the model was created, the date and time that the model was last processed, and the owner of the model.  
  
 The following example returns the date the model was created, modified, and last processed, together with the clustering parameters that were used to build the model, and the size of the training set. This information can be useful for documenting the model, or for determining which of the clustering options were used to create an existing model.  
  
```  
SELECT MODEL_NAME, DATE_CREATED, LAST_PROCESSED, PREDICTION_ENTITY, MINING_PARAMETERS   
from $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'TM_Clustering'  
```  
  
 Example results:  
  
|||  
|-|-|  
|MODEL_NAME|TM_Clustering|  
|DATE_CREATED|10/12/2007 7:42:51 PM|  
|LAST_PROCESSED|10/12/2007 8:09:54 PM|  
|PREDICTION_ENTITY|Bike Buyer|  
|MINING_PARAMETERS|CLUSTER_COUNT=10,<br /><br /> CLUSTER_SEED=0,<br /><br /> CLUSTERING_METHOD=1,<br /><br /> MAXIMUM_INPUT_ATTRIBUTES=255,<br /><br /> MAXIMUM_STATES=100,<br /><br /> MINIMUM_SUPPORT=1,<br /><br /> MODELLING_CARDINALITY=10,<br /><br /> SAMPLE_SIZE=50000,<br /><br /> STOPPING_TOLERANCE=10|  
  
 [Return to Top](#bkmk_top2)  
  
## Finding Information about Clusters  
 The most useful content queries on clustering models generally return the same type of information that you can browse by using the **Cluster Viewer**. This includes cluster profiles, cluster characteristics, and cluster discrimination. This section provides examples of queries that retrieve this information.  
  
###  <a name="bkmk_Query3"></a> Sample Query 3: Returning a Cluster or List of Clusters  
 Because all clusters have a node type of 5, you can easily retrieve a list of the clusters by querying the model content for only the nodes of that type. You can also filter the nodes that are returned by probability or by support, as shown in this example.  
  
```  
SELECT NODE_NAME, NODE_CAPTION ,NODE_SUPPORT, NODE_DESCRIPTION  
FROM TM_Clustering.CONTENT  
WHERE NODE_TYPE = 5 AND NODE_SUPPORT > 1000  
```  
  
 Example results:  
  
|||  
|-|-|  
|NODE_NAME|002|  
|NODE_CAPTION|Cluster 2|  
|NODE_SUPPORT|1649|  
|NODE_DESCRIPTION|English Education=Graduate Degree , 32 <=Age <=48 , Number Cars Owned=0 , 35964.0771121808 <=Yearly Income <=97407.7163393957 , English Occupation=Professional , Commute Distance=2-5 Miles , Region=North America , Bike Buyer=1 , Number Children At Home=0 , Number Cars Owned=1 , Commute Distance=0-1 Miles , English Education=Bachelors , Total Children=1 , Number Children At Home=2 , English Occupation=Skilled Manual , Marital Status=S , Total Children=0 , House Owner Flag=0 , Gender=F , Total Children=2 , Region=Pacific|  
  
 The attributes that define the cluster can be found in two columns in the data mining schema rowset.  
  
-   The NODE_DESCRIPTION column contains a comma-separated list of attributes. Note that the list of attributes might be abbreviated for display purposes.  
  
-   The nested table in the NODE_DISTRIBUTION column contains the full list of attributes for the cluster. If your client does not support hierarchical rowsets, you can return the nested table by adding the FLATTENED keyword before the SELECT column list. For more information about the use of the FLATTENED keyword, see [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](/sql/dmx/select-from-model-dimension-content-dmx).  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query4"></a> Sample Query 4: Returning Attributes for a Cluster  
 For every cluster, the **Cluster Viewer** displays a profile that lists the attributes and their values. The viewer also displays a histogram that shows the distribution of values for the whole population of cases in the model. If you are browsing the model in the viewer, you can easily copy the histogram from the Mining Legend and then paste it to Excel or a Word document. You can also use the Cluster Characteristics pane of the viewer to graphically compare the attributes of different clusters.  
  
 However, if you must obtain values for more than one cluster at a time, it is easier to query the model. For example, when you browse the model, you might notice that the top two clusters differ with regard to one attribute, `Number Cars Owned`. Therefore, you want to extract the values for each cluster.  
  
```  
SELECT TOP 2 NODE_NAME,   
(SELECT ATTRIBUTE_VALUE, [PROBABILITY] FROM NODE_DISTRIBUTION WHERE ATTRIBUTE_NAME = 'Number Cars Owned')  
AS t  
FROM [TM_Clustering].CONTENT  
WHERE NODE_TYPE = 5  
```  
  
 The first line of the code specifies that you want only the top two clusters.  
  
> [!NOTE]  
>  By default, the clusters are ordered by support. Therefore, the NODE_SUPPORT column can be omitted.  
  
 The second line of the code adds a sub-select statement that returns only certain columns from the nested table column. Furthermore, it restricts the rows from the nested table to those related to the target attribute, `Number Cars Owned`. To simplify the display, the nested table is aliased.  
  
> [!NOTE]  
>  The nested table column, `PROBABILITY`, must be enclosed in brackets because it is also the name of a reserved MDX keyword.  
>   
>  Example results:  
  
|NODE_NAME|T.ATTRIBUTE_VALUE|T.PROBABILITY|  
|----------------|------------------------|-------------------|  
|001|2|0.829207754|  
|001|1|0.109354156|  
|001|3|0.034481552|  
|001|4|0.013503302|  
|001|0|0.013453236|  
|001|Missing|0|  
|002|0|0.576980023|  
|002|1|0.406623939|  
|002|2|0.016380082|  
|002|3|1.60E-05|  
|002|4|0|  
|002|Missing|0|  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query5"></a> Sample Query 5: Return a Cluster Profile Using System Stored Procedures  
 As a shortcut, rather than writing your own queries by using DMX, you can also call the system stored procedures that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses to work with clusters. The following example illustrates how to use the internal stored procedures to return the profile for a cluster with the ID of 002.  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterProfiles('TM_Clustering", '002',0.0005  
```  
  
 Similarly, you can use a system stored procedure to return the characteristics of a specific cluster, as shown in the following example:  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterCharacteristics('TM_Clustering", '009',0.0005  
```  
  
 Example results:  
  
|Attributes|Values|Frequency|Support|  
|----------------|------------|---------------|-------------|  
|Number Children at Home|0|0.999999829076798|899|  
|Region|North America|0.999852875241508|899|  
|Total Children|0|0.993860958572323|893|  
  
> [!NOTE]  
>  The data mining system stored procedures are for internal use and [!INCLUDE[msCoName](../../includes/msconame-md.md)] reserves the right to change them as needed. For production use, we recommend that you create queries by using DMX, AMO, or XMLA.  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query6"></a> Sample Query 6: Find Discriminating Factors for a Cluster  
 The **Cluster Discrimination** tab of the **Cluster Viewer** enables you to easily compare a cluster with another cluster, or compare a cluster with all remaining cases (the complement of the cluster).  
  
 However, creating queries to return this information can be complex, and you might need some additional processing on the client to store the temporary results and compare the results of two or more queries. As a shortcut, you can use the system stored procedures.  
  
 The following query returns a single table that indicates the primary discriminating factors between the two clusters that have the node IDs of 009 and 007. Attributes with positive values favor cluster 009, whereas attributes with negative values favor cluster 007.  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterDiscrimination('TM_Clustering','009','007',0.0005,true)  
```  
  
 Example results:  
  
|Attributes|Values|Score|  
|----------------|------------|-----------|  
|Region|North America|100|  
|English Occupation|Skilled Manual|94.9003803898654|  
|Region|Europe|-72.5041051379789|  
|English Occupation|Manual|-69.6503163202722|  
  
 This is the same information that is presented in the chart of the **Cluster Discrimination** viewer if you select Cluster 9 from the first drop-down list and Cluster 7 from the second drop-down list. To compare cluster 9 with its complement, you use the empty string in the second parameter, as shown in the following example:  
  
```  
CALL System.Microsoft.AnalysisServices.System.DataMining.Clustering.GetClusterDiscrimination('TM_Clustering','009','',0.0005,true)  
```  
  
> [!NOTE]  
>  The data mining system stored procedures are for internal use and [!INCLUDE[msCoName](../../includes/msconame-md.md)] reserves the right to change them as needed. For production use, we recommend that you create queries by using DMX, AMO, or XMLA.  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query7"></a> Sample Query 7: Returning Cases that Belong to a Cluster  
 If drillthrough has been enabled on the mining model, you can create queries that return detailed information about the cases used in the model. Moreover, if drillthrough has been enabled on the mining structure, you can include columns from the underlying structure by using the [StructureColumn &#40;DMX&#41;](/sql/dmx/structurecolumn-dmx) function.  
  
 The following example returns two columns that were used in the model, Age and Region, and one more column, First Name, that was not used in the model. The query returns only cases that were classified into Cluster 1.  
  
```  
SELECT [Age], [Region], StructureColumn('First Name')  
FROM [TM_Clustering].CASES  
WHERE IsInNode('001')  
```  
  
 To return the cases that belong to a cluster, you must know the ID of the cluster. You can obtain the ID of the cluster by browsing the model in one of the viewers. Or, you can rename a cluster for easier reference, after which you could use the name in place of an ID number. However, know that the names that you assign to a cluster will be lost if the model is reprocessed.  
  
 [Return to Top](#bkmk_top2)  
  
## Making Predictions using the Model  
 Although clustering is typically used for describing and understanding data, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] implementation also lets you make prediction about cluster membership, and return probabilities associated with the prediction. This section provides examples of how to create prediction queries on clustering models. You can make predictions for multiple cases, by specifying a tabular data source, or you can provide new values on at a time by creating a singleton query. For clarity the examples in this section are all singleton queries.  
  
 For more information about how to create prediction queries using DMX, see [Data Mining Query Interfaces](data-mining-query-tools.md).  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query8"></a> Sample Query 8: Predicting Outcomes from a Clustering Model  
 If the clustering model you create contains a predictable attribute, you can use the model to make predictions about outcomes. However, the model handles the predictable attribute differently depending on whether you set the predictable column to `Predict` or `PredictOnly`. If you set the usage of the column to `Predict`, the values for that attribute are added to the clustering model and appear as attributes in the finished model. However, if you set the usage of the column to `PredictOnly`, the values are not used to create clusters. Instead, after the mode is completed, the clustering algorithm creates new values for the `PredictOnly` attribute based on the clusters to which each case belongs.  
  
 The following query provides a single new case to the model, where the only information about the case is the age and gender. The SELECT statement specifies the predictable attribute/value pair that you are interested in, and the [PredictProbability &#40;DMX&#41;](/sql/dmx/predictprobability-dmx) function tells you the probability that a case with those attributes will have the targeted outcome.  
  
```  
SELECT  
  [TM_Clustering].[Bike Buyer], PredictProbability([Bike Buyer],1)  
FROM  
  [TM_Clustering]  
NATURAL PREDICTION JOIN  
(SELECT 40 AS [Age],  
  'F' AS [Gender]) AS t  
```  
  
 Example of results when usage is set to `Predict`:  
  
|Bike Buyer|Expression|  
|----------------|----------------|  
|1|0.592924735740338|  
  
 Example of results when the usage is set to `PredictOnly` and the model is reprocessed:  
  
|Bike Buyer|Expression|  
|----------------|----------------|  
|1|0.55843544003102|  
  
 In this example, the difference in the model is not significant. However, sometimes it can be important to detect differences between the actual distribution of values and what the model predicts. The [PredictCaseLikelihood &#40;DMX&#41;](/sql/dmx/predictcaselikelihood-dmx) function is useful in this scenario, because it tells you how likely a case is, given the model.  
  
 The number that is returned by the PredictCaseLikelihood function is a probability, and therefore is always between 0 and 1, with a value of .5 representing random outcome. Therefore, a score less than .5 means that the predicted case is unlikely, given the model, and a score over.5 indicates that the predicted case is more likely than not to fit the model.  
  
 For example, the following query returns two values that characterize the likelihood of a new sample case. The non-normalized value represents the probability given the current model. When you use the NORMALIZED keyword, the likelihood score that is returned by the function is adjusted by dividing "probability with the model" by "probability without the model".  
  
```  
SELECT  
PredictCaseLikelihood(NORMALIZED) AS [NormalizedValue], PredictCaseLikelihood(NONNORMALIZED) AS [NonNormalizedValue]  
FROM  
  [TM_Clustering_PredictOnly]  
NATURAL PREDICTION JOIN  
(SELECT 40 AS [Age],  
  'F' AS [Gender]) AS t  
```  
  
 Example results:  
  
|NormalizedValue|NonNormalizedValue|  
|---------------------|------------------------|  
|5.56438372679893E-11|8.65459953145182E-68|  
  
 Note that the numbers in these results are expressed in scientific notation.  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query9"></a> Sample Query 9: Determining Cluster Membership  
 This example uses the [Cluster &#40;DMX&#41;](/sql/dmx/cluster-dmx) function to return the cluster to which the new case is most likely to belong, and uses the [ClusterProbability &#40;DMX&#41;](/sql/dmx/clusterprobability-dmx) function to return the probability for membership in that cluster.  
  
```  
SELECT Cluster(), ClusterProbability()  
FROM  
  [TM_Clustering]  
NATURAL PREDICTION JOIN  
(SELECT 40 AS [Age],  
  'F' AS [Gender],  
  'S' AS [Marital Status]) AS t  
```  
  
 Example results:  
  
|$CLUSTER|Expression|  
|--------------|----------------|  
|Cluster 2|0.397918596951617|  
  
 **Note** By default, the `ClusterProbability` function returns the probability of the most likely cluster. However, you can specify a different cluster by using the syntax `ClusterProbability('cluster name')`. If you do this, be aware that the results from each prediction function are independent of the other results. Therefore, the probability score in the second column could refer to a different cluster than the cluster named in the first column.  
  
 [Return to Top](#bkmk_top2)  
  
###  <a name="bkmk_Query10"></a> Sample Query 10: Returning All Possible Clusters with Probability and Distance  
 In the previous example, the probability score was not very high. To determine if there is a better cluster, you can use the [PredictHistogram &#40;DMX&#41;](/sql/dmx/predicthistogram-dmx) function together with the [Cluster &#40;DMX&#41;](/sql/dmx/cluster-dmx) function to return a nested table that includes all possible clusters, together with the probability that the new case that belongs to each cluster. The FLATTENED keyword is used to change the hierarchical rowset into a flat table for easier viewing.  
  
```  
SELECT FLATTENED PredictHistogram(Cluster())  
From  
  [TM_Clustering]  
NATURAL PREDICTION JOIN  
(SELECT 40 AS [Age],  
  'F' AS [Gender],  
  'S' AS [Marital Status])  
```  
  
|Expression.$CLUSTER|Expression.$DISTANCE|Expression.$PROBABILITY|  
|-------------------------|--------------------------|-----------------------------|  
|Cluster 2|0.602081403048383|0.397918596951617|  
|Cluster 10|0.719691686785675|0.280308313214325|  
|Cluster 4|0.867772590378791|0.132227409621209|  
|Cluster 5|0.931039872200985|0.0689601277990149|  
|Cluster 3|0.942359230072167|0.0576407699278328|  
|Cluster 6|0.958973668972756|0.0410263310272437|  
|Cluster 7|0.979081275926724|0.0209187240732763|  
|Cluster 1|0.999169044818624|0.000830955181376364|  
|Cluster 9|0.999831227795894|0.000168772204105754|  
|Cluster 8|1|0|  
  
 By default, the results are ranked by probability. The results tell you that, even though the probability for Cluster 2 is fairly low, Cluster 2 is still the best fit for the new data point.  
  
 **Note** The additional column, `$DISTANCE`, represents the distance from the data point to the cluster. By default, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering Algorithm uses scalable EM clustering, which assigns multiple clusters to each data point and ranks the possible clusters.  However, if you create your clustering model using the K-means algorithm, only one cluster can be assigned to each data point, and this query would return only one row. Understanding these differences is necessary to interpret the results of the [PredictCaseLikelihood &#40;DMX&#41;](/sql/dmx/predictcaselikelihood-dmx) function. For more information about the differences between EM and K-means clustering, see [Microsoft Clustering Algorithm Technical Reference](microsoft-clustering-algorithm-technical-reference.md).  
  
 [Return to Top](#bkmk_top2)  
  
## Function List  
 All [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms support a common set of functions. However, models that are built by using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm support the additional functions that are listed in the following table.  
  
|||  
|-|-|  
|Prediction Function|Usage|  
|[Cluster &#40;DMX&#41;](/sql/dmx/cluster-dmx)|Returns the cluster that is most likely to contain the input case.|  
|[ClusterDistance &#40;DMX&#41;](/sql/dmx/clusterdistance-dmx)|Returns the distance of the input case from the specified cluster, or if no cluster is specified, the distance of the input case from the most likely cluster.<br /><br /> Returns the probability that the input case belongs to the specified cluster.|  
|[ClusterProbability &#40;DMX&#41;](/sql/dmx/clusterprobability-dmx)|Returns the probability that the input case belongs to the specified cluster.|  
|[IsDescendant &#40;DMX&#41;](/sql/dmx/isdescendant-dmx)|Determines whether one node is a child of another node in the model.|  
|[IsInNode &#40;DMX&#41;](/sql/dmx/isinnode-dmx)|Indicates whether the specified node contains the current case.|  
|[PredictAdjustedProbability &#40;DMX&#41;](/sql/dmx/predictadjustedprobability-dmx)|Returns the weighted probability.|  
|[PredictAssociation &#40;DMX&#41;](/sql/dmx/predictassociation-dmx)|Predicts membership in an associative dataset.|  
|[PredictCaseLikelihood &#40;DMX&#41;](/sql/dmx/predictcaselikelihood-dmx)|Returns the likelihood that an input case will fit in the existing model.|  
|[PredictHistogram &#40;DMX&#41;](/sql/dmx/predicthistogram-dmx)|Returns a table of values related to the current predicted value.|  
|[PredictNodeId &#40;DMX&#41;](/sql/dmx/predictnodeid-dmx)|Returns the Node_ID for each case.|  
|[PredictProbability &#40;DMX&#41;](/sql/dmx/predictprobability-dmx)|Returns probability for the predicted value.|  
|[PredictStdev &#40;DMX&#41;](/sql/dmx/predictstdev-dmx)|Returns the predicted standard deviation for the specified column.|  
|[PredictSupport &#40;DMX&#41;](/sql/dmx/predictsupport-dmx)|Returns the support value for a specified state.|  
|[PredictVariance &#40;DMX&#41;](/sql/dmx/predictvariance-dmx)|Returns the variance of a specified column.|  
  
 For the syntax of specific functions, see [Data Mining Extensions &#40;DMX&#41; Function Reference](/sql/dmx/data-mining-extensions-dmx-function-reference).  
  
## See Also  
 [Data Mining Queries](data-mining-queries.md)   
 [Microsoft Clustering Algorithm Technical Reference](microsoft-clustering-algorithm-technical-reference.md)   
 [Microsoft Clustering Algorithm](microsoft-clustering-algorithm.md)  
  
  
