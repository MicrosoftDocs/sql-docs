---
title: "NextMember (MDX)"
description: "NextMember (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
