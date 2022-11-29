---
description: "MemberToStr (MDX)"
title: "MemberToStr (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MemberToStr (MDX)


  Returns a Multidimensional Expressions (MDX)-formatted string that corresponds to a specified member.  
  
## Syntax  
  
```  
  
MemberToStr(Member_Expression)   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 This function returns a string containing the uniquename of a member. It is usually used to pass a member's uniquename to an external function.  
  
## Example  
 The following example returns the string [Geography].[Geography].[Country].&[United States] :  
  
 `WITH MEMBER Measures.x AS MemberToStr`  
  
 `([Geography].[Geography].[Country].[United States])`  
  
 `SELECT Measures.x ON 0`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
