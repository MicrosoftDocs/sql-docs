---
title: "TRIM (Transact-SQL)"
description: "Removes the space character or other specified characters from the start and end of a string."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/04/2022
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: t-sql
ms.topic: reference
f1_keywords:
  - "TRIM"
  - "TRIM_TSQL"
helpviewer_keywords:
  - "TRIM function"
dev_langs:
  - "TSQL"
monikerRange: "= azure-sqldw-latest || = azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# TRIM (Transact-SQL)

[!INCLUDE [sqlserver2017-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi-asa.md)]

Removes the space character `char(32)` or other specified characters from the start and end of a string.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server and Azure SQL Database:

```syntaxsql
TRIM ( [ characters FROM ] string )
```

Syntax for Azure Synapse Analytics:

```syntaxsql
TRIM ( string )
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *characters*

A literal, variable, or function call of any non-LOB character type (`nvarchar`, `varchar`, `nchar`, or `char`) containing characters that should be removed. `nvarchar(max)` and `varchar(max)` types aren't allowed.

#### *string*

An expression of any character type (`nvarchar`, `varchar`, `nchar`, or `char`) where characters should be removed.

## Return types

Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from both sides. Returns `NULL` if input string is `NULL`.

## Remarks

By default, the `TRIM` function removes the space character from both the beginning and the ending ends of the string. This behavior is equivalent to `LTRIM(RTRIM(@string))`.

## Examples

### A. Remove the space character from both sides of string

The following example removes spaces from before and after the word `test`.

```sql
SELECT TRIM( '     test    ') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
test
```

### B. Remove specified characters from both sides of string

The following example removes a trailing period and spaces from before `#` and after the word `test`.

```sql
SELECT TRIM( '.,! ' FROM  '     #     test    .') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
#     test

```

## See also

- [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)
- [LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)
- [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)
- [RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)
- [STRING_SPLIT &#40;Transact-SQL&#41;](../../t-sql/functions/string-split-transact-sql.md)
- [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
