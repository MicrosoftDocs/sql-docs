---
title: "Hybrid Buffer Pool | Microsoft Docs"
ms.custom: ""
ms.date: 05/22/2019
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 
author: briancarrig
ms.author: brcarrig
---
# Hybrid Buffer Pool
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Hybrid Buffer Pool allows the database engine to directly access data pages in database files stored on persistent memory (PMEM) devices. This feature is introduced in [!INCLUDE[sqlv15](../../includes/sssqlv15-md.md)].

In a traditional system without PMEM, SQL Server caches data pages in the buffer pool. With hybrid buffer pool, SQL Server skips performing a copy of the page into the DRAM-based portion of the buffer pool, and instead accesses the page directly on the database file that lives on a PMEM device. Read access to data files on PMEM devices for hybrid buffer pool is performed directly by following a pointer to the data pages on the PMEM device.  

Only clean pages can be accessed directly on a PMEM device. When a page is marked as dirty it is copied to the DRAM buffer pool before eventually being written back to the PMEM device and marked as clean again. This will occur during regular checkpoint operations. The mechanism to copy the file from the PMEM device to DRAM is direct memory-mapped I/O (MMIO) and is also referred to as the *enlightenment* of data files within SQL Server.


The hybrid buffer pool feature is available for both Windows and Linux. The PMEM device must be formatted with a filesystem that supports DAX (DirectAccess). The XFS, EXT4, and NTFS file systems all have support for DAX. SQL Server will automatically detect if data files reside on an appropriately formatted PMEM device and perform memory mapping in user space. This mapping happens upon startup, when a new database is attached, restored, created, or when the hybrid buffer pool feature is enabled for a database.

For more on Windows Server support for PMEM, see [deploy persistent memory on Windows Server](/windows-server/storage/storage-spaces/deploy-pmem/).

For more on configuring SQL Servers on Linux for PMEM devices, see [deploy persistent memory](../../linux/sql-server-linux-configure-pmem.md).

## Enable hybrid buffer pool

[!INCLUDE[sqlv15](../../includes/sssqlv15-md.md)] introduces dynamic data language (DDL) to control hybrid buffer pool.

The following example enables hybrid buffer pool for an instance of SQL Server:

```sql
ALTER SERVER CONFIGURATION SET MEMORY_OPTIMIZED HYBRID_BUFFER_POOL = ON;
```

By default, hybrid buffer pool is set to disable at the instance scope. Note in order for the setting change to take effect, the SQL Server instance must be restarted. A restart is needed to facilitate allocating sufficient hash pages, to account for total PMEM capacity on the server.

The following example enables hybrid buffer pool for a specific database.

```sql
ALTER DATABASE <databaseName> SET MEMORY_OPTIMIZED = ON;
```

By default, hybrid buffer pool is set to enable at the database scope.

## Disable hybrid buffer pool

The following example disables hybrid buffer pool for an instance of SQL Server:

```sql
ALTER SERVER CONFIGURATION SET MEMORY_OPTIMIZED HYBRID_BUFFER_POOL = OFF;
```

By default, hybrid buffer pool is set to disable at the instance scope. Note in order for the setting change to take effect, the SQL Server instance must be restarted. A restart is needed to prevent over allocation of hash pages, as PMEM capacity on the server does not need to be accounted for.

The following example disables hybrid buffer pool for a specific database.

```sql
ALTER DATABASE <databaseName> SET MEMORY_OPTIMIZED = OFF;
```

By default, hybrid buffer pool is set to enable at the database scope.

## View hybrid buffer pool configuration

The following example returns the current status, of the hybrid buffer pool system configuration, for an instance of SQL Server.

```sql
SELECT * FROM
sys.server_memory_optimized_hybrid_buffer_pool_configuration;
```

The following example returns two tables:

- The first shows the current status of hybrid buffer pool system configuration for an instance of SQL Server.
- The second lists the databases and the database level setting for hybrid buffer pool (`is_memory_optimized_enabled`).

```sql
SELECT * FROM sys.configurations WHERE name = 'hybrid_buffer_pool';

SELECT name, is_memory_optimized_enabled FROM sys.databases;
```

## Best Practices for hybrid buffer pool

When formatting your PMEM device on Windows, use the largest allocation unit size available for NTFS (2 MB in Windows Server 2019) and ensure the device has been formatted for DAX (Direct Access).

For optimal performance, enable [Locked Pages in Memory](./enable-the-lock-pages-in-memory-option-windows.md) on Windows.

Files sizes should be a multiple of 2 MB (modulo 2 MB should equal zero).

If the server scoped setting for Hybrid buffer pool is set to disabled, Hybrid buffer pool will not be used by any user database.

If the server scoped setting for Hybrid Buffer is enabled, you can disable Hybrid buffer pool usage for individual user databases by following the steps to disable Hybrid buffer pool at the database scoped level for those user databases.
