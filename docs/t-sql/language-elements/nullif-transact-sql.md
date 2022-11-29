---
title: "NULLIF (Transact-SQL)"
description: "NULLIF (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "09/08/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "NULLIF"
  - "NULLIF_TSQL"
helpviewer_keywords:
  - "null values [SQL Server], equivalent expressions"
  - "expressions [SQL Server], null values"
  - "NULLIF function"
  - "equivalent expressions [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# NULLIF (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a null value if the two specified expressions are equal. For example, `SELECT NULLIF(4,4) AS Same, NULLIF(5,7) AS Different;` returns NULL for the first column (4 and 4) because the two input values are the same. The second column returns the first value (5) because the two input values are different. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
NULLIF ( expression , expression )  
```  
  
## Arguments  
 *expression*  
 Is any valid scalar [expression](../../t-sql/language-elements/expressions-transact-sql.md).  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 Returns the same type as the first *expression*.  
  
 NULLIF returns the first *expression* if the two expressions are not equal. If the expressions are equal, NULLIF returns a null value of the type of the first *expression*.  
  
## Remarks  
 NULLIF is equivalent to a searched CASE expression in which the two expressions are equal and the resulting expression is NULL.  
  
 We recommend that you not use time-dependent functions, such as RAND(), within a NULLIF function. This could cause the function to be evaluated twice and to return different results from the two invocations.  
  
## Examples  
  
### A. Returning budget amounts that have not changed  
 The following example creates a `budgets` table to show a department (`dept`) its current budget (`current_year`) and its previous budget (`previous_year`). For the current year, `NULL` is used for departments with budgets that have not changed from the previous year, and `0` is used for budgets that have not yet been determined. To find out the average of only those departments that receive a budget and to include the budget value from the previous year (use the `previous_year` value, where the `current_year` is `NULL`), combine the `NULLIF` and `COALESCE` functions.  
  
```sql  
CREATE TABLE dbo.budgets  
(  
   dept            TINYINT   IDENTITY,  
   current_year    DECIMAL   NULL,  
   previous_year   DECIMAL   NULL  
);  
INSERT budgets VALUES(100000, 150000);  
INSERT budgets VALUES(NULL, 300000);  
INSERT budgets VALUES(0, 100000);  
INSERT budgets VALUES(NULL, 150000);  
INSERT budgets VALUES(300000, 250000);  
GO    
SET NOCOUNT OFF;  
SELECT AVG(NULLIF(COALESCE(current_year,  
   previous_year), 0.00)) AS [Average Budget]  
FROM budgets;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Average Budget  
 --------------  
 212500.000000  
 (1 row(s) affected)
 ```  
  
### B. Comparing NULLIF and CASE  
 To show the similarity between `NULLIF` and `CASE`, the following queries evaluate whether the values in the `MakeFlag` and `FinishedGoodsFlag` columns are the same. The first query uses `NULLIF`. The second query uses the `CASE` expression.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT ProductID, MakeFlag, FinishedGoodsFlag,   
   NULLIF(MakeFlag,FinishedGoodsFlag) AS [Null if Equal]  
FROM Production.Product  
WHERE ProductID < 10;  
GO  
  
SELECT ProductID, MakeFlag, FinishedGoodsFlag, [Null if Equal] =  
   CASE  
       WHEN MakeFlag = FinishedGoodsFlag THEN NULL  
       ELSE MakeFlag  
   END  
FROM Production.Product  
WHERE ProductID < 10;  
GO  
```  

### C: Returning budget amounts that contain no data
The following example creates a `budgets` table, loads data, and uses `NULLIF` to return a null if `current_year` is null or contains the same data as `previous_year`.

```SQL

Copy
CREATE TABLE budgets (  
   dept           TINYINT,  
   current_year   DECIMAL(10,2),  
   previous_year  DECIMAL(10,2)  
);  
  
INSERT INTO budgets VALUES(1, 100000, 150000);  
INSERT INTO budgets VALUES(2, NULL, 300000);  
INSERT INTO budgets VALUES(3, 0, 100000);  
INSERT INTO budgets VALUES(4, NULL, 150000);  
INSERT INTO budgets VALUES(5, 300000, 300000);  
  
SELECT dept, NULLIF(current_year,  
   previous_year) AS LastBudget  
FROM budgets;  
```

 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 dept   LastBudget  
 ----   -----------  
 1      100000.00  
 2      null 
 3      0.00  
 4      null  
 5      null
 ```  
  
## See Also  
 [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)   
 [decimal and numeric &#40;Transact-SQL&#41;](../../t-sql/data-types/decimal-and-numeric-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)  
  
  

