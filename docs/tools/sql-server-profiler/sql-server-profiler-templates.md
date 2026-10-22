---
title: Templates
titleSuffix: SQL Server Profiler
description: Learn about the predefined templates SQL Server Profiler provides and how to use them. See how to create user-defined templates and change the default template.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: concept-article
ms.collection:
  - data-tools
---

# SQL Server Profiler Templates

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

You can use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to create templates that define the event classes and data columns to include in traces. After you define and save the template, you can run a trace that records the data for each event class you selected. You can use a template on many traces; the template isn't itself executed.

[!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] offers predefined trace templates that allow you to easily configure the event classes that you'll most likely need for specific traces. The Standard template, for example, helps you to create a generic trace for recording logins, logouts, batches completed, and connection information. You can use this template to run traces without modification or as a starting point for additional templates with different event configurations.

In addition to traces from predefined templates, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] also allows you to create them from a blank template, containing no event classes by default. Using the blank trace template can be useful when a planned trace doesn't resemble the configurations of any of the predefined templates.

[!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] can trace a variety of server types. For example you can trace [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. However, the event classes that can be included aren't the same for each type of server. Therefore, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] maintains different templates for different servers, and makes available the specific template that matches the selected server type.

## Predefined Templates

In addition to the Standard (default) template, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] includes several predefined templates for monitoring certain types of events. The following table lists the predefined templates, their purpose, and the event classes for which they capture information.

| Template name | Template purpose | Event classes |
| --- | --- | --- |
| `SP_Counts` | Captures stored procedure execution behavior over time. | **SP:Starting** |
| Standard | Generic starting point for creating a trace. Captures all stored procedures and [!INCLUDE [tsql](../../includes/tsql-md.md)] batches that are run. Use to monitor general database server activity. | **Audit Login**<br /><br />**Audit Logout**<br />**ExistingConnection**<br />**RPC:Completed**<br />**SQL:BatchCompleted**<br />**SQL:BatchStarting** |
| TSQL | Captures all [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that are submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by clients and the time issued. Use to debug client applications. | **Audit Login**<br /><br />**Audit Logout**<br />**ExistingConnection**<br />**RPC:Starting**<br />**SQL:BatchStarting** |
| TSQL_Duration | Captures all [!INCLUDE [tsql](../../includes/tsql-md.md)] statements submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by clients, their execution time (in milliseconds), and groups them by duration. Use to identify slow queries. | **RPC:Completed**<br /><br />**SQL:BatchCompleted** |
| TSQL_Grouped | Captures all [!INCLUDE [tsql](../../includes/tsql-md.md)] statements submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and the time they were issued. Groups information by user or client that submitted the statement. Use to investigate queries from a particular client or user. | **Audit Login**<br /><br />**Audit Logout**<br />**ExistingConnection**<br />**RPC:Starting**<br />**SQL:BatchStarting** |
| TSQL_Locks | Captures all of the [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that are submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by clients along with exceptional lock events. Use to troubleshoot deadlocks, lock time-out, and lock escalation events. | **Blocked Process Report**<br /><br />**SP:StmtCompleted**<br />**SP:StmtStarting**<br />**SQL:StmtCompleted**<br />**SQL:StmtStarting**<br />**Deadlock Graph**<br />**Lock:Cancel**<br />**Lock:Deadlock**<br />**Lock:Deadlock Chain**<br />**Lock:Escalation**<br />**Lock:Timeout (timeout>0)** |
| TSQL_Replay | Captures detailed information about [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that is required if the trace will be replayed. Use to perform iterative tuning, such as benchmark testing. | **CursorClose**<br /><br />**CursorExecute**<br />**CursorOpen**<br />**CursorPrepare**<br />**CursorUnprepare**<br />**Audit Login**<br />**Audit Logout**<br />**Existing Connection**<br />**RPC Output Parameter**<br />**RPC:Completed**<br />**RPC:Starting**<br />**Exec Prepared SQL**<br />**Prepare SQL**<br />**SQL:BatchCompleted**<br />**SQL:BatchStarting** |
| TSQL_SPs | Captures detailed information about all executing stored procedures. Use to analyze the component steps of stored procedures. Add the **SP:Recompile** event if you suspect that procedures are being recompiled. | **Audit Login**<br /><br />**Audit Logout**<br />**ExistingConnection**<br />**RPC:Starting**<br />**SP:Completed**<br />**SP:Starting**<br />**SP:StmtStarting**<br />**SQL:BatchStarting** |
| Tuning | Captures information about stored procedures and [!INCLUDE [tsql](../../includes/tsql-md.md)] batch execution. Use to produce trace output that [!INCLUDE [ssDE](../../includes/ssde-md.md)] Tuning Advisor can use as a workload to tune databases. | **RPC:Completed**<br /><br />**SP:StmtCompleted**<br />**SQL:BatchCompleted** |

For information about the event classes, see [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md).

## Default Template

[!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] automatically designates the **Standard** template as the default template applied to any new trace. However you can change the default template to any other predefined or user-defined template. To change the default template, select the **Use as a default template for selected server type** check box when you create or edit a template by using the **General** tab of the **Trace Template Properties** dialog box.

To navigate to the **Trace Template Properties** dialog box, on the [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu, choose **Templates**, and then select **New Template** or **Edit Template**.

The default template is specific for a given server type. Changing the default for one server type doesn't affect the default template for any other server type. For more information about setting a default template for a specific server, see [Set trace definition defaults (SQL Server Profiler)](set-trace-definition-defaults-sql-server-profiler.md).

## Related content

- [Create a trace template (SQL Server Profiler)](create-a-trace-template-sql-server-profiler.md)
- [Modify trace templates](modify-trace-templates.md)
- [Export a trace template (SQL Server Profiler)](export-a-trace-template-sql-server-profiler.md)
- [Import a trace template (SQL Server Profiler)](import-a-trace-template-sql-server-profiler.md)
