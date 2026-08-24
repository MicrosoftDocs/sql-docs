---
title: "Count (Tuple) (MDX)"
description: "Count (Tuple) (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Count (Tuple) (MDX)


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
  
## Related content

- [Count (Dimension) (MDX)](count-dimension-mdx.md)
- [Count (Hierarchy Levels) (MDX)](count-hierarchy-levels-mdx.md)
- [Count (Set) (MDX)](count-set-mdx.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
