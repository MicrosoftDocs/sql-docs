---
title: "Mtd (MDX) | Microsoft Docs"
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
  - "MTD"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Mtd function"
ms.assetid: 07d8fd65-f9e6-42d4-868d-fccfac6bdb70
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Mtd (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the Year level in the Time dimension.  
  
## Syntax  
  
```  
  
Mtd( [ Member_Expression ] )  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 If a member expression is not specified, the default is the current member of the first hierarchy with a level of type *Months* in the first dimension of type *Time* in the measure group.  
  
 The **Mtd** function is a shortcut function for the [PeriodsToDate](../mdx/periodstodate-mdx.md) function when the Type property of the attribute hierarchy on which a level is based is set to *Months*. That is, `Mtd(Member_Expression)` is equivalent to `PeriodsToDate(Month_Level_Expression,Member_Expression)`.  
  
## Example  
 The following example returns the sum of the month to date freight costs for Internet sales for the month of July, 2002 through the 20th day of July.  
  
```  
WITH MEMBER Measures.x AS SUM   
   (  
      MTD([Date].[Calendar].[Date].[July 20, 2002])  
     , [Measures].[Internet Freight Cost]  
     )  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [Sum &#40;MDX&#41;](../mdx/sum-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
