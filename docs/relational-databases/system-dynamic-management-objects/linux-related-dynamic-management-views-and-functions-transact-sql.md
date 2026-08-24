---
title: "Linux Related Dynamic Management Views and Functions (Transact-SQL)"
description: Linux related dynamic management views and functions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/13/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "Linux dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server], Linux"
dev_langs:
  - "TSQL"
---
# Linux related dynamic management views and functions (Transact-SQL)

[!INCLUDE [sqlserver2025-linux](../../includes/applies-to-version/sqlserver2025-linux.md)]

The following dynamic management objects return system-level Linux metrics beyond SQL Server, so you get comprehensive visibility into overall host performance.

These objects provide detailed CPU statistics, real-time network interface activity (bytes and packets sent and received, errors, drops, and collisions), disk I/O statistics for each storage device, and virtual memory statistics.

## In this section

- [sys.dm_os_linux_cpu_stats](sys-dm-os-linux-cpu-stats-transact-sql.md)
- [sys.dm_os_linux_disk_stats](sys-dm-os-linux-disk-stats-transact-sql.md)
- [sys.dm_os_linux_net_stats](sys-dm-os-linux-net-stats-transact-sql.md)
- [sys.dm_os_linux_vm_stats](sys-dm-os-linux-vm-stats-transact-sql.md)

## Related content

- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](../../linux/configure/performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](../../linux/configure/performance-best-practices-sql-server-memory.md)
- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
