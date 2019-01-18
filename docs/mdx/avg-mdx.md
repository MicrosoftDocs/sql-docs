---
title: "Avg (MDX) | Microsoft Docs"
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
# Avg (MDX)


  Evaluates a set and returns the average of the non empty values of the cells in the set, averaged over the measures in the set or over a specified measure.  
  
## Syntax  
  
```  
  
Avg( Set_Expression [ , Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a set of empty tuples or an empty set is specified, the **Avg** function returns an empty value.  
  
 The **Avg** function calculates the average of the nonempty values of cells in the specified set by first calculating the sum of values across cells in the specified set, and then dividing the calculated sum by the count of nonempty cells in the specified set.  
  
> [!NOTE]  
>  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] ignores nulls when calculating the average value in a set of numbers.  
  
 If a specific numeric expression (typically a measure) is not specified, the **Avg** function averages each measure within the current query context. If a specific measure is provided, the **Avg** function first evaluates the measure over the set, and then the function calculates the average based on the specified measure.  
  
> [!NOTE]  
>  When using the **CurrentMember** function in a calculated member statement, you must specify a numeric expression because no default measure exists for the current coordinate in such a query context.  
  
 To force the inclusion of empty cells, the application must use the [CoalesceEmpty](../mdx/coalesceempty-mdx.md) function or specify a valid *Numeric_Expression* that supplies a value of zero (0) for empty values. For more information about empty cells, see the OLE DB documentation.  
  
## Examples  
 The following example returns the average for a measure over a specified set. Notice that the specified measure can be either the default measure for the members of the specified set or a specified measure.  
  
 `WITH SET [NW Region] AS`  
  
 `{[Geography].[State-Province].[Washington]`  
  
 `, [Geography].[State-Province].[Oregon]`  
  
 `, [Geography].[State-Province].[Idaho]}`  
  
 `MEMBER [Geography].[Geography].[NW Region Avg] AS`  
  
 `AVG ([NW Region]`  
  
 `--Uncomment the line below to get an average by Reseller Gross Profit Margin`  
  
 `--otherwise the average will be by whatever the default measure is in the cube,`  
  
 `--or whatever measure is specified in the query`  
  
 `--, [Measures].[Reseller Gross Profit Margin]`  
  
 `)`  
  
 `SELECT [Date].[Calendar Year].[Calendar Year].Members ON 0`  
  
 `FROM [Adventure Works]`  
  
 `WHERE ([Geography].[Geography].[NW Region Avg])`  
  
 The following example returns the daily average of the `Measures.[Gross Profit Margin]` measure, calculated across the days of each month in the 2003 fiscal year, from the **Adventure Works** cube. The **Avg** function calculates the average from the set of days that are contained in each month of the `[Ship Date].[Fiscal Time]` hierarchy. The first version of the calculation shows the default behavior of Avg in excluding days that did not record any sales from the average, the second version shows how to include days with no sales in the average.  
  
 `WITH MEMBER Measures.[Avg Gross Profit Margin] AS`  
  
 `Avg(`  
  
 `Descendants(`  
  
 `[Ship Date].[Fiscal].CurrentMember,`  
  
 `[Ship Date].[Fiscal].[Date]`  
  
 `),`  
  
 `Measures.[Gross Profit Margin]`  
  
 `), format_String='percent'`  
  
 `MEMBER Measures.[Avg Gross Profit Margin Including Empty Days] AS`  
  
 `Avg(`  
  
 `Descendants(`  
  
 `[Ship Date].[Fiscal].CurrentMember,`  
  
 `[Ship Date].[Fiscal].[Date]`  
  
 `),`  
  
 `CoalesceEmpty(Measures.[Gross Profit Margin],0)`  
  
 `), Format_String='percent'`  
  
 `SELECT`  
  
 `{Measures.[Avg Gross Profit Margin],Measures.[Avg Gross Profit Margin Including Empty Days]} ON COLUMNS,`  
  
 `[Ship Date].[Fiscal].[Fiscal Year].Members ON ROWS`  
  
 `FROM`  
  
 `[Adventure Works]`  
  
 `WHERE([Product].[Product Categories].[Product].&[344])`  
  
 The following example returns the daily average of the `Measures.[Gross Profit Margin]` measure, calculated across the days of each semester in the 2003 fiscal year, from the **Adventure Works** cube.  
  
```  
WITH MEMBER Measures.[Avg Gross Profit Margin] AS  
   Avg(  
      Descendants(  
         [Ship Date].[Fiscal].CurrentMember,   
            [Ship Date].[Fiscal].[Date]  
      ),   
      Measures.[Gross Profit Margin]  
   )  
SELECT  
   Measures.[Avg Gross Profit Margin] ON COLUMNS,  
      [Ship Date].[Fiscal].[Fiscal Year].[FY 2003].Children ON ROWS  
FROM  
   [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
