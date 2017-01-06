---
title: "KILL (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 47da1abe-a135-4fcf-9445-8f4a2076f9ea
caps.latest.revision: 20
author: BarbKess
---
# KILL (SQL Server PDW)
Terminates a SQL Server PDW user session based on the session ID. Use KILL, for example, to terminate a process that is blocking other important processes with locks, or to terminate a long-running query process that is using too many system resources.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
KILL 'session_id'  
[;]  
```  
  
## Arguments  
*session_id*  
The session ID of the SQL Server PDW process to terminate. *session_id* begins with the alphabetical characters 'SID'. These are case-sensitive and must be capitalized.  
  
*session_id* is an **nvarchar(32)** value that is assigned to each user connection when the connection is made. It persists for the duration of the connection. When the connection ends, *session_id* is released.  
  
## Limitations and Restrictions  
You cannot use KILL to kill your current session.  
  
## Metadata  
Examples of system views that return PDW session IDs:  
  
-   [sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md),  
  
    [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md)  
  
-   [sys.dm_pdw_query_stats_xe &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-query-stats-xe-sql-server-pdw.md)  
  
-   [sys.dm_pdw_query_stats_xe_file &#40;SQL Server PDW&#41;](../sqlpdw/sys.dm_pdw_query_stats_xe_file-sql-server-pdw.md)  
  
-   [sys.dm_pdw_resource_waits &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-resource-waits-sql-server-pdw.md)  
  
-   [sys.dm_pdw_waits &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-waits-sql-server-pdw.md)  
  
-   [sys.dm_pdw_lock_waits &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-lock-waits-sql-server-pdw.md)  
  
## Permissions  
Requires the **ALTER ANY CONNECTION** permission.  
  
## General Remarks  
Use KILL very carefully, especially when critical processes are running.  
  
## Examples  
The following example shows how to terminate session ID `SID535`.  
  
```  
KILL 'SID535';  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SESSION_ID &#40;SQL Server PDW&#41;](../sqlpdw/session-id-sql-server-pdw.md)  
  
