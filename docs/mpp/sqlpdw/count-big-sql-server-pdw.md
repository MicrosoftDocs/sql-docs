---
title: "COUNT_BIG (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 33445683-92f5-48cd-8e42-a205cd895752
caps.latest.revision: 11
author: BarbKess
---
# COUNT_BIG (SQL Server PDW)
Returns the number of items in a group in SQL Server PDW. Use this function in the select list or HAVING clause of a SELECT statement to aggregate the number of rows in a table or partition. COUNT_BIG differs from the COUNT function only in their return values. COUNT_BIG always returns a **bigint** data type value. COUNT always returns an **int** data type value.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregation Function Syntax  
COUNT_BIG ( { [ [ ALL | DISTINCT ] expression ] | * } )  
```  
  
```  
-- Analytic Function Syntax  
COUNT_BIG ( { expression | * } ) OVER ( [ <partition_by_clause> ] )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. COUNT_BIG (ALL *expression*) evaluates *expression* for each row in a group and returns the number of non-null values. ALL is the default.  
  
DISTINCT  
Specifies that COUNT_BIG return the number of unique non-null values. COUNT_BIG (DISTINCT *expression*) evaluates *expression* for each row in a group or partition and returns the number of unique, non-null values.  
  
*expression*  
Is an expression of any type. Aggregate functions and subqueries are not permitted.  
  
**\***  
Specifies that all rows (including null and duplicate values) should be counted to return the total number of rows in a table or partition. COUNT_BIG(**\***) takes no parameters and cannot be used with ALL or DISTINCT.  
  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
< *partition_by_clause* >  
Divides the result set produced by the FROM clause into partitions to which the COUNT_BIG function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md).  
  
## Return Types  
**bigint**  
  
## Limitation and Restrictions  
The COUNT_BIG() OVER analytic function does not support the use of the ALL or DISTINCT modifiers for the expression to compute the COUNT_BIG function against.  
  
## Examples  
For examples, see [COUNT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/count-sql-server-pdw.md).  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[COUNT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/count-sql-server-pdw.md)  
[OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
  
