---
title: "Wildcard search (%)"
description: "Percent character (Wildcard - Character(s) to Match) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "12/06/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: "seo-lt-2019"
f1_keywords:
  - "%"
  - "%_TSQL"
  - "wildcard"
helpviewer_keywords:
  - "conditions [SQL Server], wildcards"
  - "% (wildcard - character(s) to match)"
  - "matching conditions [SQL Server]"
  - "wildcard characters [SQL Server]"
  - "characters [SQL Server], wildcard"
dev_langs:
  - "TSQL"
---
# Percent character (Wildcard - Character(s) to Match) (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Matches any string of zero or more characters. This wildcard character can be used as either a prefix or a suffix.  
  
## Examples  
 The following example returns all the first names of people in the `Person` table of `AdventureWorks2012` that start with `Dan`.  
  
```syntaxsql  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName  
FROM Person.Person  
WHERE FirstName LIKE 'Dan%';  
GO  
```  
  
## See Also  
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
 [&#91; &#93; (Wildcard - Character(s) to Match)](../../t-sql/language-elements/wildcard-character-s-to-match-transact-sql.md)   
  [&#91;^&#93; (Wildcard - Character(s) Not to Match)](../../t-sql/language-elements/wildcard-character-s-not-to-match-transact-sql.md)     
 [_ (Wildcard - Match One Character)](../../t-sql/language-elements/wildcard-match-one-character-transact-sql.md)  
    
  
