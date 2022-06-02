---
title: "GENERATE_SERIES (Transact-SQL)"
description: "GENERATE_SERIES (Transact-SQL)"
author: kendalvandyke
ms.author: kendalv
ms.reviewer: randolphwest
ms.date: 05/09/2022
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, sql-edge, pdw"
ms.technology: t-sql
ms.topic: reference
ms.custom:
  - "event-tier1-build-2022"
f1_keywords:
  - "GENERATE_SERIES"
  - "GENERATE_SERIES_TSQL"
helpviewer_keywords:
  - "GENERATE_SERIES function"
  - "analytic functions, GENERATE_SERIES"
dev_langs:
  - "TSQL"
---
# GENERATE_SERIES (Transact-SQL)

[!INCLUDE [sqlserver2022-asde](../../includes/applies-to-version/sqlserver2022-asde.md)]

Generates a series of numbers within a given interval. The interval and the step between series values are defined by the user.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
GENERATE_SERIES
(
    START = @start | start_literal | numeric_expression
    , STOP = @stop | stop_literal | numeric_expression
    [, STEP = @step | step_literal | numeric_expression ]
)
```

## Arguments

#### START

The `START` parameter is the first value in the interval. The `START` parameter is specified as a variable, a literal, or a scalar expression of type **tinyint**, **smallint**, **int**, **bigint**, **decimal**, or **numeric**.

#### STOP

The `STOP` parameter is the last value in the interval. The `STOP` parameter is specified as a variable, a literal, or a scalar expression of type **tinyint**, **smallint**, **int**, **bigint**, **decimal**, or **numeric**. The series stops once the last generated step value exceeds the `STOP` value.

The data type for `STOP` **must** match the data type for `START`.

#### [ STEP ]

The `STEP` parameter indicates the number of values to increment or decrement between steps in the series. The `STEP` parameter is an expression of type **tinyint**, **smallint**, **int**, **bigint**, **decimal**, or **numeric**. `STEP` can be either negative or positive, but can't be zero (`0`)).

This argument is optional. The default value for `STEP` is 1 if `START` is less than `STOP`, otherwise, the default value is -1 if `START` is greater than `STOP`.

If `START` is less than `STOP` and a negative value is specified for `STEP`, or if `START` is greater than `STOP` and a positive value is specified for `STEP`, an empty result set will be returned.

## Return types

Returns a single-column table containing a sequence of values in which each differs from the preceding by `STEP`. The name of the column is `value`. The output is the same type as `START` and `STOP`.

## Permissions

No permissions are required for `GENERATE_SERIES`; however, the user needs `EXECUTE` permission on the database, and permission to query any data that is used as inputs.

## Examples

The following examples demonstrate the syntax for calling `GENERATE_SERIES`.

### A. Generate a series of integer values between 1 and 100 in increments of 1 (default)

```sql
SELECT value
FROM GENERATE_SERIES(START = 1, STOP = 100);
```

Here's the result set.

```output
value
-----------
1
2
3
4
5
6
7
8
9
10
```

### B. Generate a series of integer values between 1 and 50 in increments of 5

```sql
SELECT value
FROM GENERATE_SERIES(START = 1, STOP = 50, STEP = 5);
```

Here's the result set.

```output
value
-----------
1
6
11
16
21
26
31
36
41
46
```

### C. Generate a series of decimal values between 0.0 and 1.0 in increments of 0.1

```sql
DECLARE @start decimal(2, 1) = 0.0;
DECLARE @stop decimal(2, 1) = 1.0;
DECLARE @step decimal(2, 1) = 0.1;

SELECT value
FROM GENERATE_SERIES(START = @start, STOP = @stop, STEP = @step);
```

Here's the result set.

```output
value
---------------------------------------
0.0
0.1
0.2
0.3
0.4
0.5
0.6
0.7
0.8
0.9
1.0
```

## See also

- [SEQUENCES (Transact-SQL)](../../relational-databases/system-information-schema-views/sequences-transact-sql.md)
- [Relational operators (Transact-SQL)](../language-elements/relational-operators-transact-sql.md)
