---
title: SYSDATETIMEOFFSET (Transact-SQL)
description: SYSDATETIMEOFFSET returns a datetimeoffset(7) value that contains the date and time (including offset) of the computer running the Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYSDATETIMEOFFSET_TSQL"
  - "SYSDATETIMEOFFSET"
helpviewer_keywords:
  - "date and time [SQL Server], SYSDATETIMEOFFSET"
  - "dates [SQL Server], functions"
  - "current date and time [SQL Server]"
  - "functions [SQL Server], time"
  - "system date and time [SQL Server]"
  - "system time [SQL Server]"
  - "SYSDATETIMEOFFSET function [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], current date and time"
  - "dates [SQL Server], system date and time"
  - "time zones [SQL Server]"
  - "time [SQL Server], system"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# SYSDATETIMEOFFSET (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

The `SYSDATETIMEOFFSET` Transact-SQL (T-SQL) function returns a **datetimeoffset(7)** value that contains the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. The time zone offset is included.

For an overview of all [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and time data types and functions](date-and-time-data-types-and-functions-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SYSDATETIMEOFFSET ( )
```

## Return types

**datetimeoffset(7)**

## Remarks

[!INCLUDE [tsql](../../includes/tsql-md.md)] statements can refer to SYSDATETIMEOFFSET anywhere they can refer to a **datetimeoffset** expression.

SYSDATETIMEOFFSET is a nondeterministic function. Views and expressions that reference this function in a column can't be indexed.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] obtains the date and time values by using the GetSystemTimeAsFileTime() Windows API. The accuracy depends on the computer hardware and version of Windows on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. The precision of this API is fixed at 100 nanoseconds. The accuracy can be determined by using the GetSystemTimeAdjustment() Windows API.

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

The following example shows you how to convert date and time values to `date`.

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

### C. Convert date and time to times

The following example shows you how to convert date and time values to `time`.

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
