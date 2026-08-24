---
title: "LEFT (Transact-SQL)"
description: "LEFT (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "LEFT"
  - "LEFT_TSQL"
helpviewer_keywords:
  - "character strings [SQL Server], LEFT"
  - "characters [SQL Server], leftmost"
  - "LEFT function"
  - "leftmost character of expression"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# LEFT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

  Returns the left part of a character string with the specified number of characters.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
LEFT ( character_expression , integer_expression )  
```  
  
## Arguments
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character or binary data. *character_expression* can be a constant, variable, or column. *character_expression* can be of any data type, except **text** or **ntext**, that can be implicitly converted to **varchar** or **nvarchar**. Otherwise, use the [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) function to explicitly convert *character_expression*.  
 
> [!NOTE]  
> If *string_expression* is of type **binary** or **varbinary**, LEFT will perform an implicit conversion to **varchar**, and therefore will not preserve the binary input.  
  
 *integer_expression*  
 Is a positive integer that specifies how many characters of the *character_expression* will be returned. If *integer_expression* is negative, an error is returned. If *integer_expression* is type **bigint** and contains a large value, *character_expression* must be of a large data type such as **varchar(max)**.  
  
 The *integer_expression* parameter counts a UTF-16 surrogate character as one character.  
  
## Return Types  
 Returns **varchar** when *character_expression* is a non-Unicode character data type.  
  
 Returns **nvarchar** when *character_expression* is a Unicode character data type.  
  
## Remarks  
 When using SC collations, the *integer_expression* parameter counts a UTF-16 surrogate pair as one character. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples  
  
### A. Using LEFT with a column  
 The following example returns the five leftmost characters of each product name in the `Product` table of the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT LEFT(Name, 5)   
FROM Production.Product  
ORDER BY ProductID;  
GO  
```  
  
### B. Using LEFT with a character string  
 The following example uses `LEFT` to return the two leftmost characters of the character string `abcdefg`.  
  
```sql  
SELECT LEFT('abcdefg',2);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
--   
ab   
  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Using LEFT with a column  
 The following example returns the five leftmost characters of each product name.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT LEFT(EnglishProductName, 5)   
FROM dbo.DimProduct  
ORDER BY ProductKey;  
```  
  
### D. Using LEFT with a character string  
 The following example uses `LEFT` to return the two leftmost characters of the character string `abcdefg`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT LEFT('abcdefg',2) FROM dbo.DimProduct;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
--   
ab  
```  
  
## Related content

- [LTRIM (Transact-SQL)](ltrim-transact-sql.md)
- [RIGHT (Transact-SQL)](right-transact-sql.md)
- [RTRIM (Transact-SQL)](rtrim-transact-sql.md)
- [STRING_SPLIT (Transact-SQL)](string-split-transact-sql.md)
- [SUBSTRING (Transact-SQL)](substring-transact-sql.md)
- [TRIM (Transact-SQL)](trim-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
