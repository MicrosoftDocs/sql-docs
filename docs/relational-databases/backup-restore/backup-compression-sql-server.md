---
title: "Backup Compression (SQL Server)"
description: Learn about compression of SQL Server backups, including restrictions, performance trade-offs, Configuring backup compression, and the compression ratio.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "log shipping [SQL Server], backup compression"
  - "backup compression [SQL Server], about backup compression"
  - "compression [SQL Server], backup compression"
  - "backups [SQL Server], compression"
  - "backing up [SQL Server], backup compression"
  - "backup compression [SQL Server]"
---
# Backup compression (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the compression of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups, including restrictions, performance trade-off of compressing backups, the configuration of backup compression, and the compression ratio.  Backup compression is supported on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] editions: Enterprise, Standard, and Developer.  Every edition of [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later can restore a compressed backup. 
 
  
##  <a name="Benefits"></a> Benefits  
  
-   Because a compressed backup is smaller than an uncompressed backup of the same data, compressing a backup typically requires less device I/O and therefore usually increases backup speed significantly.  
  
     For more information, see [Performance Impact of Compressing Backups](#PerfImpact), later in this article.  
  
  
##  <a name="Restrictions"></a> Restrictions  

 The following restrictions apply to compressed backups:  
  
-   Compressed and uncompressed backups can't coexist in a media set.  
  
-   Previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can't read compressed backups.  
  
-   NTbackups can't share a tape with compressed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups.  
  
## ZSTD compression algorithm introduced in SQL Server 2025

Starting with [!INCLUDE[sssql25-md](../../includes/sssql25-md.md)], a new compression algorithm, ZSTD, is available for backup compression. This algorithm is faster and more effective than the previous MS_XPRESS algorithm. 

You can use the ZSTD algorithm for backup compression in one of the following ways:
- By specifying the `WITH COMPRESSION (ALGORITHM = ZSTD)` option in the [BACKUP](../../t-sql/statements/backup-transact-sql.md#compression) Transact-SQL command for *a specific backup*. 
- By setting the [backup compression algorithm server configuration option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-algorithm-server-configuration-option.md#backup-compression-algorithms) to 3. This option sets the default backup compression algorithm to ZSTD *for all backups* that use the `WITH COMPRESSION` option.
 
##  <a name="PerfImpact"></a> Performance impact of compressing backups  

By default, compression significantly increases CPU usage, and the additional CPU consumed by the compression process might adversely impact concurrent operations. Therefore, you might want to create low-priority compressed backups in a session whose CPU usage is limited by [Resource Governor](../../relational-databases/resource-governor/resource-governor.md). For more information, see [Use Resource Governor to Limit CPU Usage by Backup Compression &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md). 

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can use [Integrated offloading & acceleration](../integrated-acceleration/overview.md) to compress backups and offload the CPU resources for the backup.
  
To obtain a good picture of your backup I/O performance, you can isolate the backup I/O to or from devices by evaluating the following sorts of performance counters:  
  
-   Windows I/O performance counters, such as the physical-disk counters  
  
-   The **Device Throughput Bytes/sec** counter of the [SQLServer:Backup Device](../../relational-databases/performance-monitor/sql-server-backup-device-object.md) object  
  
-   The **Backup/Restore Throughput/sec** counter of the [SQLServer:Databases](../../relational-databases/performance-monitor/sql-server-databases-object.md) object  
  
 For information about Windows counters, see Windows help. For information about how to work with SQL Server counters, see [Use SQL Server Objects](../../relational-databases/performance-monitor/use-sql-server-objects.md).  
  
   
##  <a name="CompressionRatio"></a> Calculate the compression ratio of a compressed backup  
 To calculate the compression ratio of a backup, use the values for the backup in the **backup_size** and **compressed_backup_size** columns of the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) history table, as follows:  
  
 **backup_size**:**compressed_backup_size**  
  
 For example, a 3:1 compression ratio indicates that you're saving about 66% on disk space. To query on these columns, you can use the following Transact-SQL statement:  
  
```sql  
SELECT backup_size/compressed_backup_size FROM msdb..backupset;  
```  
  
 The compression ratio of a compressed backup depends on the data that has been compressed. A variety of factors can impact the compression ratio obtained. Major factors include:  
  
-   The type of data.  
  
     Character data compresses more than other types of data.  
  
-   The consistency of the data among rows on a page.  
  
     Typically, if a page contains several rows in which a field contains the same value, significant compression might occur for that value. In contrast, for a database that contains random data or that contains only one large row per page, a compressed backup would be almost as large as an uncompressed backup.  
  
-   Whether the data is encrypted  
  
     Encrypted data compresses significantly less than equivalent unencrypted data. For example, if data is encrypted at the column level with Always Encrypted, or with other application-level encryption, compressing backups might not reduce the size significantly.

     For more information related to compressing databases encrypted with Transparent Data Encryption (TDE), see [Backup compression with TDE](#backup-compression-with-tde).

-   Whether the database is compressed.  
  
     If the database is compressed, compressing backups might not reduce their size by much, if at all.  

## Backup compression with TDE

Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], setting `MAXTRANSFERSIZE` **larger than 65536 (64 KB)** enables an optimized compression algorithm for [Transparent Data Encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md) encrypted databases that first decrypts a page, compresses it, and then encrypts it again. If `MAXTRANSFERSIZE` isn't specified, or if `MAXTRANSFERSIZE = 65536` (64 KB) is used, backup compression with TDE encrypted databases directly compresses the encrypted pages, and might not yield good compression ratios. For more information, see [Backup Compression for TDE-enabled Databases](/archive/blogs/sqlcat/sqlsweet16-episode-1-backup-compression-for-tde-enabled-databases).

Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] CU5, setting `MAXTRANSFERSIZE` is no longer required to enable this optimized compression algorithm with TDE. If the backup command is specified `WITH COMPRESSION` or the *backup compression default* server configuration is set to 1, `MAXTRANSFERSIZE` will automatically be increased to 128K to enable the optimized algorithm. If `MAXTRANSFERSIZE` is specified on the backup command with a value > 64K, the provided value is honored. In other words, SQL Server will never automatically decrease the value, it will only increase it. If you need to back up a TDE encrypted database with `MAXTRANSFERSIZE = 65536`, you must specify `WITH NO_COMPRESSION` or ensure that the *backup compression default* server configuration is set to 0.

For more information, see [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md).

> [!WARNING]
> If you are performing multiple backups to a single backup media set after applying SQL Server 2019 CU5, install SQL Server 2019 CU9 ([KB13768244](/troubleshoot/sql/releases/sqlserver-2019/cumulativeupdate9)) to resolve a known restore issue. Failure to do so can result in backups that can't be restored.
> After applying SQL Server 2019 CU9, the backup process enforces the `INIT` option, which prevents combining backups of databases that use TDE or have different Database Encryption Keys (DEKs) into a single backup media set.

##  <a name="Allocation"></a> Allocation of space for the backup file

 For compressed backups, the size of the final backup file depends on how compressible the data is, and this is unknown before the backup operation finishes.  Therefore, by default, when backing up a database using compression, the Database Engine uses a preallocation algorithm for the backup file. This algorithm preallocates a predefined percentage of the size of the database for the backup file. If more space is needed during the backup operation, the Database Engine grows the file. If the final size is less than the allocated space, at the end of the backup operation, the Database Engine shrinks the file to the actual final size of the backup.  
  
 To allow the backup file to grow only as needed to reach its final size, use trace flag 3042. Trace flag 3042 causes the backup operation to bypass the default backup compression preallocation algorithm. This trace flag is useful if you need to save on space by allocating only the actual size required for the compressed backup. However, using this trace flag might cause a slight performance penalty (a possible increase in the duration of the backup operation).  

##  <a name="RelatedTasks"></a> Related tasks  
  
-   [Configure Backup Compression &#40;SQL Server&#41;](../../relational-databases/backup-restore/configure-backup-compression-sql-server.md)  
  
-   [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md)  
  
-   [Use Resource Governor to Limit CPU Usage by Backup Compression &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md)  
  
-   [DBCC TRACEON &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md)  
  
-   [DBCC TRACEOFF &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceoff-transact-sql.md)  
  
## Related content

- [Backup overview (SQL Server)](backup-overview-sql-server.md)
- [Set trace flags with DBCC TRACEON (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
