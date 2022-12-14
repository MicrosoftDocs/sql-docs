---
title: "TRIM (Transact-SQL)"
description: "Removes the space character or other specified characters from the start and end of a string."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/14/2022
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
monikerRange: "= azure-sqldw-latest || = azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || >= sql-server-ver15 || >= sql-server-ver16 || >= sql-server-linux-ver15 || >= sql-server-linux-ver16 || = azuresqldb-mi-current"
---
# TRIM (Transact-SQL)

[!INCLUDE [sqlserver2017-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi-asa.md)]

Removes the space character `char(32)` or other specified characters from the start and end of a string.

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], optionally remove the space character `char(32)` or other specified characters from the start, end, or both sides of a string.
::: moniker-end

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15 || = azure-sqldw-latest || = azuresqldb-current || = azuresqldb-mi-current"
Syntax for SQL Server and Azure SQL Database:

```syntaxsql
TRIM ( [ characters FROM ] string )
```
::: moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later:

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the `LEADING`, `TRAILING`, or `BOTH` keywords.

```syntaxsql
TRIM ( [ LEADING | TRAILING | BOTH ] [characters FROM ] string )
```

Syntax for [!INCLUDE [ssazure_md](../../includes/ssazure_md.md)]:

```syntaxsql
TRIM ( [ characters FROM ] string )
```
::: moniker-end

Syntax for Azure Synapse Analytics:

```syntaxsql
TRIM ( string )
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
#### [ LEADING | TRAILING | BOTH ]

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.

The optional first argument specifies which side of the string to trim:

- **LEADING** removes characters specified from the start of a string.

- **TRAILING** removes characters specified from the end of a string.

- **BOTH** (default positional behavior) removes characters specified from the start and end of a string.
::: moniker-end

#### *characters*

A literal, variable, or function call of any non-LOB character type (`nvarchar`, `varchar`, `nchar`, or `char`) containing characters that should be removed. `nvarchar(max)` and `varchar(max)` types aren't allowed.

#### *string*

An expression of any character type (`nvarchar`, `varchar`, `nchar`, or `char`) where characters should be removed.

## Return types

Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from both sides. Returns `NULL` if input string is `NULL`.

## Remarks

By default, the `TRIM` function removes the space character from both the start and the end of the string. This behavior is equivalent to `LTRIM(RTRIM(@string))`.

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"
To enable the optional `LEADING`, `TRAILING`, or `BOTH` positional arguments in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you must enable database compatibility level `160` on the database(s) that you are connecting to when executing queries.

- With optional `LEADING` positional argument, the behavior is equivalent to `LTRIM(@string, characters)`.
- With optional `TRAILING` positional argument, the behavior is equivalent to `RTRIM(@string, characters)`.
::: moniker-end

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

The following example provides a list of possible characters to remove from a string.

```sql
SELECT TRIM( '.,! ' FROM '     #     test    .') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
#     test
```

In this example, only the trailing period and spaces from before `#` and after the word `test` were removed. The other characters were ignored because they didn't exist in the string.

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"

### C. Remove specified characters from the start of a string

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the `LEADING`, `TRAILING`, or `BOTH` keywords.

The following example removes the leading `.` from the start of the string before the word `test`.

```sql
SELECT TRIM(LEADING '.,! ' FROM  '     .#     test    .') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
# test .
```

### D. Remove specified characters from the end of a string

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the `LEADING`, `TRAILING`, or `BOTH` keywords.

The following example removes the trailing `.` from the end of the string after the word `test`.

```sql
SELECT TRIM(TRAILING '.,! ' FROM '     .#     test    .') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
.#     test
```

### E. Remove specified characters from the beginning and end of a string

> [!IMPORTANT]
> You will need your database compatibility level set to 160 to use the `LEADING`, `TRAILING`, or `BOTH` keywords.

The following example removes the characters `123` from the beginning and end of the string `123abc123`.

```sql
SELECT TRIM(BOTH '123' FROM '123abc123') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
abc
```
::: moniker-end

## See also

- [LEFT &#40;Transact-SQL&#41;](../../t-sql/functions/left-transact-sql.md)
- [LTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/ltrim-transact-sql.md)
- [RIGHT &#40;Transact-SQL&#41;](../../t-sql/functions/right-transact-sql.md)
- [RTRIM &#40;Transact-SQL&#41;](../../t-sql/functions/rtrim-transact-sql.md)
- [STRING_SPLIT &#40;Transact-SQL&#41;](../../t-sql/functions/string-split-transact-sql.md)
- [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
