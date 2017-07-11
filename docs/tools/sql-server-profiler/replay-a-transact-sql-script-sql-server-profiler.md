---
title: "Replay a Transact-SQL Script (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "traces [SQL Server], replaying"
  - "scripts [SQL Server], traces"
  - "replaying traces"
ms.assetid: 9c0eb222-e6e3-4bc1-a25f-a41e962d361b
caps.latest.revision: 24
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Replay a Transact-SQL Script (SQL Server Profiler)
  When you test possible solutions to a performance problem, use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to replay [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, and compare performance before and after your changes.  
  
### To replay a Transact-SQL script  
  
1.  On the **File** menu, point to **Open**, and then click **Script File**.  
  
2.  Select the [!INCLUDE[tsql](../../includes/tsql-md.md)] script file you want to open. Make sure that the [!INCLUDE[tsql](../../includes/tsql-md.md)] script contains events necessary for replay. For more information, see [Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md).  
  
3.  On the **Replay** menu, click **Start**, and connect to the server where you want to replay the script.  
  
4.  In the **Replay Configuration** dialog box, verify the settings, and then click **OK**.  
  
## See Also  
 [Replay Traces](../../tools/sql-server-profiler/replay-traces.md)   
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  