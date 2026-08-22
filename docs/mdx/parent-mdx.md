---
title: "Parent (MDX)"
description: "Parent (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Parent (MDX)


  Returns the parent of a member.  
  
## Syntax  
  
```  
  
Member_Expression.Parent   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **Parent** function returns the parent member of the specified member.  
  
## Examples  
 The following examples return the parent of the July 1, 2001 member. The first example specifies this member in the context of the Date attribute hierarchy and returns the All Periods member.  
  
```  
SELECT [Date].[Date].[July 1, 2001].Parent ON 0  
FROM [Adventure Works]  
```  
  
 The following example specifies the July 1, 2001 member in the context of the Calendar hierarchy.  
  
```  
SELECT [Date].[Calendar].[July 1, 2001].Parent ON 0  
FROM [Adventure Works]  
```  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
