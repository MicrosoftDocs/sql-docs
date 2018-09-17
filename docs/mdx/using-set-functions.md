---
title: "Using Set Functions | Microsoft Docs"
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
# Using Set Functions


  A set function retrieves a set from a dimension, hierarchy, level, or by traversing the absolute and relative locations of members within these objects, constructing sets in a variety of ways.  
  
 Set functions, like member functions and tuple functions, are essential to negotiating the multidimensional structures found in Analysis Services. Set functions are also essential to obtaining results from Multidimensional Expressions (MDX) queries because set expressions define the axes of an MDX query.  
  
 One of the most common set functions is the [Members &#40;Set&#41; &#40;MDX&#41;](../mdx/members-set-mdx.md) function, which retrieves a set containing all of the members from a dimension, hierarchy, or level. The following is an example of its use within a query:  
  
 `SELECT`  
  
 `//Returns all of the members on the Measures dimension`  
  
 `[Measures].MEMBERS`  
  
 `ON Columns,`  
  
 `//Returns all of the members on the Calendar Year level of the Calendar Year Hierarchy`  
  
 `//on the Date dimension`  
  
 `[Date].[Calendar Year].[Calendar Year].MEMBERS`  
  
 `ON Rows`  
  
 `FROM [Adventure Works]`  
  
 Another commonly used function is the [Crossjoin &#40;MDX&#41;](../mdx/crossjoin-mdx.md) function. It returns a set of tuples representing the cartesian product of the sets passed into it as parameters. In practical terms, this function enables you to create 'nested' or 'crosstabbed' axes in queries:  
  
 `SELECT`  
  
 `//Returns all of the members on the Measures dimension`  
  
 `[Measures].MEMBERS`  
  
 `ON Columns,`  
  
 `//Returns a set containing every combination of all of the members`  
  
 `//on the Calendar Year level of the Calendar Year Hierarchy`  
  
 `//on the Date dimension and all of the members on the Category level`  
  
 `//of the Category hierarchy on the Product dimension`  
  
 `Crossjoin(`  
  
 `[Date].[Calendar Year].[Calendar Year].MEMBERS,`  
  
 `[Product].[Category].[Category].MEMBERS)`  
  
 `ON Rows`  
  
 `FROM [Adventure Works]`  
  
 The [Descendants &#40;MDX&#41;](../mdx/descendants-mdx.md) function is similar the **Children** function, but is more powerful. It returns the descendants of any member at one or more levels in a hierarchy:  
  
 SELECT  
  
 [Measures].[Internet Sales Amount]  
  
 ON Columns,  
  
 //Returns a set containing all of the Dates beneath Calendar Year  
  
 //2004 in the Calendar hierarchy of the Date dimension  
  
 DESCENDANTS(  
  
 [Date].[Calendar].[Calendar Year].&[2004]  
  
 , [Date].[Calendar].[Date])  
  
 ON Rows  
  
 FROM [Adventure Works]  
  
 The [Order &#40;MDX&#41;](../mdx/order-mdx.md) function enables you to order the contents of a set in ascending or descending order according to a particular numeric expression. The following query returns the same members on rows as the previous query, but now orders them by the Internet Sales Amount measure:  
  
 `SELECT`  
  
 `[Measures].[Internet Sales Amount]`  
  
 `ON Columns,`  
  
 `//Returns a set containing all of the Dates beneath Calendar Year`  
  
 `//2004 in the Calendar hierarchy of the Date dimension`  
  
 `//ordered by Internet Sales Amount`  
  
 `ORDER(`  
  
 `DESCENDANTS(`  
  
 `[Date].[Calendar].[Calendar Year].&[2004]`  
  
 `, [Date].[Calendar].[Date])`  
  
 `, [Measures].[Internet Sales Amount], BDESC)`  
  
 `ON Rows`  
  
 `FROM [Adventure Works]`  
  
 This query also illustrates how the set returned from one set function, Descendants, can be passed as a parameter to another set function, Order.  
  
 Filtering a set according to certain criteria is very useful when writing queries, and for this purpose you can use the [Filter &#40;MDX&#41;](../mdx/filter-mdx.md) function, as shown in the following example:  
  
 `SELECT`  
  
 `[Measures].[Internet Sales Amount]`  
  
 `ON Columns,`  
  
 `//Returns a set containing all of the Dates beneath Calendar Year`  
  
 `//2004 in the Calendar hierarchy of the Date dimension`  
  
 `//where Internet Sales Amount is greater than $70000`  
  
 `FILTER(`  
  
 `DESCENDANTS(`  
  
 `[Date].[Calendar].[Calendar Year].&[2004]`  
  
 `, [Date].[Calendar].[Date])`  
  
 `, [Measures].[Internet Sales Amount]>70000)`  
  
 `ON Rows`  
  
 `FROM [Adventure Works]`  
  
 Other, more sophisticated functions exist that allow you to filter a set in other ways. For example, the following query shows the [TopCount &#40;MDX&#41;](../mdx/topcount-mdx.md) function returns the top n items in a set:  
  
 `SELECT`  
  
 `[Measures].[Internet Sales Amount]`  
  
 `ON Columns,`  
  
 `//Returns a set containing the top 10 Dates beneath Calendar Year`  
  
 `//2004 in the Calendar hierarchy of the Date dimension by Internet Sales Amount`  
  
 `TOPCOUNT(`  
  
 `DESCENDANTS(`  
  
 `[Date].[Calendar].[Calendar Year].&[2004]`  
  
 `, [Date].[Calendar].[Date])`  
  
 `,10, [Measures].[Internet Sales Amount])`  
  
 `ON Rows`  
  
 `FROM [Adventure Works]`  
  
 Finally it is possible to perform a number of logical set operations using functions such as [Intersect &#40;MDX&#41;](../mdx/intersect-mdx.md), [Union  &#40;MDX&#41;](../mdx/union-mdx.md) and [Except &#40;MDX&#41;](../mdx/except-mdx-function.md) functions. The following query shows examples of the latter two functions:  
  
 `SELECT`  
  
 `//Returns a set containing the Measures Internet Sales Amount, Internet Tax Amount and`  
  
 `//Internet Total Product Cost`  
  
 `UNION(`  
  
 `{[Measures].[Internet Sales Amount], [Measures].[Internet Tax Amount]}`  
  
 `, {[Measures].[Internet Total Product Cost]}`  
  
 `)`  
  
 `ON Columns,`  
  
 `//Returns a set containing all of the Dates beneath Calendar Year`  
  
 `//2004 in the Calendar hierarchy of the Date dimension`  
  
 `//except the January 1st 2004`  
  
 `EXCEPT(`  
  
 `DESCENDANTS(`  
  
 `[Date].[Calendar].[Calendar Year].&[2004]`  
  
 `, [Date].[Calendar].[Date])`  
  
 `,{[Date].[Calendar].[Date].&[915]})`  
  
 `ON Rows`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [Functions &#40;MDX Syntax&#41;](../mdx/functions-mdx-syntax.md)   
 [Using Member Functions](../mdx/using-member-functions.md)   
 [Using Tuple Functions](../mdx/using-tuple-functions.md)  
  
  
