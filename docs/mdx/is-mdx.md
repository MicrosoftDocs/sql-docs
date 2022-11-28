---
description: "IS (MDX)"
title: "IS (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# IS (MDX)


  Performs a logical comparison on two object expressions.  
  
## Syntax  
  
```  
  
Expression1 IS ( Expression2 | NULL )  
```  
  
#### Parameters  
 *Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns an MDX object reference.  
  
 *Expression2*  
 A valid MDX expression that returns an MDX object reference.  
  
## Return Value  
 A Boolean value that returns **true** if both arguments refer to the same object; otherwise, **false**. If the **NULL** keyword is specified, the operator returns **true** if *Expression1* is **null**; otherwise, **false**.  
  
## Remarks  
 The **IS** operator is often used to determine whether tuples and members are idempotent, meaning that they are exactly equivalent.  
  
## Examples  
 The following example shows how to use the **IS** operator to check if the current member on an axis is a specific member:  
  
 `With`  
  
 `//Returns TRUE if the currentmember is Bikes`  
  
 `Member [Measures].[IsBikes?] AS`  
  
 `[Product].[Category].CurrentMember IS [Product].[Category].&[1]`  
  
 `SELECT`  
  
 `{[Measures].[IsBikes?]} ON 0,`  
  
 `[Product].[Category].[Category].Members ON 1`  
  
 `FROM`  
  
 `[Adventure Works]`  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
