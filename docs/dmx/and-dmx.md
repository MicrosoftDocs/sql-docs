---
title: "AND (DMX)"
description: "AND (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# AND (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Performs a logical conjunction on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 AND Expression2  
```  
  
#### Parameters  
 *Expression1*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
 *Expression2*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns TRUE if both parameters evaluate to TRUE; otherwise FALSE.  
  
## Remarks  
 Both parameters are treated as Boolean values (0 as FALSE; otherwise TRUE) before the operator performs the logical conjunction. The following table lists the values that are returned based on the various combinations of parameter values.  
  
|If Expression1 is|If Expression2 is|Return value is|  
|-----------------------|-----------------------|---------------------|  
|TRUE|TRUE|TRUE|  
|TRUE|FALSE|FALSE|  
|FALSE|TRUE|FALSE|  
|FALSE|FALSE|FALSE|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Logical Operators &#40;DMX&#41;](../dmx/operators-logical.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
