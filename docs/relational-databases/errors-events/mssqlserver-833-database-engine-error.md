---
title: "MSSQLSERVER_833"
description: "MSSQLSERVER_833"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "833 (Database Engine error)"
---
# MSSQLSERVER_833
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|833|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|BUF_LONG_IO|  
|Message Text|SQL Server has encountered %d occurrence(s) of I/O requests taking longer than %d seconds to complete on file [%ls] in database `[%ls] (%d)`.  The OS file handle is 0x%p.  The offset of the latest long I/O is: %#016I64x.|  
  
## Explanation  
This message indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has issued a read or write request from disk, and that the request has taken longer than 15 seconds to return. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reports this error and indicates a problem with the I/O subsystem. A database management system (DBMS), such as SQL Server, relies on the timeliness of file input and output (I/O) operations. Any one of the following items may cause stuck or stalled I/O operations and adversely affect SQL Server responsiveness and performance:

- Faulty hardware
- Incorrectly configured hardware
- Firmware settings
- Filter drivers
- Compression
- Bugs
- Other conditions in the I/O path

These I/O problems may cause the following behavior to occur:

- Blocking.
- Latch contention and time-outs.
- Slow response time.
- Stretching of resource boundaries.
- You may also notice other symptoms associated with this message, such as:
    - High wait times for PAGEIOLATCH waits.
    - Warnings or errors in the system event log.
    - Indications of disk latency issues in system monitor counters.

When an I/O operation has been pending for 15 seconds or longer, SQL Server performs the following steps:

1. Detects that an operation has been pending.
1. Writes an informational message to the SQL Server error log as outlined in the Details section.

   Explanation to different sections of this informational message is given in the following table:

| Message text | Description |
|---|---|
| <**Number**> occurrence(s) | The number of I/O requests that didn't complete the read or write operation in less than 15 seconds. |
| File information | The complete file name, database name, and database identification (DBID) number. |
| Handle | The operating system handle of the file. You can use the operating system handle with debuggers or other utilities to help track I/O request packet (IRP) requests. |
| Offset | The offset of the last stuck I/O operation or the last stalled I/O operation. You can use the offset with debuggers or other utilities to help track IRP requests. <br/><br/> **Note**: </br> When the informational message is written to the SQL Server error log, the I/O operation may no longer be stuck or stalled. |

### Possible causes

The informational message indicates that the current load may be experiencing one of the following conditions:

- The workload exceeds the I/O path capabilities either because of misconfiguration of the I/O subsystem (SAN, NAS, and direct attached) or because the hardware capacity has been reached.
- The workload exceeds the current system capabilities, such as I/O, CPUs, and HBAs.
- The I/O path has malfunctioning software. It could be firmware or a driver issue.
- The I/O path has malfunctioning hardware components.
- Performance issue at the operating system level.
- Filter driver intervention in the I/O process or storage path of database files. For example, antivirus program.

SQL Server records the time it initiated an I/O request and records the time the I/O was completed. If that difference is 15 seconds or longer, this condition is detected. It also means that SQL Server isn't the cause of the delayed I/O condition that this message describes and reports. This condition is known as stalled I/O. Most disk requests occur within the typical speed of the disk. This typical disk speed is frequently known as disk seek time. Disk seek time for most standard disks occurs in 10 milliseconds or less. Therefore, 15 seconds is a long time for the system I/O path to return to SQL Server. For more details, see the [More Information](#more-information) section.
  
## User action

Troubleshoot this error by performing the following steps:

1. Examine the system event log for hardware-related error messages.
1. Examine hardware-specific logs if they are available. Use the necessary methods and techniques to determine the cause of the delay in the operating system, the drivers, or the I/O hardware.
1. Update all device drivers and firmware or perform other diagnostics that're associated with your I/O subsystem.
1. Disk access can be slowed by filter drivers, for example, an antivirus program. To increase access speed, exclude the SQL Server data files that're specified in the error message from the active virus scans. For more information, see [How to choose antivirus software to run on computers that are running SQL Server (microsoft.com)](https://support.microsoft.com/topic/how-to-choose-antivirus-software-to-run-on-computers-that-are-running-sql-server-feda079b-3e24-186b-945a-3051f6f3a95b).
    - Use the [fltmc.exe command line utility](/windows-hardware/drivers/ifs/development-and-testing-tools) to query all the filter drivers installed on the system and to understand the functions it performs on the storage path to the database files.
1. Use the Performance Monitor to examine the following counters:
    - **Average Disk Sec/Transfer**
    - **Average Disk Queue Length**
    - **Current Disk Queue Length**
1. You can also use facilities like [Storport ETW logging](/archive/blogs/ntdebugging/storport-etw-logging-to-measure-requests-made-to-a-disk-unit) to measure the latency of requests that're made to a disk unit. Another similar disk I/O troubleshooting kit is available as a built-in profile of [Windows Performance Recorder](/windows-hardware/test/wpt/introduction-to-wpr).
1. Monitor [sys.dm_io_virtual_file_stats](../system-dynamic-management-views/sys-dm-io-virtual-file-stats-transact-sql.md) and choose the appropriate storage tier and IOPS for your storage throughput.

For a guided walk-through for diagnosing and troubleshooting SQL Server performance issues that occur because of I/O issues, see [Troubleshoot slow SQL Server performance caused by I/O issues](/troubleshoot/sql/performance/troubleshoot-sql-io-performance).

## More information

### Stuck I/O and Stalled I/O

**Stuck I/O**

Stuck I/O is defined as an I/O request that doesn't complete. Frequently, stuck I/O indicates a stuck IRP. To resolve a stuck I/O condition, you must usually restart the computer or perform a similar action. A stuck I/O condition typically indicates one of the following issues:
- Faulty hardware.
- A bug in an I/O path component.

**Stalled I/O**

Stalled I/O is defined as an I/O request that does complete, or that takes excessive time to complete. Stalled I/O behavior typically occurs because of one of the following reasons:

- Hardware configuration.
- Firmware settings.
- A filter driver issue that requires assistance from the hardware or the software vendor to trace and resolve.

### SQL Server stalled I/O and stuck I/O recording and reporting

SQL Server Support handles many cases each year that involve stuck or stalled I/O problems. These I/O issues appear in different ways. I/O issues are some of the most difficult to diagnose and debug, and they require significant time and resources for debugging from Microsoft and the customer. The reporting and recording of I/O requests are designed on a per-file basis. The detection and reporting of stalled and stuck I/O requests are two separate actions.

**Recording**

There are two moments when a record action occurs in SQL Server. The first is when the I/O operation completes.  The second moment is when the lazy writer runs. When the lazy writer runs, it checks all the pending data and pending log file I/O requests. If the I/O request exceeds the 15 seconds threshold, a record operation occurs.

**Reporting**

Reporting occurs in intervals that are five minutes or more apart. Reporting occurs when the next I/O request is made on the file. If a record action has occurred and five minutes or more have passed since the last report occurred, the informational message that is mentioned in the Details section is written to the SQL Server error log.

The 15 seconds threshold isn't adjustable. However, you can disable stalled or stuck I/O detection by using trace flag 830, although we don't recommend doing this.

You can disable detection for stalled and stuck I/O by using trace flag 830. To enable this flag every time that SQL Server is started, use the -T830 startup parameter. To disable detection for an instance of SQL Server that is currently running, use the following statement:
```sql
    dbcc traceon(830, -1)
```

This setting is effective only for the life of the SQL Server process.

> [!NOTE]
> An I/O request that becomes stalled or stuck is reported only one time. For example, if the message reports that 10 I/O requests are stalled, those 10 reports won't occur again. If the next message reports that 15 I/O requests are stalled, it means that 15 new I/O requests have become stalled.

### Tracking the I/O request packet (IRP)

SQL Server uses the standard Microsoft Windows API calls to read and write data. For example, SQL Server uses the following functions:

- WriteFile
- ReadFile
- WriteFileScatter
- ReadFileGather

The read or write request is handled by Windows as an I/O request packet (IRP). To determine the state of the IRP, use both of the following features::

- Windows support
- [Storport ETW logging](/archive/blogs/ntdebugging/storport-etw-logging-to-measure-requests-made-to-a-disk-unit)

We recommend that you check for any available updates for the following items:

- The BIOS
- The firmware
- Any other I/O path components

Contact your hardware vendors before you perform additional debugging actions. The debug session will likely involve a third-party driver, firmware, or filter driver component.

### System performance and query plan actions

Overall, system performance can play a key role in I/O processing. You should consider the general health of the system while investigating reports of stalled or stuck I/O operations. Excessive loads can cause the overall system to be slow, including the I/O processing. The behavior of the system when the problem occurs can be a key factor in determining the root cause of the problem. For example, if the CPU usage increases or remains high while the problem occurs, it may indicate that a system process is using so much CPU that other processes are being adversely affected.

**Performance counters**

To monitor I/O performance, examine the following performance counters for specific I/O path information:

- **Average Disk Sec/Transfer**
- **Average Disk Queue Length**
- **Current Disk Queue Length**

For example, the Average Disk Sec/Transfer time on a computer that's running SQL Server is typically less than 15 milliseconds. If the Average Disk Sec/Transfer value climbs, it indicates that the I/O subsystem isn't optimally keeping up with the I/O demand.

Be careful while using the performance counters because SQL Server takes full advantage of asynchronous I/O capabilities that heavily push the disk queue lengths. Therefore, longer disk queue lengths alone don't indicate a problem.

In Windows System Monitor, you can review the counter "Physical Disk: Disk Bytes/sec" for each affected disk and compare the rate of activity against the counters "Process: IO Data Bytes/Sec" and "Process: IO Other Bytes/sec" for each process. You do this to identify whether a specific set of processes is generating excessive I/O requests. Various other I/O-related counters in the Process object reveal more granular information. If you determine that a SQL Server instance is responsible for excessive I/O load on the server, see the next section on Indexes and Parallelism. For a detailed discussion on detecting and resolving I/O bottlenecks, see [Troubleshoot slow SQL Server performance caused by I/O issues](/troubleshoot/sql/performance/troubleshoot-sql-io-performance).

**Indexes and parallelism**

Frequently, bursts of I/O occur because an index is missing. This behavior can severely push the I/O path. A pass that uses the Index Turning Wizard (ITW) may help resolve I/O pressure on the system. If a query benefits from an index instead of a table scan, or perhaps if it uses a sort or hash, the system can gain the following advantages:

- A reduction is made in the physical I/O that's required to complete the action that directly creates performance benefits for the query.
- Fewer pages in the data cache must be turned over. Therefore, those pages that're in the data cache remain relevant to active queries.
- Sorts and hashes are used because an index may be missing or because statistics are out of date. You may reduce tempdb use and contention by adding one or more indexes.
- A reduction is made in resources, parallel operations, or both. Because SQL Server does not guarantee parallel query execution, and  the load on the system is considered, it's best to optimize all queries for serial execution. To optimize a query, open Query Analyzer and set the sp_configure value of the max degree of parallelism option to **1**. If all the queries are tuned to run promptly as a serial operation, parallel execution is often just a better result. However, parallel execution is often selected because the amount of data is large. For a missing index, a large sort may have to occur. Multiple workers that are performing the sort operation will create a quicker response. However, this action can dramatically increase the pressure on the system. Large read requests from many workers can cause an I/O burst together with increased CPU usage. A query can often be tuned to run faster and use fewer resources if an index is added or if another tuning action occurs.

### Practical examples from SQL Server Support

The following examples have been handled by SQL Server Support and Windows Escalation Support. These examples are intended to provide a frame of reference and help set your expectations about stalled and stuck I/O situations. They also provide a framework for understanding how a system may be affected or may respond. No specific hardware or set of drivers pose any specific risk or increased risk over another. All systems are the same in this respect.

**Example 1: A log write that is stuck for 45 seconds**

An attempt to write a SQL Server log file periodically gets stuck for approximately 45 seconds. The log write doesn't complete in a timely manner. This behavior creates a blocking condition that causes 30 seconds client time-outs.

The application submitted a commit to SQL Server, and the commit gets stuck as a log write pending. This behavior causes the query to continue holding locks and block incoming requests from other clients. Then, other clients start to time out. This compounds the problem because the application doesn't roll back open transactions when a query time-out occurs. This creates hundreds of open transactions that are holding locks. Therefore, a severe blocking situation occurs.

For more information about transaction handling and blocking, see the following Microsoft Knowledge Base article:
[224453 Understanding and resolving SQL Server blocking problems](https://support.microsoft.com/help/224453)

The application services a website by using connection pooling. As more connections become blocked, the website creates more connections. These connections become blocked, and the cycle continues.

The log write takes approximately 45 seconds to complete. However, by this time, hundreds of connections are backed up. The blocking problems cause several minutes of recovery time for SQL Server and the application. Combined with application problems, the stalled I/O condition has a very negative effect on the system.

**Resolution**

The problem is tracked to a stuck I/O request in a Host Bus Adapter (HBA) driver. The computer has multiple HBA cards with failover support. When one HBA is behind or isn't communicating with the Storage Area Network (SAN), the "retry before failover" time-out value is configured to 45 seconds. When the time-out exceeds, the I/O request is routed to the second HBA. The second HBA handles the request and quickly gets completed. To help prevent such stall conditions, the hardware manufacturer recommends a "retry before failover" setting of five seconds.

**Example 2: Filter driver intervention**

Many antivirus software programs and backup products use I/O filter drivers. These I/O filter drivers become part of the I/O request stack and have access to the IRP request. Microsoft Product Support Services has seen various issues from bugs that create stuck I/O conditions or stalled I/O conditions in a filter driver implementation.

One such condition is a filter driver for backup processing that allows backup of the files that are open when the backup occurs. The system administrator has included the SQL Server data file directory in the file backup selections. When the backup occurs, the backup tries to gather the correct image of the file at the time backup started. Doing this delays I/O requests. The I/O requests are allowed to complete only one at a time as the software handles them.

When the backup starts, SQL Server performance drops dramatically as the I/Os of SQL Server are forced to complete one at a time. The one at a time logic is such that the I/O operation can't be performed asynchronously, which compounds the issue. Therefore, when SQL Server expects to post an I/O request and continue, the worker is stuck in the read or write call until the I/O request is completed. The actions of the filter driver effectively disable the processing tasks such as SQL Server read-ahead. Additionally, another bug in the filter driver leaves the one at a time actions in the process, even when the backup is completed. The only way to restore SQL Server performance is to restart SQL Server so that the file handle is released and reacquired without the filter driver interaction.

**Resolution**

To resolve this problem, the SQL Server data files are removed from the file backup process. The software manufacturer has corrected the problem that left the file in the "one at a time" mode.

**Example 3: Hidden errors**

Many high-end systems have multichannel I/O paths to handle load balancing or similar activities. Microsoft Product Support has found problems with the load balancing software where an I/O request fails, but the software doesn't handle the error condition correctly. The software can attempt infinite retries. The I/O operation gets stuck, and SQL Server can't complete the specified action. Much like the log write condition described earlier, many poor system behaviors can occur after such a condition wedges the system.

**Resolution**

To resolve this problem, restart the SQL Server. However, sometimes you need to restart the operating system to restore processing. We also recommend that you obtain a software update from the I/O vendor.

**Example 4: Remote storage, mirroring, and raid drives**

Many systems use mirroring or adopt similar steps to prevent data loss. Some systems that use mirroring are software-based, and some are hardware-based. The situation that is typically discovered by Microsoft Support for these systems is increased latency.

An increase in the overall I/O time occurs when the I/O must finish before it's considered complete. For remote mirror installations, network retries can become involved. When drive failures occur, and the raid system is being rebuilt, the I/O pattern can also be interrupted.

**Resolution**

Strict configuration settings are required to reduce latency to mirrors or to raid rebuild operations.

**Example 5: Compression**

Microsoft doesn't support SQL Server data files and log files on compressed drives. NTFS compression isn't safe for SQL Server because NTFS compression breaks Write Ahead Logging (WAL) protocol. NTFS compression also requires increased processing for each I/O operation. Compression creates "one at a time" like behavior that causes severe performance issues to occur.

**Resolution**

To resolve this problem, uncompress the data and the log files.

For more information, see [Support for databases on compressed volumes](/troubleshoot/sql/admin/support-databases-compressed-volumes).

### Additional data points

PAGEIOLATCH_* and writelog waits in sys.dm_os_wait_stats dynamic management views (DMV) are key indicators to investigate I/O path performance. If you see significant PAGEIOLATCH waits, it means that SQL Server is waiting on the I/O subsystem. A certain amount of PAGEIOLATCH waits is typical and expected behavior. However, if the average PAGEIOLATCH wait times are consistently greater than 10 milliseconds, you should investigate why the I/O subsystem is under pressure. For more information, see the following documents:

- [Troubleshoot slow SQL Server performance caused by I/O issues](/troubleshoot/sql/performance/troubleshoot-sql-io-performance)
- [sys.dm_os_waiting_tasks (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-waiting-tasks-transact-sql.md)
- [sys.dm_exec_requests](https://msdn.microsoft.com/library/ms177648.aspx)
- [The SQL Server Wait Type Repository](https://blogs.msdn.com/b/psssql/archive/2009/11/03/the-sql-server-wait-type-repository.aspx)

**References**

- [Use DISKSPD to test workload storage performance](/azure-stack/hci/manage/diskspd-overview)
- [826433 SQL Server diagnostics added to detect unreported I/O problems due to stale reads or lost writes](https://support.microsoft.com/help/826433)
- [Logging and data storage algorithms](/troubleshoot/sql/admin/logging-data-storage-algorithms)

SQL Server requires that systems support "guaranteed delivery to stable media" as outlined under the [SQL Server I/O Reliability Program Requirements](https://download.microsoft.com/download/f/1/e/f1ecc20c-85ee-4d73-baba-f87200e8dbc2/sql_server_io_reliability_program_review_requirements.pdf). For more information about the input and output requirements for the SQL Server database engine, visit [Database Engine Input/Output requirements](/troubleshoot/sql/admin/database-engine-input-output-requirements).

For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)).
