---
title: "SQL Server, Databases Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Databases object"
  - "SQLServer:Databases"
  - "Availability Groups [SQL Server], performance counters"
ms.assetid: a7f9e7d4-fff4-4c72-8b3e-3f18dffc8919
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Databases Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **SQLServer:Databases** object in SQL Server provides counters to monitor bulk copy operations, backup and restore throughput, and transaction log activities. Monitor transactions and the transaction log to determine how much user activity is occurring in the database and how full the transaction log is becoming. The amount of user activity can determine the performance of the database and affect log size, locking, and replication. Monitoring low-level log activity to gauge user activity and resource usage can help you to identify performance bottlenecks.  
  
 Multiple instances of the **Databases** object, each representing a single database, can be monitored at the same time.  
  
 This table describes the SQL Server **Databases** counters.  
  
|SQL Server Databases counters|Description|  
|-----------------------------------|-----------------|  
|**Active Transactions**|Number of active transactions for the database.|  
|**Avg Dist From EOL/LP Request**|Average distance in bytes from end of log per log pool request, for requests in the last VLF.| 
|**Backup/Restore Throughput/sec**|Read/write throughput for backup and restore operations of a database per second. For example, you can measure how the performance of the database backup operation changes when more backup devices are used in parallel or when faster devices are used. Throughput of a database backup or restore operation allows you to determine the progress and performance of your backup and restore operations.|  
|**Bulk Copy Rows/sec**|Number of rows bulk copied per second.|  
|**Bulk Copy Throughput/sec**|Amount of data bulk copied (in kilobytes) per second.|  
|**Commit table entries**|The size (row count) of the in-memory portion of the commit table for the database. For more information, see [sys.dm_tran_commit_table &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/change-tracking-sys-dm-tran-commit-table.md).|  
|**Data File(s) Size (KB)**|Cumulative size (in kilobytes) of all the data files in the database including any automatic growth. Monitoring this counter is useful, for example, for determining the correct size of **tempdb**.|  
|**DBCC Logical Scan Bytes/sec**|Number of logical read scan bytes per second for database console commands (DBCC).|  
|**Group Commit Time/sec**|Group stall time (microseconds) per second.|
|**Log Bytes Flushed/sec**|Total number of log bytes flushed.|  
|**Log Cache Hit Ratio**|Percentage of log cache reads satisfied from the log cache.|  
|**Log Cache Hit Ratio Base**|For internal use only.| 
|**Log Cache Reads/sec**|Reads performed per second through the log manager cache.|  
|**Log File(s) Size (KB)**|Cumulative size (in kilobytes) of all the transaction log files in the database.|  
|**Log File(s) Used Size (KB)**|The cumulative used size of all the log files in the database.|  
|**Log Flush Wait Time**|Total wait time (in milliseconds) to flush the log. On an Always On secondary database, this value indicates the wait time for log records to be hardened to disk.|  
|**Log Flush Waits/sec**|Number of commits per second waiting for the log flush.|  
|**Log Flush Write Time (ms)**|Time in milliseconds for performing writes of log flushes that were completed in the last second.|  
|**Log Flushes/sec**|Number of log flushes per second.|  
|**Log Growths**|Total number of times the transaction log for the database has been expanded.|  
|**Log Pool Cache Misses/sec**|Number of requests for which the log block was not available in the log pool. The *log pool* is an in-memory cache of the transaction log. This cache is used to optimize reading the log for recovery, transaction replication, rollback, and [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].|  
|**Log Pool Disk Reads/sec**|Number of disk reads that the log pool issued to fetch log blocks.|  
|**Log Pool Hash Deletes/sec**|Rate of raw hash entry deletes from the Log Pool.|
|**Log Pool Hash Inserts/sec**|Rate of raw hash entry inserts into the Log Pool.|
|**Log Pool Invalid Hash Entry/sec**|Rate of hash lookups failing due to being invalid.|
|**Log Pool Log Scan Pushes/sec**|Rate of Log block pushes by log scans, which may come from disk or memory.|
|**Log Pool LogWriter Pushes/sec**|Rate of Log block pushes by log writer thread.|
|**Log Pool Push Empty FreePool/sec**|Rate of Log block push fails due to empty free pool.|
|**Log Pool Push Low Memory/sec**|Rate of Log block push fails due to being low on memory.|
|**Log Pool Push No Free Buffer/sec**|Rate of Log block push fails due to free buffer unavailable.|
|**Log Pool Req. Behind Trunc/sec**|Log pool cache misses due to block requested being behind truncation LSN.|
|**Log Pool Requests Base**|For internal use only.| 
|**Log Pool Requests Old VLF/sec**|Log Pool requests that were not in the last VLF of the log.|  
|**Log Pool Requests/sec**|The number of log-block requests processed by the log pool.|  
|**Log Pool Total Active Log Size**|Current total active log stored in the shared cache buffer manager in bytes.|
|**Log Pool Total Shared Pool Size**|Current total memory usage of the shared cache buffer manager in bytes.|
|**Log Shrinks**|Total number of log shrinks for this database.|  
|**Log Truncations**|The number of times the transaction log has been truncated (in Simple Recovery Model).|  
|**Percent Log Used**|Percentage of space in the log that is in use.|  
|**Repl. Pending Xacts**|Number of transactions in the transaction log of the publication database marked for replication, but not yet delivered to the distribution database.|  
|**Repl. Trans. Rate**|Number of transactions per second read out of the transaction log of the publication database and delivered to the distribution database.|  
|**Shrink Data Movement Bytes/sec**|Amount of data being moved per second by autoshrink operations, or DBCC SHRINKDATABASE or DBCC SHRINKFILE statements.|  
|**Tracked transactions/sec**|Number of committed transactions recorded in the commit table for the database.|  
|**Transactions/sec**|Number of transactions started for the database per second.<br /><br /> **Transactions/sec** does not count XTP-only transactions (transactions started by a natively compiled stored procedure)..|  
|**Write Transactions/sec**|Number of transactions that wrote to the database and committed, in the last second.|  
|**XTP Controller DLC Latency Base**|For internal use only.| 
|**XTP Controller DLC Latency/Fetch**|Average latency in microseconds between log blocks entering the Direct Log Consumer and being retrieved by the XTP controller, per second.|
|**XTP Controller DLC Peak Latency**|The largest recorded latency, in microseconds, of a fetch from the Direct Log Consumer by the XTP controller.|
|**XTP Controller Log Processed/sec**|The amount of log bytes processed by the XTP controller thread, per second.|
|**XTP Memory Used (KB)**|The amount of memory used by XTP in the database.| 
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [SQL Server, Database Replica](../../relational-databases/performance-monitor/sql-server-database-replica.md)  
  
  
