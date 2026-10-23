---
title: "REGEXP_INSTR (Transact-SQL)"
description: REGEXP_INSTR Returns the starting or ending position of the matched substring.
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

# REGEXP_INSTR (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

The `REGEXP_INSTR` scalar regular expression function returns the starting or ending position of the matched substring, depending on the value of the `return_option` argument.

```syntaxsql
REGEXP_INSTR
(
    string_expression,
    pattern_expression [ , start [ , occurrence [ , return_option [ , flags [ , group ] ] ] ] ]
)
```

## Arguments

#### *string_expression*

[!INCLUDE [regexp-string-expression](../../includes/regexp-string-expression.md)]

#### *pattern_expression*

[!INCLUDE [regexp-pattern-expression](../../includes/regexp-pattern-expression.md)]

#### *start*

Specifies the starting position for the search within the search string. Optional. Type is **int** or **bigint**.

The numbering is 1-based, meaning the first character in the expression is `1` and the value must be `>= 1`. If the start expression is less than `1`, returns error. If the start expression is greater than the length of *string_expression*, the function returns `0`. The default is `1`.

#### *occurrence*

An expression (positive integer) that specifies which occurrence of the pattern expression within the source string to be searched or replaced. Default is `1`. Searches at the first character of the *string_expression*. For a positive integer `n`, it searches for the `nth` occurrence beginning with the first character following the first occurrence of the *pattern_expression*, and so forth.

#### *return_option*

Specifies whether to return the beginning or ending position of the matched substring. Use `0` for the beginning, and `1` for the end. The default value is `0`. The query returns error for any other value.

#### *flag*

[!INCLUDE [regexp-flags-expression](../../includes/regexp-flags-expression.md)]

#### *group*

Specifies which capture group (`subexpression`) of a *pattern_expression* determines the position within *string_expression* to return. The group is a fragment of pattern enclosed in parentheses and can be nested. The groups are numbered in the order in which their left parentheses appear in pattern. The value is an integer and must be `>= 0` and must not be greater than the number of groups in the *pattern_expression*. The default value is `0`, which indicates that the position is based on the string that matches the entire *pattern_expression*.

If the value is greater than the number of groups in *pattern_expression*, the function returns `0`.

## Return value

**int**

## Platform support

- In Azure SQL Managed Instance, T-SQL regular expression functions are available with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy) only.
- T-SQL regular expression functions are available in Azure SQL Database, SQL database in Fabric, and starting in SQL Server 2025.
- T-SQL regular expression functions aren't available in SQL Server 2022 or earlier versions.
- T-SQL regular expression scalar functions are available as a preview feature in Fabric Data Warehouse. For more information, see [Regular expressions functions in Fabric Data Warehouse](/fabric/data-warehouse/regular-expressions). T-SQL regular expression table-valued functions are not available in Fabric Data Warehouse. 

## Examples

Find the position of the first substring that contains only digits in the `PRODUCT_DESCRIPTION` column.

```sql
SELECT REGEXP_INSTR(PRODUCT_DESCRIPTION, '\d+')
FROM PRODUCTS;
```

Find the position of the third occurrence of the letter `a` (case-insensitive) in the `PRODUCT_NAME` column.

```sql
SELECT REGEXP_INSTR(PRODUCT_NAME, 'a', 1, 3, 0, 'i')
FROM PRODUCTS;
```

Find the position of the end of the first substring that starts with `t` and ends with `e` (case-sensitive) in the `PRODUCT_DESCRIPTION` column.

```sql
SELECT REGEXP_INSTR(PRODUCT_DESCRIPTION, 't.*?e', 1, 1, 1)
FROM PRODUCTS;
```

## Related content

- [Regular expressions functions](../../relational-databases/regular-expressions/overview.md)
