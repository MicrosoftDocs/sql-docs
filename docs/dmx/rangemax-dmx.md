---
title: "RangeMax (DMX) | Microsoft Docs"
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
# RangeMax (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the upper end of the predicted bucket that is discovered for a discretized column.  
  
## Syntax  
  
```  
  
RangeMax(<scalar column reference>)  
```  
  
## Applies To  
 Scalar columns.  
  
## Return Type  
 A scalar value.  
  
## Remarks  
 The **RangeMax** function can be used in [SELECT DISTINCT FROM &#60;model &#62; &#40;DMX&#41;](../dmx/select-distinct-from-model-dmx.md) queries. When used with this type of query, the scalar column reference can contain continuous or discrete columns that are either predictable or input.  
  
 When used with [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md), the **RangeMin**, **RangeMid**, and **RangeMax** functions return the actual boundary values of the specified bucket. For example, if you perform a prediction on a discretized column, the query returns the predicted bucket number in the discretized column. The **RangeMin**, **RangeMid**, and **RangeMax** functions describe the bucket that the prediction specifies. When the **RangeMax** function is used with a PREDICTION JOIN statement, the scalar column reference can only contain discrete, predictable columns.  
  
## Examples  
 The following example returns the minimum, maximum, and average values for the Yearly Income continuous column in the Decision Tree mining model.  
  
```  
SELECT DISTINCT   
    RangeMin([Yearly Income]) AS [Bucket Minimum],  
    RangeMid([Yearly Income]) AS [Bucket Average],   
    RangeMax([Yearly Income]) AS [Bucket Maximum]  
FROM [TM Decision Tree]  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [RangeMid &#40;DMX&#41;](../dmx/rangemid-dmx.md)   
 [RangeMin &#40;DMX&#41;](../dmx/rangemin-dmx.md)  
  
  
