---
title: Replay a Transact-SQL Script
titleSuffix: SQL Server Profiler
description: Discover how to use SQL Server Profiler to replay Transact-SQL scripts so that you can compare different possible solutions to a performance problem.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: 9c0eb222-e6e3-4bc1-a25f-a41e962d361b
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Replay a Transact-SQL Script (SQL Server Profiler)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

When you test possible solutions to a performance problem, use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to replay [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, and compare performance before and after your changes.  
  
### To replay a Transact-SQL script  
  
1.  On the **File** menu, point to **Open**, and then click **Script File**.  
  
2.  Select the [!INCLUDE[tsql](../../includes/tsql-md.md)] script file you want to open. Make sure that the [!INCLUDE[tsql](../../includes/tsql-md.md)] script contains events necessary for replay. For more information, see [Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md).  
  
3.  On the **Replay** menu, click **Start**, and connect to the server where you want to replay the script.  
  
4.  In the **Replay Configuration** dialog box, verify the settings, and then click **OK**.  
  
## See Also  
 [Replay Traces](../../tools/sql-server-profiler/replay-traces.md)   
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
