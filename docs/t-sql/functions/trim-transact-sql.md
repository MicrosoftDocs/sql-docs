---
title: "TRIM (Transact-SQL)"
description: "Removes the space character or other specified characters from the start and end of a string."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 10/27/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "TRIM"
  - "TRIM_TSQL"
helpviewer_keywords:
  - "TRIM function"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# TRIM (Transact-SQL)

[!INCLUDE [sqlserver2017-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi-asa-fabricse-fabricdw.md)]

Removes the space character `char(32)` or other specified characters from the start and end of a string.

::: moniker range="=fabric"
Optionally removes the space character `char(32)` or other specified characters from the start, end, or both sides of a string.
::: moniker-end

::: moniker range="=azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], optionally removes the space character `char(32)` or other specified characters from the start, end, or both sides of a string.
::: moniker-end

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)]:

```syntaxsql
TRIM ( [ characters FROM ] string )
```

Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [fabric](../../includes/fabric.md)]:

::: moniker range="=azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || = azuresqldb-mi-current"
> [!IMPORTANT]  
> You need your database compatibility level set to `160` to use the `LEADING`, `TRAILING`, or `BOTH` keywords.
::: moniker-end

```syntaxsql
TRIM ( [ LEADING | TRAILING | BOTH ] [characters FROM ] string )
```

## Arguments

::: moniker range=">=sql-server-2017 || >=sql-server-linux-2017 || = azuresqldb-mi-current || =fabric"
#### [ LEADING | TRAILING | BOTH ]

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [fabric](../../includes/fabric.md)]:

The optional first argument specifies which side of the string to trim:

- `LEADING` removes characters specified from the start of a string.

- `TRAILING` removes characters specified from the end of a string.

- `BOTH` (default positional behavior) removes characters specified from the start and end of a string.
::: moniker-end

#### *characters*

A literal, variable, or function call of any non-LOB character type (**nvarchar**, **varchar**, **nchar**, or **char**) containing characters that should be removed. **nvarchar(max)** and **varchar(max)** types aren't allowed.

#### *string*

An expression of any character type (**nvarchar**, **varchar**, **nchar**, or **char**) where characters should be removed.

## Return types

Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from both sides. Returns `NULL` if input string is `NULL`.

## Remarks

By default, the `TRIM` function removes the space character from both the start and the end of the string. This behavior is equivalent to `LTRIM(RTRIM(@string))`.

::: moniker range="=azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || = azuresqldb-mi-current"
To enable the optional `LEADING`, `TRAILING`, or `BOTH` positional arguments in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you must enable database compatibility level `160` on the database that you're connecting to when executing queries.
::: moniker-end

- With optional `LEADING` positional argument, the behavior is equivalent to `LTRIM(@string, characters)`.
- With optional `TRAILING` positional argument, the behavior is equivalent to `RTRIM(@string, characters)`.

## Examples

### A. Remove the space character from both sides of string

The following example removes spaces from before and after the word `test`.

```sql
SELECT TRIM( '     test    ') AS Result;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
test
```

### B. Remove specified characters from both sides of string

The following example provides a list of possible characters to remove from a string.

```sql
SELECT TRIM( '.,! ' FROM '     #     test    .') AS Result;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
#     test
```

In this example, only the trailing period and spaces from before `#` and after the word `test` were removed. The other characters were ignored because they didn't exist in the string.

### C. Remove specified characters from the start of a string

::: moniker range="=azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || = azuresqldb-mi-current"
> [!IMPORTANT]  
> You need your database compatibility level set to `160` to use the `LEADING`, `TRAILING`, or `BOTH` keywords.
::: moniker-end
The following example removes the leading `.` from the start of the string before the word `test`.

```sql
SELECT TRIM(LEADING '.,! ' FROM  '     .#     test    .') AS Result;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
# test .
```

### D. Remove specified characters from the end of a string

::: moniker range="=azure-sqldw-latest || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
> [!IMPORTANT]  
> You need your database compatibility level set to `160` to use the `LEADING`, `TRAILING`, or `BOTH` keywords.
::: moniker-end

The following example removes the trailing `.` from the end of the string after the word `test`.

```sql
SELECT TRIM(TRAILING '.,! ' FROM '     .#     test    .') AS Result;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
.#     test
```

### E. Remove specified characters from the beginning and end of a string

::: moniker range="=azure-sqldw-latest || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
> [!IMPORTANT]  
> You need your database compatibility level set to `160` to use the `LEADING`, `TRAILING`, or `BOTH` keywords.
::: moniker-end

The following example removes the characters `123` from the beginning and end of the string `123abc123`.

```sql
SELECT TRIM(BOTH '123' FROM '123abc123') AS Result;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
abc
```

## Related content

- [LEFT (Transact-SQL)](left-transact-sql.md)
- [LTRIM (Transact-SQL)](ltrim-transact-sql.md)
- [RIGHT (Transact-SQL)](right-transact-sql.md)
- [RTRIM (Transact-SQL)](rtrim-transact-sql.md)
- [STRING_SPLIT (Transact-SQL)](string-split-transact-sql.md)
- [SUBSTRING (Transact-SQL)](substring-transact-sql.md)
- [String Functions (Transact-SQL)](string-functions-transact-sql.md)
