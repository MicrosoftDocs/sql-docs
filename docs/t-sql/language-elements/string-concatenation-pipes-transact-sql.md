---
title: "|| (String concatenation) (Transact-SQL)"
description: "Use || to concatenate two or more character or binary strings, columns, or a combination of strings and column names into one expression (a string operator)."
author: abhimantiwari
ms.author: abhtiwar
ms.reviewer: randolphwest, wiassaf, umajay
ms.date: 06/03/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "concatenation"
  - "string"
helpviewer_keywords:
  - "concatenation [SQL Server]"
  - "string concatenation operator"
  - "|| (string concatenation)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---

# || (String concatenation) (Transact-SQL)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

The `||` pipes operator in a string expression concatenates two or more character or binary strings, columns, or a combination of strings and column names into one expression (a string operator). For example, `SELECT 'SQL ' || 'Server';` returns `SQL Server`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression || expression
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types in the character and binary data type category, except the **xml**, **json**, **image**, **ntext**, or **text** data types. Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression.

## Return types

Returns the data type of the argument with the highest precedence. For more information, see [Data type precedence](../data-types/data-type-precedence-transact-sql.md).

## Remarks

If the result of the concatenation of strings exceeds the limit of 8,000 bytes, the result is truncated. However, if at least one of the strings concatenated is a large value type, truncation doesn't occur.

### Zero-length strings and characters

The `||` (string concatenation) operator behaves differently when it works with an empty, zero-length string than when it works with `NULL`, or unknown values. A zero-length character string can be specified as two single quotation marks without any characters inside the quotation marks. A zero-length binary string can be specified as `0x` without any byte values specified in the hexadecimal constant. Concatenating a zero-length string always concatenates the two specified strings.

### Concatenation of NULL values

As with arithmetic operations that are performed on `NULL` values, when a `NULL` value is added to a known value, the result is typically a `NULL` value. A string concatenation operation performed with a `NULL` value should also produce a `NULL` result.

The `||` operator doesn't honor the `SET CONCAT_NULL_YIELDS_NULL` option, and always behaves as if the ANSI SQL behavior is enabled, yielding `NULL` if any of the inputs is `NULL`. This is the primary difference in behavior between the `+` and `||` concatenation operators. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../statements/set-concat-null-yields-null-transact-sql.md).

### Use of CAST and CONVERT when necessary

An explicit conversion to character data must be used when concatenating binary strings and any characters between the binary strings.

The following examples show when `CONVERT`, or `CAST`, must be used with binary concatenation and when `CONVERT`, or `CAST`, doesn't have to be used.

In this example, no `CONVERT` or `CAST` function is required because this example concatenates two binary strings.

```sql
DECLARE @mybin1 VARBINARY(5), @mybin2 VARBINARY(5);

SET @mybin1 = 0xFF;
SET @mybin2 = 0xA5;

-- No CONVERT or CAST function is required because this example
-- concatenates two binary strings.
SELECT @mybin1 || @mybin2
```

In this example, a `CONVERT` or `CAST` function is required because this example concatenates two binary strings plus a space.

```sql
DECLARE @mybin1 VARBINARY(5), @mybin2 VARBINARY(5);

SET @mybin1 = 0xFF;
SET @mybin2 = 0xA5;

-- A CONVERT or CAST function is required because this example
-- concatenates two binary strings plus a space.
SELECT CONVERT(VARCHAR(5), @mybin1) || ' '
    || CONVERT(VARCHAR(5), @mybin2);

-- Here is the same conversion using CAST.
SELECT CAST(@mybin1 AS VARCHAR(5)) || ' '
    || CAST(@mybin2 AS VARCHAR(5));
```

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use string concatenation

The following example creates a single column under the column heading `Name` from multiple character columns, with the family name (`LastName`) of the person followed by a comma, a single space, and then the first name (`FirstName`) of the person. The result set is in ascending, alphabetical order by the family name, and then by the first name.

```sql
SELECT (LastName || ', ' || FirstName) AS Name
FROM Person.Person
ORDER BY LastName ASC, FirstName ASC;
```

### B. Combine numeric and date data types

The following example uses the `CONVERT` function to concatenate **numeric** and **date** data types.

```sql
SELECT 'The order is due on ' || CONVERT(VARCHAR(12), DueDate, 101)
FROM Sales.SalesOrderHeader
WHERE SalesOrderID = 50001;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
------------------------------------------------
The order is due on 04/23/2007
```

### C. Use multiple string concatenations

The following example concatenates multiple strings to form one long string to display the family name and the first initial of the vice presidents at [!INCLUDE [ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)]. A comma is added after the family name and a period after the first initial.

```sql
SELECT (LastName || ',' + SPACE(1) || SUBSTRING(FirstName, 1, 1) || '.') AS Name, e.JobTitle
FROM Person.Person AS p
    JOIN HumanResources.Employee AS e
    ON p.BusinessEntityID = e.BusinessEntityID
WHERE e.JobTitle LIKE 'Vice%'
ORDER BY LastName ASC;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Name               Title
-------------      ---------------`
Duffy, T.          Vice President of Engineering
Hamilton, J.       Vice President of Production
Welcker, B.        Vice President of Sales
```

### D. Use large strings in concatenation

The following example concatenates multiple strings to form one long string and then tries to compute the length of the final string. The final length of result set is 16,000, because expression evaluation starts from left that is, `@x` + `@z` + `@y` => (`@x + @z`) + `@y`. In this case, the result of (`@x` + `@z`) is truncated at 8,000 bytes, and then `@y` is added to the result set, which makes the final string length 16,000. Since `@y` is a large value type string, truncation doesn't occur.

```sql
DECLARE @x VARCHAR(8000) = REPLICATE('x', 8000);
DECLARE @y VARCHAR(MAX) = REPLICATE('y', 8000);
DECLARE @z VARCHAR(8000) = REPLICATE('z', 8000);

SET @y = @x || @z || @y;

-- The result of following select is 16000
SELECT LEN(@y) AS y;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
y
-------
16000
```

## Related content

- [&#124;&#124;= (Compound assignment) (Transact-SQL)](compound-assignment-pipes-transact-sql.md)
- [+ (String concatenation) (Transact-SQL)](string-concatenation-transact-sql.md)
- [+= (String Concatenation Assignment) (Transact-SQL)](string-concatenation-equal-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
- [Data type conversion (Database Engine)](../data-types/data-type-conversion-database-engine.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Built-in Functions (Transact-SQL)](../functions/functions.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [SET Statements (Transact-SQL)](../statements/set-statements-transact-sql.md)
