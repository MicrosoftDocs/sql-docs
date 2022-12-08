---
description: "Covariance (MDX)"
title: "Covariance (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Covariance (MDX)


  Returns the population covariance of x-y pairs of values evaluated over a set, by using the biased population formula (dividing by the number of x-y pairs).  
  
## Syntax  
  
```  
  
Covariance(Set_Expression,Numeric_Expression_y [ ,Numeric_Expression_x ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression_y*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the y-axis.  
  
 *Numeric_Expression_x*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the x-axis.  
  
## Remarks  
 The **Covariance** function evaluates the specified set against the first numeric expression, to get the values for the y-axis. The function then evaluates the specified set against the second numeric expression, if specified, to get the set of values for the x-axis. If the second numeric expressionis not specified, the function uses the current context of the cells in the specified set as values for the x-axis.  
  
 The **Covariance** function uses the biased population formula. This is in contrast to the [CovarianceN](../mdx/covariancen-mdx.md) function that uses the unbiased population formula (dividing the number of x-y pairs, then subtracting 1).  
  
> [!NOTE]  
>  The **Covariance** function ignores empty cells or cells that contain text or logical values are ignored. However, the function includes cells with values of zero.  
  
## Example  
 The following example shows how to use the Covariance function:  
  
```  
WITH   
MEMBER [Measures].[CovarianceDemo] AS  
COVARIANCE([Date].[Date].[Date].Members, [Measures].[Internet Sales Amount], [Measures].[Internet Order Count])   
SELECT {[Measures].[CovarianceDemo]} ON 0   
FROM  
[Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
