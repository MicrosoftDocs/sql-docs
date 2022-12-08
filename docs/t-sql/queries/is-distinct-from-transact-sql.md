---
title: "IS [NOT] DISTINCT FROM (Transact-SQL)"
description: "Transact-SQL reference for the IS [NOT] DISTINCT FROM language element. Determine whether two expressions evaluate to NULL"
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "IS_[NOT]_DISTINCT_FROM_TSQL"
  - "IS [NOT] DISTINCT FROM"
  - "IS_NOT_DISTINCT_FROM_TSQL"
  - "IS NOT DISTINCT FROM"
  - "IS_DISTINCT_FROM_TSQL"
  - "IS DISTINCT FROM"
helpviewer_keywords:
  - "IS [NOT] DISTINCT FROM predicate (Transact-SQL)"
  - "conditions [SQL Server], IS [NOT] DISTINCT FROM"
dev_langs:
  - "TSQL"
---

# IS [NOT] DISTINCT FROM (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022.md)]

Compares the equality of two expressions and guarantees a true or false result, even if one or both operands are NULL.

IS [NOT] DISTINCT FROM is a predicate used in the search condition of [WHERE](../../t-sql/queries/where-transact-sql.md) clauses and [HAVING](../../t-sql/queries/select-having-transact-sql.md) clauses, the join conditions of [FROM](../../t-sql/queries/from-transact-sql.md) clauses, and other constructs where a Boolean value is required.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression IS [NOT] DISTINCT FROM expression
```

## Arguments

#### *expression*

Any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md).

The expression can be a column, a constant, a function, a variable, a scalar subquery, or any combination of column names, constants, and functions connected by an operator or operators, or a subquery.

## Remarks

Comparing a NULL value to any other value, including another NULL, will have an unknown result. IS [NOT] DISTINCT FROM will always return true or false, as it will treat NULL values as known values when used as a comparison operator.

The following sample table uses values `A` and `B` to illustrate the behavior of IS [NOT] DISTINCT FROM:

| A | B | A = B | A IS NOT DISTINCT FROM B |
| :---: | :---: | :---: | :---: |
| 0 | 0 | True | True |
| 0 | 1 | False | False |
| 0 | NULL | Unknown | False |
| NULL | NULL | Unknown | True |

When executing a query that contains IS [NOT] DISTINCT FROM against linked servers, the query text sent to the linked server will vary, based on whether we can determine that the linked server has the capability to parse the syntax.

If we determine that the linked server can parse IS [NOT] DISTINCT FROM, we will decode the syntax as-is. If we can't determine that a linked server can parse IS [NOT] DISTINCT FROM, we will decode to the following expressions:

`A IS DISTINCT FROM B` will decode to: `((A <> B OR A IS NULL OR B IS NULL) AND NOT (A IS NULL AND B IS NULL))`

`A IS NOT DISTINCT FROM B` will decode to: `(NOT (A <> B OR A IS NULL OR B IS NULL) OR (A IS NULL AND B IS NULL))`

## Examples

### A. Use IS DISTINCT FROM

The following example returns rows where the `id` field is distinct from the integer value of 17.

```sql
USE tempdb;
GO
DROP TABLE IF EXISTS #SampleTempTable;
GO
CREATE TABLE #SampleTempTable (id INT, message nvarchar(50));
INSERT INTO #SampleTempTable VALUES (null, 'hello') ;
INSERT INTO #SampleTempTable VALUES (10, null);
INSERT INTO #SampleTempTable VALUES (17, 'abc');
INSERT INTO #SampleTempTable VALUES (17, 'yes');
INSERT INTO #SampleTempTable VALUES (null, null);
GO

SELECT * FROM #SampleTempTable WHERE id IS DISTINCT FROM 17;
```

The results exclude all rows where `id` matched the value of 17.

```output
id          message
----------- ---------
NULL        hello
10          NULL
NULL        NULL
```

### B. Use IS NOT DISTINCT FROM

The following example returns rows where the `id` field isn't distinct from the integer value of 17.

```sql
USE tempdb;
GO
DROP TABLE IF EXISTS #SampleTempTable;
GO
CREATE TABLE #SampleTempTable (id INT, message nvarchar(50));
INSERT INTO #SampleTempTable VALUES (null, 'hello') ;
INSERT INTO #SampleTempTable VALUES (10, null);
INSERT INTO #SampleTempTable VALUES (17, 'abc');
INSERT INTO #SampleTempTable VALUES (17, 'yes');
INSERT INTO #SampleTempTable VALUES (null, null);
GO

SELECT * FROM #SampleTempTable WHERE id IS NOT DISTINCT FROM 17;
```

The results return only the rows where the `id` matched the value of 17.

```output
id          message
----------- --------
17          abc
17          yes
```

### C. Use IS DISTINCT FROM against a NULL value

The following example returns rows where the `id` field is distinct from NULL.

```sql
USE tempdb;
GO
DROP TABLE IF EXISTS #SampleTempTable;
GO
CREATE TABLE #SampleTempTable (id INT, message nvarchar(50));
INSERT INTO #SampleTempTable VALUES (null, 'hello') ;
INSERT INTO #SampleTempTable VALUES (10, null);
INSERT INTO #SampleTempTable VALUES (17, 'abc');
INSERT INTO #SampleTempTable VALUES (17, 'yes');
INSERT INTO #SampleTempTable VALUES (null, null);
GO

SELECT * FROM #SampleTempTable WHERE id IS DISTINCT FROM NULL;
```

The results return only the rows where the `id` wasn't NULL.

```output
id          message
----------- --------
10          NULL
17          abc
17          yes
```

### D. Use IS NOT DISTINCT FROM against a NULL value

The following example returns rows where the `id` field isn't distinct from NULL.

```sql
USE tempdb;
GO
DROP TABLE IF EXISTS #SampleTempTable;
GO
CREATE TABLE #SampleTempTable (id INT, message nvarchar(50));
INSERT INTO #SampleTempTable VALUES (null, 'hello') ;
INSERT INTO #SampleTempTable VALUES (10, null);
INSERT INTO #SampleTempTable VALUES (17, 'abc');
INSERT INTO #SampleTempTable VALUES (17, 'yes');
INSERT INTO #SampleTempTable VALUES (null, null);
GO

SELECT * FROM #SampleTempTable WHERE id IS NOT DISTINCT FROM NULL;
```

The results return only the rows where the `id` was NULL.

```output
id          message
----------- --------
NULL        hello
NULL        NULL
```

## See also

- [Predicates](predicates.md)
- [CONTAINS (Transact-SQL)](contains-transact-sql.md)
- [FREETEXT (Transact-SQL)](freetext-transact-sql.md)
- [IS &#91;NOT&#93; NULL (Transact-SQL)](is-null-transact-sql.md)
