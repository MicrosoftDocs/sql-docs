---
title: "[^] Wildcard to exclude characters"
titleSuffix: SQL Server (Transact-SQL)
description: "T-SQL wildcard for characters not to match"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "12/06/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: "seo-lt-2019"
f1_keywords:
  - "wildcard"
  - "[^]_TSQL"
  - "[^]"
  - "Not Match"
helpviewer_keywords:
  - "wildcard characters [SQL Server]"
  - "[^] (wildcard - character(s) not to match)"
dev_langs:
  - "TSQL"
---

# \[^\] (Wildcard - Character(s) Not to Match) (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Matches any single character that is not within the range or set specified between the square brackets `[^]`. These wildcard characters can be used in string comparisons that involve pattern matching, such as `LIKE` and `PATINDEX`. 
  
## Examples  
### A: Simple example   
 The following example uses the [^] operator to find the top 5 people in the `Contact` table who have a first name that starts with `Al` and has a third letter that is not the letter `a`.  
  
```sql
-- Uses AdventureWorks  
  
SELECT TOP 5 FirstName, LastName  
FROM Person.Person  
WHERE FirstName LIKE 'Al[^a]%';  
```  
[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]  

```
FirstName     LastName
---------     --------
Alex          Adams
Alexandra     Adams
Allison       Adams
Alisha        Alan
Alexandra     Alexander
```
### B: Searching for ranges of characters

A wildcard set can include single characters or ranges of characters as well as combinations of characters and ranges. The following example uses the [^] operator to find a string that does not begin with a letter or number.

```sql
SELECT [object_id], OBJECT_NAME(object_id) AS [object_name], name, column_id 
FROM sys.columns 
WHERE name LIKE '[^0-9A-z]%';
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]  

```
object_id     object_name   name    column_id
---------     -----------   ----    ---------
1591676718    JunkTable     _xyz    1
```
  
## See Also  
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)   
 [% &#40;Wildcard - Character&#40;s&#41; to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/percent-character-wildcard-character-s-to-match-transact-sql.md)   
  [&#91; &#93; &#40;Wildcard - Character&#40;s&#41; to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-character-s-to-match-transact-sql.md)   
 [\_ &#40;Wildcard - Match One Character&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-match-one-character-transact-sql.md)  
  
  
