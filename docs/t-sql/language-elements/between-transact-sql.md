---
title: "BETWEEN (Transact-SQL)"
description: "BETWEEN (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "08/28/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "BETWEEN"
  - "BETWEEN_TSQL"
helpviewer_keywords:
  - "inclusive ranges"
  - "testing range"
  - "exclusive ranges"
  - "NOT BETWEEN operator"
  - "BETWEEN operator"
  - "range to test [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# BETWEEN (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Specifies a range to test.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
test_expression [ NOT ] BETWEEN begin_expression AND end_expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *test_expression*  
 Is the [expression](../../t-sql/language-elements/expressions-transact-sql.md) to test for in the range defined by *begin_expression*and *end_expression*. *test_expression* must be the same data type as both *begin_expression* and *end_expression*.  
  
 NOT  
 Specifies that the result of the predicate be negated.  
  
 *begin_expression*  
 Is any valid expression. *begin_expression* must be the same data type as both *test_expression* and *end_expression*.  
  
 *end_expression*  
 Is any valid expression. *end_expression* must be the same data type as both *test_expression*and *begin_expression*.  
  
 AND  
 Acts as a placeholder that indicates *test_expression* should be within the range indicated by *begin_expression* and *end_expression*.  
  
## Result Types  
 **Boolean**  
  
## Result Value  
 BETWEEN returns **TRUE** if the value of *test_expression* is greater than or equal to the value of *begin_expression* and less than or equal to the value of *end_expression*.  
  
 NOT BETWEEN returns **TRUE** if the value of *test_expression* is less than the value of *begin_expression* or greater than the value of *end_expression*.  
  
## Remarks  
 To specify an exclusive range, use the greater than (>) and less than operators (<). If any input to the BETWEEN or NOT BETWEEN predicate is NULL, the result is UNKNOWN.  
  
## Examples  
  
### A. Using BETWEEN  
 The following example returns information about the database roles in a database. The first query returns all the roles. The second example uses the `BETWEEN` clause to limit the roles to the specified `database_id` values.  
  
```sql  
SELECT principal_id, name 
FROM sys.database_principals
WHERE type = 'R';

SELECT principal_id, name 
FROM sys.database_principals
WHERE type = 'R'
AND principal_id BETWEEN 16385 AND 16390;
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]   
```  
principal_id	name
------------  ---- 
0	            public
16384	        db_owner
16385	        db_accessadmin
16386	        db_securityadmin
16387	        db_ddladmin
16389	        db_backupoperator
16390	        db_datareader
16391	        db_datawriter
16392	        db_denydatareader
16393	        db_denydatawriter
```  
```  
principal_id	name
------------  ---- 
16385	        db_accessadmin
16386	        db_securityadmin
16387	        db_ddladmin
16389	        db_backupoperator
16390	        db_datareader
```  
  
### B. Using > and < instead of BETWEEN  
 The following example uses greater than (`>`) and less than (`<`) operators and, because these operators are not inclusive, returns nine rows instead of ten that were returned in the previous example.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT e.FirstName, e.LastName, ep.Rate  
FROM HumanResources.vEmployee e   
JOIN HumanResources.EmployeePayHistory ep   
    ON e.BusinessEntityID = ep.BusinessEntityID  
WHERE ep.Rate > 27 AND ep.Rate < 30  
ORDER BY ep.Rate;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 FirstName   LastName             Rate  
 ---------   -------------------  ---------  
 Paula       Barreto de Mattos    27.1394  
 Janaina     Bueno                27.4038  
 Dan         Bacon                27.4038  
 Ramesh      Meyyappan            27.4038  
 Karen       Berg                 27.4038  
 David       Bradley              28.7500  
 Hazem       Abolrous             28.8462  
 Ovidiu      Cracium              28.8462  
 Rob         Walters              29.8462  
 ```    
  
### C. Using NOT BETWEEN  
 The following example finds all rows outside a specified range of `27` through `30`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT e.FirstName, e.LastName, ep.Rate  
FROM HumanResources.vEmployee e   
JOIN HumanResources.EmployeePayHistory ep   
    ON e.BusinessEntityID = ep.BusinessEntityID  
WHERE ep.Rate NOT BETWEEN 27 AND 30  
ORDER BY ep.Rate;  
GO  
```  
  
### D. Using BETWEEN with datetime values  
 The following example retrieves rows in which **datetime** values are between `'20011212'` and `'20020105'`, inclusive.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT BusinessEntityID, RateChangeDate  
FROM HumanResources.EmployeePayHistory  
WHERE RateChangeDate BETWEEN '20011212' AND '20020105';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 BusinessEntityID RateChangeDate  
 ----------- -----------------------  
 3           2001-12-12 00:00:00.000  
 4           2002-01-05 00:00:00.000  
 ```  
 
 The query retrieves the expected rows because the date values in the query and the **datetime** values stored in the `RateChangeDate` column have been specified without the time part of the date. When the time part is unspecified, it defaults to 12:00 A.M. Note that a row that contains a time part that is after 12:00 A.M. on 2002-01-05 would not be returned by this query because it falls outside the range.  
  
  
## See Also  
 [&#62; &#40;Greater Than&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/greater-than-transact-sql.md)   
 [&#60; &#40;Less Than&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/less-than-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  


