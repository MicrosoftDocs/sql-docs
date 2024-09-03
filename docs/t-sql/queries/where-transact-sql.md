---
title: "WHERE (Transact-SQL)"
description: "WHERE (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "08/09/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "WHERE_TSQL"
  - "WHERE"
helpviewer_keywords:
  - "retrieving rows"
  - "clauses [SQL Server], WHERE"
  - "WHERE clause, about WHERE clause"
  - "row retrieval [SQL Server], WHERE clause"
  - "WHERE clause"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# WHERE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Specifies the search condition for the rows returned by the query.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
[ WHERE <search_condition> ]  
```  
  
## Arguments
\< *search_condition* >
 Defines the condition to be met for the rows to be returned. There is no limit to the number of predicates that can be included in a search condition. For more information about search conditions and predicates, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
## Examples  
 The following examples show how to use some common search conditions in the `WHERE` clause.  
  
### A. Finding a row by using a simple equality  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName = 'Smith' ;  
```  
  
### B. Finding rows that contain a value as part of a string  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName LIKE ('%Smi%');  
```  
  
### C. Finding rows by using a comparison operator  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey  <= 500;  
```  
  
### D. Finding rows that meet any of three conditions  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey = 1 OR EmployeeKey = 8 OR EmployeeKey = 12;  
```  
  
### E. Finding rows that must meet several conditions  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey <= 500 AND LastName LIKE '%Smi%' AND FirstName LIKE '%A%';  
```  
  
### F. Finding rows that are in a list of values  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName IN ('Smith', 'Godfrey', 'Johnson');  
```  
  
### G. Finding rows that have a value between two values  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey Between 100 AND 200;  
```  
  
## See Also  
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [Predicates &#40;Transact-SQL&#41;](~/t-sql/queries/predicates.md)   
 [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [MERGE &#40;Transact-SQL&#41;](../../t-sql/statements/merge-transact-sql.md)  
  
  


