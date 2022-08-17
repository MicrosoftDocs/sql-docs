---
title: Integrated offloading & acceleration
description: Learn to leverage integrated solutions from third party providers to offload and accelerate workloads for an instance of SQL Server.
ms.date: 08/16/2022
ms.prod: sql
ms.reviewer: dplessMSFT 
ms.technology: configuration
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
---

# Integrated offloading and acceleration

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can use solutions from third-party providers to offload workload from the CPU and accelerate various processes. For example, you can offload backup compression with Intel QuickAssist Technology (QAT) to reduce CPU usage, backup completion times, and reduce storage consumption.

This article explains integrated offloading and acceleration.

## Validated solution

Microsoft allows partners to develop and provide hardware and software validated solutions for integrated offloading and acceleration.

At this time, [Intel QuickAssist Technology (QAT)](https://www.intel.com/content/www/developer/topic-technology/open/quick-assist-technology/overview.html) is the only validated solution for SQL Server.

## Use case - Backup and restore operations

One use case for integrated offloading and acceleration is [backup compression](../backup-restore/backup-compression-sql-server.md). Compression speeds up backup and restore operations, and saves space. Compression also consumes CPU resources which may impact application performance. Because Intel QAT is a validated offloading and acceleration solution, you can use it to back up and restore databases with compression, and reduce the load compression places on the CPU.

By default, SQL Server does not compress backups. For information about how to compress backups, see [Configure Backup Compression (SQL Server)](../backup-restore/configure-backup-compression-sql-server.md).

## SQL Server support Intel QuickAssist Technology (QAT) by edition

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Enterprise Edition supports hardware offloading - requires an Intel QAT device. If no device is available, it falls back to software based compression.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Standard Edition supports software compression. Standard edition does not offload to hardware even if an Intel QAT device is available.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Express Edition allows you to restore backups compressed with Intel QAT, but uses the SQL Server compression algorithm (MS_XPRESS) to compress backups.

> [!NOTE]
> To back up or restore databases with Intel QAT accelerated compression, you must install Intel QAT drivers.

## Enable integrated offloading and acceleration

Before you can use integrated offloading and acceleration, you need to configure the instance of SQL Server.

1. Set the server configuration option `hardware offload enabled` to `1`. By default, this setting is `0`. This setting is an advanced configuration option. To set this setting, run the following commands:

   ```sql
   sp_configure 'show advanced options', 1
   GO
   RECONFIGURE
   GO
   sp_configure 'hardware offload enabled', 1
   GO
   RECONFIGURE
   GO
   ```

   > [!NOTE]
   > If `hardware offload enabled` is disabled (`0`), all offloading and acceleration solutions are disabled.

1. Set the server configuration. Run [ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md) to enable hardware acceleration.

   ```sql
   ALTER SERVER CONFIGURATION   
   SET HARDWARE_OFFLOAD ON;  
   ```

1. Restart the SQL Server instance. 