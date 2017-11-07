---
title: "Lead (MDX) | Microsoft Docs"
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
  - "LEAD"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Lead function"
ms.assetid: f3250092-7b98-40b5-8dca-77e3b50734a0
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Lead (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the member that is a specified number of positions following a specified member along the member's level.  
  
## Syntax  
  
```  
  
Member_Expression.Lead( Index )  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Index*  
 A valid numeric expression that specifies a number of member positions.  
  
## Remarks  
 Member positions within a level are determined by the attribute hierarchy's natural order. The numbering of the positions is zero-based.  
  
 If the specified lead is zero (0), the **Lead** function returns the specified member.  
  
 If the specified lead is negative, the **Lead** function returns a prior member.  
  
 `Lead(1)` is equivalent to the [NextMember](../mdx/nextmember-mdx.md) function. `Lead(-1)` is equivalent to the [PrevMember](../mdx/prevmember-mdx.md) function.  
  
 The **Lead** function is similar to the [Lag](../mdx/lag-mdx.md) function, except that the **Lag** function looks in the opposite direction to the **Lead** function. That is, `Lead(n)` is equivalent to `Lag(-n)`.  
  
## Example  
 The following example returns the value for December 2001:  
  
```  
SELECT [Date].[Fiscal].[Month].[February 2002].Lead(-2) ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the value for March 2002:  
  
```  
SELECT [Date].[Fiscal].[Month].[February 2002].Lead(1) ON 0  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
