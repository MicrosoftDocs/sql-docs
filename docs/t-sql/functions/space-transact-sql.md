---
title: "SPACE (Transact-SQL)"
description: "SPACE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SPACE_TSQL"
  - "SPACE"
helpviewer_keywords:
  - "strings [SQL Server], repeated spaces"
  - "repeated spaces"
  - "SPACE function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SPACE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

  Returns a string of repeated spaces.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SPACE ( integer_expression )  
```  
  
## Arguments
 *integer_expression*  
 Is a positive integer that indicates the number of spaces. If *integer_expression* is negative, a null string is returned.  
  
 For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
  
## Return Types  
 **varchar**  
  
## Remarks  
 To include spaces in Unicode data, or to return more than 8000 character spaces, use REPLICATE instead of SPACE.  
  
## Examples  
 The following example trims the last names and concatenates a comma, two spaces, and the first names of people listed in the `Person` table in [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)].  
  
```sql  
USE AdventureWorks2022;  
GO  
SELECT RTRIM(LastName) + ',' + SPACE(2) +  LTRIM(FirstName)  
FROM Person.Person  
ORDER BY LastName, FirstName;  
GO  
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example trims the last names and concatenates a comma, two spaces, and the first names of people listed in the `DimCustomer` table in `AdventureWorksPDW2012`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT RTRIM(LastName) + ',' + SPACE(2) +  LTRIM(FirstName)  
FROM dbo.DimCustomer  
ORDER BY LastName, FirstName;  
GO  
```  
  
## Related content

- [REPLICATE (Transact-SQL)](replicate-transact-sql.md)
- [What are the SQL database functions?](functions.md)
