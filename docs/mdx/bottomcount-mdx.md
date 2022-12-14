---
description: "BottomCount (MDX)"
title: "BottomCount (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# BottomCount (MDX)


  Sorts a set in ascending order, and returns the specified number of tuples in the specified set with the lowest values.  
  
## Syntax  
  
```  
  
BottomCount(Set_Expression, Count [,Numeric_Expression])  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Count*  
 A valid numeric expression that specifies the number of tuples to be returned.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, this function sorts the tuples in the specified set according to the value of the specified numeric expression as evaluated over the set, in ascending order. The **BottomCount** function then returns the specified number of tuples with the lowest value.  
  
> [!IMPORTANT]  
>  The **BottomCount** function, like the [TopCount](../mdx/topcount-mdx.md) function, always breaks the hierarchy.  
  
 If a numeric expression is not specified, the function returns the set of members in natural order, without any sorting, behaving like the [Tail (MDX)](../mdx/tail-mdx.md) function.  
  
## Example  
 The following example returns the Reseller Order Quantity measure by for each calendar year for the bottom five Product SubCategory sales, ordered based on the Reseller Sales Amount measure.  
  
```  
SELECT BottomCount([Product].[Product Categories].[Subcategory].Members  
   , 10  
   , [Measures].[Reseller Sales Amount]) ON 0,  
   [Date].[Calendar].[Calendar Year].Members ON 1  
  
FROM  
    [Adventure Works]  
WHERE  
    [Measures].[Reseller Order Quantity]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
