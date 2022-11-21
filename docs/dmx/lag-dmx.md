---
title: "Lag (DMX)"
description: "Lag (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Lag (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

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
  
  
