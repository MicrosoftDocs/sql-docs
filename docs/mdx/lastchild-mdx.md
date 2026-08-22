---
title: "LastChild (MDX)"
description: "LastChild (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# LastChild (MDX)


  Returns the last child of a specified member.  
  
## Syntax  
  
```  
  
Member_Expression.LastChild   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Example  
 The following example returns the value for September 2001, which is the last child of the first fiscal quarter of fiscal year 2002.  
  
```  
SELECT [Date].[Fiscal].[Fiscal Quarter].[Q1 FY 2002].LastChild ON 0  
FROM [Adventure Works]  
```  
  
## Related content

- [FirstChild (MDX)](firstchild-mdx.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
