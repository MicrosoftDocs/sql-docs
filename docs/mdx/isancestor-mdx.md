---
title: "IsAncestor (MDX) | Microsoft Docs"
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
# IsAncestor (MDX)


  Returns whether a specified member is an ancestor of another specified member.  
  
## Syntax  
  
```  
  
IsAncestor(Member_Expression1, Member_Expression2)   
```  
  
## Arguments  
 *Member_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Member_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **IsAncestor** function returns **true** if the first member specified is an ancestor of the second member specified. Otherwise, the function returns **false**.  
  
## Example  
 The following example returns **true** if [Date].[Fiscal].CurrentMember is an ancestor of January 2003:  
  
 `WITH MEMBER MEASURES.ISANCESTORDEMO AS`  
  
 `IsAncestor([Date].[Fiscal].CurrentMember, [Date].[Fiscal].[Month].&[2003]&[1])`  
  
 `SELECT MEASURES.ISANCESTORDEMO ON 0,`  
  
 `[Date].[Fiscal].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [Ancestor &#40;MDX&#41;](../mdx/ancestor-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
