---
title: "Set Operators | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Set Operators


  In Multidimensional Expressions (MDX), set operators perform operations on members or sets, and return a set. You often use set operators as an alternate to several set functions in MDX expressions.  
  
 MDX supports the set operators listed in the following table.  
  
|Operator|Description|  
|--------------|-----------------|  
|[- (Except)](../mdx/except-mdx-operator.md)|Returns the difference between two sets, removing duplicate members.<br /><br /> This operator is functionally equivalent to the [Except](../mdx/except-mdx-function.md) function.|  
|[* (Crossjoin)](../mdx/crossjoin-mdx-operator-reference.md)|Returns the cross product of two sets.<br /><br /> This operator is functionally equivalent to the [Crossjoin](../mdx/crossjoin-mdx.md) function.|  
|[: (Range)](../mdx/range-mdx.md)|Returns a naturally ordered set, with the two specified members as endpoints and all members between the two specified members included as members of the set.|  
|[+ (Union)](../mdx/union-mdx-operator-reference.md)|Returns a union of two sets, excluding duplicate members.<br /><br /> This operator is functionally equivalent to the [Union  &#40;MDX&#41;](../mdx/union-mdx.md) function.|  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)   
 [Operators &#40;MDX Syntax&#41;](../mdx/operators-mdx-syntax.md)  
  
  
