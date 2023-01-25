---
title: Integrated acceleration & offloading
description: Learn to leverage integrated solutions from third party providers to offload and accelerate workloads for an instance of SQL Server.
ms.date: 08/18/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: david.pless, wiassaf 
---

# Integrated acceleration and offloading

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] provides a framework for offloading specific SQL Server workload compute to hardware devices.

This article explains offloading and acceleration to hardware devices.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] enables integrated offloading and acceleration with [Intel&reg; QuickAssist Technology (QAT)](https://www.intel.com/content/www/us/en/developer/topic-technology/open/quick-assist-technology/overview.html). You can offload backup compression with Intel&reg; QAT to reduce CPU usage, backup completion times, and reduce storage consumption. To back up or restore databases with accelerated compression, install Intel&reg; QAT drivers. For specific information and instructions to use with SQL Server, see [Configure Intel&reg; QuickAssist Technology (QAT)](use-integrated-acceleration-and-offloading.md).

## Back up and restore databases

One use case for integrated acceleration and offloading is [backup compression](../backup-restore/backup-compression-sql-server.md). Compression speeds up back up and restore operations, and saves space. Compression also consumes CPU resources which may impact application performance. Because Intel&reg; QAT is an integrated acceleration and offloading solution, you can use it to back up and restore databases with compression, and reduce the load compression places on the CPU.

Backup compression is enabled by including the COMPRESSION keyword for BACKUP T-SQL commands, and is now available with an ALGORITHM parameter to choose between MS_XPRESS (default) and QAT_DEFLATE. For information about how to compress backups, see [Configure Backup Compression (SQL Server)](../backup-restore/configure-backup-compression-sql-server.md).

## Edition specific capabilities

Integrated acceleration and offloading are supported starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] on Windows.

Preview software is Evaluation Edition. It supports:

- Hardware offloading - can use a physical device. If no device is available, it falls back to software based compression.
- Software compression.
- Compressed backups to be restored if the drivers are available.

> [!NOTE]
> Hardware is not required to successfully restore a previously compressed backup. However, to back up or restore databases with accelerated compression, you must install product drivers.

## Limitations

Integrated acceleration and offloading are not available on Linux or containers.

## Next steps

 - [Hardware offload enabled configuration option](../../database-engine/configure-windows/hardware-offload-enable-configuration-option.md)
 - [sys.dm_server_accelerator_status (Transact-SQL)](../system-dynamic-management-views/sys-dm-server-accelerator-status-transact-sql.md)
 - [Use integrated acceleration and offloading solutions](use-integrated-acceleration-and-offloading.md)
 - [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
 - [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)