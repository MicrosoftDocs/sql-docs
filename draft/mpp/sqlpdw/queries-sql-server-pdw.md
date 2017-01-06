---
title: "Queries (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f49149ef-30e5-483b-a843-8c99f88dd9ab
caps.latest.revision: 12
author: BarbKess
---
# Queries (SQL Server PDW)
SQL Server PDW includes data definition statements for manipulation of tables, indexes, logins, and other database objects.  
  
This topic is organized by object type, as shown:  
  
[SELECT](#SELECT)  
  
[SELECT Clauses](#SELECTClauses)  
  
[Supporting Topics](#SupportingTopics)  
  
## <a name="SELECT"></a>SELECT  
  
|Command|Description|  
|-----------|---------------|  
|[SELECT](../sqlpdw/select-sql-server-pdw.md)|Retrieves rows from a table or view.|  
  
## <a name="SELECTClauses"></a>SELECT Clauses  
  
|Command|Description|  
|-----------|---------------|  
|[WITH](../sqlpdw/with-common-table-expression-sql-server-pdw.md)|Begins a common table expression.|  
|[TOP](../sqlpdw/top-sql-server-pdw.md)|Returns only a specified number of rows.|  
|[FROM](../sqlpdw/from-sql-server-pdw.md)|Specifies source tables or views.|  
|[GROUP BY](../sqlpdw/group-by-sql-server-pdw.md)|Groups a selected set of rows by the value of one more columns or expressions.|  
|[HAVING](../sqlpdw/having-sql-server-pdw.md)|Specifies a search condition applied to a group or aggregate.|  
|[ORDER BY](../sqlpdw/order-by-sql-server-pdw.md)|Specifies the sort order for returned values.|  
|[OVER](../sqlpdw/over-clause-sql-server-pdw.md)|Determines the partitioning and order of a rowset before an analytic function is applied.|  
|[UNION](../sqlpdw/union-sql-server-pdw.md)|Combines the rules of two or more queries into a single result set.|  
|[EXCEPT](../sqlpdw/except-and-intersect-sql-server-pdw.md)|Returns any distinct values from the left query that are not also found on the right query.|  
|[INTERSECT](../sqlpdw/except-and-intersect-sql-server-pdw.md)|Returns any distinct values that are returned by both the query on the left and right sides of the INTERSECT operand.|  
|[WHERE](../sqlpdw/where-sql-server-pdw.md)|Specifies the search condition for the rows returned by the query.|  
  
## <a name="SupportingTopics"></a>Supporting Topics  
  
|Statement|Description|  
|-------------|---------------|  
|[Aliasing](../sqlpdw/aliasing-sql-server-pdw.md)|Allows temporary substitution of a short string in place of a table or column name.|  
|[EXPLAIN](../sqlpdw/explain-sql-server-pdw.md)|Returns the query plan for a statement.|  
|[Search Condition](../sqlpdw/search-condition-sql-server-pdw.md)|A combination of one or more predicates that use the logical operators AND, OR, and NOT.|  
|[Subqueries](../sqlpdw/subqueries-sql-server-pdw.md)|A query that is nested inside another statement or subquery.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Definition Statements &#40;SQL Server PDW&#41;](../sqlpdw/data-definition-statements-sql-server-pdw.md)  
[Data Manipulation Statements &#40;SQL Server PDW&#41;](../sqlpdw/data-manipulation-statements-sql-server-pdw.md)  
  
