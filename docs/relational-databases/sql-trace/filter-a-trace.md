---
title: Filter a trace
description: Learn how to use filters limit the events collected in a trace.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 07/17/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "filters [SQL Server], events"
  - "events [SQL Server], filters"
  - "filters [SQL Server]"
  - "filters [SQL Server], traces"
  - "traces [SQL Server], filters"
---
# Filter a trace

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Filters limit the events collected in a trace. If a filter isn't set, all events of the selected event classes are returned in the trace output. For example, limiting the Windows user names in a trace to specific users reduces the output data to those users only.

It isn't mandatory to set a filter for a trace. However, a filter minimizes the overhead that is incurred during a trace. A filter returns focused data and thus makes performance analysis and audits easier.

To filter the event data captured within a trace, select trace event criteria that return only relevant data from the trace. For example, you can include or exclude monitoring the activity of a specific application from the trace.

> [!NOTE]  
> When [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] creates traces, it filters out its own activity by default.

As another example, if you monitor queries to determine the batches that take the longest time to execute, set the trace event criteria to monitor only those batches that take longer than 30 seconds to execute (a CPU minimum value of 30,000 milliseconds).

## Filter creation guidelines

In general, follow these steps to filter a trace.

1. Identify the events that you want to include in the trace.
1. Identify the data and data columns that contain the information you need.
1. Identify a subset of the data you need and define filters based on that subset of data.

For example, you might be interested only in events that take longer than a certain length of time. You could create a trace that includes events where the `Duration` data column is greater than 300 milliseconds. Your trace doesn't include events that finish in less than 300 milliseconds.

You can create filters by using SQL Server Profiler or Transact-SQL stored procedures.

### Filter events in a trace template

- [Filter events in a trace (SQL Server Profiler)](../../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md)
- [Set a trace filter (Transact-SQL)](set-a-trace-filter-transact-sql.md)

### Modify filters

- [Modify a filter (SQL Server Profiler)](../../tools/sql-server-profiler/modify-a-filter-sql-server-profiler.md)

Filter availability depends on the data column. Some data columns can't be filtered. The data columns that can be filtered are filterable only by certain relational operators, as shown in the following table.

| Relational operator | Operator symbol | Description |
| --- | --- | --- |
| **Like** | `LIKE` | Specifies that the trace event data must be like the text entered. Allows multiple values. |
| **Not like** | `NOT LIKE` | Specifies that the trace event data must not be like the text entered. Allows multiple values. |
| **Equals** | `=` | Specifies that the trace event data must equal the value entered. Allows multiple values. |
| **Not equal to** | `<>` | Specifies that the trace event data must not equal the value entered. Allows multiple values. |
| **Greater than** | `>` | Specifies that the trace event data must be greater than the value entered. |
| **Greater than or equal to** | `>=` | Specifies that the trace event data must be greater than or equal to the value entered. |
| **Less than** | `<` | Specifies that the trace event data must be less than the value entered. |
| **Less than or equal to** | `<=` | Specifies that the trace event data must be less than or equal to the value entered. |

The following table lists the filterable data columns and the available relational operators.

| Data columns | Relational operators |
| --- | --- |
| `ApplicationName` | `LIKE`, `NOT LIKE` |
| `BigintData1` | `=`, `<>`, `>=`, `<=` |
| `BigintData2` | `=`, `<>`, `>=`, `<=` |
| `BinaryData` | Use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to filter events in this data column. For more information, see [Filter traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md). |
| `ClientProcessID` | `=`, `<>`, `>=`, `<=` |
| `ColumnPermissions` | `=`, `<>`, `>=`, `<=` |
| `CPU` | `=`, `<>`, `>=`, `<=` |
| `DatabaseID` | `=`, `<>`, `>=`, `<=` |
| `DatabaseName` | `LIKE`, `NOT LIKE` |
| `DBUserName` | `LIKE`, `NOT LIKE` |
| `Duration` | `=`, `<>`, `>=`, `<=` |
| `EndTime` | `>=`, `<=` |
| `Error` | `=`, `<>`, `>=`, `<=` |
| `EventSubClass` | `=`, `<>`, `>=`, `<=` |
| `FileName` | `LIKE`, `NOT LIKE` |
| `GUID` | Use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to filter events in this data column. For more information, see [Filter traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md). |
| `Handle` | `=`, `<>`, `>=`, `<=` |
| `HostName` | `LIKE`, `NOT LIKE` |
| `IndexID` | `=`, `<>`, `>=`, `<=` |
| `IntegerData` | `=`, `<>`, `>=`, `<=` |
| `IntegerData2` | `=`, `<>`, `>=`, `<=` |
| `IsSystem` | `=`, `<>`, `>=`, `<=` |
| `LineNumber` | `=`, `<>`, `>=`, `<=` |
| `LinkedServerName` | `LIKE`, `NOT LIKE` |
| `LoginName` | `LIKE`, `NOT LIKE` |
| `LoginSid` | Use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to filter events in this data column. For more information, see [Filter traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md). |
| `MethodName` | `LIKE`, `NOT LIKE` |
| `Mode` | `=`, `<>`, `>=`, `<=` |
| `NestLevel` | `=`, `<>`, `>=`, `<=` |
| `NTDomainName` | `LIKE`, `NOT LIKE` |
| `NTUserName` | `LIKE`, `NOT LIKE` |
| `ObjectID` | `=`, `<>`, `>=`, `<=` |
| `ObjectID2` | `=`, `<>`, `>=`, `<=` |
| `ObjectName` | `LIKE`, `NOT LIKE` |
| `ObjectType` | `=`, `<>`, `>=`, `<=` |
| `Offset` | `=`, `<>`, `>=`,`<=` |
| `OwnerID` | `=`, `<>`, `>=`,`<=` |
| `OwnerName` | `LIKE`, `NOT LIKE` |
| `ParentName` | `LIKE`, `NOT LIKE` |
| `Permissions` | `=`, `<>`, `>=`,`<=` |
| `ProviderName` | `LIKE`, `NOT LIKE` |
| `Reads` | `=`, `<>`, `>=`,`<=` |
| `RequestID` | `=`, `<>`, `>=`,`<=` |
| `RoleName` | `LIKE`, `NOT LIKE` |
| `RowCounts` | `=`, `<>`, `>=`,`<=` |
| `SessionLoginName` | `LIKE`, `NOT LIKE` |
| `Severity` | `=`, `<>`, `>=`,`<=` |
| `SourceDatabaseID` | `=`, `<>`, `>=`,`<=` |
| `SPID` | `=`, `<>`, `>=`, `<=` |
| `SqlHandle` | Use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to filter events in this data column. For more information, see [Filter traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md). |
| `StartTime` | `>=`,`<=` |
| `State` | `=`, `<>`, `>=`,`<=` |
| `Success` | `=`, `<>`, `>=`,`<=` |
| `TargetLoginName` | `LIKE`, `NOT LIKE` |
| `TargetLoginSid` | Use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to filter events in this data column. For more information, see [Filter traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md). |
| `TargetUserName` | `LIKE`, `NOT LIKE` |
| `TextData` <sup>1</sup> | `LIKE`, `NOT LIKE` |
| `TransactionID` | `=`, `<>`, `>=`,`<=` |
| `Type` | `=`, `<>`, `>=`,`<=` |
| `Writes` | `=`, `<>`, `>=`,`<=` |
| `XactSequence` | `=`, `<>`, `>=`,`<=` |

<sup>1</sup> If tracing events from the **osql** utility or the **sqlcmd** utility, always append `%` to filters on the `TextData` data column.

As a security precaution, SQL Trace automatically omits from the trace any information from security-related stored procedures that affect passwords. This security mechanism is nonconfigurable and is always in effect. It prevents users, who otherwise have permissions to trace all activity on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], from capturing passwords.

The following security-related stored procedures are monitored, but no output is written to the `TextData` data column:

- [sp_addapprole](../system-stored-procedures/sp-addapprole-transact-sql.md)
- [sp_adddistpublisher](../system-stored-procedures/sp-adddistpublisher-transact-sql.md)
- [sp_adddistributiondb](../system-stored-procedures/sp-adddistributiondb-transact-sql.md)
- [sp_adddistributor](../system-stored-procedures/sp-adddistributor-transact-sql.md)
- [sp_addlinkedserver](../system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sp_addlinkedsrvlogin](../system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md)
- [sp_addlogin](../system-stored-procedures/sp-addlogin-transact-sql.md)
- [sp_addmergepullsubscription_agent](../system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md)
- [sp_addpullsubscription_agent](../system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md)
- [sp_addremotelogin](../system-stored-procedures/sp-addremotelogin-transact-sql.md)
- [sp_addsubscriber](../system-stored-procedures/sp-addsubscriber-transact-sql.md)
- [sp_approlepassword](../system-stored-procedures/sp-approlepassword-transact-sql.md)
- [sp_changedistpublisher](../system-stored-procedures/sp-changedistpublisher-transact-sql.md)
- [sp_changesubscriber](../system-stored-procedures/sp-changesubscriber-transact-sql.md)
- [sp_dsninfo](../system-stored-procedures/sp-dsninfo-transact-sql.md)
- [sp_helpsubscription_properties](../system-stored-procedures/sp-helpsubscription-properties-transact-sql.md)
- [sp_link_publication](../system-stored-procedures/sp-link-publication-transact-sql.md)
- [sp_password](../system-stored-procedures/sp-password-transact-sql.md)
- [sp_setapprole](../system-stored-procedures/sp-setapprole-transact-sql.md)

## Related content

- [Filter events in a trace (SQL Server Profiler)](../../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md)
- [Set a trace filter (Transact-SQL)](set-a-trace-filter-transact-sql.md)
- [Modify a filter (SQL Server Profiler)](../../tools/sql-server-profiler/modify-a-filter-sql-server-profiler.md)
- [Filter traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md)
