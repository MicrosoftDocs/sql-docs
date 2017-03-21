---
title: "Cluster (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "Cluster"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "Cluster function"
ms.assetid: 14b2942a-6dec-4dfa-98a6-697a3c89036a
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Cluster (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
