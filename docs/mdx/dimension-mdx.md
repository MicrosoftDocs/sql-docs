---
title: "Dimension (MDX)"
description: "Dimension (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Dimension (MDX)


  Returns the hierarchy that contains a specified member, level, or hierarchy.  
  
## Syntax  
  
```  
  
Hierarchy syntax  
Hierarchy_Expression.Dimension  
  
Level syntax  
Level_Expression.Dimension  
  
Member syntax  
Member_Expression.Dimension  
  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Examples  
 The following example uses the **Dimension** function, in conjunction with the **Name** function, to return the hierarchy name of the specified member.  
  
```  
WITH member measures.x as [Product].[Product Model Lines].[Model].&[HL Road Tire].Dimension.Name  
SELECT measures.x on 0  
FROM [Adventure Works]  
```  
  
 The following example uses the Dimension function, in conjunction with the Levels and the Count functions, to return the number of levels in the hierarchy containing the specified member.  
  
```  
WITH member measures.x as [Product].[Product Model Lines].[Model].&[HL Road Tire].Dimension.Levels.Count  
SELECT measures.x on 0  
FROM [Adventure Works]  
```  
  
 The following example uses the **Dimension** function, in conjunction with the **Members** and the **Count** functions, to return the number of members in the hierarchy containing the specified member.  
  
```  
WITH member measures.x as [Product].[Product Model Lines].[Model].&[HL Road Tire].Dimension.Members.Count  
SELECT measures.x on 0  
FROM [Adventure Works]  
```  
  
## Related content

- [Count (Hierarchy Levels) (MDX)](count-hierarchy-levels-mdx.md)
- [Count (Set) (MDX)](count-set-mdx.md)
- [Levels (MDX)](levels-mdx.md)
- [Members (Set) (MDX)](members-set-mdx.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
