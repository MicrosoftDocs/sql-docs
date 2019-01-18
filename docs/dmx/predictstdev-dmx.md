---
title: "PredictStdev (DMX) | Microsoft Docs"
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
# PredictStdev (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the predicted standard deviation for the specified column.  
  
## Syntax  
  
```  
  
PredictStdev(<scalar column reference>)  
```  
  
## Applies To  
 A scalar column.  
  
## Return Type  
 A scalar value of the type that is specified by *\<scalar column reference>*.  
  
## Remarks  
 If the column reference is discrete, **PredictStdev** returns 0 because the standard deviation cannot be calculated from discrete values.  
  
## Examples  
 The following example uses a natural prediction join to determine whether an individual is likely to be a bike buyer based on the TM Decision Tree mining model, and also determines the standard deviation for the prediction.  
  
```  
SELECT  
  [Bike Buyer],  
  PredictStdev([Bike Buyer]) AS [Standard Deviation]  
FROM  
  [TM Decision Tree]  
NATURAL PREDICTION JOIN  
(SELECT 28 AS [Age],  
  '2-5 Miles' AS [Commute Distance],  
  'Graduate Degree' AS [Education],  
  0 AS [Number Cars Owned],  
  0 AS [Number Children At Home]) AS t  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
