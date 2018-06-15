---
title: "Aggregate (MDX) | Microsoft Docs"
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
# Aggregate (MDX)


  Returns a number that is calculated by aggregating over the cells returned by the set expression. If a numeric expression is not provided, this function aggregates each measure within the current query context by using the default aggregation operator that is specified for each measure. If a numeric expression is provided, this function first evaluates, and then sums, the numeric expression for each cell in the specified set.  
  
## Syntax  
  
```  
  
Aggregate(Set_Expression [ ,Numeric_Expression ])  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a set of empty tuples or an empty set is specified, this function returns an empty value.  
  
 The following table describes how the **Aggregate** function behaves with different aggregation functions.  
  
|Aggregation Operator|Result|  
|--------------------------|------------|  
|Sum|Returns the sum of values over the set.|  
|Count|Returns the count of values over the set.|  
|Max|Returns the maximum value over the set.|  
|Min|Returns the minimum value over the set.|  
|Semi-additive aggregation functions|Returns the calculation of semi-additive behavior over the set after projecting the shape to the time axis.|  
|Distinct Count|Aggregates across the fact data contributing to the subcube when the slicer axis includes a set.<br /><br /> Returns the distinct count for each member of the set. The result depends on the security on the cells being aggregated, and not on the security on the cells that are required for the computation. Cell security on the set generates an error; cell security below the granularity of the specified set is ignored. Calculations on the set generate an error. Calculations below granularity of the set are ignored. Distinct count over a set that includes a member and one or more of its children returns the distinct count across facts contributing to the child member.|  
|Attributes that cannot be aggregated|Returns the sum of the values.|  
|Mixed aggregation functions|Not supported, and raises an error.|  
|Unary operators|Not respected; values are aggregated by summing.|  
|Calculated measures|Solve order set to ensure calculated measure applies.|  
|Calculated members|Normal rules apply, that is, the last solve order takes precedence.|  
|Assignments|Assignments aggregate according to the measure aggregation function. If the measure aggregation function is distinct count, the assignment is summed.|  
  
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
  
 The following example returns the count of the resellers whose sales have declined over the previous time period, based on user-selected State-Province member values evaluated using the Aggregate function. The **Hierarchize** and **DrillDownLevel** functions are used to return values for declining sales for product categories in the Product dimension.  
  
```  
WITH MEMBER Measures.[Declining Reseller Sales] AS   
   Count(  
      Filter(  
         Existing(Reseller.Reseller.Reseller),   
            [Measures].[Reseller Sales Amount] < ([Measures].[Reseller Sales Amount],  
            [Date].Calendar.PrevMember)  
            )  
         )  
MEMBER [Geography].[State-Province].x AS   
   Aggregate (   
      {[Geography].[State-Province].&[WA]&[US],   
      [Geography].[State-Province].&[OR]&[US] }   
         )  
SELECT NON EMPTY Hierarchize (  
   AddCalculatedMembers (  
      {DrillDownLevel({[Product].[All Products]})}  
         )  
   )  
        DIMENSION PROPERTIES PARENT_UNIQUE_NAME ON COLUMNS   
FROM [Adventure Works]  
WHERE ([Geography].[State-Province].x,   
    [Date].[Calendar].[Calendar Quarter].&[2003]&[4],  
    [Measures].[Declining Reseller Sales])  
```  
  
## See Also  
 [PeriodsToDate &#40;MDX&#41;](../mdx/periodstodate-mdx.md)   
 [Children &#40;MDX&#41;](../mdx/children-mdx.md)   
 [Hierarchize &#40;MDX&#41;](../mdx/hierarchize-mdx.md)   
 [Count &#40;Set&#41; &#40;MDX&#41;](../mdx/count-set-mdx.md)   
 [Filter &#40;MDX&#41;](../mdx/filter-mdx.md)   
 [AddCalculatedMembers &#40;MDX&#41;](../mdx/addcalculatedmembers-mdx.md)   
 [DrilldownLevel &#40;MDX&#41;](../mdx/drilldownlevel-mdx.md)   
 [Properties &#40;MDX&#41;](../mdx/properties-mdx.md)   
 [PrevMember &#40;MDX&#41;](../mdx/prevmember-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
