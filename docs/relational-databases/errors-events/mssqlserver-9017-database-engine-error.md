---
description: "MSSQLSERVER_9017"
title: "MSSQLSERVER_9017 | Microsoft Docs"
ms.custom: ""
ms.date: "08/18/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "9017 (Database Engine error)"
author: pijocoder
ms.author: jopilov
---
# MSSQLSERVER_9017
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|9017|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LOG_MANY_VLFS|  
|Message Text|Database %ls has more than %d virtual log files which is excessive. Too many virtual log files can cause long startup and backup times. Consider shrinking the log and using a different growth increment to reduce the number of virtual log files.|  
  
## Explanation

During a database startup, SQL Server detects that a database has a large number of [virtual log files](../sql-server-transaction-log-architecture-and-management-guide.md#virtual-log-files-vlfs) (VLFs) and logs this error message. The situations where you can encounter the error are:

- When you start an instance of SQL Server
- Restore a database
- Attach a database

The 9017 informational message that is similar to this example is logged in the SQL Server error log:

  `Database dbName has more than n virtual log files which is excessive. Too many virtual log files can cause long startup and backup times. Consider shrinking the log and using a different growth increment to reduce the number of virtual log files. Too many virtual log files can adversely affect the recovery time of the database.`

Additionally, if you use Replication, Database Mirroring, or AlwaysOn technologies in your environment, you may notice performance issues with these technologies.

### The effect of many VLFs on replication

Too many log files can affect replication because the log reader process must scan every virtual log file for transactions that are marked for replication. You can see this behavior by tracing the performance of the sp_replcmds stored procedure. The log reader process uses the sp_replcmds stored procedure to scan the virtual log files and to read the transactions that are marked for replication.

## Cause

This problem occurs when you specify small values for the FILEGROWTH parameter for your transaction log file(s).

The SQL Server Database Engine internally divides each physical log file into several virtual log files (VLFs). SQL Server 2008 R2 Service Pack 2 introduced a new message (9017) that is logged when a database starts (either because of the starting of an instance of SQL Server or because of the attaching or restoring of the database) and has more than 1,000 VLFs in SQL Server 2008 R2 or has more than 10,000 VLFS in SQL Server 2012 and later versions.

>[!NOTE]
>In SQL Server 2012, although this message is logged when the database has 10,000 VLFs, the actual message that is reported in the error log incorrectly states "1000 VLF." The warning does occur after 10,000 VLFs. However, the message reports 1,000 VLFs. This issue is corrected in later releases.

## User action

To resolve this problem, follow these steps:

1. You can view the VLF count and average size on your SQL Server by using this query. The result will help you identify which databases to focus on:

   ```sql
   SELECT db.name, count(dbl.database_id) as Total_VLF_count, convert(decimal (10,2), avg(dbl.vlf_size_mb)) as Avg_VLF_Size_MB
   FROM sys.databases db
    CROSS APPLY sys.dm_db_log_info(db.database_id) dbl
   GROUP BY db.name
   ORDER BY Total_VLF_count DESC
   ```

   For more information, see [sys.dm_db_log_info](../system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md).

1. Reduce your transaction log by using DBCC SHRINKDB/DBCC SHRINKFILE or by using SQL Server Management Studio.

1. Perform a one-time increase of the transaction log file size to a large value. This one-time increase is done to avoid frequent auto growths. For more information, see [Manage the size of the transaction log file](../logs/manage-the-size-of-the-transaction-log-file.md#AddOrEnlarge).

1. Increase the FILEGROWTH parameter to a larger value than what is currently configured. This should be based on the activity of your database and how frequently your log file is growing.

1. Additionally, you can review the following fix articles, depending on the version of SQL Server that you're currently running:


   [FIX: It takes a long time to restore a database in SQL Server 2008 R2, SQL Server 2008, or SQL Server 2012](https://support.microsoft.com/topic/kb2653893-fix-it-takes-a-long-time-to-restore-a-database-in-sql-server-2008-r2-or-in-sql-server-2008-or-in-sql-2012-e7b381d3-c169-d385-75c4-f43df87029d6)

   [FIX: Slow performance when you recover a database if there are many VLFs inside the transaction log in SQL Server 2005, SQL Server 2008, or SQL Server 2008 R2](https://support.microsoft.com/topic/kb2455009-fix-slow-performance-when-you-recover-a-database-if-there-are-many-vlfs-inside-the-transaction-log-in-sql-server-2005-in-sql-server-2008-or-in-sql-server-2008-r2-b6b6261a-4bfa-d4c6-868a-66696531d325)

   [FIX: Recovery takes longer than expected for a database in a SQL Server 2008 or SQL Server 2008 R2 environment](https://support.microsoft.com/topic/kb2524743-fix-recovery-takes-longer-than-expected-for-a-database-in-a-sql-server-2008-or-in-a-sql-server-2008-r2-environment-24be1262-2642-7e09-353d-f97cd632445a)


> [!TIP]
> To determine the optimal VLF distribution for the current transaction log size of all databases in a given instance, and the required growth increments to achieve the required size, see this [script](https://github.com/Microsoft/tigertoolbox/tree/master/Fixing-VLFs).
