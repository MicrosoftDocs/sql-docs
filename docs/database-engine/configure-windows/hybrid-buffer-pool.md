---
title: "Hybrid buffer pool | Microsoft Docs"
ms.custom: ""
ms.date: "11/06/2018"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 
author: DBArgenis
ms.author: argenisf
manager: craigg
---
# Hybrid buffer pool
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Hybrid buffer pool allows the database engine to directly access data pages in database files stored in persistent memory (PMEM) devices. This feature is introduced in [!INCLUDE[sqlv15](../includes/ssqlv15-md.md)].

In a traditional system without persistent memory, SQL Server caches data pages in the buffer pool. With hybrid buffer pool, SQL Server skips performing a copy of the page into the DRAM-based portion of the buffer pool, and instead references the page directly on the database file on a PMEM device. Access to data files in PMEM for hybrid buffer pool is performed using memory-mapped I/O, also known as enlightenment.

Only clean pages can be referenced directly on a PMEM device. When a page becomes dirty it is kept in DRAM, and then eventually written back to the PMEM device.

This feature is available on both Windows and Linux.

## Enable hybrid buffer pool

SQL Server 2019 preview introduces dynamic data language (DDL) to control hybrid buffer pool.

The following example enables hybrid buffer pool for an instance of SQL Server:

```sql
ALTER SERVER CONFIGURATION SET MEMORY_OPTIMIZED HYBRID_BUFFER_POOL = ON;
```

The following example returns the current status of hybrid buffer pool system configuration for an instance of SQL Server.

```sql
SELECT *
FROM sys.configurations
WHERE
name = 'hybrid_buffer_pool';
```

The following example enables hybrid buffer pool for a specific database.

```sql
ALTER DATABASE <databaseName> SET MEMORY_OPTIMIZED = ON;
```

The following example returns two tables:

- The first shows the current status of hybrid buffer pool system configuration for an instance of SQL Server.
- The second lists the databases and the database level setting for hybrid buffer pool (`is_memory_optimized`).

```sql
select * from sys.configurations where name = 'hybrid_buffer_pool';

select name, is_memory_optimized_enabled from sys.databases;
```

## Best Practices for hybrid buffer pool

When formatting your PMEM device on Windows use the largest allocation unit size available for NTFS (2MB in Windows Server 2019) and ensure the device has been enabled for DAX (DirectAccess)
