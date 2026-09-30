---
title: CURRENT_DATE (Transact-SQL)
description: CURRENT_DATE returns the current database system date as a date value, without the database time and time zone offset.
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CURRENT_DATE"
  - "CURRENT_DATE_TSQL"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "niladic functions"
  - "current date and time [SQL Server]"
  - "time [SQL Server], current"
  - "date and time [SQL Server], CURRENT_DATE"
  - "functions [SQL Server], time"
  - "system date and time [SQL Server]"
  - "system date [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], current date and time"
  - "dates [SQL Server], system date and time"
  - "CURRENT_DATE function [SQL Server]"
  - "time [SQL Server], system"
dev_langs:
  - TSQL
monikerRange: ">=sql-server-ver17 || >=sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# CURRENT_DATE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The `CURRENT_DATE` Transact-SQL (T-SQL) function returns the current database system date as a **date** value, without the database time and time zone offset. `CURRENT_DATE` derives this value from the underlying operating system on which the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] runs.

> [!NOTE]  
> `SYSDATETIME` and `SYSUTCDATE` have more precision, as measured by fractional seconds precision, than `GETDATE` and `GETUTCDATE`. The `SYSDATETIMEOFFSET` function includes the system time zone offset. You can assign `SYSDATETIME`, `SYSUTCDATETIME`, and `SYSDATETIMEOFFSET` to a variable of any of the date and time types.

This function is the ANSI SQL equivalent to `CAST(GETDATE() AS DATE)`. For more information, see [GETDATE](getdate-transact-sql.md).

[!INCLUDE [change-time-zone](../includes/change-time-zone.md)]

See [Date and time data types and functions](date-and-time-data-types-and-functions-transact-sql.md) for an overview of all the [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CURRENT_DATE
```

## Arguments

This function takes no arguments.

## Return types

**date**

## Remarks

[!INCLUDE [tsql](../../includes/tsql-md.md)] statements can refer to `CURRENT_DATE` anywhere they can refer to a **date** expression.

`CURRENT_DATE` is a nondeterministic function. Views and expressions that reference this column can't be indexed.

## Examples

These examples use the system functions that return current date and time values, to return the date, the time, or both. The examples return the values in series, so their fractional seconds might differ. The actual values returned reflect the actual day / time of execution.

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

## Related content

- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
