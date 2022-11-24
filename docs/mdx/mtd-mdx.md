---
description: "Mtd (MDX)"
title: "Mtd (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Mtd (MDX)


  Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the Year level in the Time dimension.  
  
## Syntax  
  
```  
  
Mtd( [ Member_Expression ] )  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 If a member expression is not specified, the default is the current member of the first hierarchy with a level of type *Months* in the first dimension of type *Time* in the measure group.  
  
 The **Mtd** function is a shortcut function for the [PeriodsToDate](../mdx/periodstodate-mdx.md) function when the Type property of the attribute hierarchy on which a level is based is set to *Months*. That is, `Mtd(Member_Expression)` is equivalent to `PeriodsToDate(Month_Level_Expression,Member_Expression)`.  
  
## Example  
 The following example returns the sum of the month to date freight costs for Internet sales for the month of July, 2002 through the 20th day of July.  
  
```  
WITH MEMBER Measures.x AS SUM   
   (  
      MTD([Date].[Calendar].[Date].[July 20, 2002])  
     , [Measures].[Internet Freight Cost]  
     )  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [Sum &#40;MDX&#41;](../mdx/sum-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
