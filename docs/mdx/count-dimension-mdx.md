---
description: "Count (Dimension) (MDX)"
title: "Count (Dimension) (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Count (Dimension) (MDX)


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
  
  
