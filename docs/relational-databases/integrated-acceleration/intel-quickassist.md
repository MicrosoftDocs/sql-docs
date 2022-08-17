---
title: Configure Intel QuickAssist Technology (QAT) for SQL Server
description: Describes how to configure Intel QuickAssist Technology (QAT) for an instance of SQL Server.
ms.date: 08/16/2022
ms.prod: sql
ms.reviewer: dplessMSFT 
ms.technology: configuration
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
---

# Intel QuickAssist Technology (QAT) for SQL Server

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

This article explains Intel QuickAssist Technology (QAT) for SQL Server and how to configure it on an instance of SQL Server. Intel QAT is an integrated offloading and acceleration solution. For background about these types of solutions, see [Integrated offloading and acceleration](overview.md).

## SQL Server support for Intel QuickAssist Technology (QAT)

SQL Server on Windows operating system supports Intel QAT accelerator.

- Enterprise Edition supports hardware offloading - requires an Intel QAT device. If no device is available, it falls back to software based compression.

- Standard Edition supports software compression. Standard edition does not offload to hardware even if an Intel QAT device is available.

- Express Edition allows you to restore backups compressed with Intel QAT, but uses the SQL Server compression algorithm (MS_XPRESS) to compress backups.

> [!NOTE]
> To back up or restore databases with Intel QAT accelerated compression, you must install Intel QAT drivers.

## Install drivers

1. Download the drivers.

   Drivers are available at [Intel Quick Assist Technology landing page](https://developer.intel.com/quickassist).

   At this time, the current driver is [1.8.0-0010](https://www.intel.com/content/www/us/en/download/19732/intel-quickassist-technology-driver-for-windows-hw-version-1-7.html).

1. Follow the instructions from Intel to install the drivers on your server.

1. Reboot the server after you install the drivers.

## Verify installed components

If the drivers are installed, and the server-scope configuration option is enabled for SQL Server:

- The `QATZip` library is available at `C:\Windows\system32\`.
- The ISA-L libraries are available at `C:\Program Files\Intel\ISAL\*`.

The paths above apply to both hardware and software-only deployment.

## See also

[Integrated offloading and acceleration](overview.md)

[Hardware offload enable Configuration Option](../../database-engine/configure-windows/hardware-offload-enable-configuration-option.md)

[ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)