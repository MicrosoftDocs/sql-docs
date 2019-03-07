---
title: "Specifying the Contents of a Slicer Axis (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "slicer axis"
  - "filtering data [MDX]"
ms.assetid: c56b0a70-cdec-427f-990e-425290344e7d
author: minewiskan
ms.author: owend
manager: craigg
---
# Specifying the Contents of a Slicer Axis (MDX)
  The slicer axis filters the data returned by the Multidimensional Expressions (MDX) SELECT statement, restricting the returned data so that only data intersecting with the specified members will be returned. It can be thought of as an invisible extra axis in a query. The slicer axis is defined in the WHERE clause of the SELECT statement in MDX.  
  
## Slicer Axis Syntax  
 To explicitly specify a slicer axis, you  using the `<SELECT slicer axis clause>` in MDX, as described in the following syntax:  
  
```  
<SELECT slicer axis clause> ::=  WHERE Set_Expression  
```  
  
 In the slicer axis syntax shown, `Set_Expression` can take either a tuple expression, which is treated as a set for the purposes of evaluating the clause, or a set expression. If a set expression is specified, MDX will try to evaluate the set, aggregating the result cells in every tuple along the set. In other words, MDX will try to use the [Aggregate](/sql/mdx/aggregate-mdx) function on the set, aggregating each measure by its associated aggregation function. Also, if the set expression cannot be expressed as a crossjoin of attribute hierarchy members, MDX treats cells that fall outside of the set expression for the slicer as null for evaluation purposes.  
  
> [!IMPORTANT]  
>  Unlike the WHERE clause in SQL, the WHERE clause of an MDX SELECT statement never directly filters what is returned on the Rows axis of a query. To filter what appears on the Rows or Columns axis of a query, you can use a variety of MDX functions, for example FILTER, NONEMPTY and TOPCOUNT.  
  
### Implicit Slicer Axis  
 If a member from a hierarchy within the cube is not explicitly included in a query axis, the default member from that hierarchy is implicitly included in the slicer axis. For more information about default members, see [Define a Default Member](../attribute-properties-define-a-default-member.md).  
  
## Examples  
 The following query does not include a WHERE clause, and returns the value of the Internet Sales Amount measure for all Calendar Years:  
  
```  
SELECT {[Measures].[Internet Sales Amount]} ON COLUMNS,  
[Date].[Calendar Year].MEMBERS ON ROWS  
FROM [Adventure Works]  
```  
  
 Adding a WHERE clause, as follows:  
  
```  
SELECT {[Measures].[Internet Sales Amount]} ON COLUMNS,  
[Date].[Calendar Year].MEMBERS ON ROWS  
FROM [Adventure Works]  
WHERE([Customer].[Customer Geography].[Country].&[United States])  
  
```  
  
 does not change what is returned on Rows or Columns in the query; it changes the values returned for each cell. In this example, the query is sliced so that it returns the value of Internet Sales Amount for all Calendar Years but only for Customers who live in the United States.Multiple members from different hierarchies can be added to the WHERE clause. The following query shows the value of Internet Sales Amount for all Calendar Years for Customers who live in the United States and who bought Products in the Category Bikes:  
  
```  
SELECT {[Measures].[Internet Sales Amount]} ON COLUMNS,  
[Date].[Calendar Year].MEMBERS ON ROWS  
FROM [Adventure Works]  
WHERE([Customer].[Customer Geography].[Country].&[United States], [Product].[Category].&[1])  
```  
  
 If you want to use multiple members from the same hierarchy, you need to include a set in the WHERE clause. For example, the following query shows the value of Internet Sales Amount for all Calendar Years for Customers who bought Products in the Category Bikes and live in either the United States or the United Kingdom:  
  
```  
SELECT {[Measures].[Internet Sales Amount]} ON COLUMNS,  
[Date].[Calendar Year].MEMBERS ON ROWS  
FROM [Adventure Works]  
WHERE(  
{[Customer].[Customer Geography].[Country].&[United States]  
, [Customer].[Customer Geography].[Country].&[United Kingdom]}  
, [Product].[Category].&[1])  
  
```  
  
 As mentioned above, using a set in the WHERE clause will implicitly aggregate values for all members in the set. In this case, the query shows aggregated values for the United States and the United Kingdom in each cell.  
  
  
