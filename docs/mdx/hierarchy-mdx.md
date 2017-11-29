---
title: "Hierarchy (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "Hierarchy"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Hierarchy function"
ms.assetid: 5ddf354f-8cae-4e3a-8803-0055fa86bad1
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# Hierarchy (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the hierarchy that contains a specified member or level.  
  
## Syntax  
  
```  
  
Member expression syntax  
Member_Expression.Hierarchy  
  
Level expression syntax  
Level_Expression.Hierarchy  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
### Examples  
 The following example returns the name of the Calendar hierarchy in the Date dimension in the AdventureWorks cube.  
  
 `WITH`  
  
 `MEMBER Measures.HierarchyName as`  
  
 `[Date].[Calendar].Currentmember.Hierarchy.Name`  
  
 `SELECT {Measures.HierarchyName}  ON 0,`  
  
 `{[Date].[Calendar].[All Periods]} ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
