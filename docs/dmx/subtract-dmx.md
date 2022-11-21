---
title: "- (Subtract) (DMX)"
description: "- (Subtract) (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# - (Subtract) (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Performs an arithmetic operation that subtracts one number from another number.  
  
## Syntax  
  
```  
  
Numeric_Expression - Numeric_Expression  
```  
  
#### Parameters  
 *Numeric_Expression*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
## Return Value  
 A value that has the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns the result of the other expression.  
  
## See Also  
 [Arithmetic Operators &#40;DMX&#41;](../dmx/operators-arithmetic.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
