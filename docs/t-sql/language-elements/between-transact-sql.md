---
title: "BETWEEN (Transact-SQL)"
description: "BETWEEN is a Transact-SQL language element that specifies a range to test."
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/27/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "BETWEEN"
  - "BETWEEN_TSQL"
helpviewer_keywords:
  - "inclusive ranges"
  - "testing range"
  - "exclusive ranges"
  - "NOT BETWEEN operator"
  - "BETWEEN operator"
  - "range to test [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# BETWEEN (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Specifies a range to test.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
test_expression [ NOT ] BETWEEN begin_expression AND end_expression
```

## Arguments

#### *test_expression*

The [expression](../../t-sql/language-elements/expressions-transact-sql.md) to test for in the range defined by *begin_expression*and *end_expression*. *test_expression* must be the same data type as both *begin_expression* and *end_expression*.

#### NOT

Specifies that the result of the predicate is negated.

#### *begin_expression*

Any valid expression. *begin_expression* must be the same data type as both *test_expression* and *end_expression*.

#### *end_expression*

Any valid expression. *end_expression* must be the same data type as both *test_expression*and *begin_expression*.

#### AND

Acts as a placeholder that indicates *test_expression* should be within the range indicated by *begin_expression* and *end_expression*.

## Return types

**Boolean**

## Remarks

`BETWEEN` returns `TRUE` if the value of *test_expression* is greater than or equal to the value of *begin_expression* and less than or equal to the value of *end_expression*.

`NOT BETWEEN` returns `TRUE` if the value of *test_expression* is less than the value of *begin_expression* or greater than the value of *end_expression*.

To specify an exclusive range, use the greater than (`>`) and less than operators (`<`). If any input to the `BETWEEN` or `NOT BETWEEN` predicate is `NULL`, the result depends on the results of the constituent parts.  

In the following example `test_expression >= begin_expression AND test_expression <= end_expression`, if either part is `FALSE` then the overall `BETWEEN` expression evaluates to `FALSE`. Otherwise the expression evaluates to `UNKNOWN`.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use BETWEEN

The following example returns information about the database roles in a database. The first query returns all the roles. The second example uses the `BETWEEN` clause to limit the roles to the specified `database_id` values.

```sql
SELECT principal_id,
       name
FROM sys.database_principals
WHERE type = 'R';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
principal_id name
------------  ----
0             public
16384         db_owner
16385         db_accessadmin
16386         db_securityadmin
16387         db_ddladmin
16389         db_backupoperator
16390         db_datareader
16391         db_datawriter
16392         db_denydatareader
16393         db_denydatawriter
```

```sql
SELECT principal_id,
       name
FROM sys.database_principals
WHERE type = 'R'
      AND principal_id BETWEEN 16385 AND 16390;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
principal_id name
------------  ----
16385         db_accessadmin
16386         db_securityadmin
16387         db_ddladmin
16389         db_backupoperator
16390         db_datareader
```

### B. Use `>` and `<` instead of BETWEEN

The following example uses greater than (`>`) and less than (`<`) operators and, because these operators aren't inclusive, returns nine rows instead of 10 that were returned in the previous example.

```sql
SELECT e.FirstName,
       e.LastName,
       ep.Rate
FROM HumanResources.vEmployee AS e
     INNER JOIN HumanResources.EmployeePayHistory AS ep
         ON e.BusinessEntityID = ep.BusinessEntityID
WHERE ep.Rate > 27
      AND ep.Rate < 30
ORDER BY ep.Rate;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
 FirstName   LastName             Rate
 ---------   -------------------  ---------
 Paula       Barreto de Mattos    27.1394
 Janaina     Bueno                27.4038
 Dan         Bacon                27.4038
 Ramesh      Meyyappan            27.4038
 Karen       Berg                 27.4038
 David       Bradley              28.7500
 Hazem       Abolrous             28.8462
 Ovidiu      Cracium              28.8462
 Rob         Walters              29.8462
 ```

### C. Use NOT BETWEEN

The following example finds all rows outside a specified range of `27` through `30`.

```sql
SELECT e.FirstName,
       e.LastName,
       ep.Rate
FROM HumanResources.vEmployee AS e
     INNER JOIN HumanResources.EmployeePayHistory AS ep
         ON e.BusinessEntityID = ep.BusinessEntityID
WHERE ep.Rate NOT BETWEEN 27 AND 30
ORDER BY ep.Rate;
GO
```

### D. Use BETWEEN with datetime values

The following example retrieves rows in which **datetime** values are between `20011212` and `20020105`, inclusive.

```sql
SELECT BusinessEntityID,
       RateChangeDate
FROM HumanResources.EmployeePayHistory
WHERE RateChangeDate BETWEEN '20011212' AND '20020105';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
BusinessEntityID RateChangeDate
----------- -----------------------
3           2001-12-12 00:00:00.000
4           2002-01-05 00:00:00.000
```

The query retrieves the expected rows, because the date values in the query and the **datetime** values stored in the `RateChangeDate` column are specified without the time part of the date. When the time part is unspecified, it defaults to 12:00 A.M. A row that contains a time part that is after 12:00 A.M. on January 5, 2002, isn't returned by this query, because it falls outside the range.

## Related content

- [&gt; (Greater Than) (Transact-SQL)](greater-than-transact-sql.md)
- [&lt; (Less Than) (Transact-SQL)](less-than-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [What are the SQL database functions?](../functions/functions.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [WHERE (Transact-SQL)](../queries/where-transact-sql.md)
