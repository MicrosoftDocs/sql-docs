---
title: "Count (Dimension) (MDX)"
description: "Count (Dimension) (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [Count (Tuple) (MDX)](count-tuple-mdx.md)
- [Count (Hierarchy Levels) (MDX)](count-hierarchy-levels-mdx.md)
- [Count (Set) (MDX)](count-set-mdx.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
