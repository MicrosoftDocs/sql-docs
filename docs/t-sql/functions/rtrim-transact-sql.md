---
title: "RTRIM (Transact-SQL)"
description: "RTRIM (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/08/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "RTRIM_TSQL"
  - "RTRIM"
helpviewer_keywords:
  - "RTRIM function"
  - "character strings [SQL Server], trailing blanks"
  - "blank characters [SQL Server]"
  - "trailing blanks"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || >= sql-server-ver15 || >= sql-server-ver16 || >= sql-server-linux-ver15 || >= sql-server-linux-ver16 || = azuresqldb-mi-current ||=fabric"
---
# RTRIM (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15 "
Returns a character string after truncating all trailing spaces.
::: moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16 || = azure-sqldw-latest || = azuresqldb-current || = azuresqldb-mi-current ||=fabric"
Removes space character `char(32)` or other specified characters from the end of a string.
::: moniker-end

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]:

```syntaxsql
RTRIM ( character_expression )
```

Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later, [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)], and [!INCLUDE [fabric](../../includes/fabric.md)]:

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the optional *characters* argument.

```syntaxsql
RTRIM ( character_expression , [ characters ] )
```

## Arguments

#### *character_expression*

An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character or binary data. *character_expression* can be a constant, variable, or column. *character_expression* must be of a data type, except **text**, **ntext**, and **image**, that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *character_expression*.

#### *characters*

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later.

A literal, variable, or function call of any non-LOB character type (**nvarchar**, **varchar**, **nchar**, or **char**) containing characters that should be removed. **nvarchar(max)** and **varchar(max)** types aren't allowed.

## Return types

Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from the end of a *character_expression*. Returns `NULL` if input string is `NULL`.

## Remarks

To enable the optional *characters* positional argument, enable database compatibility level `160` on the database(s) that you are connecting to when executing queries.

## Examples

### A. Remove trailing spaces

The following example takes a string of characters that has spaces at the end of the sentence, and returns the text without the spaces at the end of the sentence.

```sql
SELECT RTRIM('Removes trailing spaces.   ');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

  `Removes trailing spaces.`

### B. Remove trailing spaces with a variable

The following example demonstrates how to use `RTRIM` to remove trailing spaces from a character variable.

```sql
DECLARE @string_to_trim VARCHAR(60);  
SET @string_to_trim = 'Four spaces are after the period in this sentence.    ';  
SELECT @string_to_trim + ' Next string.';  
SELECT RTRIM(@string_to_trim) + ' Next string.';  
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Four spaces are after the period in this sentence.     Next string.

Four spaces are after the period in this sentence. Next string.
```

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"

### C. Remove specified characters from the end of a string

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the optional *characters* argument.

The following example removes the characters `abc.` from the end of the `.123abc.` string.

```sql
SELECT RTRIM('.123abc.' , 'abc.');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
.123
```
::: moniker-end

## See also

- [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)
- [TRIM (Transact-SQL)](trim-transact-sql.md)
- [LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)
- [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)
- [STRING_SPLIT &#40;Transact-SQL&#41;](../../t-sql/functions/string-split-transact-sql.md)
- [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)
- [TRIM &#40;Transact-SQL&#41;](../../t-sql/functions/trim-transact-sql.md)
- [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
- [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
