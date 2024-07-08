---
title: "datetime (Transact-SQL)"
description: datetime defines a date that is combined with a time of day with fractional seconds that is based on a 24-hour clock.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 06/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "datetime_TSQL"
  - "datetime"
helpviewer_keywords:
  - "time [SQL Server], data types"
  - "date and time [SQL Server], datetime"
  - "dates [SQL Server], data types"
  - "datetime data type [SQL Server]"
  - "data types [SQL Server], date and time"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# datetime (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Defines a date that is combined with a time of day with fractional seconds that is based on a 24-hour clock.

Avoid using **datetime** for new work. Instead, use the **time**, **date**, **datetime2**, and **datetimeoffset** data types. These types align with the SQL Standard, and are more portable. **time**, **datetime2** and **datetimeoffset** provide more seconds precision. **datetimeoffset** provides time zone support for globally deployed applications.

## Description

| Property | Value |
| --- | --- |
| **Syntax** | `DATETIME` |
| **Usage** | `DECLARE @MyDatetime DATETIME;`<br />`CREATE TABLE Table1 (Column1 DATETIME);` |
| **Default string literal formats (used for down-level client)** | Not applicable |
| **Date range** | 1753-01-01 (January 1, 1753) through 9999-12-31 (December 31, 9999) |
| **Time range** | 00:00:00 through 23:59:59.997 |
| **Time zone offset range** | None |
| **Element ranges** | `yyyy` is four digits from `1753` through `9999` that represent a year.<br /><br />`MM` is two digits, ranging from `01` to `12`, that represent a month in the specified year.<br /><br />`dd` is two digits, ranging from `01` to `31` depending on the month, which represent a day of the specified month.<br /><br />`HH` is two digits, ranging from `00` to `23`, that represent the hour.<br /><br />`mm` is two digits, ranging from `00` to `59`, that represent the minute.<br /><br />`ss` is two digits, ranging from `00` to `59`, that represent the second.<br /><br />`n*` is zero to three digits, ranging from `0` to `999`, that represent the fractional seconds. |
| **Character length** | 19 positions minimum to 23 maximum |
| **Storage size** | 8 bytes |
| **Accuracy** | Rounded to increments of `.000`, `.003`, or `.007` seconds |
| **Default value** | `1900-01-01 00:00:00` |
| **Calendar** | Gregorian (includes the complete range of years) |
| **User-defined fractional second precision** | No |
| **Time zone offset aware and preservation** | No |
| **Daylight saving aware** | No |

## Supported string literal formats for datetime

The following tables list the supported string literal formats for **datetime**. Except for ODBC, **datetime** string literals are in single quotation marks (`'`), for example, `'string_literaL'`. If the environment isn't `us_english`, the string literals should be in Unicode format `N'string_literaL'`.

### Numeric format

You can specify date data with a numeric month specified. For example, `5/20/97` represents the twentieth day of May 1997. When you use numeric date format, specify the month, day, and year in a string that uses slash marks (`/`), hyphens (`-`), or periods (`.`) as separators. This string must appear in the following form:

`<number separator number separator number [time] [time]>`

When the language is set to `us_english`, the default order for the date is `mdy` (month, day, year). You can change the date order by using the [SET DATEFORMAT](../statements/set-dateformat-transact-sql.md) statement.

The setting for `SET DATEFORMAT` determines how date values are interpreted. If the order doesn't match the setting, the values aren't interpreted as dates. Out-of-order dates might be misinterpreted as out of range or with wrong values. For example, `12/10/08` can be interpreted as one of six dates, depending on the `DATEFORMAT` setting. A four-part year is interpreted as the year.

| Date format | Order |
| --- | --- |
| `[0]4/15/[19]96` | `mdy` |
| `[0]4-15-[19]96` | `mdy` |
| `[0]4.15.[19]96` | `mdy` |
| `[0]4/[19]96/15` | `myd` |
| `15/[0]4/[19]96` | `dmy` |
| `15/[19]96/[0]4` | `dym` |
| `[19]96/15/[0]4` | `ydm` |
| `[19]96/[0]4/15` | `ymd` |

| Time format |
| --- |
| `14:30` |
| `14:30[:20:997]` |
| `14:30[:20.9]` |
| `4am` |
| `4 PM` |

### Alphabetical format

You can specify date data with a month specified as the full month name. For example, `April`, or the month abbreviation of `Apr`, specified in the current language. Commas are optional and capitalization is ignored.

Here are some guidelines for using alphabetical date formats:

- Enclose the date and time data in single quotation marks (`'`). For languages other than English, use `N''`.

- Characters that are enclosed in brackets are optional.

- If you specify only the last two digits of the year, values less than the last two digits of the value of the [two digit year cutoff](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) configuration option are in the same century as the cutoff year. Values greater than or equal to the value of this option are in the century that comes before the cutoff year. For example, if **two digit year cutoff** is `2050` (default), `25` is interpreted as `2025` and `50` is interpreted as `1950`. To avoid ambiguity, use four-digit years.

- If the day is missing, the first day of the month is supplied.

- The `SET DATEFORMAT` session setting isn't applied when you specify the month in alphabetical form.

| Alphabetical |
| --- |
| `Apr[il] [15][,] 1996` |
| `Apr[il] 15[,] [19]96` |
| `Apr[il] 1996 [15]` |
| `[15] Apr[il][,] 1996` |
| `15 Apr[il][,][19]96` |
| `15 [19]96 apr[il]` |
| `[15] 1996 apr[il]` |
| `1996 APR[IL] [15]` |
| `1996 [15] APR[IL]` |

### ISO 8601 format

To use the ISO 8601 format, you must specify each element in the format, including the `T`, the colons (`:`), and the period (`.`) that are shown in the format.

The brackets indicate that the fraction of second component is optional. The time component is specified in the 24-hour format. The `T` indicates the start of the time part of the **datetime** value.

The advantage in using the ISO 8601 format is that it's an international standard with unambiguous specification. Also, this format isn't affected by the `SET DATEFORMAT` or [SET LANGUAGE](../statements/set-language-transact-sql.md) setting.

Examples:

- `2004-05-23T14:25:10`
- `2004-05-23T14:25:10.487`

| ISO 8601 |
| --- |
| `yyyy-MM-ddTHH:mm:ss[.mmm]` |
| `yyyyMMdd[ HH:mm:ss[.mmm]]` |

### Unseparated format

This format is similar to the ISO 8601 format, except it contains no date separators.

| Unseparated |
| --- |
| `yyyyMMdd HH:mm:ss[.mmm]` |

### ODBC format

The ODBC API defines escape sequences to represent date and time values, which ODBC calls timestamp data. This ODBC timestamp format is also supported by the OLE DB language definition (DBGUID-SQL) supported by the [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB provider for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Applications that use the ADO, OLE DB, and ODBC-based APIs can use this ODBC timestamp format to represent dates and times.

ODBC timestamp escape sequences are of the format: `{ <literal_type> '<constant_value>' }`:

- `<literal_type>` specifies the type of the escape sequence. Timestamps have three `<literal_type>` specifiers:

  - `d` = date only
  - `t` = time only
  - `ts` = timestamp (time + date)

- `<constant_value>` is the value of the escape sequence. `<constant_value>` must follow these formats for each `<literal_type>`:

  - `d`: `yyyy-MM-dd`
  - `t`: `hh:mm:ss[.fff]`
  - `ts`: `yyyy-MM-dd HH:mm:ss[.fff]`

| ODBC |
| --- |
| `{ ts '1998-05-02 01:23:56.123' }` |
| `{ d '1990-10-02' }` |
| `{ t '13:33:41' }` |

## Rounding of datetime fractional second precision

**datetime** values are rounded to increments of `.000`, `.003`, or `.007` seconds, as shown in the following example.

```sql
SELECT '01/01/2024 23:59:59.999' AS [User-specified value],
    CAST('01/01/2024 23:59:59.999' AS DATETIME) AS [System stored value]
UNION SELECT '01/01/2024 23:59:59.998', CAST('01/01/2024 23:59:59.998' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.997', CAST('01/01/2024 23:59:59.997' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.996', CAST('01/01/2024 23:59:59.996' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.995', CAST('01/01/2024 23:59:59.995' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.994', CAST('01/01/2024 23:59:59.994' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.993', CAST('01/01/2024 23:59:59.993' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.992', CAST('01/01/2024 23:59:59.992' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.991', CAST('01/01/2024 23:59:59.991' AS DATETIME)
UNION SELECT '01/01/2024 23:59:59.990', CAST('01/01/2024 23:59:59.990' AS DATETIME);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| User-specified value | System stored value |
| --- | --- |
| `01/01/2024 23:59:59.999` | `2024-01-02 00:00:00.000` |
| `01/01/2024 23:59:59.998`<br />`01/01/2024 23:59:59.997`<br />`01/01/2024 23:59:59.996`<br />`01/01/2024 23:59:59.995` | `2024-01-01 23:59:59.997` |
| `01/01/2024 23:59:59.994`<br />`01/01/2024 23:59:59.993`<br />`01/01/2024 23:59:59.992` | `2024-01-01 23:59:59.993` |
| `01/01/2024 23:59:59.991`<br />`01/01/2024 23:59:59.990` | `2024-01-01 23:59:59.990` |

## ANSI and ISO 8601 compliance

**datetime** isn't ANSI or ISO 8601 compliant.

## <a id="_datetime"></a> Convert date and time data

When you convert to date and time data types, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] rejects all values it can't recognize as dates or times. For information about using the `CAST` and `CONVERT` functions with date and time data, see [CAST and CONVERT](../functions/cast-and-convert-transact-sql.md).

### Convert other date and time types to the datetime data type

This section describes what occurs when other date and time data types are converted to the **datetime** data type.

When the conversion is from **date**, the year, month, and day are copied. The time component is set to `00:00:00.000`. The following code shows the results of converting a `DATE` value to a `DATETIME` value.

```sql
DECLARE @date DATE = '12-21-16';
DECLARE @datetime DATETIME = @date;

SELECT @datetime AS '@datetime', @date AS '@date';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime                @date
------------------------ -----------
2016-12-21 00:00:00.000  2016-12-21
```

The previous example uses a region specific date format (`MM-DD-YY`).

```sql
DECLARE @date DATE = '12-21-16';
```

You should update the example to match the format for your region.

You can also complete the example with the ISO 8601 compliant date format (`yyyy-MM-dd`). For example:

```sql
DECLARE @date DATE = '2016-12-21';
DECLARE @datetime DATETIME = @date;

SELECT @datetime AS '@datetime', @date AS '@date';
```

When the conversion is from **time(*n*)**, the time component is copied, and the date component is set to `1900-01-01`. When the fractional precision of the **time(*n*)** value is greater than three digits, the value is truncated to fit. The following example shows the results of converting a `TIME(4)` value to a `DATETIME` value.

```sql
DECLARE @time TIME(4) = '12:10:05.1237';
DECLARE @datetime DATETIME = @time;

SELECT @datetime AS '@datetime', @time AS '@time';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime                @time
------------------------ --------------
1900-01-01 12:10:05.123  12:10:05.1237
```

When the conversion is from **smalldatetime**, the hours and minutes are copied. The seconds and fractional seconds are set to `0`. The following code shows the results of converting a `SMALLDATETIME` value to a `DATETIME` value.

```sql
DECLARE @smalldatetime SMALLDATETIME = '12-01-16 12:32';
DECLARE @datetime DATETIME = @smalldatetime;

SELECT @datetime AS '@datetime', @smalldatetime AS '@smalldatetime';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime                @smalldatetime
------------------------ --------------------
2016-12-01 12:32:00.000  2016-12-01 12:32:00
```

When the conversion is from **datetimeoffset(*n*)**, the date and time components are copied. The time zone is truncated. When the fractional precision of the **datetimeoffset(*n*)** value is greater than three digits, the value is truncated. The following example shows the results of converting a `DATETIMEOFFSET(4)` value to a `DATETIME` value.

```sql
DECLARE @datetimeoffset DATETIMEOFFSET(4) = '1968-10-23 12:45:37.1234 +10:0';
DECLARE @datetime DATETIME = @datetimeoffset;

SELECT @datetime AS '@datetime', @datetimeoffset AS '@datetimeoffset';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime                @datetimeoffset
------------------------ -------------------------------
1968-10-23 12:45:37.123  1968-10-23 12:45:37.1237 +10:0
```

When the conversion is from **datetime2(*n*)**, the date and time are copied. When the fractional precision of the **datetime2(*n*)** value is greater than three digits, the value is truncated. The following example shows the results of converting a `DATETIME2(4)` value to a `DATETIME` value.

```sql
DECLARE @datetime2 DATETIME2(4) = '1968-10-23 12:45:37.1237';
DECLARE @datetime DATETIME = @datetime2;

SELECT @datetime AS '@datetime', @datetime2 AS '@datetime2';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime                @datetime2
------------------------ -------------------------
1968-10-23 12:45:37.123  1968-10-23 12:45:37.1237
```

## Examples

The following example compares the results of casting a string to each **date** and **time** data type.

```sql
SELECT CAST('2024-05-08 12:35:29.1234567 +12:15' AS TIME(7)) AS 'time',
    CAST('2024-05-08 12:35:29.1234567 +12:15' AS DATE) AS 'date',
    CAST('2024-05-08 12:35:29.123' AS SMALLDATETIME) AS 'smalldatetime',
    CAST('2024-05-08 12:35:29.123' AS DATETIME) AS 'datetime',
    CAST('2024-05-08 12:35:29.1234567 +12:15' AS DATETIME2(7)) AS 'datetime2',
    CAST('2024-05-08 12:35:29.1234567 +12:15' AS DATETIMEOFFSET(7)) AS 'datetimeoffset';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

| Data type | Output |
| --- | --- |
| `time` | `12:35:29.1234567` |
| `date` | `2024-05-08` |
| `smalldatetime` | `2024-05-08 12:35:00` |
| `datetime` | `2024-05-08 12:35:29.123` |
| `datetime2` | `2024-05-08 12:35:29.1234567` |
| `datetimeoffset` | `2024-05-08 12:35:29.1234567 +12:15` |

## Related content

- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
