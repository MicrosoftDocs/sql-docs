---
title: "Hybrid Buffer Pool | Microsoft Docs"
ms.custom: ""
ms.date: 10/31/2019
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 
author: briancarrig
ms.author: brcarrig
manager: amitban
---
# Hybrid Buffer Pool
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Hybrid Buffer Pool enables buffer pool objects to reference data pages in database files residing on persistent memory (PMEM) devices, instead of copies of the data pages cached in volatile DRAM. This feature is introduced in [!INCLUDE[sqlv15](../../includes/sssqlv15-md.md)].

![Hybrid Buffer Pool](./media/hybrid-buffer-pool.png)

Persistent memory (PMEM) devices are byte-addressable and if a direct access (DAX) persistent-memory aware file system (XFS, EXT4 or NTFS) is used, files on the file system can be accessed using the usual file system APIs in the OS. Alternatively, it can perform what is known as load and store operations against memory mapped regions corresponding to files on the device. This allows an application such as SQL Server to access data files without traversing the traditional storage stack.

The hybrid buffer pool uses this ability to perform load/store/flush operations against memory mapped regions of the persistent memory device to use the PMEM device as a cache for buffer pool objects to read data pages from the data files. This creates the unique situation where the storage for the persistent memory device, also serves as the cache for database data files rather than regions of volatile DRAM on the server. Persistent memory devices are accessible via the memory bus and connect to memory channels just like DRAM.

Only clean pages are cached on the device as part of the Hybrid Buffer Pool. When a cached page is marked as dirty it is copied to the DRAM buffer pool before eventually being written back to the PMEM device and marked as clean again. This will occur during regular checkpoint operations in a manner similar to that performed against a block device.

The hybrid buffer pool feature is available for both Windows and Linux. The PMEM device must be formatted with a filesystem that supports DAX (DirectAccess). XFS, EXT4, and NTFS file systems all have support for DAX. SQL Server will automatically detect if data files reside on an appropriately formatted PMEM device and perform memory mapping in user space upon startup, when a new database is attached, restored or created or when the hybrid buffer pool feature is enabled.

For more on Windows Server support for PMEM, also referred to as Storage Class Memory (SCM) see [deploy persistent memory on Windows Server](/windows-server/storage/storage-spaces/deploy-pmem/).

For more on configuring SQL Server on Linux for PMEM devices see [deploy persistent memory](../../linux/sql-server-linux-configure-pmem.md)).


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
SELECT *
FROM sys.configurations
WHERE
    name = 'hybrid_buffer_pool';
```

The following example returns two tables:

- The first shows the current status of hybrid buffer pool system configuration for an instance of SQL Server.
- The second lists the databases and the database level setting for hybrid buffer pool (`is_memory_optimized_enabled`).

```sql
SELECT * FROM sys.configurations WHERE name = 'hybrid_buffer_pool';

SELECT name, is_memory_optimized_enabled FROM sys.databases;
```

## Best Practices for hybrid buffer pool

It is not recommended to enable hybrid buffer pool on instances with less than 16-GB RAM.

When formatting your PMEM device on Windows, use the largest allocation unit size available for NTFS (2 MB in Windows Server 2019) and ensure the device has been formatted for DAX (Direct Access).

Enable large pages.

Files sizes should be a multiple of 2 MB (modulo 2 MB should equal zero).

If the server scoped setting for Hybrid buffer pool is set to disabled, Hybrid buffer pool will not be used by any user database.

If the server scoped setting for Hybrid Buffer is enabled, you can disable Hybrid buffer pool usage for individual user databases by following the steps to disable Hybrid buffer pool at the database scoped level for those user databases.
