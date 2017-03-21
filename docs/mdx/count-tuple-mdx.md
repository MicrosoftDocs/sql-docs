---
title: "Count (Tuple) (MDX) | Microsoft Docs"
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
  - "COUNT"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Count function [MDX]"
ms.assetid: c8f4a570-54a8-4c00-ac4b-eef0ebdb353c
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Count (Tuple) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the number of dimensions in a tuple.  
  
## Syntax  
  
```  
  
Tuple_Expression.Count  
```  
  
## Arguments  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
## Remarks  
 Returns the number of dimensions in a tuple.  
  
## Example  
 The calculated measure in the following query returns the value 2, which is the number of hierarchies present in the tuple `([Measures].[Internet Sales Amount], [Date].[Calendar].[Calendar Year].&[2001])`:  
  
```  
WITH MEMBER MEASURES.COUNTTUPLE AS  
COUNT(([Measures].[Internet Sales Amount], [Date].[Calendar].[Calendar Year].&[2001]))  
SELECT MEASURES.COUNTTUPLE ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [Count &#40;Dimension&#41; &#40;MDX&#41;](../mdx/count-dimension-mdx.md)   
 [Count &#40;Hierarchy Levels&#41; &#40;MDX&#41;](../mdx/count-hierarchy-levels-mdx.md)   
 [Count &#40;Set&#41; &#40;MDX&#41;](../mdx/count-set-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
