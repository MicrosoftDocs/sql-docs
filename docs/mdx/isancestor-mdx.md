---
title: "IsAncestor (MDX)"
description: "IsAncestor (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
```  
WITH MEMBER MEASURES.ISANCESTORDEMO AS  
IsAncestor([Date].[Fiscal].CurrentMember, [Date].[Fiscal].[Month].&[2003]&[1])  
SELECT MEASURES.ISANCESTORDEMO ON 0,  
[Date].[Fiscal].MEMBERS ON 1  
FROM [Adventure Works]  
```  
  
## Related content

- [Ancestor (MDX)](ancestor-mdx.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
