---
title: "Filter (MDX) | Microsoft Docs"
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
# Filter (MDX)


  Returns the set that results from filtering a specified set based on a search condition.  
  
## Syntax  
  
```  
  
Filter(Set_Expression, Logical_Expression )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Logical_Expression*  
 A valid Multidimensional Expressions (MDX) logical expression that evaluates to true or false.  
  
## Remarks  
 The **Filter** function evaluates the specified logical expression against each tuple in the specified set. The function returns a set that consists of each tuple in the specified set where the logical expression evaluates to **true**. If no tuples evaluate to **true**, an empty set is returned.  
  
 The **Filter** function works in a fashion similar to that of the [IIf](../mdx/iif-mdx.md) function. The **IIf** function returns only one of two options based on the evaluation of an MDX logical expression, while the **Filter** function returns a set of tuples that meet the specified search condition. In effect, the **Filter** function executes `IIf(Logical_Expression, Set_Expression.Current, NULL)` on each tuple in the set, and returns the resulting set.  
  
## Examples  
 The following example shows the use of the Filter function on the Rows axis of a query, to return only the Dates where Internet Sales Amount is greater than $10000:  
  
 `SELECT [Measures].[Internet Sales Amount] ON 0,`  
  
 `FILTER(`  
  
 `[Date].[Date].[Date].MEMBERS`  
  
 `,  [Measures].[Internet Sales Amount]>10000)`  
  
 `ON 1`  
  
 `FROM`  
  
 `[Adventure Works]`  
  
 The Filter function can also be using inside calculated member definitions. The following example returns the sum of the `Measures.[Order Quantity]` member, aggregated over the first nine months of 2003 contained in the `Date` dimension, from the **Adventure Works** cube. The **PeriodsToDate** function defines the tuples in the set over which the **Aggregate** function operates. The **Filter** function limits those tuples being returned to those with lower values for the Reseller Sales Amount measure for the previous time period.  
  
```  
WITH MEMBER Measures.[Declining Reseller Sales] AS Count  
   (Filter  
      (Existing  
         (Reseller.Reseller.Reseller),   
            [Measures].[Reseller Sales Amount] <   
               ([Measures].[Reseller Sales Amount],[Date].Calendar.PrevMember)  
        )  
    )  
MEMBER [Geography].[State-Province].x AS Aggregate   
( {[Geography].[State-Province].&[WA]&[US],   
   [Geography].[State-Province].&[OR]&[US] }   
)  
SELECT NON EMPTY HIERARCHIZE   
   (AddCalculatedMembers   
      ({DrillDownLevel  
         ({[Product].[All Products]})}  
        )  
    ) DIMENSION PROPERTIES PARENT_UNIQUE_NAME ON COLUMNS   
FROM [Adventure Works]  
WHERE ([Geography].[State-Province].x,   
   [Date].[Calendar].[Calendar Quarter].&[2003]&[4],  
   [Measures].[Declining Reseller Sales])  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
