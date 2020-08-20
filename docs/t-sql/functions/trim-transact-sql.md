---
description: "TRIM (Transact-SQL)"
title: "TRIM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/27/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "jrasnick"
ms.technology: t-sql
ms.topic: conceptual
f1_keywords: 
  - "TRIM"
  - "TRIM_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TRIM function"
ms.assetid: a00245aa-32c7-4ad4-a0d1-64f3d6841153
author: julieMSFT
ms.author: jrasnick
monikerRange: "= azure-sqldw-latest||=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# TRIM (Transact-SQL)

[!INCLUDE [sqlserver2017-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi-asa.md)]

Removes the space character `char(32)` or other specified characters from the start and end of a string.  

## Syntax

```
-- Syntax for SQL Server and Azure SQL Database
TRIM ( [ characters FROM ] string )
```

```
-- Syntax for Azure SQL Data Warehouse
TRIM ( string )
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

characters
Is a literal, variable, or function call of any non-LOB character type (`nvarchar`, `varchar`, `nchar`, or `char`) containing characters that should be removed. `nvarchar(max)` and `varchar(max)` types aren't allowed.

string
Is an expression of any character type (`nvarchar`, `varchar`, `nchar`, or `char`) where characters should be removed.

## Return Types

Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from both sides. Returns `NULL` if input string is `NULL`.

## Remarks

By default, the `TRIM` function removes the space character from both the beginning and the ending ends of the string. This behavior is equivalent to `LTRIM(RTRIM(@string))`.

## Examples

### A.  Removes the space character from both sides of string

The following example removes spaces from before and after the word `test`.

```sql
SELECT TRIM( '     test    ') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```
test
```

### B.  Removes specified characters from both sides of string

The following example removes a trailing period and spaces from before `#` and after the word `test`.

```sql
SELECT TRIM( '.,! ' FROM  '     #     test    .') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]
```
#     test
```

## See Also

- [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)  
- [LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)  
- [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)  
- [RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)  
- [STRING_SPLIT &#40;Transact-SQL&#41;](../../t-sql/functions/string-split-transact-sql.md)  
- [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)  
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
