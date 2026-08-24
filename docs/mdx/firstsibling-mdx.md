---
title: "FirstSibling (MDX)"
description: "FirstSibling (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# FirstSibling (MDX)


  Returns the first child of the parent of a member.  
  
## Syntax  
  
```  
  
Member_Expression.FirstSibling   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Example  
 The following query returns the first sibling of fiscal year 2003 in the Fiscal hierarchy, which is Fiscal Year 2002.  
  
```  
SELECT [Date].[Fiscal].[Fiscal Year].&[2003].FirstSibling ON 0  
FROM [Adventure Works]  
```  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
