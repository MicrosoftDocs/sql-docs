---
title: "LTRIM (Transact-SQL)"
description: "LTRIM (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/14/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "LTRIM"
  - "LTRIM_TSQL"
helpviewer_keywords:
  - "leading blanks"
  - "deleting blank spaces"
  - "characters [SQL Server], blanks"
  - "removing blank spaces"
  - "LTRIM function"
  - "blank characters [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# LTRIM (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15 || = azure-sqldw-latest || = azuresqldb-current || = azuresqldb-mi-current"
Returns a character string after truncating all leading spaces.
::: moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
Removes space character `char(32)` or other specified characters from the start of a string.
::: moniker-end

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15 || = azure-sqldw-latest || = azuresqldb-current || = azuresqldb-mi-current"
```syntaxsql
LTRIM ( character_expression )
```
::: moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later:

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the optional *characters* argument.

```syntaxsql
LTRIM ( character_expression , [ characters ] )
```

Syntax for [!INCLUDE [ssazure_md](../../includes/ssazure_md.md)]:

```syntaxsql
LTRIM ( character_expression )
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
Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from the beginning of a *character_expression*. Returns `NULL` if input string is `NULL`.

## Remarks

To enable the optional *characters* positional argument, enable database compatibility level `160` on the database(s) that you are connecting to when executing queries.
::: moniker-end

## Examples

### A. Remove leading spaces

The following example uses LTRIM to remove leading spaces from a character expression.

```sql
SELECT LTRIM('     Five spaces are at the beginning of this string.');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
---------------------------------------------------------------  
  Five spaces are at the beginning of this string.
  ```

### B: Remove leading spaces using a variable

The following example uses `LTRIM` to remove leading spaces from a character variable.

```sql
DECLARE @string_to_trim VARCHAR(60);  
SET @string_to_trim = '     Five spaces are at the beginning of this string.';  
SELECT  
    @string_to_trim AS 'Original string',
    LTRIM(@string_to_trim) AS 'Without spaces';  
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Original string	                                        Without spaces
-----------------------------------------------------   ---------------------------------------------
     Five spaces are at the beginning of this string.	Five spaces are at the beginning of this string.
```

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
### C. Remove specified characters from the beginning of a string

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the optional *characters* argument.

The following example removes the characters `123` from the beginning of the `123abc.` string.

```sql
SELECT LTRIM('123abc.' , '123.');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
abc.
```
::: moniker-end

## See also

- [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)
- [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)
- [RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)
- [STRING_SPLIT &#40;Transact-SQL&#41;](../../t-sql/functions/string-split-transact-sql.md)
- [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)
- [TRIM &#40;Transact-SQL&#41;](../../t-sql/functions/trim-transact-sql.md)
- [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
