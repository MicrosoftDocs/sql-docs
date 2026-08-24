---
title: "Var (MDX)"
description: "Var (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Var (MDX)


  Returns the sample variance of a numeric expression evaluated over a set, using the unbiased population formula (dividing by *n*).  
  
## Syntax  
  
```  
  
Var(Set_Expression [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **Var** function returns the unbiased variance of a specified numeric expression evaluated over a specified set.  
  
 The **Var** function uses the unbiased population formula, and the [VarP](../mdx/varp-mdx.md) function uses the biased population formula.  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
