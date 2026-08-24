---
title: "PredictVariance (DMX)"
description: "PredictVariance (DMX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# PredictVariance (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns the variance of a specified column.  
  
## Syntax  
  
```  
  
PredictVariance(<scalar column reference>)  
```  
  
## Applies To  
 A scalar column.  
  
## Return Type  
 A scalar value of the type that is specified by *\<scalar column reference>*.  
  
## Remarks  
 If the column reference is discrete, **PredictVariance** returns 0 because the variance cannot be calculated from discrete values.  
  
## Examples  
 The following example uses a natural prediction join to determine if an individual is likely to be a bike buyer based on the TM Decision Tree mining model, and also determines the variance for the prediction.  
  
```  
SELECT  
  [Bike Buyer],  
  PredictVariance([Bike Buyer]) AS [Variance]  
FROM  
  [TM Decision Tree]  
NATURAL PREDICTION JOIN  
(SELECT 28 AS [Age],  
  '2-5 Miles' AS [Commute Distance],  
  'Graduate Degree' AS [Education],  
  0 AS [Number Cars Owned],  
  0 AS [Number Children At Home]) AS t  
```  
  
## Related content

- [Data Mining Extensions (DMX) Function Reference](data-mining-extensions-dmx-function-reference.md)
- [Functions (DMX)](functions-dmx.md)
- [General Prediction Functions (DMX)](general-prediction-functions-dmx.md)
