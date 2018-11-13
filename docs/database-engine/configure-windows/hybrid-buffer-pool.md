---
title: "Hybrid Buffer Pool | Microsoft Docs"
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
# Hybrid Buffer Pool
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

With SQL Server 2019 CTP 2.1 a new feature is introduced in the SQL Server database engine which allows it to directly access data pages in database files stored in persistent memory (PMEM) devices. 

In a traditional system without persistent memory, SQL Server caches data pages in the buffer pool. With Hybrid Buffer Pool, SQL Server skips performing a copy of the page into the DRAM-based portion of the buffer pool, and instead references the page directly on the database file that lives on a PMEM device. Access to data files in PMEM for Hybrid Buffer Pool is performed using memory-mapped I/O, also known as enlightenment.

Only clean pages can be referenced directly on a PMEM device. When a page becomes dirty it is kept in DRAM, and then eventually written back to the PMEM device.

This feature is available on both Windows and Linux.

## Enable Hybrid Buffer Pool

On CTP 2.1, you must enable the startup trace flag 809 in order to use Hybrid Buffer Pool.

## Best Practices for Hybrid Buffer Pool

* When formatting your PMEM device on Windows use the largest allocation unit size available for NTFS (2MB in Windows Server 2019) and ensure the device has been enabled for DAX (DirectAccess)
  