---
title: "Wtd (MDX) | Microsoft Docs"
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
# Wtd (MDX)


  Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the Week level in the Time dimension.  
  
## Syntax  
  
```  
  
Wtd( [ Member_Expression ] )  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 If a member expression is not specified, the default is the current member of the first hierarchy with a level of type Weeks in the first dimension of type Time (**Time.CurrentMember**) in the measure group.  
  
 The **Wtd** function is a shortcut function for the [PeriodsToDate](../mdx/periodstodate-mdx.md) function where the level is set to *Weeks*. That is, `Wtd(Member_Expression)` is equivalent to `PeriodsToDate(Week_Level_Expression,Member_Expression)`.  
  
## See Also  
 [Qtd &#40;MDX&#41;](../mdx/qtd-mdx.md)   
 [Mtd &#40;MDX&#41;](../mdx/mtd-mdx.md)   
 [Ytd &#40;MDX&#41;](../mdx/ytd-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
