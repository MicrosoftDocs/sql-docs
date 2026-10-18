---
title: "REGEXP_COUNT (Transact-SQL)"
description: REGEXP_COUNT returns the number of times that a matched expression pattern exists in a string.
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

# REGEXP_COUNT (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

The `REGEXP_COUNT` scalar regular expression function counts the number of times that a regular expression pattern is matched in a string.

```syntaxsql
REGEXP_COUNT
(
    string_expression,
    pattern_expression [ , start [ , flags ] ]
)
```

## Arguments

#### *string_expression*

[!INCLUDE [regexp-string-expression](../../includes/regexp-string-expression.md)]

#### *pattern_expression*

[!INCLUDE [regexp-pattern-expression](../../includes/regexp-pattern-expression.md)]

#### *start*

Specify the starting position for the search within the search string. Optional. Type is **int** or **bigint**.

The numbering is 1-based, meaning the first character in the expression is `1` and the value must be `>= 1`. If the start expression is less than `1`, the returned *pattern_expression* begins at the first character that is specified in *string_expression*. If the start expression is greater than the length of *string_expression*, the function returns `0`. The default is `1`.

If the start expression is less than `1`, the query returns an error.

#### *flags*

[!INCLUDE [regexp-flags-expression](../../includes/regexp-flags-expression.md)]

## Return value

**int**

## Platform support

- In Azure SQL Managed Instance, T-SQL regular expression functions are available with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy) only.
- T-SQL regular expression functions are available in Azure SQL Database, SQL database in Fabric, and starting in SQL Server 2025.
- T-SQL regular expression functions aren't available in SQL Server 2022 or earlier versions.
- T-SQL regular expression scalar functions are available as a preview feature in Fabric Data Warehouse. For more information, see [Regular expressions functions in Fabric Data Warehouse](/fabric/data-warehouse/regular-expressions). T-SQL regular expression table-valued functions are not available in Fabric Data Warehouse. 

## Examples

Count how many times the letter `a` appears in each product name.

```sql
SELECT PRODUCT_NAME,
       REGEXP_COUNT(PRODUCT_NAME, 'a') AS A_COUNT
FROM PRODUCTS;
```

Count how many products have a name that ends with `ing`.

```sql
SELECT COUNT(*)
FROM PRODUCTS
WHERE REGEXP_COUNT(PRODUCT_NAME, 'ing$') > 0;
```

Count how many products have a name that contains three consecutive consonants, ignoring case.

```sql
SELECT COUNT(*)
FROM PRODUCTS
WHERE REGEXP_COUNT(PRODUCT_NAME, '[^aeiou]{3}', 1, 'i') > 0;
```

## Related content

- [Regular expressions functions](../../relational-databases/regular-expressions/overview.md)
