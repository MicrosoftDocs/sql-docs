---
title: "SQL Server Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine analysis [Upgrade Advisor]"
  - "SQL Server Upgrade Advisor, Database Engine"
  - "Upgrade Advisor [SQL Server], Database Engine"
  - "analyzing system [Upgrade Advisor], Database Engine"
ms.assetid: 44a18bfe-e593-47a5-995f-382c01d3f618
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Parameters
  On this page, set parameters that the analyzer will use for [!INCLUDE[ssDE](../../includes/ssde-md.md)] analysis. You can analyze one, several, or all databases, analyze trace files that were created by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], and analyze SQL batch files.  
  
> [!NOTE]  
>  Some upgrade issues can be detected only if you submit trace files or SQL batch files to be analyzed.  
  
## Options  
 **Database(s) to analyze**  
 To analyze all databases, select the **All databases** check box. To analyze a selection of databases, select the check box next to each database to include in the scan.  
  
 **Analyze trace file(s)**  
 Select this check box to analyze trace files in the file system.  
  
 **Path to trace file(s)**  
 You can analyze one or more files. You can browse to a location and select multiple files, or you can provide multiple file names. Use the full path name to each file, include the file name, and separate entries by using the pipe character (|).  
  
 If you enable **Analyze trace file(s)**, **Next** is disabled until you enter a path name and a file name.  
  
 **Analyze [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] batch file(s)**  
 Select this check box to analyze [!INCLUDE[tsql](../../includes/tsql-md.md)] batch files in the file system.  
  
 **Path to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] batch file(s)**  
 You can analyze one or more batch files. You can browse to a location and select multiple files, or you can type multiple file names. Use the full path name to each file, include the file name, and separate entries by using the pipe character (|).  
  
 If you enable **Analyze SQL batch file(s)**, the **Next** button is disabled until you enter a path name and a file name.  
  
 **SQL Batch Separator**  
 The text that is used to separate batches of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. The default value is **GO**.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)   
 [Upgrade Advisor User Interface Reference](../../../2014/sql-server/install/upgrade-advisor-user-interface-reference.md)  
  
  
