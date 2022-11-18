---
description: "Limit Trace File and Table Sizes"
title: "Limit Trace File and Table Sizes | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "maximum file size for traces"
  - "traces [SQL Server], file size"
  - "size [SQL Server], tables"
  - "file rollover option [SQL Server]"
  - "size [SQL Server], files"
ms.assetid: 88c31b02-f44c-4a14-be8b-437f2097de12
author: "MashaMSFT"
ms.author: "mathoma"
---
# Limit Trace File and Table Sizes
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  SQL Trace results vary in size depending on the event classes that are included in the trace and the way in which the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is used. If you trace event classes that occur frequently, you can minimize the amount of data that the trace collects by setting the maximum file size or the maximum number of rows. By specifying the maximum file size or rows, you ensure that the trace file or table will not grow beyond the specified limit.  
  
> [!NOTE]  
>  If you save trace data to a file that already exists, you can append data to the file or overwrite the file. If you choose to append data to the file, and the trace file already meets or exceeds the specified maximum file size, you are notified and given the opportunity either to increase the maximum file size or specify a new file. The same is true for trace tables.  
  
## Maximum File Size  
 A trace that has a maximum file size stops saving trace information to the file after the maximum file size has been reached. This option allows you to group events into smaller, more manageable files. In addition, limiting file size makes it safer to run unattended traces, because the trace stops when the maximum file size is reached. You can set the maximum file size for traces created by means of Transact-SQL stored procedures or by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
 There is an upper limit of 1 gigabyte (GB) for the maximum file size option. The default maximum file size is 5 megabytes (MB).  
  
### Enabling File Rollover  
 The file rollover option causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to close the current file and create a new file when the maximum file size is reached. The new file has the same name as the previous file, but an integer is appended to the name to indicate its sequence. For example, if the original trace file is named filename_1.trc, the next trace file is filename_2.trc, and so on. If the name assigned to a new rollover file is already used by an existing file, the existing file is overwritten unless it is read only. The file rollover option is enabled by default when you are saving trace data to a file.  
  
> [!NOTE]  
>  With the file rollover option on, the trace continues until it is stopped by some other means. To stop the trace after you have reached the file size limit, disable the file rollover option.  
  
 **To set a maximum file size for a trace file**  
  
 [Set a Maximum File Size for a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-a-maximum-file-size-for-a-trace-file-sql-server-profiler.md)  
  
## Maximum Number of Rows  
 A trace with a maximum number of rows stops saving trace information to a table after the maximum number of rows has been reached. Each event constitutes one row, so this parameter sets a limit on the number of events that are gathered. Setting the maximum number of rows makes it easier to run unattended traces. For example, if you need to start a trace that saves trace data to a table, but you want to stop the trace if the table becomes too large, you can do so automatically.  
  
 When the maximum number of rows is specified and the maximum number of rows has been reached, the trace continues to run while [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is running, but the trace information is no longer recorded. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] continues to display the trace results until the trace stops.  
  
 **To set a maximum number of rows for a trace**  
  
 [Set a Maximum Table Size for a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-a-maximum-table-size-for-a-trace-table-sql-server-profiler.md)  
  
## See Also  
 [sp_trace_create &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md)  
  
  
