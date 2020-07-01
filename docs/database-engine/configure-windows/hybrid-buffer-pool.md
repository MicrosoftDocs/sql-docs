---
title: "Hybrid Buffer Pool | Microsoft Docs"
description: See how Hybrid Buffer Pool makes persistent memory devices accessible via the memory bus. Turn this SQL Server 2019 feature on or off, and view best practices.
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
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Hybrid Buffer Pool enables buffer pool objects to reference data pages in database files residing on persistent memory (PMEM) devices, instead of copies of the data pages cached in volatile DRAM. This feature is introduced in [!INCLUDE[sqlv15](../../includes/sssqlv15-md.md)].

![Hybrid Buffer Pool](./media/hybrid-buffer-pool.png)

Persistent memory (PMEM) devices are byte-addressable and if a direct access (DAX) persistent-memory aware file system (such as XFS, EXT4, or NTFS) is used, files on the file system can be accessed using the usual file system APIs in the OS. Alternatively, it can perform what is known as load and store operations against memory maps of the files on the device. This allows PMEM aware applications such as SQL Server to access files on the device without traversing the traditional storage stack.

The hybrid buffer pool uses this ability to perform load and store operations against memory mapped files, to leverage the PMEM device as cache for the buffer pool as well as storing database files. This creates the unique situation where both a logical read and a physical read are essentially the same operation. Persistent memory devices are accessible via the memory bus just like regular volatile DRAM.

Only clean data pages are cached on the device for the Hybrid Buffer Pool. When a page is marked as dirty, it is copied to the DRAM buffer pool before eventually being written back to the PMEM device and marked as clean again. This will occur during regular checkpoint operations in a manner similar to that performed against a standard block device.

The hybrid buffer pool feature is available for both Windows and Linux. The PMEM device must be formatted with a filesystem that supports DAX (DirectAccess). XFS, EXT4, and NTFS file systems all have support for DAX. SQL Server will automatically detect if data files reside on an appropriately formatted PMEM device and perform memory mapping of database files upon startup, when a new database is attached, restored, or created.

For more information, see:

* [Understand and deploy persistent memory (Windows)](/windows-server/storage/storage-spaces/deploy-pmem/)
* [Configure persistent memory (PMEM) for SQL Server on Linux](../../linux/sql-server-linux-configure-pmem.md)


## Enable hybrid buffer pool

[!INCLUDE[sqlv15](../../includes/sssqlv15-md.md)] introduces dynamic data language (DDL) to control hybrid buffer pool.

The following example enables hybrid buffer pool for an instance of SQL Server:

```sql
ALTER SERVER CONFIGURATION SET MEMORY_OPTIMIZED HYBRID_BUFFER_POOL = ON;
```

By default, hybrid buffer pool is disabled at the instance scope. Note in order for the setting change to take effect, the SQL Server instance must be restarted. A restart is needed to facilitate allocating sufficient hash pages, to account for total PMEM capacity on the server.

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

By default, hybrid buffer pool is disabled at the instance level. In order for this change to take effect, the instance must be restarted. This ensures enough hash pages are allocated for the buffer pool, as PMEM capacity on the server now needs to be accounted for.

The following example disables hybrid buffer pool for a specific database.

```sql
ALTER DATABASE <databaseName> SET MEMORY_OPTIMIZED = OFF;
```

By default, hybrid buffer pool is enabled at the database scope.

## View hybrid buffer pool configuration

The following example returns the current hybrid buffer pool configuration status of the instance.

```sql
SELECT * FROM
sys.server_memory_optimized_hybrid_buffer_pool_configuration;
```

The following example lists the databases and the database level setting for hybrid buffer pool (`is_memory_optimized_enabled`).

```sql
SELECT name, is_memory_optimized_enabled FROM sys.databases;
```

## Best Practices for hybrid buffer pool

 - When formatting your PMEM device on Windows, use the largest allocation unit size available for NTFS (2 MB in Windows Server 2019) and ensure the device has been formatted for DAX (Direct Access).

 - Use [Locked Pages in Memory](./enable-the-lock-pages-in-memory-option-windows.md) on Windows.

 - Files sizes should be a multiple of 2 MB (modulo 2 MB should equal zero).

 - If the server scoped setting for hybrid buffer pool is disabled, the feature will not be used by any user database.

 - If the server scoped setting for hybrid buffer pool is enabled, you can use the database scoped setting to disable the feature for individual user databases.
