---
title: "IsSibling (MDX)"
description: "IsSibling (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# IsSibling (MDX)


  Returns whether a specified member is a sibling of another specified member.  
  
## Syntax  
  
```  
  
IsSibling(Member_Expression1, Member_Expression2)   
```  
  
## Arguments  
 *Member_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Member_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **IsSibling** function returns **true** if the first specified member is a sibling of the second specified member. Otherwise, the function returns **false**.  
  
## Example  
 The following example returns TRUE if the current member on the Fiscal hierarchy of the Date dimension is a sibling of July 2002:  
  
```  
WITH MEMBER MEASURES.ISSIBLINGDEMO AS  
IsSibling([Date].[Fiscal].CURRENTMEMBER, [Date].[Fiscal].[Month].&[2002]&[7])  
SELECT {MEASURES.ISSIBLINGDEMO} ON 0,  
[Date].[Fiscal].MEMBERS ON 1  
FROM [Adventure Works]  
```  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
