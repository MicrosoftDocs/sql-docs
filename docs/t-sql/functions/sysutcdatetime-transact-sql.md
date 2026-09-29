---
title: SYSUTCDATETIME (Transact-SQL)
description: SYSUTCDATETIME returns a datetime2 value that contains the current date and time of the system.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/20/2026
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
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SYSUTCDATETIME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

The `SYSUTCDATETIME` Transact-SQL (T-SQL) function returns a **datetime2** value that contains the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. The date and time is returned as UTC time (Coordinated Universal Time). The fractional second precision specification has a range from 1 to 7 digits. The default precision is 7 digits.

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
SELECT SYSDATETIME(),
       SYSDATETIMEOFFSET(),
       SYSUTCDATETIME(),
       CURRENT_TIMESTAMP,
       GETDATE(),
       GETUTCDATE(),
       CURRENT_DATE;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
SYSDATETIME()        2026-09-01 16:15:37.7418724
SYSDATETIMEOFFSET()  2026-09-01 16:15:37.7418724 -06:00
SYSUTCDATETIME()     2026-09-01 22:15:37.7418724
CURRENT_TIMESTAMP    2026-09-01 16:15:37.740
GETDATE()            2026-09-01 16:15:37.740
GETUTCDATE()         2026-09-01 22:15:37.740
CURRENT_DATE         2026-09-01
```

### B. Convert date and time to date

The following example shows you how to convert date and time values to the **date** data type.

```sql
SELECT CONVERT (DATE, SYSDATETIME()),
       CONVERT (DATE, SYSDATETIMEOFFSET()),
       CONVERT (DATE, SYSUTCDATETIME()),
       CONVERT (DATE, CURRENT_TIMESTAMP),
       CONVERT (DATE, GETDATE()),
       CONVERT (DATE, GETUTCDATE()),
       CURRENT_DATE;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
SYSDATETIME()        2026-09-01
SYSDATETIMEOFFSET()  2026-09-01
SYSUTCDATETIME()     2026-09-01
CURRENT_TIMESTAMP    2026-09-01
GETDATE()            2026-09-01
GETUTCDATE()         2026-09-01
CURRENT_DATE         2026-09-01
```

### C. Convert date and time values to time

The following example shows you how to convert date and time values to the **time** data type.

```sql
SELECT CONVERT (TIME, SYSDATETIME()),
       CONVERT (TIME, SYSDATETIMEOFFSET()),
       CONVERT (TIME, SYSUTCDATETIME()),
       CONVERT (TIME, CURRENT_TIMESTAMP),
       CONVERT (TIME, GETDATE()),
       CONVERT (TIME, GETUTCDATE());
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
SYSDATETIME()        16:15:37.7418724
SYSDATETIMEOFFSET()  16:15:37.7418724
SYSUTCDATETIME()     22:15:37.7418724
CURRENT_TIMESTAMP    16:15:37.740
GETDATE()            16:15:37.740
GETUTCDATE()         22:15:37.740
```

## Related content

- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
- [Date and time data types and functions (Transact-SQL)](date-and-time-data-types-and-functions-transact-sql.md)
- [AT TIME ZONE (Transact-SQL)](../queries/at-time-zone-transact-sql.md)
