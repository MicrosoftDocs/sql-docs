---
description: "CovarianceN (MDX)"
title: "CovarianceN (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# CovarianceN (MDX)


  Returns the sample covariance of x-y pairs of values evaluated over a set, by using the unbiased population formula (dividing by the number of x-y pairs).  
  
## Syntax  
  
```  
  
CovarianceN(Set_Expression, Numeric_Expression_y [ ,Numeric_Expression_x ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression_y*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the y-axis.  
  
 *Numeric_Expression_x*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the x-axis.  
  
## Remarks  
 The **CovarianceN** function evaluates the specified set against the first numeric expression, to get the values for the y-axis. The function then evaluates the specified set against the second numeric expression, if specified, to get the set of values for the x-axis. If the second numeric expression is not specified, the function uses the current context of the cells in the specified set as values for the x-axis.  
  
 The **CovarianceN** function uses the unbiased population formula. This is in contrast to the [Covariance](../mdx/covariance-mdx.md) function that uses the biased population formula (dividing by the number of x-y pairs).  
  
> [!NOTE]  
>  The **CovarianceN** function ignores empty cells or cells that contain text or logical values. However, the function includes cells with values of zero.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
