---
title: "Using Dimension, Hierarchy, and Level Functions | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Using Dimension, Hierarchy, and Level Functions


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
  
  
