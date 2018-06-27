---
title: "Qtd (MDX) | Microsoft Docs"
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
# Qtd (MDX)


  Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the *Quarter* level in the Time dimension.  
  
## Syntax  
  
```  
  
Qtd( [ Member_Expression ] )  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 If a member expressionis not specified, the default is the current member of the first hierarchy with a level of type *Quarters* in the first dimension of type *Time* in the measure group.  
  
 The **Qtd** function is a shortcut function for the [PeriodsToDate &#40;MDX&#41;](../mdx/periodstodate-mdx.md) function whose level expression argument is set to *Quarter*. That is, `Qtd(Member_Expression)` is functionally equivalent to `PeriodsToDate(Quarter_Level_Expression, Member_Expression)`.  
  
## Example  
 The following example returns the sum of the `Measures.[Order Quantity]` member, aggregated over the first two months of the third quarter of calendar year 2003 that are contained in the `Date` dimension, from the **Adventure Works** cube.  
  
```  
WITH MEMBER [Date].[Calendar].[First2MonthsSecondSemester2003] AS  
    Aggregate(  
        QTD([Date].[Calendar].[Month].[August 2003])  
    )  
SELECT   
    [Date].[Calendar].[First2MonthsSecondSemester2003] ON COLUMNS,  
    [Product].[Category].Children ON ROWS  
FROM  
    [Adventure Works]  
WHERE  
    [Measures].[Order Quantity]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
