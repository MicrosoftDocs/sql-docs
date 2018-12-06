---
title: "Item (Member) (MDX) | Microsoft Docs"
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
# Item (Member) (MDX)


  Returns a member from a specified tuple.  
  
## Syntax  
  
```  
  
Tuple_Expression.Item( Index )  
```  
  
## Arguments  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
 *Index*  
 A valid numeric expression that specifies the specific member by position within the tuple to be returned.  
  
## Remarks  
 The **Item** function returns a member from the specified tuple. The function returns the member found at the zero-based position specified by *Index*.  
  
## Example  
 The following example returns the member `[2003]` - the first item in the tuple `[Date].[Calendar Year].&[2003], [Measures].[Internet Sales Amount] ).` - on columns :  
  
 `SELECT`  
  
 `{( [Date].[Calendar Year].&[2003], [Measures].[Internet Sales Amount] ).Item(0)} ON 0`  
  
 `,{[Measures].[Reseller Sales Amount]} ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
