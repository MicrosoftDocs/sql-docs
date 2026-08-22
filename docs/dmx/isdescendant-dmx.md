---
title: "IsDescendant (DMX)"
description: "IsDescendant (DMX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# IsDescendant (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Indicates whether the current node descends from the specified node.  
  
## Syntax  
  
```  
  
IsDescendant(<NodeID>)  
```  
  
## Return Type  
 A Boolean type.  
  
## Remarks  
 **IsDescendant** is only used in [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](../dmx/select-from-model-content-dmx.md) and [SELECT FROM &#60;model&#62;.DIMENSION_CONTENT &#40;DMX&#41;](../dmx/select-from-model-dimension-content-dmx.md) queries.  
  
## Examples  
 The following example returns all the cases that are descendants of the node that is specified in the IsDescendant function.  
  
```  
SELECT * FROM [TM Decision Tree].CONTENT  
WHERE IsDescendant('00000000100')  
```  
  
## Related content

- [Data Mining Extensions (DMX) Function Reference](data-mining-extensions-dmx-function-reference.md)
- [Functions (DMX)](functions-dmx.md)
- [General Prediction Functions (DMX)](general-prediction-functions-dmx.md)
