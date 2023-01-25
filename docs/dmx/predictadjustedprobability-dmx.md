---
title: "PredictAdjustedProbability (DMX)"
description: "PredictAdjustedProbability (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# PredictAdjustedProbability (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns the adjusted probability of a specified state.  
  
## Syntax  
  
```  
  
PredictAdjustedProbability(<scalar column reference>, [<predicted state>])  
```  
  
## Applies To  
 A scalar column.  
  
## Return Type  
 A scalar value.  
  
## Remarks  
 If the predicted state is omitted, the state that has the highest predictable probability is used, excluding the missing states bucket. To include the missing states bucket, set the \<predicted state> to **INCLUDE_NULL**.  
  
 To return the adjusted probability for the missing states, set the \<predicted state> to NULL.  
  
 The **PredictAdjustedProbability** function is a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] extension to the [!INCLUDE[msCoName](../includes/msconame-md.md)] OLE DB for Data Mining specification.  
  
## Examples  
 The following example uses a natural prediction join to determine if an individual is likely to be a bike buyer based on the TM Decision Tree mining model, and also determines the adjusted probability for the prediction.  
  
```  
SELECT  
  [Bike Buyer],  
  PredictAdjustedProbability([Bike Buyer]) AS [Adjusted Probability]  
From  
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
  
  
