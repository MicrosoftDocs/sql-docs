---
title: "Date and Time Data Types and Functions"
titleSuffix: SQL Server (Transact-SQL)
description: Links to Date and Time data types and functions articles.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/08/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "dates [SQL Server]"
  - "date and time [SQL Server], all data types and functions"
  - "date and time [SQL Server]"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || =azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# Date and time data types and functions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

The sections in this article cover all [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions.

- [Date and time data types](#date-and-time-data-types)
- [Date and time functions](#date-and-time-functions)
  - [Functions that return system date and time values](#functions-that-return-system-date-and-time-values)
  - [Functions that return date and time parts](#functions-that-return-date-and-time-parts)
  - [Functions that return date and time values from their parts](#functions-that-return-date-and-time-values-from-their-parts)
  - [Functions that return date and time difference values](#functions-that-return-date-and-time-difference-values)
  - [Functions that modify date and time values](#functions-that-modify-date-and-time-values)
  - [Functions that set or return session format functions](#functions-that-set-or-return-session-format-functions)
  - [Functions that validate date and time values](#functions-that-validate-date-and-time-values)
- [Date and time-related articles](#date-and-time-related-articles)

## Date and time data types

The [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types are listed in the following table:

| Data type | Format | Range | Accuracy | Storage size (bytes) | User-defined fractional second precision | Time zone offset |
| --- | --- | --- | --- | --- | --- | --- |
| [time](../data-types/time-transact-sql.md) | HH:mm:ss[.nnnnnnn] | 00:00:00.0000000 through 23:59:59.9999999 | 100 nanoseconds | 3 to 5 | Yes | No |
| [date](../data-types/date-transact-sql.md) | yyyy-MM-dd | 0001-01-01 through 9999-12-31 | 1 day | 3 | No | No |
| [smalldatetime](../data-types/smalldatetime-transact-sql.md) | yyyy-MM-dd HH:mm:ss | 1900-01-01 through 2079-06-06 | 1 minute | 4 | No | No |
| [datetime](../data-types/datetime-transact-sql.md) | yyyy-MM-dd HH:mm:ss[.nnn] | 1753-01-01 through 9999-12-31 | 0.00333 second | 8 | No | No |
| [datetime2](../data-types/datetime2-transact-sql.md) | yyyy-MM-dd HH:mm:ss[.nnnnnnn] | 0001-01-01 00:00:00.0000000 through 9999-12-31 23:59:59.9999999 | 100 nanoseconds | 6 to 8 | Yes | No |
| [datetimeoffset](../data-types/datetimeoffset-transact-sql.md) | yyyy-MM-dd HH:mm:ss[.nnnnnnn] [+&#124;-]HH:mm | 0001-01-01 00:00:00.0000000 through 9999-12-31 23:59:59.9999999 (in UTC) | 100 nanoseconds | 8 to 10 | Yes | Yes |

> [!NOTE]  
> The [!INCLUDE [tsql](../../includes/tsql-md.md)] [rowversion](../data-types/rowversion-transact-sql.md) data type isn't a date or time data type. **timestamp** is a deprecated synonym for **rowversion**.

## Date and time functions

The following tables list the [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time functions. For more information about determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

### Functions that return system date and time values

[!INCLUDE [tsql](../../includes/tsql-md.md)] derives all system date and time values from the operating system of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs.

#### Higher-precision system date and time functions

Since [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] derives the date and time values through use of the GetSystemTimeAsFileTime() Windows API. The accuracy depends on the computer hardware and version of Windows on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running. This API has a precision fixed at 100 nanoseconds. Use the GetSystemTimeAdjustment() Windows API to determine the accuracy.

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [SYSDATETIME](sysdatetime-transact-sql.md) | SYSDATETIME ( ) | Returns a **datetime2(7)** value containing the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs. The returned value doesn't include the time zone offset. | **datetime2(7)** | Nondeterministic |
| [SYSDATETIMEOFFSET](sysdatetimeoffset-transact-sql.md) | SYSDATETIMEOFFSET&nbsp;(&nbsp;) | Returns a **datetimeoffset(7)** value containing the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs. The returned value includes the time zone offset. | **datetimeoffset(7)** | Nondeterministic |
| [SYSUTCDATETIME](sysutcdatetime-transact-sql.md) | SYSUTCDATETIME ( ) | Returns a **datetime2(7)** value containing the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. The function returns the date and time values as UTC time (Coordinated Universal Time). | **datetime2(7)** | Nondeterministic |

#### Lower-precision system date and time functions

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [CURRENT_TIMESTAMP](current-timestamp-transact-sql.md) | CURRENT_TIMESTAMP | Returns a **datetime** value containing the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs. The returned value doesn't include the time zone offset. | **datetime** | Nondeterministic |
| [GETDATE](getdate-transact-sql.md) | GETDATE ( ) | Returns a **datetime** value containing the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs. The returned value doesn't include the time zone offset. | **datetime** | Nondeterministic |
| [GETUTCDATE](getutcdate-transact-sql.md) | GETUTCDATE ( ) | Returns a **datetime** value containing the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs. The function returns the date and time values as UTC time (Coordinated Universal Time). | **datetime** | Nondeterministic |
| [CURRENT_DATE](current-date-transact-sql.md) | CURRENT_DATE | Returns a **date** value containing only the date of the computer on which the instance of the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] runs. The returned value doesn't include the time and the time zone offset. | **date** | Nondeterministic |

### Functions that return date and time parts

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [DATE_BUCKET](date-bucket-transact-sql.md) | DATE_BUCKET ( *datepart*, *number*, *date*, *origin* ) | Returns a value corresponding to the start of each date-time bucket from the timestamp defined by the *origin* parameter, or the default origin value of `1900-01-01 00:00:00.000` if the origin parameter isn't specified. | The return type depends on the argument supplied for *date*. | Nondeterministic |
| [DATENAME](datename-transact-sql.md) | DATENAME ( *datepart*, *date* ) | Returns a character string representing the specified *datepart* of the specified date. | **nvarchar** | Nondeterministic |
| [DATEPART](datepart-transact-sql.md) | DATEPART ( *datepart*, *date* ) | Returns an integer representing the specified *datepart* of the specified *date*. | **int** | Nondeterministic |
| [DATETRUNC](datetrunc-transact-sql.md) | DATETRUNC ( *datepart*, *date* ) | Returns an input *date* truncated to a specified *datepart*. | The return type depends on the argument supplied for *date*. | Nondeterministic |
| [DAY](day-transact-sql.md) | DAY ( *date* ) | Returns an integer representing the day part of the specified *date*. | **int** | Deterministic |
| [MONTH](month-transact-sql.md) | MONTH ( *date* ) | Returns an integer representing the month part of a specified *date*. | **int** | Deterministic |
| [YEAR](year-transact-sql.md) | YEAR ( *date* ) | Returns an integer representing the year part of a specified *date*. | **int** | Deterministic |

### Functions that return date and time values from their parts

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [DATEFROMPARTS](datefromparts-transact-sql.md) | DATEFROMPARTS ( *year*, *month*, *day* ) | Returns a **date** value for the specified year, month, and day. | **date** | Deterministic |
| [DATETIME2FROMPARTS](datetime2fromparts-transact-sql.md) | DATETIME2FROMPARTS ( *year*, *month*, *day*, *hour*, *minute*, *seconds*, *fractions*, *precision* ) | Returns a **datetime2** value for the specified date and time, with the specified precision. | **datetime2(***precision***)** | Deterministic |
| [DATETIMEFROMPARTS](datetimefromparts-transact-sql.md) | DATETIMEFROMPARTS ( *year*, *month*, *day*, *hour*, *minute*, *seconds*, *milliseconds* ) | Returns a **datetime** value for the specified date and time. | **datetime** | Deterministic |
| [DATETIMEOFFSETFROMPARTS](datetimeoffsetfromparts-transact-sql.md) | DATETIMEOFFSETFROMPARTS ( *year*, *month*, *day*, *hour*, *minute*, *seconds*, *fractions*, *hour_offset*, *minute_offset*, *precision* ) | Returns a **datetimeoffset** value for the specified date and time, with the specified offsets and precision. | **datetimeoffset(***precision***)** | Deterministic |
| [SMALLDATETIMEFROMPARTS](smalldatetimefromparts-transact-sql.md) | SMALLDATETIMEFROMPARTS ( *year*, *month*, *day*, *hour*, *minute* ) | Returns a **smalldatetime** value for the specified date and time. | **smalldatetime** | Deterministic |
| [TIMEFROMPARTS](timefromparts-transact-sql.md) | TIMEFROMPARTS ( *hour*, *minute*, *seconds*, *fractions*, *precision* ) | Returns a **time** value for the specified time, with the specified precision. | **time(***precision***)** | Deterministic |

### Functions that return date and time difference values

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [DATEDIFF](datediff-transact-sql.md) | DATEDIFF ( *datepart*, *startdate*, *enddate* ) | Returns the number of date or time *datepart* boundaries, crossed between two specified dates. | **int** | Deterministic |
| [DATEDIFF_BIG](datediff-big-transact-sql.md) | DATEDIFF_BIG ( *datepart*, *startdate*, *enddate* ) | Returns the number of date or time *datepart* boundaries, crossed between two specified dates. | **bigint** | Deterministic |

### Functions that modify date and time values

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [DATEADD](dateadd-transact-sql.md) | DATEADD (*datepart*, *number*, *date* ) | Returns a new **datetime** value by adding an interval to the specified *datepart* of the specified *date*. | The data type of the *date* argument | Deterministic |
| [EOMONTH](eomonth-transact-sql.md) | EOMONTH ( *start_date* [ , *month_to_add* ] ) | Returns the last day of the month containing the specified date, with an optional offset. | Return type is the type of the *start_date* argument, or alternately, the **date** data type. | Deterministic |
| [SWITCHOFFSET](switchoffset-transact-sql.md) | SWITCHOFFSET (*DATETIMEOFFSET*, *time_zone* ) | SWITCHOFFSET changes the time zone offset of a DATETIMEOFFSET value, and preserves the UTC value. | **datetimeoffset** with the fractional precision of the *DATETIMEOFFSET* | Deterministic |
| [TODATETIMEOFFSET](todatetimeoffset-transact-sql.md) | TODATETIMEOFFSET (*expression*, *time_zone* ) | TODATETIMEOFFSET transforms a datetime2 value into a datetimeoffset value. *TODATETIMEOFFSET* interprets the datetime2 value in local time, for the specified time_zone. | **datetimeoffset** with the fractional precision of the *datetime* argument | Deterministic |

### Functions that set or return session format functions

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [&#x40;&#x40;DATEFIRST](datefirst-transact-sql.md) | @@DATEFIRST | Returns the current value, for the session, of SET DATEFIRST. | **tinyint** | Nondeterministic |
| [SET DATEFIRST](../statements/set-datefirst-transact-sql.md) | SET DATEFIRST { *number* &#124; *@number_var* } | Sets the first day of the week to a number from 1 through 7. | Not applicable | Not applicable |
| [SET DATEFORMAT](../statements/set-dateformat-transact-sql.md) | SET DATEFORMAT { *format* &#124; *@format_var* } | Sets the order of the dateparts (month/day/year) for entering **datetime** or **smalldatetime** data. | Not applicable | Not applicable |
| [&#x40;&#x40;LANGUAGE](language-transact-sql.md) | @@LANGUAGE | Returns the name of the language in current used. @@LANGUAGE isn't a date or time function. However, the language setting can affect the output of date functions. | Not applicable | Not applicable |
| [SET LANGUAGE](../statements/set-language-transact-sql.md) | SET LANGUAGE { [ N ] '*language*' &#124; *@language_var* } | Sets the language environment for the session and system messages. SET LANGUAGE isn't a date or time function. However, the language setting affects the output of date functions. | Not applicable | Not applicable |
| [sp_helplanguage](../../relational-databases/system-stored-procedures/sp-helplanguage-transact-sql.md) | sp_helplanguage [ [ *@language* = ] '*language*' ] | Returns information about date formats of all supported languages. `sp_helplanguage` isn't a date or time stored procedure. However, the language setting affects the output of date functions. | Not applicable | Not applicable |

### Functions that validate date and time values

| Function | Syntax | Return value | Return data type | Determinism |
| --- | --- | --- | --- | --- |
| [ISDATE](isdate-transact-sql.md) | ISDATE ( *expression* ) | Determines whether a **datetime** or **smalldatetime** input expression has a valid date or time value. | **int** | ISDATE is deterministic only used with the CONVERT function, when the CONVERT style parameter is specified, and when style isn't equal to 0, 100, 9, or 109. |

## Date and time-related articles

| Article | Description |
| --- | --- |
| [FORMAT](format-transact-sql.md) | Returns a value formatted with the specified format and optional culture. Use the FORMAT function for locale-aware formatting of date/time and number values as strings. |
| [CAST and CONVERT](cast-and-convert-transact-sql.md) | Provides information about the conversion of date and time values to and from string literals, and other date and time formats. |
| [Write International Transact-SQL Statements](../../relational-databases/collations/write-international-transact-sql-statements.md) | Provides guidelines for portability of databases and database applications that use [!INCLUDE [tsql](../../includes/tsql-md.md)] statements from one language to another, or that support multiple languages. |
| [ODBC Scalar Functions](odbc-scalar-functions-transact-sql.md) | Provides information about ODBC scalar functions available for use in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. Includes ODBC date and time functions. |
| [AT TIME ZONE](../queries/at-time-zone-transact-sql.md) | Provides time zone conversion. |

## Related content

- [Functions](functions.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
