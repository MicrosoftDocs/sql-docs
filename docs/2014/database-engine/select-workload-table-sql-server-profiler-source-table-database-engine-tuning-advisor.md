---
title: "SQL Server Profiler - Source Table-Database Engine Tuning Advisor - Select Workload Table | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.pro.replay.tools.sourcetable.f1"
helpviewer_keywords: 
  - "Select Workload Table dialog box"
  - "Source Table dialog box"
ms.assetid: 51185be7-7092-480a-a52c-cf7786c4a0a0
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Profiler - Source Table-Database Engine Tuning Advisor - Select Workload Table
  Microsoft [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] and [!INCLUDE[ssDE](../includes/ssde-md.md)] Tuning Advisor use this dialog box to select tables.  
  
 In [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)], use the **Source Table** dialog box to specify a source table for a trace table. This is a table from which a trace is loaded, and the contents of which are viewed or used for replaying the trace.  
  
 In [!INCLUDE[ssDE](../includes/ssde-md.md)] Tuning Advisor, use the **Select Workload Table** dialog box to select a database table that contains [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] trace information to use as a tuning workload, or to preview the table contents before starting tuning analysis.  
  
## Options  
 **SQL Server**  
 Specifies the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] currently connected. This field is populated automatically and cannot be updated.  
  
 **Database**  
 Specify the database where the trace table is located.  
  
 **Owner**  
 Specifies the owner of the trace table. This field is populated automatically as **dbo**.  
  
 **Table**  
 Specify the name of the trace table from which the trace should be read.  
  
## See Also  
 [Save Trace Results to a Table &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/save-trace-results-to-a-table-sql-server-profiler.md)   
 [SQL Server Profiler Templates and Permissions](../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [Database Engine Tuning Advisor](../relational-databases/performance/database-engine-tuning-advisor.md)  
  
  
