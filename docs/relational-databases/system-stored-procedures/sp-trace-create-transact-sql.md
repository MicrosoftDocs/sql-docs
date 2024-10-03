---
title: "sp_trace_create (Transact-SQL)"
description: "Creates a trace definition. The new trace is in a stopped state."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_trace_create_TSQL"
  - "sp_trace_create"
helpviewer_keywords:
  - "sp_trace_create"
dev_langs:
  - "TSQL"
---
# sp_trace_create (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a trace definition. The new trace is in a stopped state.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_trace_create
    [ @traceid = ] traceid OUTPUT
    , [ @options = ] options
    , [ @tracefile = ] N'tracefile'
    [ , [ @maxfilesize = ] maxfilesize ]
    [ , [ @stoptime = ] 'stoptime' ]
    [ , [ @filecount = ] filecount ]
[ ; ]
```

## Arguments

#### [ @traceid = ] *traceid* OUTPUT

The number assigned by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to the new trace. Any user-provided input is ignored. *@traceid* is an OUTPUT parameter of type **int**, with a default of `NULL`. The user employs the *@traceid* value to identify, modify, and control the trace defined by this stored procedure.

#### [ @options = ] *options*

Specifies the options set for the trace. *@options* is **int**, with no default. You can choose a combination of these options by specifying the sum value of options picked. For example, to turn on both the options `TRACE_FILE_ROLLOVER` and `SHUTDOWN_ON_ERROR`, specify `6` for *@options*.

The following table lists the options, descriptions, and their values.

| Option name | Option value | Description |
| --- | --- | --- |
| `TRACE_FILE_ROLLOVER` | `2` | Specifies that when the *@filecount* is reached, the current trace file is closed, and a new file is created. All new records are written to the new file. The new file has the same name as the previous file, but an integer is appended to indicate its sequence. For example, if the original trace file is named `filename.trc`, the next trace file is named `filename_1.trc`, the following trace file is `filename_2.trc`, and so on.<br /><br />As more rollover trace files are created, the integer value appended to the file name increases sequentially.<br /><br />[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses the default value of *@filecount* (5 MB) if this option is specified without specifying a value for *@filecount*. |
| `SHUTDOWN_ON_ERROR` | `4` | Specifies that if the trace can't be written to the file for whatever reason, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] shuts down. This option is useful when performing security audit traces. |
| `TRACE_PRODUCE_BLACKBOX` | `8` | Specifies that a record of the last 5 MB of trace information produced by the server is saved by the server. `TRACE_PRODUCE_BLACKBOX` is incompatible with all other options. |

#### [ @tracefile = ] N'*tracefile*'

Specifies the location and file name to which the trace is written. *@tracefile* is **nvarchar(245)** with no default. *@tracefile* can be either a local directory (such as `N'C:\MSSQL\Trace\trace.trc'`) or a UNC to a share or path (such as `N'\\<servername>\<sharename>\<directory>\trace.trc'`).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] appends a `.trc` extension to all trace file names. If the `TRACE_FILE_ROLLOVER` option and a *@filecount* are specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] creates a new trace file when the original trace file grows to its maximum size. The new file has the same name as the original file, but _*n* is appended to indicate its sequence, starting with `1`. For example, if the first trace file is named `filename.trc`, the second trace file is named `filename_1.trc`.

If you use the `TRACE_FILE_ROLLOVER` option, we recommend that you don't use underscore characters in the original trace file name. If you do use underscores, the following behavior occurs:

- [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] doesn't automatically load or prompt you to load the rollover files (if either of these file rollover options are configured).

- The [sys.fn_trace_gettable](../system-functions/sys-fn-trace-gettable-transact-sql.md) function doesn't load rollover files (when specified by using the *@number_files* argument) where the original file name ends with an underscore and a numeric value. (This doesn't apply to the underscore and number that are automatically appended when a file rolls over.)

> [!NOTE]  
> As a workaround for both of these behaviors, you can rename the files to remove the underscores in the original file name. For example, if the original file is named `my_trace.trc`, and the rollover file is named `my_trace_1.trc`, you can rename the files to `mytrace.trc` and `mytrace_1.trc` before you open the files in [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

*@tracefile* can't be specified when the `TRACE_PRODUCE_BLACKBOX` option is used.

#### [ @maxfilesize = ] *maxfilesize*

Specifies the maximum size in megabytes (MB) a trace file can grow. *@maxfilesize* is **bigint**, with a default value of `5`.

If this parameter is specified without the `TRACE_FILE_ROLLOVER` option, the trace stops recording to the file when the disk space used exceeds the amount specified by *@maxfilesize*.

#### [ @stoptime = ] '*stoptime*'

Specifies the date and time the trace will be stopped. *@stoptime* is **datetime**, with a default of `NULL`. If `NULL`, the trace runs until it's manually stopped or until the server shuts down.

If both *@stoptime* and *@maxfilesize* are specified, and `TRACE_FILE_ROLLOVER` isn't specified, the trace tops when either the specified stop time or maximum file size is reached. If *@stoptime*, *@maxfilesize*, and `TRACE_FILE_ROLLOVER` are specified, the trace stops at the specified stop time, assuming the trace doesn't fill up the drive.

#### [ @filecount = ] '*filecount*'

Specifies the maximum number or trace files to be maintained with the same base filename. *@filecount* is **int**, greater than `1`. This parameter is valid only if the `TRACE_FILE_ROLLOVER` option is specified. When *@filecount* is specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tries to maintain no more than *@filecount* trace files by deleting the oldest trace file before opening a new trace file. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tracks the age of trace files by appending a number to the base file name.

For example, when the *@tracefile* parameter is specified as `C:\mytrace`, a file with the name `C:\mytrace_123.trc` is older than a file with the name `C:\mytrace_124.trc`. If *@filecount* is set to `2`, then [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deletes the file `C:\mytrace_123.trc` before creating the trace file `C:\mytrace_125.trc`.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] only tries to delete each file once, and can't delete a file that is in use by another process. Therefore, if another application is working with trace files while the trace is running, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might leave these trace files in the file system.

## Return code values

The following table describes the code values that you could get, following completion of the stored procedure.

| Return code | Description |
| --- | --- |
| `0` | No error. |
| `1` | Unknown error. |
| `10` | Invalid options. Returned when options specified are incompatible. |
| `12` | File not created. |
| `13` | Out of memory. Returned when there isn't enough memory to perform the specified action. |
| `14` | Invalid stop time. Returned when the stop time specified has already happened. |
| `15` | Invalid parameters. Returned when the user supplied incompatible parameters. |

## Remarks

`sp_trace_create` is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that performs many of the actions previously executed by `xp_trace_*` extended stored procedures available in earlier versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Use `sp_trace_create` instead of:

- `xp_trace_addnewqueue`
- `xp_trace_setqueuecreateinfo`
- `xp_trace_setqueuedestination`

`sp_trace_create` only creates a trace definition. This stored procedure can't be used to start or change a trace.

Parameters of all SQL Trace stored procedures (`sp_trace_*`) are strictly typed. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.

For `sp_trace_create`, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account must have *write* permission on the trace file folder. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account isn't an administrator on the computer where the trace file is located, you must explicitly grant write permission to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account.

> [!NOTE]  
> You can automatically load the trace file created with `sp_trace_create` into a table by using the `fn_trace_gettable` system function. For more information, see [sys.fn_trace_gettable](../system-functions/sys-fn-trace-gettable-transact-sql.md).

For an example of using trace stored procedures, see [Create a Trace](../sql-trace/create-a-trace-transact-sql.md).

`TRACE_PRODUCE_BLACKBOX` has the following characteristics:

- It's a rollover trace. The default *@filecount* is 2 but can be overridden by the user using *@filecount* option.

- The default *@maxfilesize*, as with other traces, is 5 MB and can be changed.

- No filename can be specified. The file is saved as: `N'%SQLDIR%\MSSQL\DATA\blackbox.trc'`.

- Only the following events and their columns are contained in the trace:

  - **RPC starting**
  - **Batch starting**
  - **Exception**
  - **Attention**

- Events or columns can't be added or removed from this trace.

- Filters can't be specified for this trace.

## Permissions

Requires ALTER TRACE permission.

## Related content

- [sp_trace_generateevent (Transact-SQL)](sp-trace-generateevent-transact-sql.md)
- [sp_trace_setevent (Transact-SQL)](sp-trace-setevent-transact-sql.md)
- [sp_trace_setfilter (Transact-SQL)](sp-trace-setfilter-transact-sql.md)
- [sp_trace_setstatus (Transact-SQL)](sp-trace-setstatus-transact-sql.md)
- [SQL Trace](../sql-trace/sql-trace.md)
