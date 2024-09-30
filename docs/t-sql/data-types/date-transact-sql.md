---
title: "date (Transact-SQL)"
description: Defines a date in the SQL Server Database Engine.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/12/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "date_TSQL"
  - "date"
helpviewer_keywords:
  - "data types [SQL Server], date"
  - "date and time [SQL Server], date"
  - "dates [SQL Server], data types"
  - "date data type [SQL Server]"
  - "data types [SQL Server], date and time"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# date (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Defines a date in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The **date** data type was introduced in [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)].

## date description

| Property | Value |
| --- | --- |
| Syntax | `DATE` |
| Usage | `DECLARE @MyDate DATE`<br /><br />`CREATE TABLE Table1 (Column1 DATE)` |
| Default string literal format<br /><br />(used for down-level client) | `yyyy-MM-dd`<br /><br />For more information, see the [Backward compatibility for down-level clients](#backward-compatibility-for-down-level-clients) section. |
| Range | `0001-01-01` through `9999-12-31` (`1582-10-15` through `9999-12-31` for Informatica)<br /><br />January 1, 1 CE (Common Era) through December 31, 9999 CE (October 15, 1582 CE through December 31, 9999 CE for Informatica) |
| Element ranges | `yyyy` is four digits from `0001` to `9999` that represent a year. Informatica limits `yyyy` to the range `1582` to `9999`.<br /><br />`MM` is two digits from `01` to `12` that represent a month in the specified year.<br /><br />`dd` is two digits from `01` to `31`, depending on the month, which represents a day of the specified month. |
| Character length | 10 positions |
| Precision, scale | `10, 0` |
| Storage size | 3 bytes, fixed |
| Storage structure | one 3-byte integer stores **date** |
| Accuracy | One day |
| Default value | `1900-01-01`<br /><br />This value is used for the appended date part for implicit conversion from **time** to **datetime2** or **datetimeoffset**. |
| Calendar | Gregorian |
| User-defined fractional second precision | No |
| Time zone offset aware and preservation | No |
| Daylight saving aware | No |

## Supported string literal formats for date

The following tables show the valid string literal formats for the **date** data type.

| Numeric | Description |
| --- | --- |
| `SET DATEFORMAT mdy`:<br /><br />`[m]m/dd/[yy]yy`<br /><br />`[m]m-dd-[yy]yy`<br /><br />`[m]m.dd.[yy]yy`<br /><br />`SET DATEFORMAT myd`:<br /><br />`[m]m/[yy]yy/dd`<br /><br />`[m]m-[yy]yy-dd`<br /><br />`[m]m.[yy]yy.dd`<br /><br />`SET DATEFORMAT dmy`:<br /><br />`dd/[m]m/[yy]yy`<br /><br />`dd-[m]m-[yy]yy`<br /><br />`dd.[m]m.[yy]yy`<br /><br />`SET DATEFORMAT dym`:<br /><br />`dd/[yy]yy/[m]m`<br /><br />`dd-[yy]yy-[m]m`<br /><br />`dd.[yy]yy.[m]m`<br /><br />`SET DATEFORMAT ymd`:<br /><br />`[yy]yy/[m]m/dd`<br /><br />`[yy]yy-[m]m-dd`<br /><br />`[yy]yy-[m]m-dd` | `[m]m`, `dd`, and `[yy]yy` represent month, day, and year in a string with slash marks (`/`), hyphens (`-`), or periods (`.`) as separators.<br /><br />Only four-digit or two-digit years are supported. Use four-digit years whenever possible. To specify an integer from `0001` to `9999` that represents the cutoff year for interpreting two-digit years as four-digit years, use the [Two digit year cutoff](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) server configuration option.<br /><br />**Note:** For Informatica, `yyyy` is limited to the range `1582` to `9999`.<br /><br />A two-digit year that is less than or equal to the last two digits of the cutoff year is in the same century as the cutoff year. A two-digit year greater than the last two digits of the cutoff year is in the century that comes before the cutoff year. For example, if the two-digit year cutoff is the default `2049`, the two-digit year `49` is interpreted as `2049` and the two-digit year `50` is interpreted as `1950`.<br /><br />The current language setting determines the default date format. You can change the date format by using the [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md) and [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md) statements.<br /><br />The `ydm` format isn't supported for **date**. |

| Alphabetical | Description |
| --- | --- |
| `mon [dd][,] yyyy`<br /><br />`mon dd[,] [yy]`<br /><br />`mon yyyy [dd]`<br /><br />`[dd] mon[,] yyyy`<br /><br />`dd mon[,][yy]yy`<br /><br />`dd [yy]yy mon`<br /><br />`[dd] yyyy mon`<br /><br />`yyyy mon [dd]`<br /><br />`yyyy [dd] mon` | `mon` represents the full month name, or the month abbreviation, given in the current language. Commas are optional and capitalization is ignored.<br /><br />To avoid ambiguity, use four-digit years.<br /><br />If the day is missing, the first day of the month is supplied. |

| ISO 8601 | Description |
| --- | --- |
| `yyyy-MM-dd`<br /><br />`yyyyMMdd` | Same as the SQL standard. This format is the only format defined as an international standard. |

| Unseparated | Description |
| --- | --- |
| `[yy]yyMMdd`<br /><br />`yyyy[MMdd]` | The **date** data can be specified with four, six, or eight digits. A six-digit or eight-digit string is always interpreted as `ymd`. The month and day must always be two digits. A four-digit string is interpreted as the year. |

| ODBC | Description |
| --- | --- |
| `{ d 'yyyy-MM-dd' }` | ODBC API specific. |

| W3C XML format | Description |
| --- | --- |
| `yyyy-MM-ddTZD` | Supported for XML/SOAP usage.<br /><br />`TZD` is the time zone designator (`Z` or `+hh:mm` or `-hh:mm`):<br /><br />- `hh:mm` represents the time zone offset. `hh` is two digits, ranging from `0` to `14`, which represent the number of hours in the time zone offset.<br />- `mm` is two digits, ranging from `0` to `59`, which represent the number of additional minutes in the time zone offset.<br />- `+` (plus) or `-` (minus) is the mandatory sign of the time zone offset. This sign indicates that, to obtain the local time, the time zone offset is added or subtracted from the Coordinated Universal Times (UTC) time. The valid range of time zone offset is from `-14:00` to `+14:00`. |

## ANSI and ISO 8601 compliance

**date** complies with the ANSI SQL standard definition for the Gregorian calendar:

Datetime data types will allow dates in the Gregorian format to be stored in the date range 0001-01-01 CE through 9999-12-31 CE.

The default string literal format, which is used for down-level clients, complies with the SQL standard form that is defined as `yyyy-MM-dd`. This format is the same as the ISO 8601 definition for `DATE`.

> [!NOTE]  
> For Informatica, the range is limited to `1582-10-15` (October 15, 1582 CE) to `9999-12-31` (December 31, 9999 CE).

## Backward compatibility for down-level clients

Some down-level clients don't support the **time**, **date**, **datetime2**, and **datetimeoffset** data types. The following table shows the type mapping between an up-level instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and down-level clients.

| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type | Default string literal format passed to down-level client | Down-level ODBC | Down-level OLEDB | Down-level JDBC | Down-level SQLCLIENT |
| --- | --- | --- | --- | --- | --- |
| **time** | `hh:mm:ss[.nnnnnnn]` | `SQL_WVARCHAR` or `SQL_VARCHAR` | `DBTYPE_WSTR` or `DBTYPE_STR` | `Java.sql.String` | `String` or `SqString` |
| **date** | `yyyy-MM-dd` | `SQL_WVARCHAR` or `SQL_VARCHAR` | `DBTYPE_WSTR` or `DBTYPE_STR` | `Java.sql.String` | `String` or `SqString` |
| **datetime2** | `yyyy-MM-dd HH:mm:ss[.nnnnnnn]` | `SQL_WVARCHAR` or `SQL_VARCHAR` | `DBTYPE_WSTR` or `DBTYPE_STR` | `Java.sql.String` | `String` or `SqString` |
| **datetimeoffset** | `yyyy-MM-dd HH:mm:ss[.nnnnnnn] [+ or -]hh:mm` | `SQL_WVARCHAR` or `SQL_VARCHAR` | `DBTYPE_WSTR` or `DBTYPE_STR` | `Java.sql.String` | `String` or `SqString` |

## Convert date and time data

When you convert to date and time data types, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it doesn't recognize as dates or times. For information about using the `CAST` and `CONVERT` functions with date and time data, see [CAST and CONVERT (Transact-SQL)](../../t-sql/functions/cast-and-convert-transact-sql.md).

### Convert date to other date and time types

This section describes what occurs when you convert a **date** data type to other date and time data types.

When the conversion is to **time(*n*)**, the conversion fails, and error message 206 is raised:

> Operand type clash: date is incompatible with time.

If the conversion is to **datetime**, the date component is copied. The following code shows the results of converting a **date** value to a **datetime** value.

```sql
DECLARE @date DATE = '12-10-25';
DECLARE @datetime DATETIME = @date;

SELECT @date AS '@date',
    @datetime AS '@datetime';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@date      @datetime
---------- -----------------------
2025-12-10 2025-12-10 00:00:00.000
```

When the conversion is to **smalldatetime**, the **date** value is in the range of a [smalldatetime](smalldatetime-transact-sql.md), the date component is copied, and the time component is set to `00:00:00.000`. When the **date** value is outside the range of a **smalldatetime** value, error message 242 is raised, and the **smalldatetime** value is set to `NULL`:

> The conversion of a date data type to a smalldatetime data types resulted in an out-of-range value.

The following code shows the results of converting a **date** value to a **smalldatetime** value.

```sql
DECLARE @date DATE = '1912-10-25';
DECLARE @smalldatetime SMALLDATETIME = @date;

SELECT @date AS '@date',
    @smalldatetime AS '@smalldatetime';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@date      @smalldatetime
---------- -------------------
1912-10-25 1912-10-25 00:00:00
```

For conversion to **datetimeoffset(*n*)**, date is copied, and the time is set to `00:00.0000000 +00:00`. The following code shows the results of converting a **date** value to a **datetimeoffset(3)** value.

```sql
DECLARE @date DATE = '1912-10-25';
DECLARE @datetimeoffset DATETIMEOFFSET(3) = @date;

SELECT @date AS '@date',
    @datetimeoffset AS '@datetimeoffset';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@date      @datetimeoffset
---------- ------------------------------
1912-10-25 1912-10-25 00:00:00.000 +00:00
```

When the conversion is to **datetime2(*n*)**, the date component is copied, and the time component is set to `00:00.000000`. The following code shows the results of converting a **date** value to a **datetime2(3)** value.

```sql
DECLARE @date DATE = '1912-10-25'
DECLARE @datetime2 DATETIME2(3) = @date;

SELECT @date AS '@date',
    @datetime2 AS '@datetime2(3)';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@date      @datetime2(3)
---------- -----------------------
1912-10-25 1912-10-25 00:00:00.000
```

### Convert string literals to date

Conversions from string literals to date and time types are allowed if all parts of the strings are in valid formats. Otherwise, a runtime error is raised. Implicit conversions or explicit conversions that don't specify a style, from date and time types to string literals, are in the default format of the current session. The following table shows the rules for converting a string literal to the **date** data type.

| Input string literal | date |
| --- | --- |
| ODBC DATE | ODBC string literals are mapped to the **datetime** data type. Any assignment operation from ODBC DATETIME literals into a **date** type causes an implicit conversion between **datetime** and the type that the conversion rules define. |
| ODBC TIME | See previous ODBC DATE rule. |
| ODBC DATETIME | See previous ODBC DATE rule. |
| DATE only | Trivial |
| TIME only | Default values are supplied. |
| TIMEZONE only | Default values are supplied. |
| DATE + TIME | The DATE part of the input string is used. |
| DATE + TIMEZONE | Not allowed. |
| TIME + TIMEZONE | Default values are supplied. |
| DATE + TIME + TIMEZONE | The DATE part of local DATETIME is used. |

## Examples

The following example compares the results of casting a string to each date and time data type.

```sql
SELECT
    CAST('2022-05-08 12:35:29.1234567 +12:15' AS TIME(7)) AS 'time',
    CAST('2022-05-08 12:35:29.1234567 +12:15' AS DATE) AS 'date',
    CAST('2022-05-08 12:35:29.123' AS SMALLDATETIME) AS 'smalldatetime',
    CAST('2022-05-08 12:35:29.123' AS DATETIME) AS 'datetime',
    CAST('2022-05-08 12:35:29.1234567 +12:15' AS DATETIME2(7)) AS 'datetime2',
    CAST('2022-05-08 12:35:29.1234567 +12:15' AS DATETIMEOFFSET(7)) AS 'datetimeoffset';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

| Data type | Output |
| --- | --- |
| **time** | `12:35:29.1234567` |
| **date** | `2022-05-08` |
| **smalldatetime** | `2022-05-08 12:35:00` |
| **datetime** | `2022-05-08 12:35:29.123` |
| **datetime2** | `2022-05-08 12:35:29.1234567` |
| **datetimeoffset** | `2022-05-08 12:35:29.1234567 +12:15` |

## See also

- [CAST and CONVERT (Transact-SQL)](../../t-sql/functions/cast-and-convert-transact-sql.md)
