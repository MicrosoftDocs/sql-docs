---
title: "NextMember (MDX) | Microsoft Docs"
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
# NextMember (MDX)


  Returns the next member in the level that contains a specified member.  
  
## Syntax  
  
```  
  
Member_Expression.NextMember   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **NextMember** function returns the next member, in the same level, that contains the specified member.  
  
## Example  
 The following example returns the August 2001 member as the next member to the July 2001 member.  
  
```  
SELECT [Date].[Calendar].[Month].[July 2001].NextMember ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
