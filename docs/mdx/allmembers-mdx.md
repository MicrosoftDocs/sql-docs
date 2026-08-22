---
title: "AllMembers (MDX)"
description: "AllMembers (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# AllMembers (MDX)


  Evaluates either a hierarchy or a level expression and returns a set that contains all members of the specified hierarchy or level, which includes all calculated members in the hierarchy or level.  
  
## Syntax  
  
```  
  
Hierarchy syntax  
Hierarchy_Expression.AllMembers  
  
Level syntax  
Level_Expression.AllMembers  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
## Remarks  
 The **AllMembers** function returns a set that contains all members, which includes calculated members, in the specified hierarchy or level. The **AllMembers** function returns the calculated members even if the specified hierarchy or level contains no visible members.  
  
> [!IMPORTANT]  
>  When a dimension contains only a single visible hierarchy, the hierarchy can be either referred to by the dimension name or by the hierarchy name, because the dimension name in this case is resolved to its only visible hierarchy. For example, `Measures.AllMembers` is a valid MDX expression because it resolves to the only hierarchy in the Measures dimension.  
  
> [!NOTE]  
>  The **AllMembers** function is semantically similar to the [AddCalculatedMembers (MDX)](../mdx/addcalculatedmembers-mdx.md) function.  
  
## Examples  
 The following example returns all members in the [`Date].[Calendar Year]` attribute hierarchy on the column axis, this includes calculated members, and the set of all children of the `[Product].[Model Name]` attribute hierarchy on the row axis from the **Adventure Works** cube.  
  
```  
SELECT  
   [Date].[Calendar Year].AllMembers ON COLUMNS,  
   [Product].[Model Name].Children ON ROWS  
FROM  
   [Adventure Works]  
```  
  
 The following example returns all members in the **Measures** dimension on the column axis, this includes all calculated members, and the set of all children of the `[Product].[Model Name]` attribute hierarchy on the row axis from the **Adventure Works** cube.  
  
```  
SELECT  
    Measures.AllMembers ON COLUMNS,  
    [Product].[Model Name].Children ON ROWS  
FROM  
    [Adventure Works]  
```  
  
## Related content

- [AddCalculatedMembers (MDX)](addcalculatedmembers-mdx.md)
- [Children (MDX)](children-mdx.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
