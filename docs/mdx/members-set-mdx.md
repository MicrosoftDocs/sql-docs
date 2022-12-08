---
description: "Members (Set) (MDX)"
title: "Members (Set) (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Members (Set) (MDX)


  Returns the set of members in a dimension, level, or hierarchy.  
  
## Syntax  
  
```  
  
Hierarchy expression syntax  
Hierarchy_Expression.Members  
  
Level expression Syntax  
Level_Expression.Members  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
## Remarks  
 If a hierarchy expression is specified, the **Members (Set)** function returns the set of all members within the specified hierarchy, not including calculated members. To obtain the set of all members, calculated or otherwise, on a hierarchy use the [AllMembers &#40;MDX&#41;](../mdx/allmembers-mdx.md) function  
  
 If a level expression is specified, the **Members (Set)** function returns the set of all members within the specified level.  
  
> [!IMPORTANT]  
>  When a dimension contains only a single visible hierarchy, the hierarchy can be either referred to either by the dimension name or by the hierarchy name, because the dimension name in this scenario is resolved to its only visible hierarchy. For example, Measures.Members is a valid MDX expression because it resolves to the only hierarchy in the Measures dimension.  
  
## Examples  
 The following example returns the set of all members of the Calendar Year hierarchy in the Adventure Works cube.  
  
```  
SELECT   
   [Date].[Calendar].[Calendar Year].Members ON 0  
FROM  
   [Adventure Works]  
  
```  
  
 The following example returns the 2003 order quantities for each member in the `[Product].[Products].[Product Line]` level. The **Members** function returns a set that represents all of the members in the level.  
  
```  
SELECT   
   {Measures.[Order Quantity]} ON COLUMNS,  
   [Product].[Product Line].[Product Line].Members ON ROWS  
FROM  
   [Adventure Works]  
WHERE  
   {[Date].[Calendar Year].[Calendar Year].&[2003]}  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
