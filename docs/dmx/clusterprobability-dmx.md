---
title: "ClusterProbability (DMX)"
description: "ClusterProbability (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# ClusterProbability (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns the probability that the input case belongs to the specified cluster.  
  
## Syntax  
  
```  
  
ClusterProbability([<Node_Caption>])  
```  
  
## Applies To  
 This function can be used only if the underlying data mining model supports clustering.  
  
## Return Type  
 A scalar value.  
  
## Remarks  
 The following syntax uses the mining model content schema rowset to return the node captions that exist in the mining model.  
  
```  
SELECT NODE_CAPTION FROM <model>.CONTENT  
```  
  
 For more information about using this syntax, see [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](../dmx/select-from-model-content-dmx.md). For more information about the mining model content schema rowset, see [DMSCHEMA_MINING_MODEL_CONTENT Rowset](/previous-versions/sql/sql-server-2012/ms126267(v=sql.110)).  
  
 If a \<node caption> is not specified, the function returns the probability that the input cases belong to the most likely cluster. Use the **Cluster** function to return the most likely cluster.  
  
## Examples  
 The following example returns the probability that the specified case exists in the cluster labeled Cluster 2.  
  
```  
SELECT  
  ClusterProbability('Cluster 2')  
From  
  [TM Clustering]  
NATURAL PREDICTION JOIN  
(SELECT 28 AS [Age],  
  '2-5 Miles' AS [Commute Distance],  
  'Graduate Degree' AS [Education],  
  0 AS [Number Cars Owned],  
  0 AS [Number Children At Home]) AS t  
```  
  
## See Also  
 [Cluster &#40;DMX&#41;](../dmx/cluster-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
