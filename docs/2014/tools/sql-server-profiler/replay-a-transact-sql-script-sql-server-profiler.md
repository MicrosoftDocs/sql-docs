---
title: "Replay a Transact-SQL Script (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "traces [SQL Server], replaying"
  - "scripts [SQL Server], traces"
  - "replaying traces"
ms.assetid: 9c0eb222-e6e3-4bc1-a25f-a41e962d361b
author: stevestein
ms.author: sstein
manager: craigg
---
# Replay a Transact-SQL Script (SQL Server Profiler)
  When you test possible solutions to a performance problem, use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to replay [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, and compare performance before and after your changes.  
  
### To replay a Transact-SQL script  
  
1.  On the **File** menu, point to **Open**, and then click **Script File**.  
  
2.  Select the [!INCLUDE[tsql](../../includes/tsql-md.md)] script file you want to open. Make sure that the [!INCLUDE[tsql](../../includes/tsql-md.md)] script contains events necessary for replay. For more information, see [Replay Requirements](replay-requirements.md).  
  
3.  On the **Replay** menu, click **Start**, and connect to the server where you want to replay the script.  
  
4.  In the **Replay Configuration** dialog box, verify the settings, and then click **OK**.  
  
## See Also  
 [Replay Traces](replay-traces.md)   
 [SQL Server Profiler](sql-server-profiler.md)  
  
  
