---
description: "Lead (MDX)"
title: "Lead (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Lead (MDX)


  Returns the member that is a specified number of positions following a specified member along the member's level.  
  
## Syntax  
  
```  
  
Member_Expression.Lead( Index )  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Index*  
 A valid numeric expression that specifies a number of member positions.  
  
## Remarks  
 Member positions within a level are determined by the attribute hierarchy's natural order. The numbering of the positions is zero-based.  
  
 If the specified lead is zero (0), the **Lead** function returns the specified member.  
  
 If the specified lead is negative, the **Lead** function returns a prior member.  
  
 `Lead(1)` is equivalent to the [NextMember](../mdx/nextmember-mdx.md) function. `Lead(-1)` is equivalent to the [PrevMember](../mdx/prevmember-mdx.md) function.  
  
 The **Lead** function is similar to the [Lag](../mdx/lag-mdx.md) function, except that the **Lag** function looks in the opposite direction to the **Lead** function. That is, `Lead(n)` is equivalent to `Lag(-n)`.  
  
## Example  
 The following example returns the value for December 2001:  
  
```  
SELECT [Date].[Fiscal].[Month].[February 2002].Lead(-2) ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the value for March 2002:  
  
```  
SELECT [Date].[Fiscal].[Month].[February 2002].Lead(1) ON 0  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
