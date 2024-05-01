---
title: "Siblings (MDX)"
description: "Siblings (MDX)"
author: kfollis
ms.author: kfollis
ms.reviewer: kfollis
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
  
