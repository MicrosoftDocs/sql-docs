---
title: "Specifying the Contents of a Query Axis (MDX) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Query and Slicer Axes - Specify the Contents of a Query Axis
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Query axes specify the edges of a cellset returned by a Multidimensional Expressions (MDX) SELECT statement. Specifying the edges of a cellset lets you restrict the returned data that is visible to the client.  
  
 To specify query axes, you use the `<SELECT query axis clause>` to assign a set to a particular query axis. Each `<SELECT query axis clause>` value defines one query axis. The number of axes in the dataset is equal to the number of `<SELECT query axis clause>` values in the SELECT statement.  
  
## Query Axis Syntax  
 The following syntax shows the syntax for the `<SELECT query axis clause>`:  
  
```  
  
<SELECT query axis clause> ::=  
   [ NON EMPTY ] Set_Expression [ <SELECT dimension property list clause> ] [<HAVING clause>]  
   ON {  
      Integer_Expression |   
      AXIS( Integer_Expression ) |   
      {COLUMNS | ROWS | PAGES | SECTIONS | CHAPTERS}     
      }  
  
```  
  
 Each query axis has a number: zero (0) for the x-axis, 1 for the y-axis, 2 for the z-axis, and so on. In the syntax for the `<SELECT query axis clause>`, the `Integer_Expression` value specifies the axis number. An MDX query can support up to 128 specified axes, but very few MDX queries will use more than 5 axes. For the first 5 axes, the aliases COLUMNS, ROWS, PAGES, SECTIONS, and CHAPTERS can instead be used.  
  
 An MDX query cannot skip query axes. That is, a query that includes one or more query axes must not exclude lower-numbered or intermediate axes. For example, a query cannot have a ROWS axis without a COLUMNS axis, or have COLUMNS and PAGES axes without a ROWS axis.  
  
 However, you can specify a SELECT clause without any axes (that is, an empty SELECT clause). In this case, all dimensions are slicer dimensions, and the MDX query selects one cell.  
  
 In the query axis syntax previously shown, each `Set_Expression` value specifies the set that defines the contents of the query axis. For more information about sets, see [Working with Members, Tuples, and Sets &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/working-with-members-tuples-and-sets-mdx.md).  
  
## Examples  
 The following simple SELECT statement returns the measure Internet Sales Amount on the Columns axis, and uses the MDX MEMBERS function to return all the members from the Calendar hierarchy on the Date dimension on the Rows axis:  
  
```  
SELECT {[Measures].[Internet Sales Amount]} ON COLUMNS,  
{[Date].[Calendar].MEMBERS} ON ROWS  
FROM [Adventure Works]  
  
```  
  
 The two following queries return exactly the same results but demonstrate the use of axis numbers rather than aliases:  
  
```  
SELECT {[Measures].[Internet Sales Amount]} ON 0,  
{[Date].[Calendar].MEMBERS} ON 1  
FROM [Adventure Works]  
  
SELECT {[Measures].[Internet Sales Amount]} ON AXIS(0),  
{[Date].[Calendar].MEMBERS} ON AXIS(1)  
FROM [Adventure Works]  
  
```  
  
 The NON EMPTY keyword, used before the set definition, is an easy way to remove all empty tuples from an axis. For example, in the examples we've seen so far there is no data in the cube from August 2004 onwards. To remove all rows from the cellset that have no data in any column, simply add NON EMPTY before the set on the Rows axis definition as follows:  
  
```  
SELECT {[Measures].[Internet Sales Amount]} ON COLUMNS,  
NON EMPTY  
{[Date].[Calendar].MEMBERS} ON ROWS  
FROM [Adventure Works]  
  
```  
  
 NON EMPTY can be used on all axes in a query. Compare the results of the following two queries, the first of which does not use NON EMPTY and the second of which does on both axes:  
  
```  
SELECT {[Measures].[Internet Sales Amount]}   
* [Promotion].[Promotion].[Promotion].MEMBERS  
ON COLUMNS,  
{[Date].[Calendar].[Calendar Year].MEMBERS} ON ROWS  
FROM [Adventure Works]  
WHERE([Product].[Subcategory].&[19])  
  
SELECT NON EMPTY {[Measures].[Internet Sales Amount]}   
* [Promotion].[Promotion].[Promotion].MEMBERS  
ON COLUMNS,  
NON EMPTY  
{[Date].[Calendar].[Calendar Year].MEMBERS} ON ROWS  
FROM [Adventure Works]  
WHERE([Product].[Subcategory].&[19])  
  
```  
  
 The HAVING clause can be used to filter the contents of an axis based on a specific criteria; it is less flexible than other methods that can achieve the same results, such as the FILTER function, but it is simpler to use. Here is an example that returns only those dates where Internet Sales Amount is greater than $15,000:  
  
```  
SELECT {[Measures].[Internet Sales Amount]}   
ON COLUMNS,  
NON EMPTY  
{[Date].[Calendar].[Date].MEMBERS}   
HAVING [Measures].[Internet Sales Amount]>15000  
ON ROWS  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [Specifying the Contents of a Slicer Axis &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-and-slicer-axes-specify-the-contents-of-a-slicer-axis.md)  
  
  
