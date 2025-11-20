---
title: "REGEXP_LIKE (Transact-SQL)"
description: REGEXP_LIKE Returns a Boolean value that indicates whether the text input matches the regex pattern.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: abhtiwar, wiassaf, randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# REGEXP_LIKE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Indicates if the regular expression pattern matches in a string.

```syntaxsql
REGEXP_LIKE
(
    string_expression,
    pattern_expression [ , flags ]
)
```

`REGEXP_LIKE` requires database compatibility level 170 and above. If the database compatibility level is lower than 170, `REGEXP_LIKE` isn't available. Other [regular expression scalar functions](regular-expressions-functions-transact-sql.md) are available at all compatibility levels.

You can check compatibility level in the `sys.databases` view or in database properties. You can change the compatibility level of a database with the following command:

```sql
ALTER DATABASE [DatabaseName]
    SET COMPATIBILITY_LEVEL = 170;
```

> [!NOTE]  
> Regular expressions are available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Arguments

#### *string_expression*

[!INCLUDE [regexp-string-expression](../../includes/regexp-string-expression.md)]

#### *pattern_expression*

[!INCLUDE [regexp-pattern-expression](../../includes/regexp-pattern-expression.md)]

#### *flags*

[!INCLUDE [regexp-flags-expression](../../includes/regexp-flags-expression.md)]

## Return value

Boolean value. `true` or `false`.

## Remarks

### Cardinality estimation

To enhance the accuracy of [cardinality estimation](../../relational-databases/performance/cardinality-estimation-sql-server.md) for the `REGEXP_LIKE` function, you can use the `ASSUME_FIXED_MIN_SELECTIVITY_FOR_REGEXP` and `ASSUME_FIXED_MAX_SELECTIVITY_FOR_REGEXP` query hints to adjust the default selectivity values. For more information, see [Query hints](../queries/hints-transact-sql-query.md#use_hint).

These query hints are also integrated with [Cardinality estimation (CE) feedback](../../relational-databases/performance/intelligent-query-processing-cardinality-estimation-feedback.md). The CE feedback model automatically identifies queries using `REGEXP_LIKE` function where there's a significant difference between estimated and actual row counts. It then applies the appropriate selectivity hint at the query level to improve plan quality without requiring manual input.

To disable the automatic feedback behavior, enable trace flag 16268.

## Examples

Select all records from the `Employees` table where the first name starts with `A` and ends with `Y`

```sql
SELECT *
FROM Employees
WHERE REGEXP_LIKE (FIRST_NAME, '^A.*Y$');
```

Select all records from the `Employees` table where the first name starts with `A` and ends with `Y` using case-insensitive mode

```sql
SELECT *
FROM Employees
WHERE REGEXP_LIKE (FIRST_NAME, '^A.*Y$', 'i');
```

Select all records from the `Orders` table where the order date is in February 2020:

```sql
SELECT *
FROM Orders
WHERE REGEXP_LIKE (ORDER_DATE, '2020-02-\d\d');
```

Select all records from the `Products` table where the product name contains at least three consecutive vowels:

```sql
SELECT *
FROM Products
WHERE REGEXP_LIKE (PRODUCT_NAME, '[AEIOU]{3,}');
```

Create employees table with `CHECK` constraints for `Email` and `Phone_Number` columns:

```sql
DROP TABLE IF EXISTS Employees;
CREATE TABLE Employees
(
    ID INT IDENTITY (101, 1),
    [Name] VARCHAR (150),
    Email VARCHAR (320)
        CHECK (REGEXP_LIKE (Email, '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$')),
    Phone_Number NVARCHAR (20)
        CHECK (REGEXP_LIKE (Phone_Number, '^(\d{3})-(\d{3})-(\d{4})$'))
);
```

## Related content

- [Regular expressions](../../relational-databases/regular-expressions/overview.md)
- [Regular expressions functions (Transact-SQL)](regular-expressions-functions-transact-sql.md)
