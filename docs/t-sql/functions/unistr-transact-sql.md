---
title: "UNISTR (Transact-SQL)"
description: The UNISTR function provides support for Unicode string literals and returns the Unicode output for the input expression.
author: abhimantiwari
ms.author: abhtiwar
ms.reviewer: randolphwest, wiassaf, umajay
ms.date: 06/04/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "UNISTR"
  - "UNISTR_TSQL"
helpviewer_keywords:
  - "unicode escape sequence [SQL Server]"
  - "UNISTR function"
  - "unistr [SQL Server], UNISTR function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# UNISTR (Transact-SQL)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

`UNISTR` provides support for Unicode string literals by letting you specify the Unicode encoding value of characters in the string. `UNISTR` returns the Unicode characters, as defined by the Unicode standard, for the input expression.

The escape sequence for a Unicode character can be specified in the form of `\xxxx` or `\+xxxxxx`, where `xxxx` is a valid UTF-16 codepoint value, and `xxxxxx` is a valid Unicode codepoint value. You can look up Unicode codepoint values in the [Unicode Code Charts](https://www.unicode.org/charts).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
UNISTR ( 'character_expression' [ , 'unicode_escape_character' ] )
```

## Arguments

#### '*character_expression*'

An expression of any character type, such as **char**, **nchar**, **varchar**, or **nvarchar**. For **char** and **varchar** data types, the collation should be a valid UTF-8 collation.

#### N'*unicode_escape_character*'

A single character representing a user-defined Unicode escape sequence. If not supplied, the default value is `\`.

## Return types

A string value whose length and type depend on the input types.

## Examples

### A. Use UNISTR vs the NCHAR function

The following examples all use the `UNISTR` functions to perform the necessary conversion of the Unicode values into string character set, to display the unicode character Smiling Face With Open Mouth. The database collation must be a [UTF-8 collation](../../relational-databases/collations/collation-and-unicode-support.md) if the input is of
**char** or **varchar** data types.

Using `UNISTR` and `NCHAR`:

```sql
SELECT N'Hello! ' + NCHAR(0xd83d) + NCHAR(0xde00);
```

This sample example can also be written:

```sql
SELECT UNISTR(N'Hello! \D83D\DE00');

SELECT UNISTR(N'Hello! \+01F603');
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
Hello! 😃
```

### B. Use UNISTR function with user defined escape character

The following example uses the `UNISTR` function with a custom escape character to perform the necessary conversion of the Unicode into a string character set.

```sql
SELECT UNISTR(N'ABC#00C0#0181#0187', '#');
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
ABCÀƁƇ
```

## Related content

- [ASCII (Transact-SQL)](ascii-transact-sql.md)
- [CHAR (Transact-SQL)](char-transact-sql.md)
- [NCHAR (Transact-SQL)](nchar-transact-sql.md)
- [String Functions (Transact-SQL)](string-functions-transact-sql.md)
- [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md)
