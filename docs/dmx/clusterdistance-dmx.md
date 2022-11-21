---
title: "ClusterDistance (DMX)"
description: "ClusterDistance (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# ClusterDistance (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  The **ClusterDistance** function returns the distance of the input case from the specified cluster, or if no cluster is specified, the distance of the input case from the most likely cluster.  
  
## Syntax  
  
```  
  
ClusterDistance([<ClusterID expression>])  
```  
  
## Applies To  
 This function can be used only if the underlying data mining model supports clustering. The function can be used with any kind of clustering model (EM, K-Means, etc.), but the results differ depending on the algorithm.  
  
## Return Type  
 A scalar value.  
  
## Remarks  
 The **ClusterDistance** function returns the distance between the input case and the cluster that has the highest probability for that input case.  
  
 In case of K-Means clustering, since any case can belong to only one cluster, with a membership weight of 1.0, the cluster distance is always 0. However, in K-Means, each cluster is assumed to have a centroid. You can obtain the value of the centroid by querying or browsing the NODE_DISTRIBUTION nested table in the mining model content. For more information, see [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/mining-model-content-for-clustering-models-analysis-services-data-mining).  
  
 In the case of the default EM clustering method, all the points inside the cluster are considered equally likely; therefore, by design there is no centroid for the cluster. The value of **ClusterDistance** between a particular case and a particular cluster *N* is calculated as follows:  
  
 ClusterDistance(N) =1-(membershipWeight(N))  
  
 Or:  
  
 ClusterDistance(N) =1-ClusterProbability (N))  
  
## Related Prediction Functions  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides the following additional functions for querying clustering models:  
  
-   Use the [Cluster &#40;DMX&#41;](../dmx/cluster-dmx.md) function to return the most likely cluster.  
  
-   Use the [ClusterProbability &#40;DMX&#41;](../dmx/clusterprobability-dmx.md) function to get the probability that a case belongs to a particular cluster. This value serves as the inverse of the cluster distance.  
  
-   Use the [PredictHistogram &#40;DMX&#41;](../dmx/predicthistogram-dmx.md) function to return a histogram of the likelihood of the input case existing in each of the model's clusters.  
  
-   Use the [PredictCaseLikelihood &#40;DMX&#41;](../dmx/predictcaselikelihood-dmx.md) function to return a measure from 0 to 1 that indicates how likely an input case is to exist considering the model learned by the algorithm.  
  
## Example1: Obtaining Cluster Distance to the Most Likely Cluster  
 The following example returns the distance from the specified case to the cluster that the case most likely belongs to.  
  
```  
SELECT  
    ClusterDistance()  
FROM  
    [TM Clustering]  
NATURAL PREDICTION JOIN  
(SELECT 28 AS [Age],  
    '2-5 Miles' AS [Commute Distance],  
    'Graduate Degree' AS [Education],  
    0 AS [Number Cars Owned],  
    0 AS [Number Children At Home]) AS t  
```  
  
 Example results:  
  
|Expression|  
|----------------|  
|0.0477390930705145|  
  
 To find out which cluster this is, you can substitute `Cluster` for `ClusterDistance` in the preceding sample.  
  
 Example results:  
  
|$CLUSTER|  
|--------------|  
|Cluster 6|  
  
## Example2: Obtaining Distance to a Specified Cluster  
 The following syntax uses the mining model content schema rowset to return the list of node IDs and node captions for the clusters in the mining model. You can then use the node caption as the cluster identifier argument in the **ClusterDistance** function.  
  
```  
SELECT NODE_UNIQUE_NAME, NODE_CAPTION   
FROM <model>.CONTENT   
WHERE NODE_TYPE = 5  
```  
  
 Example results:  
  
|NODE_UNIQUE_NAME|NODE_CAPTION|  
|------------------------|-------------------|  
|001|Cluster 1|  
|002|Cluster 2|  
  
 The following syntax example returns the distance of the specified case from the cluster labeled Cluster 2.  
  
```  
SELECT  
    ClusterDistance('Cluster 2')  
AS [Cluster 2 Distance]  
FROM [TM Clustering]  
NATURAL PREDICTION JOIN  
(SELECT 28 AS [Age],  
    '2-5 Miles' AS [Commute Distance],  
    'Graduate Degree' AS [Education],  
    0 AS [Number Cars Owned],  
    0 AS [Number Children At Home]) AS t  
```  
  
 Example results:  
  
|Cluster 2 Distance|  
|------------------------|  
|0.97008209236394|  
  
## See Also  
 [Cluster &#40;DMX&#41;](../dmx/cluster-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/mining-model-content-for-clustering-models-analysis-services-data-mining)  
  
