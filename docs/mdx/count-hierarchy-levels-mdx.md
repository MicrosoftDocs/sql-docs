---
title: "Count (Hierarchy Levels) (MDX)"
description: "Count (Hierarchy Levels) (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Count (Hierarchy Levels) (MDX)


  Returns the number of levels in hierarchy.  
  
## Syntax  
  
```  
  
Hierarchy_Expression.Levels.Count  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
## Remarks  
 Returns the number of levels in a hierarchy, including the `[All]` level, if applicable.  
  
> [!IMPORTANT]  
>  When a dimension contains only a single visible hierarchy, the hierarchy can be referred to either by the dimension name or by the hierarchy name, because the dimension name is resolved to its only visible hierarchy. For example, `Measures.Levels.Count` is a valid MDX expression because it resolves to the only hierarchy in the Measures dimension.  
  
## Example  
 The following example returns a count of the number of levels in the Product Categories user-defined hierarchy in the Adventure Works cube.  
  
```  
WITH MEMBER measures.X AS  
   [Product].[Product Categories].Levels.Count   
Select Measures.X ON 0  
FROM [Adventure Works]  
```  
  
## Related content

- [Count (Dimension) (MDX)](count-dimension-mdx.md)
- [Count (Tuple) (MDX)](count-tuple-mdx.md)
- [Count (Set) (MDX)](count-set-mdx.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
