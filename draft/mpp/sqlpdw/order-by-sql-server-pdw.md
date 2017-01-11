---
title: "ORDER BY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ffe8dc21-8b3e-4f0a-9171-14b19040d730
caps.latest.revision: 21
author: BarbKess
---
# ORDER BY (SQL Server PDW)
The ORDER BY clause specifies the sort order for columns returned in a SQL Server PDW SELECT statement.  
  
> [!NOTE]  
> When ORDER BY is used in the definition of a view or subquery, the clause is used only to determine the rows returned by the [TOP &#40;SQL Server PDW&#41;](../sqlpdw/top-sql-server-pdw.md) clause. The ORDER BY clause does not guarantee ordered results when these constructs are queried, unless ORDER BY is also specified in the query itself.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
[ ORDER BY   
    {  
    order_by_expression   
    [ ASC | DESC ]   
    } [ ,...n ]   
]  
```  
  
## Arguments  
*order_by_expression*  
Specifies a column on which to sort. A sort column can be specified as a name or column alias, or a positive non-negative integer representing the position of the name or alias in the select list.  
  
**ASC** | DESC  
Specifies that the values in the specified column should be sorted in ascending (from lowest value to highest value) or descending (from highest value to lowest value) order.  
  
## General Remarks  
Null values are treated as the lowest possible values.  
  
There is no limit to the number of items in the ORDER BY clause. However, there is a limit of 8,060 bytes for the row size of intermediate worktables needed for sort operations. This limits the total size of columns specified in an ORDER BY clause.  
  
Multiple sort columns can be specified. The sequence of the sort columns in the ORDER BY clause defines the organization of the sorted result set. A sort column can include an expression. Column names and aliases can be qualified by the table or view name.  
  
When the SELECT statement includes a UNION operator, the column names or column aliases in the ORDER BY clause must be included in every select list of the UNION.  
  
## Limitations and Restrictions  
An integer cannot be specified when the *order_by_expression*  appears in a ranking function.  
  
If *order_by_expression* is not qualified, the value must be unique among all columns listed in the SELECT statement.  
  
The ORDER BY clause can include items that do not appear in the select list. However, if SELECT DISTINCT is specified, if the statement contains a GROUP BY clause, or if the SELECT statement contains a UNION operator, the sort columns must appear in the select list.  
  
## Examples  
The following example demonstrates ordering of a result set by the numerical `EmployeeKey` column in ascending order.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, FirstName, LastName FROM DimEmployee  
WHERE LastName LIKE 'A%'  
ORDER BY EmployeeKey;  
```  
  
The following example orders a result set by the numerical `EmployeeKey` column in descending order.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, FirstName, LastName FROM DimEmployee  
WHERE LastName LIKE 'A%'  
ORDER BY EmployeeKey DESC;  
```  
  
The following example orders a result set by the `LastName` column.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, FirstName, LastName FROM DimEmployee  
WHERE LastName LIKE 'A%'  
ORDER BY LastName;  
```  
  
The following example orders by two columns. This query first sorts in ascending order by the `FirstName` column, and then sorts common `FirstName` values in descending order by the `LastName` column.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, FirstName, LastName FROM DimEmployee  
WHERE LastName LIKE 'A%'  
ORDER BY LastName, FirstName;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[TOP &#40;SQL Server PDW&#41;](../sqlpdw/top-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[INSERT &#40;SQL Server PDW&#41;](../sqlpdw/insert-sql-server-pdw.md)  
[UPDATE &#40;SQL Server PDW&#41;](../sqlpdw/update-sql-server-pdw.md)  
[DELETE &#40;SQL Server PDW&#41;](../sqlpdw/delete-sql-server-pdw.md)  
  
