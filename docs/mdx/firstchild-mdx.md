---
title: "FirstChild (MDX) | Microsoft Docs"
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
  - "FIRSTCHILD"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "FirstChild function"
ms.assetid: 3c19a169-7658-4b31-93a9-85f74225ba05
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# FirstChild (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the first child of a specified member.  
  
## Syntax  
  
```  
  
Member_Expression.FirstChild   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Example  
 The following query returns the first child of fiscal year 2003 in the Fiscal hierarchy, which is the first semester of Fiscal Year 2003.  
  
```  
SELECT [Date].[Fiscal].[Fiscal Year].&[2003].FirstChild ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [LastChild &#40;MDX&#41;](../mdx/lastchild-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
