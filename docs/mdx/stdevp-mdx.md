---
description: "StdevP (MDX)"
title: "StdevP (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# StdevP (MDX)


  Returns the population standard deviation of a numeric expression evaluated over a set, using the biased population formula (dividing by *n*).  
  
## Syntax  
  
```  
  
StdevP(Set_Expression [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **StdevP** function uses the biased population formula, while the [Stdev](../mdx/stdev-mdx.md) function uses the unbiased population formula.  
  
## Example  
 The following example returns the standard deviation for Internet Order Quantity evaluated over the first three months of calendar year 2003 using the biased population formula.  
  
```  
WITH MEMBER Measures.x AS   
   StdevP   
   ( { [Date].[Calendar].[Month].[January 2003],  
       [Date].[Calendar].[Month].[February 2003],  
       [Date].[Calendar].[Month].[March 2003]},  
    [Measures].[Internet Order Quantity])  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
