---
title: "RTRIM (Transact-SQL)"
description: "RTRIM (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/14/2022
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
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || >= sql-server-ver15 || >= sql-server-ver16 || >= sql-server-linux-ver15 || >= sql-server-linux-ver16 || = azuresqldb-mi-current"
---
# RTRIM (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15 || = azure-sqldw-latest || = azuresqldb-current || = azuresqldb-mi-current"
Returns a character string after truncating all trailing spaces.
::: moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
Removes space character `char(32)` or other specified characters from the end of a string.
::: moniker-end

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15 || = azure-sqldw-latest || = azuresqldb-current || = azuresqldb-mi-current"
```syntaxsql
RTRIM ( character_expression )
```
::: moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later:

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the optional *characters* argument.

```syntaxsql
RTRIM ( character_expression , [ characters ] )
```

Syntax for [!INCLUDE [ssazure_md](../../includes/ssazure_md.md)]:

```syntaxsql
RTRIM ( character_expression )
```
::: moniker-end

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *character_expression*

An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character or binary data. *character_expression* can be a constant, variable, or column. *character_expression* must be of a data type, except **text**, **ntext**, and **image**, that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *character_expression*.

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
#### *characters*

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later.

A literal, variable, or function call of any non-LOB character type (**nvarchar**, **varchar**, **nchar**, or **char**) containing characters that should be removed. **nvarchar(max)** and **varchar(max)** types aren't allowed.
::: moniker-end

## Return types

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15 || = azure-sqldw-latest || = azuresqldb-current || = azuresqldb-mi-current"
**varchar** or **nvarchar**
::: moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from the end of a *character_expression*. Returns `NULL` if input string is `NULL`.

## Remarks

To enable the optional *characters* positional argument, enable database compatibility level `160` on the database(s) that you are connecting to when executing queries.
::: moniker-end

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
- [LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)
- [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)
- [STRING_SPLIT &#40;Transact-SQL&#41;](../../t-sql/functions/string-split-transact-sql.md)
- [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)
- [TRIM &#40;Transact-SQL&#41;](../../t-sql/functions/trim-transact-sql.md)
- [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
- [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
