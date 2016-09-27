---
title: "TOP (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 485ac6d8-d9bf-497e-9eb6-a9d1ab13f0e2
caps.latest.revision: 24
author: BarbKess
---
# TOP (SQL Server PDW)
A **SELECT** statement clause that specifies to return only a specific number of rows from the query result.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
[   
    TOP ( expression )   
    [ WITH TIES ]  
]  
```  
  
## Arguments  
*expression*  
The numeric expression that specifies the number of rows to be returned. *expression* is implicitly converted to **bigint**.  
  
If the query includes an **ORDER BY** clause, the top *expression* rows are returned as ordered by the **ORDER BY** clause. If the query has no **ORDER BY** clause, the order of the rows is arbitrary.  
  
**WITH TIES**  
Used when you want to return two or more rows that tie for last place in the limited results set. Must be used with the **ORDER BY** clause. **WITH TIES** may cause more rows to be returned than the value specified in *expression*. For example, if *expression* is set to 5 but 2 additional rows match the values of the **ORDER BY** columns in row 5, the result set will contain 7 rows.  
  
**TOP**... **WITH TIES** can be specified only in **SELECT** statements, and only if an **ORDER BY** clause is specified. The returned order of tying records is arbitrary. **ORDER BY** does not affect this rule.  
  
## Examples  
The following example returns the top 31 rows that match the query criteria. The **ORDER BY** clause is used to ensure that the 31 returned rows are the first 31 rows based on an alphabetical ordering of the `LastName` column.  
  
Using **TOP** without specifying ties.  
  
```  
SELECT TOP (31) FirstName, LastName   
FROM DimEmployee ORDER BY LastName;  
```  
  
Result: 31 rows are returned.  
  
Using TOP, specifying WITH TIES.  
  
```  
SELECT TOP (31) WITH TIES FirstName, LastName   
FROM DimEmployee ORDER BY LastName;  
```  
  
Result: 33 rows are returned, because 3 employees named Brown tie for the 31st row.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
  
