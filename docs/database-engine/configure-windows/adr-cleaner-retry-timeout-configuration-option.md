---
title: "Server configuration: ADR cleaner retry timeout (min)"
description: "Explains the SQL Server instance configuration setting for ADR cleaner retry timeout."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "ADR cleaner retry timeout (min)"
---
# Server configuration: ADR cleaner retry timeout (min)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [sssql19-starting-md](../../includes/sssql19-starting-md.md)], this configuration setting is required for [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md) (ADR). The cleaner is the asynchronous process that wakes up periodically and cleans page versions that aren't needed.

Occasionally the cleaner runs into issues while acquiring object level locks due to conflicts with user workload during its sweep. It tracks such pages in a separate list. `ADR cleaner retry timeout (min)` controls the amount of time the cleaner would spend exclusively retrying object lock acquisition and cleanup of page before abandoning the sweep. Completion of a sweep with 100 percent success is essential to keep the growth of aborted transactions in the aborted transactions map. If the separate list can't be cleaned up in the prescribed timeout, then the current sweep will be abandoned and the next sweep will start.

| Version | Default value |
| --- | --- |
| [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | 120 |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | 15 |

## Remarks

The cleaner is single threaded in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], and so one [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance can work on one database at a time. If the instance has more than one user database with ADR enabled, then don't increase the timeout to a large value. Doing so could delay cleanup on one database while the retry is happening on another database.

::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

## Known issue

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 12 and previous versions, this value might be set to `0`. We recommend that you manually reset the value to `120`, which is the designed default, using the example in this article.

## Examples

The following example sets the cleaner retry timeout to the default value.

```sql
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
EXEC sp_configure 'ADR cleaner retry timeout', 120;
RECONFIGURE;
GO
```

::: moniker-end

::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

## Examples

The following example sets the cleaner retry timeout to the default value.

```sql
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
EXEC sp_configure 'ADR cleaner retry timeout', 15;
RECONFIGURE;
GO
```

::: moniker-end

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../../relational-databases/accelerated-database-recovery-management.md)
