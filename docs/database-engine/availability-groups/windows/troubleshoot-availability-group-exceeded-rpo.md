---
title: "Troubleshoot: Availability group exceeded RPO (SQL Server) | Microsoft Docs"
ms.custom: "ag-guide"
ms.date: "06/13/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 38de1841-9c99-435a-998d-df81c7ca0f1e
author: rothja
ms.author: jroth
manager: craigg
---
# Troubleshoot: Availability group exceeded RPO
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  After you perform a forced manual failover on an availability group to an asynchronous-commit secondary replica, you may find that data loss is more than your recovery point objective (RPO). Or, when you calculate the potential data loss of an asynchronous-commit secondary replica using the method in [Monitor Performance for Always On Availability Groups](monitor-performance-for-always-on-availability-groups.md), you find that it exceeds your RPO.  
  
 A synchronous-commit secondary replica guarantees zero data loss, but the potential data loss of an asynchronous-commit secondary replica depends on how much log is still waiting to be hardened on the secondary replica.  
  
 The following sections describe the common causes for high potential data loss of an asynchronous-commit secondary replica, assuming that you do not have a systemic performance issue in your server instance that is unrelated to availability groups.  
  
1.  [High network latency or low network throughput causes log build-up on the primary replica](#BKMK_LATENCY)  
  
2.  [Disk I/O bottleneck slows down log hardening on the secondary replica](#BKMK_IO_BOTTLENECK)  
  
##  <a name="BKMK_LATENCY"></a> High network latency or low network throughput causes log build-up on the primary replica  
 The most common reason for the databases exceeding their RPO is that they cannot be sent to the secondary replica fast enough.  
  
### Explanation  
 The primary replica activates flow control on the log send when it has exceeded the maximum allowable number of unacknowledged messages sent over to the secondary replica. Until some of these messages have been acknowledged, no more log blocks can be sent to the secondary replica. Since data loss can be prevented only when they have been hardened on the secondary replica, the build-up of unsent log messages increases potential data loss.  
  
### Diagnosis and resolution  
 A high number of messages resent to the secondary replica can indicate high network latency and network noise. You can also compare the DMV value **log_send_rate** with the performance object Log Bytes Flushed/sec. If logs are being flushed to disk faster than they are being sent, the potential data loss can increase indefinitely.  
  
 Also, it is useful to check the two performance objects `SQL Server:Availability Replica > Flow Control Time (ms/sec)` and `SQL Server:Availability Replica > Flow Control/sec`. Multiplying these two values shows you in the last second how much time was spent waiting for flow control to clear. The longer the flow control wait time, the lower the send rate.  
  
 The following metrics are useful in diagnosing network latency and throughput. You can use other Windows tools, such as **ping.exe** and [Network Monitor](https://www.microsoft.com/download/details.aspx?id=4865) to evaluate latency and network utilization.  
  
-   DMV `sys.dm_hadr_database_replica_states, log_send_queue_size`  
  
-   DMV `sys.dm_hadr_database_replica_states, log_send_rate`  
  
-   Performance counter `SQL Server:Database > Log Bytes Flushed/sec`  
  
-   Performance counter `SQL Server:Database Mirroring > Send/Receive Ack Time`  
  
-   Performance counter `SQL Server:Availability Replica > Bytes Sent to Replica/sec`  
  
-   Performance counter `SQL Server:Availability Replica > Bytes Sent to Transport/sec`  
  
-   Performance counter `SQL Server:Availability Replica > Flow Control Time (ms/sec)`  
  
-   Performance counter `SQL Server:Availability Replica > Flow Control/sec`  
  
-   Performance counter `SQL Server:Availability Replica > Resent Messages/sec`  

To remedy this issue, try upgrading your network bandwidth or removing or reducing unnecessary network traffic.  


##  <a name="BKMK_IO_BOTTLENECK"></a> Disk I/O bottleneck slows down log hardening on the secondary replica  
 Depending on the database file deployment, log hardening can slow down due to I/O contention with reporting workload.  
  
### Explanation  
 Data loss is prevented as soon as the log block is hardened on the log file. Therefore, it is critical to isolate the log file from the data file. If the log file and the data file are both mapped to the same hard disk, reporting workload with intensive reads on the data file will consume the same I/O resources needed by the log hardening operation. Slow log hardening can translate to slow acknowledgment to the primary replica, which can cause excessive activation of the flow control and long flow control wait times.  
  
### Diagnosis and resolution  
 If you have verified that the network is not suffering from high latency or low throughput, then you should investigate the secondary replica for I/O contentions. Queries from [SQL Server: Minimize Disk I/O](https://technet.microsoft.com/magazine/jj643251.aspx) are useful in identifying contentions. Examples from that article are derived below for your convenience.  
  
 The following script lets you see the number of reads and writes on each data and log file for every availability database running on an instance of SQL Server. It's sorted by average I/O stall time, in milliseconds. Note that the numbers are cumulative from the last time the server instance was started. Therefore, you should take the difference between two measurements after some time has elapsed.  
  
```sql  
SELECT DB_NAME(database_id) AS   
   [Database Name] ,   
   file_id ,   
   io_stall_read_ms ,   
   num_of_reads ,   
   CAST(io_stall_read_ms / ( 1.0 + num_of_reads ) AS NUMERIC(10, 1)) AS [avg_read_stall_ms] ,   
   io_stall_write_ms ,   
   num_of_writes ,  
   CAST(io_stall_write_ms / ( 1.0 + num_of_writes ) AS NUMERIC(10, 1)) AS [avg_write_stall_ms] ,   
   io_stall_read_ms + io_stall_write_ms AS [io_stalls] ,   
   num_of_reads + num_of_writes AS [total_io] ,   
   CAST(( io_stall_read_ms + io_stall_write_ms ) / ( 1.0 + num_of_reads  
+ num_of_writes) AS NUMERIC(10,1)) AS [avg_io_stall_ms]  
FROM sys.dm_io_virtual_file_stats(NULL, NULL)  
WHERE DB_NAME(database_id) IN (SELECT DISTINCT database_name FROM sys.dm_hadr_database_replica_cluster_states)  
ORDER BY avg_io_stall_ms DESC;  
```  
  
 This next query provides a point-in-time (not cumulative) snapshot of I/O requests pending on your system.  
  
```sql  
SELECT DB_NAME(mf.database_id) AS [Database] ,   
   mf.physical_name ,  
   r.io_pending ,   
   r.io_pending_ms_ticks ,   
   r.io_type ,   
   fs.num_of_reads ,   
   fs.num_of_writes  
FROM sys.dm_io_pending_io_requests AS r   
INNER JOIN sys.dm_io_virtual_file_stats(NULL, NULL) AS fs ON r.io_handle = fs.file_handle   
INNER JOIN sys.master_files AS mf ON fs.database_id = mf.database_id  
AND fs.file_id = mf.file_id  
ORDER BY r.io_pending , r.io_pending_ms_ticks DESC;  
```  
  
 You can compare how the read I/O and the write I/O match up with one another to identify I/O contention.  
  
 The following are some other performance counters that can help you in diagnosing I/O bottlenecks:  
  
-   **Physical Disk: all counters**  
  
-   **Physical Disk: Avg. Disk sec/Transfer**  
  
-   **SQL Server: Databases > Log Flush Wait Time**  
  
-   **SQL Server: Databases > Log Flush Waits/sec**  
  
-   **SQL Server: Databases > Log Pool Disk Reads/sec**  
  
 If you identify an I/O bottleneck and you have placed the log file and the data file on the same hard disk, the first thing you should do is to place the data file and the log file on separate disks. This best practice prevents reporting workload from interfering with the log transfer path from the primary replica to the log buffer and its ability to harden the transaction on the secondary replica.  
  
## Next steps  
 [Troubleshooting performance problems in SQL Server (applies to SQL Server 2012)](https://msdn.microsoft.com/library/dd672789(v=SQL.100).aspx)  
  
  
