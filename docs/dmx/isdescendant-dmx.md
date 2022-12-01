---
title: "IsDescendant (DMX)"
description: "IsDescendant (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
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
 The following example returns all the cases that are descendents of the node that is specified in the IsDescendant function.  
  
```  
SELECT * FROM [TM Decision Tree].CONTENT  
WHERE IsDescendant('00000000100')  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
