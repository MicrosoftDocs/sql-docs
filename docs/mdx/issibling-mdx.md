---
title: "IsSibling (MDX) | Microsoft Docs"
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
  - "ISSIBLING"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "IsSibling function"
ms.assetid: 12f302f0-141e-4ec0-ad5f-329aade17a4d
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# IsSibling (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
 `WITH MEMBER MEASURES.ISSIBLINGDEMO AS`  
  
 `IsSibling([Date].[Fiscal].CURRENTMEMBER, [Date].[Fiscal].[Month].&[2002]&[7])`  
  
 `SELECT {MEASURES.ISSIBLINGDEMO} ON 0,`  
  
 `[Date].[Fiscal].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
