---
title: "IsInNode (DMX)"
description: "IsInNode (DMX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# IsInNode (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Indicates whether the specified node contains the current case.  
  
## Syntax  
  
```  
  
IsInNode(<NodeID>)  
```  
  
## Return Type  
 A Boolean type.  
  
## Remarks  
 **IsInNode** is only used in [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md) and [SELECT FROM &#60;model&#62;.SAMPLE_CASES &#40;DMX&#41;](../dmx/select-from-model-sample-cases-dmx.md) queries.  
  
## Examples  
 The following example returns all the cases that were used to create the model that is associated with the node that is specified in the IsInNode function.  
  
```  
Select * from [TM Decision Tree].Cases  
WHERE IsInNode('0')  
```  
  
## Related content

- [Data Mining Extensions (DMX) Function Reference](data-mining-extensions-dmx-function-reference.md)
- [Functions (DMX)](functions-dmx.md)
- [General Prediction Functions (DMX)](general-prediction-functions-dmx.md)
