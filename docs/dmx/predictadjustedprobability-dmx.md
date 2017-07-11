---
title: "PredictAdjustedProbability (DMX) | Microsoft Docs"
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
  - "PredictAdjustedProbability"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "PredictAdjustedProbability function"
ms.assetid: 9a1e2ec5-5a37-4df6-a78e-26a495cc9301
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# PredictAdjustedProbability (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
