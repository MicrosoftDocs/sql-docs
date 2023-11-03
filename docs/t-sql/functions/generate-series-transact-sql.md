---
title: "GENERATE_SERIES (Transact-SQL)"
description: "Generates a series of numbers within a given interval. The interval and the step between series values are defined by the user."
author: kendalvandyke
ms.author: kendalv
ms.reviewer: randolphwest
ms.date: 10/27/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GENERATE_SERIES"
  - "GENERATE_SERIES_TSQL"
helpviewer_keywords:
  - "GENERATE_SERIES function"
  - "analytic functions, GENERATE_SERIES"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"

---
# GENERATE_SERIES (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricse-fabricdw.md)]

Generates a series of numbers within a given interval. The interval and the step between series values are defined by the user.

::: moniker range="=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
#### Compatibility level 160

`GENERATE_SERIES` requires the compatibility level to be at least 160. When the compatibility level is less than 160, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] is unable to find the `GENERATE_SERIES` function.

To change the compatibility level of a database, refer to [View or change the compatibility level of a database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md).
::: moniker-end

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
GENERATE_SERIES ( start , stop [ , step ] )
```

## Arguments

#### *start*

The first value in the interval. *start* is specified as a variable, a literal, or a scalar [expression](../language-elements/expressions-transact-sql.md) of type **tinyint**, **smallint**, **int**, **bigint**, **decimal**, or **numeric**.

#### *stop*

The last value in the interval. *stop* is specified as a variable, a literal, or a scalar [expression](../language-elements/expressions-transact-sql.md) of type **tinyint**, **smallint**, **int**, **bigint**, **decimal**, or **numeric**. The series stops once the last generated step value exceeds the *stop* value.

The data type for *stop* **must** match the data type for *start*.

#### [ *step* ]

Indicates the number of values to increment or decrement between steps in the series. *step* is an [expression](../language-elements/expressions-transact-sql.md) of type **tinyint**, **smallint**, **int**, **bigint**, **decimal**, or **numeric**. *step* can be either negative or positive, but can't be zero (`0`).

This argument is optional. The default value for *step* is 1 if *start* is less than *stop*, otherwise, the default value is -1 if *start* is greater than *stop*.

If *start* is less than *stop* and a negative value is specified for *step*, or if *start* is greater than *stop* and a positive value is specified for *step*, an empty result set is returned.

## Return types

Returns a single-column table containing a sequence of values in which each differs from the preceding by *step*. The name of the column is `value`. The output is the same type as *start* and *stop*.

## Permissions

No permissions are required for `GENERATE_SERIES`. However, the user needs EXECUTE permission on the database, and permission to query any data that is used as inputs.

## Examples

The following examples demonstrate the syntax for calling `GENERATE_SERIES`.

### A. Generate a series of integer values between 1 and 10 in increments of 1 (default)

```sql
SELECT value
FROM GENERATE_SERIES(1, 10);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

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
FROM GENERATE_SERIES(1, 50, 5);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

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
FROM GENERATE_SERIES(@start, @stop, @step);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

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

## Related content

- [SEQUENCES (Transact-SQL)](../../relational-databases/system-information-schema-views/sequences-transact-sql.md)
- [Relational operators (Transact-SQL)](../language-elements/relational-operators-transact-sql.md)
