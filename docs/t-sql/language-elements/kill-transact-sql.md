---
title: "KILL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/31/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "KILL_TSQL"
  - "KILL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "WITH STATUSONLY option"
  - "terminating distributed transactions"
  - "distributed transactions [SQL Server], terminating"
  - "in-doubt transactions"
  - "IDs [SQL Server], user processes"
  - "ending distributed transactions [SQL Server]"
  - "stopping distributed transactions"
  - "session IDs [SQL Server]"
  - "system process termination [SQL Server]"
  - "stopping processes"
  - "ending processes [SQL Server]"
  - "UOW [SQL Server]"
  - "orphaned transactions [SQL Server]"
  - "unit of work [SQL Server]"
  - "process termination [SQL Server]"
  - "KILL statement"
  - "terminating process"
ms.assetid: 071cf260-c794-4b45-adc0-0e64097938c0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# KILL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Ends a user process that is based on the session ID or unit of work (UOW). If the specified session ID or UOW has much work to undo, the KILL statement may take some time to complete. The process takes longer to complete particularly when the process involves rolling back a long transaction.  
  
KILL ends a normal connection, which internally stops the transactions that are associated with the specified session ID. At times, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) might be in use. If MS DTC is in use, you can also use the statement to end orphaned and in-doubt distributed transactions.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server  
  
KILL { session ID | UOW } [ WITH STATUSONLY ]   
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
KILL 'session_id'  
[;]   
```  
  
## Arguments  
_session ID_  
Is the session ID of the process to end. _session ID_ is a unique integer (**int**) that is assigned to each user connection when the connection is made. The session ID value is tied to the connection for the duration of the connection. When the connection ends, the integer value is released and can be reassigned to a new connection.  
The following query can help you identify the `session_id` that you want to kill:  
 ```sql  
 SELECT conn.session_id, host_name, program_name,
     nt_domain, login_name, connect_time, last_request_end_time 
FROM sys.dm_exec_sessions AS sess
JOIN sys.dm_exec_connections AS conn
    ON sess.session_id = conn.session_id;
```  
  
_UOW_  
**Applies to**: ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
Identifies the Unit of Work ID (UOW) of distributed transactions. _UOW_ is a GUID that may be obtained from the request_owner_guid column of the sys.dm_tran_locks dynamic management view. _UOW_ also can be obtained from the error log or through the MS DTC monitor. For more information about monitoring distributed transactions, see the MS DTC documentation.  
  
Use KILL _UOW_ to stop orphaned distributed transactions. These transactions aren't associated with any real session ID, but instead are associated artificially with session ID = '-2'. This session ID makes it easier to identify orphaned transactions by querying the session ID column in sys.dm_tran_locks, sys.dm_exec_sessions, or sys.dm_exec_requests dynamic management views.  
  
WITH STATUSONLY  
Generates a progress report about a specified _session ID_ or _UOW_ that is being rolled back because of an earlier KILL statement. KILL WITH STATUSONLY doesn't end or roll back the _session ID_ or _UOW_. The command only displays the current progress of the rollback.  
  
## Remarks  
KILL is commonly used to end a process that is blocking other important processes with locks. KILL can also be used to stop a process that is executing a query that is using necessary system resources. System processes and processes running an extended stored procedure can't be ended.  
  
Use KILL carefully, especially when critical processes are running. You can't kill your own process. You also shouldn't kill the following processes:  
  
-   AWAITING COMMAND  
-   CHECKPOINT SLEEP  
-   LAZY WRITER  
-   LOCK MONITOR  
-   SIGNAL HANDLER  
  
Use @@SPID to display the session ID value for the current session.  
  
To obtain a report of active session ID values, query the session_id column of the sys.dm_tran_locks, sys.dm_exec_sessions, and sys.dm_exec_requests dynamic management views. You can also view the SPID column that the sp_who system stored procedure returns. If a rollback is in progress for a specific SPID, the cmd column in the sp_who result set for that SPID indicates KILLED/ROLLBACK.  
  
When a particular connection has a lock on a database resource and blocks the progress of another connection, the session ID of the blocking connection shows up in the `blocking_session_id` column of `sys.dm_exec_requests` or the `blk` column returned by `sp_who`.  
  
The KILL command can be used to resolve in-doubt distributed transactions. These transactions are unresolved distributed transactions that occur because of unplanned restarts of the database server or MS DTC coordinator. For more information about in-doubt transactions, see the "Two-Phase Commit" section in [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).  
  
## Using WITH STATUSONLY  
KILL WITH STATUSONLY generates a report if the session ID or UOW rolls back because of a previous KILL _session ID_|_UOW_ statement. The progress report states the amount of rollback completed (in percent) and the estimated length of time left (in seconds). The report states it in the following form:  
  
`Spid|UOW <xxx>: Transaction rollback in progress. Estimated rollback completion: <yy>% Estimated time left: <zz> seconds`  
  
If the rollback of the session ID or UOW finishes before the KILL _session ID_|_UOW_ WITH STATUSONLY statement runs, KILL _session ID_|_UOW_ WITH STATUSONLY returns the following error:  
  
```
"Msg 6120, Level 16, State 1, Line 1"  
"Status report cannot be obtained. Rollback operation for Process ID <session ID> is not in progress."
```  
This error also occurs if no session ID or UOW is being rolled back


The same status report can be obtained by repeating the same KILL _session ID_|_UOW_ statement without using the WITH STATUSONLY option. However, we don't recommend repeating the option this way. If you repeat a KILL _session ID_ statement, the new process might stop if the rollback finishes and the session ID is reassigned to a new task before the new KILL statement runs. Prevent the new process from stopping by specifying WITH STATUSONLY.  
  
## Permissions  
**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:** Requires the ALTER ANY CONNECTION permission. ALTER ANY CONNECTION is included with membership in the sysadmin or processadmin fixed server roles.  
  
**[!INCLUDE[ssSDS](../../includes/sssds-md.md)]:** Requires the KILL DATABASE CONNECTION permission. The server-level principal login has the KILL DATABASE CONNECTION.  
  
## Examples  
  
### A. Using KILL to stop a session  
 The following example shows how to stop session ID `53`.  
  
```sql  
KILL 53;  
GO  
```  
  
### B. Using KILL session ID WITH STATUSONLY to obtain a progress report  
The following example generates a status of the rollback process for the specific session ID.  
  
```sql  
KILL 54;  
KILL 54 WITH STATUSONLY;  
GO  
  
--This is the progress report.  
spid 54: Transaction rollback in progress. Estimated rollback completion: 80% Estimated time left: 10 seconds.  
```  
  
### C. Using KILL to stop an orphaned distributed transaction  
The following example shows how to stop an orphaned distributed transaction (session ID = -2) with a *UOW* of `D5499C66-E398-45CA-BF7E-DC9C194B48CF`.  
  
```sql  
KILL 'D5499C66-E398-45CA-BF7E-DC9C194B48CF';  
```  

  
## See Also  
[KILL STATS JOB &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-stats-job-transact-sql.md)   
[KILL QUERY NOTIFICATION SUBSCRIPTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-query-notification-subscription-transact-sql.md)   
[Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
[SHUTDOWN &#40;Transact-SQL&#41;](../../t-sql/language-elements/shutdown-transact-sql.md)   
[@@SPID &#40;Transact-SQL&#41;](../../t-sql/functions/spid-transact-sql.md)   
[sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
[sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)   
[sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)   
[sp_lock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lock-transact-sql.md)   
[sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)  
  
  
