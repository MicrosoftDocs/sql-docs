---
title: "+ (Add) (DMX)"
description: "+ (Add) (DMX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# + (Add) (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Performs an arithmetic operation that adds two numbers together.  
  
## Syntax  
  
```  
  
Numeric_Expression + Numeric_Expression  
```  
  
#### Parameters  
 *Numeric_Expression*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
## Return Value  
 A value that has the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns the result of the other expression.  
  
## Related content

- [Operators - Arithmetic](operators-arithmetic.md)
- [Data Mining Extensions (DMX) Operator Reference](data-mining-extensions-dmx-operator-reference.md)
- [Operators (DMX)](operators-dmx.md)
