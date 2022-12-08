---
description: "TopCount (MDX)"
title: "TopCount (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# TopCount (MDX)


  Sorts a set in descending order and returns the specified number of elements with the highest values.  
  
## Syntax  
  
```  
  
TopCount(Set_Expression,Count [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Count*  
 A valid numeric expression that specifies the number of tuples to be returned.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, the **TopCount** function sorts, in descending order, the tuples in the set specified by the specified set according to the value specified by the numeric expression, as evaluated over the specified set. After sorting the set, the **TopCount** function then returns the specified number of tuples with the highest value.  
  
> [!IMPORTANT]  
>  Like the [BottomCount](../mdx/bottomcount-mdx.md) function, the **TopCount** function always breaks the hierarchy.  
  
 If a numeric expression is not specified, the function returns the set of members in natural order, without any sorting, behaving like the [Head (MDX)](../mdx/head-mdx.md) function.  
  
## Examples  
 The following example returns the top 10 dates by Internet Sales Amount:  
  
 `SELECT [Measures].[Internet Sales Amount] ON 0,`  
  
 `TOPCOUNT([Date].[Date].[Date].MEMBERS, 10, [Measures].[Internet Sales Amount])`  
  
 `ON 1`  
  
 `FROM [Adventure Works]`  
  
 The following example returns, for the Bike category, the first five members in the set containing all combinations of members of the City level in the Geography hierarchy in the Geography dimension and all fiscal years from the Fiscal hierarchy of the Date dimension, ordered by the Reseller Sales Amount measure (beginning with the members of this set with the largest number of sales).  
  
```  
SELECT [Measures].[Reseller Sales Amount] ON 0,  
TopCount  
   ({[Geography].[Geography].[City].Members   
      *[Date].[Fiscal].[Fiscal Year].Members}  
   , 5  
   , [Measures].[Reseller Sales Amount]  
   ) ON 1  
FROM [Adventure Works]  
WHERE([Product].[Product Categories].Bikes)  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
