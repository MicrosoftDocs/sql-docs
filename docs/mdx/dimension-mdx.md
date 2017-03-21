---
title: "Dimension (MDX) | Microsoft Docs"
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
  - "Dimension"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Dimension function"
ms.assetid: 0e3ce539-1d34-45ca-8342-67796e11b730
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Dimension (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
## See Also  
 [Count &#40;Hierarchy Levels&#41; &#40;MDX&#41;](../mdx/count-hierarchy-levels-mdx.md)   
 [Count &#40;Set&#41; &#40;MDX&#41;](../mdx/count-set-mdx.md)   
 [Levels &#40;MDX&#41;](../mdx/levels-mdx.md)   
 [Members &#40;Set&#41; &#40;MDX&#41;](../mdx/members-set-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
