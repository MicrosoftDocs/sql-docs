---
title: Integrated offloading & acceleration
description: Learn to leverage integrated solutions from third party providers to offload and accelerate workloads for an instance of SQL Server.
ms.date: 08/18/2022
ms.prod: sql
ms.technology: configuration
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: david.pless, wiassaf 
---

# Integrated offloading and acceleration

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] provides a framework for offloading specific SQL Server workload compute to hardware devices.

This article explains offloading and acceleration to hardware devices.

- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] integrates [Intel&reg; QuickAssist Technology (QAT)](https://www.intel.com/content/www/us/en/developer/topic-technology/open/quick-assist-technology/overview.html). You can offload backup compression with Intel&reg; QAT to reduce CPU usage, backup completion times, and reduce storage consumption. To back up or restore databases with accelerated compression, install Intel&reg; QAT drivers. For specific information and instructions to use with SQL Server, see [Configure Intel&reg; QuickAssist Technology (QAT)](intel-quickassist.md).

## Use case - Backup and restore operations

One use case for integrated offloading and acceleration is [backup compression](../backup-restore/backup-compression-sql-server.md). Compression speeds up backup and restore operations, and saves space. Compression also consumes CPU resources which may impact application performance. Because Intel&reg; QAT is an integrated offloading and acceleration solution, you can use it to back up and restore databases with compression, and reduce the load compression places on the CPU.

Backup compression is enabled by including the COMPRESSION keyword for BACKUP T-SQL commands, and is now available with an ALGORITHM parameter to choose between MS_XPRESS (default) and QAT_DEFLATE. For information about how to compress backups, see [Configure Backup Compression (SQL Server)](../backup-restore/configure-backup-compression-sql-server.md).

## Enable integrated offloading and acceleration

Before you can use integrated offloading and acceleration, you need to configure the instance of SQL Server. Before you complete this section, install the drivers for your accelerator.

1. Set the server configuration option `hardware offload enabled` to `1` to enable all SQL Server accelerators. By default, this setting is `0`. This setting is an advanced configuration option. To set this setting, run the following commands:

   ```sql
   sp_configure 'show advanced options', 1;
   GO
   RECONFIGURE
   GO
   
   sp_configure 'hardware offload enabled', 1;
   GO
   RECONFIGURE
   GO
   ```

Changing this configuration option requires a restart of SQL Server service to take effect.

   > [!NOTE]
   > If `hardware offload enabled` option equals `0`, all offloading and acceleration is disabled, however the accelerator-specific configurations will persist.

1. Configure the server to use hardware offload for a specific accelerator. Run [ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md) to enable hardware acceleration. The following example sets this configuration for Intel&reg; QAT.

   ```sql
   ALTER SERVER CONFIGURATION   
   SET HARDWARE_OFFLOAD = ON (ACCELERATOR = QAT);  
   ```

   If your instance is Enterprise Edition, and you have the Intel&reg; QAT device installed, it will use hardware by default, with software fallback. If you would prefer to only use software offloading, run the following configuration.

   ```sql
   ALTER SERVER CONFIGURATION
   SET HARDWARE_OFFLOAD = ON (ACCELERATOR = QAT, MODE = SOFTWARE)
   ```

   Standard edition only supports software offloading. For more information on edition specifics, see [Edition specific capabilities](intel-quickassist.md#edition-specific-capabilities).

   > [!TIP]
   > You might specify software mode for your accelerator if you have higher computing resources and prefer higher backup performance over protecting the host systems compute resources.

1. Restart the SQL Server instance. You need to restart the SQL Server instance after you run a command to `SET HARDWARE_OFFLOAD = ...`.

## Disable offloading and acceleration

The following example disables hardware offloading and acceleration for an Intel&reg; QAT accelerator.

```sql
ALTER SERVER CONFIGURATION   
SET HARDWARE_OFFLOAD = OFF (ACCELERATOR = QAT);  
```

## Limitations

Integrated offloading and acceleration requires Windows operating system. It is not available on Linux or containers.

## Next steps

 - [Hardware offload enabled configuration option](../../database-engine/configure-windows/hardware-offload-enable-configuration-option.md)
 - [sys.dm_server_accelerator_status (Transact-SQL)](../system-dynamic-management-views/sys-dm-server-accelerator-status-transact-sql.md)
 - [Intel&reg; QuickAssist Technology (QAT) for SQL Server](intel-quickassist.md)
 - [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
 - [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)