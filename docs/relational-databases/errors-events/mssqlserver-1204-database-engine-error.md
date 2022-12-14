---
description: "MSSQLSERVER_1204"
title: "MSSQLSERVER_1204 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ramakoni
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1204 (Database Engine error)"
ms.assetid: de6ece78-79de-484d-9224-ca0f7645815f
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1204

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1204|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LK_OUTOF|  
|Message Text|The instance of the SQL Server Database Engine cannot obtain a LOCK resource at this time. Rerun your statement when there are fewer active users. Ask the database administrator to check the lock and memory configuration for this instance, or to check for long-running transactions.|  
  
## Explanation

During execution, queries frequently acquire and release locks on the resources they access. Acquiring a lock uses up the lock structures from an available pool of lock structures. When new locks can't be acquired because there are no more lock structures available in the pool, the error 1204 message is returned. This issue can be due to any of the following reasons:
  
- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't allocate more memory, either because other processes are using it, or because SQL Server has used up all of its memory and reached the value configured using the configuration option [max server memory](../../database-engine/configure-windows/server-memory-server-configuration-options.md#max_server_memory).  

  
- The lock manager won't use more than 60 percent of the memory available to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and the threshold has already been met.

- You have set up the configuration option [locks](../../database-engine/configure-windows/configure-the-locks-server-configuration-option.md) of the system stored procedure [sp_configure](../system-stored-procedures/sp-configure-transact-sql.md) to a non-default, non-dynamic value.

- You have enabled trace flags [1211](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf1211), [1224](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf1224), or both on your SQL Server to control lock escalation behavior, and you're executing queries that require many locks.
  
## User Action

- If you suspect that SQL Server can't allocate sufficient memory, try the following:  

  - Identify if any other memory clerk inside SQL Server has used up a large part of the SQL Server configured memory by using a query like the following one:

    ```sql
    SELECT pages_kb, type, name, virtual_memory_committed_kb, awe_allocated_kb
    
    FROM sys.dm_os_memory_clerks
    
    ORDER BY pages_kb DESC
    ```

    Then reduce the memory consumption of that memory clerk to allow for lock memory to use more resources. For more information, see [Troubleshoot out of memory or low memory issues in SQL Server](/troubleshoot/sql/performance/troubleshoot-memory-issues).

  - If applications besides SQL Server are consuming resources, try stopping these applications or consider running them on a separate server. This will release memory from other processes for SQL Server.

  - If you have configured **max server memory**, increase the **max server memory** setting.
  
- If you suspect that the lock manager has used the maximum amount of available memory, identify the transaction that is holding the most locks and terminate it. The following script will identify the transaction that has the most locks:  
  
    ```sql
    SELECT request_session_id, COUNT (*) num_locks  
    FROM sys.dm_tran_locks  
    GROUP BY request_session_id   
    ORDER BY count (*) DESC  
    ```  
  
    Take the highest session id, and terminate it by using the [KILL](../../t-sql/language-elements/kill-transact-sql.md) command.  

- If you're using a non-default value for `locks`, use `sp_configure` to change the value of `locks` to its default setting by using the following statement:

    ```sql
    EXEC sp_configure 'locks', 0
    ```

- If you encountered the above error message when using the SQL Server trace flags 1211, 1224, or both, review their use and disable them while executing queries that require a large number of locks. For more information, review [DBCC TRACEON - Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) and [Resolve blocking problems caused by lock escalation in SQL Server](/troubleshoot/sql/performance/resolve-blocking-problems-caused-lock-escalation).