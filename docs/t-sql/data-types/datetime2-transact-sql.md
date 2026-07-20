---
title: "datetime2 (Transact-SQL)"
description: datetime2 defines a date that is combined with a time of day with high fractional precision, based on 24-hour clock.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 10/25/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "datetime2"
  - "datetime2_TSQL"
helpviewer_keywords:
  - "time [SQL Server], data types"
  - "dates [SQL Server], data types"
  - "date and time [SQL Server], datetime2"
  - "data types [SQL Server], date and time"
  - "datetime2 data type [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# datetime2 (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Defines a date that is combined with a time of day that is based on 24-hour clock. **datetime2** can be considered as an extension of the existing **datetime** type that has a larger date range, a larger default fractional precision, and optional user-specified precision.

## datetime2 description

| Property | Value |
| --- | --- |
| Syntax | **datetime2** [ (*fractional seconds precision*) ] |
| Usage | `DECLARE @MyDatetime2 datetime2(7);`<br />`CREATE TABLE Table1 (Column1 datetime2(7));` |
| Default string literal format<br /><br />(used for down-level client) | `yyyy-MM-dd HH:mm:ss[.nnnnnnn]`<br /><br />For more information, see [Backward compatibility for down-level clients](#backward-compatibility) later in this article. |
| Date range | `0001-01-01` through `9999-12-31`<br /><br />January 1, 1 CE through December 31, 9999 CE |
| Time range | `00:00:00` through `23:59:59.9999999` |
| Time zone offset range | None |
| Element ranges | `yyyy` is a four-digit number, ranging from `0001` through `9999`, which represents a year.<br /><br />`MM` is a two-digit number, ranging from `01` to `12`, which represents a month in the specified year.<br /><br />`dd` is a two-digit number, ranging from `01` to `31` depending on the month, which represents a day of the specified month.<br /><br />`HH` is a two-digit number, ranging from `00` to `23`, which represents the hour.<br /><br />`mm` is a two-digit number, ranging from `00` to `59`, which represents the minute.<br /><br />`ss` is a two-digit number, ranging from `00` to `59`, which represents the second.<br /><br />`n*` is a zero- to seven-digit number from `0` to `9999999`, which represents the fractional seconds. In Informatica, the fractional seconds are truncated when *n* is less than `3`. |
| Character length | 19 positions minimum (`yyyy-MM-dd HH:mm:ss`) to 27 maximum (`yyyy-MM-dd HH:mm:ss.0000000`) |
| Precision, scale | 0 to 7 digits, with an accuracy of 100 nanoseconds (100 ns). The default precision is 7 digits.<br /><br />In [!INCLUDE [fabric](../../includes/fabric.md)] Data Warehouse, this precision can be an integer from 0 to 6, with no default. Precision must be specified in [!INCLUDE [fabric](../../includes/fabric.md)] Data Warehouse. |
| Storage size <sup>1</sup> | 6 bytes for precision less than 3.<br />7 bytes for precision 3 or 4.<br /><br />All other precision requires 8 bytes. <sup>2</sup> |
| Accuracy | 100 nanoseconds |
| Default value | `1900-01-01 00:00:00` |
| Calendar | Gregorian |
| User-defined fractional second precision | Yes |
| Time zone offset aware and preservation | No |
| Daylight saving aware | No |

<sup>1</sup> Provided values are for uncompressed rowstore. Use of [data compression](../../relational-databases/data-compression/data-compression.md) or [columnstore](../../relational-databases/indexes/columnstore-indexes-overview.md) might alter storage size for each precision. Additionally, storage size on disk and in memory might differ. For example, **datetime2** values always require 8 bytes in memory when batch mode is used.

<sup>2</sup> When a **datetime2** value is cast to a **varbinary** value, an extra byte is added to the **varbinary** value to store precision.

For data type metadata, see [sys.systypes](../../relational-databases/system-compatibility-views/sys-systypes-transact-sql.md) or [TYPEPROPERTY](../functions/typeproperty-transact-sql.md). Precision and scale are variable for some date and time data types. To obtain the precision and scale for a column, see [COLUMNPROPERTY](../functions/columnproperty-transact-sql.md), [COL_LENGTH](../functions/col-length-transact-sql.md), or [sys.columns](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md).

## Supported string literal formats for datetime2

The following tables list the supported ISO 8601 and ODBC string literal formats for **datetime2**. For information about alphabetical, numeric, unseparated, and time formats for the date and time parts of **datetime2**, see [date](date-transact-sql.md) and [time](time-transact-sql.md).

| ISO 8601 | Descriptions |
| --- | --- |
| `yyyy-MM-ddTHH:mm:ss[.nnnnnnn]` | This format isn't affected by the `SET LANGUAGE` and `SET DATEFORMAT` session locale settings. The `T`, the colons (`:`), and the period (`.`) are included in the string literal, for example `2024-05-02T19:58:47.1234567`. |

| ODBC | Description |
| --- | --- |
| `{ ts 'yyyy-MM-dd HH:mm:ss[.nnnnnnn]' }` | ODBC API specific:<br /><br />The number of digits to the right of the decimal point, which represents the fractional seconds, can be specified from 0 up to 7 (100 nanoseconds). |

## ANSI and ISO 8601 compliance

The ANSI and ISO 8601 compliance of [date](date-transact-sql.md) and [time](time-transact-sql.md) apply to **datetime2**.

<a id="backward-compatibility"></a>

## Backward compatibility for down-level clients

Some down-level clients don't support the **time**, **date**, **datetime2**, and **datetimeoffset** data types. The following table shows the type mapping between an up-level instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and down-level clients.

| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type | Default string literal format passed to down-level client | Down-level ODBC | Down-level OLEDB | Down-level JDBC | Down-level SQLCLIENT |
| --- | --- | --- | --- | --- | --- |
| **time** | HH:mm:ss[.nnnnnnn] | SQL_WVARCHAR or SQL_VARCHAR | DBTYPE_WSTRor DBTYPE_STR | Java.sql.String | String or SqString |
| **date** | yyyy-MM-dd | SQL_WVARCHAR or SQL_VARCHAR | DBTYPE_WSTRor DBTYPE_STR | Java.sql.String | String or SqString |
| **datetime2** | yyyy-MM-dd HH:mm:ss[.nnnnnnn] | SQL_WVARCHAR or SQL_VARCHAR | DBTYPE_WSTRor DBTYPE_STR | Java.sql.String | String or SqString |
| **datetimeoffset** | yyyy-MM-dd HH:mm:ss[.nnnnnnn] [+&#124;-]hh:mm | SQL_WVARCHAR or SQL_VARCHAR | DBTYPE_WSTRor DBTYPE_STR | Java.sql.String | String or SqString |

## Convert date and time data

When you convert to date and time data types, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] rejects all values it can't recognize as dates or times. For information about using the CAST and CONVERT functions with date and time data, see [CAST and CONVERT](../functions/cast-and-convert-transact-sql.md)

### Convert other date and time types to the datetime2 data type

This section describes what occurs when other date and time data types are converted to the **datetime2** data type.

When the conversion is from **date**, the year, month, and day are copied. The time component is set to 00:00:00.0000000. The following code shows the results of converting a `date` value to a `datetime2` value.

```sql
DECLARE @date AS DATE = '12-21-16';

DECLARE @datetime2 AS DATETIME2 = @date;

SELECT @datetime2 AS '@datetime2',
       @date AS '@date';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime2                  @date
--------------------------- ----------
2016-12-21 00:00:00.0000000 2016-12-21
```

When the conversion is from **time(n)**, the time component is copied, and the date component is set to `1900-01-01`. The following example shows the results of converting a **time(7)** value to a **datetime2** value.

```sql
DECLARE @time AS TIME (7) = '12:10:16.1234567';

DECLARE @datetime2 AS DATETIME2 = @time;

SELECT @datetime2 AS '@datetime2',
       @time AS '@time';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime2                  @time
--------------------------- ----------------
1900-01-01 12:10:16.1234567 12:10:16.1234567
```

When the conversion is from **smalldatetime**, the hours and minutes are copied. The seconds and fractional seconds are set to 0. The following code shows the results of converting a `smalldatetime` value to a `datetime2` value.

```sql
DECLARE @smalldatetime AS SMALLDATETIME = '12-01-16 12:32';

DECLARE @datetime2 AS DATETIME2 = @smalldatetime;

SELECT @datetime2 AS '@datetime2',
       @smalldatetime AS '@smalldatetime';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime2                  @smalldatetime
--------------------------- -----------------------
2016-12-01 12:32:00.0000000 2016-12-01 12:32:00
```

When the conversion is from **datetimeoffset(n)**, the date and time components are copied. The time zone is truncated. The following example shows the results of converting a `datetimeoffset(7)` value to a `datetime2` value.

```sql
DECLARE @datetimeoffset AS DATETIMEOFFSET (7) = '2016-10-23 12:45:37.1234567 +10:0';

DECLARE @datetime2 AS DATETIME2 = @datetimeoffset;

SELECT @datetime2 AS '@datetime2',
       @datetimeoffset AS '@datetimeoffset';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime2                  @datetimeoffset
--------------------------- ----------------------------------
2016-10-23 12:45:37.1234567 2016-10-23 12:45:37.1234567 +10:00
```

When the conversion is from **datetime**, the date and time are copied. The fractional precision is extended to 7 digits. The following example shows the results of converting a `datetime` value to a `datetime2` value.

```sql
DECLARE @datetime AS DATETIME = '2016-10-23 12:45:37.333';

DECLARE @datetime2 AS DATETIME2 = @datetime;

SELECT @datetime2 AS '@datetime2',
       @datetime AS '@datetime';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@datetime2                  @datetime
----------------------- ---------------------------
2016-10-23 12:45:37.3333333 2016-10-23 12:45:37.333
```

### Cast explicitly to datetime2 when using datetime

Under database compatibility level 130 and greater, implicit conversions from **datetime** to **datetime2** data types show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values, as seen in the previous example. Use explicit casting to **datetime2** data type whenever a mixed comparison scenario between **datetime** and **datetime2** datatypes exists. For more information, see [SQL Server and Azure SQL Database improvements in handling some data types and uncommon operations](/troubleshoot/sql/database-engine/general/sql-server-azure-sql-database-improvements).

### Convert string literals to datetime2

Conversions from string literals to date and time types are permitted if all parts of the strings are in valid formats. Otherwise, a runtime error is raised. Implicit conversions or explicit conversions that don't specify a style, from date and time types to string literals are in the default format of the current session. The following table shows the rules for converting a string literal to the **datetime2** data type.

| Input string literal | **datetime2(*n*)** |
| --- | --- |
| `ODBC DATE` | ODBC string literals are mapped to the **datetime** data type. Any assignment operation from `ODBC DATETIME` literals into **datetime2** types causes an implicit conversion between **datetime** and this type as defined by the conversion rules. |
| `ODBC TIME` | See previous `ODBC DATE` rule. |
| `ODBC DATETIME` | See previous `ODBC DATE` rule. |
| `DATE` only | The `TIME` part defaults to `00:00:00`. |
| `TIME` only | The `DATE` part defaults to `1900-01-01`. |
| `TIMEZONE` only | Default values are supplied. |
| `DATE` + `TIME` | Trivial. |
| `DATE` + `TIMEZONE` | Not allowed. |
| `TIME` + `TIMEZONE` | The `DATE` part defaults to 1900-1-1. `TIMEZONE` input is ignored. |
| `DATE` + `TIME` + `TIMEZONE` | The local `DATETIME` is used. |

## Examples

The following example compares the results of casting a string to each **date** and **time** data type.

```sql
SELECT CAST ('2007-05-08 12:35:29. 1234567 +12:15' AS TIME (7)) AS 'time',
       CAST ('2007-05-08 12:35:29. 1234567 +12:15' AS DATE) AS 'date',
       CAST ('2007-05-08 12:35:29.123' AS SMALLDATETIME) AS 'smalldatetime',
       CAST ('2007-05-08 12:35:29.123' AS DATETIME) AS 'datetime',
       CAST ('2007-05-08 12:35:29. 1234567 +12:15' AS DATETIME2 (7)) AS 'datetime2',
       CAST ('2007-05-08 12:35:29.1234567 +12:15' AS DATETIMEOFFSET (7)) AS 'datetimeoffset';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

| Data type | Output |
| --- | --- |
| **time** | 12:35:29.1234567 |
| **date** | 2007-05-08 |
| **smalldatetime** | 2007-05-08 12:35:00 |
| **datetime** | 2007-05-08 12:35:29.123 |
| **datetime2** | 2007-05-08 12:35:29.1234567 |
| **datetimeoffset** | 2007-05-08 12:35:29.1234567 +12:15 |

## Related content

- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
