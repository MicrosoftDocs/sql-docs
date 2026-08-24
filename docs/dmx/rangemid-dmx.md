---
title: "RangeMid (DMX)"
description: "RangeMid (DMX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# RangeMid (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

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
  
## Related content

- [Data Mining Extensions (DMX) Function Reference](data-mining-extensions-dmx-function-reference.md)
- [Functions (DMX)](functions-dmx.md)
- [General Prediction Functions (DMX)](general-prediction-functions-dmx.md)
- [RangeMax (DMX)](rangemax-dmx.md)
- [RangeMin (DMX)](rangemin-dmx.md)
