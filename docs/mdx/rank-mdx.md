---
title: "Rank (MDX) | Microsoft Docs"
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
# Rank (MDX)


  Returns the one-based rank of a specified tuple in a specified set.  
  
## Syntax  
  
```  
  
Rank(Tuple_Expression, Set_Expression [ ,Numeric Expression ] )  
```  
  
## Arguments  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, the **Rank** function determines the one-based rank for the specified tuple by evaluating the specified numeric expression against the tuple. If a numeric expression is specified, the **Rank** function assigns the same rank to tuples with duplicate values in the set. This assignment of the same rank to duplicate values affects the ranks of subsequent tuples in the set. For example, a set consists of the following tuples, `{(a,b), (e,f), (c,d)}`. The tuple `(a,b)` has the same value as the tuple `(c,d)`. If the tuple `(a,b)` has a rank of 1, then both `(a,b)` and `(c,d)` would have a rank of 1. However, the tuple `(e,f)` would have a rank of 3. There could be no tuple in this set with a rank of 2.  
  
 If a numeric expression is not specified, the **Rank** function returns the one-based ordinal position of the specified tuple.  
  
 The **Rank** function does not order the set.  
  
## Example  
 The following example returns the set of tuples containing customers and purchase dates, by using the **Filter**, **NonEmpty**, **Item**, and **Rank** functions to find the last date that each customer made a purchase.  
  
```  
WITH SET MYROWS AS FILTER  
   (NONEMPTY  
      ([Customer].[Customer Geography].MEMBERS  
         * [Date].[Date].[Date].MEMBERS  
         , [Measures].[Internet Sales Amount]  
      ) AS MYSET  
   , NOT(MYSET.CURRENT.ITEM(0)  
      IS MYSET.ITEM(RANK(MYSET.CURRENT, MYSET)).ITEM(0))  
   )  
SELECT [Measures].[Internet Sales Amount] ON 0,  
MYROWS ON 1  
FROM [Adventure Works]  
```  
  
 The following example uses the **Order** function, rather than the **Rank** function, to rank the members of the City hierarchy based on the Reseller Sales Amount measure and then displays them in ranked order. By using the **Order** function to first order the set of members of the City hierarchy, the sorting is done only once and then followed by a linear scan before being presented in sorted order.  
  
```  
WITH   
SET OrderedCities AS Order  
   ([Geography].[City].[City].members  
   , [Measures].[Reseller Sales Amount], BDESC  
   )  
MEMBER [Measures].[City Rank] AS Rank  
   ([Geography].[City].CurrentMember, OrderedCities)  
SELECT {[Measures].[City Rank],[Measures].[Reseller Sales Amount]}  ON 0   
,Order  
   ([Geography].[City].[City].MEMBERS  
   ,[City Rank], ASC)  
    ON 1  
FROM [Adventure Works]  
```  
  
## See Also  
 [Order &#40;MDX&#41;](../mdx/order-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
