---
title: "The Basic MDX Query (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [MDX], SELECT statement"
  - "queries [MDX], about queries"
  - "cellsets [MDX]"
  - "SELECT statement [MDX]"
  - "cubes [Analysis Services], SELECT statement"
ms.assetid: 4fa5a95a-fec9-4584-875c-dbf030c53e82
author: minewiskan
ms.author: owend
manager: craigg
---
# The Basic MDX Query (MDX)
  The basic Multidimensional Expressions (MDX) query is the SELECT statement-the most frequently used query in MDX. By understanding how an MDX SELECT statement must specify a result set, what the syntax of the SELECT statement is, and how to create a simple query using the SELECT statement, you will have a solid understanding of how to use MDX to query multidimensional data.  
  
## Specifying a Result Set  
 In MDX, the SELECT statement specifies a result set that contains a subset of multidimensional data that has been returned from a cube. To specify a result set, an MDX query must contain the following information:  
  
-   The number of axes that you want the result set to contain. You can specify up to 128 axes in an MDX query.  
  
-   The set of members or tuples to include on each axis of the MDX query.  
  
-   The name of the cube that sets the context of the MDX query.  
  
-   The set of members or tuples to include on the slicer axis. For more information about slicer and query axes, see[Restricting the Query with Query and Slicer Axes &#40;MDX&#41;](mdx-query-and-slicer-axes-restricting-the-query.md).  
  
 To identify the query axes, the cube that will be queried, and the slicer axis, the MDX SELECT statement uses the following clauses:  
  
-   A SELECT clause that determines the query axes of an MDX SELECT statement. For more information about the construction of query axes in a SELECT clause, see [Specifying the Contents of a Query Axis &#40;MDX&#41;](mdx-query-and-slicer-axes-specify-the-contents-of-a-query-axis.md).  
  
-   A FROM clause that determines which cube will be queried. For more information about the FROM clause, see [SELECT Statement &#40;MDX&#41;](/sql/mdx/mdx-data-manipulation-select).  
  
-   An optional WHERE clause that determines which members or tuples to use on the slicer axis to restrict the data returned. For more information about the construction of a slicer axis in a WHERE clause, see [Specifying the Contents of a Slicer Axis &#40;MDX&#41;](mdx-query-and-slicer-axes-specify-the-contents-of-a-slicer-axis.md).  
  
> [!NOTE]  
>  For more detailed information about the various clauses of the SELECT statement, see [SELECT Statement &#40;MDX&#41;](/sql/mdx/mdx-data-manipulation-select).  
  
## SELECT Statement Syntax  
 The following syntax shows a basic SELECT statement that includes the use of the SELECT, FROM, and WHERE clauses:  
  
```  
[ WITH <SELECT WITH clause> [ , <SELECT WITH clause> ... ] ]   
SELECT [ * | ( <SELECT query axis clause>   
    [ , <SELECT query axis clause> ... ] ) ]  
FROM <SELECT subcube clause>   
[ <SELECT slicer axis clause> ]  
[ <SELECT cell property list clause> ]  
```  
  
 The MDX SELECT statement supports optional syntax, such as the WITH keyword, the use of MDX functions to create calculated members for inclusion in an axis or slicer axis, and the ability to return the values of specific cell properties as part of the query. For more information about the MDX SELECT statement, see [SELECT Statement &#40;MDX&#41;](/sql/mdx/mdx-data-manipulation-select).  
  
### Comparing the Syntax of the MDX SELECT Statement to SQL  
 The syntax format for the MDX SELECT statement is similar to that of SQL syntax. However, there are several fundamental differences:  
  
-   MDX syntax distinguishes sets by surrounding tuples or members with braces (the { and } characters.) For more information about member, tuple, and set syntax, see [Working with Members, Tuples, and Sets &#40;MDX&#41;](working-with-members-tuples-and-sets-mdx.md).  
  
-   MDX queries can have 0, 1, 2 or up to 128 query axes in the SELECT statement. Each axis behaves in exactly the same way, unlike SQL where there are significant differences between how the rows and the columns of a query behave.  
  
-   As with an SQL query, the FROM clause names the source of the data for the MDX query. However, the MDX FROM clause is restricted to a single cube. Information from other cubes can be retrieved on a value-by-value basis by using the LookupCube function.  
  
-   The WHERE clause describes the slicer axis in an MDX query. It acts as something like an invisible, extra axis in the query, slicing the values that appear in the cells in the result set; unlike the SQL WHERE clause it does not directly affect what appears on the rows axis of the query. The functionality of the SQL WHERE clause is available through other MDX functions such as the FILTER function.  
  
## SELECT Statement Example  
 The following example shows a basic MDX query that uses the SELECT statement. This query returns a result set that contains the 2002 and 2003 sales and tax amounts for the Southwest sales territories.  
  
```  
SELECT  
    { [Measures].[Sales Amount],   
        [Measures].[Tax Amount] } ON COLUMNS,  
    { [Date].[Fiscal].[Fiscal Year].&[2002],   
        [Date].[Fiscal].[Fiscal Year].&[2003] } ON ROWS  
FROM [Adventure Works]  
WHERE ( [Sales Territory].[Southwest] )  
```  
  
 In this example, the query defines the following result set information:  
  
-   The SELECT clause sets the query axes as the Sales Amount and Tax Amount members of the Measures dimension, and the 2002 and 2003 members of the Date dimension.  
  
-   The FROM clause indicates that the data source is the Adventure Works cube.  
  
-   The WHERE clause defines the slicer axis as the Southwest member of the Sales Territory dimension.  
  
 Notice that the query example also uses the COLUMNS and ROWS axis aliases. The ordinal positions for these axes could also have been used. The following example shows how the MDX query could have been written to use the ordinal position of each axis:  
  
```  
SELECT  
    { [Measures].[Sales Amount],   
        [Measures].[Tax Amount] } ON 0,  
    { [Date].[Fiscal].[Fiscal Year].&[2002],   
        [Date].[Fiscal].[Fiscal Year].&[2003] } ON 1  
FROM [Adventure Works]  
WHERE ( [Sales Territory].[Southwest] )  
```  
  
 For more detailed examples, see [Specifying the Contents of a Query Axis &#40;MDX&#41;](mdx-query-and-slicer-axes-specify-the-contents-of-a-query-axis.md)and [Specifying the Contents of a Slicer Axis &#40;MDX&#41;](mdx-query-and-slicer-axes-specify-the-contents-of-a-slicer-axis.md).  
  
## See Also  
 [Key Concepts in MDX &#40;Analysis Services&#41;](../key-concepts-in-mdx-analysis-services.md)   
 [SELECT Statement &#40;MDX&#41;](/sql/mdx/mdx-data-manipulation-select)  
  
  
