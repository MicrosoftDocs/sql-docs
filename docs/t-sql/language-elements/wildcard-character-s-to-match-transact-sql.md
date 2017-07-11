---
title: "(Wildcard - Character(s) to Match) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/06/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "Match"
  - "wildcard"
  - "[ ]"
  - "[_]_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "wildcard characters [SQL Server]"
  - "[ ] (wildcard - character(s) to match)"
ms.assetid: 57817576-0bf1-49ed-b05d-fac27e8fed7a
caps.latest.revision: 32
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# (Wildcard - Character(s) to Match) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Matches any single character within the specified range or set that is specified between brackets `[ ]`. These wildcard characters can be used in string comparisons that involve pattern matching, such as `LIKE` and `PATINDEX`.  
  
## Examples  
### A: Simple example   
The following example returns the names of that start with the letter `m`. `[n-z]` specifies that the second letter must be somewhere in the range from `n` to `z`. The percent wildcard `%` allows any or no characters starting with the 3 character. The `model` and `msdb` databases meet this criteria. The `master` database does not and is excluded from the result set.
 
```tsql
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
  
```tsql  
-- Uses AdventureWorks  
  
SELECT e.BusinessEntityID, p.FirstName, p.LastName, a.PostalCode  
FROM HumanResources.Employee AS e  
INNER JOIN Person.Person AS p ON e.BusinessEntityID = p.BusinessEntityID  
INNER JOIN Person.BusinessEntityAddress AS ea ON e.BusinessEntityID = ea.BusinessEntityID  
INNER JOIN Person.Address AS a ON a.AddressID = ea.AddressID  
WHERE a.PostalCode LIKE '[0-9][0-9][0-9][0-9]';  
```  
  
 Here is the result set:  
  
```  
EmployeeID      FirstName      LastName      PostalCode  
----------      ---------      ---------     ----------  
290             Lynn           Tsoflias      3000  
```  



  
## See Also  
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)   
  [% (Wildcard - Character(s) to Match)](../../t-sql/language-elements/percent-character-wildcard-character-s-to-match-transact-sql.md)   
 [&#91;^&#93; (Wildcard - Character(s) Not to Match)](../../t-sql/language-elements/wildcard-character-s-not-to-match-transact-sql.md)     
 [_ (Wildcard - Match One Character)](../../t-sql/language-elements/wildcard-match-one-character-transact-sql.md)  
    
  
