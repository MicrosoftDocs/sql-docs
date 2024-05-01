---
title: "* (Multiply) (MDX)"
description: "* (Multiply) (MDX)"
author: kfollis
ms.author: kfollis
ms.reviewer: kfollis
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# * (Multiply) (MDX)


  Performs an arithmetic operation that multiplies two numbers.  
  
## Syntax  
  
```  
  
Numeric_Expression * Numeric_Expression  
```  
  
#### Parameters  
 *Numeric_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
## Return Value  
 A value with the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns a null value.  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
