---
title: "Siblings (MDX) | Microsoft Docs"
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
# Siblings (MDX)


  Returns the siblings of a specified member, including the member itself.  
  
## Syntax  
  
```  
  
Member_Expression.Siblings   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Example  
 The following example returns the default measure for the siblings of March of 2003, which are January 2003 and February 2003, and including March 2003.  
  
```  
SELECT [Date].[Calendar].[Month].[March 2003].Siblings ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
