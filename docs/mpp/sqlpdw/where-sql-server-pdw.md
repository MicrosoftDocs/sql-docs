---
title: "WHERE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d72a0104-800c-4eea-89ee-8fe767745f16
caps.latest.revision: 17
author: BarbKess
---
# WHERE (SQL Server PDW)
Specifies the search condition for the rows returned by a SQL Server PDWSQLquery.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
[ WHERE search_condition ]  
```  
  
## Arguments  
*search_condition*  
Defines the condition to be met for the rows to be returned. There is no limit to the number of predicates that can be included in a search condition. For more information about search conditions and predicates, see [Search Condition &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/search-condition-sql-server-pdw.md).  
  
## Examples  
The following examples show how to use some common search conditions in the `WHERE` clause.  
  
### A. Finding a row by using a simple equality  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName = 'Smith' ;  
```  
  
### B. Finding rows that contain a value as part of a string  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName LIKE ('%Smi%');  
```  
  
### C. Finding rows by using a comparison operator  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey  <= 500;  
```  
  
### D. Finding rows that meet any of three conditions  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey = 1 OR EmployeeKey = 8 OR EmployeeKey = 12;  
```  
  
### E. Finding rows that must meet several conditions  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey <= 500 AND LastName LIKE '%Smi%' AND FirstName LIKE '%A%';  
```  
  
### F. Finding rows that are in a list of values  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName IN ('Smith', 'Godfrey', 'Johnson');  
```  
  
### G. Finding rows that have a value between two values  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey Between 100 AND 200;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[DELETE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/delete-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md)  
[UPDATE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/update-sql-server-pdw.md)  
  
