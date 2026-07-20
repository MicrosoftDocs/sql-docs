---
title: "SYSUTCDATETIME (Transact-SQL)"
description: SYSUTCDATETIME returns a datetime2 value that contains the current date and time of the system.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 10/20/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYSUTCDATETIME"
  - "SYSUTCDATETIME_TSQL"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "system time [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "date and time [SQL Server], SYSUTCDATETIME"
  - "SYSUTCDATETIME function [SQL Server]"
  - "time [SQL Server], system"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SYSUTCDATETIME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns a **datetime2** value that contains the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. The date and time is returned as UTC time (Coordinated Universal Time). The fractional second precision specification has a range from 1 to 7 digits. The default precision is 7 digits.

Consider:

- `SYSDATETIME` and `SYSUTCDATETIME` have more fractional seconds precision than `GETDATE` and `GETUTCDATE`.

- `SYSDATETIMEOFFSET` includes the system time zone offset.

- `SYSDATETIME`, `SYSUTCDATETIME`, and `SYSDATETIMEOFFSET` can be assigned to a variable of any one of the date and time types.

For an overview of all [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and time data types and functions](date-and-time-data-types-and-functions-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SYSUTCDATETIME ( )
```

## Return types

**datetime2**

## Remarks

[!INCLUDE [tsql](../../includes/tsql-md.md)] statements can refer to `SYSUTCDATETIME` anywhere they can refer to a **datetime2** expression.

`SYSUTCDATETIME` is a nondeterministic function. Views and expressions that reference this function in a column can't be indexed.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] obtains the date and time values by using the `GetSystemTimeAsFileTime()` Windows API. The accuracy depends on the computer hardware and version of Windows on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. The precision of this API is fixed at 100 nanoseconds. The accuracy can be determined by using the `GetSystemTimeAdjustment()` Windows API.

## Examples

The following examples use the six [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system functions that return current date and time to return the date, time, or both. The values are returned in series; therefore, their fractional seconds might be different.

### A. Show the formats that are returned by the date and time functions

The following example shows the different formats that are returned by the date and time functions.

```sql
SELECT SYSDATETIME() AS [SYSDATETIME()],
       SYSDATETIMEOFFSET() AS [SYSDATETIMEOFFSET()],
       SYSUTCDATETIME() AS [SYSUTCDATETIME()],
       CURRENT_TIMESTAMP AS [CURRENT_TIMESTAMP],
       GETDATE() AS [GETDATE()],
       GETUTCDATE() AS [GETUTCDATE()];
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
SYSDATETIME()       2025-10-20 13:10:02.0474381
SYSDATETIMEOFFSET() 2025-10-20 13:10:02.0474381 -07:00
SYSUTCDATETIME()    2025-10-20 20:10:02.0474381
CURRENT_TIMESTAMP   2025-10-20 13:10:02.047
GETDATE()           2025-10-20 13:10:02.047
GETUTCDATE()        2025-10-20 20:10:02.047
```

### B. Convert date and time to date

The following example shows you how to convert date and time values to the **date** data type.

```sql
SELECT CONVERT (DATE, SYSDATETIME()),
       CONVERT (DATE, SYSDATETIMEOFFSET()),
       CONVERT (DATE, SYSUTCDATETIME()),
       CONVERT (DATE, CURRENT_TIMESTAMP),
       CONVERT (DATE, GETDATE()),
       CONVERT (DATE, GETUTCDATE());
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
2025-10-20
2025-10-20
2025-10-20
2025-10-20
2025-10-20
2025-10-20
```

### C. Convert date and time values to time

The following example shows you how to convert date and time values to the **time** data type.

```sql
DECLARE @DATETIME AS DATETIME = GetDate();
DECLARE @TIME AS TIME;
SELECT @TIME = CONVERT (TIME, @DATETIME);
SELECT @TIME AS 'Time',
       @DATETIME AS 'Date Time';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Time             Date Time
13:49:33.6330000 2025-10-20 13:49:33.633
```

## Related content

- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
- [Date and time data types and functions (Transact-SQL)](date-and-time-data-types-and-functions-transact-sql.md)
- [AT TIME ZONE (Transact-SQL)](../queries/at-time-zone-transact-sql.md)
