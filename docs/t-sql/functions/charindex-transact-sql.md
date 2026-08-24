---
title: "CHARINDEX (Transact-SQL)"
description: "Transact-SQL reference for the CHARINDEX function."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/14/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CHARINDEX"
  - "CHARINDEX_TSQL"
helpviewer_keywords:
  - "expressions [SQL Server], pattern searching"
  - "CHARINDEX function"
  - "pattern searching [SQL Server]"
  - "starting point of expression in character string"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# CHARINDEX (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

This function searches for one character expression inside a second character expression, returning the starting position of the first expression if found.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CHARINDEX ( expressionToFind , expressionToSearch [ , start_location ] )
```

## Arguments

#### *expressionToFind*

A character [expression](../language-elements/expressions-transact-sql.md) containing the sequence to find. *expressionToFind* has an 8,000 character limit.

#### *expressionToSearch*

A character expression to search.

#### *start_location*

An **integer** or **bigint** expression at which the search starts. If *start_location* isn't specified, has a negative value, or has a zero (`0`) value, the search starts at the beginning of *expressionToSearch*.

## Return types

**bigint** if *expressionToSearch* has an **nvarchar(max)**, **varbinary(max)**, or **varchar(max)** data type; **int** otherwise.

## Remarks

If either the *expressionToFind* or *expressionToSearch* expression has a Unicode data type (**nchar** or **nvarchar**), and the other expression doesn't, the `CHARINDEX` function converts that other expression to a Unicode data type. `CHARINDEX` can't be used with **image**, **ntext**, or **text** data types.

If either the *expressionToFind* or *expressionToSearch* expression has a `NULL` value, `CHARINDEX` returns `NULL`.

If `CHARINDEX` doesn't find *expressionToFind* within *expressionToSearch*, `CHARINDEX` returns `0`.

`CHARINDEX` performs comparisons based on the input collation. To perform a comparison in a specified collation, use `COLLATE` to apply an explicit collation to the input.

The starting position returned is 1-based, not 0-based.

`0x0000` (**char(0)**) is an undefined character in Windows collations and can't be included in `CHARINDEX`.

## Supplementary Characters (Surrogate Pairs)

When using supplementary character (SC) collations, both *start_location* and the return value count surrogate pairs as one character, not two. For more information, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

## Examples

### A. Return the starting position of an expression

This example searches for `bicycle` in the searched string value variable `@document`.

```sql
DECLARE @document AS VARCHAR (64);

SELECT @document = 'Reflectors are vital safety' +
    ' components of your bicycle.';

SELECT CHARINDEX('bicycle', @document);
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
48
```

### B. Search from a specific position

This example uses the optional *start_location* parameter to start the search for `vital` at the fifth character of the searched string value variable `@document`.

```sql
DECLARE @document AS VARCHAR (64);

SELECT @document = 'Reflectors are vital safety' +
    ' components of your bicycle.';

SELECT CHARINDEX('vital', @document, 5);
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
16
```

### C. Search for a nonexistent expression

This example shows the result set when `CHARINDEX` doesn't find *expressionToFind* within *expressionToSearch*.

```sql
DECLARE @document AS VARCHAR (64);

SELECT @document = 'Reflectors are vital safety' +
    ' components of your bicycle.';

SELECT CHARINDEX('bike', @document);
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
0
```

### D. Perform a case-sensitive search

This example shows a case-sensitive search for the string `TEST` in searched string `This is a Test`.

```sql
USE tempdb;
GO

--perform a case sensitive search
SELECT CHARINDEX('TEST', 'This is a Test' COLLATE Latin1_General_CS_AS);
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
0
```

This example shows a case-sensitive search for the string `Test` in searched string `This is a Test`.

```sql
USE tempdb;
GO

SELECT CHARINDEX('Test', 'This is a Test' COLLATE Latin1_General_CS_AS);
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
11
```

### E. Perform a case-insensitive search

This example shows a case-insensitive search for the string `TEST` in searched string `This is a Test`.

```sql
USE tempdb;
GO

SELECT CHARINDEX('TEST', 'This is a Test' COLLATE Latin1_General_CI_AS);
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
11
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### F. Search from the start of a string expression

This example returns the first location of the string `is` in string `This is a string`, starting from position 1 (the first character) of `This is a string`.

```sql
SELECT CHARINDEX('is', 'This is a string');
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
---------
3
```

### G. Search from a position other than the first position

This example returns the first location of the string `is` in string `This is a string`, starting the search from position 4 (the fourth character).

```sql
SELECT CHARINDEX('is', 'This is a string', 4);
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
---------
 6
 ```

### H. Results when the string isn't found

This example shows the return value when `CHARINDEX` doesn't find string *string_pattern* in the searched string.

```sql
SELECT TOP (1) CHARINDEX('at', 'This is a string')
FROM dbo.DimCustomer;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
---------
0
```

## Related content

- [LEN (Transact-SQL)](len-transact-sql.md)
- [PATINDEX (Transact-SQL)](patindex-transact-sql.md)
- [+ (String concatenation) (Transact-SQL)](../language-elements/string-concatenation-transact-sql.md)
- [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md)
