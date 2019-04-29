---
title: "Tracing and Replaying Events | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "replaying events"
  - "traces [SMO]"
  - "events [SMO], replaying"
  - "events [SMO], tracing"
ms.assetid: f41b3f85-2f6c-4c3e-9776-8c73d2cc7a53
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Tracing and Replaying Events
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  In SMO, the **Trace** and **Replay** objects in the <xref:Microsoft.SqlServer.Management.Trace> namespace provide programmatic access to the [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] functionality, which is used for monitoring an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. You can capture and save data about each event to a file or table to analyze later. For example, you can monitor a production environment to see which procedures are impeding performance by executing too slowly.  
  
 The **Trace** and **Replay** objects provide a set of objects that can be used to create traces on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. These objects can be used from within your own applications to create traces manually for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. Additionally, SMO **Trace** objects can be used to read SQL Trace files and tables that were created by monitoring [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], or DTS logging.  
  
 SMO **Trace** objects let you perform the following functions:  
  
-   Create a trace.  
  
-   Set filters on the trace.  
  
-   Set the events that are being traced.  
  
-   Stop or start a trace.  
  
-   Read trace files, and trace tables.  
  
-   Get information about events on a trace.  
  
-   Get information about filters on a trace.  
  
-   Manipulate trace data programmatically.  
  
-   Write trace tables and trace files.  
  
-   Replay trace files or trace tables.  
  
 The trace data from the **Trace** and **Replay** objects can be used by the SMO application, or it can be examined manually by using [SQL Server Profiler](../../../tools/sql-server-profiler/sql-server-profiler.md). The trace data is also compatible with the [SQL Trace](../../../relational-databases/sql-trace/sql-trace.md) stored procedures that also provide tracing capabilities.  
  
 The SMO trace objects reside in the <xref:Microsoft.SqlServer.Management.Trace> namespace, which requires a reference to the Microsoft.SQLServer.ConnectionInfo.dll file.  
  
 The **Trace** and **Replay** objects require a [ServerConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.serverconnection.aspx)<xref:Microsoft.SqlServer.Management.Smo.Server.%23ctor%2A> object to establish a connection with the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The [ServerConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.serverconnection.aspx) object resides in the [Microsoft.SqlServer.Management.Common](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common) namespace, which requires a reference to the Microsoft.SQLServer.ConnectionInfo.dll file.  
  
> [!NOTE]  
>  The **Trace** and **Replay** objects are not supported on a 64-bit platform.  
  
  
