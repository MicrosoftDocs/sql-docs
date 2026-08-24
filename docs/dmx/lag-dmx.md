---
title: "Lag (DMX)"
description: "Lag (DMX)"
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
  
## Related content

- [Data Mining Extensions (DMX) Function Reference](data-mining-extensions-dmx-function-reference.md)
- [Functions (DMX)](functions-dmx.md)
- [General Prediction Functions (DMX)](general-prediction-functions-dmx.md)
