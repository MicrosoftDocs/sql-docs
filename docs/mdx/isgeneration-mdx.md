---
description: "IsGeneration (MDX)"
title: "IsGeneration (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# IsGeneration (MDX)


  Returns whether a specified member is in a specified generation.  
  
## Syntax  
  
```  
  
IsGeneration(Member_Expression, Generation_Number)   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Generation_Number*  
 A valid numeric expression that specifies the generation against which the specified member is evaluated.  
  
## Remarks  
 The **IsGeneration** function returns **true** if the specified member is in the specified generation number. Otherwise, the function returns **false**. Also, if the specified member evaluates to an empty member, the **IsGeneration** function returns **false**.  
  
 For the purposes of generation indexing, leaf members are generation index 0. The generation index of nonleaf members is determined by first getting the highest generation index from the union of all child members for the specified member, then adding 1 to that index. Because of how the generation index of nonleaf members is determined, a specific nonleaf member could belong to more than one generation.  
  
## Example  
 The following example returns TRUE if [Date].[Fiscal].CurrentMember is part of the second generation:  
  
 `WITH MEMBER MEASURES.ISGENERATIONDEMO AS`  
  
 `IsGeneration([Date].[Fiscal].CURRENTMEMBER, 2)`  
  
 `SELECT {MEASURES.ISGENERATIONDEMO} ON 0,`  
  
 `[Date].[Fiscal].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
