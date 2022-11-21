---
title: "PredictProbability (DMX)"
description: "PredictProbability (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# PredictProbability (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns the probability for a specified state.  
  
## Syntax  
  
```  
  
PredictProbability(<scalar column reference>, [<predicted state>])  
```  
  
## Applies To  
 A scalar column.  
  
## Return Type  
 A scalar value.  
  
## Remarks  
 If the predicted state is omitted, the state that has the highest probability is used, excluding the missing states bucket. To include the missing states bucket, set the \<predicted state> to **INCLUDE_NULL**. To return the probability for the missing states, set the \<predicted state> to NULL.  
  
> [!NOTE]  
>  Some mining models do not provide probability values and therefore cannot use this function. In addition, the probability values for any particular target value are calculated differently or might have a different interpretation depending on the model type that you are querying. For more information about how probability is calculated for a particular model type, see the individual algorithm topic in [Mining Model Content &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/mining-model-content-analysis-services-data-mining).  
  
## Examples  
 The following example uses a natural prediction join to determine whether an individual is likely to be a bike buyer based on the TM Decision Tree mining model, and also determines the probability for the prediction. In this example, there are two PredictProbability functions, one for each possible value. If you omit this argument, the function returns the probability for the most likely value.  
  
```  
SELECT  
  [Bike Buyer],  
  PredictProbability([Bike Buyer], 1) AS [Bike Buyer = Yes],  
  PredictProbability([Bike Buyer], 0) AS [Bike Buyer = No]  
FROM [TM Decision Tree]  
NATURAL PREDICTION JOIN  
(SELECT 28 AS [Age],  
  '2-5 Miles' AS [Commute Distance],  
  'Graduate Degree' AS [Education],  
  0 AS [Number Cars Owned],  
  0 AS [Number Children At Home]) AS t  
```  
  
 Example results:  
  
|Bike Buyer|Bike Buyer = Yes|Bike Buyer = No|  
|----------------|-----------------------|----------------------|  
|1|0.867074195848097|0.132755556974282|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
