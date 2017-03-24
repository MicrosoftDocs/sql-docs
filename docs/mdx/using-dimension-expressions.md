---
title: "Using Dimension Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "expressions [MDX], dimensions"
  - "dimensions [Analysis Services], MDX"
ms.assetid: 1d40cffb-e88f-4284-93cf-d62ab4f08395
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Using Dimension Expressions
[!INCLUDE[tsql-appliesto-ss2008-all_md](../includes/tsql-appliesto-ss2008-all-md.md)]

  You typically use dimension and hierarchy expressions when passing parameters to functions in Multidimensional Expressions (MDX) to return members, sets, or tuples from a hierarchy.  
  
 Dimension expressions can only be simple expressions because they are object identifiers. See [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md) for an explanation of simple and complex expressions.  
  
## Dimension Expressions  
 A dimension expression either contains a dimension identifier or a dimension function.  
  
 Dimension expressions are rarely used on their own. Instead, you will usually want to specify a hierarchy on a dimension. The only exception is when you are working with the Measures dimension, which has no hierarchies.  
  
 The following example shows a calculated member that uses the expression [Measures] along with the .Members and Count() functions to return the number of members on the Measures dimension:  
  
 `WITH MEMBER [Measures].[MeasureCount] AS`  
  
 `COUNT([Measures].MEMBERS)`  
  
 `SELECT [Measures].[MeasureCount] ON 0`  
  
 `FROM [Adventure Works]`  
  
 A dimension identifier appears as *Dimension_Name* in the BNF notation used to describe MDX statements.  
  
## Hierarchy Expressions  
 Similarly, a hierarchy expression contains either a hierarchy identifier or a hierarchy function. The following example shows the use of the hierarchy expression [Date].[Calendar], along with the .Levels and .Count functions, to return the number of levels in the Calendar hierarchy of the Date dimension:  
  
 `WITH MEMBER [Measures].[CalendarLevelCount] AS`  
  
 `[Date].[Calendar].Levels.Count`  
  
 `SELECT [Measures].[CalendarLevelCount] ON 0`  
  
 `FROM [Adventure Works]`  
  
 The most common scenario where hierarchy expressions are used is in conjunction with the .Members function, to return all the members on a hierarchy. The following example returns all the members of [Date].[Calendar] on the rows axis:  
  
 `SELECT [Measures].[Internet Sales Amount] ON 0,`  
  
 `[Date].[Calendar].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
 A hierarchy identifier appears as *Dimension_Name**.**Hierarchy_Name* in the BNF notation used to describe MDX statements.  
  
## See Also  
 [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)  
  
  
