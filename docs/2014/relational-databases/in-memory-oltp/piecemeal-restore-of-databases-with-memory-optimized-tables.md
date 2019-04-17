---
title: "Piecemeal Restore of Databases With Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 732c9721-8dd4-481d-8ff9-1feaaa63f84f
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Piecemeal Restore of Databases With Memory-Optimized Tables
  Piecemeal restore is supported on databases with memory-optimized tables except for one restriction described below. For more information about piecemeal backup and restore, see [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql) and [Piecemeal Restores &#40;SQL Server&#41;](../backup-restore/piecemeal-restores-sql-server.md).  
  
 A memory-optimized filegroup must be backed up and restored together with the primary filegroup:  
  
-   If you back up (or restore) the primary filegroup you must specify the memory-optimized filegroup.  
  
-   If you backup (or restore) the memory-optimized filegroup you must specify the primary filegroup.  
  
 Key scenarios for piecemeal backup and restore are,  
  
-   Piecemeal backup allows you to reduce the size of backup. Some examples:  
  
    -   Configure the database backup to occur at different times or days to minimize the impact on the workload. One example is a very large database (greater than 1 TB) where a full database backup cannot complete in the time allocated for database maintenance. In that situation, you can use piecemeal backup to backup the full database in multiple piecemeal backups.  
  
    -   If a filegroup is marked read-only, it does not require a transaction log backup after it was marked read-only. You can choose to back up the filegroup only once after marking it read-only.  
  
-   Piecemeal restore.  
  
    -   The goal of a piecemeal restore is to bring the critical parts of database online without waiting for all the data. One example is if a database has partitioned data, such that older partitions are only used rarely. You can restore them only on an as-needed basis. Similar for filegroups that contain, for example, historical data.  
  
    -   Using page repair, you can fix page corruption by specifically restoring the page. For more information, see [Restore Pages &#40;SQL Server&#41;](../backup-restore/restore-pages-sql-server.md).  
  
## Samples  
 The examples use the following schema:  
  
```  
CREATE DATABASE imoltp  
ON PRIMARY (name = imoltp_primary1, filename = 'c:\data\imoltp_data1.mdf')  
LOG ON (name = imoltp_log, filename = 'c:\data\imoltp_log.ldf')  
GO  
  
ALTER DATABASE imoltp ADD FILE (name = imoltp_primary2, filename = 'c:\data\imoltp_data2.ndf')  
GO  
  
ALTER DATABASE imoltp ADD FILEGROUP imoltp_secondary  
ALTER DATABASE imoltp ADD FILE (name = imoltp_secondary, filename = 'c:\data\imoltp_secondary.ndf') TO FILEGROUP imoltp_secondary  
GO  
  
ALTER DATABASE imoltp ADD FILEGROUP imoltp_mod CONTAINS MEMORY_OPTIMIZED_DATA   
ALTER DATABASE imoltp ADD FILE (name='imoltp_mod1', filename='c:\data\imoltp_mod1') TO FILEGROUP imoltp_mod   
ALTER DATABASE imoltp ADD FILE (name='imoltp_mod2', filename='c:\data\imoltp_mod2') TO FILEGROUP imoltp_mod   
GO  
```  
  
### Backup  
 This sample shows how to backup the primary filegroup and the memory-optimized filegroup. You must specify both primary and memory-optimized filegroup together.  
  
```  
backup database imoltp filegroup='primary', filegroup='imoltp_mod' to disk='c:\data\imoltp.dmp' with init  
```  
  
 The following sample shows that a back up of filegroup(s) other than primary and memory-optimized filegroup works similar to the databases without memory-optimized tables. The following command backups up the secondary filegroup  
  
```  
backup database imoltp filegroup='imoltp_secondary' to disk='c:\data\imoltp_secondary.dmp' with init  
```  
  
### Restore  
 The following sample shows how to restore the primary filegroup and memory-optimized filegroup together.  
  
```  
restore database imoltp filegroup = 'primary', filegroup = 'imoltp_mod'   
from disk='c:\data\imoltp.dmp' with partial, norecovery  
  
--restore the transaction log  
 RESTORE LOG [imoltp] FROM DISK = N'c:\data\imoltp_log.dmp' WITH  FILE = 1,  NOUNLOAD,  STATS = 10  
GO  
```  
  
 The next sample shows that restoring filegroup(s) other than the primary and memory-optimized filegroup works similar to the databases without memory-optimized tables  
  
```  
RESTORE DATABASE [imoltp] FILE = N'imoltp_secondary'   
FROM  DISK = N'c:\data\imoltp_secondary.dmp' WITH  FILE = 1,  RECOVERY,  NOUNLOAD,  STATS = 10  
GO  
```  
  
## See Also  
 [Backup, Restore, and Recovery of Memory-Optimized Tables](../../database-engine/backup-restore-and-recovery-of-memory-optimized-tables.md)  
  
  
