---
title: CURRENT_TIMEZONE (Transact-SQL)
description: CURRENT_TIMEZONE returns the name of the time zone observed by a server or an instance.
author: MladjoA
ms.author: mlandzic
ms.reviewer: randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CURRENT_TIMEZONE"
  - "CURRENT_TIMEZONE_TSQL"
helpviewer_keywords:
  - "current time zone [SQL Server]"
  - "current timezone [SQL Server]"
  - "system time zone [SQL Server]"
  - "system timezone [SQL Server]"
  - "functions [SQL Server], time zone"
  - "functions [SQL Server], timezone"
  - "timezone [SQL Server], functions"
  - "time zone [SQL Server], functions"
  - "CURRENT_TIMEZONE function [SQL Server]"
dev_langs:
  - TSQL
---
# CURRENT_TIMEZONE (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

The `CURRENT_TIMEZONE` Transact-SQL (T-SQL) function returns the name of the time zone observed by a server or an instance. For SQL Managed Instance, the function returns the time zone of the instance itself assigned during instance creation, not the time zone of the underlying operating system.

[!INCLUDE [change-time-zone](../includes/change-time-zone.md)]

See [Date and time data types and functions](date-and-time-data-types-and-functions-transact-sql.md) for an overview of all the [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CURRENT_TIMEZONE ( )
```

## Arguments

This function takes no arguments.

## Return types

**varchar**

## Remarks

`CURRENT_TIMEZONE` is a non-deterministic function. You can't index views and expressions that reference this column.

## Examples

The value returned reflects the actual time zone and language settings of the server or the instance.

```sql
SELECT CURRENT_TIMEZONE();
```

The result returns `(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna`.

## Related content

- [SQL Managed Instance Time Zone](/azure/sql-database/sql-database-managed-instance-timezone)
- [CURRENT_TIMEZONE_ID (Transact-SQL)](current-timezone-id-transact-sql.md)
