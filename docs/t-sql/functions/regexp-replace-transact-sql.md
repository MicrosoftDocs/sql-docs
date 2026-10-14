---
title: "REGEXP_REPLACE (Transact-SQL)"
description: REGEXP_REPLACE Returns a modified source string replaced by a replacement string.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: abhtiwar, wiassaf, randolphwest
ms.date: 10/14/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# REGEXP_REPLACE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

The `REGEXP_REPLACE` scalar regular expression function returns a modified source string replaced by a replacement string, where the occurrence of the regular expression pattern found. If no matches are found, the function returns the original string.

```syntaxsql
REGEXP_REPLACE
(
    string_expression,
    pattern_expression [ , string_replacement [ , start [ , occurrence [ , flags ] ] ] ]
)
```

## Arguments

#### *string_expression*

[!INCLUDE [regexp-string-expression](../../includes/regexp-string-expression.md)]

#### *pattern_expression*

[!INCLUDE [regexp-pattern-expression](../../includes/regexp-pattern-expression.md)]

#### *string_replacement*

String expression that specifies the replacement string for matching substrings and replaces the substrings matched by the pattern. The `string_replacement` can be of **char**, **varchar**, **nchar**, **nvarchar**, **varchar(max)**, or **nvarchar(max)** data types (LOB types are supported up to 2 MB). If an empty string (`''`) is specified, the function removes all matched substrings and returns the resulting string. The default replacement string is the empty string (`''`).

The string_replacement can contain \n, where n is 1 through 9, to indicate that the source substring matching the n'th parenthesized group (subexpression) of the pattern should be inserted, and it can contain `&` to indicate that the substring matching the entire pattern should be inserted. Write \ if you need to put a literal backslash in the replacement text.

For example

```sql
REGEXP_REPLACE('123-456-7890', '(\d{3})-(\d{3})-(\d{4})', '(\1) \2-\3')
```

Returns:

```output
(123) 456-7890
```

If the provided `\n` in `string_replacement` is greater than the number of groups in the *pattern_expression*, then the function ignores the value.

For example:

```sql
REGEXP_REPLACE('123-456-7890', '(\d{3})-(\d{3})-(\d{4})', '(\1) (\4)-xxxx')
```

Returns:

```output
(123) ()-xxxx
```

#### *start*

Specify the starting position for the search within the search string. Optional. Type is **int** or **bigint**.

The numbering is 1-based, meaning the first character in the expression is `1` and the value must be `>= 1`. If the start expression is less than `1`, returns error. If the start expression is greater than the length of *string_expression*, the function returns *string_expression*. The default is `1`.

#### *occurrence*

An expression (positive integer) that specifies which occurrence of the pattern expression in the source string should be searched or replaced. The default value is `0`, which means all occurrences of the *pattern_expression* are replaced. For a positive integer `n`, it searches *string_expression* for the `n`th occurrence, continuing the search from the character immediately following each previous match.

#### *flags*

[!INCLUDE [regexp-flags-expression](../../includes/regexp-flags-expression.md)]

## Return value

Expression.

## Platform support

- In Azure SQL Managed Instance, T-SQL regular expression functions are available with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy) only.
- T-SQL regular expression functions are available in Azure SQL Database, SQL database in Fabric, and starting in SQL Server 2025.
- T-SQL regular expression functions aren't available in SQL Server 2022 or earlier versions.
- T-SQL regular expression scalar functions are available as a preview feature in Fabric Data Warehouse. For more information, see [Regular expressions functions in Fabric Data Warehouse](/fabric/data-warehouse/regular-expressions). T-SQL regular expression table-valued functions are not available in Fabric Data Warehouse. 

## Examples

Replace all occurrences of `a` or `e` with `X` in the product names.

```sql
SELECT REGEXP_REPLACE(PRODUCT_NAME, '[ae]', 'X', 1, 0, 'i')
FROM PRODUCTS;
```

Replace the first occurrence of `cat` or `dog` with `pet` in the product descriptions

```sql
SELECT REGEXP_REPLACE(PRODUCT_DESCRIPTION, 'cat|dog', 'pet', 1, 1, 'i')
FROM PRODUCTS;
```

Replace the last four digits of the phone numbers with asterisks

```sql
SELECT REGEXP_REPLACE(PHONE_NUMBER, '\d{4}$', '****')
FROM CUSTOMERS;
```

## Related content

- [Regular expressions functions](../../relational-databases/regular-expressions/overview.md)
