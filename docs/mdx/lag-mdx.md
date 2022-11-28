---
description: "Lag (MDX)"
title: "Lag (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Lag (MDX)


  Returns the member that is a specified number of positions before a specified member at the member's level.  
  
## Syntax  
  
```  
  
Member_Expression.Lag(Index)   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Index*  
 A valid numeric expression that specifies the number of member positions to lag.  
  
## Remarks  
 Member positions within a level are determined by the attribute hierarchy's natural order. The numbering of the positions is zero-based.  
  
 If the specified lag is zero, the **Lag** function returns the specified member itself.  
  
 If the specified lag is negative, the **Lag** function returns a subsequent member.  
  
 `Lag(1)` is equivalent to the [PrevMember](../mdx/prevmember-mdx.md) function. `Lag(-1)` is equivalent to the [NextMember](../mdx/nextmember-mdx.md) function.  
  
 The **Lag** function is similar to the [Lead](../mdx/lead-mdx.md) function, except that the **Lead** function looks in the opposite direction to the **Lag** function. That is, `Lag(n)` is equivalent to `Lead(-n)`.  
  
## Example  
 The following example returns the value for December 2001:  
  
```  
SELECT [Date].[Fiscal].[Month].[February 2002].Lag(2) ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the value for March 2002:  
  
```  
SELECT [Date].[Fiscal].[Month].[February 2002].Lag(-1) ON 0  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
