---
description: "Root (MDX)"
title: "Root (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Root (MDX)


  Returns a tuple that consists of the **All** members from each attribute hierarchy within the current scope in a cube, dimension, or tuple. For more information about Scope, see [SCOPE Statement &#40;MDX&#41;](../mdx/mdx-scripting-scope.md).  
  
> [!NOTE]  
>  If an attribute hierarchy does not have an **All** member, the tuple contains the default member for that hierarchy.  
  
## Syntax  
  
```  
  
Cube syntax  
Root ()  
Dimension syntax  
Root( Dimension_Name )  
Tuple syntax  
Root( Tuple_Expression )  
```  
  
## Arguments  
 *Dimension_Name*  
 A valid string expression specifying a dimension name.  
  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
## Remarks  
 If neither a dimension name nor a tuple expression is specified, the **Root** function returns a tuple that contains the **All** member (or the default member if the **All** member does not exist) from each attribute hierarchy in the cube. The order of members in the tuple is based on the sequence in which the attribute hierarchies are defined within the cube.  
  
 If a dimension name is specified, the **Root** function returns a tuple that contains the **All** member (or the default member if the **All** member does not exist) from each attribute hierarchy in the specified dimension based on the context of the current member. The order of members in the tuple is based on the sequence in which the attribute hierarchies are defined within the dimension.  
  
> [!NOTE]  
>  If a hierarchy name is specified, the **Tuple** function will pick the dimension name from the hierarchy name specified.  
  
 If a tuple expression is specified, the **Root** function returns a tuple that contains the intersection of the specified tuple and the **All** members of all other dimension attributes not explicitly included in the specified tuple.  
  
## Examples  
 The following example returns the tuple containing the **All** member (or the default if the **All** member does not exist) from each hierarchy in the Adventure Works cube.  
  
```  
SELECT Root()ON 0  
FROM [Adventure Works]  
```  
  
 The following example returns the tuple containing the **All** member (or the default if the **All** member does not exist) from each hierarchy in the Date dimension in the Adventure Works cube and the value for the specified member of Measures dimension that intersects with these default members.  
  
```  
SELECT Root([Date]) ON 0  
FROM [Adventure Works]  
WHERE [Measures].[Order Count]  
```  
  
 The following example returns the tuple containing specified tuple member (July 1, 2001, along with the **All** member (or the default if the **All** member does not exist) from each non-specified hierarchy in the Date dimension Adventure Works cube and the value for the specified member of Measures dimension that intersects with these members.  
  
```  
SELECT Root([Date].[July 1, 2001]) ON 0  
FROM [Adventure Works]  
WHERE [Measures].[Order Count]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
