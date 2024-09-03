---
title: "CURRENT_DATE (Transact-SQL)"
description: CURRENT_DATE returns the current database system date as a date value, without the database time and time zone offset.
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: randolphwest
ms.date: 08/08/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || =azuresqldb-mi-current || =fabric"
---
# CURRENT_DATE (Transact-SQL)

[!INCLUDE [asdb-asdbmi](../../includes/applies-to-version/asdb-asdbmi.md)]

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], this function returns the current database system date as a **date** value, without the database time and time zone offset. `CURRENT_DATE` derives this value from the underlying operating system on the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] runs.

> [!NOTE]  
> `SYSDATETIME` and `SYSUTCDATE` have more precision, as measured by fractional seconds precision, than `GETDATE` and `GETUTCDATE`. The `SYSDATETIMEOFFSET` function includes the system time zone offset. You can assign `SYSDATETIME`, `SYSUTCDATETIME`, and `SYSDATETIMEOFFSET` to a variable of any of the date and time types.

This function is the ANSI SQL equivalent to `CAST(GETDATE() AS DATE)`. For more information, see [GETDATE](getdate-transact-sql.md).

See [Date and time data types and functions](date-and-time-data-types-and-functions-transact-sql.md) for an overview of all the [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

[!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] only:

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

> [!NOTE]  
> [CURRENT_DATE (Transact-SQL)](current-date-transact-sql.md) is available in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] only.


[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| Data type | Value |
| --- | --- |
| `SYSDATETIME()` | 2024-06-26 14:04:21.6172014 |
| `SYSDATETIMEOFFSET()` | 2024-06-26 14:04:21.6172014 -05:00 |
| `SYSUTCDATETIME()` | 2024-06-26 19:04:21.6172014 |
| `CURRENT_TIMESTAMP` | 2024-06-26 14:04:21.617 |
| `GETDATE()` | 2024-06-26 14:04:21.617 |
| `GETUTCDATE()` | 2024-06-26 19:04:21.617 |
| `CURRENT_DATE` | 2024-06-26 |

### B. Get the current system date

```sql
SELECT CONVERT(DATE, SYSDATETIME()),
    CONVERT(DATE, SYSDATETIMEOFFSET()),
    CONVERT(DATE, SYSUTCDATETIME()),
    CONVERT(DATE, CURRENT_TIMESTAMP),
    CONVERT(DATE, GETDATE()),
    CONVERT(DATE, GETUTCDATE()),
    CURRENT_DATE;
```

> [!NOTE]  
> [CURRENT_DATE (Transact-SQL)](current-date-transact-sql.md) is available in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] only.

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| Data type | Value |
| --- | --- |
| `SYSDATETIME()` | 2024-06-26 |
| `SYSDATETIMEOFFSET()` | 2024-06-26 |
| `SYSUTCDATETIME()` | 2024-06-26 |
| `CURRENT_TIMESTAMP` | 2024-06-26 |
| `GETDATE()` | 2024-06-26 |
| `GETUTCDATE()` | 2024-06-26 |
| `CURRENT_DATE` | 2024-06-26 |

## Related content

- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
