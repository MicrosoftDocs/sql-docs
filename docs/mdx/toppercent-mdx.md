---
title: "TopPercent (MDX) | Microsoft Docs"
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
# TopPercent (MDX)


  Sorts a set in descending order, and returns a set of tuples with the highest values whose cumulative total is equal to or greater than a specified percentage.  
  
## Syntax  
  
```  
  
TopPercent(Set_Expression, Percentage, Numeric_Expression)   
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Percentage*  
 A valid numeric expression that specifies the percentage of tuples to be returned.  
  
> [!IMPORTANT]  
>  *Percentage*  needs to be a positive value; negative values generate an error.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **TopPercent** function calculates the sum of the specified numeric expression evaluated over the specified set, sorting the set in descending order. The function then returns the elements with the highest values whose cumulative percentage of the total summed value is at least the specified percentage. This function returns the smallest subset of a set whose cumulative total is at least the specified percentage. The returned elements are ordered largest to smallest.  
  
> [!WARNING]  
>  If *Numeric_Expression*  returns any negative value then **TopPercent** returns only one (1) row.  
>   
>  See the second example for a detailed presentation of this behavior.  
  
> [!IMPORTANT]  
>  Like the [BottomPercent](../mdx/bottompercent-mdx.md) function, the **TopPercent** function always breaks the hierarchy.  
  
## Example  
 The following example returns the best cities that help make the top 10% of the resellers' sales, for the Bike category. The result is sorted in descending order, beginning with the city that has the highest value of sales.  
  
```  
SELECT [Measures].[Reseller Sales Amount] ON 0,  
TopPercent  
   ({[Geography].[Geography].[City].Members}  
   , 10  
   , [Measures].[Reseller Sales Amount]  
   ) ON 1  
FROM [Adventure Works]  
WHERE([Product].[Product Categories].[Bikes])  
```  
  
 The above expression produces the following results:  
  
||Reseller Sales Amount|  
|-|---------------------------|  
|Toronto|$3,508,904.84|  
|London|$1,521,530.09|  
|Seattle|$1,209,418.16|  
|Paris|$1,170,425.18|  
  
 The original set of data can be obtained with the following query and returns 588 rows:  
  
```  
SELECT [Measures].[Reseller Sales Amount] ON 0,  
Order  
   ({[Geography].[Geography].[City].Members}  
   , [Measures].[Reseller Sales Amount]  
   , BDESC  
   ) ON 1  
FROM [Adventure Works]  
WHERE([Product].[Product Categories].[Bikes])  
  
```  
  
## Example  
 The following walkthrough will help understand the effect of negative values in the *Numeric_Expression*. First let's build some context where we can present the behavior.  
  
 The following query returns a table of Resellers 'Sales Amount', 'Total Product Cost' and 'Gross Profit', sorted in descending order of profit. Please note there are only negative values for profit; so, the smallest loss appears at the top.  
  
```  
SELECT { [Measures].[Reseller Sales Amount], [Measures].[Reseller Total Product Cost], [Measures].[Reseller Gross Profit] } ON columns  
     ,  ORDER( [Product].[Product Categories].[Bikes].[Touring Bikes].children, [Measures].[Reseller Gross Profit], BDESC )   ON rows  
FROM [Adventure Works]  
  
```  
  
 The above query returns the following results; rows from the middle section were removed for readability.  
  
||Reseller Sales Amount|Reseller Total Product Cost|Reseller Gross Profit|  
|-|---------------------------|---------------------------------|---------------------------|  
|Touring-2000 Blue, 50|$157,444.56|$163,112.57|($5,668.01)|  
|Touring-2000 Blue, 46|$321,027.03|$333,021.50|($11,994.47)|  
|Touring-3000 Blue, 62|$87,773.61|$100,133.52|($12,359.91)|  
|...|...|...|...|  
|Touring-1000 Yellow, 46|$1,016,312.83|$1,234,454.27|($218,141.44)|  
|Touring-1000 Yellow, 60|$1,184,363.30|$1,443,407.51|($259,044.21)|  
  
 Now, if you were asked to present the top 100% bikes by profit you would write a query like:  
  
```  
SELECT { [Measures].[Reseller Sales Amount], [Measures].[Reseller Total Product Cost], [Measures].[Reseller Gross Profit] } ON columns  
     ,  TOPPERCENT( [Product].[Product Categories].[Bikes].[Touring Bikes].children, 100,[Measures].[Reseller Gross Profit] )   ON rows  
FROM [Adventure Works]  
  
```  
  
 Please note that the query asks for one hundred percent (100%); that means all rows should be returned. However, because there are negative values in the *Numeric_Expression* , only one row is returned.  
  
||Reseller Sales Amount|Reseller Total Product Cost|Reseller Gross Profit|  
|-|---------------------------|---------------------------------|---------------------------|  
|Touring-2000 Blue, 50|$157,444.56|$163,112.57|($5,668.01)|  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
