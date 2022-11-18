---
description: "Divide - MDX Operator Reference"
title: "(Divide) (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Divide - MDX Operator Reference


  Performs an arithmetic operation that divides one number by another number.  
  
## Syntax  
  
```  
  
Dividend / Divisor  
```  
  
#### Parameters  
 *Dividend*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
 *Divisor*  
 A valid MDX expression that returns a numeric value.  
  
## Return Value  
 A value with the data type of the parameter that has the higher precedence.  
  
## Remarks  
 The actual value returned by the **/ (Divide)** operator represents the quotient of the first expression divided by the second expression.  
  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If *Divisor* evaluates to a null value, the operator raises an error. If both *Divisor* and *Dividend* evaluate to a null value, the operator returns a null value.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This query returns the freight cost per user,  
-- for products, averaged by month.   
With Member [Measures].[Freight Per Customer] as  
    [Measures].[Internet Freight Cost]  
    /   
    [Measures].[Customer Count]  
  
SELECT   
    [Ship Date].[Calendar].[Calendar Year] Members ON 0,  
    [Product].[Category].[Category].Members ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[Freight Per Customer])  
```  
  
 Dividing a non-zero or non-null value by zero or null will return the value Infinity, which is displayed in query results as the value "1.#INF". In most cases, you should check for division by zero to avoid this situation. The following example shows you how:  
  
 `//Returns 1.#INF when Internet Sales Amount is zero or null`  
  
 `Member [Measures].[Reseller to Internet Ratio] AS`  
  
 `[Measures].[Reseller Sales Amount]`  
  
 `/`  
  
 `[Measures].[Internet Sales Amount]`  
  
 `//Traps the division by zero scenario and returns null instead of 1.#INF`  
  
 `Member [Measures].[Reseller to Internet Ratio With Error Handling] AS`  
  
 `IIF([Measures].[Internet Sales Amount]=0, NULL,`  
  
 `[Measures].[Reseller Sales Amount]`  
  
 `/`  
  
 `[Measures].[Internet Sales Amount])`  
  
 `SELECT`  
  
 `{[Measures].[Reseller to Internet Ratio],[Measures].[Reseller to Internet Ratio With Error Handling]} ON 0,`  
  
 `[Product].[Category].[Category].Members ON 1`  
  
 `FROM`  
  
 `[Adventure Works]`  
  
 `WHERE([Date].[Calendar].[Calendar Year].&[2001])`  
  
## See Also  
 [IIf &#40;MDX&#41;](../mdx/iif-mdx.md)   
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
