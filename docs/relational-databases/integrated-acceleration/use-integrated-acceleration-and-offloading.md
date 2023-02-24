---
title: Configure integrated acceleration and offloading
description: Describes how to use integrated acceleration and offloading solution for an instance of SQL Server.
ms.date: 08/16/2022
ms.service: sql
ms.reviewer: david.pless, wiassaf 
ms.subservice: configuration
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
---

# Configure integrated acceleration and offloading

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

This article demonstrates how to configure an integrated acceleration and offloading with Intel&reg; QuickAssist Technology (QAT) for SQL Server. Intel&reg; QAT is an integrated acceleration and offloading solution. For more background, see [Integrated acceleration and offloading](overview.md).

## Install drivers

1. Download the drivers.

   The minimum QATzip accelerator library version supported is [1.8.0-0010](https://www.intel.com/content/www/us/en/download/19732/), but you should always install the latest version from the vendor. Drivers are available at [Intel&reg; Quick Assist Technology landing page](https://developer.intel.com/quickassist).

2. Follow the instructions from the vendor to install the drivers on your server.

3. Reboot the server after you install the drivers.

## Verify installed components

If the drivers are installed, the following files are available:

- The QATzip library is available at `C:\Windows\system32\`.
- The ISA-L library installed with QATzip is available at `C:\Program Files\Intel\ISAL\*`.

The paths above apply to both hardware and software-only deployment.

## Enable hardware offloading

After the drivers are installed, configure the server instance.

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

2. Stop and restart the SQL Server service.

   > [!NOTE]
   > If `hardware offload enabled` option equals `0`, all offloading and acceleration is disabled, however the accelerator-specific configurations will persist.

3. Configure the server to use hardware offload for a specific accelerator. Run [ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md) to enable hardware acceleration. The following examples set this configuration for Intel&reg; QAT.

   Choose one of the following examples, a. enable hardware-offloading with software fallback or b. software support.

   a. **Enable accelerator hardware offloading**

   Hardware compression configuration protects the host CPU - Intel&reg; QAT hardware mode is designed to protect the underlying host system CPU. This method performs best when the underlying system is under higher workloads.

   ```sql
   ALTER SERVER CONFIGURATION   
   SET HARDWARE_OFFLOAD = ON (ACCELERATOR = QAT);  
   ```

   > [!Tip]
   > If the hardware device fails for any reason, the accelerator can gracefully fall back to software mode.

   b. **Force enable accelerator software mode**

   ```sql
   ALTER SERVER CONFIGURATION
   SET HARDWARE_OFFLOAD = ON (ACCELERATOR = QAT, MODE = SOFTWARE)
   ```

   > [!IMPORTANT]
   > The performance of the QAT_DEFLATE algorithm in terms of SOFTWARE vs. HARDWARE mode compared to MS_XPRESS varies based on several factors. The workload pressure the host system may be under during backup execution and the available memory and processing power of the Intel® QuickAssist Technology (QAT) hardware device are all factors that could impact the performance of the leveraged compression algorithm.

4. Restart the SQL Server instance. You need to restart the SQL Server instance after you run a command to `SET HARDWARE_OFFLOAD = ...`.

5. To verify the configuration, run:

   ```sql
   SELECT * FROM sys.dm_server_accelerator_status;
   GO
   ```

   The query results identify:

   - `mode_desc` - NONE, SOFTWARE, or HARDWARE mode
   - `mode_reason_desc` - Reason for the mode
   - `accelerator_library_version` - User mode accelerator version
   - `accelerator_driver_version` - Kernel mode accelerator version

The accelerator is enabled if the mode description is either SOFTWARE or HARDWARE. The `mode_reason_desc` explains why the result is either SOFTWARE or HARDWARE mode.

If other results are found, refer to the [sys.dm_server_accelerator_status (Transact-SQL)](../system-dynamic-management-views/sys-dm-server-accelerator-status-transact-sql.md) for troubleshooting.

## Disable offloading and acceleration

The following example disables hardware offloading and acceleration for an Intel&reg; QAT accelerator.

```sql
ALTER SERVER CONFIGURATION   
SET HARDWARE_OFFLOAD = OFF (ACCELERATOR = QAT);  
```

## Backup operation

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces an `ALGORITHM` extension for backup compression for [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md#compression).

The T-SQL BACKUP command WITH COMPRESSION has been extended to allow for a specified backup compression algorithm. For backup compression acceleration, Intel&reg; QAT uses an algorithm called QAT_DEFLATE. If the drivers are available and the SQL Server configuration has been completed successfully as illustrated in the previously documented steps, WITH COMPRESSION initiates an Intel&reg; QAT compressed backup.  

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

The SQL Server backup compression default behavior can be adjusted. You can change the server default configuration and other options. You can enable or disable hardware acceleration, you can enable backup compression as a default, and you can also change the default compression algorithm as by using `sp_configure`.  

The status of these options is reflected in the [sys.configurations (Transact-SQL)](../system-catalog-views/sys-configurations-transact-sql.md). View the configuration of offload and acceleration configuration with the [sys.dm_server_accelerator_status (Transact-SQL)](../system-dynamic-management-views/sys-dm-server-accelerator-status-transact-sql.md) dynamic management view. 

The `backup compression algorithm` configuration changes the backup compression algorithm default for backup compression. Changing this option will change the default algorithm when the algorithm isn't specified on the `BACKUP ... WITH COMPRESSION` command. 

You can view the current default settings for the backup compression in [sys.configurations (Transact-SQL)](../system-catalog-views/sys-configurations-transact-sql.md), for example:

```sql
SELECT * FROM sys.configurations    
WHERE name = 'backup compression algorithm'; 
```

```sql
SELECT * FROM sys.configurations    
WHERE name = 'backup compression default'; 
```

To change these configuration settings, use [sp_configure (Transact-SQL)](../system-stored-procedures/sp-configure-transact-sql.md) system stored procedure. For example:

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
EXEC sp_configure 'backup compression algorithm', 1;   
RECONFIGURE; 
```

No restart of SQL Server is required for this change to take effect.

## Restore operations

The backup file metadata identifies if the database backup is compressed and what algorithm was used to compress the backup.

Use RESTORE HEADERONLY to view the compression algorithm. See [RESTORE Statements - HEADERONLY (Transact-SQL)](../../t-sql/statements/restore-statements-headeronly-transact-sql.md).

> [!NOTE]
> If the server-scope configuration `HARDWARE_OFFLOAD` option is not enabled, and/or the Intel&reg; QAT drivers have not been installed, SQL Server returns error 17441, (`Msg 17441, Level 16, State 1, Line 175 This operation requires Intel(R) QuickAssist Technology (QAT) libraries to be loaded.`)

To restore an Intel&reg; QAT compressed backup, the correct assemblies must be loaded on the SQL Server instance initiating the restore operation. It's not required to have QAT hardware to restore QAT compressed backups. However, to restore QAT backups requires the following:

- QAT driver needs to be installed on the machine
- Hardware offloading needs to be enabled (`sp_configure 'hardware offload enabled', 1;`)
- The SQL Server instance configuration has to have `ALTER SERVER CONFIGURATION SET HARDWARE_OFFLOAD ON (ACCELERATOR = QAT)` set as described previously.

QAT backups performed in HARDWARE mode can be restored in SOFTWARE mode and vice-versa.

### Backup history

You can view the compression algorithm and history of all SQL Server backup and restore operations on an instance in [backupset (Transact-SQL)](../system-tables/backupset-transact-sql.md) system table. A new column was added to this system table for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], `compression_algorithm`, which indicates `MS_EXPRESS` or `QAT_DEFLATE`, for example.

## Service start - after configuration

After you configure integrated acceleration and offloading, every time the SQL Server service starts, the SQL Server process looks for the required user space software library that interfaces with the hardware acceleration device driver API and loads the software assemblies if they're available. For the Intel&reg; QAT accelerator, the user space library is QATzip. This library provides many features. The QATzip software library is a user space software API that can interface with the QAT kernel driver API. It's used primarily by applications that are looking to accelerate the compression and decompression of files using one or more Intel&reg; QAT devices.

In the case of the Windows operating system, there's a complimentary software library to QATzip, the Intel Intelligent Storage Library (ISA-L). This serves as a software fallback mechanism for QATzip in the case of hardware failure, and a software-based option when the hardware isn't available.

> [!NOTE]
> The unavailability of an Intel&reg; QAT hardware device doesn't prevent instances from performing backup or restore operations using the QAT_DEFLATE algorithm. If the physical device is not available, the software algorithm will be leveraged as a fallback solution.

## Next steps

- [Integrated acceleration and offloading](overview.md)
- [Hardware offload enabled configuration option](../../database-engine/configure-windows/hardware-offload-enable-configuration-option.md)
- [ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)
- [BACKUP COMPRESSION (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md#compression)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [View or configure the backup compression algorithm Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-algorithm-server-configuration-option.md)
- [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md)