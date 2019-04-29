---
title: "sp_trace_create (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_trace_create_TSQL"
  - "sp_trace_create"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_trace_create"
ms.assetid: f3a43597-4c5a-4520-bcab-becdbbf81d2e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_trace_create (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a trace definition. The new trace will be in a stopped state.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_trace_create [ @traceid = ] trace_id OUTPUT   
          , [ @options = ] option_value   
          , [ @tracefile = ] 'trace_file'   
     [ , [ @maxfilesize = ] max_file_size ]  
     [ , [ @stoptime = ] 'stop_time' ]  
     [ , [ @filecount = ] 'max_rollover_files' ]  
```  
  
## Arguments  
`[ @traceid = ] trace_id`
 Is the number assigned by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the new trace. Any user-provided input will be ignored. *trace_id* is **int**, with a default of NULL. The user employs the *trace_id* value to identify, modify, and control the trace defined by this stored procedure.  
  
`[ @options = ] option_value`
 Specifies the options set for the trace. *option_value* is **int**, with no default. Users may choose a combination of these options by specifying the sum value of options picked. For example, to turn on both the options TRACE_FILE_ROLLOVER and SHUTDOWN_ON_ERROR, specify **6** for *option_value*.  
  
 The following table lists the options, descriptions, and their values.  
  
|Option name|Option value|Description|  
|-----------------|------------------|-----------------|  
|TRACE_FILE_ROLLOVER|**2**|Specifies that when the *max_file_size* is reached, the current trace file is closed and a new file is created. All new records will be written to the new file. The new file will have the same name as the previous file, but an integer will be appended to indicate its sequence. For example, if the original trace file is named filename.trc, the next trace file is named filename_1.trc, the following trace file is filename_2.trc, and so on.<br /><br /> As more rollover trace files are created, the integer value appended to the file name increases sequentially.<br /><br /> SQL Server uses the default value of *max_file_size* (5 MB) if this option is specified without specifying a value for *max_file_size*.|  
|SHUTDOWN_ON_ERROR|**4**|Specifies that if the trace cannot be written to the file for whatever reason, SQL Server shuts down. This option is useful when performing security audit traces.|  
|TRACE_PRODUCE_BLACKBOX|**8**|Specifies that a record of the last 5 MB of trace information produced by the server will be saved by the server. TRACE_PRODUCE_BLACKBOX is incompatible with all other options.|  
  
`[ @tracefile = ] 'trace_file'`
 Specifies the location and file name to which the trace will be written. *trace_file* is **nvarchar(245)** with no default. *trace_file* can be either a local directory (such as N 'C:\MSSQL\Trace\trace.trc') or a UNC to a share or path (N'\\\\*Servername*\\*Sharename*\\*Directory*\trace.trc').  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will append a **.trc** extension to all trace file names. If the TRACE_FILE_ROLLOVER option and a *max_file_size* are specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates a new trace file when the original trace file grows to its maximum size. The new file has the same name as the original file, but _*n* is appended to indicate its sequence, starting with **1**. For example, if the first trace file is named **filename.trc**, the second trace file is named **filename_1.trc**.  
  
 If you use the TRACE_FILE_ROLLOVER option, we recommend that you do not use underscore characters in the original trace file name. If you do use underscores, the following behavior occurs:  
  
-   [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] does not automatically load or prompt you to load the rollover files (if either of these file rollover options are configured).  
  
-   The fn_trace_gettable function does not load rollover files (when specified by using the *number_files* argument) where the original file name ends with an underscore and a numeric value. (This does not apply to the underscore and number that are automatically appended when a file rolls over.)  
  
> [!NOTE]  
>  As a workaround for both of these behaviors, you can rename the files to remove the underscores in the original file name. For example, if the original file is named **my_trace.trc**, and the rollover file is named **my_trace_1.trc**, you can rename the files to **mytrace.trc** and **mytrace_1.trc** before you open the files in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
 *trace_file* cannot be specified when the TRACE_PRODUCE_BLACKBOX option is used.  
  
`[ @maxfilesize = ] max_file_size`
 Specifies the maximum size in megabytes (MB) a trace file can grow. *max_file_size* is **bigint**, with a default value of **5**.  
  
 If this parameter is specified without the TRACE_FILE_ROLLOVER option, the trace stops recording to the file when the disk space used exceeds the amount specified by *max_file_size*.  
  
`[ @stoptime = ] 'stop_time'`
 Specifies the date and time the trace will be stopped. *stop_time* is **datetime**, with a default of NULL. If NULL, the trace runs until it is manually stopped or until the server shuts down.  
  
 If both *stop_time* and *max_file_size* are specified, and TRACE_FILE_ROLLOVER is not specified, the trace tops when either the specified stop time or maximum file size is reached. If *stop_time*, *max_file_size*, and TRACE_FILE_ROLLOVER are specified, the trace stops at the specified stop time, assuming the trace does not fill up the drive.  
  
`[ @filecount = ] 'max_rollover_files'`
 Specifies the maximum number or trace files to be maintained with the same base filename. *max_rollover_files* is **int**, greater than one. This parameter is valid only if the TRACE_FILE_ROLLOVER option is specified. When *max_rollover_files* is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to maintain no more than *max_rollover_files* trace files by deleting the oldest trace file before opening a new trace file. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tracks the age of trace files by appending a number to the base file name.  
  
 For example, when the *trace_file* parameter is specified as "c:\mytrace", a file with the name "c:\mytrace_123.trc" is older than a file with the name "c:\mytrace_124.trc". If *max_rollover_files* is set to 2, then SQL Server deletes the file "c:\mytrace_123.trc" before creating the trace file "c:\mytrace_125.trc".  
  
 Notice that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only tries to delete each file once, and cannot delete a file that is in use by another process. Therefore, if another application is working with trace files while the trace is running, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may leave these trace files in the file system.  
  
## Return Code Values  
 The following table describes the code values that users may get following completion of the stored procedure.  
  
|Return code|Description|  
|-----------------|-----------------|  
|0|No error.|  
|1|Unknown error.|  
|10|Invalid options. Returned when options specified are incompatible.|  
|12|File not created.|  
|13|Out of memory. Returned when there is not enough memory to perform the specified action.|  
|14|Invalid stop time. Returned when the stop time specified has already happened.|  
|15|Invalid parameters. Returned when the user supplied incompatible parameters.|  
  
## Remarks  
 **sp_trace_create** is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that performs many of the actions previously executed by **xp_trace_\*** extended stored procedures available in earlier versions of SQL Server. Use **sp_trace_create** instead of:  
  
-   **xp_trace_addnewqueue**  
  
-   **xp_trace_setqueuecreateinfo**  
  
-   **xp_trace_setqueuedestination**  
  
 **sp_trace_create** only creates a trace definition. This stored procedure cannot be used to start or change a trace.  
  
 Parameters of all SQL Trace stored procedures (**sp_trace_xx**) are strictly typed. If these parameters are not called with the correct input parameter data types, as specified in the argument description, the stored procedure will return an error.  
  
 For **sp_trace_create**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must have Write permission on the trace file folder. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account is not an administrator on the computer where the trace file is located, you must explicitly grant Write permission to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account.  
  
> [!NOTE]  
>  You can automatically load the trace file created with **sp_trace_create** into a table by using the **fn_trace_gettable** system function. For information about how to use this system function, see [sys.fn_trace_gettable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-gettable-transact-sql.md).  
  
 For an example of using trace stored procedures, see [Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md).  
  
 **TRACE_PRODUCE_BLACKBOX** has the following characteristics:  
  
-   It is a rollover trace. The default *file_count* is 2 but can be overridden by the user using *filecount* option.  
  
-   The default *file_size* as with other traces is 5 MB and can be changed.  
  
-   No filename can be specified. The file will be saved as: **N'%SQLDIR%\MSSQL\DATA\blackbox.trc'**  
  
-   Only the following events and their columns are contained in the trace:  
  
    -   **RPC starting**  
  
    -   **Batch starting**  
  
    -   **Exception**  
  
    -   **Attention**  
  
-   Events or columns cannot be added or removed from this trace.  
  
-   Filters cannot be specified for this trace.  
  
## Permissions  
 User must have ALTER TRACE permission.  
  
## See Also  
 [sp_trace_generateevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md)   
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql.md)   
 [SQL Trace](../../relational-databases/sql-trace/sql-trace.md)  
  
  
