---
title: "Replay Requirements | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "event classes [SQL Server], replaying traces"
  - "traces [SQL Server], replaying"
  - "replaying traces"
  - "TSQL_Replay template [SQL Server]"
ms.assetid: 0e01dfc7-84b9-47f6-8bf7-b0656df4fa7d
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Replay Requirements
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  In order to replay trace data with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] or the Distributed Replay Utility, a specific set of event classes and columns must be captured in the trace. These settings are enabled by default if the **TSQL_Replay** trace template is used to configure a trace that is later used for replay. This topic describes these settings and other replay requirements.  
  
> [!NOTE]  
>  We recommend using the Distributed Replay Utility for replaying an intensive OLTP application (with many active concurrent connections or high throughput). The Distributed Replay Utility can replay trace data from multiple computers, better simulating a mission-critical workload. For more information, see [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md).  
  
## Event Classes Required for Replay  
 To be replayed by [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], the following set of event classes, in addition to any other event classes you want to monitor, must be captured in the trace:  
  
-   **CursorClose (**only required when replaying server-side cursors)  
  
-   **CursorExecute** (only required when replaying server-side cursors)  
  
-   **CursorOpen** (only required when replaying server-side cursors)  
  
-   **CursorPrepare** (only required when replaying server-side cursors)  
  
-   **CursorUnprepare** (only required when replaying server-side cursors)  
  
-   **Audit Login**  
  
-   **Audit Logout**  
  
-   **ExistingConnection**  
  
-   **RPC Output Parameter**  
  
-   **RPC:Completed**  
  
-   **RPC:Starting**  
  
-   **Exec Prepared SQL** (only required when replaying server-side prepared SQL statements)  
  
-   **Prepare SQL** (only required when replaying server-side prepared SQL statements)  
  
-   **SQL:BatchCompleted**  
  
-   **SQL:BatchStarting**  
  
## Data Columns Required for Replay  
 In addition to any other data columns you want to capture, the following data columns must be captured in a trace to allow the trace to be replayed:  
  
-   **Event Class**  
  
-   **EventSequence**  
  
-   **TextData**  
  
-   **Application Name**  
  
-   **LoginName**  
  
-   **DatabaseName**  
  
-   **Database ID**  
  
-   **ClientProcessID**  
  
-   **HostName**  
  
-   **ServerName**  
  
-   **Binary Data**  
  
-   **SPID**  
  
-   **Start Time**  
  
-   **EndTime**  
  
-   **IsSystem**  
  
-   **NTDomainName**  
  
-   **NTUserName**  
  
-   **Error**  
  
> [!NOTE]  
>  Use the trace template **TSQL_Replay** for traces that capture data for replay.  
  
## Other Replay Requirements  
 In Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], replay checks for the presence of required events and columns. This change helps improve the accuracy of replay and takes the guesswork out of troubleshooting replay when required data is missing. Replay returns an error and stops replaying a file when required data is missing from a trace.  
  
 To replay a trace against a server (the target) on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running other than the server originally traced (the source), make sure the following has been done:  
  
-   All logins and users contained in the trace must be created already on the target and in the same database as the source.  
  
-   All logins and users in the target must have the same permissions they had in the source.  
  
-   All login passwords must be the same as those of the user that executes the replay.  
  
-   The database IDs on the target ideally should be the same as those on the source. However, if they are not the same, matching can be performed based on **DatabaseName** if it is present in the trace.  
  
-   The default database for each login contained in the trace must be set (on the target) to the respective target database of the login. For example, the trace to be replayed contains activity for the login, **Fred**, in the database **Fred_Db** on the source. Therefore, on the target, the default database for the login, **Fred**, must be set to the database that matches **Fred_Db** (even if the database name is different). To set the default database of the login, use the **sp_defaultdb** system stored procedure.  
  
 Replaying events associated with missing or incorrect logins results in replay errors, but the replay operation continues.  
  
 For information about what permissions are required to replay a trace, see [Permissions Required to Run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md).  
  
## See Also  
 [Replay a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-table-sql-server-profiler.md)   
 [Replay a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-file-sql-server-profiler.md)   
 [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md)   
 [sp_defaultdb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-defaultdb-transact-sql.md)   
 [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)  
  
  
