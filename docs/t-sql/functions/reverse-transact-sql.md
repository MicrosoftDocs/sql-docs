---
title: "REVERSE (Transact-SQL)"
description: "REVERSE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "REVERSE_TSQL"
  - "REVERSE"
helpviewer_keywords:
  - "expressions [SQL Server], reverse"
  - "REVERSE function"
  - "reverse character expressions"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# REVERSE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

  Returns the reverse order of a string value.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
REVERSE ( string_expression )  
```  
  
## Arguments
 *string_expression*  
 *string_expression* is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of a string or binary data type. *string_expression* can be a constant, variable, or column of either character or binary data.  
  
## Return Types  
 **varchar** or **nvarchar**  
  
## Remarks  
 *string_expression* must be of a data type that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *string_expression*.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using SC collations, the REVERSE function will not reverse the order of two halves of a surrogate pair.  
  
## Examples  
 The following example returns all contact first names with the characters reversed. This example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
SELECT FirstName, REVERSE(FirstName) AS Reverse  
FROM Person.Person  
WHERE BusinessEntityID < 5  
ORDER BY FirstName;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
FirstName      Reverse
-------------- --------------
Ken            neK
Rob            boR
Roberto        otreboR
Terri          irreT

(4 row(s) affected)
```  
  
 The following example reverses the characters in a variable.  
  
```sql
DECLARE @myvar VARCHAR(10);  
SET @myvar = 'sdrawkcaB';  
SELECT REVERSE(@myvar) AS Reversed ;  
GO  
```  
  
 The following example makes an implicit conversion from an **int** data type into **varchar** data type and then reverses the result.  
  
```sql
SELECT REVERSE(1234) AS Reversed ;  
GO  
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns names of all databases, and the names with the characters reversed.  
  
```sql
SELECT name, REVERSE(name) FROM sys.databases;  
GO  
```  
  
## Related content

- [CONCAT (Transact-SQL)](concat-transact-sql.md)
- [CONCAT_WS (Transact-SQL)](concat-ws-transact-sql.md)
- [FORMATMESSAGE (Transact-SQL)](formatmessage-transact-sql.md)
- [QUOTENAME (Transact-SQL)](quotename-transact-sql.md)
- [REPLACE (Transact-SQL)](replace-transact-sql.md)
- [STRING_AGG (Transact-SQL)](string-agg-transact-sql.md)
- [STRING_ESCAPE (Transact-SQL)](string-escape-transact-sql.md)
- [STUFF (Transact-SQL)](stuff-transact-sql.md)
- [TRANSLATE (Transact-SQL)](translate-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
