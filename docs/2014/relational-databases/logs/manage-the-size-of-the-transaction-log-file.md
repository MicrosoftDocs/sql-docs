---
title: "Manage the Size of the Transaction Log File | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "transaction logs [SQL Server], size management"
ms.assetid: 3a70e606-303f-47a8-96d4-2456a18d4297
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Manage the Size of the Transaction Log File
  In some cases, it can be useful to physically shrink or expand the physical log file of the transaction log of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. This topic contains information about how to monitor the size of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log, shrink the transaction log, add or enlarge a transaction log file, optimize the **tempdb** transaction log growth rate, and control the growth of a transaction log file.  
  
  
##  <a name="MonitorSpaceUse"></a> Monitor Log Space Use  
 You can monitor log space use by using DBCC SQLPERF (LOGSPACE). This command returns information about the amount of log space currently used and indicates when the transaction log is in need of truncation. For more information, see [DBCC SQLPERF &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-sqlperf-transact-sql). For information about the current size of a log file, its maximum size, and the autogrow option for the file, you can also use the **size**, **max_size**, and **growth** columns for that log file in **sys.database_files**. For more information, see [sys.database_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql).  
  
> [!IMPORTANT]  
>  We recommend that you avoid overloading the log disk.  
  
  
##  <a name="ShrinkSize"></a> Shrink the Size of the Log File  
 To reduce the physical size of a physical log file, you must shrink the log file. This is useful if you know that a transaction log file contains unused space that you will not be needing. Shrinking a log file can occur only while the database is online and, also, at least one virtual log file is free. In some cases, shrinking the log may not be possible until after the next log truncation.  
  
> [!NOTE]  
>  Factors, such as a long-running transaction, that keep virtual log files active for an extended period can restrict log shrinkage or even prevent the log from shrinking at all. For information about factors that can delay log truncation, see [The Transaction Log &#40;SQL Server&#41;](the-transaction-log-sql-server.md).  
  
 Shrinking a log file removes one or more virtual log files that do not hold any part of the logical log (that is, *inactive virtual log files*). When a transaction log file is shrunk, enough inactive virtual log files are removed from the end of the log file to reduce the log to approximately the target size.  
  
 **To shrink a log file (without shrinking database files)**  
  
-   [DBCC SHRINKFILE &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql)  
  
-   [Shrink a File](../databases/shrink-a-file.md)  
  
 **To monitor log-file shrink events**  
  
-   [Log File Auto Shrink Event Class](../event-classes/log-file-auto-shrink-event-class.md).  
  
 `To monitor log space`  
  
-   [DBCC SQLPERF &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-sqlperf-transact-sql)  
  
-   [sys.database_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql) (See the **size**, **max_size**, and **growth** columns for the log file or files.)  
  
> [!NOTE]  
>  Shrinking database and log files can be set to occur automatically. However, we recommend against automatic shrinking, and the `autoshrink` database property is set to FALSE by default. If `autoshrink` is set to TRUE, automatic shrinking reduces the size of a file only when more than 25 percent of its space is unused. The file is shrunk either to the size at which only 25 percent of the file is unused space or to the original size of the file, whichever is larger. For information about changing the setting of the `autoshrink` property, see [View or Change the Properties of a Database](../databases/view-or-change-the-properties-of-a-database.md)-use the **Auto Shrink** property on the **Options** page-or [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options)-use the AUTO_SHRINK option.  
  
  
##  <a name="AddOrEnlarge"></a> Add or Enlarge a Log File  
 Alternatively, you can gain space by enlarging the existing log file (if disk space permits) or by adding a log file to the database, typically on a different disk.  
  
-   To add a log file to the database, use the ADD LOG FILE clause of the ALTER DATABASE statement. Adding a log file allows the log to grow.  
  
-   To enlarge the log file, use the MODIFY FILE clause of the ALTER DATABASE statement, specifying the SIZE and MAXSIZE syntax. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql).  
  
  
##  <a name="tempdbOptimize"></a> Optimize the Size of the tempdb Transaction Log  
 Restarting a server instance resizes the transaction log of the **tempdb** database to its original, pre-autogrow size. This can reduce the performance of the **tempdb** transaction log. You can avoid this overhead by increasing the size of the **tempdb** transaction log after starting or restarting the server instance. For more information, see [tempdb Database](../databases/tempdb-database.md).  
  
  
##  <a name="ControlGrowth"></a> Control the Growth of a Transaction Log File  
 You can use the [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql) statement to manage the growth of a transaction log file. Note the following:  
  
-   To change the current file size in KB, MB, GB, and TB units, use the SIZE option.  
  
-   To change the growth increment, use the FILEGROWTH option. A value of 0 indicates that automatic growth is set to off and no additional space is permitted. A small autogrowth increment on a log file can reduce performance. The file growth increment on a log file should be sufficiently large to avoid frequent expansion. The default growth increment of 10 percent is generally suitable.  
  
     For information on changing the file-growth property on a log file, see [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql).  
  
-   To control the maximum the size of a log file in KB, MB, GB, and TB units or to set growth to UNLIMITED, use the MAXSIZE option.  
  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](troubleshoot-a-full-transaction-log-sql-server-error-9002.md)  
  
  
