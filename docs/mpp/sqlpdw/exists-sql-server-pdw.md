---
title: "EXISTS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: eb84bff4-5e57-4021-8efc-e9fd94e19201
caps.latest.revision: 19
author: BarbKess
---
# EXISTS (SQL Server PDW)
Specifies a subquery to test for the existence of rows.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
EXISTS ( subquery )  
```  
  
## Arguments  
*subquery*  
A restricted SELECT statement. For more information, see [Subqueries &#40;SQL Server PDW&#41;](../sqlpdw/subqueries-sql-server-pdw.md).  
  
## Result Types  
**Boolean**  
  
## Result Values  
Returns TRUE if a subquery contains any rows.  
  
## Examples  
  
### A. Using EXISTS  
The following example identifies whether any rows in the `ProspectiveBuyer` table could be matches to rows in the `DimCustomer` table. The query will return rows only when both the `LastName` and `BirthDate` values in the two tables match.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT a.LastName, a.BirthDate  
FROM DimCustomer AS a  
WHERE EXISTS  
(SELECT *   
    FROM dbo.ProspectiveBuyer AS b  
    WHERE (a.LastName = b.LastName) AND (a.BirthDate = b.BirthDate));  
```  
  
### B. Using NOT EXISTS  
NOT EXISTS works as the opposite as EXISTS. The WHERE clause in NOT EXISTS is satisfied if no rows are returned by the subquery. The following example finds rows in the `DimCustomer` table where the `LastName` and `BirthDate` do not match any entries in the `ProspectiveBuyers` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT a.LastName, a.BirthDate  
FROM DimCustomer AS a  
WHERE NOT EXISTS  
(SELECT *   
    FROM dbo.ProspectiveBuyer AS b  
    WHERE (a.LastName = b.LastName) AND (a.BirthDate = b.BirthDate));  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
