---
title: "Lag (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "LAG"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "Lag function"
ms.assetid: 2da6df1a-5506-4871-a0f0-83292f1df41e
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# Lag (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the time slice between the date of the current case and the last date of the training set.  
  
## Syntax  
  
```  
  
Lag()  
```  
  
## Return Type  
 A scalar value of the type integer.  
  
## Remarks  
 If the **Lag** function is used on a model where the KEY TIME column is located within a nested table, the function must be located within the sub-select of the statement.  
  
## Examples  
 The following example returns cases that fall within the last 12 months of the data that was used to train the model.  
  
```  
SELECT * FROM [Forecasting].CASES  
WHERE Lag() < 12  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
