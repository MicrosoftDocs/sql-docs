---
title: "NOT (DMX)"
description: "NOT (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# NOT (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  A logical operator that performs a logical negation on a numeric expression.  
  
## Syntax  
  
```  
  
NOT Expression1  
```  
  
#### Parameters  
 *Expression1*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns FALSE if the argument evaluates to TRUE; otherwise FALSE.  
  
## Remarks  
 The argument is treated as a Boolean value (0 as FALSE; otherwise TRUE) before the operator performs the logical negation. If *Expression1* is TRUE, the operator returns FALSE. If *Expression1* is FALSE, the operator returns TRUE. The following table illustrates how the logical conjunction is performed.  
  
|If Expression1 is|Return value is|  
|-----------------------|---------------------|  
|TRUE|FALSE|  
|FALSE|TRUE|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Logical Operators &#40;DMX&#41;](../dmx/operators-logical.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
