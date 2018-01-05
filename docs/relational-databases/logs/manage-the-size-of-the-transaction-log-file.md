---
title: "Manage Transaction Log File Size | Microsoft Docs"
ms.custom: ""
ms.date: "01/05/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "logs"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "dbe-transaction-log"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "transaction logs [SQL Server], size management"
  - "manage log size"
  - "log size, manage"
ms.assetid: 3a70e606-303f-47a8-96d4-2456a18d4297
caps.latest.revision: 23
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
ms.workload: "Active"
---
# Manage the size of the transaction log file
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
This topic covers how to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log size, shrink the transaction log, add to or enlarge a transaction log file, optimize the **tempdb** transaction log growth rate, and control the growth of a transaction log file.  

  ##  <a name="MonitorSpaceUse"></a> Monitor log space use  
Monitor log space use by using [sys.dm_db_log_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql). This DMV returns information about the amount of log space currently used, and indicates when the transaction log needs truncation. For more information, see [DBCC SQLPERF Transact-SQL](../../t-sql/database-console-commands/dbcc-sqlperf-transact-sql.md). For information about the current log file size, its maximum size, and the autogrow option for the file, you can also use the **size**, **max_size**, and **growth** columns for that log file in **sys.database_files**. For more information, see [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md).  
  
> [!IMPORTANT]
> Avoid overloading the log disk. Ensure the log storage can withstand the IOPS and low latency requirements for your tarnsactional load. 
  
##  <a name="ShrinkSize"></a> Shrink log file size  
 To reduce the physical size of a physical log file, you must shrink the log file. This is useful when you know that a transaction log file contains unused space. You can shrink a log file only while the database is online, and at least one virtual log file is free. In some cases, shrinking the log may not be possible until after the next log truncation.  
  
> [!NOTE]
> Factors such as a long-running transaction, that keep virtual log files active for an extended period, can restrict log shrinkage or even prevent the log from shrinking at all. For information, see [Factors that can delay log truncation](../../relational-databases/logs/the-transaction-log-sql-server.md.md#FactorsThatDelayTruncation).  
  
 Shrinking a log file removes one or more [virtual log files (VLFs)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) that hold no part of the logical log (that is, *inactive virtual log files*). When a transaction log file is shrunk, inactive virtual log files are removed from the end of the log file to reduce the log to approximately the target size. 

> [!IMPORTANT]
> Before shrinking the transaction log, keep in mind [Factors that can delay log truncation](../../relational-databases/logs/the-transaction-log-sql-server.md.md#FactorsThatDelayTruncation). If the storage space is required again after a log shrink, the transaction log will grow again and by doing that, introduce performance overhead during log grow operations.
  
 **Shrink a log file (without shrinking database files)**  
  
-   [DBCC SHRINKFILE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)  
  
-   [Shrink a File](../../relational-databases/databases/shrink-a-file.md)  
  
 **Monitor log-file shrink events**  
  
-   [Log File Auto Shrink Event Class](../../relational-databases/event-classes/log-file-auto-shrink-event-class.md).  
  
 **Monitor log space**  
  
-   [sys.dm_db_log_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql)  
  
-   [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) (See the **size**, **max_size**, and **growth** columns for the log file or files.)  
  
> [!WARNING]
> You can set log files to shrink automatically. However, we **recommend against automatic shrinking**, and the **autoshrink** database property is set to FALSE by default. If **autoshrink** is set to TRUE, automatic shrinking reduces the size of a file only when more than 25 percent of its space is unused. 
> The file is shrunk either to the size at which only 25 percent of the file is unused space or to the original size of the file, whichever is larger. For information about changing the setting of the **autoshrink** property, see [View or Change the Properties of a Database](../../relational-databases/databases/view-or-change-the-properties-of-a-database.md). 

##  <a name="AddOrEnlarge"></a> Add or enlarge a log file  
 You can gain space by enlarging the existing log file (if disk space permits) or by adding a log file to the database, typically on a different disk.  
  
-   To add a log file to the database, use the ADD LOG FILE clause of the ALTER DATABASE statement. Adding a log file allows the log to grow.  
-   To enlarge the log file, use the MODIFY FILE clause of the ALTER DATABASE statement, specifying the SIZE and MAXSIZE syntax. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
    
##  <a name="tempdbOptimize"></a> Optimize tempdb transaction log size  
 Restarting a server instance resizes the transaction log of the **tempdb** database to its original, pre-autogrow size. This can reduce the performance of the **tempdb** transaction log. You can avoid this overhead by increasing the size of the **tempdb** transaction log after starting or restarting the server instance. For more information, see [tempdb Database](../../relational-databases/databases/tempdb-database.md).  
  
##  <a name="ControlGrowth"></a> Control transaction log file growth  
 Use the [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md) statement to manage the growth of a transaction log file. Note the following:  
  
-   To change the current file size in KB, MB, GB, and TB units, use the SIZE option.  
  -   To change the growth increment, use the FILEGROWTH option. A value of 0 indicates that automatic growth is set to off and no additional space is permitted. A small autogrowth increment on a log file can reduce performance. The file growth increment on a log file should be sufficiently large to avoid frequent expansion. The default growth increment of 10 percent is generally suitable.  

For information on changing the file-growth property on a log file, see [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md).  
  
-   To control the maximum the size of a log file in KB, MB, GB, and TB units or to set growth to UNLIMITED, use the MAXSIZE option.  
  
  
## See also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](../../relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)    
 [Transaction Log Backups in the SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Backups)    
 [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md) 
