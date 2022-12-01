---
title: "DATE_BUCKET (Transact-SQL)"
description: "DATE_BUCKET (Transact-SQL)"
author: kendalvandyke
ms.author: kendalv
ms.reviewer: randolphwest
ms.date: 05/09/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "DATE_BUCKET"
  - "DATE_BUCKET_TSQL"
helpviewer_keywords:
  - "DATE_BUCKET function"
  - "analytic functions, DATE_BUCKET"
dev_langs:
  - "TSQL"
---
# DATE_BUCKET (Transact-SQL)

[!INCLUDE [sqlserver2022-asde](../../includes/applies-to-version/sqlserver2022-asdb-asmi-asde.md)]

This function returns the date-time value corresponding to the start of each date-time bucket from the timestamp defined by the `origin` parameter, or the default origin value of `1900-01-01 00:00:00.000` if the origin parameter isn't specified.

See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all Transact-SQL date and time data types and functions.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DATE_BUCKET (datepart, number, date [, origin ] )
```

## Arguments

#### *datepart*

The part of *date* that is used with the `number` parameter, for example, year, month, day, minute, second.

`DATE_BUCKET` doesn't accept user-defined variable equivalents for the *datepart* arguments.

|*datepart*|Abbreviations|
|---|---|
|**day**|**dd**, **d**|
|**week**|**wk**, **ww**|
|**month**|**mm**, **m**|
|**quarter**|**qq**, **q**|
|**year**|**yy**, **yyyy**|
|**hour**|**hh**|
|**minute**|**mi**, **n**|
|**second**|**ss**, **s**|
|**millisecond**|**ms**|

#### *number*

The integer number that decides the width of the bucket combined with *datepart* argument. This represents the width of the *datepart* buckets from the origin time. This argument can't be a negative integer value.

#### *date*

An expression that can resolve to one of the following values:

- **date**
- **datetime**
- **datetime2**
- **datetimeoffset**
- **smalldatetime**
- **time**

For **date**, `DATE_BUCKET` will accept a column expression, expression, or user-defined variable if they resolve to any of the data types mentioned above.

#### *origin*

An optional expression that can resolve to one of the following values:

- **date**
- **datetime**
- **datetime2**
- **datetimeoffset**
- **smalldatetime**
- **time**

The data type for `origin` should match the data type of the `date` parameter.

`DATE_BUCKET` uses a default origin date value of `1900-01-01 00:00:00.000` that is, 12:00 AM on Monday, January 1, 1900, if no `origin` value is specified for the function.

## Return type

The return value data type for this method is dynamic. The return type depends on the argument supplied for `date`. If a valid input data type is supplied for `date`, `DATE_BUCKET` returns the same data type. `DATE_BUCKET` raises an error if a string literal is specified for the `date` parameter.

## Return values

### Understanding the output from `DATE_BUCKET`

`DATE_BUCKET` returns the latest date or time value, corresponding to the *datepart* and *number* parameter. For example, in the expressions below, `DATE_BUCKET` will return the output value of `2020-04-13 00:00:00.0000000`, as the output is calculated based on one week buckets from the default origin time of `1900-01-01 00:00:00.000`. The value `2020-04-13 00:00:00.0000000` is 6,276 weeks from the origin value of `1900-01-01 00:00:00.000`.

```sql
DECLARE @date DATETIME2 = '2020-04-15 21:22:11';
SELECT DATE_BUCKET(WEEK, 1, @date);
```

For all the expressions below, the same output value of `2020-04-13 00:00:00.0000000` will be returned. This is because `2020-04-13 00:00:00.0000000` is 6,276 weeks from the origin date, and 6,276 is divisible by 2, 3, 4 and 6.

```sql
DECLARE @date DATETIME2 = '2020-04-15 21:22:11';
SELECT DATE_BUCKET(WEEK, 2, @date);
SELECT DATE_BUCKET(WEEK, 3, @date);
SELECT DATE_BUCKET(WEEK, 4, @date);
SELECT DATE_BUCKET(WEEK, 6, @date);
```

The output for the expression below is `2020-04-06 00:00:00.0000000`, which is 6275 weeks from the default origin time `1900-01-01 00:00:00.000`.

```sql
DECLARE @date DATETIME2 = '2020-04-15 21:22:11';
SELECT DATE_BUCKET(WEEK, 5, @date);
```

The output for the expression below is `2020-06-09 00:00:00.0000000`, which is 75 weeks from the specified origin time `2019-01-01 00:00:00`.

```sql
DECLARE @date DATETIME2 = '2020-06-15 21:22:11';
DECLARE @origin DATETIME2 = '2019-01-01 00:00:00';
SELECT DATE_BUCKET(WEEK, 5, @date, @origin);
```

## datepart argument

**dayofyear**, **day**, and **weekday** return the same value. Each *datepart* and its abbreviations return the same value.

## number argument

The *number* argument can't exceed the range of positive **int** values. In the following statements, the argument for *number* exceeds the range of **int** by 1. The following statement returns the error message, `Msg 8115, Level 16, State 2, Line 2. Arithmetic overflow error converting expression to data type int.`

```sql
DECLARE @date DATETIME2 = '2020-04-30 00:00:00';
SELECT DATE_BUCKET(DAY, 2147483648, @date);
```

If a negative value for number is passed to the `DATE_BUCKET` function, the following error will be returned.

```txt
Msg 9834, Level 16, State 1, Line 1
Invalid bucket width value passed to DATE_BUCKET function. Only positive values are allowed.
```

## date argument

`DATE_BUCKET` return the base value corresponding to the data type of the `date` argument. In the following example, an output value with datetime2 datatype is returned.

```sql
SELECT DATE_BUCKET(DAY, 10, SYSUTCDATETIME());
```

## origin argument

The data type of the `origin` and `date` arguments in must be the same. If different data types are used, an error will be generated.

## Remarks

Use `DATE_BUCKET` in the following clauses:

- GROUP BY
- HAVING
- ORDER BY
- SELECT \<list>
- WHERE

## Examples

### A. Calculate DATE_BUCKET with a bucket width of 1 from the origin time

Each of these statements increments `DATE_BUCKET` with a bucket width of 1 from the origin time:

```sql
DECLARE @date DATETIME2 = '2020-04-30 21:21:21';
SELECT 'Week', DATE_BUCKET(WEEK, 1, @date)
UNION ALL
SELECT 'Day', DATE_BUCKET(DAY, 1, @date)
UNION ALL
SELECT 'Hour', DATE_BUCKET(HOUR, 1, @date)
UNION ALL
SELECT 'Minutes', DATE_BUCKET(MINUTE, 1, @date)
UNION ALL
SELECT 'Seconds', DATE_BUCKET(SECOND, 1, @date);
```

Here's the result set.

```output
Week    2020-04-27 00:00:00.0000000
Day     2020-04-30 00:00:00.0000000
Hour    2020-04-30 21:00:00.0000000
Minutes 2020-04-30 21:21:00.0000000
Seconds 2020-04-30 21:21:21.0000000
```

### B. Use expressions as arguments for the number and date parameters

These examples use different types of expressions as arguments for the *number* and *date* parameters.

These examples are built using the `AdventureWorksDW2017` database.

#### Specify user-defined variables as number and date

This example specifies user-defined variables as arguments for *number* and *date*:

```sql
DECLARE @days INT = 365,
    @datetime DATETIME2 = '2000-01-01 01:01:01.1110000';
    /* 2000 was a leap year */;
SELECT DATE_BUCKET(DAY, @days, @datetime);
```

Here's the result set.

```output
1999-12-08 00:00:00.0000000
```

#### Specify a column as date

In the example below, we're calculating the sum of `OrderQuantity` and sum of `UnitPrice` grouped over weekly date buckets.

```sql
SELECT DATE_BUCKET(WEEK, 1, CAST(ShipDate AS DATETIME2)) AS ShippedDateBucket
    , SUM(OrderQuantity) AS SumOrderQuantity
    , SUM(UnitPrice) AS SumUnitPrice
FROM dbo.FactInternetSales FIS
WHERE ShipDate BETWEEN '2011-01-03 00:00:00.000'
        AND '2011-02-28 00:00:00.000'
GROUP BY DATE_BUCKET(WEEK, 1, CAST(ShipDate AS DATETIME2))
ORDER BY ShippedDateBucket;
```

Here's the result set.

```output
ShippedDateBucket           SumOrderQuantity SumUnitPrice
--------------------------- ---------------- ---------------------
2011-01-03 00:00:00.0000000 21               65589.7546
2011-01-10 00:00:00.0000000 27               89938.5464
2011-01-17 00:00:00.0000000 31               104404.9064
2011-01-24 00:00:00.0000000 36               118525.6846
2011-01-31 00:00:00.0000000 39               123555.431
2011-02-07 00:00:00.0000000 35               109342.351
2011-02-14 00:00:00.0000000 32               107804.8964
2011-02-21 00:00:00.0000000 37               119456.3428
2011-02-28 00:00:00.0000000 9                28968.6982
```

#### Specify scalar system function as date

This example specifies `SYSDATETIME` for *date*. The exact value returned depends on the day and time of statement execution:

```sql
SELECT DATE_BUCKET(WEEK, 10, SYSDATETIME());
```

Here's the result set.

```output
2020-03-02 00:00:00.0000000
```

#### Specify scalar subqueries and scalar functions as number and date

This example uses scalar subqueries, `MAX(OrderDate)`, as arguments for *number* and *date*. `(SELECT top 1 CustomerKey FROM dbo.DimCustomer where GeographyKey > 100)` serves as an artificial argument for the number parameter, to show how to select a *number* argument from a value list.

```sql
SELECT DATE_BUCKET(WEEK, (
    SELECT TOP 1 CustomerKey
    FROM dbo.DimCustomer
    WHERE GeographyKey > 100
    ), (
    SELECT MAX(OrderDate)
    FROM dbo.FactInternetSales
));
```

#### Specify numeric expressions and scalar system functions as number and date

This example uses a numeric expression ((10/2)), and scalar system functions (`SYSDATETIME`) as arguments for number and date.

```sql
SELECT DATE_BUCKET(WEEK, (10/2), SYSDATETIME());
```

#### Specify an aggregate window function as number

This example uses an aggregate window function as an argument for *number*.

```sql
SELECT DISTINCT DATE_BUCKET(DAY, 30, CAST([ShipDate] AS DATETIME2)) AS DateBucket
    , FIRST_VALUE([SalesOrderNumber]) OVER (
        ORDER BY DATE_BUCKET(DAY, 30, CAST([ShipDate] AS DATETIME2))
        ) AS First_Value_In_Bucket
    , LAST_VALUE([SalesOrderNumber]) OVER (
        ORDER BY DATE_BUCKET(DAY, 30, CAST([ShipDate] AS DATETIME2))
        ) AS Last_Value_In_Bucket
FROM [dbo].[FactInternetSales]
WHERE ShipDate BETWEEN '2011-01-03 00:00:00.000'
        AND '2011-02-28 00:00:00.000'
ORDER BY DateBucket;
GO
```

### C. Use a non-default origin value

This example uses a non-default origin value to generate the date buckets.

```sql
DECLARE @date DATETIME2 = '2020-06-15 21:22:11';
DECLARE @origin DATETIME2 = '2019-01-01 00:00:00';
SELECT DATE_BUCKET(HOUR, 2, @date, @origin);
```

## See also

- [CAST and CONVERT &#40;Transact-SQL&#41;](../functions/cast-and-convert-transact-sql.md)
- [Date and time types](../data-types/date-and-time-types.md)
- [Date and time data types and functions (Transact-SQL)](date-and-time-data-types-and-functions-transact-sql.md)
