---
title: "REGEXP_SUBSTR (Transact-SQL)"
description: REGEXP_SUBSTR returns one occurrence of a substring of a string that matches the regular expression pattern.
author: uc-msft
ms.author: umajay
ms.reviewer: abhtiwar, wiassaf
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

# REGEXP_SUBSTR (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

The `REGEXP_SUBSTR` scalar regular expression function returns one occurrence of a substring of a string that matches the regular expression pattern. If no match is found, it returns `NULL`.

```syntaxsql
REGEXP_SUBSTR
(
    string_expression,
    pattern_expression [ , start [ , occurrence [ , flags [ , group ] ] ] ]
)
```

## Arguments

#### *string_expression*

[!INCLUDE [regexp-string-expression](../../includes/regexp-string-expression.md)]

#### *pattern_expression*

[!INCLUDE [regexp-pattern-expression](../../includes/regexp-pattern-expression.md)]

#### *start*

Specify the starting position for the search within the search string. Optional. Type is **int** or **bigint**.

The numbering is 1-based, meaning the first character in the expression is `1` and the value must be `>= 1`. If the start expression is less than `1`, returns error. If the start expression is greater than the length of *string_expression*, the function returns `NULL`. The default is `1`.

#### *occurrence*

An expression (positive integer) that specifies which occurrence of the pattern expression within the source string to be searched or replaced. Default is `1`. Searches at the first character of the *string_expression*. For a positive integer ***n***, it searches for the ***n*th** occurrence beginning with the first character following the first occurrence of the *pattern_expression*, and so forth.

#### *flags*

[!INCLUDE [regexp-flags-expression](../../includes/regexp-flags-expression.md)]

#### *group*

Specifies which capture group (*subexpression*) of a *pattern_expression* determines the position within *string_expression* to return. The capture group (*subexpression*) is a fragment of pattern enclosed in parentheses and can be nested.

The capture groups are numbered in the order in which their left parentheses appear. The data type of group is **int** and the value must be greater than or equal to `0`, and must not be greater than the number of capture groups (subexpressions) in *pattern_expression*. The default group value is `0`, which indicates that the position is based on the string that matches the entire pattern.

## Return value

String.

## Platform support

- In Azure SQL Managed Instance, T-SQL regular expression functions are available with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy) only.
- T-SQL regular expression functions are available in Azure SQL Database, SQL database in Fabric, and starting in SQL Server 2025.
- T-SQL regular expression functions aren't available in SQL Server 2022 or earlier versions.
- T-SQL regular expression scalar functions are available as a preview feature in Fabric Data Warehouse. For more information, see [Regular expressions functions in Fabric Data Warehouse](/fabric/data-warehouse/regular-expressions). T-SQL regular expression table-valued functions are not available in Fabric Data Warehouse. 

## Examples

Extract the domain name from an email address.

```sql
SELECT REGEXP_SUBSTR(EMAIL, '@(.+)$', 1, 1, 'i', 1) AS DOMAIN
FROM CUSTOMERS;
```

Find the first word in a sentence that starts with a vowel.

```sql
SELECT REGEXP_SUBSTR(COMMENT, '\b[aeiou]\w*', 1, 1, 'i') AS WORD
FROM FEEDBACK;
```

Get the last four digits of a credit card number.

```sql
SELECT REGEXP_SUBSTR(CARD_NUMBER, '\d{4}$') AS LAST_FOUR
FROM PAYMENTS;
```

## Related content

- [Regular expressions functions](../../relational-databases/regular-expressions/overview.md)
