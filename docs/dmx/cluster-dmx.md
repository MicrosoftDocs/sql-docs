---
title: "Cluster (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Cluster (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the cluster that is most likely to contain the input case.  
  
## Syntax  
  
```  
  
Cluster()  
```  
  
## Applies To  
 This function can be used only if the underlying data mining model supports clustering.  
  
## Return Type  
 The **Cluster** function does not require parameters.  
  
 The **Cluster** function returns a scalar value of a cluster name. However, if you use this function as an argument of another function, you must regard it as a \<cluster column reference>.  
  
## Remarks  
 **Cluster** can also be used as a `<`cluster column reference`>` for a **PredictHistogram** function.  
  
## Examples  
 The following example uses a singleton query with the [PredictHistogram &#40;DMX&#41;](../dmx/predicthistogram-dmx.md) and Cluster functions to return the distance of the individual case from each cluster of the TM Clustering mining model and the probability that the individual case will exist in each cluster.  
  
```  
SELECT  
  PredictHistogram(Cluster())  
FROM  
  [TM Clustering]  
  NATURAL PREDICTION JOIN  
(SELECT 28 AS [Age],  
  '2-5 Miles' AS [Commute Distance],  
  'Graduate Degree' AS [Education],  
  0 AS [Number Cars Owned],  
  0 AS [Number Children At Home]) AS t  
```  
  
## See Also  
 [ClusterProbability &#40;DMX&#41;](../dmx/clusterprobability-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
