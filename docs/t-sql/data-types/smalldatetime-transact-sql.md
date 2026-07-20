---
title: "smalldatetime (Transact-SQL)"
description: Defines a date that is combined with a time of day, based on a 24-hour day with seconds always zero, and no fractional seconds.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 01/16/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "smalldatetime_TSQL"
  - "smalldatetime"
helpviewer_keywords:
  - "time [SQL Server], data types"
  - "smalldatetime data type [SQL Server]"
  - "dates [SQL Server], data types"
  - "date and time [SQL Server], smalldatetime"
  - "data types [SQL Server], date and time"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# smalldatetime (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Defines a date that is combined with a time of day. The time is based on a 24-hour day, with seconds always zero (`:00`) and without fractional seconds.

> [!NOTE]  
> Use the **time**, **date**, **datetime2**, and **datetimeoffset** data types for new work. These types align with the SQL standard, as they're more portable. **time**, **datetime2** and **datetimeoffset** provide more seconds precision. **datetimeoffset** provides time zone support for globally deployed applications.

## smalldatetime description

| Property | Value |
| --- | --- |
| **Syntax** | **smalldatetime** |
| **Usage** | `DECLARE @MySmallDateTime SMALLDATETIME;`<br /><br />`CREATE TABLE Table1 (Column1 SMALLDATETIME);` |
| **Default string literal formats**<br />(used for down-level client) | Not applicable |
| **Date range** | `1900-01-01` through `2079-06-06`<br /><br />January 1, 1900, through June 6, 2079 |
| **Time range** | `00:00:00` through `23:59:59`<br /><br />`2024-05-09 23:59:59` rounds to `2024-05-10 00:00:00` |
| **Element ranges** | `yyyy` is four digits, ranging from 1900 to 2079, which represents a year.<br /><br />`MM` is two digits, ranging from 01 to 12, which represents a month in the specified year.<br /><br />`dd` is two digits, ranging from 01 to 31 depending on the month, which represents a day of the specified month.<br /><br />`HH` is two digits, ranging from 00 to 23, which represents the hour.<br /><br />`mm` is two digits, ranging from 00 to 59, that represents the minute.<br /><br />`ss` is two digits, ranging from 00 to 59, that represents the second. Values that are 29.998 seconds or less are rounded down to the nearest minute. Values of 29.999 seconds or more are rounded up to the nearest minute. |
| **Character length** | 19 positions maximum |
| **Storage size** | 4 bytes, fixed |
| **Accuracy** | One minute |
| **Default value** | `1900-01-01 00:00:00` |
| **Calendar** | Gregorian<br /><br />(Doesn't include the complete range of years.) |
| **User-defined fractional second precision** | No |
| **Time zone offset aware and preservation** | No |
| **Daylight saving aware** | No |

## ANSI and ISO 8601 compliance

**smalldatetime** isn't ANSI or ISO 8601 compliant.

## Convert date and time data

When you convert to date and time data types, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it can't recognize as dates or times. For information about using the `CAST` and `CONVERT` functions with date and time data, see [CAST and CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md).

### Convert smalldatetime to other date and time types

This section describes what occurs when a **smalldatetime** data type is converted to other date and time data types.

For a conversion to **date**, the year, month, and day are copied. The following code shows the results of converting a **smalldatetime** value to a **date** value.

```sql
DECLARE @smalldatetime AS SMALLDATETIME = '1955-12-13 12:43:10';

DECLARE @date AS DATE = @smalldatetime;

SELECT @smalldatetime AS '@smalldatetime',
       @date AS 'date';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@smalldatetime        date
--------------------- ----------
1955-12-13 12:43:00   1955-12-13
```

When the conversion is to **time(*n*)**, the hours, minutes, and seconds are copied. The fractional seconds are set to `0`. The following code shows the results of converting a **smalldatetime** value to a **time(4)** value.

```sql
DECLARE @smalldatetime AS SMALLDATETIME = '1955-12-13 12:43:10';

DECLARE @time AS TIME (4) = @smalldatetime;

SELECT @smalldatetime AS '@smalldatetime',
       @time AS 'time';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@smalldatetime          time
----------------------- -------------
1955-12-13 12:43:00     12:43:00.0000
```

When the conversion is to **datetime**, the **smalldatetime** value is copied to the **datetime** value. The fractional seconds are set to `0`. The following code shows the results of converting a **smalldatetime** value to a **datetime** value.

```sql
DECLARE @smalldatetime AS SMALLDATETIME = '1955-12-13 12:43:10';

DECLARE @datetime AS DATETIME = @smalldatetime;

SELECT @smalldatetime AS '@smalldatetime',
       @datetime AS 'datetime';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@smalldatetime          datetime
----------------------- -----------------------
1955-12-13 12:43:00     1955-12-13 12:43:00.000
```

For a conversion to **datetimeoffset(*n*)**, the **smalldatetime** value is copied to the **datetimeoffset(*n*)** value. The fractional seconds are set to `0`, and the time zone offset is set to `+00:0`. The following code shows the results of converting a **smalldatetime** value to a **datetimeoffset(4)** value.

```sql
DECLARE @smalldatetime AS SMALLDATETIME = '1955-12-13 12:43:10';

DECLARE @datetimeoffset AS DATETIMEOFFSET (4) = @smalldatetime;

SELECT @smalldatetime AS '@smalldatetime',
       @datetimeoffset AS 'datetimeoffset(4)';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@smalldatetime        datetimeoffset(4)
--------------------- ------------------------------
1955-12-13 12:43:00   1955-12-13 12:43:00.0000 +00:0
```

For the conversion to **datetime2(n)**, the **smalldatetime** value is copied to the **datetime2(n)** value. The fractional seconds are set to `0`. The following code shows the results of converting a **smalldatetime** value to a **datetime2(4)** value.

```sql
DECLARE @smalldatetime AS SMALLDATETIME = '1955-12-13 12:43:10';

DECLARE @datetime2 AS DATETIME2 (4) = @smalldatetime;

SELECT @smalldatetime AS '@smalldatetime',
       @datetime2 AS ' datetime2(4)';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
@smalldatetime        datetime2(4)
--------------------- ------------------------
1955-12-13 12:43:00   1955-12-13 12:43:00.0000
```

## Examples

### A. Cast string literals with seconds to smalldatetime

The following example compares the conversion of seconds in string literals to **smalldatetime**.

```sql
SELECT CAST ('2024-05-08 12:35:29' AS SMALLDATETIME),
       CAST ('2024-05-08 12:35:30' AS SMALLDATETIME),
       CAST ('2024-05-08 12:59:59.998' AS SMALLDATETIME);
```

| Input | Output |
| --- | --- |
| `2024-05-08 12:35:29` | `2024-05-08 12:35:00` |
| `2024-05-08 12:35:30` | `2024-05-08 12:36:00` |
| `2024-05-08 12:59:59.998` | `2024-05-08 13:00:00` |

### B. Compare date and time data types

The following example compares the results of casting a string to each date and time data type.

```sql
SELECT CAST ('2024-05-08 12:35:29.1234567 +12:15' AS TIME (7)) AS 'time',
       CAST ('2024-05-08 12:35:29.1234567 +12:15' AS DATE) AS 'date',
       CAST ('2024-05-08 12:35:29.123' AS SMALLDATETIME) AS 'smalldatetime',
       CAST ('2024-05-08 12:35:29.123' AS DATETIME) AS 'datetime',
       CAST ('2024-05-08 12:35:29.1234567 +12:15' AS DATETIME2 (7)) AS 'datetime2',
       CAST ('2024-05-08 12:35:29.1234567 +12:15' AS DATETIMEOFFSET (7)) AS 'datetimeoffset';
```

| Data type | Output |
| --- | --- |
| **time** | `12:35:29.1234567` |
| **date** | `2024-05-08` |
| **smalldatetime** | `2024-05-08 12:35:00` |
| **datetime** | `2024-05-08 12:35:29.123` |
| **datetime2** | `2024-05-08 12:35:29.1234567` |
| **datetimeoffset** | `2024-05-08 12:35:29.1234567 +12:15` |

## Related content

- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
