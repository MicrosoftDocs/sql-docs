---
title: "Using Dimension, Hierarchy, and Level Functions | Microsoft Docs"
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
  - "dimensions [Analysis Services], functions"
  - "level functions [MDX]"
  - "hierarchy functions [MDX]"
ms.assetid: e730f65a-1798-4094-9acf-2739e2505d52
caps.latest.revision: 25
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Using Dimension, Hierarchy, and Level Functions
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Dimension, hierarchy, and level functions are useful for traversing the multidimensional structures found in Analysis Services. Typically, you use such functions in conjunction with other functions to obtain information about the members of a dimension, hierarchy, or level.  
  
 The following example shows how to use the **.Dimension**, **.Hierarchy**, and **.Level** functions:  
  
 `WITH`  
  
 `MEMBER MEASURES.DIMENSIONNAME AS [Date].[Calendar].CURRENTMEMBER.DIMENSION.NAME`  
  
 `MEMBER MEASURES.HIERARCHYNAME AS [Date].[Calendar].CURRENTMEMBER.HIERARCHY.NAME`  
  
 `MEMBER MEASURES.LEVELNAME AS [Date].[Calendar].LEVEL.NAME`  
  
 `SELECT`  
  
 `{MEASURES.DIMENSIONNAME, MEASURES.HIERARCHYNAME, MEASURES.LEVELNAME}`  
  
 `ON Columns,`  
  
 `[Date].[Calendar].MEMBERS`  
  
 `ON Rows`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [Dimension &#40;MDX&#41;](../mdx/dimension-mdx.md)   
 [Functions &#40;MDX Syntax&#41;](../mdx/functions-mdx-syntax.md)   
 [Hierarchy &#40;MDX&#41;](../mdx/hierarchy-mdx.md)   
 [Level &#40;MDX&#41;](../mdx/level-mdx.md)  
  
  
