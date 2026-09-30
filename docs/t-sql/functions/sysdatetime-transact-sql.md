---
title: SYSDATETIME (Transact-SQL)
description: SYSDATETIME returns a datetime2(7) value that contains the date and time of the computer running the Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYSDATETIME_TSQL"
  - "SYSDATETIME"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "date and time [SQL Server], SYSDATETIME"
  - "current date and time [SQL Server]"
  - "functions [SQL Server], time"
  - "system date and time [SQL Server]"
  - "system time [SQL Server]"
  - "SYSDATETIME function [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], current date and time"
  - "dates [SQL Server], system date and time"
  - "time [SQL Server], system"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SYSDATETIME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

The `SYSDATETIME` Transact-SQL (T-SQL) function returns a **datetime2(7)** value that contains the date and time of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.

> [!NOTE]  
> `SYSDATETIME` and `SYSUTCDATETIME` have more fractional seconds precision than `GETDATE` and `GETUTCDATE`. `SYSDATETIMEOFFSET` includes the system time zone offset. `SYSDATETIME`, `SYSUTCDATETIME`, and `SYSDATETIMEOFFSET` can be assigned to a variable of any of the date and time types.

Azure Synapse Analytics follows UTC. Use [AT TIME ZONE](../queries/at-time-zone-transact-sql.md) in Azure Synapse Analytics if you need to interpret date and time information in a non-UTC time zone.

[!INCLUDE [change-time-zone](../includes/change-time-zone.md)]

See [Date and time data types and functions](date-and-time-data-types-and-functions-transact-sql.md) for an overview of all the [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SYSDATETIME ( )
```

## Return types

**datetime2(7)**

## Remarks

[!INCLUDE [tsql](../../includes/tsql-md.md)] statements can refer to `SYSDATETIME` anywhere they can refer to a **datetime2(7)** expression.

`SYSDATETIME` is a nondeterministic function. You can't index views and expressions that reference this function in a column.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] gets the date and time values by using the GetSystemTimeAsFileTime() Windows API. The accuracy depends on the computer hardware and version of Windows on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running. The precision of this API is fixed at 100 nanoseconds. You can use the GetSystemTimeAdjustment() Windows API to determine the accuracy.

## Examples

The following examples use the six [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system functions that return current date and time to return the date, time, or both. The values are returned in series; therefore, their fractional seconds might be different.

In these examples, assume that the current date is April 30, 2026.

### A. Get the current system date and time

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

### B. Get the current system date

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

### C. Get the current system time

```sql
SELECT CONVERT (TIME, SYSDATETIME()),
       CONVERT (TIME, SYSDATETIMEOFFSET()),
       CONVERT (TIME, SYSUTCDATETIME()),
       CONVERT (TIME, CURRENT_TIMESTAMP),
       CONVERT (TIME, GETDATE()),
       CONVERT (TIME, GETUTCDATE());
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
SYSDATETIME()        16:15:37.7418724
SYSDATETIMEOFFSET()  16:15:37.7418724
SYSUTCDATETIME()     22:15:37.7418724
CURRENT_TIMESTAMP    16:15:37.740
GETDATE()            16:15:37.740
GETUTCDATE()         22:15:37.740
```

## Examples: Azure Synapse Analytics

### D. Get the current system date and time

```sql
SELECT SYSDATETIME();
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
--------------------------
4/30/2026 1:10:02 PM
```

## Related content

- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
- [Date and time data types and functions (Transact-SQL)](date-and-time-data-types-and-functions-transact-sql.md)
