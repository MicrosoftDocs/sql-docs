---
title: "sys.sysprocesses (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysprocesses_TSQL"
  - "sys.sysprocesses_TSQL"
  - "sys.sysprocesses"
  - "sysprocesses"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sysprocesses compatibility view"
  - "sysprocesses system table"
ms.assetid: 60a36d36-54b3-4bd6-9cac-702205a21b16
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.sysprocesses (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains information about processes that are running on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These processes can be client processes or system processes. To access sysprocesses, you must be in the master database context, or you must use the master.dbo.sysprocesses three-part name.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|spid|**smallint**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session ID.|  
|kpid|**smallint**|Windows thread ID.|  
|blocked|**smallint**|ID of the session that is blocking the request. If this column is NULL, the request is not blocked, or the session information of the blocking session is not available (or cannot be identified).<br /><br /> -2 = The blocking resource is owned by an orphaned distributed transaction.<br /><br /> -3 = The blocking resource is owned by a deferred recovery transaction.<br /><br /> -4 = Session ID of the blocking latch owner could not be determined due to internal latch state transitions.|  
|waittype|**binary(2)**|Reserved.|  
|waittime|**bigint**|Current wait time in milliseconds.<br /><br /> 0 = Process is not waiting.|  
|lastwaittype|**nchar(32)**|A string indicating the name of the last or current wait type.|  
|waitresource|**nchar(256)**|Textual representation of a lock resource.|  
|dbid|**smallint**|ID of the database currently being used by the process.|  
|uid|**smallint**|ID of the user that executed the command. Overflows or returns NULL if the number of users and roles exceeds 32,767.|  
|cpu|**int**|Cumulative CPU time for the process. The entry is updated for all processes, regardless of whether the SET STATISTICS TIME option is ON or OFF.|  
|physical_io|**bigint**|Cumulative disk reads and writes for the process.|  
|memusage|**int**|Number of pages in the procedure cache that are currently allocated to this process. A negative number indicates that the process is freeing memory allocated by another process.|  
|login_time|**datetime**|Time at which a client process logged into the server.|  
|last_batch|**datetime**|Last time a client process executed a remote stored procedure call or an EXECUTE statement.|  
|ecid|**smallint**|Execution context ID used to uniquely identify the subthreads operating on behalf of a single process.|  
|open_tran|**smallint**|Number of open transactions for the process.|  
|status|**nchar(30)**|Process ID status. The possible values are:<br /><br /> **dormant** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is resetting the session.<br /><br /> **running** = The session is running one or more batches. When Multiple Active Result Sets (MARS) is enabled, a session can run multiple batches. For more information, see [Using Multiple Active Result Sets &#40;MARS&#41;](../../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md).<br /><br /> **background** = The session is running a background task, such as deadlock detection.<br /><br /> **rollback** = The session has a transaction rollback in process.<br /><br /> **pending** = The session is waiting for a worker thread to become available.<br /><br /> **runnable** = The task in the session is in the runnable queue of a scheduler while waiting to get a time quantum.<br /><br /> **spinloop** = The task in the session is waiting for a spinlock to become free.<br /><br /> **suspended** = The session is waiting for an event, such as I/O, to complete.|  
|sid|**binary(86)**|Globally unique identifier (GUID) for the user.|  
|hostname|**nchar(128)**|Name of the workstation.|  
|program_name|**nchar(128)**|Name of the application program.|  
|hostprocess|**nchar(10)**|Workstation process ID number.|  
|cmd|**nchar(16)**|Command currently being executed.|  
|nt_domain|**nchar(128)**|Windows domain for the client, if using Windows Authentication, or a trusted connection.|  
|nt_username|**nchar(128)**|Windows user name for the process, if using Windows Authentication, or a trusted connection.|  
|net_address|**nchar(12)**|Assigned unique identifier for the network adapter on the workstation of each user. When a user logs in, this identifier is inserted in the net_address column.|  
|net_library|**nchar(12)**|Column in which the client's network library is stored. Every client process comes in on a network connection. Network connections have a network library associated with them that enables them to make the connection.|  
|loginame|**nchar(128)**|Login name.|  
|context_info|**binary(128)**|Data stored in a batch by using the SET CONTEXT_INFO statement.|  
|sql_handle|**binary(20)**|Represents the currently executing batch or object.<br /><br /> **Note** This value is derived from the batch or memory address of the object. This value is not calculated by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] hash-based algorithm.|  
|stmt_start|**int**|Starting offset of the current SQL statement for the specified sql_handle.|  
|stmt_end|**int**|Ending offset of the current SQL statement for the specified sql_handle.<br /><br /> -1 = Current statement runs to the end of the results returned by the fn_get_sql function for the specified sql_handle.|  
|request_id|**int**|ID of request. Used to identify requests running in a specific session.|
|page_resource |**binary(8)** |**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] <br /><br /> An 8-byte hexadecimal representation of the page resource if the `waitresource` column contains a page. |  
  
## Remarks  
 If a user has VIEW SERVER STATE permission on the server, the user will see all executing sessions in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; otherwise, the user will see only the current session.  
  
## See Also  
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
