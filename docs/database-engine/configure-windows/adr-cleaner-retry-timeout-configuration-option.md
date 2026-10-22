---
title: "Server Configuration: ADR cleaner retry timeout (min)"
description: "Explains the SQL Server instance configuration setting for ADR cleaner retry timeout."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 11/25/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "ADR cleaner retry timeout (min)"
---
# Server configuration: ADR cleaner retry timeout (min)

[!INCLUDE [sqlserver2019-asdbmi](../../includes/applies-to-version/sqlserver2019-asdbmi.md)]

Starting in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], and in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], this configuration setting is used for [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md) (ADR). The cleaner is an asynchronous process that wakes up periodically and cleans row versions that aren't needed.

Occasionally the cleaner might run into issues while acquiring object or partition level `IX` locks due to lock conflicts with user workloads during its sweep. The cleaner tracks such pages in a separate list. `ADR cleaner retry timeout (min)` controls the amount of time the cleaner spends exclusively retrying `IX` lock acquisition and cleanup of pages before it abandons the sweep. Completing the sweep with 100% success is essential to keep the growth of aborted transactions in the aborted transactions map. If the pages on the separate list can't be cleaned up in the prescribed time-out, then the current sweep is abandoned, and the cleanup is attempted during the next sweep.

| Version | Default value |
| --- | --- |
| [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | 120 |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | 15 |

## Remarks

The cleaner is single threaded in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the cleaner is single-threaded by default, but can be made multi-threaded by configuring the `ADR Cleaner Thread Count` server configuration.

If the cleaner is single-threaded, it can only work on one database at a time. If the instance has more than one database with ADR enabled, don't increase the time-out to a large value. Doing so could delay cleanup on one database while the retry is happening on another database.

::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

## Known issue

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 12 and previous versions, this value might be set to `0`. We recommend that you manually reset the value to `120`, which is the designed default, using the example in this article.

## Examples

The following example sets the cleaner retry time-out to the default value.

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

The following example sets the cleaner retry time-out to the default value.

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
