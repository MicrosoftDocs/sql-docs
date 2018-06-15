---
title: "Stdev (MDX) | Microsoft Docs"
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
# Stdev (MDX)


  Returns the sample standard deviation of a numeric expression evaluated over a set, using the unbiased population formula (dividing by n-1).  
  
## Syntax  
  
```  
  
Stdev(Set_Expression [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **Stdev** function uses the unbiased population formula, while the [StdevP](../mdx/stdevp-mdx.md) function uses the biased population formula.  
  
## Example  
 The following example returns the standard deviation for Internet Order Quantity, evaluated over the first three months of calendar year 2003, using the unbiased population formula.  
  
```  
WITH MEMBER Measures.x AS   
   Stdev   
   ( { [Date].[Calendar].[Month].[January 2003],  
       [Date].[Calendar].[Month].[February 2003],  
       [Date].[Calendar].[Month].[March 2003]},  
    [Measures].[Internet Order Quantity])  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
