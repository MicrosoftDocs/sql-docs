---
title: CURRENT_TIMESTAMP (Transact-SQL)
description: CURRENT_TIMESTAMP returns the current database system timestamp as a datetime value, without the database time zone offset.
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf, randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CURRENT_TIMESTAMP"
  - "CURRENT_TIMESTAMP_TSQL"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "niladic functions"
  - "current date and time [SQL Server]"
  - "time [SQL Server], current"
  - "date and time [SQL Server], CURRENT_TIMESTAMP"
  - "functions [SQL Server], time"
  - "system date and time [SQL Server]"
  - "system time [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], current date and time"
  - "dates [SQL Server], system date and time"
  - "CURRENT_TIMESTAMP function [SQL Server]"
  - "time [SQL Server], system"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# CURRENT_TIMESTAMP (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

The `CURRENT_TIMESTAMP` Transact-SQL (T-SQL) function returns the current database system timestamp as a **datetime** value, without the database time zone offset. `CURRENT_TIMESTAMP` derives this value from the operating system of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs.

> [!NOTE]  
> `SYSDATETIME` and `SYSUTCDATE` have more precision, as measured by fractional seconds precision, than `GETDATE` and `GETUTCDATE`. The `SYSDATETIMEOFFSET` function includes the system time zone offset. You can assign `SYSDATETIME`, `SYSUTCDATETIME`, and `SYSDATETIMEOFFSET` to a variable of any of the date and time types.

This function is the ANSI SQL equivalent to [GETDATE](getdate-transact-sql.md).

[!INCLUDE [change-time-zone](../includes/change-time-zone.md)]

See [Date and time data types and functions](date-and-time-data-types-and-functions-transact-sql.md) for an overview of all the [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CURRENT_TIMESTAMP
```

## Arguments

This function takes no arguments.

## Return types

**datetime**

## Remarks

[!INCLUDE [tsql](../../includes/tsql-md.md)] statements can refer to `CURRENT_TIMESTAMP`, anywhere they can refer to a **datetime** expression.

`CURRENT_TIMESTAMP` is a nondeterministic function. You can't index views and expressions that reference this column.

## Examples

These examples use the six [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system functions that return current date and time values, to return the date, the time, or both. The examples return the values in series, so their fractional seconds might differ. The actual values returned reflect the actual day and time of execution.

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

```sql
SELECT CURRENT_TIMESTAMP;
```

## Related content

- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
- [SYSDATETIME (Transact-SQL)](sysdatetime-transact-sql.md)
- [SYSDATETIMEOFFSET (Transact-SQL)](sysdatetimeoffset-transact-sql.md)
- [SYSUTCDATETIME (Transact-SQL)](sysutcdatetime-transact-sql.md)
- [GETDATE (Transact-SQL)](getdate-transact-sql.md)
- [GETUTCDATE (Transact-SQL)](getutcdate-transact-sql.md)
