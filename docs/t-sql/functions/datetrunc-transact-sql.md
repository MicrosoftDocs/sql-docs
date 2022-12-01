---
title: "DATETRUNC (Transact-SQL)"
description: "Transact-SQL reference for the DATETRUNC function. This function returns an input date-related value truncated to a specified datepart."
author: aashnabafna-ms
ms.author: aashnabafna
ms.reviewer: derekw, maghan, randolphwest
ms.date: 09/13/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATETRUNC_TSQL"
  - "DATETRUNC"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# DATETRUNC (Transact-SQL)

[!INCLUDE[SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], this function returns an input *date* truncated to a specified *datepart*.

## Syntax

```syntaxsql
DATETRUNC ( datepart, date )
```

## Arguments

#### *datepart*

Specifies the precision for truncation. This table lists all the valid *datepart* values for `DATETRUNC`, given that it's also a valid part of the input date type.

|*datepart*|Abbreviations|Truncation notes|
|---|---|---|
|**year**|**yy**, **yyyy**|
|**quarter**|**qq**, **q**|
|**month**|**mm**, **m**|
|**dayofyear**|**dy**, **y**|*dayofyear* is truncated in the same manner as *day*  
|**day**|**dd**, **d**|*day* is truncated in the same manner as *dayofyear*  
|**week**|**wk**, **ww**|Truncate to the first day of the week. In T-SQL, the first day of the week is defined by the `@@DATEFIRST` T-SQL setting. For a U.S. English environment, `@@DATEFIRST` defaults to 7 (Sunday).
|**iso\_week**|**isowk, isoww**|Truncate to the first day of an ISO Week. The first day of the week in the ISO8601 calendar system is Monday.  
|**hour**|**hh**|
|**minute**|**mi, n**|
|**second**|**ss**, **s**|
|**millisecond**|**ms**|
|**microsecond**|**mcs**|

> [!NOTE]  
> The *weekday*, *timezoneoffset*, and *nanosecond* T-SQL dateparts are not supported for `DATETRUNC`.

#### *date*

Accepts any expression, column, or user-defined variable that can resolve to any valid T-SQL date or time type. Valid types are:

- **smalldatetime**
- **datetime**
- **date**
- **time**
- **datetime2**
- **datetimeoffset**

Don't confuse the *date* parameter with the **date** data type.

`DATETRUNC` will also accept a string literal (of any string type) that can resolve to a **datetime2(7)**.

## Return type

The returned data type for `DATETRUNC` is dynamic. `DATETRUNC` returns a truncated date of the same data type (and, if applicable, the same fractional time scale) as the input date. For example, if `DATETRUNC` was given a **datetimeoffset(3)** input date, it would return a **datetimeoffset(3)**. If it was given a string literal that could resolve to a **datetime2(7)**, `DATETRUNC` would return a **datetime2(7)**.

## Fractional time scale precision

Milliseconds have a fractional time scale of 3 (`.123`), microseconds have a fractional time scale of 6 (`.123456`), and nanoseconds have a fractional time scale of 9 (`.123456789`). The **time**, **datetime2**, and **datetimeoffset** data types allow a maximum fractional time scale of 7 (`.1234567`). Therefore, to truncate to the `millisecond` *datepart*, the fractional time scale must be at least 3. Similarly, to truncate to the `microsecond` *datepart*, the fractional time scale must be at least 6. `DATETRUNC` doesn't support the `nanosecond` *datepart* since no T-SQL date type supports a fractional time scale of 9.

## Examples

### A. Use different *datepart* options

The following examples illustrate the use of various *datepart* options:

```sql
DECLARE @d datetime2 = '2021-12-08 11:30:15.1234567';
SELECT 'Year', DATETRUNC(year, @d);
SELECT 'Quarter', DATETRUNC(quarter, @d);
SELECT 'Month', DATETRUNC(month, @d);
SELECT 'Week', DATETRUNC(week, @d); -- Using the default DATEFIRST setting value of 7 (U.S. English)
SELECT 'Iso_week', DATETRUNC(iso_week, @d);
SELECT 'DayOfYear', DATETRUNC(dayofyear, @d);
SELECT 'Day', DATETRUNC(day, @d);
SELECT 'Hour', DATETRUNC(hour, @d);
SELECT 'Minute', DATETRUNC(minute, @d);
SELECT 'Second', DATETRUNC(second, @d);
SELECT 'Millisecond', DATETRUNC(millisecond, @d);
SELECT 'Microsecond', DATETRUNC(microsecond, @d);
```

Here's the result set:

```output
Year        2021-01-01 00:00:00.0000000
Quarter     2021-10-01 00:00:00.0000000
Month       2021-12-01 00:00:00.0000000
Week        2021-12-05 00:00:00.0000000
Iso_week    2021-12-06 00:00:00.0000000
DayOfYear   2021-12-08 00:00:00.0000000
Day         2021-12-08 00:00:00.0000000
Hour        2021-12-08 11:00:00.0000000
Minute      2021-12-08 11:30:00.0000000
Second      2021-12-08 11:30:15.0000000
Millisecond 2021-12-08 11:30:15.1230000
Microsecond 2021-12-08 11:30:15.1234560
```

### B. @@DATEFIRST setting

The following examples illustrate the use of the `@@DATEFIRST` setting with the `week` *datepart*:

```sql
DECLARE @d datetime2 = '2021-11-11 11:11:11.1234567';

SELECT 'Week-7', DATETRUNC(week, @d); -- Uses the default DATEFIRST setting value of 7 (U.S. English)

SET DATEFIRST 6;
SELECT 'Week-6', DATETRUNC(week, @d);

SET DATEFIRST 3;
SELECT 'Week-3', DATETRUNC(week, @d);
```

Here's the result set:

```output
Week-7  2021-11-07 00:00:00.0000000
Week-6  2021-11-06 00:00:00.0000000
Week-3  2021-11-10 00:00:00.0000000
```

### C. Date literals

The following examples illustrate the use of *date* parameter literals:

```sql
SELECT DATETRUNC(month, '1998-03-04');

SELECT DATETRUNC(millisecond, '1998-03-04 10:10:05.1234567');

DECLARE @d1 char(200) = '1998-03-04';
SELECT DATETRUNC(millisecond, @d1);

DECLARE @d2 nvarchar(max) = '1998-03-04 10:10:05';
SELECT DATETRUNC(minute, @d2);
```

Here's the result set (all the results are of type **datetime2(7)**):

```output
1998-03-01 00:00:00.0000000
1998-03-04 10:10:05.1230000
1998-03-04 00:00:00.0000000
1998-03-04 10:10:00.0000000
```

### D. Variables and the *date* parameter

The following example illustrates the use of the *date* parameter:

```sql
DECLARE @d datetime2 = '1998-12-11 02:03:04.1234567';
SELECT DATETRUNC(day, @d);
```

Here's the result:

```output
1998-12-11 00:00:00.0000000
```

### E. Columns and the *date* parameter

The `TransactionDate` column from the `Sales.CustomerTransactions` table serves as an example *column* argument for the *date* parameter:

```sql
USE WideWorldImporters;

SELECT CustomerTransactionID,
    DATETRUNC(month, TransactionDate) AS MonthTransactionOccurred,
    InvoiceID,
    CustomerID,
    TransactionAmount,
    SUM(TransactionAmount) OVER (
        PARTITION BY CustomerID ORDER BY TransactionDate,
            CustomerTransactionID ROWS UNBOUNDED PRECEDING
        ) AS RunningTotal,
    TransactionDate AS ActualTransactionDate
FROM [WideWorldImporters].[Sales].[CustomerTransactions]
WHERE InvoiceID IS NOT NULL
    AND DATETRUNC(month, TransactionDate) >= '2015-12-01';
```

### F. Expressions and the *date* parameter

The *date* parameter accepts any expression that can resolve to a T-SQL date type or any string literal that can resolve to a **datetime2(7)**. The `TransactionDate` column from the `Sales.CustomerTransactions` table serves as an artificial argument to exemplify the use of an *expression* for the *date* parameter:

```sql
SELECT DATETRUNC(m, SYSDATETIME());

SELECT DATETRUNC(yyyy, CONVERT(date, '2021-12-1'));

USE WideWorldImporters;
GO
SELECT DATETRUNC(month, DATEADD(month, 4, TransactionDate))
FROM Sales.CustomerTransactions;
GO
```

### G. Truncate a *date* to a *datepart* representing its maximum precision

If the *datepart* has the same unit maximum precision as the input date type, truncating the input date to this *datepart* would have no effect.

#### Example 1

```sql
DECLARE @d datetime = '2015-04-29 05:06:07.123';
SELECT 'Input', @d;
SELECT 'Truncated', DATETRUNC(millisecond, @d);
```

Here's the result set, which illustrates that the input **datetime** and the truncated *date* parameter are the same:

```output
Input     2015-04-29 05:06:07.123
Truncated 2015-04-29 05:06:07.123
```

#### Example 2

```sql
DECLARE @d date = '2050-04-04';
SELECT 'Input', @d;
SELECT 'Truncated', DATETRUNC(day, @d);
```

Here's the result set, which illustrates that the input **datetime** and the truncated *date* parameter are the same:

```output
Input     2050-04-04
Truncated 2050-04-04
```

#### Example 3: smalldatetime precision

**smalldatetime** is only precise up to the nearest minute, even though it has a field for seconds. Therefore, truncating it to the nearest minute or the nearest second would have no effect.

```sql
DECLARE @d smalldatetime = '2009-09-11 12:42:12'
SELECT 'Input', @d;
SELECT 'Truncated to minute', DATETRUNC(minute, @d)
SELECT 'Truncated to second', DATETRUNC(second, @d);
```

The result set illustrates that the input **smalldatetime** value is the same as both the truncated values:

```output
Input                2009-09-11 12:42:00
Truncated to minute  2009-09-11 12:42:00
Truncated to second  2009-09-11 12:42:00
```

#### Example 4: datetime precision

**datetime** is only precise up to 3.33 milliseconds.  Therefore, truncating a **datetime** to a millisecond may yield results that are different than what the user expects. However, this truncated value is the same as the internally stored **datetime** value.

```sql
DECLARE @d datetime = '2020-02-02 02:02:02.002';
SELECT 'Input', @d;
SELECT 'Truncated', DATETRUNC(millisecond, @d);
```

Here's the result set, which illustrates that the truncated *date* is the same as the stored *date*. This may be different what you expect based on the `DECLARE` statement.

```output
Input     2020-02-02 02:02:02.003
Truncated 2020-02-02 02:02:02.003
```

## Remarks

A `DATE TOO SMALL` error is thrown if the *date* truncation attempts to backtrack to a date before the minimum date supported by that data type. This only occurs when using the `week` *datepart*. It can't occur when using the `iso_week` *datepart*, since all the T-SQL date types coincidentally use a Monday for their minimum dates. Here's an example with the corresponding result error message:

```sql
DECLARE @d date= '0001-01-01 00:00:00';
SELECT DATETRUNC(week, @d);
```

```output
Msg 9837, Level 16, State 3, Line 84
An invalid date value was encountered: The date value is less than the minimum date value allowed for the data type.
```

A `DATEPART` error is thrown if the *datepart* used isn't supported by the `DATETRUNC` function or the input date data type. This can occur when:

1. A *datepart* not supported by `DATETRUNC` is used (namely, `weekday`, `tzoffset`, or `nanosecond`)

1. A **time**-related *datepart* is used with the **date** data type or a **date**-related *datepart* is used with the **time** data type. Here's an example with the corresponding result error message:

    ```sql
    DECLARE @d time = '12:12:12.1234567';
    SELECT DATETRUNC(year, @d);
    ```

    ```output
    Msg 9810, Level 16, State 10, Line 78
    The datepart year is not supported by date function datetrunc for data type time.
    ```

1. The *datepart* requires a higher fractional time scale precision than what is supported by the data type (See [Fractional time scale precision](#fractional-time-scale-precision)). Here's an example with the corresponding result error message:

    ```sql
    DECLARE @d datetime2(3) = '2021-12-12 12:12:12.12345';
    SELECT DATETRUNC(microsecond, @d);
    ```

    ```output
    Msg 9810, Level 16, State 11, Line 81
    The datepart microsecond is not supported by date function datetrunc for data type datetime2.
    ```

## See also

- [@@DATEFIRST (Transact-SQL)](datefirst-transact-sql.md)
- [DATEPART (Transact-SQL)](datepart-transact-sql.md)
- [Date and time data types and functions (Transact-SQL)](date-and-time-data-types-and-functions-transact-sql.md)
