---
title: Considerations for Replaying Traces
titleSuffix: SQL Server Profiler
description: Find out which operations, stored procedures, templates, and log activities prevent SQL Server Profiler from replaying traces.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: 73fa339f-b71a-4be4-97ca-d4ae84c8b90b
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/06/2017
---

# Considerations for Replaying Traces (SQL Server Profiler)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] cannot replay the following kinds of traces:  
  
-   Traces that contain transactional replication and other transaction log activity. These events are skipped. Other types of replication do not mark the transaction log so they are not affected.  
  
-   Traces that contain operations that involve globally unique identifiers (GUID). These events will be skipped.  
  
-   Traces that contain operations on **text**, **ntext**, and **image** columns involving the **bcp** utility, the BULK INSERT, READTEXT, WRITETEXT, and UPDATETEXT statements, and full-text operations. These events are skipped.  
  
-   Traces that contain session binding: **sp_getbindtoken** and **sp_bindsession** system stored procedures. These events are skipped.  
  
> [!NOTE]  
>  If you do not use the preconfigured replay template (**TSQL_Replay**), and do not capture all required data, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] does not replay the trace. For more information, see [Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md).  
  
 For information about what permissions are required to replay a trace, see [Permissions Required to Run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md).  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md)   
 [sp_getbindtoken &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-getbindtoken-transact-sql.md)   
 [sp_bindsession &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindsession-transact-sql.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [READTEXT &#40;Transact-SQL&#41;](../../t-sql/queries/readtext-transact-sql.md)   
 [WRITETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/writetext-transact-sql.md)   
 [UPDATETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/updatetext-transact-sql.md)  
  
  
