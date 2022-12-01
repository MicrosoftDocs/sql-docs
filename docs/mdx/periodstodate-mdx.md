---
description: "PeriodsToDate (MDX)"
title: "PeriodsToDate (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# PeriodsToDate (MDX)


  Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by a specified level in the Time dimension.  
  
## Syntax  
  
```  
  
PeriodsToDate( [ Level_Expression [ ,Member_Expression ] ] )  
```  
  
## Arguments  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 Within the scope of the specified level, the **PeriodsToDate** function returns the set of periods on the same level as the specified member, starting with the first period and ending with specified member.  
  
-   If a level is specified, the current member of the hierarchy is inferred *hierarchy*.**CurrentMember**, where *hierarchy*is the hierarchy of the specified level.  
  
-   If neither a level nor a member is specified, the level is the parent level of the current member of the first hierarchy on the first dimension of type Time  in the measure group.  
  
 `PeriodsToDate( Level_Expression, Member_Expression )` is functionally equivalent to the following MDX expression:  
  
 `TopCount(Descendants(Ancestor(Member_Expression, Level_Expression), Member_Expression.Level), 1):Member_Expression`  
  
## Examples  
 The following example returns the sum of the `Measures.[Order Quantity]` member, aggregated over the first eight months of calendar year 2003 that are contained in the `Date` dimension, from the **Adventure Works** cube.  
  
```  
WITH MEMBER [Date].[Calendar].[First8Months2003] AS  
    Aggregate(  
        PeriodsToDate(  
            [Date].[Calendar].[Calendar Year],   
            [Date].[Calendar].[Month].[August 2003]  
        )  
    )  
SELECT   
    [Date].[Calendar].[First8Months2003] ON COLUMNS,  
    [Product].[Category].Children ON ROWS  
FROM  
    [Adventure Works]  
WHERE  
    [Measures].[Order Quantity]  
```  
  
 The following example aggregates over the first two months of the second semester of calendar year 2003.  
  
```  
WITH MEMBER [Date].[Calendar].[First2MonthsSecondSemester2003] AS  
    Aggregate(  
        PeriodsToDate(  
            [Date].[Calendar].[Calendar Semester],   
            [Date].[Calendar].[Month].[August 2003]  
        )  
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
 [TopCount &#40;MDX&#41;](../mdx/topcount-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
