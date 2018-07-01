---
title: "BottomSum (MDX) | Microsoft Docs"
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
# BottomSum (MDX)


  Sorts a specified set in ascending order, and returns a set of tuples with the lowest values whose sum is equal to or less than a specified value.  
  
## Syntax  
  
```  
  
BottomSum(Set_Expression, Value, Numeric_Expression)  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Value*  
 A valid numeric expression that specifies the value against which each tuple is compared.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **BottomSum** function calculates the sum of a specified measure evaluated over a specified set, sorting the set in ascending order. The function then returns the elements with the lowest values whose total of the specified numeric expression is at least the specified value (sum). This function returns the smallest subset of a set whose cumulative total is at least the specified value. The returned elements are ordered smallest to largest.  
  
> [!IMPORTANT]  
>  The **BottomSum** function, like the [TopSum](../mdx/topsum-mdx.md) function, always breaks the hierarchy.  
  
## Examples  
 The following example returns, for the Bike category, the smallest set of members of the City level in the Geography hierarchy in the Geography dimension for fiscal year 2003, and whose cumulative total, using the Reseller Sales Amount measure, is at least the sum of 50,000 (beginning with the members of this set with the smallest number of sales):  
  
 `SELECT`  
  
 `[Product].[Product Categories].Bikes ON 0,`  
  
 `BottomSum`  
  
 `({[Geography].[Geography].[City].Members}`  
  
 `, 50000`  
  
 `, ([Measures].[Reseller Sales Amount],[Product].[Product Categories].Bikes)`  
  
 `) ON 1`  
  
 `FROM [Adventure Works]`  
  
 `WHERE([Measures].[Reseller Sales Amount],[Date].[Fiscal].[Fiscal Year].[FY 2003])`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
