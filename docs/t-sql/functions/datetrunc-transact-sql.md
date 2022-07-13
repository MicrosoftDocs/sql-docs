---
title: "DATETRUNC (Transact-SQL)"
description: "Transact-SQL reference for the DATETRUNC function. This function returns an input *date* truncated to a specified datepart."
ms.prod: sql
ms.technology: t-sql
ms.topic: reference
f1_keywords:
  - "DATETRUNC_TSQL"
  - "DATETRUNC"
dev_langs:
  - "TSQL"
author: aashnabafna-ms
ms.author: aashnabafna
ms.reviewer: "derekw"
ms.date: "07/15/2022"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# DATETRUNC (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Starting with SQL Server 2022 this function returns an input *date* truncated to a specified *datepart*.

## Syntax  
  
```syntaxsql
DATETRUNC(datepart, date)
```  

## Arguments

#### datetrunc

The datepart specifies the precision for truncation. This table lists all the valid datepart values for `DATETRUNC`, given that it's also a valid part of the input date type.

|*datepart*|Abbreviations|Truncation Notes|  
|---|---|---|
|**year**|**yy**, **yyyy**|  
|**quarter**|**qq**, **q**|  
|**month**|**mm**, **m**|  
|**dayofyear**|**dy**, **y**|*dayofyear* is truncated in the same manner as *day*  
|**day**|**dd**, **d**|*day* is truncated in the same manner as *dayofyear*  
|**week**|**wk**, **ww**|Truncate to the first day of the week. In T-SQL, this is defined by the `DateFirst` T-SQL setting and, unless otherwise set, defaults to 7 (Sunday).  
|**iso\_week**|**isowk, isoww**|Truncate to the first day of an ISO Week. The first day of the week in the ISO8601 calendar system is always Monday.  
|**hour**|**hh**|  
|**minute**|**mi, n**|  
|**second**|**ss**, **s**|  
|**millisecond**|**ms**|  
|**microsecond**|**mcs**|  

> [!NOTE]
> The *weekday*, *timezoneoffset*, and *nanosecond* T-SQL dateparts are not supported for DATETRUNC.

#### date

The date parameter accepts any expression, column, or user-defined variable that can resolve to any [valid date or time type supported by T-SQL.] such as:

- SmallDateTime

- DateTime

- Date

- Time

- DateTime2

- DateTimeOffset

> [!NOTE]
> DateTrunc will also accept any string literal (of any string type) that can resolve to a *DateTime2(7).*

## Return Type

The returned data type for this function is dynamic. DATETRUNC returns a truncated date of the same data type (and, if applicable, the same fractional time scale) as the input date. For example, if DATETRUNC was given a *DateTimeOffset(3)* input date, it would return a truncated *DateTimeOffset(3)*. If the value supplied to the input date parameter was a string literal that could resolve to a *DateTime2(7)*, DATETRUNC would return a *DateTime2(7*).

## Examples

### A. Fractional Time Scale Precision

Milliseconds have a scale of 3 (.123), microseconds have a scale of 6 (.123456), and nanoseconds have a scale of 9 (.123456789). The *time***, ***datetime2***,** and *datetimeoffset* data types have a maximum scale of 7 (.1234567). Therefore, to truncate a date to a *millisecond* datepart, the fractional time scale must be at least 3. Similarly, to truncate a date using the *microsecond* datepart, the fractional time scale must be at least 6. DATETRUNC doesn't support the *nanosecond* datepart since no T-SQL date type supports a fractional scale of 9.

The table below lists some example `dateparts` with corresponding return values

```sql
DECLARE @d datetime2 = '2021-12-08 11:30:15.1234567'; 
GO 
```

|*DATETRUNC Query*|Return value|  
|---|---|
|SELECT DATETRUNC(year, \@d)|2021-01-01 00:00:00.0000000|
|SELECT DATETRUNC(quarter, \@d)|2021-10-01 00:00:00.0000000|
| SELECT DATETRUNC(month, \@d)                            | 2021-12-01 00:00:00.0000000 |
|\-- Using the default DATEFIRST setting of 7<br />SELECT DATETRUNC(week, \@d)|2021-12-05 00:00:00.0000000|
|SET DATEFIRST 3<br />SELECT DATETRUNC(week, \@d)|2021-12-08 00:00:00.0000000|
|SET DATEFIRST 4<br />SELECT DATETRUNC(week, \@d)|2021-12-02 00:00:00.0000000|
|SELECT DATETRUNC(iso\_week, \@d)|2021-12-06 00:00:00.0000000|
|SELECT DATETRUNC(dayofyear, \@d)|2021-12-08 00:00:00.0000000|
|SELECT DATETRUNC(day, \@d)|2021-12-08 00:00:00.0000000|
|SELECT DATETRUNC(hour, \@d)|2021-12-08 11:00:00.0000000|
|SELECT DATETRUNC(minute, \@d)|2021-12-08 11:30:00.0000000|
|SELECT DATETRUNC(second, \@d)|2021-12-08 11:30:15.0000000|
|SELECT DATETRUNC(millisecond, \@d)|2021-12-08 11:30:15.1230000|
|SELECT DATETRUNC(microsecond, \@d)|2021-12-08 11:30:15.1234560|

The table below lists some Example using `literals` with corresponding return values

|*DATETRUNC Query*|Return value|  
|---|---|
|SELECT DATETRUNC(month, \'2021-12-08')|Returns a DateTime2(7):<br />2021-12-01 00:00:00.0000000|
|SELECT DATETRUNC(year, \'2021-12-08 11:30:15.1234567\')|Returns a DateTime2(7):<br />2021-01-01 00:00:00.0000000|
|DECLARE \@d char(200) = \'2021-12-08\'<br />SELECT DATETRUNC(millisecond, \@d)|Returns a DateTime2(7):<br /> 2021-12-08 00:00:00.0000000|
|DECLARE \@d nvarchar(max) = \'2021-12-08 11:12:11\'<br />SELECT DATETRUNC(minute, \@d)|Returns a DateTime2(7):<br />2021-12-08 11:12:00.0000000|

The table below lists some Example using `variables` with corresponding return values

|*DATETRUNC Query*|Return value|  
|---|---|
|DECLARE \@d datetime2 = \'2021-12-08 11:30:15.1234567\'<br />SELECT DATETRUNC(day, \@d)|Returns a DateTime2(7):<br />2021-12-08 00:00:00.0000000|
|DECLARE \@d datetimeoffset(3) = \'2021-12-08 11:30:15.1234567\'<br />SELECT DATETRUNC(second, \@d)|Returns a DateTime2(7):<br /> 2021-01-01 11:30:15.0000000|

The TransactionDate column from the Sales.CustomerTransactions table in the following example serves as the argument for the `date` parameter:

```sql
USE WideWorldImporters;
GO

SELECT 
 CustomerTransactionID
 ,DATETRUNC(month, TransactionDate) AS MonthTransactionOccured
 ,InvoiceID
 ,CustomerID
 ,TransactionAmount
 ,sum(TransactionAmount) OVER (
 PARTITION BY CustomerID
 ORDER BY TransactionDate, CustomerTransactionID
 ROWS UNBOUNDED PRECEDING) AS RunningTotal
 ,TransactionDate AS ActualTransactionDate
FROM [WideWorldImporters].[Sales].[CustomerTransactions]
WHERE InvoiceID IS NOT NULL
AND DATETRUNC(month, TransactionDate) >= '2015-12-01';
```

The date argument accepts any expression that can resolve to a T-SQL date type (or a string literal that can later resolve to a *DateTime2(7)*). The OrderDate column from an Sales.Orders table serves as an argument.

Example

```sql
SELECT DATETRUNC(m, SYSDATETIME()) AS [DATETRUNC Using SYSDATETIME];

SELECT DATETRUNC(yyyy, CONVERT(date, '2021-12-1')) AS [DATETRUNC Converting Date];

USE WideWorldImporters;
SELECT DATETRUNC(month, DATEADD(month, 4, OrderDate)) AS OrderMonth FROM Sales.Orders;
```

### B. Truncating a date to a datepart representing its maximum precision

If the datepart being used has the same unit maximum precision as the input date type, truncating the input date to this datepart would have no effect.
```sql
DECLARE @d datetime = '2021-12-08 11:30:15.123';
SELECT @d, DATETRUNC(millisecond, @d);
GO
```

Both statements return the same output.

```output
2021-12-08 11:30:15.123  2021-12-08 11:30:15.123
```

```sql
DECLARE @d date = '2021-12-08';
SELECT @d, DATETRUNC(day, @d);
GO
```

Both statements return the same output.

```output
2021-12-08 2021-12-08  2021-12-08 2021-12-08
```

### C. SmallDateTime Precision

A *SmallDateTime* is only precise up to the nearest minute, even though it has a field for seconds. Therefore, truncating it to the nearest minute or the nearest second would have no effect.

```sql
DECLARE @d smalldatetime = '2021-12-08 11:31:15';
SELECT @d, DATETRUNC(minute, @d), DATETRUNC(second, @d);
GO
```

All three statements return the same output

```output
2021-12-08 11:30:15.123  2021-12-08 11:30:15.123  2021-12-08 11:30:15.123
```

### D. DateTime2 Precision

A DateTime2 is only precise up to 1/3rd of a microsecond. Therefore, although truncating a *DateTime2* to a microsecond will yield its stored value.

```sql
DECLARE @d1 datetime2 = '2021-12-12 11:11:11.997';
SELECT @d1, DATETRUNC(microsecond, @d1);
```

```output
2021-12-12 11:11:11.9970000 2021-12-12 11:11:11.9970000
```

```sql
DECLARE @d2 datetime2 = '2021-12-12 11:11:11.998';
SELECT @d2, DATETRUNC(microsecond, @d2);
```

```output
2021-12-12 11:11:11.9980000 2021-12-12 11:11:11.9980000
```

```sql
DECLARE @d3 datetime2 = '2021-12-12 11:11:11.999'
SELECT @d3, DATETRUNC(microsecond, @d3);
```

```output
2021-12-12 11:11:11.9990000 2021-12-12 11:11:11.9990000
```

## Remarks

A *DATEPART* error is thrown if the datepart used isn't supported by the DATETRUNC function or the input date data type. This can occur when:

- A datepart not supported by DATETRUNC is used (namely, *weekday*, *tzoffset*, or *nanosecond*)

- A time-related datepart is used with data type *date* or a date-related datepart is used with data type *time*.

```sql
DECLARE @d time = '12:12:12';
SELECT DATETRUNC(year, @d);
```

```output
Msg 9810, Level 16, State 10, Line 78
The datepart year is not supported by date function datetrunc for data type time.
```

- The datepart requires a higher fractional time scale precision than what is supported by the data type (See section on Fractional Time Scale Precision).

```sql
DECLARE @d datetime2(3) = '2021-12-12 12:12:12.12345';
SELECT DATETRUNC(microsecond, @d);
```

```output
Msg 9810, Level 16, State 11, Line 81
The datepart microsecond is not supported by date function datetrunc for data type datetime2.
```

- A *DATE TOO SMALL* error is thrown if the date truncation attempts to backtrack to a date before the minimum date supported by that date type. This only occurs when using the *week* datepart.

```sql
DECLARE @d date= '0001-01-01 00:00:00';
SELECT DATETRUNC(week, @d);
```

```output
Msg 9837, Level 16, State 3, Line 84
An invalid date value was encountered: The date value is less than the minimum date value allowed for the data type.
```

## See also

- [DATEPART](../../t-sql/functions/datepart-transact-sql.md)
- [Validate a date or time type supported by TSQL](date-and-time-data-types-and-functions-transact-sql.md)