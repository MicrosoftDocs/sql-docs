---
description: "Median (MDX)"
title: "Median (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Median (MDX)


  Returns the median value of a numeric expression that is evaluated over a set.  
  
## Syntax  
  
```  
  
Median(Set_Expression [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, the specified numeric expression is evaluated across the set and then returns the median value from that evaluation. If a numeric expression is not specified, the specified set is evaluated in the current context of the members of the set and returns the median value from the evaluation.  
  
 The median value is the middle value in a set of ordered numbers. (The medial value is unlike the mean value, which is the sum of a set of numbers divided by the count of numbers in the set). The median value is determined by choosing the smallest value such that at least half of the values in the set are no greater than the chosen value. If the number of values within the set is odd, the median value corresponds to a single value. If the number of values within the set is even, the median value corresponds to the sum of the two middle values divided by two.  
  
> [!NOTE]  
>  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] ignores nulls when calculating the median value in a set of ordered numbers.  
  
## Example  
 The following example returns the median monthly sales for each quarter, each subcategory, and each country/region in the Adventure Works cube.  
  
```  
WITH MEMBER Measures.x AS Median   
   ([Date].[Calendar].CurrentMember.Children  
      , [Measures].[Reseller Order Quantity]  
   )  
SELECT Measures.x ON 0  
,NON EMPTY [Date].[Calendar].[Calendar Quarter]*   
   [Product].[Product Categories].[Subcategory].members *  
   [Geography].[Geography].[Country].Members  
ON 1  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
