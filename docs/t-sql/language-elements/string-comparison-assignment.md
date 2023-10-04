---
title: "= (String comparison or assignment)"
description: "Compares two strings in a WHERE or HAVING clause or sets a variable or column to string or result of a string operation on the right side of the equation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: jopilov
ms.date: 06/07/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "string comparison [Transact-SQL], string compare"
  - "string comparison, string assignment"
dev_langs:
  - "TSQL"
---
# = (String comparison or assignment)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Compares two strings in a `WHERE` or `HAVING` clause or sets a variable or column to string or result of a string operation on the right side of the equation. For example, if a variable `@x` equals `'Adventure'`, then `WHERE @x = 'Adventure'` compares original value of `@x` is equal to the string value `'Adventure'` exactly. Also you can use the `=` operator as an assignment operator. For example, you can call `SET @a = 'AdventureWorks'`.

## Syntax

```syntaxsql
expression = expression
```

## Arguments

#### *expression*

Specifies any valid [expression](expressions-transact-sql.md) of any one of the data types in the character and binary data type category, except the **image**, **ntext**, or **text** data types. Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression.

An explicit conversion to character data with `CONVERT`, or `CAST` must be used when comparing or assigning binary strings and any characters between the binary strings.

## Remarks

String comparison using the `=` operator assumes that both strings are identical. For partial string comparison options, refer to the [LIKE](like-transact-sql.md) operator, or the [CONTAINS](../queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) full text predicates.

The [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] follows the ANSI/ISO SQL-92 specification (Section 8.2, *Comparison Predicate*, General rules #3) on how to compare strings with spaces. The ANSI standard requires padding for the character strings used in comparisons so that their lengths match before comparing them. The padding directly affects the semantics of `WHERE` and `HAVING` clause predicates and other Transact-SQL string comparisons. For example, Transact-SQL considers the strings `'abc'` and `'abc '` to be equivalent for most comparison operations. The only exception to this rule is the [LIKE predicate](like-transact-sql.md). When the right side of a `LIKE` predicate expression features a value with a trailing space, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] doesn't pad the two values to the same length before the comparison occurs. Because the purpose of the `LIKE` predicate, by definition, is to facilitate pattern searches rather than simple string equality tests, this predicate doesn't violate the section of the ANSI SQL-92 specification mentioned earlier.

The `SET ANSI_PADDING` setting doesn't affect whether the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] pads strings before it compares them. `SET ANSI_PADDING` only affects whether trailing blanks are trimmed from values being inserted into a table, so it affects storage but not comparisons.

## Examples

### A. Compare strings in a WHERE clause

```sql
SELECT LastName,
    FirstName
FROM Person.Person
WHERE LastName = 'Johnson';
```

### B. Compare strings in a WHERE clause using conversion from binary

```sql
DECLARE @LNameBin BINARY (100) = 0x5A68656E67;

SELECT LastName,
    FirstName
FROM Person.Person
WHERE LastName = CONVERT(VARCHAR, @LNameBin);
```

### C. String assignment to a variable

This example illustrates a simple assignment of string data to a variable using the = operator.

```sql
DECLARE @dbname VARCHAR(100);

SET @dbname = 'Adventure';
```

### D. String comparison with spaces

The following queries illustrate the comparison between strings where one side contains spaces and the other doesn't:

```sql
CREATE TABLE #tmp (c1 VARCHAR(10));
GO

INSERT INTO #tmp VALUES ('abc ');

INSERT INTO #tmp VALUES ('abc');
GO

SELECT DATALENGTH(c1) AS 'EqualWithSpace', * FROM #tmp
WHERE c1 = 'abc ';

SELECT DATALENGTH(c1) AS 'EqualNoSpace  ', * FROM #tmp
WHERE c1 = 'abc';

SELECT DATALENGTH(c1) AS 'GTWithSpace   ', * FROM #tmp
WHERE c1 > 'ab ';

SELECT DATALENGTH(c1) AS 'GTNoSpace     ', * FROM #tmp
WHERE c1 > 'ab';

SELECT DATALENGTH(c1) AS 'LTWithSpace   ', * FROM #tmp
WHERE c1 < 'abd ';

SELECT DATALENGTH(c1) AS 'LTNoSpace     ', * FROM #tmp
WHERE c1 < 'abd';

SELECT DATALENGTH(c1) AS 'LikeWithSpace ', * FROM #tmp
WHERE c1 LIKE 'abc %';

SELECT DATALENGTH(c1) AS 'LikeNoSpace   ', * FROM #tmp
WHERE c1 LIKE 'abc%';
GO

DROP TABLE #tmp;
GO
```

## Next steps

- [SET ANSI_PADDING (Transact-SQL)](../statements/set-ansi-padding-transact-sql.md)
- [String operators (Transact-SQL)](string-operators-transact-sql.md)
