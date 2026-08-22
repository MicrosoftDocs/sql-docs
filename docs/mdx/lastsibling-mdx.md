---
title: "LastSibling (MDX)"
description: "LastSibling (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# LastSibling (MDX)


  Returns the last child of the parent of a specified member.  
  
## Syntax  
  
```  
  
Member_Expression.LastSibling   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Example  
 The following example returns the default measure for the last day in July 2002.  
  
```  
SELECT [Date].[Fiscal].[Date].&[20020717].LastSibling   
   ON 0  
FROM [Adventure Works]  
```  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
