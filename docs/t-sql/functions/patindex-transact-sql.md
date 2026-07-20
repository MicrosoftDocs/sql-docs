---
title: "PATINDEX (Transact-SQL)"
description: PATINDEX returns the starting position of the first occurrence of a pattern in a specified expression, or zero.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 07/18/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "PATINDEX"
  - "PATINDEX_TSQL"
helpviewer_keywords:
  - "first occurrence of pattern [SQL Server]"
  - "searches [SQL Server], pattern starting position"
  - "starting position of patten search"
  - "pattern searching [SQL Server]"
  - "PATINDEX function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# PATINDEX (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns the starting position of the first occurrence of a pattern in a specified expression, or zero if the pattern isn't found, on all valid text and character data types.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
PATINDEX ( '%pattern%' , expression )
```

## Arguments

#### *pattern*

A character expression that contains the sequence to be found. Wildcard characters can be used; however, the % character must come before and follow *pattern* (except when you search for first or last characters). *pattern* is an expression of the character string data type category. *pattern* is limited to 8,000 characters.

> [!NOTE]  
> While traditional regular expressions aren't natively supported in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, similar complex pattern matching can be achieved by using various wildcard expressions. See the [String operators](../language-elements/string-operators-transact-sql.md) documentation for more detail on wildcard syntax. For information about regular expression functions in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], see [Regular expressions functions](regular-expressions-functions-transact-sql.md).

#### *expression*

An [expression](../language-elements/expressions-transact-sql.md), typically a column that is searched for the specified pattern. *expression* is of the character string data type category.

## Return types

**bigint** if *expression* is of the **varchar(max)** or **nvarchar(max)** data types; otherwise **int**.

## Remarks

If *pattern* is `NULL`, `PATINDEX` returns `NULL`.

If expression is `NULL`, `PATINDEX` returns an error.

The starting position for `PATINDEX` is `1`.

`PATINDEX` performs comparisons based on the collation of the input. To perform a comparison in a specified collation, you can use `COLLATE` to apply an explicit collation to the input.

## Supplementary characters (Surrogate pairs)

When you use collations with supplementary characters (SC), the return value counts any UTF-16 surrogate pairs in the *expression* parameter as a single character. For more information, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

`0x0000` (**char(0)**) is an undefined character in Windows collations and can't be included in `PATINDEX`.

## Examples

### A. Basic PATINDEX example

The following example checks a short character string (`interesting data`) for the starting location of the characters `ter`.

```sql
SELECT PATINDEX('%ter%', 'interesting data') AS position;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
position
--------
3
```

### B. Use a pattern with PATINDEX

The following example finds the position at which the pattern `ensure` starts in a specific row of the `DocumentSummary` column in the `Document` table in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
SELECT PATINDEX('%ensure%', DocumentSummary) AS position
FROM Production.Document
WHERE DocumentNode = 0x7B40;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
position
--------
64
```

If you don't restrict the rows to be searched by using a `WHERE` clause, the query returns all rows in the table and reports nonzero values for those rows in which the pattern was found, and zero for all rows in which the pattern wasn't found.

### C. Use wildcard characters with PATINDEX

The following example uses % and _ wildcards to find the position at which the pattern `'en'`, followed by any one character and `'ure'` starts in the specified string (index starts at 1):

```sql
SELECT PATINDEX('%en_ure%', 'Please ensure the door is locked!') AS position;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
position
--------
8
```

`PATINDEX` works just like `LIKE`, so you can use any of the wildcards. You don't have to enclose the pattern between percents. `PATINDEX('a%', 'abc')` returns 1 and `PATINDEX('%a', 'cba')` returns 3.

Unlike `LIKE`, `PATINDEX` returns a position, similar to what `CHARINDEX` does.

### D. Use complex wildcard expressions with PATINDEX

The following example uses the `[^]` [string operator](../language-elements/wildcard-character-s-not-to-match-transact-sql.md) to find the position of a character that isn't a number, letter, or space.

```sql
SELECT PATINDEX('%[^ 0-9A-Za-z]%', 'Please ensure the door is locked!') AS position;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
position
--------
33
```

### E. Use COLLATE with PATINDEX

The following example uses the `COLLATE` function to explicitly specify the collation of the expression that is searched.

```sql
USE tempdb;
GO

SELECT PATINDEX('%ein%', 'Das ist ein Test' COLLATE Latin1_General_BIN);
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
position
--------
9
```

### F. Use a variable to specify the pattern

The following example uses a variable to pass a value to the *pattern* parameter. This example uses the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
DECLARE @MyValue AS VARCHAR (10) = 'safety';

SELECT PATINDEX('%' + @MyValue + '%', DocumentSummary) AS position
FROM Production.Document
WHERE DocumentNode = 0x7B40;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
position
--------
22
```

## Related content

- [LIKE (Transact-SQL)](../language-elements/like-transact-sql.md)
- [CHARINDEX (Transact-SQL)](charindex-transact-sql.md)
- [LEN (Transact-SQL)](len-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [\&#91; \&#93; (Wildcard - characters to match) (Transact-SQL)](../language-elements/wildcard-character-s-to-match-transact-sql.md)
- [\&#91;^\&#93; (Wildcard - characters not to match) (Transact-SQL)](../language-elements/wildcard-character-s-not-to-match-transact-sql.md)
- [_ (Wildcard - match one character) (Transact-SQL)](../language-elements/wildcard-match-one-character-transact-sql.md)
- [Percent character (wildcard - characters to match) (Transact-SQL)](../language-elements/percent-character-wildcard-character-s-to-match-transact-sql.md)

