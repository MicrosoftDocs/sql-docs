---
title: "FirstSibling (MDX) | Microsoft Docs"
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
  - "FIRSTSIBLING"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "FirstSibling function"
ms.assetid: ed2fbd36-6665-4445-a894-cbeca29584ab
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# FirstSibling (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the first child of the parent of a member.  
  
## Syntax  
  
```  
  
Member_Expression.FirstSibling   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Example  
 The following query returns the first sibling of fiscal year 2003 in the Fiscal hierarchy, which is Fiscal Year 2002.  
  
```  
SELECT [Date].[Fiscal].[Fiscal Year].&[2003].FirstSibling ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
