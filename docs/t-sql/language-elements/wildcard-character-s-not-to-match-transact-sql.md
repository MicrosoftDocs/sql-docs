---
title: "[^] (Wildcard - Character(s) Not to Match) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/06/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "wildcard"
  - "[^]_TSQL"
  - "[^]"
  - "Not Match"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "wildcard characters [SQL Server]"
  - "[^] (wildcard - character(s) not to match)"
ms.assetid: b970038f-f4e7-4a5d-96f6-51e3248c6aef
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---

# \[^\] (Wildcard - Character(s) Not to Match) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Matches any single character that is not within the range or set specified between the square brackets.  
  
## Examples  
 The following example uses the [^] operator to find all the people in the `Contact` table who have first names that start with `Al` and have a third letter that is not the letter `a`.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName  
FROM Person.Person  
WHERE FirstName LIKE 'Al[^a]%'  
ORDER BY FirstName;  
```  
  
## See Also  
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)   
 [% &#40;Wildcard - Character&#40;s&#41; to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/percent-character-wildcard-character-s-to-match-transact-sql.md)   
  [&#91; &#93; &#40;Wildcard - Character&#40;s&#41; to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-character-s-to-match-transact-sql.md)   
 [\_ &#40;Wildcard - Match One Character&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-match-one-character-transact-sql.md)  
  
  
