---
title: "Count (Dimension) (MDX) | Microsoft Docs"
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
ms.assetid: 4b9c52f6-5bb0-401a-947c-e14134558b4a
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Count (Dimension) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the number of hierarchies in a cube.  
  
## Syntax  
  
```  
  
Dimensions.Count   
```  
  
## Remarks  
 Returns the number of hierarchies in a cube, including the `[Measures].[Measures]` hierarchy.  
  
## Example  
 The following example returns the number of hierarchies in the Adventure Works cube.  
  
```  
WITH MEMBER measures.X AS  
  dimensions.count   
SELECT Measures.X ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [Count &#40;Tuple&#41; &#40;MDX&#41;](../mdx/count-tuple-mdx.md)   
 [Count &#40;Hierarchy Levels&#41; &#40;MDX&#41;](../mdx/count-hierarchy-levels-mdx.md)   
 [Count &#40;Set&#41; &#40;MDX&#41;](../mdx/count-set-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
