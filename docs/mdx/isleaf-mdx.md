---
title: "IsLeaf (MDX) | Microsoft Docs"
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
  - "ISLEAF"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "IsLeaf function"
ms.assetid: 54862bb3-acc2-4711-ac5a-faa9e9de3721
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# IsLeaf (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
 `WITH MEMBER MEASURES.ISLEAFDEMO AS`  
  
 `IsLeaf([Date].[Fiscal].CURRENTMEMBER)`  
  
 `SELECT {MEASURES.ISLEAFDEMO} ON 0,`  
  
 `[Date].[Fiscal].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
