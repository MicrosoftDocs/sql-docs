---
title: SET TIME ZONE (Transact-SQL)
description: The SET TIME ZONE statements set a time zone value in Azure SQL Database and SQL database in Microsoft Fabric.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SET TIME ZONE"
  - "SET_TIME_ZONE_TSQL"
helpviewer_keywords:
  - "time zones, SET TIME ZONE statement"
  - "SET TIME ZONE option"
  - "SET TIME ZONE statement"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =fabric-sqldb"
---
# SET TIME ZONE (Transact-SQL)

[!INCLUDE [asdb-fabricsqldb](../../includes/applies-to-version/asdb-fabricsqldb.md)]

The `SET TIME ZONE` Transact-SQL (T-SQL) statement is used to set a time zone value in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

> [!NOTE]  
> `SET TIME ZONE` is not supported in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

`SET TIME ZONE` overrides the database or instance level time zone value. The time zone value is the same as the `name` column in the [sys.time_zone_info](../../relational-databases/system-catalog-views/sys-time-zone-info-transact-sql.md) system view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET TIME ZONE { 'time_zone_value' | 'LOCAL' }
[ ; ]
```

## Arguments

#### { '*time_zone_value*' | 'LOCAL' }

Specifies the time zone value. *time_zone_value* is **nvarchar(128)**, with a default of `LOCAL`.

If `LOCAL` is specified, then the current default time zone value of the session is set to the original time zone value of the session, which can be either database scoped option or the instance default.

A list of installed time zones is exposed through the [sys.time_zone_info](../../relational-databases/system-catalog-views/sys-time-zone-info-transact-sql.md) system view.

## Remarks

If `SET TIME ZONE` is used in a T-SQL module or dynamic SQL batch, then the change applies only to the scope of execution of the module or dynamic SQL batch. After the module or dynamic SQL batch execution completes, the session time zone setting reverts to the time zone value in effect before the execution of the module or dynamic SQL batch.

## Permissions

Requires membership in the **public** fixed database role.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. View the current time zone settings

The following query returns the server name, database name, and product version.

```sql
SELECT @@servername AS ServerName,
       DB_NAME() AS DatabaseName,
       SERVERPROPERTY('ResourceVersion') AS ProductVersion;
```

The following query lists the valid time zones, ordered by their UTC offset.

```sql
SELECT *
FROM sys.time_zone_info
ORDER BY current_utc_offset;
```

The following query checks the current value of the `TIME_ZONE` database scoped configuration.

```sql
SELECT *
FROM sys.database_scoped_configurations
WHERE name = 'TIME_ZONE';
```

If the time zone isn't set at the database level, it defaults to the instance time zone. The instance time zone setting depends on the platform:

| Platform | Description |
| --- | --- |
| Windows | The time zone of the underlying OS. |
| Linux | The time zone of the SQL Server process, which you can set using the `TZ` environment variable. |
| [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] | The time zone of the SQL instance, which is always UTC and can't be changed today. |
| [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] | The time zone of the SQL instance, which is UTC by default and can be modified at time of instance creation only today. |

### B. Set the time zone at the database level

The following example sets the database scoped time zone to New Zealand Standard Time, then reviews the affected functions.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TIME_ZONE = 'New Zealand Standard Time';
GO

SELECT CURRENT_TIMEZONE() AS CurrentTimeZone,
       CURRENT_TIMEZONE_ID() AS CurrentTimeZoneId,
       SYSDATETIMEOFFSET() AS CurrentDateTimeOffset,
       SYSDATETIME() AS CurrentLocalDateTime2,
       GETDATE() AS CurrentLocalDateTime3,
       CURRENT_TIMESTAMP AS CurrentLocalDateTime4;
```

### C. Reset the database-scoped time zone to the default

The following example sets the database scoped time zone back to `LOCAL`, then reviews the affected functions.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TIME_ZONE = LOCAL;
GO

SELECT CURRENT_TIMEZONE() AS CurrentTimeZone,
       CURRENT_TIMEZONE_ID() AS CurrentTimeZoneId,
       SYSDATETIMEOFFSET() AS CurrentDateTimeOffset,
       SYSDATETIME() AS CurrentLocalDateTime2,
       GETDATE() AS CurrentLocalDateTime,
       CURRENT_TIMESTAMP AS CurrentLocalDateTime2;
```

### D. Set the time zone at the session level

The following example sets the session time zone to Pacific Standard Time and then to India Standard Time, reviewing the affected functions after each change.

```sql
SET TIME ZONE 'Pacific Standard Time';

SELECT CURRENT_TIMEZONE() AS CurrentTimeZone, CURRENT_TIMEZONE_ID() AS CurrentTimeZoneId,
       SYSDATETIMEOFFSET() AS CurrentDateTimeOffset, SYSDATETIME() AS CurrentLocalDateTime2,
       GETDATE() AS CurrentLocalDateTime3, CURRENT_TIMESTAMP AS CurrentLocalDateTime4;
GO

SET TIME ZONE 'India Standard Time';

SELECT CURRENT_TIMEZONE() AS CurrentTimeZone, CURRENT_TIMEZONE_ID() AS CurrentTimeZoneId,
       SYSDATETIMEOFFSET() AS CurrentDateTimeOffset, SYSDATETIME() AS CurrentLocalDateTime2,
       GETDATE() AS CurrentLocalDateTime3, CURRENT_TIMESTAMP AS CurrentLocalDateTime4;
```

### E. Reset the session time zone to the default

The following example resets the session to the instance or platform default time zone.

```sql
SET TIME ZONE local;
GO

SELECT CURRENT_TIMEZONE() AS CurrentTimeZone,
       CURRENT_TIMEZONE_ID() AS CurrentTimeZoneId,
       SYSDATETIMEOFFSET() AS CurrentDateTimeOffset,
       SYSDATETIME() AS CurrentLocalDateTime2,
       GETDATE() AS CurrentLocalDateTime3,
       CURRENT_TIMESTAMP AS CurrentLocalDateTime4;
```

### F. System views always return the instance time zone

System views that include time values always return the instance time zone, regardless of the session or database time zone.

```sql
SELECT *
FROM sys.dm_exec_sessions;

SELECT *
FROM sys.dm_exec_query_stats;

SELECT *
FROM sys.objects;
```

## Related content

- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)
- [sys.time_zone_info (Transact-SQL)](../../relational-databases/system-catalog-views/sys-time-zone-info-transact-sql.md)
