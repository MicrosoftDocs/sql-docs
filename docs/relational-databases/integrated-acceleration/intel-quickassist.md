---
title: Configure Intel&reg; QuickAssist Technology (QAT) for SQL Server
description: Describes how to configure Intel&reg; QuickAssist Technology (QAT) for an instance of SQL Server.
ms.date: 08/16/2022
ms.prod: sql
ms.reviewer: david.pless, wiassaf 
ms.technology: configuration
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
---

# Intel&reg; QuickAssist Technology for SQL Server

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

This article explains Intel&reg; QuickAssist Technology (QAT) for SQL Server and how to configure it on an instance of SQL Server. Intel&reg; QAT is an integrated offloading and acceleration solution. For background about these types of solutions, see [Integrated offloading and acceleration](overview.md).

## Edition specific capabilities

Intel&reg; QAT accelerator is supported starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] on Windows.

- Enterprise edition supports hardware offloading - can use a physical device. If no device is available, it falls back to software based compression.

- Standard edition supports software compression. Standard edition does not offload to hardware even if a device is available.

- Express edition allow Intel&reg; QAT compressed backups to be restored if the drivers are available. All editions lower than SQL Server Standard edition will only allow backups to be performed with the default MS_XPRESS algorithm. 

> [!NOTE]
> Hardware is not required to successfully restore a previously compressed Intel&reg; QAT backup, regardless of the SQL Server edition. However, to back up or restore databases with Intel&reg; QAT accelerated compression, you must install product drivers.

## Install drivers

1. Download the drivers. 

   The minimum QATzip accelerator library version supported is [1.8.0-0010](https://www.intel.com/content/www/us/en/download/19732/), but you should always install the latest version from the vendor. Drivers are available at [Intel&reg; Quick Assist Technology landing page](https://developer.intel.com/quickassist).

1. Follow the instructions from the vendor to install the drivers on your server.

1. Reboot the server after you install the drivers.

## Verify installed components

If the drivers are installed, the following files are available:

- The QATzip library is available at `C:\Windows\system32\`.
- The ISA-L library installed with QATzip is available at `C:\Program Files\Intel\ISAL\*`.

The paths above apply to both hardware and software-only deployment.

## Configure server hardware offloading

After the drivers are installed, configure the server. For detailed instructions and examples, see [Enable integrated offloading and acceleration](overview.md#enable-integrated-offloading-and-acceleration).

## Service start - after configuration

After the feature is enabled, every time the SQL Server service starts, the SQL Server process looks for the required user space software library that interfaces with the hardware acceleration device driver API and loads the software assemblies if they are available. For the Intel&reg; QAT accelerator, the user space library is QATzip. This library provides a number of features. The QATzip software library is a user space software API that can interface with the QAT kernel driver API. It is used primarily by applications that are looking to accelerate the compression and decompression of files using one or more Intel&reg; QAT devices.

In the case of the Windows operating system, there is a complimentary software library to QATzip, the Intel Intelligent Storage Library (ISA-L). This serves as a software fallback mechanism for QATzip in the case of hardware failure, and a software-based option when the hardware is not available.

> [!NOTE]
> The unavailability of an Intel&reg; QAT hardware device doesn't prevent instances from performing backup or restore operations using the QAT_DEFLATE algorithm. If the physical device is not available, the software algorithm will be leveraged as a fallback solution.

## Backup operation

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces an `ALGORITHM` extension for backup compression for [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md#compression).

The T-SQL BACKUP command WITH COMPRESSION has been extended to allow for a specified backup compression algorithm. When using Intel&reg; QAT for backup compression acceleration, the algorithm QAT_DEFLATE initiates an Intel&reg; QAT compressed backup if the drivers are available and the SQL Server configuration has been completed successfully as illustrated in the previously documented steps.  

> [!NOTE]
> The standard compression algorithm is MS_XPRESS and is default compression option.

Use the ALGORITHM command to specify either of these two algorithms (`MS_XPRESS`, `QAT_DEFLATE`) for backup compression.

The example below performs backup compression using Intel&reg; QAT hardware acceleration.

```sql
BACKUP DATABASE <database> TO DISK = '<path>\<file>.bak'  
WITH COMPRESSION (ALGORITHM = QAT_DEFLATE); 
```

Either of the following statements use the default MS_XPRESS compression option: 

```sql
BACKUP DATABASE <database> TO DISK = '<path>\<file>.bak'  
WITH COMPRESSION (ALGORITHM = MS_XPRESS); 
```

```sql
BACKUP DATABASE <database> TO DISK = '<path>\<file>.bak'  
WITH COMPRESSION; 
```

The table below gives a summary of the BACKUP DATABASE with COMPRESSION options beginning with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

|Backup command | Description |
|:-------|:-------|
|`BACKUP DATABASE <database_name> TO DISK` | Backup with no compression or with compression depending on default setting.|
|`BACKUP DATABASE <database_name> TO DISK WITH COMPRESSION`|Backup using the default setting in `sp_configure`.
|`BACKUP DATABASE <database_name> TO DISK WITH COMPRESSION (ALGORITHM = MS_XPRESS)` | Backup with compression using the MS_XPRESS algorithm.|
|`BACKUP DATABASE <database_name> TO  DISK WITH COMPRESSION (ALGORITHM = QAT_DEFLATE)`| Backup with compression using the QATzip library.|

> [!NOTE]
> The examples in the table above specify DISK as destination. The actual destination may be DISK, TAPE, or URL.

### Default configurations
 
The SQL Server backup compression default behavior can be adjusted. You can change the server default configuration as well as other options. You can enable or disable hardware acceleration, you can enable backup compression as a default, and you can also change the default compression algorithm as by using `sp_configure`.  

The status of these options is reflected in the [sys.configurations (Transact-SQL)](../system-catalog-views/sys-configurations-transact-sql.md). View the configuration of offload and acceleration configuration with the [sys.dm_server_accelerator_status (Transact-SQL)](../system-dynamic-management-views/sys-dm-server-accelerator-status-transact-sql.md) dynamic management view. 

The `backup compression algorithm` configuration changes the backup compression algorithm default for backup compression. Changing this option will change the default algorithm when the algorithm is not specified on the `BACKUP .. WITH COMPRESSION` command. 

You can view the current default settings for the backup compression in [sys.configurations (Transact-SQL)](../system-catalog-views/sys-configurations-transact-sql.md), for example:

```sql
SELECT * FROM sys.configurations    
WHERE name = 'backup compression algorithm'; 
```

```sql
SELECT * FROM sys.configurations    
WHERE name = 'backup compression default'; 
```

Changing this configuration is permitted through the [sp_configure (Transact-SQL)](../system-stored-procedures/sp-configure-transact-sql.md) system stored procedure. For example: 

```sql
EXEC sp_configure 'backup compression default', 1;   
RECONFIGURE; 
``` 

No restart of SQL Server is required for this change to take effect. 

The `backup compression algorithm` configuration sets the default compression algorithm. To set Intel&reg; QAT as the default compression algorithm for SQL Server, use the following script:

```sql
EXEC sp_configure 'backup compression algorithm', 2;   
RECONFIGURE; 
```

To change the default compression algorithm back to the default, use the following script:

```sql
EXEC sp_configure 'backup compression algorithm', 0;   
RECONFIGURE; 
```

No restart of SQL Server is required for this change to take effect. 

## Restore operations

The RESTORE command does not include a COMPRESSION option. The backup header metadata specifies if the database is compressed and therefore the storage engine can restore from the backup file(s) accordingly. Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the backup metadata includes the compression algorithm.

Run RESTORE HEADERONLY on a backup without compression or a backup compressed with the default MS_XPRESS algorithm. The query returns `CompressionAlgorithm`. See [RESTORE Statements - HEADERONLY (Transact-SQL)](../../t-sql/statements/restore-statements-headeronly-transact-sql.md).

Restore statements are the same, no matter what compression algorithm is used to back up a database.

```sql
RESTORE DATABASE <database> FROM DISK = '<path>\<file>.bak'  
WITH RECOVERY; 
```

SQL Server backups compressed using QAT_DEFLATE support all T-SQL RESTORE operations. The RESTORE { DATABASE | LOG } statements for restoring and recovering databases from backups, support all recovery arguments, such as WITH MOVE, PARTIAL, STOPAT, KEEP REPLICATION, KEEP CDC and RESTRICTED_USER.  

Auxiliary RESTORE commands are also supported for all backup compression algorithms. Auxiliary RESTORE commands include RESTORE FILELISTONLY, RESTORE HEADERONLY, RESTORE VERIFYONLY, and more.

> [!NOTE]
> If the server-scope configuration `HARDWARE_OFFLOAD` option is not enabled, and/or the Intel&reg; QAT drivers have not been installed, SQL Server returns error 17441 instead of attempting to perform the restore. For example: `Msg 17441, Level 16, State 1, Line 175 This operation requires Intel(R) QuickAssist Technology (QAT) libraries to be loaded.`

To restore an Intel&reg; QAT compressed backup, the correct assemblies must be loaded on the SQL Server instance initiating the restore operation.

### Backup history

You can view the compression algorithm and history of all SQL Server backup and restore operations on an instance in [backupset (Transact-SQL)](../system-tables/backupset-transact-sql.md) system table. A new column was added to this system table for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], `compression_algorithm` which indicates `MS_EXPRESS` or `QAT_DEFLATE`, for example.

## Next steps

 - [Integrated offloading and acceleration](overview.md)
 - [Hardware offload enabled configuration option](../../database-engine/configure-windows/hardware-offload-enable-configuration-option.md)
 - [ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)
 - [BACKUP COMPRESSION (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md#compression)
 - [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)