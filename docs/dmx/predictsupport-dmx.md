---
title: "PredictSupport (DMX)"
description: "PredictSupport (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# PredictSupport (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns the support value for a specified state.  
  
## Syntax  
  
```  
  
PredictSupport(<scalar column reference>, [<predicted state>])  
```  
  
## Applies To  
 A scalar column.  
  
## Return Type  
 A scalar value of the type that is specified by *\<*scalar column reference*>*.  
  
## Remarks  
 If the predicted state is omitted, the state that has the highest predictable probability is used, excluding the missing states bucket. To include the missing states bucket, set the \<predicted state> to **INCLUDE_NULL**.  
  
 To return the support for the missing states, set the \<predicted state> to NULL.  
  
> [!NOTE]  
>  The support values are calculated differently or might have a different interpretation depending on the model type that you are querying. For more information about how support is calculated for any particular model type, see the individual algorithm type in [Mining Model Content &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/mining-model-content-analysis-services-data-mining).  
  
## Examples  
 The following example uses a singleton query to predict whether an individual will be a bike buyer, and also determines the support for the prediction based on the TM Decision Tree mining model.  
  
```  
SELECT  
  [Bike Buyer],  
  PredictSupport([Bike Buyer]) AS [Support],  
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
  
