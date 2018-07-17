---
title: "+ (String Concatenation) (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# + (String Concatenation) (MDX)


  Performs a string operation that concatenates two or more character strings, tuples, or a combination of strings and tuples.  
  
## Syntax  
  
```  
  
String_Expression + String_Expression  
```  
  
#### Parameters  
 *String_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a string value.  
  
## Return Value  
 A value with the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns the result of the other expression.  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
