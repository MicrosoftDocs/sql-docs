---
title: "Distributed Replay Requirements | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 6fffee7d-891f-4d9d-b2c3-dd19855a1c2c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Distributed Replay Requirements
  Before using the [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay feature, consider the product requirements that are outlined in this topic.  
  
## Input Trace Requirements  
 To successfully replay trace data, it must meet the requirements for version and format, and contain the required events and columns.  
  
### Input Trace Versions  
 Distributed Replay supports input trace data that is collected on the following versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
-   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
-   [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]  
  
### Input Trace Formats  
 The input trace data can be in any of the following formats:  
  
-   A single trace file that has the `.trc` extension.  
  
-   A set of rollover trace files that follow the file rollover naming convention, for example: `<TraceFile>.trc`, `<TraceFile>_1.trc`, `<TraceFile>_2.trc`, `<TraceFile>_3.trc`, ... `<TraceFile>_n.trc`.  
  
### Input Trace Events and Columns  
 The input trace data must contain specific events and columns to be replayed by Distributed Replay. The **TSQL_Replay** template in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] contains all of the required events and columns, in addition to extra information. For more information about that template, see [Replay Requirements](../sql-server-profiler/replay-requirements.md).  
  
> [!WARNING]  
>  If you do not use the **TSQL_Replay** template to capture the input trace data, or if the input trace requirements are not satisfied, you may receive unexpected replay results.  
  
 You can also create a custom trace template and use it to replay events with Distributed Replay, as long as it contains the following events:  
  
-   Audit Login  
  
-   Audit Logout  
  
-   ExistingConnection  
  
-   RPC Output Parameter  
  
-   RPC:Completed  
  
-   RPC:Starting  
  
-   SQL:BatchCompleted  
  
-   SQL:BatchStarting  
  
 If you are replaying server-side cursors, the following events are also required:  
  
-   CursorClose  
  
-   CursorExecute  
  
-   CursorOpen  
  
-   CursorPrepare  
  
-   CursorUnprepare  
  
 If you are replaying server-side prepared SQL statements, the following events are also required:  
  
-   Exec Prepared SQL  
  
-   Prepare SQL  
  
 All input trace data must contain the following columns:  
  
-   Event Class  
  
-   EventSequence  
  
-   TextData  
  
-   Application Name  
  
-   LoginName  
  
-   DatabaseName  
  
-   Database ID  
  
-   HostName  
  
-   Binary Data  
  
-   SPID  
  
-   Start Time  
  
-   EndTime  
  
-   IsSystem  
  
## Supported Input Trace and Target Server Combinations  
 The following table lists the supported versions of trace data, and for each, the supported versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that data can be replayed against.  
  
|Version of Input Trace Data|Supported Versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the Target Server Instance|  
|---------------------------------|------------------------------------------------------------------------------------|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
  
## Operating System Requirements  
 Supported operating systems for running the administration tool and the controller and client services is the same as your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information about which operating systems are supported for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, see [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
 Distributed Replay features are supported on both x86-based and x64-based operating systems. For x64-based operating systems, only Windows on Windows (WOW) mode is supported.  
  
## Installation Limitations  
 Any one computer can only have a single instance of each Distributed Replay feature installed. The following table lists how many installations of each feature are allowed in a single Distributed Replay environment.  
  
|Distributed Replay Feature|Maximum Installations Per Replay Environment|  
|--------------------------------|--------------------------------------------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay controller service|1|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay client service|16 (physical or virtual computers)|  
|Administration tool|Unlimited|  
  
> [!NOTE]  
>  Although only one instance of the administration tool can be installed on a single computer, you can start multiple instances of the administration tool. Commands issued from multiple administration tools are resolved in the order in which they are received.  
  
## Data Access Provider  
 Distributed Replay only supports the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC data access provider.  
  
## Target Server Preparation Requirements  
 We recommend that the target server be located in a test environment. To replay trace data against a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] than it was originally recorded, make sure that the following has been done to the target server:  
  
-   All logins and users that are contained in the trace data must be present in the same database on the target server.  
  
-   All logins and users on the target server must have the same permissions they had on the original server.  
  
-   The database IDs on the target ideally should be the same as those on the source. However, if they are not the same, matching can be performed based on **DatabaseName** if it is present in the trace.  
  
-   The default database for each login that is contained in the trace data must be set (on the target server) to the respective target database of the login. For example, the trace data to be replayed contains activity for the login, **Fred**, in the database **Fred_Db** on the original instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, on the target server, the default database for the login, **Fred**, must be set to the database that matches **Fred_Db** (even if the database name is different). To set the default database of the login, use the `sp_defaultdb` system stored procedure.  
  
 Replaying events associated with missing or incorrect logins results in replay errors, but the replay operation continues.  
  
## See Also  
 [SQL Server Distributed Replay](sql-server-distributed-replay.md)   
 [Distributed Replay Security](distributed-replay-security.md)   
 [Install Distributed Replay](install-distributed-replay-overview.md)  
  
  
