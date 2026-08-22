---
title: "VarP (MDX)"
description: "VarP (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
