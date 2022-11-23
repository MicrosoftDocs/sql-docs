---
title: "[ ] Wildcard to match characters"
titleSuffix: SQL Server (Transact-SQL)
description: Use a wildcard to match one or more characters.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "12/06/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: "seo-lt-2019"
f1_keywords:
  - "Match"
  - "wildcard"
  - "[ ]"
  - "[_]_TSQL"
helpviewer_keywords:
  - "wildcard characters [SQL Server]"
  - "[ ] (wildcard - character(s) to match)"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# \[ \] (Wildcard - Character(s) to Match) (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Matches any single character within the specified range or set that is specified between brackets `[ ]`. These wildcard characters can be used in string comparisons that involve pattern matching, such as `LIKE` and `PATINDEX`.  

 
## Examples  
### A: Simple example   
The following example returns names that start with the letter `m`. `[n-z]` specifies that the second letter must be somewhere in the range from `n` to `z`. The percent wildcard `%` allows any or no characters starting with the 3 character. The `model` and `msdb` databases meet this criteria. The `master` database doesn't meet the criteria and is excluded from the result set.
 
```sql
SELECT name FROM sys.databases
WHERE name LIKE 'm[n-z]%';
```
[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]  

```
name
-----
model
msdb
```   
 You may have additional qualifying databases installed.


### B: More complex example   
 The following example uses the [] operator to find the IDs and names of all [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] employees who have addresses with a four-digit postal code.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT e.BusinessEntityID, p.FirstName, p.LastName, a.PostalCode  
FROM HumanResources.Employee AS e  
INNER JOIN Person.Person AS p ON e.BusinessEntityID = p.BusinessEntityID  
INNER JOIN Person.BusinessEntityAddress AS ea ON e.BusinessEntityID = ea.BusinessEntityID  
INNER JOIN Person.Address AS a ON a.AddressID = ea.AddressID  
WHERE a.PostalCode LIKE '[0-9][0-9][0-9][0-9]';  
```  
  
[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]  
  
```  
EmployeeID      FirstName      LastName      PostalCode  
----------      ---------      ---------     ----------  
290             Lynn           Tsoflias      3000  
```  

### C: Using a set that combines ranges and single characters

A wildcard set can include both single characters and ranges. The following example uses the [] operator to find a string that begins with a number or a series of special characters.

```sql
SELECT [object_id], OBJECT_NAME(object_id) AS [object_name], name, column_id 
FROM sys.columns 
WHERE name LIKE '[0-9!@#$.,;_]%';
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]  

```
object_id     object_name	                      name	column_id
---------     -----------                         ----  ---------
615673241     vSalesPersonSalesByFiscalYears	  2002	5
615673241     vSalesPersonSalesByFiscalYears	  2003	6
615673241     vSalesPersonSalesByFiscalYears	  2004	7
1591676718    JunkTable                           _xyz  1
```
  
## See Also  
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)   
  [% &#40;Wildcard - Character&#40;s&#41; to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/percent-character-wildcard-character-s-to-match-transact-sql.md)   
 [&#91;^&#93; &#40;Wildcard - Character&#40;s&#41; Not to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-character-s-not-to-match-transact-sql.md)     
 [\_ &#40;Wildcard - Match One Character&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-match-one-character-transact-sql.md)  
    
  
