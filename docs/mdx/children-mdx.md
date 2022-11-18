---
title: "Children (MDX)"
description: "Returns the set of children of a specified member."
author: minewiskan
ms.author: owend
ms.reviewer: owend, randolphwest
ms.date: 08/08/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Children (MDX)

Returns the set of children of a specified member.

## Syntax

```  
Member_Expression.Children
```

## Arguments

#### *Member_Expression*

A valid Multidimensional Expressions (MDX) expression that returns a member.

## Remarks

The **Children** function returns a naturally ordered set that contains the children of a specified member. If the specified member has no children, this function returns an empty set.

## Example

The following example returns the children of the United States member of the Geography hierarchy in the Geography dimension.

```
SELECT [Geography].[Geography].[Country].&[United States].Children ON 0
FROM [Adventure Works];
```

The following example returns all members in the **Measures** dimension on the column axis, this includes all calculated members, and the set of all children of the `[Product].[Model Name]` attribute hierarchy on the row axis from the **Adventure Works** cube.

```
SELECT
    Measures.AllMembers ON COLUMNS,
    [Product].[Model Name].Children ON ROWS
FROM
    [Adventure Works]  
```

## Next steps

- [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)
