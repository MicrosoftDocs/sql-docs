---
description: "DistinctCount (MDX)"
title: "DistinctCount (MDX) | Microsoft Docs"
ms.date: 01/12/2021
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# DistinctCount (MDX)


  Returns the number of distinct, nonempty tuples in a set.  
  
## Syntax  
  
```  
  
DistinctCount(Set_Expression)  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Remarks  
 The **DistinctCount** function is equivalent to `Count(Distinct(Set_Expression), EXCLUDEEMPTY)`.  
  
## Examples  
 The following query shows how to use the DistinctCount function:  
  
 ```mdx
WITH SET MySet AS  
 {[Customer].[Customer Geography].[Country].&[Australia],[Customer].[Customer Geography].[Country].&[Australia],
 [Customer].[Customer Geography].[Country].&[Canada],[Customer].[Customer Geography].[Country].&[France],  
 [Customer].[Customer Geography].[Country].&[United Kingdom],[Customer].[Customer Geography].[Country].&[United Kingdom]}  
 * 
 {([Date].[Calendar].[Date].&[20010701],[Measures].[Internet Sales Amount] )}   
 MEMBER MEASURES.SETDISTINCTCOUNT AS  
 DISTINCTCOUNT(MySet)  
 SELECT {MEASURES.SETDISTINCTCOUNT} ON 0 
 FROM [Adventure Works] 
 ```

The DistinctCount function returns the distinct number of items in a set; in this example, the optional second parameter is used to exclude items that don't have a value for a given tuple. In this case there are four distinct items in the set in the first parameter, but the function returns three because only Australia, Canada and France have data for July 1st 2001 for Internet Sales Amount.
 
## See Also  
 [Count &#40;Set&#41; &#40;MDX&#41;](../mdx/count-set-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
