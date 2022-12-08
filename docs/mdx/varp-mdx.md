---
description: "VarP (MDX)"
title: "VarP (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# VarP (MDX)


  Returns the population variance of a numeric expression evaluated over a set, using the biased population formula (dividing by *n*-1).  
  
## Syntax  
  
```  
  
VarP(Set_Expression [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **VarP** function returns the biased variance of a specified numeric expression, evaluated over a specified set.  
  
 The **VarP** function uses the biased population formula, while the [Var](../mdx/var-mdx.md) function uses the unbiased population formula.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
