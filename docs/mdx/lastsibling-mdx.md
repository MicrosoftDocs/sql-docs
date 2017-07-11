---
title: "LastSibling (MDX) | Microsoft Docs"
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
  - "LASTSIBLING"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "LastSibling function"
ms.assetid: 05542812-4bdb-48bf-823e-065d105b1721
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# LastSibling (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the last child of the parent of a specified member.  
  
## Syntax  
  
```  
  
Member_Expression.LastSibling   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
### Example  
 The following example returns the default measure for the last day in July 2002.  
  
```  
SELECT [Date].[Fiscal].[Date].&[20020717].LastSibling   
   ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
