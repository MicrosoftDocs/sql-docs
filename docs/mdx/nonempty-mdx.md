---
description: "NonEmpty (MDX)"
title: "NonEmpty (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# NonEmpty (MDX)


  Returns the set of tuples that are not empty from a specified set, based on the cross product of the specified set with a second set.  
  
## Syntax  
  
```  
  
NONEMPTY(set_expression1 [,set_expression2])  
```  
  
## Arguments  
 *set_expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *set_expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Remarks  
 This function returns the tuples in the first specified set that are non-empty when evaluated across the tuples in the second set. The **NonEmpty** function takes into account calculations and preserves duplicate tuples. If a second set is not provided, the expression is evaluated in the context of the current coordinates of the members of the attribute hierarchies and the measures in the cube.  
  
> [!NOTE]  
>  Use this function rather than the deprecated [NonEmptyCrossjoin &#40;MDX&#41;](../mdx/nonemptycrossjoin-mdx.md) function.  
  
> [!IMPORTANT]  
>  Non-empty is a characteristic of the cells references by the tuples, not the tuples themselves.  
  
## Examples  
 The following query shows a simple example of **NonEmpty**, returning all the Customers who had a non-null value for Internet Sales Amount on July 1st 2001:  
  
 `SELECT [Measures].[Internet Sales Amount] ON 0,`  
  
 `NONEMPTY(`  
  
 `[Customer].[Customer].[Customer].MEMBERS`  
  
 `, {([Date].[Calendar].[Date].&[20010701], [Measures].[Internet Sales Amount])}`  
  
 `)`  
  
 `ON 1`  
  
 `FROM [Adventure Works]`  
  
 The following example returns the set of tuples containing customers and purchase dates, using the **Filter** function and the **NonEmpty** functions to find the last date that each customer made a purchase:  
  
 `WITH SET MYROWS AS FILTER`  
  
 `(NONEMPTY`  
  
 `([Customer].[Customer Geography].[Customer].MEMBERS`  
  
 `* [Date].[Date].[Date].MEMBERS`  
  
 `, [Measures].[Internet Sales Amount]`  
  
 `) AS MYSET`  
  
 `, NOT(MYSET.CURRENT.ITEM(0)`  
  
 `IS MYSET.ITEM(RANK(MYSET.CURRENT, MYSET)).ITEM(0))`  
  
 `)`  
  
 `SELECT [Measures].[Internet Sales Amount] ON 0,`  
  
 `MYROWS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [DefaultMember &#40;MDX&#41;](../mdx/defaultmember-mdx.md)   
 [Filter &#40;MDX&#41;](../mdx/filter-mdx.md)   
 [IsEmpty &#40;MDX&#41;](../mdx/isempty-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [NonEmptyCrossjoin &#40;MDX&#41;](../mdx/nonemptycrossjoin-mdx.md)  
  
  
