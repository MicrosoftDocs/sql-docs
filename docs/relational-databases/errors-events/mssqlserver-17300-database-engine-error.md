---
description: "MSSQLSERVER_17300"
title: "MSSQLSERVER_17300 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17300 (Database Engine error)"
ms.assetid: c1d6bfb6-28af-4df6-8087-25807602d282
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17300
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |
| :-------- | :---- |
|Product Name|SQL Server|  
|Event ID|17300|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PROC_OUT_OF_SYSTASK_SESSIONS|  
|Message Text|SQL Server was unable to run a new system task, either because there is insufficient memory or the number of configured sessions exceeds the maximum allowed in the server. Verify that the server has adequate memory. Use sp_configure with option 'user connections' to check the maximum number of user connections allowed. Use sys.dm_exec_sessions to check the current number of sessions, including user processes.|  
  
## Explanation  
An attempt to run a new system task failed because of insufficient memory or because the number of configured sessions in the server was exceeded.  
  
## User Action  
Verify that the server has enough memory. Verify the current number of system tasks by using sys.dm_exec_sessions, and verify the configured value of maximum user connections by using sp_configure.  
  
Perform the following tasks as appropriate:  
  
-   Add more memory to the server.  
  
-   Terminate one or more sessions.  
  
-   Increase the maximum number of user connections allowed on the server.  
  
## See Also  
[sp_configure &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
[Server Configuration Options &#40;SQL Server&#41;](~/database-engine/configure-windows/server-configuration-options-sql-server.md)  
[sys.dm_exec_sessions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
[Configure the user connections Server Configuration Option](~/database-engine/configure-windows/configure-the-user-connections-server-configuration-option.md)  
[KILL &#40;Transact-SQL&#41;](~/t-sql/language-elements/kill-transact-sql.md)  
  
