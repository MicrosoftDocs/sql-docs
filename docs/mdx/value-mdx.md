---
title: "Value (MDX) | Microsoft Docs"
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
# Value (MDX)


  Returns the value of the current member of the Measures dimension that intersects with the current member of the attribute hierarchies in the context of the query.  
  
## Syntax  
  
```  
  
Member_Expression[.Value]   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **Value** function returns the value of the specified member as a string. The **Value** argument is optional because the value of a member is the default property of a member, and is value that is returned for a member if no other value is specified. For more information about properties of members, see [Intrinsic Member Properties &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-member-properties-intrinsic-member-properties.md) and [User-Defined Member Properties &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-member-properties-user-defined-member-properties.md).  
  
## Examples  
 The following example returns the value of a member as well explicitly returning the name of the member.  
  
```  
WITH MEMBER [Date].[Calendar].NumericValue as [Date].[Calendar].[July 1, 2001].Value  
MEMBER [Date].[Calendar].MemberName AS [Date].[Calendar].[July 1, 2001].Name  
  
SELECT {[Date].[Calendar].NumericValue, [Date].[Calendar].MemberName} ON 0  
from [Adventure Works]  
```  
  
 The following example returns the value of a member, as the default value that is returned for a member on an axis.  
  
```  
SELECT {[Date].[Calendar].[July 1, 2001]} ON 0  
from [Adventure Works]  
```  
  
## See Also  
 [MemberValue &#40;MDX&#41;](../mdx/membervalue-mdx.md)   
 [Properties &#40;MDX&#41;](../mdx/properties-mdx.md)   
 [Name &#40;MDX&#41;](../mdx/name-mdx.md)   
 [UniqueName &#40;MDX&#41;](../mdx/uniquename-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
