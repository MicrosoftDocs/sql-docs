---
title: "(Divide) (DMX)"
description: "(Divide) (DMX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# (Divide) (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Performs an arithmetic operation that divides one number by another number.  
  
## Syntax  
  
```  
  
Dividend / Divisor  
```  
  
#### Parameters  
 *Dividend*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
 *Divisor*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A value that has the data type of the parameter that has the higher precedence.  
  
## Remarks  
 The value that this operator returns represents the quotient of the first expression divided by the second expression.  
  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If the divisor evaluates to a null value, the operator raises an error. If both the divisor and the dividend evaluate to a null value, the operator returns a null value.  
  
## Related content

- [Operators - Arithmetic](operators-arithmetic.md)
- [Data Mining Extensions (DMX) Operator Reference](data-mining-extensions-dmx-operator-reference.md)
- [Operators (DMX)](operators-dmx.md)
- [Divide (SSIS Expression)](../integration-services/expressions/divide-ssis-expression.md)
- [/ (Division) (Transact-SQL)](../t-sql/language-elements/divide-transact-sql.md)
