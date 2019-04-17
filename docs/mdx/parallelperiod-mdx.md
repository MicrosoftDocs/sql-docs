---
title: "ParallelPeriod (MDX) | Microsoft Docs"
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
# ParallelPeriod (MDX)


  Returns a member from a prior period in the same relative position as a specified member.  
  
## Syntax  
  
```  
  
ParallelPeriod( [ Level_Expression [ ,Index [ , Member_Expression ] ] ] )  
```  
  
## Arguments  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Index*  
 A valid numeric expression that specifies the number of parallel periods to lag.  
  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 Although similar to the [Cousin](../mdx/cousin-mdx.md) function, the **ParallelPeriod** function is more closely related to time series. The **ParallelPeriod** function takes the ancestor of the specified member at the specified level, finds the ancestor's sibling with the specified lag, and finally returns the parallel period of the specified member among the descendants of the sibling.  
  
 The **ParallelPeriod** function has the following defaults:  
  
-   If neither a level expression nor a member expression is specified, the default member value is the current member of the first hierarchy on the first dimension with a type of *Time* in the measure group.  
  
-   If a level expression is specified, but a member expression is not specified, the default member value is *Level_Expression*.**Hierarchy.CurrentMember**.  
  
-   The default index value is 1.  
  
-   The default level is the level of the parent of the specified member.  
  
 The **ParallelPeriod** function is equivalent to the following MDX statement:  
  
 `Cousin(Member_Expression, Ancestor(Member_Expression, Level_Expression) .Lag(Numeric_Expression))`  
  
## Example  
 The following example returns the parallel period for the month of October 2003 with a lag of three periods, based on the quarter level, which returns the month of January, 2003.  
  
```  
SELECT ParallelPeriod ([Date].[Calendar].[Calendar Quarter]  
   , 3  
   , [Date].[Calendar].[Month].[October 2003])  
   ON 0  
   FROM [Adventure Works]  
```  
  
 The following example returns the parallel period for the month of October 2003 with a lag of three periods, based on the semester level, which returns the month of April, 2002.  
  
```  
SELECT ParallelPeriod ([Date].[Calendar].[Calendar Semester]  
   , 3  
   , [Date].[Calendar].[Month].[October 2003])  
   ON 0  
   FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
