---
title: "Ytd (MDX) | Microsoft Docs"
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
  - "YTD"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Ytd function"
ms.assetid: b77fdba2-d4a9-4271-8c21-c1f12eba526d
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Ytd (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the *Year* level in the Time dimension.  
  
## Syntax  
  
```  
  
Ytd( [ Member_Expression ] )  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 If a member expression is not specified, the default is the current member of the first hierarchy with a level of type *Years* in the first dimension of type *Time* in the measure group.  
  
 The **Ytd** function is a shortcut function for the [PeriodsToDate](../mdx/periodstodate-mdx.md) function where the Type property of the attribute hierarchy on which the level is based is set to *Years*. That is, `Ytd(Member_Expression)` is equivalent to `PeriodsToDate(Year_Level_Expression,Member_Expression)`. Note that this function will not work when the Type property is set to *FiscalYears*.  
  
## Example  
 The following example returns the sum of the `Measures.[Order Quantity]` member, aggregated over the first eight months of calendar year 2003 that are contained in the `Date` dimension, from the **Adventure Works** cube.  
  
```  
WITH MEMBER [Date].[Calendar].[First8MonthsCY2003] AS  
    Aggregate(  
        YTD([Date].[Calendar].[Month].[August 2003])  
    )  
SELECT   
    [Date].[Calendar].[First8MonthsCY2003] ON COLUMNS,  
    [Product].[Category].Children ON ROWS  
FROM  
    [Adventure Works]  
WHERE  
    [Measures].[Order Quantity]  
```  
  
 **Ytd** is frequently used in combination with no parameters specified, meaning that the [CurrentMember &#40;MDX&#41;](../mdx/currentmember-mdx.md) function will display a running cumulative year-to-date total in a report, as shown in the following query:  
  
 `WITH MEMBER MEASURES.YTDDEMO AS`  
  
 `AGGREGATE(YTD(), [Measures].[Internet Sales Amount])`  
  
 `SELECT {[Measures].[Internet Sales Amount], MEASURES.YTDDEMO} ON 0,`  
  
 `[Date].[Calendar].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
