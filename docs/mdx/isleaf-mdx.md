---
title: "IsLeaf (MDX)"
description: "IsLeaf (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# IsLeaf (MDX)


  Returns whether a specified member is a leaf member.  
  
## Syntax  
  
```  
  
IsLeaf(Member_Expression)   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **IsLeaf** function returns **true** if the specified member is a leaf member. Otherwise, the function returns **false**.  
  
## Example  
 The following example returns TRUE if [Date].[Fiscal].CurrentMember is a leaf member:  
  
```  
WITH MEMBER MEASURES.ISLEAFDEMO AS  
IsLeaf([Date].[Fiscal].CURRENTMEMBER)  
SELECT {MEASURES.ISLEAFDEMO} ON 0,  
[Date].[Fiscal].MEMBERS ON 1  
FROM [Adventure Works]  
```  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
