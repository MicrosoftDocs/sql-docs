---
description: "MemberValue (MDX)"
title: "MemberValue (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MemberValue (MDX)


  Returns the value of a member.  
  
## Syntax  
  
```  
  
Member_Expression.MemberValue  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that evaluates to a member.  
  
## Return Value  
 The member value returned contains the following information, listed in the order that this information appears in the return value:  
  
-   The value binding, if it has been defined.  
  
-   The key with the original data type if either there is no name binding, or the key and the caption are bound to the same column.  
  
-   The caption of the member.  
  
## Example  
 The following example returns the value binding, the member key, and the caption for the first date in the Date dimension in the Adventure Works cube.  
  
```  
WITH MEMBER Measures.ValueColumn as [Date].[Calendar].[July 1, 2001].MemberValue  
MEMBER Measures.KeyColumn as [Date].[Calendar].[July 1, 2001].Member_Key  
MEMBER Measures.NameColumn as [Date].[Calendar].[July 1, 2001].Member_Name  
  
SELECT {Measures.ValueColumn, Measures.KeyColumn, Measures.NameColumn}  ON 0  
from [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
