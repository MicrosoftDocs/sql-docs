---
title: "RangeMid (DMX) | Microsoft Docs"
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
# RangeMid (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the midpoint of the predicted bucket that is discovered for a discretized column.  
  
## Syntax  
  
```  
  
RangeMid(<scalar column reference>)  
```  
  
## Applies To  
 Discretized scalar columns.  
  
## Return Type  
 A scalar value.  
  
## Remarks  
 When used with [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md), the **RangeMin**, **RangeMid**, and **RangeMax** functions return the actual boundary values of the specified bucket. For example, if you perform a prediction on a discretized column, the query returns the predicted bucket number in the discretized column. The **RangeMin**, **RangeMid**, and **RangeMax** functions describe the bucket that the prediction specifies. When the **RangeMid** function is used with a PREDICTION JOIN statement, the scalar column reference can only contain discrete, predictable columns.  
  
## Examples  
 The following example returns the minimum, maximum, and average values for the Yearly Income continuous column in a TM Decision Tree mining model.  
  
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
 [RangeMax &#40;DMX&#41;](../dmx/rangemax-dmx.md)   
 [RangeMin &#40;DMX&#41;](../dmx/rangemin-dmx.md)  
  
  
