---
title: "Manage Transaction Log File Size | Microsoft Docs"
description: Learn how to monitor SQL Server transaction log size, shrink the log, enlarge a log, optimize the tempdb log growth rate, and control transaction log growth.
ms.custom: ""
ms.date: 11/10/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "transaction logs [SQL Server], size management"
  - "manage log size"
  - "log size, manage"
ms.assetid: 3a70e606-303f-47a8-96d4-2456a18d4297
author: "MashaMSFT"
ms.author: "mathoma"
---
# Manage the size of the transaction log file
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
This topic covers how to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log size, shrink the transaction log, add to or enlarge a transaction log file, optimize the **tempdb** transaction log growth rate, and control the growth of a transaction log file.  

##  <a name="MonitorSpaceUse"></a>Monitor log space use  
Monitor log space use by using [sys.dm_db_log_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md). This DMV returns information about the amount of log space currently used, and indicates when the transaction log needs truncation. 

For information about the current log file size, its maximum size, and the autogrow option for the file, you can also use the **size**, **max_size**, and **growth** columns for that log file in [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md).  
  
> [!IMPORTANT]
> Avoid overloading the log disk. Ensure the log storage can withstand the [IOPS](https://wikipedia.org/wiki/IOPS) and low latency requirements for your transactional load. 
  
##  <a name="ShrinkSize"></a> Shrink log file size  
 To reduce the physical size of a physical log file, you must shrink the log file. This is useful when you know that a transaction log file contains unused space. You can shrink a log file only while the database is online, and at least one [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) is free. In some cases, shrinking the log may not be possible until after the next log truncation.  
  
> [!NOTE]
> Factors such as a long-running transaction, that keep [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) active for an extended period, can restrict log shrinkage or even prevent the log from shrinking at all. For information, see [Factors that can delay log truncation](../../relational-databases/logs/the-transaction-log-sql-server.md#FactorsThatDelayTruncation).  
  
Shrinking a log file removes one or more [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) that hold no part of the logical log (that is, *inactive VLFs*). When a transaction log file is shrunk, inactive VLFs are removed from the end of the log file to reduce the log to approximately the target size. 

> [!IMPORTANT]
> Before shrinking the transaction log, keep in mind [Factors that can delay log truncation](../../relational-databases/logs/the-transaction-log-sql-server.md#FactorsThatDelayTruncation). If the storage space is required again after a log shrink, the transaction log will grow again and by doing that, introduce performance overhead during log growth operations. For more information, see the [Recommendations](#Recommendations) in this topic.
  
 **Shrink a log file (without shrinking database files)**  
  
-   [DBCC SHRINKFILE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)  
  
-   [Shrink a File](../../relational-databases/databases/shrink-a-file.md)  
  
 **Monitor log-file shrink events**  
  
-   [Log File Auto Shrink Event Class](../../relational-databases/event-classes/log-file-auto-shrink-event-class.md).  
  
 **Monitor log space**  
  
-   [sys.dm_db_log_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)  
  
-   [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) (See the **size**, **max_size**, and **growth** columns for the log file or files.)  
  
##  <a name="AddOrEnlarge"></a> Add or enlarge a log file  
You can gain space by enlarging the existing log file (if disk space permits) or by adding a log file to the database, typically on a different disk. One transaction log file is sufficient unless log space is running out, and disk space is also running out on the volume that holds the log file.   
  
-   To add a log file to the database, use the `ADD LOG FILE` clause of the `ALTER DATABASE` statement. Adding a log file allows the log to grow.  
-   To enlarge the log file, use the `MODIFY FILE` clause of the `ALTER DATABASE` statement, specifying the `SIZE` and `MAXSIZE` syntax. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41; File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).  

For more information, see the [Recommendations](#Recommendations) in this topic.
    
##  <a name="tempdbOptimize"></a> Optimize tempdb transaction log size  
 Restarting a server instance resizes the transaction log of the **tempdb** database to its original, pre-autogrow size. This can reduce the performance of the **tempdb** transaction log. 
 
 You can avoid this overhead by increasing the size of the **tempdb** transaction log after starting or restarting the server instance. For more information, see [tempdb Database](../../relational-databases/databases/tempdb-database.md).  
  
##  <a name="ControlGrowth"></a> Control transaction log file growth  
 Use the [ALTER DATABASE &#40;Transact-SQL&#41; File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md) statement to manage the growth of a transaction log file. Note the following:  
  
-   To change the current file size in KB, MB, GB, and TB units, use the `SIZE` option.  
-   To change the growth increment, use the `FILEGROWTH` option. A value of 0 indicates that automatic growth is set to off and no additional space is permitted.  
-   To control the maximum the size of a log file in KB, MB, GB, and TB units or to set growth to UNLIMITED, use the `MAXSIZE` option.  

For more information, see the [Recommendations](#Recommendations) in this topic.

## <a name="Recommendations"></a> Recommendations
Following are some general recommendations when you are working with transaction log files:

-   The automatic growth (autogrow) increment of the transaction log, as set by the `FILEGROWTH` option, must be large enough to stay ahead of the needs of the workload transactions. The file growth increment on a log file should be sufficiently large to avoid frequent expansion. A good pointer to properly size a transaction log is monitoring the amount of log occupied during:
    -  The time required to execute a full backup, because log backups cannot occur until it finishes.
    -  The time required for the largest index maintenance operations.
    -  The time required to execute the largest batch in a database.

-   When setting **autogrow** for data and log files using the `FILEGROWTH` option, it might be preferred to set it in **size** instead of **percentage**, to allow better control on the growth ratio, as percentage is an ever-growing amount.
    -  In versions prior to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], transaction logs cannot leverage [Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md), so extended log growth times are especially critical. 
    -  Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] (all editions) and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], instant file initialization can benefit transaction log growth events up to 64 MB. The default auto growth size increment for new databases is 64 MB. Transaction log file autogrowth events larger than 64 MB cannot benefit from instant file initialization. 
    -  As a best practice, do not set the `FILEGROWTH` option value above 1,024 MB for transaction logs. The default values for `FILEGROWTH` option are:  
  
      |Version|Default values|  
      |-------------|--------------------|  
      |Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]|Data 64 MB. Log files 64 MB.|  
      |Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Data 1 MB. Log files 10%.|  
      |Prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|Data 10%. Log files 10%.|  

-   A small growth increment can generate too many small [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) and can reduce performance. To determine the optimal VLF distribution for the current transaction log size of all databases in a given instance, and the required growth increments to achieve the required size, see this [script](https://github.com/Microsoft/tigertoolbox/tree/master/Fixing-VLFs).

-   A large growth increment can generate too few and large [VLFs](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) and can also affect performance. To determine the optimal VLF distribution for the current transaction log size of all databases in a given instance, and the required growth increments to achieve the required size, see this [script](https://github.com/Microsoft/tigertoolbox/tree/master/Fixing-VLFs). 

-   Even with autogrow enabled, you can receive a message that the transaction log is full, if it cannot grow fast enough to satisfy the needs of your query. For more information on changing the growth increment, see [ALTER DATABASE &#40;Transact-SQL&#41; File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)

-   Having multiple log files in a database does not enhance performance in any way, because the transaction log files do not use [proportional fill](../../relational-databases/pages-and-extents-architecture-guide.md#ProportionalFill) like data files in a same filegroup.  

-   Log files can be set to shrink automatically. However this is **not recommended**, and the **auto_shrink** database property is set to FALSE by default. If **auto_shrink** is set to TRUE, automatic shrinking reduces the size of a file only when more than 25 percent of its space is unused. 
    -   The file is shrunk either to the size at which only 25 percent of the file is unused space or to the original size of the file, whichever is larger. 
    -   For information about changing the setting of the **auto_shrink** property, see [View or Change the Properties of a Database](../../relational-databases/databases/view-or-change-the-properties-of-a-database.md) and [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md). 
  
## See also  
[BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
[Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](../../relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)    
[Transaction Log Backups in the SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Backups)    
[Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)    
[ALTER DATABASE &#40;Transact-SQL&#41; File and Filegroup options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)
