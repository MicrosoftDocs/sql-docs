---
title: "_ (Wildcard - Match One Character) (Transact-SQL)"
description: "_ (Wildcard - Match One Character) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "12/06/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "Match"
  - "wildcard"
  - "_TSQL"
  - "Match One"
  - "_"
helpviewer_keywords:
  - "wildcard characters [SQL Server]"
  - "_ (wildcard - match one character)"
dev_langs:
  - "TSQL"
---
# _ (Wildcard - Match One Character) (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Use the underscore character _ to match any single character in a string comparison operation that involves pattern matching, such as `LIKE` and `PATINDEX`.  
  
## Examples  

## A: Simple example   

The following example returns all database names that begin with the letter `m` and have the letter `d` as the third letter. The underscore character specifies that the second character of the name can be any letter. The `model` and `msdb` databases meet this criteria. The `master` database does not.

```sql
SELECT name FROM sys.databases
WHERE name LIKE 'm_d%';
```   
[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   
```
name
-----
model
msdb
```   
You may have additional databases that meet this criteria.

You can use multiple underscores to represent multiple characters. Changing the `LIKE` criteria to include two underscores `'m__%` includes the master database in the result.

### B: More complex example
 The following example uses the _ operator to find all the people in the `Person` table, who have a three-letter first name that ends in `an`.  
  
```sql  
-- USE AdventureWorks2012
  
SELECT FirstName, LastName  
FROM Person.Person  
WHERE FirstName LIKE '_an'  
ORDER BY FirstName;  
```  
## C: Escaping the underscore character   
The following example returns the names of the fixed database roles like `db_owner` and `db_ddladmin`, but it also returns the `dbo` user. 

```sql
SELECT name FROM sys.database_principals
WHERE name LIKE 'db_%';
```

The underscore in the third character position is taken as a wildcard, and is not filtering for only principals starting with the letters `db_`. To escape the underscore enclose it in brackets `[_]`. 

```sql
SELECT name FROM sys.database_principals
WHERE name LIKE 'db[_]%';
```   
Now the `dbo` user is excluded.   
[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   
```
name
-------------
db_owner
db_accessadmin
db_securityadmin
...
```

  
## See Also  
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)   
  [% (Wildcard - Character(s) to Match)](../../t-sql/language-elements/percent-character-wildcard-character-s-to-match-transact-sql.md)   
  [&#91; &#93; (Wildcard - Character(s) to Match)](../../t-sql/language-elements/wildcard-character-s-to-match-transact-sql.md)   
 [&#91;^&#93; (Wildcard - Character(s) Not to Match)](../../t-sql/language-elements/wildcard-character-s-not-to-match-transact-sql.md)     
  
