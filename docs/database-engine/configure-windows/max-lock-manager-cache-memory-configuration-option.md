---
title: "Server Configuration: max lock manager cache memory (%)"
description: Explains the max lock manager cache memory instance configuration setting.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: randolphwest
ms.date: 05/26/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "Max lock manager cache memory"
---

# Server configuration: max lock manager cache memory (%)

[!INCLUDE [sqlserver2025-and-later](../../includes/applies-to-version/sqlserver2025-and-later.md)]

The `max lock manager cache memory (%)` server configuration option limits the amount of memory that the lock manager cache can use, as a percentage of the total SQLOS committed memory. By default, the configuration is set to 20 percent.

## Availability

This configuration option is available in the following SQL platforms and versions:

- [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update (CU) 5 and later versions

## Remarks

When a lock is released, the memory used by the lock structure isn't freed, but is cached by the lock manager to avoid the memory allocation overhead in subsequent lock acquisition and to improve performance.

Before [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 5 and in previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the lock manager cache can grow up to the maximum size of lock manager memory, which is 60 percent of the total SQLOS committed memory. A workload can grow the size of the lock manager cache up to this limit. For example, this uncommon situation can occur in large concurrent query workloads when lock escalation is disabled for the [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance. If the lock manager cache grows large, the buffer pool, plan cache, and other memory caches for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance shrink, reducing performance.

In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 5 and later versions, the maximum size of the lock manager cache is limited to 20 percent by default. Lock manager memory can still grow up to 60 percent of the SQLOS committed memory if required by the workload. However, when locks are released, memory is freed rather than cached if the lock manager cache already reached its configured limit.

Setting the `max lock manager cache memory (%)` configuration to a value larger than 20 percent isn't recommended, but is supported for backward compatibility. You can set the value in the 20-60 percent range.

You can monitor the total size of lock manager memory using [sys.dm_os_memory_clerks](../../relational-databases/system-dynamic-management-objects/sys-dm-os-memory-clerks-transact-sql.md), with `OBJECTSTORE_LOCK_MANAGER` as the memory clerk type. On an idle database engine instance, the reported value is the size of lock manager cache memory.

## Examples

### A. Set the maximum size of the lock manager cache memory

The following example sets the max lock manager cache memory to 25 percent:

```sql
EXECUTE sp_configure 'show advanced options', 1;
RECONFIGURE;

EXECUTE sp_configure 'max lock manager cache memory (%)', 25;
RECONFIGURE;
```

### B. Monitor lock manager memory

The following example shows the current size of the lock manager memory. The value includes the memory held by the acquired locks, if any, and the memory cached to improve performance of subsequent lock acquisition.

```sql
SELECT SUM(pages_kb) / 1024. AS lock_manager_cache_memory_mb
FROM sys.dm_os_memory_clerks
WHERE type = 'OBJECTSTORE_LOCK_MANAGER';
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md)
