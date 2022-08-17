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

## Edition specific capabilities

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

If the drivers are installed, the following files are available:

- The `QATZip` library is available at `C:\Windows\system32\`.
- The ISA-L libraries are available at `C:\Program Files\Intel\ISAL\*`.

The paths above apply to both hardware and software-only deployment.

## Configure server hardware offloading

After the drivers are installed, configure the server.

1. Set the server configuration option `hardware offload enabled` to `1`.
1. Configure the server to use hardware offload.
1. Restart the server.

For detailed instructions and examples, see [Enable integrated offloading and acceleration](overview.md#enable-integrated-offloading-and-acceleration).

## Use offloading and acceleration for backup operation

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces an ALGORITHM extension for backup compression for [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md#compression).

The T-SQL BACKUP command WITH COMPRESSION has been extended to allow for a specified backup compression algorithm. When using Intel QAT for backup compression acceleration, the algorithm QAT_DEFLATE intiates an Intel QAT compressed backup if the drivers are available and the SQL Server configuration has been completed successfully as illustrated in the previously documented steps.  

> [!NOTE]
> The standard compression algorithm is MS_EXPRESS and is default compression option.

Use the ALGORITHM command to specify either of these two algorithms (MS_EXPRESS, QAT_DEFLATE) for backup compression.

The example below performs backup compression using Intel QAT hardware acceleration.

```sql
BACKUP DATABASE <database> TO DISK = '<path>\<file>.bak'  
WITH COMPRESSION (ALGORITHM = QAT_DEFLATE); 
```

Either of the following statements use the default MS_XPRESSpress compression engine: 

```sql
BACKUP DATABASE <database> TO DISK = '<path>\<file>.bak'  
WITH COMPRESSION (ALGORITHM = MS_XPRESS); 
```

```sql
BACKUP DATABASE <database> TO DISK = '<path>\<file>.bak'  
WITH COMPRESSION; 
```

The table below gives a summary of the BACKUP DATABASE with COMPRESSION options in SQL Server 2022. 

|Backup command | Description |
|:-------|:-------|
|`BACKUP DATABASE <database_name> TO DISK` | Backup with no compression or with compression depending on default setting.|
|`BACKUP DATABASE <database_name> TO DISK WITH COMPRESSION`|Backup using MS_XPRESS compression algorithm.
|`BACKUP DATABASE <database_name> TO DISK WITH COMPRESSION (ALGORITHM = MS_XPRESS)` | Backup with compression using the MS_XPRESS algorithm. There is an argument for permitting use of DEFAULT or NATIVE as permitted options.|
|`BACKUP DATABASE <database_name> TO  DISK WITH COMPRESSION (ALGORITHM = QAT_DEFLATE)`|Backup with compression using the QATZip library using QZ_DEFLATE_GZIP_EXT with the compression level 1.|

> [!NOTE]
> The examples in the table above specify DISK as destination. The actual destination may be DISK, TAPE, or URL.

## Service start - after configuration

After the feature is enabled, every time the SQL Server service starts, the SQL Server process looks for the required user space software library that interfaces with the hardware acceleration device driver API and loads the software assemblies if they are available. For the The Intel QuickAssist Technology (QAT) accelerator, the user space library is QATZip. This library provides a number of features. The QATZip software library is a user space software API that can interface with the QAT kernel driver API. It is used primarily by applications that are looking to accelerate the compression and decompression of files using one or more Intel QAT devices.

In the case of the Windows operating system, there is a complimentary software library to QATZip, the Intel Intelligent Storage Library (ISA-L). This serves as a software fallback mechanism for QATZip in the case of hardware failure, and a software-based option when the hardware is not available.

> [!NOTE]
> The unavailability of an Intel QAT hardware device doesn't prevent instances from performing backup or restore operations using the QAT_DEFLATE algorithm. If the physical device is not available, the software algorithm will be leveraged as a fallback solution.

## See also

[Integrated offloading and acceleration](overview.md)

[Hardware offload enable Configuration Option](../../database-engine/configure-windows/hardware-offload-enable-configuration-option.md)

[ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)