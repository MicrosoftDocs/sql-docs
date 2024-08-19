---
title: "Manage transaction log file size"
description: Learn how to monitor SQL Server transaction log size, shrink the log, enlarge a log, optimize the tempdb log growth rate, and control transaction log growth.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: wiassaf, maghan
ms.date: 08/19/2024 
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "transaction logs [SQL Server], size management"
  - "manage log size"
  - "log size, manage"
---

# Manage the size of the transaction log file

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article covers how to monitor [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] transaction log size, shrink the transaction log, add to or enlarge a transaction log file, optimize the `tempdb` transaction log growth rate, and control the growth of a transaction log file.

This article applies to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Though similar, for information on managing the size of transaction log files in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], see [Manage file space for databases in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/file-space-manage?view=azuresql-mi&preserve-view=true). For information about [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], see [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage?view=azuresql-db&preserve-view=true).

## Understand types of storage space for a database

Understanding the following storage space quantities is important for managing the file space of a database.

| Database quantity | Definition | Comments |
| --- | --- | --- |
| **Data space used** | The space used to store database data. | Generally, space used increases (decreases) on inserts (deletes). In some cases, the space used doesn't change on inserts or deletes depending on the amount and pattern of data involved in the operation and any fragmentation. For example, deleting one row from every data page doesn't necessarily decrease the space used. |
| **Data space allocated** | The formatted file space made available for storing database data. | The amount of space allocated grows automatically but never decreases after deletes. This behavior ensures that future inserts are faster since space doesn't need to be reformatted. |
| **Data space allocated but unused** | The difference between the amount allocated and data space used. | This quantity represents the maximum free space that shrinking database data files can reclaim. |
| **Data max size** | The maximum amount of space to store database data. | The amount of data space allocated can't grow beyond the data max size. |

The following diagram illustrates the relationship between the different types of storage space for a database.

:::image type="content" source="../media/manage-the-size-of-the-transaction-log-file/understand-database-space-quantities.png" alt-text="Diagram that demonstrates the size of difference database space concepts in the database quantity table.":::

### Query a single database for file space information

Use the following query to return the amount of database file space allocated and the amount of unused space allocated. Units of the query result are in MB.

```sql
-- Connect to a user database
SELECT file_id, type_desc,
       CAST(FILEPROPERTY(name, 'SpaceUsed') AS decimal(19,4)) * 8 / 1024. AS space_used_mb,
       CAST(size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS decimal(19,4)) AS space_unused_mb,
       CAST(size AS decimal(19,4)) * 8 / 1024. AS space_allocated_mb,
       CAST(max_size AS decimal(19,4)) * 8 / 1024. AS max_size_mb
FROM sys.database_files;
```

## <a id="MonitorSpaceUse"></a> Monitor log space use

Monitor log space use by using [sys.dm_db_log_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md). This DMV returns information about the amount of log space currently used, and indicates when the transaction log needs truncation.

For information about the current log file size, its maximum size, and the auto grow option for the file, you can also use the `size`, `max_size`, and `growth` columns for that log file in [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md).

> [!IMPORTANT]  
> Avoid overloading the log disk. Ensure the log storage can withstand the [IOPS](https://wikipedia.org/wiki/IOPS) and low latency requirements for your transactional load.

## <a id="ShrinkSize"></a> Shrink log file

Shrink the log file to reduce its physical size by returning free space to the operating system. A shrink only makes a difference when a transaction log file contains unused space.

If the log file is full, likely because of open transactions, investigate [what is preventing transaction log truncation](troubleshoot-a-full-transaction-log-sql-server-error-9002.md#how-to-resolve-a-full-transaction-log).

> [!CAUTION]  
> Shrink operations should not be considered a regular maintenance operation. Data and log files that grow due to regular, recurring business operations do not require shrink operations. Shrink commands impact database performance while running; they should be run during periods of low usage. It is not recommended to shrink data files if a regular application workload will cause the files to grow to the same allocated size again.

Be aware of the potential negative performance impact of shrinking database files; see [Index maintenance after shrink](#rebuild-indexes).

Before shrinking the transaction log, keep in mind [Factors that can delay log truncation](../../relational-databases/logs/the-transaction-log-sql-server.md#FactorsThatDelayTruncation). If storage space is required again after a log shrink, the transaction log will grow again, introducing performance overhead during log growth operations. For more information, see the [Recommendations](#Recommendations).

You can shrink a log file only while the database is online, and at least one [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) is free. In some cases, shrinking the log might only be possible after the next log truncation.

Factors, such as a long-running transaction, can keep [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) active for an extended period, can restrict log shrinkage, or even prevent the log from shrinking at all. For information, see [Factors that can delay log truncation](../../relational-databases/logs/the-transaction-log-sql-server.md#FactorsThatDelayTruncation).

Shrinking a log file removes one or more [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) that hold no part of the logical log (that is, *inactive VLFs*). When you shrink a transaction log file, inactive VLFs are removed from the end of the log file to reduce the log to approximately the target size.

For more information on shrink operations, review the following links:

**Shrink a log file (without shrinking database files)**

- [DBCC SHRINKFILE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)

- [Shrink a File](../../relational-databases/databases/shrink-a-file.md)

**Monitor log-file shrink events**

- [Log File Auto Shrink Event Class](../../relational-databases/event-classes/log-file-auto-shrink-event-class.md).

**Monitor log space**

- [sys.dm_db_log_space_usage (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)

- [sys.database_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) (See the `size`, `max_size`, and `growth` columns for the log file or files.)

### <a id="rebuild-indexes"></a> Index maintenance after shrink

Indexes might become fragmented after a shrink operation is completed against data files. This reduces their effectiveness for performance optimization for certain workloads, such as queries using large scans. If performance degradation occurs after the shrink operation is complete, consider index maintenance to rebuild indexes. Keep in mind that index rebuilds require free space in the database and hence might increase the allocated space, counteracting the effect of shrink.

For more information about index maintenance, see [Optimize index maintenance to improve query performance and reduce resource consumption](../indexes/reorganize-and-rebuild-indexes.md).

## <a id="AddOrEnlarge"></a> Add or enlarge a log file

You can gain space by enlarging the existing log file (if disk space permits) or adding a log file to the database, typically on a different disk. One transaction log file is sufficient unless log space is running out and disk space is also running out on the volume that holds the log file.

To add a log file to the database, use the `ADD LOG FILE` clause of the `ALTER DATABASE` statement. This allows the log to grow.
- To enlarge the log file, use the `MODIFY FILE` clause of the `ALTER DATABASE` statement, specifying the `SIZE` and `MAXSIZE` syntax. For more information, see [ALTER DATABASE (Transact-SQL) File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).

For more information, see the [Recommendations](#Recommendations).

## <a id="tempdbOptimize"></a> Optimize tempdb transaction log size

Restarting a server instance resizes the transaction log of the `tempdb` database to its original, pre-autogrow size. This can reduce the performance of the `tempdb` transaction log.

You can avoid this overhead by increasing the `tempdb` transaction log size after starting or restarting the server instance. For more information, see [tempdb Database](../../relational-databases/databases/tempdb-database.md).

## <a id="ControlGrowth"></a> Control transaction log file growth

Use the [ALTER DATABASE (Transact-SQL) File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md) statement to manage the growth of a transaction log file. Note the following:

Use the' SIZE' option to change the current file size in KB, MB, GB, and TB units.
- To change the growth increment, use the `FILEGROWTH` option. A value of 0 indicates that automatic growth is set to off and no extra space is permitted.
Use the' MAXSIZE' option to control the maximum size of a log file in KB, MB, GB, and TB units or to set growth to UNLIMITED.

For more information, see the [Recommendations](#Recommendations).

## <a id="Recommendations"></a> Recommendations

Following are some general recommendations when you're working with transaction log files:

- The automatic growth (auto grow) increment of the transaction log, as set by the `FILEGROWTH` option, must be large enough to stay ahead of the needs of the workload transactions. The file growth increment on a log file should be sufficiently large to avoid frequent expansion. A good pointer to properly size a transaction log is monitoring the amount of log occupied during:
    - The time required to execute a full backup because log backups can't occur until it finishes.
    - The time required for the largest index maintenance operations.
    - The time required to execute the largest batch in a database.

- When setting **auto grow** for data and log files using the `FILEGROWTH` option, it might be preferred to set it in *size* instead of *percentage to allow better control of the growth ratio, as a percentage is an ever-growing amount.
    - In versions prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], transaction logs can't use [Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md), so extended log growth times are especially critical.
    - Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] (all editions) and in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], instant file initialization can benefit transaction log growth events up to 64 MB. The default autogrowth size increment for new databases is 64 MB. Transaction log file autogrowth events larger than 64 MB can't benefit from instant file initialization.
    - As a best practice, don't set the `FILEGROWTH` option value above 1,024 MB for transaction logs. The default values for the `FILEGROWTH` option are:

      | Version | Default values |
      | --- | --- |
      | Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] | Data 64 MB. Log files 64 MB. |
      | Starting with [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] | Data 1 MB. Log files 10%. |
      | Prior to [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] | Data 10%. Log files 10%. |

- A small outgrowth increment can generate too many small [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) and can reduce performance. To determine the optimal VLF distribution for the current transaction log size of all databases in a given instance and the required growth increments to achieve the required size, see this [script for analyzing and fixing VLFs, provided by the SQL Tiger Team](https://github.com/Microsoft/tigertoolbox/tree/master/Fixing-VLFs).

- A large autogrowth increment can cause two problems:
  - A large autogrowth increment can cause the database to pause while the new space is allocated, potentially causing query timeouts.
    - A large autogrowth increment can generate too few and large [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) and can also affect performance. To determine the optimal VLF distribution for the current transaction log size of all databases in a given instance and the required growth increments to achieve the required size, see this [script for analyzing and fixing VLFs, provided by the SQL Tiger Team](https://github.com/Microsoft/tigertoolbox/tree/master/Fixing-VLFs).

- Even with autogrow enabled, you can receive a message that the transaction log is full if it can't grow fast enough to satisfy the needs of your query. For more information on changing the growth increment, see [ALTER DATABASE (Transact-SQL) File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).

- Having multiple log files in a database doesn't enhance performance in any way, because the transaction log files don't use [proportional fill](../../relational-databases/pages-and-extents-architecture-guide.md#ProportionalFill) like data files in a same filegroup.

Log files can be set to shrink automatically. However, this isn't recommended, and the **auto_shrink** database property is set to FALSE by default. If **auto_shrink** is set to TRUE, automatic shrinking reduces the size of a file only when more than 25 percent of its space is unused.
    - The file is shrunk either to the size at which only 25 percent of the file is unused space or to the original size of the file, whichever is larger.
    - For information about changing the setting of the **auto_shrink** property, see [View or Change the Properties of a Database](../../relational-databases/databases/view-or-change-the-properties-of-a-database.md) and [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md).

## Related content

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Troubleshoot a Full Transaction Log (SQL Server Error 9002)](../../relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)
- [Transaction Log Backups in the SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Backups)
- [Transaction Log Backups (SQL Server)](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)
- [ALTER DATABASE (Transact-SQL) File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)
