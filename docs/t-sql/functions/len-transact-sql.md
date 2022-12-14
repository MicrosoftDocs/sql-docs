---
title: "LEN (Transact-SQL)"
description: "LEN (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/03/2015"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "LEN"
  - "LEN_TSQL"
helpviewer_keywords:
  - "LEN function"
  - "characters [SQL Server], number of"
  - "number of characters"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# LEN (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns the number of characters of the specified string expression, excluding trailing spaces.  
  
> [!NOTE]  
> To return the number of bytes used to represent an expression, use the [DATALENGTH](../../t-sql/functions/datalength-transact-sql.md) function.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
LEN ( string_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *string_expression*  
 Is the string [expression](../../t-sql/language-elements/expressions-transact-sql.md) to be evaluated. *string_expression* can be a constant, variable, or column of either character or binary data.  
  
## Return Types  
 **bigint** if *expression* is of the **varchar(max)**, **nvarchar(max)** or **varbinary(max)** data types; otherwise, **int**.  
  
 If you are using SC collations, the returned integer value counts UTF-16 surrogate pairs as a single character. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
## Remarks  
LEN excludes trailing spaces. If that is a problem, consider using the [DATALENGTH &#40;Transact-SQL&#41;](../../t-sql/functions/datalength-transact-sql.md) function which does not trim the string. If processing a unicode string, DATALENGTH will return a number that may not be equal to the number of characters. The following example demonstrates LEN and DATALENGTH with a trailing space.  
  
```sql  
  DECLARE @v1 VARCHAR(40),  
    @v2 NVARCHAR(40);  
SELECT   
@v1 = 'Test of 22 characters ',   
@v2 = 'Test of 22 characters ';  
SELECT LEN(@v1) AS [VARCHAR LEN] , DATALENGTH(@v1) AS [VARCHAR DATALENGTH];  
SELECT LEN(@v2) AS [NVARCHAR LEN], DATALENGTH(@v2) AS [NVARCHAR DATALENGTH];  
```  

> [!NOTE]
> Use the [LEN](../../t-sql/functions/len-transact-sql.md) to return the number of characters encoded into a given string expression, and [DATALENGTH](../../t-sql/functions/datalength-transact-sql.md) to return the size in bytes for a given string expression. These outputs may differ depending on the data type and type of encoding used in the column. For more information on storage differences between different encoding types, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).

## Examples  
 The following example selects the number of characters and the data in `FirstName` for people located in `Australia`. This example uses the AdventureWorks database.  
  
```sql  
SELECT LEN(FirstName) AS Length, FirstName, LastName   
FROM Sales.vIndividualCustomer  
WHERE CountryRegionName = 'Australia';  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns the number of characters in the column `FirstName` and the first and last names of employees located in `Australia`.  
  
```sql  
USE AdventureWorks2016  
GO  
SELECT DISTINCT LEN(FirstName) AS FNameLength, FirstName, LastName   
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.DimGeography AS g   
    ON e.SalesTerritoryKey = g.SalesTerritoryKey   
WHERE EnglishCountryRegionName = 'Australia';  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
FNameLength  FirstName  LastName  
-----------  ---------  ---------------  
4            Lynn       Tsoflias
```  
  
## See Also  
 [DATALENGTH &#40;Transact-SQL&#41;](../../t-sql/functions/datalength-transact-sql.md)   
 [CHARINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/charindex-transact-sql.md)  
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)  
 [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)   
 [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
  
  
