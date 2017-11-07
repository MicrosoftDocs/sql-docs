---
title: "Members (Set) (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "Members"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Members function"
ms.assetid: 0c4d5bb9-500b-47ce-b7fc-f5a10e2400e0
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Members (Set) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
