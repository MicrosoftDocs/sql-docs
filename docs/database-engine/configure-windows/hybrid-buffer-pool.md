---
title: Hybrid buffer pool
description: See how the hybrid buffer pool makes persistent memory devices accessible via the memory bus. Turn this SQL Server feature on or off, and view best practices.
author: briancarrig
ms.author: brcarrig
ms.reviewer: randolphwest
ms.date: 01/23/2023
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---
# Hybrid buffer pool

[!INCLUDE [sqlserver2019](../../includes/applies-to-version/sqlserver2019-and-later.md)]

The hybrid buffer pool enables buffer pool objects to reference data pages in database files residing on persistent memory (PMEM) devices, instead of having to fetch copies of the data pages from disk and caching them in volatile DRAM. This feature was introduced in [!INCLUDE[sqlv15](../../includes/sssql19-md.md)] and is further enhanced in [!INCLUDE[sqlv16](../../includes/sssql22-md.md)].

:::image type="content" source="media/hybrid-buffer-pool/hybrid-buffer-pool.svg" alt-text="Diagram showing the buffer pool, with and without the hybrid buffer pool enabled.":::

Persistent memory (PMEM) devices are byte-addressable and if a direct access (DAX) persistent-memory aware file system (such as XFS, EXT4, or NTFS) is used, files on the file system can be accessed using the usual file system APIs in the OS. Alternatively, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can perform what are known as *load and store* operations against memory maps of the files on the PMEM device. This allows PMEM-aware applications such as [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to access files on the device without traversing the traditional storage stack.

The hybrid buffer pool uses this ability to perform load and store operations against memory mapped files, to use the PMEM device both as a cache for the buffer pool and a storage location for the database files. This creates the unique situation where both a logical read and a physical read become essentially the same operation. Persistent memory devices are accessible via the memory bus just like regular volatile DRAM.

By default, only clean data pages are cached on the PMEM module for the hybrid buffer pool. For a page to be modified and marked as dirty, it must be copied from the PMEM device to a DRAM buffer pool, modified and then eventually a copy of the modified page is written from DRAM back to the PMEM module, at which point it can be marked as clean again. This process occurs using normal background operations such as checkpoint, or the lazy writer, as though the PMEM module were a standard block device.

The hybrid buffer pool feature is available for both Windows and Linux. The PMEM device must use a filesystem that supports DAX (DirectAccess). XFS, EXT4, and the NTFS file systems all have support for DAX extensions, which provides access to the filesystem directly from user space. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] will detect if any database data files reside on an appropriately configured PMEM disk device and automatically perform the necessary memory mapping of the database files upon database startup, or whenever a database is attached, restored, or created.

For more information, see:

- [Configure persistent memory (PMEM) for SQL Server on Windows](configure-persistent-memory.md) ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions).
- [Configure persistent memory (PMEM) for SQL Server on Linux](../../linux/sql-server-linux-configure-pmem.md).

## Enable hybrid buffer pool

[!INCLUDE[sqlv15](../../includes/sssql19-md.md)] introduces dynamic data language (DDL) to control the hybrid buffer pool.

The following example enables hybrid buffer pool for an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

```sql
ALTER SERVER CONFIGURATION SET MEMORY_OPTIMIZED HYBRID_BUFFER_POOL = ON;
```

By default, hybrid buffer pool is disabled at the instance scope. In order for the setting change to take effect, the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance must be restarted. A restart is needed to facilitate allocating sufficient hash pages, to account for the total PMEM capacity on the server.

The following example enables hybrid buffer pool for a specific database.

```sql
ALTER DATABASE <databaseName> SET MEMORY_OPTIMIZED = ON;
```

By default, hybrid buffer pool is enabled at the database scope.

## Disable hybrid buffer pool

The following example disables hybrid buffer pool at the instance level:

```sql
ALTER SERVER CONFIGURATION SET MEMORY_OPTIMIZED HYBRID_BUFFER_POOL = OFF;
```

By default, hybrid buffer pool is disabled at the instance level. In order for this change to take effect, the instance must be restarted. The restart ensures enough hash pages are allocated for the buffer pool, as PMEM capacity on the server now needs to be accounted for.

The following example disables hybrid buffer pool for a specific database.

```sql
ALTER DATABASE <databaseName> SET MEMORY_OPTIMIZED = OFF;
```

By default, hybrid buffer pool is enabled at the database scope, and disabled at the server scope.

## View hybrid buffer pool configuration

### Show run time value

The following example returns the current hybrid buffer pool configuration status of the instance.

```sql
SELECT * FROM
sys.server_memory_optimized_hybrid_buffer_pool_configuration;
```

The following example lists the databases and the database level setting for the hybrid buffer pool (`is_memory_optimized_enabled`).

You can also mount or format the PMEM module without DAX enabled, and treat it as a regular block device (that is, perform I/O via the kernel). When configured this way, no PMEM modules may be used by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to perform byte-addressable operations (that is, all calls will use kernel-space drivers).

```sql
SELECT name, is_memory_optimized_enabled FROM sys.databases;
```

## Hybrid buffer pool with direct write

The hybrid buffer pool with *Direct Write* behavior reduces the number of `memcpy` commands that need to be performed on modified data or index pages residing on PMEM devices. It does this by using the durable persisted log buffer as a means to modify the page without having to copy it into one of the DRAM buffer pools. Instead, pages in database files residing on PMEM devices are modified directly without the need to cache in a DRAM buffer pool and later asynchronously flush to disk. This behavior still adheres to write ahead logging (WAL) semantics, as the (log) records in the persisted transaction log buffer have been written, or hardened, to durable media. Considerable performance gains have been observed for transactional workloads using the hybrid buffer pool and persisted log buffer together in this manner.

To enable direct write mode, enable the hybrid buffer pool and [persisted log buffer](../../relational-databases/databases/add-persisted-log-buffer.md) for a database and enable startup [trace flag 809](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

## Best practices for hybrid buffer pool

- When formatting your PMEM device on Windows, use the largest allocation unit size available for NTFS (2 MB in Windows Server 2019 and later) and ensure the device has been formatted for DAX (Direct Access).

- Enable the [Lock pages in memory](./enable-the-lock-pages-in-memory-option-windows.md) policy on Windows.

- Files sizes should be a multiple of 2 MB (modulo 2 MB should equal zero).

- If the server scoped setting for hybrid buffer pool is disabled, the feature won't be used by any user database.

- With the server scoped setting for hybrid buffer pool enabled, you can use the database scoped setting to disable the feature for individual user databases.

- As of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 3 (see [KB4538118](https://support.microsoft.com/help/4538118)), read caching has been enabled by default, a process where the hottest pages are tracked in the hybrid buffer pool, then automatically promoted to a DRAM buffer pool to improve performance.

- As of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 1, *Direct Write* is the default behavior when the hybrid buffer pool is combined with [persisted log buffer](../../relational-databases/databases/add-persisted-log-buffer.md). This should improve performance for almost all workloads, but there is always a chance of regression and the CU should be tested thoroughly before being applied. If you experience regression due to this behavior change, you can revert to the previous behavior using the start-up [Trace Flag 898](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf898).

- Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 1, [Trace Flag 809](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf809) will be ignored by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] at startup. Both Trace Flag 809 and [Trace Flag 898](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf898) apply to Windows only, and don't apply to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux. The trace flags should only be used when directed to do so by a certified Microsoft Server professional.
